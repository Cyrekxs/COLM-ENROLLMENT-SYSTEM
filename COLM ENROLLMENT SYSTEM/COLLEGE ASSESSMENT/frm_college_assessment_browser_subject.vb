Public Class frm_college_assessment_browser_subject
    Public RegistrationID As Integer = 0
    Public IsBridgeSubject As String = String.Empty
    Public CourseCode As String = String.Empty
    Private Sub LoadSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim SQLQuery As String = String.Empty
            If IsBridgeSubject = "YES" Then
                SQLQuery = "SELECT DISTINCT MAIN.RegisteredSubjID,MAIN.SubjCode,SubjDesc,SubjUnit,SUB.Course_Code,SUB.Subj_Price,SUB.Energy_Fee,SUB.Defence_Fee FROM FN_College_RegisteredSubjects(@RegistrationID) AS MAIN INNER JOIN tbl_settings_college_curriculum_subjects_setted AS SUB ON MAIN.SubjCode = SUB.Subj_COde WHERE SUB.Academic_Year = @ay AND SUB.Academic_Sem = @sem AND RegisteredSubjID IS NOT NULL AND SubjCode + SubjDesc LIKE @Search AND SUB.IsBridgeSubject = 'YES'"
            ElseIf IsBridgeSubject = "NO" Then
                SQLQuery = "SELECT DISTINCT MAIN.RegisteredSubjID,MAIN.SubjCode,SubjDesc,SubjUnit,SUB.Course_Code,SUB.Subj_Price,SUB.Energy_Fee,SUB.Defence_Fee FROM FN_College_RegisteredSubjects(@RegistrationID) AS MAIN INNER JOIN tbl_settings_college_curriculum_subjects_setted AS SUB ON MAIN.SubjCode = SUB.Subj_COde WHERE SUB.Academic_Year = @ay AND SUB.Academic_Sem = @sem AND RegisteredSubjID IS NOT NULL AND SubjCode + SubjDesc LIKE @Search AND SUB.IsBridgeSubject = 'NO'"
            End If

            Using comm As New SqlCommand(SQLQuery, conn)
                comm.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@Search", TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("RegisteredSubjID"),
                                              reader("Course_Code"),
                                               reader("SubjCode"),
                                               reader("SubjDesc"),
                                               reader("SubjUnit"),
                                              Convert_To_Currency(reader("Subj_Price")),
                                              Convert_To_Currency(reader("Energy_Fee")),
                                             Convert_To_Currency(reader("Defence_Fee")))
                    End While
                End Using
            End Using
        End Using
        TextBox2.Text = DataGridView1.Rows.Count
    End Sub
    Private Sub frm_college_assessment_browser_subject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSubjects()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadSubjects()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadSubjects()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 8 Then
            Dim IsSubjectExists As Boolean = False
            With frm_college_assessment_entry

                For i = 0 To .DGTuition.Rows.Count - 1
                    If .DGTuition.Rows(i).Cells(0).Value = DataGridView1.Rows(e.RowIndex).Cells(0).Value Then
                        IsSubjectExists = True
                        Exit For
                    End If
                Next

                If IsSubjectExists = False Then
                    'TUITION FEE TAB
                    .DGTuition.Rows.Add(DataGridView1.Rows(e.RowIndex).Cells(0).Value,
                              DataGridView1.Rows(e.RowIndex).Cells(2).Value,
                              DataGridView1.Rows(e.RowIndex).Cells(3).Value,
                              DataGridView1.Rows(e.RowIndex).Cells(4).Value,
                              DataGridView1.Rows(e.RowIndex).Cells(5).Value,
                              DataGridView1.Rows(e.RowIndex).Cells(6).Value,
                              DataGridView1.Rows(e.RowIndex).Cells(7).Value, "NO")
                    'SCHEDULE TAB
                    If IsBridgeSubject = "NO" Then
                        .DGSched.Rows.Add(DataGridView1.Rows(e.RowIndex).Cells(0).Value,
                                  DataGridView1.Rows(e.RowIndex).Cells(2).Value,
                                  DataGridView1.Rows(e.RowIndex).Cells(3).Value)
                    ElseIf IsBridgeSubject = "YES" Then
                        .DGSched.Rows.Add(DataGridView1.Rows(e.RowIndex).Cells(0).Value,
                                  DataGridView1.Rows(e.RowIndex).Cells(2).Value,
                                  DataGridView1.Rows(e.RowIndex).Cells(3).Value, "-", "-", "-", "-", "-", "-")

                    End If

                Else
                    MsgBox("Subject is already there!", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End With
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        With frm_college_assessment_browser_subject_new_curriculum
            .Student_Number = frm_college_assessment_entry.txtStudentNo.Text
            .RegistrationID = Me.RegistrationID
            .Course_Code = Me.CourseCode
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
        Me.Close()
        Me.Dispose()
    End Sub
End Class