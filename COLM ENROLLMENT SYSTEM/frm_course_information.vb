Public Class frm_course_information
    Public CourseID As Integer = 0
    Public SavingStatus As String = String.Empty

    Public Sub DisableControls()
        txtCourseCode.Enabled = False
        txtCourseDescription.Enabled = False
        txtDepartment.Enabled = False
        btnBrowseDepartment.Enabled = False
        txtUnits.Enabled = False

        btnNew.Enabled = True
        btnEdit.Enabled = True
        btnDelete.Enabled = True

        btnSave.Enabled = False
        btnCancel.Enabled = False

        txtSearch.Enabled = True
        DataGridView1.Enabled = True
    End Sub

    Public Sub EnableControls()
        txtCourseCode.Enabled = True
        txtCourseDescription.Enabled = True
        btnBrowseDepartment.Enabled = True
        txtUnits.Enabled = True

        btnNew.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnSave.Enabled = True
        btnCancel.Enabled = True

        txtSearch.Enabled = False
        DataGridView1.Enabled = False
    End Sub

    Public Sub ClearControls()
        txtCourseCode.Text = String.Empty
        txtCourseDescription.Text = String.Empty
        txtDepartment.Text = String.Empty
        txtUnits.Text = String.Empty
    End Sub

    Public Sub LoadCourses()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_CODE + COURSE_DESC LIKE @search ORDER BY DEPARTMENT ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & StripSpaces(txtSearch.Text) & "%")
                DataGridView1.Rows.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView1.Rows.Add(reader("Course_ID"), reader("Course_Code"), reader("Course_Desc"), reader("Department"), reader("Course_Units"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_course_information_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCourses()
        DisableControls()
        ClearControls()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        SavingStatus = "NEW"
        EnableControls()
        ClearControls()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        SavingStatus = "EDIT"
        EnableControls()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DisableControls()
        ClearControls()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_CODE = @course_code", conn)
                    comm.Parameters.AddWithValue("@course_code", StripSpaces(txtCourseCode.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_COLLEGE_COURSES VALUES(@course_code,@course_desc,@course_units,@department,GETDATE(),@activeuser)", conn)
                            comm1.Parameters.AddWithValue("@course_code", StripSpaces(txtCourseCode.Text))
                            comm1.Parameters.AddWithValue("@course_desc", StripSpaces(txtCourseDescription.Text))
                            comm1.Parameters.AddWithValue("@course_units", txtUnits.Text)
                            comm1.Parameters.AddWithValue("@department", txtDepartment.Text)
                            comm1.Parameters.AddWithValue("@activeuser", Account_Name)
                            comm1.ExecuteNonQuery()
                        End Using
                        MsgBox("Course Code: " & txtCourseCode.Text & " has been successfully saved!", MsgBoxStyle.Information)
                        DisableControls()
                        ClearControls()
                        LoadCourses()
                    Else
                        MsgBox("Course Code: " & txtCourseCode.Text & " is already exist!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf SavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_CODE = @course_code AND Course_ID <> @course_id", conn)
                    comm.Parameters.AddWithValue("@course_id", CourseID)
                    comm.Parameters.AddWithValue("@course_code", StripSpaces(txtCourseCode.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_COLLEGE_COURSES SET COURSE_CODE = @course_code, COURSE_DESC = @cours_desc, COURSE_UNITS = @course_units, COURSE_DEPARTMENT = @department, DATE = GETDATE(), ActiveUser = @activeuser WHERE COURSE_ID = @course_id", conn)
                            comm1.Parameters.AddWithValue("@course_id", CourseID)
                            comm1.Parameters.AddWithValue("@course_code", StripSpaces(txtCourseCode.Text))
                            comm1.Parameters.AddWithValue("@course_desc", StripSpaces(txtCourseDescription.Text))
                            comm1.Parameters.AddWithValue("@course_units", txtUnits.Text)
                            comm1.Parameters.AddWithValue("@department", txtDepartment.Text)
                            comm1.Parameters.AddWithValue("@activeuser", Account_Name)
                            comm1.ExecuteNonQuery()
                        End Using
                        MsgBox("Course Code: " & txtCourseCode.Text & " has been successfully updated!", MsgBoxStyle.Information)
                        DisableControls()
                        ClearControls()
                        LoadCourses()
                    Else
                        MsgBox("Course Code: " & txtCourseCode.Text & " is already exist!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadCourses()
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        CourseID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        txtCourseCode.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtCourseDescription.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtDepartment.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        txtUnits.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnBrowseDepartment.Click
        frm_browse_department.StartPosition = FormStartPosition.CenterParent
        frm_browse_department.ShowDialog()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("DELETE FROM TBL_SETTINGS_COLLEGE_COURSES WHERE COURSE_ID = @courseid", conn)
                comm.Parameters.AddWithValue("@courseid", CourseID)
                If MsgBox("Are you sure you want to delete this course?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    comm.ExecuteNonQuery()
                    MsgBox("Department has been successfully deleted!", MsgBoxStyle.Information)
                    LoadCourses()
                End If
            End Using
        End Using
    End Sub
End Class