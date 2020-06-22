Public Class frm_sectioning_entry
    Public Sched_ID As String = String.Empty
    Public Curriculum_ID As String = String.Empty 'USED FOR FACULTY SUGGESSTION
    Public Subj_ID As String = String.Empty 'USED FOR LOADING THE SCHEDULES
    Public Saving_Status As String = String.Empty
    Public Faculty_Name As String = "-"
    Public Course_Code As String = String.Empty
    Public YrLvl As String = String.Empty
    Public Section_Code As String = String.Empty
    Public Section_Name As String = String.Empty

    Public Sub Enable_Controls()
        cmbDay.Enabled = True
        DT_TimeIn.Enabled = True
        DT_TimeOut.Enabled = True
        txtRoom.Enabled = True
        btnNew.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        DataGridView2.Enabled = False
    End Sub

    Public Sub Disable_Controls()
        cmbDay.Enabled = False
        DT_TimeIn.Enabled = False
        DT_TimeOut.Enabled = False
        txtRoom.Enabled = False
        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        DataGridView2.Enabled = True
    End Sub

    Public Sub Clear_Controls()
        cmbDay.SelectedIndex = -1
        DT_TimeIn.Value = "11/10/1993 8:00 AM"
        DT_TimeOut.Value = "11/10/1993 8:00 AM"
        txtRoom.Text = String.Empty
    End Sub

    Public Function Get_SectionName(ByVal Section_ID As String)
        Dim Section_Name As String = String.Empty
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT SECTION_CODE FROM TBL_SETTINGS_SECTIONS WHERE ID = @id", conn)
                comm.Parameters.AddWithValue("@id", Section_ID)
                Section_Name = comm.ExecuteScalar
            End Using
        End Using
        Return Section_Name
    End Function

    'Public Sub Load_Suggessted_Faculties()
    '    Using conn As New SqlConnection(StringConnection)
    '        conn.Open()
    '        Using comm As New SqlCommand("SELECT DISTINCT LASTNAME,FIRSTNAME,MIDDLENAME FROM TBL_FACULTY_LIST INNER JOIN TBL_FACULTY_PORTED_SUBJECTS ON TBL_FACULTY_LIST.ID = TBL_FACULTY_PORTED_SUBJECTS.FACULTY_ID INNER JOIN TBL_COLLEGE_CURRICULUM ON TBL_FACULTY_PORTED_SUBJECTS.SUBJ_ID = TBL_COLLEGE_CURRICULUM.CURRICULUM_ID WHERE CURRICULUM_ID = @curriculum_id", conn)
    '            comm.Parameters.AddWithValue("@curriculum_id", Curriculum_ID)
    '            cmbFaculty.Items.Clear()
    '            Using reader As SqlDataReader = comm.ExecuteReader
    '                cmbFaculty.Items.Add(StripSpaces(reader("LASTNAME") & ", " & reader("FIRSTNAME") & " " & reader("MIDDLENAME")))
    '            End Using
    '        End Using
    '    End Using
    'End Sub

    Public Sub Load_Rooms()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_ROOM ORDER BY ROOM_CODE,DESCRIPTION ASC", conn)
                txtRoom.AutoCompleteCustomSource.Clear()
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtRoom.AutoCompleteCustomSource.Add(reader("ROOM_CODE"))
                    End While

                End Using
            End Using
        End Using
    End Sub

    Public Sub Load_Schedules()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SUBJECT_SCHED WHERE SUBJ_ID = @subj_id AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_id AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem ORDER BY SCHED_DAY,SCHED_TIME_IN,SCHED_TIME_OUT,SCHED_ROOM ASC", conn)
                comm.Parameters.AddWithValue("@subj_id", Subj_ID)
                comm.Parameters.AddWithValue("@course_code", Course_Code)
                comm.Parameters.AddWithValue("@yrlvl", YrLvl)
                comm.Parameters.AddWithValue("@section_id", Section_Code)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView2.Rows.Clear()
                    While reader.Read
                        Dim Day_String As String = "-"
                        If IsDBNull(reader("SCHED_DAY")) = True And IsDBNull(reader("SCHED_TIME_IN")) = True And IsDBNull(reader("SCHED_TIME_OUT")) = True And IsDBNull(reader("SCHED_ROOM")) = True Then
                            DataGridView2.Rows.Add(reader("ID"), "-", "-", "-", "-", "EDIT", "DELETE")
                        Else
                            Select Case reader("SCHED_DAY")
                                Case 1 : Day_String = "MON"
                                Case 2 : Day_String = "TUE"
                                Case 3 : Day_String = "WED"
                                Case 4 : Day_String = "THU"
                                Case 5 : Day_String = "FRI"
                                Case 6 : Day_String = "SAT"
                                Case 7 : Day_String = "SUN"
                            End Select
                            DataGridView2.Rows.Add(reader("ID"), Day_String, reader("SCHED_TIME_IN").ToString.Replace("11/10/1993 ", ""), reader("SCHED_TIME_OUT").ToString.Replace("11/10/1993 ", ""), reader("SCHED_ROOM"), "EDIT", "DELETE")
                        End If
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_sectioning_entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Rooms()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Saving_Status = "NEW"
        Enable_Controls()
        Clear_Controls()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Saving_Status = "NEW" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                'STEP 1: TITIGNAN KO MUNA KUNG MAY TINAMAAN BANG SCHEDULE ANG MGA INPUTS PER DAY TIME AT ROOM
                'BACKUP TO PERO RUNNING QUERY TO..'Using comm As New SqlCommand("SELECT *,(SELECT SUBJ_DESC FROM TBL_SETTINGS_SUBJECT_SETTED WHERE ID = TBL_SETTINGS_SUBJECT_SCHED.SUBJ_ID) AS SUBJ_DESC FROM TBL_SETTINGS_SUBJECT_SCHED WHERE ((SCHED_TIME_IN = @INPUT_TIME_IN) OR (SCHED_TIME_OUT = @INPUT_TIME_OUT) OR (SCHED_TIME_OUT > @INPUT_TIME_IN AND SCHED_TIME_OUT < @INPUT_TIME_OUT) OR (SCHED_TIME_IN > @INPUT_TIME_IN AND SCHED_TIME_IN < @INPUT_TIME_OUT) OR (@INPUT_TIME_IN > SCHED_TIME_IN AND @INPUT_TIME_OUT < SCHED_TIME_OUT)) AND SCHED_ROOM = @INPUT_ROOM AND SCHED_DAY = @INPUT_DAY AND ACADEMIC_YEAR = @AY AND ACADEMIC_SEM = @SEM", conn)
                Using comm As New SqlCommand("SELECT *,(SELECT SUBJ_DESC FROM TBL_SETTINGS_SUBJECT_SETTED WHERE ID = TBL_SETTINGS_SUBJECT_SCHED.SUBJ_ID) AS SUBJ_DESC FROM TBL_SETTINGS_SUBJECT_SCHED WHERE ((SCHED_TIME_IN = @INPUT_TIME_IN) OR (SCHED_TIME_OUT = @INPUT_TIME_OUT) OR (SCHED_TIME_OUT > @INPUT_TIME_IN AND SCHED_TIME_OUT < @INPUT_TIME_OUT) OR (SCHED_TIME_IN > @INPUT_TIME_IN AND SCHED_TIME_IN < @INPUT_TIME_OUT) OR (@INPUT_TIME_IN > SCHED_TIME_IN AND @INPUT_TIME_OUT < SCHED_TIME_OUT)) AND ((SCHED_DAY = @INPUT_DAY AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) OR (SCHED_ROOM = @INPUT_ROOM AND SCHED_DAY = @INPUT_DAY AND ACADEMIC_YEAR = @AY AND ACADEMIC_SEM = @SEM))", conn)
                    Dim Day_No As Integer = 0
                    Select Case cmbDay.Text
                        Case "MON" : Day_No = 1
                        Case "TUE" : Day_No = 2
                        Case "WED" : Day_No = 3
                        Case "THU" : Day_No = 4
                        Case "FRI" : Day_No = 5
                        Case "SAT" : Day_No = 6
                        Case "SUN" : Day_No = 7
                    End Select
                    comm.Parameters.AddWithValue("@course_code", Course_Code)
                    comm.Parameters.AddWithValue("@yrlvl", YrLvl)
                    comm.Parameters.AddWithValue("@section_code", Section_Code)
                    comm.Parameters.AddWithValue("@input_time_in", DT_TimeIn.Value)
                    comm.Parameters.AddWithValue("@input_time_out", DT_TimeOut.Value)
                    comm.Parameters.AddWithValue("@input_day", Day_No)
                    comm.Parameters.AddWithValue("@input_room", txtRoom.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 0 Then
                        'STEP 2: SAVING OF NEW SCHEDULE
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_SUBJECT_SCHED VALUES(@ay,@sem,@faculty_name,@course_code,@yrlvl,@section_code,@subj_id,@sched_day,@sched_time_in,@sched_time_out,@sched_room)", conn)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm1.Parameters.AddWithValue("@faculty_name", Faculty_Name)
                            comm1.Parameters.AddWithValue("@course_code", Course_Code)
                            comm1.Parameters.AddWithValue("@yrlvl", YrLvl)
                            comm1.Parameters.AddWithValue("@section_code", Section_Code)
                            comm1.Parameters.AddWithValue("@subj_id", Subj_ID)
                            comm1.Parameters.AddWithValue("@sched_day", Day_No)
                            comm1.Parameters.AddWithValue("@sched_time_in", DT_TimeIn.Value)
                            comm1.Parameters.AddWithValue("@sched_time_out", DT_TimeOut.Value)
                            comm1.Parameters.AddWithValue("@sched_room", txtRoom.Text)
                            If MsgBox("Are you sure you want to save this information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("New schedule has been successfully saved!", MsgBoxStyle.Information)
                                frm_conflict_list.ListView1.Items.Clear()
                                Clear_Controls()
                                Disable_Controls()
                                Load_Schedules()
                            End If
                        End Using
                    Else
                        MsgBox("Schedules conflict!" & vbNewLine & "Click VIEW SCHEDULE CONFLICT to see the list!", MsgBoxStyle.Critical)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            With frm_conflict_list
                                .txtDay.Text = cmbDay.Text
                                .txtTimeIn.Text = DT_TimeIn.Text
                                .txtTimeOut.Text = DT_TimeOut.Text
                                .txtRoom.Text = txtRoom.Text
                                .txtCourseYrSection.Text = Course_Code & " | " & YrLvl & " | " & Get_SectionName(Section_Code)
                                .ListView1.Items.Clear()
                                While reader.Read
                                    If IsDBNull(reader("FACULTY_NAME")) = False Then
                                        .ListView1.Items.Add(reader("FACULTY_NAME"))
                                    Else
                                        .ListView1.Items.Add("NO FACULTY ASSIGNED")
                                    End If
                                    .ListView1.Items(.ListView1.Items.Count - 1).SubItems.Add(reader("SUBJ_DESC"))
                                    .ListView1.Items(.ListView1.Items.Count - 1).SubItems.Add(reader("COURSE_CODE") & " | " & reader("YRLVL") & " | " & Get_SectionName(reader("SECTION_CODE")))
                                    '.ListView1.Items(.ListView1.Items.Count - 1).SubItems.Add(reader("COURSE_CODE") & " | " & reader("YRLVL") & " | " & Section_Name) 'reader("SECTION_CODE"))
                                    .ListView1.Items(.ListView1.Items.Count - 1).SubItems.Add(reader("SCHED_TIME_IN").ToString.Replace("11/10/1993", "") & " - " & reader("SCHED_TIME_OUT").ToString.Replace("11/10/1993", ""))
                                End While
                            End With
                        End Using
                    End If

                End Using
            End Using
        ElseIf Saving_Status = "EDIT" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                'STEP 1: TITIGNAN KO MUNA KUNG MAY TINAMAAN BANG SCHEDULE ANG MGA INPUTS PER DAY TIME AT ROOM
                'BACKUP TO PERO RUNNING TO.'Using comm As New SqlCommand("SELECT *,(SELECT SUBJ_DESC FROM TBL_SETTINGS_SUBJECT_SETTED WHERE ID = TBL_SETTINGS_SUBJECT_SCHED.SUBJ_ID) AS SUBJ_DESC FROM TBL_SETTINGS_SUBJECT_SCHED WHERE ((SCHED_TIME_IN = @INPUT_TIME_IN) OR (SCHED_TIME_OUT = @INPUT_TIME_OUT) OR (SCHED_TIME_OUT > @INPUT_TIME_IN AND SCHED_TIME_OUT < @INPUT_TIME_OUT) OR (SCHED_TIME_IN > @INPUT_TIME_IN AND SCHED_TIME_IN < @INPUT_TIME_OUT) OR (@INPUT_TIME_IN > SCHED_TIME_IN AND @INPUT_TIME_OUT < SCHED_TIME_OUT)) AND SCHED_ROOM = @INPUT_ROOM AND SCHED_DAY = @INPUT_DAY AND ACADEMIC_YEAR = @AY AND ACADEMIC_SEM = @SEM AND ID <> @id", conn)
                Using comm As New SqlCommand("SELECT *,(SELECT SUBJ_DESC FROM TBL_SETTINGS_SUBJECT_SETTED WHERE ID = TBL_SETTINGS_SUBJECT_SCHED.SUBJ_ID) AS SUBJ_DESC FROM TBL_SETTINGS_SUBJECT_SCHED WHERE ((SCHED_TIME_IN = @INPUT_TIME_IN) OR (SCHED_TIME_OUT = @INPUT_TIME_OUT) OR (SCHED_TIME_OUT > @INPUT_TIME_IN AND SCHED_TIME_OUT < @INPUT_TIME_OUT) OR (SCHED_TIME_IN > @INPUT_TIME_IN AND SCHED_TIME_IN < @INPUT_TIME_OUT) OR (@INPUT_TIME_IN > SCHED_TIME_IN AND @INPUT_TIME_OUT < SCHED_TIME_OUT)) AND ((SCHED_DAY = @INPUT_DAY AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECTION_CODE = @section_code AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem) OR (SCHED_ROOM = @INPUT_ROOM AND SCHED_DAY = @INPUT_DAY AND ACADEMIC_YEAR = @AY AND ACADEMIC_SEM = @SEM)) AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", Sched_ID)
                    Dim Day_No As Integer = 0
                    Select Case cmbDay.Text
                        Case "MON" : Day_No = 1
                        Case "TUE" : Day_No = 2
                        Case "WED" : Day_No = 3
                        Case "THU" : Day_No = 4
                        Case "FRI" : Day_No = 5
                        Case "SAT" : Day_No = 6
                        Case "SUN" : Day_No = 7
                    End Select
                    comm.Parameters.AddWithValue("@course_code", Course_Code)
                    comm.Parameters.AddWithValue("@yrlvl", YrLvl)
                    comm.Parameters.AddWithValue("@section_code", Section_Code)
                    comm.Parameters.AddWithValue("@input_time_in", DT_TimeIn.Value)
                    comm.Parameters.AddWithValue("@input_time_out", DT_TimeOut.Value)
                    comm.Parameters.AddWithValue("@input_day", Day_No)
                    comm.Parameters.AddWithValue("@input_room", txtRoom.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 0 Then
                        'STEP 2: UPDATING OF THE SELECTED SCHEDULE
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_SUBJECT_SCHED SET COURSE_CODE = @course_code,YRLVL = @yrlvl,SECTION_CODE = @section_code, SCHED_DAY = @sched_day, SCHED_TIME_IN = @sched_time_in, SCHED_TIME_OUT = @sched_time_out, SCHED_ROOM = @sched_room WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", Sched_ID)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm1.Parameters.AddWithValue("@faculty_name", Faculty_Name)
                            comm1.Parameters.AddWithValue("@course_code", Course_Code)
                            comm1.Parameters.AddWithValue("@yrlvl", YrLvl)
                            comm1.Parameters.AddWithValue("@section_code", Section_Code)
                            comm1.Parameters.AddWithValue("@subj_id", Subj_ID)
                            comm1.Parameters.AddWithValue("@sched_day", Day_No)
                            comm1.Parameters.AddWithValue("@sched_time_in", DT_TimeIn.Value)
                            comm1.Parameters.AddWithValue("@sched_time_out", DT_TimeOut.Value)
                            comm1.Parameters.AddWithValue("@sched_room", txtRoom.Text)
                            If MsgBox("Are you sure you want to update this information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Schedule has been successfully updated!", MsgBoxStyle.Information)
                                frm_conflict_list.ListView1.Items.Clear()
                                Clear_Controls()
                                Disable_Controls()
                                Load_Schedules()
                            End If
                        End Using
                    Else
                        MsgBox("Schedules conflict!" & vbNewLine & "Click VIEW SCHEDULE CONFLICT to see the list!", MsgBoxStyle.Critical)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            With frm_conflict_list
                                .txtDay.Text = cmbDay.Text
                                .txtTimeIn.Text = DT_TimeIn.Text
                                .txtTimeOut.Text = DT_TimeOut.Text
                                .txtRoom.Text = txtRoom.Text
                                .txtCourseYrSection.Text = Course_Code & " | " & YrLvl & " | " & Get_SectionName(Section_Code)
                                .ListView1.Items.Clear()
                                While reader.Read
                                    If IsDBNull(reader("FACULTY_NAME")) = False Then
                                        .ListView1.Items.Add(reader("FACULTY_NAME"))
                                    Else
                                        .ListView1.Items.Add("NO FACULTY ASSIGNED")
                                    End If

                                    .ListView1.Items(.ListView1.Items.Count - 1).SubItems.Add(reader("SUBJ_DESC"))
                                    .ListView1.Items(.ListView1.Items.Count - 1).SubItems.Add(reader("COURSE_CODE") & " | " & reader("YRLVL") & " | " & Get_SectionName(reader("SECTION_CODE")))
                                    .ListView1.Items(.ListView1.Items.Count - 1).SubItems.Add(reader("SCHED_TIME_IN").ToString.Replace("11/10/1993", "") & " - " & reader("SCHED_TIME_OUT").ToString.Replace("11/10/1993", ""))
                                End While
                            End With
                        End Using
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Disable_Controls()
        Clear_Controls()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.ColumnIndex = 5 Then
            Sched_ID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            Saving_Status = "EDIT"
            Enable_Controls()
        End If
    End Sub

    Private Sub DataGridView2_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.RowEnter
        If DataGridView2.Rows(e.RowIndex).Cells(1).Value <> "-" And DataGridView2.Rows(e.RowIndex).Cells(2).Value <> "-" And DataGridView2.Rows(e.RowIndex).Cells(3).Value <> "-" And DataGridView2.Rows(e.RowIndex).Cells(4).Value <> "-" Then
            cmbDay.Text = DataGridView2.Rows(e.RowIndex).Cells(1).Value
            DT_TimeIn.Value = "11/10/1993 " & DataGridView2.Rows(e.RowIndex).Cells(2).Value
            DT_TimeOut.Value = "11/10/1993 " & DataGridView2.Rows(e.RowIndex).Cells(3).Value
            txtRoom.Text = DataGridView2.Rows(e.RowIndex).Cells(4).Value
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_conflict_list
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub
End Class