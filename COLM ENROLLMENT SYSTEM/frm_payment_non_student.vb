Public Class frm_payment_non_student

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        With frm_payment_non_student_browse_payer
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub frm_payment_non_student_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class