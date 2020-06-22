Public Class frm_college_curriculum_filtering
    Public Sub LoadCourses()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT COURSE_CODE FROM tbl_settings_college_curriculum_subjects ORDER BY COURSE_CODE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbCourse.Items.Clear()
                    While reader.Read
                        cmbCourse.Items.Add(reader("COURSE_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadYearLevels()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT YRLVL FROM tbl_settings_college_curriculum_subjects WHERE COURSE_CODE = @course_code ORDER BY YRLVL ASC", conn)
                comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbYear.Items.Clear()
                    While reader.Read
                        cmbYear.Items.Add(reader("YRLVL"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub LoadSemesters()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT ACADEMIC_SEM FROM tbl_settings_college_curriculum_subjects WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl ORDER BY ACADEMIC_SEM ASC", conn)
                comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbAcademicSem.Items.Clear()
                    While reader.Read
                        cmbAcademicSem.Items.Add(reader("ACADEMIC_SEM"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_college_curriculum_filtering_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If cmbCourse.Items.Count = 0 Then
            LoadCourses()
        End If
    End Sub

    Private Sub cmbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse.SelectedIndexChanged
        LoadYearLevels()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cmbCourse.Text = String.Empty Or cmbYear.Text = String.Empty Or cmbAcademicSem.Text = String.Empty Then
            MsgBox("Please fill up all filters!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        With frm_college_curriculum
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_curriculum_subjects WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_SEM = @sem ORDER BY COURSE_CODE,YRLVL,ACADEMIC_SEM,SUBJ_CODE ASC", conn)
                    comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                    comm.Parameters.AddWithValue("@sem", cmbAcademicSem.Text)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        .DataGridView1.Rows.Clear()
                        While reader.Read
                            .DataGridView1.Rows.Add(reader("CURRICULUM_ID"), UCase(reader("ACADEMIC_SEM")), reader("COURSE_CODE"), reader("YRLVL"), IIf(IsDBNull(reader("CHED_CODE")), "-", reader("CHED_CODE")), reader("SUBJ_CODE"), reader("SUBJ_DESC"), reader("SUBJ_UNIT"), reader("LEC_HOURS"), reader("LAB_HOURS"), Convert_To_Currency(reader("SUBJ_PRICE")))
                        End While
                    End Using
                End Using
            End Using
        End With
        Me.Close()
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        LoadSemesters()
    End Sub
End Class