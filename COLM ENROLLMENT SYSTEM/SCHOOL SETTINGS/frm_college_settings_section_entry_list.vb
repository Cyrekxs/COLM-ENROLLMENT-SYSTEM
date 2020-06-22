Public Class frm_college_settings_section_entry_list
    Public SavingStatus As String = String.Empty
    Public Section_ID As Integer = 0

    Public DGRow1 As Integer = 0
    Public DGRow2 As Integer = 0

    Public Sub ClearControls()
        cmbEntryCourse.SelectedIndex = -1
        cmbEntryYear.SelectedIndex = -1
        txtEntrySectionName.Text = String.Empty
        DataGridView2.Rows.Clear()
        cmbScheduleCourse.SelectedIndex = -1
        cmbSchedulYear.SelectedIndex = -1
        cmbScheduleSection.SelectedIndex = -1
    End Sub

    Public Sub EnableControls()
        If SavingStatus = "NEW" Then
            cmbEntryCourse.Enabled = True
            cmbEntryYear.Enabled = True
        ElseIf SavingStatus = "EDIT" Then
            cmbEntryCourse.Enabled = False
            cmbEntryYear.Enabled = False
        End If

        txtEntrySectionName.Enabled = True

        btnAdd.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False
        btnSAve.Enabled = True
        btnCancel.Enabled = True

        DataGridView1.Enabled = False
        TabControl1.TabPages(1).Enabled = False
    End Sub

    Public Sub DisableControls()
        cmbEntryCourse.Enabled = False
        cmbEntryYear.Enabled = False
        txtEntrySectionName.Enabled = False

        btnAdd.Enabled = True
        btnEdit.Enabled = True
        btnDelete.Enabled = True
        btnSAve.Enabled = False
        btnCancel.Enabled = False

        DataGridView1.Enabled = True
        TabControl1.TabPages(1).Enabled = True
    End Sub

    Public Sub LoadListofSections()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT *,(SELECT COUNT(ID) FROM tbl_settings_college_curriculum_subjects_schedule WHERE SECTION_CODE = TBL_SETTINGS_SECTIONS.ID) AS NO_OF_SUBJECTS FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL = 'COLLEGE' AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem ORDER BY COURSE_CODE,YRLVL,SECTION_CODE ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("COURSE_CODE"), reader("YRLVL"), reader("SECTION_CODE"), reader("NO_OF_SUBJECTS"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadDefaultSubjects()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects_setted WHERE COURSE_CODE = @course AND YRLVL = @yrlvl AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem ORDER BY  ASC", conn)
                comm.Parameters.AddWithValue("@course", cmbEntryCourse.Text)
                comm.Parameters.AddWithValue("@yrlvl", cmbEntryYear.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), "-", "-", "-", "-")
                    End While
                End Using
            End Using
        End Using
    End Sub


    Private Sub frm_college_settings_section_entry_2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Course_Codes(cmbEntryCourse)
        Load_Course_Codes(cmbScheduleCourse)
        LoadListofSections()
        DisableControls()
    End Sub

    Private Sub cmbYearLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEntryYear.SelectedIndexChanged
        If SavingStatus = "NEW" Then
            LoadDefaultSubjects()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSAve.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = "NEW" Then

                If cmbEntryCourse.Text = String.Empty Then
                    MsgBox("Please select course!", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If cmbEntryYear.Text = String.Empty Then
                    MsgBox("Please select year level!", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If txtEntrySectionName.Text = String.Empty Then
                    MsgBox("Please enter section name!", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If DataGridView2.Rows.Count = 0 Then
                    MsgBox("No subject setted to the course of " & cmbEntryCourse.Text & " " & cmbEntryYear.Text & " Please set it first!", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL = 'COLLEGE' AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@course_code", cmbEntryCourse.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbEntryYear.Text)
                    comm.Parameters.AddWithValue("@section_code", StripSpaces(txtEntrySectionName.Text))
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 0 Then
                        If MsgBox("Are you sure you want to save this section named: " & txtEntrySectionName.Text & " in course of " & cmbEntryCourse.Text & " and year of " & cmbEntryYear.Text & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_SECTIONS VALUES('COLLEGE',@course_code,@yrlvl,@section_code,@ay,@sem)", conn)
                                comm1.Parameters.AddWithValue("@course_code", cmbEntryCourse.Text)
                                comm1.Parameters.AddWithValue("@yrlvl", cmbEntryYear.Text)
                                comm1.Parameters.AddWithValue("@section_code", StripSpaces(txtEntrySectionName.Text))
                                comm1.Parameters.AddWithValue("@ay", Academic_Year)
                                comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                                comm1.ExecuteNonQuery()
                            End Using

                            Section_ID = 0
                            Using comm1 As New SqlCommand("SELECT @@IDENTIY", conn)
                                Section_ID = comm.ExecuteScalar
                            End Using

                            For i = 0 To DataGridView2.Rows.Count - 1
                                Using comm1 As New SqlCommand("INSERT INTO tbl_settings_college_curriculum_subjects_schedule VALUES(@ay,@sem,'-',@course_code,@yrlvl,@section_code,@subj_id,'-','-','-','-')", conn)
                                    comm1.Parameters.AddWithValue("@ay", Academic_Year)
                                    comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                                    comm1.Parameters.AddWithValue("@course_code", cmbEntryCourse.Text)
                                    comm1.Parameters.AddWithValue("@yrlvl", cmbEntryYear.Text)
                                    comm1.Parameters.AddWithValue("@section_code", Section_ID)
                                    comm1.Parameters.AddWithValue("@subj_id", DataGridView2.Rows(i).Cells(0).Value)
                                    comm1.ExecuteNonQuery()
                                End Using
                            Next
                            MsgBox("Section Code: " & txtEntrySectionName.Text & " has been successfully saved in " & cmbEntryCourse.Text & " " & cmbEntryYear.Text & "!", MsgBoxStyle.Information)
                            DisableControls()
                            ClearControls()
                            LoadListofSections()
                        End If
                    Else
                        MsgBox("Section Code: " & txtEntrySectionName.Text & " is already exist in " & cmbEntryCourse.Text & " " & cmbEntryYear.Text & "!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf SavingStatus = "EDIT" Then

                If txtEntrySectionName.Text = String.Empty Then
                    MsgBox("Please enter section name!", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SECTIONS WHERE EDUCATION_LEVEL = 'COLLEGE' AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem AND ID <> @section_id", conn)
                    comm.Parameters.AddWithValue("@section_id", Section_ID)
                    comm.Parameters.AddWithValue("@course_code", cmbEntryCourse.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbEntryYear.Text)
                    comm.Parameters.AddWithValue("@section_code", StripSpaces(txtEntrySectionName.Text))
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_SECTIONS SET SECTION_CODE = @section_code WHERE ID = @section_id", conn)
                            comm1.Parameters.AddWithValue("@section_id", Section_ID)
                            comm1.Parameters.AddWithValue("@section_code", StripSpaces(txtEntrySectionName.Text))
                            comm1.ExecuteNonQuery()
                        End Using
                        MsgBox("Section Code: " & txtEntrySectionName.Text & " has been successfully updated into " & cmbEntryCourse.Text & " " & cmbEntryYear.Text & "!", MsgBoxStyle.Information)
                        DisableControls()
                        ClearControls()
                        LoadListofSections()
                    Else
                        MsgBox("Section Code: " & txtEntrySectionName.Text & " is already exist in " & cmbEntryCourse.Text & " " & cmbEntryYear.Text & "!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        SavingStatus = "NEW"
        EnableControls()
        ClearControls()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow1 = e.RowIndex
        Section_ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        cmbEntryCourse.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        cmbEntryYear.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtEntrySectionName.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        EnableControls()
        SavingStatus = "EDIT"
    End Sub

    Private Sub cmbEntryCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEntryCourse.SelectedIndexChanged
        Load_YearLvls("COLLEGE", cmbEntryCourse.Text, cmbEntryYear)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If SavingStatus = "NEW" Then
            If MsgBox("Are you sure you want cancel adding new section?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                ClearControls()
                DisableControls()
            End If
        ElseIf SavingStatus = "EDIT" Then
            If MsgBox("Are you sure you want cancel editing section?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                ClearControls()
                DisableControls()
            End If
        End If
    End Sub

    Private Sub cmbScheduleCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbScheduleCourse.SelectedIndexChanged
        Load_YearLvls("COLLEGE", cmbScheduleCourse.Text, cmbSchedulYear)
    End Sub

    Private Sub cmbSchedulYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSchedulYear.SelectedIndexChanged
        Load_Sections("COLLEGE", cmbScheduleCourse.Text, cmbSchedulYear.Text, cmbScheduleSection)
    End Sub

    Private Sub cmbScheduleSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbScheduleSection.SelectedIndexChanged
        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            Dim SectID As Integer = 0
            Using comm As New SqlCommand("SELECT ID FROM TBL_SETTINGS_SECTIONS WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@course_code", cmbScheduleCourse.Text)
                comm.Parameters.AddWithValue("@yrlvl", cmbSchedulYear.Text)
                comm.Parameters.AddWithValue("@section_code", cmbScheduleSection.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                SectID = comm.ExecuteScalar
            End Using

            Using comm As New SqlCommand("SELECT *,(SELECT SUBJ_CODE FROM tbl_settings_college_curriculum_subjects_setted WHERE ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID) AS SUBJ_CODE,(SELECT SUBJ_DESC FROM tbl_settings_college_curriculum_subjects_setted WHERE ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID) AS SUBJ_DESC,(SELECT SUBJ_UNIT FROM tbl_settings_college_curriculum_subjects_setted WHERE ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID) AS SUBJ_UNIT FROM tbl_settings_college_curriculum_subjects_schedule WHERE SECTION_CODE = @section_id AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@section_id", SectID)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        DataGridView2.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("SCHED_DAY"), reader("SCHED_TIME_IN"), reader("SCHED_TIME_OUT"), reader("SCHED_ROOM"), reader("FACULTY_NAME"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frm_schedule_entry_2
            .SchedID = DataGridView2.Rows(DGRow2).Cells(0).Value
            .txtSubjCode.Text = DataGridView2.Rows(DGRow2).Cells(1).Value
            .txtSubjectDesc.Text = DataGridView2.Rows(DGRow2).Cells(2).Value
            .txtCourse.Text = cmbScheduleCourse.Text
            .txtYear.Text = cmbSchedulYear.Text
            .txtSection.Text = cmbScheduleSection.Text


            .Sched_Day = DataGridView2.Rows(DGRow2).Cells(4).Value
            .Sched_TimeIn = DataGridView2.Rows(DGRow2).Cells(5).Value
            .Sched_TimeOut = DataGridView2.Rows(DGRow2).Cells(6).Value
            .Sched_Room = DataGridView2.Rows(DGRow2).Cells(7).Value
            .Sched_Faculty = DataGridView2.Rows(DGRow2).Cells(8).Value

            .txtDay.Text = DataGridView2.Rows(DGRow2).Cells(4).Value
            .txtStart.Text = DataGridView2.Rows(DGRow2).Cells(5).Value
            .txtEnd.Text = DataGridView2.Rows(DGRow2).Cells(6).Value
            .txtRoom.Text = DataGridView2.Rows(DGRow2).Cells(7).Value
            .LoadFaculties()
            .cmbFaculty.Text = DataGridView2.Rows(DGRow2).Cells(8).Value

            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()


            Using conn As New SqlConnection(StringConnection)
                conn.Open()

                Dim SectID As Integer = 0
                Using comm As New SqlCommand("SELECT ID FROM TBL_SETTINGS_SECTIONS WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@course_code", cmbScheduleCourse.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbSchedulYear.Text)
                    comm.Parameters.AddWithValue("@section_code", cmbScheduleSection.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    SectID = comm.ExecuteScalar
                End Using

                Using comm As New SqlCommand("SELECT *,(SELECT SUBJ_CODE FROM tbl_settings_college_curriculum_subjects_setted WHERE ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID) AS SUBJ_CODE,(SELECT SUBJ_DESC FROM tbl_settings_college_curriculum_subjects_setted WHERE ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID) AS SUBJ_DESC,(SELECT SUBJ_UNIT FROM tbl_settings_college_curriculum_subjects_setted WHERE ID = tbl_settings_college_curriculum_subjects_schedule.SUBJ_ID) AS SUBJ_UNIT FROM tbl_settings_college_curriculum_subjects_schedule WHERE SECTION_CODE = @section_id AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@section_id", SectID)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView2.Rows.Clear()
                        While reader.Read
                            DataGridView2.Rows.Add(reader("ID"), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("SCHED_DAY"), reader("SCHED_TIME_IN"), reader("SCHED_TIME_OUT"), reader("SCHED_ROOM"), reader("FACULTY_NAME"))
                        End While
                    End Using
                End Using
            End Using

        End With
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView2_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.RowEnter
        DGRow2 = e.RowIndex
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_college_settings_section_subject_entry
            .txtCourseCode.Text = cmbScheduleCourse.Text
            .txtYearLevel.Text = cmbSchedulYear.Text
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub
End Class