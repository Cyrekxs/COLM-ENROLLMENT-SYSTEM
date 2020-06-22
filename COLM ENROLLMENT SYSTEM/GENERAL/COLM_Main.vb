Public Class COLM_Main
    Public Sub RestoreDefaultControls()
        btnHome.Image = My.Resources.Home
        btnHome.BackColor = Color.White
        btnHome.ForeColor = Color.Black

        btnStudents.Image = My.Resources.Students

        btnRegistration.Image = My.Resources.Registration

        btnAssessment.Image = My.Resources.Assessment

        btnPayment.Image = My.Resources.Payment

        btnSettings.Image = My.Resources.Settings

        btnAccount.Image = My.Resources.Account

    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        RestoreDefaultControls()
        btnHome.Image = My.Resources.Home_White
        btnHome.ForeColor = Color.White
        btnHome.BackColor = Color.SeaGreen
    End Sub

    Private Sub btnStudents_Click(sender As Object, e As EventArgs) Handles btnStudents.Click
        RestoreDefaultControls()
        btnStudents.Image = My.Resources.Students_Green
    End Sub

    Private Sub btnRegistration_Click(sender As Object, e As EventArgs) Handles btnRegistration.Click
        RestoreDefaultControls()
        btnRegistration.Image = My.Resources.Registration_Green
    End Sub

    Private Sub btnAssessment_Click(sender As Object, e As EventArgs) Handles btnAssessment.Click
        RestoreDefaultControls()
        btnAssessment.Image = My.Resources.Assessment_Green
    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        RestoreDefaultControls()
        btnPayment.Image = My.Resources.Payment_Green
    End Sub
End Class