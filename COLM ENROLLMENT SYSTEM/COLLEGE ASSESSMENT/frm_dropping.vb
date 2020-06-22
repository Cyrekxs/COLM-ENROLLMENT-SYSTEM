Public Class frm_dropping
    Public AssessmentID As Integer = 0
    Public EducationLevel As EducationLevelLists
    Enum EducationLevelLists
        [COLLEGE]
        [SENIOR]
        [JUNIOR]
    End Enum
    Private Sub LoadStudentAssessmentBreakdown()
        DataGridView1.Rows.Clear()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            'LOAD STUDENT ASSESSMENT BREAKDOWN
            Dim Query As String = String.Empty
            If EducationLevel = EducationLevelLists.COLLEGE Then
                Query = "SELECT * FROM tbl_college_assessment_breakdown WHERE AssessmentID = @AssessmentID"
            ElseIf EducationLevel = EducationLevelLists.JUNIOR Then
                Query = "SELECT * FROM tbl_college_assessment_breakdown WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem"
            ElseIf EducationLevel = EducationLevelLists.SENIOR Then
                Query = "SELECT * FROM tbl_college_assessment_breakdown WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem"
            End If

            Dim AssessmentAmount As Double = 0
            Using comm As New SqlCommand(Query, conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        AssessmentAmount += CDbl(reader("Fee_Amount"))
                        DataGridView1.Rows.Add(reader("ID"), reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")), "0.00", reader("Due_Date"))
                    End While
                End Using
            End Using

            'LOAD PAYMENT
            Dim payment As Double = 0
            Using comm As New SqlCommand("SELECT SUM(Amount_Collected) AS AmountCollected FROM tbl_college_payment WHERE Student_Number = @sn AND Reciept_Status  = 'ACTIVE' AND Fee_Status = 'TUITION FEE' AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        payment = reader("AmountCollected")
                    End While
                End Using
            End Using

            txtAssessmentAmount.Text = Convert_To_Currency(AssessmentAmount)
            txtPaidAmount.Text = Convert_To_Currency(payment)

            For i = 0 To DataGridView1.Rows.Count - 1
                Dim Amount As Double = DataGridView1.Rows(i).Cells(2).Value
                If payment >= Amount Then
                    DataGridView1.Rows(i).Cells(3).Value = "0.00"
                    payment = payment - Amount

                ElseIf payment < Amount Then
                    DataGridView1.Rows(i).Cells(3).Value = Convert_To_Currency(Amount - payment)
                    payment = 0
                End If
            Next
        End Using
    End Sub
    Private Sub frm_dropping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudentAssessmentBreakdown()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("Are you sure you want to drop this student?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using t As SqlTransaction = conn.BeginTransaction
                    Try

                        'UPDATE ASSESSMENT SUMMARY THROUGH ASSESSMENT ID
                        Using comm As New SqlCommand("UPDATE tbl_college_assessment_summary SET PullOutStatus = 'INACTIVE' WHERE ID = @AssessmentID", conn, t)
                            comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                            comm.ExecuteNonQuery()
                        End Using

                        'INSERT REASON
                        Using comm As New SqlCommand("INSERT INTO tbl_college_assessment_dropped VALUES (@AssessmentID,@Reason,@DroppedBy,GETDATE())", conn, t)
                            comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                            comm.Parameters.AddWithValue("@Reason", txtReason.Text)
                            comm.Parameters.AddWithValue("@DroppedBy", Account_Name)
                            comm.ExecuteNonQuery()
                        End Using

                        'UPDATE ASSESSMENT BREAKDOWN USING LOOPINGS
                        For i = 0 To DataGridView1.Rows.Count - 1
                            Using comm As New SqlCommand("UPDATE tbl_college_assessment_breakdown SET Fee_Amount = @Fee_Amount WHERE ID = @ID", conn, t)
                                comm.Parameters.AddWithValue("@ID", DataGridView1.Rows(i).Cells(0).Value)
                                comm.Parameters.AddWithValue("@Fee_Amount", DataGridView1.Rows(i).Cells(2).Value)
                                comm.ExecuteNonQuery()
                            End Using
                        Next

                        t.Commit()

                        Using comm As New SqlCommand("INSERT INTO tbl_program_logs VALUES (@logType,@Logs,GETDATE(),@User)", conn)
                            comm.Parameters.AddWithValue("@logtype", "DROP")
                            comm.Parameters.AddWithValue("@Logs", String.Concat("Dropping of Student ", txtStudentNo.Text & " Named ", txtStudentName.Text, "In School Year ", Academic_Year, " ", Academic_Sem))
                            comm.Parameters.AddWithValue("@user", Account_Name)

                        End Using

                        MsgBox("Student dropping has been successfully done!", MsgBoxStyle.Information)
                        Me.Close()
                        Me.Dispose()
                    Catch ex As Exception
                        t.Rollback()
                        MsgBox("An error occurred while processing transaction" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
                    End Try
                End Using
            End Using
        End If

    End Sub
End Class