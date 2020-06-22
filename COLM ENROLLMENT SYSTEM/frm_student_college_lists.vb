Public Class frm_student_college_lists
    Public EducationLevel As String = String.Empty
    Public DGRow As Integer = 0
    Public Sub LoadStudents()
        Dim StudentCount As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim SQLQuery As String = String.Empty
            Select Case cmbFilter.SelectedIndex
                Case 0
                    SQLQuery = "SELECT * FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = @search AND EDUCATION_LEVEL = @educationevel ORDER BY LASTNAME,FIRSTNAME ASC"
                Case 1
                    SQLQuery = "SELECT * FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + ', ' + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = @educationevel ORDER BY LASTNAME,FIRSTNAME ASC"
            End Select

            Using comm As New SqlCommand(SQLQuery, conn)

                If cmbFilter.SelectedIndex = 0 Then
                    comm.Parameters.AddWithValue("@search", txtSearch.Text)
                ElseIf cmbFilter.SelectedIndex = 1 Then
                    comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                End If

                comm.Parameters.AddWithValue("@educationlevel", EducationLevel)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        Dim StudentNumber As String = String.Empty
                        Dim StudentName As String = String.Empty
                        Dim ContactNo As String = String.Empty
                        Dim Gender As String = String.Empty
                        Dim Birthday As String = String.Empty

                        StudentNumber = reader("Student_Number")

                        If reader("Extension_Name") = "N.A" Then
                            If reader("Middlename") = "N.A" Then
                                StudentName = reader("Lastname") & ", " & reader("Firstname")
                            Else
                                StudentName = reader("Lastname") & ", " & reader("Firstname") & " " & reader("Middlename")
                            End If
                        Else
                            If reader("Middlename") = "N.A" Then
                                StudentName = reader("Lastname") & " " & reader("Extension_Name") & ", " & reader("Firstname")
                            Else
                                StudentName = reader("Lastname") & " " & reader("Extension_Name") & ", " & reader("Firstname") & " " & reader("Middlename")
                            End If
                        End If


                        ContactNo = reader("Mobile")
                        Gender = LSet(reader("Gender"), 1)
                        Birthday = reader("B_Month") & " " & reader("B_Day") & ", " & reader("B_Year")

                        StudentCount += 1
                        DataGridView1.Rows.Add(StudentNumber, StudentName, ContactNo, Gender, Birthday, GetAge(Birthday))
                    End While
                    txtStudentCount.Text = StudentCount
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_student_college_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbFilter.SelectedIndex = 1
        LoadStudents()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadStudents()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub
End Class