Public Class frm_payment_edit_other_fees
    Public FeeLoadID As Integer = 0
    Public CurrentPayment As Double = 0

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        If IsNumeric(txtNewFeeAmount.Text) = False Then
            MsgBox("Please enter valid amount!", MsgBoxStyle.Critical)
            txtNewFeeAmount.Focus()
            Exit Sub
        End If

        If CDbl(txtNewFeeAmount.Text) < CurrentPayment Then
            MsgBox("New Fee Amount should not be less than in payment!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("UPDATE TBL_COLLEGE_FEE_LOADS SET FEE_AMOUNT = @fee_amount WHERE Ref_Code = @ref_code", conn)
                comm.Parameters.AddWithValue("@ref_code", txtRefCode.Text)
                comm.Parameters.AddWithValue("@fee_amount", txtNewFeeAmount.Text)
                If MsgBox("Are you sure you want to save this as new amount?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    comm.ExecuteNonQuery()
                    MsgBox("Amount has been successfully updated!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End If
            End Using
        End Using
    End Sub
End Class