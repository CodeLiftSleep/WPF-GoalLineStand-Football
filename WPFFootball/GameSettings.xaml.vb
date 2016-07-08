Imports System.IO

Public Class GameSettings
    Dim ReadOnly _myTeam as integer
    Dim ReadOnly _setArray(22) as string
    Dim ReadOnly _tempArray(125) as string
    dim _doOnce as Boolean
    dim ReadOnly MyVM as New GameSettingsViewModel

    Sub New(TeamID As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _myTeam = TeamID
        DataContext = MyVM
        MyVM.MyBackgroundImg = New BitmapImage(New Uri(NewGameViewModel.GetBackgroundFilePath(TeamID),
                                                               UriKind.RelativeOrAbsolute))
        LoadSettings() 'Loads the current settings file
    End Sub

    ''' <summary>
    '''     Saves any changes to game state and returns to previous menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Accept_OnClick(sender As Object, e As RoutedEventArgs)
        dim userTeam as new UserScreen(_myTeam)
        userTeam.Show()
        GetWindow(userTeam)
        close
    End Sub
    Private Function InitValues(tempstring As string) As string
        return tempstring.Substring(tempstring.LastIndexOf("=") + 1)
    End Function
    Private Function ResetValues(tempstring As string) As string
        if tempstring.Contains("=") Then
            Return tempstring.Substring(0, tempstring.LastIndexOf("="))
        Else
            Return Nothing
        end If

    End Function
    ''' <summary>
    ''' Loads settings from file
    ''' </summary>
    Private Sub LoadSettings
        if _doOnce = False
            dim i as integer
            dim mystring as string
            using SR = New StreamReader(CurDir + "\GLSsettings.txt")
                mystring = SR.Readline
                While mystring <> "LEAGUE FINANCE SETTINGS" 'sets a loop until it reaches the proper line
                    mystring = SR.ReadLine()
                    _setArray(i) = mystring
                    i += 1
                End While
                MyVM.MyStartYear = InitValues(_setArray(0))
                MyVM.MyLeagueRules = InitValues(_setArray(1))
                MyVM.MyLeagueType = InitValues(_setArray(2))
                MyVM.MyRosterSize = InitValues(_setArray(3))
                MyVM.MyInactives = InitValues(_setArray(4))
                MyVM.MyPracSquadSize = InitValues(_setArray(5))
                MyVM.MyOTFormat = InitValues(_setArray(6))
                MyVM.MyFieldType = InitValues(_setArray(7))
                MyVM.MyPenalties = InitValues(_setArray(8))
                MyVM.MyNumTeams = InitValues(_setArray(9))
                MyVM.MyNumConf = InitValues(_setArray(10))
                MyVM.MyNumDiv = InitValues(_setArray(11))
                MyVM.MyFantasyDraft = InitValues(_setArray(12))
                MyVM.MyUserFired = InitValues(_setArray(13))
                MyVM.MyAllowExpansion = InitValues(_setArray(14))
                MyVM.MyAllowRelocation = InitValues(_setArray(15))
                MyVM.MyAllowFA = InitValues(_setArray(16))
                MyVM.MyAllowDraft = InitValues(_setArray(17))
                MyVM.MyNumDraftRounds = InitValues(_setArray(18))
                MyVM.AllowSuppDraft = InitValues(_setArray(19))
                MyVM.CompPicksForFALoss = InitValues(_setArray(20))
                i = 0
                Do While SR.EndOfStream = False 'reads to end of file from this point
                    _tempArray(i) = SR.ReadLine
                    i += 1
                Loop
                MyVM.MySalCap = InitValues(_tempArray(0))
                MyVM.MySalCapType = InitValues(_tempArray(1))
                MyVM.MyLuxuryTax = InitValues(_tempArray(2))
                MyVM.MyAdjustCap = InitValues(_tempArray(3))
                MyVM.MyRookiePool = InitValues(_tempArray(4))
                MyVM.MyCapCarryOver = InitValues(_tempArray(5))
                MyVM.MyHomeTeamGate = InitValues(_tempArray(6))
                MyVM.MyLeagueSalCap = InitValues(_tempArray(7))
                MyVM.MyShareLuxBoxRev = InitValues(_tempArray(8))
                MyVM.MyShareMerchRev = InitValues(_tempArray(9))
                MyVM._myminconvalue(0) = InitValues(_tempArray(10))
                MyVM._myminconvalue(1) = InitValues(_tempArray(11))
                MyVM._myminconvalue(2) = InitValues(_tempArray(12))
                MyVM._myminconvalue(3) = InitValues(_tempArray(13))
                MyVM._myminconvalue(4) = InitValues(_tempArray(14))
                MyVM._myminconvalue(5) = InitValues(_tempArray(15))
                MyVM._myminconvalue(6) = InitValues(_tempArray(16))
                MyVM._myminconvalue(7) = InitValues(_tempArray(17))
                MyVM._myminconvalue(8) = InitValues(_tempArray(18))
                MyVM._myminconvalue(9) = InitValues(_tempArray(19))
                MyVM._myminconvalue(10) = InitValues(_tempArray(20))
                MyVM._myminconvalue(11) = InitValues(_tempArray(21))
                MyVM._myminconvalue(12) = InitValues(_tempArray(22))
                MyVM._myminconvalue(13) = InitValues(_tempArray(23))
                MyVM.MyAllowLowerVetMin = InitValues(_tempArray(24))
                MyVM.MyVetMinNumYears = InitValues(_tempArray(25))
                MyVM.MyVetMinContract = InitValues(_tempArray(26))
                MyVM._myfranchise(0) = InitValues(_tempArray(27))
                MyVM._myverygood(0) = InitValues(_tempArray(28))
                MyVM._mygood(0) = InitValues(_tempArray(29))
                MyVM._myaverage(0) = InitValues(_tempArray(30))
                MyVM._mybelowavg(0) = InitValues(_tempArray(31))
                MyVM._mydepth(0) = InitValues(_tempArray(32))
                MyVM._myfranchise(1) = InitValues(_tempArray(33))
                MyVM._myverygood(1) = InitValues(_tempArray(34))
                MyVM._mygood(1) = InitValues(_tempArray(35))
                MyVM._myaverage(1) = InitValues(_tempArray(36))
                MyVM._mybelowavg(1) = InitValues(_tempArray(37))
                MyVM._mydepth(1) = InitValues(_tempArray(38))
                MyVM._myfranchise(2) = InitValues(_tempArray(39))
                MyVM._myverygood(2) = InitValues(_tempArray(40))
                MyVM._mygood(2) = InitValues(_tempArray(41))
                MyVM._myaverage(2) = InitValues(_tempArray(42))
                MyVM._mybelowavg(2) = InitValues(_tempArray(43))
                MyVM._mydepth(2) = InitValues(_tempArray(44))
                MyVM._myfranchise(3) = InitValues(_tempArray(45))
                MyVM._myverygood(3) = InitValues(_tempArray(46))
                MyVM._mygood(3) = InitValues(_tempArray(47))
                MyVM._myaverage(3) = InitValues(_tempArray(48))
                MyVM._mybelowavg(3) = InitValues(_tempArray(49))
                MyVM._mydepth(3) = InitValues(_tempArray(50))
                MyVM._myfranchise(4) = InitValues(_tempArray(51))
                MyVM._myverygood(4) = InitValues(_tempArray(52))
                MyVM._mygood(4) = InitValues(_tempArray(53))
                MyVM._myaverage(4) = InitValues(_tempArray(54))
                MyVM._mybelowavg(4) = InitValues(_tempArray(55))
                MyVM._mydepth(4) = InitValues(_tempArray(56))
                MyVM._myfranchise(5) = InitValues(_tempArray(57))
                MyVM._myverygood(5) = InitValues(_tempArray(58))
                MyVM._mygood(5) = InitValues(_tempArray(59))
                MyVM._myaverage(5) = InitValues(_tempArray(60))
                MyVM._mybelowavg(5) = InitValues(_tempArray(61))
                MyVM._mydepth(5) = InitValues(_tempArray(62))
                MyVM._myfranchise(6) = InitValues(_tempArray(63))
                MyVM._myverygood(6) = InitValues(_tempArray(64))
                MyVM._mygood(6) = InitValues(_tempArray(65))
                MyVM._myaverage(6) = InitValues(_tempArray(66))
                MyVM._mybelowavg(6) = InitValues(_tempArray(67))
                MyVM._mydepth(6) = InitValues(_tempArray(68))
                MyVM._myfranchise(7) = InitValues(_tempArray(69))
                MyVM._myverygood(7) = InitValues(_tempArray(70))
                MyVM._mygood(7) = InitValues(_tempArray(71))
                MyVM._myaverage(7) = InitValues(_tempArray(72))
                MyVM._mybelowavg(7) = InitValues(_tempArray(73))
                MyVM._mydepth(7) = InitValues(_tempArray(74))
                MyVM._myfranchise(8) = InitValues(_tempArray(75))
                MyVM._myverygood(8) = InitValues(_tempArray(76))
                MyVM._mygood(8) = InitValues(_tempArray(77))
                MyVM._myaverage(8) = InitValues(_tempArray(78))
                MyVM._mybelowavg(8) = InitValues(_tempArray(79))
                MyVM._mydepth(8) = InitValues(_tempArray(80))
                MyVM._myfranchise(9) = InitValues(_tempArray(81))
                MyVM._myverygood(9) = InitValues(_tempArray(82))
                MyVM._mygood(9) = InitValues(_tempArray(83))
                MyVM._myaverage(9) = InitValues(_tempArray(84))
                MyVM._mybelowavg(9) = InitValues(_tempArray(85))
                MyVM._mydepth(9) = InitValues(_tempArray(86))
                MyVM._myfranchise(10) = InitValues(_tempArray(87))
                MyVM._myverygood(10) = InitValues(_tempArray(88))
                MyVM._mygood(10) = InitValues(_tempArray(89))
                MyVM._myaverage(10) = InitValues(_tempArray(90))
                MyVM._mybelowavg(10) = InitValues(_tempArray(91))
                MyVM._mydepth(10) = InitValues(_tempArray(92))
                MyVM._myfranchise(11) = InitValues(_tempArray(93))
                MyVM._myverygood(11) = InitValues(_tempArray(94))
                MyVM._mygood(11) = InitValues(_tempArray(95))
                MyVM._myaverage(11) = InitValues(_tempArray(96))
                MyVM._mybelowavg(11) = InitValues(_tempArray(97))
                MyVM._mydepth(11) = InitValues(_tempArray(98))
                MyVM._myfranchise(12) = InitValues(_tempArray(99))
                MyVM._myverygood(12) = InitValues(_tempArray(100))
                MyVM._mygood(12) = InitValues(_tempArray(101))
                MyVM._myaverage(12) = InitValues(_tempArray(102))
                MyVM._mybelowavg(12) = InitValues(_tempArray(103))
                MyVM._mydepth(12) = InitValues(_tempArray(104))
                MyVM._myfranchise(13) = InitValues(_tempArray(105))
                MyVM._myverygood(13) = InitValues(_tempArray(106))
                MyVM._mygood(13) = InitValues(_tempArray(107))
                MyVM._myaverage(13) = InitValues(_tempArray(108))
                MyVM._mybelowavg(13) = InitValues(_tempArray(109))
                MyVM._mydepth(13) = InitValues(_tempArray(110))
                MyVM._myfranchise(14) = InitValues(_tempArray(111))
                MyVM._myverygood(14) = InitValues(_tempArray(112))
                MyVM._mygood(14) = InitValues(_tempArray(113))
                MyVM._myaverage(14) = InitValues(_tempArray(114))
                MyVM._mybelowavg(14) = InitValues(_tempArray(115))
                MyVM._mydepth(14) = InitValues(_tempArray(116))
                MyVM._myfranchise(15) = InitValues(_tempArray(117))
                MyVM._myverygood(15) = InitValues(_tempArray(118))
                MyVM._mygood(15) = InitValues(_tempArray(119))
                MyVM._myaverage(15) = InitValues(_tempArray(120))
                MyVM._mybelowavg(15) = InitValues(_tempArray(121))
                MyVM._mydepth(15) = InitValues(_tempArray(122))
                _doOnce = True
            end using
        end if
    End Sub
    ''' <summary>
    '''     Writes changes to settings file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LgFinAcceptBtn_Click(sender As Object, e As RoutedEventArgs)
        dim DelFile as String = CurDir + "\GLSsettings.text" 'deletes current settings file and replaces with new one
        if File.Exists(DelFile) Then
            File.delete(DelFile)
        End If

        using SW = New StreamWriter(CurDir + "\GLSsettings.text")
            SW.WriteLine("LEAGUE GAME SETTINGS")
            for i = 0 to _setArray.Count - 1
                if _setArray(i) <> "" Then
                    _setArray(i) = ResetValues(_setArray(i))
                end if
            Next i

            SW.WriteLine(String.Format("{0}={1}", _setArray(0), MyVM.MyStartYear))
            SW.WriteLine(String.Format("{0}={1}", _setArray(1), MYVM.MyLeagueRules))
            SW.WriteLine(string.Format("{0}={1}", _setArray(2), MyVM.MyLeagueType))
            SW.WriteLine(string.Format("{0}={1}", _setArray(3), MyVM.MyRosterSize))
            SW.WriteLine(string.Format("{0}={1}", _setArray(4), MyVM.MyInactives))
            SW.WriteLine(string.Format("{0}={1}", _setArray(5), MyVM.MyPracSquadSize))
            SW.WriteLine(string.Format("{0}={1}", _setArray(6), MyVM.MyOTFormat))
            SW.WriteLine(string.Format("{0}={1}", _setArray(7), MyVM.MyFieldType))
            SW.WriteLine(string.Format("{0}={1}", _setArray(8), MyVM.MyPenalties))
            SW.WriteLine(string.Format("{0}={1}", _setArray(9), MyVM.Mynumteams))
            SW.WriteLine(string.Format("{0}={1}", _setArray(10), MyVM.Mynumconf))
            SW.WriteLine(string.Format("{0}={1}", _setArray(11), MyVM.Mynumdiv))
            SW.WriteLine(string.Format("{0}={1}", _setArray(12), MyVM.Myfantasydraft))
            SW.WriteLine(string.Format("{0}={1}", _setArray(13), MyVM.MyUserFired))
            SW.WriteLine(string.Format("{0}={1}", _setArray(14), MyVM.MyAllowExpansion))
            SW.WriteLine(string.Format("{0}={1}", _setArray(15), MyVM.MyAllowRelocation))
            SW.WriteLine(string.Format("{0}={1}", _setArray(16), MyVM.MyAllowFA))
            SW.WriteLine(string.Format("{0}={1}", _setArray(17), MyVM.MyAllowDraft))
            SW.WriteLine(string.Format("{0}={1}", _setArray(18), MyVM.MyNumDraftRounds))
            SW.WriteLine(string.Format("{0}={1}", _setArray(19), MyVM.AllowSuppDraft))
            SW.WriteLine(string.Format("{0}={1}", _setArray(20), MyVM.CompPicksForFALoss))
            for i = 0 To _tempArray.Count - 1
                if _tempArray(i) <> "" Then
                    _tempArray(i) = ResetValues(_tempArray(i))
                End If
            Next i
            SW.Flush()
            SW.WriteLine("LEAGUE FINANCE SETTINGS")
            SW.WriteLine(string.format("{0}={1}", _tempArray(0), MyVM.MySalCap))
            SW.WriteLine(string.format("{0}={1}", _tempArray(1), MyVM.MySalCapType))
            SW.WriteLine(string.format("{0}={1}", _tempArray(2), MyVM.MyLuxuryTax))
            SW.WriteLine(string.format("{0}={1}", _tempArray(3), MyVM.MyAdjustCap))
            SW.WriteLine(string.format("{0}={1}", _tempArray(4), MyVM.MyRookiePool))
            SW.WriteLine(string.format("{0}={1}", _tempArray(5), MyVM.MyCapCarryOver))
            SW.WriteLine(string.format("{0}={1}", _tempArray(6), MyVM.MyHomeTeamGate))
            SW.WriteLine(string.format("{0}={1}", _tempArray(7), MyVM.MyLeagueSalCap))
            SW.WriteLine(string.format("{0}={1}", _tempArray(8), MyVM.MyShareLuxBoxRev))
            SW.WriteLine(string.format("{0}={1}", _tempArray(9), MyVM.MyShareMerchRev))
            SW.WriteLine(string.format("{0}={1}", _tempArray(10), MyVM._myminconvalue(0)))
            SW.WriteLine(String.Format("{0}={1}", _tempArray(11), MyVM._myminconvalue(1)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(12), MyVM._myminconvalue(2)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(13), MyVM._myminconvalue(3)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(14), MyVM._myminconvalue(4)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(15), MyVM._myminconvalue(5)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(16), MyVM._myminconvalue(6)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(17), MyVM._myminconvalue(7)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(18), MyVM._myminconvalue(8)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(19), MyVM._myminconvalue(9)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(20), MyVM._myminconvalue(10)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(21), MyVM._myminconvalue(11)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(22), MyVM._myminconvalue(12)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(23), MyVM._myminconvalue(13)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(24), MyVM.MyAllowLowerVetMin))
            SW.WriteLine(string.format("{0}={1}", _tempArray(25), MyVM.MyVetMinNumYears))
            SW.WriteLine(string.format("{0}={1}", _tempArray(26), MyVM.MyVetMinContract))
            SW.Flush
            SW.WriteLine(string.format("{0}={1}", _tempArray(27), MyVM._myfranchise(0)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(28), MyVM._myverygood(0)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(29), MyVM._mygood(0)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(30), MyVM._myaverage(0)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(31), MyVM._mybelowavg(0)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(32), MyVM._mydepth(0)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(33), MyVM._myfranchise(1)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(34), MyVM._myverygood(1)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(35), MyVM._mygood(1)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(36), MyVM._myaverage(1)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(37), MyVM._mybelowavg(1)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(38), MyVM._mydepth(1)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(39), MyVM._myfranchise(2)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(40), MyVM._myverygood(2)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(41), MyVM._mygood(2)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(42), MyVM._myaverage(2)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(43), MyVM._mybelowavg(2)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(44), MyVM._mydepth(2)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(45), MyVM._myfranchise(3)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(46), MyVM._myverygood(3)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(47), MyVM._mygood(3)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(48), MyVM._myaverage(3)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(49), MyVM._mybelowavg(3)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(50), MyVM._mydepth(3)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(51), MyVM._myfranchise(4)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(52), MyVM._myverygood(4)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(53), MyVM._mygood(4)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(54), MyVM._myaverage(4)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(55), MyVM._mybelowavg(4)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(56), MyVM._mydepth(4)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(57), MyVM._myfranchise(5)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(58), MyVM._myverygood(5)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(59), MyVM._mygood(5)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(60), MyVM._myaverage(5)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(61), MyVM._mybelowavg(5)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(62), MyVM._mydepth(5)))
            SW.Flush
            SW.WriteLine(string.format("{0}={1}", _tempArray(63), MyVM._myfranchise(6)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(64), MyVM._myverygood(6)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(65), MyVM._mygood(6)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(66), MyVM._myaverage(6)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(67), MyVM._mybelowavg(6)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(68), MyVM._mydepth(6)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(69), MyVM._myfranchise(7)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(70), MyVM._myverygood(7)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(71), MyVM._mygood(7)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(72), MyVM._myaverage(7)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(73), MyVM._mybelowavg(7)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(74), MyVM._mydepth(7)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(75), MyVM._myfranchise(8)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(76), MyVM._myverygood(8)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(77), MyVM._mygood(8)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(78), MyVM._myaverage(8)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(79), MyVM._mybelowavg(8)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(80), MyVM._mydepth(8)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(81), MyVM._myfranchise(9)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(82), MyVM._myverygood(9)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(83), MyVM._mygood(9)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(84), MyVM._myaverage(9)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(85), MyVM._mybelowavg(9)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(86), MyVM._mydepth(9)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(87), MyVM._myfranchise(10)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(88), MyVM._myverygood(10)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(89), MyVM._mygood(10)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(90), MyVM._myaverage(10)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(91), MyVM._mybelowavg(10)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(92), MyVM._mydepth(10)))
            SW.Flush
            SW.WriteLine(string.format("{0}={1}", _tempArray(93), MyVM._myfranchise(11)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(94), MyVM._myverygood(11)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(95), MyVM._mygood(11)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(96), MyVM._myaverage(11)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(97), MyVM._mybelowavg(11)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(98), MyVM._mydepth(11)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(99), MyVM._myfranchise(12)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(100), MyVM._myverygood(12)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(101), MyVM._mygood(12)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(102), MyVM._myaverage(12)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(103), MyVM._mybelowavg(12)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(104), MyVM._mydepth(12)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(105), MyVM._myfranchise(13)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(106), MyVM._myverygood(13)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(107), MyVM._mygood(13)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(108), MyVM._myaverage(13)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(109), MyVM._mybelowavg(13)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(110), MyVM._mydepth(13)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(111), MyVM._myfranchise(14)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(112), MyVM._myverygood(14)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(113), MyVM._mygood(14)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(114), MyVM._myaverage(14)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(115), MyVM._mybelowavg(14)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(116), MyVM._mydepth(14)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(117), MyVM._myfranchise(15)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(118), MyVM._myverygood(15)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(119), MyVM._mygood(15)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(120), MyVM._myaverage(15)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(121), MyVM._mybelowavg(15)))
            SW.WriteLine(string.format("{0}={1}", _tempArray(122), MyVM._mydepth(15)))
        end using
    End Sub

    ''' <summary>
    '''     resets settings to default settings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LgFinResetBtn_Click(sender As Object, e As RoutedEventArgs)
        dim i as integer
        using SR = New StreamReader(CurDir + "\GLSDefaultSettings.txt")
            While SR.ReadLine <> "LEAGUE FINANCE SETTINGS" 'sets a loop until it reaches the proper line
                _setArray(i) = SR.ReadLine
                i += 1
            End While
            i = 0
            Do While SR.EndOfStream = False 'reads to end of file from this point
                _tempArray(i) = SR.ReadLine
                i += 1
            Loop
        end using

        i = 0
        using SW = New StreamWriter(CurDir + "\GLSsettings.text")
            SW.WriteLine("OVERALL LEAGUE SETTINGS")
            for i = 0 to _setArray.Count - 1
                SW.WriteLine(_setArray(i))
            Next i

            SW.WriteLine("LEAGUE FINANCE SETTINGS")
            for i = 0 To _tempArray.Count - 1
                SW.WriteLine(_tempArray(i))
            Next i
        end using
    End Sub
End Class