Imports System.Data.SQLite
Imports System.IO
Imports System.Windows.Controls
Imports System.Drawing
Imports System.Windows.Interop
Imports System.Windows

Public Class SQLiteDataFunctions
    'Dim Conn As SQLite.SQLiteConnection = New SQLite.SQLiteConnection()
    Dim FilePath As String = "Project_Files/"

    ''' <summary>
    '''     Opens a connection to this DB--Must Explicitly pass the connection string before returning it, otherwise it will
    '''     throw a Null exception as the connection string will be Nothing.
    ''' </summary>
    ''' <param name="dbName"></param>
    ''' <returns></returns>
    Public Function GetConnectionString(dbName As String, conn As SQLiteConnection,
                                        Optional ByVal myFilePath As String = "") As String

        If myFilePath <> "" Then
            FilePath = myFilePath 'changes the filepath if a new location is supplied
        End If
        conn.Close()
        If conn.State <> ConnectionState.Open Then
            conn.ConnectionString = "Data Source=" & FilePath & dbName & ".sqlite;Version=3"
            '= "Data Source=|DataDirectory|\" & DBName & ".sqlite;Version=3"
            conn.Open()
            Return conn.ConnectionString
        Else
            conn.ConnectionString = "Data Source=" & FilePath & dbName & ".sqlite;Version=3"
        End If
    End Function

    ''' <summary>
    '''     check to see if the table exists, if not then create one
    ''' </summary>
    ''' <param name="dbName"></param>
    Public Sub CreateTable(dbName As String, dt As DataTable, tableName As String, sqlFieldNames As String,
                           Optional ByVal myFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(dbName, Conn, myFilePath)
        Conn.Close()

        Using Conn
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            'Checks to see if the Table exists, if not create the table
            Try
                Dim SQL As String = ("CREATE TABLE If Not EXISTS " & tableName & "(" & sqlFieldNames & ")")
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
    ''' <param name="dbName"></param>
    ''' <param name="tableName"></param>
    Public Sub LoadTable(dbName As String, dt As DataTable, tableName As String,
                         Optional ByVal myFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(dbName, Conn, myFilePath)
        Using Conn
            Dim SQLCmd = New SQLiteCommand("Select * From " & tableName)
            Dim DA = New SQLiteDataAdapter(SQLCmd)
            DA.SelectCommand.Connection = Conn
            DA.Fill(dt)
            Conn.Close()
        End Using
    End Sub

    ''' <summary>
    '''     This will Keep the Actual Table, but simply Delete all the records in it
    ''' </summary>
    ''' <param name="dbName"></param>
    ''' <param name="dt"></param>
    ''' <param name="tableName"></param>
    Public Sub DeleteTable(dbName As String, tableName As String,
                           Optional ByVal MyFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(dbName, Conn, MyFilePath)
        Using Conn
            Dim SQL As String = "DELETE FROM " & tableName
            Dim Cmd As New SQLiteCommand(SQL, Conn)
            Cmd.ExecuteNonQuery()
        End Using
    End Sub

    ''' <summary>
    '''     This completely removes the Table from the Database
    ''' </summary>
    ''' <param name="dbName"></param>
    ''' <param name="dt"></param>
    ''' <param name="tableName"></param>
    Public Sub DropTable(dbName As String, dt As DataTable, tableName As String,
                         Optional ByVal MyFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(dbName, Conn, MyFilePath)
        Using Conn
            Dim SQL As String = "DROP TABLE " & tableName
            Dim Cmd As New SQLiteCommand(SQL, Conn)
            Cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub InsertInto(dbName As String, tableName As String, SQLCmd As String, Optional ByVal myFilePath As String = "")
        Dim Conn As New SQLiteConnection()
        GetConnectionString(dbName, Conn, myFilePath)
        Using Conn
            Dim SQL As String = SQLCmd
            Dim Cmd As New SQLiteCommand(SQL, Conn)
            Cmd.ExecuteNonQuery()
        End Using
    End Sub
    ''' <summary>
    '''     Since there is no UpdateTable command like SQL Server, Bulk Insert Into will put all the rows from the DT into the
    '''     DB
    '''     optimized to do it in a single transaction instead of every row
    ''' </summary>
    ''' <param name="dbName"></param>
    ''' <param name="dt"></param>
    ''' <param name="tableName"></param>
    Public Sub BulkInsert(dbName As String, dt As DataTable, tableName As String,
                          Optional ByVal myFilePath As String = "")
        Dim MyConn As New SQLiteConnection()
        Dim MyList = New List(Of String)
        GetConnectionString(dbName, MyConn, myFilePath)
        Using MyConn
            Using SQLCmd = New SQLiteCommand(MyConn)
                Using transaction = MyConn.BeginTransaction() 'This begins the bulk insert
                    Try
                        For Row = 1 To dt.Rows.Count - 1 'cycle through each row
                            For Col = 0 To dt.Columns.Count - 1
                                MyList.Add(dt.Rows(Row).Item(dt.Columns.Item(Col)))
                            Next Col
                            Dim Sql = String.Format("INSERT INTO " & tableName & " VALUES ({0});",
                                                    String.Join(", ", MyList))
                            SQLCmd.CommandText = Sql
                            SQLCmd.ExecuteNonQuery()
                            MyList.Clear()
                        Next Row
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
    ''' <param name="origDT"></param>
    ''' <param name="newDT"></param>
    ''' <param name="filterString"></param>
    ''' <param name="colNames"></param>
    ''' <param name="sortBy"></param>
    ''' <returns></returns>
    Public Shared Function FilterTable(origDT As DataTable, newDT As DataTable, filterString As String, colNames As String(), sortBy As String) _
        As DataTable
        Dim FoundRows() As DataRow
        Dim MyTempDT As New DataTable

        FoundRows = (origDT.Select(filterString, sortBy, DataViewRowState.CurrentRows))
        MyTempDT = FoundRows.CopyToDataTable()
        Dim MyView As New DataView(MyTempDT)

        newDT = MyView.ToTable(False, colNames)
        Return newDT
    End Function
    Public Shared Sub GetColumnNames(ByVal dt As DataTable, ByVal ParamArray args() As String)
        For i = 0 To args.Count - 1
            dt.Columns.Add(args(i))
        Next i
    End Sub

    Public Shared Function BitMapToImage(ByVal bm As Bitmap) As Windows.Media.ImageSource
        Dim MyImgSrc As New Windows.Media.ImageSourceConverter
        Dim ByteArr() As Byte

        Using Stream As New MemoryStream()
            bm.Save(Stream, System.Drawing.Imaging.ImageFormat.Bmp)
            Stream.Close()
            ByteArr = Stream.ToArray()
        End Using
        Return DirectCast(MyImgSrc.ConvertFrom(ByteArr), Windows.Media.ImageSource)
    End Function
End Class