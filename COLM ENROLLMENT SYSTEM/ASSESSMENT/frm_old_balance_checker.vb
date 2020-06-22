Public Class frm_old_balance_checker
    Public Student_Number As String = String.Empty
    Public totalbalance As Double = 0
    Public Sub LoadBalance()
        totalbalance = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_BalanceChecker() WHERE Student_Number = @sn AND NOT (Academic_Yr != @ay AND Academic_Sem != @sem)", conn)
                comm.Parameters.AddWithValue("@sn", Student_Number)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)

                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("AssessmentID"), reader("Academic_Yr"), reader("Academic_Sem"), Convert_To_Currency(reader("Total")), Convert_To_Currency(reader("TotalPayment")), Convert_To_Currency(CDbl(reader("Total")) - CDbl(reader("TotalPayment"))), Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"), reader("Assess_By"))
                        If (CDbl(reader("Total")) - CDbl(reader("TotalPayment"))) >= 10 Then
                            totalbalance += CDbl(reader("total"))
                        End If
                    End While
                End Using
            End Using
        End Using
        TextBox1.Text = Convert_To_Currency(totalbalance)
    End Sub
    Private Sub frm_old_balance_checker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBalance()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub
End Class