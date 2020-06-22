Public Class frm_faculty_subjects_entry

    Public Faculty_Id As String = String.Empty

    Private Sub Load_Subjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT SUBJ_CODE,SUBJ_DESC FROM tbl_settings_college_curriculum_subjects WHERE SUBJ_CODE + SUBJ_DESC LIKE @search ORDER BY SUBJ_CODE,SUBJ_DESC ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & StripSpaces(TextBox1.Text) & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("SUBJ_CODE").ToString.ToUpper, reader("SUBJ_DESC").ToString.ToUpper, "ADD TO LIST")
                    End While

                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_faculty_subjects_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Subjects()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.ColumnIndex = 2 Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()

                'GETTING ALL CURRICULUM_ID OF THE SELECTED SUBJECT
                Dim Curriculum_IDs As New List(Of Integer)
                Using comm As New SqlCommand("SELECT CURRICULUM_ID FROM tbl_settings_college_curriculum_subjects WHERE SUBJ_CODE = @subj_code", conn)
                    comm.Parameters.AddWithValue("@subj_code", DataGridView2.Rows(e.RowIndex).Cells(0).Value)
                    Curriculum_IDs.Clear()
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Curriculum_IDs.Add(reader("CURRICULUM_id"))
                        End While
                    End Using
                End Using

                For i = 0 To Curriculum_IDs.Count - 1
                    Using comm As New SqlCommand("SELECT * FROM TBL_FACULTY_PORTED_SUBJECTS WHERE SUBJ_ID = @subj_id AND FACULTY_ID = @faculty_id", conn)
                        comm.Parameters.AddWithValue("@subj_id", DataGridView2.Rows(e.RowIndex).Cells(0).Value)
                        comm.Parameters.AddWithValue("@faculty_id", Faculty_Id)
                        If Val(comm.ExecuteScalar) = 0 Then
                            Using comm1 As New SqlCommand("INSERT INTO TBL_FACULTY_PORTED_SUBJECTS VALUES(@faculty_id,@subj_id)", conn)
                                comm1.Parameters.AddWithValue("@faculty_id", Faculty_Id)
                                comm1.Parameters.AddWithValue("@subj_id", Curriculum_IDs(i))
                                comm1.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                Next
                MsgBox("Subject has been succesfully setted!", MsgBoxStyle.Information)
                'Using comm As New SqlCommand("SELECT * FROM TBL_FACULTY_PORTED_SUBJECTS WHERE SUBJ_ID = @subj_id AND FACULTY_ID = @faculty_id", conn)
                '    comm.Parameters.AddWithValue("@subj_id", DataGridView2.Rows(e.RowIndex).Cells(0).Value)
                '    comm.Parameters.AddWithValue("@faculty_id", Faculty_Id)
                '    If Val(comm.ExecuteScalar) = 0 Then
                '        Dim Added As Boolean = False
                '        Using comm1 As New SqlCommand("INSERT INTO TBL_FACULTY_PORTED_SUBJECTS VALUES(@faculty_id,@subj_id)", conn)
                '            comm1.Parameters.AddWithValue("@faculty_id", Faculty_Id)
                '            comm1.Parameters.AddWithValue("@subj_id", DataGridView2.Rows(e.RowIndex).Cells(0).Value)
                '            If MsgBox("Are you sure you want to this subject?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '                comm1.ExecuteNonQuery()
                '                Added = True
                '                MsgBox("Successfully addedd", MsgBoxStyle.Information)
                '            End If
                '        End Using

                '        'If Added = True Then
                '        '    Dim ID As String = String.Empty
                '        '    Using comm1 As New SqlCommand("SELECT @@IDENTITY", conn)
                '        '        ID = comm.ExecuteScalar
                '        '    End Using

                '        '    With frm_faculty_List.DataGridView2
                '        '        .Rows.Add(ID, DataGridView2.Rows(e.RowIndex).Cells(2).Value, DataGridView2.Rows(e.RowIndex).Cells(3).Value)
                '        '    End With
                '        'End If

                '    Else
                '        MsgBox("This subject is already existing to this faculty!", MsgBoxStyle.Critical)
                '    End If
                'End Using
            End Using
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Subjects()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class