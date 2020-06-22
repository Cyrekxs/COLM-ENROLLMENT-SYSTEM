Public Class frm_highschool_assessment_browse_student
    Public Form_Request As String = String.Empty
    Public EducationLevel As String = String.Empty
    Dim DGRow As Integer = 0
    Public Sub Load_Students()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' ELSE ' ' + EXTENSION_NAME END +  ', ' + FIRSTNAME + CASE MIDDLENAME WHEN 'N.A' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,(SELECT COURSECODE FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND RegistrationStatus = 'ACTIVE') AS COURSE_CODE,(SELECT YRLVL FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND RegistrationStatus = 'ACTIVE') AS YRLVL,(SELECT SECTIONCODE FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND RegistrationStatus = 'ACTIVE') AS SECT_CODE,(SELECT STUDENT_NUMBER FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND PullOutStatus = 'ACTIVE') AS AssessmentStatus,(SELECT PullOutStatus FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS PullOutStatus FROM TBL_STUDENT_INFORMATION WHERE EXISTS (SELECT STUDENT_NUMBER FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YEAR = @ay AND RegistrationStatus = 'ACTIVE') AND LASTNAME LIKE @search ORDER BY STUDENT_NAME ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                comm.Parameters.AddWithValue("@education_level", EducationLevel)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_STUDENTS.Rows.Clear()
                    Dim SN As String = String.Empty
                    While reader.Read
                        If IsDBNull(reader("AssessmentStatus")) = True Then
                            SN = reader("STUDENT_NUMBER")
                            DG_STUDENTS.Rows.Add(reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"), "NO", reader("PULLOUTSTATUS"))
                        Else
                            SN = reader("STUDENT_NUMBER")
                            DG_STUDENTS.Rows.Add(reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"), "YES", reader("PULLOUTSTATUS"))
                        End If
                    End While
                End Using
            End Using
        End Using

        For Each row As DataGridViewRow In DG_STUDENTS.Rows
            If row.Cells(5).Value = "YES" Then
                row.DefaultCellStyle.BackColor = Color.MediumSeaGreen
            Else
                row.DefaultCellStyle.BackColor = Color.Khaki
            End If

            If IsDBNull(row.Cells(6).Value) = False Then
                If row.Cells(6).Value = "INACTIVE" Then
                    row.DefaultCellStyle.BackColor = Color.Firebrick
                End If
            End If
        Next
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Students()
        End If
    End Sub

    Private Sub DG_STUDENTS_DoubleClick(sender As Object, e As EventArgs) Handles DG_STUDENTS.DoubleClick
        With frm_highschool_assessment
            .txtStudentNumber.Text = DG_STUDENTS.Rows(DGRow).Cells(0).Value
            .txtStudentName.Text = DG_STUDENTS.Rows(DGRow).Cells(1).Value
            .txtCourseCode.Text = DG_STUDENTS.Rows(DGRow).Cells(2).Value
            .txtYear.Text = DG_STUDENTS.Rows(DGRow).Cells(3).Value
            .txtSection.Text = DG_STUDENTS.Rows(DGRow).Cells(4).Value
            If DG_STUDENTS.Rows(DGRow).Cells(5).Value = "NO" Then 'MEANS NOT ASSESSED
                .ClearAssessmentSummary()
                .AssessmentStatus = "ASSESSMENT"
                .LoadDefaultFees()
            Else 'MEANS THE STUDENT HAS IS ASSESSED
                .ClearAssessmentSummary()
                .AssessmentStatus = "RE-ASSESSMENT"
                .LoadAssessedFees()
            End If
            Me.Close()
        End With
    End Sub

    Private Sub DG_STUDENTS_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DG_STUDENTS.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub DG_STUDENTS_KeyDown(sender As Object, e As KeyEventArgs) Handles DG_STUDENTS.KeyDown
        If e.KeyCode = Keys.Enter Then
            With frm_highschool_assessment
                .txtStudentNumber.Text = DG_STUDENTS.Rows(DGRow).Cells(0).Value
                .txtStudentName.Text = DG_STUDENTS.Rows(DGRow).Cells(1).Value
                .txtCourseCode.Text = DG_STUDENTS.Rows(DGRow).Cells(2).Value
                .txtYear.Text = DG_STUDENTS.Rows(DGRow).Cells(3).Value
                .txtSection.Text = DG_STUDENTS.Rows(DGRow).Cells(4).Value
                If DG_STUDENTS.Rows(DGRow).Cells(5).Value = "NO" Then 'MEANS NOT ASSESSED
                    .ClearAssessmentSummary()
                    .AssessmentStatus = "ASSESSMENT"
                    .LoadDefaultFees()
                Else 'MEANS THE STUDENT HAS IS ASSESSED
                    .ClearAssessmentSummary()
                    .AssessmentStatus = "RE-ASSESSMENT"
                    .LoadAssessedFees()
                End If
                Me.Close()
            End With
        End If
    End Sub

    Private Sub frm_highschool_assessment_browse_student_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = String.Empty
        Load_Students()
    End Sub

End Class