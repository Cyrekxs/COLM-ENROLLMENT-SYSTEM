Public Class payment_reports_other_fees
    Public TotalAmount As Double = 0
    Public CancelledCount As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim RecieptNo As String = txtRecieptNo.Text
        Dim TransactionType As String = cmbTransactionType.Text
        Dim RecieptStatus As String = cmbRecieptStatus.Text
        Dim EducationLevel As String = cmbEducationLevel.Text
        Dim DateFrom As DateTime = Nothing
        Dim DateTo As DateTime = Nothing

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT STUDENT_NUMBER,(SELECT TOP 1 LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + EXTENSION_NAME END + ' ' + FIRSTNAME + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + MIDDLENAME END FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = TBL_COLLEGE_PAYMENT.STUDENT_NUMBER) AS STUDENT_NAME,FEE_TYPE,RECIEPT_NUMBER,RECIEPT_STATUS,SUM(AMOUNT_COLLECTED) AS AMOUNT_COLLECTED,RECEIVER,CONVERT(char(10),DATE_RECEIVED,126) AS DATE_RECIEVED,EDUCATION_LEVEL FROM TBL_COLLEGE_PAYMENT WHERE RECIEPT_NUMBER LIKE @reciept_no AND FEE_STATUS = 'OTHER FEES (INTERNAL)' AND FEE_TYPE LIKE @transaction_type AND RECIEPT_STATUS LIKE @reciept_status AND EDUCATION_LEVEL LIKE @education_level AND DATE_RECEIVED BETWEEN @date1 AND @date2 GROUP BY STUDENT_NUMBER,FEE_TYPE,RECIEPT_NUMBER,RECIEPT_STATUS,RECEIVER,CONVERT(char(10),DATE_RECEIVED,126),EDUCATION_LEVEL ORDER BY RECIEPT_NUMBER ASC", conn)
                DateFrom = DateTimePicker1.Text & " 12:00:01 AM"
                DateTo = DateTimePicker2.Text & " 11:59:59 PM"

                If RecieptNo = String.Empty Then
                    RecieptNo = "%%"
                Else
                    RecieptNo = RecieptNo & "%"
                End If

                If TransactionType = "ALL" Then
                    TransactionType = "%%"
                End If

                If RecieptStatus = "ALL" Then
                    RecieptStatus = "%%"
                End If

                If EducationLevel = "ALL" Then
                    EducationLevel = "%%"
                End If
                comm.Parameters.AddWithValue("@date1", DateFrom)
                comm.Parameters.AddWithValue("@date2", DateTo)
                comm.Parameters.AddWithValue("@transaction_type", TransactionType)
                comm.Parameters.AddWithValue("@reciept_no", RecieptNo)
                comm.Parameters.AddWithValue("@reciept_status", RecieptStatus)
                comm.Parameters.AddWithValue("@education_level", EducationLevel)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    TotalAmount = 0
                    CancelledCount = 0
                    While reader.Read
                        DataGridView1.Rows.Add(Format(CDate(reader("DATE_RECIEVED")), "MM-dd-yyyy"), reader("FEE_TYPE"), reader("RECIEPT_NUMBER"), reader("RECIEPT_STATUS"), Convert_To_Currency(reader("AMOUNT_COLLECTED")), reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("EDUCATION_LEVEL"), reader("RECEIVER"))
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

    Private Sub payment_reports_tuition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtRecieptNo.Text = String.Empty
        cmbTransactionType.SelectedIndex = 0
        cmbRecieptStatus.SelectedIndex = 0
        cmbEducationLevel.SelectedIndex = 0
        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim DS As New DS_PaymentTransactions
        Dim DR As DataRow
        Dim param_ReportTitle As ReportParameter = New ReportParameter("ReportTitle", "TOTAL COLLECTIONS IN OTHER FEE")
        Dim param_DateFromTo As ReportParameter = New ReportParameter("DateFromTo", "DATE FROM " & DateTimePicker1.Value.ToString & " TO " & DateTimePicker2.Value.ToString)
        Dim param_TransactionType As ReportParameter = New ReportParameter("TransactionType", "TRANSACTION TYPE: " & cmbTransactionType.Text)
        Dim param_EducationLevel As ReportParameter = New ReportParameter("EducationLevel", "EDUCATION LEVEL: " & cmbEducationLevel.Text)
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
                    amount += DataGridView1.Rows(i).Cells(4).Value
                Else
                    DR("RecieptStatus") = DataGridView1.Rows(i).Cells(3).Value
                    Cancelled += 1
                End If

                DR("Amount") = DataGridView1.Rows(i).Cells(4).Value
                DR("StudentNo") = DataGridView1.Rows(i).Cells(5).Value
                DR("StudentName") = DataGridView1.Rows(i).Cells(6).Value
                DR("EducationLevel") = DataGridView1.Rows(i).Cells(7).Value
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


        'Dim MyReport As New payment_reports_tuition_and_other_fees
        'MyReport.Load(Application.StartupPath & "/payment_reports_tuition_and_other_fees.rpt")
        'MyReport.SetDataSource(DS.Tables("DT_PaymentTransactions"))

        'Dim myTextObjectOnReport As CrystalDecisions.CrystalReports.Engine.TextObject
        'myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtReportTitle"), CrystalDecisions.CrystalReports.Engine.TextObject)
        'myTextObjectOnReport.Text = "OTHER FEES PAYMENT TRANSACTIONS"

        'myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtDateFromTo"), CrystalDecisions.CrystalReports.Engine.TextObject)
        'myTextObjectOnReport.Text = Format(DateTimePicker1.Value, "MM-dd-yyyy") & " - " & Format(DateTimePicker2.Value, "MM-dd-yyyy")

        'myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtTransactionType"), CrystalDecisions.CrystalReports.Engine.TextObject)
        'If cmbTransactionType.Text = "ALL" Then
        '    myTextObjectOnReport.Text = "ALL (CASH AND CREDIT MEMO)"
        'Else
        '    myTextObjectOnReport.Text = cmbTransactionType.Text & " ONLY"
        'End If

        'myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtTotalAmount"), CrystalDecisions.CrystalReports.Engine.TextObject)
        'myTextObjectOnReport.Text = Convert_To_Currency(TotalAmount)

        'myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtCancelledReciept"), CrystalDecisions.CrystalReports.Engine.TextObject)
        'myTextObjectOnReport.Text = CancelledCount

        'With frm_report_assessment
        '    .MdiParent = frm_main
        '    .WindowState = FormWindowState.Maximized
        '    .CrystalReportViewer1.ReportSource = MyReport
        '    .CrystalReportViewer1.Refresh()
        '    .StartPosition = FormStartPosition.CenterParent
        '    .Show()
        'End With
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub
End Class
