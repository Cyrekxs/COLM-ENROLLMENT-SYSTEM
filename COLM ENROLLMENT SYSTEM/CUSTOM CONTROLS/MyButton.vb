Public Class MyButton
    Inherits System.Windows.Forms.Button

    Public Sub New()
        Me.FlatStyle = Windows.Forms.FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.BackgroundImage = My.Resources.Close_White
        Me.BackgroundImageLayout = ImageLayout.Zoom
        Me.Height = 32 : Me.Width = 32
        Me.Anchor = AnchorStyles.Right
        Me.TabStop = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Me.Click
        Dim MyForm As Form = Me.FindForm
        MyForm.Dispose()
        MyForm.Close()
    End Sub

    Private Sub MyButton_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Me.BackgroundImage = My.Resources.Close_Marron
    End Sub

    Private Sub MyButton_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseHover
        Me.BackgroundImage = My.Resources.Close_Marron
    End Sub

    Private Sub MyButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.BackgroundImage = My.Resources.Close_Marron
    End Sub

    Private Sub MyButton_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Me.BackgroundImage = My.Resources.Close_White
    End Sub

    Private Sub MyButton_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Me.BackgroundImage = My.Resources.Close_White
    End Sub
End Class
