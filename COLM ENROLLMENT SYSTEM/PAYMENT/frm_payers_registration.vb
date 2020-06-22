Public Class frm_payers_registration
    Public ID As Integer = 0
    Public SavingStatus As String = String.Empty
    Public PayerCode As String = String.Empty
    Public Sub GeneratePayerCode()
        Dim Count As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT COUNT (ID) FROM TBL_PAYERS_INFORMATION", conn)
                If Val(comm.ExecuteScalar) = 0 Then
                    PayerCode = "COLM0000001"
                Else
                    Using comm1 As New SqlCommand("SELECT INDENT_CURRENT('TBL_PAYERS_INFORMATION')", conn)
                        Count = Val(comm.ExecuteScalar) + 1
                    End Using

                    Select Case Count.ToString.Length
                        Case 1 : PayerCode = "COLM000000" & Count
                        Case 2 : PayerCode = "COLM00000" & Count
                        Case 3 : PayerCode = "COLM0000" & Count
                        Case 4 : PayerCode = "COLM000" & Count
                        Case 5 : PayerCode = "COLM00" & Count
                        Case 6 : PayerCode = "COLM0" & Count
                        Case Else
                            PayerCode = "COLM" & Count
                    End Select
                End If
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If SavingStatus = "NEW" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_PAYERS_INFORMATION WHERE PAYERNAME = @name", conn)
                    comm.Parameters.AddWithValue("@name", txtPayerName.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("INSERT INTO TBL_PAYERS_INFORMATION VALUES(@payercode,@payername,@payerdescription,GETDATE(),@registeredby)", conn)
                            comm1.Parameters.AddWithValue("@payercode", PayerCode)
                            comm1.Parameters.AddWithValue("@payername", txtPayerName.Text)
                            comm1.Parameters.AddWithValue("@payerdescription", txtPayerDescription.Text)
                            comm1.Parameters.AddWithValue("@registeredby", Account_Name)
                            If MsgBox("Are you sure you want save this payer information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                frm_payers_lists.LoadPayersLists()
                                MsgBox("New payer information has been successfully saved!", MsgBoxStyle.Information)
                                Me.Close()
                                Me.Dispose()
                            End If
                        End Using
                    Else
                        MsgBox("Payer Name: " & txtPayerName.Text & " is already exists!", MsgBoxStyle.Critical)
                    End If
                End Using
            ElseIf SavingStatus = "EDIT" Then
                Using comm As New SqlCommand("SELECT * FROM TBL_PAYERS_INFORMATION WHERE PAYERNAME = @name AND ID <> @id", conn)
                    comm.Parameters.AddWithValue("@id", ID)
                    comm.Parameters.AddWithValue("@name", txtPayerName.Text)
                    If Val(comm.ExecuteScalar) = 0 Then
                        Using comm1 As New SqlCommand("UPDATE TBL_PAYERS_INFORMATION SET PAYERNAME = @payername, PAYERDESCRIPTION = @payerdescription,REGISTRATIONDATE = GETDATE(), REGISTEREDBY = @registeredby WHERE ID = @id", conn)
                            comm1.Parameters.AddWithValue("@id", ID)
                            comm1.Parameters.AddWithValue("@payername", txtPayerName.Text)
                            comm1.Parameters.AddWithValue("@payerdescription", txtPayerDescription.Text)
                            comm1.Parameters.AddWithValue("@registeredby", Account_Name)
                            If MsgBox("Are you sure you want update this payer information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                comm1.ExecuteNonQuery()
                                MsgBox("Payer information has been successfully updated!", MsgBoxStyle.Information)
                                frm_payers_lists.LoadPayersLists()
                                Me.Close()
                                Me.Dispose()
                            End If
                        End Using
                    Else
                        MsgBox("Payer Name: " & txtPayerName.Text & " is already exists!", MsgBoxStyle.Critical)
                    End If
                End Using
            End If
        End Using
    End Sub

    Private Sub frm_payers_registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If SavingStatus = "NEW" Then
            GeneratePayerCode()
            txtPayerCode.Text = PayerCode
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class