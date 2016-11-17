Imports System.IO

Public Class GameSettings
    ReadOnly _MyTeam As Integer
    ReadOnly _SetArray(22) As String
    ReadOnly _TempArray(125) As String
    Dim _DoOnce As Boolean
    ReadOnly MyVM As New GameSettingsViewModel

    Sub New(TeamID As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _MyTeam = TeamID
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
        Dim userTeam As New UserScreen(_MyTeam)
        userTeam.Show()
        Me.Close()
    End Sub
    Private Function InitValues(tempstring As String) As String
        Return tempstring.Substring(tempstring.LastIndexOf("=") + 1)
    End Function
    Private Function ResetValues(tempstring As String) As String
        If tempstring.Contains("=") Then
            Return tempstring.Substring(0, tempstring.LastIndexOf("="))
        Else
            Return Nothing
        End If

    End Function
    ''' <summary>
    ''' Loads settings from file
    ''' </summary>
    Private Sub LoadSettings()
        If _DoOnce = False Then
            Dim i As Integer
            Dim mystring As String
            Using SR = New StreamReader(CurDir() + "\GLSsettings.txt")
                mystring = SR.ReadLine
                While mystring <> "LEAGUE FINANCE SETTINGS" 'sets a loop until it reaches the proper line
                    mystring = SR.ReadLine()
                    _SetArray(i) = mystring
                    i += 1
                End While
                MyVM.MyStartYear = InitValues(_SetArray(0))
                MyVM.MyLeagueRules = InitValues(_SetArray(1))
                MyVM.MyLeagueType = InitValues(_SetArray(2))
                MyVM.MyRosterSize = InitValues(_SetArray(3))
                MyVM.MyInactives = InitValues(_SetArray(4))
                MyVM.MyPracSquadSize = InitValues(_SetArray(5))
                MyVM.MyOTFormat = InitValues(_SetArray(6))
                MyVM.MyFieldType = InitValues(_SetArray(7))
                MyVM.MyPenalties = InitValues(_SetArray(8))
                MyVM.MyNumTeams = InitValues(_SetArray(9))
                MyVM.MyNumConf = InitValues(_SetArray(10))
                MyVM.MyNumDiv = InitValues(_SetArray(11))
                MyVM.MyFantasyDraft = InitValues(_SetArray(12))
                MyVM.MyUserFired = InitValues(_SetArray(13))
                MyVM.MyAllowExpansion = InitValues(_SetArray(14))
                MyVM.MyAllowRelocation = InitValues(_SetArray(15))
                MyVM.MyAllowFA = InitValues(_SetArray(16))
                MyVM.MyAllowDraft = InitValues(_SetArray(17))
                MyVM.MyNumDraftRounds = InitValues(_SetArray(18))
                MyVM.AllowSuppDraft = InitValues(_SetArray(19))
                MyVM.CompPicksForFALoss = InitValues(_SetArray(20))
                i = 0
                Do While SR.EndOfStream = False 'reads to end of file from this point
                    _TempArray(i) = SR.ReadLine
                    i += 1
                Loop
                MyVM.MySalCap = InitValues(_TempArray(0))
                MyVM.MySalCapType = InitValues(_TempArray(1))
                MyVM.MyLuxuryTax = InitValues(_TempArray(2))
                MyVM.MyAdjustCap = InitValues(_TempArray(3))
                MyVM.MyRookiePool = InitValues(_TempArray(4))
                MyVM.MyCapCarryOver = InitValues(_TempArray(5))
                MyVM.MyHomeTeamGate = InitValues(_TempArray(6))
                MyVM.MyLeagueSalCap = InitValues(_TempArray(7))
                MyVM.MyShareLuxBoxRev = InitValues(_TempArray(8))
                MyVM.MyShareMerchRev = InitValues(_TempArray(9))
                MyVM._myminconvalue(0) = InitValues(_TempArray(10))
                MyVM._myminconvalue(1) = InitValues(_TempArray(11))
                MyVM._myminconvalue(2) = InitValues(_TempArray(12))
                MyVM._myminconvalue(3) = InitValues(_TempArray(13))
                MyVM._myminconvalue(4) = InitValues(_TempArray(14))
                MyVM._myminconvalue(5) = InitValues(_TempArray(15))
                MyVM._myminconvalue(6) = InitValues(_TempArray(16))
                MyVM._myminconvalue(7) = InitValues(_TempArray(17))
                MyVM._myminconvalue(8) = InitValues(_TempArray(18))
                MyVM._myminconvalue(9) = InitValues(_TempArray(19))
                MyVM._myminconvalue(10) = InitValues(_TempArray(20))
                MyVM._myminconvalue(11) = InitValues(_TempArray(21))
                MyVM._myminconvalue(12) = InitValues(_TempArray(22))
                MyVM._myminconvalue(13) = InitValues(_TempArray(23))
                MyVM.MyAllowLowerVetMin = InitValues(_TempArray(24))
                MyVM.MyVetMinNumYears = InitValues(_TempArray(25))
                MyVM.MyVetMinContract = InitValues(_TempArray(26))
                MyVM._myfranchise(0) = InitValues(_TempArray(27))
                MyVM._myverygood(0) = InitValues(_TempArray(28))
                MyVM._mygood(0) = InitValues(_TempArray(29))
                MyVM._myaverage(0) = InitValues(_TempArray(30))
                MyVM._mybelowavg(0) = InitValues(_TempArray(31))
                MyVM._mydepth(0) = InitValues(_TempArray(32))
                MyVM._myfranchise(1) = InitValues(_TempArray(33))
                MyVM._myverygood(1) = InitValues(_TempArray(34))
                MyVM._mygood(1) = InitValues(_TempArray(35))
                MyVM._myaverage(1) = InitValues(_TempArray(36))
                MyVM._mybelowavg(1) = InitValues(_TempArray(37))
                MyVM._mydepth(1) = InitValues(_TempArray(38))
                MyVM._myfranchise(2) = InitValues(_TempArray(39))
                MyVM._myverygood(2) = InitValues(_TempArray(40))
                MyVM._mygood(2) = InitValues(_TempArray(41))
                MyVM._myaverage(2) = InitValues(_TempArray(42))
                MyVM._mybelowavg(2) = InitValues(_TempArray(43))
                MyVM._mydepth(2) = InitValues(_TempArray(44))
                MyVM._myfranchise(3) = InitValues(_TempArray(45))
                MyVM._myverygood(3) = InitValues(_TempArray(46))
                MyVM._mygood(3) = InitValues(_TempArray(47))
                MyVM._myaverage(3) = InitValues(_TempArray(48))
                MyVM._mybelowavg(3) = InitValues(_TempArray(49))
                MyVM._mydepth(3) = InitValues(_TempArray(50))
                MyVM._myfranchise(4) = InitValues(_TempArray(51))
                MyVM._myverygood(4) = InitValues(_TempArray(52))
                MyVM._mygood(4) = InitValues(_TempArray(53))
                MyVM._myaverage(4) = InitValues(_TempArray(54))
                MyVM._mybelowavg(4) = InitValues(_TempArray(55))
                MyVM._mydepth(4) = InitValues(_TempArray(56))
                MyVM._myfranchise(5) = InitValues(_TempArray(57))
                MyVM._myverygood(5) = InitValues(_TempArray(58))
                MyVM._mygood(5) = InitValues(_TempArray(59))
                MyVM._myaverage(5) = InitValues(_TempArray(60))
                MyVM._mybelowavg(5) = InitValues(_TempArray(61))
                MyVM._mydepth(5) = InitValues(_TempArray(62))
                MyVM._myfranchise(6) = InitValues(_TempArray(63))
                MyVM._myverygood(6) = InitValues(_TempArray(64))
                MyVM._mygood(6) = InitValues(_TempArray(65))
                MyVM._myaverage(6) = InitValues(_TempArray(66))
                MyVM._mybelowavg(6) = InitValues(_TempArray(67))
                MyVM._mydepth(6) = InitValues(_TempArray(68))
                MyVM._myfranchise(7) = InitValues(_TempArray(69))
                MyVM._myverygood(7) = InitValues(_TempArray(70))
                MyVM._mygood(7) = InitValues(_TempArray(71))
                MyVM._myaverage(7) = InitValues(_TempArray(72))
                MyVM._mybelowavg(7) = InitValues(_TempArray(73))
                MyVM._mydepth(7) = InitValues(_TempArray(74))
                MyVM._myfranchise(8) = InitValues(_TempArray(75))
                MyVM._myverygood(8) = InitValues(_TempArray(76))
                MyVM._mygood(8) = InitValues(_TempArray(77))
                MyVM._myaverage(8) = InitValues(_TempArray(78))
                MyVM._mybelowavg(8) = InitValues(_TempArray(79))
                MyVM._mydepth(8) = InitValues(_TempArray(80))
                MyVM._myfranchise(9) = InitValues(_TempArray(81))
                MyVM._myverygood(9) = InitValues(_TempArray(82))
                MyVM._mygood(9) = InitValues(_TempArray(83))
                MyVM._myaverage(9) = InitValues(_TempArray(84))
                MyVM._mybelowavg(9) = InitValues(_TempArray(85))
                MyVM._mydepth(9) = InitValues(_TempArray(86))
                MyVM._myfranchise(10) = InitValues(_TempArray(87))
                MyVM._myverygood(10) = InitValues(_TempArray(88))
                MyVM._mygood(10) = InitValues(_TempArray(89))
                MyVM._myaverage(10) = InitValues(_TempArray(90))
                MyVM._mybelowavg(10) = InitValues(_TempArray(91))
                MyVM._mydepth(10) = InitValues(_TempArray(92))
                MyVM._myfranchise(11) = InitValues(_TempArray(93))
                MyVM._myverygood(11) = InitValues(_TempArray(94))
                MyVM._mygood(11) = InitValues(_TempArray(95))
                MyVM._myaverage(11) = InitValues(_TempArray(96))
                MyVM._mybelowavg(11) = InitValues(_TempArray(97))
                MyVM._mydepth(11) = InitValues(_TempArray(98))
                MyVM._myfranchise(12) = InitValues(_TempArray(99))
                MyVM._myverygood(12) = InitValues(_TempArray(100))
                MyVM._mygood(12) = InitValues(_TempArray(101))
                MyVM._myaverage(12) = InitValues(_TempArray(102))
                MyVM._mybelowavg(12) = InitValues(_TempArray(103))
                MyVM._mydepth(12) = InitValues(_TempArray(104))
                MyVM._myfranchise(13) = InitValues(_TempArray(105))
                MyVM._myverygood(13) = InitValues(_TempArray(106))
                MyVM._mygood(13) = InitValues(_TempArray(107))
                MyVM._myaverage(13) = InitValues(_TempArray(108))
                MyVM._mybelowavg(13) = InitValues(_TempArray(109))
                MyVM._mydepth(13) = InitValues(_TempArray(110))
                MyVM._myfranchise(14) = InitValues(_TempArray(111))
                MyVM._myverygood(14) = InitValues(_TempArray(112))
                MyVM._mygood(14) = InitValues(_TempArray(113))
                MyVM._myaverage(14) = InitValues(_TempArray(114))
                MyVM._mybelowavg(14) = InitValues(_TempArray(115))
                MyVM._mydepth(14) = InitValues(_TempArray(116))
                MyVM._myfranchise(15) = InitValues(_TempArray(117))
                MyVM._myverygood(15) = InitValues(_TempArray(118))
                MyVM._mygood(15) = InitValues(_TempArray(119))
                MyVM._myaverage(15) = InitValues(_TempArray(120))
                MyVM._mybelowavg(15) = InitValues(_TempArray(121))
                MyVM._mydepth(15) = InitValues(_TempArray(122))
                _DoOnce = True
            End Using
        End If
    End Sub
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
            For i = 0 To _SetArray.Count - 1
                If _SetArray(i) <> "" Then
                    _SetArray(i) = ResetValues(_SetArray(i))
                End If
            Next i

            SW.WriteLine(String.Format("{0}={1}", _SetArray(0), MyVM.MyStartYear))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(1), MyVM.MyLeagueRules))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(2), MyVM.MyLeagueType))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(3), MyVM.MyRosterSize))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(4), MyVM.MyInactives))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(5), MyVM.MyPracSquadSize))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(6), MyVM.MyOTFormat))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(7), MyVM.MyFieldType))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(8), MyVM.MyPenalties))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(9), MyVM.MyNumTeams))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(10), MyVM.MyNumConf))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(11), MyVM.MyNumDiv))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(12), MyVM.MyFantasyDraft))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(13), MyVM.MyUserFired))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(14), MyVM.MyAllowExpansion))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(15), MyVM.MyAllowRelocation))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(16), MyVM.MyAllowFA))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(17), MyVM.MyAllowDraft))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(18), MyVM.MyNumDraftRounds))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(19), MyVM.AllowSuppDraft))
            SW.WriteLine(String.Format("{0}={1}", _SetArray(20), MyVM.CompPicksForFALoss))
            For i = 0 To _TempArray.Count - 1
                If _TempArray(i) <> "" Then
                    _TempArray(i) = ResetValues(_TempArray(i))
                End If
            Next i
            SW.Flush()
            SW.WriteLine("LEAGUE FINANCE SETTINGS")
            SW.WriteLine(String.Format("{0}={1}", _TempArray(0), MyVM.MySalCap))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(1), MyVM.MySalCapType))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(2), MyVM.MyLuxuryTax))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(3), MyVM.MyAdjustCap))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(4), MyVM.MyRookiePool))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(5), MyVM.MyCapCarryOver))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(6), MyVM.MyHomeTeamGate))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(7), MyVM.MyLeagueSalCap))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(8), MyVM.MyShareLuxBoxRev))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(9), MyVM.MyShareMerchRev))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(10), MyVM._myminconvalue(0)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(11), MyVM._myminconvalue(1)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(12), MyVM._myminconvalue(2)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(13), MyVM._myminconvalue(3)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(14), MyVM._myminconvalue(4)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(15), MyVM._myminconvalue(5)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(16), MyVM._myminconvalue(6)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(17), MyVM._myminconvalue(7)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(18), MyVM._myminconvalue(8)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(19), MyVM._myminconvalue(9)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(20), MyVM._myminconvalue(10)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(21), MyVM._myminconvalue(11)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(22), MyVM._myminconvalue(12)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(23), MyVM._myminconvalue(13)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(24), MyVM.MyAllowLowerVetMin))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(25), MyVM.MyVetMinNumYears))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(26), MyVM.MyVetMinContract))
            SW.Flush()
            SW.WriteLine(String.Format("{0}={1}", _TempArray(27), MyVM._myfranchise(0)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(28), MyVM._myverygood(0)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(29), MyVM._mygood(0)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(30), MyVM._myaverage(0)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(31), MyVM._mybelowavg(0)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(32), MyVM._mydepth(0)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(33), MyVM._myfranchise(1)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(34), MyVM._myverygood(1)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(35), MyVM._mygood(1)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(36), MyVM._myaverage(1)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(37), MyVM._mybelowavg(1)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(38), MyVM._mydepth(1)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(39), MyVM._myfranchise(2)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(40), MyVM._myverygood(2)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(41), MyVM._mygood(2)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(42), MyVM._myaverage(2)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(43), MyVM._mybelowavg(2)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(44), MyVM._mydepth(2)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(45), MyVM._myfranchise(3)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(46), MyVM._myverygood(3)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(47), MyVM._mygood(3)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(48), MyVM._myaverage(3)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(49), MyVM._mybelowavg(3)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(50), MyVM._mydepth(3)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(51), MyVM._myfranchise(4)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(52), MyVM._myverygood(4)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(53), MyVM._mygood(4)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(54), MyVM._myaverage(4)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(55), MyVM._mybelowavg(4)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(56), MyVM._mydepth(4)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(57), MyVM._myfranchise(5)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(58), MyVM._myverygood(5)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(59), MyVM._mygood(5)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(60), MyVM._myaverage(5)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(61), MyVM._mybelowavg(5)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(62), MyVM._mydepth(5)))
            SW.Flush()
            SW.WriteLine(String.Format("{0}={1}", _TempArray(63), MyVM._myfranchise(6)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(64), MyVM._myverygood(6)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(65), MyVM._mygood(6)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(66), MyVM._myaverage(6)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(67), MyVM._mybelowavg(6)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(68), MyVM._mydepth(6)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(69), MyVM._myfranchise(7)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(70), MyVM._myverygood(7)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(71), MyVM._mygood(7)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(72), MyVM._myaverage(7)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(73), MyVM._mybelowavg(7)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(74), MyVM._mydepth(7)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(75), MyVM._myfranchise(8)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(76), MyVM._myverygood(8)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(77), MyVM._mygood(8)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(78), MyVM._myaverage(8)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(79), MyVM._mybelowavg(8)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(80), MyVM._mydepth(8)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(81), MyVM._myfranchise(9)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(82), MyVM._myverygood(9)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(83), MyVM._mygood(9)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(84), MyVM._myaverage(9)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(85), MyVM._mybelowavg(9)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(86), MyVM._mydepth(9)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(87), MyVM._myfranchise(10)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(88), MyVM._myverygood(10)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(89), MyVM._mygood(10)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(90), MyVM._myaverage(10)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(91), MyVM._mybelowavg(10)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(92), MyVM._mydepth(10)))
            SW.Flush()
            SW.WriteLine(String.Format("{0}={1}", _TempArray(93), MyVM._myfranchise(11)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(94), MyVM._myverygood(11)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(95), MyVM._mygood(11)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(96), MyVM._myaverage(11)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(97), MyVM._mybelowavg(11)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(98), MyVM._mydepth(11)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(99), MyVM._myfranchise(12)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(100), MyVM._myverygood(12)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(101), MyVM._mygood(12)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(102), MyVM._myaverage(12)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(103), MyVM._mybelowavg(12)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(104), MyVM._mydepth(12)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(105), MyVM._myfranchise(13)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(106), MyVM._myverygood(13)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(107), MyVM._mygood(13)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(108), MyVM._myaverage(13)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(109), MyVM._mybelowavg(13)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(110), MyVM._mydepth(13)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(111), MyVM._myfranchise(14)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(112), MyVM._myverygood(14)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(113), MyVM._mygood(14)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(114), MyVM._myaverage(14)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(115), MyVM._mybelowavg(14)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(116), MyVM._mydepth(14)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(117), MyVM._myfranchise(15)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(118), MyVM._myverygood(15)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(119), MyVM._mygood(15)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(120), MyVM._myaverage(15)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(121), MyVM._mybelowavg(15)))
            SW.WriteLine(String.Format("{0}={1}", _TempArray(122), MyVM._mydepth(15)))
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
                _SetArray(i) = SR.ReadLine
                i += 1
            End While
            i = 0
            Do While SR.EndOfStream = False 'reads to end of file from this point
                _TempArray(i) = SR.ReadLine
                i += 1
            Loop
        End Using

        i = 0
        Using SW = New StreamWriter(CurDir() + "\GLSsettings.text")
            SW.WriteLine("OVERALL LEAGUE SETTINGS")
            For i = 0 To _SetArray.Count - 1
                SW.WriteLine(_SetArray(i))
            Next i

            SW.WriteLine("LEAGUE FINANCE SETTINGS")
            For i = 0 To _TempArray.Count - 1
                SW.WriteLine(_TempArray(i))
            Next i
        End Using
    End Sub
End Class