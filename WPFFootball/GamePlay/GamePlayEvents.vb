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
    Private Shared Sub KickoffRet(kickReturner As Integer, isFreeKick As Boolean)
        Dim MyRand As New MersenneTwister
        Dim GenKickRetYds As New MersenneTwister
        KickoffDist = GetKickoffDist(isFreeKick)
        YardLine += KickoffDist
        ChangeOfPoss(If(HomePossession, False, True)) 'technically this is a change of possession event as the Kicking team had possession of the ball
        Dim StartReturn = YardLine  'Gets the starting point for the return
        'YardLine = StartReturn
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
        If Fumble(kickReturner, tackler, PlayType.FumKR) Then
            If Stats.Rows.Find(kickReturner).Item("Fumbles") IsNot DBNull.Value Then
                Stats.Rows.Find(kickReturner).Item("Fumbles") += 1
            Else Stats.Rows.Find(kickReturner).Item("Fumbles") = 1
            End If
            FumRec(kickReturner, HomeDT, AwayDT, PlayType.FumKR)
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

        Console.WriteLine($"{Stats.Rows.Find(tackler).Item("Pos")} {Stats.Rows.Find(tackler).Item("FName")} {Stats.Rows.Find(tackler).Item("LName")} tackles{Stats.Rows.Find(kickReturner).Item("Pos")} _
                          {Stats.Rows.Find(kickReturner).Item("FName")} {Stats.Rows.Find(kickReturner).Item("LName")} at the {YardLine} Yard Line after a {KickReturnYards} _
                          yard return from the {StartReturn} yard line. First Down!")

    End Sub
    ''' <summary>
    ''' Returns the amount of time that has gone off the clock
    ''' </summary>
    ''' <param name="yards"></param>
    ''' <param name="play"></param>
    ''' <returns></returns>
    Private Shared Function GetTimeOffClock(yards As Single, play As PlayType) As TimeSpan
        Dim MyRand As New MersenneTwister
        Dim GetTimeOff As TimeSpan
        'Check ClockStopped on most plays to see if clock was running---if it is, then use the pace + ballSpotTime + playTime to get total time off clock
        Select Case play
            Case PlayType.KickoffRet 'Clock is never running for kickoff returns
                GetTimeOff = New TimeSpan(0, 0, (MyRand.GenerateInt32(yards / 10 + 2, yards / 10 + 5)))
                ClockStopped = True
            Case PlayType.RunInside
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(3, 6))
                ClockStopped = False
            Case PlayType.RunOutside
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(4, 8))
                ClockStopped = False
            Case PlayType.PassBehindLOS
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(2, 8))
                ClockStopped = If(IsComplete, False, True)
            Case PlayType.PassShort
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(3, 9))
                ClockStopped = If(IsComplete, False, True)
            Case PlayType.PassMed
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(4, 10))
                ClockStopped = If(IsComplete, False, True)
            Case PlayType.PassLong
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(5, 10))
                ClockStopped = If(IsComplete, False, True)
            Case PlayType.Punt
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(5, 7))
            Case PlayType.PuntReturn
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(1, 10))
                ClockStopped = True
            Case PlayType.FG
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(3, 6))
                ClockStopped = True
        End Select
        If Not ClockStopped Then 'Clock is running---add in ballspot time
            BallSpotTime = New TimeSpan(0, 0, MyRand.GenerateInt32(2, 5))
            '2 Minute Warning Check---BallSpot time is not used if the play goes past the twominute warning
            If GameTime.Subtract(PlayTime) <= New TimeSpan(0, 2, 0) And (Quarter = 2 Or Quarter = 4) And Not TwoMinuteWarning Then
                ClockStopped = True 'Two Minute Warning
                TwoMinuteWarning = True 'Prevents it from being repeated
                GetTimeOff = PlayTime
            Else
                GetTimeOff = PlayTime + BallSpotTime
            End If
        Else GetTimeOff = PlayTime 'Clock is stopped---no ball spot time
        End If
        Return GetTimeOff 'Return how much time has ran off
    End Function
    ''' <summary>
    ''' Determines who(if anyone) recovers the fumble and which team gets possession of the ball
    ''' </summary>
    ''' <param name="fumPlayer"></param>
    ''' <param name="homeDT"></param>
    ''' <param name="awayDT"></param>
    ''' <param name="play"></param>
    Private Shared Sub FumRec(fumPlayer As Integer, homeDT As DataTable, awayDT As DataTable, play As PlayType)
        Dim MyRand As New MersenneTwister
        Dim GetRec As New MersenneTwister
        Dim GetId As Integer
        Dim FumRec As Integer
        Dim FumLost As Boolean
        Dim Tackler As Integer

        Select Case play
            Case PlayType.FumKR
                If MyRand.GenerateDouble(0, 100) > 33 Then 'Fumbling team recovers 2/3 of kickoff fumbles
                    If HomePossession Then 'HomeTeam has the ball--they recover
                        While GetId = 0
                            GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                            If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(awayDT)
                    Else 'AWayTeam has ball
                        While GetId = 0
                            GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                            If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(homeDT)
                    End If
                Else 'Defending team recovers the ball
                    If HomePossession Then 'Away Team recovers
                        While GetId = 0
                            GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                            If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(homeDT)
                        ChangeOfPoss(False) 'Calls the Change of Possession sub
                    Else
                        While GetId = 0 'Home Team Recovers
                            GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                            If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(awayDT)
                        ChangeOfPoss(True) 'Calls the Change of Possession sub
                    End If
                    FumLost = True

                End If
            Case PlayType.FumPR 'Fumble occurs on a Punt Return
                If MyRand.GenerateDouble(0, 100) >= 63.63 Then 'Fumbling Team Recovers about 36.3% of punt return fumbles
                    'Who recovers it?
                    If MyRand.GenerateDouble(0, 100) <= 30 Then 'Fumbling player recovers own fumble
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Else 'Another player on their team recovers the ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    End If
                Else 'Opponent Recovers
                    If HomePossession Then 'Away Team recovers
                        While GetId = 0
                            GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                            If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(homeDT)
                        ChangeOfPoss(False) 'Calls the Change of Possession sub
                    Else
                        While GetId = 0 'Home Team Recovers
                            GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                            If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(awayDT)
                        ChangeOfPoss(True) 'Calls the Change of Possession sub
                    End If
                    FumLost = True 'Loses Fumble
                End If
            Case PlayType.FumQBExchange
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers his own fumble
                    Case <= 43.2
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 45.8 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 75.6 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayType.FumQBHandoff
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 30.5
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 32.8 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 57.5 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayType.FumQBRun
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 31.89
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 42.7 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 57.9 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayType.FumQBSacked
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 13.8
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 16.1 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 49.2 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayType.FumReception
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 9.9
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 27.2 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 40 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayType.FumRunPlay
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 13.3
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 18.9 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 37.9 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayType.MuffKR 'Muffed Kickoff...Receiving team recovers 77.8% of the time, almost always by the fumbling player
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 60.96
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 75.77 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 78.77 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayType.MuffPR
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 46
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 48 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 50 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = GetRec.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = GetRec.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
        End Select

        If FumLost Then
            'Add a lost fumble to the fumbling player
            If Stats.Rows.Find(fumPlayer).Item("FumLost") IsNot DBNull.Value Then
                Stats.Rows.Find(fumPlayer).Item("FumLost") += 1
            Else Stats.Rows.Find(fumPlayer).Item("FumLost") = 1
            End If
        End If
        If Tackler <> 0 Then
            If Stats.Rows.Find(Tackler).Item("TotTackles") IsNot DBNull.Value Then
                Stats.Rows.Find(Tackler).Item("TotTackles") += 1
            Else Stats.Rows.Find(Tackler).Item("TotTackles") = 1
            End If
        End If
        If FumRec <> 0 Then 'If there is a fumble recovery then add it to the players stats
            'Add a fumble recovery to the recovering player
            If Stats.Rows.Find(FumRec).Item("FumRec") IsNot DBNull.Value Then
                Stats.Rows.Find(FumRec).Item("FumRec") += 1
            Else Stats.Rows.Find(FumRec).Item("FumRec") = 1
            End If
        End If
    End Sub
    ''' <summary>
    ''' Change Of Possession things to do...set HomePossession to equal whatever the parameter is
    ''' </summary>
    ''' <param name="homeTeamHasBall"></param>
    Private Shared Sub ChangeOfPoss(homeTeamHasBall As Boolean)
        HomePossession = homeTeamHasBall
        YardLine = 100 - YardLine 'Need to reverse field position for the other team
        ClockStopped = True
        Down = 1
        YardsToGo = 10
    End Sub

    ''' <summary>
    ''' Returns the distance of the kickoff in yards
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function GetKickoffDist(isFreeKick As Boolean) As Integer
        Dim MyRand As New MersenneTwister
        Dim GenKickDist As New MersenneTwister
        Dim KickDist As Integer
        If isFreeKick Then 'Is this a free kick?
            KickDist = MyRand.GetGaussian(50, 3.5)
        Else 'Normal Kickoff
            Select Case MyRand.GenerateInt32(0, 100)
                Case 0 To 80 : KickDist = GenKickDist.GenerateInt32(57, 67)
                Case 81 To 90 : KickDist = GenKickDist.GenerateInt32(68, 72)
                Case 91 To 98 : KickDist = GenKickDist.GenerateInt32(50, 56)
                Case Else : KickDist = GenKickDist.GenerateInt32(73, 74)
            End Select
        End If
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
    ''' <param name="play"></param>
    Private Shared Function Fumble(ballCarrier As Integer, tackler As Integer, play As PlayType) As Boolean
        Dim MyRand As New MersenneTwister
        Dim Fum As Boolean
        Select Case play
            Case PlayType.FumKR
                If MyRand.GenerateDouble(0, 100) <= 1.7 Then
                    If MyRand.GenerateDouble(0, 100) <= 61.36 Then PlayType = PlayType.MuffKR 'This is a muffed kickoff
                    Fum = True
                End If
            Case PlayType.FumQBExchange : If MyRand.GenerateDouble(0, 100) <= 0.25 Then Fum = True
            Case PlayType.FumQBHandoff : If MyRand.GenerateDouble(0, 100) <= 0.25 Then Fum = True
            Case PlayType.FumQBRun : If MyRand.GenerateDouble(0, 100) <= 2.13 Then Fum = True
            Case PlayType.FumQBSacked : If MyRand.GenerateDouble(0, 100) <= 14.82 Then Fum = True
            Case PlayType.FumRunPlay : If MyRand.GenerateDouble(0, 100) <= 1.67 Then Fum = True
            Case PlayType.FumReception : If MyRand.GenerateDouble(0, 100) <= 1.04 Then Fum = True
            Case PlayType.FumPR
                If MyRand.GenerateDouble(0, 100) <= 3.77 Then
                    Fum = True
                    If MyRand.GenerateDouble(0, 100) <= 70.45 Then PlayType = PlayType.MuffPR 'This is a muffed punt
                End If
        End Select
        Return Fum
    End Function

    Private Shared Sub IsTouchback(onKickoff As Boolean, kicker As Integer)
        'If its a touchback from a kickoff, the ball is placed at the 25 yard line, otherwise at the 20
        YardLine = If(onKickoff, 25, 20)
        'Kicker gets a TB added to his Totals
        If Stats.Rows.Find(kicker).Item("Touchback") IsNot DBNull.Value Then 'Check for NULL Values
            Stats.Rows.Find(kicker).Item("Touchback") += 1
        Else Stats.Rows.Find(kicker).Item("Touchback") = 1
        End If
        ChangeOfPoss(If(HomePossession, False, True))
    End Sub

    ''' <summary>
    ''' Handles the kickoff Event
    ''' </summary>
    Public Shared Sub Kickoff(homeTeamKickingOff As Boolean)
        Dim MyRand As New MersenneTwister()
        HomePossession = If(homeTeamKickingOff, False, True) 'This is the same as HomePossesion = homeTeamKickingOff ? True : False in C#
        If MyRand.GenerateInt32(0, 100) < 40 Then '39% of kicks are returned, otherwise touchback
            KickoffRet(If(HomePossession, FindPlayerId(Stats, "WR4", HmTeamId), FindPlayerId(Stats, "WR4", AwTeamId)), False)
        Else IsTouchback(True, If(HomePossession, FindPlayerId(Stats, "K1", HmTeamId), FindPlayerId(Stats, "K1", AwTeamId)))
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
    ''' Determines how much time a team takes off the clock
    ''' </summary>
    Public Shared Sub GetPace()
        Dim MyRand As New MersenneTwister
        Dim NormalPace = New TimeSpan(0, 0, MyRand.GenerateInt32(23, 35))
        Dim HurryUpPace = New TimeSpan(0, 0, MyRand.GenerateInt32(10, 18))
        Dim UseTimePace = New TimeSpan(0, 0, MyRand.GenerateInt32(30, 39))

        Select Case Quarter
            Case 1, 3 : Pace = NormalPace 'Normal Pace
            Case 2
                If Not ClockStopped Then 'Checks to see if the clock is moving
                    Select Case GameTime 'Checks the GameTime
                        Case < New TimeSpan(0, 3, 0)  'Under 3 Minutes to go
                            Select Case YardLine 'Checks the Yardline
                                Case <= 10 : Pace = UseTimePace
                                Case <= 20 : Pace = NormalPace 'At your own 20 or less
                                Case Else : Pace = HurryUpPace
                            End Select
                        Case Else : Pace = NormalPace
                    End Select
                Else : Pace = NormalPace
                End If
            Case 4
                If Not ClockStopped Then
                    Select Case PointDiff
                        Case <= -12
                            Select Case GameTime
                                Case < New TimeSpan(0, 10, 0) : Pace = HurryUpPace
                                Case Else : Pace = NormalPace
                            End Select
                        Case <= -10
                            Select Case GameTime
                                Case < New TimeSpan(0, 8, 0) : Pace = HurryUpPace
                                Case Else : Pace = NormalPace
                            End Select
                        Case <= -8
                            Select Case GameTime
                                Case < New TimeSpan(0, 3, 0) : Pace = HurryUpPace
                                Case Else : Pace = NormalPace
                            End Select
                    End Select
                Else : Pace = NormalPace
                End If
        End Select
        If Not ClockStopped Then GameTime = GameTime.Subtract(Pace) 'Time is taken off the clock while its running
    End Sub
    ''' <summary>
    ''' Determines what type of play to run
    ''' </summary>
    Public Shared Sub RunPlay()
        Dim MyRand As New MersenneTwister
        Dim YardsGained As Single
        Dim passType As New PassTypeEnum
        Dim passComplete As Boolean

        If Down = 4 Then
            If YardLine >= 67 Then 'Field Goal Range --- Attempt a FG
                KickFG(If(HomePossession, HomeDT, AwayDT), ScoringTypeEnum.FG)
            ElseIf YardLine >= 60 And YardsToGo < 4 Then 'Go For it
                If YardsToGo <= 1 Then
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 70.7 'Running Play
                            If Fumble(0, 0, PlayType.FumQBHandoff) Then 'There was a fumble on the Handoff---play is aborted--check to see who recovers the fumble
                                FumRec(0, HomeDT, AwayDT, PlayType.FumQBHandoff)
                            Else
                                YardsGained = GetRunYards(GetRunType)
                                If YardLine >= 100 Then 'Its a TouchDown
                                    ClockStopped = True
                                    UpdateScore(ScoringTypeEnum.RushingTD)
                                    XPConv()
                                Else
                                    If Fumble(0, 0, PlayType.FumRunPlay) Then Fumble(0, 0, PlayType.FumRunPlay) 'Check to see if its a fumble
                                    ClockStopped = False

                                End If
                            End If
                        Case Else 'Passing Play
                            passType = GetPassType()
                            If Fumble(0, 0, PlayType.FumQBExchange) Then
                                FumRec(0, HomeDT, AwayDT, PlayType.FumQBExchange) 'Checks to see if there was a fumble on the exchange/snap
                            Else
                                If Sacked(PlayType) Then
                                    YardsGained = MyRand.GenerateDouble(-15, -0.1)
                                    ClockStopped = False
                                    If Fumble(0, 0, PlayType.FumQBSacked) Then 'Checks to see if there is a fumble on the sack
                                        FumRec(0, HomeDT, AwayDT, PlayType.FumQBSacked)
                                    End If
                                Else
                                    passComplete = GetPassCompletion(passType)
                                    If passComplete Then
                                        YardsGained = GetPassYards(passType)
                                        If Fumble(0, 0, PlayType.FumReception) Then FumRec(0, HomeDT, AwayDT, PlayType.FumReception) 'Checks for fumble on the pass reception
                                        ClockStopped = False
                                    Else 'Incomplete Pass
                                        ClockStopped = True
                                    End If
                                End If 'Checks to see if QB got sacked
                            End If
                    End Select
                ElseIf YardsToGo <= 2 Then
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 25.5 'Running Play
                            If Fumble(0, 0, PlayType.FumQBHandoff) Then 'There was a fumble on the Handoff---play is aborted--check to see who recovers the fumble
                                FumRec(0, HomeDT, AwayDT, PlayType.FumQBHandoff)
                            Else
                                YardsGained = GetRunYards(GetRunType)
                                If YardLine >= 100 Then 'Its a TouchDown
                                    ClockStopped = True
                                    UpdateScore(ScoringTypeEnum.RushingTD)
                                    XPConv()
                                Else
                                    If Fumble(0, 0, PlayType.FumRunPlay) Then Fumble(0, 0, PlayType.FumRunPlay) 'Check to see if its a fumble
                                    ClockStopped = False
                                    GetTimeOffClock(YardsGained, PlayType)
                                End If
                            End If
                        Case Else 'Passing Play
                            passType = GetPassType()
                            If Fumble(0, 0, PlayType.FumQBExchange) Then
                                FumRec(0, HomeDT, AwayDT, PlayType.FumQBExchange) 'Checks to see if there was a fumble on the exchange/snap
                            Else
                                If Sacked(PlayType) Then
                                    YardsGained = MyRand.GenerateDouble(-15, -0.1)
                                    ClockStopped = False
                                    If Fumble(0, 0, PlayType.FumQBSacked) Then 'Checks to see if there is a fumble on the sack
                                        FumRec(0, HomeDT, AwayDT, PlayType.FumQBSacked)
                                    End If
                                Else
                                    passComplete = GetPassCompletion(passType)
                                    If passComplete Then
                                        YardsGained = GetPassYards(passType)
                                        If Fumble(0, 0, PlayType.FumReception) Then FumRec(0, HomeDT, AwayDT, PlayType.FumReception) 'Checks for fumble on the pass reception
                                        ClockStopped = False
                                    Else 'Incomplete Pass
                                        ClockStopped = True
                                    End If
                                End If 'Checks to see if QB got sacked
                            End If
                    End Select
                Else
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 87.8 'Running Play
                            If Fumble(0, 0, PlayType.FumQBHandoff) Then 'There was a fumble on the Handoff---play is aborted--check to see who recovers the fumble
                                FumRec(0, HomeDT, AwayDT, PlayType.FumQBHandoff)
                            Else
                                YardsGained = GetRunYards(GetRunType)
                                If YardLine >= 100 Then 'Its a TouchDown
                                    ClockStopped = True
                                    UpdateScore(ScoringTypeEnum.RushingTD)
                                    XPConv()
                                Else
                                    If Fumble(0, 0, PlayType.FumRunPlay) Then Fumble(0, 0, PlayType.FumRunPlay) 'Check to see if its a fumble
                                    ClockStopped = False
                                End If
                            End If
                        Case Else 'Passing Play
                            passType = GetPassType()
                            If Fumble(0, 0, PlayType.FumQBExchange) Then
                                FumRec(0, HomeDT, AwayDT, PlayType.FumQBExchange) 'Checks to see if there was a fumble on the exchange/snap
                            Else
                                If Sacked(PlayType) Then
                                    YardsGained = MyRand.GenerateDouble(-15, -0.1)
                                    ClockStopped = False
                                    If Fumble(0, 0, PlayType.FumQBSacked) Then 'Checks to see if there is a fumble on the sack
                                        FumRec(0, HomeDT, AwayDT, PlayType.FumQBSacked)
                                    End If
                                Else
                                    passComplete = GetPassCompletion(passType)
                                    If passComplete Then
                                        YardsGained = GetPassYards(passType)
                                        If Fumble(0, 0, PlayType.FumReception) Then FumRec(0, HomeDT, AwayDT, PlayType.FumReception) 'Checks for fumble on the pass reception
                                        ClockStopped = False
                                    Else 'Incomplete Pass
                                        ClockStopped = True
                                    End If
                                End If 'Checks to see if QB got sacked
                            End If
                    End Select
                End If
            Else
                Punt(If(HomePossession, HomeDT, AwayDT))
            End If
        Else
            Select Case MyRand.GenerateInt32(0, 100)
                Case 0 To 44 'Running Play
                    If Fumble(0, 0, PlayType.FumQBHandoff) Then 'There was a fumble on the Handoff---play is aborted--check to see who recovers the fumble
                        FumRec(0, HomeDT, AwayDT, PlayType.FumQBHandoff)
                    Else
                        YardsGained = GetRunYards(GetRunType)
                        If YardLine >= 100 Then 'Its a TouchDown
                            ClockStopped = True
                            UpdateScore(ScoringTypeEnum.RushingTD)
                            XPConv()
                        Else
                            If Fumble(0, 0, PlayType.FumRunPlay) Then Fumble(0, 0, PlayType.FumRunPlay) 'Check to see if its a fumble
                            ClockStopped = False
                        End If
                    End If

                Case Else 'Passing Play
                    passType = GetPassType()
                    If Fumble(0, 0, PlayType.FumQBExchange) Then
                        FumRec(0, HomeDT, AwayDT, PlayType.FumQBExchange) 'Checks to see if there was a fumble on the exchange/snap
                    Else
                        If Sacked(PlayType) Then
                            YardsGained = MyRand.GenerateDouble(-15, -0.1)
                            ClockStopped = False
                            If Fumble(0, 0, PlayType.FumQBSacked) Then 'Checks to see if there is a fumble on the sack
                                FumRec(0, HomeDT, AwayDT, PlayType.FumQBSacked)
                            End If

                        Else
                            passComplete = GetPassCompletion(passType)
                            If passComplete Then
                                YardsGained = GetPassYards(passType)
                                If Fumble(0, 0, PlayType.FumReception) Then FumRec(0, HomeDT, AwayDT, PlayType.FumReception) 'Checks for fumble on the pass reception
                                ClockStopped = False
                            Else 'Incomplete Pass
                                ClockStopped = True
                            End If
                        End If 'Checks to see if QB got sacked
                    End If
            End Select
            YardLine += YardsGained
            If YardLine < 0 Then 'Safety
                Safety()
            Else
                YardsToGo -= YardsGained 'Sets how many yards to go
                Down = If(YardsToGo <= 0, 1, Down + 1) 'checks to see if its a first down
            End If
            GetTimeOffClock(YardsGained, PlayType)
        End If
    End Sub

    Private Shared Sub Safety()
        UpdateScore(ScoringTypeEnum.Safety)
        ClockStopped = True
        FreeKickPunt(HomePossession)
    End Sub
    ''' <summary>
    ''' Kick Off from the 20 yard line after a safety(Punt)
    ''' </summary>
    Private Shared Sub FreeKickPunt(homeTeamKickingOff As Boolean)
        Dim MyRand As New MersenneTwister()
        YardLine = 20
        HomePossession = If(homeTeamKickingOff, False, True) 'This is the same as HomePossesion = homeTeamKickingOff ? True : False in C#
        KickoffRet(If(HomePossession, FindPlayerId(Stats, "WR4", HmTeamId), FindPlayerId(Stats, "WR4", AwTeamId)), False)
    End Sub
    ''' <summary>
    ''' Checks to see if QB gets sacked by down
    ''' </summary>
    ''' <param name="play"></param>
    ''' <returns></returns>
    Private Shared Function Sacked(play As PlayType) As Boolean
        Dim IsSacked As Boolean
        Dim MyRand As New MersenneTwister
        Select Case Down
            Case 1 : If MyRand.GenerateDouble(0, 100) < 4.55 Then IsSacked = True
            Case 2 : If MyRand.GenerateDouble(0, 100) < 4.9 Then IsSacked = True
            Case 3 : If MyRand.GenerateDouble(0, 100) < 8.5 Then IsSacked = True
            Case 4 : If MyRand.GenerateDouble(0, 100) < 4.25 Then IsSacked = True
        End Select
        Return IsSacked
    End Function

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
    ''' <summary>
    ''' Punt
    ''' </summary>
    ''' <param name="DT"></param>
    Private Shared Sub Punt(DT As DataTable)
        Dim MyRand As New MersenneTwister
        Select Case YardLine
            Case < 45
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 0.35 'Touchback
                        Touchback = True
                        PuntDistance = 100 - YardLine
                        IsTouchback(False, If(HomePossession, FindPlayerId(Stats, "P1", HmTeamId), FindPlayerId(Stats, "P1", AwTeamId)))
                    Case Else 'No Touchback
                        PuntDistance = MyRand.GetGaussian(45.3, 3.5)
                        CallFairCatch = MyRand.GenerateDouble(0, 100) < 5.45 'Did returner call a fair catch?
                        OutOfBounds = MyRand.GenerateDouble(0, 100) < 2.99 'Did punt go out of bounds?
                End Select
            Case 45 To 55
                Select Case MyRand.GenerateDouble(0, 100)
                    Case < 15.71 'Touchback
                        Touchback = True
                        PuntDistance = 100 - YardLine
                        IsTouchback(False, If(HomePossession, FindPlayerId(Stats, "P1", HmTeamId), FindPlayerId(Stats, "P1", AwTeamId)))
                    Case Else 'No Touchback
                        PuntDistance = MyRand.GenerateInt32(27, 99 - YardLine) 'Has to at least be at the 1 yard line
                        YardLine = YardLine + PuntDistance
                        ChangeOfPoss(HomePossession)
                        CallFairCatch = MyRand.GenerateDouble(0, 100) < 47.14 'Did the returner call a fair catch?
                        OutOfBounds = MyRand.GenerateDouble(0, 100) < 7.74 'Did the punt go out of bounds?
                End Select
            Case Else 'Punting from inside the opponent's 45
                Select Case MyRand.GenerateDouble(0, 100)
                    Case < 21.93 'Touchback
                        Touchback = True
                        PuntDistance = 100 - YardLine
                        IsTouchback(False, If(HomePossession, FindPlayerId(Stats, "P1", HmTeamId), FindPlayerId(Stats, "P1", AwTeamId)))
                    Case Else 'No Touchback
                        PuntDistance = MyRand.GenerateInt32(27, 99 - YardLine) 'Has to at least be at the 1 yard line
                        YardLine = YardLine + PuntDistance
                        ChangeOfPoss(HomePossession)
                        CallFairCatch = MyRand.GenerateDouble(0, 100) < 36.9 'Did the returner call a fair catch?
                        OutOfBounds = MyRand.GenerateDouble(0, 100) < 6.42 'Did the punt go out of bounds?
                End Select
        End Select
        GetTimeOffClock(PuntDistance, PlayType.Punt) 'Get the time that ran off the clock for this play
        If Not CallFairCatch And Not OutOfBounds Then PuntReturn() 'If there is no fair catch and it doesn't go out of bounds then get the return yards
    End Sub

    Private Shared Sub PuntReturn()
        Dim MyRand As New MersenneTwister
        Select Case MyRand.GenerateInt32(0, 100)
            Case 0 To 50 : PuntReturnYards = MyRand.GenerateInt32(0, 5)
            Case 51 To 75 : PuntReturnYards = MyRand.GenerateInt32(6, 11)
            Case 76 To 90 : PuntReturnYards = MyRand.GenerateInt32(12, 19)
            Case 91 To 95 : PuntReturnYards = MyRand.GenerateInt32(20, 40)
            Case 96 To 98 : PuntReturnYards = MyRand.GenerateInt32(41, 70)
            Case Else : PuntReturnYards = MyRand.GenerateInt32(71, 100)
        End Select
        YardLine += PuntReturnYards
        Fumble(0, 0, PlayType.PuntReturn) 'Did the BallCarrier fumble?
        GetTimeOffClock(PuntReturnYards, PlayType.PuntReturn)
        ChangeOfPoss(If(HomePossession, False, True))
    End Sub

    Private Shared Sub KickFG(DT As DataTable, play As ScoringTypeEnum)
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
                    YardLine -= 7
                    ChangeOfPoss(If(HomePossession, False, True))
                End If
        End Select
        GetTimeOffClock(0, PlayType.FG)
    End Sub
End Class