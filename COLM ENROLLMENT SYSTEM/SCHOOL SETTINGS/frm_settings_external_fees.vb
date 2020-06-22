Public Class frm_settings_external_fees
    Public Saving_Status As String = String.Empty
    Public ID As Integer = 0
    Public DGRow As Integer = 0
    Public Sub Clear_Controls()
        cmbEducationLevel.SelectedIndex = -1
        cmbCourseCode.SelectedIndex = -1
        cmbYear.SelectedIndex = -1
        cmbFeeType.SelectedIndex = -1
        txtFeeCode.Text = ""
        txtAmount.Text = "0.00"
    End Sub

    Public Sub Enable_Controls()
        cmbEducationLevel.Enabled = True
        cmbCourseCode.Enabled = True
        cmbYear.Enabled = True
        cmbFeeType.Enabled = True
        txtFeeCode.Enabled = True
        txtAmount.Enabled = True
        btnNew.Enabled = False
        btnSAve.Enabled = True
        btnCancel.Enabled = True
        txtSearch.Enabled = False
        DataGridView3.Enabled = False
    End Sub

    Public Sub Disable_Controls()
        cmbEducationLevel.Enabled = False
        cmbCourseCode.Enabled = False
        cmbYear.Enabled = False
        cmbFeeType.Enabled = False
        txtFeeCode.Enabled = False
        txtAmount.Enabled = False
        btnNew.Enabled = True
        btnSAve.Enabled = False
        btnCancel.Enabled = False
        txtSearch.Enabled = True
        DataGridView3.Enabled = True
    End Sub


    Public Sub Load_Internal_Fees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE FEE_STATUS = 'EXTERNAL' AND FEE_CODE LIKE @search AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem ORDER BY EDUCATION_LEVEL,COURSE_CODE,YRLVL,FEE_TYPE,FEE_CODE ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                DataGridView3.Rows.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView3.Rows.Add(reader("ID"), reader("EDUCATION_LEVEL"), reader("COURSE_CODE"), reader("YRLVL"), reader("FEE_TYPE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")))
                    End While
                End Using
            End Using
        End Using
    End Sub


    Private Sub btnSAve_Click(sender As Object, e As EventArgs) Handles btnSAve.Click
        If cmbEducationLevel.Text = String.Empty Then
            MsgBox("Please select education level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbCourseCode.Text = String.Empty Then
            MsgBox("Please select course code", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbYear.Text = String.Empty Then
            MsgBox("Please select year level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtFeeCode.Text = String.Empty Then
            MsgBox("Please enter unique fee code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtAmount.Text = String.Empty Then
            MsgBox("Please enter amount!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If IsNumeric(txtAmount.Text) = False Then
            MsgBox("Invalid input on amount!", MsgBoxStyle.Critical)
            Exit Sub
        Else
            If txtAmount.Text <= 0 Then
                MsgBox("Please enter amount greater than 0!", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        If Saving_Status = "NEW" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE FEE_CODE = @fee_code AND FEE_TYPE = @fee_type AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                    comm.Parameters.AddWithValue("@fee_code", txtFeeCode.Text)
                    comm.Parameters.AddWithValue("@fee_type", cmbFeeType.Text)
                    comm.Parameters.AddWithValue("@fee_amount", txtAmount.Text)
                    comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_FEES VALUES(@course_code,@yrlvl,@fee_code,@fee_amount,@fee_type,'EXTERNAL',@education_level,@ay,@sem)", conn)
                            comm1.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                            comm1.Parameters.AddWithValue("@fee_code", txtFeeCode.Text)
                            comm1.Parameters.AddWithValue("@fee_type", cmbFeeType.Text)
                            comm1.Parameters.AddWithValue("@fee_amount", txtAmount.Text)
                            comm1.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            If MsgBox("Are you sure you want to save this save?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New Fee has been successfully recorded!", MsgBoxStyle.Information)
                                Load_Internal_Fees()
                                Disable_Controls()
                                Clear_Controls()
                            End If
                        End Using
                    End If
                End Using
            End Using
        ElseIf Saving_Status = "EDIT" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE FEE_CODE = @fee_code AND FEE_TYPE = @fee_type AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", ID)
                    comm.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                    comm.Parameters.AddWithValue("@fee_code", txtFeeCode.Text)
                    comm.Parameters.AddWithValue("@fee_type", cmbFeeType.Text)
                    comm.Parameters.AddWithValue("@fee_amount", txtAmount.Text)
                    comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_FEES SET COURSE_CODE = @course_code, YRLVL = @yrlvl,FEE_CODE = @fee_code,FEE_TYPE = @fee_type,FEE_AMOUNT = @fee_amount,EDUCATION_LEVEL = @education_level WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", ID)
                            comm1.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                            comm1.Parameters.AddWithValue("@fee_code", txtFeeCode.Text)
                            comm1.Parameters.AddWithValue("@fee_type", cmbFeeType.Text)
                            comm1.Parameters.AddWithValue("@fee_amount", txtAmount.Text)
                            comm1.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            If MsgBox("Are you sure you want to save this save?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Fee has been successfully updated!", MsgBoxStyle.Information)
                                Load_Internal_Fees()
                                Disable_Controls()
                                Clear_Controls()
                            End If
                        End Using
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub cmbEducationLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEducationLevel.SelectedIndexChanged
        If cmbEducationLevel.Text = "COLLEGE" Then
            Load_Course_Codes(cmbCourseCode)
            cmbCourseCode.Enabled = True
        ElseIf cmbEducationLevel.Text = "JUNIOR HIGH" Then
            cmbCourseCode.Items.Clear()
            cmbCourseCode.Items.Add("JUNIOR HIGH")
            cmbCourseCode.Text = "JUNIOR HIGH"
            cmbCourseCode.Enabled = False
        ElseIf cmbEducationLevel.Text = "SENIOR HIGH" Then
            cmbCourseCode.Items.Clear()
            cmbCourseCode.Enabled = True
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SENIOR_COURSES ORDER BY COURSE_CODE ASC", conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            cmbCourseCode.Items.Add(reader("COURSE_CODE"))
                        End While
                    End Using
                End Using
            End Using
        End If
    End Sub

    Private Sub cmbCourseCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourseCode.SelectedIndexChanged
        Load_YearLvls(cmbEducationLevel.Text, cmbCourseCode.Text, cmbYear)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Saving_Status = "NEW"
        Enable_Controls()
        Clear_Controls()
    End Sub

    Private Sub frm_settings_external_fees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Disable_Controls()
        Clear_Controls()
        Load_Internal_Fees()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ID = DataGridView3.Rows(DGRow).Cells(0).Value
        Saving_Status = "EDIT"
        Enable_Controls()

        cmbEducationLevel.Text = DataGridView3.Rows(DGRow).Cells(1).Value
        cmbCourseCode.Text = DataGridView3.Rows(DGRow).Cells(2).Value
        cmbYear.Text = DataGridView3.Rows(DGRow).Cells(3).Value
        cmbFeeType.Text = DataGridView3.Rows(DGRow).Cells(4).Value
        txtFeeCode.Text = DataGridView3.Rows(DGRow).Cells(5).Value
        txtAmount.Text = DataGridView3.Rows(DGRow).Cells(6).Value
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub DataGridView3_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.RowEnter
        DGRow = e.RowIndex
    End Sub
End Class