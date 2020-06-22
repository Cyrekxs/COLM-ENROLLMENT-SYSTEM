Public Class frm_college_assessment_subject_list
    Public Course As String = String.Empty

    Public Sub Load_Subjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT tbl_settings_college_curriculum_subjects_setted.YRLVL,tbl_settings_college_curriculum_subjects_setted.SUBJ_CODE,tbl_settings_college_curriculum_subjects_setted.SUBJ_DESC,tbl_settings_college_curriculum_subjects_setted.SUBJ_UNIT,tbl_settings_college_curriculum_subjects_setted.LEC_HOURS,tbl_settings_college_curriculum_subjects_setted.LAB_HOURS,tbl_settings_college_curriculum_subjects_setted.SUBJ_PRICE FROM tbl_settings_college_curriculum_subjects_setted INNER JOIN tbl_settings_college_curriculum_subjects_schedule ON tbl_settings_college_curriculum_subjects_setted.ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_YEAR = tbl_settings_college_curriculum_subjects_schedule.ACADEMIC_YEAR AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_SEM = tbl_settings_college_curriculum_subjects_schedule.ACADEMIC_SEM WHERE tbl_settings_college_curriculum_subjects_schedule.COURSE_CODE = @course_code AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_YEAR = @ay AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_SEM = @sem AND IS_DELETED = 'FALSE' AND tbl_settings_college_curriculum_subjects_setted.YRLVL + SUBJ_CODE + SUBJ_DESC LIKE @search ORDER BY YRLVL,SUBJ_CODE ASC", conn)
                comm.Parameters.AddWithValue("@course_code", Course)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@search", "%" & StripSpaces(TextBox1.Text) & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_SUBJECTS.Rows.Clear()
                    While reader.Read
                        DG_SUBJECTS.Rows.Add(reader("YRLVL"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), Convert_To_Currency(reader("SUBJ_PRICE")), "ADD TO LIST")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_college_assessment_subject_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Subjects()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub DG_TFEE_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_SUBJECTS.CellContentClick
        If e.ColumnIndex = 7 Then
            With frm_college_assessment.DG_TFEE
                Dim Can_Add As Boolean = True
                For i = 0 To .Rows.Count - 1
                    If .Rows(i).Cells(0).Value = DG_SUBJECTS.Rows(e.RowIndex).Cells(1).Value Then
                        Can_Add = False
                        MsgBox("Subject is already in list!", MsgBoxStyle.Critical)
                        Exit For
                        Exit Sub
                    End If
                Next

                If Can_Add = True Then
                    .Rows.Add(DG_SUBJECTS.Rows(e.RowIndex).Cells(1).Value, DG_SUBJECTS.Rows(e.RowIndex).Cells(2).Value, DG_SUBJECTS.Rows(e.RowIndex).Cells(3).Value, DG_SUBJECTS.Rows(e.RowIndex).Cells(4).Value, DG_SUBJECTS.Rows(e.RowIndex).Cells(5).Value, DG_SUBJECTS.Rows(e.RowIndex).Cells(6).Value, "REMOVE")
                    frm_college_assessment.DG_Schedule.Rows.Add(DG_SUBJECTS.Rows(e.RowIndex).Cells(1).Value, "-", "-", "-", "-", "-")
                End If
            End With
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class