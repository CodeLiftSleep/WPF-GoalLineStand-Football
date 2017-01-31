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



''' <summary>
''' Need to expose the DB objects here so we can have register them as JS Objects for the browser to use.
''' </summary>
Class MainWindow
    Public Shared NewGameScreen As New NewGame
    Public window As JSValue
    Public DBObj As New DBObject

    Sub New()
        Try
            'WebCore.Initialize(New WebConfig() With {.RemoteDebuggingPort = 9337})
            'Dim Session As WebSession = WebCore.CreateWebSession("C:/WebSession", WebPreferences.Default)
            BrowserPreferences.SetChromiumSwitches("--remote-debugging-port=9222", "--disable-web-security", "--allow-file-access-from-files")


            InitializeComponent()
            '##########################################################################
            '##########################################################################
            '################## DOTNETBROWSER INITIALIZATION ##########################
            '##########################################################################
            '##########################################################################

            Dim page As String = "C:/Users/MNasty/Documents/Visual_Studio_2015/Projects/WPFFootball/WPFFootball/Web/index.html"
            browserView1.Preferences.JavaScriptEnabled = True
            browserView1.Preferences.ImagesEnabled = True
            browserView1.Preferences.AllowRunningInsecureContent = True
            'Makes an Async and Await call to get these objects before we load the page so they will be able to utilized

            GetDBObjects()
            browserView1.Browser.LoadURL(page)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' We are going to create a Task to load up the DB objects and await it until they are complete to make sure they load before the page finishes loading
    ''' </summary>
    Private Async Sub GetDBObjects()
        'Create a Task to run and an inline function to call the LoadDBObjects Method.  This will wait here until its finished before proceeding
        Await Task.Run(Sub()
                           LoadDBObjects()
                       End Sub)
    End Sub

    Private Sub LoadDBObjects()
        'Create the window object to pass .NET values to JS Land
        window = browserView1.Browser.ExecuteJavaScriptAndReturnValue("window")
        window.AsObject().SetProperty("Teams", DBObj.Teams)
        'window.AsObject().SetProperty("Owners", DBObj.Owners)
        'window.AsObject().SetProperty("Personnel", DBObj.Personnel)
        'window.AsObject().SetProperty("Coaches", DBObj.Coaches)
        'window.AsObject().SetProperty("Players", DBObj.Players)
        'window.AsObject().SetProperty("Draft", DBObj.Draft)

    End Sub

    Private Sub browserView_FinishLoadingFrameEvent(sender As Object, e As DotNetBrowser.Events.FinishLoadingEventArgs)
        If e.IsMainFrame Then
            'Wait for the browser to finish loading, then load the page

            browserView2.Browser.LoadURL(browserView2.Browser.GetRemoteDebuggingURL())

        End If
    End Sub


End Class

Public Class DBObject
    Public SQLTable As New SQLiteDataFunctions 'Create the SQLite object
    ReadOnly MyDB As String = "Football" 'set the DB Name
    Public Property Teams As String
    Public Property Owners As String
    Public Property Personnel As String
    Public Property Coaches As String
    Public Property Players As String
    Public Property Draft As String

    ' Declare a local instance of chromium and the main form in order to execute things from here in the main thread

    ' The form class needs to be changed according to yours

    Public Sub New()
        'Load up the database tables
        SQLTable.LoadTable(MyDB, TeamDT, "Teams")
        SQLTable.LoadTable(MyDB, OwnerDT, "Owners")
        SQLTable.LoadTable(MyDB, PersonnelDT, "Personnel")
        SQLTable.LoadTable(MyDB, CoachDT, "Coaches")
        SQLTable.LoadTable(MyDB, PlayerDT, "RosterPlayers")
        SQLTable.LoadTable(MyDB, DraftDT, "DraftPlayers")
        CreateDBObjects()
    End Sub


    ''' <summary>
    ''' Serialize the DataTables into JSON and expose them to JS
    ''' </summary>
    Public Sub CreateDBObjects()
        'Now we need to serialize the objects for use in Javascript
        Dim Settings As New JsonSerializerSettings() 'create settings that will ensure no duplicates
        Settings.ObjectCreationHandling = ObjectCreationHandling.Replace

        Teams = JsonConvert.SerializeObject(TeamDT, Formatting.Indented, Settings)
        Owners = JsonConvert.SerializeObject(OwnerDT, Formatting.Indented, Settings)
        Personnel = JsonConvert.SerializeObject(PersonnelDT, Formatting.Indented, Settings)
        Coaches = JsonConvert.SerializeObject(CoachDT, Formatting.Indented, Settings)
        Players = JsonConvert.SerializeObject(PlayerDT, Formatting.Indented, Settings)
        Draft = JsonConvert.SerializeObject(DraftDT, Formatting.Indented, Settings)

        TeamDT = Nothing
        OwnerDT = Nothing
        PersonnelDT = Nothing
        CoachDT = Nothing
        PlayerDT = Nothing
        DraftDT = Nothing
    End Sub

End Class