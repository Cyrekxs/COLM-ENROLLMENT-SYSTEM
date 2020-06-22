Public Class frm_college_masterlist_breakdown

    Private Sub LoadStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_MasterLists_Breakdown(@ay,@sem) WHERE Sched_Faculty = @Faculty AND Subj_Code = @SubjCode AND Sched_Day = @Day AND Sched_Time_In = @TimeIn AND Sched_Time_Out = @TimeOut AND Sched_Room = @Room ORDER BY Student_Name ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@Faculty", txtFaculty.Text)
                comm.Parameters.AddWithValue("@SubjCode", txtSubjCode.Text)
                comm.Parameters.AddWithValue("@Day", txtDay.Text)
                comm.Parameters.AddWithValue("@TimeIn", txtTimeIn.Text)
                comm.Parameters.AddWithValue("@TimeOut", txtTimeOut.Text)
                comm.Parameters.AddWithValue("@Room", txtRoom.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        Dim Status As String = String.Empty
                        If CDbl(reader("Payment")) > 0 Then
                            Status = String.Empty
                        Else
                            Status = "NOT OFFICIAL"
                        End If

                        DataGridView1.Rows.Add(reader("Student_Number"),
                        reader("Student_Name"),
                        reader("Course_Code"),
                        reader("Yrlvl"),
                        reader("Sect_Code"), Status)

                    End While
                End Using
            End Using
        End Using
        txtStudents.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub frm_college_masterlist_breakdown_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudents()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim DS As New MasterLIstsDataSet
        Dim DR As DataRow

        Dim AcademicYearSemester As ReportParameter = New ReportParameter("AcademicYearSemester", Academic_Sem & " " & Academic_Year)
        Dim SubjCode As ReportParameter = New ReportParameter("subjCode", txtSubjCode.Text)
        Dim FacultyName As ReportParameter = New ReportParameter("facultyName", txtFaculty.Text)
        Dim StudentsCount As ReportParameter = New ReportParameter("StudentsCount", txtStudents.Text)

        Dim SubjDesc As ReportParameter = New ReportParameter("subjDesc", txtSubjDesc.Text)
        Dim SchedDay As ReportParameter = New ReportParameter("day", txtDay.Text)
        Dim TimeIn As ReportParameter = New ReportParameter("timeIn", txtTimeIn.Text)
        Dim TimeOut As ReportParameter = New ReportParameter("timeOut", txtTimeOut.Text)
        Dim Room As ReportParameter = New ReportParameter("Room", txtRoom.Text)

        With DS.Tables("Students")
            .Rows.Clear()
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("No") = i + 1
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