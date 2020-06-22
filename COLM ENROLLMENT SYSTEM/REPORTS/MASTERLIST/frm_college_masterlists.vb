Public Class frm_college_masterlists
    Private Async Function LoadMasterListsSummary() As Threading.Tasks.Task
        Using conn As New SqlConnection(StringConnection)
            Await conn.OpenAsync
            If ComboBox1.Text = "SUBJECT" Then
                Using comm As New SqlCommand("SELECT * FROM FN_College_MasterLists_Summary(@ay,@sem) WHERE Subj_Code + Subj_Desc LIKE @search ORDER BY Subj_Code ASC", conn)
                    comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = Await comm.ExecuteReaderAsync
                        DataGridView1.Rows.Clear()
                        While Await reader.ReadAsync
                            DataGridView1.Rows.Add(reader("Subj_Code"), reader("Subj_Desc"), reader("Sched_Day"), reader("Sched_Time_In"), reader("Sched_Time_Out"), reader("Sched_Faculty"), reader("Sched_Room"), reader("Students"))
                        End While
                    End Using
                End Using
            ElseIf ComboBox1.Text = "FACULTY" Then
                Using comm As New SqlCommand("SELECT * FROM FN_College_MasterLists_Summary(@ay,@sem) WHERE Sched_Faculty LIKE @search ORDER BY Subj_Code ASC", conn)
                    comm.Parameters.AddWithValue("@search", txtSearch.Text & "%")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = Await comm.ExecuteReaderAsync
                        DataGridView1.Rows.Clear()
                        While Await reader.ReadAsync
                            DataGridView1.Rows.Add(reader("Subj_Code"), reader("Subj_Desc"), reader("Sched_Day"), reader("Sched_Time_In"), reader("Sched_Time_Out"), reader("Sched_Faculty"), reader("Sched_Room"), reader("Students"))
                        End While
                    End Using
                End Using
            Else
                Using comm As New SqlCommand("SELECT * FROM FN_College_MasterLists_Summary(@ay,@sem) WHERE Subj_Code + Subj_Desc LIKE @search ORDER BY Subj_Code ASC", conn)
                    comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = Await comm.ExecuteReaderAsync
                        DataGridView1.Rows.Clear()
                        While Await reader.ReadAsync
                            DataGridView1.Rows.Add(reader("Subj_Code"), reader("Subj_Desc"), reader("Sched_Day"), reader("Sched_Time_In"), reader("Sched_Time_Out"), reader("Sched_Faculty"), reader("Sched_Room"), reader("Students"))
                        End While
                    End Using
                End Using
            End If
        End Using
    End Function
    Private Async Sub frm_college_masterlists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadMasterListsSummary()
    End Sub
    Private Async Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Await LoadMasterListsSummary()
        End If
    End Sub

    Private Sub GenerateReport(index As Integer)
        Dim DS As New MasterLIstsDataSet
        Dim DR As DataRow

        Dim AcademicYearSemester As ReportParameter = New ReportParameter("AcademicYearSemester", Academic_Sem & " " & Academic_Year)
        Dim SubjCode As ReportParameter = New ReportParameter("subjCode", DataGridView1.Rows(index).Cells(0).Value.ToString)
        Dim FacultyName As ReportParameter = New ReportParameter("facultyName", DataGridView1.Rows(index).Cells(4).Value.ToString)
        Dim StudentsCount As ReportParameter = New ReportParameter("StudentsCount", DataGridView1.Rows(index).Cells(6).Value.ToString)

        Dim SubjDesc As ReportParameter = New ReportParameter("subjDesc", DataGridView1.Rows(index).Cells(1).Value.ToString)
        Dim SchedDay As ReportParameter = New ReportParameter("day", DataGridView1.Rows(index).Cells(2).Value.ToString)
        Dim TimeIn As ReportParameter = New ReportParameter("timeIn", DataGridView1.Rows(index).Cells(3).Value.ToString)
        Dim TimeOut As ReportParameter = New ReportParameter("timeOut", DataGridView1.Rows(index).Cells(4).Value.ToString)
        Dim Room As ReportParameter = New ReportParameter("Room", DataGridView1.Rows(index).Cells(5).Value.ToString)

        With DS.Tables("Students")
            .Rows.Clear()
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM FN_College_MasterLists_Breakdown(@ay,@sem) WHERE Sched_Faculty = @Faculty AND Subj_Code = @SubjCode AND Sched_Day = @Day AND Sched_Time_In = @TimeIn AND Sched_Time_Out = @TimeOut ORDER BY Student_Name ASC", conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@Faculty", DataGridView1.Rows(index).Cells(5).Value)
                    comm.Parameters.AddWithValue("@SubjCode", DataGridView1.Rows(index).Cells(0).Value)
                    comm.Parameters.AddWithValue("@Day", DataGridView1.Rows(index).Cells(2).Value)
                    comm.Parameters.AddWithValue("@TimeIn", DataGridView1.Rows(index).Cells(3).Value)
                    comm.Parameters.AddWithValue("@TimeOut", DataGridView1.Rows(index).Cells(4).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        Dim i As Integer = 0
                        While reader.Read
                            i += 1
                            DR = .NewRow
                            DR("No") = i
                            DR("StudentNo") = reader("Student_Number")
                            DR("StudentName") = reader("Student_Name")
                            DR("Course") = reader("Course_Code")
                            DR("Yrlvl") = reader("Yrlvl")
                            DR("Section") = reader("Sect_Code")
                            If CDbl(reader("Payment")) > 0 Then
                                DR("Status") = ""
                            Else
                                DR("Status") = "NOT OFFICIAL"
                            End If
                            .Rows.Add(DR)
                        End While
                    End Using
                End Using
            End Using
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

    Private Async Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Await LoadMasterListsSummary()
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Await LoadMasterListsSummary()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 7 Then
            'GenerateReport(e.RowIndex)
            Dim frm As New frm_college_masterlist_breakdown
            With frm
                .txtFaculty.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                .txtSubjCode.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                .txtSubjDesc.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                .txtDay.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                .txtTimeIn.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                .txtTimeOut.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                .txtRoom.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub
End Class