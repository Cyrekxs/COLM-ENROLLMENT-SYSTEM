<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_edit_non_student_fee
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
        Me.txtOldAmount = New System.Windows.Forms.TextBox()
        Me.txtNewAmount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtFeeCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPayment = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "OLD AMOUNT"
        '
        'txtOldAmount
        '
        Me.txtOldAmount.Location = New System.Drawing.Point(99, 87)
        Me.txtOldAmount.Name = "txtOldAmount"
        Me.txtOldAmount.ReadOnly = True
        Me.txtOldAmount.Size = New System.Drawing.Size(129, 23)
        Me.txtOldAmount.TabIndex = 1
        Me.txtOldAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNewAmount
        '
        Me.txtNewAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNewAmount.Location = New System.Drawing.Point(99, 145)
        Me.txtNewAmount.Name = "txtNewAmount"
        Me.txtNewAmount.Size = New System.Drawing.Size(129, 23)
        Me.txtNewAmount.TabIndex = 3
        Me.txtNewAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 148)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "NEW AMOUNT"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(153, 174)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 30)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "SAVE"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtFeeCode
        '
        Me.txtFeeCode.Location = New System.Drawing.Point(15, 27)
        Me.txtFeeCode.Multiline = True
        Me.txtFeeCode.Name = "txtFeeCode"
        Me.txtFeeCode.ReadOnly = True
        Me.txtFeeCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFeeCode.Size = New System.Drawing.Size(213, 54)
        Me.txtFeeCode.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "FEE INFO"
        '
        'txtPayment
        '
        Me.txtPayment.Location = New System.Drawing.Point(99, 116)
        Me.txtPayment.Name = "txtPayment"
        Me.txtPayment.ReadOnly = True
        Me.txtPayment.Size = New System.Drawing.Size(129, 23)
        Me.txtPayment.TabIndex = 8
        Me.txtPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 15)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "PAYMENT"
        '
        'frm_edit_non_student_fee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(248, 223)
        Me.Controls.Add(Me.txtPayment)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtFeeCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtNewAmount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtOldAmount)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_edit_non_student_fee"
        Me.Text = "EDIT AMOUNT"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtOldAmount As TextBox
    Friend WithEvents txtNewAmount As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents txtFeeCode As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtPayment As TextBox
    Friend WithEvents Label4 As Label
End Class
