Public Class frm_nonstudent_browse
    Dim NonStudentID As Integer = 0
    Dim NonStudentName As String = String.Empty
    Public AccountType As New AccountTypes
    Enum AccountTypes
        [NON_STUDENT]
        [MEDICAL_ARTS]
    End Enum

    Private Sub LoadNontStudents_Lists()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_nonstudent_information WHERE Name LIKE @search AND AccountType = @AccountType", conn)
                comm.Parameters.AddWithValue("@search", "%" & TextBox1.Text & "%")
                If AccountType = AccountTypes.NON_STUDENT Then
                    comm.Parameters.AddWithValue("@AccountType", "NON STUDENT")
                ElseIf AccountType = AccountTypes.MEDICAL_ARTS Then
                    comm.Parameters.AddWithValue("@AccountType", "MEDICAL ARTS")
                End If
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("NonStudentID"), reader("Name"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Function Get_SelectedAccount() As List(Of String)
        Dim AccountInformation As New List(Of String)
        AccountInformation.Add(NonStudentID)
        AccountInformation.Add(NonStudentName)
        Return AccountInformation
    End Function

    Private Sub Set_SelectedAccount(ID As Integer, Name As String)
        NonStudentID = ID
        NonStudentName = Name
    End Sub

    Private Sub frm_non_student_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadNontStudents_Lists()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New frm_nonstudent_lists_entry(0, "", "", SavingOptions.NEW)
        With frm
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            LoadNontStudents_Lists()
        End With
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        LoadNontStudents_Lists()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 2 Then
            Set_SelectedAccount(DataGridView1.Rows(e.RowIndex).Cells(0).Value,
                                DataGridView1.Rows(e.RowIndex).Cells(1).Value)
            Me.Close()
            Me.Dispose()
        End If
    End Sub
End Class