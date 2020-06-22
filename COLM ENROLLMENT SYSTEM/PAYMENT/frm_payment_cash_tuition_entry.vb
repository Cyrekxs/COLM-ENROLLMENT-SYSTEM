Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Public Class frm_payment_cash_tuition_entry
    Public Application As Double = 0
    Dim DS As New DS_PAYMENT
    Dim DR As DataRow

    Public Sub Calculate_Payments()
        Dim Application_Amount As Double = 0
        Dim Payment As Double = 0
        DataGridView3.Rows.Clear()
        For i = 0 To frm_payment.DG_TFEE.Rows.Count - 1
            If Application > 0 Then
                If frm_payment.DG_TFEE.Rows(i).Cells(3).Value > 0 Then
                    If Application >= frm_payment.DG_TFEE.Rows(i).Cells(3).Value Then
                        Payment = frm_payment.DG_TFEE.Rows(i).Cells(3).Value
                        Application_Amount += frm_payment.DG_TFEE.Rows(i).Cells(3).Value
                        DataGridView3.Rows.Add(frm_payment.DG_TFEE.Rows(i).Cells(0).Value, frm_payment.DG_TFEE.Rows(i).Cells(3).Value, Payment)
                        Application -= frm_payment.DG_TFEE.Rows(i).Cells(3).Value
                    Else
                        Payment = Application
                        Application_Amount += Application
                        DataGridView3.Rows.Add(frm_payment.DG_TFEE.Rows(i).Cells(0).Value, frm_payment.DG_TFEE.Rows(i).Cells(3).Value, Payment)
                        Application = 0
                    End If
                End If
            End If
        Next

        txtApplication.Text = Convert_To_Currency(Application_Amount)
        txtTendered.Text = Convert_To_Currency(Application_Amount)
        txtChange.Text = Convert_To_Currency(txtTendered.Text - txtApplication.Text)
    End Sub

    Private Sub frm_payment_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtApplication_KeyDown(sender As Object, e As KeyEventArgs) Handles txtApplication.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtApplication.Text) = True Then
                Application = txtApplication.Text
                Calculate_Payments()
                txtTendered.Focus()
            Else
                MsgBox("Invalid Input!", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub txtApplication_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtApplication.KeyPress
        NumbersOnlyWithDecimal(sender, e)
    End Sub

    Private Sub txtApplication_TextChanged(sender As Object, e As EventArgs) Handles txtApplication.TextChanged
        txtTendered.Text = txtApplication.Text
    End Sub

    Private Sub txtTendered_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTendered.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtTendered.Text) = True Then
                If CDbl(txtTendered.Text) < CDbl(txtApplication.Text) Then
                    MsgBox("CASH TENDERED must be greater than or atleast equal to application!", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    txtChange.Text = Convert_To_Currency(CDbl(txtTendered.Text) - CDbl(txtApplication.Text))
                    txtReceipt.Focus()
                End If
            Else
                MsgBox("Invalid Input!", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If IsNumeric(txtApplication.Text) = False Then
            MsgBox("Please enter correct data!", MsgBoxStyle.Critical)
            txtApplication.Focus()
            Exit Sub
        Else
            Application = txtApplication.Text
        End If

        If txtTendered.Text = String.Empty Then
            MsgBox("Please enter Tendered Amount!", MsgBoxStyle.Critical)
            txtTendered.Focus()
            Exit Sub
        End If

        If IsNumeric(txtTendered.Text) = False Then
            MsgBox("Please enter correct data!", MsgBoxStyle.Critical)
            txtTendered.Focus()
            Exit Sub
        Else
            If CDbl(txtTendered.Text) < CDbl(txtApplication.Text) Then
                MsgBox("CASH TENDERED must be greater than or atleast equal to application!", MsgBoxStyle.Critical)
                Exit Sub
            Else
                txtChange.Text = Convert_To_Currency(CDbl(txtTendered.Text) - CDbl(txtApplication.Text))
                txtReceipt.Focus()
            End If
        End If

        If txtReceipt.Text = String.Empty Then
            MsgBox("Please enter reciept number!", MsgBoxStyle.Critical)
            txtReceipt.Focus()
            Exit Sub
        End If

        Dim ReceiptNumber As String = txtReceipt.Text

        Dim param_aysem As ReportParameter = New ReportParameter("aysem", Academic_Year.ToString & " " & Academic_Sem.ToString)
        Dim param_StudentNo As ReportParameter = New ReportParameter("studentno", frm_payment.txtStudentNumber.Text)
        Dim param_StudentName As ReportParameter = New ReportParameter("studentname", frm_payment.txtStudentName.Text)
        Dim param_PrintDate As ReportParameter = New ReportParameter("printdate", Date.Now.ToString)
        Dim param_Course_Year_Sect As ReportParameter = New ReportParameter("course_year_sect", frm_payment.txtCourse.Text & " " & frm_payment.txtYear.Text & " " & frm_payment.txtSection.Text)
        Dim param_Total As ReportParameter = New ReportParameter("TotalDue", Convert_To_Currency(txtApplication.Text).ToString)


        ReceiptNumber = "#" & ReceiptNumber

        Calculate_Payments()

        DS.Tables("STUDENT INFORMATION").Rows.Clear()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE RECIEPT_NUMBER = @rn", conn, t)
                        comm.Parameters.AddWithValue("@rn", txtReceipt.Text)
                        If Val(comm.ExecuteScalar) > 0 Then
                            MsgBox("Receipt Number: " & txtReceipt.Text & " is already exist!", MsgBoxStyle.Critical)
                            Exit Sub
                        End If
                    End Using

                    For i = 0 To DataGridView3.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_PAYMENT VALUES(@sn,@rn,'ACTIVE','CASH PAYMENT',@fee_code,@fee_status,NULL,@tendered,@collected,@reciever,GETDATE(),@ay,@sem,@education_level)", conn, t)
                            comm.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@rn", ReceiptNumber)
                            comm.Parameters.AddWithValue("@fee_code", DataGridView3.Rows(i).Cells(0).Value)
                            comm.Parameters.AddWithValue("@fee_status", "TUITION FEE")
                            comm.Parameters.AddWithValue("@tendered", txtTendered.Text)
                            comm.Parameters.AddWithValue("@collected", DataGridView3.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@reciever", Account_Name)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            If frm_payment.EducationLevel = "COLLEGE" Then
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            Else
                                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                            End If
                            comm.Parameters.AddWithValue("@education_level", frm_payment.EducationLevel)
                            comm.ExecuteNonQuery()

                            With DS.Tables("STUDENT INFORMATION")
                                DR = .NewRow
                                DR("STUDENT_NUMBER") = frm_payment.txtStudentNumber.Text
                                DR("STUDENT_NAME") = frm_payment.txtStudentName.Text
                                DR("FEE_CODE") = DataGridView3.Rows(i).Cells(0).Value
                                DR("FEE_AMOUNT") = DataGridView3.Rows(i).Cells(2).Value
                                DR("TOTAL") = Convert_To_Currency(txtApplication.Text)
                                .Rows.Add(DR)
                            End With
                        End Using
                    Next
                    t.Commit()
                    MsgBox("Payment has been successfully recorded!" & vbNewLine & "Please wait for the report", MsgBoxStyle.Information)


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

                Catch ex As Exception
                    t.Rollback()
                    MsgBox("An error occured while processing transaction please try again!" & vbNewLine & "Exception " & ex.Message, MsgBoxStyle.Critical)
                End Try                
            End Using
        End Using




    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("Are you sure you want to cancel this transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub txtTendered_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTendered.KeyPress
        NumbersOnlyWithDecimal(sender, e)
    End Sub

    Private Sub txtTendered_TextChanged(sender As Object, e As EventArgs) Handles txtTendered.TextChanged

    End Sub

    Private Sub txtReceipt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReceipt.KeyPress
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtReceipt_TextChanged(sender As Object, e As EventArgs) Handles txtReceipt.TextChanged

    End Sub
End Class