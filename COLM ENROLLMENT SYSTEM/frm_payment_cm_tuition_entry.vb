Public Class frm_payment_cm_tuition_entry
    Public Application As Double = 0
    Dim DS As New DS_Credit_Memo
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
                        DataGridView3.Rows.Add(frm_payment.DG_TFEE.Rows(i).Cells(0).Value, frm_payment.DG_TFEE.Rows(i).Cells(3).Value, Convert_To_Currency(Payment))
                        Application -= frm_payment.DG_TFEE.Rows(i).Cells(3).Value
                    Else
                        Payment = Application
                        Application_Amount += Application
                        DataGridView3.Rows.Add(frm_payment.DG_TFEE.Rows(i).Cells(0).Value, frm_payment.DG_TFEE.Rows(i).Cells(3).Value, Convert_To_Currency(Payment))
                        Application = 0
                    End If
                End If
            End If
        Next

        txtApplication.Text = Convert_To_Currency(Application_Amount)
    End Sub

    Private Sub txtApplication_KeyDown(sender As Object, e As KeyEventArgs) Handles txtApplication.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtApplication.Text) = True Then
                Application = txtApplication.Text
                Calculate_Payments()
                txtReceipt.Focus()
            Else
                MsgBox("Invalid Input!", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub txtApplication_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtApplication.KeyPress
        NumbersOnlyWithDecimal(sender, e)
    End Sub

    Private Sub txtApplication_LostFocus(sender As Object, e As EventArgs) Handles txtApplication.LostFocus
        If IsNumeric(txtApplication.Text) = True Then
            Application = txtApplication.Text
            Calculate_Payments()
            txtReceipt.Focus()
        Else
            MsgBox("Invalid Input!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub txtApplication_TextChanged(sender As Object, e As EventArgs) Handles txtApplication.TextChanged

    End Sub

    Private Sub txtReceiptNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtReceipt.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtReason.Focus()
        End If
    End Sub

    Private Sub txtReceipt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReceipt.KeyPress
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtReceiptNo_TextChanged(sender As Object, e As EventArgs) Handles txtReceipt.TextChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If IsNumeric(txtApplication.Text) = False Then
            MsgBox("Please enter correct Credit Memo Amount!", MsgBoxStyle.Critical)
            txtApplication.Focus()
            Exit Sub
        End If

        If txtReceipt.Text = String.Empty Then
            MsgBox("Please enter reciept number!", MsgBoxStyle.Critical)
            txtReceipt.Focus()
            Exit Sub
        End If

        Dim RecieptNo As String = "CM#: " & txtReceipt.Text

        DS.Tables("STUDENT INFORMATION").Rows.Clear()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE RECIEPT_NUMBER = @rn", conn)
                comm.Parameters.AddWithValue("@rn", RecieptNo)
                If Val(comm.ExecuteScalar) > 0 Then
                    MsgBox("Receipt Number: " & txtReceipt.Text & " is already exist!", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End Using

            For i = 0 To DataGridView3.Rows.Count - 1
                Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_PAYMENT VALUES(@sn,@rn,'ACTIVE','CREDIT MEMO',@fee_code,@fee_status,NULL,@tendered,@collected,@reciever,GETDATE(),@ay,@sem,@education_level)", conn)
                    comm.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@rn", txtReceipt.Text)
                    comm.Parameters.AddWithValue("@fee_code", DataGridView3.Rows(i).Cells(0).Value)
                    comm.Parameters.AddWithValue("@fee_status", "TUITION FEE")
                    comm.Parameters.AddWithValue("@collected", DataGridView3.Rows(i).Cells(2).Value)
                    comm.Parameters.AddWithValue("@tendered", 0)
                    comm.Parameters.AddWithValue("@reciever", Account_Name)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@education_level", frm_payment.EducationLevel)
                    comm.ExecuteNonQuery()

                    With DS.Tables("STUDENT INFORMATION")
                        DR = .NewRow
                        DR("STUDENT_NUMBER") = frm_payment.txtStudentNumber.Text
                        DR("STUDENT_NAME") = frm_payment.txtStudentName.Text
                        DR("RECEIPT_NUMBER") = RecieptNo
                        DR("REASON") = txtReason.Text
                        DR("FEE_CODE") = DataGridView3.Rows(i).Cells(0).Value
                        DR("FEE_AMOUNT") = DataGridView3.Rows(i).Cells(2).Value
                        DR("TOTAL") = Convert_To_Currency(txtApplication.Text)
                        .Rows.Add(DR)
                    End With
                End Using
            Next
            'CREATING CM PAYMENT IN STATEMENT OF ACCOUNT
            Create_Statement(frm_payment.txtStudentNumber.Text, "CM", "TUITION FEE", "TUITION FEE", Date.Now, 0, txtApplication.Text)
            'CREATING REMAINING BALANCE IN TUITION FEE
            Dim TutionBalance As Double = 0
            Using comm As New SqlCommand("SELECT TOP 1 DEBIT FROM TBL_STATEMENT_OF_ACCOUNT WHERE STUDENT_NUMBER = @sn AND TRANSACTION_TYPE = @transaction_type AND TRANSACTION_CODE = @transaction_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem ORDER BY ID DESC", conn)
                comm.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@transaction_code", "BAL TF")
                comm.Parameters.AddWithValue("@transaction_type", "TUITION FEE")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                TutionBalance = CDbl(comm.ExecuteScalar)
            End Using
            Create_Statement(frm_payment.txtStudentNumber.Text, "BAL TF", "TUITION FEE", "********************************", Date.Now, CDbl(TutionBalance) - CDbl(txtApplication.Text), 0)
        End Using

        MsgBox("Payment has been successfully recorded!", MsgBoxStyle.Information)

        Dim MyReport As New report_payment
        MyReport.SetDataSource(DS.Tables("STUDENT INFORMATION"))
        With frm_report_assessment
            .CrystalReportViewer1.ReportSource = MyReport
            .CrystalReportViewer1.Refresh()
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
        Me.Close()
        Me.Dispose()
    End Sub
End Class