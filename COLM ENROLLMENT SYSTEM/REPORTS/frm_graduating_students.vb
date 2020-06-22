Imports Microsoft.Reporting.WinForms

Public Class frm_graduating_students

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If cmbAcademicSem.Text = "1ST SEMESTER" Then
                Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudents('COLLEGE',@ay,@sem) AS E1 WHERE Yrlvl = '4TH YEAR' AND NOT EXISTS (SELECT * FROM FN_EnrolledStudents('COLLEGE',@ay,'2ND SEMESTER') WHERE Student_Number = E1.Student_Number) ORDER BY COURSE_CODE,LASTNAME,FIRSTNAME ASC", conn)
                    comm.Parameters.AddWithValue("@ay", cmbAcademicYear.Text)
                    comm.Parameters.AddWithValue("@sem", cmbAcademicSem.Text)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add(reader("Student_Number"), reader("Lastname") & " " & reader("Firstname") & reader("Middlename"), reader("Course_Code"))
                        End While
                    End Using
                End Using
            ElseIf cmbAcademicSem.Text = "2ND SEMESTER" Then
                Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudents('COLLEGE',@ay,@sem) WHERE Yrlvl = '4TH YEAR' ORDER BY COURSE_CODE,LASTNAME,FIRSTNAME ASC", conn)
                    comm.Parameters.AddWithValue("@ay", cmbAcademicYear.Text)
                    comm.Parameters.AddWithValue("@sem", cmbAcademicSem.Text)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add(reader("Student_Number"), reader("Lastname") & " " & reader("Firstname") & reader("Middlename"), reader("Course_Code"))
                        End While
                    End Using
                End Using
            End If

        End Using
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAcademicSem.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim DR As DataRow
        Dim DS As New DS_GraduatingStudents
        Dim aysem As New ReportParameter("aysem", cmbAcademicYear.Text & " " & cmbAcademicSem.Text)

        With DS.Tables("DT_GraduatingStudents")
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("No") = i + 1
                DR("StudentNo") = DataGridView1.Rows(i).Cells(0).Value
                DR("StudentName") = DataGridView1.Rows(i).Cells(1).Value
                DR("StudentCourse") = DataGridView1.Rows(i).Cells(2).Value
                .Rows.Add(DR)
            Next
        End With


        Dim Source1 As New ReportDataSource("DSGraduatingStudents", DS.Tables(0))

        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(Source1)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_graduating_students.rdlc"
            .ReportViewer1.LocalReport.SetParameters({aysem})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With

        'Dim MyReport As New payment_reports_tuition_and_other_fees
        'MyReport.Load(Application.StartupPath & "/rpt_graduating_students.rpt")
        'MyReport.SetDataSource(DS.Tables("DT_GraduatingStudents"))

        'Dim myTextObjectOnReport As CrystalDecisions.CrystalReports.Engine.TextObject
        'myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtAySem"), CrystalDecisions.CrystalReports.Engine.TextObject)
        'myTextObjectOnReport.Text = cmbAcademicYear.Text & " " & cmbAcademicSem.Text

        'With frm_report_assessment
        '    .MdiParent = frm_main
        '    .WindowState = FormWindowState.Maximized
        '    .CrystalReportViewer1.ReportSource = MyReport
        '    .CrystalReportViewer1.Refresh()
        '    .StartPosition = FormStartPosition.CenterParent
        '    .Show()
        'End With
    End Sub
End Class