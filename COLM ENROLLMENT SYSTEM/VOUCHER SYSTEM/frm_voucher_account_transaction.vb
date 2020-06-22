Public Class frm_voucher_account_transaction
    Private VoucherAccountID As Integer
    Private SelectedRow As Integer
    Sub New(VoucherAccountID As Integer)
        InitializeComponent()
        Me.VoucherAccountID = VoucherAccountID
    End Sub

    Private Function ConvertBooleanToStatus(Status As Boolean)
        If Status = False Then
            Return "UNSETTLED"
        Else
            Return "SETTLED"
        End If
    End Function
    Private Sub LoadVoucherTransactions()
        Dim UnsettledAmount As Double = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_transaction_voucher WHERE VoucherAccountID = @VoucherAccountID ORDER BY VoucherID DESC", conn)
                comm.Parameters.AddWithValue("@VoucherAccountID", VoucherAccountID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        If IsDBNull(reader("DateSettled")) = True Then
                            DataGridView1.Rows.Add(reader("VoucherID"),
                                                   reader("VoucherDesc"),
                                                   Convert_To_Currency(reader("VoucherAmount")),
                                                   Format(reader("DateCreated"), "MM-dd-yyyy"),
                                                   ConvertBooleanToStatus(reader("IsSettled")),
                                                   "-")
                            UnsettledAmount += reader("VoucherAmount")
                        Else
                            DataGridView1.Rows.Add(reader("VoucherID"),
                                                   reader("VoucherDesc"),
                                                   Convert_To_Currency(reader("VoucherAmount")),
                                                   Format(reader("DateCreated"), "MM-dd-yyyy"),
                                                   ConvertBooleanToStatus(reader("IsSettled")),
                                                   Format(reader("DateSettled"), "MM-dd-yyyy"))
                        End If
                    End While
                End Using
            End Using
        End Using


        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(4).Value = "UNSETTLED" Then
                DataGridView1.Rows(i).DefaultCellStyle.ForeColor = Color.Red
            Else
                DataGridView1.Rows(i).DefaultCellStyle.ForeColor = Color.Green
            End If
        Next
        txtUnsettledAmount.Text = Convert_To_Currency(UnsettledAmount)
    End Sub

    Private Sub frm_voucher_account_transaction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadVoucherTransactions()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New frm_voucher_account_transaction_entry(0, VoucherAccountID, SavingOptions.NEW)
        With frm
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
        LoadVoucherTransactions()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("Are you sure you want to mark this as settled?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("UPDATE tbl_transaction_voucher SET IsSettled = @IsSettled, DateSettled = GETDATE() WHERE VoucherID = @VoucherID", conn)
                    comm.Parameters.AddWithValue("@IsSettled", True)
                    comm.Parameters.AddWithValue("@VoucherID", DataGridView1.Rows(SelectedRow).Cells(0).Value)
                    comm.ExecuteNonQuery()
                    MsgBox("Voucher has been successfully settled!", MsgBoxStyle.Information)
                    LoadVoucherTransactions()
                End Using
            End Using
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        SelectedRow = e.RowIndex
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = btnEdit.Index Then
            If DataGridView1.Rows(e.RowIndex).Cells(4).Value = "SETTLED" Then
                MsgBox("Settled Transactions are not permitted to edit the amount!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim frm As New frm_voucher_account_transaction_entry(DataGridView1.Rows(e.RowIndex).Cells(0).Value, VoucherAccountID, SavingOptions.EDIT)
            With frm
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                LoadVoucherTransactions()
            End With
        End If
    End Sub
End Class