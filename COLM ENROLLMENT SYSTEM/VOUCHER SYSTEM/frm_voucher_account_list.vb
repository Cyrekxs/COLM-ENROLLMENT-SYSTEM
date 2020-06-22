Public Class frm_voucher_account_list

    Private Sub LoadVoucherAccounts()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT VoucherAccountID,AccountName,ISNULL((SELECT SUM(VoucherAmount) FROM vw_voucher_summary WHERE IsSettled = 'False' AND VoucherAccountID = MAIN.VoucherAccountID),0) AS UnsettledAmount FROM tbl_settings_voucher_accounts AS MAIN ORDER BY AccountName ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("VoucherAccountID"), reader("AccountName"), Convert_To_Currency(reader("UnsettledAmount")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New frm_voucher_account_entry(0, SavingOptions.NEW)
        With frm
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
        LoadVoucherAccounts()
    End Sub

    Private Sub frm_voucher_account_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadVoucherAccounts()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Select Case e.ColumnIndex
            Case btnTransaction.Index
                Dim frm As New frm_voucher_account_transaction(DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                With frm
                    .StartPosition = FormStartPosition.CenterScreen
                    .Text = "TRANSACTION | " & DataGridView1.Rows(e.RowIndex).Cells(1).Value
                    .ShowDialog()
                End With
        End Select
    End Sub
End Class