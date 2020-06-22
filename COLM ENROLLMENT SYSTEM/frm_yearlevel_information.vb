Public Class frm_yearlevel_information
    Public YrLvlID As Integer = 0
    Public SavingStatus As String = String.Empty

    Public Sub DisableControls()
        txtYrLvl.Enabled = False
        btnBrowseDepartment.Enabled = False
        txtDisplayIndex.Enabled = False

        btnNew.Enabled = True
        btnEdit.Enabled = True
        btnDelete.Enabled = True

        btnSave.Enabled = False
        btnCancel.Enabled = False

        txtSearch.Enabled = True
        DataGridView1.Enabled = True
    End Sub

    Public Sub EnableControls()
        txtYrLvl.Enabled = True
        btnBrowseDepartment.Enabled = True
        txtDisplayIndex.Enabled = True

        btnNew.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnSave.Enabled = True
        btnCancel.Enabled = True

        txtSearch.Enabled = False
        DataGridView1.Enabled = False
    End Sub

    Public Sub ClearControls()
        txtCourseCode.Text = String.Empty
        txtYrLvl.Text = String.Empty
        txtDisplayIndex.Text = String.Empty
    End Sub

    Public Sub LoadYrlvls()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS ORDER BY COURSE_CODE,ORDER_NO ASC", conn)
                DataGridView1.Rows.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView1.Rows.Add(reader("YEAR_ID"), reader("COURSE_CODE"), reader("YEAR_CODE"), reader("ORDER_NO"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_yearlevel_information_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadYrlvls()
        DisableControls()
        ClearControls()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE COURSE_CODE = @course_code AND YEAR_CODE = @yrlvl AND EDUCATION_LEVEL = 'COLLEGE'", conn)
                    comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", txtYrLvl.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_COLLEGE_YEARLEVELS VALUES(@display_index,@course_code,@yrlvl,@education_level)", conn)
                            comm1.Parameters.AddWithValue("@display_index", txtDisplayIndex.Text)
                            comm1.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", txtYrLvl.Text)
                            comm1.Parameters.AddWithValue("@education_level", "COLLEGE")
                            comm1.ExecuteNonQuery()
                        End Using
                    Else
                        MsgBox("Course Code: " & txtCourseCode.Text & " Year Level: " & txtYrLvl.Text & " is already in the list!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf SavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE COURSE_CODE = @course_code AND YEAR_CODE = @yrlvl AND EDUCATION_LEVEL = 'COLLEGE' AND Year_ID <> @yrlvlid", conn)
                    comm.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", txtYrLvl.Text)
                    comm.Parameters.AddWithValue("@yrlvlid", YrLvlID)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_COLLEGE_YEARLEVELS SET ORDER_NO = @display_index, COURSE_CODE = @course_code, YRLVL = @yrlvl WHERE YEAR_ID = @yrlvlid", conn)
                            comm1.Parameters.AddWithValue("@yrlvlid", YrLvlID)
                            comm1.Parameters.AddWithValue("@display_index", txtDisplayIndex.Text)
                            comm1.Parameters.AddWithValue("@course_code", txtCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", txtYrLvl.Text)
                            comm1.Parameters.AddWithValue("@education_level", "COLLEGE")
                            comm1.ExecuteNonQuery()
                        End Using
                    Else
                        MsgBox("Course Code: " & txtCourseCode.Text & " Year Level: " & txtYrLvl.Text & " is already in the list!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        YrLvlID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        txtCourseCode.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtYrLvl.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtDisplayIndex.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
    End Sub
End Class