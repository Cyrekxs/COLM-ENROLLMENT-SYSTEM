Public Class frm_voucher_account_entry
    Private SavingStatus As SavingOptions
    Private VoucherAccountID As Integer

    Private Sub GetAccountInfo()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_voucher_accounts WHERE VoucherAccountID = @VoucherAccountID", conn)
                comm.Parameters.AddWithValue("@VoucherAccountID", VoucherAccountID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        TextBox1.Text = reader("AccountName")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Sub New(VoucherAccountID As Integer, SaveStatus As SavingOptions)
        InitializeComponent()
        Me.VoucherAccountID = VoucherAccountID
        SavingStatus = SaveStatus

        If SavingStatus = SavingOptions.EDIT Then
            GetAccountInfo()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            If SavingStatus = SavingOptions.NEW Then
                Using comm As New SqlCommand("INSERT INTO tbl_settings_voucher_accounts VALUES (@AccountName,GETDATE())", conn)
                    comm.Parameters.AddWithValue("@AccountName", TextBox1.Text)
                    comm.ExecuteNonQuery()
                    MsgBox("Account has been successfully saved!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End Using
            ElseIf SavingStatus = SavingOptions.EDIT Then
                Using comm As New SqlCommand("UPDATE tbl_settings_voucher_accounts SET AccountName = @AccountName WHERE VoucherAccountID = @VoucherAccountID", conn)
                    comm.Parameters.AddWithValue("@VoucherAccountID", VoucherAccountID)
                    comm.Parameters.AddWithValue("@AccountName", TextBox1.Text)
                    comm.ExecuteNonQuery()
                    MsgBox("Account has been successfully updated!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End Using
            End If
        End Using
    End Sub
End Class