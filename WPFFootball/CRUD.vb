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
            Try
                SQLTables.InsertInto(MyDB, "SaveGames", "FileName,SaveGameJSONString", "'{fileName}', '{model}'")
                MyStr = "Success!"
            Catch ex As Exception
                MyStr = "Error Inserting Row!"
            End Try
        Else 'Return Error---duplicate
            MyStr = "Duplicate!"
        End If
        Return MyStr
    End Function

    Public Shared Function SaveSituation(ByVal model As String) As String
        Dim TempDT As New DataTable
        Dim MyStr As String = ""
        Dim myModel = New SituationModel
        myModel = JsonConvert.DeserializeObject(Of SituationModel)(model)
        SQLTables.LoadTable(MyDB, TempDT, "OffSituation")

        'Cycle through each row in the array
        Dim DupRow As DataRow = TempDT.Select($"OffSituationId = '{myModel.OffSituationId}'").FirstOrDefault()

        If DupRow Is Nothing Then 'There is no row with this Id
            Dim SQLCols As String = "Ace12, Club32, CoachId, distance, down, Heavy13, Houston20, Jet10, Joker02, Jumbo23, Kings01, OffPhilosophy, OffSituationId, passBLOS, passLg, passMed, passSh, Posse11, quarter, Regular21, Royal00, runLE, runLT,
            runMid, runRE, runRT, SaveGameId, Tank22, time, WildCat31, yardlines"

            Dim SQLValues As String = "'{myModel.Ace12}', '{myModel.Club32}', '{myModel.CoachId}', '{myModel.distance}','{myModel.down}', '{myModel.Heavy13}', '{myModel.Houston20}', '{myModel.Jet10}', '{myModel.Joker02}', '{myModel.Jumbo23}',
            '{myModel.Kings01}', '{myModel.offPhilosophy}', '{myModel.OffSituationId}', '{myModel.passBLOS}', '{myModel.passLg}', '{myModel.passMed}', '{myModel.passSh}', '{myModel.Posse11}', '{myModel.quarter}', '{myModel.Regular21}',
            '{myModel.Royal00}', '{myModel.runLE}', '{myModel.runLT}', '{myModel.runMid}', '{myModel.runRE}', '{myModel.runRT}', '{myModel.SaveGameId}', '{myModel.Tank22}', '{myModel.time}', '{myModel.Wildcat31}', '{myModel.yardlines}'"
            Try
                SQLTables.InsertInto(MyDB, "OffSituation", SQLCols, SQLValues)
                MyStr = "New Row(s) inserted successfully!"
            Catch ex As Exception
                MyStr = "Error Inserting Row(s)!"
            End Try

        Else 'This situation Id already exists, so we are going to update the table instead of creating a new row.
            Dim SQLUpdate As String = $"Ace12 = '{myModel.Ace12}', Club32 = '{myModel.Club32}', CoachId = '{myModel.CoachId}', distance = '{myModel.distance}', down = '{myModel.down}', Heavy13 = '{myModel.Heavy13}', Houston20 = '{myModel.Houston20}',
            Jet10 = '{myModel.Jet10}', Joker02 = '{myModel.Joker02}', Jumbo23 = '{myModel.Jumbo23}', Kings01 = '{myModel.Kings01}', OffPhilosophy = '{myModel.offPhilosophy}', passBLOS = '{myModel.passBLOS}', passLg = '{myModel.passLg}',
            passMed = '{myModel.passMed}', passSh = '{myModel.passSh}', Posse11 = '{myModel.Posse11}', quarter = '{myModel.quarter}', Regular21 = '{myModel.Regular21}', Royal00 = '{myModel.Royal00}', runLE = '{myModel.runLE}', runLT = '{myModel.runLT}',
            runMid = '{myModel.runMid}', runRE = '{myModel.runRE}', runRT = '{myModel.runRT}', Tank22 = '{myModel.Tank22}', time = '{myModel.time}', Wildcat31 = '{myModel.Wildcat31}', yardlines = '{myModel.yardlines}'"

            Dim SQLWhereClause = $"OffSituationID = '{myModel.OffSituationId}'"
            Try
                SQLTables.Update(MyDB, "OffSituation", SQLUpdate, SQLWhereClause) 'Update the tables
                MyStr = "Row(s) Updated Successfully!"
            Catch ex As Exception
                MyStr = "Error Updating Row(s)!"
            End Try
        End If
        Return MyStr
    End Function

#End Region
    Private Class SituationModel
        Public Property OffSituationId As String
        Public Property CoachId As Integer
        Public Property SaveGameId As Integer
        Public Property offPhilosophy As String
        Public Property down As String
        Public Property quarter As String
        Public Property distance As String
        Public Property time As String
        Public Property yardlines As String
        Public Property Ace12 As Integer
        Public Property Club32 As Integer
        Public Property Heavy13 As Integer
        Public Property Houston20 As Integer
        Public Property Jet10 As Integer
        Public Property Joker02 As Integer
        Public Property Jumbo23 As Integer
        Public Property Kings01 As Integer
        Public Property passBLOS As Integer
        Public Property passLg As Integer
        Public Property passMed As Integer
        Public Property passSh As Integer
        Public Property Posse11 As Integer
        Public Property Regular21 As Integer
        Public Property Royal00 As Integer
        Public Property runLE As Integer
        Public Property runLT As Integer
        Public Property runMid As Integer
        Public Property runRE As Integer
        Public Property runRT As Integer
        Public Property Tank22 As Integer
        Public Property Wildcat31 As Integer
    End Class
End Class