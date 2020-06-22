Public Class frm_registration_college_entry
    Public EducationLevel As String = String.Empty
    Private DGRow As Integer = 0
    Public CourseCode As String = String.Empty
    Public YrLvl As String = String.Empty
    Public SectCode As String = String.Empty

    Public Sub LoadStudents()
        Dim StudentCount As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim SQLQuery As String = String.Empty
            SQLQuery = "SELECT ID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME FROM TBL_STUDENT_INFORMATION WHERE NOT EXISTS (SELECT STUDENT_NUMBER FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem AND EDUCATION_LEVEL = 'COLLEGE') AND LASTNAME + ', ' + FIRSTNAME LIKE @search AND TBL_STUDENT_INFORMATION.EDUCATION_LEVEL = 'COLLEGE' ORDER BY LASTNAME,FIRSTNAME ASC"

            Using comm As New SqlCommand(SQLQuery, conn)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
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

    Private Sub frm_student_registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudents()
        Load_Course_Codes(cmbCourse)
        cmbCourse.Text = CourseCode
        cmbYear.Text = YrLvl
        cmbSection.Text = SectCode
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadStudents()
        End If
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

        If cmbSection.Text = String.Empty Then
            MsgBox("Please select section!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_REGISTERED WHERE EDUCATION_LEVEL = 'COLLEGE' AND STUDENT_NUMBER = @sn AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                If Val(comm.ExecuteScalar) = 0 Then
                    Using commInsert As New SqlCommand("INSERT INTO TBL_STUDENT_REGISTERED VALUES('COLLEGE',@sn,@course_code,@yrlvl,@section_code,@enrollment_status,@ay,@sem,@registeredby,GETDATE(),'ACTIVE')", conn)
                        commInsert.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                        commInsert.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                        commInsert.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                        commInsert.Parameters.AddWithValue("@section_code", cmbSection.Text)
                        commInsert.Parameters.AddWithValue("@enrollment_status", "REGISTERED")
                        commInsert.Parameters.AddWithValue("@ay", Academic_Year)
                        commInsert.Parameters.AddWithValue("@sem", Academic_Sem)
                        commInsert.Parameters.AddWithValue("@registeredby", Account_Name)
                        commInsert.ExecuteNonQuery()
                        MsgBox("Student No: " & txtStudentNo.Text & " is now registered in " & Academic_Sem & " " & Academic_Year, MsgBoxStyle.Information)
                        frm_registered_college_students.MdiParent = frm_main
                        frm_registered_college_students.Show()
                        frm_registered_college_students.Top = 0
                        frm_registered_college_students.Left = 0
                        Me.Close()
                    End Using
                Else
                    Using commupdate As New SqlCommand("UPDATE TBL_STUDENT_REGISTERED SET COURSECODE = @course_code, YRLVL = @yrlvl, SECTIONCODE = @section_code, REGISTEREDDATE = GETDATE() WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                        commupdate.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                        commupdate.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                        commupdate.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                        commupdate.Parameters.AddWithValue("@section_code", cmbSection.Text)
                        commupdate.Parameters.AddWithValue("@ay", Academic_Year)
                        commupdate.Parameters.AddWithValue("@sem", Academic_Sem)
                        commupdate.ExecuteNonQuery()
                        MsgBox("Student No: " & txtStudentNo.Text & " is now registered in " & Academic_Sem & " " & Academic_Year, MsgBoxStyle.Information)
                        frm_registered_college_students.MdiParent = frm_main
                        frm_registered_college_students.Show()
                        frm_registered_college_students.Top = 0
                        frm_registered_college_students.Left = 0
                        Me.Close()
                    End Using
                End If
            End Using
        End Using
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        Load_Sections("COLLEGE", cmbCourse.Text, cmbYear.Text, cmbSection)
    End Sub

    Private Sub cmbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse.SelectedIndexChanged
        Load_YearLvls("COLLEGE", cmbCourse.Text, cmbYear)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            frm_registered_college_students.MdiParent = frm_main
            frm_registered_college_students.Show()
            frm_registered_college_students.Top = 0
            frm_registered_college_students.Left = 0
            Me.Close()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
        txtStudentNo.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtLastname.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
    End Sub
End Class