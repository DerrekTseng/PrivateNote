
Imports System.Data.SQLite
Public Class SQLiteTools

    Private ReadOnly Connection As SQLiteConnection

    Private opened As Boolean = False

    Public Sub New(ByVal path As String)
        Connection = New SQLiteConnection("Data Source=" & path)
    End Sub

    Public Function doSelect(ByVal sql As String) As List(Of Dictionary(Of String, Object))
        Dim defaultParams As New Dictionary(Of String, Object)
        Return doSelect(sql, defaultParams)
    End Function

    Public Function doUpdate(ByVal sql As String) As Integer
        Dim defaultParams As New Dictionary(Of String, Object)
        Return doUpdate(sql, defaultParams)
    End Function


    Public Function doSelect(ByVal sql As String, ByRef params As Dictionary(Of String, Object)) As List(Of Dictionary(Of String, Object))
        Dim rows As New List(Of Dictionary(Of String, Object))
        Using sqlCommand As New SQLiteCommand(sql, Connection)
            For Each entry In params
                sqlCommand.Parameters.AddWithValue(entry.Key, entry.Value)
            Next
            Using executeReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While executeReader.Read
                    Dim row As New Dictionary(Of String, Object)
                    For fieldCount = 0 To executeReader.FieldCount - 1
                        Dim name = executeReader.GetName(fieldCount)
                        Dim value = executeReader.GetValue(fieldCount)
                        row.Add(name, value)
                    Next
                    rows.Add(row)
                End While
            End Using
        End Using
        Return rows
    End Function

    Public Function doUpdate(ByVal sql As String, ByRef params As Dictionary(Of String, Object)) As Integer
        Dim effectRows As Integer
        Using sqlCommand As New SQLiteCommand(sql, Connection)
            For Each entry In params
                sqlCommand.Parameters.AddWithValue(entry.Key, entry.Value)
            Next
            effectRows = sqlCommand.ExecuteNonQuery()
        End Using
        Return effectRows
    End Function

    Public Sub Open()
        Connection.Open()
        opened = True
    End Sub

    Public Sub Close()
        opened = False
        Connection.Close()
    End Sub

    Public Function isOpened() As Boolean
        Return opened
    End Function

End Class
