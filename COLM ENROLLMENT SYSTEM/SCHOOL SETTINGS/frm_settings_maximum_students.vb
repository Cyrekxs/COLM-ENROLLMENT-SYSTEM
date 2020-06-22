Public Class frm_settings_maximum_students
    Public Sub LoadStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_MAXIMUM_STUDENTS ORDER BY EDUCATION_LEVEL ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("EDUCATION_LEVEL"), reader("MAXIMUM"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            For i = 0 To DataGridView1.Rows.Count - 1
                Using comm As New SqlCommand("UPDATE TBL_SETTINGS_MAXIMUM_STUDENTS SET MAXIMUM = @maximum WHERE ID = @id", conn)
                    comm.Parameters.AddWithValue("@id", DataGridView1.Rows(i).Cells(0).Value)
                    comm.Parameters.AddWithValue("@maximum", DataGridView1.Rows(i).Cells(2).Value)
                    comm.ExecuteNonQuery()
                End Using
            Next
            MsgBox("Maximum Students has been successfully updated!", MsgBoxStyle.Information)
            LoadStudents()
        End Using
    End Sub

    Private Sub frm_settings_surcharge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudents()
    End Sub
End Class