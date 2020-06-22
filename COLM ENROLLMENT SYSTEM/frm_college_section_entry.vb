Public Class frm_college_section_entry
    Public Saving_Status As String = String.Empty
    Public Section_ID As String = String.Empty
    Public Section_Name As String = String.Empty


    Public Sub Load_Subjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT *,(SELECT COUNT(ID) FROM TBL_SETTINGS_SUBJECT_SCHED WHERE SUBJ_ID = TBL_SETTINGS_SUBJECT_SETTED.ID AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem AND SCHED_DAY IS NOT NULL AND SCHED_TIME_IN IS NOT NULL AND SCHED_TIME_OUT IS NOT NULL AND SCHED_ROOM IS NOT NULL) AS NO_OF_SCHED,(SELECT DISTINCT FACULTY_NAME FROM TBL_SETTINGS_SUBJECT_SCHED WHERE SUBJ_ID = TBL_SETTINGS_SUBJECT_SETTED.ID AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) AS SETTED_FACULTY FROM TBL_SETTINGS_SUBJECT_SETTED WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem AND IS_DELETED = 'FALSE' AND LOAD_TYPE = 'REGULAR'", conn)
                comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                comm.Parameters.AddWithValue("@yrlvl", cmbYearLvl.Text)
                comm.Parameters.AddWithValue("@section_code", Section_ID)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        If IsDBNull(reader("SETTED_FACULTY")) = True Then
                            DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), reader("NO_OF_SCHED"), "SET FACULTY", "VIEW SCHEDULE")
                        Else
                            DataGridView1.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), reader("NO_OF_SCHED"), reader("SETTED_FACULTY"), "VIEW SCHEDULE")
                        End If
                    End While
                End Using
            End Using
        End Using
    End Sub


    Private Sub frm_college_section_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Saving_Status = "NEW" Then
            Load_Course_Codes(cmbCourse)
            cmbCourse.Enabled = True
            cmbYearLvl.Enabled = True
        ElseIf Saving_Status = "EDIT" Then
            cmbCourse.Enabled = False
            cmbYearLvl.Enabled = False
        End If

        With DataGridView1
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If cmbCourse.Text = String.Empty Or cmbYearLvl.Text = String.Empty Or txtSectionName.Text = String.Empty Then
            MsgBox("Please enter course,year level and section!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If DataGridView1.Rows.Count = 0 Then
            MsgBox("Please SET the subject first of the Course: " & cmbCourse.Text & " " & cmbYearLvl.Text, MsgBoxStyle.Critical, "SET SUBJECT FIRST")
            Exit Sub
        End If

        If Saving_Status = "NEW" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL = 'COLLEGE' AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYearLvl.Text)
                    comm.Parameters.AddWithValue("@section_code", StripSpaces(txtSectionName.Text))
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 0 Then
                        If MsgBox("Are you sure you want to save this new section?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_SECTIONS VALUES('COLLEGE',@course_code,@yrlvl,@section_code,@ay,@sem)", conn)
                                comm1.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                                comm1.Parameters.AddWithValue("@yrlvl", cmbYearLvl.Text)
                                comm1.Parameters.AddWithValue("@section_code", StripSpaces(txtSectionName.Text))
                                comm1.Parameters.AddWithValue("@ay", Academic_Year)
                                comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm1.ExecuteNonQuery()
                            End Using

                            Section_ID = 0
                            Using comm1 As New SqlCommand("SELECT @@IDENTIY", conn)
                                Section_ID = comm.ExecuteScalar
                            End Using


                            For i = 0 To DataGridView1.Rows.Count - 1
                                Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_SUBJECT_SCHED VALUES(@ay,@sem,NULL,@course_code,@yrlvl,@section_code,@subj_id,NULL,NULL,NULL,NULL)", conn)
                                    comm1.Parameters.AddWithValue("@ay", Academic_Year)
                                    comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                                    comm1.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                                    comm1.Parameters.AddWithValue("@yrlvl", cmbYearLvl.Text)
                                    comm1.Parameters.AddWithValue("@section_code", Section_ID)
                                    comm1.Parameters.AddWithValue("@subj_id", DataGridView1.Rows(i).Cells(0).Value)
                                    comm1.ExecuteNonQuery()
                                End Using
                            Next
                            MsgBox("New section has been successfully saved!", MsgBoxStyle.Information)
                        End If
                    Else
                        MsgBox("Section Code: " & txtSectionName.Text & " in Course and Year: " & cmbCourse.Text & ", " & cmbYearLvl.Text & " is already exist!", MsgBoxStyle.Critical)
                    End If
                End Using
            End Using
        ElseIf Saving_Status = "EDIT" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL = 'COLLEGE' AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", Section_ID)
                    comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYearLvl.Text)
                    comm.Parameters.AddWithValue("@section_code", StripSpaces(txtSectionName.Text))
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_SECTIONS SET SECTION_CODE = @section_code WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", Section_ID)
                            comm1.Parameters.AddWithValue("@section_code", txtSectionName.Text)
                            If MsgBox("Are you sure you want to update the section name?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Section has been successfully updated!", MsgBoxStyle.Information)
                            End If
                        End Using
                    End If
                End Using
            End Using
        ElseIf Saving_Status = "VIEW" Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub cmbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse.SelectedIndexChanged
        Load_YearLvls("COLLEGE", cmbCourse.Text, cmbYearLvl)
    End Sub

    Private Sub cmbYearLvl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYearLvl.SelectedIndexChanged
        Load_Subjects()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 8 Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SUBJECT_SCHED WHERE SUBJ_ID = @subj_id AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@subj_id", DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                    comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYearLvl.Text)
                    comm.Parameters.AddWithValue("@section_code", Section_ID)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 0 Then
                        MsgBox("Please save the information first before viewing or adding schedule to the subject!", MsgBoxStyle.Critical)
                    Else
                        With frm_schedule_entry
                            .Subj_ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                            .Faculty_Name = DataGridView1.Rows(e.RowIndex).Cells(7).Value
                            .Course_Code = cmbCourse.Text
                            .YrLvl = cmbYearLvl.Text
                            .Section_Code = Section_ID
                            .Section_Name = txtSectionName.Text
                            .txtSubjCode.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                            .txtSubjDesc.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                            .txtUnit.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                            .txtLecHours.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                            .txtLabHours.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
                            .Load_Rooms()
                            .Load_Schedules()

                            .Disable_Controls()
                            .Clear_Controls()

                            .StartPosition = FormStartPosition.CenterParent
                            .ShowDialog()
                            Load_Subjects()
                        End With
                    End If
                End Using
            End Using
        ElseIf e.ColumnIndex = 7 Then
            With frm_sectioning_faculty_setter
                .Subj_ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                .Form_Request = "SECTIONING"
                .Load_Faculties()

                .Course_Code = cmbCourse.Text
                .YrLvl = cmbYearLvl.Text
                .Section_Code = Section_ID
                .txtSubjCode.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                .txtSubjDesc.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                .txtUnit.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                .txtLecHours.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                .txtLabHours.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
                .txtSchedules.Text = DataGridView1.Rows(e.RowIndex).Cells(8).Value
                If DataGridView1.Rows(e.RowIndex).Cells(7).Value = "SET FACULTY" Then
                    .txtOldFaculty.Text = "NONE"
                Else
                    .txtOldFaculty.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value
                End If

                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
                Load_Subjects()
            End With
        End If

    End Sub
End Class