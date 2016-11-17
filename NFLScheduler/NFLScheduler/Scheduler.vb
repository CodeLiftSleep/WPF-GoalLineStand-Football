'generates a schedule for the season using NFL rules
Imports System.IO
Public Class Scheduler
    Dim HomeTeam As New Collection
    Dim AwayTeam As New Collection
    Dim HomeTeamCopy As New Collection
    Dim AwayTeamCopy As New Collection
    Dim DoOnce As Boolean
    Dim DoOnce2 As Boolean = False
    Dim SW As StreamWriter
    Dim AlreadyPlayed(32) As Integer
    Dim AlreadyPlayed2(32) As Integer
    Dim ScheduleH(17, 16) As Integer
    Dim ScheduleA(17, 16) As Integer
    Dim HadToExitSub As Boolean
    Dim GameH As New Collection
    Dim GameA As New Collection
    Dim GameH2 As New Collection
    Dim GameA2 As New Collection
    Dim BoolCheck As Boolean
    Dim StartedOver As Integer
    Dim TeamsLeftToPlay As New Collection
    Dim RedoGame As Boolean
    Dim ConHGames(32) As Integer 'tracks the number of Consecutive Home Games a team has played
    Dim ConAGames(32) As Integer 'tracks the number of Consecutive AWay Games a team has played
    Dim LastDivGamePlayed(32) As Integer 'checks the last division game played for each team.   
    Dim GetTables As New SQLFunctions.DataFunctions
    Dim CityInfo As New DataTable
    Dim MT As New Mersenne.MersenneTwister
    Dim InitTeams(32) As Teams



    ''' <summary>
    ''' Creates a schedule following all appropriate NFL Rules.  No more than 3 consecutive home or away games.  Teams may not play each other in consecutive weeks
    ''' CHECKS:
    ''' 1) If the game is a divisional game---ensure the same teams didn't play the week before
    ''' 2) Can every team play a game this week if this game gets scheduled---if yes, continue, if no, redo the weekly schedule.
    ''' 3) If over 60 attempts are made and a weekly schedule still cannot be made---restart entire schedule over again
    ''' 4) 
    ''' </summary>
    ''' <param name="NumGames"></param>
    Public Sub GetSchedule(ByVal NumGames As Integer)
        If DoOnce = False Then 'first time through the scheduler

            SW = New StreamWriter("Schedule.txt")

            Dim SQLString As String = "TeamID int Not NULL, DivID int NOT NULL, ConfID int NOT NULL, TeamFName varchar(20) NOT NULL, TeamLName varchar(20) NOT NULL, LastYearFinish int NOT NULL, DivOutConfID int NOT NULL, DivInConfID int NOT NULL, LYFinishHGSched int NOT NULL, TeamNick char(10) NOT NULL CONSTRAINT Team_ID PRIMARY KEY(TeamID)"
            GetTables.CreateTable(CityInfo, "CityInfo", SQLString)
            GetTables.LoadTable(CityInfo, "CityInfo")

            For i As Integer = 1 To CityInfo.Rows.Count 'fills the DT with the Database information
                InitTeams(i).TeamId = CityInfo.Rows(i - 1).Item("TeamID")
                InitTeams(i).DivID = CityInfo.Rows(i - 1).Item("DivID")
                InitTeams(i).ConfID = CityInfo.Rows(i - 1).Item("ConfID")
                InitTeams(i).TeamFName = CityInfo.Rows(i - 1).Item("TeamFName").ToString.Trim
                InitTeams(i).TeamLName = CityInfo.Rows(i - 1).Item("TeamLName").ToString.Trim
                InitTeams(i).DivFinishLastYear = CityInfo.Rows(i - 1).Item("LastYearFinish")
                InitTeams(i).DivOutConfID = CityInfo.Rows(i - 1).Item("DivOutConfID")
                InitTeams(i).DivInConfID = CityInfo.Rows(i - 1).Item("DivInConfID")
                InitTeams(i).LYFinishHGSched = CityInfo.Rows(i - 1).Item("LYFinishHGSched")
                InitTeams(i).TeamNick = CityInfo.Rows(i - 1).Item("TeamNick")
            Next i

            ScheduleDivGames() 'schedules divisional games for each teams
            ScheduleInConfGames() 'schedules games against the one non-divisional in the same conference here you play all teams
            OutofConfGames() 'schedules the Out of conference games for each team
            InConfFinish() 'schedules the 2 games each team plays against the same place finisher of the other 2 divisions you don't play all the teams in
            GetByeWeek() 'Schdules Bye Weeks for teams
            DoOnce = True

            For i As Integer = 1 To HomeTeam.Count 'copies the list of games to a backup in case it needs to be restored if games cannot be played.
                HomeTeamCopy.Add(HomeTeam(i))
                AwayTeamCopy.Add(AwayTeam(i))
            Next i
        End If

        For i As Integer = 1 To 32
            'Console.WriteLine("Team " & i & " " & InitTeams(i).TeamFName & "")
        Next
        Dim TeamsOnBye As String = ""
        For i As Integer = 1 To 17 'sets the schedule to 15 games since the last 2 divisional games have already been scheduled
            Dim countbye As Integer

            If i < 16 Then

                countbye = 0
                For checkbye As Integer = 1 To 32
                    If InitTeams(checkbye).ByeWeek = i Then
                        TeamsOnBye += InitTeams(checkbye).TeamNick & ", "
                        countbye += 1
                    End If
                Next checkbye
                CreateWeeklySchedule(i, countbye)

                Do While GameH.Count = 0
                    CreateWeeklySchedule(i, countbye)
                Loop

                Console.WriteLine("-----------------------------")
                Console.WriteLine("Week " & i & " schedule")
                Console.WriteLine("-----------------------------")

                SW.WriteLine("-----------------------") 'sends output to file
                SW.WriteLine("WEEK " & i & " SCHEDULE")
                SW.WriteLine("-----------------------")

                If TeamsOnBye <> "" Then 'Only runs if teams have a ByeWeek
                    Console.WriteLine("Teams On Bye: " & TeamsOnBye.Trim)
                    SW.WriteLine("Teams On Bye: " & TeamsOnBye.Trim)
                End If

                TeamsOnBye = ""

                For X As Integer = 1 To GameH.Count

                    If InitTeams(GameH.Item(X)).DivID = InitTeams(GameA.Item(X)).DivID Then 'is this a divisional game being scheduled?

                        If LastDivGamePlayed(GameH.Item(X)) = LastDivGamePlayed(GameA.Item(X)) And LastDivGamePlayed(GameH.Item(X)) <> 0 Then 'these two team played the week before---restart the schedule for week
                            Console.WriteLine("The same teams are playing 2 weeks in a row!!")
                            '####TO DO Re-write Schedule re-start algorithm to a seperate Sub so it can be called by multiple places
                        Else 'Schedule the game and update LastGamePlayed to the proper team
                            LastDivGamePlayed(GameH.Item(X)) = GameA.Item(X)
                            LastDivGamePlayed(GameA.Item(X)) = GameH.Item(X)
                        End If
                    Else 'Not A Divisional Game, so set LastDivGamePlayed to 0
                        LastDivGamePlayed(GameH.Item(X)) = 0
                        LastDivGamePlayed(GameA.Item(X)) = 0
                    End If

                    ScheduleH(i, X) = GameH.Item(X)
                    ScheduleA(i, X) = GameA.Item(X)
                    Console.WriteLine("" & InitTeams(GameH.Item(X)).TeamNick & "(" & GameH.Item(X) & ") vs. " & InitTeams(GameA.Item(X)).TeamNick & "(" & GameA.Item(X) & ")")
                    SW.WriteLine("" & InitTeams(GameH.Item(X)).TeamNick & "(" & GameH.Item(X) & ") vs. " & InitTeams(GameA.Item(X)).TeamNick & "(" & GameA.Item(X) & ")")
                    ConHGames(GameH.Item(X)) += 1 'Adds a home game to the consecutive home games count
                    ConAGames(GameH.Item(X)) = 0 'resets consecutive away games to 0
                    ConAGames(GameA.Item(X)) += 1 'Adds an away game to the consecutive away games count
                    ConHGames(GameA.Item(X)) = 0 'resets consecutive home games to 0

                    Console.WriteLine("Con Home Games: " & ConHGames(GameH.Item(X)) & " Cons Away Games: " & ConAGames(GameA.Item(X)))
                    AlreadyPlayed(GameH.Item(X)) = 0
                    AlreadyPlayed(GameA.Item(X)) = 0

                Next X


                'verification only 1 team per week
                For teamcheck As Integer = 1 To 32
                    Dim countdup As Integer = 0
                    AlreadyPlayed(teamcheck) = 0
                    For x As Integer = 1 To GameH.Count
                        If teamcheck = GameH.Item(x) Or GameA.Item(x) Then
                            countdup += 1
                            If countdup > 1 Then
                                Console.WriteLine("Team " & teamcheck & " Is playing more than 1 game In week " & i & "")
                            End If
                        End If
                        countdup = 0
                    Next x
                Next teamcheck

                GameH.Clear() 'clears this weeks slate of games
                GameA.Clear() 'clears this weeks slate of games
                SW.Flush() 'flushes the buffer to prevent truncated outputs


            ElseIf i = 16 Or 17 Then

                Console.WriteLine("-----------------------------")
                Console.WriteLine("Week " & i & " schedule")
                Console.WriteLine("-----------------------------")

                SW.WriteLine("-----------------------") 'sends output to file
                SW.WriteLine("WEEK " & i & " SCHEDULE")
                SW.WriteLine("-----------------------")

                For X As Integer = 1 To 16
                    Console.WriteLine("" & InitTeams(ScheduleH(i, X)).TeamNick & "(" & ScheduleH(i, X) & ") vs. " & InitTeams(ScheduleA(i, X)).TeamNick & "(" & ScheduleA(i, X) & ")")
                    SW.WriteLine("" & InitTeams(ScheduleH(i, X)).TeamNick & "(" & ScheduleH(i, X) & ") vs. " & InitTeams(ScheduleA(i, X)).TeamNick & "(" & ScheduleA(i, X) & ")")
                    'Console.WriteLine("Con Home Games: " & ConHGames(GameH.Item(X)) & " Cons Away Games: " & ConAGames(GameA.Item(X)))
                Next X

            End If
            SW.Flush()
        Next i


    End Sub

    Public Structure Teams
        Dim TeamId As Integer
        Dim TeamFName As String
        Dim TeamLName As String
        Dim DivID As Integer
        Dim ConfID As Integer
        Dim YearNum As Integer
        Dim DivFinishLastYear As Integer
        Dim DivOutConfID As Integer
        Dim DivInConfID As Integer
        Dim ByeWeek As Integer
        Dim SchedID As Integer
        Dim LYFinishHGSched As Integer
        Dim LYFinishAGSched As Integer
        Dim TeamNick As String
        Dim HomeGames As List(Of Integer)
        Dim AwayGames As List(Of Integer)
    End Structure
    ''' <summary>
    ''' Schedules 1 Home and 1 Away game against each team in the same division
    ''' </summary>
    Private Sub ScheduleDivGames()
        'schedules all the division games
        Dim Teams As New Collection
        Dim Div1 As New Collection
        Dim i As Integer

        For DivID As Integer = 1 To 8 'cycles through the 8 divisions
            Do While Div1.Count < 4 And i < 33 'Number of teams in the division are under 4(4th team already has all games scheduled) and teams are under 33
                If InitTeams(i).DivID = DivID Then 'checks to see if that team is in the division
                    Div1.Add((InitTeams(i).TeamId)) 'Adds team to the count of teams in the division.
                End If
                i += 1
            Loop

            HomeTeam.Add(Div1.Item(1)) 'adds 3 homes games for team
            HomeTeam.Add(Div1.Item(1))
            HomeTeam.Add(Div1.Item(1))
            AwayTeam.Add(Div1.Item(2)) 'adds away games
            AwayTeam.Add(Div1.Item(3))
            AwayTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(2))
            HomeTeam.Add(Div1.Item(2))
            HomeTeam.Add(Div1.Item(2))
            AwayTeam.Add(Div1.Item(1))
            AwayTeam.Add(Div1.Item(3))
            AwayTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(3))
            HomeTeam.Add(Div1.Item(3))
            HomeTeam.Add(Div1.Item(3))
            AwayTeam.Add(Div1.Item(1))
            AwayTeam.Add(Div1.Item(2))
            AwayTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(4))
            AwayTeam.Add(Div1.Item(1))
            AwayTeam.Add(Div1.Item(2))
            AwayTeam.Add(Div1.Item(3))

            EndWithDivisionGames(Div1.Item(1), Div1.Item(2), Div1.Item(3), Div1.Item(4), 4)

            Div1.Clear()
            i = 0
        Next DivID


    End Sub
    ''' <summary>
    ''' Each division plays one other division in the same conference each year.
    ''' </summary>
    Private Sub ScheduleInConfGames()
        Dim div1 As New Collection
        Dim div2 As New Collection
        Dim i As Integer
        Dim Sched(8) As Boolean
        Dim oppdiv As Integer

        For DivID As Integer = 1 To 8

            If Sched(DivID) = False Then
                Sched(DivID) = True
                Do While div1.Count < 4 And i < 33
                    If InitTeams(i).DivID = DivID Then
                        div1.Add(InitTeams(i).TeamId)
                        oppdiv = InitTeams(i).DivInConfID
                    End If
                    i += 1
                Loop
                i = 0
                Do Until div2.Count = 4 And i > 32
                    If InitTeams(i).DivID = oppdiv Then
                        div2.Add(InitTeams(i).TeamId)
                    End If
                    i += 1
                Loop
                Sched(oppdiv) = True
                'randomize the teams positions in the divisions
                Dim div3 As New Collection
                Dim div4 As New Collection

                For count As Integer = 1 To div1.Count
                    Dim Mixup1 As Integer = MT.GenerateInt32(1, div1.Count)
                    Dim Mixup2 As Integer = MT.GenerateInt32(1, div2.Count)
                    div3.Add(div1.Item(Mixup1))
                    div4.Add(div2.Item(Mixup2))
                    div1.Remove(Mixup1)
                    div2.Remove(Mixup2)
                Next count
                HomeTeam.Add(div3.Item(1))
                AwayTeam.Add(div4.Item(4))
                HomeTeam.Add(div4.Item(3))
                AwayTeam.Add(div3.Item(1))
                HomeTeam.Add(div4.Item(2))
                AwayTeam.Add(div3.Item(1))
                HomeTeam.Add(div3.Item(1))
                AwayTeam.Add(div4.Item(1))

                HomeTeam.Add(div4.Item(4))
                AwayTeam.Add(div3.Item(2))
                HomeTeam.Add(div3.Item(2))
                AwayTeam.Add(div4.Item(3))
                HomeTeam.Add(div3.Item(2))
                AwayTeam.Add(div4.Item(2))
                HomeTeam.Add(div4.Item(1))
                AwayTeam.Add(div3.Item(2))

                HomeTeam.Add(div3.Item(3))
                AwayTeam.Add(div4.Item(3))
                HomeTeam.Add(div4.Item(1))
                AwayTeam.Add(div3.Item(3))
                HomeTeam.Add(div3.Item(3))
                AwayTeam.Add(div4.Item(2))
                HomeTeam.Add(div4.Item(4))
                AwayTeam.Add(div3.Item(3))

                HomeTeam.Add(div3.Item(4))
                AwayTeam.Add(div4.Item(1))
                HomeTeam.Add(div4.Item(2))
                AwayTeam.Add(div3.Item(4))
                HomeTeam.Add(div4.Item(3))
                AwayTeam.Add(div3.Item(4))
                HomeTeam.Add(div3.Item(4))
                AwayTeam.Add(div4.Item(4))
            Else
            End If
            div1.Clear()
            div2.Clear()
            i = 0
        Next DivID

    End Sub

    ''' <summary>
    ''' Schedules out of conference games for each division against another division in the other conference(ie, AFC East Vs. NFC South)
    ''' </summary>
    Private Sub OutofConfGames()
        Dim div1 As New Collection
        Dim div2 As New Collection
        Dim i As Integer
        Dim Sched(8) As Boolean
        Dim oppdiv As Integer

        For DivID As Integer = 1 To 8

            If Sched(DivID) = False Then
                Sched(DivID) = True
                Do While div1.Count < 4 And i < 33
                    If InitTeams(i).DivID = DivID Then
                        div1.Add(InitTeams(i).TeamId)
                        oppdiv = InitTeams(i).DivOutConfID
                    End If
                    i += 1
                Loop

                i = 0
                Do Until div2.Count = 4 And i > 32
                    If InitTeams(i).DivID = oppdiv Then
                        div2.Add(InitTeams(i).TeamId)
                    End If
                    i += 1
                Loop
                Sched(oppdiv) = True
                'randomize the teams positions in the divisions
                Dim div3 As New Collection
                Dim div4 As New Collection

                For count As Integer = 1 To div1.Count
                    Dim Mixup1 As Integer = MT.GenerateInt32(1, div1.Count)
                    Dim Mixup2 As Integer = MT.GenerateInt32(1, div2.Count)
                    div3.Add(div1.Item(Mixup1))
                    div4.Add(div2.Item(Mixup2))
                    div1.Remove(Mixup1)
                    div2.Remove(Mixup2)
                Next count
                HomeTeam.Add(div3.Item(1))
                AwayTeam.Add(div4.Item(4))
                HomeTeam.Add(div4.Item(3))
                AwayTeam.Add(div3.Item(1))
                HomeTeam.Add(div4.Item(2))
                AwayTeam.Add(div3.Item(1))
                HomeTeam.Add(div3.Item(1))
                AwayTeam.Add(div4.Item(1))

                HomeTeam.Add(div4.Item(4))
                AwayTeam.Add(div3.Item(2))
                HomeTeam.Add(div3.Item(2))
                AwayTeam.Add(div4.Item(3))
                HomeTeam.Add(div3.Item(2))
                AwayTeam.Add(div4.Item(2))
                HomeTeam.Add(div4.Item(1))
                AwayTeam.Add(div3.Item(2))

                HomeTeam.Add(div3.Item(3))
                AwayTeam.Add(div4.Item(3))
                HomeTeam.Add(div4.Item(1))
                AwayTeam.Add(div3.Item(3))
                HomeTeam.Add(div3.Item(3))
                AwayTeam.Add(div4.Item(2))
                HomeTeam.Add(div4.Item(4))
                AwayTeam.Add(div3.Item(3))

                HomeTeam.Add(div3.Item(4))
                AwayTeam.Add(div4.Item(1))
                HomeTeam.Add(div4.Item(2))
                AwayTeam.Add(div3.Item(4))
                HomeTeam.Add(div4.Item(3))
                AwayTeam.Add(div3.Item(4))
                HomeTeam.Add(div3.Item(4))
                AwayTeam.Add(div4.Item(4))
            Else
            End If
            div1.Clear()
            div2.Clear()
            i = 0
        Next DivID
    End Sub
    '''<summary>In the NFL, each team plays 12 games in their own conference. 6 games are played against teams in their division(the teams play twice),
    '''4 games are played against a single other division(each team played once), and the remaining two games are played against the other 2 divisions' teams
    '''that finished in the same place in their division(ie, 3rd place team from Div1 plays 3rd place team from Div3 and Div4 because they already are playing</summary> 
    Private Sub InConfFinish()


        Dim HTeam As New Collection
        Dim ATeam As New Collection
        Dim RunOnce As Boolean = False

        For Team As Integer = 1 To 32 'cycle through the teams in the conference
            'Do While 1 < 33
            For OppTeam As Integer = 1 To 32 'cycle through the teams in the conference
                If InitTeams(Team).DivInConfID <> InitTeams(OppTeam).DivID And Team < OppTeam And InitTeams(Team).DivID <> InitTeams(OppTeam).DivID And
                    InitTeams(Team).DivFinishLastYear = InitTeams(OppTeam).DivFinishLastYear And InitTeams(Team).ConfID = InitTeams(OppTeam).ConfID Then
                    'selects teams in the same conference, Not in the same division, who aren't playing all teams in that division already and finds teams with the same records

                    If HTeam.Count = 16 Then 'Conference Change
                        RunOnce = False
                    End If

                    If RunOnce = False Then
                        Dim rndInt As Integer = MT.GenerateInt32(0, 100)
                        If rndInt < 51 Then 'chooses first game to be home game
                            HTeam.Add(Team)
                            ATeam.Add(OppTeam)
                            InitTeams(Team).LYFinishHGSched = 1
                            InitTeams(OppTeam).LYFinishAGSched = 1
                            Console.WriteLine(InitTeams(Team).TeamFName & " vs. " & InitTeams(OppTeam).TeamFName)
                        Else
                            HTeam.Add(OppTeam)
                            ATeam.Add(Team)
                            InitTeams(OppTeam).LYFinishHGSched = 1
                            InitTeams(Team).LYFinishAGSched = 1
                            Console.WriteLine(InitTeams(OppTeam).TeamFName & " vs. " & InitTeams(Team).TeamFName)
                        End If

                    Else
                        For m As Integer = 1 To HTeam.Count  'checks to see if the team already has a homegame scheduled
                            'Dim Check As Boolean = False
                            If (m = Team Or ATeam.Item(m) = OppTeam) And InitTeams(m).LYFinishAGSched = 0 And InitTeams(m).LYFinishHGSched = 0 And InitTeams(OppTeam).LYFinishHGSched = 0 And InitTeams(OppTeam).LYFinishAGSched = 0 Then
                                Dim rndInt As Integer = MT.GenerateInt32(0, 100)
                                If rndInt < 51 Then 'chooses first game to be home game
                                    HTeam.Add(Team)
                                    ATeam.Add(OppTeam)
                                    InitTeams(Team).LYFinishHGSched = 1
                                    InitTeams(OppTeam).LYFinishAGSched = 1
                                    Console.WriteLine(InitTeams(Team).TeamFName & " vs. " & InitTeams(OppTeam).TeamFName)
                                    'Exit For
                                Else
                                    HTeam.Add(OppTeam)
                                    ATeam.Add(Team)
                                    InitTeams(OppTeam).LYFinishHGSched = 1
                                    InitTeams(Team).LYFinishAGSched = 1
                                    Console.WriteLine(InitTeams(OppTeam).TeamFName & " vs. " & InitTeams(Team).TeamFName)
                                    'Exit For
                                End If

                            ElseIf (m = Team Or ATeam.Item(m) = OppTeam) And InitTeams(OppTeam).LYFinishHGSched <> 1 And InitTeams(m).LYFinishAGSched = 0 Then 'This team already has a home game scheduled, must be the away team
                                HTeam.Add(OppTeam)
                                ATeam.Add(Team)
                                InitTeams(OppTeam).LYFinishHGSched = 1
                                InitTeams(Team).LYFinishAGSched = 1
                                Console.WriteLine(InitTeams(OppTeam).TeamFName & " vs. " & InitTeams(Team).TeamFName)
                                Exit For
                            ElseIf (m = Team Or HTeam.Item(m) = OppTeam) And InitTeams(m).LYFinishHGSched <> 1 And InitTeams(OppTeam).LYFinishAGSched = 0 Then 'This team already has an away game scheduled, must be the away team
                                HTeam.Add(Team)
                                ATeam.Add(OppTeam)
                                InitTeams(Team).LYFinishHGSched = 1
                                InitTeams(OppTeam).LYFinishAGSched = 1
                                Console.WriteLine(InitTeams(Team).TeamFName & " vs. " & InitTeams(OppTeam).TeamFName)
                                Exit For
                            ElseIf m = Team And InitTeams(m).LYFinishAGSched = 1 And InitTeams(OppTeam).LYFinishAGSched = 0 Then 'team must be home team
                                HTeam.Add(Team)
                                ATeam.Add(OppTeam)
                                InitTeams(Team).LYFinishHGSched = 1
                                InitTeams(OppTeam).LYFinishAGSched = 1
                                Console.WriteLine(InitTeams(Team).TeamFName & " vs. " & InitTeams(OppTeam).TeamFName)
                            ElseIf m = Team And InitTeams(OppTeam).LYFinishAGSched = 1 And InitTeams(m).LYFinishAGSched = 0 Then
                                HTeam.Add(OppTeam)
                                ATeam.Add(Team)
                                InitTeams(OppTeam).LYFinishHGSched = 1
                                InitTeams(Team).LYFinishAGSched = 1
                                Console.WriteLine(InitTeams(OppTeam).TeamFName & " vs. " & InitTeams(Team).TeamFName)
                            End If
                        Next m
                    End If
                End If

                If RunOnce = False And HTeam.Count <> 0 Then
                    RunOnce = True
                End If
            Next OppTeam
        Next Team

        For i As Integer = 1 To HTeam.Count 'adds games to the schedule.
            HomeTeam.Add(HTeam.Item(i))
            AwayTeam.Add(ATeam.Item(i))
        Next i
    End Sub

    Private Sub CreateWeeklySchedule(ByVal WeekNum As Integer, ByVal NumByeTeams As Integer) 'rewritten sub
        'rewrote 64 lines into 32

        Static count As Integer = 0
        Static NumRestarts As Integer = 0
        Static NumGamesLastWeek As Integer
        Static MaxConGames As Integer = 3
        Dim Gamecheck As Boolean
        Dim hometeamValue As Integer
        Dim awayteamValue As Integer
        Dim GameNum As Integer
        Dim PickGame As Integer
        Dim games As Integer = 0
        Dim IsHomeTeam As Boolean




        If WeekNum = 15 Then 'Schedules the last week of games to be played
            For i As Integer = 1 To 32
                If AlreadyPlayed(i) <> 1 Then
                    If InitTeams(i).AwayGames.Count > 0 Then
                        GameA.Add(i)
                        GameH.Add(InitTeams(i).AwayGames(0))
                        AlreadyPlayed(i) = 1
                        AlreadyPlayed(InitTeams(i).AwayGames(0)) = 1
                    ElseIf InitTeams(i).HomeGames.Count > 0 Then
                        GameH.Add(i)
                        GameA.Add(InitTeams(i).HomeGames(0))
                        AlreadyPlayed(i) = 1
                        AlreadyPlayed(InitTeams(i).HomeGames(0)) = 1
                    End If
                End If
            Next i
            Exit Sub
        End If

        For i As Integer = 1 To 32 'sets bye teams as unable to play this week
            If InitTeams(i).ByeWeek = WeekNum Then
                AlreadyPlayed(i) = 1
            End If

            If DoOnce2 = False Then
                InitTeams(i).HomeGames = New List(Of Integer)
                InitTeams(i).AwayGames = New List(Of Integer)

                For team As Integer = 1 To HomeTeam.Count
                    If i = HomeTeam.Item(team) Then
                        InitTeams(i).HomeGames.Add(AwayTeam.Item(team)) 'adds home games to this teams list
                    ElseIf i = AwayTeam.Item(team) Then
                        InitTeams(i).AwayGames.Add(HomeTeam.Item(team))
                    End If
                Next team
            End If
            'For game As Integer = 0 To InitTeams(i).HomeGames.Count - 1
            'Console.WriteLine(i & " Vs. " & InitTeams(i).HomeGames(game))
            'Next game
            '   For game As Integer = 0 To InitTeams(i).AwayGames.Count - 1
            '  Console.WriteLine(i & " at " & InitTeams(i).AwayGames(game))
            'Next game
        Next i
                DoOnce2 = True


        While games < (16 - (NumByeTeams / 2)) 'schedules this wekeks games ###Might want to change this to a While Loop---While games <> (16-(NumByeTeams/2))

            For team As Integer = 1 To 32 'go through each team and select a game from their schedule

                While AlreadyPlayed(team) <> 1 'skips this team if it already has played

                    If InitTeams(team).AwayGames.Count <> 0 And InitTeams(team).HomeGames.Count <> 0 Then 'has home and away games available to choose from
                        IsHomeTeam = MT.GenerateInt32(0, 1)
                        Select Case IsHomeTeam
                            Case 0 'randomly choose home game
                                PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)
                                hometeamValue = team
                                awayteamValue = InitTeams(team).HomeGames.Item(PickGame)
                            Case 1 'randomly chose away game
                                PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)
                                awayteamValue = team
                                hometeamValue = InitTeams(team).AwayGames.Item(PickGame)
                        End Select

                    ElseIf InitTeams(team).AwayGames.Count = 0 Then 'must choose home game from this teams schedule
                        PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)
                        hometeamValue = team
                        awayteamValue = InitTeams(team).HomeGames.Item(PickGame)

                    ElseIf InitTeams(team).HomeGames.Count = 0 Then 'must choose away game from this teams schedule
                        PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)
                        awayteamValue = team
                        hometeamValue = InitTeams(team).AwayGames.Item(PickGame)
                    End If

                    Gamecheck = CanGameBePlayed(WeekNum, GameNum, hometeamValue, awayteamValue)

                    While AlreadyPlayed(hometeamValue) = 1 Or AlreadyPlayed(awayteamValue) = 1 Or Gamecheck = False Or
                     LastDivGamePlayed(hometeamValue) = awayteamValue Or ConAGames(awayteamValue) = MaxConGames Or ConHGames(hometeamValue) = MaxConGames Or hometeamValue = awayteamValue 'Or
                        'InitTeams(hometeamValue).ByeWeek = WeekNum Or InitTeams(awayteamValue).ByeWeek = WeekNum

                        'rechooses game if team has played 3 games in a row home or away, team is on a bye this week, has already been chosen for a game, just played the same team the previosu week, 
                        'or if all other teams cannot play a game

                        If ConAGames(team) = MaxConGames Or InitTeams(team).AwayGames.Count = 0 Then 'must choose a home game
                            PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)

                            While ConAGames(InitTeams(team).HomeGames.Item(PickGame)) = MaxConGames 'if opponent has played more than 3 consecutive away games have to repick
                                PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)
                            End While
                            hometeamValue = team
                            awayteamValue = InitTeams(team).HomeGames.Item(PickGame)

                        ElseIf ConHGames(team) = MaxConGames Or InitTeams(team).HomeGames.Count = 0 Then 'must choose an away game
                            PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)

                            While ConHGames(InitTeams(team).AwayGames.Item(PickGame)) = MaxConGames 'if opponent has played more than 3 consecutive away games have to repick
                                PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)
                            End While

                            awayteamValue = team
                            hometeamValue = InitTeams(team).AwayGames.Item(PickGame)
                        Else 'doesn't matter

                            IsHomeTeam = MT.GenerateInt32(0, 1)
                            Select Case IsHomeTeam
                                Case 0  'randomly choose home game
                                    PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)
                                    hometeamValue = team
                                    awayteamValue = InitTeams(team).HomeGames.Item(PickGame)
                                Case 1 'randomly chose away game
                                    PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)
                                    awayteamValue = team
                                    hometeamValue = InitTeams(team).AwayGames.Item(PickGame)
                            End Select
                        End If

                        GameNum = (16 - (NumByeTeams / 2))
                        Gamecheck = CanGameBePlayed(WeekNum, GameNum, hometeamValue, awayteamValue)

                        count += 1

                        If GameH.Count = (16 - NumByeTeams / 2) Then 'full games are scheduled, exit loop
                            count = 0
                            Exit While
                        End If

                        If count >= 2500 Then  'restart entire week, too many attempts

                            For i As Integer = 1 To 32
                                For game As Integer = 1 To GameH.Count
                                    If i = GameH.Item(game) Then
                                        InitTeams(i).HomeGames.Add(GameA.Item(game)) 'puts the games back in the list
                                    ElseIf i = GameA.Item(game) Then
                                        InitTeams(i).AwayGames.Add(GameH.Item(game))
                                    End If
                                    If InitTeams(i).ByeWeek = WeekNum Then
                                        AlreadyPlayed(i) = 1
                                    Else
                                        AlreadyPlayed(i) = 0
                                    End If
                                Next game
                            Next i
                            GameH.Clear()
                            GameA.Clear()
                            games = 0 'resets the count to 1
                            'NumRestarts += 1
                            count = 0
                        End If

                    End While

                    If GameH.Count = (16 - NumByeTeams / 2) Then 'full games are scheduled exit loop
                        Exit While
                    End If



                    If count < 2500 And GameH.Count <= (16 - NumByeTeams / 2) Then 'And AlreadyPlayed(hometeamValue) <> 1 And AlreadyPlayed(awayteamValue) <> 1 Then 'only schedule this if all the games are in it
                        GameH.Add(hometeamValue)
                        GameA.Add(awayteamValue)
                        InitTeams(hometeamValue).HomeGames.Remove(awayteamValue)
                        InitTeams(awayteamValue).AwayGames.Remove(hometeamValue)
                        count = 0 'resets the count for the next game
                        games += 1 'increments the game count
                        AlreadyPlayed(hometeamValue) = 1
                        AlreadyPlayed(awayteamValue) = 1
                    End If
                End While

            Next team
        End While
        If GameH.Count = (16 - NumByeTeams / 2) Then
            'WeekNum += 1
            NumGamesLastWeek = GameH.Count
        End If

        Dim ConHGames2(32) As Integer
        Dim ConAGames2(32) As Integer
        Dim NumByeTeamsNextWeek As Integer
        count = 0

        Dim DoOnce3 As Boolean

        If DoOnce3 = False Then
            For i As Integer = 1 To 32 'cycle through teams to set Consecutive Games after this week
                ConHGames2(i) = ConHGames(i)
                ConAGames2(i) = ConAGames(i)
                If i = hometeamValue Then
                    ConHGames2(i) += 1
                    ConAGames2(i) = 0
                ElseIf i = awayteamValue Then
                    ConAGames2(i) += 1
                    ConHGames2(i) = 0
                End If
                If InitTeams(i).ByeWeek = WeekNum + 1 Then
                    AlreadyPlayed2(i) = 1
                    NumByeTeamsNextWeek += 1
                End If
            Next i
            DoOnce3 = True
        End If
        games = 0

        While games < (16 - NumByeTeamsNextWeek / 2) And WeekNum < 15 'last week of games is week 15, so these don't need to be checked since week 16 and 17 are already scheduled

            If GameH2.Count <> (16 - NumByeTeamsNextWeek / 2) Then

                For team As Integer = 1 To 32
                    If AlreadyPlayed2(team) <> 1 Then
                        If InitTeams(team).AwayGames.Count <> 0 And InitTeams(team).HomeGames.Count <> 0 Then 'has home and away games available to choose from
                            IsHomeTeam = MT.GenerateInt32(0, 1)
                            Select Case IsHomeTeam
                                Case 0 'randomly choose home game
                                    PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)
                                    hometeamValue = team
                                    awayteamValue = InitTeams(team).HomeGames.Item(PickGame)
                                Case 1 'randomly chose away game
                                    PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)
                                    awayteamValue = team
                                    hometeamValue = InitTeams(team).AwayGames.Item(PickGame)
                            End Select

                        ElseIf InitTeams(team).AwayGames.Count = 0 Then 'must choose home game from this teams schedule
                            PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)
                            hometeamValue = team
                            awayteamValue = InitTeams(team).HomeGames.Item(PickGame)

                        ElseIf InitTeams(team).HomeGames.Count = 0 Then 'must choose away game from this teams schedule
                            PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)
                            awayteamValue = team
                            hometeamValue = InitTeams(team).AwayGames.Item(PickGame)
                        End If

                        Gamecheck = CanNextWeekBePlayed(WeekNum, GameNum, hometeamValue, awayteamValue)

                        While AlreadyPlayed2(hometeamValue) = 1 Or AlreadyPlayed2(awayteamValue) = 1 Or Gamecheck = False Or
                        ConAGames2(awayteamValue) = MaxConGames Or ConHGames2(hometeamValue) = MaxConGames Or hometeamValue = awayteamValue 'Or
                            'InitTeams(hometeamValue).ByeWeek = WeekNum Or InitTeams(awayteamValue).ByeWeek = WeekNum

                            'rechooses game if team has played 3 games in a row home or away, team is on a bye this week, has already been chosen for a game, just played the same team the previosu week, 
                            'or if all other teams cannot play a game

                            If ConAGames2(team) = MaxConGames Or InitTeams(team).AwayGames.Count = 0 Then 'must choose a home game
                                PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)

                                While ConAGames2(InitTeams(team).HomeGames.Item(PickGame)) = MaxConGames 'if opponent has played more than 3 consecutive away games have to repick
                                    PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)
                                End While
                                hometeamValue = team
                                awayteamValue = InitTeams(team).HomeGames.Item(PickGame)

                            ElseIf ConHGames2(team) = MaxConGames Or InitTeams(team).HomeGames.Count = 0 Then 'must choose an away game
                                PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)

                                While ConHGames2(InitTeams(team).AwayGames.Item(PickGame)) = MaxConGames 'if opponent has played more than 3 consecutive away games have to repick
                                    PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)
                                End While

                                awayteamValue = team
                                hometeamValue = InitTeams(team).AwayGames.Item(PickGame)
                            Else 'doesn't matter

                                IsHomeTeam = MT.GenerateInt32(0, 1)
                                Select Case IsHomeTeam
                                    Case 0  'randomly choose home game
                                        PickGame = MT.GenerateInt32(0, InitTeams(team).HomeGames.Count - 1)
                                        hometeamValue = team
                                        awayteamValue = InitTeams(team).HomeGames.Item(PickGame)
                                    Case 1 'randomly chose away game
                                        PickGame = MT.GenerateInt32(0, InitTeams(team).AwayGames.Count - 1)
                                        awayteamValue = team
                                        hometeamValue = InitTeams(team).AwayGames.Item(PickGame)
                                End Select
                            End If

                            GameNum = (16 - (NumByeTeamsNextWeek / 2))

                            Gamecheck = CanNextWeekBePlayed(WeekNum, GameNum, hometeamValue, awayteamValue)
                            count += 1

                            If GameH2.Count = (16 - NumByeTeamsNextWeek / 2) Then 'full games are scheduled
                                count = 0
                                Exit While
                            End If

                            If count >= 2500 Then  'restart entire week, too many attempts

                                For i As Integer = 1 To 32
                                    For game As Integer = 1 To GameH2.Count
                                        If i = GameH2.Item(game) Then
                                            InitTeams(i).HomeGames.Add(GameA2.Item(game)) 'puts the games back in the list
                                        ElseIf i = GameA2.Item(game) Then
                                            InitTeams(i).AwayGames.Add(GameH2.Item(game))
                                        End If
                                        If InitTeams(i).ByeWeek = WeekNum + 1 Then
                                            AlreadyPlayed2(i) = 1
                                        Else
                                            AlreadyPlayed2(i) = 0
                                        End If
                                    Next game
                                    For game As Integer = 1 To GameH.Count
                                        If i = GameH.Item(game) Then
                                            InitTeams(i).HomeGames.Add(GameA.Item(game)) 'puts the games back in the list
                                        ElseIf i = GameA.Item(game) Then
                                            InitTeams(i).AwayGames.Add(GameH.Item(game))
                                        End If
                                        If InitTeams(i).ByeWeek = WeekNum Then
                                            AlreadyPlayed(i) = 1
                                        Else
                                            AlreadyPlayed(i) = 0
                                        End If
                                    Next game
                                Next i
                                GameH2.Clear()
                                GameA2.Clear()
                                GameH.Clear()
                                GameA.Clear()
                                games = 0 'resets the count to 1
                                NumRestarts += 1
                                count = 0
                                Exit While
                            End If
                        End While

                        If NumRestarts = 500 Then 'attempt to redo only the last 2 weeks of the schedule to see if we can get it to work

                            DoOnce2 = False 're-initializes the home and away lists for each team

                            GameH.Clear() 'resets all variables, collections and lists
                            GameA.Clear()
                            GameH2.Clear()
                            GameA2.Clear()
                            games = 0
                            count = 0
                            NumRestarts = 0
                            For i As Integer = 1 To 32 'resets all caount variables
                                ConAGames(i) = 0
                                ConHGames(i) = 0
                                ConHGames2(i) = 0
                                ConAGames2(i) = 0
                                AlreadyPlayed(i) = 0
                                AlreadyPlayed2(i) = 0
                            Next i
                            GetSchedule(16)
                        End If
                        If count < 1500 And NumRestarts < 500 And GameH2.Count < (16 - NumByeTeamsNextWeek / 2) Then
                            GameH2.Add(hometeamValue)
                            GameA2.Add(awayteamValue)
                            InitTeams(hometeamValue).HomeGames.Remove(awayteamValue)
                            InitTeams(awayteamValue).AwayGames.Remove(hometeamValue)
                            count = 0 'resets the count for the next game
                            games += 1 'increments the game count
                            AlreadyPlayed2(hometeamValue) = 1
                            AlreadyPlayed2(awayteamValue) = 1 'Games can be played
                            'NumRestarts = 0
                        End If

                    End If
                Next team
            End If
            If GameH2.Count = (16 - NumByeTeamsNextWeek / 2) Then 'all teams have a game to play the following week
                count = 0
                For i As Integer = 1 To 32
                    For game As Integer = 1 To GameH2.Count
                        If i = GameH2.Item(game) Then
                            InitTeams(i).HomeGames.Add(GameA2.Item(game)) 'puts the games back in the list as these games have not actually been scheduled...
                        ElseIf i = GameA2.Item(game) Then
                            InitTeams(i).AwayGames.Add(GameH2.Item(game))
                        End If
                        AlreadyPlayed2(i) = 0
                    Next game
                Next i
                GameH2.Clear() 'clears the games list
                GameA2.Clear()
                Exit While
            End If
        End While
    End Sub

    Private Sub CreateWeekSchedule(ByVal WeekNum As Integer, ByVal NumByeTeams As Integer)
        'runs through the weekly schedule

        'Dim count As Integer
        'Dim Team As Integer = 1
        'HadToExitSub = False

        'count = 1
        'For games As Integer = 1 To (16 - (NumByeTeams / 2))
        'schedules the games for the week

        'Dim PickGameNum As Integer
        'PickGameNum = MT.GenerateInt32(1, HomeTeam.Count) 'picks a random game to schedule

        'If WeekNum > 3 Then 'a team cannot play 3 games before week 3
        'While ConHGames(HomeTeam.Item(PickGameNum)) = 3 Or ConAGames(AwayTeam.Item(PickGameNum)) = 3
        ''(HomeTeam.Item(PickGameNum) = ScheduleA(WeekNum - 1, HomeTeam.Item(PickGameNum)) And AwayTeam.Item(PickGameNum) = ScheduleH(WeekNum - 1, AwayTeam.Item(PickGameNum)))
        'PickGameNum = MT.GenerateInt32(1, HomeTeam.Count)
        'End While
        'End If

        'For i As Integer = 1 To 32
        'If InitTeams(i).ByeWeek = WeekNum And (InitTeams(i).TeamId = HomeTeam.Item(PickGameNum) Or
        'InitTeams(i).TeamId = AwayTeam.Item(PickGameNum)) Then 'if a team is on a bye week and it is chosen for a game---rechoose a game.
        'PickGameNum = MT.GenerateInt32(1, HomeTeam.Count)
        'i = 0
        'End If
        'Next i

        'If GameH.Count > 0 Then 'runs if there has been a game scheduled already this week
        'CanGameBePlayed(WeekNum, games) 'checks to see if every team has at least 1 game that can be played

        'If RedoGame = True Then 'redogame is a boolean flag meaning it restarted the weekly schedule
        'If StartedOver > 30 Then
        'games = 1
        'RedoGame = False
        'StartedOver = 0
        'Else
        'games -= 1
        'RedoGame = False
        'End If
        'End If
        'If games <= (16 - (NumByeTeams / 2)) And WeekNum > 1 Then
        'count = 1
        'For teamid As Integer = 1 To GameH.Count
        'ensures only a game that actually can be played is chosen
        'Do While HomeTeam.Item(PickGameNum) = GameH.Item(teamid) Or HomeTeam.Item(PickGameNum) =
        'GameA.Item(teamid) Or AwayTeam.Item(PickGameNum) = GameH.Item(teamid) Or
        'AwayTeam.Item(PickGameNum) = GameA.Item(teamid) Or InitTeams(HomeTeam.Item(PickGameNum)).ByeWeek = WeekNum Or
        'InitTeams(AwayTeam.Item(PickGameNum)).ByeWeek = WeekNum Or ConHGames(HomeTeam.Item(PickGameNum)) = 3 Or ConAGames(AwayTeam.Item(PickGameNum)) = 3
        '(HomeTeam.Item(PickGameNum) = ScheduleA(WeekNum - 1, HomeTeam.Item(PickGameNum)) And AwayTeam.Item(PickGameNum) = ScheduleH(WeekNum - 1, AwayTeam.Item(PickGameNum)))

        'PickGameNum = MT.GenerateInt32(1, HomeTeam.Count)
        'teamid = 1
        'count += 1
        'If count > 500 Then 'puts the games back in the scheduler
        'For i As Integer = 1 To GameH.Count
        'HomeTeam.Add(GameH.Item(i))
        'AwayTeam.Add(GameA.Item(i))
        'Next i
        'GameH.Clear()
        'GameA.Clear()
        'Exit Sub
        'End If
        'Loop
        'Next teamid

        'End If
        'End If
        ' If count > 500 Then
        'count = 0
        'Team = 1
        'Else
        'GameH.Add(HomeTeam.Item(PickGameNum))
        'GameA.Add(AwayTeam.Item(PickGameNum))
        'HomeTeam.Remove(PickGameNum)
        'AwayTeam.Remove(PickGameNum)
        'End If
        'Next games

    End Sub 'unused function
    '''<summary>creates a schedule where the last 2 games in the season are divisional games. 
    '''currently in the NFL the last 2 games each teams play every year are games within
    '''the division. Takes the 4 Div teams from ScheduleDivGames and then schedules 2 games checking 
    '''to make sure they both can be played and are not both the same game</summary>
    Private Sub EndWithDivisionGames(ByVal Team1 As Integer, ByVal Team2 As Integer, ByVal Team3 As Integer, ByVal Team4 As Integer, ByVal NumTeams As Integer)

        Dim MyTeams As New List(Of Integer)
        MyTeams.Clear()
        Myteams.Add(Team1)
        MyTeams.Add(Team2)
        MyTeams.Add(Team3)
        MyTeams.Add(Team4)

        Dim PickHome As Integer
        Dim PickHome2 As Integer
        Dim PickAway As Integer
        Dim PickAway2 As Integer

        PickHome = MyTeams.Item(MT.GenerateInt32(0, MyTeams.Count - 1)) 'chooses 1st home team
        MyTeams.Remove(PickHome)
        PickHome2 = MyTeams.Item(MT.GenerateInt32(0, MyTeams.Count - 1)) 'chooses 2nd home team, only 3 teams are left in the list
        While PickHome = PickHome2 'if these teams are the same team, then it re-chooses
            PickHome2 = MyTeams.Item(MT.GenerateInt32(0, MyTeams.Count - 1))
        End While
        MyTeams.Remove(PickHome2)

        Select Case MT.GenerateInt32(0, MyTeams.Count - 1) 'randomly decides which will be away team 1 and away team 2
            Case 0
                PickAway = MyTeams(1)
                MyTeams.Remove(PickAway)
                PickAway2 = MyTeams(0) 'must be the last remaining away team
            Case 1
                PickAway2 = MyTeams(1)
                MyTeams.Remove(PickAway2)
                PickAway = MyTeams(0)
                MyTeams.Remove(0)
        End Select

        ScheduleH(16, (Team4 / 2) - 1) = PickHome 'game 1
        ScheduleA(16, (Team4 / 2) - 1) = PickAway
        ScheduleH(16, (Team4 / 2)) = PickHome2 'game 2
        ScheduleA(16, (Team4 / 2)) = PickAway2

        'PickHome and Pickaway cannot play each other again the following week, PickHome and PickHome2 cannot play each other since they both need to be away teams the following week, 
        'And a team cannot play itself, so that leaves only one option---PickHome must play at PickAway2 and PickHome2 must play at PickAway.

        ScheduleH(17, (Team4 / 2) - 1) = PickAway2 'game 1
        ScheduleA(17, (Team4 / 2) - 1) = PickHome
        ScheduleH(17, (Team4 / 2)) = PickAway 'game 2
        ScheduleA(17, (Team4 / 2)) = PickHome2

        Console.WriteLine(PickAway & " at " & PickHome)
        Console.WriteLine(PickAway2 & " at " & PickHome2)
        Console.WriteLine(PickHome & " at " & PickAway2)
        Console.WriteLine(PickHome2 & " at " & PickAway)

        Dim i As Integer = 1
        Dim count As Integer = 0
        Dim Hlist As New List(Of Integer)
        Dim Alist As New List(Of Integer)

        While count < 4
            For i = 1 To HomeTeam.Count 'removes games that have been scheduled already
                If (HomeTeam(i) = PickHome And AwayTeam(i) = PickAway) Or (HomeTeam(i) = PickHome2 And AwayTeam(i) = PickAway2) Or (HomeTeam(i) = PickAway2 And AwayTeam(i) = PickHome) Or (HomeTeam(i) = PickAway And AwayTeam(i) = PickHome2) Then
                    Hlist.Add(i) 'adds the game number to remove from HomeTeam/AwayTeam collection
                    Alist.Add(i)
                    Console.WriteLine(AwayTeam(i) & " at " & HomeTeam(i))
                    count += 1
                End If
            Next i
        End While

        count = 0
        While Hlist.Count > 0
            Console.WriteLine(AwayTeam(Hlist(0) - count) & " at " & HomeTeam(Hlist(0) - count))
            HomeTeam.Remove(Hlist(0) - count)
            AwayTeam.Remove(Alist(0) - count)
            Hlist.RemoveAt(0)
            Alist.RemoveAt(0)
            count += 1
        End While


    End Sub
    ''' <summary>
    ''' There are 12 possible bye week configurations, which range from 6 bye weeks to 9 bye weeks.  This will randomly select a bye week configuration to use for the schedule
    ''' </summary>
    Private Sub GetByeWeek()

        Dim NumTwoTeams As Integer 'number of times 2 teams have a bye in a given week for each configuration
        Dim NumFourTeams As Integer 'number of times 4 teams have a bye in a given week for each configuration
        Dim NumSixTeams As Integer 'number of times 6 teams have a bye in a given week for each configuration
        Dim FirstByeWeek As Integer 'the first week of the schedule that has byes in it
        Dim LastByeWeek As Integer 'the last week of the schedule that has byes in it

        Dim ByeConfig As Integer = MT.GenerateInt32(1, 12) 'randomly selects one of 12 bye week configurations to use

        Select Case ByeConfig
            Case 1
                NumTwoTeams = 0
                NumFourTeams = 2
                NumSixTeams = 4

            Case 2
                NumTwoTeams = 0
                NumFourTeams = 8
                NumSixTeams = 0

            Case 3
                NumTwoTeams = 0
                NumFourTeams = 5
                NumSixTeams = 2

            Case 4
                NumTwoTeams = 1
                NumFourTeams = 6

            Case 5
                NumTwoTeams = 1
                NumFourTeams = 3
                NumSixTeams = 3

            Case 6
                NumTwoTeams = 2
                NumFourTeams = 4
                NumSixTeams = 2

            Case 7
                NumTwoTeams = 2
                NumFourTeams = 1
                NumSixTeams = 4

            Case 8
                NumTwoTeams = 3
                NumFourTeams = 2
                NumSixTeams = 3

            Case 9
                NumTwoTeams = 3
                NumFourTeams = 5
                NumSixTeams = 1
            Case 10
                NumTwoTeams = 4
                NumFourTeams = 3
                NumSixTeams = 2
            Case 11
                NumTwoTeams = 4
                NumFourTeams = 0
                NumSixTeams = 4
            Case 12
                NumTwoTeams = 5
                NumFourTeams = 1
                NumSixTeams = 3
        End Select

        Select Case (NumTwoTeams + NumFourTeams + NumSixTeams) 'sets first and last bye weeks depending on how many total weeks of byes there will be
            Case 6
                FirstByeWeek = 5
                LastByeWeek = 10
            Case 7
                FirstByeWeek = 4
                LastByeWeek = 10
            Case 8
                FirstByeWeek = 4
                LastByeWeek = 11
            Case 9
                FirstByeWeek = 4
                LastByeWeek = 12
        End Select

        Dim ByeWeek As New Collection
        Dim AlreadyHadBye As Boolean 'True if team already had bye

        For week As Integer = FirstByeWeek To LastByeWeek 'starting and ending byweeks

            Dim RndByeTeams As Integer = MT.GenerateInt32(1, 3) 'chooses one of the bye week types
            Dim Check As Boolean = False

            While Check = False 'Check to make sure there are available weeks to run the byes
                While RndByeTeams = 1 And NumTwoTeams = 0 'can't have 2 teams on a bye because
                    RndByeTeams = MT.GenerateInt32(2, 3)
                End While
                While RndByeTeams = 2 And NumFourTeams = 0
                    RndByeTeams = MT.GenerateInt32(1, 3)
                End While
                While RndByeTeams = 3 And NumSixTeams = 0
                    RndByeTeams = MT.GenerateInt32(1, 2)
                End While
                If (RndByeTeams = 1 And NumTwoTeams <> 0) Or (RndByeTeams = 2 And NumFourTeams <> 0) Or (RndByeTeams = 3 And NumSixTeams <> 0) Then 'can use that type to schedule bye weeks
                    Check = True
                End If
            End While

            Dim count As Integer = 0
            Dim NumTeams As Integer = 0 'Number of teams on a bye this week

            If RndByeTeams = 3 Then 'Six teams are on bye this week
                NumTeams = 6
                NumSixTeams -= 1
            ElseIf RndByeTeams = 2 Then 'Four teams are on a bye this week
                NumTeams = 4
                NumFourTeams -= 1
            ElseIf RndByeTeams = 1 Then 'Two teams are on a bye this week
                NumTeams = 2
                NumTwoTeams -= 1
            End If

            Do Until count = NumTeams
                Dim Team As Integer = MT.GenerateInt32(1, 32)
                If ByeWeek.Count > 0 Then
                    For tmcheck As Integer = 1 To ByeWeek.Count
                        If ByeWeek.Item(tmcheck) = Team Then
                            AlreadyHadBye = True
                            Exit For
                        End If
                    Next tmcheck
                    If AlreadyHadBye = False Then
                        InitTeams(Team).ByeWeek = week
                        ByeWeek.Add(Team)
                        count += 1
                    End If
                Else
                    If AlreadyHadBye = False Then
                        InitTeams(Team).ByeWeek = week
                        ByeWeek.Add(Team)
                        count += 1
                    End If
                End If
                AlreadyHadBye = False
            Loop

        Next week
    End Sub
    ''' <summary>
    ''' Checks to see if every other team has a game that can be played this week if this game gets scheduled. Returns True if all other teams can play a game and false if they cannot
    ''' </summary>
    ''' <param name="weeknum"></param>
    ''' <param name="Games"></param>
    ''' <param name="Home"></param>
    ''' <param name="Away"></param>
    ''' <returns></returns>
    Private Function CanGameBePlayed(ByVal weeknum As Integer, ByVal Games As Integer, ByVal Home As Integer, ByVal Away As Integer) As Boolean
        'rewrote  76 lines into 25
        Dim team As Integer = 1
        Dim TeamsRemaining As New List(Of Integer)
        Dim CheckTeam As Integer = 0

        While team <> 32 'loops until team = 32
            TeamsRemaining.Clear()

            If team <> Home And team <> Away And AlreadyPlayed(team) <> 1 Then 'checks all other teams that still need to play games

                For i As Integer = 0 To InitTeams(team).HomeGames.Count - 1 'cycle through all remaining games
                    If InitTeams(team).HomeGames.Item(i) <> Away Then
                        TeamsRemaining.Add(InitTeams(team).HomeGames.Item(i))
                    End If 'adds the list of opponents this team has left
                Next i
                For i As Integer = 0 To InitTeams(team).AwayGames.Count - 1
                    If InitTeams(team).AwayGames.Item(i) <> Home Then
                        TeamsRemaining.Add(InitTeams(team).AwayGames.Item(i))
                    End If
                Next i
            End If

            If AlreadyPlayed(team) <> 1 And team <> Home And team <> Away Then 'no need to check games for teams that have already played
                For Runcheck As Integer = 1 To GameH.Count 'cycle thru games already scheduled for the week
                    CheckTeam = 0

                    Do While CheckTeam <= TeamsRemaining.Count - 1 'cycle through the list and find out if teams on the list have already been scheduled. If they have remove them as they cannot play.
                        If TeamsRemaining.Contains(GameH.Item(Runcheck)) Then
                            TeamsRemaining.Remove(GameH.Item(Runcheck))
                            CheckTeam = 0 'resets variable to start at beginning of the list 
                        End If
                        If TeamsRemaining.Contains(GameA.Item(Runcheck)) Then
                            TeamsRemaining.Remove(GameA.Item(Runcheck))
                            CheckTeam = 0 'resets variable to start at beginning of the list 
                        End If
                        CheckTeam += 1
                    Loop

                    Select Case weeknum
                        Case 11, 12, 13, 14
                            If TeamsRemaining.Count = 0 And GameH.Count > 0 Then 'no games available for this team to play---restart the schedule for the week.
                                Return False
                            End If
                        Case 6, 7, 8, 9, 10
                            If TeamsRemaining.Count < 2 And GameH.Count > 0 Then 'not enough games available for this team to play---restart the schedule for the week.
                                Return False
                            End If
                        Case Else
                            If TeamsRemaining.Count < 3 And GameH.Count > 0 Then 'not enough games available for this team to play---restart the schedule for the week.
                                Return False
                            End If
                    End Select

                    'TeamsRemaining.Clear() 'clear the list for the next team to check
                    CheckTeam = 0
                Next Runcheck
            End If
            team += 1
        End While

        Return True 'All teams must have games that can be scheduled for this week, return true


        'Dim tteam As Integer
        'team = 1
        'Do Until team = 33
        'For Gamecheck As Integer = 1 To GameH.Count
        'If team = GameH.Item(Gamecheck) Or team = GameA.Item(Gamecheck) Then
        'BoolCheck = True
        'Exit For
        'End If
        'Next Gamecheck

        'If InitTeams(team).ByeWeek = weeknum Then
        'BoolCheck = True
        'End If

        'If BoolCheck = False Then

        'For i As Integer = 1 To HomeTeam.Count 'cycle thru all scheduled games

        'If HomeTeam.Item(i) = team Then
        'If HomeTeam.Item(i) <> team And InitTeams(team).ByeWeek <> weeknum Then
        'TeamsLeftToPlay.Add(HomeTeam.Item(i))
        'ElseIf AwayTeam.Item(i) <> team And InitTeams(team).ByeWeek <> weeknum Then
        'TeamsLeftToPlay.Add(AwayTeam.Item(i))
        'End If
        'End If

        'If AwayTeam.Item(i) = team Then
        'If HomeTeam.Item(i) <> team And InitTeams(team).ByeWeek <> weeknum Then
        'TeamsLeftToPlay.Add(HomeTeam.Item(i))
        'ElseIf AwayTeam.Item(i) <> team And InitTeams(team).ByeWeek <> weeknum Then
        'TeamsLeftToPlay.Add(AwayTeam.Item(i))
        'End If
        'End If
        'Next i
        '
        'For Runcheck As Integer = 1 To GameH.Count 'cycle thru games scheduled for the week

        'Dim CheckTeam As Integer = 1

        'Do While CheckTeam <= TeamsLeftToPlay.Count

        'If GameH.Item(Runcheck) = TeamsLeftToPlay.Item(CheckTeam) _
        'Or GameA.Item(Runcheck) = TeamsLeftToPlay.Item(CheckTeam) Then
        'TeamsLeftToPlay.Remove(CheckTeam)
        'CheckTeam = 0
        'End If
        'CheckTeam += 1
        'Loop

        'Next Runcheck

        'If TeamsLeftToPlay.Count < 1 And GameH.Count > 0 Then 'no teams left to play for this team,
        'must restart the scheduler for the week.
        'StartedOver += 1
        'increments a counter to redo the weekly schedule from the beginning, as this attempted schedule is not working for whatever reason
        'If StartedOver > 30 Then
        'For i As Integer = 1 To GameH.Count
        'HomeTeam.Add(GameH.Item(i))
        'AwayTeam.Add(GameA.Item(i))
        'Next i
        'GameH.Clear()
        'GameA.Clear()
        'Else
        'HomeTeam.Add(GameH.Item(GameH.Count))
        'AwayTeam.Add(GameA.Item(GameH.Count))
        'GameH.Remove(GameH.Count)
        'GameA.Remove(GameA.Count)
        'End If
        'RedoGame = True 'flags boolean
        'TeamsLeftToPlay.Clear()
        'BoolCheck = False
        'Exit Do
        'End If
        'End If
        'If team = 32 Then
        'Exit Do
        'End If
        'team += 1
        'TeamsLeftToPlay.Clear()
        'BoolCheck = False
        'Loop
    End Function
    ''' <summary>
    ''' After each weeks games are scheduled, this Sub checks to make sure each team has a game that can be played the following week. If not, the current week schedule is redone
    ''' </summary>
    ''' <param name="weeknum"></param>
    ''' <param name="Games"></param>
    Private Function CanNextWeekBePlayed(ByVal weeknum As Integer, ByVal Games As Integer, ByVal Hometeam As Integer, ByVal AwayTeam As Integer) As Boolean

        Dim team As Integer = 1
        Dim TeamsRemaining As New List(Of Integer)
        Dim CheckTeam As Integer = 0
        'Dim ScheduleCopy(32) As Teams
        Dim HomeTeamValue As Integer 'hometeam
        Dim AwayTeamValue As Integer 'awayteam
        'Dim AlreadyPlayed2(32) As Integer '0 if hasn't played, 1 if played

        HomeTeamValue = Hometeam
        AwayTeamValue = AwayTeam

        If weeknum < 15 Then 'last 2 weeks of the schedule are already there as they are divisional games

            While team <> 32 'runs through every team
                TeamsRemaining.Clear()

                If team <> HomeTeamValue And team <> AwayTeamValue And AlreadyPlayed2(team) <> 1 Then 'checks all other teams that still need to play games

                    For i As Integer = 0 To InitTeams(team).HomeGames.Count - 1 'cycle through all remaining games
                        If InitTeams(team).HomeGames.Item(i) <> AwayTeamValue Then
                            TeamsRemaining.Add(InitTeams(team).HomeGames.Item(i))
                        End If 'adds the list of opponents this team has left
                    Next i
                    For i As Integer = 0 To InitTeams(team).AwayGames.Count - 1
                        If InitTeams(team).AwayGames.Item(i) <> HomeTeamValue Then
                            TeamsRemaining.Add(InitTeams(team).AwayGames.Item(i))
                        End If
                    Next i
                End If

                If AlreadyPlayed2(team) <> 1 And team <> HomeTeamValue And team <> AwayTeamValue Then 'no need to check games for teams that have already played
                    For Runcheck As Integer = 1 To GameH2.Count 'cycle thru games already scheduled for the week
                        CheckTeam = 0

                        Do While CheckTeam <= TeamsRemaining.Count - 1 'cycle through the list and find out if teams on the list have already been scheduled. If they have remove them as they cannot play.
                            If TeamsRemaining.Contains(GameH2.Item(Runcheck)) Then
                                TeamsRemaining.Remove(GameH2.Item(Runcheck))
                                CheckTeam = 0 'resets variable to start at beginning of the list 
                            End If
                            If TeamsRemaining.Contains(GameA2.Item(Runcheck)) Then
                                TeamsRemaining.Remove(GameA2.Item(Runcheck))
                                CheckTeam = 0 'resets variable to start at beginning of the list 
                            End If
                            CheckTeam += 1
                        Loop

                        Select Case weeknum
                            Case 11, 12, 13, 14
                                If TeamsRemaining.Count = 0 And GameH.Count > 0 Then 'no games available for this team to play---restart the schedule for the week.
                                    Return False
                                End If
                            Case 8, 9, 10
                                If TeamsRemaining.Count < 2 And GameH.Count > 0 Then 'not enough games available for this team to play---restart the schedule for the week.
                                    Return False
                                End If
                            Case Else
                                If TeamsRemaining.Count < 3 And GameH.Count > 0 Then 'not enough games available for this team to play---restart the schedule for the week.
                                    Return False
                                End If
                        End Select

                        'TeamsRemaining.Clear() 'clear the list for the next team to check
                        CheckTeam = 0
                    Next Runcheck
                End If
                team += 1
            End While

            Return True 'All teams must have games that can be scheduled for next week
        End If

    End Function


End Class
