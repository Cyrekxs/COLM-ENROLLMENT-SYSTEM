Public Module Program_Variables
    Public Account_ID As String = ""
    Public Account_Name As String = ""
    Public Account_Position As String = ""
    Public Account_Date_Time As DateTime
    Public Academic_Year As String = String.Empty
    Public Academic_Sem As String = String.Empty
    Public End_of_Enrollment As DateTime = Nothing

    Enum SavingOptions
        [NEW]
        [EDIT]
    End Enum

End Module
