<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainOfficial
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
        Me.FILEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.COLLEGEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.JUNIORHIGHToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SENIORHIGHToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FILEToolStripMenuItem, Me.ToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(974, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FILEToolStripMenuItem
        '
        Me.FILEToolStripMenuItem.Name = "FILEToolStripMenuItem"
        Me.FILEToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.FILEToolStripMenuItem.Text = "FILE"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.COLLEGEToolStripMenuItem, Me.JUNIORHIGHToolStripMenuItem, Me.SENIORHIGHToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(90, 20)
        Me.ToolStripMenuItem1.Text = "ASSESSMENT"
        '
        'COLLEGEToolStripMenuItem
        '
        Me.COLLEGEToolStripMenuItem.Name = "COLLEGEToolStripMenuItem"
        Me.COLLEGEToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.COLLEGEToolStripMenuItem.Text = "COLLEGE"
        '
        'JUNIORHIGHToolStripMenuItem
        '
        Me.JUNIORHIGHToolStripMenuItem.Name = "JUNIORHIGHToolStripMenuItem"
        Me.JUNIORHIGHToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.JUNIORHIGHToolStripMenuItem.Text = "JUNIOR HIGH"
        '
        'SENIORHIGHToolStripMenuItem
        '
        Me.SENIORHIGHToolStripMenuItem.Name = "SENIORHIGHToolStripMenuItem"
        Me.SENIORHIGHToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SENIORHIGHToolStripMenuItem.Text = "SENIOR HIGH"
        '
        'MainOfficial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 442)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainOfficial"
        Me.Text = "COLLEGE OF OUR LADY OF MERCY OF PULILAN FOUNDATION INC."
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FILEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents COLLEGEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JUNIORHIGHToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SENIORHIGHToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
