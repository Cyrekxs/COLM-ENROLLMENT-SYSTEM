Public Class frm_payment_browse_student
    Public DGRow As Integer = 0
    Public Sub LoadAssessedStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' ELSE ' ' + EXTENSION_NAME END + ', ' + FIRSTNAME + CASE MIDDLENAME WHEN 'N.A' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,(SELECT COURSE_CODE FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS COURSE_CODE,(SELECT YRLVL FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS YRLVL,(SELECT SECT_CODE FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS SECT_CODE FROM TBL_STUDENT_INFORMATION WHERE EXISTS (SELECT STUDENT_NUMBER FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AND STUDENT_NUMBER + LASTNAME + ', ' + FIRSTNAME LIKE @search AND EDUCATION_LEVEL = @education_level ORDER BY STUDENT_NAME ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & StripSpaces(TextBox1.Text) & "%")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@education_level", frm_payment.EducationLevel)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_payment_browse_student_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAssessedStudents()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadAssessedStudents()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadAssessedStudents()
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            With frm_payment
                .txtStudentNumber.Text = DataGridView1.Rows(DGRow).Cells(0).Value
                .txtStudentName.Text = DataGridView1.Rows(DGRow).Cells(1).Value
                .txtCourse.Text = DataGridView1.Rows(DGRow).Cells(2).Value
                .txtYear.Text = DataGridView1.Rows(DGRow).Cells(3).Value
                .txtSection.Text = DataGridView1.Rows(DGRow).Cells(4).Value
                .Load_Assessment_Payment()
                .LoadSOA()
                .LoadPayments()
                Me.Close()
                Me.Dispose()
            End With
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub
End Class