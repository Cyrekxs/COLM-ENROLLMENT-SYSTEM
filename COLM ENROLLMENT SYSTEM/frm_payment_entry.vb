Public Class frm_payment_entry
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

    Private Sub txtApplication_TextChanged(sender As Object, e As EventArgs) Handles txtApplication.TextChanged
        txtTendered.Text = txtApplication.Text
    End Sub

    Private Sub txtTendered_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTendered.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtTendered.Text) = True Then
                If txtTendered.Text < txtApplication.Text Then
                    MsgBox("CASH TENDERED must be greater than or atleast equal to application!", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    txtChange.Text = Convert_To_Currency(txtTendered.Text - txtApplication.Text)
                    txtReceipt.Focus()
                End If
            Else
                MsgBox("Invalid Input!", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub txtTendered_TextChanged(sender As Object, e As EventArgs) Handles txtTendered.TextChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If IsNumeric(txtApplication.Text) = False Then
            MsgBox("Please enter correct data!", MsgBoxStyle.Critical)
            txtApplication.Focus()
            Exit Sub
        End If

        If IsNumeric(txtTendered.Text) = False Then
            MsgBox("Please enter correct data!", MsgBoxStyle.Critical)
            txtTendered.Focus()
            Exit Sub
        End If

        If txtReceipt.Text = String.Empty Then
            MsgBox("Please enter reciept number!", MsgBoxStyle.Critical)
            txtReceipt.Focus()
            Exit Sub
        End If

        DS.Tables("STUDENT INFORMATION").Rows.Clear()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            For i = 0 To DataGridView3.Rows.Count - 1
                Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_PAYMENT VALUES(@sn,@rn,'ACTIVE','CASH PAYMENT',@fee_code,@fee_status,NULL,@tendered,@collected,@reciever,GETDATE(),@ay,@sem,@education_level)", conn)
                    comm.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@rn", txtReceipt.Text)
                    comm.Parameters.AddWithValue("@fee_code", DataGridView3.Rows(i).Cells(0).Value)
                    comm.Parameters.AddWithValue("@fee_status", "TUITON FEE")
                    comm.Parameters.AddWithValue("@tendered", txtTendered.Text)
                    comm.Parameters.AddWithValue("@collected", DataGridView3.Rows(i).Cells(2).Value)
                    comm.Parameters.AddWithValue("@reciever", Account_Name)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@education_level", frm_payment.txtEducationLevel.Text)
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
        End Using


        Dim MyReport As New report_payment
        MyReport.SetDataSource(DS.Tables("STUDENT INFORMATION"))
        With frm_report_assessment
            .CrystalReportViewer1.ReportSource = MyReport
            .CrystalReportViewer1.Refresh()
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub
End Class