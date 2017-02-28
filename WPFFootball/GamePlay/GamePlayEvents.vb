Imports Mersenne
Imports GoalLineStandFootball.GamePlayStats
Imports System.Data

Public Class GamePlayEvents
    Inherits GamePlay

#Region "Events"
    Public Shared Event PassCompletion(ByVal QB As Integer, ByVal recPlayer As Integer, ByVal yardsGained As Single)
    Public Shared Event PassIncompletion(ByVal QB As Integer, ByVal intReceiver As Integer)
    Public Shared Event PassDropped(ByVal QB As Integer, ByVal recPlayer As Integer)
    Public Shared Event PassDefended(ByVal defPlayer As Integer, ByVal intendedRec As Integer)

    Public Shared Event FirstDown(ByVal player As Integer, ByVal homeTeam As Boolean)
    Public Shared Event TouchDown(ByVal type As ScoringTypeEnum, ByVal homeTeam As Boolean, ByVal player As Integer)
    Public Shared Event Interception(ByVal QBThrowing As Integer, ByVal IntPlayer As Integer, ByVal homeTeam As Boolean)
    'Public Shared Event Fumble(ByVal fumblingPlayer As Integer, ByVal recoveringPlayer As Integer)
    'Public Shared Event Tackle(ByVal ballCarrier As Integer, ByVal tackler As Integer)
    Public Shared Event Sack(ByVal QB As Integer, ByVal defender As Integer, ByVal yardsLost As Integer)
    Public Shared Event Punt(ByVal punter As Integer, ByVal puntYards As Integer)
    Public Shared Event FieldGoal()
    Public Shared Event ChangeOfPoss(ByVal homeTeamHasBall As Boolean) 'Fires Change of Possession Event
    Public Shared Event Timeout(ByVal homeTeamCalled As Boolean)
    Public Shared Event EndOfQuarter()
    Public Shared Event TwoMinuteWarning()
    Public Shared Event HalfTime()
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
        Fumble(kickReturner, tackler)

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

        Console.WriteLine($"{Stats.Rows.Find(tackler).Item("Pos")} {Stats.Rows.Find(tackler).Item("FName")} {Stats.Rows.Find(tackler).Item("LName")} tackles{Stats.Rows.Find(kickReturner).Item("Pos")} _
                          {Stats.Rows.Find(kickReturner).Item("FName")} {Stats.Rows.Find(kickReturner).Item("LName")} at the {YardLine} Yard Line after a {KickReturnYards} _
                          yard return from the {StartReturn} yard line. First Down!")
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
    ''' <param name="kickReturner"></param>
    ''' <param name="tackler"></param>
    Private Shared Sub Fumble(kickReturner As Integer, tackler As Integer)

    End Sub

    Private Shared Sub Touchback(onKickoff As Boolean, kicker As Integer)
        'If its a touchback from a kickoff, the ball is placed at the 25 yard line, otherwise at the 20
        Stats.Rows.Find(kicker).Item("Touchback") += 1 'Kicker gets a TB added to his Totals
        YardLine = If(onKickoff, 25, 20)
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
End Class