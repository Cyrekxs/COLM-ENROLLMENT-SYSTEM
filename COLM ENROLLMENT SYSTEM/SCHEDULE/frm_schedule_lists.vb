Public Class frm_schedule_lists

    Private Sub LoadAvailableSections()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT *,(SELECT CurriculumCode FROM tbl_settings_college_curriculum WHERE CurriculumCourse = tbl_settings_sections.Course_Code AND CurriculumType = tbl_settings_sections.Curriculum_Type) AS Curriculum_Code FROM tbl_settings_sections WHERE Academic_Year = @ay AND Academic_Sem = @sem AND Education_Level = 'COLLEGE' ORDER BY Course_Code,Yrlvl ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"),
                                               reader("Curriculum_Code"),
                                               reader("Curriculum_Type"),
                                               reader("Course_Code"),
                                               reader("Yrlvl"),
                                               reader("Section_Code"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frm_schedule_entry
            .SavingStatus = SavingOptions.NEW
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub frm_schedule_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAvailableSections()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 6 Then
            With frm_schedule_entry
                .SavingStatus = SavingOptions.EDIT
                .SectionID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                .CurriculumCode = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                .CurriculumType = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                .CourseCode = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                .Yrlvl = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                .SectionCode = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        End If
    End Sub
End Class