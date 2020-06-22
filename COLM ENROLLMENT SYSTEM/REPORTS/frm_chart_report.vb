Public Class frm_chart_report

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim TotalStudents As Integer = 0
        Dim ListofCourses As New List(Of String)
        Academic_Year = "2017-2018"
        Academic_Sem = "2ND SEMESTER"
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT COURSE_CODE FROM FN_EnrolledStudents('COLLEGE',@ay,@sem) ORDER BY Course_Code ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        ListofCourses.Add(CStr(reader("Course_Code")))
                    End While
                End Using
            End Using
        End Using

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Chart1.Series("DEPARTMENT").Points.Clear()
            For i = 0 To ListofCourses.Count - 1
                Using comm As New SqlCommand("SELECT COUNT(*) AS StudentsCount FROM FN_EnrolledStudents('COLLEGE',@ay,@sem) WHERE Course_Code = @Course_Code", conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("Course_Code", ListofCourses(i).ToString)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Chart1.Series("DEPARTMENT").Points.AddXY(ListofCourses(i).ToString, CInt(reader("StudentsCount")))
                            TotalStudents += CInt(reader("StudentsCount"))
                        End While
                    End Using
                End Using
            Next
        End Using

        For i = 0 To Chart1.Series(0).Points.Count - 1
            Select Case Chart1.Series(0).Points(i).AxisLabel
                Case "BSCRIM"
                    Chart1.Series(0).Points(i).Color = Color.Maroon
                Case "BSIT"
                    Chart1.Series(0).Points(i).Color = Color.Silver
                Case "BSHM"
                    Chart1.Series(0).Points(i).Color = Color.Green
                Case "BSAT"
                    Chart1.Series(0).Points(i).Color = Color.Gold
                Case "BSBA"
                    Chart1.Series(0).Points(i).Color = Color.Yellow
                Case "BSRT"
                    Chart1.Series(0).Points(i).Color = Color.LightGray
                Case "BSTM"
                    Chart1.Series(0).Points(i).Color = Color.Green
            End Select
        Next
        lblTotalStudents.Text = "TOTAL STUDENTS: " & TotalStudents
    End Sub
End Class