Imports System.Data.SQLite
Imports System.IO
Imports System.Windows.Controls
Imports System.Drawing
Imports System.Windows.Interop
Imports System.Windows

Public Class SQLiteDataFunctions
    'Dim Conn As SQLite.SQLiteConnection = New SQLite.SQLiteConnection()
    Dim filepath As String = "Project_Files/"

    ''' <summary>
    '''     Opens a connection to this DB--Must Explicitly pass the connection string before returning it, otherwise it will
    '''     throw a Null exception as the connection string will be Nothing.
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <returns></returns>
    Public Function GetConnectionString(DBName As String, Conn As SQLiteConnection,
                                        Optional ByVal MyFilePath As String = "") As String

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
    '''     check to see if the table exists, if not then create one
    ''' </summary>
    ''' <param name="DBName"></param>
    Public Sub CreateTable(DBName As String, DT As DataTable, TableName As String, SQLFieldNames As String,
                           Optional ByVal MyFilePath As String = "")
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
                Dim SQLCmd = New SQLiteCommand(SQL, Conn)
                SQLCmd.ExecuteNonQuery()
                Conn.Close()

            Catch ex As SQLiteException
                Console.WriteLine(ex.Data)
                Console.WriteLine(ex.Message)
            End Try
        End Using
    End Sub

    ''' <summary>
    '''     Loads a Table from the DB.  If the table does not exist, it creates one
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <param name="TableName"></param>
    Public Sub LoadTable(DBName As String, DT As DataTable, TableName As String,
                         Optional ByVal MyFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(DBName, Conn, MyFilePath)
        Using Conn
            Dim SQLCmd = New SQLiteCommand("Select * From " & TableName)
            Dim DA = New SQLiteDataAdapter(SQLCmd)
            DA.SelectCommand.Connection = Conn
            DA.Fill(DT)
            Conn.Close()
        End Using
    End Sub

    ''' <summary>
    '''     This will Keep the Actual Table, but simply Delete all the records in it
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <param name="DT"></param>
    ''' <param name="TableName"></param>
    Public Sub DeleteTable(DBName As String, DT As DataTable, TableName As String,
                           Optional ByVal MyFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(DBName, Conn, MyFilePath)
        Using Conn
            Dim SQL As String = "DELETE FROM " & TableName
            Dim cmd As New SQLiteCommand(SQL, Conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ''' <summary>
    '''     This completely removes the Table from the Database
    ''' </summary>
    ''' <param name="DBNAme"></param>
    ''' <param name="DT"></param>
    ''' <param name="TableName"></param>
    Public Sub DropTable(DBName As String, DT As DataTable, TableName As String,
                         Optional ByVal MyFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(DBName, Conn, MyFilePath)
        Using Conn
            Dim SQL As String = "DROP TABLE " & TableName
            Dim cmd As New SQLiteCommand(SQL, Conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ''' <summary>
    '''     Since there is no UpdateTable command like SQL Server, Bulk Insert Into will put all the rows from the DT into the
    '''     DB
    '''     optimized to do it in a single transaction instead of every row
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <param name="DT"></param>
    ''' <param name="TableName"></param>
    Public Sub BulkInsert(DBName As String, DT As DataTable, TableName As String,
                          Optional ByVal MyFilePath As String = "")
        Dim MyConn As New SQLiteConnection()
        Dim MyList = New List(Of String)
        GetConnectionString(DBName, MyConn, MyFilePath)
        Using MyConn
            Using SQLCmd = New SQLiteCommand(MyConn)
                Using transaction = MyConn.BeginTransaction() 'This begins the bulk insert
                    Try
                        For row = 1 To DT.Rows.Count - 1 'cycle through each row
                            For Col = 0 To DT.Columns.Count - 1
                                MyList.Add(DT.Rows(row).Item(DT.Columns.Item(Col)))
                            Next Col
                            Dim sql = String.Format("INSERT INTO " & TableName & " VALUES ({0});",
                                                    String.Join(", ", MyList))
                            SQLCmd.CommandText = sql
                            SQLCmd.ExecuteNonQuery()
                            MyList.Clear()
                        Next row
                        transaction.Commit() 'commits all changes to the DB
                    Catch ex As InvalidOperationException
                        Console.WriteLine(ex.ToString)
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            MyConn.Close()
        End Using
    End Sub

    ''' <summary>
    ''' Uses the Filterstring  to hold a list of Columns to Filter by, if all columns are wanted put a * as the only item
    ''' in the string.  Dataview.ToTable method accepts a string array for column names, which corresponds to the ColNames variable
    ''' </summary>
    ''' <param name="OrigDT"></param>
    ''' <param name="NewDT"></param>
    ''' <param name="FilterString"></param>
    ''' <param name="ColNames"></param>
    ''' <param name="SortBy"></param>
    ''' <returns></returns>
    Public Shared Function FilterTable(OrigDT As DataTable, NewDT As DataTable, FilterString As String, ColNames As String(), SortBy As String) _
        As DataTable
        Dim FoundRows() As DataRow
        Dim MyTempDT As New DataTable

        FoundRows = (OrigDT.Select(FilterString, SortBy, DataViewRowState.CurrentRows))
        MyTempDT = FoundRows.CopyToDataTable()
        Dim MyView As New DataView(MyTempDT)

        NewDT = MyView.ToTable(False, ColNames)
        Return NewDT
    End Function
    Public Shared Sub GetColumnNames(ByVal DT As DataTable, ByVal ParamArray args() As String)
        For i = 0 To args.Count - 1
            DT.Columns.Add(args(i))
        Next i
    End Sub

    Public Shared Function BitMapToImage(ByVal BM As Bitmap) As Windows.Media.ImageSource
        Dim myImgSrc As New Windows.Media.ImageSourceConverter
        Dim byteArr() As Byte

        Using Stream As New MemoryStream()
            BM.Save(Stream, System.Drawing.Imaging.ImageFormat.Bmp)
            Stream.Close()
            byteArr = Stream.ToArray()
        End Using
        Return DirectCast(myImgSrc.ConvertFrom(byteArr), Windows.Media.ImageSource)

    End Function
End Class