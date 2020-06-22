Public Class frm_student_registration
    Public ID As String = String.Empty
    Public Saving_Status As String = String.Empty
    Public Education_Level As String = String.Empty
    Public Student_Number As String = String.Empty

    Private Function Generate_Student_Password()
        Dim Password As DateTime = cmbMonth.Text & " " & txtDay.Text & " " & txtYear.Text
        Dim Generated_Result As String = Password.ToString.Replace("/", "")
        Generated_Result = Generated_Result.ToString.Replace("12:00:00 AM", "")
        Return Generated_Result
    End Function

    Public Sub Save_Forms()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            For i = 0 To DataGridView1.Rows.Count - 1
                Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_FORMS WHERE STUDENT_NUMBER = @sn AND FORM_ID = @form_id", conn)
                    comm.Parameters.AddWithValue("@sn", Student_Number)
                    comm.Parameters.AddWithValue("@form_id", DataGridView1.Rows(i).Cells(0).Value)
                    If Val(comm.ExecuteScalar) = 0 Then
                        'INSERT FORM WITHIN A LOOP
                        Using comm1 As New SqlCommand("INSERT INTO TBL_STUDENT_FORMS VALUES(@sn,@form_id,@form_code,@is_submitted,GETDATE())", conn)
                            comm1.Parameters.AddWithValue("@sn", Student_Number)
                            comm1.Parameters.AddWithValue("@form_id", DataGridView1.Rows(i).Cells(0).Value)
                            comm1.Parameters.AddWithValue("@form_code", DataGridView1.Rows(i).Cells(1).Value)
                            If DataGridView1.Rows(i).Cells(2).Value = True Then
                                comm1.Parameters.AddWithValue("@is_submitted", "TRUE")
                            Else
                                comm1.Parameters.AddWithValue("@is_submitted", "FALSE")
                            End If
                            comm1.ExecuteNonQuery()
                        End Using
                    Else
                        'UPDATE FORM IF IS SUBMITTED = TRUE IF IS CHECKED IN LISTVIEW
                        Using comm1 As New SqlCommand("UPDATE TBL_STUDENT_FORMS SET IS_SUBMITTED = @is_submitted WHERE STUDENT_NUMBER = @sn AND FORM_ID = @form_id", conn)
                            comm1.Parameters.AddWithValue("@sn", Student_Number)
                            comm1.Parameters.AddWithValue("@form_id", DataGridView1.Rows(i).Cells(0).Value)
                            If DataGridView1.Rows(i).Cells(2).Value = True Then
                                comm1.Parameters.AddWithValue("@is_submitted", "TRUE")
                            Else
                                comm1.Parameters.AddWithValue("@is_submitted", "FALSE")
                            End If
                            comm1.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            Next
        End Using
    End Sub

    Public Sub Load_Forms()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FORMS WHERE IS_REQUIRED = 'TRUE' ORDER BY FORM_CODE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("FORM_CODE"), False)
                    End While
                End Using
            End Using

            For i = 0 To DataGridView1.Rows.Count - 1
                Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_FORMS WHERE STUDENT_NUMBER = @sn AND FORM_ID = @form_id", conn)
                    comm.Parameters.AddWithValue("@sn", Student_Number)
                    comm.Parameters.AddWithValue("@form_id", DataGridView1.Rows(i).Cells(0).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            If reader("IS_SUBMITTED") = "TRUE" Then
                                DataGridView1.Rows(i).Cells(2).Value = True
                            End If
                        End While
                    End Using
                End Using
            Next
        End Using
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Student_Type As String = String.Empty
        Dim SL_Name As String = String.Empty
        Dim SF_Name As String = String.Empty
        Dim SM_Name As String = String.Empty
        Dim SE_Name As String = String.Empty
        Dim B_Month As String = String.Empty : Dim B_Day As String = String.Empty : Dim B_Year As String = String.Empty
        Dim Gender As String = String.Empty
        Dim Street As String = String.Empty
        Dim Brgy As String = String.Empty
        Dim Municipality As String = String.Empty
        Dim Province As String = String.Empty
        Dim Mobile As String = String.Empty
        Dim Email As String = String.Empty
        Dim Last_School_Attended As String = String.Empty : Dim Adviser As String = String.Empty : Dim School_Location As String = String.Empty
        Dim MotherName As String = String.Empty : Dim Mother_Mobile As String = String.Empty
        Dim FatherName As String = String.Empty : Dim Father_Mobile As String = String.Empty
        Dim Main_Guardian As String = String.Empty : Dim Guardian_Name As String = String.Empty : Dim Guardian_Mobile As String = String.Empty
        Dim Generated_Password As String = String.Empty

        'STUDENT TYPE
        If rdbNew.Checked = True Then
            Student_Type = "NEW STUDENT"
        ElseIf rdbOld.Checked = True Then
            Student_Type = "OLD STUDENT"
        ElseIf rdbTransferee.Checked = True Then
            Student_Type = "TRANSFEREE"
        End If
        'STUDENT NAME
        SL_Name = txtLastname.Text : SF_Name = txtFirstname.Text : SM_Name = txtMiddlename.Text : SE_Name = txtExtension.Text
        'BIRTHDATE
        B_Month = cmbMonth.Text : B_Day = txtDay.Text : B_Year = txtYear.Text
        'GENDER
        If rdbFemale.Checked = True Then
            Gender = "FEMALE"
        ElseIf rdbMale.Checked = True Then
            Gender = "MALE"
        End If
        'ADDRESS
        Street = txtStreet.Text : Brgy = txtBrgy.Text : Municipality = txtMunicipality.Text : Province = txtProvince.Text
        'MOBILE AND EMAIL
        Mobile = txtMobile.Text : Email = txtEmail.Text
        'LAST SCHOOL ATTENDED AND ADVISER
        Last_School_Attended = txtLastSchoolAttended.Text : Adviser = txtAdviser.Text : School_Location = txtSchoolLocation.Text
        'MOTHER NAME AND MOBILE
        MotherName = txtMotherName.Text : Mother_Mobile = txtMotherMobile.Text
        'FATHER NAME AND MOBIEL
        FatherName = txtFatherName.Text : Father_Mobile = txtFatherMobile.Text

        'MAIN GUARDIAN, NAME AND MOBILE
        If rdbMother.Checked = True Then
            Main_Guardian = "MY MOTHER"
        ElseIf rdbFather.Checked = True Then
            Main_Guardian = "MY FATHER"
        ElseIf rdbOTher.Checked = True Then
            Main_Guardian = "OTHER"
        End If
        Guardian_Name = txtGuardianName.Text : Guardian_Mobile = txtGuardianMobile.Text
        Generated_Password = txtAccountPassword.Text

        'VALIDATION
        If Student_Type = String.Empty Then
            MsgBox("Please select student type!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If SL_Name = String.Empty Or SF_Name = String.Empty Then
            MsgBox("Please enter student lastname and firstname!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If B_Month = String.Empty Or B_Day = String.Empty Or B_Year = String.Empty Then
            MsgBox("Please enter student birthdate!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Gender = String.Empty Then
            MsgBox("Please select student gender!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Street = String.Empty Or Brgy = String.Empty Or Municipality = String.Empty Or Province = String.Empty Then
            MsgBox("Please enter address correctly!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Mobile = String.Empty Then
            MsgBox("Please enter student mobile no!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Last_School_Attended = String.Empty Or Adviser = String.Empty Or School_Location = String.Empty Then
            MsgBox("Please enter student school information!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Main_Guardian = String.Empty Then
            MsgBox("Please select student guardian!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Guardian_Name = String.Empty Or Guardian_Mobile = String.Empty Then
            MsgBox("Please enter student guardian information!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Generated_Password = String.Empty Then
            If MsgBox("Please enter student password" & vbNewLine & "Click YES if you want to auto generate password" & vbNewLine & "Click NO if you want to enter it manaully!", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                txtAccountPassword = Generate_Student_Password()
            End If
        End If


        If Saving_Status = "NEW" Then

            Student_Number = String.Empty 'CLEAR THE STUDENT_NUMBER VARIABLE BEFORE SAVING

            Using conn As New SqlConnection(StringConnection)
                conn.Open()

                If Student_Type <> "OLD STUDENT" Then
                    'GENERATE STUDENT NUMBER
                    Using comm As New SqlCommand("SELECT COUNT(ID) FROM TBL_STUDENT_INFORMATION WHERE ACADEMIC_YEAR = @ay", conn)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        Dim Count As String = comm.ExecuteScalar
                        Dim Yr As String = RSet(Date.Now.Year.ToString, 2)
                        Dim Education_Level_No As String = String.Empty
                        If Education_Level = "COLLEGE" Then
                            Education_Level_No = "11"
                        ElseIf Education_Level = "HIGH SCHOOL" Then
                            Education_Level_No = "10"
                        ElseIf Education_Level = "K-12" Then
                            Education_Level_No = "09"
                        End If
                        If Count.ToString.Length = 1 Then
                            Student_Number = Yr & "-" & Education_Level_No & "-00" & Count
                        ElseIf Count.ToString.Length = 2 Then
                            Student_Number = Yr & "-" & Education_Level_No & "-0" & Count
                        ElseIf Count.ToString.Length >= 3 Then
                            Student_Number = Yr & "-" & Education_Level_No & "-" & Count
                        End If
                    End Using
                Else
                    With frm_student_number_generator
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog()
                        Student_Number = .MaskedTextBox1.Text
                    End With
                End If


                'SAVING OF STUDENT INFORMATION
                Using comm As New SqlCommand("INSERT INTO TBL_STUDENT_INFORMATION VALUES(@educ_level,@sn,@lname,@fname,@mname,@ename,@b_month,@b_day,@b_year,@gender,'SINGLE',@street,@brgy,@town,@province,@mobile,'N.A',@email,@school_name,@school_location,@mother_name,@mother_mobile,'N.A',@father_name,@father_mobile,'N.A',@main_guardian,@guardian_name,@guardian_mobile,'N.A',@ay,@sem,@password,@adviser,@picture,@student_type,GETDATE(),'FALSE')", conn)
                    comm.Parameters.AddWithValue("@educ_level", Education_Level)
                    comm.Parameters.AddWithValue("@sn", Student_Number)
                    comm.Parameters.AddWithValue("@lname", TestEmptyAndRemoveWhiteSpace(SL_Name))
                    comm.Parameters.AddWithValue("@fname", TestEmptyAndRemoveWhiteSpace(SF_Name))
                    comm.Parameters.AddWithValue("@mname", TestEmptyAndRemoveWhiteSpace(SM_Name))
                    comm.Parameters.AddWithValue("@ename", TestEmptyAndRemoveWhiteSpace(SE_Name))
                    comm.Parameters.AddWithValue("@b_month", TestEmptyAndRemoveWhiteSpace(B_Month))
                    comm.Parameters.AddWithValue("@b_day", TestEmptyAndRemoveWhiteSpace(B_Day))
                    comm.Parameters.AddWithValue("@b_year", TestEmptyAndRemoveWhiteSpace(B_Year))
                    comm.Parameters.AddWithValue("@gender", TestEmptyAndRemoveWhiteSpace(Gender))
                    comm.Parameters.AddWithValue("@street", TestEmptyAndRemoveWhiteSpace(Street))
                    comm.Parameters.AddWithValue("@brgy", TestEmptyAndRemoveWhiteSpace(Brgy))
                    comm.Parameters.AddWithValue("@town", TestEmptyAndRemoveWhiteSpace(Municipality))
                    comm.Parameters.AddWithValue("@province", TestEmptyAndRemoveWhiteSpace(Province))
                    comm.Parameters.AddWithValue("@mobile", TestEmptyAndRemoveWhiteSpace(Mobile))
                    comm.Parameters.AddWithValue("@email", TestEmptyAndRemoveWhiteSpace(Email))
                    comm.Parameters.AddWithValue("@school_name", TestEmptyAndRemoveWhiteSpace(Last_School_Attended))
                    comm.Parameters.AddWithValue("@school_location", TestEmptyAndRemoveWhiteSpace(School_Location))
                    comm.Parameters.AddWithValue("@mother_name", TestEmptyAndRemoveWhiteSpace(MotherName))
                    comm.Parameters.AddWithValue("@mother_mobile", TestEmptyAndRemoveWhiteSpace(Mother_Mobile))
                    comm.Parameters.AddWithValue("@father_name", TestEmptyAndRemoveWhiteSpace(FatherName))
                    comm.Parameters.AddWithValue("@father_mobile", TestEmptyAndRemoveWhiteSpace(Father_Mobile))
                    comm.Parameters.AddWithValue("@main_guardian", TestEmptyAndRemoveWhiteSpace(Main_Guardian))
                    comm.Parameters.AddWithValue("@guardian_name", TestEmptyAndRemoveWhiteSpace(Guardian_Name))
                    comm.Parameters.AddWithValue("@guardian_mobile", TestEmptyAndRemoveWhiteSpace(Guardian_Mobile))
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@password", Generated_Password)
                    comm.Parameters.AddWithValue("@adviser", TestEmptyAndRemoveWhiteSpace(Adviser))

                    PictureBox1.BackgroundImage = My.Resources.USER
                    Dim ms As New System.IO.MemoryStream()
                    PictureBox1.BackgroundImage.Save(ms, PictureBox1.BackgroundImage.RawFormat)
                    Dim data As Byte() = ms.GetBuffer
                    Dim p As New SqlParameter("@picture", SqlDbType.Image)
                    p.Value = data
                    comm.Parameters.Add(p)

                    comm.Parameters.AddWithValue("@student_type", Student_Type)
                    comm.ExecuteNonQuery()
                    MsgBox("Student Information has been successfully save!", MsgBoxStyle.Information)
                    Save_Forms() 'SAVING ALL FORMS IF IS NEW STUDENT
                    Me.Close()
                    Me.Dispose()
                    frm_student_selection.Close()
                    frm_student_selection.Dispose()
                End Using
            End Using
        ElseIf Saving_Status = "EDIT" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("UPDATE TBL_STUDENT_INFORMATION SET EDUCATION_LEVEL = @educ_level,LASTNAME = @lname, FIRSTNAME = @fname, MIDDLENAME = @mname, EXTENSION_NAME = @ename, B_MONTH = @b_month, B_DAY = @b_day, B_YEAR = @b_year, GENDER = @gender, STREET = @street, BRGY = @brgy, TOWN = @town, PROVINCE = @province, MOBILE = @mobile, EMAIL_ADDRESS = @email, SCHOOL_NAME = @school_name, SCHOOL_LOCATION = @school_location, MOTHER_NAME = @mother_name, MOTHER_MOBILE = @mother_mobile, FATHER_NAME = @father_name, FATHER_MOBILE = @father_mobile, MAIN_GUARDIAN = @main_guardian, GUARDIAN_NAME = @guardian_name, GUARDIAN_MOBILE = @guardian_mobile, STUDENT_PASSWORD = @password, STUDENT_ADVISER = @adviser, STUDENT_STATUS = @student_status,  STUDENT_PICTURE = @picture WHERE ID = @id", conn)
                    comm.Parameters.AddWithValue("@id", ID)
                    comm.Parameters.AddWithValue("@educ_level", Education_Level)
                    comm.Parameters.AddWithValue("@lname", TestEmptyAndRemoveWhiteSpace(SL_Name))
                    comm.Parameters.AddWithValue("@fname", TestEmptyAndRemoveWhiteSpace(SF_Name))
                    comm.Parameters.AddWithValue("@mname", TestEmptyAndRemoveWhiteSpace(SM_Name))
                    comm.Parameters.AddWithValue("@ename", TestEmptyAndRemoveWhiteSpace(SE_Name))
                    comm.Parameters.AddWithValue("@b_month", TestEmptyAndRemoveWhiteSpace(B_Month))
                    comm.Parameters.AddWithValue("@b_day", TestEmptyAndRemoveWhiteSpace(B_Day))
                    comm.Parameters.AddWithValue("@b_year", TestEmptyAndRemoveWhiteSpace(B_Year))
                    comm.Parameters.AddWithValue("@gender", TestEmptyAndRemoveWhiteSpace(Gender))
                    comm.Parameters.AddWithValue("@street", TestEmptyAndRemoveWhiteSpace(Street))
                    comm.Parameters.AddWithValue("@brgy", TestEmptyAndRemoveWhiteSpace(Brgy))
                    comm.Parameters.AddWithValue("@town", TestEmptyAndRemoveWhiteSpace(Municipality))
                    comm.Parameters.AddWithValue("@province", TestEmptyAndRemoveWhiteSpace(Province))
                    comm.Parameters.AddWithValue("@mobile", TestEmptyAndRemoveWhiteSpace(Mobile))
                    comm.Parameters.AddWithValue("@email", TestEmptyAndRemoveWhiteSpace(Email))
                    comm.Parameters.AddWithValue("@school_name", TestEmptyAndRemoveWhiteSpace(Last_School_Attended))
                    comm.Parameters.AddWithValue("@school_location", TestEmptyAndRemoveWhiteSpace(School_Location))
                    comm.Parameters.AddWithValue("@mother_name", TestEmptyAndRemoveWhiteSpace(MotherName))
                    comm.Parameters.AddWithValue("@mother_mobile", TestEmptyAndRemoveWhiteSpace(Mother_Mobile))
                    comm.Parameters.AddWithValue("@father_name", TestEmptyAndRemoveWhiteSpace(FatherName))
                    comm.Parameters.AddWithValue("@father_mobile", TestEmptyAndRemoveWhiteSpace(Father_Mobile))
                    comm.Parameters.AddWithValue("@main_guardian", TestEmptyAndRemoveWhiteSpace(Main_Guardian))
                    comm.Parameters.AddWithValue("@guardian_name", TestEmptyAndRemoveWhiteSpace(Guardian_Name))
                    comm.Parameters.AddWithValue("@guardian_mobile", TestEmptyAndRemoveWhiteSpace(Guardian_Mobile))
                    comm.Parameters.AddWithValue("@password", Generated_Password)
                    comm.Parameters.AddWithValue("@adviser", TestEmptyAndRemoveWhiteSpace(Adviser))
                    comm.Parameters.AddWithValue("@student_status", Student_Type)

                    Dim ms As New System.IO.MemoryStream()
                    PictureBox1.BackgroundImage.Save(ms, PictureBox1.BackgroundImage.RawFormat)
                    Dim data As Byte() = ms.GetBuffer
                    Dim p As New SqlParameter("@picture", SqlDbType.Image)
                    p.Value = data
                    comm.Parameters.Add(p)

                    comm.ExecuteNonQuery()
                    MsgBox("Student Information has been successfully update!", MsgBoxStyle.Information)
                    Save_Forms() 'UPDATE STUDENT FORMS IF THE STUDENT IS EXISTED
                    Me.Close()
                    Me.Dispose()
                End Using
            End Using

        End If

    End Sub

    Private Sub rdbMother_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMother.CheckedChanged
        txtGuardianName.Text = txtMotherName.Text
        txtGuardianMobile.Text = txtMotherMobile.Text
    End Sub

    Private Sub rdbFather_CheckedChanged(sender As Object, e As EventArgs) Handles rdbFather.CheckedChanged
        txtGuardianName.Text = txtFatherName.Text
        txtGuardianMobile.Text = txtFatherMobile.Text
    End Sub

    Private Sub rdbOTher_CheckedChanged(sender As Object, e As EventArgs) Handles rdbOTher.CheckedChanged
        txtGuardianName.Text = String.Empty
        txtGuardianMobile.Text = String.Empty
    End Sub

    Private Sub rdbOld_CheckedChanged(sender As Object, e As EventArgs) Handles rdbOld.CheckedChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Select Case TabControl1.SelectedTab.Text
            Case "STUDENT INFORMATION"
                TabControl1.SelectedTab = TabPage2
            Case "SCHOOL AND GUARDIAN INFORMATION"
                TabControl1.SelectedTab = TabPage3
            Case "ACCESS & PROFILE"
                TabControl1.SelectedTab = TabPage1
        End Select
    End Sub

    Private Sub txtYear_TextChanged(sender As Object, e As EventArgs) Handles txtYear.TextChanged
        txtAge.Text = GetAge(cmbMonth, txtDay, txtYear)
        If txtAge.Text <> String.Empty Then
            txtAccountPassword.Text = Generate_Student_Password()
        Else
            txtAccountPassword.Text = String.Empty
        End If
    End Sub

    Private Sub txtDay_TextChanged(sender As Object, e As EventArgs) Handles txtDay.TextChanged
        txtAge.Text = GetAge(cmbMonth, txtDay, txtYear)
        If txtAge.Text <> String.Empty Then
            txtAccountPassword.Text = Generate_Student_Password()
        Else
            txtAccountPassword.Text = String.Empty
        End If
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonth.SelectedIndexChanged
        txtAge.Text = GetAge(cmbMonth, txtDay, txtYear)
        If txtAge.Text <> String.Empty Then
            txtAccountPassword.Text = Generate_Student_Password()
        Else
            txtAccountPassword.Text = String.Empty
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Using FD As New OpenFileDialog()
                With FD
                    FD.Title = "OPEN IMAGE FILES"
                    .Filter = "IMAGE FILES|*.png;*.jpg"
                    If .ShowDialog = Windows.Forms.DialogResult.OK Then
                        PictureBox1.BackgroundImage = Image.FromFile(FD.FileName)
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox("Image Error!", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub rdbTransferee_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTransferee.CheckedChanged

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub frm_student_registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Forms()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        With frm_student_selection
            .Form_Request = "STUDENT REGISTRATION"
            If Education_Level = "COLLEGE" Then
                .rdbCollege.Checked = True
            ElseIf Education_Level = "HIGH SCHOOL" Then
                .rdbHighschool.Checked = True
            ElseIf Education_Level = "K-12" Then
                .rdbk12.Checked = True
            End If
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub
End Class
