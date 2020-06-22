Public Class enrolled_senior_high
    Public Sub LoadEnrolledStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            'LIST OF ENROLLED SENIOR HIGH STUDENTS
            Using comm As New SqlCommand("SELECT * FROM FN_ENROLLEDSTUDENTS('SENIOR HIGH',@ay,@sem) ORDER BY Lastname,Firstname ASC", conn)
                comm.Parameters.AddWithValue("@education_level", "SENIOR HIGH")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView1.Rows.Add(reader("STUDENT_NUMBER"), reader("Lastname") & " " & reader("Firstname") & " " & reader("Middlename"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"))
                    End While
                End Using
            End Using


            'Using comm As New SqlCommand("SELECT TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER,(SELECT TOP 1 LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + EXTENSION_NAME END + ' ' + FIRSTNAME + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + MIDDLENAME END FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER) AS STUDENT_NAME,COURSE_CODE,YRLVL,SECT_CODE FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE PullOutStatus = 'ACTIVE' AND ACADEMIC_YR = @ay AND EDUCATION_LEVEL = @education_level AND EXISTS (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER AND FEE_STATUS = 'TUITION FEE' AND RECIEPT_STATUS = 'ACTIVE' AND ACADEMIC_YR = @ay HAVING SUM(AMOUNT_COLLECTED) >= 1) ORDER BY STUDENT_NAME ASC", conn)
            '    comm.Parameters.AddWithValue("@ay", Academic_Year)
            '    comm.Parameters.AddWithValue("@sem", Academic_Sem)
            '    comm.Parameters.AddWithValue("@education_level", "SENIOR HIGH")
            '    Using reader As SqlDataReader = comm.ExecuteReader
            '        DataGridView1.Rows.Clear()
            '        While reader.Read
            '            DataGridView1.Rows.Add(reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"))
            '        End While
            '    End Using
            'End Using


            'SUMMARY OF ENROLLED SENIOR HIGH STUDENTS
            'Using comm As New SqlCommand("SELECT DISTINCT COURSE_CODE,YRLVL,(SELECT COUNT(STUDENT_NUMBER) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE PullOutStatus = 'ACTIVE' AND ACADEMIC_YR = SUMMARY.ACADEMIC_YR AND COURSE_CODE = SUMMARY.COURSE_CODE AND YRLVL = SUMMARY.YRLVL AND EXISTS (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = 'TUITION FEE' AND ACADEMIC_YR = @ay HAVING SUM(AMOUNT_COLLECTED) >= 1)) AS NO_OF_STUDENTS FROM TBL_COLLEGE_ASSESSMENT_SUMMARY AS SUMMARY WHERE PullOutStatus = 'ACTIVE' AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YR = @ay ORDER BY YRLVL,COURSE_CODE ASC", conn)
            '    comm.Parameters.AddWithValue("@ay", Academic_Year)
            '    comm.Parameters.AddWithValue("@sem", Academic_Sem)
            '    comm.Parameters.AddWithValue("@education_level", "SENIOR HIGH")
            '    Using reader As SqlDataReader = comm.ExecuteReader
            '        DataGridView2.Rows.Clear()
            '        While reader.Read
            '            DataGridView2.Rows.Add(reader("COURSE_CODE"), reader("YRLVL"), reader("NO_OF_STUDENTS"))
            '        End While
            '    End Using
            'End Using

            Using comm As New SqlCommand("SELECT COUNT(*) AS NEW_STUDENTS FROM FN_ENROLLEDSTUDENTS('SENIOR HIGH',@ay,@sem) WHERE InformationRegistered LIKE '%2018%'", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtNewStudents.Text = reader("NEW_STUDENTS")
                    End While
                End Using
            End Using

            Using comm As New SqlCommand("SELECT COUNT(*) AS OLD_STUDENTS FROM FN_ENROLLEDSTUDENTS('SENIOR HIGH',@ay,@sem) WHERE InformationRegistered NOT LIKE '%2018%'", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtOldStudents.Text = reader("OLD_STUDENTS")
                    End While
                End Using
            End Using
        End Using



        lblTotalStudents.Text = "TOTAL STUDENTS: " & DataGridView1.Rows.Count

    End Sub

    Private Sub enrolled_senior_high_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEnrolledStudents()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
