<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_payment_transfer
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtReceiptNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFeeCodeFrom = New System.Windows.Forms.TextBox()
        Me.txtAmountFrom = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPaymentDate = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRefCode = New System.Windows.Forms.TextBox()
        Me.txtFeeCodeTo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAmountTo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtPaymentDate)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtAmountFrom)
        Me.GroupBox1.Controls.Add(Me.txtFeeCodeFrom)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtReceiptNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(409, 219)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "TRANSFER PAYMENT INFORMATION (FROM)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "RECIEPT NO"
        '
        'txtReceiptNo
        '
        Me.txtReceiptNo.Location = New System.Drawing.Point(18, 46)
        Me.txtReceiptNo.Name = "txtReceiptNo"
        Me.txtReceiptNo.ReadOnly = True
        Me.txtReceiptNo.Size = New System.Drawing.Size(206, 23)
        Me.txtReceiptNo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "FEE CODE"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "AMOUNT"
        '
        'txtFeeCodeFrom
        '
        Me.txtFeeCodeFrom.Location = New System.Drawing.Point(18, 90)
        Me.txtFeeCodeFrom.Name = "txtFeeCodeFrom"
        Me.txtFeeCodeFrom.ReadOnly = True
        Me.txtFeeCodeFrom.Size = New System.Drawing.Size(373, 23)
        Me.txtFeeCodeFrom.TabIndex = 4
        '
        'txtAmountFrom
        '
        Me.txtAmountFrom.Location = New System.Drawing.Point(18, 134)
        Me.txtAmountFrom.Name = "txtAmountFrom"
        Me.txtAmountFrom.ReadOnly = True
        Me.txtAmountFrom.Size = New System.Drawing.Size(206, 23)
        Me.txtAmountFrom.TabIndex = 5
        Me.txtAmountFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 160)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "PAYMENT DATE"
        '
        'txtPaymentDate
        '
        Me.txtPaymentDate.Location = New System.Drawing.Point(18, 178)
        Me.txtPaymentDate.Name = "txtPaymentDate"
        Me.txtPaymentDate.ReadOnly = True
        Me.txtPaymentDate.Size = New System.Drawing.Size(206, 23)
        Me.txtPaymentDate.TabIndex = 7
        Me.txtPaymentDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.txtAmountTo)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtFeeCodeTo)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtRefCode)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 237)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(409, 236)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "TRANSFER PAYMENT INFORMATION (TO)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 15)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "REFERENCE CODE"
        '
        'txtRefCode
        '
        Me.txtRefCode.Location = New System.Drawing.Point(18, 47)
        Me.txtRefCode.Name = "txtRefCode"
        Me.txtRefCode.ReadOnly = True
        Me.txtRefCode.Size = New System.Drawing.Size(206, 23)
        Me.txtRefCode.TabIndex = 2
        '
        'txtFeeCodeTo
        '
        Me.txtFeeCodeTo.Location = New System.Drawing.Point(18, 91)
        Me.txtFeeCodeTo.Name = "txtFeeCodeTo"
        Me.txtFeeCodeTo.ReadOnly = True
        Me.txtFeeCodeTo.Size = New System.Drawing.Size(373, 23)
        Me.txtFeeCodeTo.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 15)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "FEE CODE"
        '
        'txtAmountTo
        '
        Me.txtAmountTo.Location = New System.Drawing.Point(18, 135)
        Me.txtAmountTo.Name = "txtAmountTo"
        Me.txtAmountTo.ReadOnly = True
        Me.txtAmountTo.Size = New System.Drawing.Size(206, 23)
        Me.txtAmountTo.TabIndex = 8
        Me.txtAmountTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 117)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 15)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "AMOUNT"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(230, 47)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(161, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "BROWSE ADDITIONAL FEES"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(259, 200)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(132, 30)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "PROCEED TRANSFER"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(166, 200)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(87, 30)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "CANCEL"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'frm_payment_transfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(432, 487)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_payment_transfer"
        Me.Text = "PAYMENT TRANSFER"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtReceiptNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAmountFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtFeeCodeFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPaymentDate As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAmountTo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtFeeCodeTo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRefCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
