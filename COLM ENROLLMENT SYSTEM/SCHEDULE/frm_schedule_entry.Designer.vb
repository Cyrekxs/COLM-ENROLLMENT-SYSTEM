<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_schedule_entry
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCourseCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSectionCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.REMOVEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmbCurriculumCode = New System.Windows.Forms.ComboBox()
        Me.cmbCurriculumType = New System.Windows.Forms.ComboBox()
        Me.cmbYearLevel = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "CURRICULUM CODE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(138, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "CURRICULUM TYPE"
        '
        'txtCourseCode
        '
        Me.txtCourseCode.Enabled = False
        Me.txtCourseCode.Location = New System.Drawing.Point(268, 27)
        Me.txtCourseCode.Name = "txtCourseCode"
        Me.txtCourseCode.Size = New System.Drawing.Size(80, 23)
        Me.txtCourseCode.TabIndex = 5
        Me.txtCourseCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(265, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "COURSE CODE"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(351, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "YEAR LEVEL"
        '
        'txtSectionCode
        '
        Me.txtSectionCode.Location = New System.Drawing.Point(481, 27)
        Me.txtSectionCode.Name = "txtSectionCode"
        Me.txtSectionCode.Size = New System.Drawing.Size(314, 23)
        Me.txtSectionCode.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(478, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "SECTION CODE"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column9, Me.Column2, Me.Column3, Me.Column8, Me.Column4, Me.Column5, Me.Column7, Me.Column6})
        Me.DataGridView1.Location = New System.Drawing.Point(15, 56)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1068, 359)
        Me.DataGridView1.TabIndex = 92
        '
        'Column1
        '
        Me.Column1.HeaderText = "SCHEDULE ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "SUBJECT ID"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Visible = False
        '
        'Column2
        '
        Me.Column2.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Column2.HeaderText = "CODE"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.REMOVEToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(121, 26)
        '
        'REMOVEToolStripMenuItem
        '
        Me.REMOVEToolStripMenuItem.Name = "REMOVEToolStripMenuItem"
        Me.REMOVEToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.REMOVEToolStripMenuItem.Text = "REMOVE"
        '
        'Column3
        '
        Me.Column3.HeaderText = "DESCRIPTION"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column8
        '
        Me.Column8.HeaderText = "DAY"
        Me.Column8.Name = "Column8"
        Me.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column8.Width = 150
        '
        'Column4
        '
        Me.Column4.HeaderText = "TIME IN"
        Me.Column4.Name = "Column4"
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column4.Width = 200
        '
        'Column5
        '
        Me.Column5.HeaderText = "TIME OUT"
        Me.Column5.Name = "Column5"
        Me.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column5.Width = 200
        '
        'Column7
        '
        Me.Column7.HeaderText = "ROOM"
        Me.Column7.Name = "Column7"
        Me.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column6
        '
        Me.Column6.HeaderText = "FACULTY"
        Me.Column6.Name = "Column6"
        Me.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column6.Width = 200
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(887, 421)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(196, 30)
        Me.Button1.TabIndex = 93
        Me.Button1.Text = "SAVE SCHEDULE"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cmbCurriculumCode
        '
        Me.cmbCurriculumCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurriculumCode.FormattingEnabled = True
        Me.cmbCurriculumCode.Location = New System.Drawing.Point(14, 27)
        Me.cmbCurriculumCode.Name = "cmbCurriculumCode"
        Me.cmbCurriculumCode.Size = New System.Drawing.Size(121, 23)
        Me.cmbCurriculumCode.TabIndex = 94
        '
        'cmbCurriculumType
        '
        Me.cmbCurriculumType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurriculumType.FormattingEnabled = True
        Me.cmbCurriculumType.Location = New System.Drawing.Point(141, 27)
        Me.cmbCurriculumType.Name = "cmbCurriculumType"
        Me.cmbCurriculumType.Size = New System.Drawing.Size(121, 23)
        Me.cmbCurriculumType.TabIndex = 95
        '
        'cmbYearLevel
        '
        Me.cmbYearLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYearLevel.FormattingEnabled = True
        Me.cmbYearLevel.Items.AddRange(New Object() {"1ST YEAR", "2ND YEAR", "3RD YEAR", "4TH YEAR"})
        Me.cmbYearLevel.Location = New System.Drawing.Point(354, 27)
        Me.cmbYearLevel.Name = "cmbYearLevel"
        Me.cmbYearLevel.Size = New System.Drawing.Size(121, 23)
        Me.cmbYearLevel.TabIndex = 96
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(14, 421)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(157, 30)
        Me.Button2.TabIndex = 97
        Me.Button2.Text = "ADD UNLISTED SUBJECT"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(177, 421)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(147, 30)
        Me.Button3.TabIndex = 98
        Me.Button3.Text = "REMOVE FROM LIST"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'frm_schedule_entry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 460)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.cmbYearLevel)
        Me.Controls.Add(Me.cmbCurriculumType)
        Me.Controls.Add(Me.cmbCurriculumCode)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtSectionCode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCourseCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_schedule_entry"
        Me.Text = "SCHEDULE ENTRY"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCourseCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSectionCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmbCurriculumCode As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCurriculumType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbYearLevel As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents REMOVEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
