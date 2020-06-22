<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_college_assessment_old_assessment
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbAcademicYrSem = New System.Windows.Forms.ComboBox()
        Me.txtYesToCollegeAmount = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtTotalAmountDue = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtTotalSurcharge = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtOldBalance = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDiscountAmount = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtDiscountPercentage = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTotalMiscellaneousFee = New System.Windows.Forms.TextBox()
        Me.txtTotalTuitionFee = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtAssessmentType = New System.Windows.Forms.TextBox()
        Me.txtDiscountCode = New System.Windows.Forms.TextBox()
        Me.txtYesToCollege = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAcademic_Yr = New System.Windows.Forms.TextBox()
        Me.txtAcademic_Sem = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ASSESSMENT ON:"
        '
        'cmbAcademicYrSem
        '
        Me.cmbAcademicYrSem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAcademicYrSem.FormattingEnabled = True
        Me.cmbAcademicYrSem.Location = New System.Drawing.Point(116, 12)
        Me.cmbAcademicYrSem.Name = "cmbAcademicYrSem"
        Me.cmbAcademicYrSem.Size = New System.Drawing.Size(322, 23)
        Me.cmbAcademicYrSem.TabIndex = 1
        '
        'txtYesToCollegeAmount
        '
        Me.txtYesToCollegeAmount.BackColor = System.Drawing.Color.White
        Me.txtYesToCollegeAmount.Location = New System.Drawing.Point(339, 73)
        Me.txtYesToCollegeAmount.Name = "txtYesToCollegeAmount"
        Me.txtYesToCollegeAmount.ReadOnly = True
        Me.txtYesToCollegeAmount.Size = New System.Drawing.Size(78, 23)
        Me.txtYesToCollegeAmount.TabIndex = 50
        Me.txtYesToCollegeAmount.Text = "0.00"
        Me.txtYesToCollegeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(15, 76)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(95, 15)
        Me.Label24.TabIndex = 49
        Me.Label24.Text = "YES TO COLLEGE:"
        '
        'txtTotalAmountDue
        '
        Me.txtTotalAmountDue.BackColor = System.Drawing.Color.White
        Me.txtTotalAmountDue.Enabled = False
        Me.txtTotalAmountDue.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmountDue.Location = New System.Drawing.Point(182, 250)
        Me.txtTotalAmountDue.Name = "txtTotalAmountDue"
        Me.txtTotalAmountDue.ReadOnly = True
        Me.txtTotalAmountDue.Size = New System.Drawing.Size(235, 31)
        Me.txtTotalAmountDue.TabIndex = 47
        Me.txtTotalAmountDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(15, 259)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(116, 15)
        Me.Label22.TabIndex = 46
        Me.Label22.Text = "TOTAL AMOUNT DUE"
        '
        'txtTotalSurcharge
        '
        Me.txtTotalSurcharge.BackColor = System.Drawing.Color.White
        Me.txtTotalSurcharge.Enabled = False
        Me.txtTotalSurcharge.Location = New System.Drawing.Point(242, 218)
        Me.txtTotalSurcharge.Name = "txtTotalSurcharge"
        Me.txtTotalSurcharge.ReadOnly = True
        Me.txtTotalSurcharge.Size = New System.Drawing.Size(175, 23)
        Me.txtTotalSurcharge.TabIndex = 45
        Me.txtTotalSurcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(15, 221)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(213, 15)
        Me.Label21.TabIndex = 44
        Me.Label21.Text = "SURCHARGE (Depends on assessment)"
        '
        'txtOldBalance
        '
        Me.txtOldBalance.BackColor = System.Drawing.Color.White
        Me.txtOldBalance.Location = New System.Drawing.Point(242, 131)
        Me.txtOldBalance.Name = "txtOldBalance"
        Me.txtOldBalance.ReadOnly = True
        Me.txtOldBalance.Size = New System.Drawing.Size(175, 23)
        Me.txtOldBalance.TabIndex = 43
        Me.txtOldBalance.Text = "0.00"
        Me.txtOldBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(15, 134)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(79, 15)
        Me.Label20.TabIndex = 42
        Me.Label20.Text = "OLD BALANCE"
        '
        'txtDiscountAmount
        '
        Me.txtDiscountAmount.BackColor = System.Drawing.Color.White
        Me.txtDiscountAmount.Enabled = False
        Me.txtDiscountAmount.ForeColor = System.Drawing.Color.DarkRed
        Me.txtDiscountAmount.Location = New System.Drawing.Point(242, 102)
        Me.txtDiscountAmount.Name = "txtDiscountAmount"
        Me.txtDiscountAmount.ReadOnly = True
        Me.txtDiscountAmount.Size = New System.Drawing.Size(175, 23)
        Me.txtDiscountAmount.TabIndex = 41
        Me.txtDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(224, 105)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(12, 15)
        Me.Label19.TabIndex = 40
        Me.Label19.Text = "/"
        '
        'txtDiscountPercentage
        '
        Me.txtDiscountPercentage.BackColor = System.Drawing.Color.White
        Me.txtDiscountPercentage.Enabled = False
        Me.txtDiscountPercentage.Location = New System.Drawing.Point(182, 102)
        Me.txtDiscountPercentage.Name = "txtDiscountPercentage"
        Me.txtDiscountPercentage.ReadOnly = True
        Me.txtDiscountPercentage.Size = New System.Drawing.Size(36, 23)
        Me.txtDiscountPercentage.TabIndex = 39
        Me.txtDiscountPercentage.Text = "0"
        Me.txtDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(15, 105)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(134, 15)
        Me.Label18.TabIndex = 38
        Me.Label18.Text = "PERCENTAGE / AMOUNT"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(15, 47)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(96, 15)
        Me.Label17.TabIndex = 37
        Me.Label17.Text = "DISCOUNT CODE"
        '
        'txtTotalMiscellaneousFee
        '
        Me.txtTotalMiscellaneousFee.BackColor = System.Drawing.Color.White
        Me.txtTotalMiscellaneousFee.Enabled = False
        Me.txtTotalMiscellaneousFee.Location = New System.Drawing.Point(242, 189)
        Me.txtTotalMiscellaneousFee.Name = "txtTotalMiscellaneousFee"
        Me.txtTotalMiscellaneousFee.ReadOnly = True
        Me.txtTotalMiscellaneousFee.Size = New System.Drawing.Size(175, 23)
        Me.txtTotalMiscellaneousFee.TabIndex = 36
        Me.txtTotalMiscellaneousFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalTuitionFee
        '
        Me.txtTotalTuitionFee.BackColor = System.Drawing.Color.White
        Me.txtTotalTuitionFee.Enabled = False
        Me.txtTotalTuitionFee.Location = New System.Drawing.Point(242, 160)
        Me.txtTotalTuitionFee.Name = "txtTotalTuitionFee"
        Me.txtTotalTuitionFee.ReadOnly = True
        Me.txtTotalTuitionFee.Size = New System.Drawing.Size(175, 23)
        Me.txtTotalTuitionFee.TabIndex = 35
        Me.txtTotalTuitionFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(15, 192)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(151, 15)
        Me.Label16.TabIndex = 34
        Me.Label16.Text = "TOTAL MISCELLANEOUS FEE"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(15, 163)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(108, 15)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "TOTAL TUITION FEE"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(15, 19)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(103, 15)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "ASSESSMENT TYPE"
        '
        'txtAssessmentType
        '
        Me.txtAssessmentType.BackColor = System.Drawing.Color.White
        Me.txtAssessmentType.Location = New System.Drawing.Point(182, 16)
        Me.txtAssessmentType.Name = "txtAssessmentType"
        Me.txtAssessmentType.ReadOnly = True
        Me.txtAssessmentType.Size = New System.Drawing.Size(235, 23)
        Me.txtAssessmentType.TabIndex = 51
        '
        'txtDiscountCode
        '
        Me.txtDiscountCode.BackColor = System.Drawing.Color.White
        Me.txtDiscountCode.Location = New System.Drawing.Point(182, 44)
        Me.txtDiscountCode.Name = "txtDiscountCode"
        Me.txtDiscountCode.ReadOnly = True
        Me.txtDiscountCode.Size = New System.Drawing.Size(235, 23)
        Me.txtDiscountCode.TabIndex = 52
        '
        'txtYesToCollege
        '
        Me.txtYesToCollege.BackColor = System.Drawing.Color.White
        Me.txtYesToCollege.Location = New System.Drawing.Point(182, 73)
        Me.txtYesToCollege.Name = "txtYesToCollege"
        Me.txtYesToCollege.ReadOnly = True
        Me.txtYesToCollege.Size = New System.Drawing.Size(151, 23)
        Me.txtYesToCollege.TabIndex = 53
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtYesToCollege)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtDiscountCode)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtAssessmentType)
        Me.GroupBox1.Controls.Add(Me.txtTotalTuitionFee)
        Me.GroupBox1.Controls.Add(Me.txtYesToCollegeAmount)
        Me.GroupBox1.Controls.Add(Me.txtTotalMiscellaneousFee)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtTotalAmountDue)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.txtDiscountPercentage)
        Me.GroupBox1.Controls.Add(Me.txtTotalSurcharge)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.txtDiscountAmount)
        Me.GroupBox1.Controls.Add(Me.txtOldBalance)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 46)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(423, 315)
        Me.GroupBox1.TabIndex = 54
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ASSESSMENT INFORMATION"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(545, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 15)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "ACADEMIC YEAR"
        Me.Label2.Visible = False
        '
        'txtAcademic_Yr
        '
        Me.txtAcademic_Yr.Enabled = False
        Me.txtAcademic_Yr.Location = New System.Drawing.Point(548, 43)
        Me.txtAcademic_Yr.Name = "txtAcademic_Yr"
        Me.txtAcademic_Yr.Size = New System.Drawing.Size(90, 23)
        Me.txtAcademic_Yr.TabIndex = 56
        Me.txtAcademic_Yr.Text = "0"
        Me.txtAcademic_Yr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtAcademic_Yr.Visible = False
        '
        'txtAcademic_Sem
        '
        Me.txtAcademic_Sem.Enabled = False
        Me.txtAcademic_Sem.Location = New System.Drawing.Point(647, 43)
        Me.txtAcademic_Sem.Name = "txtAcademic_Sem"
        Me.txtAcademic_Sem.Size = New System.Drawing.Size(90, 23)
        Me.txtAcademic_Sem.TabIndex = 58
        Me.txtAcademic_Sem.Text = "0"
        Me.txtAcademic_Sem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtAcademic_Sem.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(644, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 15)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "ACADEMIC SEM"
        Me.Label3.Visible = False
        '
        'frm_college_assessment_old_assessment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 375)
        Me.Controls.Add(Me.txtAcademic_Sem)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtAcademic_Yr)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmbAcademicYrSem)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_college_assessment_old_assessment"
        Me.Text = "ASSESSMENT RECORD"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbAcademicYrSem As System.Windows.Forms.ComboBox
    Friend WithEvents txtYesToCollegeAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmountDue As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtTotalSurcharge As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtOldBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtDiscountAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtDiscountPercentage As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtTotalMiscellaneousFee As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalTuitionFee As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtAssessmentType As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtYesToCollege As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAcademic_Yr As System.Windows.Forms.TextBox
    Friend WithEvents txtAcademic_Sem As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
