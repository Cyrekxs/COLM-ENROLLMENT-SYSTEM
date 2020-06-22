Public Class frm_college_settings_yrlvl_lists
    Public DGRow As Integer = 0
    Public EducationLevel As String = String.Empty
    Public Sub LoadYrLevels()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE YEAR_CODE LIKE @search AND EDUCATION_LEVEL = @education_level ORDER BY COURSE_CODE,ORDER_NO ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@education_level", EducationLevel)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("YEAR_ID"), reader("COURSE_CODE"), reader("YEAR_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        With frm_college_settings_yrlvl_entry
            .SavingStatus = "NEW"
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        With frm_college_settings_yrlvl_entry
            .SavingStatus = "EDIT"
            .YrlvlID = DataGridView1.Rows(DGRow).Cells(0).Value
            .CourseCode = DataGridView1.Rows(DGRow).Cells(1).Value
            .YrLvl = DataGridView1.Rows(DGRow).Cells(2).Value
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub frm_yrlvl_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadYrLevels()
    End Sub
End Class