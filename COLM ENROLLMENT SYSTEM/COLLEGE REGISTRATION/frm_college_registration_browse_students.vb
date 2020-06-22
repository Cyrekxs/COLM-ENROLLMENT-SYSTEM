Public Class frm_college_registration_browse_students
    Private Sub LoadUnregisteredCollegeStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_UnregisteredStudents() WHERE Lastname + ' ' + Firstname LIKE @search ORDER BY Lastname,Firstname ASC", conn)
                comm.Parameters.AddWithValue("@search", TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("Student_Number"), reader("lastname") & " " & reader("Firstname") & " " & reader("Middlename"), LSet(reader("Gender"), 1))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_college_registration_browse_students_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUnregisteredCollegeStudents()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 3 Then
            With frm_college_registration_entry
                .txtStudentNo.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                .txtStudentName.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                .txtGender.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                Me.Close()
                Me.Dispose()
            End With
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadUnregisteredCollegeStudents()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadUnregisteredCollegeStudents()
    End Sub
End Class