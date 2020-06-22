Public Class frm_college_assessment_browser_subject_new_curriculum
    Public Course_Code As String = String.Empty
    Public Student_Number As String = String.Empty
    Public RegistrationID As Integer = 0
    Private Sub LoadNewCurriculumSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT MAIN.Curriculum_ID,MAIN.Subj_Code,MAIN.Subj_Desc,MAIN.Subj_Unit,SUB.Subj_Price,SUB.Energy_Fee,SUB.Defence_Fee FROM dbo.tbl_settings_college_curriculum_subjects AS MAIN INNER JOIN tbl_settings_college_curriculum_subjects_setted AS SUB ON MAIN.Curriculum_ID = SUB.Subj_ID WHERE MAIN.Curriculum_Type = 'VERSION 2' AND MAIN.IsBridgeSubject = 'NO' AND MAIN.Course_Code = @Course_Code AND SUB.Academic_Year = @ay AND SUB.Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@Course_Code", Course_Code)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("Curriculum_ID"),
                                               reader("Subj_Code"),
                                               reader("Subj_Desc"),
                                               reader("Subj_Unit"),
                                              Convert_To_Currency(reader("Subj_Price")),
                                              Convert_To_Currency(reader("Energy_Fee")),
                                              Convert_To_Currency(reader("Defence_Fee")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_college_assessment_browser_subject_new_curriculum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadNewCurriculumSubjects()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 7 Then
            If MsgBox("Are you sure you want to add " & DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString.ToUpper & " into the list?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim IsSubjectRegistered As Boolean = False
                Dim RegisteredSubjID As Integer = 0

                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM dbo.tbl_college_students_registered_curriculum_subjects WHERE StudentNo = @sn AND CurriculumSubjID = @Curriculum_ID", conn)
                        comm.Parameters.AddWithValue("@sn", Student_Number)
                        comm.Parameters.AddWithValue("@Curriculum_ID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            If reader.HasRows = True Then
                                IsSubjectRegistered = True
                                While reader.Read()
                                    RegisteredSubjID = reader("RegisteredSubjID")
                                End While
                            Else
                                IsSubjectRegistered = False
                            End If
                        End Using
                    End Using

                    If IsSubjectRegistered = False Then

                        Using comm As New SqlCommand("INSERT INTO dbo.tbl_college_students_registered_curriculum_subjects VALUES (@RegistrationID,@StudentNo,@CurriculumSubjID,'COLM',NULL,'',NULL,'','')", conn)
                            comm.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                            comm.Parameters.AddWithValue("@StudentNo", Student_Number)
                            comm.Parameters.AddWithValue("@CurriculumSubjID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                            comm.ExecuteNonQuery()
                        End Using

                        Using comm As New SqlCommand("SELECT * FROM tbl_college_students_registered_curriculum_subjects WHERE RegistrationID = @RegistrationID AND CurriculumSubjID = @CurriculumSubjID", conn)
                            comm.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                            comm.Parameters.AddWithValue("@CurriculumSubjID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                            Using reader As SqlDataReader = comm.ExecuteReader
                                While reader.Read
                                    RegisteredSubjID = reader("RegisteredSubjID")
                                End While
                            End Using
                        End Using

                    End If

                    With frm_college_assessment_entry
                        With .DGTuition
                            .Rows.Add(RegisteredSubjID,
                                          DataGridView1.Rows(e.RowIndex).Cells(1).Value,
                                          DataGridView1.Rows(e.RowIndex).Cells(2).Value,
                                          DataGridView1.Rows(e.RowIndex).Cells(3).Value,
                                          DataGridView1.Rows(e.RowIndex).Cells(4).Value,
                                          DataGridView1.Rows(e.RowIndex).Cells(5).Value,
                                          DataGridView1.Rows(e.RowIndex).Cells(6).Value, "NO")
                        End With

                        With .DGSched
                            .Rows.Add(RegisteredSubjID,
                                 DataGridView1.Rows(e.RowIndex).Cells(1).Value,
                                 DataGridView1.Rows(e.RowIndex).Cells(2).Value)
                        End With
                    End With


                    MsgBox("Subject " & DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString.ToUpper & " has been successfully added into the list!", MsgBoxStyle.Information)
                    DataGridView1.Rows.Remove(DataGridView1.Rows(e.RowIndex))
                End Using
            End If
        End If
    End Sub
End Class