Public Class frm_rfid_registered_lists
    Public retCode As Integer
    Public hCard As Integer
    Public hContext As Integer
    Public Protocol As Integer
    Public connActive As Boolean = False
    Private readername As String = ""
    ' change depending on reader
    Public SendBuff As Byte() = New Byte(262) {}
    Public RecvBuff As Byte() = New Byte(262) {}
    Public SendLen As Integer, RecvLen As Integer, nBytesRet As Integer, reqType As Integer, Aprotocol As Integer, dwProtocol As Integer, cbPciLength As Integer
    Public RdrState As Card.SCARD_READERSTATE
    Public pioSendRequest As Card.SCARD_IO_REQUEST
    Public states As Card.SCARD_READERSTATE()
    Private picture_path As String = String.Empty

    Private Sub LoadStudents_RFID_Registered()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_", conn)
            End Using
        End Using
    End Sub


    'GET THE RFID READER AND SELECT IT
    Public Sub SelectDevice()
        Dim availableReaders As List(Of String) = Me.ListReaders()
        Me.RdrState = New Card.SCARD_READERSTATE()
        If availableReaders.Count > 0 Then
            readername = availableReaders(0).ToString()
        Else
            readername = ""
        End If
        'selecting first device
        Me.RdrState.RdrName = readername
        'ADDITIONAL CODE FOR READING THE CARD USING BACKGROUND WORKER
        states = New Card.SCARD_READERSTATE(0) {}
        states(0) = New Card.SCARD_READERSTATE()
        states(0).RdrName = readername
        states(0).UserData = IntPtr.Zero
        states(0).RdrCurrState = Card.SCARD_STATE_EMPTY
        states(0).RdrEventState = 0
        states(0).ATRLength = 0
        states(0).ATRValue = Nothing

        If availableReaders.Count > 0 Then
            lblNFCStatus.Text = "CONNECTED"
            lblNFCStatus.ForeColor = Color.DarkGreen
            BackgroundWorker1.RunWorkerAsync()
        Else
            lblNFCStatus.Text = "DISCONNECTED"
            lblNFCStatus.ForeColor = Color.Red
        End If
    End Sub

    'GET THE LIST OF RFID READER
    Public Function ListReaders() As List(Of String)
        Dim ReaderCount As Integer = 0
        Dim AvailableReaderList As New List(Of String)()

        'Make sure a context has been established before 
        'retrieving the list of smartcard readers.
        retCode = Card.SCardListReaders(hContext, Nothing, Nothing, ReaderCount)
        If retCode <> Card.SCARD_S_SUCCESS Then
            'connActive = false;
            MessageBox.Show(Card.GetScardErrMsg(retCode))
        End If

        Dim ReadersList As Byte() = New Byte(ReaderCount - 1) {}

        'Get the list of reader present again but this time add sReaderGroup, retData as 2rd & 3rd parameter respectively.
        retCode = Card.SCardListReaders(hContext, Nothing, ReadersList, ReaderCount)
        If retCode <> Card.SCARD_S_SUCCESS Then
            MessageBox.Show(Card.GetScardErrMsg(retCode))
        End If

        Dim rName As String = ""
        Dim indx As Integer = 0
        If ReaderCount > 0 Then
            ' Convert reader buffer to string
            While ReadersList(indx) <> 0

                While ReadersList(indx) <> 0
                    rName = rName & CChar(ChrW(ReadersList(indx)))
                    indx = indx + 1
                End While

                'Add reader name to list
                AvailableReaderList.Add(rName)
                rName = ""

                indx = indx + 1
            End While
        End If
        Return AvailableReaderList
    End Function

    Private Sub frm_rfid_registered_lists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        SelectDevice()
    End Sub

    'reading via background worker
    Friend Enum SmartcardState
        None = 0
        Inserted = 1
        Ejected = 2
    End Enum

    'VERIFY IF THE THERE WAS A CONNECTED CARD.
    Public Function connectCard() As Boolean
        connActive = True
        retCode = Card.SCardConnectA(hContext, readername, Card.SCARD_SHARE_SHARED, Card.SCARD_PROTOCOL_T0 Or Card.SCARD_PROTOCOL_T1, hCard, Protocol)
        If retCode <> Card.SCARD_S_SUCCESS Then
            MessageBox.Show(Card.GetScardErrMsg(retCode), "Card not available", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            connActive = False
            Return False
        End If
        Return True
    End Function

    'BACKGROUND WORKER
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        While Not e.Cancel
            Dim nErrCode As Integer = Card.SCardGetStatusChangeA(hContext, 1000, states(0), 1)

            'Check if the state changed from the last time.
            If (Me.states(0).RdrEventState And 2) = 2 Then
                'Check what changed.
                Dim state As SmartcardState = SmartcardState.None
                If (Me.states(0).RdrEventState And 32) = 32 AndAlso (Me.states(0).RdrCurrState And 32) <> 32 Then
                    'The card was inserted. 
                    state = SmartcardState.Inserted
                ElseIf (Me.states(0).RdrEventState And 16) = 16 AndAlso (Me.states(0).RdrCurrState And 16) <> 16 Then
                    'The card was ejected.
                    state = SmartcardState.Ejected
                End If
                If state <> SmartcardState.None AndAlso Me.states(0).RdrCurrState <> 0 Then
                    Select Case state
                        Case SmartcardState.Inserted
                            If True Then
                                'GET CARD UID IF THERE WAS A CARD DETECTED/INSERTED
                                If connectCard() Then
                                    Dim CardUID As String = getcardUID()
                                    lblCardUID.Text = CardUID.ToString.ToUpper
                                    lblCardUID.ForeColor = Color.DarkGreen
                                Else
                                    MsgBox("Unable to detect information", MsgBoxStyle.Critical)
                                End If
                                Exit Select
                            End If
                        Case SmartcardState.Ejected
                            If True Then
                                'MessageBox.Show("Card ejected")
                                lblCardUID.Text = "00000000"
                                lblCardUID.ForeColor = Color.Red
                                Exit Select
                            End If
                        Case Else
                            MsgBox("An error occured while processing card information!", MsgBoxStyle.Critical)
                    End Select
                End If
                'Update the current state for the next time they are checked.
                Me.states(0).RdrCurrState = Me.states(0).RdrEventState
            End If
        End While
    End Sub

    'GET THE UID OF THE RFID CARD
    Public Function getcardUID() As String
        'only for mifare 1k cards
        Dim cardUID As String = ""
        Dim receivedUID As Byte() = New Byte(255) {}
        Dim request As New Card.SCARD_IO_REQUEST()
        request.dwProtocol = Card.SCARD_PROTOCOL_T1
        request.cbPciLength = System.Runtime.InteropServices.Marshal.SizeOf(GetType(Card.SCARD_IO_REQUEST))
        Dim sendBytes As Byte() = New Byte() {&HFF, &HCA, &H0, &H0, &H0}
        'get UID command      for Mifare cards
        Dim outBytes As Integer = receivedUID.Length
        Dim status As Integer = Card.SCardTransmit(hCard, request, sendBytes(0), sendBytes.Length, request, receivedUID(0), _
            outBytes)

        If status <> Card.SCARD_S_SUCCESS Then
            cardUID = "Error"
        Else
            cardUID = BitConverter.ToString(receivedUID.Take(4).ToArray()).Replace("-", String.Empty).ToLower()
        End If

        Return cardUID
    End Function

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.establishContext()
    End Sub

    Friend Sub establishContext()
        retCode = Card.SCardEstablishContext(Card.SCARD_SCOPE_SYSTEM, 0, 0, hContext)
        If retCode <> Card.SCARD_S_SUCCESS Then
            MsgBox("Check your device and please restart again", MsgBoxStyle.Critical)
            connActive = False
            Return
        End If
    End Sub




    ' clear memory buffers
    Private Sub ClearBuffers()
        Dim indx As Long
        For indx = 0 To 262
            RecvBuff(indx) = 0
            SendBuff(indx) = 0
        Next
    End Sub

    ' send application protocol data unit : communication unit between a smart card reader and a smart card
    Private Function SendAPDUandDisplay(reqType As Integer) As Integer
        Dim indx As Integer
        Dim tmpStr As String = ""

        pioSendRequest.dwProtocol = Aprotocol
        pioSendRequest.cbPciLength = 8

        'Display Apdu In
        For indx = 0 To SendLen - 1
            tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", SendBuff(indx))
        Next

        retCode = Card.SCardTransmit(hCard,
                                     pioSendRequest,
                                     SendBuff(0),
                                     SendLen,
                                     pioSendRequest,
                                     RecvBuff(0),
                                     RecvLen)

        If retCode <> Card.SCARD_S_SUCCESS Then
            Return retCode
        Else

            Try
                tmpStr = ""
                Select Case reqType
                    Case 0
                        For indx = (RecvLen - 2) To (RecvLen - 1)
                            tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", RecvBuff(indx))
                        Next

                        If (tmpStr).Trim() <> "90 00" Then
                            'MessageBox.Show("Return bytes are not acceptable.");
                            Return -202
                        End If

                        Exit Select

                    Case 1

                        For indx = (RecvLen - 2) To (RecvLen - 1)
                            tmpStr = tmpStr & String.Format("{0:X2}", RecvBuff(indx))
                        Next

                        If tmpStr.Trim() <> "90 00" Then
                            tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", RecvBuff(indx))
                        Else

                            tmpStr = "ATR : "
                            For indx = 0 To (RecvLen - 3)
                                tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", RecvBuff(indx))
                            Next
                        End If

                        Exit Select

                    Case 2

                        For indx = 0 To (RecvLen - 1)
                            tmpStr = (tmpStr & Convert.ToString(" ")) + String.Format("{0:X2}", RecvBuff(indx))
                        Next

                        Exit Select
                End Select
            Catch generatedExceptionName As IndexOutOfRangeException
                Return -200
            End Try
        End If
        Return retCode
    End Function

    ' block authentication
    Private Function authenticateBlock(block As [String]) As Boolean
        ClearBuffers()
        SendBuff(0) = &HFF
        ' CLA
        SendBuff(2) = &H0
        ' P1: same for all source types 
        SendBuff(1) = &H86
        ' INS: for stored key input
        SendBuff(3) = &H0
        ' P2 : Memory location;  P2: for stored key input
        SendBuff(4) = &H5
        ' P3: for stored key input
        SendBuff(5) = &H1
        ' Byte 1: version number
        SendBuff(6) = &H0
        ' Byte 2
        SendBuff(7) = CByte(Integer.Parse(block))
        ' Byte 3: sectore no. for stored key input
        SendBuff(8) = &H60
        ' Byte 4 : Key A for stored key input
        SendBuff(9) = CByte(Integer.Parse("1"))
        ' Byte 5 : Session key for non-volatile memory
        SendLen = &HA
        RecvLen = &H2

        retCode = SendAPDUandDisplay(0)

        If retCode <> Card.SCARD_S_SUCCESS Then
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
            SendBuff(0) = &HFF
            ' CLA
            SendBuff(1) = &HD6
            ' INS
            SendBuff(2) = &H0
            ' P1
            SendBuff(3) = CByte(Integer.Parse(Block))
            ' P2 : Starting Block No.
            SendBuff(4) = CByte(Integer.Parse("16"))
            ' P3 : Data length
            For indx = 0 To (tmpStr).Length - 1
                SendBuff(indx + 5) = CByte(AscW(tmpStr(indx)))
            Next
            SendLen = SendBuff(4) + 5
            RecvLen = &H2

            retCode = SendAPDUandDisplay(2)

            If retCode <> Card.SCARD_S_SUCCESS Then
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
















    'DATABASE CODE
    Private Sub CheckIfRFIDExists(UID As String)
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM tbl_student_information_card WHERE CardUID = @CardUID AND CardStatus = 'ACTIVE' AND Academic_Yr = @ay", conn)
                comm.Parameters.AddWithValue("@CardUID", UID)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                Using reader As SqlDataReader = comm.ExecuteReader
                    If reader.HasRows = True Then

                    Else
                        With frm_rfid_unregistered_lists
                            .StartPosition = FormStartPosition.CenterParent
                            .ShowDialog()
                        End With
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If connectCard() Then 'establish connection to the card: you've declared this from previous post
            Dim CardUID As String = getcardUID()

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
                Using comm As New SqlCommand("INSERT INTO tbl_student_information_card VALUES (@carduid,@Education_Level,@sn,@picture,@ay,@sem,@status,@registration_date)", conn)
                    comm.Parameters.AddWithValue("@carduid", CardUID)
                    comm.Parameters.AddWithValue("@Education_Level", txtEducationLevel.Text)
                    comm.Parameters.AddWithValue("@sn", txtStudentNo.Text)
                    comm.Parameters.AddWithValue("@Picture", picture_path)
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

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        With frm_rfid_unregistered_lists
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
        End With
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim ofd As New OpenFileDialog
        With ofd
            .InitialDirectory = "\\colm\MY SHARED\COLM FILES 2017-2018\ID\PICTURES"
            .ShowDialog()

            If .FileName.Length = 0 Then
                MsgBox("No File selected")
            Else
                picture_path = System.IO.Path.GetFullPath(.FileName)
                PictureBox1.Image = Image.FromFile(picture_path)
            End If
        End With
    End Sub
End Class