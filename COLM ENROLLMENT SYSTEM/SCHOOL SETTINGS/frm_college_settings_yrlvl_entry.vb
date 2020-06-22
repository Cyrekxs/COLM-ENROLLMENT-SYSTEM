Public Class frm_college_settings_yrlvl_entry
    Public YrlvlID As Integer = 0
    Public SavingStatus As String = String.Empty
    Public CourseCode As String = String.Empty
    Public YrLvl As String = String.Empty

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnSave.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE COURSE_CODE = @course_code AND YEAR_CODE = @yrlvl", conn)
                    comm.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", txtYrLvl.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_COLLEGE_YEARLEVELS VALUES(@orderno,@course_code,@yrlvl,@education_level)", conn)
                            comm1.Parameters.AddWithValue("@orderno", "0")
                            comm1.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", txtYrLvl.Text)
                            comm1.Parameters.AddWithValue("@education_level", "COLLEGE")
                            comm1.ExecuteNonQuery()
                        End Using
                        With frm_college_settings_yrlvl_lists.DataGridView1
                            .Rows.Add(GetInsertedID("TBL_SETTINGS_COLLEGE_YEARLEVELS"), cmbCourseCode.Text, txtYrLvl.Text)
                        End With
                        MsgBox("New Year Level has been successfully added!", MsgBoxStyle.Information)
                        Me.Close()
                        Me.Dispose()
                    Else
                        MsgBox("Year Level is already exist in this course!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf SavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE COURSE_CODE = @course_code AND YEAR_CODE = @yrlvl AND YEAR_ID <> @yrlvlID", conn)
                    comm.Parameters.AddWithValue("@yrlvlID", YrlvlID)
                    comm.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", txtYrLvl.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_COLLEGE_YEARLEVELS SET ORDER_NO = @orderno, COURSE_CODE = @course_code, YEAR_CODE = @yrlvl, EDUCATION_LEVEL = @education_level WHERE YEAR_ID = @yrlvlID", conn)
                            comm1.Parameters.AddWithValue("@yrlvlID", YrlvlID)
                            comm1.Parameters.AddWithValue("@orderno", "0")
                            comm1.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", txtYrLvl.Text)
                            comm1.Parameters.AddWithValue("@education_level", "COLLEGE")
                            comm1.ExecuteNonQuery()
                        End Using

                        With frm_college_settings_yrlvl_lists
                            .DataGridView1.Rows(.DGRow).Cells(1).Value = cmbCourseCode.Text
                            .DataGridView1.Rows(.DGRow).Cells(2).Value = txtYrLvl.Text
                        End With
                        MsgBox("Year Level has been successfully update!", MsgBoxStyle.Information)
                        Me.Close()
                        Me.Dispose()
                    Else
                        MsgBox("Year Level is already exist in this course!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub

    Private Sub frm_yrlvl_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Course_Codes(cmbCourseCode)
        cmbCourseCode.Text = CourseCode
        txtYrLvl.Text = YrLvl
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class