Imports System.Reflection

Imports NFL_Draft
Imports System.Windows.Forms
Imports System.IO
Imports SQLFunctions
Imports System.Data
Imports GlobalResources
Imports Newtonsoft.Json
Imports DotNetBrowser
Imports DotNetBrowser.WPF
'Imports Awesomium.Core
'Imports Awesomium.Windows.Controls
Imports Microsoft.Win32
Imports Newtonsoft.Json.Linq

''' <summary>
''' Need to expose the DB objects here so we can have register them as JS Objects for the browser to use.
''' </summary>
Class MainWindow
    Public Shared NewGameScreen As New NewGame
    Public window As JSValue
    Public Shared DBObj As New DBObject
    Public Team As JSObject
    Public page As String = AppDomain.CurrentDomain.BaseDirectory()

    Sub New()
        Try
            BrowserPreferences.SetChromiumSwitches("--remote-debugging-port=9222", "--disable-web-security", "--allow-file-access-from-files")

            InitializeComponent()
            '##########################################################################
            '##########################################################################
            '################## DOTNETBROWSER INITIALIZATION ##########################
            '##########################################################################
            '##########################################################################

            'Replace the absolute path with the relative path
            page = page.Replace("bin\x86\Debug\", "Web\index.html")

            browserView1.Preferences.JavaScriptEnabled = True
            browserView1.Preferences.ImagesEnabled = True
            browserView1.Preferences.AllowRunningInsecureContent = True
            'LoadDBObjects()
            'load the page
            browserView1.Browser.LoadURL(page)
            'Dim team As JSValue = browserView1.Browser.ExecuteJavaScriptAndReturnValue("getTeam")
            'Dim rtnTeam As JSArray = GetTeam()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub LoadDBObjects()
        'Create the window object to pass .NET values to JS Land

        'window.AsObject().SetProperty("Teams", DBObj.Teams)
        'DBObj.Teams = Nothing
        'window.AsObject().SetProperty("Owners", DBObj.Owners)
        'window.AsObject().SetProperty("Personnel", DBObj.Personnel)
        'window.AsObject().SetProperty("Coaches", DBObj.Coaches)
        'window.AsObject().SetProperty("Players", DBObj.Players)
        'window.AsObject().SetProperty("Draft", DBObj.Draft)
        'DBObj.Draft = Nothing
    End Sub

    Private Sub browserView_FinishLoadingFrameEvent(sender As Object, e As Events.FinishLoadingEventArgs)
        If e.IsMainFrame Then
            'Wait for the browser to finish loading, then load the page
            window = browserView1.Browser.ExecuteJavaScriptAndReturnValue("window")
            window.AsObject().SetProperty("Lookup", New JStoNETLookups())
            browserView2.Browser.LoadURL(browserView2.Browser.GetRemoteDebuggingURL())
        End If
    End Sub

End Class

''' <summary>
''' Class to create the DB Objects to be used in JavaScript
''' </summary>
Public Class DBObject
    Public SQLTable As New SQLiteDataFunctions 'Create the SQLite object
    ReadOnly MyDB As String = "Football" 'set the DB Name
    Public Property Teams As String
    Public Property Owners As String
    Public Property Personnel As String
    Public Property Coaches As String
    Public Property Players As String
    Public Property Draft As String

    Public Sub New()
        'Load up the database tables
        SQLTable.LoadTable(MyDB, TeamDT, "Teams")
        SQLTable.LoadTable(MyDB, OwnerDT, "Owners")
        SQLTable.LoadTable(MyDB, PersonnelDT, "Personnel")
        SQLTable.LoadTable(MyDB, CoachDT, "Coaches")
        SQLTable.LoadTable(MyDB, PlayerDT, "RosterPlayers")
        SQLTable.LoadTable(MyDB, DraftDT, "DraftPlayers")
        'We need to remove duplicates from the DT---unsure why this is happening...
        RemoveDuplicateRows(TeamDT, 32)
        RemoveDuplicateRows(OwnerDT, 100)
        RemoveDuplicateRows(PersonnelDT, 1800)
        RemoveDuplicateRows(CoachDT, 1800)
        RemoveDuplicateRows(PlayerDT, 2200)
        RemoveDuplicateRows(DraftDT, 3000)

        CreateDBObjects()
    End Sub
    ''' <summary>
    ''' Unsure why its happening---all tables have the same rows twice.  This removes any rows over the amount that should be there
    ''' </summary>
    ''' <param name="DT"></param>
    ''' <param name="rowIndexToRemove"></param>
    Private Sub RemoveDuplicateRows(ByVal DT As DataTable, ByVal rowIndexToRemove As Integer)
        While DT.Rows.Count > rowIndexToRemove
            DT.Rows.RemoveAt(rowIndexToRemove)
        End While
    End Sub
    ''' <summary>
    ''' Serialize the DataTables into JSON and expose them to JS
    ''' </summary>
    Public Sub CreateDBObjects()
        'Now we need to serialize the objects for use in Javascript
        Dim Settings As New JsonSerializerSettings() 'create settings that will ensure no duplicates
        Settings.ObjectCreationHandling = ObjectCreationHandling.Replace

        Teams = JsonConvert.SerializeObject(TeamDT, Settings)
        Owners = JsonConvert.SerializeObject(OwnerDT)
        Personnel = JsonConvert.SerializeObject(PersonnelDT)
        Coaches = JsonConvert.SerializeObject(CoachDT)
        Players = JsonConvert.SerializeObject(PlayerDT)
        Draft = JsonConvert.SerializeObject(DraftDT)

        'TeamDT = Nothing
        'OwnerDT = Nothing
        'PersonnelDT = Nothing
        'CoachDT = Nothing
        'PlayerDT = Nothing
        'DraftDT = Nothing
    End Sub

End Class