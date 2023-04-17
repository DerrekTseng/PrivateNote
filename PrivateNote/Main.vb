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

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Dim index As Integer = 0
    Private Sub ButtonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ButtonToolStripMenuItem.Click
        Dim noteEntity As New NoteEntity()
        noteEntity.name = "title_" & index
        noteEntity.content = "content"

        TabControl1.TabPages.Add(New TabRichTextBoxPage(noteEntity))
        index += 1
    End Sub

    Private Sub TabControl1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles TabControl1.DrawItem
        e.Graphics.DrawString("❎", e.Font, Brushes.Black, e.Bounds.Right - 20, e.Bounds.Top + 6)
        If TabControl1.TabPages.IndexOf(Me.TabControl1.SelectedTab) = e.Index Then
            e.Graphics.DrawString(TabControl1.TabPages(e.Index).Text, e.Font, Brushes.Black, e.Bounds.Left + 4, e.Bounds.Top + 4)
        Else
            e.Graphics.DrawString(TabControl1.TabPages(e.Index).Text, e.Font, Brushes.Black, e.Bounds.Left + 2, e.Bounds.Top + 4)
        End If
        e.DrawFocusRectangle()
    End Sub

    Private Sub TabControl1_MouseDown(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseDown
        If e.Button = MouseButtons.Left Then
            For i As Integer = 0 To TabControl1.TabPages.Count - 1
                Dim r As Rectangle = TabControl1.GetTabRect(i)
                Dim closeButton As Rectangle = New Rectangle(r.Right - 15, r.Top + 5, 15, 12)
                If closeButton.Contains(e.Location) Then
                    If MessageBox.Show("Close form?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        TabControl1.TabPages.RemoveAt(i)
                        Exit Sub
                    End If
                End If
            Next
        ElseIf e.Button = MouseButtons.Middle Then
            For i As Integer = 0 To TabControl1.TabPages.Count - 1
                If TabControl1.GetTabRect(i).Contains(e.Location) Then
                    If MessageBox.Show("Close form?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        TabControl1.TabPages.RemoveAt(i)
                        Exit Sub
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub TabControl1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseDoubleClick
        If e.Button = MouseButtons.Left Then
            For i As Integer = 0 To TabControl1.TabPages.Count - 1
                If TabControl1.GetTabRect(i).Contains(e.Location) Then
                    MsgBox(TabControl1.TabPages(i).Text)
                    Exit Sub
                End If
            Next
        End If
    End Sub
End Class
