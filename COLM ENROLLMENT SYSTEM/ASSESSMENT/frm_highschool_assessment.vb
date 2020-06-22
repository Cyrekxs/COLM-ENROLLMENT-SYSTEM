Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Imports Microsoft.Reporting.WinForms

Public Class frm_highschool_assessment

    Public EducationLevel As String = String.Empty
    Public CourseCode As String = String.Empty
    Public AssessmentStatus As String = String.Empty
    Public Assessment_ID As String = String.Empty

    Public Sub ClearAssessmentSummary()
        cmbAssessment.SelectedIndex = -1
        cmbVoucher.SelectedIndex = -1
        txtVoucherAmount.Text = Convert_To_Currency(0)
        cmbDiscount.SelectedIndex = -1
        txtDiscountPercentage.Text = "0"
        txtDiscountAmount.Text = Convert_To_Currency(0)
        txtDirectDiscount.Text = Convert_To_Currency(0)
        txtAdditionalSubjects.Text = Convert_To_Currency(0)
        txtOldBalance.Text = Convert_To_Currency(0)
        dgAssessmentBreakdown.Rows.Clear()
    End Sub

    Public Sub Calculate_Generate_Fees()
        If EducationLevel = "JUNIOR HIGH" Then
            Dim Old_Balance As Double = 0 'OLD Balance
            Dim TFee As Double = 0 'TUITION FEE
            Dim MFee As Double = 0 'MISCELLANEOUS
            Dim OFee As Double = 0 'OTHER FEES'
            Dim V_Amount As Double = 0 'VOUCHER AMOUNT
            Dim D_Amount As Double = 0 'DISCOUNT AMOUNT
            Dim DD_Amount As Double = 0 'DIRECT DISCOUNT
            Dim DD_AmountCopy As Double = 0 'DIRECT DISCOUNT AMOUNT COPY

            Dim Total_Surcharge As Double = 0 'SURCHARGE
            Dim Surcharge_Multiplier As Integer = 0 'SURCHARGE MULTIPLIER
            Dim Deducation As Double = 0 'DEDUCTIONS
            Dim Gross_Amount As Double = 0 'GROSS
            Dim Net_Amount As Double = 0 'NET
            Dim Net_Amount_Copy As Double = 0

            '-----PUTING VALUES TO VARIABLES---------------------------
            Old_Balance = txtOldBalance.Text
            TFee = txtTFee.Text
            MFee = txtMFee.Text
            OFee = txtOFee.Text

            V_Amount = txtVoucherAmount.Text
            TFee = TFee - V_Amount
            D_Amount = TFee * (txtDiscountPercentage.Text / 100)

            txtDiscountAmount.Text = D_Amount
            TFee = TFee - D_Amount

            DD_Amount = CDbl(txtDirectDiscount.Text)
            DD_AmountCopy = DD_Amount / 2

            TFee = TFee - DD_AmountCopy
            MFee = MFee - DD_AmountCopy

            Total_Surcharge = txtSurcharge.Text

            Gross_Amount = Old_Balance + CDbl(txtTFee.Text) + CDbl(txtMFee.Text) + CDbl(txtOFee.Text)
            Deducation = CDbl(V_Amount) + CDbl(D_Amount)

            Net_Amount = (Gross_Amount - Deducation) + Total_Surcharge
            Net_Amount = (Net_Amount - DD_Amount)
            Net_Amount_Copy = Net_Amount
            '----------------------------------------------------------

            'BREAK DOWN OF FEES
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_ASSESSMENTS WHERE Assessment_Type = @type AND Education_Level = @education_level AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY Fee_Order ASC", conn)
                    comm.Parameters.AddWithValue("@type", cmbAssessment.Text)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        dgAssessmentBreakdown.Rows.Clear()
                        While reader.Read
                            Dim T_Fee As Decimal = 0
                            Dim M_Fee As Decimal = 0
                            Dim O_Fee As Double = 0
                            Dim Fee_Code As String = String.Empty
                            Dim Due_Date As String = String.Empty
                            Dim PTF As Decimal = 0
                            Dim PMF As Decimal = 0
                            Dim POF As Decimal = 0

                            Fee_Code = reader("FEE_CODE")
                            Due_Date = reader("DUE_DATE")
                            PTF = reader("PTF") / 100
                            PMF = reader("PMF") / 100
                            POF = reader("POF") / 100
                            T_Fee = TFee * PTF
                            M_Fee = MFee * PMF
                            O_Fee = OFee * POF
                            dgAssessmentBreakdown.Rows.Add(Fee_Code, Convert_To_Currency(T_Fee + M_Fee + O_Fee), Due_Date)
                        End While
                    End Using
                End Using
            End Using

            'BREAKING DOWN THE SURCHARGES
            Dim Surcharge As Double = Total_Surcharge / (dgAssessmentBreakdown.Rows.Count - 1)
            For i = 1 To dgAssessmentBreakdown.Rows.Count - 1
                Dim Cell_Amount As Double = dgAssessmentBreakdown.Rows(i).Cells(1).Value
                dgAssessmentBreakdown.Rows(i).Cells(1).Value = Convert_To_Currency(Cell_Amount + Surcharge + (CDbl(Old_Balance) / (dgAssessmentBreakdown.Rows.Count - 1)))
            Next



            'PUTTING RESULT ON TEXTBOXES
            'txtTFee.Text = Convert_To_Currency(TFee)
            'txtMFee.Text = Convert_To_Currency(MFee)
            'txtOFee.Text = Convert_To_Currency(OFee)
            txtOldBalance.Text = Convert_To_Currency(Old_Balance)
            'txtVoucherAmount.Text = Convert_To_Currency(V_Amount)
            txtDiscountAmount.Text = Convert_To_Currency(D_Amount)

            txtGrossFee.Text = Convert_To_Currency(Gross_Amount)
            txtDeductions.Text = Convert_To_Currency(Deducation)
            txtNetFee.Text = Convert_To_Currency(Net_Amount)

        ElseIf EducationLevel = "SENIOR HIGH" Then

            Dim Old_Balance As Double = 0 'OLD Balance
            Dim TFee As Double = 0 'TUITION FEE
            Dim MFee As Double = 0 'MISCELLANEOUS
            Dim OFee As Double = 0 'OTHER FEES
            Dim V_Amount As Double = 0 'VOUCHER AMOUNT
            Dim D_Amount As Double = 0 'DISCOUNT AMOUNT
            Dim AS_Amount As Double = 0 'ACADEMIC SCHOLAR AMOUNT
            Dim DD_Amount As Double = 0 'DIRECT DISCOUNT
            Dim DD_AmountCopy As Double = 0 'DIRECT DISCOUNT AMOUNT COPY
            Dim Total_Surcharge As Double = 0 'SURCHARGE
            Dim Surcharge_Multiplier As Integer = 0 'SURCHARGE MULTIPLIER
            Dim Deducation As Double = 0 'DEDUCTIONS
            Dim Gross_Amount As Double = 0 'GROSS
            Dim Net_Amount As Double = 0 'NET
            Dim Net_Amount_Copy As Double = 0

            '-----PUTING VALUES TO VARIABLES---------------------------
            Old_Balance = txtOldBalance.Text
            TFee = txtTFee.Text
            MFee = txtMFee.Text
            OFee = txtOFee.Text
            V_Amount = txtVoucherAmount.Text

            'MFEE
            If V_Amount > MFee Then
                V_Amount = V_Amount - MFee
                MFee = 0
                If MFee <= 0 Then
                    MFee = 0
                End If
            End If

            'TFEE
            If V_Amount > TFee Then
                V_Amount = V_Amount - TFee
                TFee = TFee - V_Amount
                If TFee <= 0 Then
                    TFee = 0
                End If
            Else
                TFee = TFee - V_Amount
                V_Amount = 0
            End If


            AS_Amount = TFee * (txtHonorPerct.Text / 100)
            TFee = TFee - AS_Amount

            D_Amount = TFee * (txtDiscountPercentage.Text / 100)
            TFee = TFee - D_Amount

            DD_Amount = CDbl(txtDirectDiscount.Text)
            DD_AmountCopy = DD_Amount / 2

            TFee = TFee - DD_AmountCopy
            MFee = MFee - DD_AmountCopy

            Total_Surcharge = txtSurcharge.Text
            Gross_Amount = Old_Balance + CDbl(txtTFee.Text) + CDbl(txtMFee.Text) + CDbl(txtOFee.Text)
            Deducation = CDbl(txtVoucherAmount.Text) + CDbl(D_Amount) + CDbl(AS_Amount)

            Net_Amount = (Gross_Amount - Deducation) + Total_Surcharge
            Net_Amount = (Net_Amount - DD_Amount)
            Net_Amount_Copy = Net_Amount
            '----------------------------------------------------------

            'BREAK DOWN OF FEES
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_ASSESSMENTS WHERE Assessment_Type = @type AND Education_Level = @education_level AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY Fee_Order ASC", conn)
                    comm.Parameters.AddWithValue("@type", cmbAssessment.Text)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)

                    Using reader As SqlDataReader = comm.ExecuteReader
                        dgAssessmentBreakdown.Rows.Clear()
                        While reader.Read
                            Dim T_Fee As Decimal = 0
                            Dim M_Fee As Decimal = 0
                            Dim O_Fee As Double = 0
                            Dim Fee_Code As String = String.Empty
                            Dim Due_Date As String = String.Empty
                            Dim PTF As Decimal = 0
                            Dim PMF As Decimal = 0
                            Dim POF As Decimal = 0

                            Fee_Code = reader("FEE_CODE")
                            Due_Date = reader("DUE_DATE")
                            PTF = reader("PTF") / 100
                            PMF = reader("PMF") / 100
                            POF = reader("POF") / 100

                            T_Fee = TFee * PTF
                            M_Fee = MFee * PMF
                            O_Fee = OFee * POF

                            dgAssessmentBreakdown.Rows.Add(Fee_Code, Convert_To_Currency(T_Fee + M_Fee + O_Fee), Due_Date)
                        End While
                    End Using
                End Using
            End Using

            'BREAKING DOWN THE SURCHARGES AND OLD BALANCE
            If dgAssessmentBreakdown.Rows.Count = 1 Then
                Dim Cell_Amount As Double = dgAssessmentBreakdown.Rows(0).Cells(1).Value
                dgAssessmentBreakdown.Rows(0).Cells(1).Value = Convert_To_Currency(Cell_Amount + (CDbl(Old_Balance)))
            Else
                Dim Surcharge As Double = Total_Surcharge / (dgAssessmentBreakdown.Rows.Count - 1)
                For i = 0 To dgAssessmentBreakdown.Rows.Count - 1
                    Dim Cell_Amount As Double = dgAssessmentBreakdown.Rows(i).Cells(1).Value
                    dgAssessmentBreakdown.Rows(i).Cells(1).Value = Convert_To_Currency(Cell_Amount + Surcharge + (CDbl(Old_Balance) / (dgAssessmentBreakdown.Rows.Count - 1)))
                Next
            End If




            txtOldBalance.Text = Convert_To_Currency(Old_Balance)
            txtDiscountAmount.Text = Convert_To_Currency(D_Amount)
            txtGrossFee.Text = Convert_To_Currency(Gross_Amount)
            txtDeductions.Text = Convert_To_Currency(Deducation)
            txtNetFee.Text = Convert_To_Currency(Net_Amount)
        End If

    End Sub

    Public Sub LoadDefaultFees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            'TUITION FEE
            Using comm As New SqlCommand("SELECT AMOUNT FROM TBL_SETTINGS_TUITION_FEE WHERE EDUCATION_LEVEL = @education_level AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_YEAR = @ay", conn)
                comm.Parameters.AddWithValue("@education_level", EducationLevel)
                comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                comm.Parameters.AddWithValue("@yrlvl", txtYear.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtTFee.Text = Convert_To_Currency(reader("Amount"))
                    End While
                End Using
            End Using
            'MISCELLANOUES FEE
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE FEE_TYPE = 'MISCELLANEOUS FEE' AND FEE_STATUS = 'EXTERNAL' AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YR = @ay", conn)
                comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                comm.Parameters.AddWithValue("@yrlvl", txtYear.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@education_level", EducationLevel)
                Using reader As SqlDataReader = comm.ExecuteReader
                    Dim amount As Double = 0
                    While reader.Read
                        amount += reader("Fee_Amount")
                    End While
                    txtMFee.Text = Convert_To_Currency(amount)
                End Using
            End Using

        End Using
    End Sub

    Public Sub LoadAssessedFees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            'ASSESSMENT SUMMARY
            Dim IshasAssessment As Boolean = False
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND PullOutStatus = 'ACTIVE'", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        IshasAssessment = True
                        Assessment_ID = reader("ID")
                        cmbAssessment.Text = reader("ASSESSMENT_TYPE")
                        cmbVoucher.Text = reader("VOUCHER_CODE")
                        txtVoucherAmount.Text = Convert_To_Currency(reader("VOUCHER_AMOUNT"))
                        If reader("Discount_Code").ToString = String.Empty Then
                            cmbDiscount.Text = "NONE"
                            txtDiscountPercentage.Text = "0"
                            txtDiscountAmount.Text = "0.00"
                        Else
                            cmbDiscount.Text = reader("DISCOUNT_CODE")
                            txtDiscountPercentage.Text = reader("DISCOUNT_PERCENTAGE")
                            txtDiscountAmount.Text = Convert_To_Currency(reader("DISCOUNT_AMOUNT"))
                        End If

                        If IsDBNull(reader("Academic_Scholar")) = True Then
                            cmbHonorDiscount.Text = "NONE"
                        Else
                            Dim acad_scholar As String() = reader("Academic_Scholar").ToString.Split("|")
                            cmbHonorDiscount.Text = acad_scholar(0)

                            If acad_scholar.Count = 2 Then
                                txtHonorPerct.Text = acad_scholar(1)
                            End If

                        End If

                        txtDirectDiscount.Text = Convert_To_Currency(reader("Direct_Discount"))

                        txtOldBalance.Text = Convert_To_Currency(reader("OLD_BALANCE"))
                        txtTFee.Text = Convert_To_Currency(reader("TFEE"))
                        txtOFee.Text = Convert_To_Currency(reader("OFEE"))
                        txtMFee.Text = Convert_To_Currency(reader("MFEE"))
                        txtSurcharge.Text = Convert_To_Currency(reader("SURCHARGE"))
                        txtDeductions.Text = Convert_To_Currency(CDbl(txtVoucherAmount.Text) + CDbl(txtDiscountAmount.Text))
                        txtGrossFee.Text = Convert_To_Currency(CDbl(txtTFee.Text) + CDbl(txtMFee.Text))
                        txtNetFee.Text = Convert_To_Currency(reader("TOTAL"))
                    End While
                End Using
            End Using

            'READ NUMBER OF ADDITIONAL SUBJECTS
            Using comm As New SqlCommand("SELECT * FROM tbl_college_fee_loads WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                Using reader As SqlDataReader = comm.ExecuteReader
                    If reader.HasRows = True Then
                        While reader.Read
                            txtAdditionalSubjects.Text = reader("Quantity")
                        End While
                    Else
                        txtAdditionalSubjects.Text = 0
                    End If

                End Using
            End Using

            'ASSESSMENT BREAKDOWN
            If IshasAssessment = True Then
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                    Using reader As SqlDataReader = comm.ExecuteReader
                        dgAssessmentBreakdown.Rows.Clear()
                        While reader.Read
                            dgAssessmentBreakdown.Rows.Add(reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), reader("DUE_DATE"))
                        End While
                    End Using
                End Using
            End If
        End Using
    End Sub

    Public Sub Load_Assessment_Types()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT ASSESSMENT_TYPE FROM TBL_SETTINGS_ASSESSMENTS WHERE EDUCATION_LEVEL = @education_level AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY ASSESSMENT_TYPE ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@education_level", EducationLevel)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbAssessment.Items.Clear()
                    While reader.Read
                        cmbAssessment.Items.Add(reader("ASSESSMENT_TYPE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub Load_Discounts_And_Vouchers()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_DISCOUNTS WHERE EDUCATION_LEVEL = @education_level AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY DISCOUNT_CATEGORY,ID ASC", conn)
                comm.Parameters.AddWithValue("@education_level", EducationLevel)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbVoucher.Items.Clear()
                    cmbDiscount.Items.Clear()
                    cmbVoucher.Items.Add("NONE")
                    cmbDiscount.Items.Add("NONE")
                    While reader.Read
                        If reader("DISCOUNT_CATEGORY") = "SCHOLARSHIP" Then
                            cmbDiscount.Items.Add(reader("DISCOUNT_CODE"))
                        ElseIf reader("DISCOUNT_CATEGORY") = "ACADEMIC SCHOLARSHIP" Then
                            cmbHonorDiscount.Items.Add(reader("DISCOUNT_CODE"))
                        ElseIf reader("DISCOUNT_CATEGORY") = "VOUCHER" Then
                            cmbVoucher.Items.Add(reader("DISCOUNT_CODE"))
                        End If
                    End While
                End Using
            End Using
        End Using
        cmbDiscount.Text = "NONE"
        cmbVoucher.Text = "NONE"
        cmbHonorDiscount.Text = "NONE"
    End Sub

    Private Sub cmbVoucher_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbVoucher.SelectionChangeCommitted
        If cmbVoucher.Text = "NONE" Then
            txtVoucherAmount.Text = Convert_To_Currency("0")
        ElseIf cmbVoucher.Text <> "NONE" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT DISCOUNT_PERCENTAGE_AMOUNT FROM TBL_SETTINGS_DISCOUNTS WHERE DISCOUNT_CODE = @discount_code AND EDUCATION_LEVEL = @education_level AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@discount_code", cmbVoucher.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    txtVoucherAmount.Text = Val(comm.ExecuteScalar)
                End Using
            End Using
        End If

        Dim SelectedVoucherTypeCategory As String = String.Empty
        If cmbVoucher.Text.Contains("PUBLIC") Then
            SelectedVoucherTypeCategory = "PUBLIC"
        ElseIf cmbVoucher.Text.Contains("PRIVATE") Then
            SelectedVoucherTypeCategory = "PRIVATE"
        End If

        For i = 0 To cmbHonorDiscount.Items.Count - 1
            If cmbHonorDiscount.Items(i).ToString.Contains(SelectedVoucherTypeCategory) = False Then
                cmbHonorDiscount.Items.Remove(i)
            End If
        Next


        Calculate_Generate_Fees()
    End Sub

    Private Sub cmbDiscount_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDiscount.SelectionChangeCommitted
        If cmbDiscount.Text = "NONE" Then
            txtDiscountPercentage.Text = "0"
            txtDiscountAmount.Text = Convert_To_Currency("0")
        ElseIf cmbDiscount.Text <> "NONE" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT DISCOUNT_PERCENTAGE_AMOUNT FROM TBL_SETTINGS_DISCOUNTS WHERE DISCOUNT_CODE = @discount_code AND EDUCATION_LEVEL = @education_level", conn)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@discount_code", cmbDiscount.Text)
                    txtDiscountPercentage.Text = Val(comm.ExecuteScalar)
                    txtDiscountAmount.Text = Convert_To_Currency(CDbl(txtTFee.Text) * (CDbl(txtDiscountPercentage.Text) / 100))
                End Using
            End Using
        End If
        Calculate_Generate_Fees()
    End Sub

    Private Sub frm_highschool_assessment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If EducationLevel = "JUNIOR HIGH" Then
            cmbHonorDiscount.Enabled = False
            cmbHonorDiscount.Text = "NONE"
        ElseIf cmbHonorDiscount.Text = "SENIOR HIGH" Then
            cmbHonorDiscount.Enabled = True
            cmbHonorDiscount.Text = "NONE"
        End If

        If AssessmentStatus = "ASSESSMENT" Then
            Load_Assessment_Types()
            Load_Discounts_And_Vouchers()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With frm_highschool_assessment_browse_student
            .EducationLevel = EducationLevel
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub cmbAssessment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssessment.SelectedIndexChanged

    End Sub

    Private Sub cmbAssessment_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbAssessment.SelectionChangeCommitted
        If cmbAssessment.Text <> "CASH" Then
            Dim Surcharge As Double = 0
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                'GETTING THE SURCHARGE
                Using comm As New SqlCommand("SELECT SURCHARGE FROM TBL_SETTINGS_SURCHARGES WHERE EDUCATION_LEVEL = @education_level AND Academic_Year = @ay AND Academic_Sem= @sem", conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    Surcharge = Convert_To_Currency(Val(comm.ExecuteScalar))
                End Using
            End Using

            Dim Surcharge_Multiplier As Double = 0
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                'GETTING THE COUNT OF SURCHARGE MULTIPLIER
                Using comm As New SqlCommand("SELECT COUNT(ID) FROM TBL_SETTINGS_ASSESSMENTS WHERE ASSESSMENT_TYPE = @type AND EDUCATION_LEVEL = @education_level AND Academic_Yr = @ay AND Academic_sem = @sem", conn)
                    comm.Parameters.AddWithValue("@type", cmbAssessment.Text)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Surcharge_Multiplier = Val(comm.ExecuteScalar) - 1
                End Using
            End Using

            txtSurcharge.Text = Convert_To_Currency(Surcharge * Surcharge_Multiplier)
        Else
            txtSurcharge.Text = Convert_To_Currency("0")
        End If
        Calculate_Generate_Fees()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    If AssessmentStatus = "ASSESSMENT" Then
                        'INSERTING NEW ASSESSMENT SUMMARY
                        Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_ASSESSMENT_SUMMARY VALUES(@education_level,@sn,@course_code,@yrlvl,@sect_code,@tfee,@mfee,@ofee,@voucher_code,@voucher_amount,@discount_code,@discount_percentage,@discount_amount,@HonorCode,@DirectDiscount,@surcharge,@old_balance,@total,@assessment_type,@ay,@sem,@assess_by,GETDATE(),'ACTIVE')", conn, t)
                            comm.Parameters.AddWithValue("@education_level", EducationLevel)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm.Parameters.AddWithValue("@yrlvl", txtYear.Text)
                            comm.Parameters.AddWithValue("@sect_code", txtSection.Text)
                            comm.Parameters.AddWithValue("@tfee", txtTFee.Text)
                            comm.Parameters.AddWithValue("@mfee", txtMFee.Text)
                            comm.Parameters.AddWithValue("@ofee", txtOFee.Text)
                            comm.Parameters.AddWithValue("@old_balance", txtOldBalance.Text)
                            comm.Parameters.AddWithValue("@voucher_code", cmbVoucher.Text)
                            comm.Parameters.AddWithValue("@voucher_amount", txtVoucherAmount.Text)
                            comm.Parameters.AddWithValue("@discount_code", cmbDiscount.Text)
                            comm.Parameters.AddWithValue("@discount_percentage", txtDiscountPercentage.Text)
                            comm.Parameters.AddWithValue("@discount_amount", txtDiscountAmount.Text)
                            If cmbHonorDiscount.Text = "NONE" Then
                                comm.Parameters.AddWithValue("@HonorCode", cmbHonorDiscount.Text)
                            Else
                                comm.Parameters.AddWithValue("@HonorCode", cmbHonorDiscount.Text & "|" & txtHonorPerct.Text)
                            End If
                            comm.Parameters.AddWithValue("@DirectDiscount", txtDirectDiscount.Text)
                            comm.Parameters.AddWithValue("@surcharge", txtSurcharge.Text)
                            comm.Parameters.AddWithValue("@total", txtNetFee.Text)
                            comm.Parameters.AddWithValue("@assessment_type", cmbAssessment.Text)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                            comm.Parameters.AddWithValue("@assess_by", Account_Name)
                            comm.ExecuteNonQuery()
                        End Using

                        Dim AdditionalSubjects As Integer = CInt(txtAdditionalSubjects.Text)
                        Using comm As New SqlCommand("INSERT INTO tbl_college_fee_loads VALUES (NULL,@StudentNumber,@FeeType,NULL,@FeeCode,@Quantity,@FeeAmount,@ay,@sem,GETDATE())", conn, t)
                            comm.Parameters.AddWithValue("@StudentNumber", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@FeeType", "TFEE ADDITIONAL")
                            comm.Parameters.AddWithValue("@FeeCode", "ADDITIONAL SUBJECTS")
                            comm.Parameters.AddWithValue("@Quantity", AdditionalSubjects)
                            comm.Parameters.AddWithValue("@FeeAmount", (CDbl(AdditionalSubjects * 1500)))
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                            comm.ExecuteNonQuery()
                        End Using

                        'TBL_ASSESSMENT_BREAKDOWN
                        For i = 0 To dgAssessmentBreakdown.Rows.Count - 1
                            Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_ASSESSMENT_BREAKDOWN VALUES(NULL,@sn,@fee_code,@fee_amount,@due_date,@ay,@sem,GETDATE())", conn, t)
                                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                                comm.Parameters.AddWithValue("@fee_code", dgAssessmentBreakdown.Rows(i).Cells(0).Value)
                                comm.Parameters.AddWithValue("@fee_amount", dgAssessmentBreakdown.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@due_date", dgAssessmentBreakdown.Rows(i).Cells(2).Value)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                                comm.ExecuteNonQuery()
                            End Using
                        Next

                        AssessmentStatus = "RE-ASSESSMENT"

                    ElseIf AssessmentStatus = "RE-ASSESSMENT" Then

                        'TBL_COLLEGE_ASSESSMENT_SUMMARY
                        Using comm As New SqlCommand("UPDATE TBL_COLLEGE_ASSESSMENT_SUMMARY SET STUDENT_NUMBER = @sn, COURSE_CODE = @course_code, YRLVL = @yrlvl, SECT_CODE = @sect_code, TFEE = @tfee, MFEE = @mfee, OFEE = @ofee, VOUCHER_CODE = @voucher_code, VOUCHER_AMOUNT = @voucher_amount, DISCOUNT_CODE = @discount_code, DISCOUNT_PERCENTAGE = @discount_percentage, DISCOUNT_AMOUNT = @discount_amount,Academic_Scholar = @HonorCode,Direct_Discount = @DirectDiscount, SURCHARGE = @surcharge, OLD_BALANCE = @old_balance, TOTAL = @total, ASSESSMENT_TYPE = @assessment_type, ACADEMIC_YR = @ay, ASSESS_BY = @assess_by, ASSESSED_DATE = GETDATE() WHERE ID = @id", conn, t)
                            comm.Parameters.AddWithValue("@id", Assessment_ID)
                            comm.Parameters.AddWithValue("@education_level", EducationLevel)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm.Parameters.AddWithValue("@yrlvl", txtYear.Text)
                            comm.Parameters.AddWithValue("@sect_code", txtSection.Text)
                            comm.Parameters.AddWithValue("@tfee", txtTFee.Text)
                            comm.Parameters.AddWithValue("@ofee", txtOFee.Text)
                            comm.Parameters.AddWithValue("@mfee", txtMFee.Text)
                            comm.Parameters.AddWithValue("@old_balance", txtOldBalance.Text)
                            comm.Parameters.AddWithValue("@voucher_code", cmbVoucher.Text)
                            comm.Parameters.AddWithValue("@voucher_amount", txtVoucherAmount.Text)
                            comm.Parameters.AddWithValue("@discount_code", cmbDiscount.Text)
                            comm.Parameters.AddWithValue("@discount_percentage", txtDiscountPercentage.Text)
                            comm.Parameters.AddWithValue("@discount_amount", txtDiscountAmount.Text)
                            If cmbHonorDiscount.Text = "NONE" Then
                                comm.Parameters.AddWithValue("@HonorCode", cmbHonorDiscount.Text)
                            Else
                                comm.Parameters.AddWithValue("@HonorCode", cmbHonorDiscount.Text & "|" & txtHonorPerct.Text)
                            End If
                            comm.Parameters.AddWithValue("@DirectDiscount", txtDirectDiscount.Text)
                            comm.Parameters.AddWithValue("@surcharge", txtSurcharge.Text)
                            comm.Parameters.AddWithValue("@total", txtNetFee.Text)
                            comm.Parameters.AddWithValue("@assessment_type", cmbAssessment.Text)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                            comm.Parameters.AddWithValue("@assess_by", Account_Name)
                            comm.ExecuteNonQuery()
                        End Using

                        Dim AdditionalSubjects As Integer = CInt(txtAdditionalSubjects.Text)
                        Using comm As New SqlCommand("UPDATE tbl_college_fee_loads SET Quantity = @Quantity, Fee_Amount = @FeeAmount WHERE Student_Number = @StudentNumber AND Academic_Yr = @ay AND Academic_Sem = @sem AND Fee_Code = @FeeCode AND Fee_Type = @FeeType", conn, t)
                            comm.Parameters.AddWithValue("@StudentNumber", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@FeeType", "TFEE ADDITIONAL")
                            comm.Parameters.AddWithValue("@FeeCode", "ADDITIONAL SUBJECTS")
                            comm.Parameters.AddWithValue("@Quantity", AdditionalSubjects)
                            comm.Parameters.AddWithValue("@FeeAmount", (CDbl(AdditionalSubjects * 1500)))
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                            comm.ExecuteNonQuery()
                        End Using

                        'TBL_ASSESSMENT_BREAKDOWN
                        'DELETE OLD ASSESSMENT BREAKDOWN
                        Using comm As New SqlCommand("DELETE FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay", conn, t)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                            comm.ExecuteNonQuery()
                        End Using

                        'INSERTING NEW ASSESSMENT BREAKDOWN
                        For i = 0 To dgAssessmentBreakdown.Rows.Count - 1
                            Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_ASSESSMENT_BREAKDOWN VALUES(NULL,@sn,@fee_code,@fee_amount,@due_date,@ay,@sem,GETDATE())", conn, t)
                                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                                comm.Parameters.AddWithValue("@fee_code", dgAssessmentBreakdown.Rows(i).Cells(0).Value)
                                comm.Parameters.AddWithValue("@fee_amount", dgAssessmentBreakdown.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@due_date", dgAssessmentBreakdown.Rows(i).Cells(2).Value)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                                comm.ExecuteNonQuery()
                            End Using
                        Next
                    End If

                    t.Commit()
                    MsgBox("Assessment information has been successfully saved!", MsgBoxStyle.Information)

                Catch ex As Exception
                    t.Rollback()
                    MsgBox("Transaction not completed due to error :" & vbNewLine & "Error: " & ex.Message, MsgBoxStyle.Critical)
                End Try
            End Using

        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim studentnumber As ReportParameter = New ReportParameter("sn", txtStudentNumber.Text)
        Dim studentname As ReportParameter = New ReportParameter("studentname", txtStudentName.Text)
        Dim edlevel As String = String.Empty

        If EducationLevel = "SENIOR HIGH" Then
            edlevel = "SHS"
        ElseIf EducationLevel = "JUNIOR HIGH" Then
            edlevel = "JHS"
        End If
        Dim studentcourse As ReportParameter = New ReportParameter("course", edlevel)
        Dim edcourse As String = String.Empty

        If EducationLevel = "SENIOR HIGH" Then
            edcourse = "-" & txtCourseCode.Text
        ElseIf EducationLevel = "JUNIOR HIGH" Then
            edcourse = ""
        End If


        Dim param_studentyear As ReportParameter = New ReportParameter("year", txtYear.Text & edcourse)
        Dim param_studentsection As ReportParameter = New ReportParameter("section", txtSection.Text)
        Dim param_studentpayment As ReportParameter = New ReportParameter("payment", cmbAssessment.Text)

        Dim assessor As String = String.Empty
        Dim assessmentdate As String = String.Empty

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_college_assessment_summary WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        assessmentdate = Format(CDate(reader("Assessed_Date")), "MM-dd-yyyy")
                        assessor = reader("Assess_By")
                    End While
                End Using
            End Using
        End Using

        Dim param_assessmentdate As ReportParameter = New ReportParameter("assessmentdate", assessmentdate)
        Dim param_assessor As ReportParameter = New ReportParameter("assessor", assessor)
        Dim param_oldbalance As ReportParameter = New ReportParameter("oldbalance", Convert_To_Currency(txtOldBalance.Text).ToString)
        Dim param_tuitionfee As ReportParameter = New ReportParameter("tuitionfee", Convert_To_Currency(CDbl(txtTFee.Text) - (CDbl(txtAdditionalSubjects.Text) * 900)).ToString)
        Dim param_AdditionalTFee As ReportParameter = New ReportParameter("AdditionalTFee", Convert_To_Currency(txtAdditionalSubjects.Text * 900).ToString)
        Dim param_miscellaneousfee As ReportParameter = New ReportParameter("miscellaneousfee", Convert_To_Currency(txtMFee.Text).ToString)
        Dim param_otherfees As ReportParameter = New ReportParameter("otherfees", Convert_To_Currency(txtOFee.Text).ToString)
        Dim param_surchargefee As ReportParameter = New ReportParameter("surchargefee", Convert_To_Currency(txtSurcharge.Text).ToString)
        Dim param_totalfee As ReportParameter = New ReportParameter("totalfee", Convert_To_Currency(CDbl(txtGrossFee.Text) + CDbl(txtSurcharge.Text)).ToString)
        Dim param_schoolsubsidy As ReportParameter = New ReportParameter("SchoolSubsidy", txtDirectDiscount.Text)
        Dim param_VoucherCode As ReportParameter = New ReportParameter("VoucherCode", cmbVoucher.Text)
        Dim param_VoucherAmount As ReportParameter = New ReportParameter("VoucherAmount", Convert_To_Currency(txtVoucherAmount.Text).ToString)
        Dim param_AcademicCode As ReportParameter = New ReportParameter("AcademicCode", cmbHonorDiscount.Text)
        Dim param_AcademicAmount As ReportParameter = New ReportParameter("AcademicAmount", Convert_To_Currency((CDbl(txtGrossFee.Text) - CDbl(txtVoucherAmount.Text)) * (txtHonorPerct.Text / 100)).ToString)
        Dim param_DiscountCode As ReportParameter = New ReportParameter("DiscountCode", cmbDiscount.Text)
        Dim param_DiscountAmount As ReportParameter = New ReportParameter("DiscountAmount", Convert_To_Currency(txtDiscountAmount.Text).ToString)
        Dim param_TotalDiscont As ReportParameter = New ReportParameter("TotalDiscount", Convert_To_Currency(txtDeductions.Text).ToString)
        Dim param_TotalDue As ReportParameter = New ReportParameter("TotalDue", Convert_To_Currency(txtNetFee.Text).ToString)
        Dim DS As New DS_PaymentSchedule
        Dim DR As DataRow

        With DS.Tables("DT_PaymentSchedule")
            .Rows.Clear()
            For i = 0 To dgAssessmentBreakdown.Rows.Count - 1
                DR = .NewRow
                DR("FeeCode") = dgAssessmentBreakdown.Rows(i).Cells(0).Value.ToString
                DR("FeeAmount") = Convert_To_Currency(dgAssessmentBreakdown.Rows(i).Cells(1).Value.ToString)
                DR("FeeDueDate") = dgAssessmentBreakdown.Rows(i).Cells(2).Value.ToString
                .Rows.Add(DR)
            Next
        End With

        Dim MyReport As New ReportDataSource("DSPaymentSchedule", DS.Tables("DT_PaymentSchedule"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_assessment_senior.rdlc"
            .ReportViewer1.LocalReport.SetParameters({studentnumber,
                                                      studentname,
                                                      studentcourse,
                                                      param_studentyear,
                                                      param_studentsection,
                                                      param_studentpayment,
                                                      param_assessmentdate,
                                                      param_assessor,
                                                      param_oldbalance,
                                                      param_tuitionfee,
                                                      param_AdditionalTFee,
                                                      param_miscellaneousfee,
                                                      param_otherfees,
                                                      param_surchargefee,
                                                      param_totalfee,
                                                      param_schoolsubsidy,
                                                      param_VoucherCode,
                                                      param_VoucherAmount,
                                                      param_AcademicCode,
                                                      param_AcademicAmount,
                                                      param_DiscountCode,
                                                      param_DiscountAmount,
                                                      param_TotalDiscont,
                                                      param_TotalDue})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With

    End Sub

    Private Sub txtStudentNumber_TextChanged(sender As Object, e As EventArgs) Handles txtStudentNumber.TextChanged
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            'CHECKING BALANCE OF STUDENTS
            Using comm As New SqlCommand("SELECT BALANCE FROM TBL_STUDENT_BALANCE WHERE STUDENT_NUMBER = @sn", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                If IsDBNull(comm.ExecuteScalar) = True Then
                    txtOldBalance.Text = Convert_To_Currency(0)
                ElseIf CDbl(comm.ExecuteScalar) = 0 Then
                    txtOldBalance.Text = Convert_To_Currency(0)
                ElseIf CDbl(comm.ExecuteScalar) > 0 Then
                    If MsgBox("Program Detected that this student has a registered balance of: " & Convert_To_Currency(CDbl(comm.ExecuteScalar)) & " do you want to apply it or not?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        txtOldBalance.Text = Convert_To_Currency(CDbl(comm.ExecuteScalar))
                    End If
                End If
            End Using
        End Using
    End Sub

    Private Sub cmbDiscount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDiscount.SelectedIndexChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With frm_highschool_assessment_old
            .txtSearch.Text = txtStudentName.Text
            .txtSearch.Text = .txtSearch.Text.Replace(",", "")
            .txtSearch.Text = StripSpaces(.txtSearch.Text)
            .cmbEducationLevel.Text = EducationLevel
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub cmbHonorDiscount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbHonorDiscount.SelectedIndexChanged

    End Sub

    Private Sub cmbHonorDiscount_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbHonorDiscount.SelectionChangeCommitted
        If cmbHonorDiscount.Text = "NONE" Then
            txtHonorPerct.Text = "0"
        ElseIf cmbHonorDiscount.Text <> "NONE" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT DISCOUNT_PERCENTAGE_AMOUNT FROM TBL_SETTINGS_DISCOUNTS WHERE DISCOUNT_CODE = @discount_code AND EDUCATION_LEVEL = @education_level AND Discount_Category = @Category AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                    comm.Parameters.AddWithValue("@education_level", EducationLevel)
                    comm.Parameters.AddWithValue("@discount_code", cmbHonorDiscount.Text)
                    comm.Parameters.AddWithValue("@Category", "ACADEMIC SCHOLARSHIP")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    txtHonorPerct.Text = Val(comm.ExecuteScalar)
                End Using
            End Using
        End If
        Calculate_Generate_Fees()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        With frm_old_balance_checker
            .Student_Number = txtStudentNumber.Text
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            txtOldBalance.Text = Convert_To_Currency(.totalbalance)
        End With
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim x = InputBox("Number of additional subjects", "Tuition Fee Additional")
        If IsNumeric(x) = True Then
            Dim TFee As Double = txtTFee.Text
            Dim TotalAdditionalAmount As Double = x * 900
            Dim AddtionalSubjects As Integer = txtAdditionalSubjects.Text
            TFee = TFee + TotalAdditionalAmount
            AddtionalSubjects = x
            txtAdditionalSubjects.Text = AddtionalSubjects
            txtTFee.Text = Convert_To_Currency(TFee)
            Calculate_Generate_Fees()
        Else
            MsgBox("Invalid input!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim x = InputBox("Please enter a direct discount amount", "DIRECT DISCOUNT")
        If IsNumeric(x) = True Then
            txtDirectDiscount.Text = Convert_To_Currency(x)
            Calculate_Generate_Fees()
        Else
            MsgBox("Invalid input!", MsgBoxStyle.Critical)
        End If
    End Sub
End Class