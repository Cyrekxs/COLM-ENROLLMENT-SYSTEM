Public Class frm_settings_tuition_miscellaneous_lists
    Private Sub LoadTUitionFee_JuniorHigh()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_tuition_fee_lists_non_college() WHERE Education_Level = 'JUNIOR HIGH' AND Academic_Year = @ay", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"),
                                               reader("Yrlvl"),
                                               Convert_To_Currency(reader("Amount")),
                                               Convert_To_Currency(reader("Miscellaneous_Fee")),
                                               Convert_To_Currency(CDbl(reader("Amount")) + CDbl(reader("Miscellaneous_Fee"))))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadTUitionFee_SeniorHigh()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_tuition_fee_lists_non_college() WHERE Education_Level = 'SENIOR HIGH' AND Academic_Year = @ay", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"),
                                               reader("Course_Code"),
                                               reader("Yrlvl"),
                                               Convert_To_Currency(reader("Amount")),
                                               Convert_To_Currency(reader("Miscellaneous_Fee")),
                                               Convert_To_Currency(CDbl(reader("Amount")) + CDbl(reader("Miscellaneous_Fee"))))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_settings_tuition_miscellaneous_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTUitionFee_JuniorHigh()
        LoadTUitionFee_SeniorHigh()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 5 Then
            With frm_settings_tuition_miscellaneous_entry
                .FormCaller = frm_settings_tuition_miscellaneous_entry.FormCallerOptions.JHS
                .TuitionFeeID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                .Yrlvl = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                .SavingStatus = SavingOptions.EDIT
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frm_settings_tuition_miscellaneous_entry
            .FormCaller = frm_settings_tuition_miscellaneous_entry.FormCallerOptions.JHS
            .TuitionFeeID = 0
            .SavingStatus = SavingOptions.NEW
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            LoadTUitionFee_JuniorHigh()
        End With
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.ColumnIndex = 6 Then
            With frm_settings_tuition_miscellaneous_entry
                .FormCaller = frm_settings_tuition_miscellaneous_entry.FormCallerOptions.SHS
                .TuitionFeeID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
                .Strand = DataGridView2.Rows(e.RowIndex).Cells(1).Value
                .Yrlvl = DataGridView2.Rows(e.RowIndex).Cells(2).Value
                .SavingStatus = SavingOptions.EDIT
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_settings_tuition_miscellaneous_entry
            .FormCaller = frm_settings_tuition_miscellaneous_entry.FormCallerOptions.SHS
            .SavingStatus = SavingOptions.NEW
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            LoadTUitionFee_SeniorHigh()
        End With
    End Sub
End Class