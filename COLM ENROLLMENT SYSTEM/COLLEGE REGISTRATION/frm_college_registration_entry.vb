Public Class frm_college_registration_entry
    Private CurriculumID As Integer = 0
    Private Enum OptionReturnStatus
        [Grade]
        [Academic_Yr]
        [Academic_Sem]
    End Enum
    Private Sub LoadCurriculumCodes()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT CurriculumCode FROM tbl_settings_college_curriculum ORDER BY CurriculumCode ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCurriculumCode.Items.Clear()
                    While reader.Read
                        cmbCurriculumCode.Items.Add(reader("CurriculumCode"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadCurriculumType()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT CurriculumType FROM tbl_settings_college_curriculum WHERE CurriculumCode = @CurriculumCode ORDER BY CurriculumType ASC", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", cmbCurriculumCode.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCurriculumType.Items.Clear()
                    While reader.Read
                        cmbCurriculumType.Items.Add(reader("CurriculumType"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Function GetStudentGradeAySem(SN As String, SubjCode As String, ReturnValue As OptionReturnStatus) As String
        Dim StudentGrade As String = String.Empty
        Dim acad_year As String = String.Empty
        Dim acad_sem As String = String.Empty
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_college_subject_loads WHERE Student_Number = @sn AND Subj_Code = @Subj_Code AND Grade_Equivalent <> 'N.A'", conn)
                comm.Parameters.AddWithValue("@sn", SN)
                comm.Parameters.AddWithValue("Subj_Code", SubjCode)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        StudentGrade = reader("Grade_Equivalent")
                        acad_year = reader("Academic_Yr")
                        acad_sem = reader("Academic_Sem")
                    End While
                End Using
            End Using
        End Using

        If ReturnValue = OptionReturnStatus.Grade Then
            Return StudentGrade
        ElseIf ReturnValue = OptionReturnStatus.Academic_Yr Then
            Return acad_year
        ElseIf ReturnValue = OptionReturnStatus.Academic_Sem Then
            Return acad_sem
        Else
            Return ""
        End If
    End Function
    Private Sub LoadCurriculumSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum WHERE CurriculumCode = @CurriculumCode AND CurriculumType = @CurriculumType", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", cmbCurriculumCode.Text)
                comm.Parameters.AddWithValue("@CurriculumType", cmbCurriculumType.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        CurriculumID = reader("CurriculumID")
                        txtCourseCode.Text = reader("CurriculumCourse")
                    End While
                End Using
            End Using

            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects WHERE Course_Code = @CourseCode AND Curriculum_Type = @CurriculumType AND YrLvl IN ('1ST YEAR','2ND YEAR','3RD YEAR','4TH YEAR') ORDER BY Yrlvl,Academic_Sem ASC", conn)
                comm.Parameters.AddWithValue("@CourseCode", txtCourseCode.Text)
                comm.Parameters.AddWithValue("@CurriculumType", cmbCurriculumType.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("Curriculum_ID"),
                                               reader("Subj_Code"),
                                               reader("Subj_Desc"),
                                               reader("Lec_Hours"),
                                               reader("Lab_Hours"),
                                               reader("Subj_Unit"),
                                               GetStudentGradeAySem(txtStudentNo.Text, reader("Subj_Code"), OptionReturnStatus.Grade),
                                               GetStudentGradeAySem(txtStudentNo.Text, reader("Subj_Code"), OptionReturnStatus.Academic_Yr),
                                               GetStudentGradeAySem(txtStudentNo.Text, reader("Subj_Code"), OptionReturnStatus.Academic_Sem))
                    End While
                End Using
            End Using
            txtSubjectCount.Text = DataGridView1.Rows.Count
        End Using
    End Sub

    Private Function IsRegistrationValid() As Boolean
        Dim Valid As Boolean = True
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_college_students_registered_curriculum WHERE StudentNo = @StudentNo AND RegistrationStatus = 'ACTIVE'", conn)
                comm.Parameters.AddWithValue("@StudentNo", txtStudentNo.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    If reader.HasRows = True Then
                        Valid = False
                    End If
                End Using
            End Using
        End Using
        Return Valid
    End Function
    Private Sub frm_college_registration_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCurriculumCodes()
    End Sub

    Private Sub cmbCurriculumCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurriculumCode.SelectedIndexChanged
        LoadCurriculumType()
    End Sub

    Private Sub cmbCurriculumCode_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbCurriculumCode.SelectionChangeCommitted
        LoadCurriculumType()
    End Sub

    Private Sub cmbCurriculumType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurriculumType.SelectedIndexChanged
        LoadCurriculumSubjects()
    End Sub

    Private Sub cmbCurriculumType_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbCurriculumType.SelectionChangeCommitted
        LoadCurriculumSubjects()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frm_college_registration_browse_students
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Dim RegistrationID As Integer = 0
                Try
                    If IsRegistrationValid() = True Then
                        Using comm As New SqlCommand("INSERT INTO tbl_college_students_registered_curriculum VALUES (@StudentNo,@RegisteredCurriculumID,'ACTIVE',GETDATE(),@User)", conn, t)
                            comm.Parameters.AddWithValue("@StudentNo", txtStudentNo.Text)
                            comm.Parameters.AddWithValue("@RegisteredCurriculumID", CurriculumID)
                            comm.Parameters.AddWithValue("@User", Account_Name)
                            comm.ExecuteNonQuery()
                        End Using

                        'DIRTY READ OF REGISTRATION ID
                        Using comm As New SqlCommand("SELECT MAX(RegistrationID) AS RegistrationID FROM tbl_college_students_registered_curriculum WITH (NOLOCK) WHERE StudentNo = @StudentNo", conn, t)
                            comm.Parameters.AddWithValue("@StudentNo", txtStudentNo.Text)
                            Using reader As SqlDataReader = comm.ExecuteReader
                                While reader.Read
                                    RegistrationID = reader("RegistrationID")
                                End While
                            End Using
                        End Using

                        For i = 0 To DataGridView1.Rows.Count - 1
                            Using comm As New SqlCommand("INSERT INTO tbl_college_students_registered_curriculum_subjects VALUES (@RegistrationID,@StudentNo,@CurriculumSubjID,'COLM',NULL,@student_grade,NULL,@ay,@sem)", conn, t)
                                comm.Parameters.AddWithValue("@StudentNo", txtStudentNo.Text)
                                comm.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                                comm.Parameters.AddWithValue("@CurriculumSubjID", DataGridView1.Rows(i).Cells(0).Value)
                                comm.Parameters.AddWithValue("@student_grade", DataGridView1.Rows(i).Cells(6).Value)
                                comm.Parameters.AddWithValue("@ay", DataGridView1.Rows(i).Cells(7).Value)
                                comm.Parameters.AddWithValue("@sem", DataGridView1.Rows(i).Cells(8).Value)
                                comm.ExecuteNonQuery()
                            End Using
                        Next

                        t.Commit()
                        MsgBox("Student has been successfully registered!", MsgBoxStyle.Information)
                        Me.Close()
                        Me.Dispose()
                    Else
                        MsgBox("This student cannot be registered because it is already registered!", MsgBoxStyle.Critical)
                        Me.Close()
                        Me.Dispose()
                    End If
                Catch ex As Exception
                    MsgBox("System error occured. The transaction will be denied by the system please try again later" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
                    t.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With frm_college_registration_bridge_subjects
            .CourseCode = txtCourseCode.Text
            .CurriculumType = cmbCurriculumType.Text
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            txtSubjectCount.Text = DataGridView1.Rows.Count
        End With
    End Sub
End Class