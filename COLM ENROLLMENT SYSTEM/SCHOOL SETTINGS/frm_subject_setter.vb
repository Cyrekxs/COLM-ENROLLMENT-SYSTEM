Public Class frm_subject_setter

    Public Sub Load_Setted_Subjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects_setted WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem AND LOAD_TYPE = 'REGULAR'", conn)
                comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                If Val(comm.ExecuteScalar) = 0 Then 'KUNG WALANG NAKITANG MGA SUBJECTS
                    Using comm1 As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_SEM = @sem", conn)
                        comm1.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                        comm1.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                        comm1.Parameters.AddWithValue("@ay", Academic_Year)
                        comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                        Using reader As SqlDataReader = comm1.ExecuteReader
                            DataGridView1.Rows.Clear()
                            While reader.Read
                                DataGridView1.Rows.Add("", reader("CURRICULUM_ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), Convert_To_Currency(reader("SUBJ_PRICE")), Convert_To_Currency(reader("Energy_Fee")), Convert_To_Currency(reader("Defence_Fee")), False)
                            End While
                        End Using
                    End Using

                Else 'KUNG MERONG NAKITANG MGA SUBJECTS
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            If reader("IS_DELETED") = False Then
                                DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), Convert_To_Currency(reader("SUBJ_PRICE")), Convert_To_Currency(reader("Energy_Fee")), Convert_To_Currency(reader("Defence_Fee")), True)
                            Else
                                DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), Convert_To_Currency(reader("SUBJ_PRICE")), Convert_To_Currency(reader("Energy_Fee")), Convert_To_Currency(reader("Defence_Fee")), False)
                            End If
                        End While
                    End Using
                End If
            End Using
        End Using

        Load_SubjectsAndAmount()
    End Sub

    Private Sub Load_SubjectsAndAmount()
        Dim Total_Amount As Double
        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(8).Value = True Then
                Total_Amount += DataGridView1.Rows(i).Cells(7).Value
            End If
        Next
        lblSubjectCount.Text = StripSpaces(DataGridView1.Rows.Count)
        lblAmount.Text = StripSpaces(Convert_To_Currency(Total_Amount))
    End Sub

    Private Sub frm_subject_setter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With DataGridView1
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        Load_Course_Codes(cmbCourse)
    End Sub

    Private Sub cmbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse.SelectedIndexChanged
        Load_YearLvls("COLLEGE", cmbCourse.Text, cmbYear)
        Load_Setted_Subjects()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        With frm_subject_setter_subject_list
            Load_Course_Codes(.cmbCourse)
            .cmbCourse.Text = cmbCourse.Text
            .cmbCourse.Enabled = False
            Load_YearLvls("COLLEGE", .cmbCourse.Text, .cmbYear)
            .cmbYear.Text = cmbYear.Text
            .Load_Subjects()
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        Load_Setted_Subjects()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim Subjects_Checked As Integer = 0
            For i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells(0).Value.ToString = String.Empty And DataGridView1.Rows(i).Cells(10).Value = True Then
                    'INSERT A NEW DATA
                    Using comm As New SqlCommand("INSERT INTO tbl_settings_college_curriculum_subjects_setted VALUES(@ay,@sem,@course_code,@yrlvl,@subj_id,@subj_code,@subj_desc,@subj_unit,@lec_hours,@lab_hours,@subj_price,@energy_fee,@defence_fee,'FALSE','REGULAR')", conn)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                        comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                        comm.Parameters.AddWithValue("@subj_id", DataGridView1.Rows(i).Cells(1).Value)
                        comm.Parameters.AddWithValue("@subj_code", DataGridView1.Rows(i).Cells(2).Value)
                        comm.Parameters.AddWithValue("@subj_desc", DataGridView1.Rows(i).Cells(3).Value)
                        comm.Parameters.AddWithValue("@subj_unit", DataGridView1.Rows(i).Cells(4).Value)
                        comm.Parameters.AddWithValue("@lec_hours", DataGridView1.Rows(i).Cells(5).Value)
                        comm.Parameters.AddWithValue("@lab_hours", DataGridView1.Rows(i).Cells(6).Value)
                        comm.Parameters.AddWithValue("@subj_price", DataGridView1.Rows(i).Cells(7).Value)
                        comm.Parameters.AddWithValue("@energy_fee", DataGridView1.Rows(i).Cells(8).Value)
                        comm.Parameters.AddWithValue("@defence_fee", DataGridView1.Rows(i).Cells(9).Value)
                        comm.ExecuteNonQuery()
                    End Using
                ElseIf DataGridView1.Rows(i).Cells(0).Value.ToString <> String.Empty And DataGridView1.Rows(i).Cells(10).Value = False Then
                    'DELETING EXISTING DATA
                    Using comm As New SqlCommand("DELETE tbl_settings_college_curriculum_subjects_setted WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", DataGridView1.Rows(i).Cells(0).Value)
                        Subjects_Checked += comm.ExecuteNonQuery()
                    End Using
                End If
            Next

            MsgBox("Subject has been successfully setted!", MsgBoxStyle.Information)
            Load_Setted_Subjects()
        End Using
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim Total_Amount As Double
        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(8).Value = True Then
                Total_Amount += DataGridView1.Rows(i).Cells(7).Value
            End If
        Next
        lblSubjectCount.Text = DataGridView1.Rows.Count
        lblAmount.Text = Convert_To_Currency(Total_Amount)
    End Sub
End Class