Module Program_Functions

    'GETTING THE AGE OF PERSON USING OBJECTS AND BIRTHDATE
    Public Function GetAge(ByVal cmbMonth As ComboBox, ByVal txtDay As TextBox, ByVal txtYear As TextBox, Optional ByVal AsOf As System.DateTime = #1/1/1700#) As String
        Dim returned As String = String.Empty
        Dim Birthdate As DateTime
        If cmbMonth.Text = String.Empty Or txtDay.Text = String.Empty Or txtYear.Text = String.Empty Or Len(txtYear.Text) <> 4 Then
            returned = ""
        Else
            Try
                Birthdate = DateValue(cmbMonth.Text & " " & txtDay.Text & " " & txtYear.Text)
                Dim iMonths As Integer
                Dim iYears As Integer
                Dim dYears As Decimal
                Dim lDayOfBirth As Long
                Dim lAsOf As Long
                Dim iBirthMonth As Integer
                Dim iAsOFMonth As Integer

                If AsOf = "#1/1/1700#" Then
                    AsOf = DateTime.Now
                End If
                lDayOfBirth = DatePart(DateInterval.Day, Birthdate)
                lAsOf = DatePart(DateInterval.Day, AsOf)

                iBirthMonth = DatePart(DateInterval.Month, Birthdate)
                iAsOFMonth = DatePart(DateInterval.Month, AsOf)

                iMonths = DateDiff(DateInterval.Month, Birthdate, AsOf)

                dYears = iMonths / 12

                iYears = Math.Floor(dYears)

                If iBirthMonth = iAsOFMonth Then
                    If lAsOf < lDayOfBirth Then
                        iYears = iYears - 1
                    End If
                End If
                returned = iYears

            Catch ex As Exception
                cmbMonth.SelectedIndex = -1
                txtDay.Text = String.Empty
                txtYear.Text = String.Empty
                MsgBox("Invalid birthdate combination!", MsgBoxStyle.Critical)
                cmbMonth.Focus()
            End Try
        End If
        Return returned
    End Function

    'GETTING THE AGE OF PERSON BY BIRTHDATE
    Public Function GetAge(ByVal Birthdate As System.DateTime, Optional ByVal AsOf As System.DateTime = #1/1/1700#) As String
        Dim returned As String = String.Empty

        Try
            Birthdate = DateValue(Birthdate)
            Dim iMonths As Integer
            Dim iYears As Integer
            Dim dYears As Decimal
            Dim lDayOfBirth As Long
            Dim lAsOf As Long
            Dim iBirthMonth As Integer
            Dim iAsOFMonth As Integer

            If AsOf = "#1/1/1700#" Then
                AsOf = DateTime.Now
            End If
            lDayOfBirth = DatePart(DateInterval.Day, Birthdate)
            lAsOf = DatePart(DateInterval.Day, AsOf)

            iBirthMonth = DatePart(DateInterval.Month, Birthdate)
            iAsOFMonth = DatePart(DateInterval.Month, AsOf)

            iMonths = DateDiff(DateInterval.Month, Birthdate, AsOf)

            dYears = iMonths / 12

            iYears = Math.Floor(dYears)

            If iBirthMonth = iAsOFMonth Then
                If lAsOf < lDayOfBirth Then
                    iYears = iYears - 1
                End If
            End If
            returned = iYears
        Catch ex As Exception

        End Try

        Return returned
    End Function

    Public Function StripSpaces(input As String) As String
        'Remove all white spaces...
        Return String.Join(" ", input.Split(New Char() {}, StringSplitOptions.RemoveEmptyEntries))
    End Function

    Public Sub LettersOnly(sender As Object, e As KeyPressEventArgs)
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "abcdefghijklmnopqrstuvwxyzñ" & _
                                         "ABCDEFGHIJKLMNOPQRSTUVWXYZÑ" & _
                                         " "
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub

    Public Sub NumbersOnly(sender As Object, e As KeyPressEventArgs)
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "0123456789"
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub

    Public Sub NumbersOnlyWithDecimal(sender As Object, e As KeyPressEventArgs)
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "0123456789."
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub

    Public Sub LettersWithNumbers(sender As Object, e As KeyPressEventArgs)
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "abcdefghijklmnopqrstuvwxyzñ" & _
                                         "ABCDEFGHIJKLMNOPQRSTUVWXYZÑ" & _
                                         "0123456789" & _
                                         " "
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub

    Public Sub LettersWithDotAndComma(sender As Object, e As KeyPressEventArgs)
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "abcdefghijklmnopqrstuvwxyzñ" & _
                                         "ABCDEFGHIJKLMNOPQRSTUVWXYZÑ" & _
                                         ".," & _
                                         " "
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub

    Public Sub LettersWithNumbersAndDotAndComma(sender As Object, e As KeyPressEventArgs)
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "abcdefghijklmnopqrstuvwxyzñ" & _
                                         "ABCDEFGHIJKLMNOPQRSTUVWXYZÑ" & _
                                         "0123456789.," & _
                                         " "
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub

    Public Sub Emails(sender As Object, e As KeyPressEventArgs)
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "@_abcdefghijklmnopqrstuvwxyzñ" & _
                                         "ABCDEFGHIJKLMNOPQRSTUVWXYZÑ" & _
                                         "0123456789." & _
                                         " "
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub

    Public Function Convert_To_Currency(str As String)
        str = Format(str, "Currency")
        str = str.Replace("$", "")
        Return str
    End Function

    Public Function TestEmptyAndRemoveWhiteSpace(str As String)
        If str = String.Empty Then
            Return "N.A"
        Else
            Return StripSpaces(str)
        End If
    End Function
End Module
