Public Class frm_college_assessment_browse_schedule

    Public DGRow As Integer = 0
    Public DGSchedRow As Integer = 0
    Public CurriculumID As Integer = frm_college_assessment.DG_TFEE.Rows(DGSchedRow).Cells(11).Value
    Public Sub LoadAvailableSchedules()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT tbl_settings_college_curriculum_subjects_setted.COURSE_CODE,tbl_settings_college_curriculum_subjects_setted.YRLVL,(SELECT SECTION_CODE FROM TBL_SETTINGS_SECTIONS WHERE ID = tbl_settings_college_curriculum_subjects_schedule.SECTION_CODE) AS SECTION_CODE,SCHED_DAY,SCHED_TIME_IN,SCHED_TIME_OUT,SCHED_ROOM,FACULTY_NAME FROM tbl_settings_college_curriculum_subjects_setted INNER JOIN tbl_settings_college_curriculum_subjects_schedule ON tbl_settings_college_curriculum_subjects_setted.ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID WHERE tbl_settings_college_curriculum_subjects_schedule.ACADEMIC_YEAR = @ay AND tbl_settings_college_curriculum_subjects_schedule.ACADEMIC_SEM = @sem AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_YEAR = @ay AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_SEM = @sem AND (SUBJ_CODE = @subj_code OR SUBJ_DESC = @subj_desc) ORDER BY tbl_settings_college_curriculum_subjects_setted.COURSE_CODE,tbl_settings_college_curriculum_subjects_setted.YRLVL,SECTION_CODE ASC", conn)
                comm.Parameters.AddWithValue("@subj_code", txtSubjCode.Text)
                comm.Parameters.AddWithValue("@subj_desc", txtSubjDesc.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_Schedule.Rows.Clear()
                    While reader.Read
                        DG_Schedule.Rows.Add(reader("COURSE_CODE") & " " & reader("YRLVL") & " " & reader("SECTION_CODE"), reader("SCHED_DAY"), reader("SCHED_TIME_IN"), reader("SCHED_TIME_OUT"), reader("SCHED_ROOM"), reader("FACULTY_NAME"))
                    End While
                End Using
            End Using

            Using comm As New SqlCommand("SELECT * FROM tbl_settings_same_subjects", conn)

            End Using
        End Using
    End Sub

    Private Sub frm_college_assessment_browse_schedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAvailableSchedules()
    End Sub

    Private Sub DG_Schedule_DoubleClick(sender As Object, e As EventArgs) Handles DG_Schedule.DoubleClick
        With frm_college_assessment
            .DG_Schedule.Rows(DGSchedRow).Cells(1).Value = DG_Schedule.Rows(DGRow).Cells(1).Value
            .DG_Schedule.Rows(DGSchedRow).Cells(2).Value = DG_Schedule.Rows(DGRow).Cells(2).Value
            .DG_Schedule.Rows(DGSchedRow).Cells(3).Value = DG_Schedule.Rows(DGRow).Cells(3).Value
            .DG_Schedule.Rows(DGSchedRow).Cells(4).Value = DG_Schedule.Rows(DGRow).Cells(4).Value
            .DG_Schedule.Rows(DGSchedRow).Cells(5).Value = DG_Schedule.Rows(DGRow).Cells(5).Value
        End With
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub DG_Schedule_KeyDown(sender As Object, e As KeyEventArgs) Handles DG_Schedule.KeyDown
        If e.KeyCode = Keys.Enter Then
            With frm_college_assessment
                .DG_Schedule.Rows(DGSchedRow).Cells(1).Value = DG_Schedule.Rows(DGRow).Cells(1).Value
                .DG_Schedule.Rows(DGSchedRow).Cells(2).Value = DG_Schedule.Rows(DGRow).Cells(2).Value
                .DG_Schedule.Rows(DGSchedRow).Cells(3).Value = DG_Schedule.Rows(DGRow).Cells(3).Value
                .DG_Schedule.Rows(DGSchedRow).Cells(4).Value = DG_Schedule.Rows(DGRow).Cells(4).Value
                .DG_Schedule.Rows(DGSchedRow).Cells(5).Value = DG_Schedule.Rows(DGRow).Cells(5).Value
            End With
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub DG_Schedule_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_Schedule.CellContentClick

    End Sub

    Private Sub DG_Schedule_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DG_Schedule.RowEnter
        DGRow = e.RowIndex
    End Sub
End Class