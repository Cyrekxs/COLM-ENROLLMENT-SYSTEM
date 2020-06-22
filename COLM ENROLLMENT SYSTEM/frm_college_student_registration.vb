Public Class frm_college_student_registration
    Public RegistrationID As Integer = 0
    Public SavingStatus As String = String.Empty

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = @student_number AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@student_number", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                If Val(comm.ExecuteScalar) = 0 Then
                    Using comm1 As New SqlCommand("INSERT INTO TBL_STUDENT_REGISTERED VALUES(@student_number,@course_code,@yrlvl,@section_code,'REGISTRATION',@ay,@sem,GETDATE(),@registeredby)", conn)
                        comm1.Parameters.AddWithValue("@student_number", txtStudentNumber.Text)
                        comm1.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                        comm1.Parameters.AddWithValue("@yrlvl", cmbYrLvl.Text)
                        comm1.Parameters.AddWithValue("@section_code", cmbSectionCode.Text)
                        comm1.Parameters.AddWithValue("ay", Academic_Year)
                        comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm1.Parameters.AddWithValue("@registeredby", Account_Name)
                        If MsgBox("Are you sure you want to register this student for this School Year and Semester?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            comm1.ExecuteNonQuery()
                            MsgBox("Student has been successfully registered!", MsgBoxStyle.Information)
                        End If
                    End Using
                Else
                    MsgBox("Student is already registered!", MsgBoxStyle.Critical)
                End If
            End Using
        End Using
    End Sub

    Private Sub frm_college_student_registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Course_Codes(cmbCourseCode)
    End Sub

    Private Sub cmbCourseCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourseCode.SelectedIndexChanged
        Load_YearLvls("COLLEGE", cmbCourseCode.Text, cmbYrLvl)
    End Sub

    Private Sub cmbYrLvl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYrLvl.SelectedIndexChanged
        Load_Sections("COLLEGE", cmbCourseCode.Text, cmbYrLvl.Text, cmbSectionCode)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class