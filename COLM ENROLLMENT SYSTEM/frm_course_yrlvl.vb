Public Class frm_course_yrlvl

    Public Sub Load_Courses()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_COURSES ORDER BY DEPARTMENT ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("COURSE_ID"), reader("COURSE_CODE"), reader("COURSE_DESC"), reader("DEPARTMENT"), "EDIT", "REMOVE")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub Load_Yrlvl()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE EDUCATION_LEVEL = 'COLLEGE' ORDER BY COURSE_CODE,YEAR_CODE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("YEAR_ID"), reader("COURSE_CODE"), reader("YEAR_CODE"), "EDIT", "REMOVE")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_course_yrlvl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Courses()
        Load_Yrlvl()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frm_college_yrlvl_entry.Saving_Status = "NEW"
        frm_college_yrlvl_entry.StartPosition = FormStartPosition.CenterParent
        frm_college_yrlvl_entry.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            For i = 0 To DataGridView2.Rows.Count - 1
                If Val(DataGridView2.Rows(i).Cells(0).Value) = 0 Then
                    If DataGridView2.Rows(i).Cells(1).Value <> "-" Or DataGridView2.Rows(i).Cells(2).Value <> "-" Then
                        'CHECK IF IT IS EXISTING
                        Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE COURSE_CODE = @course_code AND YEAR_CODE = @yrlvl", conn)
                            comm.Parameters.AddWithValue("@course_code", DataGridView2.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@yrlvl", DataGridView2.Rows(i).Cells(2).Value)
                            If Val(comm.ExecuteScalar) = 0 Then
                                Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_COLLEGE_YEARLEVELS VALUES(@order_no,@course_code,@yrlvl,'COLLEGE')", conn)
                                    Dim ORDER_NO As Integer = 0
                                    Select Case DataGridView2.Rows(i).Cells(2).Value
                                        Case "1ST YEAR"
                                            ORDER_NO = 0
                                        Case "2ND YEAR"
                                            ORDER_NO = 1
                                        Case "3RD YEAR"
                                            ORDER_NO = 2
                                        Case "4TH YEAR"
                                            ORDER_NO = 3
                                        Case "5TH YEAR"
                                            ORDER_NO = 4
                                    End Select
                                    comm1.Parameters.AddWithValue("@order_no", ORDER_NO)
                                    comm1.Parameters.AddWithValue("@course_code", DataGridView2.Rows(i).Cells(1).Value)
                                    comm1.Parameters.AddWithValue("@yrlvl", DataGridView2.Rows(i).Cells(2).Value)
                                    comm1.ExecuteNonQuery()
                                End Using
                            End If
                        End Using
                    End If
                Else
                    If DataGridView2.Rows(i).Cells(1).Value <> "-" Or DataGridView2.Rows(i).Cells(2).Value <> "-" Then
                        'CHECK IF EXISTING
                        Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE COURSE_CODE = @course_code AND YEAR_CODE = @yrlvl AND YEAR_ID <> @id", conn)
                            comm.Parameters.AddWithValue("@id", DataGridView2.Rows(i).Cells(0).Value)
                            comm.Parameters.AddWithValue("@course_code", DataGridView2.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@yrlvl", DataGridView2.Rows(i).Cells(2).Value)
                            If Val(comm.ExecuteScalar) = 0 Then
                                Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_COLLEGE_YEARLEVELS VALUES(@order_no,@course_code,@yrlvl,'COLLEGE')", conn)
                                    Dim ORDER_NO As Integer = 0
                                    Select Case DataGridView2.Rows(i).Cells(2).Value
                                        Case "1ST YEAR"
                                            ORDER_NO = 0
                                        Case "2ND YEAR"
                                            ORDER_NO = 1
                                        Case "3RD YEAR"
                                            ORDER_NO = 2
                                        Case "4TH YEAR"
                                            ORDER_NO = 3
                                        Case "5TH YEAR"
                                            ORDER_NO = 4
                                    End Select
                                    comm1.Parameters.AddWithValue("@order_no", ORDER_NO)
                                    comm1.Parameters.AddWithValue("@course_code", DataGridView2.Rows(i).Cells(1).Value)
                                    comm1.Parameters.AddWithValue("@yrlvl", DataGridView2.Rows(i).Cells(2).Value)
                                    comm1.ExecuteNonQuery()
                                End Using
                            End If
                        End Using
                    End If
                End If
            Next
        End Using
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.ColumnIndex = 3 Then
            frm_college_yrlvl_entry.Saving_Status = "EDIT"
            frm_college_yrlvl_entry.ID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            Program_Loaders.Load_Course_Codes(frm_college_yrlvl_entry.cmbCourseCode)
            frm_college_yrlvl_entry.cmbCourseCode.Text = DataGridView2.Rows(e.RowIndex).Cells(1).Value
            frm_college_yrlvl_entry.cmbYrLvl.Text = DataGridView2.Rows(e.RowIndex).Cells(2).Value
            frm_college_yrlvl_entry.StartPosition = FormStartPosition.CenterParent
            frm_college_yrlvl_entry.ShowDialog()
        ElseIf e.ColumnIndex = 4 Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("DELETE FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE Year_ID = @id", conn)
                    comm.Parameters.AddWithValue("@id", DataGridView2.Rows(e.RowIndex).Cells(0).Value)
                    If MsgBox("Are you sure you want to delete this year level?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        comm.ExecuteNonQuery()
                        Load_Yrlvl()
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub DataGridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit

    End Sub

    Private Sub DataGridView2_RowLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.RowLeave
        'For i = 0 To DataGridView2.Rows.Count - 1
        '    If DataGridView2.Rows(i).Cells(1).Value = DataGridView2.Rows(e.RowIndex).Cells(1).Value And DataGridView2.Rows(i).Cells(2).Value = DataGridView2.Rows(e.RowIndex).Cells(2).Value Then
        '        MsgBox("Same information already detected!", MsgBoxStyle.Critical)
        '        DataGridView2.Rows(e.RowIndex).Cells(1).Value = "-"
        '        DataGridView2.Rows(e.RowIndex).Cells(2).Value = "-"
        '    End If
        'Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frm_course_entry.Saving_Status = "NEW"
        frm_course_entry.txtCourseCode.Enabled = True
        frm_course_entry.StartPosition = FormStartPosition.CenterParent
        frm_course_entry.ShowDialog()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 4 Then
            frm_course_entry.Saving_Status = "EDIT"
            frm_course_entry.ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            frm_course_entry.txtCourseCode.Enabled = False
            frm_course_entry.txtCourseCode.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            frm_course_entry.txtCourseDescription.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            frm_course_entry.txtDepartment.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            frm_course_entry.StartPosition = FormStartPosition.CenterParent
            frm_course_entry.ShowDialog()
        ElseIf e.ColumnIndex = 5 Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("DELETE FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_ID = @id", conn)
                    comm.Parameters.AddWithValue("@id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                    If MsgBox("Are you sure you want to remove this course!", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        comm.ExecuteNonQuery()
                        MsgBox("Course has been successfully removed!", MsgBoxStyle.Information)
                        Load_Courses()
                    End If
                End Using
            End Using
        End If
    End Sub
End Class