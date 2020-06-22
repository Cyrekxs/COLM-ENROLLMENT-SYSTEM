Public Class frm_registered_pre_elementary_students
    Public DGRow As Integer = 0
    Public Sub LoadStudents()
        Dim StudentCount As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim SQLQuery As String = String.Empty

            Select Case cmbFilter.Text
                Case "STUDENT NO"
                    SQLQuery = "SELECT ROWID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' WHEN '' THEN '' ELSE ' ' + EXTENSION_NAME END + ', ' + FIRSTNAME + ' ' + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN '' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,COURSECODE,YRLVL,SECTIONCODE,REGISTEREDDATE,REGISTEREDBY FROM TBL_STUDENT_INFORMATION INNER JOIN TBL_STUDENT_REGISTERED ON TBL_STUDENT_INFORMATION.STUDENT_NUMBER = TBL_STUDENT_REGISTERED.STUDENT_NUMBER WHERE TBL_STUDENT_REGISTERED.STUDENT_NUMBER LIKE @search AND TBL_STUDENT_REGISTERED.EDUCATION_LEVEL = @education_level AND TBL_STUDENT_REGISTERED.ACADEMIC_YEAR = @ay ORDER BY LASTNAME,FIRSTNAME ASC"
                Case "STUDENT NAME"
                    SQLQuery = "SELECT ROWID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' WHEN '' THEN '' ELSE ' ' + EXTENSION_NAME END + ', ' + FIRSTNAME + ' ' + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN '' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,COURSECODE,YRLVL,SECTIONCODE,REGISTEREDDATE,REGISTEREDBY FROM TBL_STUDENT_INFORMATION INNER JOIN TBL_STUDENT_REGISTERED ON TBL_STUDENT_INFORMATION.STUDENT_NUMBER = TBL_STUDENT_REGISTERED.STUDENT_NUMBER WHERE LASTNAME + FIRSTNAME LIKE @search AND TBL_STUDENT_REGISTERED.EDUCATION_LEVEL = @education_level AND TBL_STUDENT_REGISTERED.ACADEMIC_YEAR = @ay ORDER BY LASTNAME,FIRSTNAME ASC"
                Case "YEAR LEVEL"
                    SQLQuery = "SELECT ROWID,TBL_STUDENT_INFORMATION.STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' WHEN '' THEN '' ELSE ' ' + EXTENSION_NAME END + ', ' + FIRSTNAME + ' ' + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN '' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME,COURSECODE,YRLVL,SECTIONCODE,REGISTEREDDATE,REGISTEREDBY FROM TBL_STUDENT_INFORMATION INNER JOIN TBL_STUDENT_REGISTERED ON TBL_STUDENT_INFORMATION.STUDENT_NUMBER = TBL_STUDENT_REGISTERED.STUDENT_NUMBER WHERE YRLVL LIKE @search AND TBL_STUDENT_REGISTERED.EDUCATION_LEVEL = @education_level' AND TBL_STUDENT_REGISTERED.ACADEMIC_YEAR = @ay ORDER BY LASTNAME,FIRSTNAME ASC"
            End Select

            Using comm As New SqlCommand(SQLQuery, conn)
                comm.Parameters.AddWithValue("@education_level", "PRE ELEMENTARY")
                Select Case cmbFilter.Text
                    Case "STUDENT NO"
                        comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                    Case "STUDENT NAME"
                        comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                    Case "YEAR LEVEL"
                        comm.Parameters.AddWithValue("@search", txtSearch.Text)
                End Select

                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")

                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ROWID"), reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("YRLVL"), reader("SECTIONCODE"), reader("REGISTEREDDATE"), reader("REGISTEREDBY"))
                        StudentCount += 1
                    End While
                End Using
            End Using
            txtStudentCount.Text = StudentCount
        End Using
    End Sub

    Private Sub frm_registered_highschool_students_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbFilter.Text = "STUDENT NAME"
        LoadStudents()
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frm_registration_pre_elementary_entry
            .MdiParent = frm_main
            .EducationLevel = "PRE ELEMENTARY"
            .Show()
            .Top = 0
            .Left = 0
            Me.Close()
        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With frm_registration_pre_elementary_edit
            .DGRow = DGRow
            .RegID = DataGridView1.Rows(DGRow).Cells(0).Value
            .StudentNumber = DataGridView1.Rows(DGRow).Cells(1).Value
            .StudentName = DataGridView1.Rows(DGRow).Cells(2).Value
            .YrLvl = DataGridView1.Rows(DGRow).Cells(3).Value
            .SectionCode = DataGridView1.Rows(DGRow).Cells(4).Value
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadStudents()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub cmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilter.SelectedIndexChanged
        LoadStudents()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("EXEC sp_remove_student_registration @sn,@ay,@sem", conn)
                comm.Parameters.AddWithValue("@sn", DataGridView1.Rows(DGRow).Cells(0).Value)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                If MsgBox("Are you sure you want to Remove this student registration?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    comm.ExecuteNonQuery()
                    MsgBox("Registration,Assessment and fee loads has been successfully removed!", MsgBoxStyle.Information)
                    LoadStudents()
                End If
            End Using
        End Using
    End Sub
End Class