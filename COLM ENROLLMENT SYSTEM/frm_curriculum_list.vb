Public Class frm_curriculum_list
    Public Sub Load_Curriculum()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_CURRICULUM WHERE COURSE_CODE LIKE @course_code AND YRLVL LIKE @yrlvl AND ACADEMIC_SEM LIKE @sem AND CURRICULUM_STATUS <> 'DELETED' AND SUBJ_CODE + SUBJ_DESC LIKE @search ORDER BY ACADEMIC_SEM,COURSE_CODE,YRLVL,SUBJ_CODE ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@course_code", "%" & cmbCourseCode.Text & "%")
                comm.Parameters.AddWithValue("@yrlvl", "%" & cmbYrLvl.Text & "%")
                comm.Parameters.AddWithValue("@sem", "%" & cmbAcademicSem.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("CURRICULUM_ID"), reader("COURSE_CODE").ToString.ToUpper, reader("YRLVL").ToString.ToUpper, reader("ACADEMIC_SEM").ToString.ToUpper, reader("SUBJ_CODE").ToString.ToUpper, reader("SUBJ_DESC").ToString.ToUpper, reader("SUBJ_UNIT").ToString.ToUpper, reader("LEC_HOURS").ToString.ToUpper, reader("LAB_HOURS").ToString.ToUpper, Convert_To_Currency(reader("SUBJ_PRICE")), "EDIT", "DELETE")
                    End While
                End Using
                lblTotalSubjects.Text = DataGridView1.Rows.Count
            End Using
        End Using
    End Sub
    Private Sub frm_curriculum_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Course_Codes(cmbCourseCode)
        DataGridView1.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns(9).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Load_Curriculum()
    End Sub

    Private Sub cmbCourseCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourseCode.SelectedIndexChanged
        Load_Curriculum()
    End Sub

    Private Sub cmbYrLvl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYrLvl.SelectedIndexChanged
        Load_Curriculum()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_curriculum_entry
            .Saving_Status = "NEW"
            Load_Course_Codes(.cmbCourse)
            .cmbAcademicSem.Text = cmbAcademicSem.Text
            .cmbCourse.Text = cmbCourseCode.Text
            .cmbYear.Text = cmbYrLvl.Text

            .txtSubjCode.Text = String.Empty
            .txtSubjDesc.Text = String.Empty
            .txtUnit.Text = String.Empty
            .txtLecHours.Text = String.Empty
            .txtLabHours.Text = String.Empty
            .txtAmount.Text = String.Empty

            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With

    End Sub

    Private Sub cmbAcademicSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAcademicSem.SelectedIndexChanged
        Load_Curriculum()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 10 Then
            With frm_curriculum_entry
                frm_curriculum_entry.Saving_Status = "EDIT"
                Load_Course_Codes(frm_curriculum_entry.cmbCourse)
                frm_curriculum_entry.cmbAcademicSem.Text = cmbAcademicSem.Text
                frm_curriculum_entry.cmbCourse.Text = cmbCourseCode.Text
                frm_curriculum_entry.cmbYear.Text = cmbYrLvl.Text

                .ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                .txtSubjCode.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                .txtSubjDesc.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                .txtUnit.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
                .txtLecHours.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value
                .txtLabHours.Text = DataGridView1.Rows(e.RowIndex).Cells(8).Value
                .txtAmount.Text = DataGridView1.Rows(e.RowIndex).Cells(9).Value
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        ElseIf e.ColumnIndex = 11 Then
            If MsgBox("Are you sure you want to delete this subject?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("UPDATE TBL_COLLEGE_CURRICULUM SET CURRICULUM_STATUS = 'DELETED' WHERE CURRICULUM_ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                        comm.ExecuteNonQuery()
                        MsgBox("Curriculum has been successfully removed!", MsgBoxStyle.Critical)
                        DataGridView1.Rows.Remove(DataGridView1.Rows(e.RowIndex))
                    End Using
                End Using
            End If
        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Curriculum()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Load_Curriculum()
    End Sub
End Class