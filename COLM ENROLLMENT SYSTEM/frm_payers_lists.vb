Public Class frm_payers_lists
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
                lblTotalResults.Text = "TOTAL RESULTS: " & DataGridView1.Rows.Count
            End Using
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_payers_registration
            .SavingStatus = "NEW"
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frm_payers_registration
            .SavingStatus = "EDIT"
            .ID = DataGridView1.Rows(DGRow).Cells(0).Value
            .txtPayerCode.Text = DataGridView1.Rows(DGRow).Cells(1).Value
            .txtPayerName.Text = DataGridView1.Rows(DGRow).Cells(2).Value
            .txtPayerDescription.Text = DataGridView1.Rows(DGRow).Cells(3).Value
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub frm_payers_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPayersLists()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadPayersLists()
        End If
    End Sub


End Class