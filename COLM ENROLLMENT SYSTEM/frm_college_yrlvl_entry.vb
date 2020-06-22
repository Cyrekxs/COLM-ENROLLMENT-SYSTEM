Public Class frm_college_yrlvl_entry
    Public Saving_Status As String = String.Empty
    Public ID As Integer = 0
    Private Sub frm_college_yrlvl_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Course_Codes(cmbCourseCode)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If Saving_Status = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE COURSE_CODE = @course_code AND YEAR_CODE = @yrlvl and EDUCATION_LEVEL = 'COLLEGE'", conn)
                    comm.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYrLvl.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_COLLEGE_YEARLEVELS VALUES(@order_no,@course_code,@yrlvl,'COLLEGE')", conn)
                            Dim Order_No As Integer = -1
                            If cmbYrLvl.Text = "1ST YEAR" Then
                                Order_No = 0
                            ElseIf cmbYrLvl.Text = "2ND YEAR" Then
                                Order_No = 1
                            ElseIf cmbYrLvl.Text = "3RD YEAR" Then
                                Order_No = 2
                            ElseIf cmbYrLvl.Text = "4TH YEAR" Then
                                Order_No = 3
                            ElseIf cmbYrLvl.Text = "5TH YEAR" Then
                                Order_No = 4
                            End If
                            comm1.Parameters.AddWithValue("@order_no", Order_No)
                            comm1.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbYrLvl.Text)
                            comm1.ExecuteNonQuery()
                            frm_course_yrlvl.Load_Yrlvl()
                            Me.Close()
                            Me.Dispose()
                        End Using
                    Else
                        MsgBox("Year Level: " & cmbYrLvl.Text & " is already exist in Course: " & cmbCourseCode.Text, MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf Saving_Status = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE COURSE_CODE = @course_code AND YEAR_CODE = @yrlvl and EDUCATION_LEVEL = 'COLLEGE' AND YEAR_ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", ID)
                    comm.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYrLvl.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_COLLEGE_YEARLEVELS SET ORDER_NO = @order_no, COURSE_CODE = @course_code, YEAR_CODE = @yrlvl", conn)
                            Dim Order_No As Integer = -1
                            If cmbYrLvl.Text = "1ST YEAR" Then
                                Order_No = 0
                            ElseIf cmbYrLvl.Text = "2ND YEAR" Then
                                Order_No = 1
                            ElseIf cmbYrLvl.Text = "3RD YEAR" Then
                                Order_No = 2
                            ElseIf cmbYrLvl.Text = "4TH YEAR" Then
                                Order_No = 3
                            ElseIf cmbYrLvl.Text = "5TH YEAR" Then
                                Order_No = 4
                            End If
                            comm1.Parameters.AddWithValue("@order_no", Order_No)
                            comm1.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbYrLvl.Text)
                            comm1.ExecuteNonQuery()
                            frm_course_yrlvl.Load_Yrlvl()
                            Me.Close()
                            Me.Dispose()
                        End Using
                    Else
                        MsgBox("Year Level: " & cmbYrLvl.Text & " is already exist in Course: " & cmbCourseCode.Text, MsgBoxStyle.Critical)
                    End If
                End Using
            End If

        End Using
    End Sub
End Class