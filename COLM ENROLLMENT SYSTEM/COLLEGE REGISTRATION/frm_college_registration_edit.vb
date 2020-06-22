Public Class frm_college_registration_edit
    Public RegistrationID As Integer = 0
    Public Course_Code As String = String.Empty
    Public Curriculum_Type As String = String.Empty

    Public Sub LoadRegistrationInformation()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM dbo.FN_College_StudentsSubjects() WHERE RegistrationID = @RegistrationID", conn)
                comm.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                DataGridView1.Rows.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView1.Rows.Add(reader("RegisteredSubjID"), reader("SubjCode"), reader("SubjDesc"), reader("LecHours"), reader("LabHours"), reader("SubjUnit"), "MODIFY")
                    End While
                End Using
            End Using
        End Using
        txtSubjectCount.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub frm_college_registration_edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRegistrationInformation()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    For i = 0 To DataGridView1.Rows.Count - 1
                        If DataGridView1.Rows(i).Cells(6).Value = "INSERT" Then
                            Using comm As New SqlCommand("INSERT INTO tbl_college_students_registered_curriculum_subjects VALUES (@RegistrationID,@StudentNo,@CurriculumSubjID,'COLM',NULL,'',NULL,'','')", conn, t)
                                comm.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                                comm.Parameters.AddWithValue("@StudentNo", txtStudentNo.Text)
                                comm.Parameters.AddWithValue("@CurriculumSubjID", DataGridView1.Rows(i).Cells(0).Value)
                                comm.ExecuteNonQuery()
                            End Using
                        End If
                    Next
                    t.Commit()
                    MsgBox("Information has been successfully saved!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                Catch ex As Exception
                    t.Rollback()
                    MsgBox("An error has been occured while processing inforamtion" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
                End Try
            End Using

        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        LoadRegistrationInformation()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM dbo.tbl_settings_college_curriculum_subjects WHERE Course_Code = @Course_Code AND Curriculum_Type = @CurriculumType AND Yrlvl IN ('1ST YEAR','2ND YEAR','3RD YEAR','4TH YEAR') AND NOT EXISTS (SELECT * FROM dbo.tbl_college_students_registered_curriculum_subjects WHERE RegistrationID = @RegistrationID AND CurriculumSubjID = tbl_settings_college_curriculum_subjects.Curriculum_ID) ORDER BY Curriculum_ID ASC", conn)
                comm.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                comm.Parameters.AddWithValue("@Course_Code", Course_Code)
                comm.Parameters.AddWithValue("@CurriculumType", Curriculum_Type)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView1.Rows.Add(reader("Curriculum_ID"), reader("Subj_Code"), reader("Subj_Desc"), reader("Lec_Hours"), reader("Lab_Hours"), reader("Subj_Unit"), "INSERT")
                    End While
                End Using
            End Using
        End Using
        txtSubjectCount.Text = DataGridView1.Rows.Count
    End Sub
End Class