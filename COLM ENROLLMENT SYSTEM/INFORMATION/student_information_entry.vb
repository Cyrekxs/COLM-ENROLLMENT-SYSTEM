Public Class student_information_entry
    Dim IsStudentNameExist As Boolean = False
    Public ID As Integer = 0
    Public SavingStatus As String = String.Empty
    Public EducationLevel As String = String.Empty
    Public EducationLevelNo As String = String.Empty
    Public StudentType As String = String.Empty
    Public StudentNo As String = String.Empty
    Public LastName As String = String.Empty
    Public OldLastname As String = String.Empty
    Public ExtensionName As String = String.Empty
    Public FirstName As String = String.Empty
    Public OldFirstname As String = String.Empty
    Public MiddleName As String = String.Empty
    Public OldMIddlename As String = String.Empty
    Public BirthMonthInt As Integer = 0
    Public BirthMonth As String = String.Empty
    Public BirthDay As String = String.Empty
    Public BirthYear As String = String.Empty
    Public Gender As String = String.Empty
    Public StudentMobile As String = String.Empty
    Public StudentTelephone As String = String.Empty
    Public StudentEmail As String = String.Empty
    Public Street As String = String.Empty
    Public Brgy As String = String.Empty
    Public Municipality As String = String.Empty
    Public Province As String = String.Empty
    Public SchoolName As String = String.Empty
    Public SchoolAdviser As String = String.Empty
    Public SchoolAddress As String = String.Empty
    Public GuardianName As String = String.Empty
    Public GuardianMobile As String = String.Empty
    Public GuardianTelephone As String = String.Empty
    Public GuardianRelation As String = String.Empty
    Dim BrgyCollection As New AutoCompleteStringCollection
    Dim MunicipalityCollection As New AutoCompleteStringCollection
    Dim ProvinceCollection As New AutoCompleteStringCollection
    Dim SchoolCollection As New AutoCompleteStringCollection
    Dim DayCollection As New AutoCompleteStringCollection
    Dim YearCollection As New AutoCompleteStringCollection

    Public Sub GenerateStudentNumber()
        If StudentType <> "OLD STUDENT" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT COUNT(ID) FROM TBL_STUDENT_INFORMATION WHERE ACADEMIC_YEAR = @ay AND EDUCATION_LEVEL = @education_level", conn)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    Dim Count As String = comm.ExecuteScalar
                    Dim Yr As String = Mid(Date.Now.Year.ToString, 3, 4)
                    Dim incr As Integer = 1

                    If EducationLevel = "COLLEGE" Then
                        EducationLevelNo = "11"
                    ElseIf EducationLevel = "JUNIOR HIGH" Then
                        EducationLevelNo = "10"
                    ElseIf EducationLevel = "SENIOR HIGH" Then
                        EducationLevelNo = "12"
                    ElseIf EducationLevel = "ELEMENTARY" Then
                        EducationLevelNo = "09"
                    ElseIf EducationLevel = "PRE ELEMENTARY" Then
                        EducationLevelNo = "08"
                    End If

                    If Count.ToString.Length = 1 Then
                        StudentNo = Yr & "-" & EducationLevelNo & "-00" & CInt(Count) + CInt(incr)
                    ElseIf Count.ToString.Length = 2 Then
                        StudentNo = Yr & "-" & EducationLevelNo & "-0" & CInt(Count) + CInt(incr)
                    ElseIf Count.ToString.Length >= 3 Then
                        StudentNo = Yr & "-" & EducationLevelNo & "-" & CInt(Count) + CInt(incr)
                    End If

                    Dim StudentIExist As Boolean = True
                    Do While StudentIExist = True
                        Using comm1 As New SqlCommand("SELECT * FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = @sn", conn)
                            comm1.Parameters.AddWithValue("@sn", StudentNo)
                            If Val(comm1.ExecuteScalar) <> 0 Then
                                incr += 1
                                If Count.ToString.Length = 1 Then
                                    StudentNo = Yr & "-" & EducationLevelNo & "-00" & CInt(Count) + CInt(incr)
                                ElseIf Count.ToString.Length = 2 Then
                                    StudentNo = Yr & "-" & EducationLevelNo & "-0" & CInt(Count) + CInt(incr)
                                ElseIf Count.ToString.Length >= 3 Then
                                    StudentNo = Yr & "-" & EducationLevelNo & "-" & CInt(Count) + CInt(incr)
                                End If
                                StudentIExist = True
                            Else
                                StudentIExist = False
                            End If
                        End Using
                    Loop
                End Using
            End Using

        End If
    End Sub

    Public Function IsStudentNoExists(sn As String) As Boolean
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = @sn", conn)
                comm.Parameters.AddWithValue("@sn", StudentNo)
                If Val(comm.ExecuteScalar) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using
    End Function

    'Returns student number into string format
    Public Function GetGeneratedStudentNumber() As String
        Dim result As String = String.Empty
        If StudentType <> "OLD STUDENT" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT COUNT(ID) FROM TBL_STUDENT_INFORMATION WHERE ACADEMIC_YEAR = @ay AND EDUCATION_LEVEL = @education_level", conn)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    Dim Count As String = comm.ExecuteScalar
                    Dim Yr As String = Mid(Date.Now.Year.ToString, 3, 4)
                    Dim incr As Integer = 1

                    If EducationLevel = "COLLEGE" Then
                        EducationLevelNo = "11"
                    ElseIf EducationLevel = "JUNIOR HIGH" Then
                        EducationLevelNo = "10"
                    ElseIf EducationLevel = "SENIOR HIGH" Then
                        EducationLevelNo = "12"
                    ElseIf EducationLevel = "ELEMENTARY" Then
                        EducationLevelNo = "09"
                    ElseIf EducationLevel = "PRE ELEMENTARY" Then
                        EducationLevelNo = "08"
                    End If

                    If Count.ToString.Length = 1 Then
                        StudentNo = Yr & "-" & EducationLevelNo & "-00" & CInt(Count) + CInt(incr)
                    ElseIf Count.ToString.Length = 2 Then
                        StudentNo = Yr & "-" & EducationLevelNo & "-0" & CInt(Count) + CInt(incr)
                    ElseIf Count.ToString.Length >= 3 Then
                        StudentNo = Yr & "-" & EducationLevelNo & "-" & CInt(Count) + CInt(incr)
                    End If

                    Dim StudentIExist As Boolean = True
                    Do While StudentIExist = True
                        Using comm1 As New SqlCommand("SELECT * FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = @sn", conn)
                            comm1.Parameters.AddWithValue("@sn", StudentNo)
                            If Val(comm1.ExecuteScalar) <> 0 Then
                                incr += 1
                                If Count.ToString.Length = 1 Then
                                    StudentNo = Yr & "-" & EducationLevelNo & "-00" & CInt(Count) + CInt(incr)
                                ElseIf Count.ToString.Length = 2 Then
                                    StudentNo = Yr & "-" & EducationLevelNo & "-0" & CInt(Count) + CInt(incr)
                                ElseIf Count.ToString.Length >= 3 Then
                                    StudentNo = Yr & "-" & EducationLevelNo & "-" & CInt(Count) + CInt(incr)
                                End If
                                StudentIExist = True
                            Else
                                StudentIExist = False
                            End If
                        End Using
                    Loop
                End Using
            End Using
        End If
        result = StudentNo
        Return result
    End Function

    Public Sub LoadStudentInfo()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_INFORMATION WHERE ID = @id", conn)
                comm.Parameters.AddWithValue("@id", ID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        EducationLevel = reader("Education_Level")
                        StudentType = reader("STUDENT_STATUS")
                        txtLastname.Text = reader("LASTNAME")
                        OldLastname = txtLastname.Text
                        txtExtension.Text = reader("EXTENSION_NAME")
                        txtFirstname.Text = reader("FIRSTNAME")
                        OldFirstname = txtFirstname.Text
                        txtMiddlename.Text = reader("MIDDLENAME")
                        OldMIddlename = txtMiddlename.Text
                        cmbMonth.Text = reader("B_MONTH")
                        txtDay.Text = reader("B_DAY")
                        txtYear.Text = reader("B_YEAR")
                        cmbGender.Text = reader("GENDER")
                        txtStudentMobile.Text = reader("MOBILE")
                        txtStudentTelephone.Text = reader("TELEPHONE")
                        txtStudentEmail.Text = reader("EMAIL_ADDRESS")
                        txtSchoolName.Text = reader("School_Name")
                        txtStreet.Text = reader("STREET")
                        txtBrgy.Text = reader("BRGY")
                        txtMunicipality.Text = reader("TOWN")
                        txtProvince.Text = reader("PROVINCE")
                        txtGuardianName.Text = reader("GUARDIAN_NAME")
                        txtGuardianMobile.Text = reader("GUARDIAN_MOBILE")
                        txtGuardianTelephone.Text = reader("GUARDIAN_TELEPHONE")
                        txtGuardianRelation.Text = reader("MAIN_GUARDIAN")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub StudentNameDetector() Handles txtLastname.LostFocus, txtFirstname.LostFocus, txtMiddlename.LostFocus
        If SavingStatus = "NEW" Then
            If txtLastname.Text <> String.Empty And txtFirstname.Text <> String.Empty And txtMiddlename.Text <> String.Empty Then
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + FIRSTNAME + MIDDLENAME = @name AND Education_Level = @EducationLevel", conn)
                        comm.Parameters.AddWithValue("@name", txtLastname.Text + txtFirstname.Text + txtMiddlename.Text)
                        comm.Parameters.AddWithValue("@EducationLevel", EducationLevel)
                        If Val(comm.ExecuteScalar) <> 0 Then
                            IsStudentNameExist = True
                            MsgBox("Student name : " & txtLastname.Text & " " & txtFirstname.Text & " " & txtMiddlename.Text & " is already exist in database!", MsgBoxStyle.Information)
                            Exit Sub
                        End If
                    End Using
                End Using
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtLastname.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student Last Name!", MsgBoxStyle.Critical)
            txtLastname.Focus()
            Exit Sub
        Else
            LastName = StripSpaces(txtLastname.Text)
        End If

        If txtFirstname.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student First Name!", MsgBoxStyle.Critical)
            txtFirstname.Focus()
            Exit Sub
        Else
            FirstName = StripSpaces(txtFirstname.Text)
        End If

        If txtExtension.Text.ToLower = String.Empty Then
            ExtensionName = "N.A"
        Else
            ExtensionName = StripSpaces(txtExtension.Text)
        End If

        If txtMiddlename.Text.ToLower = String.Empty Then
            MiddleName = "N.A"
        Else
            MiddleName = StripSpaces(txtMiddlename.Text)
        End If

        'VERIFY IF STUDENT NAME IS ALREADY EXIST!
        If IsStudentNameExist = True Then
            MsgBox("Student name:  " & txtLastname.Text & " " & txtFirstname.Text & " " & txtMiddlename.Text & " is already exist!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbMonth.Text = String.Empty Then
            MsgBox("Please select Student Birthdate Month!", MsgBoxStyle.Critical)
            cmbMonth.Focus()
            Exit Sub
        Else
            BirthMonth = StripSpaces(cmbMonth.Text)
        End If

        If txtDay.Text.Length > 2 Then
            MsgBox("Invalid Birthdate day!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtDay.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student Birthdate Day!")
            txtDay.Focus()
            Exit Sub
        Else
            BirthDay = StripSpaces(txtDay.Text)
        End If

        If txtYear.Text.Length > 4 Then
            MsgBox("Invalid Birthdate year!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtYear.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student Birthdate Year!", MsgBoxStyle.Critical)
            txtYear.Focus()
            Exit Sub
        Else
            BirthYear = StripSpaces(txtYear.Text)
        End If

        If cmbGender.Text = String.Empty Then
            MsgBox("Please select Student Gender!", MsgBoxStyle.Critical)
            cmbGender.Focus()
        Else
            Gender = cmbGender.Text
        End If

        If txtStudentMobile.Text.ToLower = String.Empty Then
            StudentMobile = "N.A"
        Else
            StudentMobile = txtStudentMobile.Text
        End If

        If txtStudentTelephone.Text.ToLower = String.Empty Then
            StudentTelephone = "N.A"
        Else
            StudentTelephone = StripSpaces(txtStudentTelephone.Text)
        End If

        If txtStudentEmail.Text.ToLower = String.Empty Then
            StudentEmail = "N.A"
        Else
            StudentEmail = StripSpaces(txtStudentEmail.Text)
        End If

        If txtSchoolName.Text = String.Empty Then
            MsgBox("Please enter school name!", MsgBoxStyle.Critical)
            Exit Sub
            txtSchoolName.Focus()
        Else
            SchoolName = StripSpaces(txtSchoolName.Text)
        End If

        If txtStreet.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student Address Specific Location!", MsgBoxStyle.Critical)
            txtStreet.Focus()
            Exit Sub
        Else
            Street = StripSpaces(txtStreet.Text)
        End If

        If txtBrgy.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student Address Village (Barangay)!", MsgBoxStyle.Critical)
            txtBrgy.Focus()
            Exit Sub
        Else
            Brgy = StripSpaces(txtBrgy.Text)
        End If

        If txtMunicipality.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student Address City (Municipality)!", MsgBoxStyle.Critical)
            txtMunicipality.Focus()
            Exit Sub
        Else
            Municipality = StripSpaces(txtMunicipality.Text)
        End If

        If txtProvince.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student Address Province!", MsgBoxStyle.Critical)
            txtProvince.Focus()
            Exit Sub
        Else
            Province = StripSpaces(txtProvince.Text)
        End If

        If txtGuardianName.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student Guardian Name!", MsgBoxStyle.Critical)
            txtGuardianName.Focus()
            Exit Sub
        Else
            GuardianName = StripSpaces(txtGuardianName.Text)
        End If

        If txtGuardianMobile.Text = String.Empty Then
            MsgBox("Please enter Student Guardian Name!", MsgBoxStyle.Critical)
            txtGuardianMobile.Focus()
            Exit Sub
        Else
            GuardianMobile = txtGuardianMobile.Text
        End If

        If txtGuardianTelephone.Text.ToLower = String.Empty Then
            GuardianTelephone = "N.A"
        Else
            GuardianTelephone = StripSpaces(txtGuardianTelephone.Text)
        End If

        If txtGuardianRelation.Text.ToLower = String.Empty Then
            MsgBox("Please enter Student Guardian Relationship!", MsgBoxStyle.Critical)
            txtGuardianRelation.Focus()
            Exit Sub
        Else
            GuardianRelation = StripSpaces(txtGuardianRelation.Text)
        End If





        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = "NEW" Then

                If StudentType <> "OLD STUDENT" Then
                    GenerateStudentNumber()
                Else
                    StudentNo = InputBox("Please enter a valid student no")
                End If

                If IsStudentNoExists(StudentNo) = False Then
                    Using comm As New SqlCommand("INSERT INTO TBL_STUDENT_INFORMATION VALUES(@education_level,@student_number,@lastname,@firstname,@middlename,@extension_name,@b_month,@b_day,@b_year,@gender,'SINGLE',@street,@brgy,@town,@province,@mobile,@telephone,@email_address,@school_name,@school_location,'N.A','N.A','N.A','N.A','N.A','N.A',@guardian_relation,@guardian_name,@guardian_mobile,@guardian_telephone,@ay,@sem,'N.A',@student_adviser,NULL,@student_status,GETDATE(),'FALSE')", conn)
                        comm.Parameters.AddWithValue("@education_level", EducationLevel)
                        comm.Parameters.AddWithValue("@student_number", StudentNo)
                        comm.Parameters.AddWithValue("@lastname", LastName)
                        comm.Parameters.AddWithValue("@firstname", FirstName)
                        comm.Parameters.AddWithValue("@middlename", MiddleName)
                        comm.Parameters.AddWithValue("@extension_name", ExtensionName)
                        comm.Parameters.AddWithValue("@b_month", BirthMonth)
                        comm.Parameters.AddWithValue("@b_day", BirthDay)
                        comm.Parameters.AddWithValue("@b_year", BirthYear)
                        comm.Parameters.AddWithValue("@gender", Gender)
                        comm.Parameters.AddWithValue("@street", Street)
                        comm.Parameters.AddWithValue("@brgy", Brgy)
                        comm.Parameters.AddWithValue("@town", Municipality)
                        comm.Parameters.AddWithValue("@province", Province)
                        comm.Parameters.AddWithValue("@mobile", StudentMobile)
                        comm.Parameters.AddWithValue("@telephone", StudentTelephone)
                        comm.Parameters.AddWithValue("@email_address", StudentEmail)
                        comm.Parameters.AddWithValue("@school_name", SchoolName)
                        comm.Parameters.AddWithValue("@school_location", "NONE")
                        comm.Parameters.AddWithValue("@guardian_relation", GuardianRelation)
                        comm.Parameters.AddWithValue("@guardian_name", GuardianName)
                        comm.Parameters.AddWithValue("@guardian_mobile", GuardianMobile)
                        comm.Parameters.AddWithValue("@guardian_telephone", GuardianTelephone)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.Parameters.AddWithValue("@student_adviser", "NONE")
                        comm.Parameters.AddWithValue("@student_status", StudentType)
                        comm.ExecuteNonQuery()
                        MsgBox("New Information has been successfully saved!", MsgBoxStyle.Information)

                        Using logs As New SqlCommand("INSERT INTO tbl_program_logs VALUES (@LogType,@Logs,GETDATE(),@user)", conn)
                            logs.Parameters.AddWithValue("@LogType", "INSERT")
                            logs.Parameters.AddWithValue("@logs", "INSERT student added: " & StudentNo & " " & LastName & " " & FirstName & " " & MiddleName)
                            logs.Parameters.AddWithValue("@user", Account_Name)
                            logs.ExecuteNonQuery()
                        End Using

                        Me.Close()
                        Me.Dispose()
                    End Using
                End If



            ElseIf SavingStatus = "EDIT" Then

                Using comm As New SqlCommand("UPDATE TBL_STUDENT_INFORMATION SET LASTNAME = @lastname, FIRSTNAME = @firstname, MIDDLENAME = @middlename, EXTENSION_NAME = @extension_name, B_MONTH = @b_month, B_DAY = @b_day, B_YEAR = @b_year, GENDER = @gender, STREET = @street, BRGY = @brgy, TOWN = @town, PROVINCE = @province, MOBILE = @mobile, TELEPHONE = @telephone, EMAIL_ADDRESS = @email_address, SCHOOL_NAME = @school_name, SCHOOL_LOCATION = @school_location, MAIN_GUARDIAN = @guardian_relation, GUARDIAN_NAME = @guardian_name, GUARDIAN_MOBILE = @guardian_mobile, GUARDIAN_TELEPHONE = @guardian_telephone, STUDENT_ADVISER = @student_adviser, STUDENT_STATUS = @student_status WHERE ID = @id", conn)
                    comm.Parameters.AddWithValue("@id", ID)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@student_number", StudentNo)
                    comm.Parameters.AddWithValue("@lastname", LastName)
                    comm.Parameters.AddWithValue("@firstname", FirstName)
                    comm.Parameters.AddWithValue("@middlename", MiddleName)
                    comm.Parameters.AddWithValue("@extension_name", ExtensionName)
                    comm.Parameters.AddWithValue("@b_month", BirthMonth)
                    comm.Parameters.AddWithValue("@b_day", BirthDay)
                    comm.Parameters.AddWithValue("@b_year", BirthYear)
                    comm.Parameters.AddWithValue("@gender", Gender)
                    comm.Parameters.AddWithValue("@street", Street)
                    comm.Parameters.AddWithValue("@brgy", Brgy)
                    comm.Parameters.AddWithValue("@town", Municipality)
                    comm.Parameters.AddWithValue("@province", Province)
                    comm.Parameters.AddWithValue("@mobile", StudentMobile)
                    comm.Parameters.AddWithValue("@telephone", StudentTelephone)
                    comm.Parameters.AddWithValue("@email_address", StudentEmail)
                    comm.Parameters.AddWithValue("@school_name", SchoolName)
                    comm.Parameters.AddWithValue("@school_location", SchoolAddress)
                    comm.Parameters.AddWithValue("@guardian_relation", GuardianRelation)
                    comm.Parameters.AddWithValue("@guardian_name", GuardianName)
                    comm.Parameters.AddWithValue("@guardian_mobile", GuardianMobile)
                    comm.Parameters.AddWithValue("@guardian_telephone", GuardianTelephone)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@student_adviser", SchoolAdviser)
                    comm.Parameters.AddWithValue("@student_status", StudentType)
                    comm.ExecuteNonQuery()
                    MsgBox("Student Information has been successfully updated!", MsgBoxStyle.Information)

                    Using logs As New SqlCommand("INSERT INTO tbl_program_logs VALUES (@LogType,@Logs,GETDATE(),@user)", conn)
                        logs.Parameters.AddWithValue("@LogType", "UPDATE")
                        logs.Parameters.AddWithValue("@logs", "UPDATE student info old name is: " & StudentNo & " " & LastName & " " & FirstName & " " & MiddleName & " new name is: " & OldLastname & " " & OldFirstname & " " & OldMIddlename)
                        logs.Parameters.AddWithValue("@user", Account_Name)
                        logs.ExecuteNonQuery()
                    End Using

                    Me.Close()
                    Me.Dispose()
                End Using
            End If
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With student_information_import
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim msg As String = String.Empty
        If SavingStatus = "NEW" Then
            msg = "Are you sure you want to cancel creating student information?"
        ElseIf SavingStatus = "EDIT" Then
            msg = "Are you sure you want to cancel editing student information?"
        End If

        If MsgBox(msg, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub student_information_entry_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False

        BackgroundWorker1.RunWorkerAsync()
        BackgroundWorker2.RunWorkerAsync()
        Timer1.Enabled = True
        If SavingStatus = "NEW" Then
            Me.Text = "STUDENT INFORMATION ENTRY"
        ElseIf SavingStatus = "EDIT" Then
            Me.Text = "STUDENT INFORMATION EDITING"
            LoadStudentInfo()
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_ALL_BARANGAY ORDER BY BARANGAY ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        BrgyCollection.Add(UCase(reader("BARANGAY")))
                        MunicipalityCollection.Add(UCase(reader("MUNICIPALITY")))
                        ProvinceCollection.Add(UCase(reader("PROVINCE")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT SCHOOL_NAME FROM TBL_ALL_SCHOOL ORDER BY SCHOOL_NAME ASC", conn)
                comm.Parameters.AddWithValue("@brgy", txtBrgy.Text)
                comm.Parameters.AddWithValue("@municipality", txtMunicipality.Text)
                comm.Parameters.AddWithValue("@province", txtProvince.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    SchoolCollection.Clear()
                    While reader.Read
                        SchoolCollection.Add(UCase(reader("SCHOOL_NAME")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        txtBrgy.AutoCompleteCustomSource = BrgyCollection
        txtMunicipality.AutoCompleteCustomSource = MunicipalityCollection
        txtProvince.AutoCompleteCustomSource = ProvinceCollection
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        txtSchoolName.AutoCompleteCustomSource = SchoolCollection
    End Sub

    Private Sub txtDay_KeyPress(sender As Object, e As KeyPressEventArgs)
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtYear_KeyPress(sender As Object, e As KeyPressEventArgs)
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtLastname_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithDotAndComma(sender, e)
    End Sub

    Private Sub txtFirstname_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithDotAndComma(sender, e)
    End Sub

    Private Sub txtMiddlename_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithDotAndComma(sender, e)
    End Sub

    Private Sub txtStreet_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithNumbersAndDotAndComma(sender, e)
    End Sub

    Private Sub txtBrgy_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithNumbersAndDotAndComma(sender, e)
    End Sub

    Private Sub txtMunicipality_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithNumbersAndDotAndComma(sender, e)
    End Sub

    Private Sub txtProvince_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithNumbersAndDotAndComma(sender, e)
    End Sub

    Private Sub txtSchoolName_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithNumbersAndDotAndComma(sender, e)
    End Sub

    Private Sub txtSchoolAdviser_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithNumbersAndDotAndComma(sender, e)
    End Sub

    Private Sub txtSchoolAddress_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithNumbersAndDotAndComma(sender, e)
    End Sub

    Private Sub txtGuardianName_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithDotAndComma(sender, e)
    End Sub

    Private Sub txtGuardianMobile_KeyPress(sender As Object, e As KeyPressEventArgs)
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtGuardianRelation_KeyPress(sender As Object, e As KeyPressEventArgs)
        LettersWithDotAndComma(sender, e)
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(sender As Object, e As EventArgs)
        Select Case cmbMonth.Text
            Case "JANUARY" : BirthMonthInt = 1
            Case "FEBRUARY" : BirthMonthInt = 2
            Case "MARCH" : BirthMonthInt = 3
            Case "APRIL" : BirthMonthInt = 4
            Case "MAY" : BirthMonthInt = 5
            Case "JUNE" : BirthMonthInt = 6
            Case "JULY" : BirthMonthInt = 7
            Case "AUGUST" : BirthMonthInt = 8
            Case "SEPTEMBER" : BirthMonthInt = 9
            Case "OCTOBER" : BirthMonthInt = 10
            Case "NOVEMBER" : BirthMonthInt = 11
            Case "DECEMBER" : BirthMonthInt = 12
        End Select

        DayCollection.Clear()
        Select Case BirthMonthInt
            Case 1, 3, 5, 7, 8, 10, 12
                For i = 1 To 31
                    DayCollection.Add(i.ToString)
                Next
            Case 4, 6, 9, 11
                For i = 1 To 30
                    DayCollection.Add(i.ToString)
                Next
            Case 2
                For i = 1 To 29
                    DayCollection.Add(i.ToString)
                Next
        End Select
        txtDay.AutoCompleteCustomSource = DayCollection
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If cmbMonth.Text <> String.Empty And txtDay.Text <> String.Empty And txtDay.Text.Length <= 2 And txtYear.Text <> String.Empty And txtYear.Text.Length <= 4 Then
            txtAge.Text = GetAge(CDate(cmbMonth.Text & " " & txtDay.Text & " " & txtYear.Text))
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MsgBox(GetGeneratedStudentNumber().ToString)
    End Sub
End Class