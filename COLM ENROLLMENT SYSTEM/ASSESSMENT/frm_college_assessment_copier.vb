Public Class frm_college_assessment_copier
    Public course_code As String = String.Empty
    Public Sub LoadStudentAssessed()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT *,(SELECT LASTNAME + ' ' + FIRSTNAME + ' ' + MIDDLENAME FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER) AS StudentName FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE EXISTS (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = 'TUITION FEE' HAVING SUM(AMOUNT_COLLECTED) >= 1) AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem AND COURSE_CODE = @course_code AND EDUCATION_LEVEL = 'COLLEGE' AND PULLOUTSTATUS = 'ACTIVE'", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@course_code", course_code)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView1.Rows.Add(reader("Student_Number"), reader("StudentName"), reader("Course_Code"), reader("YrLvl"), "COPY")
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_college_assessment_copier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudentAssessed()
    End Sub
End Class