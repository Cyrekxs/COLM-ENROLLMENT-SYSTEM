Public Class frm_deans_lister
    Public Sub LoadStudents(DG As DataGridView, ay As String, sem As String, count As TextBox)
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_DeansLister_Candidates(@ay,@sem) ORDER BY StudentName ASC", conn)
                comm.Parameters.AddWithValue("@ay", ay)
                comm.Parameters.AddWithValue("@sem", sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG.Rows.Clear()
                    While reader.Read
                        DG.Rows.Add(reader("Student_Number"), reader("StudentName"), reader("TotalUnit"), reader("TotalGrade"), reader("TotalAverage"))
                    End While
                End Using
            End Using
        End Using
        count.Text = DG.Rows.Count
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadStudents(DataGridView1, cmbAy1.Text, cmbSem1.Text, txtCount1)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LoadStudents(DataGridView2, cmbAy2.Text, cmbSem2.Text, txtCount2)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim PossibleStudents As Integer = 0
        Dim ListofStudentNumbers As New List(Of String)
        Dim ListofNames As New List(Of String)
        Dim ListofAverage As New List(Of Double)
        Dim ListofCourse As New List(Of String)
        Dim ListofYearLevels As New List(Of String)
        For x = 0 To DataGridView1.Rows.Count - 1
            For y = 0 To DataGridView2.Rows.Count - 1
                If DataGridView1.Rows(x).Cells(0).Value = DataGridView2.Rows(y).Cells(0).Value Then
                    ListofStudentNumbers.Add(DataGridView1.Rows(x).Cells(0).Value)
                    ListofNames.Add(DataGridView1.Rows(x).Cells(1).Value)
                    ListofAverage.Add((CDbl(DataGridView1.Rows(x).Cells(4).Value) + CDbl(DataGridView2.Rows(y).Cells(4).Value)) / 2)
                    PossibleStudents += 1
                    Exit For
                End If
            Next
        Next

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            For i = 0 To ListofStudentNumbers.Count - 1
                Using comm As New SqlCommand("SELECT DISTINCT Course_Code FROM tbl_college_assessment_summary WHERE Student_Number = @sn", conn)
                    comm.Parameters.AddWithValue("@sn", ListofStudentNumbers(i))
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            ListofCourse.Add(reader("Course_Code"))
                        End While
                    End Using
                End Using
            Next

            For i = 0 To ListofStudentNumbers.Count - 1
                Using comm As New SqlCommand("SELECT TOP 1 * FROM tbl_college_assessment_summary WHERE Student_number = @sn ORDER BY ID DESC", conn)
                    comm.Parameters.AddWithValue("@sn", ListofStudentNumbers(i))
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        If reader.HasRows = True Then
                            While reader.Read
                                ListofYearLevels.Add(reader("Yrlvl"))
                            End While
                        Else
                            'Using conn1 As New SqlConnection(StringConnection)
                            '    conn1.Open()
                            '    Using comm1 As New SqlCommand("SELECT * FROM FN_EnrolledStudents('COLLEGE',@ay,@sem) WHERE Student_number = @sn", conn1)
                            '        comm1.Parameters.AddWithValue("@sn", ListofStudentNumbers(i))
                            '        comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            '        comm1.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                            '        Using reader1 As SqlDataReader = comm1.ExecuteReader
                            '            While reader1.Read
                            '                ListofYearLevels.Add(reader1("Yrlvl"))
                            '            End While
                            '        End Using
                            '    End Using
                            'End Using
                        End If
                    End Using
                End Using
            Next
        End Using

        frm_deans_lister_candidates.DataGridView1.Rows.Clear()
        For i = 0 To PossibleStudents - 1
            With frm_deans_lister_candidates
                .DataGridView1.Rows.Add(ListofStudentNumbers(i), ListofNames(i), ListofCourse(i), ListofYearLevels(i), ListofAverage(i))
            End With
        Next

        With frm_deans_lister_candidates
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With

    End Sub

    Private Sub frm_deans_lister_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub
End Class