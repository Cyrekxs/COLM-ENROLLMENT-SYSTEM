Public Class frm_college_settings_course_entry
    Public ID As Integer = 0
    Public Saving_Status = String.Empty
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If cmbProgram.Text = String.Empty Then
            MsgBox("Please select course program!", MsgBoxStyle.Critical)
            Exit Sub
        End If

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
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_COLLEGE_COURSES VALUES(@course_code,@course_desc,'0',@department,@program,GETDATE(),@ActiveUser)", conn)
                            comm1.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm1.Parameters.AddWithValue("@course_desc", txtCourseDescription.Text)
                            comm1.Parameters.AddWithValue("@department", txtDepartment.Text)
                            comm1.Parameters.AddWithValue("@program", cmbProgram.Text)
                            comm1.Parameters.AddWithValue("@ActiveUser", Account_Name)
                            comm1.ExecuteNonQuery()
                            MsgBox("New Course has been successfully saved!", MsgBoxStyle.Information)
                        End Using

                        With frm_college_settings_course_list.DataGridView1
                            .Rows.Add(GetInsertedID("TBL_SETTINGS_COLLEGE_COURSES"), txtCourseCode.Text, txtCourseDescription.Text, txtDepartment.Text)
                        End With
                        Me.Close()
                        Me.Dispose()
                    Else
                        MsgBox("Cannot Add Course Code:" & txtCourseCode.Text & " is already exist!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf Saving_Status = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_CODE = @course_code AND COURSE_ID <> @course_id", conn)
                    comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                    comm.Parameters.AddWithValue("@course_id", ID)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_COLLEGE_COURSES SET COURSE_CODE = @course_code, COURSE_DESC = @course_desc, DEPARTMENT = @department, PROGRAM = @program WHERE COURSE_ID = @course_id", conn)
                            comm1.Parameters.AddWithValue("@course_id", ID)
                            comm1.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm1.Parameters.AddWithValue("@course_desc", txtCourseDescription.Text)
                            comm1.Parameters.AddWithValue("@department", txtDepartment.Text)
                            comm1.Parameters.AddWithValue("@program", cmbProgram.Text)
                            comm1.ExecuteNonQuery()
                            MsgBox("Course has been successfully updated!", MsgBoxStyle.Information)
                        End Using

                        With frm_college_settings_course_list
                            .DataGridView1.Rows(.DGRow).Cells(1).Value = txtCourseCode.Text
                            .DataGridView1.Rows(.DGRow).Cells(2).Value = txtCourseDescription.Text
                            .DataGridView1.Rows(.DGRow).Cells(3).Value = txtDepartment.Text
                        End With

                        Me.Close()
                        Me.Dispose()
                    Else
                        MsgBox("Cannot Update Course Code:" & txtCourseCode.Text & " is already used by another Course!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub
End Class