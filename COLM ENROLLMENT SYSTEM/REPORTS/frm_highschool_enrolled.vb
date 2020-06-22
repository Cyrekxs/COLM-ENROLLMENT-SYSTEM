Public Class frm_highschool_enrolled
    Dim conn As New SqlConnection(StringConnection)

    Private Sub LoadCollege()
        Dim M As Integer = 0
        Dim F As Integer = 0
        Dim TNew As Integer = 0
        Dim TOld As Integer = 0
        Dim TotalEnrolled As Integer = 0
        'LISTING THE BREAKDOWN OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsCollege(@ay,@sem) WHERE StudentName LIKE @search ORDER BY StudentName ASC", conn)
            comm.Parameters.AddWithValue("@search", "%" & txtSearchCollege.Text & "%")
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", Academic_Sem)
            Using reader As SqlDataReader = comm.ExecuteReader
                dgCollegeBreakdown.Rows.Clear()
                While reader.Read
                    dgCollegeBreakdown.Rows.Add(reader("Student_Number"),
                                                reader("StudentName"),
                                                LSet(reader("Gender"), 1),
                                                reader("Course_Code"),
                                                reader("Yrlvl"),
                                                reader("Sect_Code"),
                                                Format(CDate(reader("Assessed_Date")), "MM-dd-yyyy hh:mm tt"))
                End While
            End Using
        End Using

        'LISTING THE SUMMARY OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT DISTINCT Education_Level,Course_Code,Yrlvl,Sect_Code,COUNT(Student_Number) AS NoOfStudents FROM FN_EnrolledStudentsCollege(@ay,@sem) GROUP BY Education_Level,Course_Code,Yrlvl,Sect_Code", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", Academic_Sem)
            Using reader As SqlDataReader = comm.ExecuteReader
                dgCollegeSummary.Rows.Clear()
                While reader.Read
                    dgCollegeSummary.Rows.Add(reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), reader("NoOfStudents"))
                End While
            End Using
        End Using

        'GETTING THE Total Count of Females and Males
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsCollege(@ay,@sem)", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", Academic_Sem)
            comm.Parameters.AddWithValue("@dateyr", "%" & Date.Now.Year.ToString & "%")
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    If reader("InformationRegistered").ToString.Contains(Date.Now.Year.ToString) Then
                        TNew += 1
                    Else
                        TOld += 1
                    End If

                    If IsDBNull(reader("Gender")) = False Then
                        If LSet(reader("Gender"), 1).ToString.ToUpper = "M" Then
                            M += 1
                        Else
                            F += 1
                        End If
                    End If
                End While
            End Using
        End Using

        'GETTING THE Total Enrolled Students
        Using comm As New SqlCommand("SELECT COUNT(*) AS NoOfStudents FROM FN_EnrolledStudentsCollege(@ay,@sem)", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", Academic_Sem)
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    TotalEnrolled = reader("NoOfStudents")
                End While
            End Using
        End Using


        'SHOWING FETCHED DATA
        txtNewCollege.Text = TNew
        txtOldCollege.Text = TOld
        txtMaleCollege.Text = M
        txtFemaleCollege.Text = F
        txtEnrolledCollege.Text = TotalEnrolled
    End Sub

    Private Sub LoadEnrolledJuniorHigh()
        Dim M As Integer = 0
        Dim F As Integer = 0
        Dim TNew As Integer = 0
        Dim TOld As Integer = 0
        Dim TotalEnrolled As Integer = 0
        'LISTING THE BREAKDOWN OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege('JUNIOR HIGH',@ay) WHERE Lastname + ' ' + Firstname LIKE @search ORDER BY Lastname,Firstname ASC", conn)
            comm.Parameters.AddWithValue("@search", "%" & txtSearchJunior.Text & "%")
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView1.Rows.Clear()
                While reader.Read
                    DataGridView1.Rows.Add(reader("Student_Number"), reader("Lastname") + " " + reader("Firstname") + " " + reader("Middlename"), LSet(reader("Gender"), 1), reader("Yrlvl"), reader("Sect_Code"), Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"))
                End While
            End Using
        End Using

        'LISTING THE SUMMARY OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT DISTINCT Education_Level,Course_Code,Yrlvl,Sect_Code,COUNT(Student_Number) AS NoOfStudents FROM FN_EnrolledStudentsNonCollege('JUNIOR HIGH',@ay) GROUP BY Education_Level,Course_Code,Yrlvl,Sect_Code", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView2.Rows.Clear()
                While reader.Read
                    DataGridView2.Rows.Add(reader("Yrlvl"), reader("Sect_Code"), reader("NoOfStudents"))
                End While
            End Using
        End Using


        'GETTING THE Total Count of Females and Males
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege('JUNIOR HIGH',@ay)", conn)
            comm.Parameters.AddWithValue("@search", "%" & txtSearchJunior.Text & "%")
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@dateyr", "%" & Date.Now.Year.ToString & "%")
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    If reader("InformationRegistered").ToString.Contains(Date.Now.Year.ToString) Then
                        TNew += 1
                    Else
                        TOld += 1
                    End If

                    If LSet(reader("Gender"), 1).ToString.ToUpper = "M" Then
                        M += 1
                    Else
                        F += 1
                    End If
                End While
            End Using
        End Using

        'GETTING THE Total Enrolled Students
        Using comm As New SqlCommand("SELECT COUNT(*) AS NoOfStudents FROM FN_EnrolledStudentsNonCollege('JUNIOR HIGH',@ay)", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    TotalEnrolled = reader("NoOfStudents")
                End While
            End Using
        End Using


        'SHOWING FETCHED DATA
        txtTotalNewJunior.Text = TNew
        txtTotalOldJunior.Text = TOld
        txtTotalMaleJunior.Text = M
        txtTotalFemaleJunior.Text = F
        txtTotalEnrolledJunior.Text = TotalEnrolled
    End Sub

    Public Sub LoadEnrolledSeniorHigh()
        Dim M As Integer = 0
        Dim F As Integer = 0
        Dim TNew As Integer = 0
        Dim TOld As Integer = 0
        Dim TotalEnrolled As Integer = 0
        'LISTING THE BREAKDOWN OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege('SENIOR HIGH',@ay) WHERE Lastname + ' ' + Firstname LIKE @search ORDER BY Lastname,Firstname ASC", conn)
            comm.Parameters.AddWithValue("@search", "%" & txtSearchSenior.Text & "%")
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", Academic_Sem)
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView3.Rows.Clear()
                While reader.Read
                    DataGridView3.Rows.Add(reader("Student_Number"), reader("Lastname") + " " + reader("Firstname") + " " + reader("Middlename"), LSet(reader("Gender"), 1), reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"))
                End While
            End Using
        End Using

        'LISTING THE SUMMARY OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT DISTINCT Education_Level,Course_Code,Yrlvl,Sect_Code,COUNT(Student_Number) AS NoOfStudents FROM FN_EnrolledStudentsNonCollege('SENIOR HIGH',@ay) GROUP BY Education_Level,Course_Code,Yrlvl,Sect_Code", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", Academic_Sem)
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView4.Rows.Clear()
                While reader.Read
                    DataGridView4.Rows.Add(reader("Course_Code") & "-" & reader("Yrlvl"), reader("Sect_Code"), reader("NoOfStudents"))
                End While
            End Using
        End Using


        'GETTING THE Total Count of Females and Males
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege('SENIOR HIGH',@ay)", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", Academic_Sem)
            comm.Parameters.AddWithValue("@dateyr", "%" & Date.Now.Year.ToString & "%")
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    If reader("InformationRegistered").ToString.Contains(Date.Now.Year.ToString) Then
                        TNew += 1
                    Else
                        TOld += 1
                    End If

                    If LSet(reader("Gender"), 1).ToString.ToUpper = "M" Then
                        M += 1
                    Else
                        F += 1
                    End If
                End While
            End Using
        End Using

        'GETTING THE Total Enrolled Students
        Using comm As New SqlCommand("SELECT COUNT(*) AS NoOfStudents FROM FN_EnrolledStudentsNonCollege('SENIOR HIGH',@ay)", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", Academic_Sem)
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    TotalEnrolled = reader("NoOfStudents")
                End While
            End Using
        End Using


        'SHOWING FETCHED DATA
        txtTotalNewSenior.Text = TNew
        txtTotalOldSenior.Text = TOld
        txtTotalMaleSenior.Text = M
        txtTotalFemaleSenior.Text = F
        txtTotalEnrolledSenior.Text = TotalEnrolled
    End Sub

    Public Sub LoadEnrolledElementary()
        Dim M As Integer = 0
        Dim F As Integer = 0
        Dim TNew As Integer = 0
        Dim TOld As Integer = 0
        Dim TotalEnrolled As Integer = 0
        'LISTING THE BREAKDOWN OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege('ELEMENTARY',@ay) WHERE Lastname + ' ' + Firstname LIKE @search ORDER BY Lastname,Firstname ASC", conn)
            comm.Parameters.AddWithValue("@search", "%" & txtElementarySearch.Text & "%")
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView6.Rows.Clear()
                While reader.Read
                    DataGridView6.Rows.Add(reader("Student_Number"), reader("Lastname") + " " + reader("Firstname") + " " + reader("Middlename"), LSet(reader("Gender"), 1), reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"))
                End While
            End Using
        End Using

        'LISTING THE SUMMARY OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT DISTINCT Education_Level,Course_Code,Yrlvl,Sect_Code,COUNT(Student_Number) AS NoOfStudents FROM FN_EnrolledStudentsNonCollege('ELEMENTARY',@ay) GROUP BY Education_Level,Course_Code,Yrlvl,Sect_Code", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView5.Rows.Clear()
                While reader.Read
                    DataGridView5.Rows.Add(reader("Course_Code") & "-" & reader("Yrlvl"), reader("Sect_Code"), reader("NoOfStudents"))
                End While
            End Using
        End Using


        'GETTING THE Total Count of Females and Males
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege('ELEMENTARY',@ay)", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
            comm.Parameters.AddWithValue("@dateyr", "%" & Date.Now.Year.ToString & "%")
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    If reader("InformationRegistered").ToString.Contains(Date.Now.Year.ToString) Then
                        TNew += 1
                    Else
                        TOld += 1
                    End If

                    If LSet(reader("Gender"), 1).ToString.ToUpper = "M" Then
                        M += 1
                    Else
                        F += 1
                    End If
                End While
            End Using
        End Using

        'GETTING THE Total Enrolled Students
        Using comm As New SqlCommand("SELECT COUNT(*) AS NoOfStudents FROM FN_EnrolledStudentsNonCollege('ELEMENTARY',@ay)", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    TotalEnrolled = reader("NoOfStudents")
                End While
            End Using
        End Using


        'SHOWING FETCHED DATA
        txtTotalNewElem.Text = TNew
        txtTotalOldElem.Text = TOld
        txtTotalMaleElem.Text = M
        txtTotalFemaleElem.Text = F
        txtTotalEnrolledElem.Text = TotalEnrolled
    End Sub


    Public Sub LoadEnrolledPreElementary()
        Dim M As Integer = 0
        Dim F As Integer = 0
        Dim TNew As Integer = 0
        Dim TOld As Integer = 0
        Dim TotalEnrolled As Integer = 0
        'LISTING THE BREAKDOWN OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege('PRE ELEMENTARY',@ay) WHERE Lastname + ' ' + Firstname LIKE @search ORDER BY Lastname,Firstname ASC", conn)
            comm.Parameters.AddWithValue("@search", "%" & txtElementarySearch.Text & "%")
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView8.Rows.Clear()
                While reader.Read
                    DataGridView8.Rows.Add(reader("Student_Number"), reader("Lastname") + " " + reader("Firstname") + " " + reader("Middlename"), LSet(reader("Gender"), 1), reader("Course_Code"), reader("Yrlvl"), reader("Sect_Code"), Format(reader("Assessed_Date"), "MM-dd-yyyy hh:mm tt"))
                End While
            End Using
        End Using

        'LISTING THE SUMMARY OF ENROLLED STUDENTS
        Using comm As New SqlCommand("SELECT DISTINCT Education_Level,Course_Code,Yrlvl,Sect_Code,COUNT(Student_Number) AS NoOfStudents FROM FN_EnrolledStudentsNonCollege('PRE ELEMENTARY',@ay) GROUP BY Education_Level,Course_Code,Yrlvl,Sect_Code", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView7.Rows.Clear()
                While reader.Read
                    DataGridView7.Rows.Add(reader("Course_Code") & "-" & reader("Yrlvl"), reader("Sect_Code"), reader("NoOfStudents"))
                End While
            End Using
        End Using


        'GETTING THE Total Count of Females and Males
        Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsNonCollege('PRE ELEMENTARY',@ay)", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
            comm.Parameters.AddWithValue("@dateyr", "%" & Date.Now.Year.ToString & "%")
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    If reader("InformationRegistered").ToString.Contains(Date.Now.Year.ToString) Then
                        TNew += 1
                    Else
                        TOld += 1
                    End If

                    If LSet(reader("Gender"), 1).ToString.ToUpper = "M" Then
                        M += 1
                    Else
                        F += 1
                    End If
                End While
            End Using
        End Using

        'GETTING THE Total Enrolled Students
        Using comm As New SqlCommand("SELECT COUNT(*) AS NoOfStudents FROM FN_EnrolledStudentsNonCollege('PRE ELEMENTARY',@ay)", conn)
            comm.Parameters.AddWithValue("@ay", Academic_Year)
            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
            Using reader As SqlDataReader = comm.ExecuteReader
                While reader.Read
                    TotalEnrolled = reader("NoOfStudents")
                End While
            End Using
        End Using


        'SHOWING FETCHED DATA
        txtTotalNewPreElem.Text = TNew
        txtTotalOldPreElem.Text = TOld
        txtTotalMalePreElem.Text = M
        txtTotalFemalePreElem.Text = F
        txtTotalEnrolledPreElem.Text = TotalEnrolled
    End Sub


    Private Sub frm_highschool_enrolled_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If conn.State <> ConnectionState.Open Then
            conn.Close()
            conn.Open()
        Else
            conn.Open()
        End If
        LoadCollege()
        LoadEnrolledJuniorHigh()
        LoadEnrolledSeniorHigh()
        LoadEnrolledElementary()
        LoadEnrolledPreElementary()
    End Sub

    Private Sub txtSearchJunior_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchJunior.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadEnrolledJuniorHigh()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadEnrolledJuniorHigh()
    End Sub

    Private Sub txtTotalEnrolledJunior_TextChanged(sender As Object, e As EventArgs) Handles txtTotalEnrolledJunior.TextChanged

    End Sub

    Private Sub txtSearchSenior_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchSenior.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadEnrolledSeniorHigh()
        End If
    End Sub

    Private Sub txtSearchSenior_TextChanged(sender As Object, e As EventArgs) Handles txtSearchSenior.TextChanged

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        LoadEnrolledSeniorHigh()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadEnrolledSeniorHigh()
    End Sub

    Private Sub txtElementarySearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtElementarySearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadEnrolledElementary()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        LoadEnrolledElementary()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        LoadEnrolledElementary()
    End Sub

    Private Sub txtPreElemSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPreElemSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadEnrolledPreElementary()
        End If
    End Sub

    Private Sub txtPreElemSearch_TextChanged(sender As Object, e As EventArgs) Handles txtPreElemSearch.TextChanged

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        LoadEnrolledPreElementary()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        LoadEnrolledPreElementary()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        LoadEnrolledJuniorHigh()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub

    Private Sub dgCollegeSummary_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCollegeSummary.CellContentClick
        If e.ColumnIndex = 4 Then
            Dim param_course As ReportParameter = New ReportParameter("course", dgCollegeSummary.Rows(e.RowIndex).Cells(0).Value.ToString)
            Dim param_yrlvl As ReportParameter = New ReportParameter("yrlvl", dgCollegeSummary.Rows(e.RowIndex).Cells(1).Value.ToString)
            Dim param_section As ReportParameter = New ReportParameter("section", dgCollegeSummary.Rows(e.RowIndex).Cells(2).Value.ToString)
            Dim param_count As ReportParameter = New ReportParameter("count", dgCollegeSummary.Rows(e.RowIndex).Cells(3).Value.ToString)

            Dim ds As New DS_Masterlists_College_Section
            Dim dr As DataRow

            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM FN_EnrolledStudentsCollege(@ay,@sem) WHERE Course_Code = @Course_Code AND Yrlvl = @Yrlvl AND Sect_Code = @Sect_Code ORDER BY StudentName ASC", conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@course_code", dgCollegeSummary.Rows(e.RowIndex).Cells(0).Value)
                    comm.Parameters.AddWithValue("@yrlvl", dgCollegeSummary.Rows(e.RowIndex).Cells(1).Value)
                    comm.Parameters.AddWithValue("@sect_code", dgCollegeSummary.Rows(e.RowIndex).Cells(2).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        With ds.Tables("DT_College_Section")
                            .Rows.Clear()
                            Dim i As Integer = 0
                            While reader.Read
                                dr = .NewRow
                                i += 1
                                dr("No") = i
                                dr("StudentNo") = reader("Student_Number")
                                dr("StudentName") = reader("StudentName")
                                dr("Gender") = reader("Gender")
                                .Rows.Add(dr)
                            End While
                        End With
                    End Using
                End Using
            End Using

            Dim myreport As New ReportDataSource("DataSet1", ds.Tables("DT_College_Section"))
            With frm_rdlc_report_viewer
                .ReportViewer1.LocalReport.DataSources.Clear()
                .ReportViewer1.LocalReport.DataSources.Add(myreport)
                .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_masterlists_college_section.rdlc"
                .ReportViewer1.LocalReport.SetParameters({param_course, param_yrlvl, param_section, param_count})
                .ReportViewer1.RefreshReport()
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        End If
    End Sub
End Class