Public Class frm_program_settings

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case TabControl1.SelectedTab.Text
            Case "AY / SEM"
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("UPDATE TBL_PROGRAM_USERS SET ACADEMIC_YEAR = @ay,ACADEMIC_SEM = @sem WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", Account_ID)
                        comm.Parameters.AddWithValue("@ay", cmbAcademic_Year.Text)
                        comm.Parameters.AddWithValue("@sem", cmbAcademic_Sem.Text)
                        comm.ExecuteNonQuery()
                        With frm_main
                            .TS_Academic_Year.Text = cmbAcademic_Year.Text
                            .TS_Academic_Sem.Text = cmbAcademic_Sem.Text
                            Academic_Year = cmbAcademic_Year.Text
                            Academic_Sem = cmbAcademic_Sem.Text
                        End With
                        MsgBox("Successfully Setted!", MsgBoxStyle.Information)
                        Me.Close()
                    End Using
                End Using
            Case "ACCOUNT"

                If txtOldPass.Text = String.Empty Then
                    MsgBox("Old password is required for user verification!", MsgBoxStyle.Critical)
                    txtOldPass.Focus()
                    Exit Sub
                End If

                If txtNewPass.Text = String.Empty Then
                    MsgBox("Please enter your new desired password!", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM TBL_PROGRAM_USERS WHERE ID = @id AND USERPASS = @oldpassword", conn)
                        comm.Parameters.AddWithValue("@id", Account_ID)
                        comm.Parameters.AddWithValue("@oldpassword", txtOldPass.Text)
                        If Val(comm.ExecuteScalar) >= 1 Then
                            Using comm1 As New SqlCommand("UPDATE TBL_PROGRAM_USERS SET USERPASS = @newpassword WHERE ID = @id", conn)
                                comm1.Parameters.AddWithValue("@id", Account_ID)
                                comm1.Parameters.AddWithValue("@newpassword", txtNewPass.Text)
                                If MsgBox("Are you sure that you want to change your password?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    comm1.ExecuteNonQuery()
                                    MsgBox("Account Password has been successfully changed!", MsgBoxStyle.Information)
                                    Me.Close()
                                    Me.Dispose()
                                End If
                            End Using
                        Else
                            MsgBox("Invalid User!", MsgBoxStyle.Critical)
                            Exit Sub
                        End If
                    End Using
                End Using
        End Select
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txtOldPass.UseSystemPasswordChar = False
            txtNewPass.UseSystemPasswordChar = False
        Else
            txtOldPass.UseSystemPasswordChar = True
            txtNewPass.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub frm_program_settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbAcademic_Year.Text = Academic_Year
        cmbAcademic_Sem.Text = Academic_Sem
    End Sub
End Class