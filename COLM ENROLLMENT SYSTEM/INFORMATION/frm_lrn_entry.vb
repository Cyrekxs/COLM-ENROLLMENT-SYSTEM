Public Class frm_lrn_entry

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    Using comm As New SqlCommand("INSERT INTO tbl_student_information_lrn VALUES (@lrn,@lname,@fname,@mname,@bdate)", conn, t)
                        comm.Parameters.AddWithValue("@lrn", txtLRN.Text)
                        comm.Parameters.AddWithValue("@lname", txtLastname.Text)
                        comm.Parameters.AddWithValue("@fname", txtFirstname.Text)
                        comm.Parameters.AddWithValue("@mname", txtMiddlename.Text)
                        comm.Parameters.AddWithValue("@bdate", txtBirthdate.Text)
                        comm.ExecuteNonQuery()
                    End Using
                    t.Commit()
                    MsgBox("LRN has been successfully saved!", MsgBoxStyle.Information)
                Catch ex As Exception
                    t.Rollback()
                    MsgBox("An error occured while processing information" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
                End Try
            End Using
        End Using
    End Sub
End Class