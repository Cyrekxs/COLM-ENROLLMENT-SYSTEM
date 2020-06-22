Public Class frm_college_assessment_browser_schedule
    Public DGSchedRow As Integer = 0
    Public SubjCode As String = String.Empty
    Private AlternateSubjCode As String = String.Empty
    Private Sub GetSubjectSchedule()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_SubjectSchedules() WHERE SubjCode = @SubjCode AND Academic_Year = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@SubjCode", SubjCode)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DGSched.Rows.Clear()
                    While reader.Read
                        DGSched.Rows.Add(reader("ScheduleID"), reader("Course_Code") & "/" & reader("Yrlvl") & "/" & reader("Section_Code"), reader("Sched_Day"), reader("Sched_Time_In"), reader("Sched_Time_Out"), reader("Sched_Room"), reader("Faculty_Name"))
                    End While
                End Using
            End Using
        End Using

        If DGSched.Rows.Count = 0 Then
            MsgBox("Program detected that there is no schedule setted on this subject", MsgBoxStyle.Exclamation)
            AlternateSubjCode = InputBox("Please enter an alternative subject code", "ALTERNATIVE SUBJECT CODE")
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM FN_College_SubjectSchedules() WHERE SubjCode LIKE @SubjCode AND Academic_Year = @ay AND Academic_Sem = @sem", conn)
                    comm.Parameters.AddWithValue("@SubjCode", AlternateSubjCode & "%")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DGSched.Rows.Clear()
                        While reader.Read
                            DGSched.Rows.Add(reader("ScheduleID"), reader("Course_Code") & "/" & reader("Yrlvl") & "/" & reader("Section_Code"), reader("Sched_Day"), reader("Sched_Time_In"), reader("Sched_Time_Out"), reader("Sched_Room"), reader("Faculty_Name"))
                        End While
                    End Using
                End Using
            End Using
        End If
    End Sub
    Private Sub frm_college_assessment_browser_schedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetSubjectSchedule()
    End Sub

    Private Sub DGSched_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGSched.CellContentClick
        Try
            If e.ColumnIndex = 7 Then
                With frm_college_assessment_entry
                    .DGSched.Rows(DGSchedRow).Cells(3).Value = DGSched.Rows(e.RowIndex).Cells(0).Value
                    .DGSched.Rows(DGSchedRow).Cells(4).Value = DGSched.Rows(e.RowIndex).Cells(2).Value
                    .DGSched.Rows(DGSchedRow).Cells(5).Value = DGSched.Rows(e.RowIndex).Cells(3).Value
                    .DGSched.Rows(DGSchedRow).Cells(6).Value = DGSched.Rows(e.RowIndex).Cells(4).Value
                    .DGSched.Rows(DGSchedRow).Cells(7).Value = DGSched.Rows(e.RowIndex).Cells(5).Value
                    .DGSched.Rows(DGSchedRow).Cells(8).Value = DGSched.Rows(e.RowIndex).Cells(6).Value
                    Me.Close()
                    Me.Dispose()
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AlternateSubjCode = InputBox("Please enter an alternative subject code", "ALTERNATIVE SUBJECT CODE")
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_SubjectSchedules() WHERE SubjCode LIKE @SubjCode AND Academic_Year = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@SubjCode", AlternateSubjCode & "%")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DGSched.Rows.Clear()
                    While reader.Read
                        DGSched.Rows.Add(reader("ScheduleID"), reader("Course_Code") & "/" & reader("Yrlvl") & "/" & reader("Section_Code"), reader("Sched_Day"), reader("Sched_Time_In"), reader("Sched_Time_Out"), reader("Sched_Room"), reader("Faculty_Name"))
                    End While
                End Using
            End Using
        End Using
    End Sub
End Class