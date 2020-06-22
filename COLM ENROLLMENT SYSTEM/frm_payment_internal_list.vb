Public Class frm_payment_internal_list

    Public Course As String = String.Empty
    Public Year As String = String.Empty

    Public Sub GenerateReferenceCode()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT IDENT_CURRENT('TBL_COLLEGE_FEE_LOADS')", conn)
                Dim LastID As Integer = 0
                LastID = comm.ExecuteScalar
                LastID += 1
                txtRefCode.Text = "REF" & LastID
            End Using
        End Using
    End Sub

    Public Sub Load_Internal_Fees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND FEE_STATUS = 'INTERNAL' AND FEE_TYPE = 'OTHER FEE' AND EDUCATION_LEVEL <> 'NON STUDENT' AND FEE_CODE LIKE @search AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem ORDER BY EDUCATION_LEVEL,COURSE_CODE,YRLVL,FEE_CODE ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & StripSpaces(TextBox1.Text) & "%")
                comm.Parameters.AddWithValue("@course_code", Course)
                comm.Parameters.AddWithValue("@yrlvl", Year)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                DataGridView3.Rows.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView3.Rows.Add(reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), "CHARGE FEE")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_payment_internal_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Internal_Fees()
        GenerateReferenceCode()
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick
        If e.ColumnIndex = 2 Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND FEE_TYPE = 'OTHER FEES (INTERNAL)' AND FEE_CODE = @fee_code AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@fee_code", DataGridView3.Rows(e.RowIndex).Cells(0).Value)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_COLLEGE_FEE_LOADS VALUES(@sn,@fee_type,@fee_code,@fee_amount,@ay,@sem,GETDATE())", conn)
                            comm1.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                            comm1.Parameters.AddWithValue("@fee_type", "OTHER FEES (INTERNAL)")
                            comm1.Parameters.AddWithValue("@fee_code", DataGridView3.Rows(e.RowIndex).Cells(0).Value)
                            comm1.Parameters.AddWithValue("@fee_amount", DataGridView3.Rows(e.RowIndex).Cells(1).Value)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            If MsgBox("Are you sure you want to charge this fee to this student?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                Create_Statement(frm_payment.txtStudentNumber.Text, "ADDL", "OTHER FEE", DataGridView3.Rows(e.RowIndex).Cells(0).Value, Date.Now, DataGridView3.Rows(e.RowIndex).Cells(1).Value, 0)
                                Create_Statement(frm_payment.txtStudentNumber.Text, "BAL OF", "OTHER FEE", DataGridView3.Rows(e.RowIndex).Cells(0).Value, Date.Now, DataGridView3.Rows(e.RowIndex).Cells(1).Value, 0)
                                MsgBox("Fee has been successfully added!", MsgBoxStyle.Information)
                            End If
                        End Using
                    Else
                        MsgBox("Fee is already exist to the account!", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Internal_Fees()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtQuantity.Text = String.Empty Then
            MsgBox("Please enter Quantity!", MsgBoxStyle.Critical)
            txtQuantity.Focus()
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            'VERIFY IF THE REFERENCE CODE IS EXISITNG
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_FEE_LOADS WHERE REF_CODE = @refcode", conn)
                comm.Parameters.AddWithValue("@refcode", txtRefCode.Text)
                If Val(comm.ExecuteScalar) = 0 Then
                    'IF REFERENCE CODE IS NOT EXISTING THEN PROCEED TO INSERT
                    Using comm1 As New SqlCommand("INSERT INTO TBL_COLLEGE_FEE_LOADS VALUES(@sn,@fee_type,@refcode,@fee_code,@quantity,@fee_amount,@ay,@sem,GETDATE())", conn)
                        comm1.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                        comm1.Parameters.AddWithValue("@fee_type", "OTHER FEES (INTERNAL)")
                        comm1.Parameters.AddWithValue("@refcode", txtRefCode.Text)
                        comm1.Parameters.AddWithValue("@fee_code", txtFeeCode.Text)
                        comm1.Parameters.AddWithValue("@quantity", txtQuantity.Text)
                        comm1.Parameters.AddWithValue("@fee_amount", txtTotalAmount.Text)
                        comm1.Parameters.AddWithValue("@ay", Academic_Year)
                        comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                        If MsgBox("Are you sure you want to charge this fee to this student?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            comm1.ExecuteNonQuery()
                            Create_Statement(frm_payment.txtStudentNumber.Text, "ADDL", "OTHER FEE", txtFeeCode.Text, Date.Now, txtTotalAmount.Text, 0)
                            Create_Statement(frm_payment.txtStudentNumber.Text, "BAL OF", "OTHER FEE", txtFeeCode.Text, Date.Now, txtTotalAmount.Text, 0)
                            MsgBox("Fee has been successfully added!", MsgBoxStyle.Information)
                            GenerateReferenceCode()
                        End If
                    End Using
                Else
                    MsgBox("Reference Code is already exist!", MsgBoxStyle.Critical)
                End If
            End Using
        End Using
    End Sub

    Private Sub DataGridView3_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.RowEnter
        txtFeeCode.Text = DataGridView3.Rows(e.RowIndex).Cells(0).Value
        txtFeeAmount.Text = DataGridView3.Rows(e.RowIndex).Cells(1).Value
        txtQuantity.Text = 1
        txtTotalAmount.Text = DataGridView3.Rows(e.RowIndex).Cells(1).Value
    End Sub

    Private Sub txtQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQuantity.KeyPress
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtQuantity.TextChanged
        If txtQuantity.Text <> String.Empty Then
            txtTotalAmount.Text = Convert_To_Currency(CDbl(txtQuantity.Text) * CDbl(txtFeeAmount.Text))
        End If
    End Sub
End Class