Imports Mersenne
Imports GoalLineStandFootball.GamePlayStats
Imports System.Data
Imports GoalLineStandFootball

Public Class GamePlayEvents
    Inherits GamePlay

#Region "Events"
    'Public Shared Event PassCompletion(ByVal QB As Integer, ByVal recPlayer As Integer, ByVal yardsGained As Single)
    'Public Shared Event PassIncompletion(ByVal QB As Integer, ByVal intReceiver As Integer)
    'Public Shared Event PassDropped(ByVal QB As Integer, ByVal recPlayer As Integer)
    'Public Shared Event PassDefended(ByVal defPlayer As Integer, ByVal intendedRec As Integer)

    'Public Shared Event FirstDown(ByVal player As Integer, ByVal homeTeam As Boolean)
    'Public Shared Event TouchDown(ByVal type As ScoringTypeEnum, ByVal homeTeam As Boolean, ByVal player As Integer)
    'Public Shared Event Interception(ByVal QBThrowing As Integer, ByVal IntPlayer As Integer, ByVal homeTeam As Boolean)
    'Public Shared Event Fumble(ByVal fumblingPlayer As Integer, ByVal recoveringPlayer As Integer)
    'Public Shared Event Tackle(ByVal ballCarrier As Integer, ByVal tackler As Integer)
    'Public Shared Event Sack(ByVal QB As Integer, ByVal defender As Integer, ByVal yardsLost As Integer)
    'Public Shared Event Punt(ByVal punter As Integer, ByVal puntYards As Integer)
    'Public Shared Event FieldGoal()
    'Public Shared Event ChangeOfPoss(ByVal homeTeamHasBall As Boolean) 'Fires Change of Possession Event
    'Public Shared Event Timeout(ByVal homeTeamCalled As Boolean)
    'Public Shared Event EndOfQuarter()
    'Public Shared Event TwoMinuteWarning()
    'Public Shared Event HalfTime()
    'Public Shared Event Kickoff(ByVal homeTeamKickingOff As Boolean)
    'Public Shared Event KickoffRet(ByVal kickReturner As Integer)
    'Public Shared Event Touchback(onKickoff As Boolean)

