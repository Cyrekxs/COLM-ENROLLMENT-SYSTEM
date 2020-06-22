Public Class frm_registration_highschool_edit
    Public RegID As Integer = 0
    Public StudentNumber As String = String.Empty
    Public StudentName As String = String.Empty
    Public Course As String = String.Empty
    Public YrLvl As String = String.Empty
    Public SectionCode As String = String.Empty
    Public DGRow As Integer = 0

    Private Sub frm_registration_highschool_edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_YearLvls("JUNIOR HIGH", "JUNIOR HIGH", cmbYear)
        txtStudentNumber.Text = StudentNumber
        txtStudentName.Text = StudentName
        cmbYear.Text = YrLvl
        cmbSection.Text = SectionCode
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        Load_Sections("JUNIOR HIGH", "JUNIOR HIGH", cmbYear.Text, cmbSection)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If cmbYear.Text = String.Empty Then
            MsgBox("Please select Year Level!", MsgBoxStyle.Critical)
            Exit Sub
        End If


        If cmbSection.Text = String.Empty Then
            MsgBox("Please select Section!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    If MsgBox("Are you sure you want to update this Student Registration?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        Using comm As New SqlCommand("UPDATE TBL_STUDENT_REGISTERED SET YrLvl = @yrlvl, SectionCode = @Section_Code WHERE ROWID = @RegID", conn, t)
                            comm.Parameters.AddWithValue("@RegID", RegID)
                            comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                            comm.Parameters.AddWithValue("@section_code", cmbSection.Text)
                            comm.ExecuteNonQuery()
                            With frm_registered_highschool_students.DataGridView1.Rows(DGRow)
                                .Cells(3).Value = cmbYear.Text
                                .Cells(4).Value = cmbSection.Text
                            End With
                        End Using

                        Using comm As New SqlCommand("UPDATE tbl_college_assessment_summary SET Yrlvl = @yrlvl, Sect_Code = @Section_Code WHERE Student_Number = @sn AND Academic_Yr = @ay", conn, t)
                            comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                            comm.Parameters.AddWithValue("@yrlvl", cmbYear.Text)
                            comm.Parameters.AddWithValue("@section_code", cmbSection.Text)
                            comm.Parameters.AddWithValue("@ay", Academic_Year)
                            comm.ExecuteNonQuery()
                        End Using

                        t.Commit()
                        MsgBox("Registration has been successfully updated!", MsgBoxStyle.Information)
                        Me.Close()
                        Me.Dispose()
                    End If
                Catch ex As Exception
                    t.Rollback()
                    MsgBox("An error occured while processing information", MsgBoxStyle.Critical)
                End Try
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class