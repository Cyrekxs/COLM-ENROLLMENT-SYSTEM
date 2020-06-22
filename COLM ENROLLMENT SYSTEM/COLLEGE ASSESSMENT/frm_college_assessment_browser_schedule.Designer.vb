<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_college_assessment_browser_schedule
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGSched = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSubjCode = New System.Windows.Forms.TextBox()
        Me.txtSubjDesc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.DGSched, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGSched
        '
        Me.DGSched.AllowUserToAddRows = False
        Me.DGSched.AllowUserToDeleteRows = False
        Me.DGSched.AllowUserToResizeColumns = False
        Me.DGSched.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        Me.DGSched.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGSched.BackgroundColor = System.Drawing.Color.White
        Me.DGSched.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGSched.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Column2, Me.Column9, Me.Column10, Me.Column11, Me.Column1, Me.Column12, Me.Column13})
        Me.DGSched.Location = New System.Drawing.Point(12, 56)
        Me.DGSched.Name = "DGSched"
        Me.DGSched.ReadOnly = True
        Me.DGSched.RowHeadersVisible = False
        Me.DGSched.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGSched.Size = New System.Drawing.Size(857, 291)
        Me.DGSched.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "SUBJECT CODE"
        '
        'txtSubjCode
        '
        Me.txtSubjCode.Enabled = False
        Me.txtSubjCode.Location = New System.Drawing.Point(12, 27)
        Me.txtSubjCode.Name = "txtSubjCode"
        Me.txtSubjCode.Size = New System.Drawing.Size(84, 23)
        Me.txtSubjCode.TabIndex = 4
        '
        'txtSubjDesc
        '
        Me.txtSubjDesc.Enabled = False
        Me.txtSubjDesc.Location = New System.Drawing.Point(102, 27)
        Me.txtSubjDesc.Name = "txtSubjDesc"
        Me.txtSubjDesc.Size = New System.Drawing.Size(766, 23)
        Me.txtSubjDesc.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(102, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 15)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "DESCRIPTION"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 353)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(265, 30)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "GET A SCHEDULE DIFFERENT FROM SUBJECT"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "SCHEDULE ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'Column2
        '
        Me.Column2.HeaderText = "COURSE/YEAR/SECTION"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 200
        '
        'Column9
        '
        Me.Column9.HeaderText = "DAY"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "TIME IN"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 120
        '
        'Column11
        '
        Me.Column11.HeaderText = "TIME OUT"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        Me.Column11.Width = 120
        '
        'Column1
        '
        Me.Column1.HeaderText = "ROOM"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column12
        '
        Me.Column12.HeaderText = "FACULTY"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Width = 120
        '
        'Column13
        '
        Me.Column13.HeaderText = "SELECT"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        Me.Column13.Text = "SELECT"
        Me.Column13.UseColumnTextForButtonValue = True
        Me.Column13.Width = 80
        '
        'frm_college_assessment_browser_schedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(880, 392)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtSubjDesc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSubjCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DGSched)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_college_assessment_browser_schedule"
        Me.Text = "SUBJECT SCHEDULE BROWSER"
        CType(Me.DGSched, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGSched As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSubjCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSubjDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewButtonColumn
End Class