#End Region

    Private Shared Sub KickoffRet(kickReturner As Integer)
        Dim MyRand As New MersenneTwister
        Dim GenKickRetYds As New MersenneTwister
        Dim StartReturn = 100 - (YardLine + GetKickoffDist())  'Gets the starting point for the return
        YardLine = StartReturn
        Select Case MyRand.GenerateInt32(0, 100)
            Case 0 To 80
                KickReturnYards = GenKickRetYds.GenerateInt32(10, 30)
            Case 71 To 90
                KickReturnYards = GenKickRetYds.GenerateInt32(31, 40)
            Case 91 To 97
                KickReturnYards = GenKickRetYds.GenerateInt32(41, 60)
            Case Else
                KickReturnYards = GenKickRetYds.GenerateInt32(61, 100)
        End Select
        Dim tackler = Tackle(If(HomePossession, AwayDT, HomeDT)) 'Gets the tackling player
        YardLine += KickReturnYards 'Gets the starting field position

        'Checks play for Fumble
        If Fumble(kickReturner, tackler, PlayType.KickoffRet) Then
            If Stats.Rows.Find(kickReturner).Item("Fumbles") IsNot DBNull.Value Then
                Stats.Rows.Find(kickReturner).Item("Fumbles") += 1
            Else Stats.Rows.Find(kickReturner).Item("Fumbles") = 1
            End If
            FumRec(kickReturner, HomeDT, AwayDT, PlayType.KickoffRet)
        End If

        'Add a kick Return to the returner's total
        If Stats.Rows.Find(kickReturner).Item("KORet") IsNot DBNull.Value Then
            Stats.Rows.Find(kickReturner).Item("KORet") += 1
        Else Stats.Rows.Find(kickReturner).Item("KORet") = 1
        End If

        'Add Kick Return yards to the returners total
        If Stats.Rows.Find(kickReturner).Item("KORetYds") IsNot DBNull.Value Then
            Stats.Rows.Find(kickReturner).Item("KORetYds") += KickReturnYards
        Else Stats.Rows.Find(kickReturner).Item("KORetYds") = KickReturnYards
        End If

        'Check to see if the long return needs to be updated
        If Stats.Rows.Find(kickReturner).Item("KORetLong") IsNot DBNull.Value Then
            If Stats.Rows.Find(kickReturner).Item("KORetYds") < KickReturnYards Then
                Stats.Rows.Find(kickReturner).Item("KORetYds") = KickReturnYards 'If this is longer, then update it.
            End If
        Else 'Returner has no returns, this is automatically their long
            Stats.Rows.Find(kickReturner).Item("KORetYds") = KickReturnYards
        End If

        GameTime = GameTime.Subtract(GetTimeOffClock(KickReturnYards, PlayType.KickoffRet))

        ChangeOfPoss(HomePossession) 'technically this is a change of possession event as the Kicking team had possession of the ball

        Console.WriteLine($"{Stats.Rows.Find(tackler).Item("Pos")} {Stats.Rows.Find(tackler).Item("FName")} {Stats.Rows.Find(tackler).Item("LName")} tackles{Stats.Rows.Find(kickReturner).Item("Pos")} _
                          {Stats.Rows.Find(kickReturner).Item("FName")} {Stats.Rows.Find(kickReturner).Item("LName")} at the {YardLine} Yard Line after a {KickReturnYards} _
                          yard return from the {StartReturn} yard line. First Down!")

    End Sub

    Private Shared Function GetTimeOffClock(kickReturnYards As Single, play As PlayType) As TimeSpan
        Dim MyRand As New MersenneTwister
        Dim GetTimeOff As TimeSpan
        'Check ClockStopped on most plays to see if clock was running---if it is, then use the pace + ballSpotTime + playTime to get total time off clock
        Select Case play
            Case PlayType.KickoffRet 'Clock is never running for kickoff returns
                GetTimeOff = TimeSpan.FromSeconds(MyRand.GenerateInt32(kickReturnYards / 10 + 2, kickReturnYards / 10 + 5))
        End Select
        Return GetTimeOff
    End Function

    Private Shared Sub FumRec(fumPlayer As Integer, homeDT As DataTable, awayDT As DataTable, play As PlayType)
        Dim MyRand As New MersenneTwister
        Dim GetRec As New MersenneTwister
        Dim GetId As Integer
        Dim FumRec As Integer
        Select Case play
            Case PlayType.KickoffRet
                If MyRand.GenerateDouble(0, 100) > 33 Then 'Fumbling team recovers 2/3 of kickoff fumbles
                    If HomePossession Then 'HomeTeam has the ball--they recover
                        While GetId = 0
                            GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                            If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End If
                        End While
                    Else 'AWayTeam has ball
                        While GetId = 0
                            GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                            If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End If
                        End While
                    End If
                Else 'Defending team recovers the ball
                    If HomePossession Then 'Away Team recovers
                        While GetId = 0
                            GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                            If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End If
                        End While
                        ChangeOfPoss(False) 'Calls the Change of Possession sub
                    Else
                        While GetId = 0 'Home Team Recovers
                            GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                            If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End If
                        End While
                        ChangeOfPoss(True) 'Calls the Change of Possession sub
                    End If
                    'Add a lost fumble to the fumbling player
                    If Stats.Rows.Find(fumPlayer).Item("FumLost") IsNot DBNull.Value Then
                        Stats.Rows.Find(fumPlayer).Item("FumLost") += 1
                    Else Stats.Rows.Find(fumPlayer).Item("FumLost") = 1
                    End If
                End If
        End Select
        'Add a fumble recovery to the recovering player
        If Stats.Rows.Find(FumRec).Item("FumRec") IsNot DBNull.Value Then
            Stats.Rows.Find(fumPlayer).Item("FumRec") += 1
        Else Stats.Rows.Find(fumPlayer).Item("FumRec") = 1
        End If
    End Sub
    ''' <summary>
    ''' Change Of Possession things to do...set HomePossession to equal whatever the parameter is
    ''' </summary>
    ''' <param name="homeTeamHasBall"></param>
    Private Shared Sub ChangeOfPoss(homeTeamHasBall As Boolean)
        HomePossession = homeTeamHasBall
        ClockStopped = True
        Down = 1
        YardsToGo = 10
    End Sub

    ''' <summary>
    ''' Returns the distance of the kickoff in yards
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function GetKickoffDist() As Integer
        Dim MyRand As New MersenneTwister
        Dim GenKickDist As New MersenneTwister
        Dim KickDist As Integer
        Select Case MyRand.GenerateInt32(0, 100)
            Case 0 To 80 : KickDist = GenKickDist.GenerateInt32(57, 67)
            Case 81 To 90 : KickDist = GenKickDist.GenerateInt32(68, 72)
            Case 91 To 98 : KickDist = GenKickDist.GenerateInt32(50, 56)
            Case Else : KickDist = GenKickDist.GenerateInt32(73, 74)
        End Select
        Return KickDist
    End Function

    ''' <summary>
    ''' REturns the ID of the tackling player
    ''' </summary>
    ''' <param name="defTeamDT"></param>
    ''' <returns></returns>
    Private Shared Function Tackle(defTeamDT As DataTable) As Integer
        Dim tackler As Integer
        Dim MyRand As New MersenneTwister
        Dim getId As Integer
        While tackler = 0
            getId = MyRand.GenerateInt32(0, defTeamDT.Rows.Count() - 1)
            If (defTeamDT.Rows(getId).Item("STCoverage")) > 50 Then
                tackler = defTeamDT.Rows(getId).Item("PlayerId")
            End If
        End While
        'Add a tackle to this players total
        If Stats.Rows.Find(tackler).Item("TotTackles") IsNot DBNull.Value Then 'Check for NULL Values
            Stats.Rows.Find(tackler).Item("TotTackles") += 1
        Else Stats.Rows.Find(tackler).Item("TotTackles") = 1
        End If
        Return tackler
    End Function
    ''' <summary>
    ''' Determines if there is a fumble on the play or not
    ''' </summary>
    ''' <param name="ballCarrier"></param>
    ''' <param name="tackler"></param>
    ''' <param name="playType"></param>
    Private Shared Function Fumble(ballCarrier As Integer, tackler As Integer, playType As PlayType) As Boolean
        Dim MyRand As New MersenneTwister
        Dim Fum As Boolean
        Select Case playType
            Case PlayType.KickoffRet
                If MyRand.GenerateDouble(0, 100) <= 1.7 Then
                    Fum = True
                End If
        End Select
        Return Fum
    End Function

    Private Shared Sub Touchback(onKickoff As Boolean, kicker As Integer)
        'If its a touchback from a kickoff, the ball is placed at the 25 yard line, otherwise at the 20
        YardLine = If(onKickoff, 25, 20)
        'Kicker gets a TB added to his Totals
        If Stats.Rows.Find(kicker).Item("Touchback") IsNot DBNull.Value Then 'Check for NULL Values
            Stats.Rows.Find(kicker).Item("Touchback") += 1
        Else Stats.Rows.Find(kicker).Item("Touchback") = 1
        End If
        Down = 1
    End Sub

    ''' <summary>
    ''' Handles the kickoff Event
    ''' </summary>
    Public Shared Sub Kickoff(homeTeamKickingOff As Boolean)
        Dim MyRand As New MersenneTwister()
        HomePossession = If(homeTeamKickingOff, False, True) 'This is the same as HomePossesion = homeTeamKickingOff ? True : False in C#
        If MyRand.GenerateInt32(0, 100) < 40 Then '39% of kicks are returned, otherwise touchback
            'Dim HomeKOR =  'Home Kickoff Returner
            'Dim AwayKOR = ) 'AWay Kickoff Returner)
            'Dim KR =
            KickoffRet(If(HomePossession, FindPlayerId(Stats, "WR4", HmTeamId), FindPlayerId(Stats, "WR4", AwTeamId)))
        Else Touchback(True, If(HomePossession, FindPlayerId(Stats, "K1", HmTeamId), FindPlayerId(Stats, "K1", AwTeamId)))
        End If
    End Sub
    ''' <summary>
    ''' This function returns the player Id via the Data Table and the Depth Chart Position
    ''' </summary>
    ''' <param name="DT"></param>
    ''' <param name="DepthPos"></param>
    ''' <returns></returns>
    Private Shared Function FindPlayerId(DT As DataTable, DepthPos As String, teamID As Integer) As Integer
        Dim PID = From p In DT
                  Where p.Item("DepthChart") = DepthPos And p.Item("TeamId") = teamID
                  Select p.Item("PlayerId")
        Return PID.ElementAt(0)
    End Function
    ''' <summary>
    ''' Determines what type of play to run
    ''' </summary>
    Public Shared Sub RunPlay()
        Dim MyRand As New MersenneTwister
        Dim PlayTime As New TimeSpan
        Dim YardsGained As Single
        Dim passType As New PassTypeEnum
        Dim passComplete As Boolean
        If ClockStopped Then
            Pace = New TimeSpan(0, 0, 0)
        Else
            Pace = New TimeSpan(0, 0, MyRand.GenerateInt32(23, 38))
        End If

        If Down = 4 Then
            If YardLine >= 67 Then 'Field Goal Range --- Attempt a FG
                KickFG(If(HomePossession, HomeDT, AwayDT), ScoringTypeEnum.FG)
            Else
                Punt(If(HomePossession, HomeDT, AwayDT))
            End If
        Else
            Select Case MyRand.GenerateInt32(0, 100)
                Case 0 To 44 'Running Play
                    YardsGained = GetRunYards(GetRunType)
                    If YardLine >= 100 Then 'Its a TouchDown
                        ClockStopped = True
                        UpdateScore(ScoringTypeEnum.RushingTD)
                        XPConv()
                    Else
                        ClockStopped = False
                        BallSpotTime = New TimeSpan(0, 0, MyRand.GenerateInt32(2, 5))
                        PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(3, 8))
                        GameTime -= Pace - PlayTime - BallSpotTime
                    End If
                Case Else 'Passing Play
                    passType = GetPassType()
                    passComplete = GetPassCompletion(passType)
                    YardsGained = If(passComplete, GetPassYards(passType), 0)
            End Select

            YardLine += YardsGained
            YardsToGo -= YardsGained 'Sets how many yards to go
            Down = If(YardsToGo <= 0, 1, Down + 1) 'checks to see if its a first down
        End If
    End Sub

    Private Shared Sub XPConv()
        Dim MyRand As New MersenneTwister
        Dim PointDiff = If(HomePossession, HomeScore - AWayScore, AWayScore - HomeScore)
        Dim PassType As New PassTypeEnum
        Dim PassComplete As Boolean
        If Quarter = 4 Then
            Select Case Math.Abs(PointDiff)
                'Attempt 2 Point Conv
                Case 2, 5, 8, 11, 16, > 18
                    YardLine = 98
                    Select Case MyRand.GenerateInt32(0, 100)
                        Case < 44
                            If GetRunYards(GetRunType) >= 2 Then UpdateScore(ScoringTypeEnum.TwoPointConv) '2Pt Conversion succeeds
                        Case Else
                            PassType = GetPassType()
                            PassComplete = GetPassCompletion(PassType)
                            If PassComplete Then
                                If GetPassYards(PassType) > 2 Then UpdateScore(ScoringTypeEnum.TwoPointConv) '2Pt Conversion Succeeds
                            End If
                    End Select
                Case Else 'XP Attempt
                    YardLine = 85
                    KickFG(If(HomePossession, HomeDT, AwayDT), ScoringTypeEnum.XP)
            End Select
        End If
    End Sub
    ''' <summary>
    ''' Updates the score based on the type of scoring play
    ''' </summary>
    ''' <param name="play"></param>
    Private Shared Sub UpdateScore(play As ScoringTypeEnum)
        Select Case play
            Case ScoringTypeEnum.OffFumRecTD, ScoringTypeEnum.PassingTD, ScoringTypeEnum.RushingTD, ScoringTypeEnum.PuntRetTD, ScoringTypeEnum.KORetTD
                HomeScore = If(HomePossession, HomeScore + 6, HomeScore)
                AWayScore = If(HomePossession, AWayScore, AWayScore + 6)
            Case ScoringTypeEnum.FG
                HomeScore = If(HomePossession, HomeScore + 3, HomeScore)
                AWayScore = If(HomePossession, AWayScore, AWayScore + 3)
            Case ScoringTypeEnum.TwoPointConv
                HomeScore = If(HomePossession, HomeScore + 2, HomeScore)
                AWayScore = If(HomePossession, AWayScore, AWayScore + 2)
            Case ScoringTypeEnum.XP
                HomeScore = If(HomePossession, HomeScore + 1, HomeScore)
                AWayScore = If(HomePossession, AWayScore, AWayScore + 1)
            Case ScoringTypeEnum.Safety, ScoringTypeEnum.DefXPReturnFor2Pts
                HomeScore = If(HomePossession, HomeScore, HomeScore + 2)
                AWayScore = If(HomePossession, AWayScore + 2, AWayScore)
            Case ScoringTypeEnum.DefFumRecTD, ScoringTypeEnum.IntReturnTD
                HomeScore = If(HomePossession, HomeScore, HomeScore + 6)
                AWayScore = If(HomePossession, AWayScore + 6, AWayScore)
        End Select

    End Sub

    Private Shared Sub Punt(DT As DataTable)

    End Sub

    Private Shared Function KickFG(DT As DataTable, play As ScoringTypeEnum) As Boolean
        Dim MakeFG As New MersenneTwister
        Dim FGMade As Boolean
        Select Case play
            Case ScoringTypeEnum.XP 'XP
                If MakeFG.GenerateDouble(0, 100) <= 93.99 Then UpdateScore(ScoringTypeEnum.XP) 'XP Made---93.99%last year
            Case Else 'FG
                Select Case YardLine
                    Case >= 98 : If MakeFG.GenerateDouble(0, 100) <= 99.3 Then UpdateScore(ScoringTypeEnum.FG) FGMade = True
                    Case >= 88 : If MakeFG.GenerateDouble(0, 100) <= 96.15 Then UpdateScore(ScoringTypeEnum.FG) FGMade = True
                    Case >= 78 : If MakeFG.GenerateDouble(0, 100) <= 92.15 Then UpdateScore(ScoringTypeEnum.FG) FGMade = True
                    Case >= 68 : If MakeFG.GenerateDouble(0, 100) <= 77.1 Then UpdateScore(ScoringTypeEnum.FG) FGMade = True
                    Case Else : If MakeFG.GenerateDouble(0, 100) <= 53.33 Then UpdateScore(ScoringTypeEnum.FG) FGMade = True
                End Select
                If FGMade Then
                    ClockStopped = True
                    YardLine = 35
                    Kickoff(If(HomePossession, True, False))
                Else 'FG Missed
                    ClockStopped = True
                    ChangeOfPoss(If(HomePossession, False, True))
                    YardLine -= 7
                End If
        End Select
    End Function

    ''' <summary>
    ''' Determines how much time to run off the clock if any
    ''' </summary>
    Public Shared Sub RunClock()

    End Sub
End Class