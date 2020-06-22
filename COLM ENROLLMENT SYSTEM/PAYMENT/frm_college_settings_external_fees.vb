Public Class frm_college_settings_external_fees
    Public MID As Integer = 0
    Public OID As Integer = 0
    Public MDGRow As Integer = 0
    Public ODGRow As Integer = 0
    Public MSavingStatus As String = String.Empty
    Public OSavingStatus As String = String.Empty

    Public Sub MEnableControls()
        grpMEntry.Enabled = True
        grpMLists.Enabled = False

        btnMNew.Enabled = False
        btnMEdit.Enabled = False
        btnMDelete.Enabled = False
        btnMSave.Enabled = True
        btnMCancel.Enabled = True
    End Sub

    Public Sub MDisableControls()
        grpMEntry.Enabled = False
        grpMLists.Enabled = True

        btnMNew.Enabled = True
        btnMEdit.Enabled = True
        btnMDelete.Enabled = True
        btnMSave.Enabled = False
        btnMCancel.Enabled = False
    End Sub

    Public Sub MClearControls()
        cmbMCourseCode.SelectedIndex = -1
        cmbMYrLvl.SelectedIndex = -1
        txtMFeeCode.Text = String.Empty
        txtMFeeAmount.Text = Convert_To_Currency(0)
    End Sub

    Public Sub MLoadFees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE EDUCATION_LEVEL = 'COLLEGE' AND FEE_STATUS = 'EXTERNAL' AND FEE_TYPE = 'MISCELLANEOUS FEE' ORDER BY COURSE_CODE,YRLVL,ACADEMIC_SEM,FEE_CODE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("COURSE_CODE"), reader("YRLVL"), reader("ACADEMIC_SEM"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub OEnableControls()
        grpOEntry.Enabled = True
        grpOLists.Enabled = False

        btnONew.Enabled = False
        btnOEdit.Enabled = False
        btnODelete.Enabled = False
        btnOSave.Enabled = True
        btnOCancel.Enabled = True
    End Sub

    Public Sub ODisableControls()
        grpOEntry.Enabled = False
        grpOLists.Enabled = True

        btnONew.Enabled = True
        btnOEdit.Enabled = True
        btnODelete.Enabled = True
        btnOSave.Enabled = False
        btnOCancel.Enabled = False
    End Sub

    Public Sub OClearControls()
        cmbOCourseCode.SelectedIndex = -1
        cmbOYrLvl.SelectedIndex = -1
        txtOFeeCode.Text = String.Empty
        txtOFeeAmount.Text = Convert_To_Currency(0)
    End Sub

    Public Sub OLoadFees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE EDUCATION_LEVEL = 'COLLEGE' AND FEE_STATUS = 'EXTERNAL' AND FEE_TYPE = 'OTHER FEE' ORDER BY COURSE_CODE,YRLVL,ACADEMIC_SEM,FEE_CODE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"), reader("COURSE_CODE"), reader("YRLVL"), reader("ACADEMIC_SEM"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnMNew_Click(sender As Object, e As EventArgs) Handles btnMNew.Click
        MSavingStatus = "NEW"
        MEnableControls()
        MClearControls()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        MDGRow = e.RowIndex
    End Sub

    Private Sub DataGridView2_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.RowEnter
        ODGRow = e.RowIndex
    End Sub

    Private Sub btnONew_Click(sender As Object, e As EventArgs) Handles btnONew.Click
        OSavingStatus = "NEW"
        OEnableControls()
        OClearControls()
    End Sub

    Private Sub btnMEdit_Click(sender As Object, e As EventArgs) Handles btnMEdit.Click
        MSavingStatus = "EDIT"
        MEnableControls()
        MID = DataGridView1.Rows(MDGRow).Cells(0).Value
        cmbMCourseCode.Text = DataGridView1.Rows(MDGRow).Cells(1).Value
        cmbMYrLvl.Text = DataGridView1.Rows(MDGRow).Cells(2).Value
        cmbMSemester.Text = DataGridView1.Rows(MDGRow).Cells(3).Value
        txtMFeeCode.Text = DataGridView1.Rows(MDGRow).Cells(4).Value
        txtMFeeAmount.Text = DataGridView1.Rows(MDGRow).Cells(5).Value
    End Sub

    Private Sub btnOEdit_Click(sender As Object, e As EventArgs) Handles btnOEdit.Click
        OSavingStatus = "EDIT"
        OEnableControls()
        OID = DataGridView1.Rows(MDGRow).Cells(0).Value
        cmbOCourseCode.Text = DataGridView2.Rows(ODGRow).Cells(1).Value
        cmbOYrLvl.Text = DataGridView2.Rows(ODGRow).Cells(2).Value
        cmbOSemester.Text = DataGridView2.Rows(ODGRow).Cells(3).Value
        txtOFeeCode.Text = DataGridView2.Rows(ODGRow).Cells(4).Value
        txtOFeeAmount.Text = DataGridView2.Rows(ODGRow).Cells(5).Value
    End Sub

    Private Sub btnMSave_Click(sender As Object, e As EventArgs) Handles btnMSave.Click

        If cmbMCourseCode.Text = String.Empty Then
            MsgBox("Please select Course Code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbMYrLvl.Text = String.Empty Then
            MsgBox("Please select Year Level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbMSemester.Text = String.Empty Then
            MsgBox("Please select Semester!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtMFeeCode.Text = String.Empty Then
            MsgBox("Please enter Fee Code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtMFeeAmount.Text = String.Empty Then
            MsgBox("Please enter Fee Amount!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If MSavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_SEM = @sem AND FEE_CODE = @fee_code AND FEE_TYPE = 'MISCELLANEOUS FEE' AND EDUCATION_LEVEL = 'COLLEGE'", conn)
                    comm.Parameters.AddWithValue("@course_code", cmbMCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbMYrLvl.Text)
                    comm.Parameters.AddWithValue("@sem", cmbMSemester.Text)
                    comm.Parameters.AddWithValue("@fee_code", StripSpaces(txtMFeeCode.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_FEES VALUES(@course_code,@yrlvl,@fee_code,@fee_amount,'MISCELLANEOUS FEE','EXTERNAL','COLLEGE',@ay,@sem)", conn)
                            comm1.Parameters.AddWithValue("@course_code", cmbMCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbMYrLvl.Text)
                            comm1.Parameters.AddWithValue("@fee_code", StripSpaces(txtMFeeCode.Text))
                            comm1.Parameters.AddWithValue("@fee_amount", txtMFeeAmount.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", cmbMSemester.Text)
                            If MsgBox("Are you sure you want to save this Miscellaneous Fee?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New Miscellaneous Fee has been successfully saved!", MsgBoxStyle.Information)
                                MDisableControls()
                                MClearControls()
                                MLoadFees()
                            End If
                        End Using
                    Else
                        MsgBox("Miscellaneous Fee Information is already exists!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf MSavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_SEM = @sem AND FEE_CODE = @fee_code AND FEE_TYPE = 'MISCELLANEOUS FEE' AND EDUCATION_LEVEL = 'COLLEGE' AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", MID)
                    comm.Parameters.AddWithValue("@course_code", cmbMCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbMYrLvl.Text)
                    comm.Parameters.AddWithValue("@sem", cmbMSemester.Text)
                    comm.Parameters.AddWithValue("@fee_code", StripSpaces(txtMFeeCode.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_FEES SET COURSE_CODE = @course_code, YRLVL = @yrlvl, FEE_CODE = @fee_code, FEE_AMOUNT = @fee_amount, ACADEMIC_SEM = @sem WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", MID)
                            comm1.Parameters.AddWithValue("@course_code", cmbMCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbMYrLvl.Text)
                            comm1.Parameters.AddWithValue("@fee_code", StripSpaces(txtMFeeCode.Text))
                            comm1.Parameters.AddWithValue("@fee_amount", txtMFeeAmount.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", cmbMSemester.Text)
                            If MsgBox("Are you sure you want to update this Miscellaneous Fee?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Miscellaneous Fee has been successfully updated!", MsgBoxStyle.Information)
                                MDisableControls()
                                MClearControls()
                                MLoadFees()
                            End If
                        End Using
                    Else
                        MsgBox("Miscellaneous Fee Information is already used!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub

    Private Sub frm_college_settings_external_fees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Course_Codes(cmbMCourseCode)
        Load_Course_Codes(cmbOCourseCode)
        MDisableControls()
        ODisableControls()
        MLoadFees()
        OLoadFees()
    End Sub

    Private Sub cmbMCourseCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMCourseCode.SelectedIndexChanged
        Load_YearLvls("COLLEGE", cmbMCourseCode.Text, cmbMYrLvl)
    End Sub

    Private Sub cmbOCourseCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOCourseCode.SelectedIndexChanged
        Load_YearLvls("COLLEGE", cmbOCourseCode.Text, cmbOYrLvl)
    End Sub

    Private Sub btnOSave_Click(sender As Object, e As EventArgs) Handles btnOSave.Click
        If cmbOCourseCode.Text = String.Empty Then
            MsgBox("Please select Course Code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbOYrLvl.Text = String.Empty Then
            MsgBox("Please select Year Level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbOSemester.Text = String.Empty Then
            MsgBox("Please select Semester!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtOFeeCode.Text = String.Empty Then
            MsgBox("Please enter Fee Code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtOFeeAmount.Text = String.Empty Then
            MsgBox("Please enter Fee Amount!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If OSavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_SEM = @sem AND FEE_CODE = @fee_code AND FEE_TYPE = 'OTHER FEE' AND EDUCATION_LEVEL = 'COLLEGE'", conn)
                    comm.Parameters.AddWithValue("@course_code", cmbOCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbOYrLvl.Text)
                    comm.Parameters.AddWithValue("@sem", cmbOSemester.Text)
                    comm.Parameters.AddWithValue("@fee_code", StripSpaces(txtOFeeCode.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_FEES VALUES(@course_code,@yrlvl,@fee_code,@fee_amount,'OTHER FEE','EXTERNAL','COLLEGE',@ay,@sem)", conn)
                            comm1.Parameters.AddWithValue("@course_code", cmbOCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbOYrLvl.Text)
                            comm1.Parameters.AddWithValue("@fee_code", StripSpaces(txtOFeeCode.Text))
                            comm1.Parameters.AddWithValue("@fee_amount", txtOFeeAmount.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", cmbOSemester.Text)
                            If MsgBox("Are you sure you want to save this Other Fee?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New Other Fee has been successfully saved!", MsgBoxStyle.Information)
                                ODisableControls()
                                OClearControls()
                                OLoadFees()
                            End If
                        End Using
                    Else
                        MsgBox("Other Fee Information is already exists!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf OSavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_SEM = @sem AND FEE_CODE = @fee_code AND FEE_TYPE = 'OTHER FEE' AND EDUCATION_LEVEL = 'COLLEGE' AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", OID)
                    comm.Parameters.AddWithValue("@course_code", cmbOCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbOYrLvl.Text)
                    comm.Parameters.AddWithValue("@sem", cmbOSemester.Text)
                    comm.Parameters.AddWithValue("@fee_code", StripSpaces(txtOFeeCode.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_FEES SET COURSE_CODE = @course_code, YRLVL = @yrlvl, FEE_CODE = @fee_code, FEE_AMOUNT = @fee_amount, ACADEMIC_SEM = @sem WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", OID)
                            comm1.Parameters.AddWithValue("@course_code", cmbOCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbOYrLvl.Text)
                            comm1.Parameters.AddWithValue("@fee_code", StripSpaces(txtOFeeCode.Text))
                            comm1.Parameters.AddWithValue("@fee_amount", txtOFeeAmount.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", cmbOSemester.Text)
                            If MsgBox("Are you sure you want to update this Other Fee?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Other Fee has been successfully updated!", MsgBoxStyle.Information)
                                ODisableControls()
                                OClearControls()
                                OLoadFees()
                            End If
                        End Using
                    Else
                        MsgBox("Other Fee Information is already used!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub
End Class