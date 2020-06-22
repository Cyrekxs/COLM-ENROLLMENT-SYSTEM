Public Class student_lists_junior_high

    Public DGRow As Integer = 0
    Public Conn As New SqlConnection(StringConnection)

    Public Sub LoadCollegeStudents()
        Using comm As New SqlCommand("SELECT * FROM FN_StudentInformation() WHERE Education_Level = @EducationLevel AND Student_Name LIKE @search ORDER BY Student_Name ASC", Conn)
            comm.Parameters.AddWithValue("@EducationLevel", "JUNIOR HIGH")
            comm.Parameters.AddWithValue("@search", TextBox1.Text & "%")
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView1.Rows.Clear()
                While reader.Read

                    'Dim birthmonth As Integerpin = 0

                    'Select Case reader("B_MONTH")
                    '    Case "JANUARY" : birthmonth = 1
                    '    Case "FEBRUARY" : birthmonth = 2
                    '    Case "MARCH" : birthmonth = 3
                    '    Case "APRIL" : birthmonth = 4
                    '    Case "MAY" : birthmonth = 5
                    '    Case "JUNE" : birthmonth = 6
                    '    Case "JULY" : birthmonth = 7
                    '    Case "AUGUST" : birthmonth = 8
                    '    Case "SEPTEMBER" : birthmonth = 9
                    '    Case "OCTOBER" : birthmonth = 10
                    '    Case "NOVEMBER" : birthmonth = 11
                    '    Case "DECEMBER" : birthmonth = 12
                    'End Select

                    'Dim Birthdate As DateTime = birthmonth & "/" & reader("B_DAY") & "/" & reader("B_YEAR")

                    DataGridView1.Rows.Add(reader("ID"),
                                           reader("Student_Number"),
                                           "",
                                           reader("Student_Name"),
                                           reader("Mobile"),
                                           LSet(reader("Gender"), 1),
                                           Format(CDate(reader("BirthDate")), "MM-dd-yyyy"),
                                           GetAge(CDate(reader("BirthDate"))), reader("Guardian_Name"),
                                           reader("Guardian_Mobile"),
                                           reader("Address"))
                End While
                TextBox2.Text = DataGridView1.Rows.Count
            End Using
        End Using
    End Sub

    Private Sub students_lists_college_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Conn.Open()
        'BackgroundWorker1.RunWorkerAsync()
        LoadCollegeStudents()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        LoadCollegeStudents()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With student_information_entry
            .EducationLevel = "JUNIOR HIGH"
        End With

        student_information_student_type.txtEducationLevel.Text = "JUNIOR HIGH"
        student_information_student_type.StartPosition = FormStartPosition.CenterParent
        student_information_student_type.ShowDialog()

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With student_information_entry
            .SavingStatus = "EDIT"
            .ID = DataGridView1.Rows(DGRow).Cells(0).Value
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("DELETE FROM TBL_STUDENT_INFORMATION WHERE ID = @id", conn)
                comm.Parameters.AddWithValue("@id", DataGridView1.Rows(DGRow).Cells(0).Value)
                If MsgBox("Are you sure you want to delete this Student Information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    comm.ExecuteNonQuery()
                    MsgBox("Student Information has been successfully deleted!", MsgBoxStyle.Information)
                    DataGridView1.Rows.Remove(DataGridView1.Rows(DGRow))
                End If
            End Using
        End Using
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        With student_information_entry

        End With
    End Sub
End Class
