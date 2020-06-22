Public Class frm_rfid_entry

    ' clear memory buffers
    Private Sub ClearBuffers()
        Dim indx As Long
        For indx = 0 To 262
            frm_rfid_registered_lists.RecvBuff(indx) = 0
            frm_rfid_registered_lists.SendBuff(indx) = 0
        Next
    End Sub

    ' send application protocol data unit : communication unit between a smart card reader and a smart card
    Private Function SendAPDUandDisplay(reqType As Integer) As Integer
        Dim indx As Integer
        Dim tmpStr As String = ""

        frm_rfid_registered_lists.pioSendRequest.dwProtocol = frm_rfid_registered_lists.Aprotocol
        frm_rfid_registered_lists.pioSendRequest.cbPciLength = 8

        'Display Apdu In
        For indx = 0 To frm_rfid_registered_lists.SendLen - 1
            tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", frm_rfid_registered_lists.SendBuff(indx))
        Next

        frm_rfid_registered_lists.retCode = Card.SCardTransmit(frm_rfid_registered_lists.hCard,
                                                               frm_rfid_registered_lists.pioSendRequest,
                                                               frm_rfid_registered_lists.SendBuff(0),
                                                               frm_rfid_registered_lists.SendLen,
                                                               frm_rfid_registered_lists.pioSendRequest,
                                                               frm_rfid_registered_lists.RecvBuff(0),
                                                               frm_rfid_registered_lists.RecvLen)

        If frm_rfid_registered_lists.retCode <> Card.SCARD_S_SUCCESS Then
            Return frm_rfid_registered_lists.retCode
        Else

            Try
                tmpStr = ""
                Select Case reqType
                    Case 0
                        For indx = (frm_rfid_registered_lists.RecvLen - 2) To (frm_rfid_registered_lists.RecvLen - 1)
                            tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", frm_rfid_registered_lists.RecvBuff(indx))
                        Next

                        If (tmpStr).Trim() <> "90 00" Then
                            'MessageBox.Show("Return bytes are not acceptable.");
                            Return -202
                        End If

                        Exit Select

                    Case 1

                        For indx = (frm_rfid_registered_lists.RecvLen - 2) To (frm_rfid_registered_lists.RecvLen - 1)
                            tmpStr = tmpStr & String.Format("{0:X2}", frm_rfid_registered_lists.RecvBuff(indx))
                        Next

                        If tmpStr.Trim() <> "90 00" Then
                            tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", frm_rfid_registered_lists.RecvBuff(indx))
                        Else

                            tmpStr = "ATR : "
                            For indx = 0 To (frm_rfid_registered_lists.RecvLen - 3)
                                tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", frm_rfid_registered_lists.RecvBuff(indx))
                            Next
                        End If

                        Exit Select

                    Case 2

                        For indx = 0 To (frm_rfid_registered_lists.RecvLen - 1)
                            tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", frm_rfid_registered_lists.RecvBuff(indx))
                        Next

                        Exit Select
                End Select
            Catch generatedExceptionName As IndexOutOfRangeException
                Return -200
            End Try
        End If
        Return frm_rfid_registered_lists.retCode
    End Function

    ' block authentication
    Private Function authenticateBlock(block As [String]) As Boolean
        ClearBuffers()
        frm_rfid_registered_lists.SendBuff(0) = &HFF
        ' CLA
        frm_rfid_registered_lists.SendBuff(2) = &H0
        ' P1: same for all source types 
        frm_rfid_registered_lists.SendBuff(1) = &H86
        ' INS: for stored key input
        frm_rfid_registered_lists.SendBuff(3) = &H0
        ' P2 : Memory location;  P2: for stored key input
        frm_rfid_registered_lists.SendBuff(4) = &H5
        ' P3: for stored key input
        frm_rfid_registered_lists.SendBuff(5) = &H1
        ' Byte 1: version number
        frm_rfid_registered_lists.SendBuff(6) = &H0
        ' Byte 2
        frm_rfid_registered_lists.SendBuff(7) = CByte(Integer.Parse(block))
        ' Byte 3: sectore no. for stored key input
        frm_rfid_registered_lists.SendBuff(8) = &H60
        ' Byte 4 : Key A for stored key input
        frm_rfid_registered_lists.SendBuff(9) = CByte(Integer.Parse("1"))
        ' Byte 5 : Session key for non-volatile memory
        frm_rfid_registered_lists.SendLen = &HA
        frm_rfid_registered_lists.RecvLen = &H2

        frm_rfid_registered_lists.retCode = SendAPDUandDisplay(0)

        If frm_rfid_registered_lists.retCode <> Card.SCARD_S_SUCCESS Then
            'MessageBox.Show("FAIL Authentication!");
            Return False
        End If
        Return True
    End Function

    'WRITING DATA INTO CARD
    Public Function submitText(Text As [String], Block As [String]) As Boolean
        Dim result As Boolean = False
        Dim tmpStr As [String] = Text
        Dim indx As Integer
        If authenticateBlock(Block) Then
            ClearBuffers()
            frm_rfid_registered_lists.SendBuff(0) = &HFF
            ' CLA
            frm_rfid_registered_lists.SendBuff(1) = &HD6
            ' INS
            frm_rfid_registered_lists.SendBuff(2) = &H0
            ' P1
            frm_rfid_registered_lists.SendBuff(3) = CByte(Integer.Parse(Block))
            ' P2 : Starting Block No.
            frm_rfid_registered_lists.SendBuff(4) = CByte(Integer.Parse("16"))
            ' P3 : Data length
            For indx = 0 To (tmpStr).Length - 1
                frm_rfid_registered_lists.SendBuff(indx + 5) = CByte(AscW(tmpStr(indx)))
            Next
            frm_rfid_registered_lists.SendLen = frm_rfid_registered_lists.SendBuff(4) + 5
            frm_rfid_registered_lists.RecvLen = &H2

            frm_rfid_registered_lists.retCode = SendAPDUandDisplay(2)

            If frm_rfid_registered_lists.retCode <> Card.SCARD_S_SUCCESS Then
                result = False
            Else
                result = True
            End If
        Else
            MessageBox.Show("FailAuthentication")
            result = False
        End If
        Return result
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If frm_rfid_registered_lists.connectCard() Then            ' establish connection to the card: you've declared this from previous post
            Dim CardUID As String = frm_rfid_registered_lists.getcardUID()

            'STUDENT NO
            If submitText(txtStudentNo.Text, "1") = False Then
                Exit Sub
            End If

            'EDUCATION LEVEL
            If submitText(txtEducationLevel.Text, "2") = False Then
                Exit Sub
            End If

            'STUDENT NAME

            'If the name length is less than 16 write only into block 4
            If submitText(txtName.Text, "4") = False Then
                Exit Sub
            End If
            If submitText(Mid(txtName.Text, 17, 16), "5") = False Then
                Exit Sub
            End If

            If submitText(Mid(txtName.Text, 33, 16), "6") = False Then
                Exit Sub
            End If

            'COURSE
            If submitText(txtCourse.Text, "8") = False Then
                Exit Sub
            End If
            'YEAR
            If submitText(txtYear.Text, "9") = False Then
                Exit Sub
            End If

            If submitText(txtSection.Text, "10") = False Then
                Exit Sub
            End If

            'GUARDIAN NAME
            If submitText(Mid(txtGuardian.Text, 1, 16), "12") = False Then
                Exit Sub
            End If

            If submitText(Mid(txtGuardian.Text, 17, 16), "13") = False Then
                Exit Sub
            End If

            If submitText(Mid(txtGuardian.Text, 33, 16), "14") = False Then
                Exit Sub
            End If

            'GUARDIAN RELATION
            If submitText(txtRelation.Text, "16") = False Then
                Exit Sub
            End If

            'GUARDIAN MOBILE
            If submitText(txtMobile.Text, "17") = False Then
                Exit Sub
            End If

            If submitText("ACTIVE", "21") = False Then
                Exit Sub
            End If

            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("INSERT INTO tbl_student_information_card (CardUID,StudentNumber,Academic_Yr,Academic_Sem,CardStatus,RegistrationDate) VALUES (@carduid,@sn,@ay,@sem,@status,@registration_date)", conn)
                    comm.Parameters.AddWithValue("@carduid", CardUID)
                    comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    comm.Parameters.AddWithValue("@status", "ACTIVE")
                    comm.Parameters.AddWithValue("@registration_date", Date.Now)
                    If comm.ExecuteNonQuery() > 0 Then
                        MsgBox("Information has been successfully written to the card and saved into the database!", MsgBoxStyle.Information)
                    End If
                End Using
            End Using
            ' 5 - is the block we are writing data on the card
        End If
    End Sub
End Class