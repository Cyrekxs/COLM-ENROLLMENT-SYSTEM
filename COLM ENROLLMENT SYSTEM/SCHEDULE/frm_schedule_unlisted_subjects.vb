Public Class frm_schedule_unlisted_subjects
    Public Course_Code As String = String.Empty
    Private Sub LoadUnsettedSubjectsSchedules()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_Settings_College_Schedule_Unsetted(@ay,@sem) WHERE Course_Code = @Course_Code ORDER BY Subj_Code ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@Course_Code", Course_Code)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("Subj_Code"), reader("Subj_Desc"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_schedule_unlisted_subjects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUnsettedSubjectsSchedules()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 3 Then
            With frm_schedule_entry.DataGridView1
                .Rows.Add(0,
                          DataGridView1.Rows(e.RowIndex).Cells(0).Value,
                          DataGridView1.Rows(e.RowIndex).Cells(1).Value,
                          DataGridView1.Rows(e.RowIndex).Cells(2).Value,
                          "-", "-", "-", "-", "-")
            End With
        End If
    End Sub
End Class