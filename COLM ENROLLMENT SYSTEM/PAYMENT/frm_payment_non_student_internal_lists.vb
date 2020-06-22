Public Class frm_payment_non_student_internal_lists

    Public Sub Load_Internal_Fees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND FEE_STATUS = 'INTERNAL' AND FEE_TYPE = 'OTHER FEE' AND EDUCATION_LEVEL = 'NON STUDENT' AND FEE_CODE LIKE @search AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem ORDER BY EDUCATION_LEVEL,COURSE_CODE,YRLVL,FEE_CODE ASC", conn)
                comm.Parameters.AddWithValue("@search", "%" & StripSpaces(TextBox1.Text) & "%")
                comm.Parameters.AddWithValue("@course_code", "N.S")
                comm.Parameters.AddWithValue("@yrlvl", "N.S")
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

    Private Sub frm_payment_non_student_internal_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Internal_Fees()
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
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND FEE_TYPE = 'OTHER FEES (INTERNAL)' AND FEE_CODE = @fee_code AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@fee_code", txtFeeCode.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                If Val(comm.ExecuteScalar) = 0 Then
                    Using comm1 As New SqlCommand("INSERT INTO TBL_COLLEGE_FEE_LOADS VALUES(@sn,@fee_type,@fee_code,@quantity,@fee_amount,@ay,@sem,GETDATE())", conn)
                        comm1.Parameters.AddWithValue("@sn", frm_payment.txtStudentNumber.Text)
                        comm1.Parameters.AddWithValue("@fee_type", "OTHER FEES (INTERNAL)")
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
                        End If
                    End Using
                Else
                    MsgBox("Fee is already exist to the account!", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End Using
        End Using
    End Sub
End Class