Public Class frm_edit_non_student_fee
    Public AccountFeeID = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim oldamount As Double = txtOldAmount.Text
        Dim payment As Double = txtPayment.Text
        Dim newamount As Double = txtNewAmount.Text

        If (newamount) < (payment) Then
            MessageBox.Show("Entered amount not allowed", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If


        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("UPDATE tbl_nonstudent_account SET TotalAmount = @Amount WHERE AccountFeeID = @AccountFeeID", conn)
                comm.Parameters.AddWithValue("@AccountFeeID", AccountFeeID)
                comm.Parameters.AddWithValue("@Amount", txtNewAmount.Text)
                comm.ExecuteNonQuery()
                MessageBox.Show("Amount has been successfully updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
                Dispose()
            End Using
        End Using
    End Sub
End Class