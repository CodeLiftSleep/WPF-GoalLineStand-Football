Imports System.Globalization
Imports Microsoft.Win32
Imports GoalLineStandFootball.My.Resources
Imports GlobalResources

Public Class UserScreen
    ReadOnly MyTeam As Integer
    ReadOnly MyVM As New UserScreenViewModel
    Dim MyList As New List(Of String)
    Dim MyRand As New Troschuetz.Random.TRandom

    ''' <summary>
    '''     TeamID gets the the team the user selected when creating the window passed in as a parameter
    ''' </summary>
    ''' <param name="teamID"></param>
    Sub New(teamID As Integer)
        Dim FilePath = "pack://application:,,,/Project_Files/"
        Dim MyNum As Integer

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        '********Currently pic loading is only implemented for the AFC East Teams as it was very tedious to look for and download the pictures.  You can automatically add pictures to use for a team
        'by starting the file name with the teamnickname, ie "Bills_Watkins" or "Broncos_Miller", etc.  It will automatically find all of the files this way and will randomly select 5 from the list
        'to display.  Each team should have between 10-15 pictures for these purposes...action shots, celebrations, fans, stadium, etc...

        DataContext = MyVM
        MyTeam = teamID
        LoadPics(MyTeam)

        MyNum = MyRand.NextUInt(0, MyList.Count - 1)
        MyVM.Image1 = New BitmapImage(New Uri(FilePath + ResourceManager.GetObject(MyList(MyNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))
        MyList.RemoveAt(MyNum)
        MyNum = MyRand.NextUInt(0, MyList.Count - 1)
        MyVM.Image2 = New BitmapImage(New Uri(FilePath + ResourceManager.GetObject(MyList(MyNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))
        MyList.RemoveAt(MyNum)
        MyNum = MyRand.NextUInt(0, MyList.Count - 1)
        MyVM.Image3 = New BitmapImage(New Uri(FilePath + ResourceManager.GetObject(MyList(MyNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))
        MyList.RemoveAt(MyNum)
        MyNum = MyRand.NextUInt(0, MyList.Count - 1)
        MyVM.Image4 = New BitmapImage(New Uri(FilePath + ResourceManager.GetObject(MyList(MyNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))
        MyList.RemoveAt(MyNum)
        MyNum = MyRand.NextUInt(0, MyList.Count - 1)
        MyVM.Image5 = New BitmapImage(New Uri(FilePath + ResourceManager.GetObject(MyList(MyNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))

    End Sub

    Private Function LoadPics(ByVal teamID As Integer) As List(Of String)
        Dim DictEntry As New DictionaryEntry
        Dim RunTimeResourceSet As Object
        Dim TeamNick As String = TeamDT.Rows(teamID).Item("TeamNickname").ToString()

        RunTimeResourceSet = My.Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, False, True)

        For Each DictEntry In RunTimeResourceSet
            MyRand.NextUInt()
            If DictEntry.Key.ToString().StartsWith(TeamNick) Then
                MyList.Add(DictEntry.Key)
            End If
        Next DictEntry

        Return MyList
    End Function
    ''' <summary>
    '''     Loads Game from file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FileLoad_OnClick(sender As Object, e As RoutedEventArgs)
        Dim MyFile As New OpenFileDialog
        MyFile.Filter = "GLS Save Game Files (*.gls)|*.gls"
        Dim Result As Boolean = MyFile.ShowDialog()

        If Result = True Then
            '#TODO need to figure out what gets loaded for the game
        End If
    End Sub

    Private Sub FileSave_OnClick(sender As Object, e As RoutedEventArgs)
        Dim MyFile As New OpenFileDialog
        Dim Result As Boolean = MyFile.ShowDialog()

        MyFile.Filter = "GLS Save Game Files (*.gls)|*.gls"

        If Result = True Then
            '#TODO need to figure out what to save so the file can then be loaded properly again and the game resumed.
        End If
    End Sub

    Private Sub FileHelp_OnClick(sender As Object, e As RoutedEventArgs)
        'bring up help file
    End Sub

    Private Sub FileExit_OnClick(sender As Object, e As RoutedEventArgs)
        Dim Res As MsgBoxResult = MsgBox("Are you sure you want to exit the game? Any unsaved data will be lost!",
                                         MsgBoxStyle.YesNo, "Exit Game")
        If Res = MsgBoxResult.Yes Then
            End
        End If
    End Sub

    Private Sub LgSet_OnClick(sender As Object, e As RoutedEventArgs)
        Dim MySettings As New GameSettings(MyTeam)
        MySettings.Show()
        Close()
    End Sub

    Private Sub LgHm_Click(sender As Object, e As RoutedEventArgs)
        Dim LeagueHm As New LeagueHome
        LeagueHm.Show()
        GetWindow(LeagueHm)
        Close()
    End Sub
End Class