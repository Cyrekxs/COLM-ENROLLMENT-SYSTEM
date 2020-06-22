Public Class student_information_import
    Public conn As New SqlConnection(StringConnection)
    Private DGRow As Integer = 0
    Public Sub LoadStudents()
        Using comm As New SqlCommand("SELECT ID,EDUCATION_LEVEL,STUDENT_NUMBER,LASTNAME + CASE EXTENSION_NAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + EXTENSION_NAME END + ' ' + FIRSTNAME + CASE MIDDLENAME WHEN 'N.A' THEN '' WHEN 'NONE' THEN '' WHEN '' THEN '' ELSE ' ' + MIDDLENAME END AS STUDENT_NAME FROM TBL_STUDENT_INFORMATION WHERE LASTNAME + ' ' + FIRSTNAME + ' ' + MIDDLENAME LIKE @search ORDER BY STUDENT_NAME ASC", conn)
            comm.Parameters.AddWithValue("@search", TextBox1.Text & "%")
            Using reader As SqlDataReader = comm.ExecuteReader
                DataGridView1.Rows.Clear()
                While reader.Read
                    DataGridView1.Rows.Add(reader("ID"), reader("EDUCATION_LEVEL"), reader("STUDENT_NUMBER"), reader("STUDENT_NAME"))
                End While
            End Using
        End Using
    End Sub
    Private Sub student_information_import_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If conn.State <> ConnectionState.Open Then
            conn.Close()
            conn.Open()
        Else
            conn.Open()
        End If
        LoadStudents()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        LoadStudents()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("Are you sure you want to import this data?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            With student_information_entry
                Using conn1 As New SqlConnection(StringConnection)
                    conn1.Open()
                    Using comm1 As New SqlCommand("SELECT * FROM TBL_STUDENT_INFORMATION WHERE ID = @id", conn)
                        comm1.Parameters.AddWithValue("@id", DataGridView1.Rows(DGRow).Cells(0).Value)
                        Using reader As SqlDataReader = comm1.ExecuteReader
                            While reader.Read
                                .txtLastName.Text = reader("LASTNAME")
                                .txtExtension.Text = reader("EXTENSION_NAME")
                                .txtFirstName.Text = reader("FIRSTNAME")
                                .txtMiddleName.Text = reader("MIDDLENAME")
                                .cmbMonth.Text = reader("B_MONTH")
                                .txtDay.Text = reader("B_DAY")
                                .cmbGender.Text = reader("GENDER")
                                .txtYear.Text = reader("B_YEAR")
                                .txtStudentMobile.Text = reader("MOBILE")
                                .txtStudentTelephone.Text = reader("TELEPHONE")
                                .txtStudentEmail.Text = reader("EMAIL_ADDRESS")
                                .txtStreet.Text = reader("STREET")
                                .txtSchoolName.Text = reader("School_Name")
                                .txtBrgy.Text = reader("BRGY")
                                .txtMunicipality.Text = reader("TOWN")
                                .txtProvince.Text = reader("PROVINCE")
                                .txtGuardianName.Text = reader("GUARDIAN_NAME")
                                .txtGuardianMobile.Text = reader("GUARDIAN_MOBILE")
                                .txtGuardianTelephone.Text = reader("GUARDIAN_TELEPHONE")
                                .txtGuardianRelation.Text = reader("MAIN_GUARDIAN")
                                Me.Close()
                                Me.Dispose()
                            End While
                        End Using
                    End Using
                End Using
            End With
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Button1.PerformClick()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub
End Class