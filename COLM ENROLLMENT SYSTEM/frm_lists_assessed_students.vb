Imports Microsoft.Reporting.WinForms

Public Class frm_lists_assessed_students

    Public Sub LoadYearLevels()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT YRLVL FROM FN_ENROLLEDSTUDENTS('JUNIOR HIGH',@ay,@sem) ORDER BY YRLVL ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbYrlvl.Items.Clear()
                    cmbYrlvl.Items.Add("ALL")
                    While reader.Read
                        cmbYrlvl.Items.Add(reader("YRLVL"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadSections()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT SECT_CODE FROM FN_ENROLLEDSTUDENTS('JUNIOR HIGH',@ay,@sem) WHERE YRLVL = @yrlvl ORDER BY SECT_CODE ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@yrlvl", cmbYrlvl.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbSection.Items.Clear()
                    cmbSection.Items.Add("ALL")
                    While reader.Read
                        cmbSection.Items.Add(reader("SECT_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If cmbYrlvl.Text = "ALL" And cmbSection.Text = "ALL" Then
                Using comm As New SqlCommand("SELECT * FROM FN_ENROLLEDSTUDENTS('JUNIOR HIGH',@ay,@sem) ORDER BY COURSE_CODE,YRLVL,SECT_CODE,LASTNAME,FIRSTNAME ASC", conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add(reader("ASSESSMENT_ID"), reader("Education_Level"), reader("Student_Number"), reader("Lastname") & " " & reader("Firstname") & " " & reader("Middlename"), reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), "PRINT ASSESSMENT")
                        End While
                    End Using
                End Using
            ElseIf cmbYrlvl.Text <> "ALL" And cmbSection.Text = "ALL" Then
                Using comm As New SqlCommand("SELECT * FROM FN_ENROLLEDSTUDENTS('JUNIOR HIGH',@ay,@sem) WHERE YRLVL = @yrlvl ORDER BY COURSE_CODE,YRLVL,SECT_CODE,LASTNAME,FIRSTNAME ASC", conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYrlvl.Text)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add(reader("ASSESSMENT_ID"), reader("Education_Level"), reader("Student_Number"), reader("Lastname") & " " & reader("Firstname") & " " & reader("Middlename"), reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), "PRINT ASSESSMENT")
                        End While
                    End Using
                End Using
            ElseIf cmbYrlvl.Text <> "ALL" And cmbSection.Text <> "ALL" Then
                Using comm As New SqlCommand("SELECT * FROM FN_ENROLLEDSTUDENTS('JUNIOR HIGH',@ay,@sem) WHERE YRLVL = @yrlvl AND SECT_CODE = @sect_code ORDER BY COURSE_CODE,YRLVL,SECT_CODE,LASTNAME,FIRSTNAME ASC", conn)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYrlvl.Text)
                    comm.Parameters.AddWithValue("@sect_code", cmbSection.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add(reader("ASSESSMENT_ID"), reader("Education_Level"), reader("Student_Number"), reader("Lastname") & " " & reader("Firstname") & " " & reader("Middlename"), reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), "PRINT ASSESSMENT")
                        End While
                    End Using
                End Using
            End If

        End Using
        TextBox1.Text = DataGridView1.Rows.Count
    End Sub
    Private Sub frm_lists_assessed_students_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudents()
        LoadYearLevels()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ''Dim myTextObjectOnReport As CrystalDecisions.CrystalReports.Engine.TextObject
        ''myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtschooldiscount"), CrystalDecisions.CrystalReports.Engine.TextObject)
        ''myTextObjectOnReport.Text = Convert_To_Currency(txtTFee.Text)

        'With frm_report_assessment
        '    .MdiParent = frm_main
        '    .CrystalReportViewer1.ReportSource = MyReport
        '    .CrystalReportViewer1.Refresh()
        '    .StartPosition = FormStartPosition.CenterParent
        '    .Show()

        '    Dim DS As New DS_HS_Assessment
        '    Dim DR As DataRow
        '    With DS.Tables("DT_Assessment")
        '        Using conn As New SqlConnection(StringConnection)
        '            conn.Open()
        '            For i = 0 To DataGridView1.Rows.Count - 1
        '                Using comm As New SqlCommand("SELECT STUDENT_ASSESSMENT.*,FEE_CODE,FEE_AMOUNT,DUE_DATE FROM FN_ENROLLEDSTUDENTS('JUNIOR HIGH','2017-2018','1ST SEMESTER') AS STUDENT_ASSESSMENT INNER JOIN TBL_COLLEGE_ASSESSMENT_BREAKDOWN AS BREAKDOWN ON STUDENT_ASSESSMENT.Student_Number = BREAKDOWN.Student_Number WHERE BREAKDOWN.Academic_Yr = '2017-2018' AND BREAKDOWN.Academic_Sem = '1ST SEMESTER' AND STUDENT_ASSESSMENT.Student_Number = @SN", conn)
        '                    comm.Parameters.AddWithValue("@SN", DataGridView1.Rows(i).Cells(2).Value)
        '                    Using reader As SqlDataReader = comm.ExecuteReader
        '                        While reader.Read
        '                            DR = .NewRow
        '                            DR("StudentNo") = DataGridView1.Rows(i).Cells(2).Value
        '                            DR("StudentName") = DataGridView1.Rows(i).Cells(3).Value
        '                            DR("CourseCode") = DataGridView1.Rows(i).Cells(4).Value
        '                            DR("Yrlvl") = DataGridView1.Rows(i).Cells(5).Value
        '                            DR("SectCode") = DataGridView1.Rows(i).Cells(6).Value
        '                            DR("PaymentMode") = reader("Assessment_Type")
        '                            DR("AssessmentDate") = reader("Assessed_Date")
        '                            DR("AssessedBy") = "ANTHONY QUIJANO"
        '                            DR("OldBalance") = reader("Old_Balance")
        '                            DR("TFee") = reader("TFEE")
        '                            DR("MFee") = reader("MFEE")
        '                            DR("OFee") = reader("OFEE")
        '                            DR("SurchargeFee") = reader("Surcharge")
        '                            DR("VoucherCode") = reader("Voucher_Code")
        '                            DR("VoucherAmount") = reader("Voucher_Amount")
        '                            DR("DiscountCode") = reader("Discount_Code")
        '                            DR("DiscountAmount") = reader("Discount_Amount")
        '                            DR("TotalAmount") = reader("Total_Tuition")
        '                            DR("FeeCode") = reader("Fee_Code")
        '                            DR("FeeAmount") = reader("Fee_Amount")
        '                            DR("DueDate") = reader("Due_Date")
        '                            .Rows.Add(DR)
        '                        End While
        '                    End Using
        '                End Using
        '            Next
        '        End Using
        '    End With

        '    Dim MyReport As New ReportDataSource("DSAssessment", DS.Tables(0))
        '    With frm_rdlc_report_viewer
        '        .ReportViewer1.LocalReport.DataSources.Clear()
        '        .ReportViewer1.LocalReport.DataSources.Add(MyReport)
        '        .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_hs_student_assessment.rdlc"
        '        .ReportViewer1.RefreshReport()
        '        .StartPosition = FormStartPosition.CenterScreen
        '        .ShowDialog()
        '        .Close()
        '        .Dispose()
        '    End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 7 Then
            Dim DS As New DS_ASSESSMENT
            Dim DR As DataRow
            DS.Tables("1_Student_Assessment_Summary").Rows.Clear()
            DS.Tables("2_Student_Subjects").Rows.Clear()
            DS.Tables("3_Student_Assessment_Breakdown").Rows.Clear()

            With DS.Tables("1_Student_Assessment_Summary")
                DR = .NewRow
                DR("Student_Number") = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                DR("Student_Name") = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                DR("Course") = "JHS"
                DR("YrLvl") = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                DR("Section_Code") = DataGridView1.Rows(e.RowIndex).Cells(6).Value
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM FN_ENROLLEDSTUDENTS('JUNIOR HIGH',@ay,@sem) WHERE Student_Number = @SN", conn)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.Parameters.AddWithValue("@SN", DataGridView1.Rows(e.RowIndex).Cells(2).Value)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            While reader.Read
                                DR("Assessment_Type") = reader("Assessment_Type")
                                DR("TFee") = Convert_To_Currency(reader("TFEE"))
                                DR("MFee") = Convert_To_Currency(reader("MFEE"))
                                DR("OFee") = Convert_To_Currency(reader("OFEE"))
                                DR("Old_Balance") = Convert_To_Currency(reader("OLD_Balance"))
                                DR("Voucher_Code") = reader("Voucher_Code")
                                DR("Voucher_Amount") = Convert_To_Currency(reader("Voucher_Amount"))
                                DR("Discount_Code") = reader("Discount_Code")
                                DR("Discount_Percentage") = reader("Discount_Percentage")
                                DR("Discount_Amount") = Convert_To_Currency(reader("Discount_Amount"))
                                DR("Surcharge") = Convert_To_Currency(reader("Surcharge"))
                                DR("Total") = Convert_To_Currency(reader("TOTAL_TUITION"))
                                DR("Academic_Year") = Academic_Year
                                DR("Academic_Sem") = Academic_Sem
                                DR("Assess_By") = Account_Name
                                DR("Assessed_Date") = Format(reader("Assessed_Date"), "MM/dd/yyy hh:mm")
                            End While
                        End Using
                    End Using
                End Using
                .Rows.Add(DR)
            End With


            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM tbl_college_assessment_breakdown WHERE Student_Number = @SN AND Academic_Yr = @ay AND ACADEMIC_SEM = @sem ORDER BY ID ASC", conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@SN", DataGridView1.Rows(e.RowIndex).Cells(2).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            With DS.Tables("3_Student_Assessment_Breakdown")
                                DR = .NewRow
                                DR("Student_Number") = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                                DR("Fee_Code") = reader("Fee_Code")
                                DR("Fee_Amount") = reader("Fee_Amount")
                                DR("Due_Date") = reader("Due_Date")
                                .Rows.Add(DR)
                            End With
                        End While
                    End Using
                End Using
            End Using

            'Dim MyReport As New report_assessment_highschool
            'MyReport.Load(Application.StartupPath & "/report_assessment_highschool.rpt")
            'MyReport.SetDataSource(DS.Tables("1_Student_Assessment_Summary"))
            'MyReport.OpenSubreport("Assessment_Breakdown").SetDataSource(DS.Tables("3_Student_Assessment_Breakdown"))

            'With frm_report_assessment
            '    .WindowState = FormWindowState.Maximized
            '    .CrystalReportViewer1.ReportSource = MyReport
            '    .CrystalReportViewer1.Refresh()
            '    .StartPosition = FormStartPosition.CenterParent
            '    .ShowDialog()
            'End With
        End If
    End Sub

    Private Sub cmbYrlvl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYrlvl.SelectedIndexChanged
        LoadSections()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        LoadStudents()
    End Sub
End Class