Public Class frm_student_pull_out
    Public AssessmentID As Integer = 0

    Public Sub LoadEnrolledStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim DefaultQuery As String = String.Empty
            Dim NonCollegeQuery As String = "SELECT * FROM FN_EnrolledStudentsNonCollege(@EducationLevel,@ay) ORDER BY Lastname,Firstname ASC"
            Dim CollegeQuery As String = "SELECT * FROM FN_EnrolledStudentsCollege(@EducationLevel,@ay) ORDER BY Lastname,Firstname ASC"
            If cmbEducationLevel.Text <> "ALL" Then
                '                DefaultQuery = 
            Else
                DefaultQuery = "SELECT ID,TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER,(SELECT TOP 1 LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + EXTENSION_NAME END + ' ' + FIRSTNAME + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + MIDDLENAME END FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER) AS STUDENT_NAME,(SELECT TOP 1 GENDER FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER) AS GENDER,COURSE_CODE,YRLVL,SECT_CODE FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE PullOutStatus = 'ACTIVE' AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem AND EXISTS (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER AND FEE_STATUS = 'TUITION FEE' AND RECIEPT_STATUS = 'ACTIVE' AND STUDENT_NUMBER = TBL_COLLEGE_ASSESSMENT_SUMMARY.STUDENT_NUMBER HAVING SUM(AMOUNT_COLLECTED) >= 300) ORDER BY STUDENT_NAME ASC"
            End If
            Using comm As New SqlCommand(DefaultQuery, conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("STUDENT_NUMBER"), reader("STUDENT_NAME"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"), "SELECT")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtStudentNumber.Text = String.Empty Then
            MsgBox("Please select student first!", MsgBoxStyle.Critical)
            Exit Sub
        End If


        If MsgBox("Are you sure you want to pull out " & txtStudentName.Text & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("UPDATE TBL_COLLEGE_ASSESSMENT_SUMMARY SET PullOutStatus = 'INACTIVE' WHERE ID = @AssessmentID", conn)
                    comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                    Dim result As Integer = comm.ExecuteNonQuery
                    If result > 0 Then
                        MsgBox("Student has been successfully pull out!", MsgBoxStyle.Information)
                        Me.Close()
                        Me.Dispose()
                    End If
                End Using
            End Using
        Else
            Exit Sub
        End If
    End Sub

    Private Sub frm_student_pull_out_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbEducationLevel.Text = "ALL"
        LoadEnrolledStudents()
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadEnrolledStudents()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        LoadEnrolledStudents()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 6 Then
            AssessmentID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            txtStudentNumber.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            txtStudentName.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmbEducationLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEducationLevel.SelectedIndexChanged
        LoadEnrolledStudents()
    End Sub
End Class