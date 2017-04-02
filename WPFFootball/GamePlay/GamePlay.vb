Imports System.Data
Imports GlobalResources
Imports System.Linq
Imports GoalLineStandFootball.GamePlayEvents
Imports GoalLineStandFootball.GamePlayStats
Imports System.Reflection
Public Enum PlayTypeEnum
    PassBehindLOS
    PassShort
    PassMed
    PassLong
    RunInside
    RunOutside
    Sack
    Interception
    KickoffRet
    Safety
    FakePunt
    Punt
    PuntBlock
    PuntReturn
    PuntBlockRet
    Touchdown
    FakeFG
    FGMissed
    FGMade
    FGBlock 'Kick is Blocked
    FGBlockRet
    FumQBExchange 'QB Fumbles the ball when snapped
    FumQBSacked 'QB Gets Sacked and Fumbles
    FumQBRun 'QB Gets Hit while running and fumbles
    FumQBHandoff 'QB Fumbles while handing it off to RB/WR/FB/TE in the backfield
    FumRunPlay 'Fumbles on running play
    FumReception 'Fumbles after Pass Completion
    FumKR  'Fumbles on Kickoff Return once he has secured ball
    FumPR  'Fumbles on Punt Return once he has secured the ball
    MuffKR 'Ball is dropped when trying to catch Kickoff
    MuffPR 'Ball is dropped when trying to catch Punt
    Lateral
End Enum
Public Enum PassTypeEnum
    PBehindLOSFarL
    PBehindLOSLMid
    PBehindLOSMid
    PBehindLOSRMid
    PBehindLOSFarR
    PShortFarL
    PShortLMid
    PShortMid
    PShortRMid
    PShortFarR
    PMedFarL
    PMedLMid
    PMedMid
    PMedRMid
    PMedFarR
    PLongFarL
    PLongLMid
    PLongMid
    PLongRMid
    PLongFarR
End Enum
Public Enum RunTypeEnum
    LeftEnd
    LeftTackle
    Middle
    RightTackle
    RightEnd
End Enum
Public Enum ScoringTypeEnum
    RushingTD
    PassingTD
    OffFumRecTD
    DefFumRecTD
    IntReturnTD
    PuntRetTD
    KORetTD
    FG
    Safety
    XP
    TwoPointConv
    DefXPReturnFor2Pts
    FreeKickFG
End Enum
''' <summary>
''' These are personnel groupings taken from team playbooks---note the numbers afterwards---these correspond to the 5 "skill positions".  The first number is how many RB's are in the game.  The second number is how many TE's are in.
''' Add those up and subtract from 5 and that is how many WR's are in the game.
''' </summary>
Public Enum PersGroupingOff
    Regular21 '2RB 1TE 2WR
    Ace12 '1RB 2TE 2WR
    Posse11 '1RB 1TE 3WR
    Jet10 '1RB 0TE 4WR
    Houston20 '2RB 0TE 3WR
    Kings01 '0RB 1TE 4WR
    Tank22 '2RB 2TE 1WR
    Heavy13 '1RB 3TE 1WR
    Jumbo23 '2RB 3TE 0WR
    Wildcat31 '3RB 1TE 1WR
    Joker02 '0RB 2TE 3WR
    Royal00 '0RB 0TE 5WR
    Club32 '3RB 2TE 0WR
End Enum
''' <summary>
''' This lists the personnel groupings a defense could have on the field.  First number is DL, second number is LB, subtract the addition of these 2 from 11 and that's how many DB's are on the field.
''' </summary>
Public Enum PersGroupingDef
    Base43 '4DL 3LB 4DB
    Base34 '3DL 4LB 4DB
    GoalLine52 '5DL 2LB 4DB
    GoalLine44 '4DL 4LB 3DB
    GoalLine53 '5DL 3LB 3DB
    Nickel42 '4DL 2LB 5DB
    Nickel33 '3DL 3LB 5DB
    Nickel24 '2DL 4LB 5DB
    Dime23 '2DL 3LB 6DB
    Dime41 '4DL 1LB 6DB
    Dime32 '3DL 2LB 6DB
    Dime14 '1DL 4LB 6DB
    Quarter31 '3DL 1LB 7DB
    Quarter22 '2DL 2LB 7DB
    Quarter13 '1DL 3LB 7DB
End Enum
''' <summary>
''' These are potential fronts for the Base43 defense
''' https://theinsidezone.com/football-101-4-3-defense#comment-1125
''' http://bleacherreport.com/articles/1999358-nfl-101-the-basics-of-the-4-3-defensive-front
''' http://www.bloggingtheboys.com/2013/1/26/3917768/dallas-cowboys-inside-monte-kiffins-playbook-4-3-over-under-and-over-stem-fronts
''' </summary>
Public Enum DefFront43
    Over  'DT's: 1-TEch aligned to the strong side of the play and 3-Tech aligned to the weak side. 3 LB's align in a "tan-Zero-Tan" alignment having Tackle-Zero-Technique-Tackle resposnbility versus the run
    Under 'DT's: 3-TEch aligned to weak side of the play and 1-Tech aligned towards the strong side.  3 LB's have a single gap for run responsibility
    Wide9 'DT's: 3-Tech aligned to the strong side of the play and 2i technique towards the weak side. Both D's align in the 9-tech. 3 LB's in the "Tan-Zero-Tan" alignment
    OverStem 'DT's: 3-Tech aligned to strong side of the play, 1-TEch aligned towards the weak side.  Closed DE(strong-side) in the 9-TEch, OpenDE in the 5-Tech. Sam-Stack B, Will Tan, Mike Strong Zero
End Enum

''' <summary>
''' These are potential fronts for the Base34 defense
''' http://bleacherreport.com/articles/2007958-nfl-101-the-basics-of-the-3-4-defensive-front
''' Sam LB is responsible for contain in all schemes with the Will LB responsible for staying at howm vs. cutback/bootlegs
''' </summary>
Public Enum DefFront34
    Eagle '1 Gap scheme for the 3 downlinemen with the SAm backer playing a 2 Gap scheme on the outside. 2 DE's align in a 4i technique, Nose Tackle in a 0, Outside LB's in a 6 technique.  Jack/Mike LB's play A Gap/Scrape technique
    Under '1 Gap scheme with the LDE playing a "2 Gap" scheme.  LD 2/3 TEch, NT Shade Tech, RDE 5/6 Tech, Will LB 6 Tech, Sam LB 7 tech, Jack LB ill/Scrape tech and Mike LB plays B Gap
    Okie '2 Gap scheme---all 3 down linemen line up directly over their man and play both gaps to either side of him. 2 D's 5 Tech, NT in 0 Tech, Will LB in a 6 Tech, Sam LB in a 7 tech, Jack/Mike LB's in 2 Gap vs. Base Runs scheme
