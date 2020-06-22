Public Class enrolled_college
    Public Sub LoadEnrolledStudents()
        Dim dtList As New DataTable
        Dim dtCount As New DataTable
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            'LIST OF ENROLLED COLLEGE STUDENTS
            Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudents('COLLEGE',@ay,@sem) ORDER BY Lastname,Firstname ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@education_level", "COLLEGE")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    dtList.Load(reader)
                End Using
            End Using

            For Each row As DataRow In dtList.Rows
                DataGridView1.Rows.Add(row("STUDENT_NUMBER"), row("Lastname") & " " & row("Firstname") & " " & row("Middlename"), row("GENDER"), row("COURSE_CODE"), row("YRLVL"), row("SECT_CODE"))
            Next

            'SUMMARY OF ENROLLED COLLEGE STUDENTS
            'Using comm As New SqlCommand("SELECT DISTINCT COURSE_CODE,YRLVL,(SELECT COUNT(STUDENT_NUMBER) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE ACADEMIC_YR = SUMMARY.ACADEMIC_YR AND ACADEMIC_SEM = SUMMARY.ACADEMIC_SEM AND COURSE_CODE = SUMMARY.COURSE_CODE AND YRLVL = SUMMARY.YRLVL AND EXISTS (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = 'TUITION FEE' AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem HAVING SUM(AMOUNT_COLLECTED) >= 1)) AS NO_OF_STUDENTS FROM TBL_COLLEGE_ASSESSMENT_SUMMARY AS SUMMARY WHERE PullOutStatus = 'ACTIVE' AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem ORDER BY COURSE_CODE,YRLVL ASC", conn)
            '    comm.Parameters.AddWithValue("@ay", Academic_Year)
            '    comm.Parameters.AddWithValue("@sem", Academic_Sem)
            '    comm.Parameters.AddWithValue("@education_level", "COLLEGE")
            '    Using reader As SqlDataReader = comm.ExecuteReader
            '        DataGridView2.Rows.Clear()
            '        dtCount.Load(reader)
            '        'While reader.Read
            '        '    DataGridView2.Rows.Add(reader("COURSE_CODE"), reader("YRLVL"), reader("NO_OF_STUDENTS"))
            '        'End While
            '    End Using
            'End Using

            For Each row As DataRow In dtCount.Rows
                DataGridView2.Rows.Add(row("COURSE_CODE"), row("YRLVL"), row("NO_OF_STUDENTS"))
            Next

        End Using


        Dim MaleCount As Integer = 0
        Dim FemaleCount As Integer = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(2).Value = "MALE" Then
                MaleCount += 1
            ElseIf DataGridView1.Rows(i).Cells(2).Value = "FEMALE" Then
                FemaleCount += 1
            End If
        Next
        txtTotal.Text = DataGridView1.Rows.Count
        txtMale.Text = MaleCount
        txtFemale.Text = FemaleCount
    End Sub

    Public Sub LoadNotEnrolledStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            'LIST OF ENROLLED COLLEGE STUDENTS
            Using comm As New SqlCommand("SELECT TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER,(SELECT TOP 1 LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + EXTENSION_NAME END + ' ' + FIRSTNAME + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + MIDDLENAME END FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER) AS STUDENT_NAME,(SELECT TOP 1 GENDER FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER) AS GENDER,COURSE_CODE,YRLVL,SECT_CODE FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE PullOutStatus = 'ACTIVE' AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem AND EDUCATION_LEVEL = @education_level AND NOT EXISTS (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER AND FEE_STATUS = 'TUITION FEE' AND RECIEPT_STATUS = 'ACTIVE' AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem HAVING SUM(AMOUNT_COLLECTED) >= 1) ORDER BY STUDENT_NAME ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@education_level", "COLLEGE")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView3.Rows.Clear()
                    While reader.Read
                        DataGridView3.Rows.Add(reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("GENDER"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"))
                    End While
                End Using
            End Using

            'SUMMARY OF ENROLLED COLLEGE STUDENTS
            'Using comm As New SqlCommand("SELECT DISTINCT COURSE_CODE,YRLVL,(SELECT COUNT(STUDENT_NUMBER) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE ACADEMIC_YR = SUMMARY.ACADEMIC_YR AND ACADEMIC_SEM = SUMMARY.ACADEMIC_SEM AND COURSE_CODE = SUMMARY.COURSE_CODE AND YRLVL = SUMMARY.YRLVL AND NOT EXISTS (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = 'TUITION FEE' AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem HAVING SUM(AMOUNT_COLLECTED) >= 1)) AS NO_OF_STUDENTS FROM TBL_COLLEGE_ASSESSMENT_SUMMARY AS SUMMARY WHERE PullOutStatus = 'ACTIVE' AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem ORDER BY COURSE_CODE,YRLVL ASC", conn)
            '    comm.Parameters.AddWithValue("@ay", Academic_Year)
            '    comm.Parameters.AddWithValue("@sem", Academic_Sem)
            '    comm.Parameters.AddWithValue("@education_level", "COLLEGE")
            '    Using reader As SqlDataReader = comm.ExecuteReader
            '        DataGridView4.Rows.Clear()
            '        While reader.Read
            '            DataGridView4.Rows.Add(reader("COURSE_CODE"), reader("YRLVL"), reader("NO_OF_STUDENTS"))
            '        End While
            '    End Using
            'End Using
        End Using


        Dim MaleCount As Integer = 0
        Dim FemaleCount As Integer = 0
        'For i = 0 To DataGridView3.Rows.Count - 1
        '    If DataGridView1.Rows(i).Cells(2).Value = "MALE" Then
        '        MaleCount += 1
        '    ElseIf DataGridView1.Rows(i).Cells(2).Value = "FEMALE" Then
        '        FemaleCount += 1
        '    End If
        'Next
        TextBox3.Text = DataGridView3.Rows.Count
        TextBox2.Text = MaleCount
        TextBox1.Text = FemaleCount
    End Sub

    Private Sub enrolled_college_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEnrolledStudents()
        LoadNotEnrolledStudents()
    End Sub
End Class
