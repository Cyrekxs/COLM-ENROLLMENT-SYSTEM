Public Class frm_college_assessment_browse_subject
    Public Course As String = String.Empty
    Public SavingStatus As String = String.Empty
    Public DGRow As Integer = 0
    Public MyDGRow As Integer = 0
    Public Sub LoadAvailableSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT *,(SELECT DISTINCT SUBJ_PRICE FROM tbl_settings_college_curriculum_subjects_setted WHERE SUBJ_ID = tbl_settings_college_curriculum_subjects.CURRICULUM_ID AND COURSE_CODE = @course_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS MAIN_PRICE FROM tbl_settings_college_curriculum_subjects WHERE COURSE_CODE = @course_code AND SUBJ_CODE + SUBJ_DESC LIKE @search ORDER BY SUBJ_CODE,SUBJ_DESC ASC", conn)
                comm.Parameters.AddWithValue("@course_code", Course)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                comm.Parameters.AddWithValue("@search", "%" & StripSpaces(TextBox1.Text) & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_TFEE.Rows.Clear()
                    While reader.Read
                        If IsDBNull(reader("MAIN_PRICE")) = True Then
                            DG_TFEE.Rows.Add(reader("Subj_Code"), reader("Subj_Desc"), reader("Subj_Unit"), reader("Lec_Hours"), reader("Lab_Hours"), Convert_To_Currency(reader("SUBJ_Price")), "ADD TO LIST", Convert_To_Currency(reader("ENERGY_FEE")), Convert_To_Currency(reader("Defence_Fee")), reader("Curriculum_ID"))
                        Else
                            DG_TFEE.Rows.Add(reader("Subj_Code"), reader("Subj_Desc"), reader("Subj_Unit"), reader("Lec_Hours"), reader("Lab_Hours"), Convert_To_Currency(reader("MAIN_Price")), "ADD TO LIST", Convert_To_Currency(reader("ENERGY_FEE")), Convert_To_Currency(reader("Defence_Fee")), reader("Curriculum_ID"))
                        End If
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_college_assessment_browse_subject_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'frm_college_assessment.Load_College_Default_Fess()
    End Sub

    Private Sub frm_college_assessment_browse_subject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAvailableSubjects()
        If SavingStatus <> "ADD SUBJECT" Then
            Column5.Visible = False
        Else
            Column5.Visible = True
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadAvailableSubjects()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = String.Empty Then
            LoadAvailableSubjects()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If SavingStatus = "CHANGE SUBJECT" Then
            With frm_college_assessment
                Dim CanChange As Boolean = True
                For i = 0 To .DG_TFEE.Rows.Count - 1
                    If DG_TFEE.Rows(MyDGRow).Cells(0).Value = .DG_TFEE.Rows(i).Cells(1).Value And DGRow <> i Then
                        CanChange = False
                        Exit For
                    End If
                Next

                If CanChange = True Then
                    .DG_TFEE.Rows(DGRow).Cells(1).Value = DG_TFEE.Rows(MyDGRow).Cells(0).Value
                    .DG_TFEE.Rows(DGRow).Cells(2).Value = DG_TFEE.Rows(MyDGRow).Cells(1).Value
                    .DG_TFEE.Rows(DGRow).Cells(3).Value = DG_TFEE.Rows(MyDGRow).Cells(2).Value
                    .DG_TFEE.Rows(DGRow).Cells(4).Value = DG_TFEE.Rows(MyDGRow).Cells(3).Value
                    .DG_TFEE.Rows(DGRow).Cells(5).Value = DG_TFEE.Rows(MyDGRow).Cells(4).Value
                    .DG_TFEE.Rows(DGRow).Cells(6).Value = DG_TFEE.Rows(MyDGRow).Cells(5).Value
                    .DG_TFEE.Rows(DGRow).Cells(9).Value = DG_TFEE.Rows(MyDGRow).Cells(7).Value
                    .DG_TFEE.Rows(DGRow).Cells(10).Value = DG_TFEE.Rows(MyDGRow).Cells(8).Value

                    .DG_Schedule.Rows(DGRow).Cells(0).Value = DG_TFEE.Rows(MyDGRow).Cells(0).Value
                    .DG_Schedule.Rows(DGRow).Cells(1).Value = "-"
                    .DG_Schedule.Rows(DGRow).Cells(2).Value = "-"
                    .DG_Schedule.Rows(DGRow).Cells(3).Value = "-"
                    .DG_Schedule.Rows(DGRow).Cells(4).Value = "-"
                    .DG_Schedule.Rows(DGRow).Cells(5).Value = "-"
                    Me.Close()
                    Me.Dispose()
                Else
                    MsgBox("Subject: " & DG_TFEE.Rows(MyDGRow).Cells(1).Value & " is already exist in different row!", MsgBoxStyle.Critical)
                End If
            End With
        ElseIf SavingStatus = "ADD SUBJECT" Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub DG_TFEE_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_TFEE.CellContentClick
        If e.ColumnIndex = 6 Then
            With frm_college_assessment
                Dim CanAdd As Boolean = True
                For i = 0 To .DG_TFEE.Rows.Count - 1
                    If DG_TFEE.Rows(e.RowIndex).Cells(0).Value = .DG_TFEE.Rows(i).Cells(1).Value Then
                        CanAdd = False
                        Exit For
                    End If
                Next

                If CanAdd = True Then
                    .DG_TFEE.Rows.Add("", DG_TFEE.Rows(e.RowIndex).Cells(0).Value, DG_TFEE.Rows(e.RowIndex).Cells(1).Value, DG_TFEE.Rows(e.RowIndex).Cells(2).Value, DG_TFEE.Rows(e.RowIndex).Cells(3).Value, DG_TFEE.Rows(e.RowIndex).Cells(4).Value, DG_TFEE.Rows(e.RowIndex).Cells(5).Value, "CHANGE", "", DG_TFEE.Rows(e.RowIndex).Cells(7).Value, DG_TFEE.Rows(e.RowIndex).Cells(8).Value)
                    .DG_Schedule.Rows.Add(DG_TFEE.Rows(e.RowIndex).Cells(0).Value, "-", "-", "-", "-", "-")
                Else
                    MsgBox("Subject is already in list!", MsgBoxStyle.Critical)
                End If

            End With
        End If
    End Sub

    Private Sub DG_TFEE_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DG_TFEE.RowEnter
        MyDGRow = e.RowIndex
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class