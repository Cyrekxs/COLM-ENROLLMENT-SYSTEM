Public Class frm_nonstudent_charge_fee
    Private _StudentID As Integer = 0
    Public AccountType As New AccountTypes

    Private DGRow1 As Integer = 0
    Private DGRow2 As Integer = 0

    Enum AccountTypes
        [NON_STUDENT]
        [MEDICAL_ARTS]
    End Enum


    Public Sub GenerateReferenceCode()
        Dim LatestFeeID As Integer
        Dim GeneratedCode As String = "REF " & LatestFeeID

        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT TOP 1 AccountFeeID AS LatestFeeID FROM tbl_nonstudent_account ORDER BY AccountFeeID DESC", conn)
                LatestFeeID = comm.ExecuteScalar
            End Using

            Dim i As Integer = 1


            Dim IsRefCodeExists As Boolean = True

            While IsRefCodeExists = True
                LatestFeeID += i
                GeneratedCode = "REF " & LatestFeeID
                Using comm As New SqlCommand("SELECT * FROM tbl_nonstudent_account WHERE RefCode = @RefCode", conn)
                    comm.Parameters.AddWithValue("@RefCode", GeneratedCode)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        If reader.HasRows = True Then
                            IsRefCodeExists = True
                            i += 1
                        Else
                            IsRefCodeExists = False
                        End If
                    End Using
                End Using
            End While
        End Using

        txtRefCode.Text = GeneratedCode
    End Sub
    Private Sub Load_InternalFees()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_settings_fees WHERE Education_Level = @AccountType AND Fee_Code LIKE @search AND Fee_Status = 'INTERNAL'", conn)
                If AccountType = AccountTypes.NON_STUDENT Then
                    comm.Parameters.AddWithValue("@AccountType", "NON STUDENT")
                ElseIf AccountType = AccountTypes.MEDICAL_ARTS Then
                    comm.Parameters.AddWithValue("@AccountType", "MEDICAL ARTS")
                End If
                comm.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")))
                    End While
                End Using
            End Using
        End Using
    End Sub
    Private Sub frm_nonstudent_charge_fee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_InternalFees()
        GenerateReferenceCode()
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow1 = e.RowIndex
    End Sub

    Private Sub DataGridView2_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.RowEnter
        DGRow2 = e.RowIndex
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim IsFeeIDExists As Boolean = False

        For i = 0 To DataGridView2.Rows.Count - 1
            If DataGridView1.Rows(DGRow1).Cells(0).Value = DataGridView2.Rows(i).Cells(0).Value Then
                IsFeeIDExists = True
                Exit For
            End If
        Next

        If IsFeeIDExists = False Then
            DataGridView2.Rows.Add(DataGridView1.Rows(DGRow1).Cells(0).Value,
                                   DataGridView1.Rows(DGRow1).Cells(1).Value,
                                   DataGridView1.Rows(DGRow1).Cells(2).Value,
                                   1)
        Else
            MsgBox("Already in the list!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView2.Rows.Count > 0 Then
            DataGridView2.Rows.Remove(DataGridView2.Rows(DGRow2))
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim QTY As Integer = 0
        For i = 0 To DataGridView2.Rows.Count - 1
            If IsNumeric(DataGridView2.Rows(i).Cells(3).Value) = True And IsNumeric(DataGridView2.Rows(i).Cells(2).Value) = True Then
                Dim Amount As Double = DataGridView2.Rows(i).Cells(2).Value
                QTY = DataGridView2.Rows(i).Cells(3).Value
                Dim Total As Double = Amount * QTY
                DataGridView2.Rows(i).Cells(4).Value = Convert_To_Currency(Total)
            End If
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GenerateReferenceCode()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using t As SqlTransaction = conn.BeginTransaction
                Try
                    For i = 0 To DataGridView2.Rows.Count - 1
                        Using comm As New SqlCommand("INSERT INTO tbl_nonstudent_account VALUES (@NonStudentID,@FeeID,@FeeCode,@RefCode,@Amount,@Quantity,@TotalAmount,GETDATE())", conn, t)
                            comm.Parameters.AddWithValue("@NonStudentID", _StudentID)
                            comm.Parameters.AddWithValue("@FeeID", DataGridView2.Rows(i).Cells(0).Value)
                            comm.Parameters.AddWithValue("@FeeCode", DataGridView2.Rows(i).Cells(1).Value)
                            comm.Parameters.AddWithValue("@RefCode", txtRefCode.Text)
                            comm.Parameters.AddWithValue("@Amount", DataGridView2.Rows(i).Cells(2).Value)
                            comm.Parameters.AddWithValue("@Quantity", DataGridView2.Rows(i).Cells(3).Value)
                            comm.Parameters.AddWithValue("@TotalAmount", DataGridView2.Rows(i).Cells(4).Value)
                            comm.ExecuteNonQuery()
                        End Using
                    Next
                    t.Commit()
                    MsgBox("Transaction has been successfully completed!", MsgBoxStyle.Information)
                    Me.Close()
                    Me.Dispose()
                Catch ex As Exception
                    t.Rollback()
                    MsgBox("An error occured while processing transction please try again!", MsgBoxStyle.Critical)
                    Me.Close()
                    Me.Dispose()
                End Try
            End Using
        End Using
    End Sub

    Public Sub New(StudentID As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _StudentID = StudentID
    End Sub
End Class