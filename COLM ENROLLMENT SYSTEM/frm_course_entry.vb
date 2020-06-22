Public Class frm_course_entry
    Public Saving_Status = String.Empty
    Public ID As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If txtCourseCode.Text = String.Empty Then
            MsgBox("Please enter a course code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtCourseDescription.Text = String.Empty Then
            MsgBox("Please enter a course descripton!")
            Exit Sub
        End If

        If txtDepartment.Text = String.Empty Then
            MsgBox("Please enter a course department!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If Saving_Status = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_CODE = @course_code", conn)
                    comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_COLLEGE_COURSES VALUES (@course_code,@course_desc,'AVAILABLE',@department)", conn)
                            comm1.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm1.Parameters.AddWithValue("@course_desc", txtCourseDescription.Text)
                            comm1.Parameters.AddWithValue("@department", txtDepartment.Text)
                            comm1.ExecuteNonQuery()
                            frm_course_yrlvl.Load_Courses()
                            Me.Close()
                            Me.Dispose()
                        End Using
                    Else
                        MsgBox("Course code: " & txtCourseCode.Text & " is already exist!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf Saving_Status = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_CODE = @course_code AND COURSE_ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", ID)
                    comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_COLLEGE_COURSES SET COURSE_CODE = @course_code, COURSE_DESC = @course_desc,DEPARTMENT = @department WHERE COURSE_ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", ID)
                            comm1.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm1.Parameters.AddWithValue("@course_desc", txtCourseDescription.Text)
                            comm1.Parameters.AddWithValue("@department", txtDepartment.Text)
                            comm1.ExecuteNonQuery()
                            frm_course_yrlvl.Load_Courses()
                            Me.Close()
                            Me.Dispose()
                        End Using
                    Else
                        MsgBox("Course code: " & txtCourseCode.Text & " is already exist!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub
End Class