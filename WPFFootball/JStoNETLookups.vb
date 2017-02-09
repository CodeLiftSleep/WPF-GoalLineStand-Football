Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports GlobalResources
Imports System.Data

Imports System.Linq
''' <summary>
''' This Class is used to call "lookup" functions from Javascript to Databases in .NET and call functions to manipulate and return information to JS
''' All methods are "Shared" and as such, this class is a Singleton that does not get Instantiated
''' </summary>
Public NotInheritable Class JStoNETLookups
    Private Shared TempDT As New DataTable
    ''' <summary>
    ''' This Filters a DT, returning all rows found to a new DT and returns the new DT to the caller.
    ''' </summary>
    ''' <param name="origDT"></param>
    ''' <param name="FilterString"></param>
    ''' <param name="sortBy"></param>
    ''' <returns></returns>
    Private Shared Function FilterTable(ByVal origDT As DataTable, ByVal FilterString As String, ByVal sortBy As String) As DataTable
        Dim FoundRows() As DataRow = origDT.Select(FilterString, sortBy, DataViewRowState.CurrentRows)
        TempDT = FoundRows.CopyToDataTable()
        Return TempDT
    End Function
#Region "General Lookups"
    ''' <summary>
    ''' Finds people by attribute ranges---supply the Type Of Person(DT Name), the attribute to lookup and min and max range for the attribute
    ''' </summary>
    ''' <param name="dbType"></param>
    ''' <param name="attribute"></param>
    ''' <param name="minAttr"></param>
    ''' <param name="maxAttr"></param>
    ''' <returns></returns>
    Public Shared Function GetPersonByAttr(ByVal dbType As String, ByVal attribute As String, ByVal minAttr As Integer, ByVal maxAttr As Integer) As String
        Dim myDT As New DataTable
        myDT = FilterTable(DraftDT, $"{attribute} >= {minAttr} and {attribute} <= {maxAttr}", $"{attribute} DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()
    End Function
#End Region

#Region "Team"
    Public Shared Function GetTeam(ByVal teamName As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(TeamDT, $"TeamName = '{teamName}'", "TeamName")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetTeam(ByVal teamId As Integer) As String
        Dim myDT As New DataTable
        myDT = FilterTable(TeamDT, $"TeamID = '{teamId}'", "TeamName")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
#End Region

#Region "Team Offense"
    Public Shared Function GetTeamOff(ByVal teamName As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(TeamDT, $"TeamName = '{teamName}'", "TeamName")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetTeamOff(ByVal teamId As Integer) As String
        Dim myDT As New DataTable
        myDT = FilterTable(TeamDT, $"TeamID = '{teamId}'", "TeamName")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetRankTeamOff() As String
        Dim myDT As New DataTable
        myDT = FilterTable(TeamOffenseDT, $"TeamID < 33", "TotalYards")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
#End Region
#Region "Team Defense"
    Public Shared Function GetTeamDef(ByVal teamName As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(TeamDT, $"TeamName = '{teamName}'", "TeamName")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetTeamDef(ByVal teamId As Integer) As String
        Dim myDT As New DataTable
        myDT = FilterTable(TeamDT, $"TeamID = '{teamId}'", "TeamName")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetRankTeamDef() As String
        Dim myDT As New DataTable
        myDT = FilterTable(TeamDefenseDT, $"TeamID < 33", "TotalYards")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
#End Region
#Region "Draft Players"
    ''' <summary>
    ''' Returns the entire draft class object
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetDraftClass() As String
        'Dim settings As New JsonSerializerSettings()
        'settings.NullValueHandling = NullValueHandling.Ignore
        'Dim myArray As JArray = JArray.FromObject(DraftDT, JsonSerializer.CreateDefault(settings))
        'Return myArray
        Return MainWindow.DBObj.Draft
    End Function

    ''' <summary>
    ''' Returns all players in the draft at this position
    ''' </summary>
    ''' <param name="pos"></param>
    ''' <returns></returns>
    Public Shared Function GetDraftPosition(ByVal pos As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(DraftDT, $"CollegePos = '{pos}'", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function

    ''' <summary>
    ''' Returns all players with a grade equal to or above the argument passed in
    ''' </summary>
    ''' <param name="Grade"></param>
    ''' <returns></returns>
    Public Shared Function GetDraftPlayers(ByVal Grade As Decimal) As String
        Dim myDT As New DataTable
        myDT = FilterTable(DraftDT, $"ActualGrade >= {Grade * 10}", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    ''' <summary>
    ''' Gets players within a certain grade range
    ''' </summary>
    ''' <param name="minGrade"></param>
    ''' <param name="maxGrade"></param>
    ''' <returns></returns>
    Public Shared Function GetDraftPlayers(ByVal minGrade As Decimal, ByVal maxGrade As Decimal) As String
        Dim myDT As New DataTable
        myDT = FilterTable(DraftDT, $"ActualGrade >= {minGrade * 10} and ActualGrade <= {maxGrade * 10}", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    ''' <summary>
    ''' Returns players by college
    ''' </summary>
    ''' <param name="college"></param>
    ''' <returns></returns>
    Public Shared Function GetDraftPlayers(ByVal college As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(DraftDT, $"College = '{college}'", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetDraftPlayers(ByVal college As String, pos As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(DraftDT, $"College = '{college}' and CollegePos = '{pos}'", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetDraftRegion(ByVal scoutRegion As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(DraftDT, $"ScoutRegion = '{scoutRegion}'", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetDraftRegion(ByVal scoutRegion As String, ByVal pos As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(DraftDT, $"ScoutRegion = '{scoutRegion}' and CollegePos = '{pos}'", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    ''' <summary>
    ''' Returns a single player in the draft by their Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Shared Function GetDraftPlayer(ByVal Id As Integer) As String
        Dim myDT As New DataTable
        myDT = FilterTable(DraftDT, $"DraftId = {Id}", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
#End Region

#Region "Roster Players"
    ''' <summary>
    ''' Returns all players in the draft at this position
    ''' </summary>
    ''' <param name="pos"></param>
    ''' <returns></returns>
    Public Shared Function GetRosterPosition(ByVal pos As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(PlayerDT, $"CollegePos = '{pos}'", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    ''' <summary>
    ''' Returns players by team
    ''' </summary>
    ''' <param name="teamName"></param>
    ''' <returns></returns>
    Public Shared Function GetRosterPlayers(ByVal teamName As String) As String
        Dim myDT As New DataTable
        Dim TeamID = TeamDT.AsEnumerable().Where(Function(t) t.Item("TeamName") = teamName).[Select](Function(i) i.Item("TeamID"))
        myDT = FilterTable(PlayerDT, $"TeamID = '{TeamID}'", "Age DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetRosterPlayers(ByVal teamName As String, pos As String) As String
        Dim myDT As New DataTable
        Dim TeamID = TeamDT.AsEnumerable().Where(Function(t) t.Item("TeamName") = teamName).[Select](Function(i) i.Item("TeamID"))
        myDT = FilterTable(PlayerDT, $"TeamID = '{TeamID}' and Pos = '{pos}'", "Age DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()
    End Function
    Public Shared Function GetRosterPositionType(ByVal posType As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(PlayerDT, $"PosType = '{posType}'", "Age DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()
    End Function
    ''' <summary>
    ''' Returns all players that share the same team ID
    ''' </summary>
    ''' <param name="teamId"></param>
    ''' <returns></returns>
    Public Shared Function GetRosterPlayers(ByVal teamId As Integer) As String
        Dim myDT As New DataTable
        myDT = FilterTable(PlayerDT, $"TeamID = '{teamId}'", "Age DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()
    End Function

    ''' <summary>
    ''' Returns a single player in the draft by their Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Shared Function GetRosterPlayer(ByVal Id As Integer) As String
        Dim myDT As New DataTable
        myDT = FilterTable(PlayerDT, $"PlayerId = {Id}", "ActualGrade DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
#End Region

#Region "Owners"
    ''' <summary>
    ''' Returns a single owner by team name---there is only a TeamID in the owner table so we need to look up the team name and get the ID
    ''' </summary>
    ''' <param name="teamName"></param>
    ''' <returns></returns>
    Public Shared Function GetOwner(ByVal teamName As String) As String

        Dim myDT As New DataTable
        Dim TeamID = TeamDT.AsEnumerable().Where(Function(t) t.Item("TeamName") = teamName).[Select](Function(i) i.Item("TeamID"))
        myDT = FilterTable(OwnerDT, $"TeamID = {TeamID.ElementAt(0)}", "OwnerId DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetOwner(ByVal teamId As Integer) As String

        Dim myDT As New DataTable
        myDT = FilterTable(OwnerDT, $"TeamID = {teamId}", "OwnerId DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
#End Region
#Region "Coach Lookups"
    ''' <summary>
    ''' Returns all the coaches for a particular team
    ''' </summary>
    ''' <param name="teamName"></param>
    ''' <returns></returns>
    Public Shared Function GetCoaches(ByVal teamName As String) As String

        Dim myDT As New DataTable
        Dim TeamID = TeamDT.AsEnumerable().Where(Function(t) t.Item("TeamName") = teamName).[Select](Function(i) i.Item("TeamID"))
        myDT = FilterTable(CoachDT, $"TeamID = {TeamID.ElementAt(0)}", "CoachId DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    Public Shared Function GetCoaches(ByVal teamId As Integer) As String

        Dim myDT As New DataTable
        myDT = FilterTable(CoachDT, $"TeamID = {teamId}", "CoachId DESC")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    ''' <summary>
    ''' Gets the coach by team and type
    ''' </summary>
    ''' <param name="teamName"></param>
    ''' <param name="coachType"></param>
    ''' <returns></returns>
    Public Shared Function GetCoaches(ByVal teamName As String, ByVal coachType As String) As String

        Dim myDT As New DataTable
        Dim TeamID = TeamDT.AsEnumerable().Where(Function(t) t.Item("TeamName") = teamName).[Select](Function(i) i.Item("TeamID"))
        myDT = FilterTable(CoachDT, $"TeamID = {TeamID.ElementAt(0)} and CoachTypeStr = '{coachType}'", "CoachTypeStr")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function

    Public Shared Function GetCoachType(ByVal coachType As String) As String
        Dim myDT As New DataTable
        myDT = FilterTable(CoachDT, $"CoachTypeStr = '{coachType}'", "CoachTypeStr")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
    ''' <summary>
    ''' Returns unemployed coaches by type---putting "All" as CoachType will return all unemployed coaches regardless of type
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetCoachesUnemployed(ByVal coachType As String) As String
        Dim myDT As New DataTable
        If coachType = "All" Then
            myDT = FilterTable(CoachDT, $"TeamID = 0", "CoachTypeStr")
        Else
            myDT = FilterTable(CoachDT, $"TeamID = 0 and CoachTypeStr = '{coachType}'", "CoachTypeStr")
        End If

        Return JsonConvert.SerializeObject(myDT).ToString()
    End Function
    ''' <summary>
    ''' Returns employed caches by type---putting "All" as CoachType will return all employed coaches regardless of type
    ''' </summary>
    ''' <param name="coachType"></param>
    ''' <returns></returns>
    Public Shared Function GetCoachesEmployed(ByVal coachType As String) As String
        Dim myDT As New DataTable
        If coachType = "All" Then
            myDT = FilterTable(CoachDT, $"TeamID <> 0", "CoachTypeStr")
        Else
            myDT = FilterTable(CoachDT, $"TeamID <> 0 and CoachTypeStr = '{coachType}'", "CoachTypeStr")
        End If

        Return JsonConvert.SerializeObject(myDT).ToString()
    End Function
    ''' <summary>
    ''' Gets all coaches by what side of the ball they specialize on
    ''' </summary>
    ''' <param name="sideOfBall"></param>
    ''' <returns></returns>
    Public Shared Function GetCoachesSideOfBall(ByVal sideOfBall As String) As String
        Dim myDT As New DataTable

        myDT = FilterTable(CoachDT, $"SideOfBall = '{sideOfBall}'", "CoachTypeStr")
        Return JsonConvert.SerializeObject(myDT).ToString()
    End Function
    ''' <summary>
    ''' Gets all coaches that share the same offensive philosophy
    ''' </summary>
    ''' <param name="offPhil"></param>
    ''' <returns></returns>
    Public Shared Function GetCoachesByOffPhil(ByVal offPhil As String) As String
        Dim myDT As New DataTable

        myDT = FilterTable(CoachDT, $"OffPhil = '{offPhil}'", "CoachTypeStr")
        Return JsonConvert.SerializeObject(myDT).ToString()
    End Function
    ''' <summary>
    ''' Gets all coaches that share the same defensive philosophy
    ''' </summary>
    ''' <param name="defPhil"></param>
    ''' <returns></returns>
    Public Shared Function GetCoachesByDefPhil(ByVal defPhil As String) As String
        Dim myDT As New DataTable

        myDT = FilterTable(CoachDT, $"DefPhil = '{defPhil}'", "CoachTypeStr")
        Return JsonConvert.SerializeObject(myDT).ToString()
    End Function
#End Region

#Region "Personnel Lookups"
    Public Shared Function GetPersonnel(ByVal teamName As String) As String

        Dim myDT As New DataTable
        Dim TeamID = TeamDT.AsEnumerable().Where(Function(t) t.Item("TeamName") = teamName).[Select](Function(i) i.Item("TeamID"))
        myDT = FilterTable(PersonnelDT, $"TeamID = {TeamID.ElementAt(0)}", "PersonnelType")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function

    Public Shared Function GetPersonnel(ByVal teamId As Integer) As String

        Dim myDT As New DataTable
        myDT = FilterTable(PersonnelDT, $"TeamID = {teamId}", "PersonnelType")

        Return JsonConvert.SerializeObject(myDT).ToString()

    End Function
#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' Passes a JSON string over and saves to a file
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Public Shared Function Save(ByVal teamId As Integer, ByVal model As String) As String
        Dim myList As New List(Of String)
        Dim myObj As JObject = JsonConvert.DeserializeObject(model)

        For Each item In myObj.Children
            myList.Add(item)
        Next
    End Function
#End Region
End Class