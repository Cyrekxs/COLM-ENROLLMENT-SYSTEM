Public Class frm_college_settings_course_list
    Public DGRow As Integer = 0
    Public Sub LoadCourses()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_CODE LIKE @search", conn)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("COURSE_ID"), reader("COURSE_CODE"), reader("COURSE_DESC"), reader("DEPARTMENT"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_course_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCourses()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadCourses()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadCourses()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        With frm_college_settings_course_entry
            .Saving_Status = "NEW"
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        With frm_college_settings_course_entry
            .Saving_Status = "EDIT"
            .ID = DataGridView1.Rows(DGRow).Cells(0).Value
            .txtCourseCode.Text = DataGridView1.Rows(DGRow).Cells(1).Value
            .txtCourseDescription.Text = DataGridView1.Rows(DGRow).Cells(2).Value
            .txtDepartment.Text = DataGridView1.Rows(DGRow).Cells(3).Value
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MsgBox("Are you sure you want to delete this course?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("DELETE FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_ID = @course_id", conn)
                    comm.Parameters.AddWithValue("@course_id", DataGridView1.Rows(DGRow).Cells(0).Value)
                    comm.ExecuteNonQuery()
                    DataGridView1.Rows.Remove(DataGridView1.Rows(DGRow))
                    MsgBox("Course has been successfully deleted!", MsgBoxStyle.Information)
                End Using
            End Using
        End If
    End Sub
End Class