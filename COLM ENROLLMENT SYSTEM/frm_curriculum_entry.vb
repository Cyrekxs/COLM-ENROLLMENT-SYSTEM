Public Class frm_curriculum_entry
    Public ID As String = String.Empty
    Public Saving_Status As String = String.Empty

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If txtChedCode.Text = String.Empty Then
            MsgBox("Please enter ched code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtSubjCode.Text = String.Empty Then
            MsgBox("Please enter subject code!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtSubjDesc.Text = String.Empty Then
            MsgBox("Please enter subject description!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtUnit.Text = String.Empty Then
            MsgBox("Please enter subject unit!", MsgBoxStyle.Critical)
        End If

        If txtLecHours.Text = String.Empty Then
            MsgBox("Please enter lecture hours!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If txtLabHours.Text = String.Empty Then
            MsgBox("Please enter laboratory hours!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If IsNumeric(txtAmount.Text) = False Then
            MsgBox("Please enter a correct amount!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Saving_Status = "NEW" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT SUBJ_CODE FROM TBL_COLLEGE_CURRICULUM WHERE SUBJ_CODE = @subj_code AND COURSE_CODE = @course_code AND CURRICULUM_STATUS <> 'DELETED'", conn)
                    comm.Parameters.AddWithValue("@subj_code", txtSubjCode.Text)
                    comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_COLLEGE_CURRICULUM VALUES(@course_code,@yrlvl,@subj_code,@ched_code,@subj_desc,@lec_hours,@lab_hours,@unit,@subj_price,@ay,@sem,'OPEN')", conn)
                            comm1.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                            comm1.Parameters.AddWithValue("@subj_code", txtSubjCode.Text)
                            comm1.Parameters.AddWithValue("@ched_code", txtChedCode.Text)
                            comm1.Parameters.AddWithValue("@subj_desc", txtSubjDesc.Text)
                            comm1.Parameters.AddWithValue("@lec_hours", txtLecHours.Text)
                            comm1.Parameters.AddWithValue("@lab_hours", txtLabHours.Text)
                            comm1.Parameters.AddWithValue("@unit", txtUnit.Text)
                            comm1.Parameters.AddWithValue("@subj_price", txtAmount.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm1.ExecuteNonQuery()
                            MsgBox("Successfully Add!", MsgBoxStyle.Information)
                            Me.Close()
                            Me.Dispose()
                        End Using
                    Else
                        MsgBox("Subject code is already in used!", MsgBoxStyle.Critical)
                    End If
                End Using
            End Using
        ElseIf Saving_Status = "EDIT" Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_CURRICULUM WHERE SUBJ_CODE = @subj_code AND COURSE_CODE = @course_code AND CURRICULUM_STATUS <> 'DELETED' AND CURRICULUM_ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", ID)
                    comm.Parameters.AddWithValue("@subj_code", txtSubjCode.Text)
                    comm.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_COLLEGE_CURRICULUM SET SUBJ_CODE = @subj_code,CHED_CODE = @ched_code,SUBJ_DESC = @subj_desc,SUBJ_UNIT = @subj_unit,LEC_HOURS = @lec_hours,LAB_HOURS = @lab_hours,SUBJ_PRICE = @subj_price,ACADEMIC_SEM = @sem,COURSE_CODE = @course_code,YRLVL = @yrlvl WHERE CURRICULUM_ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", ID)
                            comm1.Parameters.AddWithValue("@course_code", cmbCourse.Text)
                            comm1.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                            comm1.Parameters.AddWithValue("@subj_code", txtSubjCode.Text)
                            comm1.Parameters.AddWithValue("@ched_code", txtChedCode.Text)
                            comm1.Parameters.AddWithValue("@subj_desc", txtSubjDesc.Text)
                            comm1.Parameters.AddWithValue("@lec_hours", txtLecHours.Text)
                            comm1.Parameters.AddWithValue("@lab_hours", txtLabHours.Text)
                            comm1.Parameters.AddWithValue("@subj_unit", txtUnit.Text)
                            comm1.Parameters.AddWithValue("@subj_price", txtAmount.Text)
                            comm1.Parameters.AddWithValue("@ay", Academic_Year)
                            comm1.Parameters.AddWithValue("@sem", Academic_Sem)
                            comm1.ExecuteNonQuery()
                            MsgBox("Successfully Updated!", MsgBoxStyle.Information)
                            Me.Close()
                            Me.Dispose()
                        End Using
                    Else
                        MsgBox("Subject code is already in used!", MsgBoxStyle.Critical)
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub txtUnit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUnit.KeyPress
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtLabHours_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLabHours.KeyPress
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtLecHours_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLecHours.KeyPress
        NumbersOnly(sender, e)
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        NumbersOnlyWithDecimal(sender, e)
    End Sub
End Class