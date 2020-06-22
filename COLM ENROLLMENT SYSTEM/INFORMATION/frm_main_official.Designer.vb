<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_main_official
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.STUDENTINFORMATIONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.REGISTRATIONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ASSESSMENTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PAYMENTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.REPORTSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SETTINGSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.STUDENTINFORMATIONToolStripMenuItem, Me.REGISTRATIONToolStripMenuItem, Me.ASSESSMENTToolStripMenuItem, Me.PAYMENTToolStripMenuItem, Me.REPORTSToolStripMenuItem, Me.SETTINGSToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1230, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'STUDENTINFORMATIONToolStripMenuItem
        '
        Me.STUDENTINFORMATIONToolStripMenuItem.Name = "STUDENTINFORMATIONToolStripMenuItem"
        Me.STUDENTINFORMATIONToolStripMenuItem.Size = New System.Drawing.Size(154, 20)
        Me.STUDENTINFORMATIONToolStripMenuItem.Text = "STUDENT INFORMATION"
        '
        'REGISTRATIONToolStripMenuItem
        '
        Me.REGISTRATIONToolStripMenuItem.Name = "REGISTRATIONToolStripMenuItem"
        Me.REGISTRATIONToolStripMenuItem.Size = New System.Drawing.Size(99, 20)
        Me.REGISTRATIONToolStripMenuItem.Text = "REGISTRATION"
        '
        'ASSESSMENTToolStripMenuItem
        '
        Me.ASSESSMENTToolStripMenuItem.Name = "ASSESSMENTToolStripMenuItem"
        Me.ASSESSMENTToolStripMenuItem.Size = New System.Drawing.Size(90, 20)
        Me.ASSESSMENTToolStripMenuItem.Text = "ASSESSMENT"
        '
        'PAYMENTToolStripMenuItem
        '
        Me.PAYMENTToolStripMenuItem.Name = "PAYMENTToolStripMenuItem"
        Me.PAYMENTToolStripMenuItem.Size = New System.Drawing.Size(74, 20)
        Me.PAYMENTToolStripMenuItem.Text = "PAYMENT"
        '
        'REPORTSToolStripMenuItem
        '
        Me.REPORTSToolStripMenuItem.Name = "REPORTSToolStripMenuItem"
        Me.REPORTSToolStripMenuItem.Size = New System.Drawing.Size(68, 20)
        Me.REPORTSToolStripMenuItem.Text = "REPORTS"
        '
        'SETTINGSToolStripMenuItem
        '
        Me.SETTINGSToolStripMenuItem.Name = "SETTINGSToolStripMenuItem"
        Me.SETTINGSToolStripMenuItem.Size = New System.Drawing.Size(71, 20)
        Me.SETTINGSToolStripMenuItem.Text = "SETTINGS"
        '
        'frm_main_official
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1230, 484)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "frm_main_official"
        Me.Text = "COLLEGE OF OUR LADY OF MERCY PULILAN FOUNDATION INC."
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents STUDENTINFORMATIONToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents REGISTRATIONToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ASSESSMENTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PAYMENTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents REPORTSToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SETTINGSToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
