Public Class My_NavigationPanel
    Inherits System.Windows.Forms.Panel

    Public Sub New()
        '65, 125, 90
    End Sub

    Private Sub TheCrazyProgrammersBtn_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Me.BackColor = Color.FromArgb(65, 125, 90)
    End Sub

    Private Sub TheCrazyProgrammersBtn_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.BackColor = Color.FromArgb(65, 125, 90)
    End Sub

    Private Sub TheCrazyProgrammersBtn_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Me.BackColor = Color.White
    End Sub

    Private Sub TheCrazyProgrammersBtn_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Me.BackColor = Color.FromArgb(65, 125, 90)
    End Sub
End Class
