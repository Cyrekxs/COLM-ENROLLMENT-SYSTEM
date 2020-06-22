Public Class frm_faculty_lists

    Private Sub LoadFaculties()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_faculty_list WHERE Lastname + ' '  + Firstname LIKE @search ORDER BY Lastname,Firstname ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("FacultyID"),
                                               reader("Lastname") & " " & reader("Firstname") & " " & reader("Middlename"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_faculty_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFaculties()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadFaculties()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadFaculties()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_faculty_lists_entry
            .SavingStatus = SavingOptions.NEW
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 2 Then
            With frm_faculty_lists_entry
                .SavingStatus = SavingOptions.EDIT
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        ElseIf e.ColumnIndex = 3 Then
            With frm_faculty_account
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM tbl_faculty_login WHERE Faculty_ID = @FacultyID", conn)
                        comm.Parameters.AddWithValue("@FacultyID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            If reader.HasRows = True Then
                                While reader.Read
                                    .FacultyAccountID = reader("FacultyAccountID")
                                    .txtFacultyName.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                                End While
                                .SavingStatus = SavingOptions.EDIT
                                .StartPosition = FormStartPosition.CenterScreen
                                .ShowDialog()
                            Else
                                .FacultyID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                                .txtFacultyName.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                                .SavingStatus = SavingOptions.NEW
                                .StartPosition = FormStartPosition.CenterScreen
                                .ShowDialog()
                            End If
                        End Using
                    End Using
                End Using
            End With
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class