Public Class frm_college_assessment_browse_fees
    Public Education_Level As String = String.Empty
    Public Course As String = String.Empty
    Public Yrlvl As String = String.Empty
    Public Fee_Type As String = String.Empty

    Public Sub Load_Fees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim Query As String = String.Empty
            If Education_Level = "COLLEGE" Then
                Query = "SELECT DISTINCT FEE_CODE,FEE_AMOUNT FROM TBL_SETTINGS_FEES WHERE FEE_TYPE = @fee_Type AND COURSE_CODE = @course_code AND EDUCATION_LEVEL = @education_level AND FEE_STATUS = 'EXTERNAL' AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem AND FEE_CODE LIKE @search ORDER BY FEE_CODE ASC"
            ElseIf Education_Level = "HIGH SCHOOL" Then
                Query = "SELECT * FROM TBL_SETTINGS_FEES WHERE FEE_TYPE = @fee_Type AND COURSE_CODE = @course_code AND EDUCATION_LEVEL = @education_level AND FEE_STATUS = 'EXTERNAL' AND ACADEMIC_YR = @ay AND FEE_CODE LIKE @search"
            End If
            Using comm As New SqlCommand(Query, conn)
                comm.Parameters.AddWithValue("@fee_type", Fee_Type)
                comm.Parameters.AddWithValue("@course_code", Course)
                comm.Parameters.AddWithValue("@yrlvl", Yrlvl)
                comm.Parameters.AddWithValue("@education_level", Education_Level)
                comm.Parameters.AddWithValue("@search", "%" & StripSpaces(TextBox1.Text) & "%")
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    DG_FEE.Rows.Clear()
                    While reader.Read
                        If CDbl(reader("FEE_AMOUNT")) > 0 Then
                            DG_FEE.Rows.Add(reader("FEE_CODE").ToString.ToUpper, Convert_To_Currency(reader("FEE_AMOUNT")), "ADD TO LIST")
                        End If
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Fees()
        End If
    End Sub

    Private Sub DG_MFEE_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_FEE.CellContentClick
        If e.ColumnIndex = 2 Then
            With frm_college_assessment
                Select Case Fee_Type
                    Case "MISCELLANEOUS FEE"
                        Dim Can_Add As Boolean = True
                        For i = 0 To .DG_MFEE.Rows.Count - 1
                            If DG_FEE.Rows(e.RowIndex).Cells(0).Value.ToString.ToUpper = .DG_MFEE.Rows(i).Cells(1).Value.ToString.ToUpper Then
                                Can_Add = False
                                MsgBox("Fee is already exist!", MsgBoxStyle.Critical)
                                Exit For
                                Exit Sub
                            End If
                        Next

                        If Can_Add = True Then
                            .DG_MFEE.Rows.Add("", DG_FEE.Rows(e.RowIndex).Cells(0).Value, DG_FEE.Rows(e.RowIndex).Cells(1).Value, "REMOVE")
                        End If
                    Case "OTHER FEE"
                        Dim Can_Add As Boolean = True
                        Dim Can_AddAffilationFee As Boolean = False
                        For i = 0 To .DG_TFEE.Rows.Count - 1
                            If .DG_TFEE.Rows(i).Cells(1).Value = "CE 1" Or .DG_TFEE.Rows(i).Cells(1).Value = "CE 2" Then
                                Can_AddAffilationFee = True
                                Exit For
                            End If
                        Next

                        For i = 0 To .DG_OFEE.Rows.Count - 1
                            If DG_FEE.Rows(e.RowIndex).Cells(0).Value.ToString.ToUpper = .DG_OFEE.Rows(i).Cells(1).Value.ToString.ToUpper Then
                                Can_Add = False
                                Exit For
                                Exit Sub
                            End If
                        Next

                        If Can_Add = True Then
                            'VERIFY IF THE AFFILATION FEE IS ALLOWED TO ADD
                            If DG_FEE.Rows(e.RowIndex).Cells(0).Value.ToString.ToUpper = "AFFILIATION FEE & PERCENTER'S FEE" Then
                                If Can_AddAffilationFee = True Then
                                    .DG_OFEE.Rows.Add("", DG_FEE.Rows(e.RowIndex).Cells(0).Value, DG_FEE.Rows(e.RowIndex).Cells(1).Value, "REMOVE")
                                Else
                                    MsgBox("AFFILIATION FEE & PERCENTER'S FEE is not allowed to add because it student has no CE 1 or CE 2", MsgBoxStyle.Critical)
                                    Exit Sub
                                End If
                            Else
                                .DG_OFEE.Rows.Add("", DG_FEE.Rows(e.RowIndex).Cells(0).Value, DG_FEE.Rows(e.RowIndex).Cells(1).Value, "REMOVE")
                            End If
                        Else
                            MsgBox("Fee is already exist!", MsgBoxStyle.Critical)
                        End If
                End Select
            End With
        End If
    End Sub

    Private Sub frm_college_assessment_browse_fees_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub
End Class