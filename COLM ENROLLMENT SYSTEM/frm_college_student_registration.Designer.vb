<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_college_student_registration
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtStudentNumber = New System.Windows.Forms.TextBox()
        Me.txtLastname = New System.Windows.Forms.TextBox()
        Me.txtFirstname = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtMiddlename = New System.Windows.Forms.TextBox()
        Me.cmbCourseCode = New System.Windows.Forms.ComboBox()
        Me.cmbYrLvl = New System.Windows.Forms.ComboBox()
        Me.cmbSectionCode = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(368, 50)
        Me.Panel1.TabIndex = 59
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(12, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(189, 20)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "STUDENT REGISTRATION"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 17)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Student No:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 17)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Last Name:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 187)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 17)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Course Code:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 218)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 17)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Year Level:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 249)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 17)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Section Code:"
        '
        'txtStudentNumber
        '
        Me.txtStudentNumber.Location = New System.Drawing.Point(110, 61)
        Me.txtStudentNumber.Name = "txtStudentNumber"
        Me.txtStudentNumber.ReadOnly = True
        Me.txtStudentNumber.Size = New System.Drawing.Size(211, 25)
        Me.txtStudentNumber.TabIndex = 65
        '
        'txtLastname
        '
        Me.txtLastname.Location = New System.Drawing.Point(110, 91)
        Me.txtLastname.Name = "txtLastname"
        Me.txtLastname.ReadOnly = True
        Me.txtLastname.Size = New System.Drawing.Size(246, 25)
        Me.txtLastname.TabIndex = 66
        '
        'txtFirstname
        '
        Me.txtFirstname.Location = New System.Drawing.Point(110, 122)
        Me.txtFirstname.Name = "txtFirstname"
        Me.txtFirstname.ReadOnly = True
        Me.txtFirstname.Size = New System.Drawing.Size(246, 25)
        Me.txtFirstname.TabIndex = 67
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 125)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 17)
        Me.Label7.TabIndex = 68
        Me.Label7.Text = "First Name:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 156)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(92, 17)
        Me.Label8.TabIndex = 69
        Me.Label8.Text = "Middle Name:"
        '
        'txtMiddlename
        '
        Me.txtMiddlename.Location = New System.Drawing.Point(110, 153)
        Me.txtMiddlename.Name = "txtMiddlename"
        Me.txtMiddlename.ReadOnly = True
        Me.txtMiddlename.Size = New System.Drawing.Size(246, 25)
        Me.txtMiddlename.TabIndex = 70
        '
        'cmbCourseCode
        '
        Me.cmbCourseCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCourseCode.FormattingEnabled = True
        Me.cmbCourseCode.Location = New System.Drawing.Point(110, 184)
        Me.cmbCourseCode.Name = "cmbCourseCode"
        Me.cmbCourseCode.Size = New System.Drawing.Size(246, 25)
        Me.cmbCourseCode.TabIndex = 71
        '
        'cmbYrLvl
        '
        Me.cmbYrLvl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYrLvl.FormattingEnabled = True
        Me.cmbYrLvl.Location = New System.Drawing.Point(110, 215)
        Me.cmbYrLvl.Name = "cmbYrLvl"
        Me.cmbYrLvl.Size = New System.Drawing.Size(246, 25)
        Me.cmbYrLvl.TabIndex = 72
        '
        'cmbSectionCode
        '
        Me.cmbSectionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSectionCode.FormattingEnabled = True
        Me.cmbSectionCode.Location = New System.Drawing.Point(110, 246)
        Me.cmbSectionCode.Name = "cmbSectionCode"
        Me.cmbSectionCode.Size = New System.Drawing.Size(246, 25)
        Me.cmbSectionCode.TabIndex = 73
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(327, 61)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 25)
        Me.Button1.TabIndex = 74
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnNew.ForeColor = System.Drawing.Color.White
        Me.btnNew.Location = New System.Drawing.Point(257, 277)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(99, 34)
        Me.btnNew.TabIndex = 75
        Me.btnNew.Text = "REGISTER"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(152, 277)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(99, 34)
        Me.Button2.TabIndex = 76
        Me.Button2.Text = "CANCEL"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'frm_college_student_registration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(368, 323)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmbSectionCode)
        Me.Controls.Add(Me.cmbYrLvl)
        Me.Controls.Add(Me.cmbCourseCode)
        Me.Controls.Add(Me.txtMiddlename)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtFirstname)
        Me.Controls.Add(Me.txtLastname)
        Me.Controls.Add(Me.txtStudentNumber)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frm_college_student_registration"
        Me.Text = "frm_student_registration"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtStudentNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtLastname As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstname As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMiddlename As System.Windows.Forms.TextBox
    Friend WithEvents cmbCourseCode As System.Windows.Forms.ComboBox
    Friend WithEvents cmbYrLvl As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSectionCode As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
