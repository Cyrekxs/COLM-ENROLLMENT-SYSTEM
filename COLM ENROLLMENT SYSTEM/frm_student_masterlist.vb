Public Class frm_student_masterlist

    Private Sub LoadStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_INFORMATION INNER JOIN TBL_STUDENT_REGISTERED ON TBL_STUDENT_INFORMATION.STUDENT_NUMBER =
            End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_student_information_registration
            .Show()
            Me.Close()
        End With
    End Sub

    Private Sub frm_student_masterlist_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class