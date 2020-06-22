Public Class frm_registered_college_students
    Public DGRow As Integer = 0
    Public Sub LoadStudents()
        Dim StudentCount As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim SQLQuery As String = String.Empty

            'Select Case cmbFilter.Text
            '    Case "STUDENT NO"
            '        SQLQuery = "SELECT ROWID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' ELSE ' ' + EXTENSION_NAME END + ', ' + FIRSTNAME + ' ' + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,COURSECODE,YRLVL,SECTIONCODE,REGISTEREDDATE,REGISTEREDBY FROM TBL_STUDENT_INFORMATION INNER JOIN TBL_STUDENT_REGISTERED ON TBL_STUDENT_INFORMATION.STUDENT_NUMBER = TBL_STUDENT_REGISTERED.STUDENT_NUMBER WHERE STUDENT_NUMBER LIKE @search AND TBL_STUDENT_REGISTERED.EDUCATION_LEVEL = 'COLLEGE' AND TBL_STUDENT_REGISTERED.ACADEMIC_YEAR = @ay AND TBL_STUDENT_REGISTERED.ACADEMIC_SEM = @sem ORDER BY LASTNAME,FIRSTNAME ASC"
            '    Case "STUDENT NAME"
            '        SQLQuery = "SELECT ROWID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' ELSE ' ' + EXTENSION_NAME END + ', ' + FIRSTNAME + ' ' + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,COURSECODE,YRLVL,SECTIONCODE,REGISTEREDDATE,REGISTEREDBY FROM TBL_STUDENT_INFORMATION INNER JOIN TBL_STUDENT_REGISTERED ON TBL_STUDENT_INFORMATION.STUDENT_NUMBER = TBL_STUDENT_REGISTERED.STUDENT_NUMBER WHERE LASTNAME + FIRSTNAME LIKE @search AND TBL_STUDENT_REGISTERED.EDUCATION_LEVEL = 'COLLEGE' AND TBL_STUDENT_REGISTERED.ACADEMIC_YEAR = @ay AND TBL_STUDENT_REGISTERED.ACADEMIC_SEM = @sem ORDER BY LASTNAME,FIRSTNAME ASC"
            '    Case "COURSE"
            '        SQLQuery = "SELECT ROWID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' ELSE ' ' + EXTENSION_NAME END + ', ' + FIRSTNAME + ' ' + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,COURSECODE,YRLVL,SECTIONCODE,REGISTEREDDATE,REGISTEREDBY FROM TBL_STUDENT_INFORMATION INNER JOIN TBL_STUDENT_REGISTERED ON TBL_STUDENT_INFORMATION.STUDENT_NUMBER = TBL_STUDENT_REGISTERED.STUDENT_NUMBER WHERE COURSECODE LIKE @search AND TBL_STUDENT_REGISTERED.EDUCATION_LEVEL = 'COLLEGE' AND TBL_STUDENT_REGISTERED.ACADEMIC_YEAR = @ay AND TBL_STUDENT_REGISTERED.ACADEMIC_SEM = @sem ORDER BY LASTNAME,FIRSTNAME ASC"
            '    Case "YEAR LEVEL"
            '        SQLQuery = "SELECT ROWID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' ELSE ' ' + EXTENSION_NAME END + ', ' + FIRSTNAME + ' ' + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,COURSECODE,YRLVL,SECTIONCODE,REGISTEREDDATE,REGISTEREDBY FROM TBL_STUDENT_INFORMATION INNER JOIN TBL_STUDENT_REGISTERED ON TBL_STUDENT_INFORMATION.STUDENT_NUMBER = TBL_STUDENT_REGISTERED.STUDENT_NUMBER WHERE YRLVL LIKE @search AND TBL_STUDENT_REGISTERED.EDUCATION_LEVEL = 'COLLEGE' AND TBL_STUDENT_REGISTERED.ACADEMIC_YEAR = @ay AND TBL_STUDENT_REGISTERED.ACADEMIC_SEM = @sem ORDER BY LASTNAME,FIRSTNAME ASC"
            'End Select


            SQLQuery = "SELECT ROWID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' ELSE ' ' + EXTENSION_NAME END + ', ' + FIRSTNAME + ' ' + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,COURSECODE,YRLVL,SECTIONCODE,REGISTEREDDATE,REGISTEREDBY FROM TBL_STUDENT_INFORMATION INNER JOIN TBL_STUDENT_REGISTERED ON TBL_STUDENT_INFORMATION.STUDENT_NUMBER = TBL_STUDENT_REGISTERED.STUDENT_NUMBER WHERE TBL_STUDENT_INFORMATION.STUDENT_NUMBER + LASTNAME + FIRSTNAME LIKE @search AND TBL_STUDENT_REGISTERED.EDUCATION_LEVEL = 'COLLEGE' AND TBL_STUDENT_REGISTERED.ACADEMIC_YEAR = @ay AND TBL_STUDENT_REGISTERED.ACADEMIC_SEM = @sem ORDER BY LASTNAME,FIRSTNAME ASC"
            Using comm As New SqlCommand(SQLQuery, conn)

                'Select Case cmbFilter.Text
                '    Case "STUDENT NO"
                '        comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                '    Case "STUDENT NAME"
                '        comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                '    Case "COURSE"
                '        comm.Parameters.AddWithValue("@search", txtSearch.Text)
                '    Case "YEAR LEVEL"
                '        comm.Parameters.AddWithValue("@search", txtSearch.Text)
                'End Select
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)

                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ROWID"), reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSECODE"), reader("YRLVL"), reader("SECTIONCODE"), reader("REGISTEREDDATE"), reader("REGISTEREDBY"))
                        StudentCount += 1
                    End While
                End Using
            End Using
            txtStudentCount.Text = StudentCount
        End Using
    End Sub

    Private Sub frm_registered_college_students_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'cmbFilter.Text = "STUDENT NAME"
        LoadStudents()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadStudents()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text = String.Empty Then
            LoadStudents()
        End If
    End Sub

    Private Sub cmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadStudents()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frm_registration_college_entry.MdiParent = frm_main
        frm_registration_college_entry.Show()
        frm_registration_college_entry.Top = 0
        frm_registration_college_entry.Left = 0
        Me.Close()
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With frm_registration_college_edit
            .DGRow = DGRow
            .RegID = DataGridView1.Rows(DGRow).Cells(0).Value
            .StudentNumber = DataGridView1.Rows(DGRow).Cells(1).Value
            .StudentName = DataGridView1.Rows(DGRow).Cells(2).Value
            .Course = DataGridView1.Rows(DGRow).Cells(3).Value
            .Yrlvl = DataGridView1.Rows(DGRow).Cells(4).Value
            .Sect_code = DataGridView1.Rows(DGRow).Cells(5).Value
            .DGRow = DGRow
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @SN AND ACADEMIC_YR = @AY AND Academic_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@sn", DataGridView1.Rows(DGRow).Cells(1).Value)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                If Val(comm.ExecuteScalar) > 0 Then
                    MsgBox("The account has already paid delete option is not allowed to this account!", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End Using


            Dim registration_status As Integer = 0
            Dim assessment_status As Integer = 0
            Dim breakdown_status As Integer = 0
            Dim subjects_status As Integer = 0

            Dim comm_registration As New SqlCommand("DELETE FROM TBL_STUDENT_REGISTERED WHERE ROWID = @sn AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
            comm_registration.Parameters.AddWithValue("@sn", DataGridView1.Rows(DGRow).Cells(0).Value)
            comm_registration.Parameters.AddWithValue("@ay", Academic_Year)
            comm_registration.Parameters.AddWithValue("@sem", Academic_Sem)

            Dim comm_assessment As New SqlCommand("DELETE FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
            comm_assessment.Parameters.AddWithValue("@sn", DataGridView1.Rows(DGRow).Cells(1).Value)
            comm_assessment.Parameters.AddWithValue("@ay", Academic_Year)
            comm_assessment.Parameters.AddWithValue("@sem", Academic_Sem)

            Dim comm_breakdown As New SqlCommand("DELETE FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
            comm_breakdown.Parameters.AddWithValue("@sn", DataGridView1.Rows(DGRow).Cells(1).Value)
            comm_breakdown.Parameters.AddWithValue("@ay", Academic_Year)
            comm_breakdown.Parameters.AddWithValue("@sem", Academic_Sem)

            Dim comm_subjects As New SqlCommand("DELETE FROM TBL_COLLEGE_SUBJECT_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
            comm_subjects.Parameters.AddWithValue("@sn", DataGridView1.Rows(DGRow).Cells(1).Value)
            comm_subjects.Parameters.AddWithValue("@ay", Academic_Year)
            comm_subjects.Parameters.AddWithValue("@sem", Academic_Sem)


            If MsgBox("Are you sure you want to delete this registration?" & vbNewLine & " assessment data will also be deleted if you press yes!", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                registration_status = comm_registration.ExecuteNonQuery()
                assessment_status = comm_assessment.ExecuteNonQuery()
                breakdown_status = comm_breakdown.ExecuteNonQuery()
                subjects_status = comm_subjects.ExecuteNonQuery()
                MsgBox("School Information has been successfully removed in this academic year and academic sem!", MsgBoxStyle.Information)
                LoadStudents()
            End If


            'Using comm As New SqlCommand("DELETE FROM TBL_STUDENT_REGISTERED WHERE ROWID = @sn AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
            '    comm.Parameters.AddWithValue("@sn", DataGridView1.Rows(DGRow).Cells(0).Value)
            '    comm.Parameters.AddWithValue("@ay", Academic_Year)
            '    comm.Parameters.AddWithValue("@sem", Academic_Sem)
            '    If MsgBox("Are you sure you want to Remove this student registration?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '        registration_int = comm.ExecuteNonQuery()
            '    End If
            'End Using


        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox("You Can Search Student Student Number, Last Name or First Name in the search box!", MsgBoxStyle.Information)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadStudents()
    End Sub
End Class