Public Class frm_payment_non_student_browse_payer
    Public DGRow As Integer = 0
    Public Sub LoadPayersLists()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_PAYERS_INFORMATION WHERE PAYERNAME LIKE @search ORDER BY PAYERNAME ASC", conn)
                comm.Parameters.AddWithValue("@search", TextBox1.Text & "%")
                Using reader As SqlDataReader = comm.ExecuteReader
                    DataGridView1.Rows.Clear()
                    While reader.Read
                        DataGridView1.Rows.Add(reader("ID"), reader("PAYERCODE"), reader("PAYERNAME"), reader("PAYERDESCRIPTION"), Format(CDate(reader("REGISTRATIONDATE")), "MM-dd-yyyy"), reader("REGISTEREDBY"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            With frm_payment_non_student
                .txtPayerCode.Text = DataGridView1.Rows(DGRow).Cells(1).Value
                .txtPayerName.Text = DataGridView1.Rows(DGRow).Cells(2).Value
                '.txtPayerDescription.Text = DataGridView1.Rows(DGRow).Cells(3).Value
                Me.Close()
                Me.Dispose()
            End With
        End If
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        DGRow = e.RowIndex
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadPayersLists()
        End If
    End Sub

    Private Sub frm_payment_non_student_browse_payer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPayersLists()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadPayersLists()
    End Sub
End Class