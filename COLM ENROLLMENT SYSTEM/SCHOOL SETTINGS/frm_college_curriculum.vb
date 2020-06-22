Public Class frm_college_curriculum
    Public DGRow As Integer = 0
    Public Sub LoadCurriculum()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects ORDER BY COURSE_CODE,YRLVL,ACADEMIC_SEM,SUBJ_CODE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("CURRICULUM_ID"), UCase(reader("ACADEMIC_SEM")), reader("COURSE_CODE"), reader("YRLVL"), reader("CHED_CODE"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), Convert_To_Currency(reader("SUBJ_PRICE")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_college_curriculum_2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCurriculum()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frm_college_curriculum_entry
            .Saving_Status = "NEW"
            Load_Course_Codes(.cmbCourse)
            .cmbAcademicSem.Text = String.Empty
            .cmbCourse.Text = String.Empty
            .cmbYear.Text = String.Empty

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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_college_curriculum_entry
            frm_college_curriculum_entry.Saving_Status = "EDIT"
            Load_Course_Codes(frm_college_curriculum_entry.cmbCourse)
            frm_college_curriculum_entry.cmbAcademicSem.Text = DataGridView1.Rows(DGRow).Cells(1).Value
            frm_college_curriculum_entry.cmbCourse.Text = DataGridView1.Rows(DGRow).Cells(2).Value
            frm_college_curriculum_entry.cmbYear.Text = DataGridView1.Rows(DGRow).Cells(3).Value

            .DGRow = DGRow
            .ID = DataGridView1.Rows(DGRow).Cells(0).Value
            .txtChedCode.Text = DataGridView1.Rows(DGRow).Cells(4).Value
            .txtSubjCode.Text = DataGridView1.Rows(DGRow).Cells(5).Value
            .txtSubjDesc.Text = DataGridView1.Rows(DGRow).Cells(6).Value
            .txtUnit.Text = DataGridView1.Rows(DGRow).Cells(7).Value
            .txtLecHours.Text = DataGridView1.Rows(DGRow).Cells(8).Value
            .txtLabHours.Text = DataGridView1.Rows(DGRow).Cells(9).Value
            .txtAmount.Text = DataGridView1.Rows(DGRow).Cells(10).Value
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MsgBox("Are you sure you want to delete subject: " & DataGridView1.Rows(DGRow).Cells(5).Value & " in the course course of: " & DataGridView1.Rows(DGRow).Cells(2).Value, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("DELETE FROM tbl_settings_college_curriculum_subjects WHERE CURRICULUM_ID = @id", conn)
                    comm.Parameters.AddWithValue("@id", DataGridView1.Rows(DGRow).Cells(0).Value)
                    comm.ExecuteNonQuery()
                    MsgBox("Successfully deleted!", MsgBoxStyle.Information)
                    DataGridView1.Rows.Remove(DataGridView1.Rows(DGRow))
                End Using
            End Using
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frm_college_curriculum_filtering.StartPosition = FormStartPosition.CenterParent
        frm_college_curriculum_filtering.ShowDialog()
    End Sub
End Class