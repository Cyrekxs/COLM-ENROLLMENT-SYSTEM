Public Class frm_registration_seniorhigh_entry
    Public CourseCode As String = String.Empty
    Public YrLvl As String = String.Empty
    Public SectCode As String = String.Empty

    Public Sub LoadStudents()
        Dim StudentCount As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim SQLQuery As String = String.Empty
            SQLQuery = "SELECT ID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME FROM TBL_STUDENT_INFORMATION WHERE NOT EXISTS (SELECT STUDENT_NUMBER FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND EDUCATION_LEVEL = 'SENIOR HIGH' AND RegistrationStatus = 'ACTIVE') AND LASTNAME + ', ' + FIRSTNAME LIKE @search AND TBL_STUDENT_INFORMATION.EDUCATION_LEVEL = 'SENIOR HIGH' ORDER BY LASTNAME,FIRSTNAME ASC"

            Using comm As New SqlCommand(SQLQuery, conn)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        Dim StudentNumber As String = String.Empty
                        Dim StudentName As String = String.Empty

                        StudentNumber = reader("Student_Number")

                        If reader("Extension_Name") = "N.A" Then
                            If reader("Middlename") = "N.A" Then
                                StudentName = reader("Lastname") & ", " & reader("Firstname")
                            Else
                                StudentName = reader("Lastname") & ", " & reader("Firstname") & " " & reader("Middlename")
                            End If
                        Else
                            If reader("Middlename") = "N.A" Then
                                StudentName = reader("Lastname") & " " & reader("Extension_Name") & ", " & reader("Firstname")
                            Else
                                StudentName = reader("Lastname") & " " & reader("Extension_Name") & ", " & reader("Firstname") & " " & reader("Middlename")
                            End If
                        End If

                        StudentCount += 1
                        DataGridView1.Rows.Add(reader("ID"), StudentNumber, StudentName)
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadSeniorHighCourses()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SENIOR_COURSES WHERE Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY COURSE_CODE ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCourse.Items.Clear()
                    While reader.Read
                        cmbCourse.Items.Add(reader("COURSE_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadYrlvls()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_yearlevels WHERE Education_Level = 'SENIOR HIGH'", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbYear.Items.Clear()
                    While reader.Read
                        cmbYear.Items.Add(reader("Year_Code"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        Load_Sections("SENIOR HIGH", cmbCourse.Text, cmbYear.Text, cmbSection)
    End Sub

    Private Sub frm_registration_seniorhigh_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSeniorHighCourses()
        LoadStudents()
        LoadYrlvls()
        cmbCourse.Text = CourseCode
        cmbYear.Text = YrLvl
        cmbSection.Text = SectCode
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadStudents()
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        txtStudentNo.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtLastname.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If cmbCourse.Text = String.Empty Then
            MsgBox("Please select course!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbYear.Text = String.Empty Then
            MsgBox("Please select year level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_REGISTERED WHERE EDUCATION_LEVEL = 'SENIOR HIGH' AND STUDENT_NUMBER = @sn AND ACADEMIC_YEAR = @ay AND RegistrationStatus = 'ACTIVE'", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                If Val(comm.ExecuteScalar) = 0 Then
                    Using commInsert As New SqlCommand("INSERT INTO TBL_STUDENT_REGISTERED VALUES('SENIOR HIGH',@sn,@course_code,@yrlvl,@section_code,@enrollment_status,@ay,@sem,@registeredby,GETDATE(),'ACTIVE')", conn)
                        commInsert.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                        commInsert.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                        commInsert.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                        commInsert.Parameters.AddWithValue("@section_code", cmbSection.Text)
                        commInsert.Parameters.AddWithValue("@enrollment_status", "REGISTERED")
                        commInsert.Parameters.AddWithValue("@ay", Academic_Year)
                        commInsert.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                        commInsert.Parameters.AddWithValue("@registeredby", Account_Name)
                        commInsert.ExecuteNonQuery()
                        MsgBox("Student No: " & txtStudentNo.Text & " is now registered in " & Academic_Sem & " " & Academic_Year, MsgBoxStyle.Information)
                        frm_registered_seniorhigh_students.MdiParent = frm_main
                        frm_registered_seniorhigh_students.Show()
                        frm_registered_seniorhigh_students.Top = 0
                        frm_registered_seniorhigh_students.Left = 0
                        Me.Close()
                    End Using
                End If
            End Using
        End Using
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel this transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            frm_registered_seniorhigh_students.MdiParent = frm_main
            frm_registered_seniorhigh_students.Show()
            frm_registered_seniorhigh_students.Top = 0
            frm_registered_seniorhigh_students.Left = 0
            Me.Close()
        End If
    End Sub
End Class