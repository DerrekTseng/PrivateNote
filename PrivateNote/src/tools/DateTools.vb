Module DateTools

    Public Function getCurrentTimeNumberString() As String
        Return Now.ToString("yyyyMMddHHmmss")
    End Function

    Public Function formatTimeNumberString(ByVal timeNumberString As String) As String
        Dim d = DateTime.ParseExact(timeNumberString, "yyyyMMddHHmmss", Nothing)
        Return d.ToString("yyyy-MM-dd HH:mm:ss")
    End Function

End Module
