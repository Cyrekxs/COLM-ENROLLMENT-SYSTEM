Public Class payment_reports_medical_arts

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim RecieptNo As String = txtRecieptNo.Text
        Dim TransactionType As String = cmbFeeType.Text
        Dim RecieptStatus As String = cmbRecieptStatus.Text
        Dim EducationLevel As String = txtEducationLevel.Text
        Dim DateFrom As DateTime = Nothing
        Dim DateTo As DateTime = Nothing
        Dim TotalAmount As Double = 0
        Dim CancelledCount As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM vw_nonstudent_collection_reports WHERE AccountType = @education_level AND Fee_Description LIKE @FeeCode AND Fee_Type = @FeeType AND Reciept_Status = @ReceiptStatus AND DATE_RECEIVED BETWEEN @date1 AND @date2", conn)
                DateFrom = DateTimePicker1.Text & " 12:00:01 AM"
                DateTo = DateTimePicker2.Text & " 11:59:59 PM"

                comm.Parameters.AddWithValue("@date1", DateFrom)
                comm.Parameters.AddWithValue("@date2", DateTo)
                If cmbFilter.Text = "ALL" Or cmbFilter.Text = "" Then
                    comm.Parameters.AddWithValue("@FeeCode", "%%")
                Else
                    comm.Parameters.AddWithValue("@FeeCode", "%" & cmbFilter.Text & "%")
                End If

                comm.Parameters.AddWithValue("@FeeType", cmbFeeType.Text)
                comm.Parameters.AddWithValue("@ReceiptStatus", cmbRecieptStatus.Text)
                comm.Parameters.AddWithValue("@education_level", EducationLevel)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    TotalAmount = 0
                    CancelledCount = 0

                    While reader.Read
                        DataGridView1.Rows.Add(Format(CDate(reader("DATE_RECEIVED")), "MM-dd-yyyy"),
                                               reader("FEE_TYPE"),
                                               reader("RECIEPT_NUMBER"),
                                               reader("RECIEPT_STATUS"),
                                               reader("Fee_Description"),
                                               Convert_To_Currency(reader("AMOUNT_COLLECTED")),
                                               reader("AccountName"),
                                               reader("RECEIVER"))
                        If reader("RECIEPT_STATUS") = "ACTIVE" Then
                            TotalAmount += CDbl(reader("Amount_Collected"))
                        Else
                            CancelledCount += 1
                        End If
                    End While
                End Using
            End Using
        End Using
        lblTotalResults.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim DS As New DS_PaymentTransactions
        Dim DR As DataRow
        Dim param_ReportTitle As ReportParameter = New ReportParameter("ReportTitle", "TOTAL COLLECTIONS IN NON STUDENT ACCOUNTS")
        Dim param_DateFromTo As ReportParameter = New ReportParameter("DateFromTo", "DATE FROM " & DateTimePicker1.Value.ToString & " TO " & DateTimePicker2.Value.ToString)
        Dim param_TransactionType As ReportParameter = New ReportParameter("TransactionType", "TRANSACTION TYPE: " & cmbFeeType.Text)
        Dim param_EducationLevel As ReportParameter = New ReportParameter("EducationLevel", "EDUCATION LEVEL: " & txtEducationLevel.Text)
        Dim param_DatePrinted As ReportParameter = New ReportParameter("DatePrinted", Date.Now.ToString)
        Dim amount As Double = 0
        Dim Cancelled As Integer = 0
        DS.Tables("DT_PaymentTransactions").Rows.Clear()

        With DS.Tables("DT_PaymentTransactions")
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("TXDate") = DataGridView1.Rows(i).Cells(0).Value
                DR("TXType") = DataGridView1.Rows(i).Cells(1).Value
                DR("RecieptNo") = DataGridView1.Rows(i).Cells(2).Value

                If DataGridView1.Rows(i).Cells(3).Value = "ACTIVE" Then
                    DR("RecieptStatus") = ""
                    amount += DataGridView1.Rows(i).Cells(5).Value
                Else
                    DR("RecieptStatus") = DataGridView1.Rows(i).Cells(3).Value
                    Cancelled += 1
                End If

                DR("Amount") = DataGridView1.Rows(i).Cells(5).Value
                DR("StudentName") = DataGridView1.Rows(i).Cells(6).Value
                DR("EducationLevel") = DataGridView1.Rows(i).Cells(4).Value
                .Rows.Add(DR)
            Next
        End With

        Dim param_TotalAmount As ReportParameter = New ReportParameter("TotalAmount", Convert_To_Currency(amount).ToString)
        Dim param_CancelledReciept As ReportParameter = New ReportParameter("CancelledReciept", Cancelled.ToString)

        Dim MyReport As New ReportDataSource("DSPaymentTransactions", DS.Tables("DT_PaymentTransactions"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.payment_reports_tuition_and_other_fees.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_ReportTitle, param_DateFromTo, param_TransactionType, param_EducationLevel, param_CancelledReciept, param_TotalAmount, param_DatePrinted})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub
End Class
