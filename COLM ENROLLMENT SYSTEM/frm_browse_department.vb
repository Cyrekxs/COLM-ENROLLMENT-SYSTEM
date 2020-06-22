Public Class frm_browse_department

    Private Sub frm_browse_department__Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_DEPARTMENTS ORDER BY DEPARTMENT ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    ListView1.Items.Clear()
                    While reader.Read
                        ListView1.Items.Add(reader("DEPARTMENT"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            With frm_course_information
                .txtDepartment.Text = ListView1.FocusedItem.Text
                Me.Close()
                Me.Dispose()
            End With
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class