Public Class frm_h_student_registration

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                If Val(comm.ExecuteScalar) = 0 Then
                    Using comm1 As New SqlCommand("INSERT INTO TBL_STUDENT_REGISTERED VALUES(@education_level,@sn,@course_code,@yrlvl,@sect_code,@enrollment_status,@ay,@sem,@regby,GETDATE())", conn)
                        With comm1.Parameters
                            .AddWithValue("@education_level", "HIGH SCHOOL")
                            .AddWithValue("@sn", txtStudentNo.Text)
                            .AddWithValue("@course_code", "HIGH SCHOOL")
                            .AddWithValue("@yrlvl", cmbYear.Text)
                            .AddWithValue("@sect_code", cmbSection.Text)
                            .AddWithValue("@enrollment_status", "REGISTERED")
                            .AddWithValue("@ay", Academic_Year)
                            .AddWithValue("@sem", Academic_Sem)
                            .AddWithValue("@regby", Account_Name)
                        End With
                        If MsgBox("Are you sure you want to register this student in this Academic Year and Semester?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            comm1.ExecuteNonQuery()
                            MsgBox("Student has been succesfully registered!", MsgBoxStyle.Information)
                        End If
                    End Using
                Else
                    Using comm1 As New SqlCommand("UPDATE TBL_STUDENT_REGISTERED SET EDUCATION_LEVEL = @education_level, STUDENT_NUMBER = @sn, COURSECODE = @course_code, YRLVL = @yrlvl, SECTIONCODE = @sect_code,ENROLLMENT_STATUS =  @enrollment_status, ACADEMIC_YEAR = @ay, ACADEMIC_SEM = @sem, REGISTEREDBY = @regby, REGISTEREDDATE = GETDATE()", conn)
                        With comm1.Parameters
                            .AddWithValue("@education_level", "HIGH SCHOOL")
                            .AddWithValue("@sn", txtStudentNo.Text)
                            .AddWithValue("@course_code", "HIGH SCHOOL")
                            .AddWithValue("@yrlvl", cmbYear.Text)
                            .AddWithValue("@sect_code", cmbSection.Text)
                            .AddWithValue("@enrollment_status", "REGISTERED")
                            .AddWithValue("@ay", Academic_Year)
                            .AddWithValue("@sem", Academic_Sem)
                            .AddWithValue("@regby", Account_Name)
                        End With
                        If MsgBox("Are you sure you want to register this student in this Academic Year and Semester?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            comm1.ExecuteNonQuery()
                            MsgBox("Student has been succesfully registered!", MsgBoxStyle.Information)
                        End If
                    End Using
                End If
            End Using
        End Using
    End Sub
End Class