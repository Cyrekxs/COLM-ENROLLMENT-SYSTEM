Public Class frm_department_list_entry
    Dim Saving_Status As String = String.Empty
    Dim Department_ID As Integer = 0
    Private Sub Enable_Controls()
        txtDepartment.Enabled = True
        btnNew.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        DataGridView2.Enabled = False
    End Sub

    Private Sub Disable_Controls()
        txtDepartment.Enabled = False
        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        DataGridView2.Enabled = True
    End Sub

    Private Sub Load_Departments()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_DEPARTMENTS ORDER BY DEPARTMENT ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"), reader("DEPARTMENT"), "EDIT", "DELETE")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_department_list_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Departments()
        Disable_Controls()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.ColumnIndex = 2 Then
            Saving_Status = "EDIT"
            Enable_Controls()
            btnSave.Text = "UPDATE"
            txtDepartment.Focus()
        ElseIf e.ColumnIndex = 3 Then
            If MsgBox("Are you sure you want to delete this department?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("DELETE FROM TBL_COLLEGE_DEPARTMENTS WHERE ID = @department_id", conn)
                        comm.Parameters.AddWithValue("@department_id", DataGridView2.Rows(e.RowIndex).Cells(0).Value)
                        comm.ExecuteNonQuery()
                        MsgBox("Department has been successfully deleted!", MsgBoxStyle.Information)
                        Load_Departments()
                        txtDepartment.Text = String.Empty
                    End Using
                End Using
            End If
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Saving_Status = "NEW"
        Enable_Controls()
        txtDepartment.Text = String.Empty
        btnSave.Text = "SAVE"
        txtDepartment.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Saving_Status = "NEW" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_DEPARTMENTS WHERE DEPARTMENT = @department", conn)
                    comm.Parameters.AddWithValue("@department", StripSpaces(txtDepartment.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_COLLEGE_DEPARTMENTS VALUES(@department)", conn)
                            comm1.Parameters.AddWithValue("@department", StripSpaces(txtDepartment.Text))
                            If MsgBox("Are you sure you want to save this department?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New department has been successfully saved!", MsgBoxStyle.Information)
                                Load_Departments()
                                Disable_Controls()
                                txtDepartment.Text = String.Empty
                            End If
                        End Using
                    Else
                        MsgBox("Department is already exist!", MsgBoxStyle.Critical)
                    End If
                End Using
            End Using
        ElseIf Saving_Status = "EDIT" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_DEPARTMENTS WHERE DEPARTMENT = @department AND ID <> @department_id", conn)
                    comm.Parameters.AddWithValue("@department_id", Department_ID)
                    comm.Parameters.AddWithValue("@department", StripSpaces(txtDepartment.Text))
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_COLLEGE_DEPARTMENT SET DEPARTMENT = @department WHERE ID = @department_id", conn)
                            comm1.Parameters.AddWithValue("@department_id", Department_ID)
                            comm1.Parameters.AddWithValue("@department", StripSpaces(txtDepartment.Text))
                            If MsgBox("Are you sure you want to update this department?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Department has been successfully updated!", MsgBoxStyle.Information)
                                Load_Departments()
                                Disable_Controls()
                                txtDepartment.Text = String.Empty
                            End If
                        End Using
                    Else
                        MsgBox("Department is already exist!", MsgBoxStyle.Critical)
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub DataGridView2_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.RowEnter
        Department_ID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
    End Sub
End Class