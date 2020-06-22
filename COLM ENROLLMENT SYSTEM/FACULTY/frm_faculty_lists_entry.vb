Public Class frm_faculty_lists_entry
    Public SavingStatus As New SavingOptions
    Public FacultyID As Integer = 0

    Private Sub LoadDepartments()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_college_departments ORDER BY Department ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbDepartment.Items.Clear()
                    While reader.Read
                        cmbDepartment.Items.Add(reader("Department"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadFacultyInformation()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_faculty_list WHERE FacultyID = @FacultyID", conn)
                comm.Parameters.AddWithValue("@FacultyID", FacultyID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtLastname.Text = reader("Lastname")
                        txtFirstname.Text = reader("Firstname")
                        txtMiddlename.Text = reader("Middlename")
                        cmbDepartment.Text = reader("Department")
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_faculty_lists_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDepartments()
        If SavingStatus = SavingOptions.EDIT Then
            LoadFacultyInformation()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = SavingOptions.NEW Then
                Using comm As New SqlCommand("INSERT INTO tbl_faculty_list VALUES (@lastname,@firstname,@middlename,@department,'FALSE')", conn)
                    comm.Parameters.AddWithValue("@lastname", txtLastname.Text)
                    comm.Parameters.AddWithValue("@firstname", txtFirstname.Text)
                    comm.Parameters.AddWithValue("@middlename", txtMiddlename.Text)
                    comm.Parameters.AddWithValue("@department", cmbDepartment.Text)
                    comm.ExecuteNonQuery()
                End Using
            ElseIf SavingStatus = SavingOptions.EDIT Then
                Using comm As New SqlCommand("UPDATE tbl_faculty_list SET Lastname = @lastname, Firstname = @firstname, Middlename = @middlename, Department = @department WHERE FacultyID = @FacultyID", conn)
                    comm.Parameters.AddWithValue("@FacultyID", FacultyID)
                    comm.Parameters.AddWithValue("@lastname", txtLastname.Text)
                    comm.Parameters.AddWithValue("@firstname", txtFirstname.Text)
                    comm.Parameters.AddWithValue("@middlename", txtMiddlename.Text)
                    comm.Parameters.AddWithValue("@department", cmbDepartment.Text)
                    comm.ExecuteNonQuery()
                End Using
            End If

            MsgBox("Faculty has been successfully saved!", MsgBoxStyle.Information)
            Me.Close()
            Me.Dispose()
        End Using
    End Sub
End Class