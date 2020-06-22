<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_settings_college_tuition
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clmTFee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clmEnergyFee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clmDefenceFee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbCurriculumCode = New System.Windows.Forms.ComboBox()
        Me.cmbCurriculumType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.cmbYrLevel = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clmMFee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewButtonColumn1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTFee = New System.Windows.Forms.TextBox()
        Me.txtEnergyFee = New System.Windows.Forms.TextBox()
        Me.txtMFee = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column10, Me.Column8, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.clmTFee, Me.clmEnergyFee, Me.clmDefenceFee, Me.Column12, Me.Column11})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 83)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(785, 310)
        Me.DataGridView1.TabIndex = 2
        '
        'Column10
        '
        Me.Column10.HeaderText = "SETTED SUBJECT ID"
        Me.Column10.Name = "Column10"
        Me.Column10.Visible = False
        '
        'Column8
        '
        Me.Column8.HeaderText = "CURRICULUM SUBJECT ID"
        Me.Column8.Name = "Column8"
        Me.Column8.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "CODE"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "DESCRIPTION"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 250
        '
        'Column3
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column3.HeaderText = "LEC"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Visible = False
        Me.Column3.Width = 70
        '
        'Column4
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column4.HeaderText = "LAB"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Visible = False
        Me.Column4.Width = 70
        '
        'Column5
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column5.HeaderText = "UNITS"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 70
        '
        'clmTFee
        '
        Me.clmTFee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.clmTFee.DefaultCellStyle = DataGridViewCellStyle4
        Me.clmTFee.HeaderText = "TUITION"
        Me.clmTFee.Name = "clmTFee"
        Me.clmTFee.Width = 77
        '
        'clmEnergyFee
        '
        Me.clmEnergyFee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.clmEnergyFee.DefaultCellStyle = DataGridViewCellStyle5
        Me.clmEnergyFee.HeaderText = "ENERGY FEE"
        Me.clmEnergyFee.Name = "clmEnergyFee"
        Me.clmEnergyFee.Width = 94
        '
        'clmDefenceFee
        '
        Me.clmDefenceFee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.clmDefenceFee.DefaultCellStyle = DataGridViewCellStyle6
        Me.clmDefenceFee.HeaderText = "DEFENCE FEE"
        Me.clmDefenceFee.Name = "clmDefenceFee"
        '
        'Column12
        '
        Me.Column12.HeaderText = "IS BRIDGE"
        Me.Column12.Name = "Column12"
        Me.Column12.Visible = False
        '
        'Column11
        '
        Me.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Column11.HeaderText = "REMOVE"
        Me.Column11.Name = "Column11"
        Me.Column11.Text = "REMOVE"
        Me.Column11.UseColumnTextForButtonValue = True
        Me.Column11.Width = 59
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1066, 452)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(131, 30)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "SAVE FEE SETTINGS"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "CURRICULUM CODE"
        '
        'cmbCurriculumCode
        '
        Me.cmbCurriculumCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurriculumCode.FormattingEnabled = True
        Me.cmbCurriculumCode.Location = New System.Drawing.Point(12, 32)
        Me.cmbCurriculumCode.Name = "cmbCurriculumCode"
        Me.cmbCurriculumCode.Size = New System.Drawing.Size(112, 23)
        Me.cmbCurriculumCode.TabIndex = 5
        '
        'cmbCurriculumType
        '
        Me.cmbCurriculumType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurriculumType.FormattingEnabled = True
        Me.cmbCurriculumType.Location = New System.Drawing.Point(133, 32)
        Me.cmbCurriculumType.Name = "cmbCurriculumType"
        Me.cmbCurriculumType.Size = New System.Drawing.Size(112, 23)
        Me.cmbCurriculumType.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(130, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 15)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "CURRICULUM TYPE"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 399)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(131, 30)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "ADD SUBJECTS"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cmbYrLevel
        '
        Me.cmbYrLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYrLevel.FormattingEnabled = True
        Me.cmbYrLevel.Location = New System.Drawing.Point(251, 32)
        Me.cmbYrLevel.Name = "cmbYrLevel"
        Me.cmbYrLevel.Size = New System.Drawing.Size(112, 23)
        Me.cmbYrLevel.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(248, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "YEAR LEVEL"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToResizeColumns = False
        Me.DataGridView2.AllowUserToResizeRows = False
        Me.DataGridView2.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column13, Me.Column14, Me.clmMFee, Me.DataGridViewButtonColumn1})
        Me.DataGridView2.Location = New System.Drawing.Point(803, 83)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.Size = New System.Drawing.Size(394, 310)
        Me.DataGridView2.TabIndex = 11
        '
        'Column13
        '
        Me.Column13.HeaderText = "SETTED FEE ID"
        Me.Column13.Name = "Column13"
        Me.Column13.Visible = False
        '
        'Column14
        '
        Me.Column14.HeaderText = "FEE CODE"
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        Me.Column14.Width = 200
        '
        'clmMFee
        '
        Me.clmMFee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.clmMFee.DefaultCellStyle = DataGridViewCellStyle7
        Me.clmMFee.HeaderText = "AMOUNT"
        Me.clmMFee.Name = "clmMFee"
        Me.clmMFee.Width = 81
        '
        'DataGridViewButtonColumn1
        '
        Me.DataGridViewButtonColumn1.HeaderText = "REMOVE"
        Me.DataGridViewButtonColumn1.Name = "DataGridViewButtonColumn1"
        Me.DataGridViewButtonColumn1.Text = "REMOVE"
        Me.DataGridViewButtonColumn1.UseColumnTextForButtonValue = True
        Me.DataGridViewButtonColumn1.Width = 80
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(125, 15)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "TUITION FEE SETTINGS"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(800, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(172, 15)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "MISCELLANEOUS FEE SETTINGS"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(803, 399)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(131, 30)
        Me.Button3.TabIndex = 14
        Me.Button3.Text = "ADD MISCELLANEOUS"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(367, 402)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 15)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "TOTAL TUITION FEE"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(587, 402)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 15)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "TOTAL ENERGY FEE"
        '
        'txtTFee
        '
        Me.txtTFee.Location = New System.Drawing.Point(481, 399)
        Me.txtTFee.Name = "txtTFee"
        Me.txtTFee.ReadOnly = True
        Me.txtTFee.Size = New System.Drawing.Size(100, 23)
        Me.txtTFee.TabIndex = 17
        Me.txtTFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEnergyFee
        '
        Me.txtEnergyFee.Location = New System.Drawing.Point(697, 399)
        Me.txtEnergyFee.Name = "txtEnergyFee"
        Me.txtEnergyFee.ReadOnly = True
        Me.txtEnergyFee.Size = New System.Drawing.Size(100, 23)
        Me.txtEnergyFee.TabIndex = 18
        Me.txtEnergyFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMFee
        '
        Me.txtMFee.Location = New System.Drawing.Point(1097, 399)
        Me.txtMFee.Name = "txtMFee"
        Me.txtMFee.ReadOnly = True
        Me.txtMFee.Size = New System.Drawing.Size(100, 23)
        Me.txtMFee.TabIndex = 20
        Me.txtMFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(961, 402)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(130, 15)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "TOTAL MISCELLANEOUS"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(915, 452)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(145, 30)
        Me.Button4.TabIndex = 21
        Me.Button4.Text = "RECALCULATE TOTALS"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'frm_settings_college_tuition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1213, 494)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.txtMFee)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtEnergyFee)
        Me.Controls.Add(Me.txtTFee)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.cmbYrLevel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.cmbCurriculumType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbCurriculumCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_settings_college_tuition"
        Me.Text = "SETTINGS OF TUITION FEE ON COLLEGE"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCurriculumCode As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCurriculumType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents cmbYrLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTFee As System.Windows.Forms.TextBox
    Friend WithEvents txtEnergyFee As System.Windows.Forms.TextBox
    Friend WithEvents txtMFee As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmTFee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmEnergyFee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmDefenceFee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmMFee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewButtonColumn1 As System.Windows.Forms.DataGridViewButtonColumn
End Class
