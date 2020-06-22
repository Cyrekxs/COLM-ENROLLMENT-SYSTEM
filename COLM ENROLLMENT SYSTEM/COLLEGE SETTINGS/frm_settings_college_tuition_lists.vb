Public Class frm_settings_college_tuition_lists
    Private Sub Load_College_Fee_Summary()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_Fee_Summary(@ay,@sem) ORDER BY CurriculumCode,Course_Code,Yrlvl ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("CurriculumID"),
                                               reader("CurriculumCode"),
                                               reader("CurriculumType"),
                                               reader("Course_Code"),
                                               reader("Yrlvl"),
                                              Convert_To_Currency(reader("Tuition_Fee")),
                                              Convert_To_Currency(reader("Bridge_Fee")),
                                              Convert_To_Currency(reader("Energy_Fee")),
                                              Convert_To_Currency(reader("Defence_Fee")),
                                              Convert_To_Currency(reader("Miscellaneous")),
                                              Convert_To_Currency(CDbl(reader("Tuition_Fee")) + CDbl(reader("Bridge_Fee")) + CDbl(reader("Energy_Fee")) + CDbl(reader("Defence_Fee")) + CDbl(reader("Miscellaneous"))))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_settings_college_tuition_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_College_Fee_Summary()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 11 Then
            With frm_settings_college_tuition
                .CurriculumID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                .CurriculumCode = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                .CurriculumType = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                .YearLevel = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                .StartPosition = FormStartPosition.CenterParent
                .SavingStatus = SavingOptions.EDIT
                .ShowDialog()
                Load_College_Fee_Summary()
            End With
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frm_settings_college_tuition
            .SavingStatus = SavingOptions.NEW
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            Load_College_Fee_Summary()
        End With
    End Sub
End Class