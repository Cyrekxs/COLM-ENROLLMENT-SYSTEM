Public Class frm_student_lists
    Public Selected_Row
    Private Sub GetStudentCounts()
        Dim TotalStudents As Integer = 0
        Dim RegisteredStudents As Integer = 0
        Dim AssessedStudents As Integer = 0
        Dim EnrolledStudents As Integer = 0
        Dim Query As String = String.Empty

        For i = 0 To DataGridView1.Rows.Count - 1
            Select Case DataGridView1.Rows(i).Cells(4).Value
                Case "REGISTERED"
                    RegisteredStudents += 1
                Case "ASSESSED"
                    AssessedStudents += 1
                Case "ENROLLED"
                    EnrolledStudents += 1
            End Select
        Next

        If rdbAll.Checked = True Then
            Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
            'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
        ElseIf rdbCollege.Checked = True Then
            Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE EDUCATION_LEVEL = 'COLLEGE' AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
            'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = 'COLLEGE' AND IS_DELETED = 'FALSE' ORDER BY LASTNAME,FIRSTNAME ASC"
        ElseIf rdbk12.Checked = True Then
            Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE EDUCATION_LEVEL = 'K-12' AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
            'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = 'K-12' AND IS_DELETED = 'FALSE' ORDER BY LASTNAME,FIRSTNAME ASC"
        ElseIf rdbHighschool.Checked = True Then
            Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE EDUCATION_LEVEL = 'HIGH SCHOOL' AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
            'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = 'HIGH SCHOOL' AND IS_DELETED = 'FALSE' ORDER BY LASTNAME,FIRSTNAME ASC"
        Else
            Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
            'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand(Query, conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        TotalStudents += 1
                    End While
                End Using
            End Using
        End Using

        TS_Total.Text = TotalStudents
        TS_Registered.Text = RegisteredStudents
        TS_Assessed.Text = AssessedStudents
        TS_Enrolled.Text = EnrolledStudents

    End Sub
    Private Sub Load_Student_Information()
        Dim Query As String = String.Empty
        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            If rdbAll.Checked = True Then
                Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
                'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
            ElseIf rdbCollege.Checked = True Then
                Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = 'COLLEGE' AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
                'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = 'COLLEGE' AND IS_DELETED = 'FALSE' ORDER BY LASTNAME,FIRSTNAME ASC"
            ElseIf rdbk12.Checked = True Then
                Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = 'K-12' AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
                'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = 'K-12' AND IS_DELETED = 'FALSE' ORDER BY LASTNAME,FIRSTNAME ASC"
            ElseIf rdbHighschool.Checked = True Then
                Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = 'HIGH SCHOOL' AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
                'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = 'HIGH SCHOOL' AND IS_DELETED = 'FALSE' ORDER BY LASTNAME,FIRSTNAME ASC"
            Else
                Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT ENROLLMENT_STATUS FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLMENT_STATUS FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
                'Query = "SELECT ID,STUDENT_NUMBER,LASTNAME,FIRSTNAME,MIDDLENAME,EXTENSION_NAME,EDUCATION_LEVEL,(SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS ENROLLED FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME LIKE @search AND IS_DELETED = 'FALSE' ORDER BY EDUCATION_LEVEL,LASTNAME,FIRSTNAME ASC"
            End If

            Using comm As New SqlCommand(Query, conn)
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        If IsDBNull(reader("ENROLLMENT_STATUS")) = True Then
                            DataGridView1.Rows.Add(reader("ID"), reader("STUDENT_NUMBER"), reader("LASTNAME") & ", " & reader("FIRSTNAME") & " " & reader("EXTENSION_NAME").ToString.Replace("N.A", "") & ", " & reader("MIDDLENAME").ToString.Replace("N.A", ""), reader("EDUCATION_LEVEL"), "NOT REGISTERED", "EDIT", "DELETE")
                        Else
                            DataGridView1.Rows.Add(reader("ID"), reader("STUDENT_NUMBER"), reader("LASTNAME") & ", " & reader("FIRSTNAME") & " " & reader("EXTENSION_NAME").ToString.Replace("N.A", "") & ", " & reader("MIDDLENAME").ToString.Replace("N.A", ""), reader("EDUCATION_LEVEL"), reader("ENROLLMENT_STATUS"), "EDIT", "DELETE")
                        End If
                        'If Val(reader("ENROLLED")) >= 1 Then
                        '    DataGridView1.Rows.Add(reader("ID"), reader("STUDENT_NUMBER"), reader("LASTNAME") & ", " & reader("FIRSTNAME") & " " & reader("EXTENSION_NAME").ToString.Replace("N.A", "") & ", " & reader("MIDDLENAME").ToString.Replace("N.A", ""), reader("EDUCATION_LEVEL"), "YES", "EDIT", "DELETE")
                        'Else
                        '    DataGridView1.Rows.Add(reader("ID"), reader("STUDENT_NUMBER"), reader("LASTNAME") & ", " & reader("FIRSTNAME") & " " & reader("EXTENSION_NAME").ToString.Replace("N.A", "") & ", " & reader("MIDDLENAME").ToString.Replace("N.A", ""), reader("EDUCATION_LEVEL"), "NO", "EDIT", "DELETE")
                        'End If
                    End While
                End Using
            End Using
        End Using

        GetStudentCounts()
    End Sub

    Private Sub frm_student_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Student_Information()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Student_Information()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 5 Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                frm_student_information_entry.ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                frm_student_information_entry.Saving_Status = "EDIT"
                Dim image_data As Byte() = Nothing
                Using comm As New SqlCommand("SELECT STUDENT_PICTURE FROM TBL_STUDENT_INFORMATION WHERE ID = @id", conn)
                    comm.Parameters.AddWithValue("@id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                    If IsDBNull(comm.ExecuteScalar) = False Then
                        image_data = DirectCast(comm.ExecuteScalar, Byte())
                    End If
                End Using

                If Not image_data Is Nothing Then
                    Dim mybytearray As Byte() = image_data
                    Dim myimage As Image
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(mybytearray)
                    myimage = Image.FromStream(ms)
                    frm_student_information_entry.PictureBox1.BackgroundImage = myimage
                End If

                Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_INFORMATION WHERE ID = @id", conn)
                    comm.Parameters.AddWithValue("@id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            With frm_student_information_entry
                                .Student_Number = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                                If reader("STUDENT_STATUS") = "NEW STUDENT" Then
                                    .rdbNew.Checked = True
                                ElseIf reader("STUDENT_STATUS") = "OLD STUDENT" Then
                                    .rdbOld.Checked = True
                                ElseIf reader("STUDENT_STATUS") = "TRANSFEREE" Then
                                    .rdbTransferee.Checked = True
                                End If
                                .Student_Number = reader("STUDENT_NUMBER")
                                .txtLastname.Text = reader("LASTNAME").ToString.Replace("N.A", "")
                                .txtFirstname.Text = reader("FIRSTNAME").ToString.Replace("N.A", "")
                                .txtMiddlename.Text = reader("MIDDLENAME").ToString.Replace("N.A", "")
                                .txtExtension.Text = reader("EXTENSION_NAME").ToString.Replace("N.A", "")

                                .cmbMonth.Text = reader("B_MONTH")
                                .txtDay.Text = reader("B_DAY")
                                .txtYear.Text = reader("B_YEAR")

                                If reader("GENDER") = "MALE" Then
                                    .rdbMale.Checked = True
                                ElseIf reader("GENDER") = "FEMALE" Then
                                    .rdbFemale.Checked = True
                                End If

                                .txtStreet.Text = reader("STREET").ToString.Replace("N.A", "")
                                .txtBrgy.Text = reader("BRGY").ToString.Replace("N.A", "")
                                .txtMunicipality.Text = reader("TOWN").ToString.Replace("N.A", "")
                                .txtProvince.Text = reader("PROVINCE").ToString.Replace("N.A", "")


                                .txtMobile.Text = reader("MOBILE").ToString.Replace("N.A", "")
                                .txtEmail.Text = reader("EMAIL_ADDRESS").ToString.Replace("N.A", "")

                                .txtLastSchoolAttended.Text = reader("SCHOOL_NAME").ToString.Replace("N.A", "")
                                .txtAdviser.Text = reader("STUDENT_ADVISER").ToString.Replace("N.A", "")
                                .txtSchoolLocation.Text = reader("SCHOOL_LOCATION").ToString.Replace("N.A", "")

                                .txtMotherName.Text = reader("MOTHER_NAME").ToString.Replace("N.A", "")
                                .txtMotherMobile.Text = reader("MOTHER_MOBILE").ToString.Replace("N.A", "")

                                .txtFatherName.Text = reader("FATHER_NAME").ToString.Replace("N.A", "")
                                .txtFatherMobile.Text = reader("FATHER_MOBILE").ToString.Replace("N.A", "")


                                If reader("MAIN_GUARDIAN") = "MY MOTHER" Then
                                    .rdbMother.Checked = True
                                ElseIf reader("MAIN_GUARDIAN") = "MY FATHER" Then
                                    .rdbFather.Checked = True
                                ElseIf reader("MAIN_GUARDIAN") = "OTHER" Then
                                    .rdbOTher.Checked = True
                                End If

                                .txtGuardianName.Text = reader("GUARDIAN_NAME").ToString.Replace("N.A", "")
                                .txtGuardianMobile.Text = reader("GUARDIAN_MOBILE").ToString.Replace("N.A", "")
                                .txtEducationLevel.Text = reader("EDUCATION_LEVEL")

                                Select Case .txtEducationLevel.Text
                                    Case "COLLEGE"
                                        .rdbCollege.Checked = True
                                    Case "K-12"
                                        .rdbK12.Checked = True
                                    Case "HIGH SCHOOL"
                                        .rdbHS.Checked = True
                                End Select
                            End With
                        End While
                    End Using
                End Using
            End Using

            frm_student_information_entry.StartPosition = FormStartPosition.CenterParent
            frm_student_information_entry.ShowDialog()
        ElseIf e.ColumnIndex = 6 Then
            If MsgBox("Are you sure you want to remove this student?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("UPDATE TBL_STUDENT_INFORMATION SET IS_DELETED = 'TRUE' WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                        comm.ExecuteNonQuery()
                        MsgBox("Student has been successfully removed!", MsgBoxStyle.Information)
                        DataGridView1.Rows.Remove(DataGridView1.Rows(e.RowIndex))
                    End Using
                End Using
            End If
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        Selected_Row = e.RowIndex
        lblStudentNo.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        lblStudentName.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value

        Dim image_data As Byte() = Nothing
        Using conn1 As New SqlConnection(StringConnection)
            conn1.Open()
            Using comm As New SqlCommand("SELECT STUDENT_PICTURE FROM TBL_STUDENT_INFORMATION WHERE ID = @id", conn1)
                comm.Parameters.AddWithValue("@id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                If IsDBNull(comm.ExecuteScalar) = False Then
                    image_data = DirectCast(comm.ExecuteScalar, Byte())
                End If
            End Using

            Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem ORDER BY REGISTEREDDATE DESC", conn1)
                comm.Parameters.AddWithValue("@sn", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                If Val(comm.ExecuteScalar) = 0 Then
                    lblCourseCode.Text = "-"
                    lblYearLvl.Text = "-"
                    lblSectionCode.Text = "-"
                Else
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            lblCourseCode.Text = reader("COURSECODE")
                            lblYearLvl.Text = reader("YRLVL")
                            lblSectionCode.Text = reader("SECTIONCODE") 'Get_SectionName(reader("SECT_CODE"), reader("COURSE_CODE"), reader("YRLVL"))
                        End While
                    End Using
                End If
            End Using
        End Using

        If Not image_data Is Nothing Then
            Dim mybytearray As Byte() = image_data
            Dim myimage As Image
            Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(mybytearray)
            myimage = Image.FromStream(ms)
            PictureBox1.BackgroundImage = myimage
        End If

        Select Case DataGridView1.Rows(e.RowIndex).Cells(4).Value
            Case "REGISTERED" : btnAssessment.Text = "ASSESSMENT"
            Case "ASSESSED" : btnAssessment.Text = "RE-ASSESSMENT"
        End Select

        'If DataGridView1.Rows(e.RowIndex).Cells(4).Value = "YES" Then
        '    btnAssessment.Text = "RE-ASSESS"
        'ElseIf DataGridView1.Rows(e.RowIndex).Cells(4).Value = "NO" Then
        '    btnAssessment.Text = "ASSESS"
        'End If
    End Sub

    Private Sub rdbAll_CheckedChanged(sender As Object, e As EventArgs) Handles rdbAll.CheckedChanged
        If rdbAll.Checked = True Then
            Load_Student_Information()
        End If
        'Load_Student_Information()
    End Sub

    Private Sub rdbCollege_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCollege.CheckedChanged
        If rdbCollege.Checked = True Then
            Load_Student_Information()
        End If
    End Sub

    Private Sub rdbHighschool_CheckedChanged(sender As Object, e As EventArgs) Handles rdbHighschool.CheckedChanged
        If rdbHighschool.Checked = True Then
            Load_Student_Information()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frm_student_selection.Form_Request = "STUDENT LIST"
        If rdbCollege.Checked = True Then
            frm_student_selection.rdbCollege.Checked = True
        ElseIf rdbHighschool.Checked = True Then
            frm_student_selection.rdbHighschool.Checked = True
        ElseIf rdbk12.Checked = True Then
            frm_student_selection.rdbk12.Checked = True
        ElseIf rdbAll.Checked = True Then
            frm_student_selection.rdbCollege.Checked = False
            frm_student_selection.rdbk12.Checked = False
            frm_student_selection.rdbHighschool.Checked = False
        End If

        frm_student_selection.StartPosition = FormStartPosition.CenterParent
        frm_student_selection.ShowDialog()
    End Sub

    Private Sub rdbk12_CheckedChanged(sender As Object, e As EventArgs) Handles rdbk12.CheckedChanged
        If rdbk12.Checked = True Then
            Load_Student_Information()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnAssessment.Click
        Dim Course As String = String.Empty
        Dim YrLvl As String = String.Empty
        Dim Section As String = String.Empty

        Select Case DataGridView1.Rows(Selected_Row).Cells(4).Value
            Case "REGISTERED"

                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                        comm.Parameters.AddWithValue("@sn", lblStudentNo.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            While reader.Read
                                Course = reader("COURSECODE")
                                YrLvl = reader("YRLVL")
                                Section = reader("SECTIONCODE")
                            End While
                        End Using
                    End Using
                End Using

                With frm_college_assessment
                    Me.Hide()
                    .lblTitle.Text = "ASSESSMENT"
                    .Assessment_Status = "ASSESSMENT"
                    .txtEducationLevel.Text = "COLLEGE"
                    .txtStudentNumber.Text = lblStudentNo.Text
                    .txtStudentName.Text = lblStudentName.Text
                    .Course_Code = Course
                    .YrLvl = YrLvl
                    .Section_Code = Section
                    .MdiParent = frm_main
                    .Show()
                    .Left = 0
                    .Top = 0
                End With

            Case "ASSESSED"
                With frm_college_assessment
                    Me.Hide()
                    .lblTitle.Text = "RE-ASSESSMENT"

                    Using conn As New SqlConnection(StringConnection)
                        conn.Open()
                        Using comm As New SqlCommand("SELECT ID FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                            comm.Parameters.AddWithValue("@sn", lblStudentNo.Text)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            .Assessment_ID = comm.ExecuteScalar
                        End Using
                    End Using
                    .Assessment_Status = "RE-ASSESSMENT"
                    .txtEducationLevel.Text = "COLLEGE"
                    .txtStudentNumber.Text = lblStudentNo.Text
                    .txtStudentName.Text = lblStudentName.Text
                    .Course_Code = lblCourseCode.Text
                    .YrLvl = lblYearLvl.Text
                    .Section_Code = lblSectionCode.Text
                    .MdiParent = frm_main
                    .Show()
                    .Left = 0
                    .Top = 0
                    .Load_Setted_Assessment_Information()
                End With
            Case "NOT REGISTERED"
                MsgBox("Please Register this student first before assessment!", MsgBoxStyle.Critical)
                If MsgBox("Do you want to register this student now?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    frm_registration_college.EducationLevel = DataGridView1.Rows(Selected_Row).Cells(3).Value
                    frm_registration_college.txtSearch.Text = lblStudentNo.Text
                    'frm_registration_college.btnSearch.PerformClick()
                    frm_registration_college.StartPosition = FormStartPosition.CenterParent
                    frm_registration_college.ShowDialog()
                End If
        End Select
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.Rows(Selected_Row).Cells(4).Value = "ASSESSED" Then
            With frm_payment
                .txtStudentNumber.Text = lblStudentNo.Text
                .txtStudentName.Text = lblStudentName.Text

                .txtEducationLevel.Text = DataGridView1.Rows(Selected_Row).Cells(3).Value
                .txtCourse.Text = lblCourseCode.Text
                .txtYear.Text = lblYearLvl.Text
                .txtSection.Text = lblSectionCode.Text
                .Course = lblCourseCode.Text
                .Yrlvl = lblYearLvl.Text
                .Section_Code = lblSectionCode.Text
                .Load_Assessment_Payment()

                .StartPosition = FormStartPosition.CenterParent
                Me.Hide()
                .ShowDialog()
                Me.Show()
                Load_Student_Information()
            End With

        Else
            MsgBox(lblStudentName.Text & " is not yet assessed!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frm_registration_college.EducationLevel = DataGridView1.Rows(Selected_Row).Cells(3).Value
        frm_registration_college.txtSearch.Text = lblStudentNo.Text
        'frm_registration_college.btnSearch.PerformClick()
        If frm_registration_college.DataGridView1.Rows.Count > 0 Then
            frm_registration_college.DataGridView1.Rows(0).Selected = True
        End If
        frm_registration_college.StartPosition = FormStartPosition.CenterParent
        frm_registration_college.ShowDialog()
    End Sub
End Class