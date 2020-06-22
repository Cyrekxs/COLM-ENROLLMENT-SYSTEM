Public Class frm_college_registration_lists

    Private Sub LoadRegisteredCollegeStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_RegisteredLists() WHERE StudentName LIKE @search", conn)
                comm.Parameters.AddWithValue("@search", TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("RegistrationID"), reader("StudentNo"), reader("StudentName"), reader("CurriculumCode"), reader("CurriculumType"))
                    End While
                End Using
            End Using
        End Using
        txtStudentCount.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_college_registration_entry
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadRegisteredCollegeStudents()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadRegisteredCollegeStudents()
    End Sub

    Private Sub frm_college_registration_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRegisteredCollegeStudents()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = clmUpdate.Index Then
            With frm_college_registration_edit
                .RegistrationID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                .txtStudentNo.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                .txtStudentName.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                .txtCurriculumCode.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                .txtCurriculumType.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value

                'KKUHANIN LANG UNG CURRICULUM TYPE SKA UNG CURRICULUM COURSE NA NKAREGISTERED SA BATA NA TO
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM dbo.tbl_settings_college_curriculum WHERE CurriculumID = (SELECT RegisteredCurriculumID FROM dbo.tbl_college_students_registered_curriculum WHERE RegistrationID = @RegistrationID)", conn)
                        comm.Parameters.AddWithValue("@RegistrationID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            While reader.Read
                                .Curriculum_Type = reader("CurriculumType")
                                .Course_Code = reader("CurriculumCourse")
                                .txtCourseCode.Text = reader("CurriculumCourse")
                            End While
                        End Using
                    End Using
                End Using

                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        ElseIf e.ColumnIndex = clmDelete.Index Then

            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Dim IsStudentEnrolled As Boolean = False
                Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsCollege(@ay,@sem) WHERE Student_Number = @sn", conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@sn", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        If reader.HasRows = True Then
                            IsStudentEnrolled = True
                        End If
                    End Using
                End Using

                If MsgBox("Are you sure you want to delete this record?" & vbNewLine & "Delete this record will delete the following: Registration Information and Assessment Information in the active Academic Year and Semester, Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Using t As SqlTransaction = conn.BeginTransaction
                        Try
                            Using comm As New SqlCommand("UPDATE tbl_college_students_registered_curriculum SET RegistrationStatus = 'INACTIVE' WHERE RegistrationID = @registrationid", conn, t)
                                comm.Parameters.AddWithValue("@registrationid", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                                comm.ExecuteNonQuery()
                            End Using

                            Using comm As New SqlCommand("DELETE FROM dbo.tbl_college_students_registered_curriculum_subjects WHERE RegistrationID = @registrationid", conn, t)
                                comm.Parameters.AddWithValue("@registrationid", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                                comm.ExecuteNonQuery()
                            End Using

                            Using comm As New SqlCommand("DELETE FROM tbl_college_assessment_summary WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn, t)
                                comm.Parameters.AddWithValue("@sn", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.ExecuteNonQuery()
                            End Using

                            Using comm As New SqlCommand("DELETE FROM tbl_college_assessment_breakdown WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn, t)
                                comm.Parameters.AddWithValue("@sn", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.ExecuteNonQuery()
                            End Using

                            Using comm As New SqlCommand("DELETE FROM tbl_college_subject_loads WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn, t)
                                comm.Parameters.AddWithValue("@sn", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.ExecuteNonQuery()
                            End Using

                            Using comm As New SqlCommand("DELETE FROM tbl_college_fee_loads WHERE Student_Number = @sn AND Fee_Type IN('MFEE','OFEE') AND Academic_Yr = @ay AND Academic_Sem = @sem", conn, t)
                                comm.Parameters.AddWithValue("@sn", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.ExecuteNonQuery()
                            End Using

                            Using comm As New SqlCommand("INSERT INTO tbl_program_logs VALUES (@LogType,@LogDesc,GETDATE(),@user)", conn, t)
                                comm.Parameters.AddWithValue("@LogType", "DELETE")
                                comm.Parameters.AddWithValue("@LogDesc", "DELETE Registration student no: " & DataGridView1.Rows(e.RowIndex).Cells(1).Value & " named: " & DataGridView1.Rows(e.RowIndex).Cells(2).Value)
                                comm.Parameters.AddWithValue("@user", Account_Name)
                                comm.ExecuteNonQuery()
                            End Using

                            t.Commit()
                            MsgBox("Registration and assessment has been successfully deleted!", MsgBoxStyle.Information)
                        Catch ex As Exception
                            t.Rollback()
                            MsgBox("An error occured while processing information please try again!" & ex.Message, MsgBoxStyle.Critical)
                        End Try
                    End Using
                End If
            End Using

        ElseIf e.ColumnIndex = clmDeactivate.Index Then
            If MsgBox("Are you sure you want to deactivate his/her registration?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Using conn = New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("UPDATE tbl_college_students_registered_curriculum SET RegistrationStatus = 'INACTIVE' WHERE RegistrationID = @registrationid", conn)
                        comm.Parameters.AddWithValue("@registrationid", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                        comm.ExecuteNonQuery()
                        MsgBox("Account has been successfully deactivated!", MsgBoxStyle.Information)
                        LoadRegisteredCollegeStudents()
                    End Using
                End Using
            End If
        End If
    End Sub
End Class