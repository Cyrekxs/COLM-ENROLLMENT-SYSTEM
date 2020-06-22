Public Class frm_nonstudent_lists_entry
    Private _SavingStatus As New SavingOptions
    Private _NonStudentID As Integer
    Sub New(NonStudentID As Integer, AccountName As String, AccountType As String, SaveStatus As SavingOptions)
        InitializeComponent()
        _SavingStatus = SaveStatus
        _NonStudentID = NonStudentID
        TextBox1.Text = AccountName
        ComboBox1.Text = AccountType
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = String.Empty Then
            MsgBox("Please enter account name!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If ComboBox1.Text = String.Empty Then
            MsgBox("Please select account type!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            If _SavingStatus = SavingOptions.NEW Then
                Using comm As New SqlCommand("INSERT INTO tbl_nonstudent_information VALUES (@Name,@AccountType)", conn)
                    comm.Parameters.AddWithValue("@Name", TextBox1.Text)
                    comm.Parameters.AddWithValue("@AccountType", ComboBox1.Text)
                    comm.ExecuteNonQuery()
                    MsgBox("Account has been successfully saved!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End Using
            ElseIf _SavingStatus = SavingOptions.EDIT Then
                Using comm As New SqlCommand("UPDATE tbl_nonstudent_information SET Name = @Name,AccountType = @AccountType WHERE NonStudentID = @NonStudentID", conn)
                    comm.Parameters.AddWithValue("@NonStudentID", _NonStudentID)
                    comm.Parameters.AddWithValue("@Name", TextBox1.Text)
                    comm.Parameters.AddWithValue("@AccountType", ComboBox1.Text)
                    comm.ExecuteNonQuery()
                    MsgBox("Account has been successfully update!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                End Using
            End If
        End Using
    End Sub
End Class