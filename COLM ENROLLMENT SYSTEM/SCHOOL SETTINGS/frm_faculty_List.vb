Public Class frm_faculty_List
    Public DG1_Selected_Row
    Public DG2_Selected_Row
    Public Curriculum_IDs As New List(Of Integer)
    Public Sub Load_Faculty_List()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_FACULTY_LIST WHERE LASTNAME + ', ' + FIRSTNAME LIKE @faculty_name AND IS_DELETED = 'FALSE' ORDER BY Lastname,Firstname ASC", conn)
                comm.Parameters.AddWithValue("@faculty_name", "%" & StripSpaces(TextBox1.Text) & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("FacultyID"), reader("LASTNAME") & ", " & reader("FIRSTNAME"), reader("DEPARTMENT"), "EDIT", "DELETE")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_faculty_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Faculty_List()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frm_faculty_entry
            .Saving_Status = "NEW"
            .Load_Departments()
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 3 Then



            With frm_faculty_entry
                .Saving_Status = "EDIT"
                .Load_Departments()

                Dim image_data As Byte() = Nothing
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()

                    Using comm As New SqlCommand("SELECT PICTURE FROM TBL_FACULTY_LIST WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                        If IsDBNull(comm.ExecuteScalar) = False Then
                            image_data = DirectCast(comm.ExecuteScalar, Byte())
                        End If
                    End Using

                    Using comm As New SqlCommand("SELECT * FROM TBL_FACULTY_LIST WHERE ID = @id", conn)
                        comm.Parameters.AddWithValue("@id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            While reader.Read
                                .ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                                .txtLname.Text = reader("LASTNAME")
                                .txtFname.Text = reader("FIRSTNAME")
                                .txtMname.Text = reader("MIDDLENAME")
                                .cmbDepartment.Text = reader("DEPARTMENT")
                            End While
                        End Using
                    End Using
                End Using

                If Not image_data Is Nothing Then
                    Dim mybytearray As Byte() = image_data
                    Dim myimage As Image
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(mybytearray)
                    myimage = Image.FromStream(ms)
                    .PictureBox1.BackgroundImage = myimage
                End If

                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        ElseIf e.ColumnIndex = 4 Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()

                Dim AffectedStudents As Integer = 0
                Using comm As New SqlCommand("SELECT COUNT(*) AS AffectedStudents FROM tbl_college_subject_loads WHERE Sched_Faculty LIKE @FacultyName", conn)
                    comm.Parameters.AddWithValue("@FacultyName", DataGridView1.Rows(e.RowIndex).Cells(1).Value)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            AffectedStudents = reader("AffectedStudents")
                        End While
                    End Using
                End Using

                If AffectedStudents = 0 Then
                    Using comm As New SqlCommand("DELETE FROM tbl_faculty_list WHERE ID = @FacultyID", conn)
                        comm.Parameters.AddWithValue("@FacultyID", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                        If MsgBox("Are you sure you want to delete this faculty?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) Then
                            comm.ExecuteNonQuery()
                        End If
                    End Using
                Else
                    MsgBox("Cannot delete this faculty because it has " & AffectedStudents & " students affected!", MsgBoxStyle.Critical)
                End If
            End Using
        End If
    End Sub

    Private Sub DataGridView1_QueryAccessibilityHelp(sender As Object, e As QueryAccessibilityHelpEventArgs) Handles DataGridView1.QueryAccessibilityHelp

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DG1_Selected_Row = e.RowIndex
        Dim image_data As Byte() = Nothing
        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            lblName.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            lblDepartment.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            Using comm As New SqlCommand("SELECT DISTINCT SUBJ_CODE,SUBJ_DESC FROM TBL_FACULTY_PORTED_SUBJECTS INNER JOIN tbl_settings_college_curriculum_subjects ON TBL_FACULTY_PORTED_SUBJECTS.SUBJ_ID = tbl_settings_college_curriculum_subjects.CURRICULUM_ID WHERE FACULTY_ID = @id ORDER BY SUBJ_CODE,SUBJ_DESC ASC", conn)
                comm.Parameters.AddWithValue("@id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("SUBJ_CODE"), reader("SUBJ_DESC"))
                    End While
                End Using
            End Using


            If Not image_data Is Nothing Then
                Dim mybytearray As Byte() = image_data
                Dim myimage As Image
                Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(mybytearray)
                myimage = Image.FromStream(ms)
                PictureBox1.BackgroundImage = myimage
            End If
        End Using

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        With frm_faculty_subjects_entry
            .Faculty_Id = DataGridView1.Rows(DG1_Selected_Row).Cells(0).Value
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView2_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.RowEnter
        DG2_Selected_Row = e.RowIndex
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects WHERE SUBJ_CODE = @subj_code", conn)
                comm.Parameters.AddWithValue("@subj_code", DataGridView2.Rows(e.RowIndex).Cells(0).Value)
                Using reader As SqlDataReader = comm.ExecuteReader
                    Curriculum_IDs.Clear()
                    While reader.Read
                        Curriculum_IDs.Add(reader("CURRICULUM_ID"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If MsgBox("Are you sure you want to remove this subject?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                For i = 0 To Curriculum_IDs.Count - 1
                    Using comm As New SqlCommand("DELETE FROM TBL_FACULTY_PORTED_SUBJECTS WHERE FACULTY_ID = @faculty_id AND SUBJ_ID = @subj_id", conn)
                        comm.Parameters.AddWithValue("@faculty_id", DataGridView1.Rows(DG1_Selected_Row).Cells(0).Value)
                        comm.Parameters.AddWithValue("@subj_id", Curriculum_IDs(i))
                        comm.ExecuteNonQuery()
                    End Using
                Next
            End Using
            DataGridView2.Rows.Remove(DataGridView2.Rows(DG2_Selected_Row))
            MsgBox("Subject has been successfully removed!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Faculty_List()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class