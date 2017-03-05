Imports GoalLineStandFootball.GamePlayStats
Imports System.Data
Imports GoalLineStandFootball

Public Class GamePlayEvents
    Inherits GamePlay
    Private Shared Sub KickoffRet(kickReturner As Integer, isFreeKick As Boolean)
        KickoffDist = GetKickoffDist(isFreeKick)
        YardLine += KickoffDist
        ChangeOfPoss(If(HomePossession, False, True)) 'technically this is a change of possession event as the Kicking team had possession of the ball
        Dim StartReturn = YardLine  'Gets the starting point for the return
        'YardLine = StartReturn
        Select Case MyRand.GenerateInt32(0, 100)
            Case 0 To 80
                KickReturnYards = MyRand.GenerateInt32(10, 30)
            Case 71 To 90
                KickReturnYards = MyRand.GenerateInt32(31, 40)
            Case 91 To 97
                KickReturnYards = MyRand.GenerateInt32(41, 60)
            Case Else
                KickReturnYards = MyRand.GenerateInt32(61, 100)
        End Select
        Dim tackler = Tackle(If(HomePossession, AwayDT, HomeDT)) 'Gets the tackling player
        YardLine += KickReturnYards 'Gets the starting field position

        'Checks play for Fumble
        If Fumble(kickReturner, tackler, PlayTypeEnum.FumKR) Then
            If Stats.Rows.Find(kickReturner).Item("Fumbles") IsNot DBNull.Value Then
                Stats.Rows.Find(kickReturner).Item("Fumbles") += 1
            Else Stats.Rows.Find(kickReturner).Item("Fumbles") = 1
            End If
            FumRec(kickReturner, HomeDT, AwayDT, PlayTypeEnum.FumKR)
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

        GameTime = GameTime.Subtract(GetTimeOffClock(KickReturnYards, PlayTypeEnum.KickoffRet))

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
    Private Shared Function GetTimeOffClock(yards As Single, play As PlayTypeEnum) As TimeSpan
        Dim GetTimeOff As TimeSpan
        'Check ClockStopped on most plays to see if clock was running---if it is, then use the pace + ballSpotTime + playTime to get total time off clock
        Select Case play
            Case PlayTypeEnum.KickoffRet 'Clock is never running for kickoff returns
                GetTimeOff = New TimeSpan(0, 0, (MyRand.GenerateInt32(yards / 10 + 2, yards / 10 + 5)))
                ClockStopped = True
            Case PlayTypeEnum.RunInside
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(3, 6))
                ClockStopped = False
            Case PlayTypeEnum.RunOutside
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(4, 8))
                ClockStopped = False
            Case PlayTypeEnum.PassBehindLOS
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(2, 8))
                ClockStopped = If(IsComplete, False, True)
            Case PlayTypeEnum.PassShort
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(3, 9))
                ClockStopped = If(IsComplete, False, True)
            Case PlayTypeEnum.PassMed
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(4, 10))
                ClockStopped = If(IsComplete, False, True)
            Case PlayTypeEnum.PassLong
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(5, 10))
                ClockStopped = If(IsComplete, False, True)
            Case PlayTypeEnum.Punt
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(5, 7))
            Case PlayTypeEnum.PuntReturn
                PlayTime = New TimeSpan(0, 0, MyRand.GenerateInt32(1, 10))
                ClockStopped = True
            Case PlayTypeEnum.FGMade, PlayTypeEnum.FGMissed
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
    Private Shared Sub FumRec(fumPlayer As Integer, homeDT As DataTable, awayDT As DataTable, play As PlayTypeEnum)
        Dim GetId As Integer
        Dim FumRec As Integer
        Dim FumLost As Boolean
        Dim Tackler As Integer

        Select Case play
            Case PlayTypeEnum.FumKR
                If MyRand.GenerateDouble(0, 100) > 33 Then 'Fumbling team recovers 2/3 of kickoff fumbles
                    If HomePossession Then 'HomeTeam has the ball--they recover
                        While GetId = 0
                            GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                            If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(awayDT)
                    Else 'AWayTeam has ball
                        While GetId = 0
                            GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                            If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(homeDT)
                    End If

                Else 'Defending team recovers the ball
                    If HomePossession Then 'Away Team recovers
                        While GetId = 0
                            GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                            If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(homeDT)
                        ChangeOfPoss(False) 'Calls the Change of Possession sub
                    Else
                        While GetId = 0 'Home Team Recovers
                            GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                            If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(awayDT)
                        ChangeOfPoss(True) 'Calls the Change of Possession sub
                    End If
                    FumLost = True

                End If
            Case PlayTypeEnum.FumPR 'Fumble occurs on a Punt Return
                If MyRand.GenerateDouble(0, 100) >= 63.63 Then 'Fumbling Team Recovers about 36.3% of punt return fumbles
                    'Who recovers it?
                    If MyRand.GenerateDouble(0, 100) <= 30 Then 'Fumbling player recovers own fumble
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Else 'Another player on their team recovers the ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If
                    End If

                Else 'Opponent Recovers
                    If HomePossession Then 'Away Team recovers
                        While GetId = 0
                            GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                            If (awayDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(homeDT)
                        ChangeOfPoss(False) 'Calls the Change of Possession sub
                    Else
                        While GetId = 0 'Home Team Recovers
                            GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                            If (homeDT.Rows(GetId).Item("STCoverage")) > 50 Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                        End While
                        Tackler = Tackle(awayDT)
                        ChangeOfPoss(True) 'Calls the Change of Possession sub
                    End If
                    FumLost = True 'Loses Fumble

                End If
            Case PlayTypeEnum.FumQBExchange
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers his own fumble
                    Case <= 43.2
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 45.8 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 75.6 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If

                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayTypeEnum.FumQBHandoff
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 30.5
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 32.8 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 57.5 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If

                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayTypeEnum.FumQBRun
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 31.89
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 42.7 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 57.9 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If

                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayTypeEnum.FumQBSacked
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 13.8
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 16.1 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 49.2 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "WR" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If

                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                If (awayDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                If (homeDT.Rows(GetId).Item("Pos")) <> "CB" Then FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayTypeEnum.FumReception, PlayTypeEnum.PassBehindLOS, PlayTypeEnum.PassShort, PlayTypeEnum.PassMed, PlayTypeEnum.PassLong
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 9.9
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 27.2 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 40 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If

                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayTypeEnum.FumRunPlay, PlayTypeEnum.RunOutside, PlayTypeEnum.RunInside
                Select Case MyRand.GenerateDouble(0, 100)  'QB Recovers the fumble
                    Case <= 13.3
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 18.9 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 37.9 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If

                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayTypeEnum.MuffKR 'Muffed Kickoff...Receiving team recovers 77.8% of the time, almost always by the fumbling player
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 60.96
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 75.77 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 78.77 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If

                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                            ChangeOfPoss(True) 'Calls the Change of Possession sub
                        End If
                        FumLost = True 'Loses Fumble
                End Select
            Case PlayTypeEnum.MuffPR
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 46
                        FumRec = fumPlayer
                        Tackler = Tackle(If(HomePossession, awayDT, homeDT))
                    Case <= 48 : ClockStopped = True 'Ball fumbled out of bounds
                    Case <= 50 'Offense recovers ball
                        If HomePossession Then 'HomeTeam has the ball--they recover
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
                                FumRec = homeDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(awayDT)
                        Else 'AWayTeam has ball
                            While GetId = 0 And GetId <> fumPlayer
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                        End If

                    Case Else 'Defense recovers
                        If HomePossession Then 'Away Team recovers
                            While GetId = 0
                                GetId = MyRand.GenerateInt32(0, awayDT.Rows.Count - 1)
                                FumRec = awayDT.Rows(GetId).Item("PlayerId")
                            End While
                            Tackler = Tackle(homeDT)
                            ChangeOfPoss(False) 'Calls the Change of Possession sub
                        Else
                            While GetId = 0 'Home Team Recovers
                                GetId = MyRand.GenerateInt32(0, homeDT.Rows.Count - 1)
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
                Console.WriteLine($"***DEFENSE RECOVERS! TURNOVER!")
            Else
                Stats.Rows.Find(fumPlayer).Item("FumLost") = 1
                Console.WriteLine($"***OFFENSE MAINTAINS POSSESSION!")
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
        Dim KickDist As Integer
        If isFreeKick Then 'Is this a free kick?
            KickDist = MyRand.GetGaussian(50, 3.5)
        Else 'Normal Kickoff
            If Touchback Then
                KickDist = MyRand.GenerateInt32(66, 78) 'Has to be in the endzone
            Else
                Select Case MyRand.GenerateInt32(0, 100)
                    Case 0 To 80 : KickDist = MyRand.GenerateInt32(57, 67)
                    Case 81 To 90 : KickDist = MyRand.GenerateInt32(68, 72)
                    Case 91 To 98 : KickDist = MyRand.GenerateInt32(50, 56)
                    Case Else : KickDist = MyRand.GenerateInt32(73, 78)
                End Select
            End If
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
    Private Shared Function Fumble(ballCarrier As Integer, tackler As Integer, play As PlayTypeEnum) As Boolean
        Dim Fum As Boolean
        Select Case play
            Case PlayTypeEnum.FumKR
                If MyRand.GenerateDouble(0, 100) <= 1.7 Then
                    If MyRand.GenerateDouble(0, 100) <= 61.36 Then PlayType = PlayTypeEnum.MuffKR 'This is a muffed kickoff
                    Fum = True
                End If
            Case PlayTypeEnum.FumQBExchange : If MyRand.GenerateDouble(0, 100) <= 0.25 Then Fum = True
            Case PlayTypeEnum.FumQBHandoff : If MyRand.GenerateDouble(0, 100) <= 0.25 Then Fum = True
            Case PlayTypeEnum.FumQBRun : If MyRand.GenerateDouble(0, 100) <= 2.13 Then Fum = True
            Case PlayTypeEnum.FumQBSacked : If MyRand.GenerateDouble(0, 100) <= 14.82 Then Fum = True
            Case PlayTypeEnum.FumRunPlay, PlayTypeEnum.RunInside, PlayTypeEnum.RunOutside : If MyRand.GenerateDouble(0, 100) <= 1.67 Then Fum = True
            Case PlayTypeEnum.FumReception, PlayTypeEnum.PassBehindLOS, PlayTypeEnum.PassShort, PlayTypeEnum.PassMed, PlayTypeEnum.PassLong : If MyRand.GenerateDouble(0, 100) <= 1.04 Then Fum = True
            Case PlayTypeEnum.FumPR
                If MyRand.GenerateDouble(0, 100) <= 3.77 Then
                    Fum = True
                    If MyRand.GenerateDouble(0, 100) <= 70.45 Then PlayType = PlayTypeEnum.MuffPR 'This is a muffed punt
                End If
        End Select
        Return Fum
    End Function

    Private Shared Sub IsTouchback(onKickoff As Boolean, kicker As Integer)
        'If its a touchback from a kickoff, the ball is placed at the 25 yard line, otherwise at the 20
        'Kicker gets a TB added to his Totals
        If Stats.Rows.Find(kicker).Item("Touchback") IsNot DBNull.Value Then 'Check for NULL Values
            Stats.Rows.Find(kicker).Item("Touchback") += 1
        Else Stats.Rows.Find(kicker).Item("Touchback") = 1
        End If
        ChangeOfPoss(If(HomePossession, False, True))
        YardLine = If(onKickoff, 25, 20)
        If onKickoff Then
            Console.WriteLine($"TOUCHBACK Kickoff: {PlayType}//KODist: {KickoffDist}//FairCatch?: {CallFairCatch}//Punt OOB?: {OutOfBounds}//Touchback?: {Touchback}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
        Else
            Console.WriteLine($"TOUCHBACK Punt: {PlayType}//Punt: {PuntDistance}//FairCatch?: {CallFairCatch}//Punt OOB?: {OutOfBounds}//Touchback?: {Touchback}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
        End If

    End Sub

    ''' <summary>
    ''' KickOff after a score
    ''' </summary>
    Public Shared Sub KickoffEvt(homeTeamKickingOff As Boolean)
        'HomePossession = If(homeTeamKickingOff, False, True) 'This is the same as HomePossesion = homeTeamKickingOff ? True : False in C#
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
        Dim Run As Boolean
        Dim passType As New PassTypeEnum

        If Down = 4 Then
            If YardLine >= 67 Then 'Field Goal Range --- Attempt a FG
                KickFG(If(HomePossession, HomeDT, AwayDT), ScoringTypeEnum.FG)
                Exit Sub ' Exits sub so we don't re-run code
            ElseIf YardLine >= 60 And YardsToGo < 4 Then 'Go For it
                If YardsToGo <= 1 Then '1 yard or less on 4th down
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 70.7 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                ElseIf YardsToGo <= 2 Then '2 Yards or less to go on 4th down
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 25.5 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Else 'More than 2 Yards to go on 4th down
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 12.2 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                End If
            Else
                Punt(If(HomePossession, HomeDT, AwayDT)) 'If they aren't in FG range and not in "go for it" range then they punt
                Exit Sub 'Exits out of the sub so the code doesn't keep running
                'TODO: Add code to make them check the gametime to see if they punt or not
            End If
        ElseIf Down = 3 Then 'Its 3rd Down
            Select Case YardsToGo
                Case <= 1 '1 Yard or less
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 70.1 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 2 '1-2 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 52.2 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 3 '2-3 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 23.7 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 4 '3-4 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 13.3 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 5 '4-5 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 9.4 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 10 '6-10 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 8.5 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 15 '10 - 15 yards to go
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 9.7 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case Else '15+ Yards to go
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 18.1 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                    'Now we check the various conditions of the play:
            End Select
        ElseIf Down = 2 Then 'Its Second Down
            Select Case YardsToGo
                Case <= 1 '1 Yard or less
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 69.4 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 2 '1-2 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 65.6 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 3 '2-3 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 58.2 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 4 '3-4 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 52.5 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 5 '4-5 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 49.5 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 6 '5-6 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 47.3 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 7 '6-7 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 41.6 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 8 '7-8 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 34.7 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 9 '8-9 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 31.2 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 10 '9-10 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 36.1 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 12 '10-12 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 35.6 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 15 '12 - 15 yards to go
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 23.5 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case Else '15+ Yards to go
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 25.9 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
            End Select
        Else 'Its First Down
            Select Case YardsToGo
                Case <= 1 '1 Yard or less
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 80.3 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 2 '1-2 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 73.2 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 3 '2-3 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 60.8 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 4 '3-4 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 61.3 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 5 '4-5 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 61.5 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 6 '5-6 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 60.3 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 7 '6-7 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 55.3 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 8 '7-8 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 56.8 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 9 '8-9 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 64.3 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 10 '9-10 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 49 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 12 '10-12 Yards
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 48.9 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case <= 15 '12 - 15 yards to go
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 38.4 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
                Case Else '15+ Yards to go
                    Select Case MyRand.GenerateDouble(0, 100)
                        Case 0 To 34.4 : Run = True 'Running Play
                        Case Else : Run = False 'Passing Play
                    End Select
            End Select
        End If
        'Checks to see if there was a fumbled QB Exchange---play would be aborted if there is
        If Fumble(0, 0, PlayTypeEnum.FumQBExchange) Then
            FumRec(0, HomeDT, AwayDT, PlayTypeEnum.FumQBExchange) 'Checks to see if there was a fumble on the exchange/snap
            PlayType = PlayTypeEnum.FumQBExchange

        Else 'Now we check the various conditions of the play
            If Run Then 'This is a running play --- lets check any Run Specific code
                If Fumble(0, 0, PlayTypeEnum.FumQBHandoff) Then 'There was a fumble on the Handoff---play is aborted--check to see who recovers the fumble
                    FumRec(0, HomeDT, AwayDT, PlayTypeEnum.FumQBHandoff)
                Else
                    YardsGained = Math.Round(GetRunYards(GetRunType), 1)
                End If
            Else 'This is a Passing Play
                passType = GetPassType() 'Get the pass type
                If Sacked(PlayType) Then 'Check to see if the QB gets sacked
                    YardsGained = Math.Abs(Math.Round(MyRand.GetGaussian(-4.5, 3.5), 1)) * -1
                    Console.WriteLine($"***SACK!!***  Yards Lost: {YardsGained}")
                    ClockStopped = False
                    If Fumble(0, 0, PlayTypeEnum.FumQBSacked) Then 'Checks to see if there is a fumble on the sack
                        Console.WriteLine("***FUMBLE!!**")
                        FumRec(0, HomeDT, AwayDT, PlayTypeEnum.FumQBSacked)
                        PlayType = PlayTypeEnum.FumQBSacked
                    End If
                Else
                    IsComplete = GetPassCompletion(passType)
                    If IsComplete Then
                        YardsGained = Math.Round(GetPassYards(passType), 1)
                        ClockStopped = False
                    Else 'Incomplete Pass
                        ClockStopped = True
                        YardsGained = 0
                    End If
                End If
            End If
        End If
        'Now we check all non-type specific play code:
        YardLine += YardsGained
        If Not IsTouchdown() Then 'Is it a Touchdown?  Checks to see and then runs TD Code if it is
            If Fumble(0, 0, PlayType) Then
                Fumble(0, 0, PlayType) 'Check to see if its a fumble
                ClockStopped = False
            ElseIf YardLine < 0 Then 'Safety
                Safety()
            Else 'Its not a safety or a fumble
                YardsToGo -= YardsGained 'Sets how many yards to go
                Down = If(YardsToGo <= 0, 1, Down + 1) 'checks to see if its a first down
                If Down = 1 Then 'Its a first down, reset the yards to go
                    YardsToGo = If(YardLine < 91, 10, 100 - YardLine) 'If its anywhere past before the Opponent 10 yard line it resets to 10, otherwise its Goal to Go
                End If
            End If
            GameTime = GameTime.Subtract(GetTimeOffClock(YardsGained, PlayType)) 'Runs time off clock based on what just happened

            Console.WriteLine($"Play: {PlayType}//Yards Gained: {YardsGained}//Down: {Down}//YardsToGo: {YardsToGo}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//IsComplete(If Pass): {IsComplete}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
        End If
    End Sub

    Private Shared Function IsTouchdown() As Boolean
        Dim TD As Boolean
        If YardLine >= 100 Then 'Its a TouchDown
            ClockStopped = True
            TD = True
            YardsGained -= (YardLine - 100) 'Sets the YardsGained to the proper amount
            Select Case PlayType
                Case PlayTypeEnum.RunInside, PlayTypeEnum.RunOutside : ScoringType = ScoringTypeEnum.RushingTD
                Case PlayTypeEnum.PassBehindLOS, PlayTypeEnum.PassLong, PlayTypeEnum.PassMed, PlayTypeEnum.PassShort : ScoringType = ScoringTypeEnum.PassingTD
                Case PlayTypeEnum.KickoffRet : ScoringType = ScoringTypeEnum.KORetTD
                Case PlayTypeEnum.PuntReturn : ScoringType = ScoringTypeEnum.PuntRetTD
                Case PlayTypeEnum.PuntBlockRet : ScoringType = ScoringTypeEnum.DefFumRecTD
                Case PlayTypeEnum.Interception : ScoringType = ScoringTypeEnum.IntReturnTD
            End Select
            Console.WriteLine($"**TOUCHDOWN!!**: {PlayType}//Yards Gained: {YardsGained}//Down: {Down}//YardsToGo: {YardsToGo}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//IsComplete(If Pass): {IsComplete}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
            UpdateScore(ScoringType)
            XPConv()
        End If
        Return TD
    End Function

    Private Shared Sub Safety()
        ScoringType = ScoringTypeEnum.Safety
        UpdateScore(ScoringType)
        Console.WriteLine($"**SAFETY!!**: {PlayType}//Yards Gained: {YardsGained}//Down: {Down}//YardsToGo: {YardsToGo}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
        ClockStopped = True
        FreeKickPunt(HomePossession)
    End Sub
    ''' <summary>
    ''' Kick Off from the 20 yard line after a safety(Punt)
    ''' </summary>
    Private Shared Sub FreeKickPunt(homeTeamKickingOff As Boolean)
        YardLine = 20
        HomePossession = If(homeTeamKickingOff, False, True) 'This is the same as HomePossesion = homeTeamKickingOff ? True : False in C#
        Console.WriteLine($"**FREE KICK PUNT AFTER SAFETY!!**: {PlayType}//Yards Gained: {YardsGained}//Down: {Down}//YardsToGo: {YardsToGo}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
        KickoffRet(If(HomePossession, FindPlayerId(Stats, "WR4", HmTeamId), FindPlayerId(Stats, "WR4", AwTeamId)), False)
    End Sub
    ''' <summary>
    ''' Checks to see if QB gets sacked by down
    ''' </summary>
    ''' <param name="play"></param>
    ''' <returns></returns>
    Private Shared Function Sacked(play As PlayTypeEnum) As Boolean
        Dim IsSacked As Boolean
        Select Case Down
            Case 1 : If MyRand.GenerateDouble(0, 100) < 4.55 Then IsSacked = True
            Case 2 : If MyRand.GenerateDouble(0, 100) < 4.9 Then IsSacked = True
            Case 3 : If MyRand.GenerateDouble(0, 100) < 8.5 Then IsSacked = True
            Case 4 : If MyRand.GenerateDouble(0, 100) < 4.25 Then IsSacked = True
        End Select
        Return IsSacked
    End Function

    Private Shared Sub XPConv()
        Dim PointDiff = If(HomePossession, HomeScore - AwayScore, AwayScore - HomeScore)
        Dim PassType As New PassTypeEnum
        If Quarter = 4 Then
            Select Case Math.Abs(PointDiff)
                'Attempt 2 Point Conv
                Case 2, 5, 8, 11, 16, > 18
                    YardLine = 98
                    Select Case MyRand.GenerateInt32(0, 100)
                        Case < 44
                            If GetRunYards(GetRunType) >= 2 Then
                                ScoringType = ScoringTypeEnum.TwoPointConv
                                UpdateScore(ScoringType) '2Pt Conversion succeeds
                            End If
                        Case Else
                            PassType = GetPassType()
                            IsComplete = GetPassCompletion(PassType)
                            If IsComplete Then
                                If GetPassYards(PassType) >= 2 Then
                                    ScoringType = ScoringTypeEnum.TwoPointConv
                                    UpdateScore(ScoringType) '2Pt Conversion Succeeds
                                End If
                            End If
                    End Select
                Case Else 'XP Attempt
                    YardLine = 85
                    ScoringType = ScoringTypeEnum.XP
                    KickFG(If(HomePossession, HomeDT, AwayDT), ScoringType)
            End Select
        Else 'Not the 4th Quarter
            YardLine = 85
            ScoringType = ScoringTypeEnum.XP
            KickFG(If(HomePossession, HomeDT, AwayDT), ScoringType)
        End If

        Kickoff = True 'Kickoff after the Extra Point Attempt
    End Sub
    ''' <summary>
    ''' Updates the score based on the type of scoring play
    ''' </summary>
    ''' <param name="play"></param>
    Private Shared Sub UpdateScore(play As ScoringTypeEnum)
        Select Case play
            Case ScoringTypeEnum.OffFumRecTD, ScoringTypeEnum.PassingTD, ScoringTypeEnum.RushingTD, ScoringTypeEnum.PuntRetTD, ScoringTypeEnum.KORetTD
                HomeScore = If(HomePossession, HomeScore + 6, HomeScore)
                AwayScore = If(HomePossession, AwayScore, AwayScore + 6)
            Case ScoringTypeEnum.FG
                HomeScore = If(HomePossession, HomeScore + 3, HomeScore)
                AwayScore = If(HomePossession, AwayScore, AwayScore + 3)
            Case ScoringTypeEnum.TwoPointConv 'Two Point Conversion
                HomeScore = If(HomePossession, HomeScore + 2, HomeScore)
                AwayScore = If(HomePossession, AwayScore, AwayScore + 2)
            Case ScoringTypeEnum.XP 'Extra Point
                HomeScore = If(HomePossession, HomeScore + 1, HomeScore)
                AwayScore = If(HomePossession, AwayScore, AwayScore + 1)
            Case ScoringTypeEnum.Safety, ScoringTypeEnum.DefXPReturnFor2Pts 'Defensive 2 Points
                HomeScore = If(HomePossession, HomeScore, HomeScore + 2)
                AwayScore = If(HomePossession, AwayScore + 2, AwayScore)
            Case ScoringTypeEnum.DefFumRecTD, ScoringTypeEnum.IntReturnTD 'Defensive TD
                HomeScore = If(HomePossession, HomeScore, HomeScore + 6)
                AwayScore = If(HomePossession, AwayScore + 6, AwayScore)
        End Select
    End Sub
    ''' <summary>
    ''' Punt
    ''' </summary>
    ''' <param name="DT"></param>
    Private Shared Sub Punt(DT As DataTable)
        Select Case YardLine
            Case < 45
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 0.35 : Touchback = True 'Touchback
                    Case Else
                        Touchback = False
                        PuntDistance = MyRand.GetGaussian(45.3, 3.5) 'No Touchback
                        CallFairCatch = MyRand.GenerateDouble(0, 100) < 5.45 'Did returner call a fair catch?
                        OutOfBounds = MyRand.GenerateDouble(0, 100) < 2.99 'Did punt go out of bounds?
                End Select
            Case 45 To 55
                Select Case MyRand.GenerateDouble(0, 100)
                    Case < 15.71 : Touchback = True 'Touchback
                    Case Else 'No Touchback
                        Touchback = False
                        PuntDistance = MyRand.GenerateInt32(27, 99 - YardLine) 'Has to at least be at the 1 yard line
                        CallFairCatch = MyRand.GenerateDouble(0, 100) < 47.14 'Did the returner call a fair catch?
                        OutOfBounds = MyRand.GenerateDouble(0, 100) < 7.74 'Did the punt go out of bounds?
                End Select
            Case Else 'Punting from inside the opponent's 45
                Select Case MyRand.GenerateDouble(0, 100)
                    Case < 21.93 : Touchback = True 'Touchback
                    Case Else 'No Touchback
                        Touchback = False
                        PuntDistance = MyRand.GenerateInt32(27, 99 - YardLine) 'Has to at least be at the 1 yard line
                        CallFairCatch = MyRand.GenerateDouble(0, 100) < 36.9 'Did the returner call a fair catch?
                        OutOfBounds = MyRand.GenerateDouble(0, 100) < 6.42 'Did the punt go out of bounds?
                End Select
        End Select
        If Touchback Then
            PuntDistance = 100 - YardLine
            PuntReturnYards = 0
            IsTouchback(False, If(HomePossession, FindPlayerId(Stats, "P1", HmTeamId), FindPlayerId(Stats, "P1", AwTeamId)))
        Else
            YardLine = YardLine + PuntDistance
            If Not CallFairCatch And Not OutOfBounds Then
                PuntReturn() 'If there is no fair catch and it doesn't go out of bounds then get the return yards
            Else PuntReturnYards = 0
            End If

            Console.WriteLine($"Play: {PlayType}//Punt: {PuntDistance}//FairCatch?: {CallFairCatch}//Punt OOB?: {OutOfBounds}//Touchback?: {Touchback}//Punt Returned: {PuntReturnYards}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//IsComplete(If Pass): {IsComplete}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
        End If
        GetTimeOffClock(PuntDistance, PlayTypeEnum.Punt) 'Get the time that ran off the clock for this play

    End Sub

    Private Shared Sub PuntReturn()
        Select Case MyRand.GenerateInt32(0, 100)
            Case 0 To 50 : PuntReturnYards = MyRand.GenerateInt32(0, 5)
            Case 51 To 75 : PuntReturnYards = MyRand.GenerateInt32(6, 11)
            Case 76 To 90 : PuntReturnYards = MyRand.GenerateInt32(12, 19)
            Case 91 To 95 : PuntReturnYards = MyRand.GenerateInt32(20, 40)
            Case 96 To 98 : PuntReturnYards = MyRand.GenerateInt32(41, 70)
            Case Else : PuntReturnYards = MyRand.GenerateInt32(71, 100)
        End Select
        YardLine -= PuntReturnYards 'Posession Hasn't change yet
        Fumble(0, 0, PlayTypeEnum.PuntReturn) 'Did the BallCarrier fumble?
        GetTimeOffClock(PuntReturnYards, PlayTypeEnum.PuntReturn)
        ChangeOfPoss(If(HomePossession, False, True))
    End Sub

    Private Shared Sub KickFG(DT As DataTable, play As ScoringTypeEnum)
        Dim FGMade As Boolean
        Select Case play
            Case ScoringTypeEnum.XP 'XP
                If MyRand.GenerateDouble(0, 100) <= 93.99 Then
                    ScoringType = ScoringTypeEnum.XP
                    UpdateScore(ScoringType) 'XP Made---93.99%last year
                    Console.WriteLine($"**XP GOOD!!**: {PlayType}//Yards Gained: {YardsGained}//Down: {Down}//YardsToGo: {YardsToGo}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
                Else
                    Console.WriteLine($"**XP NO GOOD!!**: {PlayType}//Yards Gained: {YardsGained}//Down: {Down}//YardsToGo: {YardsToGo}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
                End If
            Case Else 'FG
                Select Case YardLine
                    Case >= 98 : If MyRand.GenerateDouble(0, 100) <= 99.3 Then
                            ScoringType = ScoringTypeEnum.FG
                            UpdateScore(ScoringType)
                            FGMade = True
                        End If
                    Case >= 88 : If MyRand.GenerateDouble(0, 100) <= 96.15 Then
                            ScoringType = ScoringTypeEnum.FG
                            UpdateScore(ScoringType)
                            FGMade = True
                        End If
                    Case >= 78 : If MyRand.GenerateDouble(0, 100) <= 92.15 Then
                            ScoringType = ScoringTypeEnum.FG
                            UpdateScore(ScoringType)
                            FGMade = True
                        End If
                    Case >= 68 : If MyRand.GenerateDouble(0, 100) <= 77.1 Then
                            ScoringType = ScoringTypeEnum.FG
                            UpdateScore(ScoringType)
                            FGMade = True
                        End If
                    Case Else : If MyRand.GenerateDouble(0, 100) <= 53.33 Then
                            ScoringType = ScoringTypeEnum.FG
                            UpdateScore(ScoringType)
                            FGMade = True
                        End If
                End Select
                If FGMade Then
                    ClockStopped = True
                    YardLine = 35
                    Kickoff = True
                    Console.WriteLine($"**FG GOOD!!**: {PlayType}//Yards Gained: {YardsGained}//Down: {Down}//YardsToGo: {YardsToGo}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
                Else 'FG Missed
                    ClockStopped = True
                    YardLine -= 7
                    ChangeOfPoss(If(HomePossession, False, True))
                    Console.WriteLine($"**FG NO GOOD!!**: {PlayType}//Yards Gained: {YardsGained}//Down: {Down}//YardsToGo: {YardsToGo}//YardLine: {YardLine}//GameTime: {GameTime}//Pace: {Pace}
            //ClockStopped?: {ClockStopped}//PlayTime: {PlayTime}//BallSpotTime: {BallSpotTime}//HomeScore: {HomeScore}//AwayScore: {AwayScore}
            //HomeTeamHasBall?: {HomePossession}")
                End If
        End Select
        GetTimeOffClock(0, PlayTypeEnum.FGMade)
    End Sub
End Class