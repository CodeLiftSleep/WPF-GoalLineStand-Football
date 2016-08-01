Imports System.Data.SQLite
Imports System.Collections
Imports System.Text
Imports System.IO

Public Class SQLiteDataFunctions
    'Dim Conn As SQLite.SQLiteConnection = New SQLite.SQLiteConnection()
    Dim filepath As String = "Project Files/"
    ''' <summary>
    ''' Opens a connection to this DB--Must Explicitly pass the connection string before returning it, otherwise it will throw a Null exception as the connection string will be Nothing.
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <returns></returns>
    Public Function GetConnectionString(ByVal DBName As String, ByVal Conn As SQLiteConnection, Optional ByVal MyFilePath As String = "") As String

        If MyFilePath <> "" Then
            filepath = MyFilePath 'changes the filepath if a new location is supplied
        End If
        Conn.Close()
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = "Data Source=" & filepath & DBName & ".sqlite;Version=3"
            '= "Data Source=|DataDirectory|\" & DBName & ".sqlite;Version=3"
            Conn.Open()
            Return Conn.ConnectionString
        Else
            Conn.ConnectionString = "Data Source=" & filepath & DBName & ".sqlite;Version=3"
        End If

    End Function
    ''' <summary>
    ''' check to see if the table exists, if not then create one
    ''' </summary>
    ''' <param name="DBName"></param>
    Public Sub CreateTable(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String, ByVal SQLFieldNames As String, Optional ByVal MyFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(DBName, Conn, MyFilePath)
        Conn.Close()

        Using Conn
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            'Checks to see if the Table exists, if not create the table
            Try
                Dim SQL As String = ("CREATE TABLE If Not EXISTS " & TableName & "(" & SQLFieldNames & ")")
                Dim SQLCmd As SQLiteCommand = New SQLiteCommand(SQL, Conn)
                SQLCmd.ExecuteNonQuery()
                Conn.Close()

            Catch ex As System.Data.SQLite.SQLiteException
                Console.WriteLine(ex.Data)
                Console.WriteLine(ex.Message)
            End Try
        End Using
    End Sub
    ''' <summary>
    ''' Loads a Table from the DB.  If the table does not exist, it creates one
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <param name="TableName"></param>
    Public Sub LoadTable(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String, Optional ByVal MyFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(DBName, Conn, MyFilePath)
        Using Conn
            Dim SQLCmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand("Select * From " & TableName)
            Dim DA As SQLite.SQLiteDataAdapter = New SQLiteDataAdapter(SQLCmd)
            DA.SelectCommand.Connection = Conn
            DA.Fill(DT)
            Conn.Close()
        End Using
    End Sub
    ''' <summary>
    ''' This will Keep the Actual Table, but simply Delete all the records in it
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <param name="DT"></param>
    ''' <param name="TableName"></param>
    Public Sub DeleteTable(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String, Optional ByVal MyFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(DBName, Conn, MyFilePath)
        Using Conn
            Dim SQL As String = "DELETE FROM " & TableName
            Dim cmd As New SQLite.SQLiteCommand(SQL, Conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    ''' <summary>
    ''' This completely removes the Table from the Database
    ''' </summary>
    ''' <param name="DBNAme"></param>
    ''' <param name="DT"></param>
    ''' <param name="TableName"></param>
    Public Sub DropTable(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String, Optional ByVal MyFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(DBName, Conn, MyFilePath)
        Using Conn
            Dim SQL As String = "DROP TABLE " & TableName
            Dim cmd As New SQLite.SQLiteCommand(SQL, Conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    ''' <summary>
    ''' Since there is no UpdateTable command like SQL Server, Bulk Insert Into will put all the rows from the DT into the DB
    ''' optimized to do it in a single transaction instead of every row
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <param name="DT"></param>
    ''' <param name="TableName"></param>
    Public Sub BulkInsert(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String, Optional ByVal MyFilePath As String = "")
        Dim MyConn As New SQLiteConnection()
        Dim MyList As List(Of String) = New List(Of String)
        GetConnectionString(DBName, MyConn, MyFilePath)
        Using MyConn
            Using SQLCmd = New SQLiteCommand(MyConn)
                Using transaction = MyConn.BeginTransaction() 'This begins the bulk insert
                    Try
                        For row As Integer = 0 To DT.Rows.Count - 1 'cycle through each row
                            For Col As Integer = 0 To DT.Columns.Count - 1
                                MyList.Add(DT.Rows(row).Item(DT.Columns.Item(Col)))
                            Next Col
                            Dim sql = String.Format("INSERT INTO " & TableName & " VALUES ({0});", String.Join(", ", MyList))
                            SQLCmd.CommandText = sql
                            SQLCmd.ExecuteNonQuery()
                            MyList.Clear()
                        Next row
                        transaction.Commit() 'commits all changes to the DB
                    Catch ex As System.InvalidOperationException
                        Console.WriteLine(ex.ToString)
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            MyConn.Close()
        End Using
    End Sub
    ''' <summary>
    ''' Uses the Filterstring  to hold a list of Columns to Filter by, if all columns are wanted put a * as the only item in the string
    ''' </summary>
    ''' <param name="OrigDT"></param>
    ''' <param name="NewDT"></param>
    ''' <param name="FilterString"></param>
    ''' <param name="SortBy"></param>
    ''' <returns></returns>
    Public Function FilterTable(ByVal OrigDT As DataTable, ByVal NewDT As DataTable, ByVal FilterString As String, ByVal SortBy As String) As DataTable
        Dim FoundRows() As DataRow
        Dim MyTempDT As New DataTable

        FoundRows = (OrigDT.Select(FilterString, SortBy, DataViewRowState.CurrentRows))
        MyTempDT = FoundRows.CopyToDataTable()
        Dim MyView As New DataView(MyTempDT)

        NewDT = MyView.ToTable(False, "FName", "LName", "College", "Age", "DOB", "Height", "Weight", "Pos", "PosType", "FortyYardTime", "Dominant", "Weakest")
        Return NewDT
    End Function

    ''' <summary>
    ''' Takes in the name of the file, ColumnName str array, and a DT.  Reads each line of the file, splits it by a ";" and then adds in the array to the DT
    ''' after calling the GetColumnNames Sub to take the parameter array and create columns. Optional Parameters for DateFormatting string and the index which which to apply it
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <param name="ColumnNames"></param>
    ''' <param name="DT"></param>
    ''' <param name="DateFormatPattern"></param>
    ''' <param name="DateFormatIndex"></param>
    Public Shared Sub ReadFile(ByVal FileName As String, ByVal ColumnNames() As String, ByVal DT As DataTable, Optional DateFormatPattern As String = "",
                         Optional DateFormatIndex As Integer = -1, Optional OrderBy As String = "")
        Dim str As String = ""
        Dim myarray() As String

        GetColumnNames(DT, ColumnNames)
        'Automatically dispose of the StreamReader once its completed.
        Using myreader As New StreamReader(FileName)
            'First Line contains information about formatting and not actual content
            myreader.ReadLine()
            While myreader.EndOfStream = False
                str = myreader.ReadLine()
                myarray = str.Split(";")
                'Checks to see if there is a Date involved and if there is, formats it to its proper form before returning it back to the array
                If DateFormatPattern <> "" Then
                    Dim MyDate As New Date
                    MyDate = myarray(DateFormatIndex)
                    myarray(DateFormatIndex) = MyDate.ToString(DateFormatPattern)
                End If

                'Adds the array to the data row and sorts it according to the parameter supplied
                DT.Rows.Add().ItemArray = myarray
                DT.Select("", OrderBy, DataViewRowState.CurrentRows)
            End While
        End Using
    End Sub

    Public Shared Sub GetColumnNames(ByVal DT As DataTable, ByVal ParamArray args() As String)
        For i = 0 To args.Count - 1
            DT.Columns.Add(args(i))
        Next i
    End Sub
End Class