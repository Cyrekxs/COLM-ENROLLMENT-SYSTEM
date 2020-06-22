Public Class frm_college_section_lists

    Private Sub Load_Sections()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT *,(SELECT COUNT(DISTINCT(SUBJ_ID)) FROM TBL_SETTINGS_SUBJECT_SCHED WHERE SECTION_CODE = TBL_SETTINGS_SECTIONS.ID AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @SEM) AS NO_OF_SUBJECTS FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL = 'COLLEGE' AND COURSE_CODE + YRLVL + SECTION_CODE LIKE @search AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem ORDER BY COURSE_CODE,YRLVL,SECTION_CODE ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECTION_CODE"), reader("NO_OF_SUBJECTS"), "EDIT", "DELETE", "VIEW")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_college_section_entry
            .Section_ID = 0
            .cmbCourse.Enabled = True
            .cmbYearLvl.Enabled = True
            .txtSectionName.Enabled = True
            .btnSave.Text = "SAVE"
            .Saving_Status = "NEW"
            .StartPosition = FormStartPosition.CenterParent
            Me.Hide()
            .ShowDialog()
            Load_Sections()
            Me.Show()
        End With
    End Sub

    Private Sub frm_college_section_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Sections()
        DataGridView2.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.ColumnIndex = 5 Then
            With frm_college_section_entry
                .Section_ID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
                .cmbCourse.Items.Add(DataGridView2.Rows(e.RowIndex).Cells(1).Value)
                .cmbCourse.Text = DataGridView2.Rows(e.RowIndex).Cells(1).Value
                .cmbYearLvl.Items.Add(DataGridView2.Rows(e.RowIndex).Cells(2).Value)
                .cmbYearLvl.Text = DataGridView2.Rows(e.RowIndex).Cells(2).Value
                .txtSectionName.Text = DataGridView2.Rows(e.RowIndex).Cells(3).Value
                .txtSectionName.Enabled = True
                .Load_Subjects()
                .btnSave.Text = "UPDATE"
                .Saving_Status = "EDIT"
                .StartPosition = FormStartPosition.CenterParent
                Me.Hide()
                .ShowDialog()
                Load_Sections()
                Me.Show()
            End With
        ElseIf e.ColumnIndex = 6 Then

        ElseIf e.ColumnIndex = 7 Then
            With frm_college_section_entry
                .Section_ID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
                .cmbCourse.Items.Add(DataGridView2.Rows(e.RowIndex).Cells(1).Value)
                .cmbCourse.Text = DataGridView2.Rows(e.RowIndex).Cells(1).Value
                .cmbYearLvl.Items.Add(DataGridView2.Rows(e.RowIndex).Cells(2).Value)
                .cmbYearLvl.Text = DataGridView2.Rows(e.RowIndex).Cells(2).Value
                .txtSectionName.Text = DataGridView2.Rows(e.RowIndex).Cells(3).Value
                .txtSectionName.Enabled = False
                .Load_Subjects()

                .btnSave.Text = "DONE"
                .Saving_Status = "VIEW"
                .StartPosition = FormStartPosition.CenterParent
                Me.Hide()
                .ShowDialog()
                Me.Show()
            End With
        End If
    End Sub
End Class