Public Class frm_voucher_account_transaction_entry
    Private VoucherAccountID As Integer
    Private VoucherID As Integer
    Private SavingStatus As SavingOptions

    Private Sub LoadVoucherInfo()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_transaction_voucher WHERE VoucherID = @VoucherID", conn)
                comm.Parameters.AddWithValue("@VoucherID", VoucherID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtVoucherDescription.Text = reader("VoucherDesc")
                        txtVoucherAmount.Text = Convert_To_Currency(CDbl(reader("VoucherAmount")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadVouchersDescription()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT VoucherDesc FROM tbl_transaction_voucher ORDER BY VoucherDesc ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtVoucherDescription.AutoCompleteCustomSource.Add(reader("VoucherDesc"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Sub New(VoucherID As Integer, VoucherAccountID As Integer, SaveStatus As SavingOptions)
        InitializeComponent()
        Me.VoucherAccountID = VoucherAccountID
        Me.VoucherID = VoucherID
        SavingStatus = SaveStatus
        LoadVouchersDescription()
        If SavingStatus = SavingOptions.EDIT Then
            LoadVoucherInfo()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtVoucherDescription.Text = String.Empty Then
            MsgBox("Please enter description!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If IsNumeric(txtVoucherAmount.Text) = False Then
            MsgBox("Please enter a valid amount!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = SavingOptions.NEW Then
                Using comm As New SqlCommand("INSERT INTO tbl_transaction_voucher VALUES (@VoucherAccountID,@VoucherDesc,@VoucherAmount,GETDATE(),NULL,@IsSettled)", conn)
                    comm.Parameters.AddWithValue("@VoucherAccountID", VoucherAccountID)
                    comm.Parameters.AddWithValue("@VoucherDesc", txtVoucherDescription.Text)
                    comm.Parameters.AddWithValue("@VoucherAmount", CDbl(txtVoucherAmount.Text))
                    comm.Parameters.AddWithValue("@IsSettled", False)
                    comm.ExecuteNonQuery()
                    MsgBox("Voucher transaction has been successfully saved!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End Using
            ElseIf SavingStatus = SavingOptions.EDIT Then
                Using comm As New SqlCommand("UPDATE tbl_transaction_voucher SET VoucherDesc = @VoucherDesc, VoucherAmount = @VoucherAmount WHERE VoucherID = @VoucherID", conn)
                    comm.Parameters.AddWithValue("@VoucherID", VoucherID)
                    comm.Parameters.AddWithValue("@VoucherAccountID", VoucherAccountID)
                    comm.Parameters.AddWithValue("@VoucherDesc", txtVoucherDescription.Text)
                    comm.Parameters.AddWithValue("@VoucherAmount", CDbl(txtVoucherAmount.Text))
                    comm.Parameters.AddWithValue("@IsSettled", False)
                    comm.ExecuteNonQuery()
                    MsgBox("Voucher transaction has been successfully updated!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End Using
            End If
        End Using
    End Sub

End Class