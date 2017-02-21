Imports System.Data
''' <summary>
''' This is the class where game play takes place
''' </summary>
Public Class GamePlay
    Public GameLoop As Boolean 'This is what controls whether the game is still going on or it has ended
    Private PassType As New PassTypeEnum 'Enumeration for the different types of passes
#Region "Time Variables"
    Private Property StopClock As Boolean
    Private Property Pace As Integer
    Private Property GameTime As New TimeSpan(0, 15, 0) 'Sets the clock to 15 minutes(0 hours, 15 minutes, 0 seconds)
    Private Property BallSpot As Integer
#End Region

#Region "Passing Variables"
    Private Property PBehLOSFarLComp As Single = 64.5
    Private Property PBehLOSLMidComp As Single = 75.9
    Private Property PBehLOSMidComp As Single = 51.3
    Private Property PBehLOSRMidComp As Single = 74.7
    Private Property PBehLOSFarRComp As Single = 64.5
    Private Property PShortFarLComp As Single = 64.8
    Private Property PShortLMidComp As Single = 67.4
    Private Property PShortMidComp As Single = 70.3
    Private Property PShortRMidComp As Single = 67.1
    Private Property PShortFarRComp As Single = 67.6
    Private Property PMedFarLComp As Single = 47
    Private Property PMedLMidComp As Single = 56.7
    Private Property PMedMidComp As Single = 60.9
    Private Property PMedRMidComp As Single = 55
    Private Property PMedFarRComp As Single = 46.9
    Private Property PLongFarLComp As Single = 27.9
    Private Property PLongLMidComp As Single = 36.8
    Private Property PLongMidComp As Single = 38
    Private Property PLongRMidComp As Single = 36.1
    Private Property PLongFarRComp As Single = 30.6
#End Region

    Private Property RunMid As Single
    Private Property RunLeft As Single
    Private Property RunLeftEnd As Single
    Private Property RunRight As Single
    Private Property RunRightEnd As Single

    Private Property PuntDistance As Single
    Private Property FGDistance As Single
    Private Property ExpDecayFG As Single
    Private Property Down As Integer
    Private Property YardsToGo As Single
    Private Property YardLine As Single = 35

    Private Property Quarter As Integer = 1
    Private Property Kickoff As Boolean = True 'Initializes the game to start with a kickoff
    Private Property KickoffDist As Single
    Private Property KickReturnYards As Single
    Private HomeTeam As New DataView 'Filters the players DataTable to get the appropriate team's players
    Private AwayTeam As New DataView 'Filters the players DataTable to get the appropriate team's players

    ''' <summary>
    ''' Start the game and pass in the 2 teams who are playing----home and away
    ''' </summary>
    ''' <param name="homeTeam"></param>
    ''' <param name="awayTeam"></param>
    Public Sub StartGame(ByVal homeTeam As Integer, ByVal awayTeam As Integer)
        While GameLoop
            ' While the GameLoop is Set to True run the game.
            If Kickoff Then 'We are kicking off

            End If

        End While

    End Sub
    Private Enum PassTypeEnum
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
    Private Function GetPassType() As PassTypeEnum
        Dim MyRand As New Mersenne.MersenneTwister
        Dim PassType As String
        Select Case MyRand.GenerateDouble(0, 100) 'Generate a new Random number
            Case <= 4.0
                PassType = PassTypeEnum.PBehindLOSFarL
            Case <= 8.0
                PassType = PassTypeEnum.PBehindLOSLMid
            Case <= 9.3
                PassType = PassTypeEnum.PBehindLOSLMid
            Case <= 13.7
                PassType = PassTypeEnum.PBehindLOSRMid
            Case <= 18.9
                PassType = PassTypeEnum.PBehindLOSFarR
            Case <= 29
                PassType = PassTypeEnum.PShortFarL
            Case <= 38.6
                PassType = PassTypeEnum.PShortLMid
            Case <= 46
                PassType = PassTypeEnum.PShortMid
            Case <= 57.2
                PassType = PassTypeEnum.PShortRMid
            Case <= 68.2
                PassType = PassTypeEnum.PShortFarR
            Case <= 73.1
                PassType = PassTypeEnum.PMedFarL
            Case <= 76.8
                PassType = PassTypeEnum.PMedLMid
            Case <= 79.9
                PassType = PassTypeEnum.PMedMid
            Case <= 83.8
                PassType = PassTypeEnum.PMedRMid
            Case <= 88.9
                PassType = PassTypeEnum.PMedFarR
            Case <= 92.5
                PassType = PassTypeEnum.PLongFarL
            Case <= 93.5
                PassType = PassTypeEnum.PLongLMid
            Case <= 94.5
                PassType = PassTypeEnum.PLongMid
            Case <= 95.8
                PassType = PassTypeEnum.PLongRMid
            Case Else
                PassType = PassTypeEnum.PLongFarR
        End Select
        Return PassType
    End Function

    Private Function GetPassCompletion() As Boolean
        Dim MyRand As New Mersenne.MersenneTwister
        Dim IsComplete As Boolean 'Returns TRUE if it is Below this number, false otherwise
        Select Case PassType
            Case PassTypeEnum.PBehindLOSFarL
                IsComplete = MyRand.GenerateDouble(0, 100) <= 64.5
            Case PassTypeEnum.PBehindLOSLMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 75.9
            Case PassTypeEnum.PBehindLOSMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 51.3
            Case PassTypeEnum.PBehindLOSRMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 74.7
            Case PassTypeEnum.PBehindLOSFarR
                IsComplete = MyRand.GenerateDouble(0, 100) <= 64.5
            Case PassTypeEnum.PShortFarL
                IsComplete = MyRand.GenerateDouble(0, 100) <= 64.8
            Case PassTypeEnum.PShortLMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 67.4
            Case PassTypeEnum.PShortMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 70.3
            Case PassTypeEnum.PShortRMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 67.1
            Case PassTypeEnum.PShortFarR
                IsComplete = MyRand.GenerateDouble(0, 100) <= 67.6
            Case PassTypeEnum.PMedFarL
                IsComplete = MyRand.GenerateDouble(0, 100) <= 47.0
            Case PassTypeEnum.PMedLMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 56.7
            Case PassTypeEnum.PMedMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 60.9
            Case PassTypeEnum.PMedRMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 55.0
            Case PassTypeEnum.PMedFarR
                IsComplete = MyRand.GenerateDouble(0, 100) <= 46.9
            Case PassTypeEnum.PLongFarL
                IsComplete = MyRand.GenerateDouble(0, 100) <= 27.9
            Case PassTypeEnum.PLongLMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 36.8
            Case PassTypeEnum.PLongMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 38.0
            Case PassTypeEnum.PLongRMid
                IsComplete = MyRand.GenerateDouble(0, 100) <= 36.1
            Case PassTypeEnum.PLongFarR
                IsComplete = MyRand.GenerateDouble(0, 100) <= 30.6
        End Select

        Return IsComplete
    End Function

    Private Function GetPassYards(ByVal pass As PassTypeEnum) As Single
        Dim MyRand As New Mersenne.MersenneTwister
        Dim GenPassYards As New Mersenne.MersenneTwister
        Dim PassYards As Single
        Select Case pass
            'Pass is caught behind the LOS
            Case PassTypeEnum.PBehindLOSFarL, PassTypeEnum.PBehindLOSFarR, PassTypeEnum.PBehindLOSLMid, PassTypeEnum.PBehindLOSMid, PassTypeEnum.PBehindLOSRMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 12.22
                        PassYards = GenPassYards.GenerateDouble(-4, -0.1)
                    Case 12.23 To 60
                        PassYards = GenPassYards.GenerateDouble(0, 4.9)
                    Case 60.01 To 85
                        PassYards = GenPassYards.GenerateDouble(5, 9.9)
                    Case 85.01 To 92
                        PassYards = GenPassYards.GenerateDouble(10, 14.9)
                    Case 92.01 To 95.5
                        PassYards = GenPassYards.GenerateDouble(15, 19.9)
                    Case 95.51 To 97
                        PassYards = GenPassYards.GenerateDouble(20, 24.9)
                    Case 97.01 To 98
                        PassYards = GenPassYards.GenerateDouble(25, 29.9)
                    Case 98.01 To 98.5
                        PassYards = GenPassYards.GenerateDouble(30, 34.9)
                    Case 98.51 To 99
                        PassYards = GenPassYards.GenerateDouble(35, 39.9)
                    Case 99.01 To 99.25
                        PassYards = GenPassYards.GenerateDouble(40, 50)
                    Case 99.26 To 99.5
                        PassYards = GenPassYards.GenerateDouble(50.1, 60)
                    Case 99.51 To 99.75
                        PassYards = GenPassYards.GenerateDouble(60.1, 70)
                    Case 99.76 To 99.9
                        PassYards = GenPassYards.GenerateDouble(70.1, 80)
                    Case Else
                        PassYards = GenPassYards.GenerateDouble(80.1, 100)
                End Select
                'Pass is caught 0-10 yards downfield
            Case PassTypeEnum.PShortFarL, PassTypeEnum.PShortFarR, PassTypeEnum.PShortLMid, PassTypeEnum.PShortLMid, PassTypeEnum.PShortMid, PassTypeEnum.PShortRMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 30
                        PassYards = GenPassYards.GenerateDouble(0, 4.9)
                    Case 30.01 To 71.6
                        PassYards = GenPassYards.GenerateDouble(5, 9.9)
                    Case 71.61 To 83
                        PassYards = GenPassYards.GenerateDouble(10, 14.9)
                    Case 83.01 To 90
                        PassYards = GenPassYards.GenerateDouble(15, 19.9)
                    Case 90.01 To 94
                        PassYards = GenPassYards.GenerateDouble(20, 24.9)
                    Case 94.01 To 96
                        PassYards = GenPassYards.GenerateDouble(25, 29.9)
                    Case 96.01 To 97
                        PassYards = GenPassYards.GenerateDouble(30, 34.9)
                    Case 97.01 To 97.5
                        PassYards = GenPassYards.GenerateDouble(35, 39.9)
                    Case 97.51 To 98
                        PassYards = GenPassYards.GenerateDouble(40, 50)
                    Case 98.01 To 98.75
                        PassYards = GenPassYards.GenerateDouble(50.1, 60)
                    Case 98.76 To 99.5
                        PassYards = GenPassYards.GenerateDouble(60.1, 70)
                    Case 99.51 To 99.75
                        PassYards = GenPassYards.GenerateDouble(70.1, 80)
                    Case Else
                        PassYards = GenPassYards.GenerateDouble(80.1, 100)
                End Select
                'Medium Pass 10-20 yards
            Case PassTypeEnum.PMedFarL, PassTypeEnum.PMedFarR, PassTypeEnum.PMedLMid, PassTypeEnum.PMedRMid, PassTypeEnum.PMedMid
                Select Case MyRand.GenerateDouble(0, 100)
                    Case 0 To 30
                        PassYards = GenPassYards.GenerateDouble(10, 14.9)
                    Case 30.01 To 53.7
                        PassYards = GenPassYards.GenerateDouble(15.0, 19.9)
                    Case 53.71 To 75
                        PassYards = GenPassYards.GenerateDouble(20, 24.9)
                    Case 75.01 To 85
                        PassYards = GenPassYards.GenerateDouble(25, 29.9)
                    Case 85.01 To 92
                        PassYards = GenPassYards.GenerateDouble(30, 34.9)
                    Case 92.01 To 95
                        PassYards = GenPassYards.GenerateDouble(35, 40)
                    Case 95.01 To 97
                        PassYards = GenPassYards.GenerateDouble(40.1, 50)
                    Case 97.01 To 98
                        PassYards = GenPassYards.GenerateDouble(50.01, 60)
                    Case 98.01 To 99
                        PassYards = GenPassYards.GenerateDouble(60.01, 70)
                    Case 99.01 To 99.5
                        PassYards = GenPassYards.GenerateDouble(70.1, 80)
                    Case Else
                        PassYards = GenPassYards.GenerateDouble(80.1, 100)
                End Select
                'Long passes---20+ yards
            Case Else
                Select Case MyRand.GenerateDouble(0, 100)

                End Select

                Return PassYards
    End Function
End Class