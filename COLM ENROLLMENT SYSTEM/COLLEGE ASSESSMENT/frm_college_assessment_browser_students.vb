Public Class frm_college_assessment_browser_students
    Private Sub LoadRegisteredCollegeStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_RegisteredLists() AS REGISTERED WHERE StudentName LIKE @search AND NOT EXISTS (SELECT * FROM FN_AssessedStudents() WHERE Education_Level = 'COLLEGE' AND Academic_Yr = @ay AND Academic_Sem = @sem AND Student_Number = REGISTERED.StudentNo) ORDER BY StudentName ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@search", TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("RegistrationID"),
                                               reader("StudentNo"),
                                               reader("StudentName"),
                                               LSet(reader("Gender"), 1),
                                               GetAge(CDate(reader("BirthDate"))),
                                               reader("CurriculumCode"),
                                               reader("CurriculumType"),
                                               reader("CurriculumCourse"),
                                               reader("RegisteredCurriculumID"))
                    End While
                End Using
            End Using
        End Using
        '
    End Sub

    Private Sub frm_college_assessment_browser_students_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRegisteredCollegeStudents()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 9 Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()

                Dim ListofCurriculumID As New List(Of Integer)

                Using comm As New SqlCommand("SELECT * FROM dbo.tbl_settings_college_curriculum_subjects WHERE Course_Code = @Course_Code AND Curriculum_Type = @Curriculum_Type AND IsBridgeSubject = 'NO' AND Yrlvl IN ('1ST YEAR','2ND YEAR','3RD YEAR','4TH YEAR') AND NOT EXISTS (SELECT * FROM dbo.tbl_college_students_registered_curriculum_subjects WHERE RegistrationID = @RegistrationID AND CurriculumSubjID = tbl_settings_college_curriculum_subjects.Curriculum_ID) ORDER BY Curriculum_ID ASC", conn)
                    comm.Parameters.AddWithValue("@RegistrationID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                    comm.Parameters.AddWithValue("@Course_Code", DataGridView1.Rows(e.RowIndex).Cells(7).Value)
                    comm.Parameters.AddWithValue("@Curriculum_Type", DataGridView1.Rows(e.RowIndex).Cells(6).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            ListofCurriculumID.Add(reader("Curriculum_ID"))
                        End While
                    End Using
                End Using

                Using t As SqlTransaction = conn.BeginTransaction
                    Try
                        For i = 0 To ListofCurriculumID.Count - 1
                            Using comm As New SqlCommand("INSERT INTO tbl_college_students_registered_curriculum_subjects VALUES (@RegistrationID,@StudentNo,@CurriculumSubjID,'COLM',NULL,'',NULL,'','')", conn, t)
                                comm.Parameters.AddWithValue("@RegistrationID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                                comm.Parameters.AddWithValue("@StudentNo", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
                                comm.Parameters.AddWithValue("@CurriculumSubjID", ListofCurriculumID(i))
                                comm.ExecuteNonQuery()
                            End Using
                        Next
                        t.Commit()

                        With frm_college_assessment_entry
                            .RegistrationID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                            .txtStudentNo.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                            .txtStudentName.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                            .txtGender.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                            .txtAge.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                            .txtCurriculumCode.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                            .txtCurriculumType.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
                            .txtCourseCode.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value
                            .CurriculumID = DataGridView1.Rows(e.RowIndex).Cells(8).Value
                        End With
                        Me.Close()
                        Me.Dispose()
                    Catch ex As Exception
                        t.Rollback()
                        MsgBox("An error has been occured while processing inforamtion" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
                    End Try
                End Using
            End Using
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadRegisteredCollegeStudents()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadRegisteredCollegeStudents()
    End Sub
End Class