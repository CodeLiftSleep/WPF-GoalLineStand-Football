Imports System.Data
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
    Public Sub New()
    End Sub

#Region "CRUD Operations"
    ''' <summary>
    ''' Passes a JSON string over and saves to a file
    ''' </summary>
    ''' <param name="model"></param>
    Public Shared Function Save(ByVal teamId As Integer, ByVal fileName As String, ByVal model As String) As String
        Dim TempDT As New DataTable
        Dim myObj As JObject = JsonConvert.DeserializeObject(model)
        Dim MyStr As String
        SQLTables.LoadTable(MyDB, TempDT, "SaveGames")
        Dim DupRow As DataRow = TempDT.Select($"FileName = '{fileName}'").FirstOrDefault()

        'Save row---no matching values
        If DupRow Is Nothing Then
            'TempDT.Clear() 'Clear the Table of any rows....we are only inserting this one
            'Dim newRow As DataRow = TempDT.NewRow()
            'newRow("FileName") = fileName
            'newRow("SaveGameJSONString") = model
            'TempDT.Rows.Add(newRow)
            'SQLTables.DeleteTable(MyDB, "SaveGames")
            SQLTables.InsertInto(MyDB, "SaveGames", "INSERT INTO SaveGames(FileName,SaveGameJSONString) VALUES " & "('" & fileName & "', 'Test')")
            'SQLTables.BulkInsert(MyDB, TempDT, "SaveGames")

        Else 'Return Error---duplicate
            MyStr = "Duplicate!"
        End If

        Return MyStr

    End Function
#End Region
End Class