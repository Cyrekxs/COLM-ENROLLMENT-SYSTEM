Public Class frm_balance_checker_revised
    Private Sub LoadTracker(Search As String)
        Dim dt As New DataTable
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM vw_student_balance_summary WHERE StudentName IS NOT NULL AND StudentName LIKE @search ORDER BY StudentName ASC", conn)
                comm.Parameters.AddWithValue("@search", String.Concat("%", Search, "%"))
                Using reader As SqlDataReader = comm.ExecuteReader
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Dim c_tution, c_tutionpayment, c_others, c_otherspayment, c_balance As Double
        Dim s_tution, s_tutionpayment, s_others, s_otherspayment, s_balance As Double
        Dim j_tution, j_tutionpayment, j_others, j_otherspayment, j_balance As Double

        dgCollege.Rows.Clear()
        dgSenior.Rows.Clear()
        dgJunior.Rows.Clear()
        For Each row As DataRow In dt.Rows
            Dim TuitionFee As Double = row("TuitionFee")
            Dim TuitionPayment As Double = row("TuitionPayment")
            Dim OtherFee As Double = row("OtherFees")
            Dim OtherFeePayment As Double = row("OtherFeesPayment")
            Dim Balance As Double = (TuitionFee + OtherFee) - (TuitionPayment + OtherFeePayment)

            Select Case row("Education_Level")
                Case "COLLEGE"

                    c_tution += TuitionFee
                    c_tutionpayment += TuitionPayment
                    c_others += OtherFee
                    c_otherspayment += OtherFeePayment
                    c_balance += Balance

                    dgCollege.Rows.Add(row("Student_Number"),
                                       row("StudentName"),
                                       row("Academic_Yr") & "-" & row("Academic_Sem"),
                                       Convert_To_Currency(row("TuitionFee")),
                                       Convert_To_Currency(row("TuitionPayment")),
                                       Convert_To_Currency(row("OtherFees")),
                                       Convert_To_Currency(row("OtherFeesPayment")), Convert_To_Currency(Balance))
                Case "SENIOR HIGH"

                    s_tution += TuitionFee
                    s_tutionpayment += TuitionPayment
                    s_others += OtherFee
                    s_otherspayment += OtherFeePayment
                    s_balance += Balance

                    dgSenior.Rows.Add(row("Student_Number"),
                                       row("StudentName"),
                                       row("Academic_Yr") & "-" & row("Academic_Sem"),
                                       Convert_To_Currency(row("TuitionFee")),
                                       Convert_To_Currency(row("TuitionPayment")),
                                       Convert_To_Currency(row("OtherFees")),
                                       Convert_To_Currency(row("OtherFeesPayment")), Convert_To_Currency(Balance))
                Case "JUNIOR HIGH"

                    j_tution += TuitionFee
                    j_tutionpayment += TuitionPayment
                    j_others += OtherFee
                    j_otherspayment += OtherFeePayment
                    j_balance += Balance

                    dgJunior.Rows.Add(row("Student_Number"),
                                       row("StudentName"),
                                       row("Academic_Yr") & "-" & row("Academic_Sem"),
                                       Convert_To_Currency(row("TuitionFee")),
                                       Convert_To_Currency(row("TuitionPayment")),
                                       Convert_To_Currency(row("OtherFees")),
                                       Convert_To_Currency(row("OtherFeesPayment")), Convert_To_Currency(Balance))
            End Select
        Next

        txtC_Tution.Text = Convert_To_Currency(c_tution)
        txtC_TuitionPayment.Text = Convert_To_Currency(c_tutionpayment)
        txtC_Others.Text = Convert_To_Currency(c_others)
        txtC_OthersPayment.Text = Convert_To_Currency(c_otherspayment)
        txtC_Balance.Text = Convert_To_Currency(c_balance)

        txtS_Tution.Text = Convert_To_Currency(s_tution)
        txtS_TuitionPayment.Text = Convert_To_Currency(s_tutionpayment)
        txtS_Others.Text = Convert_To_Currency(s_others)
        txtS_OthersPayment.Text = Convert_To_Currency(s_otherspayment)
        txtS_Balance.Text = Convert_To_Currency(s_balance)

        txtJ_Tution.Text = Convert_To_Currency(j_tution)
        txtJ_TuitionPayment.Text = Convert_To_Currency(j_tutionpayment)
        txtJ_Others.Text = Convert_To_Currency(j_others)
        txtJ_OthersPayment.Text = Convert_To_Currency(j_otherspayment)
        txtJ_Balance.Text = Convert_To_Currency(j_balance)
    End Sub

    Private Function SearchStudent(DG As DataGridView, Search As String) As Integer
        Dim location_result As Integer = 0
        Dim result As DataGridViewCell() = (From row As DataGridViewRow In DG.Rows
                                    From cell As DataGridViewCell In row.Cells
                                    Select cell Where CStr(cell.FormattedValue).Contains(Search.ToString.ToUpper)).ToArray()
        If result.Count > 0 Then
            location_result = result(0).RowIndex
        Else
            location_result = 0
        End If
        Return location_result
    End Function

    Private Sub frm_balance_checker_revised_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTracker("")
    End Sub

    Private Sub txtSearchCollege_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchCollege.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadTracker(txtSearchCollege.Text)
        'Dim result As Integer = SearchStudent(dgCollege, txtSearchCollege.Text)
        'dgCollege.ClearSelection()
        'dgCollege.Rows(result).Selected = True
        'dgCollege.FirstDisplayedScrollingRowIndex = result
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs)
        'If e.KeyCode = Keys.Enter Then
        '    Button2.PerformClick()
        'End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs)
        'Dim result As Integer = SearchStudent(dgSenior, txtSearchSenior.Text)
        'dgSenior.ClearSelection()
        'dgSenior.Rows(result).Selected = True
        'dgSenior.FirstDisplayedScrollingRowIndex = result
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        'Dim result As Integer = SearchStudent(dgJunior, txtSearchJunior.Text)
        'dgJunior.ClearSelection()
        'dgJunior.Rows(result).Selected = True
        'dgJunior.FirstDisplayedScrollingRowIndex = result
    End Sub

    Private Sub txtSearchJunior_KeyDown(sender As Object, e As KeyEventArgs)
        'If e.KeyCode = Keys.Enter Then
        '    Button3.PerformClick()
        'End If
    End Sub

    Private Sub txtSearchCollege_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCollege.TextChanged

    End Sub
End Class