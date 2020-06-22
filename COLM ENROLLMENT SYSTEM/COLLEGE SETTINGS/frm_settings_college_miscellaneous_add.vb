Public Class frm_settings_college_miscellaneous_add
    Public CurriculumID As Integer = 0
    Public CourseCode As String
    Public Yrlvl As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("INSERT INTO tbl_settings_fees VALUES (@Course_Code,@Yrlvl,@Fee_Code,@Fee_Amount,@Fee_Type,@Fee_Status,@Education_Level,@CurriculumID,@ay,@sem)", conn)
                comm.Parameters.AddWithValue("@Course_Code", CourseCode)
                comm.Parameters.AddWithValue("@Yrlvl", Yrlvl)
                comm.Parameters.AddWithValue("@Fee_Code", txtMFeeCode.Text)
                comm.Parameters.AddWithValue("@Fee_Amount", txtMFeeAmount.Text)
                comm.Parameters.AddWithValue("@Fee_Type", "MISCELLANEOUS FEE")
                comm.Parameters.AddWithValue("@Fee_Status", "EXTERNAL")
                comm.Parameters.AddWithValue("@Education_Level", "COLLEGE")
                comm.Parameters.AddWithValue("@CurriculumID", CurriculumID)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.ExecuteNonQuery()
                MsgBox("Miscellaneous has been successfully saved!", MsgBoxStyle.Information)
                Me.Close()
                Me.Dispose()
            End Using
        End Using
    End Sub
End Class