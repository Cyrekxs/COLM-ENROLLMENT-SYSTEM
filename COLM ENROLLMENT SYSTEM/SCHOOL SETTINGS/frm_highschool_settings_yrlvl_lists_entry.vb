Public Class frm_highschool_settings_yrlvl_lists_entry
    Public YrlvlID As Integer = 0
    Public SavingStatus As String = String.Empty

    Public Sub EnableControls()
        cmbEntryEducationLevel.Enabled = True
        txtEntryYrlvl.Enabled = True
        GroupBox2.Enabled = False
        btnAdd.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
    End Sub

    Public Sub DisableControls()
        cmbEntryEducationLevel.Enabled = False
        txtEntryYrlvl.Enabled = False
        GroupBox2.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
    End Sub

    Public Sub ClearControls()
        cmbEntryEducationLevel.SelectedIndex = -1
        txtEntryYrlvl.Text = String.Empty
    End Sub

    Public Sub LoadYrLvls()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim SQLQuery As String = String.Empty

            If cmbFilterEducationLevel.Text = "ALL" Then
                SQLQuery = "SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE EDUCATION_LEVEL <> 'COLLEGE' ORDER BY EDUCATION_LEVEL,YEAR_CODE ASC"
            Else
                SQLQuery = "SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE EDUCATION_LEVEL = @education_level ORDER BY EDUCATION_LEVEL,YEAR_CODE ASC"
            End If

            Using comm As New SqlCommand(SQLQuery, conn)
                comm.Parameters.AddWithValue("@education_level", cmbFilterEducationLevel.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("YEAR_ID"), reader("EDUCATION_LEVEL"), reader("YEAR_CODE"), "EDIT", "DELETE")
                    End While
                End Using
            End Using

        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE EDUCATION_LEVEL = @education_level AND YEAR_CODE = @yrlvl", conn)
                    comm.Parameters.AddWithValue("@education_level", cmbEntryEducationLevel.Text)
                    comm.Parameters.AddWithValue("@yrlvl", txtEntryYrlvl.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_COLLEGE_YEARLEVELS VALUES(0,@course_code,@yrlvl,@education_level)", conn)
                            comm1.Parameters.AddWithValue("@course_code", cmbEntryEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", txtEntryYrlvl.Text)
                            comm1.Parameters.AddWithValue("@education_leveL", cmbEntryEducationLevel.Text)
                            If MsgBox("Are you sure you want to save this year level?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New Year Level has been successfully saved!", MsgBoxStyle.Information)
                                DisableControls()
                                ClearControls()
                                LoadYrLvls()
                            End If
                        End Using
                    Else
                        MsgBox("Already exists!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf SavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE EDUCATION_LEVEL = @education_level AND YEAR_CODE = @yrlvl AND YEAR_ID <> @year_id", conn)
                    comm.Parameters.AddWithValue("@year_id", YrlvlID)
                    comm.Parameters.AddWithValue("@education_level", cmbEntryEducationLevel.Text)
                    comm.Parameters.AddWithValue("@yrlvl", txtEntryYrlvl.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_COLLEGE_YEARLEVELS SET COURSE_CODE = @course_code, YEAR_CODE = @yrlvl, EDUCATION_LEVEL = @education_level WHERE YEAR_ID = @year_id", conn)
                            comm1.Parameters.AddWithValue("@year_id", YrlvlID)
                            comm1.Parameters.AddWithValue("@course_code", cmbEntryEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", txtEntryYrlvl.Text)
                            comm1.Parameters.AddWithValue("@education_leveL", cmbEntryEducationLevel.Text)
                            If MsgBox("Are you sure you want to update this year level?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New Year Level has been successfully updated!", MsgBoxStyle.Information)
                                DisableControls()
                                ClearControls()
                                LoadYrLvls()
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
        EnableControls()
        SavingStatus = "NEW"
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DisableControls()
            ClearControls()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 3 Then 'EDIT
            YrlvlID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            cmbEntryEducationLevel.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            txtEntryYrlvl.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            EnableControls()
            SavingStatus = "EDIT"
        ElseIf e.ColumnIndex = 4 Then 'DELETE

        End If
    End Sub

    Private Sub frm_highschool_settings_yrlvl_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbFilterEducationLevel.Text = "ALL"
        LoadYrLvls()
        DisableControls()
    End Sub

    Private Sub cmbFilterEducationLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterEducationLevel.SelectedIndexChanged
        LoadYrLvls()
    End Sub
End Class