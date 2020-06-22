Public Class frm_rfid_unregistered_lists
    Private Sub Load_SeniorHigh()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege(@Education_Level,@ay) WHERE Lastname + ' ' + Firstname LIKE @search ORDER BY Lastname,Firstname ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@education_level", "SENIOR HIGH")
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    dgSenior.Rows.Clear()
                    While reader.Read
                        dgSenior.Rows.Add(reader("Student_Number"), reader("Lastname") & " " & reader("Firstname") & " " & reader("Middlename"), reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), reader("Guardian_Name"), reader("Guardian_Relation"), reader("Guardian_Mobile"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Load_JuniorHigh()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege(@Education_Level,@ay) WHERE Lastname + ' ' + Firstname LIKE @search  ORDER BY Lastname,Firstname ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@education_level", "JUNIOR HIGH")
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    dgJunior.Rows.Clear()
                    While reader.Read
                        dgJunior.Rows.Add(reader("Student_Number"), reader("Lastname") & " " & reader("Firstname") & " " & reader("Middlename"), reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), reader("Guardian_Name"), reader("Guardian_Relation"), reader("Guardian_Mobile"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Load_College()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsCollege(@ay,@sem) WHERE StudentName LIKE @search ORDER BY StudentName ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    dgCollege.Rows.Clear()
                    While reader.Read
                        dgCollege.Rows.Add(reader("Student_Number"), reader("StudentName"), reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), reader("Guardian_Name"), reader("Guardian_Relation"), reader("Guardian_Mobile"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_rfid_unregistered_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub dgSenior_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSenior.CellContentClick
        If e.ColumnIndex = 8 Then 'SELECT DATA
            With frm_rfid_registered_lists
                .txtStudentNo.Text = dgSenior.Rows(e.RowIndex).Cells(0).Value
                .txtEducationLevel.Text = "SENIOR HIGH"
                .txtName.Text = dgSenior.Rows(e.RowIndex).Cells(1).Value
                .txtCourse.Text = dgSenior.Rows(e.RowIndex).Cells(2).Value
                .txtYear.Text = dgSenior.Rows(e.RowIndex).Cells(3).Value
                .txtSection.Text = dgSenior.Rows(e.RowIndex).Cells(4).Value
                .txtGuardian.Text = dgSenior.Rows(e.RowIndex).Cells(5).Value
                .txtRelation.Text = dgSenior.Rows(e.RowIndex).Cells(6).Value
                .txtMobile.Text = dgSenior.Rows(e.RowIndex).Cells(7).Value
            End With
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub dgJunior_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgJunior.CellContentClick
        If e.ColumnIndex = 8 Then 'SELECT DATA
            With frm_rfid_registered_lists
                .txtStudentNo.Text = dgJunior.Rows(e.RowIndex).Cells(0).Value
                .txtEducationLevel.Text = "JUNIOR HIGH"
                .txtName.Text = dgJunior.Rows(e.RowIndex).Cells(1).Value
                .txtCourse.Text = dgJunior.Rows(e.RowIndex).Cells(2).Value
                .txtYear.Text = dgJunior.Rows(e.RowIndex).Cells(3).Value
                .txtSection.Text = dgJunior.Rows(e.RowIndex).Cells(4).Value
                .txtGuardian.Text = dgJunior.Rows(e.RowIndex).Cells(5).Value
                .txtRelation.Text = dgJunior.Rows(e.RowIndex).Cells(6).Value
                .txtMobile.Text = dgJunior.Rows(e.RowIndex).Cells(7).Value
            End With
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub dgCollege_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCollege.CellContentClick
        If e.ColumnIndex = 8 Then 'SELECT DATA
            With frm_rfid_registered_lists
                .txtStudentNo.Text = dgCollege.Rows(e.RowIndex).Cells(0).Value
                .txtEducationLevel.Text = "COLLEGE"
                .txtName.Text = dgCollege.Rows(e.RowIndex).Cells(1).Value
                .txtCourse.Text = dgCollege.Rows(e.RowIndex).Cells(2).Value
                .txtYear.Text = dgCollege.Rows(e.RowIndex).Cells(3).Value
                .txtSection.Text = dgCollege.Rows(e.RowIndex).Cells(4).Value
                .txtGuardian.Text = dgCollege.Rows(e.RowIndex).Cells(5).Value
                .txtRelation.Text = dgCollege.Rows(e.RowIndex).Cells(6).Value
                .txtMobile.Text = dgCollege.Rows(e.RowIndex).Cells(7).Value
            End With
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case TabControl1.SelectedIndex
            Case 0
                Load_College()
            Case 1
                Load_SeniorHigh()
            Case 2
                Load_JuniorHigh()
        End Select
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class