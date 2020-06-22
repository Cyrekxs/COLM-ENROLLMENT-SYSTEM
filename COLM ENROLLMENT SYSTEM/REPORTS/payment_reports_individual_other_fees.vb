Public Class payment_reports_individual_other_fees
    Dim TotalAmount As Double = 0
    Dim FeeCodeSource As New AutoCompleteStringCollection

    Public Sub LoadStudents()
        Dim TotalAmount As Double = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim sqlquery As String = "SELECT * FROM dbo.FN_StudentsPaymentOtherFees(@Education_Level) WHERE Fee_Type = @Fee_Type AND Reciept_Status = @Reciept_Status AND Fee_Code LIKE @Fee_Code AND Date_Received BETWEEN @date1 AND @Date2 AND Academic_Yr = @ay"

            If cmbEducationLevel.Text = "COLLEGE" Then
                sqlquery = String.Concat(sqlquery, " AND Academic_Sem = @sem")
            End If

            Using comm As New SqlCommand(sqlquery, conn)
                Dim Date1 As DateTime = Nothing
                Dim Date2 As DateTime = Nothing
                Date1 = Format(DateTimePicker1.Value, "MM-dd-yyyy") & " 12:00:01 AM"
                Date2 = Format(DateTimePicker2.Value, "MM-dd-yyyy") & " 11:59:59 PM"
                comm.Parameters.AddWithValue("@date1", Date1)
                comm.Parameters.AddWithValue("@date2", Date2)
                comm.Parameters.AddWithValue("@Fee_Code", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@fee_type", cmbTransactionType.Text)
                comm.Parameters.AddWithValue("@reciept_status", cmbRecieptStatus.Text)
                comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    Dim i As Integer = 0
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        i += 1
                        DataGridView1.Rows.Add(i,
                                               Format(CDate(reader("DATE_RECEIVED")), "MM-dd-yyyy"),
                                               reader("FEE_TYPE"), reader("RECIEPT_NUMBER"),
                                               reader("RECIEPT_STATUS"),
                                               Convert_To_Currency(reader("AMOUNT_COLLECTED")),
                                               reader("STUDENT_NUMBER"),
                                               reader("STUDENT_NAME"),
                                               reader("EDUCATION_LEVEL"),
                                               reader("RECEIVER"))
                        TotalAmount += CDbl(reader("Amount_Collected"))
                    End While
                End Using
            End Using
        End Using
        lblTotalAmount.Text = Convert_To_Currency(TotalAmount)
        lblTotalResults.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadStudents()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim param_ReportTitle As ReportParameter = New ReportParameter("ReportTitle", "TOTAL COLLECTIONS IN " & txtSearch.Text)
        Dim param_DateFromTo As ReportParameter = New ReportParameter("DateFromTo", "DATE FROM " & DateTimePicker1.Value.ToString & " TO " & DateTimePicker2.Value.ToString)
        Dim param_TransactionType As ReportParameter = New ReportParameter("TransactionType", "TRANSACTION TYPE: " & cmbTransactionType.Text)
        Dim param_EducationLevel As ReportParameter = New ReportParameter("EducationLevel", "EDUCATION LEVEL: " & cmbEducationLevel.Text)
        Dim param_DatePrinted As ReportParameter = New ReportParameter("DatePrinted", Date.Now.ToString)
        Dim amount As Double = 0
        Dim Cancelled As Integer = 0

        Dim DS As New DS_PaymentTransactions
        Dim DR As DataRow
        DS.Tables("DT_PaymentTransactions").Rows.Clear()

        With DS.Tables("DT_PaymentTransactions")
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("TXDate") = DataGridView1.Rows(i).Cells(1).Value
                DR("TXType") = DataGridView1.Rows(i).Cells(2).Value
                DR("RecieptNo") = DataGridView1.Rows(i).Cells(3).Value

                If DataGridView1.Rows(i).Cells(3).Value = "ACTIVE" Then
                    DR("RecieptStatus") = ""
                Else
                    DR("RecieptStatus") = DataGridView1.Rows(i).Cells(3).Value
                End If

                DR("Amount") = DataGridView1.Rows(i).Cells(5).Value
                DR("StudentNo") = DataGridView1.Rows(i).Cells(6).Value
                DR("StudentName") = DataGridView1.Rows(i).Cells(7).Value
                DR("EducationLevel") = DataGridView1.Rows(i).Cells(8).Value
                amount += DataGridView1.Rows(i).Cells(5).Value
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

    Private Sub payment_reports_individual_other_fees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT FEE_CODE FROM TBL_COLLEGE_FEE_LOADS WHERE FEE_TYPE = 'OTHER FEES (INTERNAL)'", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    txtSearch.AutoCompleteCustomSource.Clear()
                    While reader.Read
                        FeeCodeSource.Add(reader("FEE_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        txtSearch.AutoCompleteCustomSource = FeeCodeSource
    End Sub
End Class
