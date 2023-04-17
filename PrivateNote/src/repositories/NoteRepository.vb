Public Class NoteRepository

    Private ReadOnly sqlTools As SQLiteTools
    Private ReadOnly cryptoTools As CryptoTools

    Public Sub New(ByRef sqliteTools As SQLiteTools, ByRef cryptoTools As CryptoTools)
        Me.sqlTools = sqliteTools
        Me.cryptoTools = cryptoTools
    End Sub

    Public Sub CreateTableIfNotExists()
        Dim sql = ""
        sql &= " CREATE TABLE IF NOT EXISTS ""note"" ( "
        sql &= """id""	TEXT Not NULL, "
        sql &= """name""	BLOB NOT NULL, "
        sql &= """content""	BLOB NOT NULL, "
        sql &= """ctime""	TEXT Not NULL, "
        sql &= """utime""	TEXT Not NULL, "
        sql &= "PRIMARY KEY(""id"") "
        sql &= "); "
        sqlTools.doUpdate(sql)
    End Sub

    Public Function GetNoteInfos() As List(Of NoteInfoBean)
        Dim result As New List(Of NoteInfoBean)

        Dim sql = "SELECT id, name, ctime, utime FROM note"
        Dim rows = sqlTools.doSelect(sql)

        For Each row In rows
            Dim noteInfoBean = New NoteInfoBean
            noteInfoBean.id = row("id")
            noteInfoBean.name = cryptoTools.DecryptData(row("name"))
            noteInfoBean.ctime = row("ctime")
            noteInfoBean.utime = row("utime")
            result.Add(noteInfoBean)
        Next

        Return result
    End Function

    Public Function GetNoteEntity(ByVal id As String) As NoteEntity
        Dim sql = "SELECT * FROM note WHERE id=@id"

        Dim params As New Dictionary(Of String, Object)
        params.Add("@id", id)

        Dim rows = sqlTools.doSelect(sql, params)
        If rows.Count > 0 Then
            Dim row = rows(0)
            Dim noteEntity As New NoteEntity
            noteEntity.id = row("id")
            noteEntity.name = cryptoTools.DecryptData(row("name"))
            noteEntity.content = cryptoTools.DecryptData(row("content"))
            noteEntity.ctime = row("ctime")
            noteEntity.utime = row("utime")
            Return noteEntity
        Else
            Return Nothing
        End If
    End Function

    Public Function NoteExists(ByVal id As String) As Boolean
        Dim sql = "SELECT id FROM note WHERE id=@id"

        Dim params As New Dictionary(Of String, Object)
        params.Add("@id", id)

        Return sqlTools.doSelect(sql, params).Count > 0
    End Function

    Public Sub Save(ByRef noteEntity As NoteEntity)

        If (noteEntity.id = Nothing) Then
            noteEntity.id = Guid.NewGuid().ToString()
            noteEntity.ctime = DateTools.getCurrentTimeNumberString()
        End If

        noteEntity.utime = DateTools.getCurrentTimeNumberString()

        Dim sql As String

        If NoteExists(noteEntity.id) Then
            sql = "UPDATE note SET content=@content, utime=@utime WHERE id=@id"
        Else
            sql = "INSERT INTO note (id, name, content, ctime, utime) VALUES (@id, @name, @content, @ctime, @utime)"
        End If

        Dim param As New Dictionary(Of String, Object)

        param.Add("@id", noteEntity.id)
        param.Add("@name", cryptoTools.EncryptData(noteEntity.name))
        param.Add("@content", cryptoTools.EncryptData(noteEntity.content))
        param.Add("@ctime", noteEntity.ctime)
        param.Add("@utime", noteEntity.utime)

        sqlTools.doUpdate(sql, param)

    End Sub

End Class
