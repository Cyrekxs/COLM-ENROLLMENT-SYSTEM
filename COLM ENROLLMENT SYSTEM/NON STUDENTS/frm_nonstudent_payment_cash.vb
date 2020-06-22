Public Class frm_nonstudent_payment_cash
    Dim DTPaymentLoads As New DataTable
    Dim _NonStudentID As Integer = 0
    Dim _PaymentType As New PaymentOptions
    Enum PaymentOptions
        [CASH_PAYMENT]
        [CREDIT_MEMO]
    End Enum

    Sub New(NonStudentID As Integer, PaymentLoads As DataTable, PaymentType As PaymentOptions)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _NonStudentID = NonStudentID
        _PaymentType = PaymentType
        For i = 0 To PaymentLoads.Rows.Count - 1
            DataGridView1.Rows.Add(PaymentLoads(i).Item(0),
                                   PaymentLoads(i).Item(1),
                                   PaymentLoads(i).Item(2),
                                   PaymentLoads(i).Item(3),
                                   PaymentLoads(i).Item(3))
        Next
    End Sub

    Private Sub PrintReceipt()
        Dim RecieptNo As String = txtRecieptNo.Text & "*"
        Dim param_aysem As ReportParameter = New ReportParameter("aysem", Academic_Year.ToString & " " & Academic_Sem.ToString)
        Dim param_StudentNo As ReportParameter = New ReportParameter("studentno", _NonStudentID)
        Dim param_StudentName As ReportParameter = New ReportParameter("studentname", txtAccountName.Text)
        Dim param_PrintDate As ReportParameter = New ReportParameter("printdate", Date.Now.ToString)
        Dim param_Course_Year_Sect As ReportParameter = New ReportParameter("course_year_sect", " ")
        Dim param_Total As ReportParameter = New ReportParameter("TotalDue", Convert_To_Currency(txtApplication.Text).ToString)


        Dim DS As New DS_PAYMENT
        Dim DR As DataRow
        For i = 0 To DataGridView1.Rows.Count - 1
            With DS.Tables("STUDENT INFORMATION")
                DR = .NewRow
                DR("STUDENT_NUMBER") = frm_payment.txtStudentNumber.Text
                DR("STUDENT_NAME") = frm_payment.txtStudentName.Text
                DR("FEE_CODE") = DataGridView1.Rows(i).Cells(1).Value & " | " & DataGridView1.Rows(i).Cells(2).Value
                DR("FEE_AMOUNT") = DataGridView1.Rows(i).Cells(4).Value
                DR("TOTAL") = Convert_To_Currency(txtApplication.Text)
                .Rows.Add(DR)
            End With
        Next

        Dim MyReport As New ReportDataSource("DSPayment", DS.Tables("STUDENT INFORMATION"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.report_payment.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_aysem, param_StudentNo, param_StudentName, param_PrintDate, param_Course_Year_Sect, param_Total})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub frm_nonstudent_payment_cash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _PaymentType = PaymentOptions.CREDIT_MEMO Then
            txtPaymentType.Text = "CREDIT MEMO"
        Else
            txtPaymentType.Text = "CASH PAYMENT"
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim TotalApplication As Double = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            If IsNumeric(DataGridView1.Rows(i).Cells(4).Value) = True Then
                TotalApplication += CDbl(DataGridView1.Rows(i).Cells(4).Value)
            End If
        Next
        txtApplication.Text = Convert_To_Currency(TotalApplication)

        If IsNumeric(txtTendered.Text) = True Then
            txtChange.Text = Convert_To_Currency(CDbl(txtTendered.Text) - CDbl(txtApplication.Text))
        End If
    End Sub

    Private Function IsValidReciept(ReceiptNo As String) As Boolean
        Dim IsValid As Boolean = False
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_college_payment WHERE Reciept_Number = @RecieptNo", conn)
                comm.Parameters.AddWithValue("@RecieptNo", ReceiptNo)
                Using reader As SqlDataReader = comm.ExecuteReader
                    If reader.HasRows = True Then
                        IsValid = False
                    Else
                        IsValid = True
                    End If
                End Using
            End Using
        End Using
        Return IsValid
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If StripSpaces(txtRecieptNo.Text) = String.Empty Then
            MsgBox("Please enter receipt no!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If IsValidReciept(StripSpaces(txtRecieptNo.Text)) = True Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using t As SqlTransaction = conn.BeginTransaction
                    Try
                        For i = 0 To DataGridView1.Rows.Count - 1
                            Using comm As New SqlCommand("INSERT INTO tbl_college_payment VALUES (@NonStudentID,@RecieptNo,'ACTIVE',@PaymentType,@FeeCode,@FeeStatus,@FeeDesc,@Recieved,@Collected,@Reciever,GETDATE(),@ay,@sem,'NON STUDENT')", conn, t)
                                comm.Parameters.AddWithValue("@NonStudentID", _NonStudentID)
                                comm.Parameters.AddWithValue("@RecieptNo", txtRecieptNo.Text)
                                If _PaymentType = PaymentOptions.CASH_PAYMENT Then
                                    comm.Parameters.AddWithValue("@PaymentType", "CASH PAYMENT")
                                ElseIf _PaymentType = PaymentOptions.CREDIT_MEMO Then
                                    comm.Parameters.AddWithValue("@PaymentType", "CREDIT MEMO")
                                End If
                                comm.Parameters.AddWithValue("@FeeCode", DataGridView1.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@FeeStatus", "NON STUDENT FEE")
                                comm.Parameters.AddWithValue("@FeeDesc", DataGridView1.Rows(i).Cells(2).Value)
                                comm.Parameters.AddWithValue("@Recieved", txtTendered.Text)
                                comm.Parameters.AddWithValue("@Collected", DataGridView1.Rows(i).Cells(4).Value)
                                comm.Parameters.AddWithValue("@Reciever", Account_Name)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.ExecuteNonQuery()
                            End Using
                        Next
                        t.Commit()
                        MsgBox("Transaction has been successfully completed!", MsgBoxStyle.Information)

                        PrintReceipt()

                        Me.Close()
                        Me.Dispose()
                    Catch ex As Exception
                        t.Rollback()
                        MsgBox("An error occured while processing transaction" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
                        Me.Close()
                        Me.Dispose()
                    End Try
                End Using
            End Using
        Else
            MsgBox("Receipt No is already exists!", MsgBoxStyle.Critical)
        End If
    End Sub
End Class