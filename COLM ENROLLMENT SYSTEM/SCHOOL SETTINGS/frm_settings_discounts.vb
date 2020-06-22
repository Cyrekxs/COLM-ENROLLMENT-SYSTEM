Public Class frm_settings_discounts
    Public SID As Integer = 0
    Public VID As Integer = 0
    Public SDGRow As Integer = 0
    Public VDGRow As Integer = 0
    Public SSavingStatus As String = String.Empty
    Public VSavingStatus As String = String.Empty

    Public Sub SEnableControls()
        grpSEntry.Enabled = True
        grpSLists.Enabled = False
        btnSNew.Enabled = False
        btnSEdit.Enabled = False
        btnSDelete.Enabled = False
        btnSSave.Enabled = True
        btnSCancel.Enabled = True
    End Sub

    Public Sub SDisableControls()
        grpSEntry.Enabled = False
        grpSLists.Enabled = True
        btnSNew.Enabled = True
        btnSEdit.Enabled = True
        btnSDelete.Enabled = True
        btnSSave.Enabled = False
        btnSCancel.Enabled = False
    End Sub

    Public Sub SClearControls()
        cmbSEducationLevel.SelectedIndex = -1
        txtSDiscountCode.Text = String.Empty
        txtSPercentage.Text = 0
    End Sub

    Public Sub SLoadDiscounts()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_DISCOUNTS WHERE DISCOUNT_CATEGORY = 'SCHOLARSHIP' AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY DISCOUNT_CODE ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("EDUCATION_LEVEL"), reader("DISCOUNT_CODE"), reader("DISCOUNT_PERCENTAGE_AMOUNT"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub VEnableControls()
        grpVEntry.Enabled = True
        grpVLists.Enabled = False
        btnVNew.Enabled = False
        btnVEdit.Enabled = False
        btnVDelete.Enabled = False
        btnVSave.Enabled = True
        btnVCancel.Enabled = True
    End Sub

    Public Sub VDisableControls()
        grpVEntry.Enabled = False
        grpVLists.Enabled = True
        btnVNew.Enabled = True
        btnVEdit.Enabled = True
        btnVDelete.Enabled = True
        btnVSave.Enabled = False
        btnVCancel.Enabled = False
    End Sub

    Public Sub VClearControls()
        cmbVEducationLevel.SelectedIndex = -1
        txtVDiscountCode.Text = String.Empty
        txtVAmount.Text = Convert_To_Currency(0)
    End Sub

    Public Sub VLoadDiscounts()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_DISCOUNTS WHERE DISCOUNT_CATEGORY = 'VOUCHER'  AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY DISCOUNT_CODE ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"), reader("EDUCATION_LEVEL"), reader("DISCOUNT_CODE"), Convert_To_Currency(reader("DISCOUNT_PERCENTAGE_AMOUNT")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnSNew_Click(sender As Object, e As EventArgs) Handles btnSNew.Click
        SSavingStatus = "NEW"
        SEnableControls()
        SClearControls()
    End Sub

    Private Sub btnVNew_Click(sender As Object, e As EventArgs) Handles btnVNew.Click
        VSavingStatus = "NEW"
        VEnableControls()
        VClearControls()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        SDGRow = e.RowIndex
    End Sub

    Private Sub DataGridView2_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.RowEnter
        VDGRow = e.RowIndex
    End Sub

    Private Sub btnSEdit_Click(sender As Object, e As EventArgs) Handles btnSEdit.Click
        SSavingStatus = "EDIT"
        SEnableControls()
        SID = DataGridView1.Rows(SDGRow).Cells(0).Value
        cmbSEducationLevel.Text = DataGridView1.Rows(SDGRow).Cells(1).Value
        txtSDiscountCode.Text = DataGridView1.Rows(SDGRow).Cells(2).Value
        txtSPercentage.Text = DataGridView1.Rows(SDGRow).Cells(3).Value
    End Sub

    Private Sub btnVEdit_Click(sender As Object, e As EventArgs) Handles btnVEdit.Click
        VSavingStatus = "EDIT"
        VEnableControls()
        VID = DataGridView2.Rows(VDGRow).Cells(0).Value
        cmbVEducationLevel.Text = DataGridView2.Rows(VDGRow).Cells(1).Value
        txtVDiscountCode.Text = DataGridView2.Rows(VDGRow).Cells(2).Value
        txtVAmount.Text = DataGridView2.Rows(VDGRow).Cells(3).Value
    End Sub

    Private Sub btnSCancel_Click(sender As Object, e As EventArgs) Handles btnSCancel.Click
        If MsgBox("Are you sure you want to cancel transaction in Scholarship Tab?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            SDisableControls()
            SClearControls()
        End If
    End Sub

    Private Sub btnVCancel_Click(sender As Object, e As EventArgs) Handles btnVCancel.Click
        If MsgBox("Are you sure you want to cancel transaction in Vouchers Tab?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            VDisableControls()
            VClearControls()
        End If
    End Sub

    Private Sub btnSSave_Click(sender As Object, e As EventArgs) Handles btnSSave.Click

        If cmbSEducationLevel.Text = String.Empty Then
            MsgBox("Please select Education Level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtSDiscountCode.Text = String.Empty Then
            MsgBox("Please enter Discount Code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtSPercentage.Text = String.Empty Then
            MsgBox("Please enter Percentage!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SSavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_DISCOUNTS WHERE EDUCATION_LEVEL = @education_level AND DISCOUNT_CODE = @discount_code AND DISCOUNT_CATEGORY = 'SCHOLARSHIP'", conn)
                    comm.Parameters.AddWithValue("@education_level", cmbSEducationLevel.Text)
                    comm.Parameters.AddWithValue("@discount_code", txtSDiscountCode.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_DISCOUNTS VALUES(@discount_code,@discount_category,@discount_classification,@education_level,@discount_percentage,@ay,@sem)", conn)
                            comm1.Parameters.AddWithValue("@education_level", cmbSEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@discount_code", txtSDiscountCode.Text)
                            comm1.Parameters.AddWithValue("@discount_category", "SCHOLARSHIP")
                            comm1.Parameters.AddWithValue("@discount_classification", "PERCENTAGE")
                            comm1.Parameters.AddWithValue("@discount_percentage", txtSPercentage.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            If MsgBox("Are you sure you want to save this Scholarship Discount?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Scholarship Discount has been successfully saved!", MsgBoxStyle.Information)
                                SDisableControls()
                                SClearControls()
                                SLoadDiscounts()
                            End If
                        End Using
                    Else
                        MsgBox("Scholarship Discount: " & txtSDiscountCode.Text & " is already exist in " & cmbSEducationLevel.Text, MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf SSavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_DISCOUNTS WHERE EDUCATION_LEVEL = @education_level AND DISCOUNT_CODE = @discount_code AND DISCOUNT_CATEGORY = 'SCHOLARSHIP' AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", SID)
                    comm.Parameters.AddWithValue("@education_level", cmbSEducationLevel.Text)
                    comm.Parameters.AddWithValue("@discount_code", txtSDiscountCode.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_DISCOUNTS SET DISCOUNT_CODE = @discount_code, EDUCATION_LEVEL = @education_level, DISCOUNT_PERCENTAGE_AMOUNT = @discount_percentage WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", SID)
                            comm1.Parameters.AddWithValue("@education_level", cmbSEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@discount_code", txtSDiscountCode.Text)
                            comm1.Parameters.AddWithValue("@discount_category", "SCHOLARSHIP")
                            comm1.Parameters.AddWithValue("@discount_classification", "PERCENTAGE")
                            comm1.Parameters.AddWithValue("@discount_percentage", txtSPercentage.Text)
                            If MsgBox("Are you sure you want to update this Scholarship Discount?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Scholarship Discount has been successfully updated!", MsgBoxStyle.Information)
                                SDisableControls()
                                SClearControls()
                                SLoadDiscounts()
                            End If
                        End Using
                    Else
                        MsgBox("Scholarship Discount: " & txtSDiscountCode.Text & " is already used in " & cmbSEducationLevel.Text, MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub

    Private Sub frm_settings_discounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SDisableControls()
        SClearControls()
        SLoadDiscounts()

        VDisableControls()
        VClearControls()
        VLoadDiscounts()
    End Sub

    Private Sub btnVSave_Click(sender As Object, e As EventArgs) Handles btnVSave.Click

        If cmbVEducationLevel.Text = String.Empty Then
            MsgBox("Please select Education Level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtVDiscountCode.Text = String.Empty Then
            MsgBox("Please enter Discount Code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtVAmount.Text = String.Empty Then
            MsgBox("Please enter Percentage!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If VSavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_DISCOUNTS WHERE EDUCATION_LEVEL = @education_level AND DISCOUNT_CODE = @discount_code AND DISCOUNT_CATEGORY = 'VOUCHER'", conn)
                    comm.Parameters.AddWithValue("@education_level", cmbVEducationLevel.Text)
                    comm.Parameters.AddWithValue("@discount_code", txtVDiscountCode.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_DISCOUNTS VALUES(@discount_code,@discount_category,@discount_classification,@education_level,@discount_percentage,@ay,@sem)", conn)
                            comm1.Parameters.AddWithValue("@education_level", cmbVEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@discount_code", txtVDiscountCode.Text)
                            comm1.Parameters.AddWithValue("@discount_category", "VOUCHER")
                            comm1.Parameters.AddWithValue("@discount_classification", "AMOUNT")
                            comm1.Parameters.AddWithValue("@discount_percentage", txtVAmount.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            If MsgBox("Are you sure you want to save this Voucher Discount?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Voucher Discount has been successfully saved!", MsgBoxStyle.Information)
                                VDisableControls()
                                VClearControls()
                                VLoadDiscounts()
                            End If
                        End Using
                    Else
                        MsgBox("Vocher Discount: " & txtVDiscountCode.Text & " is already exist in " & cmbVEducationLevel.Text, MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf VSavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_DISCOUNTS WHERE EDUCATION_LEVEL = @education_level AND DISCOUNT_CODE = @discount_code AND DISCOUNT_CATEGORY = 'VOUCHER' AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", VID)
                    comm.Parameters.AddWithValue("@education_level", cmbVEducationLevel.Text)
                    comm.Parameters.AddWithValue("@discount_code", txtVDiscountCode.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_DISCOUNTS SET DISCOUNT_CODE = @discount_code, EDUCATION_LEVEL = @education_level, DISCOUNT_PERCENTAGE_AMOUNT = @discount_percentage WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", VID)
                            comm1.Parameters.AddWithValue("@education_level", cmbVEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@discount_code", txtVDiscountCode.Text)
                            comm1.Parameters.AddWithValue("@discount_percentage", CDbl(txtVAmount.Text))
                            If MsgBox("Are you sure you want to update this Voucher Discount?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Voucher Discount has been successfully updated!", MsgBoxStyle.Information)
                                VDisableControls()
                                VClearControls()
                                VLoadDiscounts()
                            End If
                        End Using
                    Else
                        MsgBox("Voucher Discount: " & txtVDiscountCode.Text & " is already used in " & cmbVEducationLevel.Text, MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub
End Class