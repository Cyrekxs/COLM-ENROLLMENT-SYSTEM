Public Class frm_settings_college_tuition
    Public CurriculumID As Integer = 0
    Public CurriculumCode As String = String.Empty
    Public YearLevel As String = String.Empty
    Public CurriculumType As String = String.Empty
    Private CourseCode As String = String.Empty
    Public SavingStatus As SavingOptions

    Private Sub GetCurriculumID()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT CurriculumID FROM tbl_settings_college_curriculum WHERE CurriculumCode = @CurriculumCode AND CurriculumType = @CurriculumType", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", cmbCurriculumCode.Text)
                comm.Parameters.AddWithValue("@CurriculumType", cmbCurriculumType.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        CurriculumID = reader("CurriculumID")
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadCurriculumCodes()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT CurriculumCode FROM tbl_settings_college_curriculum ORDER BY CurriculumCode ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCurriculumCode.Items.Clear()
                    While reader.Read
                        cmbCurriculumCode.Items.Add(reader("CurriculumCode"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadCurriculumType()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT CurriculumType FROM tbl_settings_college_curriculum WHERE CurriculumCode = @CurriculumCode ORDER BY CurriculumType ASC", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", cmbCurriculumCode.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCurriculumType.Items.Clear()
                    While reader.Read
                        cmbCurriculumType.Items.Add(reader("CurriculumType"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub GetCourse()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT CurriculumCourse FROM tbl_settings_college_curriculum WHERE CurriculumCode = @CurriculumCode AND CurriculumType = @CurriculumType", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", cmbCurriculumCode.Text)
                comm.Parameters.AddWithValue("@CurriculumType", cmbCurriculumType.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        CourseCode = reader("CurriculumCourse")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadYrLevels()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT YearLevel FROM FN_Settings_Curriculum() WHERE CurriculumCode = @CurriculumCode AND CurriculumType = @CurriculumType AND CurriculumCourse = @CourseCode ORDER BY YearLevel ASC", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", cmbCurriculumCode.Text)
                comm.Parameters.AddWithValue("@CurriculumType", cmbCurriculumType.Text)
                comm.Parameters.AddWithValue("@CourseCode", CourseCode)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbYrLevel.Items.Clear()
                    While reader.Read
                        cmbYrLevel.Items.Add(reader("YearLevel"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadDefaultCurriculumSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects WHERE Curriculum_Type = @CurriculumType AND Course_Code = @CourseCode AND YrLvl = @Yrlvl AND Academic_Sem = @Academic_Sem", conn)
                comm.Parameters.AddWithValue("@CurriculumType", cmbCurriculumType.Text)
                comm.Parameters.AddWithValue("@CourseCode", CourseCode)
                comm.Parameters.AddWithValue("@Yrlvl", cmbYrLevel.Text)
                comm.Parameters.AddWithValue("@Academic_Sem", Academic_Sem)
                DataGridView1.Rows.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView1.Rows.Add(0,
                                               reader("Curriculum_ID"),
                                               reader("Subj_Code"),
                                               reader("Subj_Desc"),
                                               reader("Lec_Hours"),
                                               reader("Lab_Hours"),
                                               reader("Subj_Unit"),
                                               Convert_To_Currency(reader("Subj_Price")),
                                               Convert_To_Currency(reader("Energy_Fee")),
                                               Convert_To_Currency(reader("Defence_Fee")),
                                               reader("IsBridgeSubject"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadSettedCurriculumSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects_setted WHERE CurriculumID = @CurriculumID  AND Yrlvl = @Yrlvl AND Is_Deleted = 'FALSE' AND Academic_Year = @ay AND Academic_Sem = @sem ORDER BY ID ASC", conn)
                comm.Parameters.AddWithValue("@CurriculumID", CurriculumID)
                comm.Parameters.AddWithValue("@yrlvl", cmbYrLevel.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"),
                                               reader("Subj_ID"),
                                               reader("Subj_Code"),
                                               reader("Subj_Desc"),
                                               reader("Lec_Hours"),
                                               reader("Lab_Hours"),
                                               reader("Subj_Unit"),
                                               Convert_To_Currency(reader("Subj_Price")),
                                               Convert_To_Currency(reader("Energy_Fee")),
                                               Convert_To_Currency(reader("Defence_Fee")),
                                               reader("IsBridgeSubject"))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub GetDefaultMiscellanouesFee()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_fee_lists ORDER BY FeeID ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(0, reader("FeeCode"), Convert_To_Currency(reader("FeeAmount")))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadSettedMiscellaneousFee()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_fees WHERE CurriculumID = @CurriculumID AND Yrlvl = @yrlvl AND Fee_Status = 'EXTERNAL' AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY ID ASC", conn)
                comm.Parameters.AddWithValue("@CurriculumID", CurriculumID)
                comm.Parameters.AddWithValue("@yrlvl", cmbYrLevel.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"), reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_settings_college_tuition_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frm_settings_college_tuition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCurriculumCodes()
        If SavingStatus = SavingOptions.NEW Then
            GetDefaultMiscellanouesFee()
        ElseIf SavingStatus = SavingOptions.EDIT Then
            cmbCurriculumCode.Text = CurriculumCode
            cmbCurriculumType.Text = CurriculumType
            cmbYrLevel.Text = YearLevel
            cmbCurriculumCode.Enabled = False
            cmbCurriculumType.Enabled = False
            cmbYrLevel.Enabled = False
            LoadSettedMiscellaneousFee()
            LoadSettedCurriculumSubjects()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_settings_college_tuition_add_subjects
            .CourseCode = CourseCode
            .CurriculumType = cmbCurriculumType.Text
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
        CalculateTotals()
    End Sub

    Private Sub cmbYrLevel_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbYrLevel.SelectionChangeCommitted
        LoadDefaultCurriculumSubjects()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cmbCurriculumCode.Text = String.Empty Then
            MsgBox("Please select curriculum code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbCurriculumType.Text = String.Empty Then
            MsgBox("Please select curriculum type!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbYrLevel.Text = String.Empty Then
            MsgBox("Please select year level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If SavingStatus = SavingOptions.NEW Then
            GetCurriculumID()
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    For i = 0 To DataGridView1.Rows.Count - 1
                        If DataGridView1.Rows(i).Cells(0).Value = 0 Then
                            Using comm As New SqlCommand("INSERT INTO tbl_settings_college_curriculum_subjects_setted VALUES(@CurriculumID,@ay,@sem,@coursecode,@yrlvl,@subj_id,@subj_code,@subj_desc,@subj_unit,@lec_hours,@lab_hours,@subj_price,@energy_fee,@defence_fee,@BridgeSubject,'FALSE','REGULAR')", conn, t)
                                comm.Parameters.AddWithValue("@CurriculumID", CurriculumID)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.Parameters.AddWithValue("@coursecode", CourseCode)
                                comm.Parameters.AddWithValue("@yrlvl", cmbYrLevel.Text)
                                comm.Parameters.AddWithValue("@subj_id", DataGridView1.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@subj_code", DataGridView1.Rows(i).Cells(2).Value)
                                comm.Parameters.AddWithValue("@subj_desc", DataGridView1.Rows(i).Cells(3).Value)
                                comm.Parameters.AddWithValue("@lec_hours", DataGridView1.Rows(i).Cells(4).Value)
                                comm.Parameters.AddWithValue("@lab_hours", DataGridView1.Rows(i).Cells(5).Value)
                                comm.Parameters.AddWithValue("@subj_Unit", DataGridView1.Rows(i).Cells(6).Value)
                                comm.Parameters.AddWithValue("@subj_price", DataGridView1.Rows(i).Cells(7).Value)
                                comm.Parameters.AddWithValue("@energy_fee", DataGridView1.Rows(i).Cells(8).Value)
                                comm.Parameters.AddWithValue("@defence_fee", DataGridView1.Rows(i).Cells(9).Value)
                                comm.Parameters.AddWithValue("@BridgeSubject", DataGridView1.Rows(i).Cells(10).Value)
                                comm.ExecuteNonQuery()
                            End Using
                        ElseIf DataGridView1.Rows(i).Cells(0).Value <> 0 Then
                            Using comm As New SqlCommand("UPDATE tbl_settings_college_curriculum_subjects_setted SET Subj_Price = @subj_price, Energy_Fee = @energy_fee, Defence_Fee = @defence_fee WHERE ID = @SettedSubjectID", conn, t)
                                comm.Parameters.AddWithValue("@SettedSubjectID", DataGridView1.Rows(i).Cells(0).Value)
                                comm.Parameters.AddWithValue("@subj_Unit", DataGridView1.Rows(i).Cells(6).Value)
                                comm.Parameters.AddWithValue("@subj_price", DataGridView1.Rows(i).Cells(7).Value)
                                comm.Parameters.AddWithValue("@energy_fee", DataGridView1.Rows(i).Cells(8).Value)
                                comm.Parameters.AddWithValue("@defence_fee", DataGridView1.Rows(i).Cells(9).Value)
                                comm.ExecuteNonQuery()
                            End Using
                        End If
                    Next

                    GetCourse()

                    For i = 0 To DataGridView2.Rows.Count - 1
                        If DataGridView2.Rows(i).Cells(0).Value = 0 Then

                            Using comm As New SqlCommand("SELECT * FROM tbl_settings_fees WHERE Course_Code = @Course_Code AND Yrlvl = @Yrlvl AND Academic_Yr = @ay AND Academic_Sem = @sem AND Fee_Code = @FeeCode", conn, t)
                                comm.Parameters.AddWithValue("@Course_Code", CourseCode)
                                comm.Parameters.AddWithValue("@Yrlvl", cmbYrLevel.Text)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.Parameters.AddWithValue("@FeeCode", DataGridView2.Rows(i).Cells(1).Value)
                                Using reader As SqlDataReader = comm.ExecuteReader
                                    While reader.Read

                                    End While
                                End Using

                            End Using

                            Using comm As New SqlCommand("INSERT INTO tbl_settings_fees VALUES (@Course_Code,@Yrlvl,@Fee_Code,@Fee_Amount,@Fee_Type,@Fee_Status,@Education_Level,@CurriculumID,@ay,@sem)", conn, t)
                                comm.Parameters.AddWithValue("@Course_Code", CourseCode)
                                comm.Parameters.AddWithValue("@Yrlvl", cmbYrLevel.Text)
                                comm.Parameters.AddWithValue("@Fee_Code", DataGridView2.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@Fee_Amount", DataGridView2.Rows(i).Cells(2).Value)
                                comm.Parameters.AddWithValue("@Fee_Type", "MISCELLANEOUS FEE")
                                comm.Parameters.AddWithValue("@Fee_Status", "EXTERNAL")
                                comm.Parameters.AddWithValue("@Education_Level", "COLLEGE")
                                comm.Parameters.AddWithValue("@CurriculumID", CurriculumID)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.ExecuteNonQuery()
                            End Using
                        ElseIf DataGridView2.Rows(i).Cells(0).Value <> 0 Then
                            Using comm As New SqlCommand("UPDATE tbl_settings_fees SET Fee_Amount = @Fee_Amount WHERE ID = @SettedFeeID", conn, t)
                                comm.Parameters.AddWithValue("@SettedFeeID", DataGridView2.Rows(i).Cells(0).Value)
                                comm.Parameters.AddWithValue("@Fee_Amount", DataGridView2.Rows(i).Cells(2).Value)
                                comm.ExecuteNonQuery()
                            End Using
                        End If
                    Next
                    t.Commit()
                    MsgBox("Tuition fee has been successfully setted!", MsgBoxStyle.Information)
                Catch ex As Exception
                    t.Rollback()
                    MsgBox("An error occured while processing data please try again" & vbNewLine & "Exception error: " & ex.Message, MsgBoxStyle.Critical)
                End Try
            End Using
        End Using
    End Sub

    Private Sub cmbCurriculumCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurriculumCode.SelectedIndexChanged
        LoadCurriculumType()
    End Sub

    Private Sub cmbCurriculumType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurriculumType.SelectedIndexChanged
        GetCourse()
        If SavingStatus = SavingOptions.NEW Then
            GetCurriculumID()
        End If
        LoadYrLevels()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 11 Then
            If DataGridView1.Rows(e.RowIndex).Cells(0).Value <> 0 Then
                If MsgBox("Are you sure you want to remove this subject?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Using conn As New SqlConnection(StringConnection)
                        conn.Open()
                        Using t As SqlTransaction = conn.BeginTransaction
                            Using comm As New SqlCommand("UPDATE tbl_settings_college_curriculum_subjects_setted SET Is_Deleted = 'TRUE' WHERE ID = @ID", conn, t)
                                comm.Parameters.AddWithValue("@ID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                                comm.ExecuteNonQuery()
                            End Using

                            t.Commit()
                            MsgBox("Subject has been successfully removed!", MsgBoxStyle.Information)
                            DataGridView1.Rows.Remove(DataGridView1.Rows(e.RowIndex))
                        End Using
                    End Using
                End If
            ElseIf DataGridView1.Rows(e.RowIndex).Cells(0).Value = 0 Then
                If MsgBox("Are you sure you want to remove this subject?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    DataGridView1.Rows.Remove(DataGridView1.Rows(e.RowIndex))
                End If
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With frm_settings_college_miscellaneous_add
            .CurriculumID = CurriculumID
            .CourseCode = CourseCode
            .Yrlvl = cmbYrLevel.Text
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            LoadSettedMiscellaneousFee()
            LoadSettedCurriculumSubjects()
        End With
        CalculateTotals()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.ColumnIndex = 3 Then
            If MsgBox("Are you sure you want remove this fee?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                DataGridView2.Rows.Remove(DataGridView2.Rows(e.RowIndex))
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        CalculateTotals()
    End Sub

    Private Sub CalculateTotals()
        Dim TFee As Double = 0
        Dim EnergyFee As Double = 0
        Dim MFee As Double = 0

        For i = 0 To DataGridView1.Rows.Count - 1
            TFee += DataGridView1.Rows(i).Cells(clmTFee.Index).Value
            EnergyFee += DataGridView1.Rows(i).Cells(clmEnergyFee.Index).Value
        Next

        For i = 0 To DataGridView2.Rows.Count - 1
            MFee += DataGridView2.Rows(i).Cells(clmMFee.Index).Value
        Next

        txtTFee.Text = Convert_To_Currency(TFee)
        txtEnergyFee.Text = Convert_To_Currency(EnergyFee)
        txtMFee.Text = Convert_To_Currency(MFee)


    End Sub

    Private Sub cmbYrLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYrLevel.SelectedIndexChanged
        CalculateTotals()
    End Sub
End Class