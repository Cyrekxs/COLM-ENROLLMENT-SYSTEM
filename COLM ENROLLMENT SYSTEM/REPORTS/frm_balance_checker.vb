Public Class frm_balance_checker

    Private Sub LoadOldBalances()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM vw_student_balance_summary WHERE StudentName IS NOT NULL ORDER BY StudentName ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        Dim TuitionFee As Double = reader("TuitionFee")
                        Dim TuitionPayment As Double = reader("TuitionPayment")
                        Dim OtherFee As Double = reader("OtherFees")
                        Dim OtherFeePayment As Double = reader("OtherFeesPayment")
                        Dim Balance As Double = (TuitionFee + OtherFee) - (TuitionPayment + OtherFeePayment)

                        Select Case reader("Education_Level")
                            Case "COLLEGE"
                                dgCollege.Rows.Add(reader("Student_Number"),
                                                   reader("StudentName"),
                                                   reader("Academic_Yr") & "-" & reader("Academic_Sem"),
                                                   Convert_To_Currency(reader("TuitionFee")),
                                                   Convert_To_Currency(reader("TuitionPayment")),
                                                   Convert_To_Currency(reader("OtherFees")),
                                                   Convert_To_Currency(reader("OtherFeesPayment")), Convert_To_Currency(Balance))
                            Case "SENIOR HIGH"
                                dgSenior.Rows.Add(reader("Student_Number"),
                                                   reader("StudentName"),
                                                   reader("Academic_Yr") & "-" & reader("Academic_Sem"),
                                                   Convert_To_Currency(reader("TuitionFee")),
                                                   Convert_To_Currency(reader("TuitionPayment")),
                                                   Convert_To_Currency(reader("OtherFees")),
                                                   Convert_To_Currency(reader("OtherFeesPayment")), Convert_To_Currency(Balance))
                            Case "JUNIOR HIGH"
                                dgJunior.Rows.Add(reader("Student_Number"),
                                                   reader("StudentName"),
                                                   reader("Academic_Yr") & "-" & reader("Academic_Sem"),
                                                   Convert_To_Currency(reader("TuitionFee")),
                                                   Convert_To_Currency(reader("TuitionPayment")),
                                                   Convert_To_Currency(reader("OtherFees")),
                                                   Convert_To_Currency(reader("OtherFeesPayment")), Convert_To_Currency(Balance))
                        End Select
                    End While
                End Using
            End Using
        End Using
        'Using conn As New SqlConnection(StringConnection)
        '    conn.Open()
        '    If ComboBox1.Text = "ALL" Then
        '        Using comm As New SqlCommand("SELECT * FROM FN_BalanceChecker() WHERE StudentName LIKE @search ORDER BY StudentName ASC", conn)
        '            comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
        '            Using reader As SqlDataReader = comm.ExecuteReader
        '                dgCollege.Rows.Clear()
        '                While reader.Read
        '                    dgCollege.Rows.Add(reader("AssessmentID"), reader("Student_Number"), reader("StudentName"), Convert_To_Currency(reader("Total")), Convert_To_Currency(reader("TotalPayment")), Convert_To_Currency(reader("Balance")), reader("Academic_Yr"), reader("Academic_Sem"), Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"), reader("Assess_By"))
        '                End While
        '            End Using
        '        End Using
        '    ElseIf ComboBox1.Text = "BALANCE ONLY" Then
        '        Using comm As New SqlCommand("SELECT * FROM FN_BalanceChecker() WHERE StudentName LIKE @search AND Balance > 10 ORDER BY StudentName ASC", conn)
        '            comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
        '            Using reader As SqlDataReader = comm.ExecuteReader
        '                dgCollege.Rows.Clear()
        '                While reader.Read
        '                    dgCollege.Rows.Add(reader("AssessmentID"), reader("Student_Number"), reader("StudentName"), Convert_To_Currency(reader("Total")), Convert_To_Currency(reader("TotalPayment")), Convert_To_Currency(reader("Balance")), reader("Academic_Yr"), reader("Academic_Sem"), Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"), reader("Assess_By"))
        '                End While
        '            End Using
        '        End Using
        '    ElseIf ComboBox1.Text = "OVER PAYMENT" Then
        '        Using comm As New SqlCommand("SELECT * FROM FN_BalanceChecker() WHERE StudentName LIKE @search AND Balance < -10 ORDER BY StudentName ASC", conn)
        '            comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
        '            Using reader As SqlDataReader = comm.ExecuteReader
        '                dgCollege.Rows.Clear()
        '                While reader.Read
        '                    dgCollege.Rows.Add(reader("AssessmentID"), reader("Student_Number"), reader("StudentName"), Convert_To_Currency(reader("Total")), Convert_To_Currency(reader("TotalPayment")), Convert_To_Currency(reader("Balance")), reader("Academic_Yr"), reader("Academic_Sem"), Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"), reader("Assess_By"))
        '                End While
        '            End Using
        '        End Using
        '    End If

        '    Using comm As New SqlCommand("SELECT SUM(Balance) AS OverCollected FROM FN_BalanceChecker() WHERE Balance < -10", conn)
        '        Using reader As SqlDataReader = comm.ExecuteReader
        '            While reader.Read
        '                txtOverCollected.Text = Convert_To_Currency(reader("OverCollected"))
        '            End While
        '        End Using
        '    End Using

        '    Using comm As New SqlCommand("SELECT SUM(Balance) AS TotalBalance FROM FN_BalanceChecker() WHERE Balance > 10", conn)
        '        Using reader As SqlDataReader = comm.ExecuteReader
        '            While reader.Read
        '                txtUncollected.Text = Convert_To_Currency(reader("TotalBalance"))
        '            End While
        '        End Using
        '    End Using

        'End Using
    End Sub
    Private Sub frm_balance_checker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadOldBalances()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadOldBalances()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadOldBalances()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadOldBalances()
    End Sub
End Class