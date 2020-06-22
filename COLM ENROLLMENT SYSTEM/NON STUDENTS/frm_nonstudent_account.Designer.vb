<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_nonstudent_account
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtAccountID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtBalance = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.btnPrintSoa = New System.Windows.Forms.Button()
        Me.btnCreditMemo = New System.Windows.Forms.Button()
        Me.btnCashPayment = New System.Windows.Forms.Button()
        Me.btnRemoveFee = New System.Windows.Forms.Button()
        Me.btnChargeFee = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clmAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clmPayment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clmBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtAccountID)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.txtAccountName)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(976, 70)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "NON STUDENT INFORMATION"
        '
        'txtAccountID
        '
        Me.txtAccountID.Location = New System.Drawing.Point(7, 37)
        Me.txtAccountID.Name = "txtAccountID"
        Me.txtAccountID.ReadOnly = True
        Me.txtAccountID.Size = New System.Drawing.Size(71, 23)
        Me.txtAccountID.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "ACCOUNT ID"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(832, 23)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(125, 41)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "BROWSE"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtAccountName
        '
        Me.txtAccountName.Location = New System.Drawing.Point(84, 37)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.ReadOnly = True
        Me.txtAccountName.Size = New System.Drawing.Size(742, 23)
        Me.txtAccountName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(81, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "NAME"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column8, Me.Column2, Me.Column6, Me.clmAmount, Me.clmPayment, Me.clmBalance, Me.Column9})
        Me.DataGridView1.Location = New System.Drawing.Point(6, 6)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(947, 323)
        Me.DataGridView1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.ItemSize = New System.Drawing.Size(64, 30)
        Me.TabControl1.Location = New System.Drawing.Point(12, 88)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(976, 410)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Controls.Add(Me.txtBalance)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.CheckBox1)
        Me.TabPage1.Controls.Add(Me.btnPrintSoa)
        Me.TabPage1.Controls.Add(Me.btnCreditMemo)
        Me.TabPage1.Controls.Add(Me.btnCashPayment)
        Me.TabPage1.Controls.Add(Me.btnRemoveFee)
        Me.TabPage1.Controls.Add(Me.btnChargeFee)
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 34)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(968, 372)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "ACCOUNT"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(408, 335)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(95, 30)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "EDIT AMOUNT"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtBalance
        '
        Me.txtBalance.Location = New System.Drawing.Point(868, 338)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.ReadOnly = True
        Me.txtBalance.Size = New System.Drawing.Size(85, 23)
        Me.txtBalance.TabIndex = 6
        Me.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(773, 341)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "TOTAL BALANCE"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(661, 342)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(78, 19)
        Me.CheckBox1.TabIndex = 7
        Me.CheckBox1.Text = "PRINT ALL"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'btnPrintSoa
        '
        Me.btnPrintSoa.Location = New System.Drawing.Point(509, 335)
        Me.btnPrintSoa.Name = "btnPrintSoa"
        Me.btnPrintSoa.Size = New System.Drawing.Size(146, 30)
        Me.btnPrintSoa.TabIndex = 6
        Me.btnPrintSoa.Text = "PRINT SOA"
        Me.btnPrintSoa.UseVisualStyleBackColor = True
        '
        'btnCreditMemo
        '
        Me.btnCreditMemo.Location = New System.Drawing.Point(205, 335)
        Me.btnCreditMemo.Name = "btnCreditMemo"
        Me.btnCreditMemo.Size = New System.Drawing.Size(95, 30)
        Me.btnCreditMemo.TabIndex = 8
        Me.btnCreditMemo.Text = "CREDIT MEMO"
        Me.btnCreditMemo.UseVisualStyleBackColor = True
        '
        'btnCashPayment
        '
        Me.btnCashPayment.Location = New System.Drawing.Point(104, 335)
        Me.btnCashPayment.Name = "btnCashPayment"
        Me.btnCashPayment.Size = New System.Drawing.Size(95, 30)
        Me.btnCashPayment.TabIndex = 5
        Me.btnCashPayment.Text = "CASH PAYMENT"
        Me.btnCashPayment.UseVisualStyleBackColor = True
        '
        'btnRemoveFee
        '
        Me.btnRemoveFee.Location = New System.Drawing.Point(306, 335)
        Me.btnRemoveFee.Name = "btnRemoveFee"
        Me.btnRemoveFee.Size = New System.Drawing.Size(95, 30)
        Me.btnRemoveFee.TabIndex = 4
        Me.btnRemoveFee.Text = "REMOVE FEE"
        Me.btnRemoveFee.UseVisualStyleBackColor = True
        '
        'btnChargeFee
        '
        Me.btnChargeFee.Location = New System.Drawing.Point(3, 335)
        Me.btnChargeFee.Name = "btnChargeFee"
        Me.btnChargeFee.Size = New System.Drawing.Size(95, 30)
        Me.btnChargeFee.TabIndex = 3
        Me.btnChargeFee.Text = "CHARGE FEE"
        Me.btnChargeFee.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridView2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 34)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(968, 372)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "PAYMENT HISTORY"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.Column7})
        Me.DataGridView2.Location = New System.Drawing.Point(5, 6)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(878, 359)
        Me.DataGridView2.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "RECEIPT ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "RECIEPT"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "DATE"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 80
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn4.HeaderText = "FEE CODE"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 200
        '
        'Column7
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column7.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column7.HeaderText = "AMOUNT"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 75
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "FEE ID"
        Me.Column1.Name = "Column1"
        Me.Column1.Visible = False
        Me.Column1.Width = 50
        '
        'Column8
        '
        Me.Column8.HeaderText = "REF CODE"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "FEE CODE"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 200
        '
        'Column6
        '
        Me.Column6.HeaderText = "DATE"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 80
        '
        'clmAmount
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.clmAmount.DefaultCellStyle = DataGridViewCellStyle1
        Me.clmAmount.HeaderText = "AMOUNT"
        Me.clmAmount.Name = "clmAmount"
        Me.clmAmount.ReadOnly = True
        Me.clmAmount.Width = 75
        '
        'clmPayment
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.clmPayment.DefaultCellStyle = DataGridViewCellStyle2
        Me.clmPayment.HeaderText = "PAID"
        Me.clmPayment.Name = "clmPayment"
        Me.clmPayment.ReadOnly = True
        Me.clmPayment.Width = 75
        '
        'clmBalance
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.clmBalance.DefaultCellStyle = DataGridViewCellStyle3
        Me.clmBalance.HeaderText = "BALANCE"
        Me.clmBalance.Name = "clmBalance"
        Me.clmBalance.ReadOnly = True
        Me.clmBalance.Width = 75
        '
        'Column9
        '
        Me.Column9.HeaderText = ""
        Me.Column9.Name = "Column9"
        Me.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column9.Width = 35
        '
        'frm_nonstudent_account
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 511)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_nonstudent_account"
        Me.Text = "NON STUDENT ACCOUNT"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAccountName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents btnChargeFee As System.Windows.Forms.Button
    Friend WithEvents btnRemoveFee As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAccountID As System.Windows.Forms.TextBox
    Friend WithEvents btnCashPayment As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnPrintSoa As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents btnCreditMemo As System.Windows.Forms.Button
    Friend WithEvents txtBalance As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents clmAmount As DataGridViewTextBoxColumn
    Friend WithEvents clmPayment As DataGridViewTextBoxColumn
    Friend WithEvents clmBalance As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewCheckBoxColumn
End Class
