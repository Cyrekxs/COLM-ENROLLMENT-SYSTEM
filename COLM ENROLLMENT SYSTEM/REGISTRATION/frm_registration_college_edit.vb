Public Class frm_registration_college_edit
    Public RegID As Integer = 0
    Public StudentNumber As String = String.Empty = String.Empty
    Public StudentName As String = String.Empty
    Public Course As String = String.Empty
    Public Yrlvl As String = String.Empty
    Public Sect_code As String = String.Empty
    Public DGRow As Integer = 0

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If cmbCourse.Text = String.Empty Then
            MsgBox("Please select course!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbYear.Text = String.Empty Then
            MsgBox("Please select year level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbSection.Text = String.Empty Then
            MsgBox("Please select section!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("UPDATE TBL_STUDENT_REGISTERED SET COURSECODE = @course_code,YRLVL = @yrlvl,SECTIONCODE = @sect_code WHERE ROWID = @rowid", conn)
                comm.Parameters.AddWithValue("@rowid", RegID)
                comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                comm.Parameters.AddWithValue("@sect_code", cmbSection.Text)
                If MsgBox("Are you sure you want to update the registration of this student?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    comm.ExecuteNonQuery()
                    With frm_registered_college_students.DataGridView1.Rows(DGRow)
                        .Cells(3).Value = cmbCourse.Text
                        .Cells(4).Value = cmbYear.Text
                        .Cells(5).Value = cmbSection.Text
                    End With
                    MsgBox("Registration has been successfully updated!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End If
            End Using
        End Using
    End Sub

    Private Sub frm_registration_college_edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Course_Codes(cmbCourse)
        txtStudentNumber.Text = StudentNumber
        txtStudentName.Text = StudentName
        cmbCourse.Text = Course
        cmbYear.Text = Yrlvl
        cmbSection.Text = Sect_code
    End Sub

    Private Sub cmbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse.SelectedIndexChanged
        Load_YearLvls("COLLEGE", cmbCourse.Text, cmbYear)
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        Load_Sections("COLLEGE", cmbCourse.Text, cmbYear.Text, cmbSection)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class