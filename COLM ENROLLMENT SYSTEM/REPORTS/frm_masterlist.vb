Imports Microsoft.Reporting.WinForms
Public Class frm_masterlist
    Dim SubjIDList As New List(Of Integer)

    Public Sub LoadFaculties()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT Sched_Faculty FROM FN_College_MasterLists(@ay,@sem) ORDER BY Sched_Faculty ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbFacultyName.Items.Clear()
                    cmbFacultyName.Items.Add("-")
                    While reader.Read
                        cmbFacultyName.Items.Add(reader("Sched_Faculty"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT SUBJ_CODE FROM FN_College_MasterLists(@ay,@sem) WHERE SCHED_FACULTY = @faculty_name", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@faculty_name", cmbFacultyName.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    SubjIDList.Clear()
                    cmbSubject.Items.Clear()
                    While reader.Read
                        cmbSubject.Items.Add(reader("SUBJ_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadDay()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT Sched_Day FROM FN_College_MasterLists(@ay,@sem) WHERE SCHED_FACULTY = @faculty_name AND SUBJ_CODE = @subj_code", conn)
                comm.Parameters.AddWithValue("@faculty_name", cmbFacultyName.Text)
                comm.Parameters.AddWithValue("@subj_code", cmbSubject.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbDay.Items.Clear()
                    While reader.Read
                        cmbDay.Items.Add(reader("SCHED_DAY"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadTimeIn()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT Sched_Time_In FROM FN_College_MasterLists(@ay,@sem) WHERE SCHED_FACULTY = @faculty_name AND SUBJ_CODE = @subj_code AND SCHED_DAY = @day", conn)
                comm.Parameters.AddWithValue("@faculty_name", cmbFacultyName.Text)
                comm.Parameters.AddWithValue("@subj_code", cmbSubject.Text)
                comm.Parameters.AddWithValue("@day", cmbDay.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbStartTime.Items.Clear()
                    While reader.Read
                        cmbStartTime.Items.Add(reader("SCHED_TIME_IN"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadTimeOut()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT Sched_Time_Out FROM FN_College_MasterLists(@ay,@sem) WHERE SCHED_FACULTY = @faculty_name AND SCHED_DAY = @day AND SCHED_TIME_IN = @time_in", conn)
                comm.Parameters.AddWithValue("@faculty_name", cmbFacultyName.Text)
                comm.Parameters.AddWithValue("@day", cmbDay.Text)
                comm.Parameters.AddWithValue("@time_in", cmbStartTime.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbEndTime.Items.Clear()
                    While reader.Read
                        cmbEndTime.Items.Add(reader("SCHED_TIME_OUT"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_masterlist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFaculties()
    End Sub

    Private Sub cmbFacultyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFacultyName.SelectedIndexChanged
        LoadSubjects()
        'If cmbSubject.Items.Count = 1 Then
        '    cmbSubject.SelectedIndex = 0
        'End If
    End Sub

    Private Sub cmbSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSubject.SelectedIndexChanged
        LoadDay()
        'If cmbDay.Items.Count = 1 Then
        '    cmbDay.SelectedIndex = 0
        'End If
    End Sub

    Private Sub cmbDay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDay.SelectedIndexChanged
        LoadTimeIn()
        'If cmbStartTime.Items.Count = 1 Then
        '    cmbStartTime.SelectedIndex = 0
        'End If
    End Sub

    Private Sub cmbStartTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStartTime.SelectedIndexChanged
        LoadTimeOut()
        'If cmbEndTime.Items.Count = 1 Then
        '    cmbEndTime.SelectedIndex = 0
        'End If
    End Sub

    Private Sub cmbEndTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEndTime.SelectedIndexChanged
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_MasterLists(@ay,@sem) WHERE Sched_Faculty = @faculty AND Subj_Code = @Subj_Code AND Sched_Day = @day AND Sched_Time_In = @TimeIn AND Sched_Time_Out = @TimeOut", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@faculty", cmbFacultyName.Text)
                comm.Parameters.AddWithValue("@subj_code", cmbSubject.Text)
                comm.Parameters.AddWithValue("@day", cmbDay.Text)
                comm.Parameters.AddWithValue("@timein", cmbStartTime.Text)
                comm.Parameters.AddWithValue("@timeout", cmbEndTime.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        txtSchedRoom.Text = reader("SCHED_ROOM")
                        If IsDBNull(reader("Enrollment_Status")) = False Then
                            DataGridView1.Rows.Add(reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECTION_CODE"), "")
                        Else
                            DataGridView1.Rows.Add(reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECTION_CODE"), "UNOFFICIAL")
                        End If
                    End While
                End Using
            End Using
        End Using
        txtStudents.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim DS As New MasterLIstsDataSet
        Dim DR As DataRow

        Dim AcademicYearSemester As ReportParameter = New ReportParameter("AcademicYearSemester", Academic_Sem & " " & Academic_Year)
        Dim StudentsCount As ReportParameter = New ReportParameter("StudentsCount", DataGridView1.Rows.Count)
        Dim FacultyName As ReportParameter = New ReportParameter("facultyName", cmbFacultyName.Text)
        Dim SubjCode As ReportParameter = New ReportParameter("subjCode", cmbSubject.Text)

        Dim description As String = String.Empty
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT SUBJ_DESC FROM tbl_settings_college_curriculum_subjects WHERE SUBJ_CODE = @subj_code", conn)
                comm.Parameters.AddWithValue("@subj_code", cmbSubject.Text)
                description = comm.ExecuteScalar
            End Using
        End Using

        Dim SubjDesc As ReportParameter = New ReportParameter("subjDesc", description)
        Dim SchedDay As ReportParameter = New ReportParameter("day", cmbDay.Text)
        Dim TimeIn As ReportParameter = New ReportParameter("timeIn", cmbStartTime.Text)
        Dim TimeOut As ReportParameter = New ReportParameter("timeOut", cmbEndTime.Text)
        Dim Room As ReportParameter = New ReportParameter("Room", txtSchedRoom.Text)

        With DS.Tables("Students")
            .Rows.Clear()
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("No") = i + CInt(1)
                DR("StudentNo") = DataGridView1.Rows(i).Cells(0).Value
                DR("StudentName") = DataGridView1.Rows(i).Cells(1).Value
                DR("Course") = DataGridView1.Rows(i).Cells(2).Value
                DR("Yrlvl") = DataGridView1.Rows(i).Cells(3).Value
                DR("Section") = DataGridView1.Rows(i).Cells(4).Value
                DR("Status") = DataGridView1.Rows(i).Cells(5).Value
                .Rows.Add(DR)
            Next
        End With

        Dim rptSource As New ReportDataSource("DSMasterLists", DS.Tables("Students"))

        With frm_report
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(rptSource)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.MasterListsReport.rdlc"
            .ReportViewer1.LocalReport.SetParameters({FacultyName,
                                                      SubjCode,
                                                      SubjDesc,
                                                      SchedDay,
                                                      TimeIn,
                                                      TimeOut,
                                                      Room,
                                                      AcademicYearSemester,
                                                      StudentsCount})
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With

    End Sub
End Class