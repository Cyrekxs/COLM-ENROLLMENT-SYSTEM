Public Class frm_schedule_entry_2
    Public SchedID As Integer = 0
    Public Sched_Day As String = String.Empty
    Public Sched_TimeIn As String = String.Empty
    Public Sched_TimeOut As String = String.Empty
    Public Sched_Room As String = String.Empty
    Public Sched_Faculty As String = String.Empty

    Public Sub LoadFaculties()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_FACULTY_LIST ORDER BY LASTNAME,FIRSTNAME ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbFaculty.Items.Clear()
                    cmbFaculty.Items.Add("-")
                    While reader.Read
                        cmbFaculty.Items.Add(reader("Lastname") & ", " & reader("Firstname"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            'UPDATING SCHEDULE OF ALL STUDENTS WHO HAVE THE SAME SCHEDULE BEFORE
            Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET SCHED_DAY = @day, SCHED_TIME_IN = @time_in, SCHED_TIME_OUT = @time_out, SCHED_ROOM = @room, SCHED_FACULTY = @faculty_name WHERE SUBJ_CODE = @subj_code AND SCHED_DAY = @old_day AND SCHED_TIME_IN = @old_time_in AND SCHED_TIME_OUT = @old_time_out AND SCHED_ROOM = @old_room AND SCHED_FACULTY = @old_faculty AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@subj_code", txtSubjCode.Text)
                comm.Parameters.AddWithValue("@old_day", Sched_Day)
                comm.Parameters.AddWithValue("@old_time_in", Sched_TimeIn)
                comm.Parameters.AddWithValue("@old_time_out", Sched_TimeOut)
                comm.Parameters.AddWithValue("@old_room", Sched_Room)
                comm.Parameters.AddWithValue("@old_faculty", Sched_Faculty)

                comm.Parameters.AddWithValue("@day", txtDay.Text)
                comm.Parameters.AddWithValue("@time_in", txtStart.Text)
                comm.Parameters.AddWithValue("@time_out", txtEnd.Text)
                comm.Parameters.AddWithValue("@room", txtRoom.Text)
                comm.Parameters.AddWithValue("@faculty_name", cmbFaculty.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.ExecuteNonQuery()
            End Using

            'UPDATING INFORMATION INTO TABLE SUBJECT SCHED
            Using comm As New SqlCommand("UPDATE tbl_settings_college_curriculum_subjects_schedule SET SCHED_DAY = @day, SCHED_TIME_IN = @time_in, SCHED_TIME_OUT = @time_out, SCHED_ROOM = @room, FACULTY_NAME = @faculty_name WHERE ID = @id", conn)
                comm.Parameters.AddWithValue("@id", SchedID)
                comm.Parameters.AddWithValue("@day", txtDay.Text)
                comm.Parameters.AddWithValue("@time_in", txtStart.Text)
                comm.Parameters.AddWithValue("@time_out", txtEnd.Text)
                comm.Parameters.AddWithValue("@room", txtRoom.Text)
                comm.Parameters.AddWithValue("@faculty_name", cmbFaculty.Text)
                comm.ExecuteNonQuery()
                MsgBox("Schedule has been successfully updated!", MsgBoxStyle.Information)
                Me.Close()
                Me.Dispose()
            End Using
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class