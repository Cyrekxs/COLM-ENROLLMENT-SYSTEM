Public Class frm_main
    Dim CurrentVersion As String = String.Empty
    Public Sub ShowForm(frm As Form)
        frm.MdiParent = Me
        frm.Show()
        frm.Left = 0
        frm.Top = 0
    End Sub

    Public Sub ReadCurrentVersion()
        Dim VersionLocation As String = Application.StartupPath & "\Program Version.txt"
        If System.IO.File.Exists(VersionLocation) = True Then
            Using reader As New System.IO.StreamReader(VersionLocation)
                While reader.Peek <> -1
                    CurrentVersion = reader.ReadLine
                End While
            End Using
            ToolStripStatusLabel4.Text = CurrentVersion
        End If
    End Sub

    Public Sub CloseAllForms()
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frm_program_settings.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Account_Panel.Visible = False Then
            Account_Panel.Visible = True
        ElseIf Account_Panel.Visible = True Then
            Account_Panel.Visible = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Account_Panel.Visible = False
        frm_program_settings.TabControl1.SelectedTab = frm_program_settings.TabPage2
        frm_program_settings.StartPosition = FormStartPosition.CenterParent
        frm_program_settings.ShowDialog()
    End Sub

    Private Sub FACULTIESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FACULTIESToolStripMenuItem.Click
        With frm_faculty_List
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub DEPARTMENTToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DEPARTMENTToolStripMenuItem1.Click
        With frm_college_settings_department_list_entry
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub COURSEANDYEARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles COURSEANDYEARToolStripMenuItem.Click
        With frm_college_settings_course_list
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub SECTIONSToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SECTIONSToolStripMenuItem1.Click
        frm_college_settings_section_entry_list.MdiParent = Me
        frm_college_settings_section_entry_list.Show()
        frm_college_settings_section_entry_list.Left = 0
        frm_college_settings_section_entry_list.Top = 0
        'ShowForm(frm_college_settings_section_lists)
    End Sub

    Private Sub SUBJECTSETTERToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SUBJECTSETTERToolStripMenuItem1.Click
        With frm_settings_college_tuition_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
        'With frm_subject_setter
        '    .MdiParent = Me
        '    .StartPosition = FormStartPosition.CenterScreen
        '    .Show()
        'End With
    End Sub

    Private Sub INTERNALFEESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles INTERNALFEESToolStripMenuItem.Click
        With frm_settings_internal_fees
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub YEARLEVELToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YEARLEVELToolStripMenuItem.Click
        With frm_college_settings_yrlvl_lists
            .EducationLevel = "COLLEGE"
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub SENIORHIGHToolStripMenuItem_Click(sender As Object, e As EventArgs)
        CloseAllForms()
    End Sub

    Private Sub JUNIORHIGHToolStripMenuItem_Click(sender As Object, e As EventArgs)
        CloseAllForms()
    End Sub

    Private Sub COLLEGEToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles COLLEGEToolStripMenuItem5.Click
        'If Academic_Year <> "2018-2019" Then

        'Else

        'End If

        Select Case Academic_Year
            Case "2016-2017", "2017-2018"
                CloseAllForms()
                With frm_registered_college_students
                    .MdiParent = Me
                    .StartPosition = FormStartPosition.CenterScreen
                    .Show()
                End With
            Case "2018-2019", "2019-2020"
                CloseAllForms()
                With frm_college_registration_lists
                    .MdiParent = Me
                    .StartPosition = FormStartPosition.CenterScreen
                    .Show()
                End With
        End Select
    End Sub

    Private Sub SENIORHIGHToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles SENIORHIGHToolStripMenuItem2.Click
        CloseAllForms()
        With frm_registered_seniorhigh_students
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub JUNIORHIGHToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles JUNIORHIGHToolStripMenuItem2.Click
        CloseAllForms()
        With frm_registered_highschool_students
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub COLLEGEToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles COLLEGEToolStripMenuItem4.Click
        CloseAllForms()
        With frm_assessment_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .TabControl1.SelectedTab = .tbCollege
            .Show()
        End With
    End Sub

    Private Sub COLLEGEToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles COLLEGEToolStripMenuItem6.Click
        With frm_payment
            .EducationLevel = "COLLEGE"
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub SENIORHIGHToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles SENIORHIGHToolStripMenuItem3.Click
        With frm_payment
            .EducationLevel = "SENIOR HIGH"
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub JUNIORHIGHToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles JUNIORHIGHToolStripMenuItem3.Click
        With frm_payment
            .EducationLevel = "JUNIOR HIGH"
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub JUNIORHIGHToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles JUNIORHIGHToolStripMenuItem1.Click
        CloseAllForms()
        With frm_assessment_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .TabControl1.SelectedTab = .tbJunior
            .Show()
        End With
        'With frm_highschool_assessment
        '    .EducationLevel = "JUNIOR HIGH"
        '    .MdiParent = Me
        '    .StartPosition = FormStartPosition.CenterScreen
        '    .Show()
        'End With
    End Sub

    Private Sub YEARLEVELSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YEARLEVELSToolStripMenuItem.Click
        With frm_highschool_settings_yrlvl_lists_entry
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub SECTIONSToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles SECTIONSToolStripMenuItem3.Click
        With frm_highschool_settings_section_lists_entry
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub TUITIONFEEToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TUITIONFEEToolStripMenuItem1.Click
        With frm_settings_tuition_miscellaneous_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub SENIORHIGHToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SENIORHIGHToolStripMenuItem1.Click
        CloseAllForms()
        With frm_assessment_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .TabControl1.SelectedTab = .tbSenior
            .Show()
        End With
        'With frm_highschool_assessment
        '    .EducationLevel = "SENIOR HIGH"
        '    .MdiParent = Me
        '    .StartPosition = FormStartPosition.CenterScreen
        '    .Show()
        'End With
    End Sub

    Private Sub PAYMENTTYPESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PAYMENTTYPESToolStripMenuItem.Click
        With frm_settings_payments_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub SURCHARGESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SURCHARGESToolStripMenuItem.Click
        With frm_settings_surcharge
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub MAXIMUMSTUDENTSPERROOMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MAXIMUMSTUDENTSPERROOMToolStripMenuItem.Click
        With frm_settings_maximum_students
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub DISCOUNTSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DISCOUNTSToolStripMenuItem.Click
        With frm_settings_discounts
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub COURSESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles COURSESToolStripMenuItem.Click
        With frm_highschool_settings_seniorhigh_courses
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub MISCANDOTHERFEEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MISCANDOTHERFEEToolStripMenuItem.Click
        With frm_college_settings_external_fees
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub CURRICULUMToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CURRICULUMToolStripMenuItem1.Click
        With frm_college_curriculum
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub EXTERNALFEESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EXTERNALFEESToolStripMenuItem.Click
        With frm_settings_external_fees
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub OFFIALLYENROLEDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OFFIALLYENROLEDToolStripMenuItem.Click

        'With frm_officially_enrolled
        '    .MdiParent = Me
        '    .Show()
        '    .Left = 0
        '    .Top = 0
        'End With
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If MsgBox("Are you sure you want to exit this program?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub STUDENTLISTToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles STUDENTLISTToolStripMenuItem1.Click
        With student_information
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub REGISTRATIONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles REGISTRATIONToolStripMenuItem.Click

    End Sub

    Private Sub frm_main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo1
        Dim ctl As Control
        Dim ctlMDI As MdiClient

        TS_User.Text = Account_Name
        TS_Position.Text = Account_Position
        TS_Academic_Year.Text = Academic_Year
        TS_Academic_Sem.Text = Academic_Sem
        lblName.Text = Account_Name

        Try
            ToolStripStatusLabel4.Text = My.Application.Deployment.CurrentVersion.ToString()
        Catch ex As Exception

        End Try

        ' Loop through all of the form's controls looking
        ' for the control of type MdiClient.
        For Each ctl In Me.Controls
            Try
                ' Attempt to cast the control to type MdiClient.
                ctlMDI = CType(ctl, MdiClient)

                ' Set the BackColor of the MdiClient control.
                ctlMDI.BackColor = Me.BackColor

            Catch exc As InvalidCastException
                ' Catch and ignore the error if casting failed.
            End Try
        Next


        Select Case Account_Position
            Case "SCHOOL REGISTRAR"
                STUDENTLISTToolStripMenuItem1.Visible = True
                REGISTRATIONToolStripMenuItem.Visible = True
                ASSESSMENTToolStripMenuItem.Visible = True
                PAYMENTToolStripMenuItem.Visible = False
                REPORTToolStripMenuItem.Visible = True
                COLLEGEToolStripMenuItem2.Visible = True
                TRANSACTIONToolStripMenuItem.Visible = True
                INTERNALFEESToolStripMenuItem.Visible = False
                EXTERNALFEESToolStripMenuItem.Visible = False
                PAYMENTTYPESToolStripMenuItem.Visible = False
                SURCHARGESToolStripMenuItem.Visible = False
                VOUCHERSYSTEMToolStripMenuItem.Visible = False
            Case "ASSISTANT REGISTRAR"
                STUDENTLISTToolStripMenuItem1.Visible = True
                REGISTRATIONToolStripMenuItem.Visible = True
                ASSESSMENTToolStripMenuItem.Visible = True
                PAYMENTToolStripMenuItem.Visible = False
                REPORTToolStripMenuItem.Visible = True
                COLLEGEToolStripMenuItem2.Visible = True
                TRANSACTIONToolStripMenuItem.Visible = True
                INTERNALFEESToolStripMenuItem.Visible = False
                EXTERNALFEESToolStripMenuItem.Visible = False
                PAYMENTTYPESToolStripMenuItem.Visible = False
                SURCHARGESToolStripMenuItem.Visible = False
                VOUCHERSYSTEMToolStripMenuItem.Visible = False
            Case "CASHIER"
                STUDENTLISTToolStripMenuItem1.Visible = False
                REGISTRATIONToolStripMenuItem.Visible = False
                ASSESSMENTToolStripMenuItem.Visible = False
                PAYMENTToolStripMenuItem.Visible = True
                STUDENTGRADEREPORTToolStripMenuItem.Visible = False
                MASTERLISTToolStripMenuItem.Visible = True
                DATAMANAGEMENTToolStripMenuItem.Visible = False
                COLLEGEToolStripMenuItem.Visible = False
                JUNIORANDSENIORHIGHToolStripMenuItem.Visible = False
                EXTERNALFEESToolStripMenuItem.Visible = False
                PAYMENTTYPESToolStripMenuItem.Visible = False
                SURCHARGESToolStripMenuItem.Visible = False
                MAXIMUMSTUDENTSPERROOMToolStripMenuItem.Visible = False
                DISCOUNTSToolStripMenuItem.Visible = False
                VOUCHERSYSTEMToolStripMenuItem.Visible = True
            Case "ASSESSOR"
                STUDENTLISTToolStripMenuItem1.Visible = True
                REGISTRATIONToolStripMenuItem.Visible = True
                ASSESSMENTToolStripMenuItem.Visible = True
                PAYMENTToolStripMenuItem.Visible = False
                REPORTToolStripMenuItem.Visible = True
                COLLEGEToolStripMenuItem2.Visible = True
                TRANSACTIONToolStripMenuItem.Visible = True
                INTERNALFEESToolStripMenuItem.Visible = False
                EXTERNALFEESToolStripMenuItem.Visible = False
                PAYMENTTYPESToolStripMenuItem.Visible = False
                SURCHARGESToolStripMenuItem.Visible = False
                VOUCHERSYSTEMToolStripMenuItem.Visible = False
            Case "INFORMATION OFFICER"
                STUDENTLISTToolStripMenuItem1.Visible = True
                REGISTRATIONToolStripMenuItem.Visible = True
                ASSESSMENTToolStripMenuItem.Visible = True
                PAYMENTToolStripMenuItem.Visible = False
                REPORTToolStripMenuItem.Visible = True
                COLLEGEToolStripMenuItem2.Visible = True
                TRANSACTIONToolStripMenuItem.Visible = True
                INTERNALFEESToolStripMenuItem.Visible = False
                EXTERNALFEESToolStripMenuItem.Visible = False
                PAYMENTTYPESToolStripMenuItem.Visible = False
                SURCHARGESToolStripMenuItem.Visible = False
                VOUCHERSYSTEMToolStripMenuItem.Visible = False
            Case "STUDENT ASSISTANT"
                STUDENTLISTToolStripMenuItem1.Visible = True
                REGISTRATIONToolStripMenuItem.Visible = False
                ASSESSMENTToolStripMenuItem.Visible = False
                PAYMENTToolStripMenuItem.Visible = False
                REPORTToolStripMenuItem.Visible = False
                COLLEGEToolStripMenuItem2.Visible = False
                TRANSACTIONToolStripMenuItem.Visible = False
                INTERNALFEESToolStripMenuItem.Visible = False
                EXTERNALFEESToolStripMenuItem.Visible = False
                PAYMENTTYPESToolStripMenuItem.Visible = False
                SURCHARGESToolStripMenuItem.Visible = False
                DATAMANAGEMENTToolStripMenuItem.Visible = False
                VOUCHERSYSTEMToolStripMenuItem.Visible = False
        End Select

    End Sub

    Private Sub CHECKUPDATESToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Process.Start("ARQUpdater")
    End Sub

    Private Sub ALLSTUDENTINFORMATIONToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PAYMENTCOLLECTIONSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PAYMENTCOLLECTIONSToolStripMenuItem.Click
       
    End Sub

    Private Sub STUDENTGRADEREPORTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STUDENTGRADEREPORTToolStripMenuItem.Click

    End Sub

    Private Sub PAYERSLISTSToolStripMenuItem_Click(sender As Object, e As EventArgs)
        With frm_payers_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub RECIEVEPAYMENTToolStripMenuItem_Click(sender As Object, e As EventArgs)
        With frm_payment_non_student
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub COLLEGEToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles COLLEGEToolStripMenuItem1.Click
        'With frm_masterlist
        '    .MdiParent = Me
        '    .StartPosition = FormStartPosition.CenterScreen
        '    .Show()
        'End With
        With frm_college_masterlists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub JUNIORHIGHToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles JUNIORHIGHToolStripMenuItem.Click
        With frm_masterlists_junior
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub SENIORHIGHToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles SENIORHIGHToolStripMenuItem.Click
        With frm_masterlists_senior
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub PULLOUTSTUDENTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PULLOUTSTUDENTToolStripMenuItem.Click
        With frm_student_pull_out
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub JUNIORHIGHToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles JUNIORHIGHToolStripMenuItem4.Click
        'With frm_lists_assessed_students
        '    .MdiParent = Me
        '    .StartPosition = FormStartPosition.CenterScreen
        '    .Show()
        'End With
    End Sub

    Private Sub GRADUATINGSTUDENTSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GRADUATINGSTUDENTSToolStripMenuItem.Click
        With frm_graduating_students
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub STUDENTGRADEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STUDENTGRADEToolStripMenuItem.Click
        With student_grade_report
            .MdiParent = Me
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub DEANSLISTERToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DEANSLISTERToolStripMenuItem.Click
        With frm_deans_lister
            .MdiParent = Me
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub LISTOFENROLLESToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'With frm_report_officially_enrolled
        '    .MdiParent = Me
        '    .StartPosition = FormStartPosition.CenterScreen
        '    .Show()
        'End With
    End Sub

    Private Sub CHARTOFENROLLESToolStripMenuItem_Click(sender As Object, e As EventArgs)
        With frm_chart_report
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub ELToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELToolStripMenuItem.Click
        With frm_enrollment_lists
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub ELEMENTARYToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELEMENTARYToolStripMenuItem.Click
        With frm_registered_elementary_students
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub COLLECTIONSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles COLLECTIONSToolStripMenuItem.Click
        With payment_reports
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub BALANCETRACKERToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BALANCETRACKERToolStripMenuItem.Click
        Dim frm As New frm_balance_checker_revised
        With frm
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
        'With frm_balance_checker
        '    .MdiParent = Me
        '    .StartPosition = FormStartPosition.CenterScreen
        '    .Show()
        'End With
    End Sub

    Private Sub JUNIORHIGHSENIORHIGHToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub COLLEGEToolStripMenuItem3_Click(sender As Object, e As EventArgs)
        'With frm_report_officially_enrolled
        '    .MdiParent = Me
        '    .StartPosition = FormStartPosition.CenterScreen
        '    .Show()
        'End With
    End Sub

    Private Sub ELEMENTARYToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ELEMENTARYToolStripMenuItem1.Click
        CloseAllForms()
        With frm_assessment_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .TabControl1.SelectedTab = .TabPage3
            .Show()
        End With
    End Sub

    Private Sub ELEMENTARYToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ELEMENTARYToolStripMenuItem2.Click
        With frm_payment
            .EducationLevel = "ELEMENTARY"
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub PREELEMENTARYToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PREELEMENTARYToolStripMenuItem.Click
        With frm_payment
            .EducationLevel = "PRE ELEMENTARY"
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub PREELEMENTARYToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PREELEMENTARYToolStripMenuItem1.Click
        With frm_registered_pre_elementary_students
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub PREELEMENTARYToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PREELEMENTARYToolStripMenuItem2.Click
        CloseAllForms()
        With frm_assessment_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .TabControl1.SelectedTab = .TabPage4
            .Show()
        End With
    End Sub

    Private Sub OLDASSESSMENTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OLDASSESSMENTToolStripMenuItem.Click
        CloseAllForms()
        With frm_college_assessment
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub NEWASSESSMENTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NEWASSESSMENTToolStripMenuItem.Click
        CloseAllForms()
        With frm_college_assessment_entry
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub SCHEDULEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SCHEDULEToolStripMenuItem.Click
        With frm_schedule_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub ENROLLEESREPORTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ENROLLEESREPORTToolStripMenuItem.Click
        With frm_highschool_enrolled
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub ASSESSMENTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASSESSMENTToolStripMenuItem.Click

    End Sub

    Private Sub OLDMASTERLISTSToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub RFIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RFIDToolStripMenuItem.Click
        With frm_rfid_registered_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub FACULTYToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FACULTYToolStripMenuItem.Click
        With frm_faculty_lists
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub NONSTUDENTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NONSTUDENTToolStripMenuItem.Click
        With frm_nonstudent_account
            .AccountType = frm_nonstudent_account.AccountTypes.NON_STUDENT
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub MEDICALARTSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MEDICALARTSToolStripMenuItem.Click
        With frm_nonstudent_account
            .AccountType = frm_nonstudent_account.AccountTypes.MEDICAL_ARTS
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub LISTOFNONSTUDENTSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LISTOFNONSTUDENTSToolStripMenuItem.Click
        With frm_nonstudent_lists
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub ACCOUNTToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub VOUCHERToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VOUCHERToolStripMenuItem.Click
        With frm_voucher_report
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub VOUCHERSYSTEMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VOUCHERSYSTEMToolStripMenuItem.Click
        With frm_voucher_account_list
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub NONSTUDENTACCOUNTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NONSTUDENTACCOUNTToolStripMenuItem.Click
        With frm_nonstudent_account
            .StartPosition = FormStartPosition.CenterParent
            .MdiParent = Me
            .Show()
        End With
    End Sub
End Class