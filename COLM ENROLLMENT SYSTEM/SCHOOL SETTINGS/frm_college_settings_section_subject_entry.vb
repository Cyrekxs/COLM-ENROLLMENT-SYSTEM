Public Class frm_college_settings_section_subject_entry

    Private Sub LoadSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_Subjects_Not_Listed_In_Schedule(@CourseCode,@Yrlvl,@ay,@sem)", conn)
                comm.Parameters.AddWithValue("@CourseCode", txtCourseCode.Text)
                comm.Parameters.AddWithValue("@Yrlvl", txtYearLevel.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("Subj_Code"), reader("Subj_Desc"), reader("Subj_Unit"), reader("Lec_Hours"), reader("Lab_Hours"), reader("Subj_Price"), "ADD TO LIST")
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_college_settings_section_subject_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSubjects()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        LoadSubjects()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 7 Then
            With frm_college_settings_section_entry_list
                .DataGridView2.Rows.Add()
            End With
        End If
    End Sub
End Class