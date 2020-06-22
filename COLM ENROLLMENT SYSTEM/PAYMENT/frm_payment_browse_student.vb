Public Class frm_payment_browse_student
    Public DGRow As Integer = 0
    Public Sub LoadAssessedStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If frm_payment.EducationLevel = "COLLEGE" Then
                Using comm As New SqlCommand("SELECT * FROM FN_College_AssessedStudents() WHERE Education_Level = @education_level AND Academic_Yr = @ay AND Academic_Sem = @sem AND StudentName LIKE @search ORDER BY StudentName", conn)
                    comm.Parameters.AddWithValue("@search", "%" & StripSpaces(TextBox1.Text) & "%")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@education_level", frm_payment.EducationLevel)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add(reader("AssessmentID"), reader("STUDENT_NUMBER"), reader("Studentname"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"))
                        End While
                    End Using
                End Using
            Else
                Using comm As New SqlCommand("SELECT * FROM FN_College_AssessedStudents() WHERE Education_Level = @education_level AND StudentName LIKE @search AND Academic_Yr = @ay ORDER BY StudentName", conn)
                    comm.Parameters.AddWithValue("@search", "%" & StripSpaces(TextBox1.Text) & "%")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@education_level", frm_payment.EducationLevel)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add(reader("AssessmentID"), reader("STUDENT_NUMBER"), reader("Studentname"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECT_CODE"))
                        End While
                    End Using
                End Using
            End If

        End Using
    End Sub

    Private Sub frm_payment_browse_student_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAssessedStudents()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadAssessedStudents()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadAssessedStudents()
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        With frm_payment
            .AssessmentID = DataGridView1.Rows(DGRow).Cells(0).Value
            .txtStudentNumber.Text = DataGridView1.Rows(DGRow).Cells(1).Value
            .txtStudentName.Text = DataGridView1.Rows(DGRow).Cells(2).Value
            .txtCourse.Text = DataGridView1.Rows(DGRow).Cells(3).Value
            .txtYear.Text = DataGridView1.Rows(DGRow).Cells(4).Value
            .txtSection.Text = DataGridView1.Rows(DGRow).Cells(5).Value
            .Load_Assessment_Payment()
            .LoadSOA()
            .LoadSOA_Summary()
            .LoadPayments()
            Me.Close()
            Me.Dispose()
        End With
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            With frm_payment
                .AssessmentID = DataGridView1.Rows(DGRow).Cells(0).Value
                .txtStudentNumber.Text = DataGridView1.Rows(DGRow).Cells(1).Value
                .txtStudentName.Text = DataGridView1.Rows(DGRow).Cells(2).Value
                .txtCourse.Text = DataGridView1.Rows(DGRow).Cells(3).Value
                .txtYear.Text = DataGridView1.Rows(DGRow).Cells(4).Value
                .txtSection.Text = DataGridView1.Rows(DGRow).Cells(5).Value
                .Load_Assessment_Payment()
                .LoadSOA()
                .LoadSOA_Summary()
                .LoadPayments()
                Me.Close()
                Me.Dispose()
            End With
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub
End Class