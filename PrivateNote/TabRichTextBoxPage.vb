Public Class TabRichTextBoxPage

    Public Sub New(ByVal noteEntity As NoteEntity)
        Me.New()
        Me.Text = noteEntity.name
        Me.RichTextBox1.Text = noteEntity.content
    End Sub

End Class
