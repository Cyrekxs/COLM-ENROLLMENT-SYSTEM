Public Class frm_voucher_report
    Private Sub LoadSettledAccounts()
        Dim TotalSummary As Double = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM vw_voucher_summary WHERE IsSettled = @IsSettled AND DateSettled BETWEEN @date1 AND @date2 AND AccountName LIKE @search ORDER BY DateSettled ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@IsSettled", True)
                comm.Parameters.AddWithValue("@date1", dtFrom.Text & " 12:00:01 AM")
                comm.Parameters.AddWithValue("@date2", dtTO.Text & " 11:59:59 PM")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("VoucherID"),
                                               reader("AccountName"),
                                               reader("VoucherDesc"),
                                              Convert_To_Currency(reader("VoucherAmount")),
                                               Format(reader("DateCreated"), "MM-dd-yyyy"),
                                               Format(reader("DateSettled"), "MM-dd-yyyy"))
                        TotalSummary += CDbl(reader("VoucherAmount"))
                    End While
                End Using
            End Using
        End Using
        txtTotalSettled.Text = Convert_To_Currency(TotalSummary)
    End Sub

    Private Sub LoadUnsettledAccounts()
        Dim TotalUnsettledAccounts As Double = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM vw_voucher_summary WHERE IsSettled = @IsSettled AND AccountName LIKE @search ORDER BY DateSettled ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@IsSettled", False)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("VoucherID"),
                                               reader("AccountName"),
                                               reader("VoucherDesc"),
                                              Convert_To_Currency(reader("VoucherAmount")),
                                               Format(reader("DateCreated"), "MM-dd-yyyy"))
                        TotalUnsettledAccounts += CDbl(reader("VoucherAmount"))
                    End While
                End Using
            End Using
        End Using
        txtTotalUnsettled.Text = Convert_To_Currency(TotalUnsettledAccounts)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadSettledAccounts()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim DS As New DS_Voucher
        Dim DR As DataRow
        Dim param_datefromto As New ReportParameter("datefromto", "FROM: " & dtFrom.Text & " TO: " & dtTO.Text)
        Dim param_totalamount As New ReportParameter("totalamount", Convert_To_Currency(txtTotalSettled.Text).ToString)
        With DS.Tables("DT_Voucher")
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("AccountName") = DataGridView1.Rows(i).Cells(1).Value
                DR("VoucherDesc") = DataGridView1.Rows(i).Cells(2).Value
                DR("VoucherAmount") = DataGridView1.Rows(i).Cells(3).Value
                DR("DateCreated") = DataGridView1.Rows(i).Cells(4).Value
                DR("DateSettled") = DataGridView1.Rows(i).Cells(5).Value
                .Rows.Add(DR)
            Next
        End With

        Dim MyReport As New ReportDataSource("DataSet1", DS.Tables("DT_Voucher"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_voucher.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_datefromto, param_totalamount})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadSettledAccounts()
        LoadUnsettledAccounts()
    End Sub

    Private Sub frm_voucher_report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSettledAccounts()
        LoadUnsettledAccounts()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSearch.PerformClick()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub
End Class