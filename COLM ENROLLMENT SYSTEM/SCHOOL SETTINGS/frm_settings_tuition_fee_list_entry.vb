Public Class frm_settings_tuition_fee_list_entry
    Public Saving_Status As String = String.Empty
    Public ID As String = String.Empty
    Public CourseCode As String = String.Empty
    Public Sub ClearControls()
        cmbEducationLevel.SelectedIndex = -1
        cmbCourseCode.SelectedIndex = -1
        cmbYear.SelectedIndex = -1
        txtAmount.Text = "0.00"
    End Sub

    Public Sub Enable_Controls()
        cmbEducationLevel.Enabled = True
        cmbCourseCode.Enabled = True
        cmbYear.Enabled = True
        txtAmount.Enabled = True
        btnNew.Enabled = False
        btnSAve.Enabled = True
        btnCancel.Enabled = True
        DataGridView1.Enabled = False
    End Sub

    Public Sub Disable_Controls()
        cmbEducationLevel.Enabled = False
        cmbCourseCode.Enabled = False
        cmbYear.Enabled = False
        txtAmount.Enabled = False
        btnNew.Enabled = True
        btnSAve.Enabled = False
        btnCancel.Enabled = False
        DataGridView1.Enabled = True
    End Sub

    Public Sub Load_Tuition_Fees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_TUITION_FEE WHERE ACADEMIC_YEAR = @ay ORDER BY ACADEMIC_SEM,EDUCATION_LEVEL,COURSE_CODE,YRLVL ASC", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("EDUCATION_LEVEL"), reader("COURSE_CODE"), reader("YRLVL"), Convert_To_Currency(reader("AMOUNT")), reader("ACADEMIC_SEM"), "EDIT", "DELETE")
                    End While
                End Using
            End Using
        End Using
    End Sub


    Private Sub frm_tuition_fee_listvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Tuition_Fees()
        Disable_Controls()
    End Sub

    Private Sub cmbEducationLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEducationLevel.SelectedIndexChanged
        If cmbEducationLevel.Text = "JUNIOR HIGH" Then
            CourseCode = "JUNIOR HIGH"
            cmbCourseCode.Items.Clear()
            cmbCourseCode.Items.Add(CourseCode)
            cmbCourseCode.SelectedIndex = 0
            cmbCourseCode.Enabled = False
            Load_YearLvls(cmbEducationLevel.Text, CourseCode, cmbYear)
        ElseIf cmbEducationLevel.Text = "SENIOR HIGH" Then
            CourseCode = "SENIOR HIGH"
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_SENIOR_COURSES ORDER BY COURSE_CODE ASC", conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        cmbCourseCode.Items.Clear()
                        While reader.Read
                            cmbCourseCode.Items.Add(reader("COURSE_CODE"))
                        End While
                    End Using
                End Using
            End Using
            cmbCourseCode.Enabled = True
        End If
    End Sub

    Private Sub cmbCourseCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourseCode.SelectedIndexChanged
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE EDUCATION_LEVEL = @education_level ORDER BY YEAR_CODE ASC", conn)
                comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbYear.Items.Clear()
                    While reader.Read
                        cmbYear.Items.Add(reader("YEAR_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnSAve_Click(sender As Object, e As EventArgs) Handles btnSAve.Click
        If cmbEducationLevel.Text = String.Empty Then
            MsgBox("Please select education level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbCourseCode.Text = String.Empty Then
            MsgBox("Please select course code", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cmbYear.Text = String.Empty Then
            MsgBox("Please select year level!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtAmount.Text = String.Empty Then
            MsgBox("Please enter amount!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If IsNumeric(txtAmount.Text) = False Then
            MsgBox("Invalid input on amount!", MsgBoxStyle.Critical)
            Exit Sub
        Else
            If txtAmount.Text <= 0 Then
                MsgBox("Please enter amount greater than 0!", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            If Saving_Status = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_TUITION_FEE WHERE EDUCATION_LEVEL = @education_level AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_YEAR = @ay", conn)
                    comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                    comm.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_SETTINGS_TUITION_FEE VALUES(@education_level,@course_code,@yrlvl,@amount,@ay,@sem)", conn)
                            comm1.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                            comm1.Parameters.AddWithValue("@amount", txtAmount.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm1.ExecuteNonQuery()
                            MsgBox("New tuition fee has been successfully recorded!", MsgBoxStyle.Information)
                            Load_Tuition_Fees()
                            Disable_Controls()
                            ClearControls()
                        End Using
                    Else
                        MsgBox("Education Level: " & cmbEducationLevel.Text & " Year: " & cmbYear.Text & " is already setted!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf Saving_Status = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_TUITION_FEE WHERE EDUCATION_LEVEL = @education_level AND COURSE_CODE = @course_code AND YRLVL = @yrlvl AND ACADEMIC_YEAR = @ay AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", ID)
                    comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                    comm.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                    comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_SETTINGS_TUITION_FEE SET EDUCATION_LEVEL = @education_level, COURSE_CODE = @course_code, YRLVL = @yrlvl, AMOUNT = @amount WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", ID)
                            comm1.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                            comm1.Parameters.AddWithValue("@course_code", cmbCourseCode.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                            comm1.Parameters.AddWithValue("@amount", txtAmount.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.ExecuteNonQuery()
                            MsgBox("Tuition fee has been successfully updated!", MsgBoxStyle.Information)
                            Load_Tuition_Fees()
                            Disable_Controls()
                            ClearControls()
                        End Using
                    Else
                        MsgBox("Education Level: " & cmbEducationLevel.Text & " Year: " & cmbYear.Text & " is already setted!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If

        End Using
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Saving_Status = "NEW"
        Enable_Controls()
        ClearControls()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 6 Then
            Saving_Status = "EDIT"
            ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            cmbEducationLevel.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            cmbCourseCode.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            cmbYear.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            txtAmount.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            Enable_Controls()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Disable_Controls()
        ClearControls()
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter

    End Sub
End Class