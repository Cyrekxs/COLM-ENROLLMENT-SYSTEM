Public Class frm_payment_cm_other_entry
    Dim DS As New DS_PAYMENT
    Dim DR As DataRow

    Public Sub CalculateFees()
        Dim AppliedAmount As Double = 0
        For i = 0 To DataGridView3.Rows.Count - 1
            AppliedAmount += DataGridView3.Rows(i).Cells(3).Value
        Next
        txtApplication.Text = Convert_To_Currency(AppliedAmount)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If txtApplication.Text = String.Empty Then
            MsgBox("Application amount is required!", MsgBoxStyle.Critical)
            Exit Sub
        Else
            If CDbl(txtApplication.Text) <= 0 Then
                MsgBox("Application amount do not require zero amount!", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        If txtReceipt.Text = String.Empty Then
            MsgBox("Please enter reciept no!", MsgBoxStyle.Critical)
            txtReceipt.Focus()
            Exit Sub
        End If

        CalculateFees()

        Dim RecieptNo As String = "CM#: " & txtReceipt.Text

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE RECIEPT_NUMBER = @rn", conn)
                comm.Parameters.AddWithValue("@rn", RecieptNo)
                If Val(comm.ExecuteScalar) = 0 Then
                    If MsgBox("Are you sure you want to proceed this transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        For i = 0 To DataGridView3.Rows.Count - 1
                            If CDbl(DataGridView3.Rows(i).Cells(3).Value) > 0 Then
                                Using comm1 As New SqlCommand("INSERT INTO TBL_COLLEGE_PAYMENT VALUES(@sn,@rn,@r_status,@fee_type,@fee_code,@fee_status,@reason,@amount_received,@amount_collected,@reciever,GETDATE(),@ay,@sem,@education_level)", conn)
                                    comm1.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                                    comm1.Parameters.AddWithValue("@rn", RecieptNo)
                                    comm1.Parameters.AddWithValue("@r_status", "ACTIVE")
                                    comm1.Parameters.AddWithValue("@fee_type", "CREDIT MEMO")
                                    comm1.Parameters.AddWithValue("@fee_code", DataGridView3.Rows(i).Cells(0).Value)
                                    comm1.Parameters.AddWithValue("@fee_status", "OTHER FEES (INTERNAL)")
                                    comm1.Parameters.AddWithValue("@reason", txtReason.Text)
                                    comm1.Parameters.AddWithValue("@amount_received", txtApplication.Text)
                                    comm1.Parameters.AddWithValue("@amount_collected", DataGridView3.Rows(i).Cells(3).Value)
                                    comm1.Parameters.AddWithValue("@reciever", Account_Name)
                                    comm1.Parameters.AddWithValue("@ay", Academic_Year)
                                    comm1.Parameters.AddWithValue("@sem", Academic_Sem)
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

                        For i = 0 To DataGridView3.Rows.Count - 1
                            Create_Statement(frm_payment.txtStudentNumber.Text, "PAY", "OTHER FEE", DataGridView3.Rows(i).Cells(0).Value, Date.Now, 0, DataGridView3.Rows(i).Cells(3).Value)
                            Create_Statement(frm_payment.txtStudentNumber.Text, "BAL OF", "OTHER FEE", DataGridView3.Rows(i).Cells(0).Value, Date.Now, CDbl(DataGridView3.Rows(i).Cells(2).Value) - CDbl(DataGridView3.Rows(i).Cells(3).Value), 0)
                        Next

                        MsgBox("Payment has been successfully processed!" & vbNewLine & " Please wait to the report!", MsgBoxStyle.Information)


                        Dim MyReport As New report_payment
                        MyReport.SetDataSource(DS.Tables("STUDENT INFORMATION"))

                        Dim myTextObjectOnReport As CrystalDecisions.CrystalReports.Engine.TextObject
                        myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtSYSem"), CrystalDecisions.CrystalReports.Engine.TextObject)
                        myTextObjectOnReport.Text = Academic_Sem & " " & Academic_Year

                        myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtCYS"), CrystalDecisions.CrystalReports.Engine.TextObject)
                        myTextObjectOnReport.Text = frm_payment.txtCourse.Text & " " & frm_payment.txtYear.Text & " " & frm_payment.txtSection.Text

                        With frm_report_assessment
                            .CrystalReportViewer1.ReportSource = MyReport
                            .CrystalReportViewer1.Refresh()
                            .StartPosition = FormStartPosition.CenterParent
                            .ShowDialog()
                        End With
                    End If
                End If
            End Using
        End Using
    End Sub

    Private Sub frm_payment_cm_other_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CalculateFees()
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

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

    Private Sub txtReceipt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReceipt.KeyPress
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtReceipt_TextChanged(sender As Object, e As EventArgs) Handles txtReceipt.TextChanged

    End Sub

    Private Sub txtApplication_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtApplication.KeyPress
        NumbersOnlyWithDecimal(sender, e)
    End Sub

    Private Sub txtApplication_TextChanged(sender As Object, e As EventArgs) Handles txtApplication.TextChanged

    End Sub
End Class