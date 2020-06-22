<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_payers_registration
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
        Me.txtPayerName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPayerDescription = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtPayerCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "PAYER NAME:"
        '
        'txtPayerName
        '
        Me.txtPayerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPayerName.Location = New System.Drawing.Point(15, 71)
        Me.txtPayerName.Name = "txtPayerName"
        Me.txtPayerName.Size = New System.Drawing.Size(452, 23)
        Me.txtPayerName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(130, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "OTHER INFORMATIONS"
        '
        'txtPayerDescription
        '
        Me.txtPayerDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPayerDescription.Location = New System.Drawing.Point(15, 115)
        Me.txtPayerDescription.Multiline = True
        Me.txtPayerDescription.Name = "txtPayerDescription"
        Me.txtPayerDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPayerDescription.Size = New System.Drawing.Size(452, 207)
        Me.txtPayerDescription.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(392, 328)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 30)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "SAVE"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(311, 328)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 30)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "CANCEL"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtPayerCode
        '
        Me.txtPayerCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPayerCode.Enabled = False
        Me.txtPayerCode.Location = New System.Drawing.Point(15, 27)
        Me.txtPayerCode.Name = "txtPayerCode"
        Me.txtPayerCode.Size = New System.Drawing.Size(452, 23)
        Me.txtPayerCode.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(168, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "AUTO GENERATED PAYER CODE"
        '
        'frm_payers_registration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(477, 366)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtPayerCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtPayerDescription)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPayerName)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximumSize = New System.Drawing.Size(493, 400)
        Me.Name = "frm_payers_registration"
        Me.Text = "PAYER INFORMATION REGISTRATION"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPayerName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPayerDescription As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txtPayerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
