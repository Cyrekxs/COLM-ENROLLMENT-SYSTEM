Public Class frm_college_assessment_old_assessment
    Public sn As String = String.Empty

    Public Sub LoadStudentAssessmentAcademicYearSem()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT DISTINCT Academic_Yr,Academic_Sem FROM FN_College_AssessedStudents() WHERE Education_Level = 'COLLEGE' AND Student_Number = @sn", conn)
                comm.Parameters.AddWithValue("@sn", sn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        cmbAcademicYrSem.Items.Add(reader("Academic_Yr") & "|" & reader("Academic_Sem"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub cmbAcademicYrSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAcademicYrSem.SelectedIndexChanged
        Dim aysem As String() = cmbAcademicYrSem.Text.Split(New Char() {"|"c})
        txtAcademic_Yr.Text = aysem(0)
        txtAcademic_Sem.Text = aysem(1)

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM FN_College_AssessedStudents() WHERE Education_Level = 'COLLEGE' AND Student_Number = @sn AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@sn", sn)
                comm.Parameters.AddWithValue("@ay", txtAcademic_Yr.Text)
                comm.Parameters.AddWithValue("@sem", txtAcademic_Sem.Text)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        txtAssessmentType.Text = reader("Assessment_Type")
                        txtDiscountCode.Text = reader("Discount_Code")
                        txtDiscountAmount.Text = Convert_To_Currency(reader("Discount_Amount"))
                        If IsDBNull(reader("Voucher_Code")) = True Then
                            txtYesToCollege.Text = "NONE"
                            txtYesToCollegeAmount.Text = Convert_To_Currency(0)

                        Else
                            txtYesToCollege.Text = reader("Voucher_Code")
                            txtYesToCollegeAmount.Text = Convert_To_Currency(reader("Voucher_Amount"))
                        End If
                        txtOldBalance.Text = Convert_To_Currency(reader("Old_Balance"))
                        txtTotalTuitionFee.Text = Convert_To_Currency(reader("TFee"))
                        txtTotalMiscellaneousFee.Text = Convert_To_Currency(CDbl(reader("OFee")) + CDbl(reader("MFee")))
                        txtTotalSurcharge.Text = Convert_To_Currency(reader("Surcharge"))
                        txtTotalAmountDue.Text = Convert_To_Currency(reader("Total"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub frm_college_assessment_old_assessment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudentAssessmentAcademicYearSem()
    End Sub
End Class