<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_elementary_assessment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_elementary_assessment))
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.MyButton1 = New COLM_ENROLLMENT_SYSTEM.MyButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtSection = New System.Windows.Forms.TextBox()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.txtCourseCode = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtStudentName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtStudentNumber = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtHonorPerct = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cmbHonorDiscount = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtVoucherAmount = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbVoucher = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmbAssessment = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.DG_Summary = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtDiscountAmount = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDiscountPercentage = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbDiscount = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtDeductions = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtOldBalance = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtSurcharge = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtGrossFee = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNetFee = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtMFee = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTFee = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.txtDirectDiscount = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DG_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lblTitle)
        Me.Panel1.Controls.Add(Me.MyButton1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(985, 50)
        Me.Panel1.TabIndex = 89
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(12, 14)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(336, 20)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "ELEMENTARY ASSESSMENT / RE-ASSESSMENT"
        '
        'MyButton1
        '
        Me.MyButton1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.MyButton1.BackgroundImage = CType(resources.GetObject("MyButton1.BackgroundImage"), System.Drawing.Image)
        Me.MyButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MyButton1.FlatAppearance.BorderSize = 0
        Me.MyButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MyButton1.Location = New System.Drawing.Point(941, 9)
        Me.MyButton1.Name = "MyButton1"
        Me.MyButton1.Size = New System.Drawing.Size(32, 32)
        Me.MyButton1.TabIndex = 1
        Me.MyButton1.TabStop = False
        Me.MyButton1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.txtSection)
        Me.Panel2.Controls.Add(Me.txtYear)
        Me.Panel2.Controls.Add(Me.txtCourseCode)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtStudentName)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.txtStudentNumber)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 50)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(219, 515)
        Me.Panel2.TabIndex = 90
        '
        'txtSection
        '
        Me.txtSection.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSection.Location = New System.Drawing.Point(17, 425)
        Me.txtSection.Name = "txtSection"
        Me.txtSection.ReadOnly = True
        Me.txtSection.Size = New System.Drawing.Size(192, 23)
        Me.txtSection.TabIndex = 33
        Me.txtSection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtYear
        '
        Me.txtYear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYear.Location = New System.Drawing.Point(17, 383)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ReadOnly = True
        Me.txtYear.Size = New System.Drawing.Size(192, 23)
        Me.txtYear.TabIndex = 32
        Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCourseCode
        '
        Me.txtCourseCode.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCourseCode.Location = New System.Drawing.Point(17, 339)
        Me.txtCourseCode.Name = "txtCourseCode"
        Me.txtCourseCode.ReadOnly = True
        Me.txtCourseCode.Size = New System.Drawing.Size(192, 23)
        Me.txtCourseCode.TabIndex = 31
        Me.txtCourseCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(15, 170)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(194, 34)
        Me.Button3.TabIndex = 30
        Me.Button3.Text = "BROWSE STUDENT"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.COLM_ENROLLMENT_SYSTEM.My.Resources.Resources.USER
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(15, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(194, 161)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 27
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 207)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 15)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "STUDENT NUMBER"
        '
        'txtStudentName
        '
        Me.txtStudentName.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStudentName.Location = New System.Drawing.Point(15, 269)
        Me.txtStudentName.Multiline = True
        Me.txtStudentName.Name = "txtStudentName"
        Me.txtStudentName.ReadOnly = True
        Me.txtStudentName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtStudentName.Size = New System.Drawing.Size(194, 47)
        Me.txtStudentName.TabIndex = 10
        Me.txtStudentName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(14, 251)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "STUDENT NAME"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 321)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 15)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "COURSE"
        '
        'txtStudentNumber
        '
        Me.txtStudentNumber.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStudentNumber.Location = New System.Drawing.Point(17, 225)
        Me.txtStudentNumber.Name = "txtStudentNumber"
        Me.txtStudentNumber.ReadOnly = True
        Me.txtStudentNumber.Size = New System.Drawing.Size(192, 23)
        Me.txtStudentNumber.TabIndex = 7
        Me.txtStudentNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 409)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 15)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "SECTION"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 365)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 15)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "YEAR"
        '
        'txtHonorPerct
        '
        Me.txtHonorPerct.Location = New System.Drawing.Point(514, 194)
        Me.txtHonorPerct.Name = "txtHonorPerct"
        Me.txtHonorPerct.ReadOnly = True
        Me.txtHonorPerct.Size = New System.Drawing.Size(56, 23)
        Me.txtHonorPerct.TabIndex = 128
        Me.txtHonorPerct.Text = "0"
        Me.txtHonorPerct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(511, 176)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(31, 15)
        Me.Label23.TabIndex = 127
        Me.Label23.Text = "( % )"
        '
        'cmbHonorDiscount
        '
        Me.cmbHonorDiscount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHonorDiscount.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbHonorDiscount.FormattingEnabled = True
        Me.cmbHonorDiscount.Items.AddRange(New Object() {"NONE"})
        Me.cmbHonorDiscount.Location = New System.Drawing.Point(232, 196)
        Me.cmbHonorDiscount.Name = "cmbHonorDiscount"
        Me.cmbHonorDiscount.Size = New System.Drawing.Size(276, 21)
        Me.cmbHonorDiscount.TabIndex = 125
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(230, 177)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 15)
        Me.Label2.TabIndex = 126
        Me.Label2.Text = "HONOR DISCOUNT"
        '
        'txtVoucherAmount
        '
        Me.txtVoucherAmount.Enabled = False
        Me.txtVoucherAmount.Location = New System.Drawing.Point(514, 148)
        Me.txtVoucherAmount.Name = "txtVoucherAmount"
        Me.txtVoucherAmount.ReadOnly = True
        Me.txtVoucherAmount.Size = New System.Drawing.Size(56, 23)
        Me.txtVoucherAmount.TabIndex = 124
        Me.txtVoucherAmount.Text = "0.00"
        Me.txtVoucherAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(511, 130)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(59, 15)
        Me.Label22.TabIndex = 123
        Me.Label22.Text = "AMOUNT"
        '
        'cmbVoucher
        '
        Me.cmbVoucher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucher.Enabled = False
        Me.cmbVoucher.FormattingEnabled = True
        Me.cmbVoucher.Location = New System.Drawing.Point(231, 148)
        Me.cmbVoucher.Name = "cmbVoucher"
        Me.cmbVoucher.Size = New System.Drawing.Size(277, 23)
        Me.cmbVoucher.TabIndex = 96
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(227, 130)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(61, 15)
        Me.Label21.TabIndex = 122
        Me.Label21.Text = "VOUCHER"
        '
        'cmbAssessment
        '
        Me.cmbAssessment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAssessment.FormattingEnabled = True
        Me.cmbAssessment.Location = New System.Drawing.Point(231, 104)
        Me.cmbAssessment.Name = "cmbAssessment"
        Me.cmbAssessment.Size = New System.Drawing.Size(339, 23)
        Me.cmbAssessment.TabIndex = 95
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label20.Location = New System.Drawing.Point(225, 59)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(190, 21)
        Me.Label20.TabIndex = 121
        Me.Label20.Text = "ASSESSMENT SETTINGS"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Black
        Me.Panel3.Location = New System.Drawing.Point(584, 50)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(3, 446)
        Me.Panel3.TabIndex = 120
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label19.Location = New System.Drawing.Point(599, 60)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(219, 21)
        Me.Label19.TabIndex = 119
        Me.Label19.Text = "ASSESSMENT BREAKDOWN"
        '
        'DG_Summary
        '
        Me.DG_Summary.AllowUserToAddRows = False
        Me.DG_Summary.AllowUserToDeleteRows = False
        Me.DG_Summary.AllowUserToResizeColumns = False
        Me.DG_Summary.AllowUserToResizeRows = False
        Me.DG_Summary.BackgroundColor = System.Drawing.Color.White
        Me.DG_Summary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_Summary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.Column14})
        Me.DG_Summary.Location = New System.Drawing.Point(603, 86)
        Me.DG_Summary.Name = "DG_Summary"
        Me.DG_Summary.ReadOnly = True
        Me.DG_Summary.RowHeadersVisible = False
        Me.DG_Summary.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Summary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DG_Summary.Size = New System.Drawing.Size(365, 410)
        Me.DG_Summary.TabIndex = 118
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle16
        Me.DataGridViewTextBoxColumn5.HeaderText = "FEE CODE"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Width = 150
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold)
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle17
        Me.DataGridViewTextBoxColumn6.HeaderText = "AMOUNT"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column14
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column14.DefaultCellStyle = DataGridViewCellStyle18
        Me.Column14.HeaderText = "DUE DATE"
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        Me.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'txtDiscountAmount
        '
        Me.txtDiscountAmount.Location = New System.Drawing.Point(514, 241)
        Me.txtDiscountAmount.Name = "txtDiscountAmount"
        Me.txtDiscountAmount.ReadOnly = True
        Me.txtDiscountAmount.Size = New System.Drawing.Size(57, 23)
        Me.txtDiscountAmount.TabIndex = 117
        Me.txtDiscountAmount.Text = "0.00"
        Me.txtDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(511, 223)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(59, 15)
        Me.Label18.TabIndex = 116
        Me.Label18.Text = "AMOUNT"
        '
        'txtDiscountPercentage
        '
        Me.txtDiscountPercentage.Location = New System.Drawing.Point(458, 241)
        Me.txtDiscountPercentage.Name = "txtDiscountPercentage"
        Me.txtDiscountPercentage.ReadOnly = True
        Me.txtDiscountPercentage.Size = New System.Drawing.Size(50, 23)
        Me.txtDiscountPercentage.TabIndex = 115
        Me.txtDiscountPercentage.Text = "0"
        Me.txtDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(455, 223)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(31, 15)
        Me.Label17.TabIndex = 114
        Me.Label17.Text = "( % )"
        '
        'cmbDiscount
        '
        Me.cmbDiscount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDiscount.FormattingEnabled = True
        Me.cmbDiscount.Location = New System.Drawing.Point(231, 241)
        Me.cmbDiscount.Name = "cmbDiscount"
        Me.cmbDiscount.Size = New System.Drawing.Size(224, 23)
        Me.cmbDiscount.TabIndex = 97
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(229, 223)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(109, 15)
        Me.Label16.TabIndex = 113
        Me.Label16.Text = "SELECT DISCOUNT"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(230, 398)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(112, 15)
        Me.Label15.TabIndex = 111
        Me.Label15.Text = "TOTAL DISCOUNTS"
        '
        'txtDeductions
        '
        Me.txtDeductions.Location = New System.Drawing.Point(234, 416)
        Me.txtDeductions.Name = "txtDeductions"
        Me.txtDeductions.ReadOnly = True
        Me.txtDeductions.Size = New System.Drawing.Size(109, 23)
        Me.txtDeductions.TabIndex = 112
        Me.txtDeductions.Text = "0.00"
        Me.txtDeductions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(227, 86)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(112, 15)
        Me.Label14.TabIndex = 110
        Me.Label14.Text = "ASSESSMENT TYPE"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(229, 311)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(85, 15)
        Me.Label13.TabIndex = 108
        Me.Label13.Text = "OLD BALANCE"
        '
        'txtOldBalance
        '
        Me.txtOldBalance.Location = New System.Drawing.Point(232, 329)
        Me.txtOldBalance.Name = "txtOldBalance"
        Me.txtOldBalance.ReadOnly = True
        Me.txtOldBalance.Size = New System.Drawing.Size(109, 23)
        Me.txtOldBalance.TabIndex = 109
        Me.txtOldBalance.Text = "0.00"
        Me.txtOldBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(457, 354)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 15)
        Me.Label12.TabIndex = 106
        Me.Label12.Text = "SURCHARGE"
        '
        'txtSurcharge
        '
        Me.txtSurcharge.Location = New System.Drawing.Point(460, 373)
        Me.txtSurcharge.Name = "txtSurcharge"
        Me.txtSurcharge.ReadOnly = True
        Me.txtSurcharge.Size = New System.Drawing.Size(109, 23)
        Me.txtSurcharge.TabIndex = 107
        Me.txtSurcharge.Text = "0.00"
        Me.txtSurcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(345, 398)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(97, 15)
        Me.Label11.TabIndex = 104
        Me.Label11.Text = "TOTAL AMOUNT"
        '
        'txtGrossFee
        '
        Me.txtGrossFee.Location = New System.Drawing.Point(349, 416)
        Me.txtGrossFee.Name = "txtGrossFee"
        Me.txtGrossFee.ReadOnly = True
        Me.txtGrossFee.Size = New System.Drawing.Size(109, 23)
        Me.txtGrossFee.TabIndex = 105
        Me.txtGrossFee.Text = "0.00"
        Me.txtGrossFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(228, 436)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(201, 25)
        Me.Label10.TabIndex = 102
        Me.Label10.Text = "TOTAL AMOUNT DUE"
        '
        'txtNetFee
        '
        Me.txtNetFee.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetFee.Location = New System.Drawing.Point(233, 463)
        Me.txtNetFee.Name = "txtNetFee"
        Me.txtNetFee.ReadOnly = True
        Me.txtNetFee.Size = New System.Drawing.Size(339, 33)
        Me.txtNetFee.TabIndex = 103
        Me.txtNetFee.Text = "0.00"
        Me.txtNetFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(343, 354)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 15)
        Me.Label8.TabIndex = 100
        Me.Label8.Text = "MISCELLANEOUS"
        '
        'txtMFee
        '
        Me.txtMFee.Location = New System.Drawing.Point(347, 373)
        Me.txtMFee.Name = "txtMFee"
        Me.txtMFee.ReadOnly = True
        Me.txtMFee.Size = New System.Drawing.Size(109, 23)
        Me.txtMFee.TabIndex = 101
        Me.txtMFee.Text = "0.00"
        Me.txtMFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(228, 354)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 15)
        Me.Label7.TabIndex = 98
        Me.Label7.Text = "TUITION FEE"
        '
        'txtTFee
        '
        Me.txtTFee.Location = New System.Drawing.Point(232, 373)
        Me.txtTFee.Name = "txtTFee"
        Me.txtTFee.ReadOnly = True
        Me.txtTFee.Size = New System.Drawing.Size(109, 23)
        Me.txtTFee.TabIndex = 99
        Me.txtTFee.Text = "0.00"
        Me.txtTFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Button2)
        Me.Panel4.Controls.Add(Me.Button1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(219, 512)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(766, 53)
        Me.Panel4.TabIndex = 129
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(463, 10)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(140, 34)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "PRINT ASSESSMENT"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(609, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(140, 34)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "DONE"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.SeaGreen
        Me.Button6.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Location = New System.Drawing.Point(349, 285)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(110, 25)
        Me.Button6.TabIndex = 132
        Me.Button6.Text = "CHANGE"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'txtDirectDiscount
        '
        Me.txtDirectDiscount.Location = New System.Drawing.Point(231, 285)
        Me.txtDirectDiscount.Name = "txtDirectDiscount"
        Me.txtDirectDiscount.ReadOnly = True
        Me.txtDirectDiscount.Size = New System.Drawing.Size(111, 23)
        Me.txtDirectDiscount.TabIndex = 131
        Me.txtDirectDiscount.Text = "0.00"
        Me.txtDirectDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(228, 267)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(110, 15)
        Me.Label24.TabIndex = 130
        Me.Label24.Text = "DIRECT DISCOUNT"
        '
        'frm_elementary_assessment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(985, 565)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.txtDirectDiscount)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.txtHonorPerct)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.cmbHonorDiscount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtVoucherAmount)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.cmbVoucher)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.cmbAssessment)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.DG_Summary)
        Me.Controls.Add(Me.txtDiscountAmount)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtDiscountPercentage)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.cmbDiscount)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtDeductions)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtOldBalance)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtSurcharge)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtGrossFee)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtNetFee)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtMFee)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTFee)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_elementary_assessment"
        Me.Text = "frm_elementary_assessment"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DG_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents MyButton1 As COLM_ENROLLMENT_SYSTEM.MyButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtSection As System.Windows.Forms.TextBox
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents txtCourseCode As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtStudentName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtStudentNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtHonorPerct As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents cmbHonorDiscount As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtVoucherAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbVoucher As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbAssessment As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents DG_Summary As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtDiscountAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtDiscountPercentage As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbDiscount As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtDeductions As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtOldBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSurcharge As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtGrossFee As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtNetFee As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMFee As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTFee As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button6 As Button
    Friend WithEvents txtDirectDiscount As TextBox
    Friend WithEvents Label24 As Label
End Class
