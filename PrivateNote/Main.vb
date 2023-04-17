Public Class Main
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim args As String() = Environment.GetCommandLineArgs()

        If args.Length > 1 Then
            Dim path As String = args(1)
            If My.Computer.FileSystem.FileExists(path) Then
                ' 這裡要開啟 db
            End If
        Else
            ' 這裡要顯示讀取檔案視窗
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class
