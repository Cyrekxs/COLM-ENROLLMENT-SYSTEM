Public Class frm_college_assessment_browser_mfee
    Public CurriculumID As Integer = 0
    Private Sub LoadFees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_fees WHERE CurriculumID = @CurriculumID AND Fee_Status = 'EXTERNAL' AND Fee_Type = 'MISCELLANEOUS FEE' AND Yrlvl = @Yrlvl AND Academic_Yr = @ay AND Academic_Sem = @sem", conn)
                comm.Parameters.AddWithValue("@CurriculumID", CurriculumID)
                comm.Parameters.AddWithValue("@Yrlvl", txtYearLevel.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DGSched.Rows.Clear()
                    While reader.Read
                        DGSched.Rows.Add(reader("ID"), reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_college_assessment_browser_mfee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFees()
    End Sub

    Private Sub DGSched_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGSched.CellContentClick
        If e.ColumnIndex = 3 Then
            With frm_college_assessment_entry.DGMFee

                Dim isExists As Boolean = False

                For i = 0 To .Rows.Count - 1
                    If DGSched.Rows(e.RowIndex).Cells(0).Value = .Rows(i).Cells(0).Value Then
                        isExists = True
                        Exit For
                    End If
                Next

                If isExists = False Then
                    .Rows.Add(DGSched.Rows(e.RowIndex).Cells(0).Value, DGSched.Rows(e.RowIndex).Cells(1).Value, DGSched.Rows(e.RowIndex).Cells(2).Value)
                Else
                    MsgBox("Fee is already exists!", MsgBoxStyle.Critical)
                End If
            End With
        End If
    End Sub
End Class