Public Class frm_dummy

    Private Sub frm_dummy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using conn As New SqlConnection(StringConnection)
            Using t As SqlTransaction = conn.BeginTransaction
                Try

                    Using comm1 As New SqlCommand("", conn, t)
                        comm1.ExecuteNonQuery()
                    End Using

                    Using comm2 As New SqlCommand("", conn, t)
                        comm2.ExecuteNonQuery()
                    End Using
                    t.Commit()
                Catch ex As Exception
                    t.Rollback()
                End Try
            End Using
        End Using
    End Sub

End Class