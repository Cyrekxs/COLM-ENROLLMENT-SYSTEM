Public Class frm_settings_tuition_miscellaneous_entry
    Public TuitionFeeID As Integer = 0
    Public Yrlvl As String = String.Empty
    Public Strand As String = String.Empty
    Public SavingStatus As New SavingOptions
    Public FormCaller As New FormCallerOptions
    Public Enum FormCallerOptions
        [JHS]
        [SHS]
    End Enum

    Private Function IsValid() As Boolean
        Dim Result As Boolean = False
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = SavingOptions.NEW Then
                Using comm As New SqlCommand("SELECT * FROM tbl_settings_tuition_fee WHERE Course_Code = @Course_Code AND Yrlvl = @Yrlvl AND Academic_Year = @ay AND Academic_Sem = @sem", conn)
                    comm.Parameters.AddWithValue("@Course_Code", cmbStrand.Text)
                    comm.Parameters.AddWithValue("@Yrlvl", cmbYrLvl.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        If reader.HasRows = True Then
                            Result = False
                        Else
                            Result = True
                        End If
                    End Using
                End Using
                If SavingStatus = SavingOptions.EDIT Then
                    Using comm As New SqlCommand("SELECT * FROM tbl_settings_tuition_fee WHERE Course_Code = @Course_Code AND Yrlvl = @Yrlvl AND Academic_Year = @ay AND Academic_Sem = @sem AND ID <> @TuitionFeeID", conn)
                        comm.Parameters.AddWithValue("@TuitionFeeID", TuitionFeeID)
                        comm.Parameters.AddWithValue("@Course_Code", cmbStrand.Text)
                        comm.Parameters.AddWithValue("@Yrlvl", cmbYrLvl.Text)
                        comm.Parameters.AddWithValue("@ay", Academic_Year)
                        comm.Parameters.AddWithValue("@sem", Academic_Sem)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            If reader.HasRows = True Then
                                Result = False
                            Else
                                Result = True
                            End If
                        End Using
                    End Using
                End If
            End If
        End Using
        Return Result
    End Function

    Private Sub LoadCourses()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_senior_courses WHERE Academic_Yr = @ay", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbStrand.Items.Clear()
                    cmbStrand.Items.Add("JUNIOR HIGH")
                    While reader.Read
                        cmbStrand.Items.Add(reader("Course_Code").ToString.ToUpper)
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadYrLevels()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_college_yearlevels WHERE Education_Level = @EducationLevel", conn)
                comm.Parameters.AddWithValue("@EducationLevel", cmbEducationLevel.Text)
                comm.Parameters.AddWithValue("@CourseCode", cmbStrand.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmbYrLvl.Items.Clear()
                    While reader.Read
                        cmbYrLvl.Items.Add(reader("Year_Code"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadDefaultMFee()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_default_miscellaneous WHERE EducationLevel = @education_level", conn)
                comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(0, reader("FeeCode"), Convert_To_Currency(reader("FeeAmount")))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadSavedMFee()
        Dim TuitionFee As Double = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_tuition_fee WHERE Education_Level = @education_Level AND Course_Code = @Course_Code AND Yrlvl = @Yrlvl AND Academic_Year = @ay AND Academic_Sem = @sem AND ID = @TuitionFeeID", conn)
                comm.Parameters.AddWithValue("@TuitionFeeID", TuitionFeeID)
                comm.Parameters.AddWithValue("@education_level", cmbEducationLevel.Text)
                comm.Parameters.AddWithValue("@Course_Code", cmbStrand.Text)
                comm.Parameters.AddWithValue("@yrlvl", Yrlvl)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        TuitionFee = reader("Amount")
                    End While
                End Using
                txtTFee.Text = Convert_To_Currency(TuitionFee)
            End Using

            Using comm As New SqlCommand("SELECT * FROM tbl_settings_fees WHERE Education_Level = @education_Level AND Course_Code = @Course_Code AND Yrlvl = @Yrlvl AND Fee_Status =  'EXTERNAL' AND Academic_Yr = @ay AND Academic_Sem = @sem ORDER BY ID ASC", conn)
                comm.Parameters.AddWithValue("@education_level", "JUNIOR HIGH")
                comm.Parameters.AddWithValue("@Course_Code", cmbStrand.Text)
                comm.Parameters.AddWithValue("@yrlvl", Yrlvl)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_settings_tuition_miscellaneous_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCourses()
        'KAPAG NEW PWEDENG MAMILI NG YEAR LEVEL AT STRAND DEPENDE SA CALLER NA FORM
        If SavingStatus = SavingOptions.NEW Then
            LoadDefaultMFee()
            'KAPAG JUNIOR HIGH ANG ICCREATE DISABLE NA AGD SI EDUCATION LEVEL AT STRAND
            If FormCaller = FormCallerOptions.JHS Then
                cmbEducationLevel.Enabled = False
                cmbStrand.Enabled = False
                cmbEducationLevel.Text = "JUNIOR HIGH"
                cmbStrand.Text = "JUNIOR HIGH"
            ElseIf FormCaller = FormCallerOptions.SHS Then 'KAPAG SENIOR HIGH ANG ICCREATE THEN ENABLE SI STRAND
                cmbEducationLevel.Enabled = False
                cmbStrand.Enabled = True
                cmbEducationLevel.Text = "SENIOR HIGH"
            End If
        ElseIf SavingStatus = SavingOptions.EDIT Then 'KAPAG EDIT BAWAL NG LAHAT NG NSA SUMMARY KUNDI AMOUNT NLNG ANG PPWEDE
            LoadSavedMFee()

            If FormCaller = FormCallerOptions.JHS Then
                cmbEducationLevel.Enabled = False
                cmbStrand.Enabled = False
                cmbYrLvl.Enabled = False
                cmbEducationLevel.Text = "JUNIOR HIGH"
                cmbStrand.Text = "JUNIOR HIGH"
                cmbYrLvl.Text = Yrlvl
            ElseIf FormCaller = FormCallerOptions.SHS Then
                cmbEducationLevel.Enabled = False
                cmbStrand.Enabled = False
                cmbYrLvl.Enabled = False
                cmbEducationLevel.Text = "JUNIOR HIGH"
                cmbStrand.Text = Strand
                cmbYrLvl.Text = Yrlvl
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    If IsValid() = True Then
                        If TuitionFeeID = 0 Then
                            'SAVE DATA IF THERE WAS NO CURRENTLY SETTED TUITION FEE
                            Using comm As New SqlCommand("INSERT INTO tbl_settings_tuition_fee VALUES (@EducationLevel,@Course_Code,@Yrlvl,@Amount,@Academic_Yr,@Academic_Sem)", conn, t)
                                comm.Parameters.AddWithValue("@EducationLevel", cmbEducationLevel.Text)
                                comm.Parameters.AddWithValue("@Course_Code", cmbStrand.Text)
                                comm.Parameters.AddWithValue("@Yrlvl", cmbYrLvl.Text)
                                comm.Parameters.AddWithValue("@Amount", txtTFee.Text)
                                comm.Parameters.AddWithValue("@Academic_Yr", Academic_Year)
                                comm.Parameters.AddWithValue("@Academic_Sem", Academic_Sem)
                                comm.ExecuteNonQuery()
                            End Using
                        ElseIf TuitionFeeID <> 0 Then
                            'UPDATE TUITION FEE AMOUNT IF THERE WAS ALREADY SETTED TUITION FEE
                            Using comm As New SqlCommand("UPDATE tbl_settings_tuition_fee SET Amount = @Amount WHERE ID = @TuitionFeeID", conn, t)
                                comm.Parameters.AddWithValue("@Amount", txtTFee.Text)
                                comm.ExecuteNonQuery()
                            End Using
                        End If

                        For i = 0 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(i).Cells(0).Value = 0 Then
                                'INSERT MISCELLANEOUS IF THE MISCELLANEOUS IS NOT YET SAVED
                                Using comm As New SqlCommand("INSERT INTO tbl_settings_fees VALUES (@Course_Code,@Yrlvl,@Fee_Code,@Fee_Amount,@Fee_Type,@Fee_Status,@Education_Level,NULL,@ay,@sem)", conn, t)
                                    comm.Parameters.AddWithValue("@Course_Code", cmbStrand.Text)
                                    comm.Parameters.AddWithValue("@Yrlvl", cmbYrLvl.Text)
                                    comm.Parameters.AddWithValue("@Fee_Code", DataGridView1.Rows(i).Cells(1).Value)
                                    comm.Parameters.AddWithValue("@Fee_Amount", DataGridView1.Rows(i).Cells(2).Value)
                                    comm.Parameters.AddWithValue("@Fee_Type", "MISCELLANEOUS FEE")
                                    comm.Parameters.AddWithValue("@Fee_Status", "EXTERNAL")
                                    comm.Parameters.AddWithValue("@Education_Level", cmbEducationLevel.Text)
                                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                                    comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                                    comm.ExecuteNonQuery()
                                End Using
                            Else
                                'INSERT MISCELLANEOUS FEE CODE AND AMOUNT IF THE MISCELLANEOUS IS SAVED
                                Using comm As New SqlCommand("UPDATE tbl_settings_fees VALUES SET Fee_Code = @Fee_Code, Fee_Amount = @Fee_Amount WHERE ID = @FeeID", conn, t)
                                    comm.Parameters.AddWithValue("@FeeID", DataGridView1.Rows(i).Cells(0).Value)
                                    comm.Parameters.AddWithValue("@Fee_Amount", DataGridView1.Rows(i).Cells(1).Value)
                                    comm.Parameters.AddWithValue("@Fee_Amount", DataGridView1.Rows(i).Cells(2).Value)
                                    comm.ExecuteNonQuery()
                                End Using
                            End If
                        Next

                        t.Commit()
                        MsgBox("Tuition fee has been successfully saved!", MsgBoxStyle.Information)
                        Me.Close()
                        Me.Dispose()
                    Else
                        MsgBox("Tuition Settings in this Course/Strand, Year level along with academic year and academic sem is already exists!", MsgBoxStyle.Critical)
                    End If
                Catch ex As Exception
                    t.Rollback()
                    MsgBox("An error occured while processing information" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
                End Try
            End Using
        End Using
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim TotalMFee As Double = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            If IsNumeric(DataGridView1.Rows(i).Cells(2).Value) = True Then
                TotalMFee += CDbl(DataGridView1.Rows(i).Cells(2).Value)
            End If
        Next
        txtMFee.Text = Convert_To_Currency(TotalMFee)
        txtMiscellaneousCount.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub cmbStrand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStrand.SelectedIndexChanged
        LoadYrLevels()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmbEducationLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEducationLevel.SelectedIndexChanged
        LoadDefaultMFee()
    End Sub
End Class