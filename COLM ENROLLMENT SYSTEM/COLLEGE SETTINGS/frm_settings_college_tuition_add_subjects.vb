Public Class frm_settings_college_tuition_add_subjects
    Public CurriculumType As String = String.Empty
    Public CourseCode As String = String.Empty

    Private Sub LoadCollegeCurriculumSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects WHERE Curriculum_Type = @CurriculumType AND Course_Code = @CourseCode", conn)
                comm.Parameters.AddWithValue("@CurriculumType", CurriculumType)
                comm.Parameters.AddWithValue("@CourseCode", CourseCode)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(0,
                                               reader("Curriculum_ID"),
                                               reader("Subj_Code"),
                                               reader("Subj_Desc"),
                                               reader("Lec_Hours"),
                                               reader("Lab_Hours"),
                                               reader("Subj_Unit"),
                                               Convert_To_Currency(reader("Subj_Price")),
                                               Convert_To_Currency(reader("Energy_Fee")),
                                               Convert_To_Currency(reader("Defence_Fee")),
                                               reader("IsBridgeSubject"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_settings_college_tuition_add_subjects_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub frm_settings_college_tuition_add_subjects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCollegeCurriculumSubjects()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 11 Then
            With frm_settings_college_tuition
                Dim IsExists As Boolean = False
                For i = 0 To .DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(e.RowIndex).Cells(1).Value = .DataGridView1.Rows(i).Cells(1).Value Then
                        IsExists = True
                        Exit For
                    End If
                Next
                If IsExists = False Then
                    .DataGridView1.Rows.Add(DataGridView1.Rows(e.RowIndex).Cells(0).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(1).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(2).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(3).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(4).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(5).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(6).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(7).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(8).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(9).Value,
                                             DataGridView1.Rows(e.RowIndex).Cells(10).Value)
                Else
                    MsgBox("Subject is already exists!", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End With
        End If

    End Sub
End Class