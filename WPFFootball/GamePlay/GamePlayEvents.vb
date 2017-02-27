Imports Mersenne
Imports GoalLineStandFootball.GamePlayStats
Public Class GamePlayEvents
    Inherits GamePlay

#Region "Events"
    Public Event PassCompletion(ByVal QB As Integer, ByVal recPlayer As Integer, ByVal yardsGained As Single)
    Public Event PassIncompletion(ByVal QB As Integer, ByVal intReceiver As Integer)
    Public Event PassDropped(ByVal QB As Integer, ByVal recPlayer As Integer)
    Public Event PassDefended(ByVal defPlayer As Integer, ByVal intendedRec As Integer)
    Public Event Touchback(onKickoff As Boolean)
    Public Event FirstDown(ByVal player As Integer, ByVal homeTeam As Boolean)
    Public Event TouchDown(ByVal type As ScoringTypeEnum, ByVal homeTeam As Boolean, ByVal player As Integer)
    Public Event Interception(ByVal QBThrowing As Integer, ByVal IntPlayer As Integer, ByVal homeTeam As Boolean)
    Public Event Fumble(ByVal fumblingPlayer As Integer, ByVal recoveringPlayer As Integer, ByVal offenseRecovers As Boolean)
    Public Event Tackle(ByVal ballCarrier As Integer, ByVal tackler As Integer)
    Public Event Sack(ByVal QB As Integer, ByVal defender As Integer, ByVal yardsLost As Integer)
    Public Event Punt(ByVal punter As Integer, ByVal puntYards As Integer)
    Public Event FieldGoal()
    Public Event ChangeOfPoss(ByVal homeTeamHasBall As Boolean) 'Fires Change of Possession Event
    Public Event Timeout(ByVal homeTeamCalled As Boolean)
    Public Event EndOfQuarter()
    Public Event TwoMinuteWarning()
    Public Event HalfTime()
    Public Event Kickoff(ByVal homeTeamKickingOff As Boolean)
    Public Event KickoffRet(ByVal kickReturner As Integer)

#End Region
    Public Sub New(homeTeamId As Integer, awayTeamId As Integer)
        MyBase.New(homeTeamId, awayTeamId)
        'Add Event Handlers
        AddHandler Kickoff, AddressOf KickoffEvt
        AddHandler KickoffRet, AddressOf KickoffRetEvt
        AddHandler Touchback, AddressOf TouchbackEvt
    End Sub

    Public Overridable Sub KickoffRetEvt(kickReturner As Integer)
        Dim MyRand As New MersenneTwister
        Dim GenKickRetYds As New MersenneTwister

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
        If Stats.Rows.Find(kickReturner) IsNot Nothing Then 'If there is row with this player then add stats to it

        Else
            Dim Row = Stats.NewRow()
            Row("PlayerId") = kickReturner

        End If
    End Sub

    Public Overridable Sub TouchbackEvt(onKickoff As Boolean)
        'If its a touchback from a kickoff, the ball is placed at the 25 yard line, otherwise at the 20
        Stats.Rows.Find(If(HomePossession, Home.K1.Item("PlayerId"), Away.K1.Item("PlayerId"))).Item("Touchback") += 1 'Add a TB for the appropriate Kicker
        YardLine = If(onKickoff, 25, 20)
        Down = 1
    End Sub

    ''' <summary>
    ''' Handles the kickoff Event
    ''' </summary>
    Public Overridable Sub KickoffEvt(homeTeamKickingOff As Boolean)
        Dim MyRand As New MersenneTwister()

        HomePossession = If(homeTeamKickingOff, False, True) 'This is the same as HomePossesion = homeTeamKickingOff ? True : False in C#
        If MyRand.GenerateInt32(0, 100) < 40 Then '39% of kicks are returned, otherwise touchback
            RaiseEvent KickoffRet(If(HomePossession, Home.WR5.Item("PlayerId"), Away.WR5.Item("PlayerId")))
        Else RaiseEvent Touchback(True)
        End If
    End Sub
End Class