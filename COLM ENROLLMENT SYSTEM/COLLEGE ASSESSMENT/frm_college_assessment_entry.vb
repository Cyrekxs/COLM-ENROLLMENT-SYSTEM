Public Class frm_college_assessment_entry
    Public AssessmentID As Integer = 0
    Public Old_AssessmentID As Integer = 0
    Public RegistrationID As Integer = 0
    Public CurriculumID As Integer = 0
    Private ListofSectionID As New List(Of Integer)
    Private SelectedSectionID As Integer
    Public AssessmentStatus As New AssessmentOptions
    Private IsSaved As Boolean = False
    Enum AssessmentOptions
        [ASSESSMENT]
        [REASSESSMENT]
    End Enum

    Private Function GetCurriculumID() As Integer
        Dim LocalCurriculumID As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum WHERE CurriculumCode = @CurriculumCode AND CurriculumType = @CurriculumType AND CurriculumCourse = @CurriculumCourse", conn)
                comm.Parameters.AddWithValue("@CurriculumCode", txtCurriculumCode.Text)
                comm.Parameters.AddWithValue("@CurriculumType", txtCurriculumType.Text)
                comm.Parameters.AddWithValue("@CurriculumCourse", txtCourseCode.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        LocalCurriculumID = reader("CurriculumID")
                    End While
                End Using
            End Using
        End Using
        Return LocalCurriculumID
    End Function

    Private Sub CalculateData()
        'TFEE
        Dim TFee As Double = 0
        Dim EnergyFee As Double = 0
        Dim Units As Integer = 0
        Dim DefenceFee As Double = 0
        Dim SubjWithDefence As String = String.Empty

        For i = 0 To DGTuition.Rows.Count - 1
            Units += DGTuition.Rows(i).Cells(3).Value
            TFee += DGTuition.Rows(i).Cells(4).Value
            EnergyFee += DGTuition.Rows(i).Cells(5).Value
            If DGTuition.Rows(i).Cells(6).Value > 0 Then
                SubjWithDefence = DGTuition.Rows(i).Cells(1).Value
                DefenceFee += DGTuition.Rows(i).Cells(6).Value
            End If
        Next

        'REMOVING THE DEFENCE FEE FIRST
        Dim inc_DFee As Integer = 0
        Do
            If DGMFee.Rows.Count = 0 Then
                Exit Do
            End If

            If DGMFee.Rows(inc_DFee).Cells(1).Value.ToString.Contains("DEFENCE") Then
                DGMFee.Rows.Remove(DGMFee.Rows(inc_DFee))
                inc_DFee = 0
            Else
                inc_DFee += 1
            End If
        Loop While inc_DFee <= DGMFee.Rows.Count - 1

        DGMFee.Rows.Add(0, "DEFENCE FEE (" & SubjWithDefence & ")", Convert_To_Currency(DefenceFee))

        'REMOVING THE ENERGY FEE FIRST
        Dim inc_EFee As Integer = 0
        Do
            If DGMFee.Rows.Count = 0 Then
                Exit Do
            End If

            If DGMFee.Rows(inc_EFee).Cells(1).Value.ToString.Contains("ENERGY FEE") Then
                DGMFee.Rows.Remove(DGMFee.Rows(inc_EFee))
                inc_EFee = 0
            Else
                inc_EFee += 1
            End If
        Loop While inc_EFee <= DGMFee.Rows.Count - 1


        'ADD A NEW ENERGY FEE
        DGMFee.Rows.Add(0, "ENERGY FEE", Convert_To_Currency(EnergyFee))

        'RECACULATE ALL MFEE INCLUDING A NEWLY ADDED ENERGY FEE
        Dim MFee As Double = 0
        For i = 0 To DGMFee.Rows.Count - 1
            MFee += DGMFee.Rows(i).Cells(2).Value
        Next

        'CACULATE DISCOUNT AMOUNT
        Dim DiscountPercentage As Double = CInt(txtDiscountPercentage.Text) / 100
        Dim NotPetitionSubjectAmount As Double = 0

        'GET ALL THE SUBJECT THAT IS NOT A PETITION SUBJECT FOR CALCULATION OF DISCOUNT AMOUNT
        For i = 0 To DGTuition.Rows.Count - 1
            If DGTuition.Rows(i).Cells(7).Value = "NO" Then
                NotPetitionSubjectAmount += CDbl(DGTuition.Rows(i).Cells(4).Value)
            End If
        Next

        Dim DiscountAmount As Double = NotPetitionSubjectAmount * DiscountPercentage
        Dim VoucherAmount As Double = 0
        Dim VoucherDiscountAmount As Double = 0

        'CALCULATE TOTAL DUE
        Dim AmountDue As Double = (TFee + MFee + CDbl(txtOldBalance.Text)) - DiscountAmount


        'GETTING THE PTF AND PMF
        Dim ListofFeeCodes As New List(Of String)
        Dim ListofPTF As New List(Of Double)
        Dim ListofPMF As New List(Of Double)
        Dim ListofDueDate As New List(Of String)
        Dim Surcharge As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_assessments WHERE Assessment_Type = @Assessment_Type AND Education_Level = @Education_Level AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY Fee_Order ASC", conn)
                comm.Parameters.AddWithValue("@Assessment_Type", cmbAssessmentType.Text)
                comm.Parameters.AddWithValue("@Education_Level", "COLLEGE")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                ListofPTF.Clear()
                ListofPMF.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        ListofFeeCodes.Add(reader("Fee_Code"))
                        ListofPTF.Add(CDbl(reader("PTF")))
                        ListofPMF.Add(CDbl(reader("PMF")))
                        ListofDueDate.Add(reader("Due_Date"))
                    End While
                End Using
            End Using

            'GETTING THE SURCHARGE
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_surcharges WHERE Education_Level = 'COLLEGE' AND Academic_Year = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        Surcharge = reader("Surcharge")
                    End While
                End Using
            End Using
        End Using


        'GENERATE ASSESSMENT BREAKDOWN
        Dim ListofCalculatedBreakdown As New List(Of Double)
        Dim Calculated_TFee As Double = TFee - DiscountAmount
        Dim Calculated_MFee As Double = MFee
        Dim Calculated_Surcharge As Integer = 0
        Dim OldBalance As Double = CDbl(txtOldBalance.Text) / ListofFeeCodes.Count

        ListofCalculatedBreakdown.Clear()


        'GET THE TOTAL AMOUNT OF BRIDING SUBJECTS TO ADD IT INTO CALCULATION
        Dim BridgingProgram As Double = 0
        Dim BridgingProgram_Calculated As Double = 0
        For x = 0 To DGTuition.Rows.Count - 1
            If DGTuition.Rows(x).Cells(1).Value.ToString.Contains("BP-") = True Then
                BridgingProgram += DGTuition.Rows(x).Cells(4).Value
            End If
        Next
        BridgingProgram_Calculated = BridgingProgram / ListofFeeCodes.Count


        'MEANING KAPAG MERONG NKASSELECT NA VOUCHER
        If cmbVoucher.SelectedIndex > 0 Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT VoucherAmount FROM tbl_settings_college_vouchers WHERE VoucherCode = @VoucherCode AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                    comm.Parameters.AddWithValue("@VoucherCode", cmbVoucher.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            VoucherAmount = reader("VoucherAmount")
                        End While
                    End Using
                End Using
            End Using


            For i = 0 To ListofFeeCodes.Count - 1
                If i = 0 Then
                    ListofCalculatedBreakdown.Add((VoucherAmount * (ListofPTF(i) / 100)) +
                                                     OldBalance + BridgingProgram_Calculated)
                ElseIf i > 0 Then
                    ListofCalculatedBreakdown.Add((VoucherAmount * (ListofPTF(i) / 100)) +
                                                     OldBalance +
                                                     Surcharge + BridgingProgram_Calculated)
                    Calculated_Surcharge += Surcharge
                End If
            Next

        Else 'PAG WALANG NAKASELECT NA VOUCHER
            VoucherAmount = 0
            For i = 0 To ListofFeeCodes.Count - 1
                If i = 0 Then
                    ListofCalculatedBreakdown.Add((Calculated_TFee * (ListofPTF(i) / 100) +
                                                   (Calculated_MFee * (ListofPMF(i) / 100))) +
                                                     OldBalance)
                ElseIf i > 0 Then
                    ListofCalculatedBreakdown.Add((Calculated_TFee * (ListofPTF(i) / 100) +
                                                   (Calculated_MFee * (ListofPMF(i) / 100))) +
                                                     OldBalance +
                                                     Surcharge)
                    Calculated_Surcharge += Surcharge
                End If
            Next
        End If




        DGBreakDown.Rows.Clear()
        For i = 0 To ListofFeeCodes.Count - 1
            DGBreakDown.Rows.Add(ListofFeeCodes(i),
                                 Convert_To_Currency(ListofCalculatedBreakdown(i)),
                                 ListofDueDate(i))
        Next

        'DISPLAY DATA
        If VoucherAmount > 0 Then
            VoucherDiscountAmount = (AmountDue) - VoucherAmount
            AmountDue = (AmountDue - VoucherDiscountAmount) + Calculated_Surcharge
            AmountDue += BridgingProgram
            AmountDue += CDbl(txtOldBalance.Text)
        ElseIf VoucherAmount = 0 Then
            AmountDue += Calculated_Surcharge
        End If


        txtTotalSubjects.Text = DGTuition.Rows.Count
        txtTotalUnits.Text = Units
        txtTuition.Text = Convert_To_Currency(TFee)
        txtMFee.Text = Convert_To_Currency(MFee)
        txtTotalTuitionFee.Text = Convert_To_Currency(TFee)
        txtTotalMiscellaneousFee.Text = Convert_To_Currency(MFee)
        txtDiscountAmount.Text = Convert_To_Currency(DiscountAmount)
        txtVoucherAmount.Text = Convert_To_Currency(VoucherAmount)
        txtTotalSurcharge.Text = Convert_To_Currency(Calculated_Surcharge)
        txtTotalAmountDue.Text = Convert_To_Currency(AmountDue)
    End Sub

    Private Sub GetAssessmentInformation()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            'FOR INFORMATION
            Using comm As New SqlCommand("SELECT * FROM FN_College_AssessedStudents() WHERE AssessmentID = @AssessmentID", conn)
                comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        Old_AssessmentID = AssessmentID
                        txtStudentName.Text = reader("StudentName")
                        If IsDBNull(reader("Gender")) = False Then
                            txtGender.Text = LSet(reader("Gender"), 1)
                        End If

                        If IsDBNull(reader("BirthDate")) = False Then
                            txtAge.Text = GetAge(CDate(reader("BirthDate")))
                        End If

                        txtCurriculumCode.Text = reader("CurriculumCode")
                        txtCurriculumType.Text = reader("CurriculumType")
                        txtCourseCode.Text = reader("Course_Code")
                    End While
                End Using
            End Using

            'FOR ASSESSMENT SUMMARY
            Using comm As New SqlCommand("SELECT * FROM tbl_college_assessment_summary WHERE ID = @AssessmentID", conn)
                comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtStudentNo.Text = reader("Student_Number")
                        cmbYearLevel.Text = reader("Yrlvl")
                        cmbSection.Text = reader("Sect_Code")
                        cmbAssessmentType.Text = reader("Assessment_Type")
                        cmbDiscountCode.Text = reader("Discount_Code")
                        txtDiscountPercentage.Text = reader("Discount_Percentage")
                        txtDiscountAmount.Text = Convert_To_Currency(reader("Discount_Amount"))
                        If IsDBNull(reader("Voucher_Code")) = True Then
                            cmbVoucher.Text = "NONE"
                            txtVoucherAmount.Text = Convert_To_Currency("0")
                        Else
                            cmbVoucher.Text = reader("Voucher_Code")
                            txtVoucherAmount.Text = Convert_To_Currency(reader("Voucher_Amount"))
                        End If

                        txtOldBalance.Text = Convert_To_Currency(reader("Old_Balance"))
                        txtTuition.Text = Convert_To_Currency(reader("TFee"))
                        txtTotalTuitionFee.Text = Convert_To_Currency(reader("TFee"))
                        txtTotalMiscellaneousFee.Text = Convert_To_Currency(reader("MFee"))
                        txtMFee.Text = Convert_To_Currency(reader("MFee"))
                        txtTotalSurcharge.Text = Convert_To_Currency(reader("Surcharge"))
                        txtTotalAmountDue.Text = Convert_To_Currency(reader("Total"))
                    End While
                End Using
            End Using


            'FOR ASSESSMENT BREAKDOWN
            Using comm As New SqlCommand("SELECT * FROM tbl_college_assessment_breakdown WHERE AssessmentID = @AssessmentID", conn)
                comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DGBreakDown.Rows.Add(reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")), reader("Due_Date"))
                    End While
                End Using
            End Using


            Using comm As New SqlCommand("SELECT * FROM FN_College_RegisteredLists() WHERE StudentNo = @sn", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        RegistrationID = reader("RegistrationID")
                    End While
                End Using
            End Using

            'DGTUTION
            Using comm As New SqlCommand("SELECT * FROM dbo.FN_College_Subject_Loads(@RegistrationID,@SN,@ay,@sem)", conn)
                comm.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read

                        DGTuition.Rows.Add(reader("RegisteredSubjID"), reader("SubjCode"), reader("SubjDesc"), reader("SubjUnit"),
                                          Convert_To_Currency(reader("SubjPrice")),
                                          Convert_To_Currency(reader("SubjEnergyFee")),
                                          Convert_To_Currency(reader("SubjDefenceFee")), reader("IsPetition"))


                        DGSched.Rows.Add(reader("RegisteredSubjID"),
                                         reader("SubjCode"),
                                         reader("SubjDesc"),
                                         reader("ScheduleID"),
                                         reader("Sched_Day"),
                                         reader("Sched_Time_In"),
                                         reader("Sched_Time_Out"),
                                         reader("Sched_Room"),
                                         reader("Sched_Faculty"))
                    End While
                End Using
            End Using

            Using comm As New SqlCommand("SELECT * FROM tbl_college_fee_loads WHERE AssessmentID = @AssessmentID AND Fee_Type ='MFEE'", conn)
                comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DGMFee.Rows.Add(reader("ID"), reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub GetMFee()
        'GETTING THE MFEE
        Dim MFee As Double = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_fees WHERE CurriculumID = @CurriculumID AND Fee_Status = 'EXTERNAL' AND Fee_Type = 'MISCELLANEOUS FEE' AND YrLvl = @Yrlvl AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@CurriculumID", GetCurriculumID)
                comm.Parameters.AddWithValue("@Yrlvl", cmbYearLevel.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DGMFee.Rows.Clear()
                    While reader.Read
                        DGMFee.Rows.Add(reader("ID"), reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")))
                        MFee += CDbl(reader("Fee_Amount"))
                    End While
                End Using
            End Using
        End Using
        CalculateData()
    End Sub

    Private Sub GetDiscountInformation()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_discounts WHERE Discount_Code = @Discount_Code AND Education_Level = @Education_Level AND Discount_Category = 'SCHOLARSHIP' AND Discount_Classification = 'PERCENTAGE' AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@Discount_Code", cmbDiscountCode.Text)
                comm.Parameters.AddWithValue("@Education_Level", "COLLEGE")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtDiscountPercentage.Text = reader("Discount_Percentage_Amount")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadAssessmentTypesAndDiscounts()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT Assessment_Type FROM tbl_settings_assessments WHERE Education_Level = @Education_Level AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@Education_Level", "COLLEGE")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                cmbAssessmentType.Items.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        cmbAssessmentType.Items.Add(reader("Assessment_Type"))
                    End While
                End Using
            End Using

            Using comm As New SqlCommand("SELECT DISTINCT Discount_Code FROM tbl_settings_discounts WHERE Education_Level = @Education_Level AND Discount_Category = 'SCHOLARSHIP' AND Discount_Classification = 'PERCENTAGE' AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@Education_Level", "COLLEGE")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                cmbDiscountCode.Items.Clear()
                cmbDiscountCode.Items.Add("NONE")
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        cmbDiscountCode.Items.Add(reader("Discount_Code"))
                    End While
                End Using
            End Using

            '================================================YES TO COLLEGE=====================================
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_vouchers WHERE Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbVoucher.Items.Clear()
                    cmbVoucher.Items.Add("NONE")
                    While reader.Read
                        cmbVoucher.Items.Add(reader("VoucherCode"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub GetSections()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_sections WHERE Course_Code = @Course_Code AND Yrlvl = @Yrlvl AND Academic_Year = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@Course_Code", txtCourseCode.Text)
                comm.Parameters.AddWithValue("@Yrlvl", cmbYearLevel.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbSection.Items.Clear()
                    ListofSectionID.Clear()
                    While reader.Read
                        ListofSectionID.Add(reader("ID"))
                        cmbSection.Items.Add(reader("Section_Code"))
                    End While
                    ListofSectionID.Add(0)
                    cmbSection.Items.Add("IRREGULAR")
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        With frm_college_assessment_browser_students
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If cmbYearLevel.Text = String.Empty Then
            MsgBox("Please select year level first!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbSection.Text = String.Empty Then
            MsgBox("Please select section first!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If RegistrationID = 0 Then
            MsgBox("Please select student first!", MsgBoxStyle.Critical)
            Exit Sub
        Else
            With frm_college_assessment_browser_subject
                .RegistrationID = RegistrationID
                .CourseCode = txtCourseCode.Text
                .IsBridgeSubject = "NO"
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                CalculateData()
            End With
        End If
    End Sub

    Private Sub DGTuition_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGTuition.CellContentClick
        If e.ColumnIndex = 7 Then
            Select Case DGTuition.Rows(e.RowIndex).Cells(7).Value
                Case "NO"
                    DGTuition.Rows(e.RowIndex).Cells(7).Value = "YES"
                Case "YES"
                    DGTuition.Rows(e.RowIndex).Cells(7).Value = "NO"
            End Select
        ElseIf e.ColumnIndex = 8 Then
            DGTuition.Rows.Remove(DGTuition.Rows(e.RowIndex))
            DGSched.Rows.Remove(DGSched.Rows(e.RowIndex))
            CalculateData()
        End If
    End Sub

    Private Sub DGSched_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGSched.CellContentClick
        If e.ColumnIndex = 9 Then
            With frm_college_assessment_browser_schedule
                .DGSchedRow = e.RowIndex
                .SubjCode = DGSched.Rows(e.RowIndex).Cells(1).Value
                .txtSubjCode.Text = DGSched.Rows(e.RowIndex).Cells(1).Value
                .txtSubjDesc.Text = DGSched.Rows(e.RowIndex).Cells(2).Value
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        End If
        Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DGMFee_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGMFee.CellContentClick
        If e.ColumnIndex = 3 Then
            DGMFee.Rows.Remove(DGMFee.Rows(e.RowIndex))
            CalculateData()
        End If
    End Sub

    Private Sub cmbYearLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYearLevel.SelectedIndexChanged
        GetSections()
        'AUTO DETECT IF THE CMB SECTION HAS NO CONTENT IT WILL AUTOMATICALLY SELECT THE IRREGULAR MODE
        If AssessmentStatus = AssessmentOptions.REASSESSMENT Then
            If cmbSection.Items.Count = 1 Then
                cmbSection.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub cmbYearLevel_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbYearLevel.SelectionChangeCommitted
        GetMFee()
    End Sub

    Private Sub frm_college_assessment_entry_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frm_college_assessment_entry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If IsSaved = False Then
            If MsgBox("Warning the transaction has not yet been saved" & vbNewLine & "Are you sure you want to close this form?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frm_college_assessment_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAssessmentTypesAndDiscounts()
        cmbDiscountCode.SelectedIndex = 0
        If AssessmentStatus = AssessmentOptions.REASSESSMENT Then
            GetAssessmentInformation()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If cmbYearLevel.Text = String.Empty Then
            MsgBox("Please select year level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbSection.Text = String.Empty Then
            MsgBox("Please select section code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbAssessmentType.Text = String.Empty Then
            MsgBox("Please select assessment type!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbDiscountCode.Text = String.Empty Then
            MsgBox("Please select discount code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim CanSave As Boolean = True
        For i = 0 To DGSched.Rows.Count - 1
            If DGSched.Rows(i).Cells(4).Value = Nothing Then
                CanSave = False
                Exit For
            End If
        Next

        If CanSave = False Then
            MsgBox("One or more of the subjects is not setted in the schedule tab area!", MsgBoxStyle.Critical)
            Exit Sub
        End If


        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    'INSERT DATA INTO tbl_college_assessment_summary
                    Using comm As New SqlCommand("INSERT INTO tbl_college_assessment_summary VALUES (@EducationLevel,@StudentNo,@CourseCode,@YrLvl,@SectCode,@TFee,@MFee,@OFee,@VoucherCode,@VoucherAmount,@DiscountCode,@DiscountPercentage,@DiscountAmount,NULL,NULL,@Surcharge,@OldBalance,@Total,@AssessmentType,@ay,@sem,@Assessor,GETDATE(),'ACTIVE')", conn, t)
                        comm.Parameters.AddWithValue("@EducationLevel", "COLLEGE")
                        comm.Parameters.AddWithValue("@StudentNo", txtStudentNo.Text)
                        comm.Parameters.AddWithValue("@CourseCode", txtCourseCode.Text)
                        comm.Parameters.AddWithValue("@Yrlvl", cmbYearLevel.Text)
                        comm.Parameters.AddWithValue("@SectCode", cmbSection.Text)
                        comm.Parameters.AddWithValue("@TFee", txtTotalTuitionFee.Text)
                        comm.Parameters.AddWithValue("@MFee", txtTotalMiscellaneousFee.Text)
                        comm.Parameters.AddWithValue("@OFee", "0.00")
                        comm.Parameters.AddWithValue("@VoucherCode", cmbVoucher.Text)
                        comm.Parameters.AddWithValue("@VoucherAmount", txtVoucherAmount.Text)
                        comm.Parameters.AddWithValue("@DiscountCode", cmbDiscountCode.Text)
                        comm.Parameters.AddWithValue("@DiscountPercentage", txtDiscountPercentage.Text)
                        comm.Parameters.AddWithValue("@DiscountAmount", txtDiscountAmount.Text)
                        comm.Parameters.AddWithValue("@Surcharge", txtTotalSurcharge.Text)
                        comm.Parameters.AddWithValue("@OldBalance", txtOldBalance.Text)
                        comm.Parameters.AddWithValue("@Total", txtTotalAmountDue.Text)
                        comm.Parameters.AddWithValue("@AssessmentType", cmbAssessmentType.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.Parameters.AddWithValue("@Assessor", Account_Name)
                        comm.ExecuteNonQuery()
                    End Using

                    'GET THE AssessmentID 
                    Using comm As New SqlCommand("SELECT MAX (ID) AS AssessmentID FROM tbl_college_assessment_summary WITH (NOLOCK) WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn, t)
                        comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            While reader.Read
                                AssessmentID = reader("AssessmentID")
                            End While
                        End Using
                    End Using

                    Using comm As New SqlCommand("DELETE FROM tbl_college_assessment_breakdown WHERE Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn, t)
                        comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        comm.ExecuteNonQuery()
                    End Using

                    'ASSESSMENT BRAEKDOWN
                    For i = 0 To DGBreakDown.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO tbl_college_assessment_breakdown VALUES (@AssessmentID,@sn,@FeeCode,@FeeAmount,@DueDate,@ay,@sem,GETDATE())", conn, t)
                            comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                            comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                            comm.Parameters.AddWithValue("@FeeCode", DGBreakDown.Rows(i).Cells(0).Value)
                            comm.Parameters.AddWithValue("@FeeAmount", DGBreakDown.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@DueDate", DGBreakDown.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next

                    'FEE LOADS
                    For i = 0 To DGMFee.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO tbl_college_fee_loads VALUES (@AssessmentID,@sn,@FeeType,NULL,@FeeCode,@Quantity,@FeeAmount,@ay,@sem,GETDATE())", conn, t)
                            comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                            comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                            comm.Parameters.AddWithValue("@FeeType", "MFEE")
                            comm.Parameters.AddWithValue("@FeeCode", DGMFee.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@Quantity", 1)
                            comm.Parameters.AddWithValue("@FeeAmount", DGMFee.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next

                    'GET OLD GRADE RECORD
                    Dim ListofSubjGrades As New List(Of String)
                    Dim ListofSubjGradesRemarks As New List(Of String)
                    Dim ListofSubjGradesEncoder As New List(Of String)
                    For i = 0 To DGTuition.Rows.Count - 1
                        Using comm As New SqlCommand("SELECT * FROM tbl_college_subject_loads WHERE Subj_Code = @Subj_Code AND AssessmentID = @AssessmentID", conn, t)
                            comm.Parameters.AddWithValue("@AssessmentID", Old_AssessmentID)
                            comm.Parameters.AddWithValue("@Subj_Code", DGTuition.Rows(i).Cells(1).Value)
                            Using reader As SqlDataReader = comm.ExecuteReader
                                If reader.HasRows = True Then
                                    While reader.Read
                                        ListofSubjGrades.Add(reader("Grade_Equivalent"))
                                        ListofSubjGradesRemarks.Add(reader("Grade_Remarks"))
                                        ListofSubjGradesEncoder.Add(reader("Encoder"))
                                    End While
                                Else
                                    ListofSubjGrades.Add("N.A")
                                    ListofSubjGradesRemarks.Add("N.A")
                                    ListofSubjGradesEncoder.Add("N.A")
                                End If

                            End Using
                        End Using
                    Next

                    'SUBJECT LOADS
                    For i = 0 To DGTuition.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO tbl_college_subject_loads VALUES (@AssessmentID,@sn,@subjcode,@subjdesc,@subjunit,NULL,NULL,@IsPetition,@subjprice,@schedid,@sched_day,@sched_time_in,@sched_time_out,@sched_room,@sched_faculty,'0',@grade_equivalent,@grade_remarks,@grade_encoder,@ay,@sem,GETDATE())", conn, t)
                            comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                            comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                            comm.Parameters.AddWithValue("@subjcode", DGTuition.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@subjdesc", DGTuition.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@subjunit", DGTuition.Rows(i).Cells(3).Value)
                            comm.Parameters.AddWithValue("@IsPetition", DGTuition.Rows(i).Cells(7).Value)
                            comm.Parameters.AddWithValue("@subjprice", DGTuition.Rows(i).Cells(4).Value)
                            comm.Parameters.AddWithValue("@schedid", DGSched.Rows(i).Cells(3).Value)
                            comm.Parameters.AddWithValue("@sched_day", DGSched.Rows(i).Cells(4).Value.ToString)
                            comm.Parameters.AddWithValue("@sched_time_in", DGSched.Rows(i).Cells(5).Value.ToString)
                            comm.Parameters.AddWithValue("@sched_time_out", DGSched.Rows(i).Cells(6).Value.ToString)
                            comm.Parameters.AddWithValue("@sched_room", DGSched.Rows(i).Cells(7).Value.ToString)
                            comm.Parameters.AddWithValue("@sched_faculty", DGSched.Rows(i).Cells(8).Value.ToString)
                            comm.Parameters.AddWithValue("@grade_equivalent", ListofSubjGrades(i))
                            comm.Parameters.AddWithValue("@grade_remarks", ListofSubjGradesRemarks(i))
                            comm.Parameters.AddWithValue("@grade_encoder", ListofSubjGradesEncoder(i))
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm.ExecuteNonQuery()
                        End Using
                    Next

                    t.Commit()
                    MsgBox("Assessment has been successfully saved", MsgBoxStyle.Information)
                    AssessmentStatus = AssessmentOptions.REASSESSMENT
                    btnBrowse.Visible = False
                    IsSaved = True
                Catch ex As Exception
                    MsgBox("An error occured while processing the data please try again" & vbNewLine & "Exception: " & ex.Message, MsgBoxStyle.Critical)
                    t.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Sub cmbSection_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbSection.SelectionChangeCommitted
        SelectedSectionID = ListofSectionID(cmbSection.SelectedIndex)
        If SelectedSectionID <> 0 Then
            If MsgBox("Changing this data will affect the tuition fee and schedule tab do you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM FN_College_SubjectSchedulesIndividual(@RegistrationID,@ScheduleID,@ay,@sem)", conn)
                        comm.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                        comm.Parameters.AddWithValue("@ScheduleID", SelectedSectionID)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            DGTuition.Rows.Clear()
                            DGSched.Rows.Clear()
                            Try
                                While reader.Read
                                    DGTuition.Rows.Add(reader("RegisteredSubjID"),
                                                       reader("SubjCode"),
                                                       reader("SubjDesc"),
                                                       reader("SubjUnit"),
                                                       Convert_To_Currency(reader("SubjPrice")),
                                                       Convert_To_Currency(reader("SubjEnergyFee")),
                                                       Convert_To_Currency(reader("SubjDefenceFee")), "NO")

                                    DGSched.Rows.Add(reader("RegisteredSubjID"),
                                                     reader("SubjCode"),
                                                     reader("SubjDesc"),
                                                     reader("ScheduleID"),
                                                     reader("Sched_Day"),
                                                     reader("Sched_Time_In"),
                                                     reader("Sched_Time_Out"),
                                                     reader("Sched_Room"),
                                                     reader("Faculty_Name"))
                                End While
                            Catch ex As Exception
                                MsgBox("Please update the registration of this student due to curriculum changes!", MsgBoxStyle.Critical)
                                Exit Sub
                            End Try
                        End Using
                    End Using
                End Using
            End If
        End If
    End Sub

    Private Sub cmbAssessmentType_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbAssessmentType.SelectionChangeCommitted
        CalculateData()
    End Sub

    Private Sub cmbDiscountCode_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDiscountCode.SelectionChangeCommitted
        If cmbDiscountCode.Text = "NONE" Then
            txtDiscountPercentage.Text = 0
        Else
            GetDiscountInformation()
        End If
        CalculateData()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim param_studentnumber As ReportParameter = New ReportParameter("sn", txtStudentNo.Text)
        Dim param_studentname As ReportParameter = New ReportParameter("studentname", txtStudentName.Text)
        Dim edlevel As String = String.Empty
        Dim param_studentcourse As ReportParameter = New ReportParameter("course", txtCourseCode.Text)
        Dim param_studentyear As ReportParameter = New ReportParameter("year", cmbYearLevel.Text)
        Dim param_studentsection As ReportParameter = New ReportParameter("section", cmbSection.Text)
        Dim param_studentpayment As ReportParameter = New ReportParameter("payment", cmbAssessmentType.Text)
        Dim param_aysem As ReportParameter = New ReportParameter("aysem", Academic_Year & " " & Academic_Sem)
        Dim param_showsignature As ReportParameter = New ReportParameter("ShowSign", "True")

        If chkShowSignature.Checked = True Then
            param_showsignature = New ReportParameter("ShowSign", "False")
        ElseIf chkShowSignature.Checked = False Then
            param_showsignature = New ReportParameter("ShowSign", "True")
        End If

        Dim assessor As String = String.Empty
        Dim assessmentdate As String = String.Empty
        'GET THE ACTUAL ASSESSMENT DATE AND ASSESSOR
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_AssessedStudents() WHERE AssessmentID = @AssessmentID", conn)
                comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        assessmentdate = Format(CDate(reader("Assessed_Date")), "MM-dd-yyyy")
                        assessor = reader("Assess_By")
                    End While
                End Using
            End Using
        End Using

        Dim AcadUnits As Integer = 0
        Dim NonAcadUnits As Integer = 0
        Dim TotalUnits As Integer = 0
        'GET ACAD AND NON ACAD UNITS
        For i = 0 To DGTuition.Rows.Count - 1
            If DGTuition.Rows(i).Cells(1).Value.ToString.Contains("PE1") Then
                NonAcadUnits += Val(DGTuition.Rows(i).Cells(3).Value)
            ElseIf DGTuition.Rows(i).Cells(1).Value.ToString.Contains("NSTP1") Then
                NonAcadUnits += Val(DGTuition.Rows(i).Cells(3).Value)
            Else
                AcadUnits += Val(DGTuition.Rows(i).Cells(3).Value)
            End If
        Next
        TotalUnits = AcadUnits + NonAcadUnits

        Dim param_assessmentdate As ReportParameter = New ReportParameter("assessmentdate", assessmentdate)
        Dim param_assessor As ReportParameter = New ReportParameter("assessor", assessor)
        Dim param_oldbalance As ReportParameter = New ReportParameter("oldbalance", Convert_To_Currency(txtOldBalance.Text).ToString)
        Dim param_tuitionfee As ReportParameter = New ReportParameter("tuitionfee", Convert_To_Currency(txtTotalTuitionFee.Text).ToString)
        Dim param_miscellaneousfee As ReportParameter = New ReportParameter("miscellaneousfee", Convert_To_Currency(txtTotalMiscellaneousFee.Text).ToString)
        Dim param_otherfee As ReportParameter = New ReportParameter("otherfee", Convert_To_Currency("0.00").ToString)
        Dim param_surchargefee As ReportParameter = New ReportParameter("surchargefee", Convert_To_Currency(txtTotalSurcharge.Text).ToString)
        Dim param_totalfee As ReportParameter = New ReportParameter("totalfee", Convert_To_Currency(CDbl(txtTotalTuitionFee.Text) + CDbl(txtTotalMiscellaneousFee.Text) + CDbl(txtTotalSurcharge.Text)).ToString)
        Dim param_DiscountCode As ReportParameter = New ReportParameter("discountcode", cmbDiscountCode.Text)
        Dim param_DiscountAmount As ReportParameter = New ReportParameter("discountamount", Convert_To_Currency(txtDiscountAmount.Text).ToString)
        Dim param_VoucherAmount As ReportParameter = New ReportParameter("VoucherAmount", Convert_To_Currency(CDbl(txtTotalTuitionFee.Text) + CDbl(txtTotalMiscellaneousFee.Text) - CDbl(txtVoucherAmount.Text)).ToString)
        Dim param_TotalDiscount As ReportParameter
        If cmbVoucher.SelectedIndex > 0 Then
            param_VoucherAmount = New ReportParameter("VoucherAmount", Convert_To_Currency(CDbl(txtTotalTuitionFee.Text) + CDbl(txtTotalMiscellaneousFee.Text) - CDbl(txtVoucherAmount.Text)).ToString)
            param_TotalDiscount = New ReportParameter("totaldiscount", Convert_To_Currency(CDbl(txtDiscountAmount.Text) + (CDbl(txtTotalTuitionFee.Text) + CDbl(txtTotalMiscellaneousFee.Text) - CDbl(txtVoucherAmount.Text))).ToString)
        Else
            param_VoucherAmount = New ReportParameter("VoucherAmount", "0.00")
            param_TotalDiscount = New ReportParameter("totaldiscount", Convert_To_Currency(CDbl(txtDiscountAmount.Text)).ToString)
        End If


        Dim param_TotalDue As ReportParameter = New ReportParameter("totaldue", Convert_To_Currency(txtTotalAmountDue.Text).ToString)
        Dim param_NonAcadUnits As ReportParameter = New ReportParameter("NonAcadUnits", NonAcadUnits)
        Dim param_AcadUnits As ReportParameter = New ReportParameter("AcadUnits", AcadUnits)
        Dim param_TotalUnits As ReportParameter = New ReportParameter("TotalUnits", TotalUnits)

        Dim DS As New DS_ASSESSMENT
        Dim DR As DataRow

        With DS.Tables("3_Student_Assessment_Breakdown")
            .Rows.Clear()
            For i = 0 To DGBreakDown.Rows.Count - 1
                DR = .NewRow
                DR("Fee_Code") = DGBreakDown.Rows(i).Cells(0).Value.ToString
                DR("Fee_Amount") = Convert_To_Currency(DGBreakDown.Rows(i).Cells(1).Value.ToString)
                If (DGBreakDown.Rows(i).Cells(2).Value = Nothing) Then
                    DR("Due_Date") = ""
                Else
                    DR("Due_Date") = DGBreakDown.Rows(i).Cells(2).Value.ToString
                End If
                .Rows.Add(DR)
            Next
        End With

        With DS.Tables("2_Student_Subjects")
            .Rows.Clear()
            For i = 0 To DGTuition.Rows.Count - 1
                DR = .NewRow
                DR("Subj_Code") = DGTuition.Rows(i).Cells(1).Value
                DR("Subj_Desc") = DGTuition.Rows(i).Cells(2).Value
                If DGTuition.Rows(i).Cells(1).Value.ToString.Contains("PE1") Then
                    DR("Subj_Unit") = "(" & DGTuition.Rows(i).Cells(3).Value & ")"
                ElseIf DGTuition.Rows(i).Cells(1).Value.ToString.Contains("NSTP1") Then
                    DR("Subj_Unit") = "(" & DGTuition.Rows(i).Cells(3).Value & ")"
                Else
                    DR("Subj_Unit") = DGTuition.Rows(i).Cells(3).Value
                End If

                DR("Sched_Day") = DGSched.Rows(i).Cells(4).Value
                DR("Sched_Time_In") = DGSched.Rows(i).Cells(5).Value
                DR("Sched_Time_Out") = DGSched.Rows(i).Cells(6).Value
                DR("Sched_Room") = DGSched.Rows(i).Cells(7).Value
                DR("Sched_Faculty") = DGSched.Rows(i).Cells(8).Value
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
                                                      param_oldbalance,
                                                      param_totalfee,
                                                      param_DiscountCode,
                                                      param_DiscountAmount,
                                                      param_VoucherAmount,
                                                      param_TotalDiscount,
                                                      param_TotalDue,
                                                      param_assessmentdate,
                                                      param_assessor,
                                                      param_AcadUnits,
                                                      param_NonAcadUnits,
                                                      param_TotalUnits, param_showsignature})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        With frm_college_assessment_browser_mfee
            .CurriculumID = GetCurriculumID()
            .txtYearLevel.Text = cmbYearLevel.Text
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            CalculateData()
        End With
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CalculateData()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If RegistrationID = 0 Then
            MsgBox("Please select student first!", MsgBoxStyle.Critical)
            Exit Sub
        Else
            With frm_college_assessment_browser_subject
                .RegistrationID = RegistrationID
                .IsBridgeSubject = "YES"
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                CalculateData()
            End With
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MsgBox("This is not yet working!", MsgBoxStyle.Information)
    End Sub

    Private Sub cmbVoucher_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbVoucher.SelectionChangeCommitted
        CalculateData()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If txtStudentNo.Text = String.Empty Then
            MsgBox("Please browse student first before viewing old assessment!", MsgBoxStyle.Critical)
        Else
            Dim frm As New frm_college_assessment_old_assessment
            With frm
                .sn = txtStudentNo.Text
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim Units As Integer = 0
            Dim TotalAmount As Integer = 0
            For i = 0 To DGTuition.Rows.Count - 1
                Units += CInt(DGTuition.Rows(i).Cells(3).Value)
                TotalAmount += CDbl(DGTuition.Rows(i).Cells(4).Value)
            Next

            txtTotalUnits.Text = Units
            txtTuition.Text = Convert_To_Currency(TotalAmount)
            txtTotalSubjects.Text = DGTuition.Rows.Count
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim x As Double = InputBox("Please enter amount per unit")

        CalculateData2(x)
    End Sub

    Private Sub CalculateData2(AmountPerUnit As Double)
        Dim totalUnits As Integer = 0
        Dim nonAcadUnits As Integer = 0
        For i = 0 To DGTuition.Rows.Count - 1
            If (DGTuition.Rows(i).Cells(clmSubjCode.Index).Value.ToString.Contains("NSTP")) Then
                nonAcadUnits = DGTuition.Rows(i).Cells(clmUnits.Index).Value
            Else
                totalUnits += DGTuition.Rows(i).Cells(clmUnits.Index).Value
            End If
        Next

        Dim FeeInfo As New Fee(AmountPerUnit, totalUnits, False)

        If (nonAcadUnits > 0) Then
            FeeInfo = New Fee(AmountPerUnit, totalUnits, True)
        End If


        Dim ListFeeBreakdown As New List(Of FeeBreakDown)
        Dim FeeBreakDown As New FeeBreakDown
        FeeBreakDown.FeeAmount = FeeInfo.Total / 5
        FeeBreakDown.FeeCode = "UPON ENROLLMENT"
        FeeBreakDown.FeeDue = "-"
        ListFeeBreakdown.Add(FeeBreakDown)

        FeeBreakDown = New FeeBreakDown
        FeeBreakDown.FeeAmount = FeeInfo.Total / 5
        FeeBreakDown.FeeCode = "INSTALLMENT #1"
        FeeBreakDown.FeeDue = "SEPT 5,6,7"
        ListFeeBreakdown.Add(FeeBreakDown)

        FeeBreakDown = New FeeBreakDown
        FeeBreakDown.FeeAmount = FeeInfo.Total / 5
        FeeBreakDown.FeeCode = "INSTALLMENT #2"
        FeeBreakDown.FeeDue = "OCT 1,2,3"
        ListFeeBreakdown.Add(FeeBreakDown)

        FeeBreakDown = New FeeBreakDown
        FeeBreakDown.FeeAmount = FeeInfo.Total / 5
        FeeBreakDown.FeeCode = "INSTALLMENT #3"
        FeeBreakDown.FeeDue = "NOV 7,9"
        ListFeeBreakdown.Add(FeeBreakDown)

        FeeBreakDown = New FeeBreakDown
        FeeBreakDown.FeeAmount = FeeInfo.Total / 5
        FeeBreakDown.FeeCode = "INSTALLMENT #4"
        FeeBreakDown.FeeDue = "NOV 23,25,26"
        ListFeeBreakdown.Add(FeeBreakDown)

        txtTotalTuitionFee.Text = Convert_To_Currency(FeeInfo.TutionFee)
        txtTotalMiscellaneousFee.Text = Convert_To_Currency(FeeInfo.MiscellaneousFee)
        txtTotalSurcharge.Text = Convert_To_Currency(0)
        txtTotalAmountDue.Text = Convert_To_Currency(FeeInfo.Total)

        DGBreakDown.Rows.Clear()
        For Each breakdown As FeeBreakDown In ListFeeBreakdown
            DGBreakDown.Rows.Add(breakdown.FeeCode, Convert_To_Currency(breakdown.FeeAmount), breakdown.FeeDue)
        Next
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged

    End Sub
End Class

Public Class Fee
    Private _TuitionFee As Double
    Public Property TutionFee() As Double
        Get
            If (HasNSTP = True) Then
                Return _TuitionFee + 585
            Else
                Return _TuitionFee
            End If
        End Get

        Private Set(ByVal value As Double)
            _TuitionFee = value
        End Set
    End Property

    Private _MiscellaneousFee As Double
    Public ReadOnly Property MiscellaneousFee() As Double
        Get
            Return 4500
        End Get
    End Property

    Private _Total As Double
    Public ReadOnly Property Total() As Double
        Get
            Return (Me.TutionFee + Me.MiscellaneousFee)
        End Get
    End Property

    Private _TotalAcadUnits As Integer
    Public Property TotalAcadUnits() As Integer
        Get
            Return _TotalAcadUnits
        End Get
        Set(ByVal value As Integer)
            _TotalAcadUnits = value
        End Set
    End Property

    Private _HasNSTP As Boolean
    Public Property HasNSTP() As Boolean
        Get
            Return _HasNSTP
        End Get
        Set(ByVal value As Boolean)
            _HasNSTP = value
        End Set
    End Property

    Sub New(AmountPerUnit As Double, AcadUnits As Integer, HasNTSP As Boolean)
        _TotalAcadUnits = AcadUnits
        TutionFee = _TotalAcadUnits * AmountPerUnit
        HasNSTP = HasNTSP
    End Sub
End Class

Public Class FeeBreakDown
    Private _FeeCode As String
    Public Property FeeCode() As String
        Get
            Return _FeeCode
        End Get
        Set(ByVal value As String)
            _FeeCode = value
        End Set
    End Property

    Private _FeeAmount As Double
    Public Property FeeAmount() As Double
        Get
            Return _FeeAmount
        End Get
        Set(ByVal value As Double)
            _FeeAmount = value
        End Set
    End Property

    Private _FeeDue As String
    Public Property FeeDue() As String
        Get
            Return _FeeDue
        End Get
        Set(ByVal value As String)
            _FeeDue = value
        End Set
    End Property

End Class
