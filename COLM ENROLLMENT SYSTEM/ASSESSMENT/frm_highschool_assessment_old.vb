Public Class frm_highschool_assessment_old
    '    Public StringConnection As String = "Server=COLM\SQLEXPRESS;Database=PROGRAM_TEST_2;User Id=sa;Password=sa;"
    Public Sub LoadStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT *,(SELECT TOP 1 Lastname  + ' ' + Firstname + ' ' + Middlename FROM tbl_student_information WHERE Student_Number = tbl_college_assessment_summary.student_number) AS Student_Name FROM tbl_college_assessment_summary WHERE Academic_YR = @ay AND Academic_Sem = @sem AND (SELECT TOP 1 Lastname  + ' ' + Firstname + ' ' + Middlename FROM tbl_student_information WHERE Student_Number = tbl_college_assessment_summary.student_number) LIKE @search AND EDUCATION_LEVEL = @EducationLevel ORDER BY STUDENT_NAME ASC", conn)
                comm.Parameters.AddWithValue("@ay", txtAcademicYear.Text)
                comm.Parameters.AddWithValue("@sem", txtAcademicSem.Text)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@EducationLevel", cmbEducationLevel.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("Student_Number"), reader("Student_Name"), reader("Education_Level"), reader("Course_Code"), reader("YrLvl"), reader("Sect_Code"), reader("Voucher_Code"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudents()
    End Sub

    Private Sub cmbEducationLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEducationLevel.SelectedIndexChanged
        LoadStudents()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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
End Class
