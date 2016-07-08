Imports System.Globalization
Imports Microsoft.Win32
Imports WPFFootball.My.Resources

Public Class UserScreen
    Dim ReadOnly myTeam as integer
    Dim ReadOnly MyVM as new UserScreenViewModel
    dim myList as New List(Of String)
    Dim myRand as new Troschuetz.Random.TRandom

    ''' <summary>
    '''     TeamID gets the the team the user selected when creating the window passed in as a parameter
    ''' </summary>
    ''' <param name="TeamID"></param>
    Sub New(TeamID As integer)
        Dim filepath = "pack://application:,,,/Project Files/"
        dim myNum as integer

        ' This call is required by the designer.
        InitializeComponent()   
        ' Add any initialization after the InitializeComponent() call.

        DataContext=MyVM   
        myTeam = TeamID   
        LoadPics(myteam)
        
        myNum=myRand.NextUInt(0,myList.Count-1)       
        MyVM.Image1=New BitmapImage(New Uri(filepath+ResourceManager.GetObject(myList(myNum),CultureInfo.InvariantCulture).ToString(),UriKind.RelativeOrAbsolute))
        myList.Removeat(myNum)    
        myNum=myRand.NextUInt(0,myList.Count-1)  
        MyVM.Image2=New BitmapImage(New Uri(filepath+ResourceManager.GetObject(myList(myNum),CultureInfo.InvariantCulture).ToString(),UriKind.RelativeOrAbsolute))
        myList.RemoveAt(myNum)
        myNum=myRand.NextUInt(0,myList.Count-1)
        MyVM.Image3=New BitmapImage(New Uri(filepath+ResourceManager.GetObject(myList(myNum),CultureInfo.InvariantCulture).ToString(),UriKind.RelativeOrAbsolute))
        myList.RemoveAt(myNum)
        myNum=myRand.NextUInt(0,myList.Count-1)
        MyVM.Image4=New BitmapImage(New Uri(filepath+ResourceManager.GetObject(myList(myNum),CultureInfo.InvariantCulture).ToString(),UriKind.RelativeOrAbsolute)) 
        myList.RemoveAt(myNum)
        myNum=myRand.NextUInt(0,myList.Count-1)
        MyVM.Image5=New BitmapImage(New Uri(filepath+ResourceManager.GetObject(myList(myNum),CultureInfo.InvariantCulture).ToString(),UriKind.RelativeOrAbsolute))
        
    End Sub

    Private function LoadPics(byval TeamID) As List(Of string)
        Dim dictEntry as new DictionaryEntry
        Dim runTimeResourceSet as Object
        dim teamNick as String=NewGame.TeamDT.Rows(TeamID).Item("TeamNickname")

        runTimeResourceSet=My.Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture,False,True)

        for each dictEntry In runTimeResourceSet
            myRand.NextUInt()
            if dictEntry.Key.ToString().StartsWith(teamNick) then
                myList.Add(dictEntry.key)
            end if             
        Next dictEntry

        return myList
    End function
    ''' <summary>
    '''     Loads Game from file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FileLoad_OnClick(sender As Object, e As RoutedEventArgs)
        dim myFile as New OpenFileDialog
        myFile.Filter = "GLS Save Game Files (*.gls)|*.gls"
        dim result as Boolean = myFile.ShowDialog()

        if result = True then
            '#TODO need to figure out what gets loaded for the game
        end if
    End Sub

    Private Sub FileSave_OnClick(sender As Object, e As RoutedEventArgs)
        dim myFile as new OpenFileDialog
        dim result as Boolean = myFile.ShowDialog()

        myFile.Filter = "GLS Save Game Files (*.gls)|*.gls"

        if result = True Then
            '#TODO need to figure out what to save so the file can then be loaded properly again and the game resumed.
        End If
    End Sub

    Private Sub FileHelp_OnClick(sender As Object, e As RoutedEventArgs)
        'bring up help file 
    End Sub

    Private Sub FileExit_OnClick(sender As Object, e As RoutedEventArgs)
        dim res as MsgBoxResult = MsgBox("Are you sure you want to exit the game? Any unsaved data will be lost!",
                                         MsgBoxStyle.YesNo, "Exit Game")
        if res = MsgBoxResult.Yes Then
            End
        End If
    End Sub

    Private Sub LgSet_OnClick(sender As Object, e As RoutedEventArgs)
        dim mySettings as new GameSettings(myTeam)
        mysettings.Show()
        GetWindow(mysettings)
        Close
    End Sub

    Private Sub LgHm_Click(sender As Object, e As RoutedEventArgs)
        dim LeagueHm as New LeagueHome
        LeagueHm.Show()
        GetWindow(LeagueHm)
        Close
    End Sub
End Class
