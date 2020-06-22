Public Class frm_enrollment_lists
    Dim student_numbers As New List(Of String)

    Public Function GetEnrollmentList_Summary() As ds_enrollment_list.DT_EnrollmentListSummaryDataTable
        Dim DT As New ds_enrollment_list.DT_EnrollmentListSummaryDataTable
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM dbo.FN_EnrollmentList_Summary(@ay,@sem)", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    While reader.Read
                        Dim DR As ds_enrollment_list.DT_EnrollmentListSummaryRow
                        DR = DT.NewDT_EnrollmentListSummaryRow
                        DR.Course = reader("Course")

                        DR.frstyear_male = reader("_1styear_Male")
                        DR.frstyear_female = reader("_1styear_Female")
                        DR.frstyear_total = CInt(reader("_1styear_Male")) + CInt(reader("_1styear_Female"))

                        DR.scndyear_male = reader("_2ndyear_Male")
                        DR.scndyear_female = reader("_2ndyear_Female")
                        DR.scndyear_total = CInt(reader("_2ndyear_Male")) + CInt(reader("_2ndyear_Female"))

                        DR.thrdyear_male = reader("_3rdYear_Male")
                        DR.thrdyear_female = reader("_3rdYear_Female")
                        DR.thrdyear_total = CInt(reader("_3rdYear_Male")) + CInt(reader("_3rdYear_Female"))

                        DR.frthyear_male = reader("_4thYear_Male")
                        DR.frthyear_female = reader("_4thYear_Female")
                        DR.frthyear_total = CInt(reader("_4thYear_Male")) + CInt(reader("_4thYear_Female"))

                        DR.GrandTotal = CInt(reader("_1styear_Male")) + CInt(reader("_1styear_Female")) + CInt(reader("_2ndyear_Male")) + CInt(reader("_2ndyear_Female")) + CInt(reader("_3rdYear_Male")) + CInt(reader("_3rdYear_Female")) + CInt(reader("_4thYear_Male")) + CInt(reader("_4thYear_Female"))
                        DT.AddDT_EnrollmentListSummaryRow(DR)
                    End While
                End Using
            End Using
        End Using
        Return DT
    End Function

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView1.Rows.Clear()
        Dim dt As New DataTable
        dt.Rows.Clear()
        Using conn As New SqlConnection(StringConnection)
            Await conn.OpenAsync
            Using comm As New SqlCommand("SELECT * FROM FN_EnrollmentLists(@ay,@sem) WHERE Student_Name IS NOT NULL ORDER BY STUDENT_NAME", conn)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = Await comm.ExecuteReaderAsync
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Dim Course_Codes As New List(Of String)
        Dim YearLevels As New List(Of String)
        Dim CurrentSN As String = dt.Rows(0).Item("Student_Number").ToString
        Dim CurrentCourse As String = dt.Rows(0).Item("Course").ToString
        Dim CurrentYear As String = dt.Rows(0).Item("Yrlvl").ToString
        For i = 0 To dt.Rows.Count - 1
            If CurrentSN <> dt.Rows(i).Item("Student_Number").ToString Then
                student_numbers.Add(CurrentSN)
                Course_Codes.Add(CurrentCourse)
                YearLevels.Add(CurrentYear)
                CurrentSN = dt.Rows(i).Item("Student_Number").ToString
                CurrentCourse = dt.Rows(i).Item("Course").ToString
                CurrentYear = dt.Rows(i).Item("Yrlvl").ToString
            End If
        Next

        For x = 0 To student_numbers.Count - 1
            Dim n As Integer = x + 1
            Dim NoOfSubjects As New List(Of String)
            Dim NoOfUnits As New List(Of String)
            Dim CurrentName As String = String.Empty
            DataGridView1.Rows.Add(student_numbers(x).ToString, n)
            CurrentSN = student_numbers(x).ToString
            Dim CurrentRowCount As Integer = DataGridView1.Rows.Count - 1

            For y = 0 To dt.Rows.Count - 1

                If dt.Rows(y).Item("Student_Number").ToString = student_numbers(x).ToString Then
                    DataGridView1.Rows(CurrentRowCount).Cells(2).Value = dt.Rows(y).Item("Student_Name").ToString
                    DataGridView1.Rows(CurrentRowCount).Cells(5).Value = dt.Rows(y).Item("Course").ToString
                    DataGridView1.Rows(CurrentRowCount).Cells(6).Value = dt.Rows(y).Item("Yrlvl").ToString
                    If dt.Rows(y).Item("Gender").ToString = "F" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(3).Value = ""
                        DataGridView1.Rows(CurrentRowCount).Cells(4).Value = "X"
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(3).Value = "X"
                        DataGridView1.Rows(CurrentRowCount).Cells(4).Value = ""
                    End If
                    If IsDBNull(dt.Rows(y).Item("Ched_Code")) = False Then
                        NoOfSubjects.Add(dt.Rows(y).Item("Ched_Code"))
                        NoOfUnits.Add(dt.Rows(y).Item("Subj_Unit"))
                    Else

                    End If
                End If
            Next

            Try
                Dim col As Integer
                Dim cellcol1 As Integer
                Dim cellcol2 As Integer

                If NoOfSubjects.Count <= 5 Then

                    col = 0
                    cellcol1 = 7
                    cellcol2 = 8

                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 1
                    cellcol1 = 9
                    cellcol2 = 10
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 2
                    cellcol1 = 11
                    cellcol2 = 12
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 3
                    cellcol1 = 13
                    cellcol2 = 14
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 4
                    cellcol1 = 15
                    cellcol2 = 16
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                ElseIf NoOfSubjects.Count <= 10 Then

                    col = 0
                    cellcol1 = 7
                    cellcol2 = 8

                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 1
                    cellcol1 = 9
                    cellcol2 = 10
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 2
                    cellcol1 = 11
                    cellcol2 = 12
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 3
                    cellcol1 = 13
                    cellcol2 = 14
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 4
                    cellcol1 = 15
                    cellcol2 = 16
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    DataGridView1.Rows.Add(CurrentSN, CurrentName)
                    CurrentRowCount = DataGridView1.Rows.Count - 1

                    col = 5
                    cellcol1 = 7
                    cellcol2 = 8
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 6
                    cellcol1 = 9
                    cellcol2 = 10
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 7
                    cellcol1 = 11
                    cellcol2 = 12
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 8
                    cellcol1 = 13
                    cellcol2 = 14
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 9
                    cellcol1 = 15
                    cellcol2 = 16
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If


                ElseIf NoOfSubjects.Count <= 15 Then

                    col = 0
                    cellcol1 = 7
                    cellcol2 = 8

                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 1
                    cellcol1 = 9
                    cellcol2 = 10
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 2
                    cellcol1 = 11
                    cellcol2 = 12
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 3
                    cellcol1 = 13
                    cellcol2 = 14
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 4
                    cellcol1 = 15
                    cellcol2 = 16
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    DataGridView1.Rows.Add(CurrentSN, CurrentName)
                    CurrentRowCount = DataGridView1.Rows.Count - 1

                    col = 5
                    cellcol1 = 7
                    cellcol2 = 8
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 6
                    cellcol1 = 9
                    cellcol2 = 10
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 7
                    cellcol1 = 11
                    cellcol2 = 12
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 8
                    cellcol1 = 13
                    cellcol2 = 14
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 9
                    cellcol1 = 15
                    cellcol2 = 16
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    DataGridView1.Rows.Add(CurrentSN, CurrentName)
                    CurrentRowCount = DataGridView1.Rows.Count - 1

                    col = 10
                    cellcol1 = 7
                    cellcol2 = 8
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 11
                    cellcol1 = 9
                    cellcol2 = 10
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 12
                    cellcol1 = 11
                    cellcol2 = 12
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 13
                    cellcol1 = 13
                    cellcol2 = 14
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                    col = 14
                    cellcol1 = 15
                    cellcol2 = 16
                    If NoOfSubjects(col).ToString = "NO CHED CODE" Then
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Style.ForeColor = Color.Red
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    Else
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol1).Value = NoOfSubjects(col).ToString
                        DataGridView1.Rows(CurrentRowCount).Cells(cellcol2).Value = NoOfUnits(col).ToString
                    End If

                End If

            Catch ex As Exception

            End Try


        Next


        'CALCULATION OF UNITS OF STUDENTS
        Dim nonAcadUnits As Integer = 0
        Dim AcadUnits As Integer = 0
        Dim TotalUnits As Integer = 0
        Dim laststoprow As Integer = 0
        Dim SubjCode As String
        For x = 0 To student_numbers.Count - 1
            For y = laststoprow To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(y).Cells(0).Value = student_numbers(x).ToString Then
                    Try
                        SubjCode = DataGridView1.Rows(y).Cells(7).Value
                        If SubjCode.ToString.IndexOf("-") <> 0 Then
                            If SubjCode.ToString.Contains("PE") = True Or SubjCode.ToString.Contains("NSTP") = True Then
                                nonAcadUnits += DataGridView1.Rows(y).Cells(8).Value
                            Else
                                AcadUnits += DataGridView1.Rows(y).Cells(8).Value
                            End If
                            TotalUnits += DataGridView1.Rows(y).Cells(8).Value
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        SubjCode = DataGridView1.Rows(y).Cells(9).Value
                        If SubjCode.ToString.IndexOf("-") <> 0 Then
                            If SubjCode.ToString.Contains("PE") = True Or SubjCode.ToString.Contains("NSTP") = True Then
                                nonAcadUnits += DataGridView1.Rows(y).Cells(10).Value
                            Else
                                AcadUnits += DataGridView1.Rows(y).Cells(10).Value
                            End If
                            TotalUnits += DataGridView1.Rows(y).Cells(10).Value
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        SubjCode = DataGridView1.Rows(y).Cells(11).Value
                        If SubjCode.ToString.IndexOf("-") <> 0 Then
                            If SubjCode.ToString.Contains("PE") = True Or SubjCode.ToString.Contains("NSTP") = True Then
                                nonAcadUnits += DataGridView1.Rows(y).Cells(12).Value
                            Else
                                AcadUnits += DataGridView1.Rows(y).Cells(12).Value
                            End If
                            TotalUnits += DataGridView1.Rows(y).Cells(12).Value
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        SubjCode = DataGridView1.Rows(y).Cells(13).Value
                        If SubjCode.ToString.IndexOf("-") <> 0 Then
                            If SubjCode.ToString.Contains("PE") = True Or SubjCode.ToString.Contains("NSTP") = True Then
                                nonAcadUnits += DataGridView1.Rows(y).Cells(14).Value
                            Else
                                AcadUnits += DataGridView1.Rows(y).Cells(14).Value
                            End If
                            TotalUnits += DataGridView1.Rows(y).Cells(14).Value
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        SubjCode = DataGridView1.Rows(y).Cells(15).Value
                        If SubjCode.ToString.IndexOf("-") <> 0 Then
                            If SubjCode.ToString.Contains("PE") = True Or SubjCode.ToString.Contains("NSTP") = True Then
                                nonAcadUnits += DataGridView1.Rows(y).Cells(16).Value
                            Else
                                AcadUnits += DataGridView1.Rows(y).Cells(16).Value
                            End If
                            TotalUnits += DataGridView1.Rows(y).Cells(16).Value
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    DataGridView1.Rows(y - 1).Cells(17).Value = AcadUnits
                    If nonAcadUnits <> 0 Then
                        DataGridView1.Rows(y - 1).Cells(18).Value = nonAcadUnits
                    End If
                    DataGridView1.Rows(y - 1).Cells(19).Value = TotalUnits
                    laststoprow = y
                    TotalUnits = 0
                    AcadUnits = 0
                    nonAcadUnits = 0
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click        
        MsgBox(DataGridView1.Rows.Count)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles bntPrintBreakdown.Click
        Dim DR As ds_enrollment_list.DT_EnrollmentListRow
        Dim DS As New ds_enrollment_list
        Dim param_aysem As New ReportParameter("aysem", String.Concat(Academic_Sem, " ", Academic_Year))

        DS.Tables("DT_EnrollmentList").Rows.Clear()
        For Each row As DataGridViewRow In DataGridView1.Rows
            With DS.Tables("DT_EnrollmentList")
                DR = .NewRow()
                DR.ELNo = row.Cells(1).Value
                DR.StudentName = row.Cells(2).Value
                DR.Male = row.Cells(3).Value
                DR.Female = row.Cells(4).Value
                DR.Course = row.Cells(5).Value
                DR.Year = row.Cells(6).Value

                DR.Subj1 = row.Cells(7).Value
                DR.Unit1 = row.Cells(8).Value

                DR.Subj2 = row.Cells(9).Value
                DR.Unit2 = row.Cells(10).Value

                DR.Subj3 = row.Cells(11).Value
                DR.Unit3 = row.Cells(12).Value

                DR.Subj4 = row.Cells(13).Value
                DR.Unit4 = row.Cells(14).Value

                DR.Subj5 = row.Cells(15).Value
                DR.Unit5 = row.Cells(16).Value

                DR.Acad = row.Cells(17).Value
                DR.NonAcad = row.Cells(18).Value
                DR.TotalUnits = row.Cells(19).Value
                .Rows.Add(DR)
            End With
        Next

        Dim MyReport As New ReportDataSource("DataSet1", DS.Tables("DT_EnrollmentList"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_enrollment_list.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_aysem})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnPrintSummary.Click
        Dim DR As ds_enrollment_list.DT_EnrollmentListSummaryRow
        Dim DS As New ds_enrollment_list
        Dim DT As New DataTable
        Dim param_aysem As New ReportParameter("aysem", String.Concat(Academic_Sem, " ", Academic_Year))
        DT = GetEnrollmentList_Summary()

        Dim Course As String = "TOTAL SUMMARY"
        Dim Major As String = ""
        Dim Total_FirstYear_Male As Integer = 0
        Dim Total_FirstYear_Female As Integer = 0
        Dim Total_FirstYear_Total As Integer = 0

        Dim Total_SecondYear_Male As Integer = 0
        Dim Total_SecondYear_Female As Integer = 0
        Dim Total_SecondYear_Total As Integer = 0

        Dim Total_ThirdYear_Male As Integer = 0
        Dim Total_ThirdYear_Female As Integer = 0
        Dim Total_ThirdYear_Total As Integer = 0

        Dim Total_FourthYear_Male As Integer = 0
        Dim Total_FourthYear_Female As Integer = 0
        Dim Total_FourthYear_Total As Integer = 0

        Dim GrandTotal_Total As Integer = 0

        DS.Tables("DT_EnrollmentListSummary").Rows.Clear()
        With DS.Tables("DT_EnrollmentListSummary")
            For Each row As ds_enrollment_list.DT_EnrollmentListSummaryRow In DT.Rows
                DR = .NewRow
                DR("Course") = row.Course
                DR("Major") = ""

                DR("frstyear_male") = row.frstyear_male
                Total_FirstYear_Male += row.frstyear_male
                DR("frstyear_Female") = row.frstyear_female
                Total_FirstYear_Female += row.frstyear_female
                DR("frstyear_total") = row.frstyear_total
                Total_FirstYear_Total += row.frstyear_total

                DR("scndyear_male") = row.scndyear_male
                Total_SecondYear_Male += row.scndyear_male
                DR("scndyear_Female") = row.scndyear_female
                Total_SecondYear_Female += row.scndyear_female
                DR("scndyear_total") = row.scndyear_total
                Total_SecondYear_Total += row.scndyear_total

                DR("thrdyear_male") = row.thrdyear_male
                Total_ThirdYear_Male += row.thrdyear_male
                DR("thrdyear_Female") = row.thrdyear_female
                Total_ThirdYear_Female += row.thrdyear_female
                DR("thrdyear_total") = row.thrdyear_total
                Total_ThirdYear_Total += row.thrdyear_total

                DR("frthyear_male") = row.frthyear_male
                Total_FourthYear_Male += row.frthyear_male
                DR("frthyear_Female") = row.frthyear_female
                Total_FourthYear_Female += row.frthyear_female
                DR("frthyear_total") = row.frthyear_total
                Total_FourthYear_Total += row.frthyear_total

                DR("GrandTotal") = row.GrandTotal
                GrandTotal_Total += row.GrandTotal

                .Rows.Add(DR)
            Next
        End With

        With DS.Tables("DT_EnrollmentListSummary")
            DR = .NewRow
            DR("Course") = Course
            DR("Major") = Major

            DR("frstyear_male") = Total_FirstYear_Male
            DR("frstyear_Female") = Total_FirstYear_Female
            DR("frstyear_total") = Total_FirstYear_Total

            DR("scndyear_male") = Total_SecondYear_Male
            DR("scndyear_Female") = Total_SecondYear_Female
            DR("scndyear_total") = Total_SecondYear_Total

            DR("thrdyear_male") = Total_ThirdYear_Male
            DR("thrdyear_Female") = Total_ThirdYear_Female
            DR("thrdyear_total") = Total_ThirdYear_Total

            DR("frthyear_male") = Total_FourthYear_Male
            DR("frthyear_Female") = Total_FourthYear_Female
            DR("frthyear_total") = Total_FourthYear_Total

            DR("GrandTotal") = GrandTotal_Total

            .Rows.Add(DR)
        End With


        Dim MyReport As New ReportDataSource("DataSet2", DS.Tables("DT_EnrollmentListSummary"))
        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(MyReport)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_enrollment_list_summary.rdlc"
            .ReportViewer1.LocalReport.SetParameters({param_aysem})
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub
End Class