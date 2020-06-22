Public Class frm_faculty_entry

    Public Saving_Status As String = String.Empty
    Public ID As String = String.Empty

    Public Sub Load_Departments()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT DEPARTMENT FROM TBL_COLLEGE_DEPARTMENTS ORDER BY DEPARTMENT ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbDepartment.Items.Clear()
                    While reader.Read
                        cmbDepartment.Items.Add(reader("DEPARTMENT"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_faculty_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If txtLname.Text = String.Empty Then
            MsgBox("Please enter faculty lastname!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtFname.Text = String.Empty Then
            MsgBox("Please enter faculty firstname", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Saving_Status = "NEW" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("INSERT INTO TBL_FACULTY_LIST VALUES(@lname,@fname,@mname,@department,@picture,'FALSE')", conn)
                    comm.Parameters.AddWithValue("@lname", StripSpaces(txtLname.Text))
                    comm.Parameters.AddWithValue("@fname", StripSpaces(txtFname.Text))
                    comm.Parameters.AddWithValue("@mname", StripSpaces(txtMname.Text))
                    comm.Parameters.AddWithValue("@department", cmbDepartment.Text)

                    Dim ms As New System.IO.MemoryStream()
                    PictureBox1.BackgroundImage.Save(ms, PictureBox1.BackgroundImage.RawFormat)
                    Dim data As Byte() = ms.GetBuffer
                    Dim p As New SqlParameter("@picture", SqlDbType.Image)
                    p.Value = data
                    comm.Parameters.Add(p)

                    If MsgBox("Are you sure you want to save this faculty information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        comm.ExecuteNonQuery()
                        Dim ID As String = String.Empty

                        Using comm1 As New SqlCommand("SELECT @@IDENTITY", conn)
                            ID = comm1.ExecuteScalar
                        End Using

                        With frm_faculty_List.DataGridView1
                            .Rows.Add(ID, StripSpaces(txtLname.Text & ", " & txtFname.Text & " " & txtMname.Text), cmbDepartment.Text, "EDIT", "DELETE")
                        End With

                        MsgBox("New faculty records has been successfully saved!", MsgBoxStyle.Information)
                        Me.Close()
                        Me.Dispose()
                    End If

                End Using
            End Using
        ElseIf Saving_Status = "EDIT" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("UPDATE TBL_FACULTY_LIST SET LASTNAME = @lname, FIRSTNAME = @fname, MIDDLENAME = @mname, DEPARTMENT = @department, PICTURE = @picture WHERE ID = @id", conn)
                    comm.Parameters.AddWithValue("@id", ID)
                    comm.Parameters.AddWithValue("@lname", StripSpaces(txtLname.Text))
                    comm.Parameters.AddWithValue("@fname", StripSpaces(txtFname.Text))
                    comm.Parameters.AddWithValue("@mname", StripSpaces(txtMname.Text))
                    comm.Parameters.AddWithValue("@department", cmbDepartment.Text)

                    Dim ms As New System.IO.MemoryStream()
                    PictureBox1.BackgroundImage.Save(ms, PictureBox1.BackgroundImage.RawFormat)
                    Dim data As Byte() = ms.GetBuffer
                    Dim p As New SqlParameter("@picture", SqlDbType.Image)
                    p.Value = data
                    comm.Parameters.Add(p)

                    If MsgBox("Are you sure you want to update this faculty information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        comm.ExecuteNonQuery()
                        With frm_faculty_List.DataGridView1
                            .Rows(frm_faculty_List.DG1_Selected_Row).Cells(1).Value = StripSpaces(txtLname.Text & ", " & txtFname.Text & " " & txtMname.Text)
                            .Rows(frm_faculty_List.DG1_Selected_Row).Cells(2).Value = cmbDepartment.Text
                        End With
                        MsgBox("Faculty information has been successfully updated!", MsgBoxStyle.Information)
                        Me.Close()
                        Me.Dispose()
                    End If
                End Using
            End Using
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Using FD As New OpenFileDialog()
                With FD
                    FD.Title = "OPEN IMAGE FILES"
                    .Filter = "IMAGE FILES|*.png;*.jpg"
                    If .ShowDialog = Windows.Forms.DialogResult.OK Then
                        PictureBox1.BackgroundImage = Image.FromFile(FD.FileName)
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox("Image Error!", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With frm_college_settings_department_list_entry
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            Load_Departments()
        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class