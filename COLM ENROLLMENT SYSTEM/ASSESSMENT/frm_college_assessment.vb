Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Public Class frm_college_assessment
    Public Course_Code As String = String.Empty
    Public YrLvl As String = String.Empty
    Public Section_Code As String = String.Empty
    Public Assessment_ID As String = String.Empty
    Public Assessment_Status As String = String.Empty
    Public EnrollmentStatus As String = String.Empty
    Public DGRowTFee As Integer = 0
    Public DGSchedRow As Integer = 0
    Dim DS As New DS_ASSESSMENT
    Dim DR As DataRow

    Public Sub CalculateEnergyFeeAndDefenceFee()
        Dim EnergyFee As Double = 0
        Dim DefenceFee As Double = 0
        Dim ListofSubjectDefenceFee As New List(Of String)
        Dim ListOfDefence As New List(Of KeyValuePair(Of String, Double))

        For i = 0 To DG_TFEE.Rows.Count - 1
            EnergyFee += DG_TFEE.Rows(i).Cells(9).Value
            If DG_TFEE.Rows(i).Cells(10).Value > 0 Then
                ListOfDefence.Add(New KeyValuePair(Of String, Double)("DEFENCE FEE" & DG_TFEE.Rows(i).Cells(1).Value.ToString.ToUpper, DG_TFEE.Rows(i).Cells(10).Value))
            End If
        Next

        Dim RowCount As Integer = DG_OFEE.Rows.Count - 1
        For i = 0 To RowCount
            If i <= RowCount Then
                If DG_OFEE.Rows(i).Cells(1).Value.ToString.Contains("ENERGY FEE") Then
                    DG_OFEE.Rows.Remove(DG_OFEE.Rows(i))
                    RowCount -= 1
                    i = 0
                End If
            End If

            If i <= RowCount Then
                If DG_OFEE.Rows(i).Cells(1).Value.ToString.Contains("DEFENCE FEE") Then
                    DG_OFEE.Rows.Remove(DG_OFEE.Rows(i))
                    RowCount -= 1
                    i = 0
                End If
            End If
        Next

        If EnergyFee > 0 Then
            DG_OFEE.Rows.Add("", "ENERGY FEE", Convert_To_Currency(EnergyFee))
        End If

        For Each pair As KeyValuePair(Of String, Double) In ListOfDefence
            DG_OFEE.Rows.Add("", pair.Key, Convert_To_Currency(pair.Value.ToString))
        Next
        '        DG_OFEE.Rows.Add("", "DEFENCE FEE", Convert_To_Currency(DefenceFee))

    End Sub

    Public Sub CalculateAdditionalFees()

        'VERIFY FIRST IF THE AFFILATION FEE IS ALREADY IN THE LIST
        'IF IT IS IN THE LIST THEN REMOVE IT FIRST BEFORE ADDING IT AUTOMATICALLY
        For i = 0 To DG_OFEE.Rows.Count - 1
            Try
                If DG_OFEE.Rows(i).Cells(1).Value.ToString.ToUpper = "AFFILIATION FEE & PERCENTER'S FEE" Then
                    DG_OFEE.Rows.Remove(DG_OFEE.Rows(i))
                End If
            Catch ex As Exception

            End Try
        Next

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            For i = 0 To DG_TFEE.Rows.Count - 1
                'VERIFY IF THE STUDENT IS RADTECH AND HAS AN CE1 OR CE 2 IF HAS THEN INCLUDE AFFILATION FEE
                'IF NO THEN REMOVE AFFILIATION FEE
                If DG_TFEE.Rows(i).Cells(1).Value = "CE 1" Or DG_TFEE.Rows(i).Cells(1).Value = "CE 2" Then
                    Dim IsFeeExists As Boolean = False
                    For x = 0 To DG_OFEE.Rows.Count - 1
                        If DG_OFEE.Rows(i).Cells(1).Value.ToString.ToUpper = "AFFILIATION FEE & PERCENTER'S FEE" Then
                            IsFeeExists = True
                            Exit For
                        End If
                    Next

                    If IsFeeExists = False Then
                        Using comm As New SqlCommand("SELECT * FROM tbl_settings_fees WHERE COURSE_CODE = 'BSRT' AND ACADEMIC_SEM = @sem AND ACADEMIC_YR = @ay AND FEE_CODE = 'AFFILIATION FEE & PERCENTER''S FEE'", conn)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            Using reader As SqlDataReader = comm.ExecuteReader
                                While reader.Read
                                    DG_OFEE.Rows.Add(reader("ID"), reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")), "REMOVE")
                                End While
                            End Using
                        End Using
                    End If
                End If
            Next
        End Using
    End Sub

    Public Sub Calculate_Generate_Fees()

        Dim Old_Balance As Double = 0 'OLD Balance
        Dim TFee As Double = 0 'TUITION FEE 
        Dim MFee As Double = 0 'MISCELLANEOUS
        Dim OFee As Double = 0 'OTHER FEE
        Dim V_Amount As Double = 0 'VOUCHER AMOUNT
        Dim D_Amount As Double = 0 'DISCOUNT AMOUNT
        Dim Total_Surcharge As Double = 0 'SURCHARGE
        Dim Surcharge_Multiplier As Integer = 0 'SURCHARGE MULTIPLIER
        Dim Deducation As Double = 0 'DEDUCTIONS
        Dim Gross_Amount As Double = 0 'GROSS
        Dim Net_Amount As Double = 0 'NET

        '-----PUTING VALUES TO VARIABLES---------------------------
        Old_Balance = txtOldBalance.Text
        TFee = txtTotalTFee.Text
        MFee = txtTotalMFee.Text
        OFee = txtTotalOFee.Text

        V_Amount = txtVoucherAmount.Text
        D_Amount = (TFee - V_Amount)
        D_Amount = D_Amount * (txtDiscountPercentage.Text / 100)
        Total_Surcharge = txtSurcharge.Text

        Gross_Amount = Old_Balance + TFee + MFee + OFee
        Deducation = V_Amount + D_Amount
        Net_Amount = (Gross_Amount - Deducation) + Total_Surcharge
        '----------------------------------------------------------

        'BREAK DOWN OF FEES
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_ASSESSMENTS WHERE Assessment_Type = @type AND Education_Level = @education_level AND Academic_yr = @ay AND Academic_sem = @sem ORDER BY Fee_Order ASC", conn)
                comm.Parameters.AddWithValue("@type", cmbAssessment.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@education_level", "COLLEGE")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_Summary.Rows.Clear()
                    While reader.Read

                        Dim T_Fee As Double = 0
                        Dim M_Fee As Double = 0
                        Dim O_Fee As Double = 0

                        Dim Fee_Code As String = String.Empty
                        Dim Due_Date As String = String.Empty
                        Dim PTF As Double = 0
                        Dim PMF As Double = 0
                        Dim POF As Double = 0


                        Fee_Code = reader("FEE_CODE")
                        Due_Date = reader("DUE_DATE")
                        PTF = reader("PTF") / 100
                        PMF = reader("PMF") / 100
                        POF = reader("POF") / 100

                        T_Fee = (TFee - Deducation) * PTF
                        M_Fee = MFee * PMF
                        O_Fee = OFee * POF

                        DG_Summary.Rows.Add(Fee_Code, Convert_To_Currency(T_Fee + M_Fee + O_Fee), Due_Date)

                    End While
                End Using
            End Using
        End Using

        'BREAKING DOWN THE SURCHARGES AND OLD BALANCE
        Dim Surcharge As Double = Total_Surcharge / (DG_Summary.Rows.Count - 1)
        Dim OBal As Double = Old_Balance / DG_Summary.Rows.Count
        For i = 0 To DG_Summary.Rows.Count - 1
            If i = 0 Then
                Dim Cell_Amount As Double = DG_Summary.Rows(i).Cells(1).Value
                DG_Summary.Rows(i).Cells(1).Value = Convert_To_Currency(Cell_Amount + OBal)
            Else
                Dim Cell_Amount As Double = DG_Summary.Rows(i).Cells(1).Value
                DG_Summary.Rows(i).Cells(1).Value = Convert_To_Currency(Cell_Amount + Surcharge + OBal)
            End If
        Next


        'PUTTING RESULT ON TEXTBOXES
        txtTFee.Text = Convert_To_Currency(TFee)
        txtMFee.Text = Convert_To_Currency(MFee)
        txtOFee.Text = Convert_To_Currency(OFee)
        txtVoucherAmount.Text = Convert_To_Currency(V_Amount)
        txtDiscountAmount.Text = Convert_To_Currency(D_Amount)

        txtGrossFee.Text = Convert_To_Currency(Gross_Amount)
        txtDeductions.Text = Convert_To_Currency(Deducation)
        txtNetFee.Text = Convert_To_Currency(Net_Amount)
    End Sub

    Public Sub View_Fees()
        CalculateAdditionalFees()
        CalculateEnergyFeeAndDefenceFee() 'CALCULATE FIRST THE ENERGY FEE AND DEFENCE FEE
        'TFEE
        Dim T_Amount As Double = 0
        Dim T_units As Integer = 0
        Dim EnergyFee As Double = 0
        Dim SubjectCount As Integer = 0

        For i = 0 To DG_TFEE.Rows.Count - 1
            If CDbl(DG_TFEE.Rows(i).Cells(6).Value) > 0 Then
                T_units += DG_TFEE.Rows(i).Cells(3).Value
                T_Amount += DG_TFEE.Rows(i).Cells(6).Value
                EnergyFee += DG_TFEE.Rows(i).Cells(9).Value
                SubjectCount += 1
            End If
        Next

        'MFEE
        Dim T_MFEE As Double = 0
        For i = 0 To DG_MFEE.Rows.Count - 1
            T_MFEE += DG_MFEE.Rows(i).Cells(2).Value
        Next

        'OFEE
        Dim T_OFEE As Double = 0
        For i = 0 To DG_OFEE.Rows.Count - 1
            T_OFEE += DG_OFEE.Rows(i).Cells(2).Value
        Next

        txtTotalSubjects.Text = DG_TFEE.Rows.Count
        txtTotalUnits.Text = T_units
        txtTotalTFee.Text = Convert_To_Currency(T_Amount)
        txtEnergyFee.Text = Convert_To_Currency(EnergyFee)

        txtTotalMFee.Text = Convert_To_Currency(T_MFEE)
        txtTotalOFee.Text = Convert_To_Currency(T_OFEE)
    End Sub

    Public Sub Load_Assessment_Types()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT ASSESSMENT_TYPE FROM TBL_SETTINGS_ASSESSMENTS WHERE EDUCATION_LEVEL = @education_level AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY ASSESSMENT_TYPE ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@education_level", "COLLEGE")
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
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_DISCOUNTS WHERE (EDUCATION_LEVEL = 'ALL' OR EDUCATION_LEVEL = @education_level) AND Academic_yr = @ay AND Academic_sem = @sem ORDER BY DISCOUNT_CATEGORY,DISCOUNT_CODE ASC", conn)
                comm.Parameters.AddWithValue("@education_level", "COLLEGE")
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
                        ElseIf reader("DISCOUNT_CATEGORY") = "VOUCHER" Then
                            cmbVoucher.Items.Add(reader("DISCOUNT_CODE"))
                        End If
                    End While
                End Using
            End Using
        End Using
        cmbDiscount.Text = "NONE"
        cmbVoucher.Text = "NONE"
    End Sub

    Public Sub Load_College_Default_Assessment()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT tbl_settings_college_curriculum_subjects_setted.SUBJ_CODE,tbl_settings_college_curriculum_subjects_setted.SUBJ_DESC,tbl_settings_college_curriculum_subjects_setted.SUBJ_UNIT,tbl_settings_college_curriculum_subjects_setted.LEC_HOURS,tbl_settings_college_curriculum_subjects_setted.LAB_HOURS,tbl_settings_college_curriculum_subjects_setted.SUBJ_PRICE,tbl_settings_college_curriculum_subjects_setted.ENERGY_FEE,tbl_settings_college_curriculum_subjects_setted.DEFENCE_FEE FROM tbl_settings_college_curriculum_subjects_setted INNER JOIN tbl_settings_college_curriculum_subjects_schedule ON tbl_settings_college_curriculum_subjects_setted.ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_YEAR = tbl_settings_college_curriculum_subjects_schedule.ACADEMIC_YEAR AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_SEM = tbl_settings_college_curriculum_subjects_schedule.ACADEMIC_SEM WHERE tbl_settings_college_curriculum_subjects_schedule.COURSE_CODE = @course_code AND tbl_settings_college_curriculum_subjects_schedule.YRLVL = @yrlvl AND tbl_settings_college_curriculum_subjects_schedule.SECTION_CODE = @section_id AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_YEAR = @ay AND tbl_settings_college_curriculum_subjects_setted.ACADEMIC_SEM = @sem AND IS_DELETED = 'FALSE' ORDER BY tbl_settings_college_curriculum_subjects_setted.SUBJ_CODE ASC", conn)
                comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                comm.Parameters.AddWithValue("@yrlvl", txtYear.Text)
                comm.Parameters.AddWithValue("@section_id", Get_SectionID(txtSection.Text, txtCourseCode.Text, txtYear.Text))
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)

                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_TFEE.Rows.Clear()
                    Dim amount As Double = 0
                    While reader.Read
                        amount += reader("SUBJ_PRICE")
                        DG_TFEE.Rows.Add("",
                                         reader("SUBJ_CODE"),
                                         reader("SUBJ_DESC"),
                                         reader("SUBJ_UNIT"),
                                         reader("LEC_HOURS"),
                                         reader("LAB_HOURS"),
                                         Convert_To_Currency(reader("SUBJ_PRICE")),
                                         "",
                                         "",
                                         Convert_To_Currency(reader("Energy_Fee")),
                                         Convert_To_Currency(reader("Defence_Fee")))
                    End While
                    txtTotalTFee.Text = Convert_To_Currency(amount)
                    txtTotalSubjects.Text = DG_TFEE.Rows.Count
                End Using
            End Using
        End Using
    End Sub

    Public Sub Load_College_Default_Fess()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            'MISCELLANOUES FEE
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE FEE_TYPE = 'MISCELLANEOUS FEE' AND FEE_STATUS = 'EXTERNAL' AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                comm.Parameters.AddWithValue("@yrlvl", txtYear.Text)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@education_level", "COLLEGE")

                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_MFEE.Rows.Clear()
                    Dim amount As Double = 0
                    While reader.Read
                        amount += reader("Fee_Amount")
                        DG_MFEE.Rows.Add("", reader("FEE_CODE").ToString.ToUpper, Convert_To_Currency(reader("FEE_AMOUNT")), "REMOVE")
                    End While
                    txtTotalMFee.Text = Convert_To_Currency(amount)
                End Using
            End Using


            'OTHER FEE
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_FEES WHERE FEE_TYPE = 'OTHER FEE' AND FEE_STATUS = 'EXTERNAL' AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                comm.Parameters.AddWithValue("@yrlvl", txtYear.Text)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@education_level", "COLLEGE")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_OFEE.Rows.Clear()
                    Dim amount As Double = 0
                    While reader.Read
                        amount += reader("Fee_Amount")
                        DG_OFEE.Rows.Add("", reader("FEE_CODE").ToString.ToUpper, Convert_To_Currency(reader("FEE_AMOUNT")), "REMOVE")
                    End While
                    txtTotalOFee.Text = Convert_To_Currency(amount)
                End Using
            End Using
            CalculateEnergyFeeAndDefenceFee() 'CALCULATE THE ENERGY FEE AND DEFENCE FEE
        End Using
    End Sub

    Public Sub Load_College_Default_Schedule()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT *,(SELECT SUBJ_CODE FROM tbl_settings_college_curriculum_subjects_setted WHERE ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID) AS SUBJ_CODE FROM tbl_settings_college_curriculum_subjects_schedule WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem ORDER BY SUBJ_CODE ASC", conn)
                comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                comm.Parameters.AddWithValue("@yrlvl", txtYear.Text)
                comm.Parameters.AddWithValue("@section_code", Get_SectionID(txtSection.Text, txtCourseCode.Text, txtYear.Text))
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    With DG_Schedule
                        .Rows.Clear()
                        While reader.Read
                            .Rows.Add(reader("SUBJ_CODE"), reader("SCHED_DAY"), reader("SCHED_TIME_IN"), reader("SCHED_TIME_OUT"), reader("SCHED_ROOM"), reader("FACULTY_NAME"))
                        End While
                    End With
                End Using
            End Using
        End Using
    End Sub

    Public Sub Load_Setted_Assessment_Information()
        DG_TFEE.Rows.Clear()
        DG_MFEE.Rows.Clear()
        DG_OFEE.Rows.Clear()
        DG_Schedule.Rows.Clear()
        DG_Summary.Rows.Clear()

        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            'FOR TUITON FEE TAB AND SCHEDULE TAB
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_SUBJECT_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem ORDER BY ID ASC", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_TFEE.Rows.Clear()
                    DG_Schedule.Rows.Clear()
                    While reader.Read
                        Dim SubjectCode As String = reader("Subj_Code")
                        Dim EnergyFeeAmount As Double = 0
                        Dim DefenceFeeAmount As Double = 0
                        Using conn1 As New SqlConnection(StringConnection)
                            conn1.Open()
                            Using comm1 As New SqlCommand("SELECT ENERGY_FEE,DEFENCE_FEE FROM tbl_settings_college_curriculum_subjects_setted WHERE SUBJ_CODE = @subj_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn1)
                                comm1.Parameters.AddWithValue("@subj_code", SubjectCode)
                                comm1.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                                comm1.Parameters.AddWithValue("@ay", Academic_Year)
                                comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                                Using reader1 As SqlDataReader = comm1.ExecuteReader
                                    While reader1.Read
                                        EnergyFeeAmount = reader1("Energy_Fee")
                                        DefenceFeeAmount = reader1("Defence_Fee")
                                    End While
                                End Using
                            End Using
                        End Using

                        If txtSection.Text = "IRREGULAR" Then
                            If reader("GRADE_EQUIVALENT") = "WP" Then
                                DG_TFEE.Rows.Add(reader("ID"),
                                                 reader("SUBJ_CODE"),
                                                 reader("SUBJ_DESC"),
                                                 reader("SUBJ_UNIT"),
                                                 reader("LEC_HOURS"),
                                                 reader("LAB_HOURS"),
                                                 Convert_To_Currency(reader("SUBJ_PRICE")),
                                                 "",
                                                 "UNDROP",
                                                 Convert_To_Currency(0))

                                DG_Schedule.Rows.Add(reader("SUBJ_CODE"),
                                                     reader("SCHED_DAY"),
                                                     reader("SCHED_TIME_IN"),
                                                     reader("SCHED_TIME_OUT"),
                                                     reader("SCHED_ROOM"),
                                                     reader("SCHED_FACULTY"))

                            ElseIf reader("GRADE_EQUIVALENT").ToString.Contains("REMOVED") Then
                                DG_TFEE.Rows.Add(reader("ID"),
                                                 reader("SUBJ_CODE"),
                                                 reader("SUBJ_DESC"),
                                                 reader("SUBJ_UNIT"),
                                                 reader("LEC_HOURS"),
                                                 reader("LAB_HOURS"),
                                                 Convert_To_Currency(reader("SUBJ_PRICE")),
                                                 "",
                                                 "UNREMOVE",
                                                 Convert_To_Currency(0))

                                DG_Schedule.Rows.Add(reader("SUBJ_CODE"),
                                                     reader("SCHED_DAY"),
                                                     reader("SCHED_TIME_IN"),
                                                     reader("SCHED_TIME_OUT"),
                                                     reader("SCHED_ROOM"),
                                                     reader("SCHED_FACULTY"))
                            Else
                                DG_TFEE.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), Convert_To_Currency(reader("SUBJ_PRICE")), "CHANGE", "DROP", Convert_To_Currency(EnergyFeeAmount), Convert_To_Currency(DefenceFeeAmount))
                                DG_Schedule.Rows.Add(reader("SUBJ_CODE"), reader("SCHED_DAY"), reader("SCHED_TIME_IN"), reader("SCHED_TIME_OUT"), reader("SCHED_ROOM"), reader("SCHED_FACULTY"))
                            End If
                        Else
                            DG_TFEE.Rows.Add(reader("ID"),
                                             reader("SUBJ_CODE"),
                                             reader("SUBJ_DESC"),
                                             reader("SUBJ_UNIT"),
                                             reader("LEC_HOURS"),
                                             reader("LAB_HOURS"),
                                             Convert_To_Currency(reader("SUBJ_PRICE")),
                                             "", "",
                                             Convert_To_Currency(EnergyFeeAmount),
                                             Convert_To_Currency(DefenceFeeAmount))
                            DG_Schedule.Rows.Add(reader("SUBJ_CODE"), reader("SCHED_DAY"), reader("SCHED_TIME_IN"), reader("SCHED_TIME_OUT"), reader("SCHED_ROOM"), reader("SCHED_FACULTY"))
                        End If
                    End While
                End Using
            End Using


            'FOR MFEE TAB AND OFEE TAB
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem ORDER BY ID ASC", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_MFEE.Rows.Clear()
                    DG_OFEE.Rows.Clear()
                    While reader.Read
                        If txtSection.Text = "IRREGULAR" Then
                            If reader("FEE_TYPE") = "MFEE" Then
                                DG_MFEE.Rows.Add(reader("ID"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), "REMOVE")
                            ElseIf reader("FEE_TYPE") = "OFEE" Then
                                DG_OFEE.Rows.Add(reader("ID"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), "REMOVE")
                            End If
                        Else
                            If reader("FEE_TYPE") = "MFEE" Then
                                DG_MFEE.Rows.Add(reader("ID"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")))
                            ElseIf reader("FEE_TYPE") = "OFEE" Then
                                DG_OFEE.Rows.Add(reader("ID"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")))
                            End If
                        End If
                    End While
                End Using
            End Using

            'FOR ASSESSMENT BREAKDOWN
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_Summary.Rows.Clear()
                    While reader.Read
                        DG_Summary.Rows.Add(reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), reader("DUE_DATE"))
                    End While
                End Using
            End Using

            'FOR ASSESSMENT SUMMARY TAB
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        Assessment_ID = reader("ID")
                        cmbAssessment.Text = reader("ASSESSMENT_TYPE")
                        cmbVoucher.Text = reader("VOUCHER_CODE")
                        txtVoucherAmount.Text = Convert_To_Currency(reader("VOUCHER_AMOUNT"))
                        cmbDiscount.Text = reader("DISCOUNT_CODE")
                        txtDiscountPercentage.Text = reader("DISCOUNT_PERCENTAGE")
                        txtDiscountAmount.Text = Convert_To_Currency(reader("DISCOUNT_AMOUNT"))
                        txtOldBalance.Text = Convert_To_Currency(reader("OLD_BALANCE"))
                        txtTotalTFee.Text = Convert_To_Currency(reader("TFEE"))
                        txtTotalMFee.Text = Convert_To_Currency(reader("MFEE"))
                        txtTotalOFee.Text = Convert_To_Currency(reader("OFEE"))

                        txtTFee.Text = Convert_To_Currency(reader("TFEE"))
                        txtMFee.Text = Convert_To_Currency(reader("MFEE"))
                        txtOFee.Text = Convert_To_Currency(reader("OFEE"))
                        txtSurcharge.Text = Convert_To_Currency(reader("SURCHARGE"))

                        txtGrossFee.Text = Convert_To_Currency(CDbl(txtTFee.Text) + CDbl(txtMFee.Text) + CDbl(txtOFee.Text))
                        txtDeductions.Text = Convert_To_Currency(reader("VOUCHER_AMOUNT") + reader("DISCOUNT_AMOUNT"))
                        txtNetFee.Text = Convert_To_Currency(reader("TOTAL"))
                    End While
                End Using
            End Using
            View_Fees()
        End Using
    End Sub

    Public Sub Assessment_Copier(sn As String)
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT *,(SELECT Energy_Fee FROM tbl_settings_college_curriculum_subjects_setted WHERE Subj_Code = tbl_college_subject_loads.subj_code AND Course_Code = @course_code AND Academic_Year = @ay AND Academic_Sem = @sem) AS Energy_Fee,(SELECT Defence_Fee FROM tbl_settings_college_curriculum_subjects_setted WHERE Subj_Code = tbl_college_subject_loads.subj_code AND Course_Code = @course_code AND Academic_Year = @ay AND Academic_Sem = @sem) AS Defence_Fee FROM tbl_college_subject_loads WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@sn", sn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DG_TFEE.Rows.Add(reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), reader("SUBJ_PRICE"), "CHANGE", "DROP", reader("ENERGY_FEE"), reader("DEFENCE_FEE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub Clear_Controls()
        DG_TFEE.Rows.Clear()
        DG_MFEE.Rows.Clear()
        DG_OFEE.Rows.Clear()
        DG_Schedule.Rows.Clear()
        DG_Summary.Rows.Clear()

        txtTotalSubjects.Text = "0"
        txtTotalTFee.Text = "0.00"
        txtTotalMFee.Text = "0.00"
        txtTotalOFee.Text = "0.00"
        cmbAssessment.Items.Clear()
        cmbVoucher.Items.Clear()
        txtVoucherAmount.Text = "0.00"
        cmbDiscount.Items.Clear()
        txtDiscountPercentage.Text = "0"
        txtDiscountAmount.Text = "0.00"
        txtOldBalance.Text = "0"
        txtTFee.Text = "0.00"
        txtMFee.Text = "0.00"
        txtOFee.Text = "0.00"
        txtSurcharge.Text = "0.00"
        txtDeductions.Text = "0.00"
        txtGrossFee.Text = "0.00"
        txtNetFee.Text = "0.00"
    End Sub
    Public Sub ClearAssessmentSummary()
        cmbAssessment.SelectedIndex = -1
        cmbVoucher.SelectedIndex = -1
        txtVoucherAmount.Text = Convert_To_Currency(0)
        cmbDiscount.SelectedIndex = -1
        txtDiscountPercentage.Text = "0"
        txtDiscountAmount.Text = Convert_To_Currency(0)
        txtOldBalance.Text = Convert_To_Currency(0)
        DG_Summary.Rows.Clear()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub frm_college_assessment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Assessment_Status = "ASSESSMENT" Then
            Load_Assessment_Types()
            Load_Discounts_And_Vouchers()
        End If
    End Sub

    Private Sub txtyear_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            DS._1_Student_Assessment_Summary.Rows.Clear()
            DS._2_Student_Subjects.Rows.Clear()
            DS._3_Student_Assessment_Breakdown.Rows.Clear()

            If txtCourseCode.Text = String.Empty Then
                MsgBox("Please select student course!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If txtYear.Text = String.Empty Then
                MsgBox("No Year Detected Please Edit this in registration!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If txtSection.Text = String.Empty Then
                MsgBox("No Section Detected Please Edit this in registration!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim NoofNoSettedSchedule As Integer = 0
            For i = 0 To DG_Schedule.Rows.Count - 1
                If DG_Schedule.Rows(i).Cells(1).Value = "-" Then
                    NoofNoSettedSchedule += 1
                End If
            Next

            If NoofNoSettedSchedule > 0 Then
                If MsgBox("Program detected that you not setted " & NoofNoSettedSchedule & " of schedule in schedule tab!" & vbNewLine & " Do you want to continue saving?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            If cmbAssessment.Text = String.Empty Then
                MsgBox("Please select assessment type!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            View_Fees()
            Calculate_Generate_Fees()

            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                'INSERTING NEW SUBJECT LOADS
                If Assessment_Status = "ASSESSMENT" Then
                    'INSERTING NEW ASSESSMENT SUMMARY
                    Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_ASSESSMENT_SUMMARY VALUES(@education_level,@sn,@course_code,@yrlvl,@sect_code,@tfee,@mfee,@ofee,@voucher_code,@voucher_amount,@discount_code,@discount_percentage,@discount_amount,NULL,@surcharge,@old_balance,@total,@assessment_type,@ay,@sem,@assess_by,GETDATE(),'ACTIVE')", conn)
                        comm.Parameters.AddWithValue("@education_level", "COLLEGE")
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
                        comm.Parameters.AddWithValue("@surcharge", txtSurcharge.Text)
                        comm.Parameters.AddWithValue("@total", txtNetFee.Text)
                        comm.Parameters.AddWithValue("@assessment_type", cmbAssessment.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.Parameters.AddWithValue("@assess_by", Account_Name)
                        comm.ExecuteNonQuery()
                    End Using

                    'TBL_COLLEGE_SUBJECT_LOADS
                    For i = 0 To DG_Schedule.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_SUBJECT_LOADS VALUES(NULL,@sn,@subj_code,@subj_desc,@subj_unit,@lec_hours,@lab_hours,@subj_price,@sched_day,@sched_time_in,@sched_time_out,@sched_room,@sched_faculty,0,'N.A','N.A',@encoder,@ay,@sem,GETDATE())", conn)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@subj_code", DG_TFEE.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@subj_desc", DG_TFEE.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@subj_unit", DG_TFEE.Rows(i).Cells(3).Value)
                            comm.Parameters.AddWithValue("@lec_hours", DG_TFEE.Rows(i).Cells(4).Value)
                            comm.Parameters.AddWithValue("@lab_hours", DG_TFEE.Rows(i).Cells(5).Value)
                            comm.Parameters.AddWithValue("@subj_price", DG_TFEE.Rows(i).Cells(6).Value)
                            comm.Parameters.AddWithValue("@sched_day", DG_Schedule.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@sched_time_in", DG_Schedule.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@sched_time_out", DG_Schedule.Rows(i).Cells(3).Value)
                            comm.Parameters.AddWithValue("@sched_room", DG_Schedule.Rows(i).Cells(4).Value)
                            comm.Parameters.AddWithValue("@sched_faculty", DG_Schedule.Rows(i).Cells(5).Value)
                            comm.Parameters.AddWithValue("@encoder", Account_Name)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next

                    'INSERTING NEW FEES
                    'TBL_COLLEGE_FEE_LOADS
                    '-MFEE
                    For i = 0 To DG_MFEE.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_FEE_LOADS VALUES(NULL,@sn,@fee_type,NULL,@fee_code,1,@fee_amount,@ay,@sem,GETDATE())", conn)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@fee_type", "MFEE")
                            comm.Parameters.AddWithValue("@fee_code", DG_MFEE.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@fee_amount", DG_MFEE.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next

                    'TBL_COLLEGE_FEE_LOADS
                    '-OFEE
                    For i = 0 To DG_OFEE.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_FEE_LOADS VALUES(NULL,@sn,@fee_type,NULL,@fee_code,1,@fee_amount,@ay,@sem,GETDATE())", conn)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@fee_type", "OFEE")
                            comm.Parameters.AddWithValue("@fee_code", DG_OFEE.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@fee_amount", DG_OFEE.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next

                    'TBL_ASSESSMENT_BREAKDOWN
                    For i = 0 To DG_Summary.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_ASSESSMENT_BREAKDOWN VALUES(NULL,@sn,@fee_code,@fee_amount,@due_date,@ay,@sem,GETDATE())", conn)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@fee_code", DG_Summary.Rows(i).Cells(0).Value)
                            comm.Parameters.AddWithValue("@fee_amount", DG_Summary.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@due_date", DG_Summary.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next

                    'UPDATE TBL_STUDENT_REGISTERED SET ENROLLMENT_STATUS = 'ASSESSED'
                    Using comm As New SqlCommand("UPDATE TBL_STUDENT_REGISTERED SET ENROLLMENT_STATUS = 'ASSESSED' WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                        comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.ExecuteNonQuery()
                    End Using

                    Assessment_Status = "RE-ASSESSMENT"
                    Load_College_Default_Fess()
                    LinkAddRegSubj.Visible = True
                    LinkMFee.Visible = True
                    LinkOFee.Visible = True
                    Column5.Visible = True 'CHANGE SUBJECT
                    Column15.Visible = False 'DROP SUBJECT
                    DataGridViewLinkColumn1.Visible = True 'MFEE
                    DataGridViewLinkColumn2.Visible = True ' OFEE
                    Load_Setted_Assessment_Information()

                ElseIf Assessment_Status = "RE-ASSESSMENT" Then

                    'TBL_COLLEGE_ASSESSMENT_SUMMARY
                    Using comm As New SqlCommand("UPDATE TBL_COLLEGE_ASSESSMENT_SUMMARY SET STUDENT_NUMBER = @sn, COURSE_CODE = @course_code, YRLVL = @yrlvl, SECT_CODE = @sect_code, TFEE = @tfee, MFEE = @mfee, OFEE = @ofee, VOUCHER_CODE = @voucher_code, VOUCHER_AMOUNT = @voucher_amount, DISCOUNT_CODE = @discount_code, DISCOUNT_PERCENTAGE = @discount_percentage, DISCOUNT_AMOUNT = @discount_amount, SURCHARGE = @surcharge, OLD_BALANCE = @old_balance, TOTAL = @total, ASSESSMENT_TYPE = @assessment_type, ACADEMIC_YR = @ay, ACADEMIC_SEM = @sem, ASSESS_BY = @assess_by, ASSESSED_DATE = GETDATE() WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", Assessment_ID)
                        comm.Parameters.AddWithValue("@education_level", "COLLEGE")
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
                        comm.Parameters.AddWithValue("@surcharge", txtSurcharge.Text)
                        comm.Parameters.AddWithValue("@total", txtNetFee.Text)
                        comm.Parameters.AddWithValue("@assessment_type", cmbAssessment.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.Parameters.AddWithValue("@assess_by", Account_Name)
                        comm.ExecuteNonQuery()
                    End Using

                    'TBL_COLLEGE_SUBJECT_LOADS
                    For i = 0 To DG_TFEE.Rows.Count - 1
                        If Val(DG_TFEE.Rows(i).Cells(0).Value) = 0 Then
                            Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_SUBJECT_LOADS VALUES(NULL,@sn,@subj_code,@subj_desc,@subj_unit,@lec_hours,@lab_hours,@subj_price,@sched_day,@sched_time_in,@sched_time_out,@sched_room,@sched_faculty,0,'N.A','N.A',@encoder,@ay,@sem,GETDATE())", conn)
                                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                                comm.Parameters.AddWithValue("@subj_code", DG_TFEE.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@subj_desc", DG_TFEE.Rows(i).Cells(2).Value)
                                comm.Parameters.AddWithValue("@subj_unit", DG_TFEE.Rows(i).Cells(3).Value)
                                comm.Parameters.AddWithValue("@lec_hours", DG_TFEE.Rows(i).Cells(4).Value)
                                comm.Parameters.AddWithValue("@lab_hours", DG_TFEE.Rows(i).Cells(5).Value)
                                comm.Parameters.AddWithValue("@subj_price", DG_TFEE.Rows(i).Cells(6).Value)
                                comm.Parameters.AddWithValue("@sched_day", DG_Schedule.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@sched_time_in", DG_Schedule.Rows(i).Cells(2).Value)
                                comm.Parameters.AddWithValue("@sched_time_out", DG_Schedule.Rows(i).Cells(3).Value)
                                comm.Parameters.AddWithValue("@sched_room", DG_Schedule.Rows(i).Cells(4).Value)
                                comm.Parameters.AddWithValue("@sched_faculty", DG_Schedule.Rows(i).Cells(5).Value)
                                comm.Parameters.AddWithValue("@encoder", Account_Name)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.ExecuteNonQuery()
                            End Using
                        Else
                            Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET STUDENT_NUMBER = @sn, SUBJ_CODE = @subj_code, SUBJ_DESC = @subj_desc, SUBJ_UNIT = @subj_unit, LEC_HOURS = @lec_hours, LAB_HOURS = @lab_hours, SUBJ_PRICE = @subj_price, SCHED_DAY = @sched_day, SCHED_TIME_IN = @sched_time_in, SCHED_TIME_OUT = @sched_time_out, SCHED_ROOM = @sched_room, SCHED_FACULTY = @sched_faculty, GRADE_AVERAGE = 0, ENCODER = @encoder, ACADEMIC_YR = @ay, ACADEMIC_SEM = @sem, DATE_SAVE = GETDATE() WHERE ID = @id", conn)
                                comm.Parameters.AddWithValue("@id", DG_TFEE.Rows(i).Cells(0).Value)
                                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                                comm.Parameters.AddWithValue("@subj_code", DG_TFEE.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@subj_desc", DG_TFEE.Rows(i).Cells(2).Value)
                                comm.Parameters.AddWithValue("@subj_unit", DG_TFEE.Rows(i).Cells(3).Value)
                                comm.Parameters.AddWithValue("@lec_hours", DG_TFEE.Rows(i).Cells(4).Value)
                                comm.Parameters.AddWithValue("@lab_hours", DG_TFEE.Rows(i).Cells(5).Value)
                                comm.Parameters.AddWithValue("@subj_price", DG_TFEE.Rows(i).Cells(6).Value)
                                comm.Parameters.AddWithValue("@sched_day", DG_Schedule.Rows(i).Cells(1).Value)
                                comm.Parameters.AddWithValue("@sched_time_in", DG_Schedule.Rows(i).Cells(2).Value)
                                comm.Parameters.AddWithValue("@sched_time_out", DG_Schedule.Rows(i).Cells(3).Value)
                                comm.Parameters.AddWithValue("@sched_room", DG_Schedule.Rows(i).Cells(4).Value)
                                comm.Parameters.AddWithValue("@sched_faculty", DG_Schedule.Rows(i).Cells(5).Value)
                                comm.Parameters.AddWithValue("@encoder", Account_Name)
                                comm.Parameters.AddWithValue("@ay", Academic_Year)
                                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm.ExecuteNonQuery()
                            End Using
                        End If
                    Next


                    'TBL_COLLEGE_FEE_LOADS
                    'DELETE OLD EXTERNAL FEES
                    Using comm As New SqlCommand("DELETE FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem AND FEE_TYPE <> 'OTHER FEE (INTERNAL)'", conn)
                        comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.ExecuteNonQuery()
                    End Using

                    'INSERTING NEW FEES
                    '-MFEE
                    For i = 0 To DG_MFEE.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_FEE_LOADS VALUES(NULL,@sn,@fee_type,NULL,@fee_code,1,@fee_amount,@ay,@sem,GETDATE())", conn)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@fee_type", "MFEE")
                            comm.Parameters.AddWithValue("@fee_code", DG_MFEE.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@fee_amount", DG_MFEE.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next

                    'TBL_COLLEGE_FEE_LOADS
                    '-OFEE
                    For i = 0 To DG_OFEE.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_FEE_LOADS VALUES(NULL,@sn,@fee_type,NULL,@fee_code,1,@fee_amount,@ay,@sem,GETDATE())", conn)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@fee_type", "OFEE")
                            comm.Parameters.AddWithValue("@fee_code", DG_OFEE.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@fee_amount", DG_OFEE.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next

                    'TBL_ASSESSMENT_BREAKDOWN
                    'DELETE OLD ASSESSMENT BREAKDOWN
                    Using comm As New SqlCommand("DELETE FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                        comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.ExecuteNonQuery()
                    End Using

                    'INSERTING NEW ASSESSMENT BREAKDOWN
                    For i = 0 To DG_Summary.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO TBL_COLLEGE_ASSESSMENT_BREAKDOWN VALUES(NULL,@sn,@fee_code,@fee_amount,@due_date,@ay,@sem,GETDATE())", conn)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@fee_code", DG_Summary.Rows(i).Cells(0).Value)
                            comm.Parameters.AddWithValue("@fee_amount", DG_Summary.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@due_date", DG_Summary.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next
                End If
                MsgBox("Assessment information has been successfully saved!", MsgBoxStyle.Information)
            End Using
        Catch ex As Exception
            RecordErros(Me.Name, ex.Message)
            MsgBox("Unidentified Error Occured please retry transaction!" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkMFee.LinkClicked
        With frm_college_assessment_browse_fees
            .Course = txtCourseCode.Text
            .Yrlvl = txtYear.Text
            .Education_Level = "COLLEGE"
            .Fee_Type = "MISCELLANEOUS FEE"
            .Load_Fees()
            .lblFees.Text = "MISCELLANEOUS FEE"
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            View_Fees()
            If Assessment_Status = "RE-ASSESSMENT" Then
                Calculate_Generate_Fees()
            End If
        End With
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkOFee.LinkClicked
        With frm_college_assessment_browse_fees
            .Course = txtCourseCode.Text
            .Yrlvl = txtYear.Text
            .Education_Level = "COLLEGE"
            .Fee_Type = "OTHER FEE"
            .Load_Fees()
            .lblFees.Text = "MISCELLANEOUS FEE"
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            View_Fees()
            If Assessment_Status = "RE-ASSESSMENT" Then
                Calculate_Generate_Fees()
            End If
        End With
    End Sub

    Private Sub DG_MFEE_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_MFEE.CellContentClick
        If e.ColumnIndex = 3 Then
            If DG_MFEE.Rows(e.RowIndex).Cells(3).Value = "REMOVE" Then
                If MsgBox("Are you sure you want to remove this fee?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    DG_MFEE.Rows.Remove(DG_MFEE.Rows(e.RowIndex))
                End If
            End If
        End If
    End Sub

    Private Sub DG_OFEE_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_OFEE.CellContentClick
        If e.ColumnIndex = 3 Then
            If DG_OFEE.Rows(e.RowIndex).Cells(3).Value = "REMOVE" Then
                If MsgBox("Are you sure you want to remove this fee?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    DG_OFEE.Rows.Remove(DG_OFEE.Rows(e.RowIndex))
                End If
            End If
        End If
    End Sub

    Private Sub LinkAddRegSubj_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkAddRegSubj.LinkClicked
        With frm_college_assessment_browse_subject
            .Text = "LIST OF AVAILABLE SUBJECTS IN CURRICULUM OF: " & txtCourseCode.Text
            .SavingStatus = "ADD SUBJECT"
            .Course = txtCourseCode.Text
            .DGRow = DGRowTFee
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            View_Fees()
            Calculate_Generate_Fees()
            If Assessment_Status = "RE-ASSESSMENT" Then
                View_Fees()
                Calculate_Generate_Fees()
            End If
        End With
    End Sub

    Private Sub DG_TFEE_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_TFEE.CellContentClick
        If e.ColumnIndex = 7 Then
            If MsgBox("Are you sure you want to change this subject?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                With frm_college_assessment_browse_subject
                    .Text = "LIST OF AVAILABLE SUBJECTS IN CURRICULUM OF: " & txtCourseCode.Text
                    .SavingStatus = "CHANGE SUBJECT"
                    .Course = txtCourseCode.Text
                    .DGRow = DGRowTFee
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog()
                    View_Fees()
                    If Assessment_Status = "RE-ASSESSMENT" Then
                        Calculate_Generate_Fees()
                    End If
                End With
            End If
        ElseIf e.ColumnIndex = 8 Then
            If DG_TFEE.Rows(e.RowIndex).Cells(8).Value = "DROP" Then
                If MsgBox("Are you sure that " & txtStudentName.Text & " want to DROP this subject: " & DG_TFEE.Rows(e.RowIndex).Cells(1).Value, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    If MsgBox("You have a 2 choices press YES if you want removed it PERMANENTLY and NO if you want to removed it TEMPORARILY and mark as WP", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Using conn As New SqlConnection(StringConnection)
                            conn.Open()
                            Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET SUBJ_PRICE = '0', GRADE_EQUIVALENT = 'WP' WHERE ID = @subj_id", conn)
                                comm.Parameters.AddWithValue("@subj_id", DG_TFEE.Rows(e.RowIndex).Cells(0).Value)
                                comm.ExecuteNonQuery()
                                DG_TFEE.Rows(e.RowIndex).Cells(6).Value = Convert_To_Currency("0")
                                DG_TFEE.Rows(e.RowIndex).Cells(7).Value = ""
                                DG_TFEE.Rows(e.RowIndex).Cells(8).Value = "UNDROP"
                                DG_TFEE.Rows(e.RowIndex).Cells(9).Value = Convert_To_Currency("0")
                                MsgBox("Subject has been successfully dropped!", MsgBoxStyle.Information)
                                View_Fees()
                                Calculate_Generate_Fees()
                            End Using
                        End Using
                    Else
                        'BERFORE DELETING THE SUBJECT PERMANENTLY WE NEED TO VERIFY IF THERE'S AN EXISTING GRADE
                        Dim IsGradeExists As Boolean = False
                        Using conn As New SqlConnection(StringConnection)
                            conn.Open()
                            Using comm As New SqlCommand("SELECT * FROM tbl_college_subject_loads WHERE ID = @subj_id", conn)
                                comm.Parameters.AddWithValue("@subj_id", DG_TFEE.Rows(e.RowIndex).Cells(0).Value)
                                Using reader As SqlDataReader = comm.ExecuteReader
                                    While reader.Read
                                        If reader("Grade_Equivalent") <> "N.A" Then
                                            IsGradeExists = True
                                        End If
                                    End While
                                End Using
                            End Using
                            If IsGradeExists = True Then
                                MsgBox("There is an existing grade in this subject and cannot be removed" & vbNewLine & " if you want to remove it you need to remark the grade as N.A in student grade report under report tab!", MsgBoxStyle.Information)
                            Else
                                Using comm As New SqlCommand("DELETE FROM TBL_COLLEGE_SUBJECT_LOADS WHERE ID = @subj_id", conn)
                                    comm.Parameters.AddWithValue("@subj_id", DG_TFEE.Rows(e.RowIndex).Cells(0).Value)
                                    comm.ExecuteNonQuery()
                                    DG_TFEE.Rows.Remove(DG_TFEE.Rows(e.RowIndex))
                                    MsgBox("Subject has been successfully removed permanently!", MsgBoxStyle.Information)
                                    View_Fees()
                                    Calculate_Generate_Fees()
                                End Using
                            End If
                        End Using
                    End If
                End If
            ElseIf DG_TFEE.Rows(e.RowIndex).Cells(8).Value = "UNDROP" Then
                If MsgBox("Are you sure that " & txtStudentName.Text & " want to UNDROP this subject: " & DG_TFEE.Rows(e.RowIndex).Cells(1).Value, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Using conn As New SqlConnection(StringConnection)
                        conn.Open()

                        'GETTING THE PRICE OF THE SUBJECT
                        Dim SubjPrice As Double = 0
                        Dim EnergyFee As Double = 0
                        Using comm As New SqlCommand("SELECT SUBJ_PRICE,Energy_Fee FROM tbl_settings_college_curriculum_subjects_setted WHERE ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem AND SUBJ_CODE = @subj_code AND COURSE_CODE = @course_code", conn)
                            comm.Parameters.AddWithValue("@subj_code", DG_TFEE.Rows(e.RowIndex).Cells(1).Value)
                            comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            Using reader As SqlDataReader = comm.ExecuteReader
                                While reader.Read
                                    SubjPrice = reader("SUBJ_PRICE")
                                    EnergyFee = reader("ENERGY_FEE")
                                End While
                            End Using
                        End Using

                        'UPDATING THE SUBJECT TO UNDROP SETTING THE GRADE EQUIVALENT INTO N.A AND SUBJECT PRICE TO FETCHED AMOUNT
                        Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET SUBJ_PRICE = @subj_price, GRADE_EQUIVALENT = 'N.A' WHERE ID = @subj_id", conn)
                            comm.Parameters.AddWithValue("@subj_id", DG_TFEE.Rows(e.RowIndex).Cells(0).Value)
                            comm.Parameters.AddWithValue("@subj_price", SubjPrice)
                            comm.ExecuteNonQuery()
                            DG_TFEE.Rows(e.RowIndex).Cells(6).Value = Convert_To_Currency(SubjPrice)
                            DG_TFEE.Rows(e.RowIndex).Cells(7).Value = "CHANGE"
                            DG_TFEE.Rows(e.RowIndex).Cells(8).Value = "DROP"
                            DG_TFEE.Rows(e.RowIndex).Cells(9).Value = Convert_To_Currency(EnergyFee)
                            MsgBox("Subject has been successfully UNDROPPED!", MsgBoxStyle.Information)
                        End Using
                    End Using
                End If
            ElseIf DG_TFEE.Rows(e.RowIndex).Cells(8).Value = "UNREMOVE" Then
                If MsgBox("Are you sure that " & txtStudentName.Text & " want to UNREMOVE this subject: " & DG_TFEE.Rows(e.RowIndex).Cells(1).Value, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Using conn As New SqlConnection(StringConnection)
                        conn.Open()

                        'GETTING THE PRICE OF THE SUBJECT
                        Dim SubjPrice As Double = 0
                        Using comm As New SqlCommand("SELECT SUBJ_PRICE FROM tbl_settings_college_curriculum_subjects_setted WHERE ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem AND SUBJ_CODE = @subj_code AND COURSE_CODE = @course_code", conn)
                            comm.Parameters.AddWithValue("@subj_code", DG_TFEE.Rows(e.RowIndex).Cells(1).Value)
                            comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            SubjPrice = comm.ExecuteScalar
                        End Using

                        'UPDATING THE SUBJECT TO UNDROP SETTING THE GRADE EQUIVALENT INTO N.A AND SUBJECT PRICE TO FETCHED AMOUNT
                        Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET SUBJ_PRICE = @subj_price, GRADE_EQUIVALENT = 'N.A' WHERE ID = @subj_id", conn)
                            comm.Parameters.AddWithValue("@subj_id", DG_TFEE.Rows(e.RowIndex).Cells(0).Value)
                            comm.Parameters.AddWithValue("@subj_price", SubjPrice)
                            comm.ExecuteNonQuery()
                            DG_TFEE.Rows(e.RowIndex).Cells(6).Value = Convert_To_Currency(SubjPrice)
                            DG_TFEE.Rows(e.RowIndex).Cells(7).Value = "CHANGE"
                            DG_TFEE.Rows(e.RowIndex).Cells(8).Value = "DROP"
                            MsgBox("Subject has been successfully UNREMOVED!", MsgBoxStyle.Information)
                        End Using
                    End Using
                End If
            End If

        End If
    End Sub

    Private Sub txtsection_SelectionChangeCommitted(sender As Object, e As EventArgs)
        Load_College_Default_Assessment()
        Load_College_Default_Fess()
        Load_College_Default_Schedule()

        View_Fees()
        If txtSection.Text = "IRREGULAR" Then
            LinkAddRegSubj.Visible = True
            LinkMFee.Visible = True
            LinkOFee.Visible = True
        Else
            LinkAddRegSubj.Visible = False
            LinkMFee.Visible = False
            LinkOFee.Visible = False
        End If
    End Sub

    Private Sub cmbAssessment_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbAssessment.SelectionChangeCommitted
        If cmbAssessment.Text <> "CASH" Then
            Dim Surcharge As Double = 0
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                'GETTING THE SURCHARGE
                Using comm As New SqlCommand("SELECT SURCHARGE FROM TBL_SETTINGS_SURCHARGES WHERE EDUCATION_LEVEL = @education_level AND Academic_Year = @ay AND Academic_Sem = @sem", conn)
                    comm.Parameters.AddWithValue("@education_level", "COLLEGE")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Surcharge = Convert_To_Currency(Val(comm.ExecuteScalar))
                End Using
            End Using

            Dim Surcharge_Multiplier As Double = 0

            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                'GETTING THE COUNT OF SURCHARGE MULTIPLIER
                Using comm As New SqlCommand("SELECT COUNT(ID) FROM TBL_SETTINGS_ASSESSMENTS WHERE ASSESSMENT_TYPE = @type AND EDUCATION_LEVEL = @education_level AND Academic_Yr = @ay AND Academic_Sem = @Sem", conn)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@type", cmbAssessment.Text)
                    comm.Parameters.AddWithValue("@education_level", "COLLEGE")
                    Surcharge_Multiplier = Val(comm.ExecuteScalar) - 1
                End Using
            End Using
            txtSurcharge.Text = Convert_To_Currency(Surcharge * Surcharge_Multiplier)

        Else
            txtSurcharge.Text = Convert_To_Currency("0")
        End If
        View_Fees()
        Calculate_Generate_Fees()
    End Sub

    Private Sub cmbVoucher_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbVoucher.SelectionChangeCommitted
        Try
            If cmbVoucher.Text = "NONE" Then
                txtVoucherAmount.Text = Convert_To_Currency("0")
            ElseIf cmbVoucher.Text <> "NONE" Then
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT DISCOUNT_PERCENTAGE_AMOUNT FROM TBL_SETTINGS_DISCOUNTS WHERE DISCOUNT_CODE = @discount_code AND EDUCATION_LEVEL = @education_level", conn)
                        comm.Parameters.AddWithValue("@education_level", "COLLEGE")
                        comm.Parameters.AddWithValue("@discount_code", cmbVoucher.Text)
                        txtVoucherAmount.Text = Val(comm.ExecuteScalar)
                    End Using
                End Using
            End If
            Calculate_Generate_Fees()
        Catch ex As Exception
            RecordErros(Me.Name, ex.Message)
            MsgBox("An error occured please retry transaction!", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cmbDiscount_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDiscount.SelectionChangeCommitted
        Try
            If cmbDiscount.Text = "NONE" Then
                txtDiscountPercentage.Text = "0"
                txtDiscountAmount.Text = Convert_To_Currency("0")
            ElseIf cmbDiscount.Text <> "NONE" Then
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT DISCOUNT_PERCENTAGE_AMOUNT FROM TBL_SETTINGS_DISCOUNTS WHERE DISCOUNT_CODE = @discount_code AND EDUCATION_LEVEL = @education_level", conn)
                        comm.Parameters.AddWithValue("@education_level", "COLLEGE")
                        comm.Parameters.AddWithValue("@discount_code", cmbDiscount.Text)
                        txtDiscountPercentage.Text = Val(comm.ExecuteScalar)
                    End Using
                End Using
            End If
            Calculate_Generate_Fees()
        Catch ex As Exception
            RecordErros(Me.Name, ex.Message)
            MsgBox("An Unidentified Error Occured please retry transaction!", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        View_Fees()
        Calculate_Generate_Fees()

        Dim param_studentnumber As ReportParameter = New ReportParameter("sn", txtStudentNumber.Text)
        Dim param_studentname As ReportParameter = New ReportParameter("studentname", txtStudentName.Text)
        Dim edlevel As String = String.Empty
        Dim param_studentcourse As ReportParameter = New ReportParameter("course", txtCourseCode.Text)
        Dim param_studentyear As ReportParameter = New ReportParameter("year", txtYear.Text)
        Dim param_studentsection As ReportParameter = New ReportParameter("section", txtSection.Text)
        Dim param_studentpayment As ReportParameter = New ReportParameter("payment", cmbAssessment.Text)
        Dim param_aysem As ReportParameter = New ReportParameter("aysem", Academic_Year & " " & Academic_Sem)
        Dim assessor As String = String.Empty

        Dim assessmentdate As String = String.Empty
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_college_assessment_summary WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
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
        Dim param_tuitionfee As ReportParameter = New ReportParameter("tuitionfee", Convert_To_Currency(txtTFee.Text).ToString)
        Dim param_miscellaneousfee As ReportParameter = New ReportParameter("miscellaneousfee", Convert_To_Currency(txtMFee.Text).ToString)
        Dim param_otherfee As ReportParameter = New ReportParameter("otherfee", Convert_To_Currency(txtOFee.Text).ToString)
        Dim param_surchargefee As ReportParameter = New ReportParameter("surchargefee", Convert_To_Currency(txtSurcharge.Text).ToString)
        Dim param_totalfee As ReportParameter = New ReportParameter("totalfee", Convert_To_Currency(CDbl(txtGrossFee.Text) + CDbl(txtSurcharge.Text)).ToString)
        Dim param_DiscountCode As ReportParameter = New ReportParameter("discountcode", cmbDiscount.Text)
        Dim param_DiscountAmount As ReportParameter = New ReportParameter("discountamount", Convert_To_Currency(txtDiscountAmount.Text).ToString)
        Dim param_TotalDiscount As ReportParameter = New ReportParameter("totaldiscount", Convert_To_Currency(txtDeductions.Text).ToString)
        Dim param_TotalDue As ReportParameter = New ReportParameter("totaldue", Convert_To_Currency(txtNetFee.Text).ToString)


        Dim DS As New DS_ASSESSMENT
        Dim DR As DataRow


        With DS.Tables("3_Student_Assessment_Breakdown")
            .Rows.Clear()
            For i = 0 To DG_Summary.Rows.Count - 1
                DR = .NewRow
                DR("Fee_Code") = DG_Summary.Rows(i).Cells(0).Value.ToString
                DR("Fee_Amount") = Convert_To_Currency(DG_Summary.Rows(i).Cells(1).Value.ToString)
                DR("Due_Date") = DG_Summary.Rows(i).Cells(2).Value.ToString
                .Rows.Add(DR)
            Next
        End With

        With DS.Tables("2_Student_Subjects")
            .Rows.Clear()
            For i = 0 To DG_TFEE.Rows.Count - 1
                DR = .NewRow
                DR("Subj_Code") = DG_TFEE.Rows(i).Cells(1).Value
                DR("Subj_Desc") = DG_TFEE.Rows(i).Cells(2).Value
                DR("Subj_Unit") = DG_TFEE.Rows(i).Cells(3).Value
                DR("Sched_Day") = DG_Schedule.Rows(i).Cells(1).Value
                DR("Sched_Time_In") = DG_Schedule.Rows(i).Cells(2).Value
                DR("Sched_Time_Out") = DG_Schedule.Rows(i).Cells(3).Value
                DR("Sched_Room") = DG_Schedule.Rows(i).Cells(4).Value
                DR("Sched_Faculty") = DG_Schedule.Rows(i).Cells(5).Value
                .Rows.Add(DR)
            Next
        End With


        Dim MyReport As New ReportDataSource("DSPaymentSchedule", DS.Tables("3_Student_Assessment_Breakdown"))
        Dim MyReport2 As New ReportDataSource("DSSubjectSchedule", DS.Tables("2_Student_Subjects"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.DataSources.Add(MyReport2)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rtp_assessment_college.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_aysem, param_studentnumber,
                                                      param_studentname,
                                                      param_studentyear,
                                                      param_studentcourse,
                                                      param_studentsection,
                                                      param_studentpayment,
                                                      param_oldbalance,
                                                      param_tuitionfee,
                                                      param_miscellaneousfee,
                                                      param_otherfee,
                                                      param_surchargefee,
                                                      param_totalfee,
                                                      param_DiscountCode,
                                                      param_DiscountAmount,
                                                      param_TotalDiscount,
                                                      param_TotalDue,
                                                      param_assessmentdate,
                                                      param_assessor})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With

    End Sub

    Private Sub DG_TFEE_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DG_TFEE.RowEnter
        DGRowTFee = e.RowIndex
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frm_college_assessment_browse_students.StartPosition = FormStartPosition.CenterParent
        frm_college_assessment_browse_students.ShowDialog()
    End Sub

    Private Sub DG_Schedule_DoubleClick(sender As Object, e As EventArgs) Handles DG_Schedule.DoubleClick
        If DG_Schedule.Rows.Count = 0 Then
            MsgBox("No Subjects Detected please add subject first in tuition fee tab!", MsgBoxStyle.Critical)
        Else
            With frm_college_assessment_browse_schedule
                .DGSchedRow = DGSchedRow
                .txtSubjCode.Text = DG_TFEE.Rows(DGSchedRow).Cells(1).Value
                .txtSubjDesc.Text = DG_TFEE.Rows(DGSchedRow).Cells(2).Value
                .txtSubjUnit.Text = DG_TFEE.Rows(DGSchedRow).Cells(3).Value
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub DG_Schedule_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DG_Schedule.RowEnter
        DGSchedRow = e.RowIndex
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            If DG_Schedule.Rows.Count = 0 Then
                MsgBox("No Subjects Detected please add subject first in tuition fee tab!", MsgBoxStyle.Critical)
            Else
                With frm_college_assessment_browse_schedule
                    .DGSchedRow = DGSchedRow
                    .txtSubjCode.Text = DG_TFEE.Rows(DGSchedRow).Cells(1).Value
                    .txtSubjDesc.Text = DG_TFEE.Rows(DGSchedRow).Cells(2).Value
                    .txtSubjUnit.Text = DG_TFEE.Rows(DGSchedRow).Cells(3).Value
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog()
                End With
            End If
        Catch ex As Exception
            RecordErros(Me.Name, ex.Message)
        End Try
    End Sub

    Private Sub linkRemoveSubject_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkRemoveSubject.LinkClicked
        If Val(DG_TFEE.Rows(DGRowTFee).Cells(0).Value) = 0 Then
            If MsgBox("Are you sure you want to remove this subject?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                DG_Schedule.Rows.Remove(DG_Schedule.Rows(DGRowTFee))
                DG_TFEE.Rows.Remove(DG_TFEE.Rows(DGRowTFee))
                View_Fees()
                Calculate_Generate_Fees()
            End If
        Else
            If MsgBox("Are you sure that " & txtStudentName.Text & " want to REMOVE this subject: " & DG_TFEE.Rows(DGRowTFee).Cells(1).Value, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    If End_of_Enrollment < Date.Now Then
                        Using comm As New SqlCommand("UPDATE TBL_COLLEGE_SUBJECT_LOADS SET SUBJ_PRICE = '0', GRADE_EQUIVALENT = 'REMOVED BY' + @account WHERE ID = @subj_id", conn)
                            comm.Parameters.AddWithValue("@subj_id", DG_TFEE.Rows(DGRowTFee).Cells(0).Value)
                            comm.Parameters.AddWithValue("@account", Account_Name)
                            comm.ExecuteNonQuery()
                            DG_TFEE.Rows(DGRowTFee).Cells(6).Value = Convert_To_Currency("0")
                            DG_TFEE.Rows(DGRowTFee).Cells(7).Value = ""
                            DG_TFEE.Rows(DGRowTFee).Cells(8).Value = "UNDROP"
                            DG_TFEE.Rows(DGRowTFee).Cells(9).Value = Convert_To_Currency("0")
                            DG_TFEE.Rows(DGRowTFee).Cells(10).Value = Convert_To_Currency("0")
                            MsgBox("Subject has been successfully dropped!", MsgBoxStyle.Information)
                            View_Fees()
                            Calculate_Generate_Fees()
                        End Using

                        'INSERT INTO DROPPED TABLE
                        Using comm As New SqlCommand("INSERT INTO TBL_STUDENTS_DROPPED VALUES(@student_number,@subj_code,@accountname,GETDATE())", conn)
                            comm.Parameters.AddWithValue("@student_number", txtStudentName.Text)
                            comm.Parameters.AddWithValue("@subj_code", DG_TFEE.Rows(DGRowTFee).Cells(1).Value)
                            comm.Parameters.AddWithValue("@accountname", Account_Name)
                            comm.ExecuteNonQuery()
                        End Using
                    Else
                        Using comm As New SqlCommand("DELETE FROM TBL_COLLEGE_SUBJECT_LOADS WHERE ID = @subj_id", conn)
                            comm.Parameters.AddWithValue("@subj_id", DG_TFEE.Rows(DGRowTFee).Cells(0).Value)
                            comm.ExecuteNonQuery()
                        End Using

                        'INSERT INTO DROPPED TABLE
                        Using comm As New SqlCommand("INSERT INTO TBL_STUDENTS_DROPPED VALUES(@student_number,@subj_code,@accountname,GETDATE())", conn)
                            comm.Parameters.AddWithValue("@student_number", txtStudentName.Text)
                            comm.Parameters.AddWithValue("@subj_code", DG_TFEE.Rows(DGRowTFee).Cells(1).Value)
                            comm.Parameters.AddWithValue("@accountname", Account_Name)
                            comm.ExecuteNonQuery()
                        End Using
                    End If
                    DG_Schedule.Rows.Remove(DG_Schedule.Rows(DGRowTFee))
                    DG_TFEE.Rows.Remove(DG_TFEE.Rows(DGRowTFee))
                End Using
            End If
        End If
        View_Fees()
        Calculate_Generate_Fees()
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub cmbAssessment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssessment.SelectedIndexChanged

    End Sub

    Private Sub LinkLabel2_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        With frm_college_assessment_copier
            .course_code = txtCourseCode.Text
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub DG_Schedule_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_Schedule.CellContentClick

    End Sub
End Class