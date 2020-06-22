Public Class frm_college_registration_bridge_subjects
    Public CourseCode As String = String.Empty
    Public CurriculumType As String = String.Empty
    Private Sub LoadBridgingSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects WHERE Course_Code = @CourseCode AND Curriculum_Type = @CurriculumType AND YrLvl IN ('1ST YEAR','2ND YEAR','3RD YEAR','4TH YEAR') AND IsBridgeSubject = 'YES' ORDER BY Yrlvl,Academic_Sem ASC", conn)
                comm.Parameters.AddWithValue("@CourseCode", CourseCode)
                comm.Parameters.AddWithValue("@CurriculumType", CurriculumType)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("Curriculum_ID"),
                                               reader("Subj_Code"),
                                               reader("Subj_Desc"),
                                               reader("Lec_Hours"),
                                               reader("Lab_Hours"),
                                               reader("Subj_Unit"))
                    End While
                End Using
            End Using
        End Using
        TextBox1.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("Are you sure you want to add all this bridge subjects?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            With frm_college_registration_entry
                For i = 0 To DataGridView1.Rows.Count - 1
                    .DataGridView1.Rows.Add(DataGridView1.Rows(i).Cells(0).Value,
                                            DataGridView1.Rows(i).Cells(1).Value,
                                            DataGridView1.Rows(i).Cells(2).Value,
                                            DataGridView1.Rows(i).Cells(3).Value,
                                            DataGridView1.Rows(i).Cells(4).Value,
                                            DataGridView1.Rows(i).Cells(5).Value, "", "", ""
                                            )
                Next
            End With
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub frm_college_registration_bridge_subjects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBridgingSubjects()
    End Sub
End Class