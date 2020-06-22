Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Public Class student_grade_report
    Dim StringConnection_ProjectCOLM As String = "Server=COLM\SQLEXPRESS;Database=PROJECT_COLM;User Id=sa;Password=sa;"
    Private Function GetListofGrade(ConnString As String) As List(Of C_StudentSubjectGrades)
        Dim Result As New List(Of C_StudentSubjectGrades)

        Select Case cmbAcademicYear.Text
            Case "2015-2016", "2016-2017", "2017-2018"

                Using conn As New SqlConnection(ConnString)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_SUBJECT_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                        comm.Parameters.AddWithValue("@sn", txtSN.Text)
                        comm.Parameters.AddWithValue("@ay", cmbAcademicYear.Text)
                        comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            DataGridView1.Rows.Clear()
                            While reader.Read
                                If reader("GRADE_EQUIVALENT") <> "WP" Then
                                    Dim StudentSubjectGrade As New C_StudentSubjectGrades
                                    With StudentSubjectGrade
                                        .SubjID = reader("ID")
                                        .SubjCode = reader("Subj_Code")
                                        .SubjDesc = reader("Subj_Desc")
                                        .SubjUnit = reader("Subj_Unit")
                                        .SubjGrade = reader("Grade_Equivalent")
                                    End With
                                    Result.Add(StudentSubjectGrade)
                                End If
                            End While
                        End Using
                    End Using
                End Using

            Case Nothing

            Case Else

                Dim AssessmentID As Integer = 0
                Using conn As New SqlConnection(ConnString)
                    conn.Open()
                    'GET THE ASSESSMENT ID
                    Using comm As New SqlCommand("SELECT * FROM dbo.FN_College_AssessedStudents() WHERE Education_Level = 'COLLEGE' AND Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                        comm.Parameters.AddWithValue("@sn", txtSN.Text)
                        comm.Parameters.AddWithValue("@ay", cmbAcademicYear.Text)
                        comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            While reader.Read
                                AssessmentID = reader("AssessmentID")
                            End While
                        End Using
                    End Using

                    'LOAD SUBJECTS THROUGH ASSESSMENT ID
                    Using comm As New SqlCommand("SELECT * FROM tbl_college_subject_loads WHERE AssessmentID = @AssessmentID", conn)
                        comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            While reader.Read
                                If reader("GRADE_EQUIVALENT") <> "WP" Then
                                    Dim StudentSubjectGrade As New C_StudentSubjectGrades
                                    With StudentSubjectGrade
                                        .SubjID = reader("ID")
                                        .SubjCode = reader("Subj_Code")
                                        .SubjDesc = reader("Subj_Desc")
                                        .SubjUnit = reader("Subj_Unit")
                                        .SubjGrade = reader("Grade_Equivalent")
                                    End With
                                    Result.Add(StudentSubjectGrade)
                                End If
                            End While
                        End Using
                    End Using
                End Using
        End Select

        Return Result
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        student_grade_report_browse_student.StartPosition = FormStartPosition.CenterParent
        student_grade_report_browse_student.ShowDialog()
    End Sub

    Private Sub CheckBalance()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsWithBalance(@educlevel,@ay,@sem) WHERE Student_Number = @sn", conn)
                comm.Parameters.AddWithValue("@educlevel", "COLLEGE")
                comm.Parameters.AddWithValue("@ay", cmbAcademicYear.Text)
                comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
                comm.Parameters.AddWithValue("@sn", txtSN.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        Dim RemBal As Double = reader("REMAINING_BALANCE")
                        For i = 0 To DataGridView1.Rows.Count - 1
                            If RemBal > 2 Then
                                DataGridView1.Rows(i).Cells(5).Value = "THIS WILL BE PRINTED INC BECAUSE HAS A BALANCE OF: " & Convert_To_Currency(RemBal)
                            End If
                        Next
                    End While
                End Using
            End Using
        End Using
        'For I = 0 To DataGridView1.Rows.Count - 1
        '    DataGridView1.Rows(I).Cells(5).Value = ""
        'Next
    End Sub

    Private Sub LoadStudentGrade()
        Dim ListofStudentGrade As New List(Of C_StudentSubjectGrades)
        Select Case cmbAcademicYear.Text
            Case "2015-2016"
                Select Case cmbSemester.Text
                    Case "SUMMER"
                        ListofStudentGrade = GetListofGrade(StringConnection_ProjectCOLM)
                    Case Else
                        ListofStudentGrade = GetListofGrade(StringConnection)
                End Select
            Case Else
                ListofStudentGrade = GetListofGrade(StringConnection)
        End Select

        DataGridView1.Rows.Clear()
        For i = 0 To ListofStudentGrade.Count - 1
            DataGridView1.Rows.Add(ListofStudentGrade(i).SubjID,
                                   ListofStudentGrade(i).SubjCode,
                                   ListofStudentGrade(i).SubjDesc,
                                   ListofStudentGrade(i).SubjUnit,
                                   ListofStudentGrade(i).SubjGrade, "")
        Next


        'If cmbAcademicYear.Text = "2015-2016" Then
        '    If cmbSemester.Text = "SUMMER" Then
        '        Using conn As New SqlConnection(StringConnection)
        '            conn.Open()
        '            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_SUBJECT_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
        '                comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '                comm.Parameters.AddWithValue("@ay", "2015-2016")
        '                comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '                Using reader As SqlDataReader = comm.ExecuteReader
        '                    DataGridView1.Rows.Clear()
        '                    While reader.Read
        '                        If reader("GRADE_EQUIVALENT") <> "WP" Then
        '                            DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("GRADE_EQUIVALENT"), "")
        '                        End If
        '                    End While
        '                End Using
        '            End Using
        '        End Using
        '    Else
        '        Using conn As New SqlConnection(StringConnection_ProjectCOLM)
        '            conn.Open()
        '            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_SUBJECT_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
        '                comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '                comm.Parameters.AddWithValue("@ay", "2015-2016")
        '                comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '                Using reader As SqlDataReader = comm.ExecuteReader
        '                    DataGridView1.Rows.Clear()
        '                    While reader.Read
        '                        If reader("GRADE_EQUIVALENT") <> "WP" Then
        '                            DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("GRADE_EQUIVALENT"), "")
        '                        End If
        '                    End While
        '                End Using
        '            End Using
        '        End Using
        '    End If
        'ElseIf cmbAcademicYear.Text = "2016-2017" Then
        '    If cmbSemester.Text = "SUMMER" Then
        '        Using conn As New SqlConnection(StringConnection)
        '            conn.Open()
        '            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_SUBJECT_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
        '                comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '                comm.Parameters.AddWithValue("@ay", "2016-2017")
        '                comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '                Using reader As SqlDataReader = comm.ExecuteReader
        '                    DataGridView1.Rows.Clear()
        '                    While reader.Read
        '                        If reader("GRADE_EQUIVALENT") <> "WF" Then
        '                            DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("GRADE_EQUIVALENT"), "")
        '                        End If
        '                    End While
        '                End Using
        '            End Using
        '        End Using
        '    Else
        '        Using conn As New SqlConnection(StringConnection)
        '            conn.Open()
        '            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_SUBJECT_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
        '                comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '                comm.Parameters.AddWithValue("@ay", "2016-2017")
        '                comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '                Using reader As SqlDataReader = comm.ExecuteReader
        '                    DataGridView1.Rows.Clear()
        '                    While reader.Read
        '                        If reader("GRADE_EQUIVALENT") <> "WF" Then
        '                            DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("GRADE_EQUIVALENT"), "")
        '                        End If
        '                    End While
        '                End Using
        '            End Using
        '        End Using
        '    End If
        'End If

        'If cmbAcademicYear.Text <> "2015-2016" And cmbSemester.Text <> "SUMMER" Then

        '    Using conn As New SqlConnection(StringConnection)
        '        conn.Open()
        '        Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_SUBJECT_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
        '            comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '            comm.Parameters.AddWithValue("@ay", cmbAcademicYear.Text)
        '            comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '            Using reader As SqlDataReader = comm.ExecuteReader
        '                DataGridView1.Rows.Clear()
        '                While reader.Read
        '                    If reader("GRADE_EQUIVALENT") <> "WF" Then
        '                        DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("GRADE_EQUIVALENT"), "")
        '                    End If
        '                End While
        '            End Using
        '        End Using
        '    End Using
        'ElseIf cmbAcademicYear.Text <> "2015-2016" And cmbSemester.Text = "SUMMER" Then
        '    Using conn As New SqlConnection(StringConnection)
        '        conn.Open()
        '        Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_SUBJECT_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
        '            comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '            comm.Parameters.AddWithValue("@ay", cmbAcademicYear.Text)
        '            comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '            Using reader As SqlDataReader = comm.ExecuteReader
        '                DataGridView1.Rows.Clear()
        '                While reader.Read
        '                    If reader("GRADE_EQUIVALENT") <> "WF" Then
        '                        DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("GRADE_EQUIVALENT"), "")
        '                    End If
        '                End While
        '            End Using
        '        End Using
        '    End Using
        'End If
        'CHECK THE BALANCE OF STUDENT
        CheckBalance()
    End Sub

    Public Sub LoadAssessmentInfo()

        Dim comm_param_sn As String = String.Empty
        Dim comm_param_ay As String = String.Empty
        Dim comm_param_sem As String = String.Empty
        Dim comm_param_conn As String = String.Empty
        Dim comm_param_query As String = "SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND Academic_Yr = @ay AND ACADEMIC_SEM = @sem"
        Select Case cmbAcademicYear.Text
            Case "2015-2016"
                Select Case cmbSemester.Text
                    Case "SUMMER"
                        comm_param_sn = txtSN.Text
                        comm_param_ay = cmbAcademicYear.Text
                        comm_param_sem = cmbSemester.Text
                        comm_param_conn = StringConnection
                    Case Else
                        comm_param_sn = txtSN.Text
                        comm_param_ay = cmbAcademicYear.Text
                        comm_param_sem = cmbSemester.Text
                        comm_param_conn = StringConnection_ProjectCOLM
                End Select
            Case "2016-2017", "2017-2018"
                comm_param_sn = txtSN.Text
                comm_param_ay = cmbAcademicYear.Text
                comm_param_sem = cmbSemester.Text
                comm_param_conn = StringConnection
            Case Else
                comm_param_sn = txtSN.Text
                comm_param_ay = cmbAcademicYear.Text
                comm_param_sem = cmbSemester.Text
                comm_param_conn = StringConnection
                comm_param_query = "SELECT * FROM dbo.FN_College_AssessedStudents() WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem"
        End Select

        Using conn As New SqlConnection(comm_param_conn)
            conn.Open()
            Using comm As New SqlCommand(comm_param_query, conn)
                comm.Parameters.AddWithValue("@sn", comm_param_sn)
                comm.Parameters.AddWithValue("@ay", comm_param_ay)
                comm.Parameters.AddWithValue("@sem", comm_param_sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtCourse.Text = reader("COURSE_CODE")
                        txtYear.Text = reader("YRLVL")
                        txtSection.Text = reader("SECT_CODE")
                    End While
                End Using
            End Using
        End Using

        'If cmbAcademicYear.Text = "2015-2016" Then
        '    If cmbSemester.Text = "SUMMER" Then
        '        Using conn As New SqlConnection(StringConnection)
        '            conn.Open()
        '            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND Academic_Yr = '2015-2016' AND ACADEMIC_SEM = @sem", conn)
        '                comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '                comm.Parameters.AddWithValue("@ay", Academic_Year)
        '                comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '                Using reader As SqlDataReader = comm.ExecuteReader
        '                    While reader.Read
        '                        txtCourse.Text = reader("COURSE_CODE")
        '                        txtYear.Text = reader("YRLVL")
        '                        txtSection.Text = reader("SECT_CODE")
        '                    End While
        '                End Using
        '            End Using
        '        End Using
        '    Else
        '        Using conn As New SqlConnection(StringConnection_ProjectCOLM)
        '            conn.Open()
        '            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND Academic_Yr = '2015-2016' AND ACADEMIC_SEM = @sem", conn)
        '                comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '                comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '                Using reader As SqlDataReader = comm.ExecuteReader
        '                    While reader.Read
        '                        txtCourse.Text = reader("COURSE_CODE")
        '                        txtYear.Text = reader("YRLVL")
        '                        txtSection.Text = reader("SECT_CODE")
        '                    End While
        '                End Using
        '            End Using
        '        End Using
        '    End If
        'ElseIf cmbAcademicYear.Text = "2016-2017" Then
        '    If cmbSemester.Text = "SUMMER" Then
        '        Using conn As New SqlConnection(StringConnection)
        '            conn.Open()
        '            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND Academic_Yr = '2016-2017' AND ACADEMIC_SEM = @sem", conn)
        '                comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '                comm.Parameters.AddWithValue("@ay", Academic_Year)
        '                comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '                Using reader As SqlDataReader = comm.ExecuteReader
        '                    While reader.Read
        '                        txtCourse.Text = reader("COURSE_CODE")
        '                        txtYear.Text = reader("YRLVL")
        '                        txtSection.Text = reader("SECT_CODE")
        '                    End While
        '                End Using
        '            End Using
        '        End Using

        '    Else
        '        Using conn As New SqlConnection(StringConnection)
        '            conn.Open()
        '            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND Academic_Yr = '2016-2017' AND ACADEMIC_SEM = @sem", conn)
        '                comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '                comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '                Using reader As SqlDataReader = comm.ExecuteReader
        '                    While reader.Read
        '                        txtCourse.Text = reader("COURSE_CODE")
        '                        txtYear.Text = reader("YRLVL")
        '                        txtSection.Text = reader("SECT_CODE")
        '                    End While
        '                End Using
        '            End Using
        '        End Using
        '    End If
        'Else
        '    Using conn As New SqlConnection(StringConnection)
        '        conn.Open()
        '        Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND Academic_Yr = @ay AND ACADEMIC_SEM = @sem", conn)
        '            comm.Parameters.AddWithValue("@sn", txtSN.Text)
        '            comm.Parameters.AddWithValue("@ay", cmbAcademicYear.Text)
        '            comm.Parameters.AddWithValue("@sem", cmbSemester.Text)
        '            Using reader As SqlDataReader = comm.ExecuteReader
        '                While reader.Read
        '                    txtCourse.Text = reader("COURSE_CODE")
        '                    txtYear.Text = reader("YRLVL")
        '                    txtSection.Text = reader("SECT_CODE")
        '                End While
        '            End Using
        '        End Using
        '    End Using
        'End If
    End Sub

    Private Sub txtSN_TextChanged(sender As Object, e As EventArgs) Handles txtSN.TextChanged
        LoadStudentGrade()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LoadAssessmentInfo()
        If cmbAcademicYear.Text = "2015-2016" Then
            Using conn As New SqlConnection(StringConnection_ProjectCOLM)
                conn.Open()
                For i = 0 To DataGridView1.Rows.Count - 1
                    Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET GRADE_EQUIVALENT = @equivalent WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", DataGridView1.Rows(i).Cells(0).Value)
                        comm.Parameters.AddWithValue("@equivalent", DataGridView1.Rows(i).Cells(4).Value)
                        comm.ExecuteNonQuery()
                    End Using
                Next
            End Using
        Else
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                For i = 0 To DataGridView1.Rows.Count - 1
                    Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET GRADE_EQUIVALENT = @equivalent WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", DataGridView1.Rows(i).Cells(0).Value)
                        comm.Parameters.AddWithValue("@equivalent", DataGridView1.Rows(i).Cells(4).Value)
                        comm.ExecuteNonQuery()
                    End Using
                Next
            End Using
        End If


        Dim DS As New DS_SGR
        Dim DR As DataRow
        Dim TotalUnits As Integer = 0
        Dim param_sn As ReportParameter = New ReportParameter("sn", txtSN.Text)
        Dim param_name As ReportParameter = New ReportParameter("name", txtStudName.Text)
        Dim param_cys As ReportParameter = New ReportParameter("course_year_sect", txtCourse.Text & " " & txtYear.Text & " " & txtSection.Text)
        Dim param_aysem As ReportParameter = New ReportParameter("aysem", "GRADING SYSTEM REPORT " & vbNewLine & cmbAcademicYear.Text & " " & cmbSemester.Text)
        With DS.Tables("2_Subject_Grades")
            .Rows.Clear()
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("Student_Number") = txtStudName.Text
                DR("Subj_Code") = DataGridView1.Rows(i).Cells(1).Value
                DR("Subj_Desc") = DataGridView1.Rows(i).Cells(2).Value
                DR("Subj_Unit") = DataGridView1.Rows(i).Cells(3).Value
                If DataGridView1.Rows(i).Cells(5).Value.ToString = String.Empty Then
                    DR("Subj_Grade") = DataGridView1.Rows(i).Cells(4).Value
                Else
                    'DR("Subj_Grade") = DataGridView1.Rows(i).Cells(4).Value
                    DR("Subj_Grade") = "INC" '- CODE TO MAKE THE GRADE AS "INC" IF HAVE BALANCE
                End If
                TotalUnits += CInt(DataGridView1.Rows(i).Cells(3).Value)
                .Rows.Add(DR)
            Next
        End With

        Dim MyReport As New ReportDataSource("DSSGR", DS.Tables("2_Subject_Grades"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.student_grade_report.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_sn, param_name, param_cys, param_aysem})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With

    End Sub

    Private Sub cmbSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSemester.SelectedIndexChanged
        LoadAssessmentInfo()
        LoadStudentGrade()
    End Sub

    Private Sub cmbAcademicYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAcademicYear.SelectedIndexChanged
        LoadAssessmentInfo()
        LoadStudentGrade()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If cmbAcademicYear.Text = "2015-2016" Then
            Using conn As New SqlConnection(StringConnection_ProjectCOLM)
                conn.Open()
                For i = 0 To DataGridView1.Rows.Count - 1
                    Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET GRADE_EQUIVALENT = @equivalent WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", DataGridView1.Rows(i).Cells(0).Value)
                        comm.Parameters.AddWithValue("@equivalent", DataGridView1.Rows(i).Cells(4).Value)
                        comm.ExecuteNonQuery()
                    End Using
                Next
            End Using
            MsgBox("Grade has been successfully updated!", MsgBoxStyle.Information)
        Else
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                For i = 0 To DataGridView1.Rows.Count - 1
                    Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET GRADE_EQUIVALENT = @equivalent WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", DataGridView1.Rows(i).Cells(0).Value)
                        comm.Parameters.AddWithValue("@equivalent", DataGridView1.Rows(i).Cells(4).Value)
                        comm.ExecuteNonQuery()
                    End Using
                Next
            End Using
            MsgBox("Grade has been successfully updated!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        LoadAssessmentInfo()
        Dim DS As New DS_SGR
        Dim DR As DataRow
        Dim TotalUnits As Integer = 0
        Dim param_sn As ReportParameter = New ReportParameter("sn", txtSN.Text)
        Dim param_name As ReportParameter = New ReportParameter("name", txtStudName.Text)
        Dim param_cys As ReportParameter = New ReportParameter("course_year_sect", txtCourse.Text & " " & txtYear.Text & " " & txtSection.Text)
        Dim param_aysem As ReportParameter = New ReportParameter("aysem", "GRADING SYSTEM REPORT " & vbNewLine & cmbAcademicYear.Text & " " & cmbSemester.Text)
        With DS.Tables("2_Subject_Grades")
            .Rows.Clear()
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("Student_Number") = txtStudName.Text
                DR("Subj_Code") = DataGridView1.Rows(i).Cells(1).Value
                DR("Subj_Desc") = DataGridView1.Rows(i).Cells(2).Value
                DR("Subj_Unit") = DataGridView1.Rows(i).Cells(3).Value
                If DataGridView1.Rows(i).Cells(5).Value.ToString = String.Empty Then
                    DR("Subj_Grade") = DataGridView1.Rows(i).Cells(4).Value
                Else
                    DR("Subj_Grade") = "INC" '- CODE TO MAKE THE GRADE AS "INC" IF HAVE BALANCE
                End If
                TotalUnits += CInt(DataGridView1.Rows(i).Cells(3).Value)
                .Rows.Add(DR)
            Next
        End With

        Dim MyReport As New ReportDataSource("DSSGR", DS.Tables("2_Subject_Grades"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.student_grade_report.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_sn, param_name, param_cys, param_aysem})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadAssessmentInfo()
        Dim DS As New DS_SGR
        Dim DR As DataRow
        Dim TotalUnits As Integer = 0
        Dim param_sn As ReportParameter = New ReportParameter("sn", txtSN.Text)
        Dim param_name As ReportParameter = New ReportParameter("name", txtStudName.Text)
        Dim param_cys As ReportParameter = New ReportParameter("course_year_sect", txtCourse.Text & " " & txtYear.Text & " " & txtSection.Text)
        Dim param_aysem As ReportParameter = New ReportParameter("aysem", "GRADING SYSTEM REPORT " & vbNewLine & cmbAcademicYear.Text & " " & cmbSemester.Text)
        With DS.Tables("2_Subject_Grades")
            .Rows.Clear()
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("Student_Number") = txtStudName.Text
                DR("Subj_Code") = DataGridView1.Rows(i).Cells(1).Value
                DR("Subj_Desc") = DataGridView1.Rows(i).Cells(2).Value
                DR("Subj_Unit") = DataGridView1.Rows(i).Cells(3).Value
                DR("Subj_Grade") = DataGridView1.Rows(i).Cells(4).Value
                TotalUnits += CInt(DataGridView1.Rows(i).Cells(3).Value)
                .Rows.Add(DR)
            Next
        End With

        Dim MyReport As New ReportDataSource("DSSGR", DS.Tables("2_Subject_Grades"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.student_grade_report.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_sn, param_name, param_cys, param_aysem})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class

Public Class C_StudentSubjectGrades
    Private _SubjectID As Integer
    Public Property SubjID() As Integer
        Get
            Return _SubjectID
        End Get
        Set(ByVal value As Integer)
            _SubjectID = value
        End Set
    End Property

    Private _SubjectCode As String
    Public Property SubjCode() As String
        Get
            Return _SubjectCode
        End Get
        Set(ByVal value As String)
            _SubjectCode = value
        End Set
    End Property

    Private _SubjectDesc As String
    Public Property SubjDesc() As String
        Get
            Return _SubjectDesc
        End Get
        Set(ByVal value As String)
            _SubjectDesc = value
        End Set
    End Property

    Private _SubjUnit As String
    Public Property SubjUnit() As String
        Get
            Return _SubjUnit
        End Get
        Set(ByVal value As String)
            _SubjUnit = value
        End Set
    End Property

    Private _SubjectGrade As String
    Public Property SubjGrade() As String
        Get
            Return _SubjectGrade
        End Get
        Set(ByVal value As String)
            _SubjectGrade = value
        End Set
    End Property
End Class
