Public Class student_grade_report_browse_student
    Dim DGRow As Integer = 0
    Dim StringConnection_ProjectCOLM As String = "Server=COLM\SQLEXPRESS;Database=PROJECT_COLM;User Id=sa;Password=sa;"

    Public Sub LoadStudents()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT TOP 100 * FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER + LASTNAME + ' ' + FIRSTNAME + ' ' + MIDDLENAME LIKE @search AND EDUCATION_LEVEL = 'COLLEGE' AND STUDENT_NUMBER <> 'N.A' ORDER BY LASTNAME,FIRSTNAME ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("STUDENT_NUMBER"), reader("LASTNAME") & ", " & reader("FIRSTNAME") & " " & reader("MIDDLENAME"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadStudents()
        'Dim ThreadLoadStudents As New System.Threading.Thread(AddressOf LoadStudents)
        'ThreadLoadStudents.Start()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            With student_grade_report
                .txtSN.Text = DataGridView1.Rows(DGRow).Cells(0).Value
                .txtStudName.Text = DataGridView1.Rows(DGRow).Cells(1).Value
                '.txtCourse.Text = DataGridView1.Rows(DGRow).Cells(2).Value
                '.txtYear.Text = DataGridView1.Rows(DGRow).Cells(3).Value
                '.txtSection.Text = DataGridView1.Rows(DGRow).Cells(4).Value
                Me.Close()
                Me.Dispose()
            End With
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub student_grade_report_browse_student_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        LoadStudents()
        'Dim ThreadLoadStudents As New System.Threading.Thread(AddressOf LoadStudents)
        'ThreadLoadStudents.Start()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'Using conn As New SqlConnection(StringConnection_ProjectCOLM)
        '    conn.Open()
        '    Using comm As New SqlCommand("SELECT TOP 100 * FROM TBL_STUDENT_INFORMATION WHERE STUDENT_NUMBER + LASTNAME + ' ' + FIRSTNAME + ' ' + MIDDLENAME LIKE @search AND EDUCATION_LEVEL = 'COLLEGE' AND STUDENT_NUMBER <> 'N.A' ORDER BY LASTNAME,FIRSTNAME ASC", conn)
        '        comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
        '        Using reader As SqlDataReader = comm.ExecuteReader
        '            DataGridView1.Rows.Clear()
        '            While reader.Read
        '                DataGridView1.Rows.Add(reader("STUDENT_NUMBER"), reader("LASTNAME") & ", " & reader("FIRSTNAME") & " " & reader("MIDDLENAME"))
        '            End While
        '        End Using
        '    End Using
        'End Using
    End Sub
End Class