﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class payment_reports_nonstudent
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTotalResults = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbRecieptStatus = New System.Windows.Forms.ComboBox()
        Me.cmbFeeType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRecieptNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtEducationLevel = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(412, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 15)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "EDUCATION LEVEL"
        '
        'lblTotalResults
        '
        Me.lblTotalResults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTotalResults.AutoSize = True
        Me.lblTotalResults.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalResults.Location = New System.Drawing.Point(108, 416)
        Me.lblTotalResults.Name = "lblTotalResults"
        Me.lblTotalResults.Size = New System.Drawing.Size(14, 15)
        Me.lblTotalResults.TabIndex = 31
        Me.lblTotalResults.Text = "0"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 416)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 15)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "TOTAL RESULTS:"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(768, 408)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(122, 30)
        Me.Button2.TabIndex = 29
        Me.Button2.Text = "PRINT REPORT"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column9, Me.Column2, Me.Column5, Me.Column8, Me.Column6, Me.Column4, Me.Column7})
        Me.DataGridView1.Location = New System.Drawing.Point(17, 59)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(873, 343)
        Me.DataGridView1.TabIndex = 28
        '
        'Column1
        '
        Me.Column1.HeaderText = "TX DATE"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 80
        '
        'Column9
        '
        Me.Column9.HeaderText = "TX TYPE"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "RECIEPT NO"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "STATUS"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "FEE CODE"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 200
        '
        'Column6
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column6.HeaderText = "AMOUNT"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "STUDENT NAME"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 200
        '
        'Column7
        '
        Me.Column7.HeaderText = "RECIEVED BY"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 150
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(768, 30)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(122, 23)
        Me.Button1.TabIndex = 27
        Me.Button1.Text = "VIEW RESULTS"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker2.CustomFormat = "MM-dd-yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(656, 30)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(106, 23)
        Me.DateTimePicker2.TabIndex = 26
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.CustomFormat = "MM-dd-yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(544, 30)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(106, 23)
        Me.DateTimePicker1.TabIndex = 25
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(653, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 15)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "TO DATE"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(541, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 15)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "FROM DATE"
        '
        'cmbRecieptStatus
        '
        Me.cmbRecieptStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbRecieptStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRecieptStatus.FormattingEnabled = True
        Me.cmbRecieptStatus.Items.AddRange(New Object() {"ACTIVE", "CANCELLED"})
        Me.cmbRecieptStatus.Location = New System.Drawing.Point(288, 30)
        Me.cmbRecieptStatus.Name = "cmbRecieptStatus"
        Me.cmbRecieptStatus.Size = New System.Drawing.Size(121, 23)
        Me.cmbRecieptStatus.TabIndex = 22
        '
        'cmbFeeType
        '
        Me.cmbFeeType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFeeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFeeType.FormattingEnabled = True
        Me.cmbFeeType.Items.AddRange(New Object() {"CASH PAYMENT", "CREDIT MEMO"})
        Me.cmbFeeType.Location = New System.Drawing.Point(161, 30)
        Me.cmbFeeType.Name = "cmbFeeType"
        Me.cmbFeeType.Size = New System.Drawing.Size(121, 23)
        Me.cmbFeeType.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(285, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 15)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "RECIEPT STATUS"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(158, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 15)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "TRANSACTION TYPE"
        '
        'txtRecieptNo
        '
        Me.txtRecieptNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRecieptNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtRecieptNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtRecieptNo.Location = New System.Drawing.Point(17, 30)
        Me.txtRecieptNo.Name = "txtRecieptNo"
        Me.txtRecieptNo.Size = New System.Drawing.Size(138, 23)
        Me.txtRecieptNo.TabIndex = 18
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 15)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "SEARCH ACCOUNT / FEE"
        '
        'txtEducationLevel
        '
        Me.txtEducationLevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEducationLevel.Location = New System.Drawing.Point(415, 30)
        Me.txtEducationLevel.Name = "txtEducationLevel"
        Me.txtEducationLevel.Size = New System.Drawing.Size(123, 23)
        Me.txtEducationLevel.TabIndex = 33
        Me.txtEducationLevel.Text = "NON STUDENT"
        Me.txtEducationLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'payment_reports_nonstudent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtEducationLevel)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblTotalResults)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbRecieptStatus)
        Me.Controls.Add(Me.cmbFeeType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRecieptNo)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "payment_reports_nonstudent"
        Me.Size = New System.Drawing.Size(904, 450)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTotalResults As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbRecieptStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFeeType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRecieptNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEducationLevel As System.Windows.Forms.TextBox
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn

End Class