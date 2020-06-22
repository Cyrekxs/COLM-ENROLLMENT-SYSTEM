Public Class frm_college_assessment_browse_students
    Public Form_Request As String = String.Empty
    Dim DGRow As Integer = 0
    Public Sub Load_Students()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' ELSE ' ' + EXTENSION_NAME END +  ', ' + FIRSTNAME + CASE MIDDLENAME WHEN 'N.A' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,(SELECT COURSECODE FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS COURSE_CODE,(SELECT YRLVL FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS YRLVL,(SELECT SECTIONCODE FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS SECT_CODE,(SELECT STUDENT_NUMBER FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS AssessmentStatus,(SELECT PullOutStatus FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS PullOutStatus FROM TBL_STUDENT_INFORMATION WHERE EXISTS (SELECT STUDENT_NUMBER FROM TBL_STUDENT_REGISTERED WHERE STUDENT_NUMBER = TBL_STUDENT_INFORMATION.STUDENT_NUMBER AND EDUCATION_LEVEL = 'COLLEGE' AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AND STUDENT_NUMBER + LASTNAME + FIRSTNAME LIKE @search ORDER BY STUDENT_NAME ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_STUDENTS.Rows.Clear()
                    While reader.Read
                        If IsDBNull(reader("AssessmentStatus")) = True Then
                            DG_STUDENTS.Rows.Add(reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"), "NO", reader("PULLOUTSTATUS"))
                        Else
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

    Private Sub frm_browse_students_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = String.Empty
        Load_Students()
    End Sub

    Private Sub DG_STUDENTS_DoubleClick(sender As Object, e As EventArgs) Handles DG_STUDENTS.DoubleClick
        With frm_college_assessment
            .DG_TFEE.Rows.Clear()
            .DG_OFEE.Rows.Clear()
            .DG_MFEE.Rows.Clear()
            .DG_Schedule.Rows.Clear()
            .DG_Summary.Rows.Clear()

            .txtStudentNumber.Text = DG_STUDENTS.Rows(DGRow).Cells(0).Value
            .txtStudentName.Text = DG_STUDENTS.Rows(DGRow).Cells(1).Value
            .txtCourseCode.Text = DG_STUDENTS.Rows(DGRow).Cells(2).Value
            .txtYear.Text = DG_STUDENTS.Rows(DGRow).Cells(3).Value
            .txtSection.Text = DG_STUDENTS.Rows(DGRow).Cells(4).Value
            If DG_STUDENTS.Rows(DGRow).Cells(5).Value = "NO" Then 'MEANS NOT ASSESSED
                .Assessment_Status = "ASSESSMENT"
                If DG_STUDENTS.Rows(DGRow).Cells(4).Value <> "IRREGULAR" Then
                    .ClearAssessmentSummary()
                    .Load_College_Default_Assessment()
                    .Load_College_Default_Fess()
                    .Load_College_Default_Schedule()

                    .LinkAddRegSubj.Visible = False
                    .LinkMFee.Visible = False
                    .LinkOFee.Visible = False
                    .Column5.Visible = False 'CHANGE SUBJECT
                    .Column15.Visible = False 'DROP SUBJECT
                    .DataGridViewLinkColumn1.Visible = False 'MFEE
                    .DataGridViewLinkColumn2.Visible = False ' OFEE
                Else
                    .Load_College_Default_Fess()
                    .LinkAddRegSubj.Visible = True
                    .LinkMFee.Visible = True
                    .LinkOFee.Visible = True
                    .Column5.Visible = True 'CHANGE SUBJECT
                    .Column15.Visible = False 'DROP SUBJECT
                    .DataGridViewLinkColumn1.Visible = True 'MFEE
                    .DataGridViewLinkColumn2.Visible = True ' OFEE
                End If
            Else 'MEANS THE STUDENT HAS ASSESSED
                .Assessment_Status = "RE-ASSESSMENT"
                .Load_Setted_Assessment_Information()
                If DG_STUDENTS.Rows(DGRow).Cells(4).Value <> "IRREGULAR" Then
                    .LinkAddRegSubj.Visible = False
                    .LinkMFee.Visible = False
                    .LinkOFee.Visible = False
                    .Column5.Visible = False 'CHANGE SUBJECT
                    .Column15.Visible = False 'DROP SUBJECT
                    .DataGridViewLinkColumn1.Visible = False 'MFEE
                    .DataGridViewLinkColumn2.Visible = False ' OFEE
                Else
                    .LinkAddRegSubj.Visible = True
                    .LinkMFee.Visible = True
                    .LinkOFee.Visible = True
                    .Column5.Visible = True 'CHANGE SUBJECT
                    .Column15.Visible = True 'DROP SUBJECT
                    .DataGridViewLinkColumn1.Visible = True 'MFEE
                    .DataGridViewLinkColumn2.Visible = True ' OFEE
                End If
            End If
            .View_Fees()
            Me.Close()
        End With
    End Sub

    Private Sub DG_STUDENTS_KeyDown(sender As Object, e As KeyEventArgs) Handles DG_STUDENTS.KeyDown
        If e.KeyCode = Keys.Enter Then
            With frm_college_assessment
                .DG_TFEE.Rows.Clear()
                .DG_OFEE.Rows.Clear()
                .DG_MFEE.Rows.Clear()
                .DG_Schedule.Rows.Clear()
                .DG_Summary.Rows.Clear()

                .txtStudentNumber.Text = DG_STUDENTS.Rows(DGRow).Cells(0).Value
                .txtStudentName.Text = DG_STUDENTS.Rows(DGRow).Cells(1).Value
                .txtCourseCode.Text = DG_STUDENTS.Rows(DGRow).Cells(2).Value
                .txtYear.Text = DG_STUDENTS.Rows(DGRow).Cells(3).Value
                .txtSection.Text = DG_STUDENTS.Rows(DGRow).Cells(4).Value

                If DG_STUDENTS.Rows(DGRow).Cells(5).Value = "NO" Then 'MEANS NOT ASSESSED
                    .Assessment_Status = "ASSESSMENT"
                    If DG_STUDENTS.Rows(DGRow).Cells(4).Value <> "IRREGULAR" Then 'IF THE STUDENT IS NOT IRREGULAR
                        .ClearAssessmentSummary()
                        .Load_College_Default_Assessment()
                        .Load_College_Default_Fess()
                        .Load_College_Default_Schedule()

                        .LinkAddRegSubj.Visible = False
                        .linkRemoveSubject.Visible = True

                        .LinkMFee.Visible = False
                        .LinkOFee.Visible = False
                        .Column5.Visible = False 'CHANGE SUBJECT
                        .Column15.Visible = False 'DROP SUBJECT
                        .DataGridViewLinkColumn1.Visible = False 'MFEE
                        .DataGridViewLinkColumn2.Visible = False ' OFEE
                    Else 'IF THE STUDENT IS IRREGULAR
                        .Load_College_Default_Fess()
                        .LinkAddRegSubj.Visible = True
                        .linkRemoveSubject.Visible = True
                        .LinkMFee.Visible = True
                        .LinkOFee.Visible = True
                        .Column5.Visible = True 'CHANGE SUBJECT
                        .Column15.Visible = False 'DROP SUBJECT
                        .DataGridViewLinkColumn1.Visible = True 'MFEE
                        .DataGridViewLinkColumn2.Visible = True ' OFEE
                    End If
                Else 'MEANS THE STUDENT HAS ASSESSED
                    .Assessment_Status = "RE-ASSESSMENT"
                    .Load_Setted_Assessment_Information()
                    If DG_STUDENTS.Rows(DGRow).Cells(4).Value <> "IRREGULAR" Then
                        .LinkAddRegSubj.Visible = False
                        .linkRemoveSubject.Visible = False
                        .LinkMFee.Visible = False
                        .LinkOFee.Visible = False
                        .Column5.Visible = False 'CHANGE SUBJECT
                        .Column15.Visible = False 'DROP SUBJECT
                        .DataGridViewLinkColumn1.Visible = False 'MFEE
                        .DataGridViewLinkColumn2.Visible = False ' OFEE
                    Else
                        .LinkAddRegSubj.Visible = True
                        .linkRemoveSubject.Visible = True
                        .LinkMFee.Visible = True
                        .LinkOFee.Visible = True
                        .Column5.Visible = True 'CHANGE SUBJECT
                        .Column15.Visible = True 'DROP SUBJECT
                        .DataGridViewLinkColumn1.Visible = True 'MFEE
                        .DataGridViewLinkColumn2.Visible = True ' OFEE
                    End If
                End If
                .View_Fees()
                Me.Close()
            End With
        End If
    End Sub

    Private Sub DG_STUDENTS_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DG_STUDENTS.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub DG_STUDENTS_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_STUDENTS.CellContentClick

    End Sub
End Class