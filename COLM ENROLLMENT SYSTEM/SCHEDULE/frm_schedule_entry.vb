Public Class frm_schedule_entry
    Private CurriculumID As Integer = 0
    Public SectionID As Integer = 0
    Public CurriculumCode As String = String.Empty
    Public CurriculumType As String = String.Empty
    Public CourseCode As String = String.Empty
    Public Yrlvl As String = String.Empty
    Public SectionCode As String = String.Empty
    Public SavingStatus As SavingOptions
    Public DGRow As Integer = 0
    Dim ListofFaculties As New List(Of String)
    Private Sub LoadCurriculumCodes()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT CurriculumCode FROM tbl_settings_college_curriculum ORDER BY CurriculumCode ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCurriculumCode.Items.Clear()
                    While reader.Read
                        cmbCurriculumCode.Items.Add(reader("CurriculumCode"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadCurriculumType()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT CurriculumType FROM tbl_settings_college_curriculum WHERE CurriculumCode = @CurriculumCode ORDER BY CurriculumType ASC", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", cmbCurriculumCode.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCurriculumType.Items.Clear()
                    While reader.Read
                        cmbCurriculumType.Items.Add(reader("CurriculumType"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub GetCourse()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT CurriculumCourse FROM tbl_settings_college_curriculum WHERE CurriculumCode = @CurriculumCode AND CurriculumType = @CurriculumType", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", cmbCurriculumCode.Text)
                comm.Parameters.AddWithValue("@CurriculumType", cmbCurriculumType.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        CourseCode = reader("CurriculumCourse")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub GetCurriculumID()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT CurriculumID FROM tbl_settings_college_curriculum WHERE CurriculumCode = @CurriculumCode AND CurriculumType = @CurriculumType", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", cmbCurriculumCode.Text)
                comm.Parameters.AddWithValue("@CurriculumType", cmbCurriculumType.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        CurriculumID = reader("CurriculumID")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadYrLevels()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT YRLVL FROM tbl_settings_college_curriculum_subjects_setted WHERE CurriculumID = @CurriculumID AND Academic_Year = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@CurriculumID", CurriculumID)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                cmbYearLevel.Items.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        cmbYearLevel.Items.Add(reader("YrLvl"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadFaculties()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_faculty_list ORDER BY Lastname,Firstname ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    ListofFaculties.Clear()
                    ListofFaculties.Add("-")
                    While reader.Read
                        ListofFaculties.Add(reader("Lastname") & ", " & reader("Firstname"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadSubjectSetted()
        For i = 0 To ListofFaculties.Count - 1
            Column6.Items.Add(ListofFaculties(i))
        Next

        If SavingStatus = SavingOptions.NEW Then
            Dim SQLQuery As String = "SELECT ID AS SettedSubjID,(SELECT SubjCode FROM dbo.FN_College_CurriculumSubjects() WHERE CurriculumID = SETTED.CurriculumID AND CurriculumSubjID = SETTED.Subj_ID) AS SubjCode,(SELECT SubjDesc FROM dbo.FN_College_CurriculumSubjects() WHERE CurriculumID = SETTED.CurriculumID AND CurriculumSubjID = SETTED.Subj_ID) AS SubjDesc FROM tbl_settings_college_curriculum_subjects_setted AS SETTED WHERE Academic_Year = @ay AND Academic_Sem = @sem AND Course_Code = @course_code AND YRLVL = @Yrlvl"
            Using conn As New SqlConnection(StringConnection)
                conn.Open()

                Using comm As New SqlCommand(SQLQuery, conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@course_code", CourseCode)
                    comm.Parameters.AddWithValue("@Yrlvl", cmbYearLevel.Text)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add(0, reader("SettedSubjID"), reader("SubjCode"), reader("SubjDesc"), "-", "-", "-", "-", "-")
                        End While
                    End Using
                End Using

            End Using
        ElseIf SavingStatus = SavingOptions.EDIT Then

            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM FN_College_SubjectSchedules() WHERE SectionID = @SectionID", conn)
                    comm.Parameters.AddWithValue("@SectionID", SectionID)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add(reader("ScheduleID"), reader("SettedSubjID"), reader("SubjCode"), reader("SubjDesc"), reader("Sched_Day"), reader("Sched_Time_In"), reader("Sched_Time_Out"), reader("Sched_Room"), reader("Faculty_Name"))
                        End While
                    End Using
                End Using
            End Using
        End If

    End Sub

    Private Sub frm_schedule_entry_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frm_schedule_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCurriculumCodes()
        LoadFaculties()

        If SavingStatus = SavingOptions.EDIT Then
            cmbCurriculumCode.Text = CurriculumCode
            cmbCurriculumType.Text = CurriculumType
            txtCourseCode.Text = CourseCode
            cmbYearLevel.Text = Yrlvl
            txtSectionCode.Text = SectionCode

            cmbCurriculumCode.Enabled = False
            cmbCurriculumType.Enabled = False
            txtCourseCode.Enabled = False
            cmbYearLevel.Enabled = False
            txtSectionCode.Enabled = False
        End If
    End Sub

    Private Sub cmbCurriculumCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurriculumCode.SelectedIndexChanged
        LoadCurriculumType()
    End Sub

    Private Sub cmbCurriculumType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurriculumType.SelectedIndexChanged
        GetCourse()
        txtCourseCode.Text = CourseCode
    End Sub

    Private Sub txtCourseCode_TextChanged(sender As Object, e As EventArgs) Handles txtCourseCode.TextChanged
        GetCurriculumID()
        LoadYrLevels()
    End Sub

    Private Sub cmbYearLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYearLevel.SelectedIndexChanged
        LoadSubjectSetted()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    If SavingStatus = SavingOptions.NEW Then
                        Using comm As New SqlCommand("INSERT INTO tbl_settings_sections VALUES (@EducationLevel,@CourseCode,@CurriculumType,@Yrlvl,@Section_Code,@ay,@sem)", conn, t)
                            comm.Parameters.AddWithValue("@EducationLevel", "COLLEGE")
                            comm.Parameters.AddWithValue("@CourseCode", txtCourseCode.Text)
                            comm.Parameters.AddWithValue("@CurriculumType", cmbCurriculumType.Text)
                            comm.Parameters.AddWithValue("@Yrlvl", cmbYearLevel.Text)
                            comm.Parameters.AddWithValue("@SectioN_Code", txtSectionCode.Text)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using

                        Using comm As New SqlCommand("SELECT MAX (ID) AS SectionID FROM tbl_settings_sections (NOLOCK) WHERE Section_Code = @Section_Code AND Course_Code = @CourseCode AND Yrlvl = @Yrlvl AND Curriculum_Type = @Curriculum_Type AND Academic_Year = @ay AND Academic_Sem = @sem", conn, t)
                            comm.Parameters.AddWithValue("@CourseCode", txtCourseCode.Text)
                            comm.Parameters.AddWithValue("@Curriculum_Type", cmbCurriculumType.Text)
                            comm.Parameters.AddWithValue("@Yrlvl", cmbYearLevel.Text)
                            comm.Parameters.AddWithValue("@SectioN_Code", txtSectionCode.Text)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            Using reader As SqlDataReader = comm.ExecuteReader
                                While reader.Read
                                    SectionID = reader("SectionID")
                                End While
                            End Using
                        End Using

                        For i = 0 To DataGridView1.Rows.Count - 1
                            Using comm As New SqlCommand("INSERT INTO tbl_settings_college_curriculum_subjects_schedule VALUES (@ay,@sem,@faculty,@course_code,@yrlvl,@section_code,@subj_id,@sched_day,@sched_time_in,@sched_time_out,@sched_room)", conn, t)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.Parameters.AddWithValue("@faculty", DataGridView1.Rows(i).Cells(8).Value)
                                comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                                comm.Parameters.AddWithValue("@yrlvl", cmbYearLevel.Text)
                                comm.Parameters.AddWithValue("@section_code", SectionID)
                                comm.Parameters.AddWithValue("@subj_id", DataGridView1.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@sched_day", DataGridView1.Rows(i).Cells(4).Value)
                                comm.Parameters.AddWithValue("@sched_time_in", DataGridView1.Rows(i).Cells(5).Value)
                                comm.Parameters.AddWithValue("@sched_time_out", DataGridView1.Rows(i).Cells(6).Value)
                                comm.Parameters.AddWithValue("@sched_room", DataGridView1.Rows(i).Cells(7).Value)
                                comm.ExecuteNonQuery()
                            End Using
                        Next
                    ElseIf SavingStatus = SavingOptions.EDIT Then
                        For i = 0 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(i).Cells(0).Value <> 0 Then
                                Using comm As New SqlCommand("UPDATE tbl_settings_college_curriculum_subjects_schedule SET Faculty_Name = @faculty, Sched_Day = @sched_day, Sched_Time_In =  @sched_time_in, Sched_Time_Out = @sched_time_out, Sched_Room = @sched_room WHERE ID = @ScheduleID", conn, t)
                                    comm.Parameters.AddWithValue("@ScheduleID", DataGridView1.Rows(i).Cells(0).Value)
                                    comm.Parameters.AddWithValue("@faculty", DataGridView1.Rows(i).Cells(8).Value)
                                    comm.Parameters.AddWithValue("@sched_day", DataGridView1.Rows(i).Cells(4).Value)
                                    comm.Parameters.AddWithValue("@sched_time_in", DataGridView1.Rows(i).Cells(5).Value)
                                    comm.Parameters.AddWithValue("@sched_time_out", DataGridView1.Rows(i).Cells(6).Value)
                                    comm.Parameters.AddWithValue("@sched_room", DataGridView1.Rows(i).Cells(7).Value)
                                    comm.ExecuteNonQuery()
                                End Using

                                Using comm As New SqlCommand("UPDATE tbl_college_subject_loads SET Sched_Day = @Sched_Day, Sched_Time_In = @Sched_Time_In, Sched_Time_Out = @Sched_Time_Out, Sched_Room = @Sched_Room, Sched_Faculty = @Sched_Faculty WHERE ScheduleID = @ScheduleID AND Academic_Yr = @ay AND Academic_Sem = @sem", conn, t)
                                    comm.Parameters.AddWithValue("@ScheduleID", DataGridView1.Rows(i).Cells(0).Value)
                                    comm.Parameters.AddWithValue("@sched_faculty", DataGridView1.Rows(i).Cells(8).Value)
                                    comm.Parameters.AddWithValue("@sched_day", DataGridView1.Rows(i).Cells(4).Value)
                                    comm.Parameters.AddWithValue("@sched_time_in", DataGridView1.Rows(i).Cells(5).Value)
                                    comm.Parameters.AddWithValue("@sched_time_out", DataGridView1.Rows(i).Cells(6).Value)
                                    comm.Parameters.AddWithValue("@sched_room", DataGridView1.Rows(i).Cells(7).Value)
                                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                    comm.ExecuteNonQuery()
                                End Using
                            ElseIf DataGridView1.Rows(i).Cells(0).Value = 0 Then

                                Using comm As New SqlCommand("INSERT INTO tbl_settings_college_curriculum_subjects_schedule VALUES (@ay,@sem,@faculty,@course_code,@yrlvl,@section_code,@subj_id,@sched_day,@sched_time_in,@sched_time_out,@sched_room)", conn, t)
                                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                    comm.Parameters.AddWithValue("@faculty", DataGridView1.Rows(i).Cells(8).Value)
                                    comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                                    comm.Parameters.AddWithValue("@yrlvl", cmbYearLevel.Text)
                                    comm.Parameters.AddWithValue("@section_code", SectionID)
                                    comm.Parameters.AddWithValue("@subj_id", DataGridView1.Rows(i).Cells(1).Value)
                                    comm.Parameters.AddWithValue("@sched_day", DataGridView1.Rows(i).Cells(4).Value)
                                    comm.Parameters.AddWithValue("@sched_time_in", DataGridView1.Rows(i).Cells(5).Value)
                                    comm.Parameters.AddWithValue("@sched_time_out", DataGridView1.Rows(i).Cells(6).Value)
                                    comm.Parameters.AddWithValue("@sched_room", DataGridView1.Rows(i).Cells(7).Value)
                                    comm.ExecuteNonQuery()
                                End Using

                            End If
                        Next
                    End If

                    t.Commit()
                    MsgBox("Schedule has been successfully saved!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                Catch ex As Exception
                    t.Rollback()
                    MsgBox("An error occured while processing information please try again!" & vbNewLine & "Error info: " & ex.Message, MsgBoxStyle.Critical)
                End Try
            End Using
        End Using
    End Sub

    Private Sub REMOVEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles REMOVEToolStripMenuItem.Click
        If MsgBox("Are you sure you want to remove this subject?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DataGridView1.Rows.Remove(DataGridView1.Rows(DGRow))
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_schedule_unlisted_subjects
            .Course_Code = txtCourseCode.Text
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MsgBox("Are you sure you want to remove this schedule?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DataGridView1.Rows.Remove(DataGridView1.Rows(DGRow))
        End If
    End Sub
End Class