Module Program_Loaders
    Public Sub Save_Logs(txt As String)
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("INSERT INTO TBL_PROGRAM_LOGS VALUES (@logs,GETDATE(),@user)", conn)
                comm.Parameters.AddWithValue("@logs", txt)
                comm.Parameters.AddWithValue("@user", Account_Name)
                comm.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub RecordErros(FormLocation As String, ByVal ErrorOccured As String)
        'Using conn As New SqlConnection(StringConnection)
        '    conn.Open()
        '    Using comm As New SqlCommand("INSERT INTO TBL_PROGRAM_USERS VALUES(GETDATE(),@formlocation,@error,@user)", conn)
        '        comm.Parameters.AddWithValue("@formlocation", FormLocation)
        '        comm.Parameters.AddWithValue("@error", ErrorOccured)
        '        comm.Parameters.AddWithValue("@user", Account_Name)
        '        comm.ExecuteNonQuery()
        '    End Using
        'End Using
    End Sub

    Public Function Create_Statement(sn As String, TransCode As String, TransType As String, TransDesc As String, TransDate As DateTime, Debit As Double, Credit As Double) As Boolean
        Dim Is_Saved As Boolean = False
        Try
            Using conn As New SqlConnection(StringConnection)
                conn.Open()
                Using comm As New SqlCommand("INSERT INTO TBL_STATEMENT_OF_ACCOUNT VALUES(@sn,@transcode,@transtype,@transdesc,@transdate,@debit,@credit,@ay,@sem)", conn)
                    comm.Parameters.AddWithValue("@sn", sn)
                    comm.Parameters.AddWithValue("@transcode", TransCode)
                    comm.Parameters.AddWithValue("@transtype", TransType)
                    comm.Parameters.AddWithValue("@transdesc", TransDesc)
                    comm.Parameters.AddWithValue("@transdate", TransDate)
                    comm.Parameters.AddWithValue("@debit", Debit)
                    comm.Parameters.AddWithValue("@credit", Credit)
                    comm.Parameters.AddWithValue("@ay", Academic_Year)
                    comm.Parameters.AddWithValue("@sem", Academic_Sem)
                    If Val(comm.ExecuteScalar) = 1 Then
                        Is_Saved = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Is_Saved
    End Function

    Public Function Get_Section_Count(course As String, yrlvl As String, section_code As String)
        Dim Count As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT COUNT(ID) FROM TBL_COLLEGE_ASSESSMENT_SUMMARY WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND SECT_CODE = @section_code", conn)
                comm.Parameters.AddWithValue("@course_code", course)
                comm.Parameters.AddWithValue("@yrlvl", yrlvl)
                comm.Parameters.AddWithValue("@section_code", section_code)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Count = Val(comm.ExecuteScalar)
            End Using
        End Using
        Return Count
    End Function

    Public Sub Load_Course_Codes(cmb As ComboBox)
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_COURSES ORDER BY DEPARTMENT,COURSE_CODE ASC", conn)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmb.Items.Clear()
                    While reader.Read
                        cmb.Items.Add(reader("COURSE_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub Load_YearLvls(education_level As String, course As String, cmb As ComboBox)
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT * FROM TBL_SETTINGS_COLLEGE_YEARLEVELS WHERE COURSE_CODE = @course_code AND EDUCATION_LEVEL = @education_level ORDER BY ORDER_NO ASC", conn)
                comm.Parameters.AddWithValue("@course_code", course)
                comm.Parameters.AddWithValue("@education_level", education_level)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmb.Items.Clear()
                    While reader.Read
                        cmb.Items.Add(reader("YEAR_CODE"))
                    End While
                End Using
            End Using
        End Using
    End Sub

    Public Sub Load_Sections(Education_Level As String, course As String, yrlvl As String, cmb As ComboBox)
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Dim SQLQuery As String = String.Empty
            SQLQuery = "SELECT * FROM TBL_SETTINGS_SECTIONS WHERE COURSE_CODE = @course_code AND YRLVL = @yrlvl AND EDUCATION_LEVEL = @education_level AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem ORDER BY ID ASC"
            Using comm As New SqlCommand(SQLQuery, conn)
                comm.Parameters.AddWithValue("@course_code", course)
                comm.Parameters.AddWithValue("@yrlvl", yrlvl)
                comm.Parameters.AddWithValue("@education_level", Education_Level)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Using reader As SqlDataReader = comm.ExecuteReader
                    cmb.Items.Clear()
                    While reader.Read
                        cmb.Items.Add(reader("SECTION_CODE"))
                    End While
                End Using
            End Using
        End Using

        If Education_Level = "COLLEGE" Then
            cmb.Items.Add("IRREGULAR")
        End If

    End Sub

    Public Function Get_SectionID(section_code As String, course As String, yrlvl As String)
        Dim Section_ID As String = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT ID FROM TBL_SETTINGS_SECTIONS WHERE SECTION_CODE = @section_code AND COURSE_CODE = @course_code AND YRLVL  = @yrlvl AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@section_code", section_code)
                comm.Parameters.AddWithValue("@course_code", course)
                comm.Parameters.AddWithValue("@yrlvl", yrlvl)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
                Section_ID = comm.ExecuteScalar
            End Using
        End Using

        If Section_ID Is Nothing Then
            Section_ID = section_code
        End If
        Return Section_ID
    End Function

    Public Function Get_SectionName(section_code As String, course As String, yrlvl As String)
        Dim Section_Name As String = "-"
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT SECTION_CODE FROM TBL_SETTINGS_SECTIONS WHERE SECTION_CODE = @section_code AND COURSE_CODE = @course_code AND YRLVL  = @yrlvl AND ACADEMIC_YEAR = @ay AND ACADEMIC_SEM = @sem", conn)
                comm.Parameters.AddWithValue("@section_code", section_code)
                comm.Parameters.AddWithValue("@course_code", course)
                comm.Parameters.AddWithValue("@yrlvl", yrlvl)
                comm.Parameters.AddWithValue("@ay", Academic_Year)
                comm.Parameters.AddWithValue("@sem", Academic_Sem)
            End Using
        End Using
        Return Section_Name
    End Function

    Public Function GetInsertedID(tblname As String)
        Dim InsertedID As Integer = 0
        Using conn As New SqlConnection(StringConnection)
            conn.Open()
            Using comm As New SqlCommand("SELECT IDENT_CURRENT(@tablename)", conn)
                comm.Parameters.AddWithValue("@tablename", tblname)
                InsertedID = comm.ExecuteScalar
            End Using
        End Using
        Return InsertedID
    End Function
End Module
