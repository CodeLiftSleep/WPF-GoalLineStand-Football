Imports System.IO

Public Class GameSettings
    ReadOnly myTeam As Integer
    ReadOnly SetArray(22) As String
    ReadOnly TempArray(125) As String
    Dim DoOnce As Boolean
    Dim MyVM As New GameSettingsViewModel

    Sub New(TeamID As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        myTeam = TeamID
        DataContext = MyVM
        ' Add any initialization after the InitializeComponent() call.
        GetSettings(TeamID)
    End Sub

    ''' <summary>
    '''     Saves any changes to game state and returns to previous menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Accept_OnClick(sender As Object, e As RoutedEventArgs)
        Dim userTeam As New UserScreen(myTeam)
        userTeam.Show()
        GetWindow(userTeam)
        Close()
    End Sub

    Private Sub GetSettings(TeamID As Integer)
        MyVM.MyBackgroundImg = New BitmapImage(New Uri(NewGameViewModel.GetBackgroundFilePath(TeamID),
                                                   UriKind.RelativeOrAbsolute))
        LoadSettings()
        'LgSettingsTabControlBG=
        RosterSize.SelectedIndex = 7
        GameInactives.SelectedIndex = 7
        OTFormat.SelectedIndex = 2
        For i = 0 To 15

        Next i
    End Sub

    Private Sub LoadSettings()
        If DoOnce = False Then
            Dim i As Integer
            Dim mystring As String
            Using SR = New StreamReader(CurDir() + "\GLSsettings.txt")
                mystring = SR.ReadLine
                While mystring <> "LEAGUE FINANCE SETTINGS" 'sets a loop until it reaches the proper line
                    mystring = SR.ReadLine()
                    SetArray(i) = mystring
                    i += 1
                End While
                i = 0
                Do While SR.EndOfStream = False 'reads to end of file from this point
                    TempArray(i) = SR.ReadLine
                    i += 1
                Loop
                DoOnce = True
            End Using
        End If
    End Sub

    Private Function ResetValues(tempstring As String) As String
        Dim myIndex As Integer
        myIndex = tempstring.IndexOf("=")
        If myIndex <> tempstring.Length - 1 Then
            myIndex = tempstring.IndexOf("=") + 1
            tempstring = tempstring.Remove(myIndex)
        End If
        Return tempstring
    End Function

    ''' <summary>
    '''     Writes changes to settings file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LgFinAcceptBtn_Click(sender As Object, e As RoutedEventArgs)
        Dim DelFile As String = CurDir() + "\GLSsettings.text" 'deletes current settings file and replaces with new one
        If File.Exists(DelFile) Then
            File.Delete(DelFile)
        End If

        Using SW = New StreamWriter(CurDir() + "\GLSsettings.text")
            SW.WriteLine("LEAGUE GAME SETTINGS")
            For i = 0 To SetArray.Count - 1
                If SetArray(i) <> "" Then
                    SetArray(i) = ResetValues(SetArray(i))
                End If
            Next i
            SW.WriteLine(String.Format("{0}{1}", SetArray(0), MyVM.MyStartYear))
            SW.WriteLine(String.Format("{0}{1}", SetArray(1), MyVM.MyLeagueRules))
            SW.WriteLine(String.Format("{0}{1}", SetArray(2), MyVM.MyLeagueType))
            SW.WriteLine(String.Format("{0}{1}", SetArray(3), MyVM.MyRosterSize))
            SW.WriteLine(String.Format("{0}{1}", SetArray(4), MyVM.MyInactives))
            SW.WriteLine(String.Format("{0}{1}", SetArray(5), MyVM.MyPracSquadSize))
            SW.WriteLine(String.Format("{0}{1}", SetArray(6), MyVM.MyOTFormat))
            SW.WriteLine(String.Format("{0}{1}", SetArray(7), MyVM.MyFieldType))
            SW.WriteLine(String.Format("{0}{1}", SetArray(8), MyVM.MyPenalties))
            SW.WriteLine(String.Format("{0}{1}", SetArray(9), MyVM.MyNumTeams))
            SW.WriteLine(String.Format("{0}{1}", SetArray(10), MyVM.MyNumConf))
            SW.WriteLine(String.Format("{0}{1}", SetArray(11), MyVM.MyNumDiv))
            SW.WriteLine(String.Format("{0}{1}", SetArray(12), MyVM.MyFantasyDraft))
            SW.WriteLine(String.Format("{0}{1}", SetArray(13), MyVM.MyUserFired))
            SW.WriteLine(String.Format("{0}{1}", SetArray(14), MyVM.MyAllowExpansion))
            SW.WriteLine(String.Format("{0}{1}", SetArray(15), MyVM.MyAllowRelocation))
            SW.WriteLine(String.Format("{0}{1}", SetArray(16), MyVM.MyAllowFA))
            SW.WriteLine(String.Format("{0}{1}", SetArray(17), MyVM.MyAllowDraft))
            SW.WriteLine(String.Format("{0}{1}", SetArray(18), MyVM.MyNumDraftRounds))
            SW.WriteLine(String.Format("{0}{1}", SetArray(19), MyVM.AllowSuppDraft))
            SW.WriteLine(String.Format("{0}{1}", SetArray(20), MyVM.CompPicksForFALoss))
            For i = 0 To TempArray.Count - 1
                If TempArray(i) <> "" Then
                    TempArray(i) = ResetValues(TempArray(i))
                End If
            Next i
            SW.Flush()
            SW.WriteLine("LEAGUE FINANCE SETTINGS")
            SW.WriteLine(String.Format("{0}{1}", TempArray(0), MyVM.MySalCap))
            SW.WriteLine(String.Format("{0}{1}", TempArray(1), MyVM.MySalCapType))
            SW.WriteLine(String.Format("{0}{1}", TempArray(2), MyVM.MyLuxuryTax))
            SW.WriteLine(String.Format("{0}{1}", TempArray(3), MyVM.MyAdjustCap))
            SW.WriteLine(String.Format("{0}{1}", TempArray(4), MyVM.MyRookiePool))
            SW.WriteLine(String.Format("{0}{1}", TempArray(5), MyVM.MyCapCarryOver))
            SW.WriteLine(String.Format("{0}{1}", TempArray(6), MyVM.MyHomeTeamGate))
            SW.WriteLine(String.Format("{0}{1}", TempArray(7), MyVM.MyLeagueSalCap))
            SW.WriteLine(String.Format("{0}{1}", TempArray(8), MyVM.MyShareLuxBoxRev))
            SW.WriteLine(String.Format("{0}{1}", TempArray(9), MyVM.MyShareMerchRev))
            SW.WriteLine(String.Format("{0}{1}", TempArray(10), MyVM._myminconvalue(0)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(11), MyVM._myminconvalue(1)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(12), MyVM._myminconvalue(2)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(13), MyVM._myminconvalue(3)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(14), MyVM._myminconvalue(4)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(15), MyVM._myminconvalue(5)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(16), MyVM._myminconvalue(6)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(17), MyVM._myminconvalue(7)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(18), MyVM._myminconvalue(8)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(19), MyVM._myminconvalue(9)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(20), MyVM._myminconvalue(10)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(21), MyVM._myminconvalue(11)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(22), MyVM._myminconvalue(12)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(23), MyVM._myminconvalue(13)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(24), MyVM._myminconvalue(14)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(25), MyVM.MyAllowLowerVetMin))
            SW.WriteLine(String.Format("{0}{1}", TempArray(26), MyVM.MyVetMinNumYears))
            SW.WriteLine(String.Format("{0}{1}", TempArray(27), MyVM.MyVetMinContract))
            SW.Flush()
            SW.WriteLine(String.Format("{0}{1}", TempArray(28), MyVM._myfranchise(0)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(29), MyVM._myverygood(0)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(30), MyVM._mygood(0)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(31), MyVM._myaverage(0)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(32), MyVM._mybelowavg(0)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(33), MyVM._mydepth(0)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(34), MyVM._myfranchise(1)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(35), MyVM._myverygood(1)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(36), MyVM._mygood(1)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(37), MyVM._myaverage(1)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(38), MyVM._mybelowavg(1)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(39), MyVM._mydepth(1)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(40), MyVM._myfranchise(2)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(41), MyVM._myverygood(2)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(42), MyVM._mygood(2)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(43), MyVM._myaverage(2)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(44), MyVM._mybelowavg(2)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(45), MyVM._mydepth(2)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(46), MyVM._myfranchise(3)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(47), MyVM._myverygood(3)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(48), MyVM._mygood(3)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(49), MyVM._myaverage(3)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(50), MyVM._mybelowavg(3)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(51), MyVM._mydepth(3)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(52), MyVM._myfranchise(4)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(53), MyVM._myverygood(4)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(54), MyVM._mygood(4)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(55), MyVM._myaverage(4)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(56), MyVM._mybelowavg(4)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(57), MyVM._mydepth(4)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(58), MyVM._myfranchise(5)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(59), MyVM._myverygood(5)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(60), MyVM._mygood(5)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(61), MyVM._myaverage(5)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(62), MyVM._mybelowavg(5)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(63), MyVM._mydepth(5)))
            SW.Flush()
            SW.WriteLine(String.Format("{0}{1}", TempArray(64), MyVM._myfranchise(6)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(65), MyVM._myverygood(6)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(66), MyVM._mygood(6)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(67), MyVM._myaverage(6)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(68), MyVM._mybelowavg(6)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(69), MyVM._mydepth(6)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(70), MyVM._myfranchise(7)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(71), MyVM._myverygood(7)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(72), MyVM._mygood(7)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(73), MyVM._myaverage(7)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(74), MyVM._mybelowavg(7)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(75), MyVM._mydepth(7)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(76), MyVM._myfranchise(8)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(77), MyVM._myverygood(8)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(78), MyVM._mygood(8)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(79), MyVM._myaverage(8)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(80), MyVM._mybelowavg(8)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(81), MyVM._mydepth(8)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(82), MyVM._myfranchise(9)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(83), MyVM._myverygood(9)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(84), MyVM._mygood(9)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(85), MyVM._myaverage(9)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(86), MyVM._mybelowavg(9)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(87), MyVM._mydepth(9)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(88), MyVM._myfranchise(10)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(89), MyVM._myverygood(10)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(90), MyVM._mygood(10)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(91), MyVM._myaverage(10)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(92), MyVM._mybelowavg(10)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(93), MyVM._mydepth(10)))
            SW.Flush()
            SW.WriteLine(String.Format("{0}{1}", TempArray(94), MyVM._myfranchise(11)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(95), MyVM._myverygood(11)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(96), MyVM._mygood(11)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(97), MyVM._myaverage(11)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(98), MyVM._mybelowavg(11)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(99), MyVM._mydepth(11)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(100), MyVM._myfranchise(12)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(101), MyVM._myverygood(12)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(102), MyVM._mygood(12)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(103), MyVM._myaverage(12)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(104), MyVM._mybelowavg(12)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(105), MyVM._mydepth(12)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(106), MyVM._myfranchise(13)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(107), MyVM._myverygood(13)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(108), MyVM._mygood(13)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(109), MyVM._myaverage(13)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(110), MyVM._mybelowavg(13)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(111), MyVM._mydepth(13)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(112), MyVM._myfranchise(14)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(113), MyVM._myverygood(14)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(114), MyVM._mygood(14)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(115), MyVM._myaverage(14)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(116), MyVM._mybelowavg(14)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(117), MyVM._mydepth(14)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(118), MyVM._myfranchise(15)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(119), MyVM._myverygood(15)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(120), MyVM._mygood(15)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(121), MyVM._myaverage(15)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(122), MyVM._mybelowavg(15)))
            SW.WriteLine(String.Format("{0}{1}", TempArray(123), MyVM._mydepth(15)))

        End Using
    End Sub

    ''' <summary>
    '''     resets settings to default settings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LgFinResetBtn_Click(sender As Object, e As RoutedEventArgs)
        Dim i As Integer
        Using SR = New StreamReader(CurDir() + "\GLSDefaultSettings.txt")
            While SR.ReadLine <> "LEAGUE FINANCE SETTINGS" 'sets a loop until it reaches the proper line
                SetArray(i) = SR.ReadLine
                i += 1
            End While
            i = 0
            Do While SR.EndOfStream = False 'reads to end of file from this point
                TempArray(i) = SR.ReadLine
                i += 1
            Loop
        End Using

        i = 0
        Using SW = New StreamWriter(CurDir() + "\GLSsettings.text")
            SW.WriteLine("OVERALL LEAGUE SETTINGS")
            For i = 0 To SetArray.Count - 1
                SW.WriteLine(SetArray(i))
            Next i

            SW.WriteLine("LEAGUE FINANCE SETTINGS")
            For i = 0 To TempArray.Count - 1
                SW.WriteLine(TempArray(i))
            Next i
        End Using
    End Sub
End Class