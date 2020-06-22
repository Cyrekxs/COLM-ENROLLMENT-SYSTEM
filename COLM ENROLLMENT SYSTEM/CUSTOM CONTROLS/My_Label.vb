Public Class My_Label
    Inherits System.Windows.Forms.Label

    Public Sub New()

    End Sub

    Private Sub TheCrazyProgrammersBtn_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Me.ForeColor = Color.IndianRed
    End Sub

    Private Sub TheCrazyProgrammersBtn_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.ForeColor = Color.SeaGreen
    End Sub

    Private Sub TheCrazyProgrammersBtn_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Me.ForeColor = Color.Black
    End Sub

    Private Sub TheCrazyProgrammersBtn_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Me.ForeColor = Color.SeaGreen
    End Sub
End Class
