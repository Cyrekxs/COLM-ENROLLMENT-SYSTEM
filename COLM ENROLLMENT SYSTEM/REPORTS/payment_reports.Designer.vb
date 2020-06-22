<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class payment_reports
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Payment_reports_tuition1 = New COLM_ENROLLMENT_SYSTEM.payment_reports_tuition()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Payment_reports_other_fees1 = New COLM_ENROLLMENT_SYSTEM.payment_reports_other_fees()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Payment_reports_individual_other_fees1 = New COLM_ENROLLMENT_SYSTEM.payment_reports_individual_other_fees()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Payment_reports_nonstudent1 = New COLM_ENROLLMENT_SYSTEM.payment_reports_nonstudent()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Payment_reports_medical_arts1 = New COLM_ENROLLMENT_SYSTEM.payment_reports_medical_arts()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(890, 485)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Payment_reports_tuition1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(882, 457)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "PAYMENT TRANSACTION IN STUDENT TUITION FEE"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Payment_reports_tuition1
        '
        Me.Payment_reports_tuition1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Payment_reports_tuition1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Payment_reports_tuition1.Location = New System.Drawing.Point(3, 3)
        Me.Payment_reports_tuition1.Name = "Payment_reports_tuition1"
        Me.Payment_reports_tuition1.Size = New System.Drawing.Size(876, 451)
        Me.Payment_reports_tuition1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Payment_reports_other_fees1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(882, 457)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "PAYMENT TRANSACTION IN STUDENT OTHER FEE"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Payment_reports_other_fees1
        '
        Me.Payment_reports_other_fees1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Payment_reports_other_fees1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Payment_reports_other_fees1.Location = New System.Drawing.Point(3, 3)
        Me.Payment_reports_other_fees1.Name = "Payment_reports_other_fees1"
        Me.Payment_reports_other_fees1.Size = New System.Drawing.Size(876, 451)
        Me.Payment_reports_other_fees1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Payment_reports_individual_other_fees1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(882, 457)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "STUDENTS PAID IN OTHER FEES"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Payment_reports_individual_other_fees1
        '
        Me.Payment_reports_individual_other_fees1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Payment_reports_individual_other_fees1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Payment_reports_individual_other_fees1.Location = New System.Drawing.Point(3, 3)
        Me.Payment_reports_individual_other_fees1.Name = "Payment_reports_individual_other_fees1"
        Me.Payment_reports_individual_other_fees1.Size = New System.Drawing.Size(876, 451)
        Me.Payment_reports_individual_other_fees1.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Payment_reports_nonstudent1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(882, 457)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "NON STUDENT COLLECTIONS"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Payment_reports_nonstudent1
        '
        Me.Payment_reports_nonstudent1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Payment_reports_nonstudent1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Payment_reports_nonstudent1.Location = New System.Drawing.Point(3, 3)
        Me.Payment_reports_nonstudent1.Name = "Payment_reports_nonstudent1"
        Me.Payment_reports_nonstudent1.Size = New System.Drawing.Size(876, 451)
        Me.Payment_reports_nonstudent1.TabIndex = 0
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Payment_reports_medical_arts1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 24)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(882, 457)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "MEDICAL ARTS"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Payment_reports_medical_arts1
        '
        Me.Payment_reports_medical_arts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Payment_reports_medical_arts1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Payment_reports_medical_arts1.Location = New System.Drawing.Point(3, 3)
        Me.Payment_reports_medical_arts1.Name = "Payment_reports_medical_arts1"
        Me.Payment_reports_medical_arts1.Size = New System.Drawing.Size(876, 451)
        Me.Payment_reports_medical_arts1.TabIndex = 0
        '
        'payment_reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(915, 509)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "payment_reports"
        Me.Text = "PAYMENT TRANSACTIONS REPORT"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Payment_reports_tuition1 As COLM_ENROLLMENT_SYSTEM.payment_reports_tuition
    Friend WithEvents Payment_reports_other_fees1 As COLM_ENROLLMENT_SYSTEM.payment_reports_other_fees
    Friend WithEvents Payment_reports_individual_other_fees1 As COLM_ENROLLMENT_SYSTEM.payment_reports_individual_other_fees
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Payment_reports_nonstudent1 As COLM_ENROLLMENT_SYSTEM.payment_reports_nonstudent
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents Payment_reports_medical_arts1 As COLM_ENROLLMENT_SYSTEM.payment_reports_medical_arts
End Class
