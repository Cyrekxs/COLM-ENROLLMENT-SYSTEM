Public Class frm_student_information_registration_2

    Public ID As Integer = 0
    Public SavingStatus As String = String.Empty
    Public StudentNumber As String = String.Empty
    Public EducationLevel As String = String.Empty
    Public EducationLevelNo As Integer = 0


    Private Sub frm_student_information_registration_2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If SavingStatus = "NEW" Then

            'GENERATE STUDENT NUMBER
            Using conn As New SqlConnection(StringConnection)
                conn.Open()

                If cmbStudentType.Text <> "OLD STUDENT" Then
                    Using comm As New SqlCommand("SELECT COUNT(ID) FROM TBL_STUDENT_INFORMATION WHERE ACADEMIC_YEAR = @ay", conn)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        Dim Count As String = comm.ExecuteScalar
                        Dim Yr As String = RSet(Date.Now.Year.ToString, 2)
                        Dim Education_Level_No As String = String.Empty
                        If EducationLevel = "COLLEGE" Then
                            EducationLevelNo = "11"
                        ElseIf EducationLevel = "HIGH SCHOOL" Then
                            EducationLevelNo = "10"
                        ElseIf EducationLevel = "K-12" Then
                            EducationLevelNo = "09"
                        End If
                        If Count.ToString.Length = 1 Then
                            StudentNumber = Yr & "-" & Education_Level_No & "-00" & Count
                        ElseIf Count.ToString.Length = 2 Then
                            StudentNumber = Yr & "-" & Education_Level_No & "-0" & Count
                        ElseIf Count.ToString.Length >= 3 Then
                            StudentNumber = Yr & "-" & Education_Level_No & "-" & Count
                        End If
                    End Using
                End If

                Using comm As New SqlCommand("INSERT INTO TBL_STUDENT_INFORMATION VALUES(@education_level,@student_number,@lastname,@firstname,@middlename,@extension_name,@b_month,@b_day,@b_year,@gender,'SINGLE',@street,@brgy,@town,@province,@mobile,@telephone,@email_address,@school_name,@school_location,'N.A','N.A','N.A','N.A','N.A','N.A',@guardian_relation,@guardian_name,@guardian_mobile,@guardian_telephone,@ay,@sem,'N.A',@student_adviser,NULL,@student_status,GETDATE()),'FALSE'", conn)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@student_number", StudentNumber)
                    comm.Parameters.AddWithValue("@lastname", StripSpaces(txtLastname.Text))
                    comm.Parameters.AddWithValue("@firstname", StripSpaces(txtFirstname.Text))
                    comm.Parameters.AddWithValue("@middlename", StripSpaces(txtMiddlename.Text))
                    comm.Parameters.AddWithValue("@extension_name", StripSpaces(txtExtension.Text))
                    comm.Parameters.AddWithValue("@b_month", cmbMonth.Text)
                    comm.Parameters.AddWithValue("@b_day", txtDay.Text)
                    comm.Parameters.AddWithValue("@b_year", txtYear.Text)
                    comm.Parameters.AddWithValue("@gender", cmbGender.Text)
                    comm.Parameters.AddWithValue("@street", StripSpaces(txtStreet.Text))
                    comm.Parameters.AddWithValue("@brgy", StripSpaces(txtBrgy.Text))
                    comm.Parameters.AddWithValue("@municipality", StripSpaces(txtMunicipality.Text))
                    comm.Parameters.AddWithValue("@province", StripSpaces(txtProvince.Text))
                    comm.Parameters.AddWithValue("@mobile", txtMobile.Text)
                    comm.Parameters.AddWithValue("@telephone", txtTelephone.Text)
                    comm.Parameters.AddWithValue("@email_address", txtEmailAddress.Text)
                    comm.Parameters.AddWithValue("@school_name", StripSpaces(txtSchoolName.Text))
                    comm.Parameters.AddWithValue("@school_location", StripSpaces(txtSchoolAddress.Text))
                    comm.Parameters.AddWithValue("@guardian_relation", StripSpaces(txtGuardianRelation.Text))
                    comm.Parameters.AddWithValue("@guardian_name", StripSpaces(txtGuardianName.Text))
                    comm.Parameters.AddWithValue("@guardian_mobile", txtGuardianMobile.Text)
                    comm.Parameters.AddWithValue("@guardian_telephone", txtGuardianTelephone.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@student_adviser", StripSpaces(txtSchoolAdviser.Text))
                    comm.Parameters.AddWithValue("@student_status", cmbStudentType.Text)
                    comm.ExecuteNonQuery()
                    MsgBox("New Information has been successfully saved!", MsgBoxStyle.Information)
                End Using
            End Using
        ElseIf SavingStatus = "EDIT" Then

        End If
    End Sub
End Class