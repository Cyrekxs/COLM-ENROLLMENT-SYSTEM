<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_settings_discounts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_settings_discounts))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MyButton1 = New COLM_ENROLLMENT_SYSTEM.MyButton()
        Me.grpSEntry = New System.Windows.Forms.GroupBox()
        Me.txtSPercentage = New System.Windows.Forms.TextBox()
        Me.lblAmountPercentage = New System.Windows.Forms.Label()
        Me.txtSDiscountCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbSEducationLevel = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grpSLists = New System.Windows.Forms.GroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmbSFilter = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSNew = New System.Windows.Forms.Button()
        Me.btnSEdit = New System.Windows.Forms.Button()
        Me.btnSDelete = New System.Windows.Forms.Button()
        Me.btnSCancel = New System.Windows.Forms.Button()
        Me.btnSSave = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.grpVEntry = New System.Windows.Forms.GroupBox()
        Me.txtVAmount = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtVDiscountCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbVEducationLevel = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnVCancel = New System.Windows.Forms.Button()
        Me.grpVLists = New System.Windows.Forms.GroupBox()
        Me.cmbVFilter = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnVSave = New System.Windows.Forms.Button()
        Me.btnVDelete = New System.Windows.Forms.Button()
        Me.btnVNew = New System.Windows.Forms.Button()
        Me.btnVEdit = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.grpSEntry.SuspendLayout()
        Me.grpSLists.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.grpVEntry.SuspendLayout()
        Me.grpVLists.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.MyButton1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(567, 50)
        Me.Panel1.TabIndex = 62
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(12, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(159, 20)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "DISCOUNT SETTINGS"
        '
        'MyButton1
        '
        Me.MyButton1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.MyButton1.BackgroundImage = CType(resources.GetObject("MyButton1.BackgroundImage"), System.Drawing.Image)
        Me.MyButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.MyButton1.FlatAppearance.BorderSize = 0
        Me.MyButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MyButton1.Location = New System.Drawing.Point(523, 9)
        Me.MyButton1.Name = "MyButton1"
        Me.MyButton1.Size = New System.Drawing.Size(32, 32)
        Me.MyButton1.TabIndex = 1
        Me.MyButton1.TabStop = False
        Me.MyButton1.UseVisualStyleBackColor = True
        '
        'grpSEntry
        '
        Me.grpSEntry.Controls.Add(Me.txtSPercentage)
        Me.grpSEntry.Controls.Add(Me.lblAmountPercentage)
        Me.grpSEntry.Controls.Add(Me.txtSDiscountCode)
        Me.grpSEntry.Controls.Add(Me.Label2)
        Me.grpSEntry.Controls.Add(Me.cmbSEducationLevel)
        Me.grpSEntry.Controls.Add(Me.Label4)
        Me.grpSEntry.Location = New System.Drawing.Point(6, 6)
        Me.grpSEntry.Name = "grpSEntry"
        Me.grpSEntry.Size = New System.Drawing.Size(505, 70)
        Me.grpSEntry.TabIndex = 63
        Me.grpSEntry.TabStop = False
        Me.grpSEntry.Text = "DISCOUNTS ENTRY"
        '
        'txtSPercentage
        '
        Me.txtSPercentage.Location = New System.Drawing.Point(408, 37)
        Me.txtSPercentage.Name = "txtSPercentage"
        Me.txtSPercentage.Size = New System.Drawing.Size(91, 23)
        Me.txtSPercentage.TabIndex = 13
        Me.txtSPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblAmountPercentage
        '
        Me.lblAmountPercentage.AutoSize = True
        Me.lblAmountPercentage.Location = New System.Drawing.Point(405, 19)
        Me.lblAmountPercentage.Name = "lblAmountPercentage"
        Me.lblAmountPercentage.Size = New System.Drawing.Size(94, 15)
        Me.lblAmountPercentage.TabIndex = 12
        Me.lblAmountPercentage.Text = "PERCENTAGE (%)"
        '
        'txtSDiscountCode
        '
        Me.txtSDiscountCode.Location = New System.Drawing.Point(196, 37)
        Me.txtSDiscountCode.Name = "txtSDiscountCode"
        Me.txtSDiscountCode.Size = New System.Drawing.Size(206, 23)
        Me.txtSDiscountCode.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(193, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 15)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "DISCOUNT CODE"
        '
        'cmbSEducationLevel
        '
        Me.cmbSEducationLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSEducationLevel.FormattingEnabled = True
        Me.cmbSEducationLevel.Items.AddRange(New Object() {"COLLEGE", "SENIOR HIGH", "JUNIOR HIGH", "ELEMENTARY", "PRE ELEMENTARY"})
        Me.cmbSEducationLevel.Location = New System.Drawing.Point(9, 37)
        Me.cmbSEducationLevel.Name = "cmbSEducationLevel"
        Me.cmbSEducationLevel.Size = New System.Drawing.Size(181, 23)
        Me.cmbSEducationLevel.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "EDUCATION LEVEL"
        '
        'grpSLists
        '
        Me.grpSLists.Controls.Add(Me.DataGridView1)
        Me.grpSLists.Controls.Add(Me.cmbSFilter)
        Me.grpSLists.Controls.Add(Me.Label5)
        Me.grpSLists.Location = New System.Drawing.Point(6, 82)
        Me.grpSLists.Name = "grpSLists"
        Me.grpSLists.Size = New System.Drawing.Size(505, 310)
        Me.grpSLists.TabIndex = 64
        Me.grpSLists.TabStop = False
        Me.grpSLists.Text = "LISTS OF DISCOUNTS"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.DataGridView1.Location = New System.Drawing.Point(6, 66)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(493, 238)
        Me.DataGridView1.TabIndex = 64
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "EDUCATION LEVEL"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 130
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "DISCOUNT CODE"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn4.HeaderText = "PERCENTAGE"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'cmbSFilter
        '
        Me.cmbSFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSFilter.FormattingEnabled = True
        Me.cmbSFilter.Items.AddRange(New Object() {"ALL", "COLLEGE", "SENIOR HIGH", "JUNIOR HIGH", "ELEMENTARY"})
        Me.cmbSFilter.Location = New System.Drawing.Point(6, 37)
        Me.cmbSFilter.Name = "cmbSFilter"
        Me.cmbSFilter.Size = New System.Drawing.Size(184, 23)
        Me.cmbSFilter.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "FILTER BY"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToResizeColumns = False
        Me.DataGridView2.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView2.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column3, Me.Column1, Me.Column4, Me.Column5})
        Me.DataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.DataGridView2.Location = New System.Drawing.Point(6, 66)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(493, 238)
        Me.DataGridView2.TabIndex = 64
        '
        'Column3
        '
        Me.Column3.HeaderText = "ID"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "EDUCATION LEVEL"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 130
        '
        'Column4
        '
        Me.Column4.HeaderText = "DISCOUNT CODE"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 150
        '
        'Column5
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column5.HeaderText = "AMOUNT(PHP)"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'btnSNew
        '
        Me.btnSNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnSNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSNew.FlatAppearance.BorderSize = 0
        Me.btnSNew.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSNew.ForeColor = System.Drawing.Color.White
        Me.btnSNew.Location = New System.Drawing.Point(6, 398)
        Me.btnSNew.Name = "btnSNew"
        Me.btnSNew.Size = New System.Drawing.Size(97, 35)
        Me.btnSNew.TabIndex = 66
        Me.btnSNew.Text = "CREATE NEW"
        Me.btnSNew.UseVisualStyleBackColor = False
        '
        'btnSEdit
        '
        Me.btnSEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnSEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSEdit.FlatAppearance.BorderSize = 0
        Me.btnSEdit.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSEdit.ForeColor = System.Drawing.Color.White
        Me.btnSEdit.Location = New System.Drawing.Point(109, 398)
        Me.btnSEdit.Name = "btnSEdit"
        Me.btnSEdit.Size = New System.Drawing.Size(97, 35)
        Me.btnSEdit.TabIndex = 67
        Me.btnSEdit.Text = "EDIT"
        Me.btnSEdit.UseVisualStyleBackColor = False
        '
        'btnSDelete
        '
        Me.btnSDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnSDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSDelete.FlatAppearance.BorderSize = 0
        Me.btnSDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSDelete.ForeColor = System.Drawing.Color.White
        Me.btnSDelete.Location = New System.Drawing.Point(212, 398)
        Me.btnSDelete.Name = "btnSDelete"
        Me.btnSDelete.Size = New System.Drawing.Size(97, 35)
        Me.btnSDelete.TabIndex = 68
        Me.btnSDelete.Text = "DELETE"
        Me.btnSDelete.UseVisualStyleBackColor = False
        '
        'btnSCancel
        '
        Me.btnSCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnSCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSCancel.FlatAppearance.BorderSize = 0
        Me.btnSCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSCancel.ForeColor = System.Drawing.Color.White
        Me.btnSCancel.Location = New System.Drawing.Point(418, 398)
        Me.btnSCancel.Name = "btnSCancel"
        Me.btnSCancel.Size = New System.Drawing.Size(97, 35)
        Me.btnSCancel.TabIndex = 71
        Me.btnSCancel.Text = "CANCEL"
        Me.btnSCancel.UseVisualStyleBackColor = False
        '
        'btnSSave
        '
        Me.btnSSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnSSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSSave.FlatAppearance.BorderSize = 0
        Me.btnSSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSSave.ForeColor = System.Drawing.Color.White
        Me.btnSSave.Location = New System.Drawing.Point(315, 398)
        Me.btnSSave.Name = "btnSSave"
        Me.btnSSave.Size = New System.Drawing.Size(97, 35)
        Me.btnSSave.TabIndex = 70
        Me.btnSSave.Text = "SAVE"
        Me.btnSSave.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(16, 56)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(532, 467)
        Me.TabControl1.TabIndex = 72
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.grpSEntry)
        Me.TabPage1.Controls.Add(Me.btnSCancel)
        Me.TabPage1.Controls.Add(Me.grpSLists)
        Me.TabPage1.Controls.Add(Me.btnSSave)
        Me.TabPage1.Controls.Add(Me.btnSDelete)
        Me.TabPage1.Controls.Add(Me.btnSNew)
        Me.TabPage1.Controls.Add(Me.btnSEdit)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(524, 439)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "SCHOLARSHIPS"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.grpVEntry)
        Me.TabPage2.Controls.Add(Me.btnVCancel)
        Me.TabPage2.Controls.Add(Me.grpVLists)
        Me.TabPage2.Controls.Add(Me.btnVSave)
        Me.TabPage2.Controls.Add(Me.btnVDelete)
        Me.TabPage2.Controls.Add(Me.btnVNew)
        Me.TabPage2.Controls.Add(Me.btnVEdit)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(524, 439)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "VOUCHERS"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'grpVEntry
        '
        Me.grpVEntry.Controls.Add(Me.txtVAmount)
        Me.grpVEntry.Controls.Add(Me.Label1)
        Me.grpVEntry.Controls.Add(Me.txtVDiscountCode)
        Me.grpVEntry.Controls.Add(Me.Label6)
        Me.grpVEntry.Controls.Add(Me.cmbVEducationLevel)
        Me.grpVEntry.Controls.Add(Me.Label7)
        Me.grpVEntry.Location = New System.Drawing.Point(6, 6)
        Me.grpVEntry.Name = "grpVEntry"
        Me.grpVEntry.Size = New System.Drawing.Size(505, 70)
        Me.grpVEntry.TabIndex = 72
        Me.grpVEntry.TabStop = False
        Me.grpVEntry.Text = "DISCOUNTS ENTRY"
        '
        'txtVAmount
        '
        Me.txtVAmount.Location = New System.Drawing.Point(408, 37)
        Me.txtVAmount.Name = "txtVAmount"
        Me.txtVAmount.Size = New System.Drawing.Size(91, 23)
        Me.txtVAmount.TabIndex = 13
        Me.txtVAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(405, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 15)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "AMOUNT (PHP)"
        '
        'txtVDiscountCode
        '
        Me.txtVDiscountCode.Location = New System.Drawing.Point(196, 37)
        Me.txtVDiscountCode.Name = "txtVDiscountCode"
        Me.txtVDiscountCode.Size = New System.Drawing.Size(206, 23)
        Me.txtVDiscountCode.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(193, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "DISCOUNT CODE"
        '
        'cmbVEducationLevel
        '
        Me.cmbVEducationLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVEducationLevel.FormattingEnabled = True
        Me.cmbVEducationLevel.Items.AddRange(New Object() {"COLLEGE", "SENIOR HIGH", "JUNIOR HIGH", "ELEMENTARY"})
        Me.cmbVEducationLevel.Location = New System.Drawing.Point(9, 37)
        Me.cmbVEducationLevel.Name = "cmbVEducationLevel"
        Me.cmbVEducationLevel.Size = New System.Drawing.Size(181, 23)
        Me.cmbVEducationLevel.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 15)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "EDUCATION LEVEL"
        '
        'btnVCancel
        '
        Me.btnVCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnVCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnVCancel.FlatAppearance.BorderSize = 0
        Me.btnVCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVCancel.ForeColor = System.Drawing.Color.White
        Me.btnVCancel.Location = New System.Drawing.Point(418, 398)
        Me.btnVCancel.Name = "btnVCancel"
        Me.btnVCancel.Size = New System.Drawing.Size(97, 35)
        Me.btnVCancel.TabIndex = 78
        Me.btnVCancel.Text = "CANCEL"
        Me.btnVCancel.UseVisualStyleBackColor = False
        '
        'grpVLists
        '
        Me.grpVLists.Controls.Add(Me.DataGridView2)
        Me.grpVLists.Controls.Add(Me.cmbVFilter)
        Me.grpVLists.Controls.Add(Me.Label8)
        Me.grpVLists.Location = New System.Drawing.Point(6, 82)
        Me.grpVLists.Name = "grpVLists"
        Me.grpVLists.Size = New System.Drawing.Size(505, 310)
        Me.grpVLists.TabIndex = 73
        Me.grpVLists.TabStop = False
        Me.grpVLists.Text = "LISTS OF DISCOUNTS"
        '
        'cmbVFilter
        '
        Me.cmbVFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVFilter.FormattingEnabled = True
        Me.cmbVFilter.Items.AddRange(New Object() {"ALL", "COLLEGE", "SENIOR HIGH", "JUNIOR HIGH", "ELEMENTARY"})
        Me.cmbVFilter.Location = New System.Drawing.Point(6, 37)
        Me.cmbVFilter.Name = "cmbVFilter"
        Me.cmbVFilter.Size = New System.Drawing.Size(184, 23)
        Me.cmbVFilter.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 15)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "FILTER BY"
        '
        'btnVSave
        '
        Me.btnVSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnVSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnVSave.FlatAppearance.BorderSize = 0
        Me.btnVSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVSave.ForeColor = System.Drawing.Color.White
        Me.btnVSave.Location = New System.Drawing.Point(315, 398)
        Me.btnVSave.Name = "btnVSave"
        Me.btnVSave.Size = New System.Drawing.Size(97, 35)
        Me.btnVSave.TabIndex = 77
        Me.btnVSave.Text = "SAVE"
        Me.btnVSave.UseVisualStyleBackColor = False
        '
        'btnVDelete
        '
        Me.btnVDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnVDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnVDelete.FlatAppearance.BorderSize = 0
        Me.btnVDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVDelete.ForeColor = System.Drawing.Color.White
        Me.btnVDelete.Location = New System.Drawing.Point(212, 398)
        Me.btnVDelete.Name = "btnVDelete"
        Me.btnVDelete.Size = New System.Drawing.Size(97, 35)
        Me.btnVDelete.TabIndex = 76
        Me.btnVDelete.Text = "DELETE"
        Me.btnVDelete.UseVisualStyleBackColor = False
        '
        'btnVNew
        '
        Me.btnVNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnVNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnVNew.FlatAppearance.BorderSize = 0
        Me.btnVNew.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVNew.ForeColor = System.Drawing.Color.White
        Me.btnVNew.Location = New System.Drawing.Point(6, 398)
        Me.btnVNew.Name = "btnVNew"
        Me.btnVNew.Size = New System.Drawing.Size(97, 35)
        Me.btnVNew.TabIndex = 74
        Me.btnVNew.Text = "CREATE NEW"
        Me.btnVNew.UseVisualStyleBackColor = False
        '
        'btnVEdit
        '
        Me.btnVEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnVEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnVEdit.FlatAppearance.BorderSize = 0
        Me.btnVEdit.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVEdit.ForeColor = System.Drawing.Color.White
        Me.btnVEdit.Location = New System.Drawing.Point(109, 398)
        Me.btnVEdit.Name = "btnVEdit"
        Me.btnVEdit.Size = New System.Drawing.Size(97, 35)
        Me.btnVEdit.TabIndex = 75
        Me.btnVEdit.Text = "EDIT"
        Me.btnVEdit.UseVisualStyleBackColor = False
        '
        'frm_settings_discounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(567, 534)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frm_settings_discounts"
        Me.Text = "frm_settings_discounts"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpSEntry.ResumeLayout(False)
        Me.grpSEntry.PerformLayout()
        Me.grpSLists.ResumeLayout(False)
        Me.grpSLists.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.grpVEntry.ResumeLayout(False)
        Me.grpVEntry.PerformLayout()
        Me.grpVLists.ResumeLayout(False)
        Me.grpVLists.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents MyButton1 As COLM_ENROLLMENT_SYSTEM.MyButton
    Friend WithEvents grpSEntry As System.Windows.Forms.GroupBox
    Friend WithEvents grpSLists As System.Windows.Forms.GroupBox
    Friend WithEvents lblAmountPercentage As System.Windows.Forms.Label
    Friend WithEvents txtSDiscountCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbSEducationLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSPercentage As System.Windows.Forms.TextBox
    Friend WithEvents btnSNew As System.Windows.Forms.Button
    Friend WithEvents btnSEdit As System.Windows.Forms.Button
    Friend WithEvents btnSDelete As System.Windows.Forms.Button
    Friend WithEvents btnSCancel As System.Windows.Forms.Button
    Friend WithEvents btnSSave As System.Windows.Forms.Button
    Friend WithEvents cmbSFilter As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grpVEntry As System.Windows.Forms.GroupBox
    Friend WithEvents txtVAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtVDiscountCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbVEducationLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnVCancel As System.Windows.Forms.Button
    Friend WithEvents grpVLists As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmbVFilter As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnVSave As System.Windows.Forms.Button
    Friend WithEvents btnVDelete As System.Windows.Forms.Button
    Friend WithEvents btnVNew As System.Windows.Forms.Button
    Friend WithEvents btnVEdit As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
