Imports System.Globalization
Imports Microsoft.Win32
Imports WPFFootball.My.Resources
Imports GlobalResources

Public Class UserScreen
    ReadOnly myTeam As Integer
    ReadOnly MyVM As New UserScreenViewModel
    Dim myList As New List(Of String)
    Dim myRand As New Troschuetz.Random.TRandom

    ''' <summary>
    '''     TeamID gets the the team the user selected when creating the window passed in as a parameter
    ''' </summary>
    ''' <param name="TeamID"></param>
    Sub New(TeamID As Integer)
        Dim filepath = "pack://application:,,,/Project Files/"
        Dim myNum As Integer

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        '********Currently pic loading is only implemented for the AFC East Teams as it was very tedious to look for and download the pictures.  You can automatically add pictures to use for a team
        'by starting the file name with the teamnickname, ie "Bills_Watkins" or "Broncos_Miller", etc.  It will automatically find all of the files this way and will randomly select 5 from the list
        'to display.  Each team should have between 10-15 pictures for these purposes...action shots, celebrations, fans, stadium, etc...

        DataContext = MyVM
        myTeam = TeamID
        LoadPics(myTeam)

        myNum = myRand.NextUInt(0, myList.Count - 1)
        MyVM.Image1 = New BitmapImage(New Uri(filepath + ResourceManager.GetObject(myList(myNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))
        myList.RemoveAt(myNum)
        myNum = myRand.NextUInt(0, myList.Count - 1)
        MyVM.Image2 = New BitmapImage(New Uri(filepath + ResourceManager.GetObject(myList(myNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))
        myList.RemoveAt(myNum)
        myNum = myRand.NextUInt(0, myList.Count - 1)
        MyVM.Image3 = New BitmapImage(New Uri(filepath + ResourceManager.GetObject(myList(myNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))
        myList.RemoveAt(myNum)
        myNum = myRand.NextUInt(0, myList.Count - 1)
        MyVM.Image4 = New BitmapImage(New Uri(filepath + ResourceManager.GetObject(myList(myNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))
        myList.RemoveAt(myNum)
        myNum = myRand.NextUInt(0, myList.Count - 1)
        MyVM.Image5 = New BitmapImage(New Uri(filepath + ResourceManager.GetObject(myList(myNum), CultureInfo.InvariantCulture).ToString(), UriKind.RelativeOrAbsolute))

    End Sub

    Private Function LoadPics(ByVal TeamID) As List(Of String)
        Dim dictEntry As New DictionaryEntry
        Dim runTimeResourceSet As Object
        Dim teamNick As String = TeamDT.Rows(TeamID).Item("TeamNickname")

        runTimeResourceSet = My.Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, False, True)

        For Each dictEntry In runTimeResourceSet
            myRand.NextUInt()
            If dictEntry.Key.ToString().StartsWith(teamNick) Then
                myList.Add(dictEntry.Key)
            End If
        Next dictEntry

        Return myList
    End Function
    ''' <summary>
    '''     Loads Game from file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FileLoad_OnClick(sender As Object, e As RoutedEventArgs)
        Dim myFile As New OpenFileDialog
        myFile.Filter = "GLS Save Game Files (*.gls)|*.gls"
        Dim result As Boolean = myFile.ShowDialog()

        If result = True Then
            '#TODO need to figure out what gets loaded for the game
        End If
    End Sub

    Private Sub FileSave_OnClick(sender As Object, e As RoutedEventArgs)
        Dim myFile As New OpenFileDialog
        Dim result As Boolean = myFile.ShowDialog()

        myFile.Filter = "GLS Save Game Files (*.gls)|*.gls"

        If result = True Then
            '#TODO need to figure out what to save so the file can then be loaded properly again and the game resumed.
        End If
    End Sub

    Private Sub FileHelp_OnClick(sender As Object, e As RoutedEventArgs)
        'bring up help file
    End Sub

    Private Sub FileExit_OnClick(sender As Object, e As RoutedEventArgs)
        Dim res As MsgBoxResult = MsgBox("Are you sure you want to exit the game? Any unsaved data will be lost!",
                                         MsgBoxStyle.YesNo, "Exit Game")
        If res = MsgBoxResult.Yes Then
            End
        End If
    End Sub

    Private Sub LgSet_OnClick(sender As Object, e As RoutedEventArgs)
        Dim mySettings As New GameSettings(myTeam)
        mySettings.Show()
        GetWindow(mySettings)
        Close()
    End Sub

    Private Sub LgHm_Click(sender As Object, e As RoutedEventArgs)
        Dim LeagueHm As New LeagueHome
        LeagueHm.Show()
        GetWindow(LeagueHm)
        Close()
    End Sub
End Class