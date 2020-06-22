<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_report_officially_enrolled
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
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Enrolled_senior_high2 = New COLM_ENROLLMENT_SYSTEM.enrolled_senior_high()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Enrolled_junior_high2 = New COLM_ENROLLMENT_SYSTEM.enrolled_junior_high()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Enrolled_college2 = New COLM_ENROLLMENT_SYSTEM.enrolled_college()
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
        Me.TabControl1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.ItemSize = New System.Drawing.Size(118, 25)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(866, 497)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Enrolled_college2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(858, 464)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "COLLEGE STUDENTS"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Enrolled_senior_high2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(858, 464)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "SENIOR HIGH STUDENTS"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Enrolled_senior_high2
        '
        Me.Enrolled_senior_high2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Enrolled_senior_high2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Enrolled_senior_high2.Location = New System.Drawing.Point(3, 3)
        Me.Enrolled_senior_high2.Name = "Enrolled_senior_high2"
        Me.Enrolled_senior_high2.Size = New System.Drawing.Size(852, 458)
        Me.Enrolled_senior_high2.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Enrolled_junior_high2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 29)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(858, 464)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "JUNIOR HIGH STUDENTS"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Enrolled_junior_high2
        '
        Me.Enrolled_junior_high2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Enrolled_junior_high2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Enrolled_junior_high2.Location = New System.Drawing.Point(3, 3)
        Me.Enrolled_junior_high2.Name = "Enrolled_junior_high2"
        Me.Enrolled_junior_high2.Size = New System.Drawing.Size(852, 458)
        Me.Enrolled_junior_high2.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 29)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(858, 464)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "ELEMENTARY STUDENTS"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(852, 458)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "NOT AVAILABLE YET"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Label2)
        Me.TabPage5.Location = New System.Drawing.Point(4, 29)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(858, 464)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "PREPARATORY STUDENTS"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(852, 458)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "NOT AVAILABLE YET"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Enrolled_college2
        '
        Me.Enrolled_college2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Enrolled_college2.Location = New System.Drawing.Point(6, 6)
        Me.Enrolled_college2.Name = "Enrolled_college2"
        Me.Enrolled_college2.Size = New System.Drawing.Size(832, 455)
        Me.Enrolled_college2.TabIndex = 0
        '
        'frm_report_officially_enrolled
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 521)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frm_report_officially_enrolled"
        Me.Text = "OFFICIALLY ENROLLED STUDENTS"
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
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Enrolled_college1 As COLM_ENROLLMENT_SYSTEM.enrolled_college
    Friend WithEvents Enrolled_senior_high1 As COLM_ENROLLMENT_SYSTEM.enrolled_senior_high
    Friend WithEvents Enrolled_junior_high1 As COLM_ENROLLMENT_SYSTEM.enrolled_junior_high
    Friend WithEvents Enrolled_senior_high2 As COLM_ENROLLMENT_SYSTEM.enrolled_senior_high
    Friend WithEvents Enrolled_junior_high2 As COLM_ENROLLMENT_SYSTEM.enrolled_junior_high
    Friend WithEvents Enrolled_college2 As COLM_ENROLLMENT_SYSTEM.enrolled_college
End Class
