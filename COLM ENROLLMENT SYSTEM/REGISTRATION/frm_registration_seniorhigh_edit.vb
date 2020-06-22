Public Class frm_registration_seniorhigh_edit
    Public RegID As Integer = 0
    Public StudentNumber As String = String.Empty
    Public StudentName As String = String.Empty
    Public Course As String = String.Empty
    Public YrLvl As String = String.Empty
    Public SectionCode As String = String.Empty
    Public DGRow As Integer = 0
    Public Sub LoadSeniorHighCourses()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SENIOR_COURSES ORDER BY COURSE_CODE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCourse.Items.Clear()
                    While reader.Read
                        cmbCourse.Items.Add(reader("COURSE_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_registration_seniorhigh_edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSeniorHighCourses()
        txtStudentNumber.Text = StudentNumber
        txtStudentName.Text = StudentName
        cmbCourse.Text = Course
        cmbYear.Text = YrLvl
        cmbSection.Text = SectionCode
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        Load_Sections("SENIOR HIGH", cmbCourse.Text, cmbYear.Text, cmbSection)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If cmbCourse.Text = String.Empty Then
            MsgBox("Please select Course!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbYear.Text = String.Empty Then
            MsgBox("Please select Year Level!", MsgBoxStyle.Critical)
            Exit Sub
        End If


        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("UPDATE TBL_STUDENT_REGISTERED SET CourseCode = @Course,YrLvl = @yrlvl, SectionCode = @Section_Code WHERE ROWID = @RegID", conn)
                comm.Parameters.AddWithValue("@RegID", RegID)
                comm.Parameters.AddWithValue("@Course", cmbCourse.Text)
                comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                comm.Parameters.AddWithValue("@section_code", cmbSection.Text)
                If MsgBox("Are you sure you want to update this Student Registration?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    comm.ExecuteNonQuery()

                    With frm_registered_seniorhigh_students.DataGridView1.Rows(DGRow)
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class