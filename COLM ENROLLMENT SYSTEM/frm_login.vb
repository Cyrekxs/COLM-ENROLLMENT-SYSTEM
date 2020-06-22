Public Class frm_login

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_program_users WHERE Username = @Username AND Userpass = @Password", conn)
                comm.Parameters.AddWithValue("@Username", txtUsername.Text)
                comm.Parameters.AddWithValue("@Password", txtPassword.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    If reader.HasRows = True Then
                        While reader.Read
                            Account_ID = reader("ID")
                            Account_Name = reader("Name")
                            Account_Position = reader("Position_Name")
                            Academic_Year = reader("Academic_Year")
                            Academic_Sem = reader("Academic_Sem")
                            Account_Date_Time = Date.Now
                        End While
                        With frm_main
                            .StartPosition = FormStartPosition.CenterScreen
                            .Show()
                        End With
                        Me.Hide()
                    Else
                        MsgBox("Login Failed!", MsgBoxStyle.Critical)
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frm_login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblVersion.Text = "VERSION: " & My.Application.Deployment.CurrentVersion.ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub
End Class