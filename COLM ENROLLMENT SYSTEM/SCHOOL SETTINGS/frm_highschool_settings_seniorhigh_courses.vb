Public Class frm_highschool_settings_seniorhigh_courses
    Public CourseID As Integer = 0
    Public SavingStatus As String = String.Empty
    Public DGRow As Integer = 0

    Public Sub EnableControls()
        GroupBox1.Enabled = True
        GroupBox2.Enabled = False
        btnNew.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
    End Sub

    Public Sub DisableControls()
        GroupBox1.Enabled = False
        GroupBox2.Enabled = True
        btnNew.Enabled = True
        btnEdit.Enabled = True
        btnDelete.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
    End Sub

    Public Sub ClearControls()
        txtCourseCode.Text = String.Empty
        txtCourseDesc.Text = String.Empty
    End Sub

    Public Sub LoadCourses()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SENIOR_COURSES ORDER BY COURSE_CODE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("COURSE_CODE"), reader("COURSE_DESC"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_highschool_settings_seniorhigh_courses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCourses()
        ClearControls()
        DisableControls()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        SavingStatus = "NEW"
        EnableControls()
        ClearControls()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        SavingStatus = "EDIT"
        EnableControls()
        CourseID = DataGridView1.Rows(DGRow).Cells(0).Value
        txtCourseCode.Text = DataGridView1.Rows(DGRow).Cells(1).Value
        txtCourseDesc.Text = DataGridView1.Rows(DGRow).Cells(2).Value
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtCourseCode.Text = String.Empty Then
            MsgBox("Please enter Course Code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtCourseDesc.Text = String.Empty Then
            MsgBox("Please enter Course Description!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SENIOR_COURSES WHERE COURSE_CODE = @course_code", conn)
                    comm.Parameters.AddWithValue("@course_code", StripSpaces(txtCourseCode.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_SENIOR_COURSES VALUES(@course_code,@course_desc,'HIGH SCHOOL',GETDATE(),@user)", conn)
                            comm1.Parameters.AddWithValue("@course_code", StripSpaces(txtCourseCode.Text))
                            comm1.Parameters.AddWithValue("@course_desc", StripSpaces(txtCourseDesc.Text))
                            comm1.Parameters.AddWithValue("@user", Account_Name)
                            If MsgBox("Are you sure you want to save this Course?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New Course has been successfully saved!", MsgBoxStyle.Information)
                                ClearControls()
                                DisableControls()
                                LoadCourses()
                            End If
                        End Using
                    Else
                        MsgBox("Course is already exists!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf SavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SENIOR_COURSES WHERE COURSE_CODE = @course_code AND ID <> @course_id", conn)
                    comm.Parameters.AddWithValue("@course_id", CourseID)
                    comm.Parameters.AddWithValue("@course_code", StripSpaces(txtCourseCode.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_SENIOR_COURSES SET COURSE_CODE = @course_code, COURSE_DESC = @course_desc, SAVEDDATE = GETDATE(), ACTIVEUSER = @user WHERE ID = @course_id", conn)
                            comm1.Parameters.AddWithValue("@course_id", CourseID)
                            comm1.Parameters.AddWithValue("@course_code", StripSpaces(txtCourseCode.Text))
                            comm1.Parameters.AddWithValue("@course_desc", StripSpaces(txtCourseDesc.Text))
                            comm1.Parameters.AddWithValue("@user", Account_Name)
                            If MsgBox("Are you sure you want to update this Course?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New Course has been successfully updated!", MsgBoxStyle.Information)
                                ClearControls()
                                DisableControls()
                                LoadCourses()
                            End If
                        End Using
                    Else
                        MsgBox("Course is already used!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub
End Class