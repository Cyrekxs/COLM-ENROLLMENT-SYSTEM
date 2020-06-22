Public Class frm_faculty_account
    Public FacultyAccountID As Integer = 0
    Public FacultyID As Integer = 0
    Public SavingStatus As New SavingOptions

    Private Sub LoadFacultyAccount()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_faculty_login WHERE FacultyAccountID = @FacultyAccountID", conn)
                comm.Parameters.AddWithValue("@FacultyAccountID", FacultyAccountID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        FacultyID = reader("Faculty_ID")
                        txtUsername.Text = reader("Faculty_Username")
                        txtPassword.Text = reader("Faculty_Password")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txtPassword.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = SavingOptions.NEW Then
                Using comm As New SqlCommand("INSERT INTO tbl_faculty_login VALUES (@FacultyID,@Username,@Password,@ay,@sem)", conn)
                    comm.Parameters.AddWithValue("@FacultyID", FacultyID)
                    comm.Parameters.AddWithValue("@Username", txtUsername.Text)
                    comm.Parameters.AddWithValue("@Password", txtPassword.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.ExecuteNonQuery()
                    MsgBox("Account has been successfully created!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End Using
            ElseIf SavingStatus = SavingOptions.EDIT Then
                Using comm As New SqlCommand("UPDATE tbl_faculty_login SET Faculty_Username = @Username, Faculty_Password = @Password WHERE FacultyAccountID = @FacultyAccountID", conn)
                    comm.Parameters.AddWithValue("@FacultyAccountID", FacultyAccountID)
                    comm.Parameters.AddWithValue("@Username", txtUsername.Text)
                    comm.Parameters.AddWithValue("@Password", txtPassword.Text)
                    comm.ExecuteNonQuery()
                    MsgBox("Account has been successfully created!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End Using
            End If
        End Using
    End Sub

    Private Sub frm_faculty_account_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If SavingStatus = SavingOptions.EDIT Then
            LoadFacultyAccount()
        End If
    End Sub
End Class