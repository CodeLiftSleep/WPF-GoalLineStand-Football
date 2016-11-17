Imports System.Data.SqlClient

''' <summary>
'''     For use with SQL Server---Deprecated as we have switched over to SQLite
''' </summary>
Public Class DataFunctions
    Private Function GetConnectionString()
        Return "Data Source=MNasty-PC;Initial Catalog=FootballData;User ID=MNasty-PC\MNasty;Integrated Security=True"
    End Function
    'Loads DT into memory
    Public Sub LoadTable(DT As DataTable, TableName As String)
        Dim Conn = New SqlConnection
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = GetConnectionString()
            Conn.Open()
        Else
            Conn.ConnectionString = GetConnectionString()
        End If
        Dim Cmd = New SqlCommand("Select * From " & TableName & "")
        Dim DA = New SqlDataAdapter(Cmd)
        DA.SelectCommand.Connection = Conn
        DA.Fill(DT)
        Conn.Close()
    End Sub
    'Updates Data Into a Table that has not been modified except by inserting data into the columns
    Public Sub UpdateTable(DT As DataTable, TableName As String)

        Dim Conn = New SqlConnection
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = GetConnectionString()
            Conn.Open()
        Else
            Conn.ConnectionString = GetConnectionString()
        End If

        Dim SQL = New SqlCommand("Select * From " & TableName & "")

        'For row As Integer = 1 To DT.Rows.Count - 1
        'For column As Integer = 0 To DT.Columns.Count - 1
        'Console.WriteLine(DT.Rows(row).Item(column))
        'Next column
        'Next row

        Dim DA = New SqlDataAdapter(SQL)
        DA.SelectCommand.Connection = Conn
        Dim CMD = New SqlCommandBuilder(DA)

        CMD.GetUpdateCommand()
        DA.ContinueUpdateOnError = True
        DA.Update(DT)
        Conn.Close()
    End Sub

    Public Sub DeleteTable(DT As DataTable, TableName As String)

        Dim Conn = New SqlConnection
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = GetConnectionString()
            Conn.Open()
        Else
            Conn.ConnectionString = GetConnectionString()
        End If

        Dim strSQL As String
        strSQL = "TRUNCATE TABLE " & TableName & ""
        Using connection As New SqlConnection(Conn.ConnectionString)
            Dim cmd As New SqlCommand(strSQL, connection)
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub CreateTable(DT As DataTable, TableName As String, SQLFieldNames As String)

        Dim Conn = New SqlConnection
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = GetConnectionString()
            Conn.Open()
        Else
            Conn.ConnectionString = GetConnectionString()
        End If

        Dim restrictions(3) As String
        restrictions(2) = TableName
        Dim dbTbl As DataTable = Conn.GetSchema("Tables", restrictions)

        If dbTbl.Rows.Count = 0 Then 'Checks to see if Table Exists or not.  
            'Table does not exist---makes new table
            Dim strSQL As String = "CREATE TABLE " & TableName & " ( " & SQLFieldNames & " )"

            Using connection As New SqlConnection(Conn.ConnectionString)
                Dim cmd As New SqlCommand(strSQL, connection)
                cmd.Connection.Open()
                cmd.ExecuteNonQuery()
            End Using
        Else
            'Table exists---exits function.
            Exit Sub
        End If
        dbTbl.Dispose()
        Conn.Close()
        Conn.Dispose()
    End Sub


    Public Sub AlterTable(DT As DataTable, TableName As String, SQLAlterString As String)

        Dim Conn = New SqlConnection
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = GetConnectionString()
            Conn.Open()
        Else
            Conn.ConnectionString = GetConnectionString()
        End If

        Dim StrSQL As String = "ALTER TABLE " & TableName & " " & SQLAlterString & ""
        Using connection As New SqlConnection(Conn.ConnectionString)
            Dim cmd As New SqlCommand(StrSQL, connection)
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
End Class
