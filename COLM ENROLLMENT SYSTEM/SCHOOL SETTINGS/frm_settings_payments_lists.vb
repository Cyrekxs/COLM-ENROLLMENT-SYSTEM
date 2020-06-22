Public Class frm_settings_payments_lists
    Public SavingStatus As String = String.Empty
    Public DGRow As Integer = 0
    Public Sub EnableControls()
        GroupBox1.Enabled = False
        GroupBox2.Enabled = True
        If SavingStatus = "NEW" Then
            btnGenerate.Enabled = True
        ElseIf SavingStatus = "EDIT" Then
            btnGenerate.Enabled = False
        End If
    End Sub

    Public Sub DisableControls()
        GroupBox1.Enabled = True
        GroupBox2.Enabled = False
    End Sub

    Public Sub ClearControls()
        cmbEducationLevel.SelectedIndex = -1
        txtPaymentType.Text = String.Empty
        txtNoOfPayments.Text = 0
        DataGridView2.Rows.Clear()
    End Sub

    Public Sub LoadPaymentSettings()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT ASSESSMENT_TYPE,EDUCATION_LEVEL FROM TBL_SETTINGS_ASSESSMENTS ORDER BY EDUCATION_LEVEL,ASSESSMENT_TYPE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("EDUCATION_LEVEL"), reader("ASSESSMENT_TYPE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_settings_payments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPaymentSettings()
        DisableControls()
        ClearControls()
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        DataGridView2.Rows.Clear()
        For i = 1 To CInt(txtNoOfPayments.Text)
            DataGridView2.Rows.Add("", i, "-", "-", "-", "-", "-")
        Next
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        SavingStatus = "NEW"
        EnableControls()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        SavingStatus = "EDIT"
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_ASSESSMENTS WHERE EDUCATION_LEVEL = @education_level AND ASSESSMENT_TYPE = @assessment_type ORDER BY FEE_ORDER ASC", conn)
                comm.Parameters.AddWithValue("@education_level", DataGridView1.Rows(DGRow).Cells(0).Value)
                comm.Parameters.AddWithValue("@assessment_type", DataGridView1.Rows(DGRow).Cells(1).Value)
                Using reader As SqlDataReader = comm.ExecuteReader
                    Dim i As Integer = 1
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"), i, reader("FEE_CODE"), reader("PTF"), reader("PMF"), reader("POF"), reader("DUE_DATE"))
                        i += 1
                    End While
                End Using
                cmbEducationLevel.Text = DataGridView1.Rows(DGRow).Cells(0).Value
                txtPaymentType.Text = DataGridView1.Rows(DGRow).Cells(1).Value
                txtNoOfPayments.Text = DataGridView2.Rows.Count
            End Using
        End Using
        EnableControls()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim CanSave As Boolean = False
        For i = 0 To DataGridView2.Rows.Count - 1
            If IsNumeric(DataGridView2.Rows(i).Cells(3).Value) = True And IsNumeric(DataGridView2.Rows(i).Cells(4).Value) = True And IsNumeric(DataGridView2.Rows(i).Cells(5).Value) = True Then
                CanSave = True
            Else
                CanSave = False
                Exit For
            End If
        Next

        If CanSave = True Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                If SavingStatus = "NEW" Then
                    Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_ASSESSMENTS WHERE EDUCATION_LEVEL = @education_level AND ASSESSMENT_TYPE = @assessment_type", conn)
                        comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                        comm.Parameters.AddWithValue("@assessment_type", StripSpaces(txtPaymentType.Text))
                        If Val(comm.ExecuteScalar) = 0 Then
                            For i = 0 To DataGridView2.Rows.Count - 1
                                Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_ASSESSMENTS VALUES(@assessment_type,@fee_code,@fee_order,@ptf,@pmf,@pof,@due_date,@education_level)", conn)
                                    comm1.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                                    comm1.Parameters.AddWithValue("@assessment_type", StripSpaces(txtPaymentType.Text))
                                    comm1.Parameters.AddWithValue("@fee_code", StripSpaces(DataGridView2.Rows(i).Cells(2).Value))
                                    comm1.Parameters.AddWithValue("@fee_order", DataGridView2.Rows(i).Cells(1).Value)
                                    comm1.Parameters.AddWithValue("@ptf", DataGridView2.Rows(i).Cells(3).Value)
                                    comm1.Parameters.AddWithValue("@pmf", DataGridView2.Rows(i).Cells(4).Value)
                                    comm1.Parameters.AddWithValue("@pof", DataGridView2.Rows(i).Cells(5).Value)
                                    comm1.Parameters.AddWithValue("@due_date", DataGridView2.Rows(i).Cells(6).Value)
                                    comm1.ExecuteNonQuery()
                                End Using
                            Next
                            MsgBox("New Payment Settings has been successfully saved!", MsgBoxStyle.Information)
                            DisableControls()
                            ClearControls()
                            LoadPaymentSettings()
                        Else
                            MsgBox("Assessment Type: " & txtPaymentType.Text & " is already exist in " & cmbEducationLevel.Text, MsgBoxStyle.Critical)
                        End If
                    End Using
                ElseIf SavingStatus = "EDIT" Then
                    For i = 0 To DataGridView2.Rows.Count - 1
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_ASSESSMENTS SET ASSESSMENT_TYPE = @assessment_type, FEE_CODE = @fee_code, FEE_ORDER = @fee_order, PTF = @ptf, PMF = @pmf, POF = @pof, DUE_DATE = @due_date, EDUCATION_LEVEL = @education_level WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", DataGridView2.Rows(i).Cells(0).Value)
                            comm1.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@assessment_type", StripSpaces(txtPaymentType.Text))
                            comm1.Parameters.AddWithValue("@fee_code", StripSpaces(DataGridView2.Rows(i).Cells(2).Value))
                            comm1.Parameters.AddWithValue("@fee_order", DataGridView2.Rows(i).Cells(1).Value)
                            comm1.Parameters.AddWithValue("@ptf", DataGridView2.Rows(i).Cells(3).Value)
                            comm1.Parameters.AddWithValue("@pmf", DataGridView2.Rows(i).Cells(4).Value)
                            comm1.Parameters.AddWithValue("@pof", DataGridView2.Rows(i).Cells(5).Value)
                            comm1.Parameters.AddWithValue("@due_date", DataGridView2.Rows(i).Cells(6).Value)
                            comm1.ExecuteNonQuery()
                        End Using
                    Next
                    MsgBox("New Payment Settings has been successfully saved!", MsgBoxStyle.Information)
                    DisableControls()
                    ClearControls()
                    LoadPaymentSettings()
                End If
            End Using
        Else
            MsgBox("Numbers are only allowed in Tuition (%), Misc (%) and Other (%)", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DisableControls()
            ClearControls()
        End If
    End Sub
End Class