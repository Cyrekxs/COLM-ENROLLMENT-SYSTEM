Public Class frm_c_student_registration
    Public EducationLevel As String = String.Empty
    Private DGRow As Integer = 0
    Public Sub LoadStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER + LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = @education_level", conn)
                comm.Parameters.AddWithValue("@education_level", EducationLevel)
                comm.Parameters.AddWithValue("@search", "%" & StripSpaces(txtSearch.Text) & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("EDUCATION_LEVEL"), reader("STUDENT_NUMBER"), reader("LASTNAME"), reader("FIRSTNAME") & " " & reader("EXTENSION_NAME").ToString.Replace("N.A", ""), reader("MIDDLENAME"), GetAge(reader("B_MONTH") & " " & reader("B_DAY") & " " & reader("B_YEAR")), reader("GENDER"), reader("MOBILE"))
                    End While
                End Using
            End Using
        End Using
        TS_RecordCount.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub frm_student_registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudents()
        cmbCourse.Items.Clear()
        cmbYear.Items.Clear()
        cmbSection.Items.Clear()
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
        txtStudentNo.Text = DataGridView1.Rows(DGRow).Cells(2).Value
        txtLastname.Text = DataGridView1.Rows(DGRow).Cells(3).Value
        txtFirstname.Text = DataGridView1.Rows(DGRow).Cells(4).Value
        txtMiddlename.Text = DataGridView1.Rows(DGRow).Cells(5).Value
        If DataGridView1.Rows(DGRow).Cells(1).Value = "COLLEGE" Then
            cmbCourse.Items.Clear()
            cmbCourse.Enabled = True
            Load_Course_Codes(cmbCourse)
            cmbCourse.Items.Remove("K-12")
            cmbCourse.Items.Remove("HIGH SCHOOL")
        ElseIf DataGridView1.Rows(DGRow).Cells(1).Value = "K-12" Then
            cmbCourse.Items.Clear()
            cmbCourse.Items.Add("K-12")
            cmbCourse.Enabled = False
            cmbCourse.SelectedIndex = 0
            'cmbCourse.Text = "K-12"
        ElseIf DataGridView1.Rows(DGRow).Cells(1).Value = "HIGH SCHOOL" Then
            cmbCourse.Items.Clear()
            cmbCourse.Items.Add("HIGH SCHOOL")
            cmbCourse.Enabled = False
            cmbCourse.SelectedIndex = 0
            'cmbCourse.Text = "HIGH SCHOOL"
        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadStudents()
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadStudents()
    End Sub

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
                            .AddWithValue("@education_level", EducationLevel)
                            .AddWithValue("@sn", txtStudentNo.Text)
                            .AddWithValue("@course_code", cmbCourse.Text)
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
                            .AddWithValue("@education_level", EducationLevel)
                            .AddWithValue("@sn", txtStudentNo.Text)
                            .AddWithValue("@course_code", cmbCourse.Text)
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

    Private Sub cmbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse.SelectedIndexChanged
        If cmbCourse.Text <> "HIGH SCHOOL" And cmbCourse.Text <> "K-12" Then
            Load_YearLvls("COLLEGE", cmbCourse.Text, cmbYear)
        ElseIf cmbCourse.Text = "K-12" Then
            Load_YearLvls("K-12", cmbCourse.Text, cmbYear)
        ElseIf cmbCourse.Text = "HIGH SCHOOL" Then
            Load_YearLvls("HIGH SCHOOL", cmbCourse.Text, cmbYear)
        End If
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        If cmbCourse.Text <> "HIGH SCHOOL" And cmbCourse.Text <> "K-12" Then
            Load_Sections("COLLEGE", cmbCourse.Text, cmbYear.Text, cmbSection)
        ElseIf cmbCourse.Text = "K-12" Then
            Load_Sections("K-12", cmbCourse.Text, cmbYear.Text, cmbSection)
        ElseIf cmbCourse.Text = "HIGH SCHOOL" Then
            Load_Sections("HIGH SCHOOL", cmbCourse.Text, cmbYear.Text, cmbSection)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
End Class