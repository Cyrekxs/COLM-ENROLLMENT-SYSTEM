
Imports Microsoft.Reporting.WinForms
Public Class frm_masterlists_senior
    Public Sub LoadSeniorHighCourses()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SENIOR_COURSES WHERE Academic_Yr = @ay ORDER BY COURSE_CODE ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCourseCode.Items.Clear()
                    While reader.Read
                        cmbCourseCode.Items.Add(reader("Course_Code"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_masterlists_senior_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSeniorHighCourses()
    End Sub

    Private Sub cmbYearLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYearLevel.SelectedIndexChanged
        Load_Sections("SENIOR HIGH", cmbCourseCode.Text, cmbYearLevel.Text, cmbSectionCode)
    End Sub

    Private Sub cmbCourseCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourseCode.SelectedIndexChanged
        'Load_YearLvls("SENIOR HIGH", cmbCourseCode.Text, cmbYearLevel)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim m As Integer = 0
        Dim f As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege('SENIOR HIGH',@ay) WHERE Course_Code = @course_code AND Yrlvl = @yrlvl AND Sect_Code = @sect_code ORDER BY Gender,Lastname,Firstname ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                comm.Parameters.AddWithValue("@yrlvl", cmbYearLevel.Text)
                comm.Parameters.AddWithValue("@sect_code", cmbSectionCode.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    Dim i As Integer = 0
                    While reader.Read
                        i += 1
                        If reader("GENDER").ToString = "MALE" Then
                            m += 1
                        ElseIf reader("GENDER").ToString = "FEMALE" Then
                            f += 1
                        End If
                        DataGridView1.Rows.Add(i, reader("Student_Number"), reader("Lastname") & " " & reader("Firstname") & " " & reader("Middlename"), LSet(reader("GENDER"), 1))
                    End While
                End Using
            End Using
        End Using
        txtMale.Text = m
        txtFemale.Text = f
        TextBox1.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim AcademicYear As New ReportParameter("AcademicYear", Academic_Year.ToString)
        Dim CourseYearAndSection As New ReportParameter("CourseYearAndSection", cmbCourseCode.Text & " " & cmbYearLevel.Text.ToString & " " & cmbSectionCode.Text.ToString)
        Dim TotalMaleFemale As New ReportParameter("TotalMaleFemale", "TOTAL MALE: " & txtMale.Text & " TOTAL FEMALE: " & txtFemale.Text)
        Dim DS As New DS_SeniorHigh_Masterlist
        Dim DR As DataRow

        With DS.Tables("DT_MasterListsSenior")
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("IndexNo") = DataGridView1.Rows(i).Cells(0).Value.ToString
                DR("StudentNumber") = DataGridView1.Rows(i).Cells(1).Value.ToString
                DR("StudentName") = DataGridView1.Rows(i).Cells(2).Value.ToString
                DR("Gender") = DataGridView1.Rows(i).Cells(3).Value.ToString
                .Rows.Add(DR)
            Next
        End With


        Dim Source1 As New ReportDataSource("DSSHSMasterLists", DS.Tables(0))

        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(Source1)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.report_masterlists_senior.rdlc"
            .ReportViewer1.LocalReport.SetParameters({AcademicYear, CourseYearAndSection, TotalMaleFemale})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            .Close()
            .Dispose()
        End With
    End Sub
End Class