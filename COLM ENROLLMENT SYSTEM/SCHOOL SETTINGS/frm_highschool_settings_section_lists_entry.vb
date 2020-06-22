Public Class frm_highschool_settings_section_lists_entry
    Public SectionId As Integer = 0
    Public SavingStatus As String = String.Empty

    Public Sub EnableControls()
        GroupBox1.Enabled = True
        GroupBox2.Enabled = False
        btnAdd.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
    End Sub

    Public Sub DisableControls()
        GroupBox1.Enabled = False
        GroupBox2.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
    End Sub

    Public Sub ClearControls()
        cmbEntryEducationLevel.SelectedIndex = -1
        cmbEntryCourse.SelectedIndex = -1
        cmbEntryYearLevel.SelectedIndex = -1
        txtEntrySection.Text = String.Empty
    End Sub

    Public Sub LoadSections()
        Dim SQLQuery As String = String.Empty

        If cmbFilterEducationLevel.Text = "ALL" Then
            SQLQuery = "SELECT * FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL <> 'COLLEGE' AND Academic_Year = @ay AND Academic_Sem = @sem ORDER BY EDUCATION_LEVEL,COURSE_CODE,YRLVL,SECTION_CODE ASC"
        Else
            SQLQuery = "SELECT * FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL = @education_level AND Academic_Year = @ay AND Academic_Sem = @sem ORDER BY EDUCATION_LEVEL,COURSE_CODE,YRLVL,SECTION_CODE ASC"
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand(SQLQuery, conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@education_level", cmbFilterEducationLevel.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("EDUCATION_LEVEL"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECTION_CODE"), "EDIT", "DELETE")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_highschool_settings_section_lists_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisableControls()
        ClearControls()
        cmbFilterEducationLevel.Text = "ALL"
        LoadSections()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cmbEntryEducationLevel.Text = String.Empty Or cmbEntryCourse.Text = String.Empty Or txtEntrySection.Text = String.Empty Then
            MsgBox("Please fill up all fields!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL = @education_level AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code", conn)
                    comm.Parameters.AddWithValue("@education_level", cmbEntryEducationLevel.Text)
                    comm.Parameters.AddWithValue("@course_code", cmbEntryCourse.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbEntryYearLevel.Text)
                    comm.Parameters.AddWithValue("@section_code", txtEntrySection.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_SECTIONS VALUES(@education_level,@course_code,NULL,@yrlvl,@section_code,@ay,@sem)", conn)
                            comm1.Parameters.AddWithValue("@education_level", cmbEntryEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@course_code", cmbEntryCourse.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbEntryYearLevel.Text)
                            comm1.Parameters.AddWithValue("@section_code", txtEntrySection.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@seM", Academic_Sem)
                            If MsgBox("Are you sure you want to save this section?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New Section has been successfully saved!", MsgBoxStyle.Information)
                                DisableControls()
                                ClearControls()
                                LoadSections()
                            End If
                        End Using
                    Else
                        MsgBox("Already exists!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf SavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL = @education_level AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", SectionId)
                    comm.Parameters.AddWithValue("@education_level", cmbEntryEducationLevel.Text)
                    comm.Parameters.AddWithValue("@course_code", cmbEntryCourse.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbEntryYearLevel.Text)
                    comm.Parameters.AddWithValue("@section_code", txtEntrySection.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_SECTIONS SET EDUCATION_LEVEL =  @education_level, COURSE_CODE = @course_code, YRLVL = @yrlvl, SECTION_CODE = @section_code WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", SectionId)
                            comm1.Parameters.AddWithValue("@education_level", cmbEntryEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@course_code", cmbEntryCourse.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbEntryYearLevel.Text)
                            comm1.Parameters.AddWithValue("@section_code", txtEntrySection.Text)
                            If MsgBox("Are you sure you want to update this section?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Section has been successfully updated!", MsgBoxStyle.Information)
                                DisableControls()
                                ClearControls()
                                LoadSections()
                            End If
                        End Using
                    Else
                        MsgBox("Already used!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        SavingStatus = "NEW"
        ClearControls()
        EnableControls()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 5 Then ' edit
            SavingStatus = "EDIT"
            EnableControls()
            SectionId = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            cmbEntryEducationLevel.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            cmbEntryCourse.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            cmbEntryYearLevel.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            txtEntrySection.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        End If
    End Sub

    Private Sub cmbEntryEducationLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEntryEducationLevel.SelectedIndexChanged
        If cmbEntryEducationLevel.Text = "SENIOR HIGH" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SENIOR_COURSES ORDER BY COURSE_CODE ASC", conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        cmbEntryCourse.Items.Clear()
                        While reader.Read
                            cmbEntryCourse.Items.Add(reader("COURSE_CODE"))
                        End While
                    End Using
                End Using
            End Using
            cmbEntryCourse.Enabled = True
        Else
            cmbEntryCourse.Enabled = False
            cmbEntryCourse.Items.Add(cmbEntryEducationLevel.Text)
            cmbEntryCourse.Text = cmbEntryEducationLevel.Text
        End If
    End Sub

    Private Sub cmbEntryCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEntryCourse.SelectedIndexChanged
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE EDUCATION_LEVEL = @education_level ORDER BY YEAR_CODE ASC", conn)
                comm.Parameters.AddWithValue("@education_level", cmbEntryEducationLevel.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbEntryYearLevel.Items.Clear()
                    While reader.Read
                        cmbEntryYearLevel.Items.Add(reader("YEAR_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DisableControls()
            ClearControls()
            LoadSections()
        End If
    End Sub

    Private Sub cmbFilterEducationLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterEducationLevel.SelectedIndexChanged
        LoadSections()
    End Sub
End Class