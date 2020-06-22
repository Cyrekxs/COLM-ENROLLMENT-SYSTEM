<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_voucher_account_transaction_entry
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
        Me.txtVoucherDescription = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtVoucherAmount = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "VOUCHER DESCRIPTION"
        '
        'txtVoucherDescription
        '
        Me.txtVoucherDescription.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtVoucherDescription.Location = New System.Drawing.Point(15, 27)
        Me.txtVoucherDescription.Multiline = True
        Me.txtVoucherDescription.Name = "txtVoucherDescription"
        Me.txtVoucherDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtVoucherDescription.Size = New System.Drawing.Size(422, 97)
        Me.txtVoucherDescription.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "VOUCHER AMOUNT"
        '
        'txtVoucherAmount
        '
        Me.txtVoucherAmount.Location = New System.Drawing.Point(15, 145)
        Me.txtVoucherAmount.Name = "txtVoucherAmount"
        Me.txtVoucherAmount.Size = New System.Drawing.Size(422, 23)
        Me.txtVoucherAmount.TabIndex = 3
        Me.txtVoucherAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(311, 174)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(126, 30)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "SAVE TRANSACTION"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(220, 174)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(85, 30)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "CANCEL"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frm_voucher_account_transaction_entry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 212)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtVoucherAmount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtVoucherDescription)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_voucher_account_transaction_entry"
        Me.Text = "VOUCHER TRANSACTION EDIT | ENTRY"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtVoucherDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtVoucherAmount As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