End Enum

''' <summary>
''' This is the class where game play takes place
''' </summary>
Public Class GamePlay
    Public GameLoop As Boolean = True 'This is what controls whether the game is still going on or it has ended
    Public Shared PassType As New PassTypeEnum 'Enumeration for the different types of passes
    Public Shared ScoringType As New ScoringTypeEnum 'Determines what type of score it is
    Public Shared PlayType As New PlayTypeEnum
    Public Shared OffPersonnel As New PersGroupingOff
    Public Shared DefPersonnel As New PersGroupingDef
    Public Shared MyRand As New Mersenne.MersenneTwister
    'Public GameEvents As New GamePlayEvents
#Region "Time Variables"
    Public Shared Property ClockStopped As Boolean
    Public Shared Property Pace As New TimeSpan(0, 0, 0)
    Public Shared Property GameTime As New TimeSpan(0, 15, 0) 'Sets the clock to 15 minutes(0 hours, 15 minutes, 0 seconds)
    Public Shared Property PlayTime As New TimeSpan(0, 0, 0) 'Length of time the play took to run
    Public Shared Property BallSpotTime As New TimeSpan(0, 0, 0)
    Public Shared Property TwoMinuteWarning As Boolean
#End Region

#Region "Passing Variables"
    Public Shared Property PBehLOSFarLComp As Single = 64.5
    Public Shared Property PBehLOSLMidComp As Single = 75.9
    Public Shared Property PBehLOSMidComp As Single = 51.3
    Public Shared Property PBehLOSRMidComp As Single = 74.7
    Public Shared Property PBehLOSFarRComp As Single = 64.5
    Public Shared Property PShortFarLComp As Single = 64.8
    Public Shared Property PShortLMidComp As Single = 67.4
    Public Shared Property PShortMidComp As Single = 70.3
    Public Shared Property PShortRMidComp As Single = 67.1
    Public Shared Property PShortFarRComp As Single = 67.6
    Public Shared Property PMedFarLComp As Single = 47
    Public Shared Property PMedLMidComp As Single = 56.7
    Public Shared Property PMedMidComp As Single = 60.9
    Public Shared Property PMedRMidComp As Single = 55
    Public Shared Property PMedFarRComp As Single = 46.9
    Public Shared Property PLongFarLComp As Single = 27.9
    Public Shared Property PLongLMidComp As Single = 36.8
    Public Shared Property PLongMidComp As Single = 38
    Public Shared Property PLongRMidComp As Single = 36.1
    Public Shared Property PLongFarRComp As Single = 30.6
    Public Shared Property IsComplete As Boolean
#End Region

#Region "Running Variables"
    Public Shared Property RunMid As Single
    Public Shared Property RunLeft As Single
    Public Shared Property RunLeftEnd As Single
    Public Shared Property RunRight As Single
    Public Shared Property RunRightEnd As Single
#End Region

#Region "Kicking Game"
    Public Shared Property Kickoff As Boolean = True
    Public Shared Property KickoffDist As Single
    Public Shared Property KickReturnYards As Single
    Public Shared Property PuntReturnYards As Single
    Public Shared Property CallFairCatch As Boolean
    Public Shared Property PuntDistance As Single
    Public Shared Property FGDistance As Single
    Public Shared Property ExpDecayFG As Single
    Public Shared Property Touchback As Boolean
#End Region

#Region "Basic Game Info"
    Public Shared Property Down As Integer
    Public Shared Property YardsToGo As Single
    Public Shared Property YardLine As Single = 35 'YardLine will be from 0(Your GoalLine) to 100(Opp GoalLine)
    Public Shared Property YardsGained As Single
    Public Shared Property Quarter As Integer = 1
    Public Shared Property HomePossession As Boolean 'Does Home Team Have the Ball?
    Public Shared Property HomeRec2ndHalfKickoff As Boolean
    Public Shared Property HalfStart As Boolean = True 'Is this the start of the half?
    Public Shared Property HomeScore As Integer
    Public Shared Property AwayScore As Integer
    Public Shared Property PointDiff As Integer 'How much the team is currently ahead or behind by
    Public Shared Property OutOfBounds As Boolean
    Public Shared Property HomeTotYards As Integer
    Public Shared Property HomeRushYards As Integer
    Public Shared Property HomePassYards As Integer
    Public Shared Property AwayTotYards As Integer
    Public Shared Property AWayPassYards As Integer
    Public Shared Property AwayRushYards As Integer
    Public Shared Property HomeTotPlays As Integer
    Public Shared Property AwayTotPlays As Integer
    Public Shared Property HomeRushPlays As Integer
    Public Shared Property AWayRushPlays As Integer
    Public Shared Property HomePassPlays As Integer
    Public Shared Property AwayPassPlays As Integer
    Public Shared Property HomeAvgPerRush As Single
    Public Shared Property HomeAvgPerPass As Single
    Public Shared Property AwayAvgPerRush As Single
    Public Shared Property AwayAvgPerPass As Single
    Public Shared Property HomeAvgPerPlay As Single
    Public Shared Property AwayAvgPerPlay As Single

#End Region

#Region "Turnovers"
    'Private Property Intercepted As Boolean
    Public Shared Property IntReturnYds As Single
    Public Shared Property IntReturnTD As Boolean
    Public Shared Property IsIntercepted As Boolean
    'Private Property Fumble As Boolean
    Public Shared Property DefFumRec As Boolean
    Public Shared Property FumbleRetYds As Single
    Public Shared Property FumbleRetTD As Boolean

#End Region

#Region "Teams"
    Public Shared HomeDT As New DataTable
    Public Shared AwayDT As New DataTable
    Public Shared Property HmTeamId As Integer
    Public Shared Property AwTeamId As Integer
#End Region
    Public Sub GenDT(ByVal homeTeamId As Integer, ByVal awayTeamId As Integer)
        HomeDT = PlayerDT.Select($"TeamId = {homeTeamId}").CopyToDataTable()
        AwayDT = PlayerDT.Select($"TeamId = {awayTeamId}").CopyToDataTable()

        Dim TempDT As DataTable = PlayerDT.DefaultView.ToTable(False, {"PlayerID", "Pos", "TeamID", "FName", "LName"})
        Stats = TempDT.Select($"TeamId = {homeTeamId} or TeamId = {awayTeamId}").CopyToDataTable()
        AddColumns() 'Adds columns to the Stats DT
        GenDepthChart(homeTeamId, awayTeamId)
        'StartGame(homeTeamId, awayTeamId)
    End Sub

    Private Sub GenDepthChart(homeTeamId As Integer, awayTeamId As Integer)
        Dim QBCount, RBCount, FBCount, WRCount, TECount, LTCount, LGCount, CCount, RGCount, RTCount, DECount, DTCount, OLBCount, ILBCount, FSCount, SSCount, CBCount As Integer

        Try
            For Each row As DataRow In HomeDT.Rows
                Select Case row.Item("Pos")
                    Case "QB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"QB{QBCount + 1}"
                        QBCount += 1
                    Case "RB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"RB{RBCount + 1}"
                        RBCount += 1
                    Case "FB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"FB{FBCount + 1}"
                        FBCount += 1
                    Case "WR" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"WR{WRCount + 1}"
                        WRCount += 1
                    Case "TE" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"TE{TECount + 1}"
                        TECount += 1
                    Case "OT" 'Split them between RT and LT
                        Dim myvar = MyRand.GenerateInt32(0, 1)
                        Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = If(myvar = 1, $"LT{LTCount + 1}", $"RT{RTCount + 1}")
                        If (myvar = 1) Then
                            LTCount += 1
                        Else RTCount += 1
                        End If
                    Case "OG"
                        Dim myvar = MyRand.GenerateInt32(0, 1)
                        Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = If(myvar = 1, $"LG{LGCount + 1}", $"RG{RGCount + 1}")
                        If (myvar = 1) Then
                            LGCount += 1
                        Else RGCount += 1
                        End If
                    Case "C" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"C{CCount + 1}"
                        CCount += 1
                    Case "DE" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"DE{DECount + 1}"
                        DECount += 1
                    Case "DT" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"DT{DTCount + 1}"
                        DTCount += 1
                    Case "OLB", "ROLB", "LOLB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"OLB{OLBCount + 1}"
                        OLBCount += 1
                    Case "ILB", "MLB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"ILB{ILBCount + 1}"
                        ILBCount += 1
                    Case "FS" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"FS{FSCount + 1}"
                        FSCount += 1
                    Case "SS" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"SS{SSCount + 1}"
                        SSCount += 1
                    Case "CB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"CB{CBCount + 1}"
                        CBCount += 1
                    Case "K" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = "K1"
                    Case "P" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = "P1"
                End Select
            Next row
            'Reset Variables to 0 for away team
            QBCount = 0
            RBCount = 0
            FBCount = 0
            WRCount = 0
            TECount = 0
            LTCount = 0
            LGCount = 0
            CCount = 0
            LTCount = 0
            RTCount = 0
            DECount = 0
            DTCount = 0
            OLBCount = 0
            ILBCount = 0
            FSCount = 0
            SSCount = 0
            CBCount = 0

            For Each row As DataRow In AwayDT.Rows
                Select Case row.Item("Pos")
                    Case "QB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"QB{QBCount + 1}"
                        QBCount += 1
                    Case "RB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"RB{RBCount + 1}"
                        RBCount += 1
                    Case "FB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"FB{FBCount + 1}"
                        FBCount += 1
                    Case "WR" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"WR{WRCount + 1}"
                        WRCount += 1
                    Case "TE" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"TE{TECount + 1}"
                        TECount += 1
                    Case "OT" 'Split them between RT and LT
                        Dim myvar = MyRand.GenerateInt32(0, 1)
                        Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = If(myvar = 1, $"LT{LTCount + 1}", $"RT{RTCount + 1}")
                        If (myvar = 1) Then
                            LTCount += 1
                        Else RTCount += 1
                        End If
                    Case "OG"
                        Dim myvar = MyRand.GenerateInt32(0, 1)
                        Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = If(myvar = 1, $"LG{LGCount + 1}", $"RG{RGCount + 1}")
                        If (myvar = 1) Then
                            LGCount += 1
                        Else RGCount += 1
                        End If
                    Case "C" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"C{CCount + 1}"
                        CCount += 1
                    Case "DE" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"DE{DECount + 1}"
                        DECount += 1
                    Case "DT" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"DT{DTCount + 1}"
                        DTCount += 1
                    Case "OLB", "ROLB", "LOLB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"OLB{OLBCount + 1}"
                        OLBCount += 1
                    Case "ILB", "MLB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"ILB{ILBCount + 1}"
                        ILBCount += 1
                    Case "FS" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"FS{FSCount + 1}"
                        FSCount += 1
                    Case "SS" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"SS{SSCount + 1}"
                        SSCount += 1
                    Case "CB" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = $"CB{CBCount + 1}"
                        CBCount += 1
                    Case "K" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = "K1"
                    Case "P" : Stats.Rows.Find(row.Item("PlayerId")).Item("DepthChart") = "P1"
                End Select
            Next row
        Catch ex As Exception
            Console.WriteLine(ex.Data)
        End Try

    End Sub

    ''' <summary>
    ''' Start the game and pass in the 2 teams who are playing----home and away
    ''' </summary>
    ''' <param name="homeTeamId"></param>
    ''' <param name="awayTeamId"></param>
    Public Sub StartGame(ByVal homeTeamId As Integer, ByVal awayTeamId As Integer)
        For i As Integer = 0 To 100
            MyRand.GenerateDouble(0, 100) 'Initializes the Mersenne Twister with randomness
        Next i
        GenDT(homeTeamId, awayTeamId)
        HomePossession = MyRand.GenerateInt32(0, 1) = 1 'Determines who is kicking off
        HomeRec2ndHalfKickoff = If(HomePossession, False, True) 'Sets the receiver of the 2nd half kickoff
        HmTeamId = homeTeamId
        AwTeamId = awayTeamId

        While GameLoop
            ' While the GameLoop is Set to True run the game.
            If Kickoff Then 'We are kicking off
                YardLine = 35
                KickoffEvt(HomePossession)
                Kickoff = False
            Else
                GetPace()
                'Two Minute Warning Check---before a play is ran
                If (Quarter = 2 Or Quarter = 4) And GameTime <= New TimeSpan(0, 2, 0) And Not TwoMinuteWarning Then
                    ClockStopped = True
                    TwoMinuteWarning = True 'Set the two minute warning so it doesn't continue to occur
                    GameTime = New TimeSpan(0, 2, 0) 'reset the clock to 2 minutes

                    'End Of Quarter Check
                ElseIf GameTime <= New TimeSpan(0, 0, 0) Then
                    ClockStopped = True
                    TwoMinuteWarning = False 'reset two minute warning for the next half
                    GameTime = New TimeSpan(0, 15, 0)
                    If Quarter = 2 Then
                        HomePossession = HomeRec2ndHalfKickoff 'Sets the team ready to receive ball in the 2nd half
                        Kickoff = True
                    ElseIf Quarter = 4 And (HomeScore <> AwayScore) Then
                        GameLoop = False 'End of game---no OT
                    End If
                    Quarter += 1

                Else
                    GetOffPersonnel()
                    RunPlay() 'If none of those things are true, then run a play
                    HomeTotYards = HomePassYards + HomeRushYards
                    AwayTotYards = AWayPassYards + AwayRushYards
                End If
                If PlayType = PlayTypeEnum.Touchdown Then 'If there is a Touchdown on the play

                ElseIf PlayType = PlayTypeEnum.Safety Then 'If There is a safety on the play

                ElseIf PlayType = PlayTypeEnum.FGMade Then 'If there is a FG made on the play
                End If
            End If
        End While
        HomeAvgPerPlay = Math.Round((HomeTotYards / HomeTotPlays), 1)
        AwayAvgPerPlay = Math.Round((AwayTotYards / AwayTotPlays), 1)
        HomeAvgPerRush = Math.Round((HomeRushYards / HomeRushPlays), 1)
        AwayAvgPerRush = Math.Round((AwayRushYards / AWayRushPlays), 1)
        HomeAvgPerPass = Math.Round((HomePassYards / HomePassPlays), 1)
        AwayAvgPerPass = Math.Round((AWayPassYards / AwayPassPlays), 1)
        HomeTotPlays = HomeRushPlays + HomePassPlays
        AwayTotPlays = AWayRushPlays + AwayPassPlays
        Console.WriteLine($"***END OF GAME*** FINAL SCORE: Home {HomeScore} Away {AwayScore}")
        Console.WriteLine($"HOME TOTAL YARDS: {HomeTotYards}//HOME TOTAL PLAYS: {HomeTotPlays}//HOME AVG PER PLAY: {HomeAvgPerPlay}")
        Console.WriteLine($"HOME RUSH YARDS: {HomeRushYards}//HOME RUSH PLAYS: {HomeRushPlays}//HOME AVG PER RUSH: {HomeAvgPerRush}")
        Console.WriteLine($"HOME PASS YARDS: {HomePassYards}//HOME PASS PLAYS: {HomePassPlays}//HOME AVG PER PASS: {HomeAvgPerPass}")
        Console.WriteLine($"AWAY TOTAL YARDS: {AwayTotYards}//AWAY TOTAL PLAYS: {AwayTotPlays}//AWAY AVG PER PLAY: {AwayAvgPerPlay}")
        Console.WriteLine($"AWAY RUSH YARDS: {AwayRushYards}//AWAY RUSH PLAYS: {AWayRushPlays}//AWAY AVG PER RUSH: {AwayAvgPerRush}")
        Console.WriteLine($"AWAY PASS YARDS: {AWayPassYards}//AWAY PASS PLAYS: {AwayPassPlays}//AWAY AVG PER PASS: {AwayAvgPerPass}")

    End Sub

    Private Sub LoadDepthCharts(homeTeamHasBall As Boolean, ByVal homeTeamId As Integer, ByVal awayTeamId As Integer)
        If homeTeamHasBall Then 'The home team is in possession, load the home team depth charts
            'Home.QB1 = TeamDT.Rows.Find(PlayerDT.AsEnumerable().GroupBy(Sub(x)
            'x.Item("TeamId") = homeTeamId.ToString() And x.Item("DepthPos") Is "QB1"
            'End Sub)
        End If
    End Sub

    Public Shared Function GetPassType() As PassTypeEnum

        Dim PassType As String
        Select Case MyRand.GenerateDouble(0, 100) 'Generate a new Random number
            Case <= 4.0
                PassType = PassTypeEnum.PBehindLOSFarL
                PlayType = PlayTypeEnum.PassBehindLOS
            Case <= 8.0
                PassType = PassTypeEnum.PBehindLOSLMid
                PlayType = PlayTypeEnum.PassBehindLOS
            Case <= 9.3
                PassType = PassTypeEnum.PBehindLOSLMid
                PlayType = PlayTypeEnum.PassBehindLOS
            Case <= 13.7
                PassType = PassTypeEnum.PBehindLOSRMid
                PlayType = PlayTypeEnum.PassBehindLOS
            Case <= 18.9
                PassType = PassTypeEnum.PBehindLOSFarR
                PlayType = PlayTypeEnum.PassBehindLOS
            Case <= 29
                PassType = PassTypeEnum.PShortFarL
                PlayType = PlayTypeEnum.PassShort
            Case <= 38.6
                PassType = PassTypeEnum.PShortLMid
                PlayType = PlayTypeEnum.PassShort
            Case <= 46
                PassType = PassTypeEnum.PShortMid
                PlayType = PlayTypeEnum.PassShort
            Case <= 57.2
                PassType = PassTypeEnum.PShortRMid
                PlayType = PlayTypeEnum.PassShort
            Case <= 68.2
                PassType = PassTypeEnum.PShortFarR
                PlayType = PlayTypeEnum.PassShort
            Case <= 73.1
                PassType = PassTypeEnum.PMedFarL
                PlayType = PlayTypeEnum.PassMed
            Case <= 76.8
                PassType = PassTypeEnum.PMedLMid
                PlayType = PlayTypeEnum.PassMed
            Case <= 79.9
                PassType = PassTypeEnum.PMedMid
                PlayType = PlayTypeEnum.PassMed
            Case <= 83.8
                PassType = PassTypeEnum.PMedRMid
                PlayType = PlayTypeEnum.PassMed
            Case <= 88.9
                PassType = PassTypeEnum.PMedFarR
                PlayType = PlayTypeEnum.PassMed
            Case <= 92.5
                PassType = PassTypeEnum.PLongFarL
                PlayType = PlayTypeEnum.PassLong
            Case <= 93.5
                PassType = PassTypeEnum.PLongLMid
                PlayType = PlayTypeEnum.PassLong
            Case <= 94.5
                PassType = PassTypeEnum.PLongMid
                PlayType = PlayTypeEnum.PassLong
            Case <= 95.8
                PassType = PassTypeEnum.PLongRMid
                PlayType = PlayTypeEnum.PassLong
            Case Else
                PassType = PassTypeEnum.PLongFarR
                PlayType = PlayTypeEnum.PassLong
        End Select
        Return PassType
    End Function
    ''' <summary>
    ''' Checks to see what happens to the pass if it is thrown
    ''' </summary>
    ''' <param name="passType"></param>
    ''' <returns></returns>
    Public Shared Function GetPassCompletion(passType As PassTypeEnum) As Boolean
        Select Case passType
            Case PassTypeEnum.PBehindLOSFarL
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 64.5 : IsComplete = True
                    Case <= 64.8 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PBehindLOSLMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 75.9 : IsComplete = True
                    Case <= 76.2 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PBehindLOSMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 51.3 : IsComplete = True
                    Case <= 51.75 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PBehindLOSRMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 74.7 : IsComplete = True
                    Case <= 75.2 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PBehindLOSFarR
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 64.5 : IsComplete = True
                    Case <= 65.0 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PShortFarL
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 64.8 : IsComplete = True
                    Case <= 66.1 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PShortLMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 67.4 : IsComplete = True
                    Case <= 68.7 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PShortMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 70.3 : IsComplete = True
                    Case <= 71.7 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PShortRMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 67.1 : IsComplete = True
                    Case <= 68.7 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PShortFarR
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 67.6 : IsComplete = True
                    Case <= 69.2 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PMedFarL
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 47.0 : IsComplete = True
                    Case <= 50.5 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PMedLMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 56.7 : IsComplete = True
                    Case <= 60.2 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PMedMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 60.9 : IsComplete = True
                    Case <= 65.9 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PMedRMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 55.0 : IsComplete = True
                    Case <= 58.8 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PMedFarR
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 46.9 : IsComplete = True
                    Case <= 50.7 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PLongFarL
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 27.9 : IsComplete = True
                    Case <= 32.9 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PLongLMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 36.8 : IsComplete = True
                    Case <= 41.8 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PLongMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 38.0 : IsComplete = True
                    Case <= 45.5 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PLongRMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 36.1 : IsComplete = True
                    Case <= 41.1 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
            Case PassTypeEnum.PLongFarR
                Select Case MyRand.GenerateDouble(0, 100)
                    Case <= 30.6 : IsComplete = True
                    Case <= 35.6 : IsIntercepted = True
                    Case Else
                        IsComplete = False
                        IsIntercepted = False
                End Select
        End Select
        If IsComplete Then IsIntercepted = False
        If IsIntercepted Then IsComplete = False
        HomePassPlays += If(HomePossession, 1, 0)
        AwayPassPlays += If(HomePossession, 0, 1)
        Return IsComplete
    End Function

    Public Shared Function GetPassYards(pass As PassTypeEnum) As Single

        Dim PassYards As Single
        Select Case pass
            'Pass is caught behind the LOS
            Case PassTypeEnum.PBehindLOSFarL, PassTypeEnum.PBehindLOSFarR, PassTypeEnum.PBehindLOSLMid, PassTypeEnum.PBehindLOSMid, PassTypeEnum.PBehindLOSRMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 12.22
                        PassYards = MyRand.GenerateDouble(-4, -0.1)
                    Case 12.23 To 60
                        PassYards = MyRand.GenerateDouble(0, 4.9)
                    Case 60.01 To 85
                        PassYards = MyRand.GenerateDouble(5, 9.9)
                    Case 85.01 To 92
                        PassYards = MyRand.GenerateDouble(10, 14.9)
                    Case 92.01 To 95.5
                        PassYards = MyRand.GenerateDouble(15, 19.9)
                    Case 95.51 To 97
                        PassYards = MyRand.GenerateDouble(20, 24.9)
                    Case 97.01 To 98
                        PassYards = MyRand.GenerateDouble(25, 29.9)
                    Case 98.01 To 98.5
                        PassYards = MyRand.GenerateDouble(30, 34.9)
                    Case 98.51 To 99
                        PassYards = MyRand.GenerateDouble(35, 39.9)
                    Case 99.01 To 99.25
                        PassYards = MyRand.GenerateDouble(40, 50)
                    Case 99.26 To 99.5
                        PassYards = MyRand.GenerateDouble(50.1, 60)
                    Case 99.51 To 99.75
                        PassYards = MyRand.GenerateDouble(60.1, 70)
                    Case 99.76 To 99.9
                        PassYards = MyRand.GenerateDouble(70.1, 80)
                    Case Else
                        PassYards = MyRand.GenerateDouble(80.1, 100)
                End Select
                'Pass is caught 0-10 yards downfield
            Case PassTypeEnum.PShortFarL, PassTypeEnum.PShortFarR, PassTypeEnum.PShortLMid, PassTypeEnum.PShortLMid, PassTypeEnum.PShortMid, PassTypeEnum.PShortRMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 30
                        PassYards = MyRand.GenerateDouble(0, 4.9)
                    Case 30.01 To 71.6
                        PassYards = MyRand.GenerateDouble(5, 9.9)
                    Case 71.61 To 83
                        PassYards = MyRand.GenerateDouble(10, 14.9)
                    Case 83.01 To 90
                        PassYards = MyRand.GenerateDouble(15, 19.9)
                    Case 90.01 To 94
                        PassYards = MyRand.GenerateDouble(20, 24.9)
                    Case 94.01 To 96
                        PassYards = MyRand.GenerateDouble(25, 29.9)
                    Case 96.01 To 97
                        PassYards = MyRand.GenerateDouble(30, 34.9)
                    Case 97.01 To 97.5
                        PassYards = MyRand.GenerateDouble(35, 39.9)
                    Case 97.51 To 98
                        PassYards = MyRand.GenerateDouble(40, 50)
                    Case 98.01 To 98.75
                        PassYards = MyRand.GenerateDouble(50.1, 60)
                    Case 98.76 To 99.5
                        PassYards = MyRand.GenerateDouble(60.1, 70)
                    Case 99.51 To 99.75
                        PassYards = MyRand.GenerateDouble(70.1, 80)
                    Case Else
                        PassYards = MyRand.GenerateDouble(80.1, 100)
                End Select
                'Medium Pass 10-20 yards
            Case PassTypeEnum.PMedFarL, PassTypeEnum.PMedFarR, PassTypeEnum.PMedLMid, PassTypeEnum.PMedRMid, PassTypeEnum.PMedMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 30
                        PassYards = MyRand.GenerateDouble(10, 14.9)
                    Case 30.01 To 53.7
                        PassYards = MyRand.GenerateDouble(15.0, 19.9)
                    Case 53.71 To 75
                        PassYards = MyRand.GenerateDouble(20, 24.9)
                    Case 75.01 To 85
                        PassYards = MyRand.GenerateDouble(25, 29.9)
                    Case 85.01 To 92
                        PassYards = MyRand.GenerateDouble(30, 34.9)
                    Case 92.01 To 95
                        PassYards = MyRand.GenerateDouble(35, 40)
                    Case 95.01 To 97
                        PassYards = MyRand.GenerateDouble(40.1, 50)
                    Case 97.01 To 98
                        PassYards = MyRand.GenerateDouble(50.01, 60)
                    Case 98.01 To 99
                        PassYards = MyRand.GenerateDouble(60.01, 70)
                    Case 99.01 To 99.5
                        PassYards = MyRand.GenerateDouble(70.1, 80)
                    Case Else
                        PassYards = MyRand.GenerateDouble(80.1, 100)
                End Select
                'Long passes---20+ yards
            Case Else
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 40
                        PassYards = MyRand.GenerateDouble(20, 24.9)
                    Case 40.01 To 65
                        PassYards = MyRand.GenerateDouble(25, 29.9)
                    Case 65.01 To 83
                        PassYards = MyRand.GenerateDouble(30, 34.9)
                    Case 83.01 To 94
                        PassYards = MyRand.GenerateDouble(35, 39.9)
                    Case 94.01 To 97
                        PassYards = MyRand.GenerateDouble(40, 50)
                    Case 97.01 To 98
                        PassYards = MyRand.GenerateDouble(50.1, 60)
                    Case 98.01 To 98.75
                        PassYards = MyRand.GenerateDouble(60.1, 70)
                    Case 98.76 To 99.5
                        PassYards = MyRand.GenerateDouble(70.1, 80)
                    Case Else
                        PassYards = MyRand.GenerateDouble(80.1, 100)
                End Select
        End Select
        HomePassYards += If(HomePossession, PassYards, 0)
        AWayPassYards += If(HomePossession, 0, PassYards)
        Return PassYards
    End Function

    Public Shared Function GetRunType() As RunTypeEnum
        Dim Run As New RunTypeEnum
        Select Case MyRand.GenerateInt32(0, 100)
            Case 0 To 10
                Run = RunTypeEnum.LeftEnd
                PlayType = PlayTypeEnum.RunOutside
            Case 11 To 22
                Run = RunTypeEnum.LeftTackle
                PlayType = PlayTypeEnum.RunInside
            Case 23 To 78
                Run = RunTypeEnum.Middle
                PlayType = PlayTypeEnum.RunInside
            Case 79 To 90
                Run = RunTypeEnum.RightTackle
                PlayType = PlayTypeEnum.RunInside
            Case Else
                Run = RunTypeEnum.RightEnd
                PlayType = PlayTypeEnum.RunOutside
        End Select
        Return Run
    End Function
    ''' <summary>
    ''' Gets the run distance by type and the place on the field
    ''' </summary>
    ''' <param name="run"></param>
    ''' <returns></returns>
    Public Shared Function GetRunYards(run As RunTypeEnum) As Single
        Dim Yards As Single
        Select Case YardLine
            Case < 11 ' Ball is on your own 10 or inside your own 10 yard line
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 0.7
                        Yards = MyRand.GenerateDouble(-5, -4.1)
                    Case 0.71 To 1.05
                        Yards = MyRand.GenerateDouble(-4, -3.1)
                    Case 1.06 To 1.76
                        Yards = MyRand.GenerateDouble(-3, -2.1)
                    Case 1.77 To 3.89
                        Yards = MyRand.GenerateDouble(-2, -1.1)
                    Case 3.9 To 7.44
                        Yards = MyRand.GenerateDouble(-1, -0.1)
                    Case 7.45 To 16.31
                        Yards = MyRand.GenerateDouble(0, 0.9)
                    Case 16.32 To 28.19
                        Yards = MyRand.GenerateDouble(1, 1.9)
                    Case 28.2 To 41.31
                        Yards = MyRand.GenerateDouble(2, 2.9)
                    Case 41.32 To 53.37
                        Yards = MyRand.GenerateDouble(3, 3.9)
                    Case 53.38 To 66.14
                        Yards = MyRand.GenerateDouble(4, 4.9)
                    Case 66.15 To 75.89
                        Yards = MyRand.GenerateDouble(5, 5.9)
                    Case 75.9 To 81.74
                        Yards = MyRand.GenerateDouble(6, 6.9)
                    Case 81.75 To 86.35
                        Yards = MyRand.GenerateDouble(7, 7.9)
                    Case 86.36 To 88.65
                        Yards = MyRand.GenerateDouble(8, 8.9)
                    Case 88.66 To 91.31
                        Yards = MyRand.GenerateDouble(9, 9.9)
                    Case 91.32 To 92.02
                        Yards = MyRand.GenerateDouble(10, 10.9)
                    Case 92.03 To 93.62
                        Yards = MyRand.GenerateDouble(11, 11.9)
                    Case 93.63 To 94.68
                        Yards = MyRand.GenerateDouble(12, 12.9)
                    Case 94.69 To 95.39
                        Yards = MyRand.GenerateDouble(13, 13.9)
                    Case 95.4 To 95.92
                        Yards = MyRand.GenerateDouble(14, 14.9)
                    Case 95.93 To 96.27
                        Yards = MyRand.GenerateDouble(15, 15.9)
                    Case 96.28 To 96.45
                        Yards = MyRand.GenerateDouble(16, 16.9)
                    Case 96.46 To 96.98
                        Yards = MyRand.GenerateDouble(17, 17.9)
                    Case 96.99 To 97.51
                        Yards = MyRand.GenerateDouble(18, 18.9)
                    Case 97.52 To 97.86
                        Yards = MyRand.GenerateDouble(19, 19.9)
                    Case 97.87 To 98.21
                        Yards = MyRand.GenerateDouble(20, 20.9)
                    Case 98.22 To 98.56
                        Yards = MyRand.GenerateDouble(21, 30)
                    Case 98.57 To 99.09
                        Yards = MyRand.GenerateDouble(31, 40)
                    Case 99.1 To 99.44
                        Yards = MyRand.GenerateDouble(41, 50)
                    Case 99.45 To 99.54
                        Yards = MyRand.GenerateDouble(51, 60)
                    Case 99.55 To 99.64
                        Yards = MyRand.GenerateDouble(61, 70)
                    Case 99.65 To 99.74
                        Yards = MyRand.GenerateDouble(71, 80)
                    Case Else
                        Yards = MyRand.GenerateDouble(81, 100)
                End Select

            Case 11 To 89 'Ball is Between your 11 yard line and the opponent's 11 yard line.
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 0.51
                        Yards = MyRand.GenerateDouble(-9, -6)
                    Case 0.52 To 0.91
                        Yards = MyRand.GenerateDouble(-5, -4.1)
                    Case 0.92 To 1.77
                        Yards = MyRand.GenerateDouble(-4, -3.1)
                    Case 1.78 To 3.2
                        Yards = MyRand.GenerateDouble(-3, -2.1)
                    Case 3.21 To 5.77
                        Yards = MyRand.GenerateDouble(-2, -1.1)
                    Case 5.78 To 9.86
                        Yards = MyRand.GenerateDouble(-1, -0.1)
                    Case 9.87 To 18.85
                        Yards = MyRand.GenerateDouble(0, 0.9)
                    Case 18.86 To 29.06
                        Yards = MyRand.GenerateDouble(1, 1.9)
                    Case 29.07 To 40.91
                        Yards = MyRand.GenerateDouble(2, 2.9)
                    Case 40.92 To 53.44
                        Yards = MyRand.GenerateDouble(3, 3.9)
                    Case 53.45 To 63.46
                        Yards = MyRand.GenerateDouble(4, 4.9)
                    Case 63.47 To 71.19
                        Yards = MyRand.GenerateDouble(5, 5.9)
                    Case 71.2 To 76.74
                        Yards = MyRand.GenerateDouble(6, 6.9)
                    Case 76.75 To 80.88
                        Yards = MyRand.GenerateDouble(7, 7.9)
                    Case 80.89 To 84.18
                        Yards = MyRand.GenerateDouble(8, 8.9)
                    Case 84.19 To 87.37
                        Yards = MyRand.GenerateDouble(9, 9.9)
                    Case 87.38 To 88.84
                        Yards = MyRand.GenerateDouble(10, 10.9)
                    Case 88.85 To 90.84
                        Yards = MyRand.GenerateDouble(11, 11.9)
                    Case 90.85 To 92.1
                        Yards = MyRand.GenerateDouble(12, 12.9)
                    Case 92.11 To 93.45
                        Yards = MyRand.GenerateDouble(13, 13.9)
                    Case 93.46 To 94.43
                        Yards = MyRand.GenerateDouble(14, 14.9)
                    Case 94.44 To 95.34
                        Yards = MyRand.GenerateDouble(15, 15.9)
                    Case 95.35 To 95.88
                        Yards = MyRand.GenerateDouble(16, 16.9)
                    Case 95.89 To 96.39
                        Yards = MyRand.GenerateDouble(17, 17.9)
                    Case 96.4 To 96.67
                        Yards = MyRand.GenerateDouble(18, 18.9)
                    Case 96.68 To 97.13
                        Yards = MyRand.GenerateDouble(19, 19.9)
                    Case 97.14 To 97.56
                        Yards = MyRand.GenerateDouble(20, 20.9)
                    Case 97.57 To 97.84
                        Yards = MyRand.GenerateDouble(21, 21.9)
                    Case 97.85 To 98.06
                        Yards = MyRand.GenerateDouble(22, 22.9)
                    Case 98.07 To 98.23
                        Yards = MyRand.GenerateDouble(23, 23.9)
                    Case 98.24 To 98.38
                        Yards = MyRand.GenerateDouble(24, 24.9)
                    Case 98.39 To 98.52
                        Yards = MyRand.GenerateDouble(25, 25.9)
                    Case 98.53 To 98.61
                        Yards = MyRand.GenerateDouble(26, 26.9)
                    Case 98.62 To 98.72
                        Yards = MyRand.GenerateDouble(27, 27.9)
                    Case 98.73 To 98.85
                        Yards = MyRand.GenerateDouble(28, 28.9)
                    Case 98.86 To 98.93
                        Yards = MyRand.GenerateDouble(29, 29.9)
                    Case 98.94 To 98.98
                        Yards = MyRand.GenerateDouble(30, 20.9)
                    Case 98.99 To 99.09
                        Yards = MyRand.GenerateDouble(31, 31.9)
                    Case 99.1 To 99.16
                        Yards = MyRand.GenerateDouble(32, 32.9)
                    Case 99.17 To 99.19
                        Yards = MyRand.GenerateDouble(33, 33.9)
                    Case 99.2 To 99.24
                        Yards = MyRand.GenerateDouble(34, 34.9)
                    Case 99.25 To 99.28
                        Yards = MyRand.GenerateDouble(35, 35.9)
                    Case 99.29 To 99.33
                        Yards = MyRand.GenerateDouble(36, 36.9)
                    Case 99.34 To 99.37
                        Yards = MyRand.GenerateDouble(37, 37.9)
                    Case 99.38 To 99.39
                        Yards = MyRand.GenerateDouble(38, 38.9)
                    Case 99.4 To 99.42
                        Yards = MyRand.GenerateDouble(39, 39.9)
                    Case 99.43 To 99.48
                        Yards = MyRand.GenerateDouble(40, 40.9)
                    Case 99.49 To 99.72
                        Yards = MyRand.GenerateDouble(41, 50)
                    Case 99.73 To 99.82
                        Yards = MyRand.GenerateDouble(51, 60)
                    Case 99.83 To 99.9
                        Yards = MyRand.GenerateDouble(61, 70)
                    Case 99.91 To 99.96
                        Yards = MyRand.GenerateDouble(71, 80)
                    Case Else
                        Yards = MyRand.GenerateDouble(81, 100)
                End Select

            Case > 89 'Ball is on or inside the Opponents 10 yard line
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 0.38
                        Yards = MyRand.GenerateDouble(-8, -6)
                    Case 0.39 To 0.97
                        Yards = MyRand.GenerateDouble(-5, -4.1)
                    Case 0.98 To 2.15
                        Yards = MyRand.GenerateDouble(-4, -3.1)
                    Case 2.16 To 4.1
                        Yards = MyRand.GenerateDouble(-3, -2.1)
                    Case 4.11 To 6.45
                        Yards = MyRand.GenerateDouble(-2, -1.1)
                    Case 6.46 To 12.81
                        Yards = MyRand.GenerateDouble(-1, -0.1)
                    Case 12.82 To 26.31
                        Yards = MyRand.GenerateDouble(0, 0.9)
                    Case 26.32 To 52.83
                        Yards = MyRand.GenerateDouble(1, 1.9)
                    Case 52.84 To 67.7
                        Yards = MyRand.GenerateDouble(2, 2.9)
                    Case 67.71 To 78.38
                        Yards = MyRand.GenerateDouble(3, 3.9)
                    Case 78.39 To 85.02
                        Yards = MyRand.GenerateDouble(4, 4.9)
                    Case 85.03 To 90.5
                        Yards = MyRand.GenerateDouble(5, 5.9)
                    Case 90.51 To 93.83
                        Yards = MyRand.GenerateDouble(6, 6.9)
                    Case 93.84 To 96.96
                        Yards = MyRand.GenerateDouble(7, 7.9)
                    Case 96.97 To 98.62
                        Yards = MyRand.GenerateDouble(8, 8.9)
                    Case 98.63 To 99.21
                        Yards = MyRand.GenerateDouble(9, 9.9)
                    Case Else
                        Yards = 10
                End Select
        End Select
        HomeRushYards += If(HomePossession, Yards, 0)
        AwayRushYards += If(HomePossession, 0, Yards)
        HomeRushPlays += If(HomePossession, 1, 0)
        AWayRushPlays += If(HomePossession, 0, 1)
        Return Yards
    End Function

End Class