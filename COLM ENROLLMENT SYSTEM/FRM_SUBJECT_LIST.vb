Public Class frm_subject_list

    Public Sub Load_Subjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_CURRICULUM WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SUBJ_CODE + SUBJ_DESC LIKE @search", conn)
                comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("CURRICULUM_ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), Convert_To_Currency(reader("SUBJ_PRICE")), "ADD")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_subject_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With DataGridView1
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 7 Then
            Dim Inserted_Id As String = String.Empty
            Dim Verification As String = "CAN ADD"

            For i = 0 To frm_subject_setter.DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(e.RowIndex).Cells(1).Value = frm_subject_setter.DataGridView1.Rows(i).Cells(2).Value Then
                    Verification = "CANT ADD"
                    Exit For
                    Exit Sub
                End If
            Next


            'Using conn As New SqlConnection(StringConnection)
            '    conn.Open()
            '    Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SUBJECTS_SETTED WHERE SUBJ_CODE = @subj_code AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
            '        comm.Parameters.AddWithValue("@subj_code", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
            '        comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
            '        comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
            '        comm.Parameters.AddWithValue("@ay", Academic_Year)
            '        comm.Parameters.AddWithValue("@sem", Academic_Sem)
            '        If Val(comm.ExecuteScalar) = 0 Then
            '            Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_SUBJECTS_SETTED VALUES(@subj_code,@course_code,@yrlvl,@ay,@sem)", conn)
            '                comm1.Parameters.AddWithValue("@subj_code", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
            '                comm1.Parameters.AddWithValue("@course_code", cmbCourse.Text)
            '                comm1.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
            '                comm1.Parameters.AddWithValue("@ay", Academic_Year)
            '                comm1.Parameters.AddWithValue("@sem", Academic_Sem)
            '                If MsgBox("Are you sure you want to set this subjects?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '                    comm1.ExecuteNonQuery()
            '                End If
            '            End Using

            '            Using comm1 As New SqlCommand("SELECT @@IDENTITY", conn)
            '                Inserted_Id = comm1.ExecuteScalar
            '            End Using
            '        End If
            '    End Using
            'End Using

            If Verification = "CAN ADD" Then
                With frm_subject_setter.DataGridView1
                    .Rows.Add("", DataGridView1.Rows(e.RowIndex).Cells(0).Value, DataGridView1.Rows(e.RowIndex).Cells(1).Value, DataGridView1.Rows(e.RowIndex).Cells(2).Value, DataGridView1.Rows(e.RowIndex).Cells(3).Value, DataGridView1.Rows(e.RowIndex).Cells(4).Value, DataGridView1.Rows(e.RowIndex).Cells(5).Value, DataGridView1.Rows(e.RowIndex).Cells(6).Value, True)
                End With
            Else
                MsgBox("Subject is already in the list!", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub cmbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Subjects()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        Load_Subjects()
    End Sub
End Class