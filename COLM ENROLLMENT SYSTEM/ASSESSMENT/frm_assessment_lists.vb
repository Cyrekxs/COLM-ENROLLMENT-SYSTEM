Public Class frm_assessment_lists

    Private Async Function LoadCollegeStudents() As Threading.Tasks.Task
        dgCollege.Rows.Clear()
        Dim dt As New DataTable
        Using conn As New SqlConnection(StringConnection)
            Await conn.OpenAsync
            Using comm As New SqlCommand("SELECT TOP 1000 * FROM FN_College_AssessedStudents() WHERE Education_Level = 'COLLEGE' AND StudentName LIKE @search AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY StudentName ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@search", "%" & txtCollegeSearch.Text & "%")
                Using reader As SqlDataReader = Await comm.ExecuteReaderAsync
                    Dim i As Integer = 0
                    While Await reader.ReadAsync
                        dgCollege.Rows.Add(reader("AssessmentID"),
                                           reader("Student_Number").ToString,
                                           reader("StudentName").ToString,
                                           reader("Course_Code").ToString,
                                           reader("Yrlvl").ToString,
                                           reader("Sect_Code").ToString,
                                           Convert_To_Currency(reader("TFee")),
                                           Convert_To_Currency(reader("MFee")),
                                           Convert_To_Currency(reader("Total")),
                                           Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"),
                                           reader("Assess_By"))
                    End While
                End Using
            End Using
        End Using
        txtAssessedCollege.Text = dgCollege.Rows.Count
    End Function

    Private Async Function LoadSeniorStudents() As Threading.Tasks.Task
        dgSenior.Rows.Clear()
        Using conn As New SqlConnection(StringConnection)
            Await conn.OpenAsync
            Using comm As New SqlCommand("SELECT * FROM FN_AssessedStudents() WHERE Education_Level = 'SENIOR HIGH' AND StudentName LIKE @search AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY StudentName ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                comm.Parameters.AddWithValue("@search", "%" & txtSeniorSearch.Text & "%")
                Using reader As SqlDataReader = Await comm.ExecuteReaderAsync
                    While Await reader.ReadAsync
                        dgSenior.Rows.Add(reader("ID"),
                                               reader("Student_Number").ToString,
                                               reader("StudentName").ToString,
                                               reader("Course_Code").ToString,
                                               reader("Yrlvl").ToString,
                                               reader("Sect_Code").ToString,
                                               Convert_To_Currency(reader("TFee")),
                                               Convert_To_Currency(reader("MFee")),
                                               Convert_To_Currency(reader("Total")),
                                               Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"),
                                               reader("Assess_By"))
                    End While
                End Using
            End Using
        End Using
        txtAssessedSenior.Text = dgSenior.Rows.Count
    End Function

    Private Async Function LoadJuniorStudents() As Threading.Tasks.Task
        dgJunior.Rows.Clear()
        Using conn As New SqlConnection(StringConnection)
            Await conn.OpenAsync
            Using comm As New SqlCommand("SELECT * FROM FN_AssessedStudents() WHERE Education_Level = 'JUNIOR HIGH' AND StudentName LIKE @search AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY StudentName ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                comm.Parameters.AddWithValue("@search", "%" & txtJuniorSearch.Text & "%")
                Using reader As SqlDataReader = Await comm.ExecuteReaderAsync
                    While Await reader.ReadAsync
                        dgJunior.Rows.Add(reader("ID"),
                                               reader("Student_Number").ToString,
                                               reader("StudentName").ToString,
                                               reader("Course_Code").ToString,
                                               reader("Yrlvl").ToString,
                                               reader("Sect_Code").ToString,
                                               Convert_To_Currency(reader("TFee")),
                                               Convert_To_Currency(reader("MFee")),
                                               Convert_To_Currency(reader("Total")),
                                               Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"),
                                               reader("Assess_By"))
                    End While
                End Using
            End Using
        End Using
        txtAssessedJunior.Text = dgJunior.Rows.Count
    End Function

    Private Async Function LoadElementaryStudents() As Threading.Tasks.Task
        DataGridView3.Rows.Clear()
        Using conn As New SqlConnection(StringConnection)
            Await conn.OpenAsync()
            Using comm As New SqlCommand("SELECT * FROM FN_AssessedStudents() WHERE Education_Level = 'ELEMENTARY' AND StudentName LIKE @search AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY StudentName ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                comm.Parameters.AddWithValue("@search", "%" & txtElemSearch.Text & "%")
                Using reader As SqlDataReader = Await comm.ExecuteReaderAsync
                    While Await reader.ReadAsync
                        DataGridView3.Rows.Add(reader("ID"),
                                               reader("Student_Number").ToString,
                                               reader("StudentName").ToString,
                                               reader("Course_Code").ToString,
                                               reader("Yrlvl").ToString,
                                               reader("Sect_Code").ToString,
                                               Convert_To_Currency(reader("TFee")),
                                               Convert_To_Currency(reader("MFee")),
                                               Convert_To_Currency(reader("Total")),
                                               Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"),
                                               reader("Assess_By"))
                    End While
                End Using
            End Using
        End Using
    End Function

    Private Async Function LoadPreElementaryStudents() As Threading.Tasks.Task
        DataGridView4.Rows.Clear()
        Using conn As New SqlConnection(StringConnection)
            Await conn.OpenAsync
            Using comm As New SqlCommand("SELECT * FROM FN_AssessedStudents() WHERE Education_Level = 'PRE ELEMENTARY' AND StudentName LIKE @search AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY StudentName ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                comm.Parameters.AddWithValue("@search", "%" & txtPreElemSearch.Text & "%")
                Using reader As SqlDataReader = Await comm.ExecuteReaderAsync
                    While Await reader.ReadAsync
                        DataGridView4.Rows.Add(reader("ID"),
                                               reader("Student_Number").ToString,
                                               reader("StudentName").ToString,
                                               reader("Course_Code").ToString,
                                               reader("Yrlvl").ToString,
                                               reader("Sect_Code").ToString,
                                               Convert_To_Currency(reader("TFee")),
                                               Convert_To_Currency(reader("MFee")),
                                               Convert_To_Currency(reader("Total")),
                                               Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"),
                                               reader("Assess_By"))
                    End While
                End Using
            End Using
        End Using
    End Function

    Private Async Sub frm_assessment_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Await LoadCollegeStudents()
            'Await LoadSeniorStudents()
            'Await LoadJuniorStudents()
            'Await LoadElementaryStudents()
            'Await LoadPreElementaryStudents()
        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Account_Position = "INFORMATION OFFICER" Then
            MsgBox("Your account is not allowed to edit/delete/drop the student assessment!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        With frm_highschool_assessment
            .AssessmentStatus = "ASSESSMENT"
            .LoadDefaultFees()
            .EducationLevel = "SENIOR HIGH"
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            Await LoadSeniorStudents()
        End With
    End Sub

    Private Async Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Account_Position = "INFORMATION OFFICER" Then
            MsgBox("Your account is not allowed to edit/delete/drop the student assessment!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        With frm_highschool_assessment
            .AssessmentStatus = "ASSESSMENT"
            .LoadDefaultFees()
            .EducationLevel = "JUNIOR HIGH"
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            Await LoadJuniorStudents()
        End With
    End Sub

    Private Async Sub txtSeniorSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSeniorSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Await LoadSeniorStudents()
        End If
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Await LoadSeniorStudents()
    End Sub

    Private Async Sub txtJuniorSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtJuniorSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Await LoadJuniorStudents()
        End If
    End Sub

    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Await LoadJuniorStudents()
    End Sub

    Private Async Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSenior.CellContentClick
        If e.ColumnIndex = 11 Or e.ColumnIndex = 13 Or e.ColumnIndex = 14 Then
            If Account_Position = "INFORMATION OFFICER" Then
                MsgBox("Your account is not allowed to edit/delete/drop the student assessment!", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        If e.ColumnIndex = 11 Then
            With frm_highschool_assessment
                .EducationLevel = "SENIOR HIGH"
                .AssessmentStatus = "RE-ASSESSMENT"
                .Load_Assessment_Types()
                .Load_Discounts_And_Vouchers()
                .txtStudentNumber.Text = dgSenior.Rows(e.RowIndex).Cells(1).Value
                .txtStudentName.Text = dgSenior.Rows(e.RowIndex).Cells(2).Value
                .txtCourseCode.Text = dgSenior.Rows(e.RowIndex).Cells(3).Value
                .txtYear.Text = dgSenior.Rows(e.RowIndex).Cells(4).Value
                .txtSection.Text = dgSenior.Rows(e.RowIndex).Cells(5).Value
                .ClearAssessmentSummary()
                .LoadAssessedFees()
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                Await LoadSeniorStudents()
            End With
        ElseIf e.ColumnIndex = 13 Then 'DROP STUDENT
            With frm_dropping
                .AssessmentID = dgSenior.Rows(e.RowIndex).Cells(0).Value
                .EducationLevel = frm_dropping.EducationLevelLists.SENIOR
                .txtStudentNo.Text = dgSenior.Rows(e.RowIndex).Cells(1).Value
                .txtStudentName.Text = dgSenior.Rows(e.RowIndex).Cells(2).Value
                .txtCourseCode.Text = dgSenior.Rows(e.RowIndex).Cells(3).Value
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        End If
    End Sub

    Private Async Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgJunior.CellContentClick

        If e.ColumnIndex = 11 Or e.ColumnIndex = 13 Or e.ColumnIndex = 14 Then
            If Account_Position = "INFORMATION OFFICER" Then
                MsgBox("Your account is not allowed to edit/delete/drop the student assessment!", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        If e.ColumnIndex = 11 Then
            With frm_highschool_assessment
                .EducationLevel = "JUNIOR HIGH"
                .AssessmentStatus = "RE-ASSESSMENT"
                .Load_Assessment_Types()
                .Load_Discounts_And_Vouchers()
                .txtStudentNumber.Text = dgJunior.Rows(e.RowIndex).Cells(1).Value
                .txtStudentName.Text = dgJunior.Rows(e.RowIndex).Cells(2).Value
                .txtCourseCode.Text = dgJunior.Rows(e.RowIndex).Cells(3).Value
                .txtYear.Text = dgJunior.Rows(e.RowIndex).Cells(4).Value
                .txtSection.Text = dgJunior.Rows(e.RowIndex).Cells(5).Value
                .ClearAssessmentSummary()
                .LoadAssessedFees()
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                Await LoadJuniorStudents()
            End With
        ElseIf e.ColumnIndex = 13 Then 'DROP STUDENT
            With frm_dropping
                .AssessmentID = dgJunior.Rows(e.RowIndex).Cells(0).Value
                .EducationLevel = frm_dropping.EducationLevelLists.JUNIOR
                .txtStudentNo.Text = dgJunior.Rows(e.RowIndex).Cells(1).Value
                .txtStudentName.Text = dgJunior.Rows(e.RowIndex).Cells(2).Value
                .txtCourseCode.Text = dgJunior.Rows(e.RowIndex).Cells(3).Value
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        End If
    End Sub

    Private Async Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        With frm_elementary_assessment
            .AssessmentStatus = "ASSESSMENT"
            .LoadDefaultFees()
            .EducationLevel = "ELEMENTARY"
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            Await LoadElementaryStudents()
        End With
    End Sub

    Private Async Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtElemSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Await LoadElementaryStudents()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtElemSearch.TextChanged

    End Sub

    Private Async Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        With frm_pre_elementary_assessment
            .AssessmentStatus = "ASSESSMENT"
            .LoadDefaultFees()
            .EducationLevel = "PRE ELEMENTARY"
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            Await LoadPreElementaryStudents()
        End With
    End Sub

    Private Async Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick
        If e.ColumnIndex = 11 Then
            With frm_elementary_assessment
                .EducationLevel = "ELEMENTARY"
                .AssessmentStatus = "RE-ASSESSMENT"
                .Load_Assessment_Types()
                .Load_Discounts_And_Vouchers()
                .txtStudentNumber.Text = DataGridView3.Rows(e.RowIndex).Cells(1).Value
                .txtStudentName.Text = DataGridView3.Rows(e.RowIndex).Cells(2).Value
                .txtCourseCode.Text = DataGridView3.Rows(e.RowIndex).Cells(3).Value
                .txtYear.Text = DataGridView3.Rows(e.RowIndex).Cells(4).Value
                .txtSection.Text = DataGridView3.Rows(e.RowIndex).Cells(5).Value
                .ClearAssessmentSummary()
                .LoadAssessedFees()
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                Await LoadElementaryStudents()
            End With
        End If
    End Sub

    Private Async Sub DataGridView4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellContentClick
        If e.ColumnIndex = 11 Then
            With frm_pre_elementary_assessment
                .EducationLevel = "PRE ELEMENTARY"
                .AssessmentStatus = "RE-ASSESSMENT"
                .Load_Assessment_Types()
                .Load_Discounts_And_Vouchers()
                .txtStudentNumber.Text = DataGridView4.Rows(e.RowIndex).Cells(1).Value
                .txtStudentName.Text = DataGridView4.Rows(e.RowIndex).Cells(2).Value
                .txtCourseCode.Text = DataGridView4.Rows(e.RowIndex).Cells(3).Value
                .txtYear.Text = DataGridView4.Rows(e.RowIndex).Cells(4).Value
                .txtSection.Text = DataGridView4.Rows(e.RowIndex).Cells(5).Value
                .ClearAssessmentSummary()
                .LoadAssessedFees()
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                Await LoadPreElementaryStudents()
            End With
        End If
    End Sub

    Private Async Sub txtPreElemSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPreElemSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Await LoadPreElementaryStudents()
        End If
    End Sub

    Private Sub txtPreElemSearch_TextChanged(sender As Object, e As EventArgs) Handles txtPreElemSearch.TextChanged

    End Sub

    Private Sub txtSeniorSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSeniorSearch.TextChanged

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Account_Position = "INFORMATION OFFICER" Then
            MsgBox("Your account is not allowed to assess a student you can only view their assessment information!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Academic_Year = "2018-2019" Or Academic_Year = "2019-2020" Then
            With frm_college_assessment_entry
                .btnBrowse.Visible = True
                .AssessmentStatus = frm_college_assessment_entry.AssessmentOptions.ASSESSMENT
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        Else
            With frm_college_assessment
                .Assessment_Status = "ASSESSMENT"
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        End If
    End Sub

    Private Async Sub txtCollegeSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCollegeSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Await LoadCollegeStudents()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Async Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            Await LoadCollegeStudents()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dgCollege_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCollege.CellContentClick
        If e.ColumnIndex = 11 Or e.ColumnIndex = 13 Or e.ColumnIndex = 14 Then
            If Account_Position = "INFORMATION OFFICER" Then
                MsgBox("Your account is not allowed to edit/delete/drop the student assessment!", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If


        If e.ColumnIndex = 11 Then
            If Academic_Year = "2018-2019" Or Academic_Year = "2019-2020" Then
                With frm_college_assessment_entry
                    .btnBrowse.Visible = False
                    .AssessmentStatus = frm_college_assessment_entry.AssessmentOptions.REASSESSMENT
                    .AssessmentID = dgCollege.Rows(e.RowIndex).Cells(0).Value
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog()
                End With
            Else
                With frm_college_assessment
                    .DG_TFEE.Rows.Clear()
                    .DG_OFEE.Rows.Clear()
                    .DG_MFEE.Rows.Clear()
                    .DG_Schedule.Rows.Clear()
                    .DG_Summary.Rows.Clear()

                    .txtStudentNumber.Text = dgCollege.Rows(e.RowIndex).Cells(1).Value
                    .txtStudentName.Text = dgCollege.Rows(e.RowIndex).Cells(2).Value
                    .txtCourseCode.Text = dgCollege.Rows(e.RowIndex).Cells(3).Value
                    .txtYear.Text = dgCollege.Rows(e.RowIndex).Cells(4).Value
                    .txtSection.Text = dgCollege.Rows(e.RowIndex).Cells(5).Value

                    .Assessment_Status = "RE-ASSESSMENT"
                    .Load_Assessment_Types()
                    .Load_Discounts_And_Vouchers()
                    .Load_Setted_Assessment_Information()
                    If dgCollege.Rows(e.RowIndex).Cells(5).Value <> "IRREGULAR" Then
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

                    .StartPosition = FormStartPosition.CenterParent
                    .View_Fees()
                    .ShowDialog()
                    Me.Close()
                End With
            End If

        ElseIf e.ColumnIndex = 13 Then 'DROP STUDENT

            With frm_dropping
                .AssessmentID = dgCollege.Rows(e.RowIndex).Cells(0).Value
                .EducationLevel = frm_dropping.EducationLevelLists.COLLEGE
                .txtStudentNo.Text = dgCollege.Rows(e.RowIndex).Cells(1).Value
                .txtStudentName.Text = dgCollege.Rows(e.RowIndex).Cells(2).Value
                .txtCourseCode.Text = dgCollege.Rows(e.RowIndex).Cells(3).Value
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With

        ElseIf e.ColumnIndex = 14 Then ' PRINT COR
            Dim param_name As ReportParameter = New ReportParameter("StudentName", dgCollege.Rows(e.RowIndex).Cells(2).Value.ToString)
            Dim aysem As ReportParameter = New ReportParameter("aysem", Academic_Year & " " & Academic_Sem)
            Dim ay As ReportParameter = New ReportParameter("ay", Academic_Year)
            Dim sem As ReportParameter = New ReportParameter("sem", Academic_Sem)
            Dim sn As ReportParameter = New ReportParameter("sn", dgCollege.Rows(e.RowIndex).Cells(1).Value.ToString)
            Dim course_code As ReportParameter = Nothing
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_courses WHERE Course_Code = @Course_Code", conn)
                    comm.Parameters.AddWithValue("@Course_Code", dgCollege.Rows(e.RowIndex).Cells(3).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            course_code = New ReportParameter("course_code", (reader("Course_Desc") & "(" & reader("Course_Code") & ")").ToString)
                        End While
                    End Using
                End Using
            End Using
            Dim yrlvl As ReportParameter = New ReportParameter("yrlvl", dgCollege.Rows(e.RowIndex).Cells(4).Value.ToString)
            Dim requestname As String = InputBox("Enter the name of a person who request this form!", "Enter name")
            Dim request_name As ReportParameter = New ReportParameter("requestname", requestname)
            Dim datenow As ReportParameter = New ReportParameter("datenow", Date.Now)

            With frm_rdlc_report_viewer
                .ReportViewer1.LocalReport.DataSources.Clear()
                .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_COR.rdlc"
                .ReportViewer1.LocalReport.SetParameters({param_name, aysem, ay, sem, sn, course_code, yrlvl, request_name, datenow})
                .ReportViewer1.RefreshReport()
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab.Name = tbCollege.Name Then
            Await LoadCollegeStudents()
        ElseIf TabControl1.SelectedTab.Name = tbSenior.Name Then
            Await LoadSeniorStudents()
        ElseIf TabControl1.SelectedTab.Name = tbJunior.Name Then
            Await LoadJuniorStudents()
        End If
    End Sub
End Class