Imports MySql.Data.MySqlClient

Module ModDatabase
    Public dbServer As String
    Public dbUser As String
    Public dbPassword As String
    Public dbName As String
    Public sLocalConn As String


    'You can use this procedure when you use dynamic connection using my.setting
    'Public Sub GetDatabaseSetting()
    '    dbServer = My.Settings.dbServer
    '    dbUser = My.Settings.dbUser
    '    dbPassword = My.Settings.dbPassword
    '    dbName = My.Settings.dbName
    '    sLocalConn = "server=" & dbServer & ";user id=" & dbUser & ";" &
    '                 "password=" & dbPassword & ";database=" & dbName & ""
    'End Sub

    ''' <summary>
    ''' This Procedure is used when you hard code your connection setting.
    ''' </summary>
    Public Sub GetDatabaseSetting()
        dbServer = "localhost"
        dbUser = "root"
        dbPassword = ""
        dbName = "db_laundrybagus"

        'this should the default setting
        'sLocalConn = "server=" & dbServer & ";user id=" & dbUser & ";" & _
        '     "password=" & dbPassword & ";database=" & dbName & ";"

        'if you're experience error: "open connect the given key was not present in the dictionary."
        'Adding "Charset=utf8;" would work
        sLocalConn = "server=" & dbServer & ";user id=" & dbUser & ";" & _
                     "password=" & dbPassword & ";database=" & dbName & ";Charset=utf8;"

    End Sub

    ''' <summary>
    ''' This is to get query result into DataTable
    ''' </summary>
    ''' <param name="SQL"></param>
    ''' <returns></returns>
    Public Function GetDataTable(ByVal SQL As String) As DataTable
        Dim conn As MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapt As New MySqlDataAdapter
        Dim dt As New DataTable

        conn = New MySqlConnection()
        conn.ConnectionString = sLocalConn
        Try
            conn.Open()

            cmd.Connection = conn
            cmd.CommandText = SQL

            adapt.SelectCommand = cmd
            adapt.Fill(dt)

            conn.Close()
            Return dt
        Catch myerror As MySqlException
            MessageBox.Show("Error: " & myerror.Message)
        Finally
            conn.Dispose()
        End Try
        Return (Nothing)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="FieldName"></param>
    ''' <param name="TableName"></param>
    ''' <param name="WhereClause"></param>
    ''' <param name="DefaultValue"></param>
    ''' <returns></returns>
    Public Function GetFieldValue(ByVal FieldName As String, ByVal TableName As String, ByVal WhereClause As String, ByVal DefaultValue As String) As String
        Dim conn As MySqlConnection
        Dim cmd As New MySqlCommand
        Dim objValue As Object

        conn = New MySqlConnection()
        conn.ConnectionString = sLocalConn
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT " & FieldName & " FROM " & TableName & " " & WhereClause & " LIMIT 1"
            objValue = cmd.ExecuteScalar()
            conn.Close()
            If objValue Is Nothing Then
                Return DefaultValue
            ElseIf IsDBNull(objValue) Then
                Return DefaultValue
            Else
                Return objValue.ToString
            End If
        Catch myerror As MySqlException
            MessageBox.Show("Error: " & myerror.Message)
        Finally
            conn.Dispose()
        End Try
        Return DefaultValue
    End Function

    Public Sub GetComboDataWithArray(ByVal SQL As String, ByRef cbo As ComboBox, _
                                     Optional ByRef Arr1() As String = Nothing, _
                                     Optional ByRef Arr2() As String = Nothing, _
                                     Optional ByRef Arr3() As String = Nothing)
        Dim conn As MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        Dim tblData As New DataTable
        Dim i As Long = 0

        conn = New MySqlConnection()
        conn.ConnectionString = sLocalConn
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL

            adpt.SelectCommand = cmd
            adpt.Fill(tblData)
            conn.Close()

            cbo.DataSource = tblData
            cbo.DisplayMember = tblData.Columns(0).ColumnName
            cbo.ValueMember = tblData.Columns(1).ColumnName

            If tblData.Columns.Count > 2 Then
                ReDim Arr1(tblData.Rows.Count)
                For i = 0 To tblData.Rows.Count - 1
                    Arr1(i) = tblData.Rows(i).Item(2).ToString
                Next
            End If

            If tblData.Columns.Count > 3 Then
                ReDim Arr2(tblData.Rows.Count)
                For i = 0 To tblData.Rows.Count - 1
                    Arr2(i) = tblData.Rows(i).Item(3).ToString
                Next
            End If

            If tblData.Columns.Count > 4 Then
                ReDim Arr3(tblData.Rows.Count)
                For i = 0 To tblData.Rows.Count - 1
                    Arr3(i) = tblData.Rows(i).Item(4).ToString
                Next
            End If

        Catch myerror As MySqlException
            MessageBox.Show("Error: " & myerror.Message)
        Finally
            conn.Dispose()
        End Try
    End Sub

    Public Function RunCommand(ByVal SQL As String) As Boolean
        Dim conn As MySqlConnection
        Dim cmd As New MySqlCommand

        conn = New MySqlConnection()
        conn.ConnectionString = sLocalConn
        Try
            conn.Open()

            cmd.Connection = conn
            cmd.CommandText = SQL
            cmd.ExecuteNonQuery()

            conn.Close()
            Return True
        Catch myerror As MySqlException
            Throw myerror
            Return False
        Finally
            conn.Dispose()
        End Try
        Return False
    End Function

    Public Function GetDataRowArray(ByVal SQL As String) As String()
        Dim conn As MySqlConnection
        Dim cmd As New MySqlCommand
        Dim reader As MySqlDataReader
        Dim str As String = String.Empty
        conn = New MySqlConnection()
        conn.ConnectionString = sLocalConn

        Try
            conn.Open()
            cmd = New MySqlCommand(SQL, conn)
            reader = cmd.ExecuteReader()

            Dim strArray() As String = Nothing
            If reader.Read Then
                ReDim strArray(reader.FieldCount)
                For i As Integer = 0 To reader.FieldCount - 1
                    strArray(i) = str & reader.Item(i).ToString
                Next
            End If

            reader.Close()
            cmd.Dispose()
            conn.Close()

            Return strArray
        Catch myerror As MySqlException
            MessageBox.Show("Error: " & myerror.Message)
            Return Nothing
        Finally
            conn.Dispose()
        End Try
        Return Nothing
    End Function

    Public Function GetDataColumnArray(ByVal SQL As String) As String()
        Dim conn As MySqlConnection
        Dim cmd As New MySqlCommand
        Dim reader As MySqlDataReader
        Dim str As String = String.Empty
        Dim i As Integer
        conn = New MySqlConnection()
        conn.ConnectionString = sLocalConn

        Try
            conn.Open()
            cmd = New MySqlCommand(SQL, conn)
            reader = cmd.ExecuteReader()

            Do While reader.Read
                str = str & reader.Item(i).ToString & ","
            Loop
            str = Left(str, Len(str) - 1)
            Dim strArray() As String
            strArray = Split(str, ",")

            reader.Close()
            cmd.Dispose()
            conn.Close()

            Return strArray
        Catch myerror As MySqlException
            MessageBox.Show("Error: " & myerror.Message)
            Return Nothing
        Finally
            conn.Dispose()
        End Try
        Return Nothing
    End Function
End Module
