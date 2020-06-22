Imports Microsoft.Reporting.WinForms

Public Class frm_payment
    Public Course As String = String.Empty
    Public Yrlvl As String = String.Empty
    Public Section_Code As String = String.Empty
    Public EducationLevel As String = String.Empty
    Public DGRow3 As Integer = 0
    Public DG_OFeeRow As Integer = 0
    Public AssessmentID As Integer = 0
    Public Sub Load_Assessment_Payment()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim Amount_Collected_TFEE As Double = 0
            Dim Amount_Collected_OFEE As Double = 0

            If EducationLevel = "COLLEGE" Then
                'COLLECTING OF FEES
                'TUITION FEE
                Using comm As New SqlCommand("SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @sn AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = 'TUITION FEE' AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If IsDBNull(comm.ExecuteScalar) = True Then
                        Amount_Collected_TFEE = 0
                    Else
                        Amount_Collected_TFEE = Val(comm.ExecuteScalar)
                    End If
                End Using

                txtCredit.Text = Convert_To_Currency(Amount_Collected_TFEE)

                Dim Total_Amount As Double = 0
                'LOADING OF FEES ON TUITION FEE TAB
                Dim SQLQuery As String = String.Empty
                If Academic_Year <> "2018-2019" Then
                    SQLQuery = "SELECT * FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem ORDER BY ID ASC"
                ElseIf Academic_Year = "2018-2019" Then
                    SQLQuery = "SELECT * FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem AND AssessmentID = @AssessmentID ORDER BY ID ASC"
                End If
                Using comm As New SqlCommand(SQLQuery, conn)
                    comm.Parameters.AddWithValue("@AssessmentID", AssessmentID)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DG_TFEE.Rows.Clear()
                        While reader.Read
                            Total_Amount += reader("FEE_AMOUNT")
                            Dim Fee_Amount As Double = reader("FEE_AMOUNT")
                            Dim Balance As Double = 0
                            Dim Paid As Double = 0
                            If Amount_Collected_TFEE >= Fee_Amount Then
                                Balance = 0
                                Amount_Collected_TFEE -= Fee_Amount
                                Paid = Fee_Amount
                            Else
                                Balance = Fee_Amount - Amount_Collected_TFEE
                                Paid = Amount_Collected_TFEE
                                Amount_Collected_TFEE = 0
                            End If

                            DG_TFEE.Rows.Add(reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(Paid), Convert_To_Currency(Balance), False)
                        End While
                    End Using
                End Using

                Dim Count As Integer = 0
                'LOADING OF FEES ON OTHER FEE TAB - COLLEGE
                Using comm As New SqlCommand("SELECT ID,REF_CODE,FEE_CODE,FEE_AMOUNT,(SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND FEE_STATUS = @fee_type AND RECIEPT_STATUS = 'ACTIVE' AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem) AS PAYMENT,(FEE_AMOUNT - (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = @fee_type AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem)) AS BALANCE FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND FEE_TYPE = @fee_type AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@fee_type", "OTHER FEES (INTERNAL)")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView2.Rows.Clear()
                        While reader.Read
                            If IsDBNull(reader("PAYMENT")) = True Then
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(0), Convert_To_Currency(0), False, reader("ID"))
                            Else
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(reader("PAYMENT")), Convert_To_Currency(reader("BALANCE")), False, reader("ID"))
                            End If
                            Count += 1
                        End While
                    End Using
                End Using

                For i = 0 To DataGridView2.Rows.Count - 1
                    DataGridView2.Rows(i).Cells(4).Value = Convert_To_Currency(CDbl(DataGridView2.Rows(i).Cells(2).Value) - CDbl(DataGridView2.Rows(i).Cells(3).Value))
                    If CDbl(DataGridView2.Rows(i).Cells(4).Value) <= 0 Then
                        DataGridView2.Rows(i).Cells(5).ReadOnly = True
                    Else
                        DataGridView2.Rows(i).Cells(5).ReadOnly = False
                    End If
                Next

                'txtTotalCountOtherFees.Text = Count

                txtDebit.Text = Convert_To_Currency(Total_Amount)
                txtBalance.Text = Convert_To_Currency(CDbl(txtDebit.Text.Replace("TOTAL DEBIT: ", "")) - CDbl(txtCredit.Text.Replace("TOTAL CREDIT: ", "")))

            ElseIf EducationLevel = "JUNIOR HIGH" Then

                'COLLECTING OF FEES - HIGH SCHOOL
                Using comm As New SqlCommand("SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @sn AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = 'TUITION FEE' AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    If IsDBNull(comm.ExecuteScalar) = True Then
                        Amount_Collected_TFEE = 0
                    Else
                        Amount_Collected_TFEE = Val(comm.ExecuteScalar)
                    End If
                End Using

                txtCredit.Text = Convert_To_Currency(Amount_Collected_TFEE)

                Dim Total_Amount As Double = 0
                'LOADING OF FEES ON TUITION FEE TAB
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND  ACADEMIC_YR = @ay ORDER BY ID ASC", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DG_TFEE.Rows.Clear()
                        While reader.Read
                            Total_Amount += reader("FEE_AMOUNT")
                            Dim Fee_Amount As Double = reader("FEE_AMOUNT")
                            Dim Balance As Double = 0
                            Dim Paid As Double = 0
                            If Amount_Collected_TFEE >= Fee_Amount Then
                                Balance = 0
                                Amount_Collected_TFEE -= Fee_Amount
                                Paid = Fee_Amount
                            Else
                                Balance = Fee_Amount - Amount_Collected_TFEE
                                Paid = Amount_Collected_TFEE
                                Amount_Collected_TFEE = 0
                            End If

                            DG_TFEE.Rows.Add(reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(Paid), Convert_To_Currency(Balance), False)
                        End While
                    End Using
                End Using


                'LOADING OF FEES ON OTHER FEE TAB - HIGH SCHOOL

                Dim Count As Integer = 0
                'LOADING OF FEES ON OTHER FEE TAB - HIGH SCHOOL
                Using comm As New SqlCommand("SELECT ID,REF_CODE,FEE_CODE,FEE_AMOUNT,(SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND FEE_STATUS = @fee_type AND RECIEPT_STATUS = 'ACTIVE' AND ACADEMIC_YR = @ay) AS PAYMENT,(FEE_AMOUNT - (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = @fee_type AND ACADEMIC_YR = @ay)) AS BALANCE FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND FEE_TYPE = @fee_type AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@fee_type", "OTHER FEES (INTERNAL)")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView2.Rows.Clear()
                        While reader.Read
                            If IsDBNull(reader("PAYMENT")) = True Then
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(0), Convert_To_Currency(0), False, reader("ID"))
                            Else
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(reader("PAYMENT")), Convert_To_Currency(reader("BALANCE")), False, reader("ID"))
                            End If
                            Count += 1
                        End While
                    End Using
                End Using

                For i = 0 To DataGridView2.Rows.Count - 1
                    DataGridView2.Rows(i).Cells(4).Value = Convert_To_Currency(CDbl(DataGridView2.Rows(i).Cells(2).Value) - CDbl(DataGridView2.Rows(i).Cells(3).Value))
                    If CDbl(DataGridView2.Rows(i).Cells(4).Value) <= 0 Then
                        DataGridView2.Rows(i).Cells(5).ReadOnly = True
                    Else
                        DataGridView2.Rows(i).Cells(5).ReadOnly = False
                    End If
                Next

                'txtTotalCountOtherFees.Text = Count

                txtDebit.Text = Convert_To_Currency(Total_Amount)
                txtBalance.Text = Convert_To_Currency(CDbl(txtDebit.Text.Replace("TOTAL DEBIT: ", "")) - CDbl(txtCredit.Text.Replace("TOTAL CREDIT: ", "")))

            ElseIf EducationLevel = "SENIOR HIGH" Then

                'COLLECTING OF FEES - ELEMENTARY
                Using comm As New SqlCommand("SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @sn AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = 'TUITION FEE' AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If IsDBNull(comm.ExecuteScalar) = True Then
                        Amount_Collected_TFEE = 0
                    Else
                        Amount_Collected_TFEE = Val(comm.ExecuteScalar)
                    End If
                End Using

                txtCredit.Text = Convert_To_Currency(Amount_Collected_TFEE)

                Dim Total_Amount As Double = 0
                'LOADING OF FEES ON TUITION FEE TAB
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay ORDER BY ID ASC", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DG_TFEE.Rows.Clear()
                        While reader.Read
                            Total_Amount += reader("FEE_AMOUNT")
                            Dim Fee_Amount As Double = reader("FEE_AMOUNT")
                            Dim Balance As Double = 0
                            Dim Paid As Double = 0
                            If Amount_Collected_TFEE >= Fee_Amount Then
                                Balance = 0
                                Amount_Collected_TFEE -= Fee_Amount
                                Paid = Fee_Amount
                            Else
                                Balance = Fee_Amount - Amount_Collected_TFEE
                                Paid = Amount_Collected_TFEE
                                Amount_Collected_TFEE = 0
                            End If

                            DG_TFEE.Rows.Add(reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(Paid), Convert_To_Currency(Balance), False)
                        End While
                    End Using
                End Using


                'LOADING OF FEES ON OTHER FEE TAB - HIGH SCHOOL

                Dim Count As Integer = 0
                'LOADING OF FEES ON OTHER FEE TAB - COLLEGE
                Using comm As New SqlCommand("SELECT ID,REF_CODE,FEE_CODE,FEE_AMOUNT,(SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND FEE_STATUS = @fee_type AND RECIEPT_STATUS = 'ACTIVE' AND ACADEMIC_YR = @ay) AS PAYMENT,(FEE_AMOUNT - (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = @fee_type AND ACADEMIC_YR = @ay)) AS BALANCE FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND FEE_TYPE = @fee_type AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@fee_type", "OTHER FEES (INTERNAL)")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView2.Rows.Clear()
                        While reader.Read
                            If IsDBNull(reader("PAYMENT")) = True Then
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(0), Convert_To_Currency(0), False, reader("ID"))
                            Else
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(reader("PAYMENT")), Convert_To_Currency(reader("BALANCE")), False, reader("ID"))
                            End If
                            Count += 1
                        End While
                    End Using
                End Using

                For i = 0 To DataGridView2.Rows.Count - 1
                    DataGridView2.Rows(i).Cells(4).Value = Convert_To_Currency(CDbl(DataGridView2.Rows(i).Cells(2).Value) - CDbl(DataGridView2.Rows(i).Cells(3).Value))
                    If CDbl(DataGridView2.Rows(i).Cells(4).Value) <= 0 Then
                        DataGridView2.Rows(i).Cells(5).ReadOnly = True
                    Else
                        DataGridView2.Rows(i).Cells(5).ReadOnly = False
                    End If
                Next

                'txtTotalCountOtherFees.Text = Count

                txtDebit.Text = Convert_To_Currency(Total_Amount)
                txtBalance.Text = Convert_To_Currency(CDbl(txtDebit.Text.Replace("TOTAL DEBIT: ", "")) - CDbl(txtCredit.Text.Replace("TOTAL CREDIT: ", "")))
            ElseIf EducationLevel = "ELEMENTARY" Then
                'COLLECTING OF FEES - SENIOR HIGH SCHOOL
                Using comm As New SqlCommand("SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @sn AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = 'TUITION FEE' AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If IsDBNull(comm.ExecuteScalar) = True Then
                        Amount_Collected_TFEE = 0
                    Else
                        Amount_Collected_TFEE = Val(comm.ExecuteScalar)
                    End If
                End Using

                txtCredit.Text = Convert_To_Currency(Amount_Collected_TFEE)

                Dim Total_Amount As Double = 0
                'LOADING OF FEES ON TUITION FEE TAB
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay ORDER BY ID ASC", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DG_TFEE.Rows.Clear()
                        While reader.Read
                            Total_Amount += reader("FEE_AMOUNT")
                            Dim Fee_Amount As Double = reader("FEE_AMOUNT")
                            Dim Balance As Double = 0
                            Dim Paid As Double = 0
                            If Amount_Collected_TFEE >= Fee_Amount Then
                                Balance = 0
                                Amount_Collected_TFEE -= Fee_Amount
                                Paid = Fee_Amount
                            Else
                                Balance = Fee_Amount - Amount_Collected_TFEE
                                Paid = Amount_Collected_TFEE
                                Amount_Collected_TFEE = 0
                            End If

                            DG_TFEE.Rows.Add(reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(Paid), Convert_To_Currency(Balance), False)
                        End While
                    End Using
                End Using


                'LOADING OF FEES ON OTHER FEE TAB - HIGH SCHOOL

                Dim Count As Integer = 0
                'LOADING OF FEES ON OTHER FEE TAB - COLLEGE
                Using comm As New SqlCommand("SELECT ID,REF_CODE,FEE_CODE,FEE_AMOUNT,(SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND FEE_STATUS = @fee_type AND RECIEPT_STATUS = 'ACTIVE' AND ACADEMIC_YR = @ay) AS PAYMENT,(FEE_AMOUNT - (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = @fee_type AND ACADEMIC_YR = @ay)) AS BALANCE FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND FEE_TYPE = @fee_type AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@fee_type", "OTHER FEES (INTERNAL)")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView2.Rows.Clear()
                        While reader.Read
                            If IsDBNull(reader("PAYMENT")) = True Then
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(0), Convert_To_Currency(0), False, reader("ID"))
                            Else
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(reader("PAYMENT")), Convert_To_Currency(reader("BALANCE")), False, reader("ID"))
                            End If
                            Count += 1
                        End While
                    End Using
                End Using

                For i = 0 To DataGridView2.Rows.Count - 1
                    DataGridView2.Rows(i).Cells(4).Value = Convert_To_Currency(CDbl(DataGridView2.Rows(i).Cells(2).Value) - CDbl(DataGridView2.Rows(i).Cells(3).Value))
                    If CDbl(DataGridView2.Rows(i).Cells(4).Value) <= 0 Then
                        DataGridView2.Rows(i).Cells(5).ReadOnly = True
                    Else
                        DataGridView2.Rows(i).Cells(5).ReadOnly = False
                    End If
                Next

                txtDebit.Text = Convert_To_Currency(Total_Amount)
                txtBalance.Text = Convert_To_Currency(CDbl(txtDebit.Text.Replace("TOTAL DEBIT: ", "")) - CDbl(txtCredit.Text.Replace("TOTAL CREDIT: ", "")))
            ElseIf EducationLevel = "PRE ELEMENTARY" Then
                'COLLECTING OF FEES - SENIOR HIGH SCHOOL
                Using comm As New SqlCommand("SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @sn AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = 'TUITION FEE' AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If IsDBNull(comm.ExecuteScalar) = True Then
                        Amount_Collected_TFEE = 0
                    Else
                        Amount_Collected_TFEE = Val(comm.ExecuteScalar)
                    End If
                End Using

                txtCredit.Text = Convert_To_Currency(Amount_Collected_TFEE)

                Dim Total_Amount As Double = 0
                'LOADING OF FEES ON TUITION FEE TAB
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_BREAKDOWN WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay ORDER BY ID ASC", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DG_TFEE.Rows.Clear()
                        While reader.Read
                            Total_Amount += reader("FEE_AMOUNT")
                            Dim Fee_Amount As Double = reader("FEE_AMOUNT")
                            Dim Balance As Double = 0
                            Dim Paid As Double = 0
                            If Amount_Collected_TFEE >= Fee_Amount Then
                                Balance = 0
                                Amount_Collected_TFEE -= Fee_Amount
                                Paid = Fee_Amount
                            Else
                                Balance = Fee_Amount - Amount_Collected_TFEE
                                Paid = Amount_Collected_TFEE
                                Amount_Collected_TFEE = 0
                            End If

                            DG_TFEE.Rows.Add(reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(Paid), Convert_To_Currency(Balance), False)
                        End While
                    End Using
                End Using


                'LOADING OF FEES ON OTHER FEE TAB - HIGH SCHOOL

                Dim Count As Integer = 0
                'LOADING OF FEES ON OTHER FEE TAB - COLLEGE
                Using comm As New SqlCommand("SELECT ID,REF_CODE,FEE_CODE,FEE_AMOUNT,(SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND FEE_STATUS = @fee_type AND RECIEPT_STATUS = 'ACTIVE' AND ACADEMIC_YR = @ay) AS PAYMENT,(FEE_AMOUNT - (SELECT SUM(AMOUNT_COLLECTED) FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = TBL_COLLEGE_FEE_LOADS.STUDENT_NUMBER AND FEE_CODE = TBL_COLLEGE_FEE_LOADS.REF_CODE AND RECIEPT_STATUS = 'ACTIVE' AND FEE_STATUS = @fee_type AND ACADEMIC_YR = @ay)) AS BALANCE FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND FEE_TYPE = @fee_type AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@fee_type", "OTHER FEES (INTERNAL)")
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView2.Rows.Clear()
                        While reader.Read
                            If IsDBNull(reader("PAYMENT")) = True Then
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(0), Convert_To_Currency(0), False, reader("ID"))
                            Else
                                DataGridView2.Rows.Add(reader("REF_CODE"), reader("FEE_CODE"), Convert_To_Currency(reader("FEE_AMOUNT")), Convert_To_Currency(reader("PAYMENT")), Convert_To_Currency(reader("BALANCE")), False, reader("ID"))
                            End If
                            Count += 1
                        End While
                    End Using
                End Using

                For i = 0 To DataGridView2.Rows.Count - 1
                    DataGridView2.Rows(i).Cells(4).Value = Convert_To_Currency(CDbl(DataGridView2.Rows(i).Cells(2).Value) - CDbl(DataGridView2.Rows(i).Cells(3).Value))
                    If CDbl(DataGridView2.Rows(i).Cells(4).Value) <= 0 Then
                        DataGridView2.Rows(i).Cells(5).ReadOnly = True
                    Else
                        DataGridView2.Rows(i).Cells(5).ReadOnly = False
                    End If
                Next

                txtDebit.Text = Convert_To_Currency(Total_Amount)
                txtBalance.Text = Convert_To_Currency(CDbl(txtDebit.Text.Replace("TOTAL DEBIT: ", "")) - CDbl(txtCredit.Text.Replace("TOTAL CREDIT: ", "")))
            End If

        End Using
    End Sub

    Public Sub LoadSOA()
        Dim TotalTuitionFee As Double = 0
        Dim Ddate As DateTime = Nothing
        Dim TFee As Double = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If EducationLevel = "COLLEGE" Then
                '--------------------------------FOR TUITION FEE
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add("BEG BAL", "BEGGINING BALANCE", reader("ASSESSED_DATE"), Convert_To_Currency(reader("OLD_BALANCE")), "-", Convert_To_Currency(reader("OLD_BALANCE")))
                            TFee = reader("OLD_BALANCE")
                            DataGridView1.Rows.Add("ENROLL", "ASSESSMENT", reader("ASSESSED_DATE"), Convert_To_Currency(CDbl(reader("TFEE")) + CDbl(reader("MFEE")) + CDbl(reader("OFEE") + CDbl(reader("SURCHARGE")))), "-", Convert_To_Currency(CDbl(reader("TFEE")) + CDbl(reader("MFEE")) + CDbl(reader("OFEE") + CDbl(reader("SURCHARGE")))))
                            TFee = TFee + CDbl(Convert_To_Currency(CDbl(reader("TFEE")) + CDbl(reader("MFEE")) + CDbl(reader("OFEE") + CDbl(reader("SURCHARGE")))))
                            If IsDBNull(reader("Voucher_Code")) = False Then
                                DataGridView1.Rows.Add("DISC", reader("VOUCHER_CODE"), reader("ASSESSED_DATE"), "-", Convert_To_Currency(reader("VOUCHER_AMOUNT")), Convert_To_Currency(TFee - CDbl(reader("VOUCHER_AMOUNT"))))
                                TFee = TFee - CDbl(reader("VOUCHER_AMOUNT"))
                            End If
                            DataGridView1.Rows.Add("DISC", reader("DISCOUNT_CODE"), reader("ASSESSED_DATE"), "-", Convert_To_Currency(reader("DISCOUNT_AMOUNT")), Convert_To_Currency(TFee - CDbl(reader("DISCOUNT_AMOUNT"))))
                            TotalTuitionFee = reader("TOTAL")
                        End While
                    End Using
                End Using

                'GETTING PAYMENTS IN TUITION FEE
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @sn AND FEE_STATUS = 'TUITION FEE' AND RECIEPT_STATUS = 'ACTIVE' AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem ORDER BY DATE_RECEIVED ASC", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        If reader.HasRows = True Then
                            While reader.Read
                                Dim Fee_Type As String = reader("FEE_TYPE")
                                Dim Amount_Collected As Double = reader("AMOUNT_COLLECTED")
                                Select Case Fee_Type
                                    Case "CASH PAYMENT"
                                        DataGridView1.Rows.Add("PAY", reader("FEE_CODE"), reader("DATE_RECEIVED"), "-", Convert_To_Currency(reader("AMOUNT_COLLECTED")), Convert_To_Currency(TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))))
                                        DataGridView1.Rows.Add("BAL TF", "********************************", Format(CDate(reader("DATE_RECEIVED")), "MM-dd-yyyy"), Convert_To_Currency(TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))), "-")
                                        TotalTuitionFee = TotalTuitionFee - Amount_Collected
                                    Case "CREDIT MEMO"
                                        DataGridView1.Rows.Add("CM", reader("FEE_CODE"), reader("DATE_RECEIVED"), "-", Convert_To_Currency(reader("AMOUNT_COLLECTED")), Convert_To_Currency(TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))))
                                        DataGridView1.Rows.Add("BAL TF", "********************************", Format(CDate(reader("DATE_RECEIVED")), "MM-dd-yyyy"), Convert_To_Currency(TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))), "-")
                                        TotalTuitionFee = TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))
                                End Select
                            End While
                        Else
                            DataGridView1.Rows.Add("BAL TF", "********************************", Format(Date.Now, "MM-dd-yyyy"), Convert_To_Currency(TotalTuitionFee), "-")
                        End If

                    End Using
                End Using

                'FOR OTHER FEES
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem AND FEE_TYPE = 'OTHER FEES (INTERNAL)'", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Dim TotalOtherFee As Double = 0
                            DataGridView1.Rows.Add("ADDL", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader("DATE_SAVE"), Convert_To_Currency(reader("FEE_AMOUNT")), "-", Convert_To_Currency(reader("FEE_AMOUNT")))
                            TotalOtherFee = reader("FEE_AMOUNT")
                            'GETTING PAYMENTS ON OTHER FEES
                            Using conn1 As New SqlConnection(StringConnection)
                                conn1.Open()
                                Using comm1 As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE FEE_CODE = @refcode ORDER BY DATE_RECEIVED ASC", conn1)
                                    comm1.Parameters.AddWithValue("@refcode", reader("REF_CODE"))
                                    Using reader1 As SqlDataReader = comm1.ExecuteReader
                                        If reader1.HasRows = True Then
                                            While reader1.Read
                                                Select Case reader1("FEE_TYPE")
                                                    Case "CASH PAYMENT"
                                                        DataGridView1.Rows.Add("PAY", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader1("DATE_RECEIVED"), "-", Convert_To_Currency(reader1("AMOUNT_COLLECTED")), Convert_To_Currency(TotalOtherFee - CDbl(reader1("AMOUNT_COLLECTED"))))
                                                        DataGridView1.Rows.Add("BAL OF", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader1("DATE_RECEIVED"), Convert_To_Currency(TotalOtherFee - CDbl(reader1("AMOUNT_COLLECTED"))), "-")
                                                        TotalOtherFee = TotalOtherFee - CDbl(reader1("Amount_Collected"))
                                                    Case "CREDIT MEMO"
                                                        DataGridView1.Rows.Add("CM", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader1("DATE_RECEIVED"), "-", Convert_To_Currency(reader1("AMOUNT_COLLECTED")), Convert_To_Currency(TotalOtherFee - CDbl(reader1("AMOUNT_COLLECTED"))))
                                                        DataGridView1.Rows.Add("BAL OF", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader1("DATE_RECEIVED"), Convert_To_Currency(TotalOtherFee - CDbl(reader1("AMOUNT_COLLECTED"))), "-")
                                                        TotalOtherFee = TotalOtherFee - CDbl(reader1("Amount_Collected"))
                                                    Case Else

                                                End Select
                                            End While
                                        Else
                                            DataGridView1.Rows.Add("BAL OF", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader("DATE_SAVE"), Convert_To_Currency(TotalOtherFee), "-")
                                        End If
                                    End Using
                                End Using
                            End Using
                        End While
                    End Using
                End Using
            Else 'IF THE EDUCATION LEVEL IS NOT COLLEGE
                '--------------------------------FOR TUITION FEE
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView1.Rows.Clear()
                        While reader.Read
                            DataGridView1.Rows.Add("BEG BAL", "BEGGINING BALANCE", reader("ASSESSED_DATE"), Convert_To_Currency(reader("OLD_BALANCE")), "-", Convert_To_Currency(reader("OLD_BALANCE")))
                            TFee = reader("OLD_BALANCE")
                            DataGridView1.Rows.Add("ENROLL", "ASSESSMENT", reader("ASSESSED_DATE"), Convert_To_Currency(CDbl(reader("TFEE")) + CDbl(reader("MFEE")) + CDbl(reader("OFEE") + CDbl(reader("SURCHARGE")))), "-", Convert_To_Currency(CDbl(reader("TFEE")) + CDbl(reader("MFEE")) + CDbl(reader("OFEE") + CDbl(reader("SURCHARGE")))))
                            TFee = TFee + CDbl(Convert_To_Currency(CDbl(reader("TFEE")) + CDbl(reader("MFEE")) + CDbl(reader("OFEE") + CDbl(reader("SURCHARGE")))))
                            DataGridView1.Rows.Add("DISC", reader("VOUCHER_CODE"), reader("ASSESSED_DATE"), "-", Convert_To_Currency(reader("VOUCHER_AMOUNT")), Convert_To_Currency(TFee - CDbl(reader("VOUCHER_AMOUNT"))))
                            TFee = TFee - CDbl(reader("VOUCHER_AMOUNT"))
                            DataGridView1.Rows.Add("DISC", reader("DISCOUNT_CODE"), reader("ASSESSED_DATE"), "-", Convert_To_Currency(reader("DISCOUNT_AMOUNT")), Convert_To_Currency(TFee - CDbl(reader("DISCOUNT_AMOUNT"))))
                            Dim acad_scholar As String() = reader("Academic_Scholar").ToString.Split("|")
                            If acad_scholar.Count = 2 Then
                                Dim acad_discount As Double = ((TFee - CDbl(reader("Surcharge"))) * (acad_scholar(1) / 100))
                                DataGridView1.Rows.Add("DISC", acad_scholar(0), reader("Assessed_Date"), "-", Convert_To_Currency(acad_discount), Convert_To_Currency(TFee - acad_discount))
                                TFee = TFee - acad_discount
                            End If

                            TotalTuitionFee = reader("TOTAL")
                        End While
                    End Using
                End Using
                'GETTING PAYMENTS IN TUITION FEE
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @sn AND FEE_STATUS = 'TUITION FEE' AND RECIEPT_STATUS = 'ACTIVE' AND ACADEMIC_YR = @ay ORDER BY DATE_RECEIVED ASC", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        If reader.HasRows = True Then
                            While reader.Read
                                Dim Fee_Type As String = reader("FEE_TYPE")
                                Dim Amount_Collected As Double = reader("AMOUNT_COLLECTED")
                                Select Case Fee_Type
                                    Case "CASH PAYMENT"
                                        DataGridView1.Rows.Add("PAY", reader("FEE_CODE"), reader("DATE_RECEIVED"), "-", Convert_To_Currency(reader("AMOUNT_COLLECTED")), Convert_To_Currency(TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))))
                                        DataGridView1.Rows.Add("BAL TF", "********************************", Format(CDate(reader("DATE_RECEIVED")), "MM-dd-yyyy"), Convert_To_Currency(TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))), "-")
                                        TotalTuitionFee = TotalTuitionFee - Amount_Collected
                                    Case "CREDIT MEMO"
                                        DataGridView1.Rows.Add("CM", reader("FEE_CODE"), reader("DATE_RECEIVED"), "-", Convert_To_Currency(reader("AMOUNT_COLLECTED")), Convert_To_Currency(TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))))
                                        DataGridView1.Rows.Add("BAL TF", "********************************", Format(CDate(reader("DATE_RECEIVED")), "MM-dd-yyyy"), Convert_To_Currency(TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))), "-")
                                        TotalTuitionFee = TotalTuitionFee - CDbl(reader("AMOUNT_COLLECTED"))
                                End Select
                            End While
                        Else
                            DataGridView1.Rows.Add("BAL TF", "********************************", Format(Date.Now, "MM-dd-yyyy"), Convert_To_Currency(TotalTuitionFee), "-")
                        End If

                    End Using
                End Using

                'FOR OTHER FEES
                Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND FEE_TYPE = 'OTHER FEES (INTERNAL)'", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Dim TotalOtherFee As Double = 0
                            DataGridView1.Rows.Add("ADDL", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader("DATE_SAVE"), Convert_To_Currency(reader("FEE_AMOUNT")), "-", Convert_To_Currency(reader("FEE_AMOUNT")))
                            TotalOtherFee = reader("FEE_AMOUNT")
                            'GETTING PAYMENTS ON OTHER FEES
                            Using conn1 As New SqlConnection(StringConnection)
                                conn1.Open()
                                Using comm1 As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE FEE_CODE = @refcode AND Reciept_Status = 'ACTIVE' ORDER BY DATE_RECEIVED ASC", conn1)
                                    comm1.Parameters.AddWithValue("@refcode", reader("REF_CODE"))
                                    Using reader1 As SqlDataReader = comm1.ExecuteReader
                                        If reader1.HasRows = True Then
                                            While reader1.Read
                                                Select Case reader1("FEE_TYPE")
                                                    Case "CASH PAYMENT"
                                                        DataGridView1.Rows.Add("PAY", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader1("DATE_RECEIVED"), "-", Convert_To_Currency(reader1("AMOUNT_COLLECTED")), Convert_To_Currency(TotalOtherFee - CDbl(reader1("AMOUNT_COLLECTED"))))
                                                        DataGridView1.Rows.Add("BAL OF", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader1("DATE_RECEIVED"), Convert_To_Currency(TotalOtherFee - CDbl(reader1("AMOUNT_COLLECTED"))), "-")
                                                        TotalOtherFee = TotalOtherFee - CDbl(reader1("Amount_Collected"))
                                                    Case "CREDIT MEMO"
                                                        DataGridView1.Rows.Add("CM", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader1("DATE_RECEIVED"), "-", Convert_To_Currency(reader1("AMOUNT_COLLECTED")), Convert_To_Currency(TotalOtherFee - CDbl(reader1("AMOUNT_COLLECTED"))))
                                                        DataGridView1.Rows.Add("BAL OF", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader1("DATE_RECEIVED"), Convert_To_Currency(TotalOtherFee - CDbl(reader1("AMOUNT_COLLECTED"))), "-")
                                                        TotalOtherFee = TotalOtherFee - CDbl(reader1("Amount_Collected"))
                                                    Case Else

                                                End Select
                                            End While
                                        Else
                                            DataGridView1.Rows.Add("BAL OF", reader("REF_CODE") & " | " & reader("FEE_CODE"), reader("DATE_SAVE"), Convert_To_Currency(TotalOtherFee), "-")
                                        End If
                                    End Using
                                End Using
                            End Using
                        End While
                    End Using
                End Using

            End If
        End Using


        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(0).Value = "BAL TF" Then
                row.DefaultCellStyle.BackColor = Color.LightSalmon
            ElseIf row.Cells(0).Value = "BAL OF" Then
                row.DefaultCellStyle.BackColor = Color.Wheat
            End If
        Next

        Dim TDebit As Double = 0
        Dim TCredit As Double = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(0).Value <> "BAL TF" And DataGridView1.Rows(i).Cells(0).Value <> "BAL OF" Then
                If DataGridView1.Rows(i).Cells(3).Value <> "-" Then
                    TDebit += CDbl(DataGridView1.Rows(i).Cells(3).Value)
                End If

                If DataGridView1.Rows(i).Cells(4).Value <> "-" Then
                    TCredit += CDbl(DataGridView1.Rows(i).Cells(4).Value)
                End If

            End If
        Next
        txtTotalPaid.Text = Convert_To_Currency(TCredit)
        txtTotalRecievable.Text = Convert_To_Currency(TDebit)
        txtTotalBalance.Text = Convert_To_Currency(TDebit - TCredit)
    End Sub

    Public Sub LoadSOA_Summary()
        dgSOASummary.Rows.Clear()
        Dim L_TotalPaid As Double = 0
        Dim L_TotalAmount As Double = 0
        Dim L_TotalBalance As Double = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()

            Using comm As New SqlCommand("SELECT * FROM FN_SOA_TuitionFee(@ay,@sem) WHERE Student_Number = @sn AND Balance >= 5", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                If EducationLevel = "COLLEGE" Then
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Else
                    comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                End If

                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        dgSOASummary.Rows.Add("TUITION FEE", Convert_To_Currency(reader("Total")),
                                             Convert_To_Currency(reader("Paid")),
                                             Convert_To_Currency(reader("Balance")))
                        L_TotalBalance += reader("Balance")
                    End While
                End Using
            End Using

            Using comm As New SqlCommand("SELECT * FROM FN_SOA_OtherFee(@ay,@sem) WHERE Student_Number = @sn AND Balance > 5", conn)
                comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                If EducationLevel <> "COLLEGE" Then
                    comm.Parameters.AddWithValue("@sem", "1ST SEMESTER")
                Else
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                End If
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        dgSOASummary.Rows.Add(reader("Fee_Code"), Convert_To_Currency(reader("Fee_Amount")),
                                             Convert_To_Currency(reader("Paid")),
                                             Convert_To_Currency(reader("Balance")))

                        L_TotalBalance += reader("Balance")
                    End While
                End Using
            End Using
        End Using

        txtSummaryBalance.Text = Convert_To_Currency(L_TotalBalance)
    End Sub
    Public Sub LoadPayments()
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            If EducationLevel = "COLLEGE" Then
                Using comm As New SqlCommand("SELECT DISTINCT CONVERT(VARCHAR(10),DATE_RECEIVED,120) AS DATE_RECEIVED,RECIEPT_NUMBER,RECIEPT_STATUS,FEE_TYPE,SUM(AMOUNT_COLLECTED) AS AMOUNT,RECEIVER FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay AND ACADEMIC_SEM = @sem GROUP BY CONVERT(VARCHAR(10),DATE_RECEIVED,120),RECIEPT_NUMBER,RECIEPT_STATUS,FEE_TYPE,RECEIVER ORDER BY DATE_RECEIVED DESC", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView3.Rows.Clear()
                        While reader.Read
                            If reader("RECIEPT_STATUS") = "ACTIVE" Then
                                DataGridView3.Rows.Add(reader("DATE_RECEIVED"), reader("RECIEPT_NUMBER"), reader("RECIEPT_STATUS"), reader("FEE_TYPE"), Convert_To_Currency(reader("AMOUNT")), reader("RECEIVER"), "CANCEL")
                            Else
                                DataGridView3.Rows.Add(reader("DATE_RECEIVED"), reader("RECIEPT_NUMBER"), reader("RECIEPT_STATUS"), reader("FEE_TYPE"), Convert_To_Currency(reader("AMOUNT")), reader("RECEIVER"), "ACTIVATE")
                            End If
                        End While
                    End Using
                End Using
            Else 'if the eduction level is not college then
                Using comm As New SqlCommand("SELECT DISTINCT CONVERT(VARCHAR(10),DATE_RECEIVED,120) AS DATE_RECEIVED,RECIEPT_NUMBER,RECIEPT_STATUS,FEE_TYPE,SUM(AMOUNT_COLLECTED) AS AMOUNT,RECEIVER FROM TBL_COLLEGE_PAYMENT WHERE STUDENT_NUMBER = @sn AND ACADEMIC_YR = @ay GROUP BY CONVERT(VARCHAR(10),DATE_RECEIVED,120),RECIEPT_NUMBER,RECIEPT_STATUS,FEE_TYPE,RECEIVER ORDER BY DATE_RECEIVED DESC", conn)
                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        DataGridView3.Rows.Clear()
                        While reader.Read
                            If reader("RECIEPT_STATUS") = "ACTIVE" Then
                                DataGridView3.Rows.Add(reader("DATE_RECEIVED"), reader("RECIEPT_NUMBER"), reader("RECIEPT_STATUS"), reader("FEE_TYPE"), Convert_To_Currency(reader("AMOUNT")), reader("RECEIVER"), "CANCEL")
                            Else
                                DataGridView3.Rows.Add(reader("DATE_RECEIVED"), reader("RECIEPT_NUMBER"), reader("RECIEPT_STATUS"), reader("FEE_TYPE"), Convert_To_Currency(reader("AMOUNT")), reader("RECEIVER"), "ACTIVATE")
                            End If
                        End While
                    End Using
                End Using
            End If

        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Checked_Fees As New List(Of String)
        Dim Checked_Balance As New List(Of Double)
        Dim Total_Payments As Double = 0
        With frm_payment_cash_tuition_entry
            For i = 0 To DG_TFEE.Rows.Count - 1
                If DG_TFEE.Rows(i).Cells(4).Value = True Then
                    Total_Payments += DG_TFEE.Rows(i).Cells(3).Value
                    .Application = Total_Payments
                    .Calculate_Payments()
                End If
            Next
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            Load_Assessment_Payment()
            LoadPayments()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Checked_Fees As New List(Of String)
        Dim Checked_Balance As New List(Of Double)
        Dim Total_Payments As Double = 0
        With frm_payment_cm_tuition_entry
            For i = 0 To DG_TFEE.Rows.Count - 1
                If DG_TFEE.Rows(i).Cells(4).Value = True Then
                    Total_Payments += DG_TFEE.Rows(i).Cells(3).Value
                    .Application = Total_Payments
                    .Calculate_Payments()
                End If
            Next
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            Load_Assessment_Payment()
        End With
    End Sub

    Private Sub DG_TFEE_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_TFEE.CellContentClick
        If DG_TFEE.Rows(e.RowIndex).Cells(4).Value = True And DG_TFEE.Rows(e.RowIndex).Cells(3).Value <= 0 Then
            DG_TFEE.Rows(e.RowIndex).Cells(4).Value = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With frm_payment_internal_list
            .Course = txtCourse.Text
            .Year = txtYear.Text
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            Load_Assessment_Payment()
            LoadSOA()
        End With
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        frm_payment_browse_student.StartPosition = FormStartPosition.CenterParent
        frm_payment_browse_student.ShowDialog()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim DS As New DS_SOA
        Dim DR As DataRow
        Dim tablename As String = "DT_SOA"
        Dim StudentNumber As ReportParameter = Nothing
        Dim StudentName As ReportParameter = Nothing
        Dim ProgramUser As ReportParameter = Nothing
        Dim AccountBalance As ReportParameter = Nothing

        StudentNumber = New ReportParameter("StudentNumber", txtStudentNumber.Text)
        StudentName = New ReportParameter("StudentName", txtStudentName.Text)
        ProgramUser = New ReportParameter("ProgramUser", Account_Name)
        Dim AySem As New ReportParameter("aysem", Academic_Year & " " & Academic_Sem)

        Dim CheckedAmountBalanceTfee As Double = 0
        Dim CheckedAmountBalanceOFee As Double = 0
        DS.Tables(tablename).Rows.Clear()
        With DS.Tables(tablename)

            For i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells(6).Value = True Then
                    DR = .NewRow
                    DR("CODE") = DataGridView1.Rows(i).Cells(0).Value
                    DR("DESCRIPTION") = DataGridView1.Rows(i).Cells(1).Value
                    DR("DATE") = Format(CDate(DataGridView1.Rows(i).Cells(2).Value), "MM-dd-yyyy")
                    DR("AMOUNT") = DataGridView1.Rows(i).Cells(3).Value
                    DR("PAID") = DataGridView1.Rows(i).Cells(4).Value
                    If DataGridView1.Rows(i).Cells(0).Value = "BAL TF" Then
                        CheckedAmountBalanceTfee = DataGridView1.Rows(i).Cells(3).Value
                    End If

                    If DataGridView1.Rows(i).Cells(0).Value = "BAL OF" Then
                        CheckedAmountBalanceOFee += CDbl(DataGridView1.Rows(i).Cells(3).Value)
                    End If
                    DR("BALANCE") = DataGridView1.Rows(i).Cells(5).Value
                    .Rows.Add(DR)
                End If
            Next
        End With
        Dim RemBalance As Double = CheckedAmountBalanceOFee + CheckedAmountBalanceTfee
        Dim RemBalanceStr As String = Convert_To_Currency(RemBalance)
        AccountBalance = New ReportParameter("Balance", RemBalanceStr)

        Dim MyReport As New ReportDataSource("DSOA", DS.Tables(0))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.SOA_Report.rdlc"
            .ReportViewer1.LocalReport.SetParameters({StudentNumber, StudentName, ProgramUser, AccountBalance, AySem})
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim Checked_Fees As New List(Of String)
        Dim Checked_Balance As New List(Of Double)
        Dim Total_Payments As Double = 0
        With frm_payment_cash_other_entry
            For i = 0 To DataGridView2.Rows.Count - 1
                If DataGridView2.Rows(i).Cells(5).Value = True Then
                    .DataGridView3.Rows.Add(DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(1).Value, DataGridView2.Rows(i).Cells(4).Value, DataGridView2.Rows(i).Cells(4).Value)
                End If
            Next
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            Load_Assessment_Payment()
        End With
    End Sub

    Private Sub DataGridView2_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView2.CellBeginEdit
        'If e.ColumnIndex = 4 Then
        '    If CDbl(DataGridView2.Rows(e.RowIndex).Cells(3).Value) <= 0 Then
        '        DataGridView2.Rows(e.RowIndex).Cells(4)
        '    End If
        'End If
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Checked_Fees As New List(Of String)
        Dim Checked_Balance As New List(Of Double)
        Dim Total_Payments As Double = 0
        With frm_payment_cm_other_entry
            For i = 0 To DataGridView2.Rows.Count - 1
                If DataGridView2.Rows(i).Cells(5).Value = True Then
                    .DataGridView3.Rows.Add(DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(1).Value, DataGridView2.Rows(i).Cells(4).Value, DataGridView2.Rows(i).Cells(4).Value)
                End If
            Next
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            Load_Assessment_Payment()
            LoadPayments()
        End With
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel this receipt?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("UPDATE TBL_COLLEGE_PAYMENT SET RECIEPT_STATUS = 'CANCELLED' WHERE RECIEPT_NUMBER = @rn", conn)
                    comm.Parameters.AddWithValue("@rn", DataGridView3.Rows(DGRow3).Cells(1).Value)
                    comm.ExecuteNonQuery()
                    MsgBox("Reciept No: " & DataGridView3.Rows(DGRow3).Cells(1).Value & " has been successfully cancelled!", MsgBoxStyle.Information)
                    Load_Assessment_Payment()
                    LoadSOA()
                    LoadPayments()
                End Using
            End Using
        End If
    End Sub
    Public Function GetFeeCodeForOtherFees(RefCode As String)
        Dim FeeCode As String = String.Empty
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT FEE_CODE FROM TBL_COLLEGE_FEE_LOADS WHERE REF_CODE = @refcode", conn)
                comm.Parameters.AddWithValue("@refcode", RefCode)
                FeeCode = comm.ExecuteScalar
            End Using
        End Using
        Return FeeCode
    End Function
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If DataGridView3.Rows.Count > 0 Then
            Dim DS As New DS_PAYMENT
            Dim DR As DataRow
            Dim param_aysem As ReportParameter = New ReportParameter("aysem", Academic_Year.ToString & " " & Academic_Sem.ToString)
            Dim param_StudentNo As ReportParameter = New ReportParameter("studentno", txtStudentNumber.Text)
            Dim param_StudentName As ReportParameter = New ReportParameter("studentname", txtStudentName.Text)
            Dim param_PrintDate As ReportParameter = New ReportParameter("printdate", Date.Now.ToString)
            Dim param_Course_Year_Sect As ReportParameter = New ReportParameter("course_year_sect", txtCourse.Text & " " & txtYear.Text & " " & txtSection.Text)
            Dim param_Total As ReportParameter = New ReportParameter("TotalDue", DataGridView3.Rows(DGRow3).Cells(4).Value.ToString)
            With DS.Tables("STUDENT INFORMATION")
                .Rows.Clear()
                Using conn As New SqlConnection(StringConnection)
                    conn.Open()
                    Using comm As New SqlCommand("SELECT * FROM TBL_COLLEGE_PAYMENT WHERE Reciept_Number = @rn", conn)
                        comm.Parameters.AddWithValue("@rn", DataGridView3.Rows(DGRow3).Cells(1).Value)
                        Using reader As SqlDataReader = comm.ExecuteReader
                            If DataGridView3.Rows(DGRow3).Cells(1).Value.ToString.Contains("*") Then
                                While reader.Read
                                    DR = .NewRow
                                    DR("STUDENT_NUMBER") = txtStudentNumber.Text
                                    DR("STUDENT_NAME") = txtStudentName.Text
                                    DR("FEE_CODE") = GetFeeCodeForOtherFees(reader("FEE_CODE"))
                                    DR("FEE_AMOUNT") = Convert_To_Currency(reader("AMOUNT_COLLECTED"))
                                    DR("TOTAL") = DataGridView3.Rows(DGRow3).Cells(4).Value
                                    .Rows.Add(DR)
                                End While
                            Else
                                While reader.Read
                                    DR = .NewRow
                                    DR("STUDENT_NUMBER") = txtStudentNumber.Text
                                    DR("STUDENT_NAME") = txtStudentName.Text
                                    DR("FEE_CODE") = reader("FEE_CODE")
                                    DR("FEE_AMOUNT") = Convert_To_Currency(reader("AMOUNT_COLLECTED"))
                                    DR("TOTAL") = DataGridView3.Rows(DGRow3).Cells(4).Value
                                    .Rows.Add(DR)
                                End While
                            End If
                        End Using
                    End Using
                End Using
            End With


            Dim MyReport As New ReportDataSource("DSPayment", DS.Tables("STUDENT INFORMATION"))
            With frm_rdlc_report_viewer
                .ReportViewer1.LocalReport.DataSources.Clear()
                .ReportViewer1.LocalReport.DataSources.Add(MyReport)
                .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.report_payment.rdlc"
                .ReportViewer1.LocalReport.SetParameters({param_aysem, param_StudentNo, param_StudentName, param_PrintDate, param_Course_Year_Sect, param_Total})
                .ReportViewer1.RefreshReport()
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With

            'Dim MyReport As New report_payment
            'MyReport.SetDataSource(DS.Tables("STUDENT INFORMATION"))

            'Dim myTextObjectOnReport As CrystalDecisions.CrystalReports.Engine.TextObject
            'myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtSYSem"), CrystalDecisions.CrystalReports.Engine.TextObject)
            'myTextObjectOnReport.Text = Academic_Sem & " " & Academic_Year

            'myTextObjectOnReport = CType(MyReport.ReportDefinition.ReportObjects.Item("txtCYS"), CrystalDecisions.CrystalReports.Engine.TextObject)
            'myTextObjectOnReport.Text = txtCourse.Text & " " & txtYear.Text & " " & txtSection.Text

            'With frm_report_assessment
            '    .CrystalReportViewer1.ReportSource = MyReport
            '    .CrystalReportViewer1.Refresh()
            '    .StartPosition = FormStartPosition.CenterParent
            '    .ShowDialog()
            'End With
        Else
            MsgBox("No Reciept Found!", MsgBoxStyle.Critical)
            Exit Sub
        End If

    End Sub

    Private Sub DataGridView3_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.RowEnter
        DGRow3 = e.RowIndex
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim Checked_Fees As New List(Of String)
        Dim Checked_Balance As New List(Of Double)
        Dim CanRemovedChecked As Boolean = True
        With frm_payment_cash_other_entry

            For i = 0 To DataGridView2.Rows.Count - 1
                If DataGridView2.Rows(i).Cells(5).Value = True And CDbl(DataGridView2.Rows(i).Cells(3).Value) > 0 Then
                    CanRemovedChecked = False
                    Exit For
                End If
            Next

            If CanRemovedChecked = False Then
                MsgBox("Program Detected that there is an existing payment that cannot be removed please contact system adminsitrator!", MsgBoxStyle.Critical)
            Else
                Dim WillRemovedRow As New List(Of Integer)
                For i = 0 To DataGridView2.Rows.Count - 1
                    If DataGridView2.Rows(i).Cells(5).Value = True Then
                        WillRemovedRow.Add(i)
                    End If
                Next

                If WillRemovedRow.Count = 0 Then
                    MsgBox("No checked fees detected, if you want to remove fees please check it and click remove!", MsgBoxStyle.Critical)
                Else
                    If MsgBox("Are you sure you want to removed checked fees?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        Using conn As New SqlConnection(StringConnection)
                            conn.Open()
                            For i = 0 To WillRemovedRow.Count - 1
                                'OLD QUERY
                                'DELETE FROM TBL_COLLEGE_FEE_LOADS WHERE STUDENT_NUMBER = @sn AND REF_CODE = @ref_code
                                Using comm As New SqlCommand("DELETE FROM TBL_COLLEGE_FEE_LOADS WHERE ID = @FeeID", conn)
                                    comm.Parameters.AddWithValue("@FeeID", DataGridView2.Rows(WillRemovedRow(i)).Cells(6).Value)
                                    comm.Parameters.AddWithValue("@sn", txtStudentNumber.Text)
                                    comm.Parameters.AddWithValue("@ref_code", DataGridView2.Rows(WillRemovedRow(i)).Cells(0).Value)
                                    comm.ExecuteNonQuery()
                                End Using
                            Next
                            MsgBox("Fees has been successfully removed!", MsgBoxStyle.Information)
                            Load_Assessment_Payment()
                            LoadSOA()
                            LoadPayments()
                        End Using
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub DataGridView2_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.RowEnter
        DG_OFeeRow = e.RowIndex
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'If CDbl(DataGridView2.Rows(DG_OFeeRow).Cells(3).Value) > 0 Then
        '    MsgBox("You cannot edit ")
        'End If
        With frm_payment_edit_other_fees
            .FeeLoadID = DataGridView2.Rows(DG_OFeeRow).Cells(6).Value
            .txtRefCode.Text = DataGridView2.Rows(DG_OFeeRow).Cells(0).Value
            .txtFeeCode.Text = DataGridView2.Rows(DG_OFeeRow).Cells(1).Value
            .txtOldFeeAmount.Text = DataGridView2.Rows(DG_OFeeRow).Cells(2).Value
            .CurrentPayment = DataGridView2.Rows(DG_OFeeRow).Cells(3).Value
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            Load_Assessment_Payment()
            LoadSOA()
            LoadPayments()
        End With
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Dim DS As New DS_SOA
        Dim DR As DataRow
        Dim tablename As String = "DT_SOA"
        Dim StudentNumber As ReportParameter = Nothing
        Dim StudentName As ReportParameter = Nothing
        Dim ProgramUser As ReportParameter = Nothing
        Dim AccountBalance As ReportParameter = Nothing

        StudentNumber = New ReportParameter("StudentNumber", txtStudentNumber.Text)
        StudentName = New ReportParameter("StudentName", txtStudentName.Text)
        ProgramUser = New ReportParameter("ProgramUser", Account_Name)
        AccountBalance = New ReportParameter("Balance", txtTotalBalance.Text)


        DS.Tables(tablename).Rows.Clear()
        With DS.Tables(tablename)
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("CODE") = DataGridView1.Rows(i).Cells(0).Value
                DR("DESCRIPTION") = DataGridView1.Rows(i).Cells(1).Value
                DR("DATE") = Format(CDate(DataGridView1.Rows(i).Cells(2).Value), "MM-dd-yyyy")
                DR("AMOUNT") = DataGridView1.Rows(i).Cells(3).Value
                DR("PAID") = DataGridView1.Rows(i).Cells(4).Value
                DR("BALANCE") = DataGridView1.Rows(i).Cells(5).Value
                .Rows.Add(DR)
            Next
        End With


        Dim MyReport As New ReportDataSource("DSOA", DS.Tables(0))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.SOA_Report.rdlc"
            .ReportViewer1.LocalReport.SetParameters({StudentNumber, StudentName, ProgramUser, AccountBalance})
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub CHECKToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtStudentNumber_TextChanged(sender As Object, e As EventArgs) Handles txtStudentNumber.TextChanged

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim DS As New DS_SOA
        Dim DR As DataRow
        Dim tablename As String = "DT_SOA"
        Dim aysem As ReportParameter = New ReportParameter("aysem", Academic_Year & " " & Academic_Sem)
        Dim StudentNumber As ReportParameter = New ReportParameter("sn", txtStudentNumber.Text)
        Dim StudentName As ReportParameter = New ReportParameter("studentname", txtStudentName.Text)
        Dim course As ReportParameter = New ReportParameter("course", txtCourse.Text)
        Dim year As ReportParameter = New ReportParameter("year", txtYear.Text)
        Dim section As ReportParameter = New ReportParameter("section", txtSection.Text)
        Dim ddate As ReportParameter = New ReportParameter("date", Format(Date.Now, "MM-dd-yyyy").ToString)
        Dim balance As ReportParameter = New ReportParameter("totalbalance", txtSummaryBalance.Text)

        DS.Tables(tablename).Rows.Clear()
        With DS.Tables(tablename)
            For i = 0 To dgSOASummary.Rows.Count - 1
                DR = .NewRow
                DR("CODE") = dgSOASummary.Rows(i).Cells(0).Value
                DR("AMOUNT") = dgSOASummary.Rows(i).Cells(1).Value
                DR("PAID") = dgSOASummary.Rows(i).Cells(2).Value
                DR("BALANCE") = dgSOASummary.Rows(i).Cells(3).Value
                .Rows.Add(DR)
            Next
        End With


        Dim MyReport As New ReportDataSource("DataSet1", DS.Tables(0))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.RPT_StatementOfAccount.rdlc"
            .ReportViewer1.LocalReport.SetParameters({aysem, StudentNumber, StudentName, course, year, section, ddate, balance})
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub
End Class