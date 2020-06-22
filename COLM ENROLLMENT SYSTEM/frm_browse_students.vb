Public Class frm_browse_students
    Public Form_Request As String = String.Empty
    Dim DGRow As Integer = 0
    Public Sub Load_Students()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' ELSE ' ' + EXTENSION_NAME END +  ', ' + FIRSTNAME + CASE MIDDLENAME WHEN 'N.A' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,(SELECT COURSECODE FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS COURSE_CODE,(SELECT YRLVL FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS YRLVL,(SELECT SECTIONCODE FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS SECT_CODE FROM TBL_STUDENT_INFORMATION WHERE EXISTS (SELECT STUDENT_NUMBER FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND EDUCATION_LEVEL = 'COLLEGE' AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AND LASTNAME LIKE @search", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_STUDENTS.Rows.Clear()
                    While reader.Read
                        DG_STUDENTS.Rows.Add(reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Students()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub frm_browse_students_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Students()
    End Sub

    Private Sub DG_STUDENTS_KeyDown(sender As Object, e As KeyEventArgs) Handles DG_STUDENTS.KeyDown
        If e.KeyCode = Keys.Enter Then
            With frm_college_assessment
                .txtStudentNumber.Text = DG_STUDENTS.Rows(DGRow).Cells(0).Value
                .txtStudentName.Text = DG_STUDENTS.Rows(DGRow).Cells(1).Value
                .txtCourseCode.Text = DG_STUDENTS.Rows(DGRow).Cells(2).Value
                .txtYear.Text = DG_STUDENTS.Rows(DGRow).Cells(3).Value
                .txtSection.Text = DG_STUDENTS.Rows(DGRow).Cells(4).Value
                Me.Close()
            End With
        End If
    End Sub

    Private Sub DG_STUDENTS_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DG_STUDENTS.RowEnter
        DGRow = e.RowIndex
    End Sub
End Class