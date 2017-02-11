Imports System.Data
Imports DotNetBrowser
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports SQLFunctions.SQLiteDataFunctions
''' <summary>
''' This class handles all CRUD operations---Create, Read, Update, Delete.
''' It acts as an intermediary between the SQLiteDataFunctions class and JS
''' </summary>
Public NotInheritable Class CRUD
    Shared SQLTables As New SQLFunctions.SQLiteDataFunctions
    Shared ReadOnly MyDB As String = "Football"
    Public Shared Property ReturnVal As String

    Public Sub New()
    End Sub

#Region "CRUD Operations"
    ''' <summary>
    ''' Passes a JSON string over and saves to a file
    ''' </summary>
    ''' <param name="model"></param>
    Public Shared Function Save(ByVal teamId As Integer, ByVal fileName As String, ByVal model As String) As String
        Dim TempDT As New DataTable
        Dim MyStr As String = ""
        SQLTables.LoadTable(MyDB, TempDT, "SaveGames")
        Dim DupRow As DataRow = TempDT.Select($"FileName = '{fileName}'").FirstOrDefault()
        'Save row---no matching values
        If DupRow Is Nothing Then
            SQLTables.InsertInto(MyDB, "SaveGames", $"INSERT INTO SaveGames(FileName,SaveGameJSONString) VALUES ('{fileName}', '{model}')")
            MyStr = "Success!"
        Else 'Return Error---duplicate
            MyStr = "Duplicate!"
        End If
        Return MyStr
    End Function

#End Region
End Class