Public Class frm_payment_cash_other_entry
    Dim DS As New DS_PAYMENT
    Dim DR As DataRow

    Public Sub CalculateFees()
        Dim AppliedAmount As Double = 0
        For i = 0 To DataGridView3.Rows.Count - 1
            AppliedAmount += DataGridView3.Rows(i).Cells(3).Value
        Next
        txtApplication.Text = Convert_To_Currency(AppliedAmount)

        If txtTendered.Text = String.Empty Then
        Else
            If IsNumeric(txtTendered.Text) = True Then
                Dim Tendered As Double = txtTendered.Text
                txtChange.Text = Convert_To_Currency(Tendered - CDbl(txtApplication.Text))
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("Are you sure you want to cancel this transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub DataGridView3_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellEndEdit
        If e.ColumnIndex = 3 Then
            If IsNumeric(DataGridView3.Rows(e.RowIndex).Cells(3).Value) = True Then 'KUNG NUBMER SYA
                If CDbl(DataGridView3.Rows(e.RowIndex).Cells(3).Value) <= CDbl(DataGridView3.Rows(e.RowIndex).Cells(2).Value) Then
                    DataGridView3.Rows(e.RowIndex).Cells(3).Value = Convert_To_Currency(DataGridView3.Rows(e.RowIndex).Cells(3).Value)
                Else
                    MsgBox("Applied must be less than or equal to the amount!", MsgBoxStyle.Critical)
                    DataGridView3.Rows(e.RowIndex).Cells(3).Value = DataGridView3.Rows(e.RowIndex).Cells(2).Value
                End If
            Else
                If Val(DataGridView3.Rows(e.RowIndex).Cells(3).Value) = 0 Then
                    DataGridView3.Rows(e.RowIndex).Cells(3).Value = Convert_To_Currency(0)
                ElseIf DataGridView3.Rows(e.RowIndex).Cells(3).Value = String.Empty Then
                    DataGridView3.Rows(e.RowIndex).Cells(3).Value = Convert_To_Currency(0)
                End If
            End If
            CalculateFees()
        End If
    End Sub

    Private Sub frm_payment_cash_other_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CalculateFees()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If txtApplication.Text = String.Empty Then
            MsgBox("No application amount detected!", MsgBoxStyle.Critical)
            Exit Sub
        Else
            If CDbl(txtApplication.Text) <= 0 Then
                MsgBox("Application amount must be greater that Zero!", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        If txtTendered.Text = String.Empty Then
            MsgBox("Please enter amount tendered!", MsgBoxStyle.Critical)
            txtTendered.Focus()
            Exit Sub
        Else
            If CDbl(txtTendered.Text) <= 0 Then
                MsgBox("Amount tendered must be greater than Zero!", MsgBoxStyle.Critical)
                Exit Sub
            ElseIf CDbl(txtTendered.Text) < CDbl(txtApplication.Text) Then
                MsgBox("Amount tendered must be greater than or equal to application amount!", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        If txtReceipt.Text = String.Empty Then
            MsgBox("Please enter Reciept No!", MsgBoxStyle.Critical)
            txtReceipt.Focus()
            Exit Sub
        End If

        CalculateFees()

        Dim RecieptNo As String = txtReceipt.Text & "*"
        Dim param_aysem As ReportParameter = New ReportParameter("aysem", Academic_Year.ToString & " " & Academic_Sem.ToString)
        Dim param_StudentNo As ReportParameter = New ReportParameter("studentno", frm_payment.txtStudentNumber.Text)
        Dim param_StudentName As ReportParameter = New ReportParameter("studentname", frm_payment.txtStudentName.Text)
        Dim param_PrintDate As ReportParameter = New ReportParameter("printdate", Date.Now.ToString)
        Dim param_Course_Year_Sect As ReportParameter = New ReportParameter("course_year_sect", frm_payment.txtCourse.Text & " " & frm_payment.txtYear.Text & " " & frm_payment.txtSection.Text)
        Dim param_Total As ReportParameter = New ReportParameter("TotalDue", Convert_To_Currency(txtApplication.Text).ToString)

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE RECIEPT_NUMBER = @rn", conn, t)
                        comm.Parameters.AddWithValue("@rn", RecieptNo)
                        If Val(comm.ExecuteScalar) = 0 Then
                            If MsgBox("Are you sure you want to proceed this transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                For i = 0 To DataGridView3.Rows.Count - 1
                                    If CDbl(DataGridView3.Rows(i).Cells(3).Value) > 0 Then
                                        Using comm1 As New SqlCommand("INSERT INTO TBL_COLLEGE_PAYMENT VALUES(@sn,@rn,@r_status,@fee_type,@fee_code,@fee_status,NULL,@amount_received,@amount_collected,@reciever,GETDATE(),@ay,@sem,@education_level)", conn, t)
                                            comm1.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                                            comm1.Parameters.AddWithValue("@rn", RecieptNo)
                                            comm1.Parameters.AddWithValue("@r_status", "ACTIVE")
                                            comm1.Parameters.AddWithValue("@fee_type", "CASH PAYMENT")
                                            comm1.Parameters.AddWithValue("@fee_code", DataGridView3.Rows(i).Cells(0).Value)
                                            comm1.Parameters.AddWithValue("@fee_status", "OTHER FEES (INTERNAL)")
                                            comm1.Parameters.AddWithValue("@amount_received", txtTendered.Text)
                                            comm1.Parameters.AddWithValue("@amount_collected", DataGridView3.Rows(i).Cells(3).Value)
                                            comm1.Parameters.AddWithValue("@reciever", Account_Name)
                                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                                            If frm_payment.EducationLevel = "COLLEGE" Then
                                                comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                                            Else
                                                comm1.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                                            End If
                                            comm1.Parameters.AddWithValue("@education_level", frm_payment.EducationLevel)
                                            comm1.ExecuteNonQuery()
                                        End Using
                                    End If


                                    With DS.Tables("STUDENT INFORMATION")
                                        DR = .NewRow
                                        DR("STUDENT_NUMBER") = frm_payment.txtStudentNumber.Text
                                        DR("STUDENT_NAME") = frm_payment.txtStudentName.Text
                                        DR("FEE_CODE") = DataGridView3.Rows(i).Cells(0).Value & " | " & DataGridView3.Rows(i).Cells(1).Value
                                        DR("FEE_AMOUNT") = DataGridView3.Rows(i).Cells(3).Value
                                        DR("TOTAL") = Convert_To_Currency(txtApplication.Text)
                                        .Rows.Add(DR)
                                    End With
                                Next

                                t.Commit()
                                MsgBox("Payment has been successfully processed!" & vbNewLine & " Please wait to the report!", MsgBoxStyle.Information)


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
                            End If
                        Else
                            MsgBox("Reciept No: " & txtReceipt.Text & " is already exist!", MsgBoxStyle.Critical)
                        End If
                    End Using
                Catch ex As Exception
                    t.Rollback()
                    MsgBox("An error occured while processing information please try again" & vbNewLine & "Exception " & ex.Message, MsgBoxStyle.Critical)
                End Try
            End Using
            
        End Using
    End Sub

    Private Sub txtTendered_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTendered.KeyDown
        If e.KeyCode = Keys.Enter Then
            CalculateFees()
            txtReceipt.Focus()
        End If
    End Sub

    Private Sub txtTendered_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTendered.KeyPress
        NumbersOnlyWithDecimal(sender, e)
    End Sub

    Private Sub txtReceipt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReceipt.KeyPress
        NumbersOnly(sender, e)
    End Sub
End Class