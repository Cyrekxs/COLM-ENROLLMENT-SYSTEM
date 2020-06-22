Public Class frm_nonstudent_lists
    Private Sub LoadNonStudents(Optional AccountType As String = "")
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim query As String = "SELECT * FROM tbl_nonstudent_information WHERE Name LIKE @search ORDER BY NAME ASC"

            If AccountType <> "" Then
                query = "SELECT * FROM tbl_nonstudent_information WHERE AccountType = @AccountType AND Name LIKE @search ORDER BY NAME ASC"
            End If

            Using comm As New SqlCommand("SELECT * FROM tbl_nonstudent_information WHERE Name LIKE @search ORDER BY NAME ASC", conn)
                comm.Parameters.AddWithValue("@search", TextBox1.Text & "%")
                comm.Parameters.AddWithValue("@AccountType", AccountType)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("NonStudentID"), reader("Name"), reader("AccountType"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_nonstudent_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadNonStudents()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If ComboBox1.SelectedIndex - 1 Then
            LoadNonStudents()
        Else
            LoadNonStudents(ComboBox1.Text)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frm As New frm_nonstudent_lists_entry(0, "", "", SavingOptions.NEW)
        With frm
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = btnEdit.Index Then
            Dim frm As New frm_nonstudent_lists_entry(DataGridView1.Rows(e.RowIndex).Cells(0).Value,
                                                      DataGridView1.Rows(e.RowIndex).Cells(1).Value,
                                                      DataGridView1.Rows(e.RowIndex).Cells(2).Value,
                                                      SavingOptions.EDIT)

            With frm
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                LoadNonStudents()
            End With
        ElseIf e.ColumnIndex = btnDelete.Index Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Dim CanDeleteAccount As Boolean = True
                Using comm As New SqlCommand("SELECT * FROM vw_nonstudent_collection_reports WHERE NonStudentID = @NonStudentID", conn)
                    comm.Parameters.AddWithValue("@NonStudentID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        If reader.HasRows = True Then
                            CanDeleteAccount = False
                        Else
                            CanDeleteAccount = True
                        End If
                    End Using
                End Using

                If CanDeleteAccount = True Then
                    If MsgBox("Are you sure you want to delete this account?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        Using t As SqlTransaction = conn.BeginTransaction
                            Try
                                Using comm As New SqlCommand("DELETE FROM tbl_nonstudent_account WHERE NonStudentID = @NonStudentID", conn, t)
                                    comm.Parameters.AddWithValue("@NonStudentID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                                    comm.ExecuteNonQuery()
                                End Using

                                Using comm As New SqlCommand("DELETE FROM tbl_nonstudent_information WHERE NonStudentID = @NonStudentID", conn, t)
                                    comm.Parameters.AddWithValue("@NonStudentID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                                    comm.ExecuteNonQuery()
                                End Using

                                t.Commit()
                                MsgBox("Inforamtion has been successfully deleted!", MsgBoxStyle.Information)
                                LoadNonStudents()
                            Catch ex As Exception
                                t.Rollback()
                                MsgBox(ex.Message, MsgBoxStyle.Critical)
                            End Try
                        End Using
                    End If
                End If
            End Using
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSearch.PerformClick()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        btnSearch.PerformClick()
    End Sub
End Class