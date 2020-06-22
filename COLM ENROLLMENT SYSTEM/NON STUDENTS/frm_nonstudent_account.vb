Public Class frm_nonstudent_account
    Public AccountType As AccountTypes
    Public selectedrow As Integer = -1
    Enum AccountTypes
        [NON_STUDENT]
        [MEDICAL_ARTS]
    End Enum

    Private Sub LoadAccountInformation()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM vw_nonstudent_account_breakdown WHERE NonStudentID = @NonStudentID", conn)
                comm.Parameters.AddWithValue("@NonStudentID", txtAccountID.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("AccountFeeID"),
                                               reader("RefCode"),
                                               reader("FeeCode"),
                                               Format(CDate(reader("ChargedDate")), "MM-dd-yyyy"),
                                               Convert_To_Currency(reader("TotalAmount")),
                                               Convert_To_Currency(reader("AmountPaid")),
                                               0)
                    End While
                End Using
            End Using

            Using comm As New SqlCommand("SELECT * FROM tbl_college_payment WHERE Student_Number = @NonStudentID ORDER BY Date_Received DESC", conn)
                comm.Parameters.AddWithValue("@NonStudentID", txtAccountID.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"),
                                               reader("Reciept_Number"),
                                               Format(CDate(reader("Date_Received")), "MM-dd-yyyy"),
                                               reader("Fee_Description"),
                                               Convert_To_Currency(reader("Amount_Collected")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnChargeFee.Click
        Dim frm As New frm_nonstudent_charge_fee(txtAccountID.Text)
        With frm
            If Me.AccountType = AccountTypes.MEDICAL_ARTS Then
                .AccountType = frm_nonstudent_charge_fee.AccountTypes.MEDICAL_ARTS
            ElseIf Me.AccountType = AccountTypes.NON_STUDENT Then
                .AccountType = frm_nonstudent_charge_fee.AccountTypes.NON_STUDENT
            End If

            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            LoadAccountInformation()
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New frm_nonstudent_browse
        With frm
            If AccountType = AccountTypes.NON_STUDENT Then
                .AccountType = frm_nonstudent_browse.AccountTypes.NON_STUDENT
            ElseIf AccountType = AccountTypes.MEDICAL_ARTS Then
                .AccountType = frm_nonstudent_browse.AccountTypes.MEDICAL_ARTS
            End If

            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            Dim AccountInformation As New List(Of String)
            AccountInformation = frm.Get_SelectedAccount
            txtAccountID.Text = AccountInformation(0)
            txtAccountName.Text = AccountInformation(1)
            LoadAccountInformation()
        End With
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim Amount As Double = 0
        Dim AmountPaid As Double = 0
        Dim Balance As Double = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            Amount = DataGridView1.Rows(i).Cells(4).Value
            AmountPaid = DataGridView1.Rows(i).Cells(5).Value
            Balance = Amount - AmountPaid

            DataGridView1.Rows(i).Cells(6).Value = Convert_To_Currency(Balance)
            If Balance <= 0 Then
                DataGridView1.Rows(i).Cells(7).ReadOnly = True
            Else
                DataGridView1.Rows(i).Cells(7).ReadOnly = False
            End If
        Next

        Dim totalBalance As Double = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            totalBalance += CDbl(DataGridView1.Rows(i).Cells("clmBalance").Value)
        Next
        txtBalance.Text = Convert_To_Currency(totalBalance)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnCashPayment.Click
        Dim dt As New DataTable
        dt.Columns.Add("AccountFeeID")
        dt.Columns.Add("RefCode")
        dt.Columns.Add("FeeCode")
        dt.Columns.Add("Balance")

        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(7).Value = True Then
                Dim dr As DataRow
                dr = dt.NewRow
                dr("AccountFeeID") = DataGridView1.Rows(i).Cells(0).Value
                dr("RefCode") = DataGridView1.Rows(i).Cells(1).Value
                dr("FeeCode") = DataGridView1.Rows(i).Cells(2).Value
                dr("Balance") = DataGridView1.Rows(i).Cells(6).Value
                dt.Rows.Add(dr)
            End If
        Next

        Dim frm As New frm_nonstudent_payment_cash(txtAccountID.Text, dt, frm_nonstudent_payment_cash.PaymentOptions.CASH_PAYMENT)
        With frm
            .txtAccountName.Text = txtAccountName.Text
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            LoadAccountInformation()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnRemoveFee.Click
        Dim ListofRemovedID As New List(Of Integer)
        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(7).Value = True Then
                ListofRemovedID.Add(DataGridView1.Rows(i).Cells(0).Value)
            End If
        Next

        Dim TotalToRemoved As Integer = ListofRemovedID.Count
        Dim TotalRemoved As Integer = 0


        While ListofRemovedID.Count > 0
            Dim x As Integer = 0
            Dim TobeRemovedID As Integer = ListofRemovedID(x)
            For i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells(0).Value = TobeRemovedID Then
                    Using conn As New SqlConnection(StringConnection)
                        conn.Open()
                        Using comm As New SqlCommand("DELETE FROM tbl_nonstudent_account WHERE AccountFeeID = @AccountFeeID", conn)
                            comm.Parameters.AddWithValue("@AccountFeeID", TobeRemovedID)
                            comm.ExecuteNonQuery()
                        End Using
                    End Using
                    DataGridView1.Rows.Remove(DataGridView1.Rows(i))
                    ListofRemovedID.Remove(TobeRemovedID)
                    TotalRemoved += 1
                    Exit For
                End If
            Next
        End While

        MsgBox(TotalRemoved & " out of " & TotalToRemoved & " has been successfully removed!", MsgBoxStyle.Information)

        If TotalToRemoved <> TotalRemoved Then
            MsgBox(TotalToRemoved - TotalRemoved & " has not been removed because there was already a payment exists!", MsgBoxStyle.Critical)
            Exit Sub
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnPrintSoa.Click
        Dim DS As New DS_NonStudentSOA
        Dim DR As DataRow
        Dim TotalBalance As Double = 0

        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            For i = 0 To DataGridView1.Rows.Count - 1
                If CheckBox1.Checked = True Then
                    Dim FeeCode As String = DataGridView1.Rows(i).Cells(1).Value
                    Dim Description As String = DataGridView1.Rows(i).Cells(2).Value
                    Dim Balance As Double = CDbl(DataGridView1.Rows(i).Cells(4).Value)
                    TotalBalance += DataGridView1.Rows(i).Cells(4).Value

                    With DS.DT_SOA
                        DR = .NewRow
                        DR("Date") = Format(CDate(DataGridView1.Rows(i).Cells(3).Value), "MM-dd-yyyy")
                        DR("RefNo") = DataGridView1.Rows(i).Cells(1).Value
                        DR("ORNo") = ""
                        DR("Description") = DataGridView1.Rows(i).Cells(2).Value
                        DR("Paid") = ""
                        DR("Amount") = DataGridView1.Rows(i).Cells(4).Value
                        DR("Balance") = Convert_To_Currency(TotalBalance)
                        .Rows.Add(DR)
                    End With

                    Using comm As New SqlCommand("SELECT * FROM tbl_college_payment WHERE Fee_Code = @FeeCode AND Fee_Description = @Description AND Reciept_Status = 'ACTIVE'", conn)
                        comm.Parameters.AddWithValue("@FeeCode", FeeCode)
                        comm.Parameters.AddWithValue("@Description", Description)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            While reader.Read
                                Balance = CDbl(reader("Amount_Collected"))
                                With DS.DT_SOA
                                    TotalBalance -= Balance
                                    DR = .NewRow
                                    DR("Date") = Format(CDate(reader("Date_Received")), "MM-dd-yyyy")
                                    DR("RefNo") = ""
                                    DR("ORNo") = "OR#" & reader("Reciept_Number")
                                    DR("Description") = ""
                                    DR("Paid") = "(" & Convert_To_Currency(reader("Amount_Collected")) & ")"
                                    DR("Amount") = ""
                                    DR("Balance") = Convert_To_Currency(TotalBalance)
                                    .Rows.Add(DR)
                                End With
                            End While
                        End Using
                    End Using
                Else
                    If DataGridView1.Rows(i).Cells(6).Value > 0 Then
                        Dim FeeCode As String = DataGridView1.Rows(i).Cells(1).Value
                        Dim Description As String = DataGridView1.Rows(i).Cells(2).Value
                        Dim Balance As Double = CDbl(DataGridView1.Rows(i).Cells(4).Value)
                        TotalBalance += DataGridView1.Rows(i).Cells(4).Value

                        With DS.DT_SOA
                            DR = .NewRow
                            DR("Date") = Format(CDate(DataGridView1.Rows(i).Cells(3).Value), "MM-dd-yyyy")
                            DR("RefNo") = DataGridView1.Rows(i).Cells(1).Value
                            DR("ORNo") = ""
                            DR("Description") = DataGridView1.Rows(i).Cells(2).Value
                            DR("Paid") = ""
                            DR("Amount") = DataGridView1.Rows(i).Cells(4).Value
                            DR("Balance") = Convert_To_Currency(TotalBalance)
                            .Rows.Add(DR)
                        End With

                        Using comm As New SqlCommand("SELECT * FROM tbl_college_payment WHERE Fee_Code = @FeeCode AND Fee_Description = @Description AND Reciept_Status = 'ACTIVE'", conn)
                            comm.Parameters.AddWithValue("@FeeCode", FeeCode)
                            comm.Parameters.AddWithValue("@Description", Description)
                            Using reader As SqlDataReader = comm.ExecuteReader

                                While reader.Read
                                    Balance = CDbl(reader("Amount_Collected"))
                                    With DS.DT_SOA
                                        TotalBalance -= Balance
                                        DR = .NewRow
                                        DR("Date") = Format(CDate(reader("Date_Received")), "MM-dd-yyyy")
                                        DR("RefNo") = ""
                                        DR("ORNo") = "OR#" & reader("Reciept_Number")
                                        DR("Description") = ""
                                        DR("Paid") = "(" & Convert_To_Currency(reader("Amount_Collected")) & ")"
                                        DR("Amount") = ""
                                        DR("Balance") = Convert_To_Currency(TotalBalance)
                                        .Rows.Add(DR)
                                    End With
                                End While

                            End Using
                        End Using
                    End If
                End If
            Next
        End Using


        Dim param_totalbalance As ReportParameter = New ReportParameter("totalbalance", Convert_To_Currency(TotalBalance).ToString)
        Dim param_billto As ReportParameter = New ReportParameter("BillTo", txtAccountName.Text)
        Dim param_date As ReportParameter = New ReportParameter("PrintedDate", Format(Date.Now, "MM-dd-yyyy"))
        Dim MyReport As New ReportDataSource("DataSet1", DS.Tables("DT_SOA"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_statement_of_account_nonstudent.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_totalbalance, param_billto, param_date})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles btnCreditMemo.Click
        Dim dt As New DataTable
        dt.Columns.Add("AccountFeeID")
        dt.Columns.Add("RefCode")
        dt.Columns.Add("FeeCode")
        dt.Columns.Add("Balance")

        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(7).Value = True Then
                Dim dr As DataRow
                dr = dt.NewRow
                dr("AccountFeeID") = DataGridView1.Rows(i).Cells(0).Value
                dr("RefCode") = DataGridView1.Rows(i).Cells(1).Value
                dr("FeeCode") = DataGridView1.Rows(i).Cells(2).Value
                dr("Balance") = DataGridView1.Rows(i).Cells(6).Value
                dt.Rows.Add(dr)
            End If
        Next

        Dim frm As New frm_nonstudent_payment_cash(txtAccountID.Text, dt, frm_nonstudent_payment_cash.PaymentOptions.CREDIT_MEMO)
        With frm
            .txtAccountName.Text = txtAccountName.Text
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            LoadAccountInformation()
        End With
    End Sub

    Private Sub frm_nonstudent_account_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Account_Position = "ASSESSOR") Then
            btnChargeFee.Visible = False
            btnCashPayment.Visible = False
            btnCreditMemo.Visible = False
            btnRemoveFee.Visible = False
            btnPrintSoa.Visible = False
            CheckBox1.Visible = False
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        selectedrow = e.RowIndex
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.Rows(selectedrow).Cells("clmbalance").Value > 0 Then
            With frm_edit_non_student_fee
                .StartPosition = FormStartPosition.CenterParent
                .AccountFeeID = DataGridView1.Rows(selectedrow).Cells(0).Value
                .txtFeeCode.Text = String.Concat("FEE ID: ", DataGridView1.Rows(selectedrow).Cells(0).Value, " FEE CODE: ", DataGridView1.Rows(selectedrow).Cells(2).Value)
                .txtOldAmount.Text = DataGridView1.Rows(selectedrow).Cells("clmAmount").Value
                .txtPayment.Text = DataGridView1.Rows(selectedrow).Cells("clmpayment").Value
                .txtNewAmount.Text = Convert_To_Currency("0")
                .ShowDialog()
                LoadAccountInformation()
            End With
        End If
    End Sub
End Class