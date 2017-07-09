Imports System.Text.RegularExpressions
Imports GlobalResources
Imports Newtonsoft.Json

Public Class CollegePlayers
    Inherits Players


    'Public DraftDT As DataTable
    'Helper function to create draft class in JS
    Public Function CreatePlayers(ByVal numPlayers As Integer)

        Dim myColPlayer As New CollegePlayers

        NumUnderclassmen = numPlayers * MT.GetGaussian(3.033333333, 0.343139025) 'Get the number of underclassman in this draft

        'Initialize the DB
        Initialize("Football", DraftDT, "DraftPlayers", GetSQLString("College"))
        'Generate a Draft Class
        GenDraftClass()
        'Cycle through and create the specified number of players
        For x As Integer = 1 To numPlayers
            GenDraftPlayers(x, myColPlayer, DraftDT)
            Underclassman = False
        Next x
        'Update the DB
        Update("Football", DraftDT, "DraftPlayers")
        'We are going to return the Table of players
        Return JsonConvert.SerializeObject(DraftDT).ToString()
    End Function

    Public Sub GenDraftPlayers(ByVal playerNum As Integer, ByVal xCollegePlayer As CollegePlayers, ByVal draftDT As DataTable)
        Dim MyPos As String
        Dim PosType As String
        Dim DraftRound As String
        Dim myRating As Integer

        OtherRatingsCount = 0
        draftDT.Rows.Add()
        xCollegePlayer = New CollegePlayers
        PersonalityModel(draftDT, playerNum, xCollegePlayer)
        Try
            MyPos = GetCollegePos(playerNum, draftDT)
            draftDT.Rows(playerNum).Item("CollegePOS") = String.Format("'{0}'", MyPos)
            DraftRound = GetDraftRound(MyPos)
            AthleticFreak = GetFreak(MyPos, DraftRound)

            If AthleticFreak Then
                AthFreak = Enumerable.Range(1, 7).OrderBy(Function(n) MT.GenerateInt32(1, 7)).Take(4).ToList()
            End If

            GenNames(draftDT, playerNum, "CollegePlayer", MyPos, DraftRound)
            draftDT.Rows(playerNum).Item("FortyYardTime") = Get40Time(MyPos, playerNum, draftDT, DraftRound, AthFreak) 'freak 1
            draftDT.Rows(playerNum).Item("TwentyYardTime") = Get20Time(MyPos)
            draftDT.Rows(playerNum).Item("TenYardTime") = Get10Time(MyPos, DraftRound, AthFreak) 'freak 2
            draftDT.Rows(playerNum).Item("ShortShuttle") = GetShortShuttle(MyPos, DraftRound, AthFreak) 'freak 3
            draftDT.Rows(playerNum).Item("BroadJump") = GetBroadJump(MyPos, DraftRound) 'freak 4
            draftDT.Rows(playerNum).Item("VertJump") = GetVertJump(MyPos, DraftRound) 'freak 5
            draftDT.Rows(playerNum).Item("ThreeConeDrill") = Get3Cone(MyPos, DraftRound) 'freak 6
            draftDT.Rows(playerNum).Item("BenchPress") = GetBenchPress(MyPos, DraftRound) 'freak 7
            draftDT.Rows(playerNum).Item("InterviewSkills") = CInt(MT.GetGaussian(49.5, 16.5))
            draftDT.Rows(playerNum).Item("WonderlicTest") = GetWonderlic(MyPos, DraftRound)
            draftDT.Rows(playerNum).Item("SkillsTranslateToNFL") = GetSkillsTranslate(MyPos)
            draftDT.Rows(playerNum).Item("ProjNFLPos") = GetNFLPos(String.Format("'{0}'", MyPos), playerNum)
            draftDT.Rows(playerNum).Item("DLPrimaryTech") = "'NONE'"
            draftDT.Rows(playerNum).Item("DLSecondaryTech") = "'NONE'"
            draftDT.Rows(playerNum).Item("DLPassRushTech") = "'NONE'"
            GetSTAbility(MyPos, playerNum, draftDT)
            GetLSAbility(MyPos, playerNum, draftDT)
            draftDT.Rows(playerNum).Item("RETKickReturn") = GetKickRetAbility(MyPos, playerNum)
            draftDT.Rows(playerNum).Item("RETPuntReturn") = GetPuntRetAbility(MyPos, playerNum, draftDT)

            GetKeyRatings(draftDT, playerNum, MyPos, DraftRound) 'Assigns base ratings based on Round drafted using an Exponential Decay Function

            For x As Integer = 0 To draftDT.Columns.Count - 1 'cycles through the columns and assigns a rating to any of them that are still NULL values
                If draftDT.Rows(playerNum).Item(x) Is DBNull.Value Then
                    OtherRatingsCount += 1
                    myRating = CInt(MT.GetGaussian(49.5, 16.5))
                    draftDT.Rows(playerNum).Item(x) = myRating
                    ActualGrade.OtherRatings += myRating
                End If
            Next x

            'Get the position type, this is somewhat based on ratings
            PosType = GetPosType(MyPos, playerNum, draftDT)
            draftDT.Rows(playerNum).Item("PosType") = String.Format("'{0}'", PosType)

            If Underclassman Then draftDT.Rows(playerNum).Item("Underclassman") = 1 'sets them as an underclassman

            GetPosRatings(MyPos, PosType, playerNum, draftDT) 'Will get positional skills for players based on their position type

            draftDT.Rows(playerNum).Item("ActualGrade") = GetActualGrade(playerNum, MyPos) 'Get the actual grade
            'Reset the Actual Grades
            ActualGrade.KeyRatings = 0
            ActualGrade.OtherRatings = 0
            ActualGrade.Combine = 0

            draftDT.Rows(playerNum).Item("DraftID") = playerNum
        Catch ex As System.InvalidCastException
            Console.WriteLine(ex.Data)
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    Private Function GetFreak(myPos As String, draftRound As String) As Boolean
        Dim IsAthFreak As Boolean
        Dim GetNum As Double = MT.GenerateDouble(0, 100)

        Select Case draftRound
            Case 1
                Select Case myPos
                    Case "QB" : If GetNum <= 7.55 Then IsAthFreak = True
                    Case "RB" : If GetNum <= 13.04 Then IsAthFreak = True
                    Case "WR" : If GetNum <= 5.33 Then IsAthFreak = True
                    Case "TE" : If GetNum <= 9.09 Then IsAthFreak = True
                    Case "OT" : If GetNum <= 5.71 Then IsAthFreak = True
                    Case "DE" : If GetNum <= 11.84 Then IsAthFreak = True
                    Case "DT" : If GetNum <= 8.2 Then IsAthFreak = True
                    Case "ILB" : If GetNum <= 6.67 Then IsAthFreak = True
                    Case "CB" : If GetNum <= 5.13 Then IsAthFreak = True
                    Case "SS" : If GetNum <= 22.22 Then IsAthFreak = True
                    Case "FS" : If GetNum <= 7.14 Then IsAthFreak = True
                    Case Else : If GetNum <= 0.5 Then IsAthFreak = True 'All other positions have not ever had an Athletic Freak drafted in the first round, but this doesn't mean it will never happen---gives them a small chance
                End Select
            Case 2 '2nd round
                Select Case myPos
                    Case "QB" : If GetNum <= 4.76 Then IsAthFreak = True
                    Case "RB" : If GetNum <= 2.13 Then IsAthFreak = True
                    Case "WR" : If GetNum <= 2.5 Then IsAthFreak = True
                    Case "OT" : If GetNum <= 2.04 Then IsAthFreak = True
                    Case "OG" : If GetNum <= 7.41 Then IsAthFreak = True
                    Case "DT" : If GetNum <= 1.92 Then IsAthFreak = True
                    Case "OLB" : If GetNum <= 3.57 Then IsAthFreak = True
                    Case "ILB" : If GetNum <= 3.23 Then IsAthFreak = True
                    Case "CB" : If GetNum <= 2.67 Then IsAthFreak = True
                    Case "SS" : If GetNum <= 7.69 Then IsAthFreak = True
                    Case Else : If GetNum <= 0.5 Then IsAthFreak = True
                End Select
            Case 3
                Select Case myPos
                    Case "RB", "OT" : If GetNum <= 2.08 Then IsAthFreak = True
                    Case "FB" : If GetNum <= 20.0 Then IsAthFreak = True
                    Case "WR" : If GetNum <= 1.12 Then IsAthFreak = True
                    Case "TE" : If GetNum <= 7.32 Then IsAthFreak = True
                    Case "OG" : If GetNum <= 7.69 Then IsAthFreak = True
                    Case "DE" : If GetNum <= 1.92 Then IsAthFreak = True
                    Case "DT" : If GetNum <= 3.23 Then IsAthFreak = True
                    Case "OLB" : If GetNum <= 5.77 Then IsAthFreak = True
                    Case "SS" : If GetNum <= 10.0 Then IsAthFreak = True
                    Case "FS" : If GetNum <= 3.85 Then IsAthFreak = True
                    Case Else : If GetNum <= 0.5 Then IsAthFreak = True
                End Select
            Case 4
                Select Case myPos
                    Case "FB" : If GetNum <= 10.0 Then IsAthFreak = True
                    Case "WR" : If GetNum <= 1.14 Then IsAthFreak = True
                    Case "OG" : If GetNum <= 6.52 Then IsAthFreak = True
                    Case "DE" : If GetNum <= 1.79 Then IsAthFreak = True
                    Case "OLB" : If GetNum <= 1.72 Then IsAthFreak = True
                    Case "SS" : If GetNum <= 3.85 Then IsAthFreak = True
                    Case "FS" : If GetNum <= 7.89 Then IsAthFreak = True
                    Case Else : If GetNum <= 0.5 Then IsAthFreak = True
                End Select
            Case 5
                Select Case myPos
                    Case "FB" : If GetNum <= 4.76 Then IsAthFreak = True
                    Case "WR" : If GetNum <= 2.6 Then IsAthFreak = True
                    Case "TE" : If GetNum <= 4.44 Then IsAthFreak = True
                    Case "OT" : If GetNum <= 3.57 Then IsAthFreak = True
                    Case "OC", "C" : If GetNum <= 9.09 Then IsAthFreak = True
                    Case "OG" : If GetNum <= 5.77 Then IsAthFreak = True
                    Case "CB" : If GetNum <= 2.44 Then IsAthFreak = True
                    Case "DT" : If GetNum <= 2.56 Then IsAthFreak = True
                    Case "ILB" : If GetNum <= 5.88 Then IsAthFreak = True
                    Case "OLB" : If GetNum <= 4.84 Then IsAthFreak = True
                    Case "SS" : If GetNum <= 3.45 Then IsAthFreak = True
                    Case "FS" : If GetNum <= 6.25 Then IsAthFreak = True
                    Case Else : If GetNum <= 0.5 Then IsAthFreak = True
                End Select
            Case 6
                Select Case myPos
                    Case "QB" : If GetNum <= 9.3 Then IsAthFreak = True
                    Case "RB" : If GetNum <= 1.85 Then IsAthFreak = True
                    Case "WR" : If GetNum <= 2.25 Then IsAthFreak = True
                    Case "OT" : If GetNum <= 5.77 Then IsAthFreak = True
                    Case "OC", "C" : If GetNum <= 10.71 Then IsAthFreak = True
                    Case "DE" : If GetNum <= 3.57 Then IsAthFreak = True
                    Case "DT" : If GetNum <= 6.67 Then IsAthFreak = True
                    Case "CB" : If GetNum <= 1.33 Then IsAthFreak = True
                    Case "SS" : If GetNum <= 8.0 Then IsAthFreak = True
                    Case "FS" : If GetNum <= 2.33 Then IsAthFreak = True
                    Case Else : If GetNum <= 0.5 Then IsAthFreak = True
                End Select
            Case 7
                Select Case myPos
                    Case "FB" : If GetNum <= 7.69 Then IsAthFreak = True
                    Case "WR" : If GetNum <= 5.31 Then IsAthFreak = True
                    Case "TE" : If GetNum <= 8.47 Then IsAthFreak = True
                    Case "OT" : If GetNum <= 2.94 Then IsAthFreak = True
                    Case "OC", "C" : If GetNum <= 11.54 Then IsAthFreak = True
                    Case "OG" : If GetNum <= 2.04 Then IsAthFreak = True
                    Case "DE" : If GetNum <= 3.75 Then IsAthFreak = True
                    Case "DT" : If GetNum <= 6.94 Then IsAthFreak = True
                    Case "OLB" : If GetNum <= 4.69 Then IsAthFreak = True
                    Case "CB" : If GetNum <= 7.06 Then IsAthFreak = True
                    Case "SS" : If GetNum <= 2.78 Then IsAthFreak = True
                    Case "FS" : If GetNum <= 3.7 Then IsAthFreak = True
                    Case Else : If GetNum <= 0.5 Then IsAthFreak = True
                End Select
            Case Else
                Select Case myPos
                    Case "QB" : If GetNum <= 1.45 Then IsAthFreak = True
                    Case "RB" : If GetNum <= 1.87 Then IsAthFreak = True
                    Case "FB" : If GetNum <= 2.22 Then IsAthFreak = True
                    Case "WR" : If GetNum <= 0.82 Then IsAthFreak = True
                    Case "TE" : If GetNum <= 2.56 Then IsAthFreak = True
                    Case "OT" : If GetNum <= 2.46 Then IsAthFreak = True
                    Case "OC", "C" : If GetNum <= 4.41 Then IsAthFreak = True
                    Case "OG" : If GetNum <= 2.1 Then IsAthFreak = True
                    Case "DE" : If GetNum <= 2.29 Then IsAthFreak = True
                    Case "DT" : If GetNum <= 3.2 Then IsAthFreak = True
                    Case "OLB" : If GetNum <= 3.92 Then IsAthFreak = True
                    Case "CB" : If GetNum <= 2.24 Then IsAthFreak = True
                    Case "FS" : If GetNum <= 2.53 Then IsAthFreak = True
                    Case Else : If GetNum <= 0.5 Then IsAthFreak = True
                End Select
        End Select
        Return IsAthFreak 'returns true or false
    End Function

    ''' <summary>
    ''' We are going to grade the players based on certain formula which will give them an "Actual Grade"
    ''' 50% Key Ratings, 25% other ratings, 25% Combnie
    ''' </summary>
    ''' <param name="playerNum"></param>
    Private Function GetActualGrade(playerNum As Integer, pos As String) As Single
        Dim Grade As Single
        If pos = "QB" Then ActualGrade.Combine += (DraftDT.Rows(playerNum).Item("InterviewSkills") * 2.5)
        ActualGrade.OtherRatings = ActualGrade.OtherRatings / (OtherRatingsCount - 4)
        Grade = (((ActualGrade.KeyRatings * 0.65) / 9.85) + ((ActualGrade.Combine * 0.2) / 4) + (ActualGrade.OtherRatings * 0.15)) / 9.25
        'Returns the grade calculated
        Return Math.Round(Grade, 2)
    End Function

    ''' <summary>
    ''' Common Position switches include:
    ''' QB ---> WR ---Typically very athletic QB's that aren't good enough at QB for the NFL(Julian Edelman for example)
    ''' QB ---> RB ---Typically very athletic QB's that are option type QB's in college who do a lot of running(Michael Robinson for example)
    ''' DE ---> OLB ---Typically "smaller", athletic DE's in college are too small to play DE in the NFL(Jerry Hughes for example)
    ''' CB ---> SF ---Typically slower type CB's in college that have good ball and football instincts but lack the speed to cover WR's or hands to catch the ball in the NFL(Jairius Byrd, Aaron Williams, and Devin McCourty for example)
    ''' WR ---> SF ---Typically slower type WR's that are good playing the ball but lack hands needed at WR(George Wilson for example)
    ''' OT ---> OG ---Typically "smaller" OT's in college that are the size of guards in the NFL(one of the most common--numerous examples)
    ''' LB ---> SF ---Typically "smaller" LB's in college that are athletic and fast enough to play safety but don't have enough size to play LB(Adam Archuleta for example)
    ''' FB ---> TE ---Typically the more athletic FB's in college in a run heavy offensive scheme can make more use of their skills as a TE or H-Back(Charles Clay for example)
    ''' other examples and less common changes occur---
    '''
    ''' Need to figure out how often and under what circumstances a player would have a different position---currently it sets it to the same position as they are in college
    '''
    ''' on offense: QB > WR(RB) > RB > FB > TE > OT > OG > OC
    ''' QB--->WR: Athleticism, speed, quickness, catching ability/QB Traits: Arm strength, height,
    '''
    '''
    '''
    '''
    ''' On Defense: CB > S > LB > DE > DT
    '''
    ''' players are able To move up Or down 1 slot - so starting from most athletic To least athletic defensively you'd have CB > S > LB > DE > DT.
    ''' So all a corner could do Is move DOWN. Safeties can move UP Or DOWN to corner Or LB. Any player moving UP would need to have considerable athleticism.
    '''</summary>
    ''' <param name="pos"></param>
    ''' <returns></returns>
    Public Shared Function GetNFLPos(ByVal pos As String, ByVal playerNum As Integer) As String '####TODO: Determine how often and what percentage of players would play a different position in the NFL than in college(I'm thinking maybe 5-7%, most common is OT to OG and CB to SF
        'Players who are too small/light/slow for their current college positions
        'can be projected to play a different position in the NFL

        Select Case pos
            Case "QB"
                If DraftDT.Rows(playerNum).Item("Athleticism") > 7.0 And DraftDT.Rows(playerNum).Item("FortyYardTime") < 4.5 And DraftDT.Rows(playerNum).Item("QAB") > 7.0 And
                   DraftDT.Rows(playerNum).Item("COD") > 7.0 And DraftDT.Rows(playerNum).Item("ShortAcc") < 6.0 And DraftDT.Rows(playerNum).Item("QBDecMaking") < 6.0 Then
                    'change pos to WR
                    DraftDT.Rows(playerNum).Item("NFLPos") = "WR"
                Else : Return "'QB'"
                End If
            Case "RB" : Return "'RB'"
            Case "FB" : Return "'FB'"
            Case "TE" : Return "'TE'"
            Case "WR" : Return "'WR'"
            Case "OT" : Return "'OT'"
            Case "OG" : Return "'OG'"
            Case "C" : Return "'C'"
            Case "DE" : Return "'DE'"
            Case "DT" : Return "'DT'"
            Case "OLB" : Return "'OLB'"
            Case "ILB" : Return "'ILB'"
            Case "SF" : Return "'SF'"
            Case "CB" : Return "'CB'"
            Case "P" : Return "'P'"
            Case "K" : Return "'K'"
        End Select

        Return pos
    End Function

    Public Shared Function GetSkillsTranslate(ByVal pos As String) As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB" 'QB 53% Bust 33% ProBowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 53 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 54 To 66 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "RB" 'RB Skills 49% Bust 36% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 49 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 50 To 63 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "FB" 'FB Skills usually translate pretty well to the NFL as well..
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 30 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 31 To 75 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "WR" 'WR 45% Bust 31% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 45 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 46 To 68 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "TE" 'TE Skills are fairly translatable
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 35 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 36 To 67 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "OT", "C", "OG" '31% Bust 26% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 31 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 32 To 73 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "DE" '31% Bust 33% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 31 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 32 To 66 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "DT" '33% Bust 40% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 33 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 34 To 59 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "OLB", "ILB" '16% Bust 26% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 16 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 17 To 73 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "CB" '29% Bust 23% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 29 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 30 To 77 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "FS", "SS" '11% Bust 53% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 11 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 12 To 46 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 45 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 46 To 76 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 40 : Result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 41 To 76 : Result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Result = CInt(MT.GetGaussian(85, 5))
                End Select
        End Select
        Return Result
    End Function

    Public Shared Function GetWonderlic(ByVal pos As String, DraftRound As String) As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(27.78947368, 7.87103655), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(31.2, 8.700574694), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(26.44444444, 4.362084109), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(29.625, 4.838461975), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(27.5, 4.767679649), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(27.94117647, 5.425294787), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(25.53846154, 8.099540981), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(26.5, 6.921832467), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(27.21428571, 5.924896253), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(25.73333333, 10.16623731), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(25.2, 9.118624908), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(24.66666667, 8.07101251), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(25.16, 8.07101251), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(25.65333333, 8.07101251), 2)
                End Select

                ActualGrade.Combine += Result * 1.5
            Case "RB" : Result = CInt(MT.GetGaussian(18.68, 6.1561))
            Case "FB" : Result = CInt(MT.GetGaussian(19.5313, 6.13384))
            Case "WR" : Result = CInt(MT.GetGaussian(19.6814, 6.14898))
            Case "TE" : Result = CInt(MT.GetGaussian(24.0909, 8.18485))
            Case "OT" : Result = CInt(MT.GetGaussian(24.881, 7.15376))
            Case "C" : Result = CInt(MT.GetGaussian(26.9697, 6.28844))
            Case "OG" : Result = CInt(MT.GetGaussian(24.0339, 6.09241))
            Case "DE" : Result = CInt(MT.GetGaussian(20.1348, 7.59389))
            Case "DT" : Result = CInt(MT.GetGaussian(19.4054, 7.94052))
            Case "OLB" : Result = CInt(MT.GetGaussian(20.2623, 5.85872))
            Case "ILB" : Result = CInt(MT.GetGaussian(22.4286, 6.27976))
            Case "CB" : Result = CInt(MT.GetGaussian(18.4881, 5.74351))
            Case "FS" : Result = CInt(MT.GetGaussian(21.0213, 5.71854))
            Case "SS" : Result = CInt(MT.GetGaussian(19.2553, 5.38109))
            Case "K" : Result = CInt(MT.GetGaussian(24.32, 6.21431))
            Case "P" : Result = CInt(MT.GetGaussian(25.875, 5.38952))
        End Select
        Return Result
    End Function

    Public Shared Function GetBenchPress(ByVal pos As String, ByVal DraftRound As String) As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB" : Result = CInt(MT.GetGaussian(18.19047619, 4.214487))
            Case "RB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(20.125, 3.943801647), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(15, 4.358898944), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(23, 4.147288271), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(20.28571429, 4.496640993), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(21.38461538, 3.753001498), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(19.78378378, 4.950505783), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(18.52941176, 5.080759554), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(18.22580645, 3.159215909), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(19.06666667, 4.013613199), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(20.73469388, 5.41516201), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(19.68602826, 4.816515542), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(18.63736264, 4.217869074), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(18.26461538, 4.217869074), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(17.89186813, 4.217869074), 2)
                End Select

            Case "FB"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    'Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(19.5, 2.121320344), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(24.5, 5.196152423), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(23.30769231, 5.893281703), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(24.26315789, 4.369618662), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(22.92307692, 5.392302206), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(23.78947368, 4.950633488), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(22.88283208, 5.21946372), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(21.97619048, 5.488293952), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(21.53666667, 5.488293952), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(21.09714286, 5.488293952), 2)
                End Select

                ActualGrade.Combine += (100 - ((37 - Result) * (100 / 31))) * 1.4
            Case "WR"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(17.4, 2.966479395), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(16.16666667, 3.763863264), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(13.375, 5.235524261), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(15.61538462, 3.775766181), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(16.82352941, 4.063664477), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(14.87755102, 3.711651997), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(13.84782609, 4.381615082), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(13.75862069, 5.186900894), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(15, 3.825460278), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(14.31914894, 4.22264281), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(13.96577602, 4.109670409), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(13.6124031, 3.996698007), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(13.34015504, 3.996698007), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(13.06790698, 3.996698007), 2)
                End Select

            Case "TE"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(27, 5.196152423), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(22, 0), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(23.33333333, 4.472135955), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(21.82758621, 4.293424919), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(22, 4.530939391), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(21.3125, 5.324547535), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(21.14634146, 3.678049589), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(20.14285714, 3.687362018), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(18.6, 4.855871413), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(18.76698113, 4.621290245), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(18.93396226, 4.386709078), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(18.55528302, 4.386709078), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(18.17660377, 4.386709078), 2)
                End Select

                ActualGrade.Combine += (100 - ((35 - Result) * (100 / 33))) * 1.1
            Case "OT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(29.38461538, 3.863039855), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(27.44444444, 6.38574802), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(25.31578947, 4.096782363), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(25.66666667, 2.990180006), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(25.02380952, 4.561172802), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(25.78378378, 5.677123367), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(25.23809524, 4.552188023), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(24.14583333, 4.806863813), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(25.24390244, 4.282408714), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(23.33928571, 5.36411235), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(23.1140873, 5.129000992), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(22.88888889, 4.893889635), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(22.43111111, 4.893889635), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(21.97333333, 4.893889635), 2)
                End Select

                ActualGrade.Combine += (100 - ((40 - Result) * (100 / 32))) * 1.4
            Case "C"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(27.33333333, 2.309401077), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(24.2, 4.024922359), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(26.6875, 4.045058714), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(24.21428571, 4.693343427), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(26.85, 7.198501306), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(28.25, 5.063877679), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(26.45, 4.198684004), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(28.33333333, 5.615105677), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(26.81818182, 5.43136348), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(25.3030303, 5.247621284), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(24.7969697, 5.247621284), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(24.29090909, 5.247621284), 2)
                End Select

                ActualGrade.Combine += (100 - ((42 - Result) * (100 / 30))) * 1.4
            Case "OG"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(23, 3.5333), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(35, 3.53333), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(27, 3.535533906), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(27.55555556, 4.275251779), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(28.13043478, 4.855216013), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(26.82857143, 5.136293664), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(26.63157895, 4.840264973), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(26.44186047, 6.776367638), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(25.29032258, 5.242095945), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(24.175, 4.70617434), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(24.27192623, 4.747295764), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(24.36885246, 4.788417188), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(23.88147541, 4.788417188), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(23.39409836, 4.788417188), 2)
                End Select

                ActualGrade.Combine += (100 - ((45 - Result) * (100 / 37))) * 1.4
            Case "DE"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(26.33333333, 5.291502622), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(24.375, 6.232117274), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(23.8, 5.84537604), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(25.57142857, 4.467341811), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(23.69565217, 5.5372258), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(24.22222222, 4.562141089), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(23.71111111, 4.957190472), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(23.07692308, 4.515380822), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(23.48888889, 4.760740117), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(24.35483871, 4.875824518), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(23.17335431, 4.785300007), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(21.99186992, 4.694775497), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(21.55203252, 4.694775497), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(21.11219512, 4.694775497), 2)
                End Select

            Case "DT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(26.5, 4.041451884), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(29.4, 4.159326869), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(28.9, 7.21766838), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(29.04761905, 3.27835615), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(28.47826087, 6.561636417), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(27.24528302, 4.984227663), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(27.2195122, 6.080757334), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(27.03333333, 4.334968745), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(26.95238095, 4.903718753), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(26.39130435, 6.244920641), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(26.21648551, 5.681343212), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(26.04166667, 5.117765783), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(25.52083333, 5.117765783), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(25, 5.117765783), 2)
                End Select
                ActualGrade.Combine += (100 - ((51 - Result) * (100 / 44))) * 1.4
            Case "OLB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(22.4, 2.408318916), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(25.5, 8.22597512), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(24, 5.366563146), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(24, 3.905124838), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(22.07894737, 3.906900195), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(22.8125, 4.569353276), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(23.57777778, 5.516458387), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(22.59259259, 5.608344586), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(22.42553191, 4.721572513), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(21.85714286, 4.396968653), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(21.12743506, 4.286407692), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(20.39772727, 4.175846731), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(19.98977273, 4.175846731), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(19.58181818, 4.175846731), 2)
                End Select

                ActualGrade.Combine += (100 - ((41 - Result) * (100 / 32))) * 0.7
            Case "ILB"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(22.25, 4.645786622), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(18.5, 4.949747468), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(20.66666667, 2.732520204), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(23.23076923, 2.997434801), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(22.9, 3.707866754), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(22.46153846, 4.263620708), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(23.34482759, 4.849711789), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(22.15, 4.934038593), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(22.34615385, 3.084701706), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(21.94136961, 4.275543173), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(21.53658537, 5.46638464), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(21.10585366, 5.46638464), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(20.67512195, 5.46638464), 2)
                End Select

                ActualGrade.Combine += (100 - ((36 - Result) * (100 / 29))) * 1.4
            Case "CB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(14.25, 3.304037934), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(15, 3.251373336), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(15.29411765, 3.196965473), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(16.65384615, 4.480556284), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(14.71666667, 4.318237504), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(14.64556962, 3.915904371), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(14.50793651, 4.56449487), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(14.015625, 4.08439612), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(14.8, 4.519095288), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(15.23728814, 4.983901612), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(14.28531073, 4.603514697), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(13.33333333, 4.223127782), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(13.06666667, 4.223127782), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(12.8, 4.223127782), 2)
                End Select

            Case "FS"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(19, 2.55645), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(18, 2.5453454), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(17.33333333, 0.577350269), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(13.75, 3.774917218), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(15.83333333, 3.655450566), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(16.25, 4.883423603), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(17.23333333, 3.673608893), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(17.72413793, 3.250236824), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(16.72222222, 4.657473939), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(16.30434783, 3.746935507), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(15.82140468, 3.978663935), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(15.33846154, 4.210392362), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(15.03169231, 4.210392362), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(14.72492308, 4.210392362), 2)
                End Select

            Case "SS"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(17.5, 3.31662479), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(19.6, 6.542170894), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(16.66666667, 3.881580434), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(17.08695652, 4.066638588), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(19.66666667, 4.670066789), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(18.95652174, 4.626782319), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(17.75, 4.326360053), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(17.92307692, 4.232051088), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(16.62068966, 5.115898141), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(16.88292547, 4.730517948), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(17.14516129, 4.345137754), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(16.80225806, 4.345137754), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(16.45935484, 4.345137754), 2)
                End Select

            Case "P", "K" : Result = Math.Round(MT.GetGaussian(15.5, 6.024948133), 2)
            Case "LS" : Result = Math.Round(MT.GetGaussian(18.6, 7.503332593), 2)
        End Select
        Return Result
    End Function

    Public Shared Function Get3Cone(ByVal pos As String, ByVal DraftRound As String) As Double
        Dim Result As Double
        Select Case pos
            Case "QB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(7.025625, 0.171735019), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(6.83, 0.055677644), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(7.055555556, 0.152242497), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(7.101428571, 0.248825814), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.071875, 0.193845944), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.19, 0.26925824), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.1395, 0.205079574), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.1125, 0.224736561), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.239354839, 0.269207918), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.172, 0.245348302), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.154623188, 0.248669845), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.137246377, 0.251991387), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.279991304, 0.251991387), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.422736232, 0.251991387), 2)

                End Select

            Case "RB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(7.034, 0.281211664), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(6.83, 0.367695526), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(6.976666667, 0.118462371), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(6.988333333, 0.135117233), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.0075, 0.231743393), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.023611111, 0.209763851), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.061282051, 0.180031861), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.007741935, 0.225325094), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.05125, 0.178701692), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.142291667, 0.226943048), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.121613123, 0.20941405), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.100934579, 0.191885052), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.242953271, 0.191885052), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.384971963, 0.191885052), 2)
                End Select

                ActualGrade.Combine += (100 - (100 * (Result - 6.5) * (100 / 114))) * 0.8
            Case "FB"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    'Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.035, 0.3165632), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.075, 0.318198052), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.385625, 0.434434019), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.234375, 0.195105741), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.264166667, 0.255288013), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.2425, 0.18509457), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.2565625, 0.191432214), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.270625, 0.197769858), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.4160375, 0.197769858), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.56145, 0.197769858), 2)
                End Select

                ActualGrade.Combine += (100 - (100 * (Result - 7.04) * (100 / 65))) * 0.7
            Case "WR"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(6.925, 0.172336879), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(6.971428571, 0.19377208), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(6.931111111, 0.215025838), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(6.95, 0.191930977), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(6.947719298, 0.218975027), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(6.926811594, 0.193181511), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(6.922424242, 0.195212394), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(6.895614035, 0.190937701), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(6.97546875, 0.215943332), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(6.956666667, 0.19179038), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(6.970266667, 0.210796354), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(6.983866667, 0.229802327), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.123544, 0.229802327), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.263221333, 0.229802327), 2)

                End Select

                ActualGrade.Combine += (100 - (100 * (Result - 6.3) * (100 / 122))) * 0.7
            Case "TE"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(7.245, 0.346482323), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(6.985, 0.190918831), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(6.987142857, 0.122299398), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.149615385, 0.236058989), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.1424, 0.163714182), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.093333333, 0.216901254), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.231282051, 0.19811187), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.131428571, 0.231752011), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.20725, 0.282969487), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.212534091, 0.241497201), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.217818182, 0.200024914), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.362174545, 0.200024914), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.506530909, 0.200024914), 2)

                End Select

                ActualGrade.Combine += (100 - (100 * (Result - 6.76) * (100 / 82))) * 0.8
            Case "OT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(7.665, 0.39306488), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(7.71125, 0.227246719), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(7.671052632, 0.316875278), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(7.69, 0.295296461), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.8145, 0.274506083), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.803947368, 0.24089405), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.847435897, 0.283768975), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.8366, 0.299902093), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.815121951, 0.321404122), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.8386, 0.28107672), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.890716667, 0.29650199), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.942833333, 0.31192726), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(8.10169, 0.31192726), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(8.260546667, 0.31192726), 2)

                End Select

            Case "C"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(7.756666667, 0.395769293), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(7.642, 0.295584167), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.591428571, 0.233793372), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.759, 0.24465168), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.748095238, 0.238948092), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.74625, 0.274483281), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.566111111, 0.178955758), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.620625, 0.254098898), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.72703125, 0.255027581), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.8334375, 0.255956264), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.99010625, 0.255956264), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(8.146775, 0.255956264), 2)

                End Select

            Case "OG"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(7.18, 0.155512), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(7.78, 0.152521313), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(7.82, 0.155241747), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(7.728888889, 0.257509439), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.777692308, 0.252623161), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.76, 0.251846514), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.983333333, 0.350122003), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.81575, 0.263748982), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.851428571, 0.359492588), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.833428571, 0.335392157), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.90306044, 0.338831604), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.972692308, 0.342271051), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(8.132146154, 0.342271051), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(8.2916, 0.342271051), 2)
                End Select

            Case "DE"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(7.14, 0.223215143), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(7.2575, 0.310931779), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(7.235, 0.221228627), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(7.279047619, 0.198315525), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.252765957, 0.252463533), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.290222222, 0.24575014), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.321666667, 0.248015702), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.350526316, 0.299936456), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.376052632, 0.248775808), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.312142857, 0.269961036), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.340478208, 0.257725996), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.368813559, 0.245490956), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.516189831, 0.245490956), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.663566102, 0.245490956), 2)

                End Select

                ActualGrade.Combine += (100 - (100 * (Result - 6.8) * (100 / 116))) * 1.1
            Case "DT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(7.675, 0.219203102), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(7.7125, 0.185719322), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(7.584, 0.332368126), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(7.576153846, 0.298957591), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.655789474, 0.238753877), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.712941176, 0.2730223), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.715714286, 0.261143292), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.6224, 0.272049015), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.747674419, 0.260574797), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.721463415, 0.288613591), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.732850351, 0.291194635), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.744237288, 0.293775679), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.899122034, 0.293775679), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(8.05400678, 0.293775679), 2)

                End Select

            Case "OLB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(7.03, 0.276947648), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(7.032, 0.204621602), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(7.005294118, 0.188351986), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(7.124, 0.248649687), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.117027027, 0.204693061), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.145714286, 0.241409912), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.070540541, 0.197708342), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.12106383, 0.23043658), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.1565, 0.204657939), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.127, 0.260484755), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.1717, 0.24898047), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.2164, 0.237476186), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.360728, 0.237476186), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.505056, 0.237476186), 2)

                End Select

                ActualGrade.Combine += (100 - (100 * (Result - 6.64) * (100 / 114))) * 0.8
            Case "ILB"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(7.2675, 0.318472919), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(7.265, 0.049497475), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(7.235, 0.225721067), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(7.231428571, 0.242884452), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.082222222, 0.249111241), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.222222222, 0.272697823), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(7.196923077, 0.242516296), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.178888889, 0.256466691), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.166086957, 0.189490684), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.196633222, 0.214717645), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.227179487, 0.239944607), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.371723077, 0.239944607), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.516266667, 0.239944607), 2)

                End Select

                ActualGrade.Combine += (100 - (100 * (Result - 6.68) * (100 / 77))) * 0.8
            Case "CB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(6.8, 0.192873015), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(6.855, 0.197917732), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(6.838947368, 0.215507018), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(6.903333333, 0.151833242), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(6.915098039, 0.197183899), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(6.906811594, 0.180053038), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(6.982586207, 0.212871433), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(6.927118644, 0.194883262), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(6.999333333, 0.223082159), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(6.978166667, 0.183704734), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(6.986702381, 0.177898384), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(6.995238095, 0.172092034), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.135142857, 0.172092034), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.275047619, 0.172092034), 2)

                End Select

                ActualGrade.Combine += (100 - (100 * (Result - 6.48) * (100 / 86))) * 0.8
            Case "FS"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #DIV/0!	,   #DIV/0!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(7.11, 0.14511), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(6.9525, 0.143845982), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(6.938, 0.192795228), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(6.935185185, 0.192181212), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(6.912105263, 0.22621472), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.02, 0.212554253), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(6.961904762, 0.168659985), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(7.040285714, 0.226734954), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(6.993809524, 0.211435006), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.037718715, 0.200119949), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(7.081627907, 0.188804891), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.223260465, 0.188804891), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.364893023, 0.188804891), 2)

                End Select

                ActualGrade.Combine += (100 - (100 * (Result - 6.47) * (100 / 89))) * 0.8
            Case "SS"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(6.96, 0.14532323), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(6.823333333, 0.247857486), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(6.8425, 0.145459043), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(6.953157895, 0.211346283), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(7.005833333, 0.222116199), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(7.032222222, 0.204236183), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(6.9975, 0.242830531), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(6.935714286, 0.173014641), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(7.036896552, 0.212621452), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(7.018053539, 0.178331571), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(6.999210526, 0.14404169), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(7.139194737, 0.14404169), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(7.279178947, 0.14404169), 2)
                End Select
                ActualGrade.Combine += (100 - (100 * (Result - 6.63) * (100 / 93))) * 0.8

            Case "P", "K", "LS" : Result = Math.Round(MT.GetGaussian(7.066, 0.18928814), 2)

        End Select
        Return Result
    End Function

    Public Shared Function GetVertJump(ByVal pos As String, ByVal DraftRound As String) As Double
        Dim Result As Double
        Dim NumString As String = ""
        Dim Num As Integer
        Dim NumStr As Double
        Select Case pos
            Case "QB"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(32.5, 3.31265516), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(32.83333333, 2.56580072), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(33.77777778, 2.73988037), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(34, 2.516611478), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(31.76315789, 2.474135208), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(31.19565217, 3.08861226), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(32.33333333, 3.441014343), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(30.67241379, 4.038378082), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(30.1969697, 3.7289815), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(31.25, 3.117273056), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(31.05921053, 3.099093077), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(30.86842105, 3.080913098), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(30.25105263, 3.080913098), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(29.63368421, 3.080913098), 2)
                End Select

            Case "RB"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(34.94444444, 3.9086798), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(37.33333333, 1.258305739), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(36.75, 2.62202212), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(36.2, 3.069434569), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(35.69047619, 3.262784201), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(34.88888889, 2.736306587), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(34.98039216, 2.887837918), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(34.55405405, 3.10864338), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(35.12244898, 3.329994076), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(34.45, 3.717375089), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(34.19484925, 3.2230409), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(33.93969849, 2.728706711), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(33.26090452, 2.728706711), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(32.58211055, 2.728706711), 2)
                End Select

            Case "FB"
                Select Case DraftRound
                    'Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    'Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(33.75, 3.181980515), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(34, 4.654746681), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(32.61111111, 3.229257589), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(33.31578947, 2.461991777), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(33.15625, 2.055227076), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(32.05882353, 3.8522816), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(32.40731874, 3.496297841), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(32.75581395, 3.140314082), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(32.10069767, 3.140314082), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(31.4455814, 3.140314082), 2)
                End Select

            Case "WR"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(36.57142857, 3.396426694), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(36.28125, 2.121074806), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(37.07142857, 2.487330534), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(36.92, 2.498666311), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(36.55, 3.300362299), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(35.96103896, 2.805969006), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(35.83125, 2.777558975), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(35.5234375, 3.103287667), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(35.07236842, 3.14611289), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(35.58988764, 2.857965143), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(35.01139404, 2.909181573), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(34.43290043, 2.960398003), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(33.74424242, 2.960398003), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(33.05558442, 2.960398003), 2)
                End Select

            Case "TE"
                Select Case DraftRound
                    'Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : NumString = Math.Round(MT.GetGaussian(37, 7.071067812), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(31.5, 2.121320344), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(36.03846154, 1.952152001), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(33.89655172, 2.193249832), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(34.34722222, 3.03743182), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(33.45454545, 3.148322064), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(33.25, 2.820331238), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(33.01470588, 2.80282675), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(33.13043478, 3.844815821), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(32.35456924, 3.582889359), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(31.5787037, 3.320962897), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(30.94712963, 3.320962897), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(30.31555556, 3.320962897), 2)
                End Select

            Case "OT"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(29.21428571, 3.023715784), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(29.16666667, 2.291287847), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(28.86956522, 3.094046304), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(29.46875, 2.747536776), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(29.44444444, 2.862814537), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(28.81818182, 2.799690409), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(27.90697674, 2.962575725), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(28.23469388, 2.824930015), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(28.08695652, 2.612585666), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(27.98181818, 2.917005752), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(27.225, 2.954014998), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(26.46818182, 2.991024243), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(25.93881818, 2.991024243), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(25.40945455, 2.991024243), 2)
                End Select

            Case "OC", "C"
                Select Case DraftRound
                    'Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(29.66666667, 1.040833), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(29.4, 1.816590212), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(28.39285714, 3.369424577), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(28.90909091, 3.884701931), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(29.13636364, 2.960782627), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(29.2, 3.038640047), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(27.78571429, 2.736655936), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(28.55882353, 2.962697003), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(28.03344402, 2.869598325), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(27.50806452, 2.776499647), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(26.95790323, 2.776499647), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(26.40774194, 2.776499647), 2)
                End Select

            Case "OG"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(32.5, 2.855), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(27, 2.888), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(28, 3.29772649), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(29.22222222, 2.13762589), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(29.21153846, 2.768295782), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(28.09459459, 2.818189524), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(27.68918919, 3.116602832), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(28.44565217, 2.700263898), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(27, 2.822528417), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(27.56578947, 2.477387062), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(27.19830827, 2.871943921), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(26.83082707, 3.266500779), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(26.29421053, 3.266500779), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(25.75759398, 3.266500779), 2)
                End Select

            Case "DE"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(35.125, 3.517004149), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(33.3, 3.359894178), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(33.89130435, 3.107748826), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(32.75, 2.022198238), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(33.05102041, 2.944353526), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(34.01041667, 3.307775468), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(33.29807692, 2.949342923), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(32.8625, 2.835189814), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(33.54545455, 2.895100476), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(33.28688525, 2.787560188), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(32.52490814, 3.108315526), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(31.76293103, 3.429070863), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(31.12767241, 3.429070863), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(30.49241379, 3.429070863), 2)
                End Select

            Case "DT"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(29.625, 4.441752657), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(29, 2.483277404), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(30.58333333, 2.504407879), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(30.13888889, 2.554152073), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(29.5106383, 3.111146443), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(29.5625, 3.049683292), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(29.01111111, 2.410886504), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(28.46428571, 2.837998028), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(29.38541667, 3.167722155), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(30.23913043, 3.329563085), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(29.5399192, 3.311364004), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(28.84070796, 3.293164923), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(28.26389381, 3.293164923), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(27.68707965, 3.293164923), 2)
                End Select

            Case "OLB"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(37.3, 3.07408523), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(38.66666667, 3.516627172), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(35.52777778, 3.041246949), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(35.95454545, 2.850039872), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(35.15957447, 3.749067106), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(35.44444444, 3.558316479), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(34.54166667, 2.763426283), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(34.74107143, 3.012083349), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(34.26041667, 2.893480536), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(33.85, 2.839678769), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(33.3728022, 3.117679713), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(32.8956044, 3.395680658), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(32.23769231, 3.395680658), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(31.57978022, 3.395680658), 2)
                End Select

            Case "ILB"
                Select Case DraftRound
                    'Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : NumString = Math.Round(MT.GetGaussian(34.75, 2.5), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(37, 2.828427125), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(34.5, 2.607680962), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(33.91666667, 3.491574423), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(33.45, 2.478702386), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(33.23214286, 2.470125739), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(33.67307692, 2.845847177), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(33, 2.677063067), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(33.09615385, 2.416688771), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(32.77508842, 2.784249526), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(32.45402299, 3.151810281), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(31.80494253, 3.151810281), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(31.15586207, 3.151810281), 2)
                End Select

            Case "CB"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(39.125, 2.594063736), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(36.65, 1.901023116), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(37.06818182, 2.546217805), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(37.33928571, 2.870841828), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(36.26984127, 2.870631881), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(36.55882353, 2.885113438), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(35.93571429, 2.948742582), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(35.3943662, 2.671162792), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(35.79032258, 2.847990933), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(36.55882353, 2.969407966), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(35.95851624, 2.880054729), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(35.35820896, 2.790701491), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(34.65104478, 2.790701491), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(33.9438806, 2.790701491), 2)
                End Select

            Case "FS"
                Select Case DraftRound
                    Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(43, 2.5955), 2)
                    Case "R1Top10" : NumString = Math.Round(MT.GetGaussian(37.5, 2.656), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(37.75, 2.753785274), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(34.6, 2.162174831), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(36.25862069, 3.299406574), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(37.17391304, 2.753546043), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(36.48484848, 2.821471382), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(35.32142857, 3.240982606), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(35.77142857, 2.324241993), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(36.13043478, 3.653197203), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(35.34378882, 3.292727573), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(34.55714286, 2.932257943), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(33.866, 2.932257943), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(33.17485714, 2.932257943), 2)
                End Select

            Case "SS"
                Select Case DraftRound
                    'Case "R1Top5" : NumString = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : NumString = Math.Round(MT.GetGaussian(35.4, 4.669047012), 2)
                    Case "R1MidFirst" : NumString = Math.Round(MT.GetGaussian(37.33333333, 1.154700538), 2)
                    Case "R1LateFirst" : NumString = Math.Round(MT.GetGaussian(36.25, 1.573213272), 2)
                    Case "R2" : NumString = Math.Round(MT.GetGaussian(36.60869565, 3.082046699), 2)
                    Case "R3" : NumString = Math.Round(MT.GetGaussian(36.73529412, 2.6286739), 2)
                    Case "R4" : NumString = Math.Round(MT.GetGaussian(36.41304348, 2.782462014), 2)
                    Case "R5" : NumString = Math.Round(MT.GetGaussian(35.47619048, 3.156248527), 2)
                    Case "R6" : NumString = Math.Round(MT.GetGaussian(36.32352941, 2.639490815), 2)
                    Case "R7" : NumString = Math.Round(MT.GetGaussian(34.80645161, 3.226653115), 2)
                    Case "PUFA" : NumString = Math.Round(MT.GetGaussian(34.64186217, 2.799328096), 2)
                    Case "LUFA" : NumString = Math.Round(MT.GetGaussian(34.47727273, 2.372003078), 2)
                    Case "PracSquad" : NumString = Math.Round(MT.GetGaussian(33.78772727, 2.372003078), 2)
                    Case "Reject" : NumString = Math.Round(MT.GetGaussian(33.09818182, 2.372003078), 2)
                End Select

            Case "K", "P", "LS" : NumString = Math.Round(MT.GetGaussian(30.76470588, 2.856558501), 2)
        End Select

        NumStr = CInt(NumString)
        If NumString = NumStr Then : Result = NumStr
        Else

            Num = Regex.Match(NumString, "(?<=\d+\.)\d").Value
            If Num < 4 Then : Result = NumStr
            ElseIf Num = 4 Then : Result = CDbl(NumString) + 0.1
            ElseIf Num = 5 Then : Result = CDbl(NumString)
            ElseIf Num = 6 Then : Result = CDbl(NumString) - 0.1
            ElseIf Num = 7 Then : Result = CDbl(NumString) - 0.2
            ElseIf Num = 8 Then : Result = CDbl(NumString) + 0.2
            ElseIf Num = 9 Then : Result = CDbl(NumString) + 0.1
            End If
        End If
        Select Case pos
            Case "FB" : ActualGrade.Combine += (100 - (2 * (40 - Result) * (100 / 30))) * 0.8
            Case "WR" : ActualGrade.Combine += (100 - (2 * (45 - Result) * (100 / 39))) * 0.8
            Case "OT" : ActualGrade.Combine += (100 - (2 * (35.5 - Result) * (100 / 33))) * 0.7
            Case "C" : ActualGrade.Combine += (100 - (2 * (37.5 - Result) * (100 / 34))) * 0.7
            Case "OG" : ActualGrade.Combine += (100 - (2 * (36 - Result) * (100 / 37))) * 0.7
            Case "DE" : ActualGrade.Combine += (100 - (2 * (42 - Result) * (100 / 36))) * 0.7
            Case "DT" : ActualGrade.Combine += (100 - (2 * (37 - Result) * (100 / 34))) * 0.7
            Case "CB" : ActualGrade.Combine += (100 - (2 * (45 - Result) * (100 / 34))) * 1.1
            Case "FS" : ActualGrade.Combine += (100 - (2 * (46 - Result) * (100 / 37))) * 1.1
            Case "SS" : ActualGrade.Combine += (100 - (2 * (41.5 - Result) * (100 / 33))) * 1.1
        End Select
        Return Result
    End Function

    Public Shared Function GetBroadJump(ByVal pos As String, ByVal DraftRound As String) As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(115, 6.186005712), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(116.3333333, 3.214550254), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(116.1111111, 4.01386486), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(112.75, 4.949747468), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(111.2105263, 4.916852509), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(110.3478261, 6.759563566), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(113.8, 6.582272844), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(109.8148148, 7.290863814), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(108.90625, 8.719127564), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(109.3846154, 5.557531273), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(109.4309441, 6.06269941), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(109.4772727, 6.567867546), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(107.2877273, 6.567867546), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(105.0981818, 6.567867546), 2)
                End Select

            Case "RB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(122.1666667, 5.154286242), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(124.6666667, 3.214550254), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(122.6666667, 4.676180778), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(122.7272727, 6.388910848), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(119.9210526, 4.766899264), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(120.5609756, 5.991864403), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(119.2553191, 5.565687133), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(118.9142857, 4.692744265), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(119.1836735, 6.190293038), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(117.8541667, 7.892872042), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(116.9244792, 6.458447687), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(115.9947917, 5.024023331), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(113.6748958, 5.024023331), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(111.355, 5.024023331), 2)
                End Select

            Case "FB"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    'Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(110, 4.242640687), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(116.5, 8.020806277), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(111.2222222, 5.7451315), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(113.2105263, 4.391645444), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(115.0625, 5.470755585), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(111.7647059, 6.02629044), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(112.227591, 6.208965843), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(112.6904762, 6.391641246), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(110.4366667, 6.391641246), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(108.1828571, 6.391641246), 2)
                End Select

                ActualGrade.Combine += (100 - ((134 - Result) * (100 / 35))) * 1.1
            Case "WR"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(127.6666667, 6.831300511), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(122.5833333, 6.229815893), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(122, 3.668043819), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(123.5, 4.433077251), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(122.9090909, 5.576674699), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(121.164557, 5.400451975), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(120.8311688, 5.613429849), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(120.0806452, 5.795131895), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(120.375, 5.620479404), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(121.1481481, 5.775186385), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(119.8273208, 5.563114324), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(118.5064935, 5.351042263), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(116.1363636, 5.351042263), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(113.7662338, 5.351042263), 2)
                End Select

            Case "TE"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(122.6666667, 4.618802154), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(119.5, 2.121320344), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(122.4166667, 6.374071531), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(116.5925926, 4.13483568), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(117.6060606, 4.513447919), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(114.4545455, 5.788409265), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(115.1666667, 5.016638981), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(115.1470588, 5.297983338), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(114.3695652, 7.622652486), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(113.1477456, 6.845172467), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(111.9259259, 6.067692449), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(109.6874074, 6.067692449), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(107.4488889, 6.067692449), 2)
                End Select

                ActualGrade.Combine += (100 - ((131 - Result) * (100 / 34))) * 0.7
            Case "OT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(106.8461538, 6.914310689), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(106, 6.800735254), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(105.952381, 5.554063292), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(104.5625, 4.732423622), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(103.2727273, 6.662507512), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(104.1666667, 5.418651919), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(101.1363636, 4.953696807), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(103.0204082, 5.78968118), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(102.8444444, 5.807344705), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(102.4423077, 5.958725556), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(100.8036781, 6.534321402), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(99.16504854, 7.109917249), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(97.18174757, 7.109917249), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(95.1984466, 7.109917249), 2)
                End Select

                ActualGrade.Combine += (100 - ((118 - Result) * (100 / 45))) * 1.1
            Case "C"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(102.5, 7.141428429), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(102.2, 3.962322551), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(103.4285714, 5.50124861), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(102.2, 5.553777493), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(103.5909091, 5.551997418), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(102.3, 5.49848464), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(101.3809524, 6.094884662), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(101.8235294, 6.607148535), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(101.0055147, 6.676993945), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(100.1875, 6.746839354), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(98.18375, 6.746839354), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(96.18, 6.746839354), 2)
                End Select

                ActualGrade.Combine += (100 - ((117 - Result) * (100 / 33))) * 1.1
            Case "OG"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(107, 1.41112), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(109, 1.414213562), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(102.4, 8.018728079), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(100, 5.454356057), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(104.4230769, 5.954313239), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(102.3428571, 5.687669104), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(101.0857143, 7.228910212), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(102, 6.210590034), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(100.516129, 6.937199088), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(99.34210526, 5.905912364), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(98.59667247, 6.210422706), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(97.85123967, 6.514933048), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(95.89421488, 6.514933048), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(93.93719008, 6.514933048), 2)
                End Select

                ActualGrade.Combine += (100 - ((119 - Result) * (100 / 38))) * 1.1

            Case "DE"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(120, 6.884765791), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(118.2, 7.899367063), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(116.3913043, 6.867175749), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(116, 5.119621697), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(114.7959184, 6.438748558), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(117.875, 5.583505343), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(114.9411765, 6.001372392), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(114.925, 6.533493588), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(113.3636364, 5.640760748), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(114.1612903, 5.997796176), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(113.1902943, 5.725981826), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(112.2192982, 5.454167477), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(109.9749123, 5.454167477), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(107.7305263, 5.454167477), 2)
                End Select

                ActualGrade.Combine += (100 - ((139 - Result) * (100 / 39))) * 0.8
            Case "DT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(106.25, 5.795112884), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(107.25, 3.862210075), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(109.5, 6.617712418), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(105, 5.412404054), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(106.3863636, 6.269957565), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(105.5, 5.005451573), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(104.5106383, 5.140936898), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(103.3846154, 5.755532455), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(105.1956522, 5.733409167), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(105.3333333, 7.698878313), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(104.7166667, 7.077008185), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(104.1, 6.455138057), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(102.018, 6.455138057), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(99.936, 6.455138057), 2)
                End Select

                ActualGrade.Combine += (100 - ((124 - Result) * (100 / 51))) * 1.1
            Case "OLB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(121, 7.071067812), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(125, 3.949683532), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(120.2777778, 7.168604389), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(121, 7.019453488), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(119.2045455, 6.105961804), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(120.1555556, 5.865754061), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(117.6734694, 4.753892068), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(117.5849057, 6.033349504), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(116.5416667, 5.712838596), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(115.78, 6.392885076), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(115.2921739, 5.689552968), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(114.8043478, 4.986220861), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(112.5082609, 4.986220861), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(110.2121739, 4.986220861), 2)
                End Select

            Case "ILB"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(115.5, 5.802298395), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(118.5, 0.707106781), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(119.8333333, 5.706721184), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(115.3333333, 5), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(114.6785714, 5.375207637), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(114.0357143, 7.089283215), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(116.3333333, 5.298766184), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(115.1428571, 3.395375006), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(111.44, 9.811048194), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(112.2082353, 7.153507943), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(112.9764706, 4.495967692), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(110.7169412, 4.495967692), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(108.4574118, 4.495967692), 2)
                End Select

            Case "CB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(130.25, 6.130524719), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(122.4444444, 2.788866755), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(124.826087, 5.228003042), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(125.1724138, 6.216734131), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(122.7903226, 5.322815404), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(122.3139535, 5.982841402), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(121.5072464, 5.320944157), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(120, 5.758834874), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(121.0793651, 5.436737599), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(121.625, 5.429197959), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(120.3655303, 5.012338971), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(119.1060606, 4.595479983), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(116.7239394, 4.595479983), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(114.3418182, 4.595479983), 2)
                End Select

            Case "FS"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(130, 6.130524719), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(123, 2.788866755), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(124, 9.899494937), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(119.4, 5.412947441), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(121.1428571, 4.7587291), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(122, 6.195236265), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(121.2727273, 5.227766775), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(121.4615385, 6.198262784), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(121.3235294, 5.558071326), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(120.5454545, 6.981112428), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(119.6495389, 5.997026953), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(118.7536232, 5.012941479), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(116.3785507, 5.012941479), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(114.0034783, 5.012941479), 2)
                End Select

            Case "SS"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(122.8, 6.300793601), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(124.3333333, 6.658328118), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(123.5, 3.391164992), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(123.4090909, 6.709333165), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(121.1764706, 6.043956632), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(121.7727273, 5.263819166), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(120.0909091, 5.991335736), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(121.4705882, 7.459202765), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(118.5333333, 4.732378081), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(118.3636816, 4.71501016), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(118.1940299, 4.697642239), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(115.8301493, 4.697642239), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(113.4662687, 4.697642239), 2)
                End Select

            Case "P", "K", "LS" : Result = Math.Round(MT.GetGaussian(113.0833333, 5.63202), 2)
        End Select
        Return Result
    End Function

    Public Shared Function GetShortShuttle(ByVal pos As String, ByVal DraftRound As String, freak As List(Of Integer)) As Double
        Dim Result As Double = MT.GenerateDouble(0, 100)

        If freak.Contains(3) Then
            Select Case pos
                Case "QB" : Result = CaseLookup({1.25, 2.5, 3.75, 5.0, 6.25, 7.5, 8.75, 10, 11.25, 12.5, 15, 17.5, 20, 25, 30, 35, 40, 45, 50, 60, 75, 100.1}, {3.87, 3.88, 3.89, 3.9, 3.91, 3.92, 3.93, 3.94, 3.95, 3.96, 3.97,
                                                3.98, 3.99, 4.0, 4.01, 4.02, 4.03, 4.04, 4.05, 4.06, 4.07, 4.08}, 0, 100)
                Case "RB" : Result = CaseLookup({1.087, 2.174, 3.261, 4.348, 6.522, 8.696, 10.87, 13.043, 15.217, 17.391, 19.891, 22.391, 24.891, 27.065, 29.239, 31.413, 34.848, 39.196, 43.544, 47.892, 60.935, 69.631, 78.326, 87.022, 100.1},
                                                {3.82, 3.83, 3.84, 3.85, 3.86, 3.87, 3.88, 3.89, 3.9, 3.91, 3.92, 3.93, 3.94, 3.95, 3.96, 3.97, 3.98, 3.99, 4, 4.01, 4.02, 4.03, 4.04, 4.05, 4.06}, 0, 100)
                Case "FB" : Result = CaseLookup({1.429, 2.857, 4.286, 5.714, 7.143, 8.571, 10, 11.429, 12.857, 14.286, 28.571, 35.714, 42.857, 57.143, 61.857, 66.571, 71.286, 100.1}, {3.95, 3.96, 3.97, 3.98, 3.99, 4.0, 4.01, 4.02,
                                                4.03, 4.04, 4.05, 4.06, 4.07, 4.08, 4.09, 4.1, 4.11, 4.12}, 0, 100)
                Case "WR" : Result = CaseLookup({0.917, 1.833, 2.75, 3.667, 4.583, 5.5, 6.417, 7.333, 8.25, 9.167, 10.083, 11, 11.917, 14.694, 17.472, 20.25, 25.111, 27.889, 33.444, 39, 47.333, 55.667, 66.778, 80.667, 83.444, 100.1},
                                                {3.73, 3.74, 3.75, 3.76, 3.77, 3.78, 3.79, 3.8, 3.81, 3.82, 3.83, 3.84, 3.85, 3.86, 3.87, 3.88, 3.89, 3.9, 3.91, 3.92, 3.93, 3.94, 3.95, 3.96, 3.97, 3.98}, 0, 100)

                Case "TE"
                    Select Case Result

                    End Select
            End Select
        Else
            Select Case pos
                Case "QB"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.274705882, 0.139692414), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.153333333, 0.094516313), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.221, 0.122515305), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.2175, 0.131121536), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.333333333, 0.12792461), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.301904762, 0.218417468), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.325909091, 0.133796544), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.251851852, 0.199557916), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.356969697, 0.163735397), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.3225, 0.159380595), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.343945313, 0.166798028), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.365390625, 0.174215462), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.387217578, 0.174215462), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.409044531, 0.174215462), 2)
                    End Select

                Case "RB"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.133333333, 0.141656862), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.31, 0.127279221), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.318, 0.182400658), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.2525, 0.18979312), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.214736842, 0.139569548), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.277297297, 0.155503638), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.242142857, 0.145711638), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.234666667, 0.17446931), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.313953488, 0.174043502), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.308695652, 0.159089195), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.303133953, 0.154763081), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.297572254, 0.150436967), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.319060116, 0.150436967), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.340547977, 0.150436967), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 3.82) * (100 / 92))) * 0.7
                Case "FB"
                    Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    'Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.7, 0.05), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.306666667, 0.115470054), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.401176471, 0.241140062), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.314705882, 0.133936442), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.281333333, 0.160395345), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.396875, 0.155637131), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.409621711, 0.152483893), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.422368421, 0.149330655), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.444480263, 0.149330655), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.466592105, 0.149330655), 2)
                    End Select

                Case "WR"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.215, 0.152019736), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.1675, 0.085481828), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.138888889, 0.181689601), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.242, 0.163049879), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.198615385, 0.17121694), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.192027027, 0.119633913), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.207230769, 0.142099302), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.178275862, 0.123999688), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.229384615, 0.16514082), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.224375, 0.137756378), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.236886574, 0.14526781), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.249398148, 0.152779241), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.270645139, 0.152779241), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.29189213, 0.152779241), 2)
                    End Select

                Case "TE"
                    Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                        Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(4.31, 0.197989899), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.265, 0.148492424), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.345555556, 0.240214025), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.356666667, 0.151352873), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.330666667, 0.170939258), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.336, 0.155687574), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.373846154, 0.134274177), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.369117647, 0.171682717), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.376666667, 0.16453792), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.40540404, 0.153843846), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.434141414, 0.143149772), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.456312121, 0.143149772), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.478482828, 0.143149772), 2)
                    End Select

                Case "OT"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.722307692, 0.21319607), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.69125, 0.198813445), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.643, 0.166198993), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.644666667, 0.14302181), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.725714286, 0.183464529), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.698571429, 0.137921353), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.77, 0.157897775), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.773061224, 0.180706992), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.719268293, 0.202106287), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.750943396, 0.155690326), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.790618757, 0.168142954), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.830294118, 0.180595582), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.854445588, 0.180595582), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.878597059, 0.180595582), 2)
                    End Select

                Case "C"
                    Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.7825, 0.201886932), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.606, 0.155016128), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.546428571, 0.188173698), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.659166667, 0.204737452), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.693181818, 0.152077247), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.577777778, 0.068516016), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.57, 0.184133819), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.564117647, 0.223188788), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.628994307, 0.196042238), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.693870968, 0.168895688), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.717340323, 0.168895688), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.740809677, 0.168895688), 2)
                    End Select

                Case "OG"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.57, 0.05), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.84, 0.05), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.744, 0.122188379), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.688888889, 0.169885582), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.705769231, 0.205371333), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.666857143, 0.193508951), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.801111111, 0.252097235), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.739302326, 0.183425025), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.771612903, 0.18751172), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.739714286, 0.17597603), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.801404762, 0.194452026), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.863095238, 0.212928023), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.887410714, 0.212928023), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.91172619, 0.212928023), 2)
                    End Select

                Case "DE"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.363333333, 0.180277564), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.458, 0.228122774), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.3865, 0.189882928), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.393333333, 0.148166573), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.405957447, 0.179245515), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.419090909, 0.188196065), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.425365854, 0.150467564), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.421463415, 0.171691016), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.4295, 0.169689762), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.431147541, 0.168850016), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.452979431, 0.170096671), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.474811321, 0.171343326), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.497185377, 0.171343326), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.519559434, 0.171343326), 2)
                    End Select

                Case "DT"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.585, 0.162788206), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.63, 0.129871732), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.565, 0.160457679), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.615, 0.186690475), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.624782609, 0.180366776), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.651132075, 0.184980876), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.623777778, 0.19810797), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.627037037, 0.195247669), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.634772727, 0.204512906), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.639302326, 0.191403281), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.663105708, 0.202979778), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.686909091, 0.214556275), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.710343636, 0.214556275), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.733778182, 0.214556275), 2)
                    End Select

                Case "OLB"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.206, 0.216748702), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.241666667, 0.089758936), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.236842105, 0.181721785), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.235454545, 0.185653636), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.285526316, 0.150129626), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.282045455, 0.148896398), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.269782609, 0.149940568), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.282244898, 0.160328638), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.326666667, 0.14021088), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.336428571, 0.128211362), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.342294745, 0.142418295), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.34816092, 0.156625228), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.369901724, 0.156625228), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.391642529, 0.156625228), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 3.83) * (100 / 90))) * 0.7
                Case "ILB"
                    Select Case DraftRound

                        Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(4.29, 0.120277457), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.43, 0.042426407), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.295, 0.207822039), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.2672, 0.11670904), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.274285714, 0.141772603), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.32137931, 0.168432355), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.301153846, 0.170747227), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.3495, 0.185230298), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.328076923, 0.138910597), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.348163462, 0.148850859), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.36825, 0.158791121), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.39009125, 0.158791121), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.4119325, 0.158791121), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 3.89) * (100 / 87))) * 0.7
                Case "CB"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.026666667, 0.178978583), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.063, 0.235751753), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.127222222, 0.115595927), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.0975, 0.162728101), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.140714286, 0.129542652), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.144210526, 0.145558814), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.196290323, 0.151703268), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.195151515, 0.172691298), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.203220339, 0.154020159), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.226349206, 0.124942588), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.229059559, 0.135959723), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.231769912, 0.146976857), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.252928761, 0.146976857), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.274087611, 0.146976857), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 3.75) * (100 / 102))) * 1.1
                Case "FS"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.23, 0.05), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.36, 0.05), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.09, 0.212132034), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.196, 0.103344085), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.178461538, 0.144767187), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.205238095, 0.174086733), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.162413793, 0.148026426), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.224782609, 0.139963293), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.221714286, 0.154167322), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.236, 0.121499404), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.240076923, 0.144050233), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.244153846, 0.166601061), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.265374615, 0.166601061), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.286595385, 0.166601061), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 3.83) * (100 / 90))) * 0.7
                Case "SS"
                    Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                        Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(4.13, 0.152326323), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.18, 0.174355958), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.145, 0.137961347), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(4.157777778, 0.165762252), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(4.157692308, 0.159799554), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(4.196666667, 0.130473497), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(4.245789474, 0.201282438), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(4.232142857, 0.124602665), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(4.258846154, 0.147548688), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.264740537, 0.150005492), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.270634921, 0.152462296), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.291988095, 0.152462296), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(4.31334127, 0.152462296), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 3.86) * (100 / 80))) * 0.7
            End Select
        End If
        Return Result
    End Function

    Public Shared Function Get20Time(ByVal pos As String) As Double
        Dim Result As Double
        Select Case pos
            Case "QB" : Result = Math.Round(MT.GetGaussian(2.8, 0.094), 2)
            Case "RB" : Result = Math.Round(MT.GetGaussian(2.63, 0.082), 2)
            Case "FB" : Result = Math.Round(MT.GetGaussian(2.74, 0.0776), 2)
            Case "WR" : Result = Math.Round(MT.GetGaussian(2.62, 0.0715), 2)
            Case "TE" : Result = Math.Round(MT.GetGaussian(2.76, 0.0889), 2)
            Case "OT" : Result = Math.Round(MT.GetGaussian(3.04, 0.1187), 2)
            Case "OG" : Result = Math.Round(MT.GetGaussian(3.01, 0.0839), 2)
            Case "C" : Result = Math.Round(MT.GetGaussian(3.07, 0.1067), 2)
            Case "DE" : Result = Math.Round(MT.GetGaussian(2.8, 0.086), 2)
            Case "DT" : Result = Math.Round(MT.GetGaussian(2.96, 0.0906), 2)
            Case "OLB" : Result = Math.Round(MT.GetGaussian(2.7, 0.0809), 2)
            Case "ILB" : Result = Math.Round(MT.GetGaussian(2.75, 0.0885), 2)
            Case "CB" : Result = Math.Round(MT.GetGaussian(2.595, 0.0704), 2)
            Case "FS" : Result = Math.Round(MT.GetGaussian(2.644, 0.0675), 2)
            Case "SS" : Result = Math.Round(MT.GetGaussian(2.649, 0.08177), 2)
        End Select
        Return Result
    End Function

    Private Shared Function Get10Time(ByVal pos As String, DraftRound As String, freak As List(Of Integer)) As Double
        Dim Result As Double

        If freak.Contains(2) Then
            Select Case pos
                Case "QB" : Result = CDbl(CaseLookup({11.765, 17.647, 23.529, 29.412, 52.941, 70.588, 100.1}, {1.53, 1.54, 1.55, 1.56, 1.57, 1.58, 1.59}, 0, 100))
                Case "RB" : Result = CDbl(CaseLookup({0.952, 1.905, 2.857, 4.286, 5.714, 11.429, 14.286, 20, 28.571, 42.857, 60, 100.1}, {1.4, 1.41, 1.42, 1.43, 1.44, 1.45, 1.46, 1.47, 1.48, 1.49, 1.5, 1.51}, 0, 100))
                Case "FB" : Result = CDbl(CaseLookup({12.5, 37.5, 50, 100.1}, {1.55, 1.56, 1.57, 1.58}, 0, 100))
                Case "WR" : Result = CDbl(CaseLookup({1.351, 2.703, 5.405, 10.811, 16.216, 21.622, 32.432, 54.054, 75.676, 100.1}, {1.4, 1.41, 1.42, 1.43, 1.44, 1.45, 1.46, 1.47, 1.48, 1.49}, 0, 100))
                Case "TE" : Result = CDbl(CaseLookup({1.389, 2.778, 4.167, 5.556, 11.111, 16.667, 22.222, 50, 55.556, 66.667, 100.1}, {1.46, 1.47, 1.48, 1.49, 1.5, 1.51, 1.52, 1.53, 1.54, 1.55, 1.56}, 0, 100))
                Case "OT" : Result = CDbl(CaseLookup({0.893, 1.786, 3.571, 5.357, 7.143, 10.714, 14.286, 17.857, 28.571, 39.286, 64.286, 71.429, 100.1}, {1.59, 1.6, 1.61, 1.62, 1.63, 1.64, 1.65, 1.66, 1.67, 1.68, 1.69, 1.7, 1.71}, 0, 100))
                Case "OC", "C" : Result = CDbl(CaseLookup({4.125, 8.25, 12.5, 25, 37.5, 87.5, 100.1}, {1.64, 1.65, 1.66, 1.67, 1.68, 1.69, 1.7}, 0, 100))
                Case "OG" : Result = CDbl(CaseLookup({2.2, 4.4, 6.6, 8.8, 11, 13.35, 20.017, 53.35, 73.35, 80.017, 100.1}, {1.61, 1.62, 1.63, 1.64, 1.65, 1.66, 1.67, 1.68, 1.69, 1.7, 1.71}, 0, 100))
                Case "DE" : Result = CDbl(CaseLookup({3.125, 6.25, 12.5, 15.625, 25, 40.625, 56.25, 100.1}, {1.51, 1.52, 1.53, 1.54, 1.55, 1.56, 1.57, 1.58}, 0, 100))
                Case "DT" : Result = CDbl(CaseLookup({1.19, 2.381, 3.81, 5.238, 6.667, 8.095, 9.524, 19.048, 33.333, 38.095, 52.381, 61.905, 100.1}, {1.55, 1.56, 1.57, 1.58, 1.59, 1.6, 1.61, 1.62, 1.63, 1.64, 1.65, 1.66, 1.67}, 0, 100))
                Case "OLB" : Result = CDbl(CaseLookup({4.167, 8.333, 12.5, 16.667, 20.833, 33.333, 45.833, 75, 100.1}, {1.46, 1.47, 1.48, 1.49, 1.5, 1.51, 1.52, 1.53, 1.54}, 0, 100))
                Case "ILB" : Result = CDbl(CaseLookup({6.25, 12.5, 25, 31.25, 37.5, 56.25, 100.1}, {1.51, 1.52, 1.53, 1.54, 1.55, 1.56, 1.57}, 0, 100))
                Case "CB" : Result = CDbl(CaseLookup({3.448, 6.897, 13.793, 51.724, 100.1}, {1.43, 1.44, 1.45, 1.46, 1.47}, 0, 100))
                Case "FS" : Result = CDbl(CaseLookup({3.333, 6.667, 13.333, 20, 26.667, 40, 60, 100.1}, {1.44, 1.45, 1.46, 1.47, 1.48, 1.49, 1.5, 1.51}, 0, 100))
                Case "SS" : Result = CDbl(CaseLookup({2.273, 4.545, 6.818, 9.091, 45.455, 72.727, 100.1}, {1.45, 1.46, 1.47, 1.48, 1.49, 1.5, 1.51}, 0, 100))
            End Select
        Else
            Select Case pos
                Case "QB"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.662777778, 0.062666771), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.643333333, 0.02081666), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.648888889, 0.062538877), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.63375, 0.053167525), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.670555556, 0.052184314), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.714782609, 0.081901802), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.679565217, 0.050854361), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.691111111, 0.067158443), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.685666667, 0.067858033), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.669583333, 0.062864877), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.684150641, 0.065078408), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.698717949, 0.06729194), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.707211538, 0.06729194), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.715705128, 0.06729194), 2)
                    End Select

                Case "RB"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.555, 0.040355563), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.556666667, 0.011547005), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.591666667, 0.03920034), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.548461538, 0.065554831), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.579318182, 0.046275099), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.579069767, 0.053577445), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.582857143, 0.045643546), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.591666667, 0.040602604), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.595833333, 0.056862407), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.594897959, 0.068041729), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.605920054, 0.064310047), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.616942149, 0.060578365), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.62502686, 0.060578365), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.63311157, 0.060578365), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 1.44) * (100 / 23))) * 1.1
                Case "FB"
                    Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    'Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    'Case "R2" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.595, 0.007071068), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.657333333, 0.062731476), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.640555556, 0.056928461), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.644285714, 0.040518616), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.674, 0.065334305), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.663578947, 0.058704126), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.653157895, 0.052073947), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.661423684, 0.052073947), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.669689474, 0.052073947), 2)
                    End Select

                Case "WR"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.552, 0.034928498), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.551538462, 0.061623339), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.56, 0.040676104), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.563478261, 0.066442641), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.560606061, 0.050623156), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.558734177, 0.054428666), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.576533333, 0.049168583), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.570461538, 0.046281434), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.579473684, 0.044748027), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.577108434, 0.049743982), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.588015295, 0.048477879), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.598922156, 0.047211775), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.606916766, 0.047211775), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.614911377, 0.047211775), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 1.4) * (100 / 30))) * 1.1
                Case "TE"
                    Select Case DraftRound
                        Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(1.575, 0.077781746), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.655, 0.148492424), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.63375, 0.040333432), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.646, 0.06264982), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.636363636, 0.050300235), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.659032258, 0.049352801), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.654146341, 0.060455587), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.681428571, 0.066780715), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.668444444, 0.070901839), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.676980843, 0.067020564), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.685517241, 0.063139289), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.693944828, 0.063139289), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.702372414, 0.063139289), 2)
                    End Select

                Case "OT"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.77, 0.078624539), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.791, 0.06384878), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.802105263, 0.065453881), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.80625, 0.060318599), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.816444444, 0.061243017), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.7965, 0.076344362), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.827727273, 0.062834727), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.81877551, 0.058367823), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.817857143, 0.083506556), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.818571429, 0.069816085), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.843464819, 0.070906748), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.868358209, 0.071997412), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.8777, 0.071997412), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.887041791, 0.071997412), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 1.61) * (100 / 29))) * 0.8
                Case "OG"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.8, 0.05), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.78, 0.070710678), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.776, 0.070922493), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.83, 0.043011626), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.809615385, 0.052572295), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.821764706, 0.082405891), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.839411765, 0.064804656), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.834680851, 0.074712523), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.817, 0.064655586), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.82625, 0.072944964), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.847421875, 0.080513112), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.86859375, 0.088081261), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.877936719, 0.088081261), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.887279688, 0.088081261), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 1.68) * (100 / 25))) * 0.8
                Case "C"
                    Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.8125, 0.047871355), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.802, 0.052630789), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.793333333, 0.068521807), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.804285714, 0.062722133), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.811904762, 0.055282823), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.845, 0.055025247), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.820952381, 0.062442354), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.784705882, 0.078351583), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.814171123, 0.068379232), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.843636364, 0.05840688), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.852854545, 0.05840688), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.862072727, 0.05840688), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 1.68) * (100 / 20))) * 0.8
                Case "DE"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.623, 0.066674999), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.649, 0.082117802), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.665238095, 0.052308608), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.654285714, 0.038544964), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.665208333, 0.051653081), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.663829787, 0.055150119), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.670784314, 0.060061406), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.671315789, 0.062867129), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.685227273, 0.067254762), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.684444444, 0.057886925), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.699207516, 0.058010288), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.713970588, 0.058133652), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.722540441, 0.058133652), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.731110294, 0.058133652), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 1.53) * (100 / 23))) * 1.4
                Case "DT"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.7375, 0.005), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.8275, 0.017078251), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.73875, 0.061305247), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.74625, 0.053150729), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.767333333, 0.065031461), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.759272727, 0.06466021), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.767608696, 0.065732954), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.764615385, 0.06034771), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.771, 0.067044), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.76, 0.062547543), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.7825, 0.058976846), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.805, 0.055406148), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.814025, 0.055406148), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.82305, 0.055406148), 2)
                    End Select

                    ActualGrade.Combine += (100 - (100 * (Result - 1.63) * (100 / 26))) * 0.8
                Case "OLB"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.606, 0.027018512), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.601666667, 0.041190614), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.623333333, 0.048628242), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.611818182, 0.050954525), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.635909091, 0.053191981), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.613478261, 0.049585723), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.625, 0.056481821), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.638888889, 0.055377492), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.630217391, 0.058670043), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.638333333, 0.064257902), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.64975, 0.065232431), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.661166667, 0.06620696), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.6694725, 0.06620696), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.677778333, 0.06620696), 2)
                    End Select

                Case "ILB"
                    Select Case DraftRound
                        Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(1.5725, 0.046457866), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.585, 0.035355339), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.638333333, 0.067651066), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.651481481, 0.057559581), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.637307692, 0.04703681), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.65137931, 0.051459009), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.629655172, 0.046173799), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.661, 0.046555004), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.667407407, 0.051335442), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.674203704, 0.053410159), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.681, 0.055484876), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.689405, 0.055484876), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.69781, 0.055484876), 2)
                    End Select

                Case "CB"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.546666667, 0.032145503), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.539090909, 0.035903912), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.553333333, 0.053945759), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.545517241, 0.037185462), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.560322581, 0.047596933), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.566071429, 0.047870756), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.579852941, 0.061873551), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.576811594, 0.052538298), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.582295082, 0.055659484), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.561639344, 0.058855199), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.581299124, 0.064577718), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.600958904, 0.070300237), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.608963699, 0.070300237), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.616968493, 0.070300237), 2)
                    End Select

                Case "SS"
                    Select Case DraftRound

                        Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(1.58, 0.046368092), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.585, 0.021213203), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.55, 0.015491933), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.567272727, 0.042558846), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.584375, 0.042890364), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.57952381, 0.045329482), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.6075, 0.052902592), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.599411765, 0.051656387), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.592, 0.046118289), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.600883721, 0.04807077), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.609767442, 0.05002325), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.617816279, 0.05002325), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.625865116, 0.05002325), 2)
                    End Select

                Case "FS"
                    Select Case DraftRound
                        Case "R1Top5" : Result = Math.Round(MT.GetGaussian(1.58, 0.046368092), 2)
                        Case "R1Top10" : Result = Math.Round(MT.GetGaussian(1.55, 0.021213203), 2)
                        Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(1.57, 0.10984838), 2)
                        Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(1.576, 0.068044103), 2)
                        Case "R2" : Result = Math.Round(MT.GetGaussian(1.58875, 0.040466035), 2)
                        Case "R3" : Result = Math.Round(MT.GetGaussian(1.579565217, 0.043534549), 2)
                        Case "R4" : Result = Math.Round(MT.GetGaussian(1.590909091, 0.048047089), 2)
                        Case "R5" : Result = Math.Round(MT.GetGaussian(1.598888889, 0.047013364), 2)
                        Case "R6" : Result = Math.Round(MT.GetGaussian(1.57625, 0.045843352), 2)
                        Case "R7" : Result = Math.Round(MT.GetGaussian(1.6, 0.044192094), 2)
                        Case "PUFA" : Result = Math.Round(MT.GetGaussian(1.610454545, 0.054657584), 2)
                        Case "LUFA" : Result = Math.Round(MT.GetGaussian(1.620909091, 0.065123074), 2)
                        Case "PracSquad" : Result = Math.Round(MT.GetGaussian(1.629013636, 0.065123074), 2)
                        Case "Reject" : Result = Math.Round(MT.GetGaussian(1.637118182, 0.065123074), 2)
                    End Select
                'No real correlation or enough data for K, P and LS...
                Case "K", "P", "LS" : Result = Math.Round(MT.GetGaussian(1.75, 0.06585), 2)
            End Select
        End If
        Return Result
    End Function

    Private Shared Function GetExplosiveNumber(ByVal pos As String, i As Integer, dt As DataTable) As Double
        Dim Result As Integer
        Result = dt.Rows(i).Item("VerticalJump") + dt.Rows(i).Item("BenchPressReps") + (dt.Rows(i).Item("BroadJump") / 12)
        Return Result
    End Function

    ''' <summary>
    ''' Types of drafts---Poor--few good players and lack of depth, Shallow---lack of both depth and good players, Normal, Top Heavy(high quality players at the top not good depth), Deep Draft(Not as many high quality top players but more good players than normal in later
    ''' rounds, Stacked Draft(High Quality and Deep---very rare), need to be position specific
    ''' Returns an ArrayList(QBPercentageTopEnd, QBPercentageMidRound, RBPercentageTopEnd, RBPercentageMidround, WRPercentageTopend, WRPercentageMidround, TEPercentageTopEnd, TEPercentageMidRound, OTPercentageTopEnd, OTPercentageMidRound,
    ''' CPercentageTopeend, CPercentageMidRound, OGPercentageopEnd, OGPercentageMidRound, DEPercentageTopEnd, DEPercentageMidRound, DTPercentageTopEnd, DTPercentageMidRound, OLBPercentageTopEnd, OLBPercentageMidRound, ILBPercentageTopEnd, ILBPercentageMidRound,
    ''' CBPrecentageTopEnd, CBPercentageMidRound,SFPercentageTopEnd, SFPercentageidRound)
    ''' </summary>

    Public Shared Sub GenDraftClass()
        Dim Result As Integer
        Dim TopEnd As Double
        Dim MidRound As Double

        'Generates a draft class
        '-------------------------------------------------
        'Gens strength of draft class for each position
        '---------------------------------------------------
        'Poor-3%/25-35% less 1st/2nd round talent|25-35% less mid round talent
        'Shallow-10%/10-15% less 1st/2nd round talent|25-35% less mid round talent
        'LackingTopEndButDeep-10%/ 15-25% less 1st/2nd round talent|15-25% more mid round talent
        'Normal-54%/between -5 to 5% on both
        'TopHeavy-10%/ 15-25% more 1st/2nd round talent|15-25% less mid round talent
        'Deep- 10%/3-13% more 1st/2nd round talent/25-35% more mid round talent
        'Stacked-3%

        For i As Integer = 1 To 17
            Result = MT.GenerateInt32(1, 100) 'selects the type of class for each of the 16 positions

            Select Case Result
                Case 1 To 2 'poor
                    TopEnd = MT.GenerateDouble(0.65, 0.75)
                    MidRound = MT.GenerateDouble(0.65, 0.75)
                    DraftClassDesc.Add("Poor")
                Case 3 To 7 'shallow
                    TopEnd = MT.GenerateDouble(0.85, 0.9)
                    MidRound = MT.GenerateDouble(0.65, 0.75)
                    DraftClassDesc.Add("Shallow")
                Case 8 To 15 'LackingButDeep
                    TopEnd = MT.GenerateDouble(0.75, 0.85)
                    MidRound = MT.GenerateDouble(1.15, 1.25)
                    DraftClassDesc.Add("LackingButDeep")
                Case 16 To 85 'Normal
                    TopEnd = MT.GenerateDouble(0.95, 1.05)
                    MidRound = MT.GenerateDouble(0.95, 1.05)
                    DraftClassDesc.Add("Normal")
                Case 86 To 90 'TopHeavy
                    TopEnd = MT.GenerateDouble(1.15, 1.25)
                    MidRound = MT.GenerateDouble(0.75, 0.85)
                    DraftClassDesc.Add("TopHeavy")
                Case 91 To 98 'Deep
                    TopEnd = MT.GenerateDouble(1.03, 1.13)
                    MidRound = MT.GenerateDouble(1.25, 1.35)
                    DraftClassDesc.Add("Deep")
                Case 99 To 100 'Stacked
                    TopEnd = MT.GenerateDouble(1.25, 1.35)
                    MidRound = MT.GenerateDouble(1.25, 1.35)
                    DraftClassDesc.Add("Stacked")
            End Select

            DraftClassType.Add(TopEnd)
            DraftClassType.Add(MidRound)
        Next i

    End Sub

    ''' <summary>
    ''' Position numbers for PlayerDict: QB1,HB2,FB3,WR4,TE5,OT6,C7,OG8,DE9,DT10,OLB11,ILB12,CB13,FS13,SS14,K15,P16
    ''' </summary>
    Public Shared Function GetDraftRound(ByVal pos As String) As String
        Dim TopEnd As Double
        Dim MidRound As Double
        Dim Result As String = ""
        Dim CheckPos As Integer
        Dim DraftPosEnd(14) As Double
        Dim Remaining As Integer
        Dim RemainingPercentage(6) As Single

        Dim Count(14) As Integer

        Select Case pos
            Case "QB" : CheckPos = 0

            Case "RB" : CheckPos = 2

            Case "FB" : CheckPos = 4

            Case "WR" : CheckPos = 6

            Case "TE" : CheckPos = 8

            Case "OT" : CheckPos = 10

            Case "C" : CheckPos = 12

            Case "OG" : CheckPos = 14

            Case "DE" : CheckPos = 16

            Case "DT" : CheckPos = 18

            Case "OLB" : CheckPos = 20

            Case "ILB" : CheckPos = 22

            Case "CB" : CheckPos = 24

            Case "FS" : CheckPos = 26

            Case "SS" : CheckPos = 28

            Case "K" : CheckPos = 30

            Case "P" : CheckPos = 32

        End Select

        'These are the breakdowns of how players get created by rounds---the top 4 are all 1st round---top 5 talent, top 10 talent, mid first round talent, late first round talent
        DraftPosEnd(1) = 1 'Top 5
        DraftPosEnd(2) = 3 'Top 10
        DraftPosEnd(3) = 8 'Mid first
        DraftPosEnd(4) = 18 'Late First
        DraftPosEnd(5) = 47 '2nd
        DraftPosEnd(6) = 90 '3rd
        DraftPosEnd(7) = 155 '4th
        DraftPosEnd(8) = 235 '5th
        DraftPosEnd(9) = 335 '6th
        DraftPosEnd(10) = 500 '7th
        DraftPosEnd(11) = 850 'PFA
        DraftPosEnd(12) = 1350 'LFA
        DraftPosEnd(13) = 2000 'PracSquad
        DraftPosEnd(14) = 3000 'Reject
        Try
            Remaining = DraftPosEnd(14) - DraftPosEnd(8)
            RemainingPercentage(1) = (DraftPosEnd(9) - DraftPosEnd(8)) / Remaining
            RemainingPercentage(2) = (DraftPosEnd(10) - DraftPosEnd(9)) / Remaining
            RemainingPercentage(3) = (DraftPosEnd(11) - DraftPosEnd(10)) / Remaining
            RemainingPercentage(4) = (DraftPosEnd(12) - DraftPosEnd(11)) / Remaining
            RemainingPercentage(5) = (DraftPosEnd(13) - DraftPosEnd(12)) / Remaining
            RemainingPercentage(6) = (DraftPosEnd(14) - DraftPosEnd(13)) / Remaining

            TopEnd = DraftClassType.Item(CheckPos)
            MidRound = DraftClassType.Item(CheckPos + 1)
        Catch ex As System.ArgumentOutOfRangeException
            Console.WriteLine(ex.Data)
            Console.WriteLine(ex.Message)
        End Try
        '##### TOP ROUND MODIFIERS #####
        DraftPosEnd(1) *= TopEnd
        DraftPosEnd(2) = CInt((DraftPosEnd(2) - DraftPosEnd(1) + 1) * TopEnd)
        DraftPosEnd(3) = CInt((DraftPosEnd(3) - DraftPosEnd(2) + 1) * TopEnd)
        DraftPosEnd(4) = CInt((DraftPosEnd(4) - DraftPosEnd(3) + 1) * TopEnd)
        DraftPosEnd(5) = CInt((DraftPosEnd(5) - DraftPosEnd(4) + 1) * TopEnd)
        '##### MID ROUND MODIFIERS #####
        DraftPosEnd(6) = CInt((DraftPosEnd(6) - DraftPosEnd(5) + 1) * MidRound)
        DraftPosEnd(7) = CInt((DraftPosEnd(7) - DraftPosEnd(6) + 1) * MidRound)
        DraftPosEnd(8) = CInt((DraftPosEnd(8) - DraftPosEnd(7) + 1) * MidRound)
        '##### LATE ROUND/UFA MODIFIERS ##### Basically we are taking the remaining number of players and breaking them down according to the same percentage as they are currently
        Remaining = DraftPosEnd(14) - DraftPosEnd(8)

        DraftPosEnd(9) = CInt(RemainingPercentage(1) * Remaining) + DraftPosEnd(8)
        DraftPosEnd(10) = CInt(RemainingPercentage(2) * Remaining) + DraftPosEnd(9)
        DraftPosEnd(11) = CInt(RemainingPercentage(3) * Remaining) + DraftPosEnd(10)
        DraftPosEnd(12) = CInt(RemainingPercentage(4) * Remaining) + DraftPosEnd(11)
        DraftPosEnd(13) = CInt(RemainingPercentage(5) * Remaining) + DraftPosEnd(12)

        Dim Num As Integer = MT.GenerateInt32(1, DraftPosEnd(14))

        Select Case Num 'normal draft at 0% changes
            Case 1 To DraftPosEnd(1) 'Elite 1st---Top 5
                Result = "R1Top5"
                Count(1) += 1
                Select Case pos
                    Case "QB" : posCount(1, 1) += 1
                    Case "RB" : posCount(1, 2) += 1
                    Case "FB" : posCount(1, 3) += 1
                    Case "WR" : posCount(1, 4) += 1
                    Case "TE" : posCount(1, 5) += 1
                    Case "OT" : posCount(1, 6) += 1
                    Case "C" : posCount(1, 7) += 1
                    Case "OG" : posCount(1, 8) += 1
                    Case "DE" : posCount(1, 9) += 1
                    Case "DT" : posCount(1, 10) += 1
                    Case "OLB" : posCount(1, 11) += 1
                    Case "ILB" : posCount(1, 12) += 1
                    Case "CB" : posCount(1, 13) += 1
                    Case "FS" : posCount(1, 14) += 1
                    Case "SS" : posCount(1, 15) += 1
                    Case "K" : posCount(1, 16) += 1
                    Case "P" : posCount(1, 17) += 1
                End Select
            Case DraftPosEnd(1) + 1 To DraftPosEnd(2) 'Elite 1st---Top 10
                Result = "R1Top10"
                Count(2) += 1
                Select Case pos
                    Case "QB" : posCount(2, 1) += 1
                    Case "RB" : posCount(2, 2) += 1
                    Case "FB" : posCount(2, 3) += 1
                    Case "WR" : posCount(2, 4) += 1
                    Case "TE" : posCount(2, 5) += 1
                    Case "OT" : posCount(2, 6) += 1
                    Case "C" : posCount(2, 7) += 1
                    Case "OG" : posCount(2, 8) += 1
                    Case "DE" : posCount(2, 9) += 1
                    Case "DT" : posCount(2, 10) += 1
                    Case "OLB" : posCount(2, 11) += 1
                    Case "ILB" : posCount(2, 12) += 1
                    Case "CB" : posCount(2, 13) += 1
                    Case "FS" : posCount(2, 14) += 1
                    Case "SS" : posCount(2, 15) += 1
                    Case "K" : posCount(2, 16) += 1
                    Case "P" : posCount(2, 17) += 1
                End Select
            Case DraftPosEnd(2) + 1 To DraftPosEnd(3) 'Mid First
                Result = "R1MidFirst"
                Count(3) += 1
                Select Case pos
                    Case "QB" : posCount(3, 1) += 1
                    Case "RB" : posCount(3, 2) += 1
                    Case "FB" : posCount(3, 3) += 1
                    Case "WR" : posCount(3, 4) += 1
                    Case "TE" : posCount(3, 5) += 1
                    Case "OT" : posCount(3, 6) += 1
                    Case "C" : posCount(3, 7) += 1
                    Case "OG" : posCount(3, 8) += 1
                    Case "DE" : posCount(3, 9) += 1
                    Case "DT" : posCount(3, 10) += 1
                    Case "OLB" : posCount(3, 11) += 1
                    Case "ILB" : posCount(3, 12) += 1
                    Case "CB" : posCount(3, 13) += 1
                    Case "FS" : posCount(3, 14) += 1
                    Case "SS" : posCount(3, 15) += 1
                    Case "K" : posCount(3, 16) += 1
                    Case "P" : posCount(3, 17) += 1
                End Select
            Case DraftPosEnd(3) + 1 To DraftPosEnd(4) 'LowFirst
                Result = "R1LateFirst"
                Count(4) += 1
                Select Case pos
                    Case "QB" : posCount(4, 1) += 1
                    Case "RB" : posCount(4, 2) += 1
                    Case "FB" : posCount(4, 3) += 1
                    Case "WR" : posCount(4, 4) += 1
                    Case "TE" : posCount(4, 5) += 1
                    Case "OT" : posCount(4, 6) += 1
                    Case "C" : posCount(4, 7) += 1
                    Case "OG" : posCount(4, 8) += 1
                    Case "DE" : posCount(4, 9) += 1
                    Case "DT" : posCount(4, 10) += 1
                    Case "OLB" : posCount(4, 11) += 1
                    Case "ILB" : posCount(4, 12) += 1
                    Case "CB" : posCount(4, 13) += 1
                    Case "FS" : posCount(4, 14) += 1
                    Case "SS" : posCount(4, 15) += 1
                    Case "K" : posCount(4, 16) += 1
                    Case "P" : posCount(4, 17) += 1
                End Select
            Case DraftPosEnd(4) + 1 To DraftPosEnd(5) '2nd
                Result = "R2"
                Count(5) += 1
                Select Case pos
                    Case "QB" : posCount(5, 1) += 1
                    Case "RB" : posCount(5, 2) += 1
                    Case "FB" : posCount(5, 3) += 1
                    Case "WR" : posCount(5, 4) += 1
                    Case "TE" : posCount(5, 5) += 1
                    Case "OT" : posCount(5, 6) += 1
                    Case "C" : posCount(5, 7) += 1
                    Case "OG" : posCount(5, 8) += 1
                    Case "DE" : posCount(5, 9) += 1
                    Case "DT" : posCount(5, 10) += 1
                    Case "OLB" : posCount(5, 11) += 1
                    Case "ILB" : posCount(5, 12) += 1
                    Case "CB" : posCount(5, 13) += 1
                    Case "FS" : posCount(5, 14) += 1
                    Case "SS" : posCount(5, 15) += 1
                    Case "K" : posCount(5, 16) += 1
                    Case "P" : posCount(5, 17) += 1
                End Select
            Case DraftPosEnd(5) + 1 To DraftPosEnd(6) '3rd
                Result = "R3"
                Count(6) += 1
                Select Case pos
                    Case "QB" : posCount(6, 1) += 1
                    Case "RB" : posCount(6, 2) += 1
                    Case "FB" : posCount(6, 3) += 1
                    Case "WR" : posCount(6, 4) += 1
                    Case "TE" : posCount(6, 5) += 1
                    Case "OT" : posCount(6, 6) += 1
                    Case "C" : posCount(6, 7) += 1
                    Case "OG" : posCount(6, 8) += 1
                    Case "DE" : posCount(6, 9) += 1
                    Case "DT" : posCount(6, 10) += 1
                    Case "OLB" : posCount(6, 11) += 1
                    Case "ILB" : posCount(6, 12) += 1
                    Case "CB" : posCount(6, 13) += 1
                    Case "FS" : posCount(6, 14) += 1
                    Case "SS" : posCount(6, 15) += 1
                    Case "K" : posCount(6, 16) += 1
                    Case "P" : posCount(6, 17) += 1
                End Select
            Case DraftPosEnd(6) + 1 To DraftPosEnd(7) '4th
                Result = "R4"
                Count(7) += 1
                Select Case pos
                    Case "QB" : posCount(7, 1) += 1
                    Case "RB" : posCount(7, 2) += 1
                    Case "FB" : posCount(7, 3) += 1
                    Case "WR" : posCount(7, 4) += 1
                    Case "TE" : posCount(7, 5) += 1
                    Case "OT" : posCount(7, 6) += 1
                    Case "C" : posCount(7, 7) += 1
                    Case "OG" : posCount(7, 8) += 1
                    Case "DE" : posCount(7, 9) += 1
                    Case "DT" : posCount(7, 10) += 1
                    Case "OLB" : posCount(7, 11) += 1
                    Case "ILB" : posCount(7, 12) += 1
                    Case "CB" : posCount(7, 13) += 1
                    Case "FS" : posCount(7, 14) += 1
                    Case "SS" : posCount(7, 15) += 1
                    Case "K" : posCount(7, 16) += 1
                    Case "P" : posCount(7, 17) += 1
                End Select
            Case DraftPosEnd(7) + 1 To DraftPosEnd(8) '5th
                Result = "R5"
                Count(8) += 1
                Select Case pos
                    Case "QB" : posCount(8, 1) += 1
                    Case "RB" : posCount(8, 2) += 1
                    Case "FB" : posCount(8, 3) += 1
                    Case "WR" : posCount(8, 4) += 1
                    Case "TE" : posCount(8, 5) += 1
                    Case "OT" : posCount(8, 6) += 1
                    Case "C" : posCount(8, 7) += 1
                    Case "OG" : posCount(8, 8) += 1
                    Case "DE" : posCount(8, 9) += 1
                    Case "DT" : posCount(8, 10) += 1
                    Case "OLB" : posCount(8, 11) += 1
                    Case "ILB" : posCount(8, 12) += 1
                    Case "CB" : posCount(8, 13) += 1
                    Case "FS" : posCount(8, 14) += 1
                    Case "SS" : posCount(8, 15) += 1
                    Case "K" : posCount(8, 16) += 1
                    Case "P" : posCount(8, 17) += 1
                End Select
            Case DraftPosEnd(8) + 1 To DraftPosEnd(9) '6th
                Result = "R6"
                Count(9) += 1
                Select Case pos
                    Case "QB" : posCount(9, 1) += 1
                    Case "RB" : posCount(9, 2) += 1
                    Case "FB" : posCount(9, 3) += 1
                    Case "WR" : posCount(9, 4) += 1
                    Case "TE" : posCount(9, 5) += 1
                    Case "OT" : posCount(9, 6) += 1
                    Case "C" : posCount(9, 7) += 1
                    Case "OG" : posCount(9, 8) += 1
                    Case "DE" : posCount(9, 9) += 1
                    Case "DT" : posCount(9, 10) += 1
                    Case "OLB" : posCount(9, 11) += 1
                    Case "ILB" : posCount(9, 12) += 1
                    Case "CB" : posCount(9, 13) += 1
                    Case "FS" : posCount(9, 14) += 1
                    Case "SS" : posCount(9, 15) += 1
                    Case "K" : posCount(9, 16) += 1
                    Case "P" : posCount(9, 17) += 1
                End Select
            Case DraftPosEnd(9) + 1 To DraftPosEnd(10) '7th
                Result = "R7"
                Count(10) += 1
                Select Case pos
                    Case "QB" : posCount(10, 1) += 1
                    Case "RB" : posCount(10, 2) += 1
                    Case "FB" : posCount(10, 3) += 1
                    Case "WR" : posCount(10, 4) += 1
                    Case "TE" : posCount(10, 5) += 1
                    Case "OT" : posCount(10, 6) += 1
                    Case "C" : posCount(10, 7) += 1
                    Case "OG" : posCount(10, 8) += 1
                    Case "DE" : posCount(10, 9) += 1
                    Case "DT" : posCount(10, 10) += 1
                    Case "OLB" : posCount(10, 11) += 1
                    Case "ILB" : posCount(10, 12) += 1
                    Case "CB" : posCount(10, 13) += 1
                    Case "FS" : posCount(10, 14) += 1
                    Case "SS" : posCount(10, 15) += 1
                    Case "K" : posCount(10, 16) += 1
                    Case "P" : posCount(10, 17) += 1
                End Select
            Case DraftPosEnd(10) + 1 To DraftPosEnd(11) 'PSA
                Result = "PUFA"
                Count(11) += 1
                Select Case pos
                    Case "QB" : posCount(11, 1) += 1
                    Case "RB" : posCount(11, 2) += 1
                    Case "FB" : posCount(11, 3) += 1
                    Case "WR" : posCount(11, 4) += 1
                    Case "TE" : posCount(11, 5) += 1
                    Case "OT" : posCount(11, 6) += 1
                    Case "C" : posCount(11, 7) += 1
                    Case "OG" : posCount(11, 8) += 1
                    Case "DE" : posCount(11, 9) += 1
                    Case "DT" : posCount(11, 10) += 1
                    Case "OLB" : posCount(11, 11) += 1
                    Case "ILB" : posCount(11, 12) += 1
                    Case "CB" : posCount(11, 13) += 1
                    Case "FS" : posCount(11, 14) += 1
                    Case "SS" : posCount(11, 15) += 1
                    Case "K" : posCount(11, 16) += 1
                    Case "P" : posCount(11, 17) += 1
                End Select
            Case DraftPosEnd(11) + 1 To DraftPosEnd(12) 'PFA
                Result = "LUFA"
                Count(12) += 1
                Select Case pos
                    Case "QB" : posCount(12, 1) += 1
                    Case "RB" : posCount(12, 2) += 1
                    Case "FB" : posCount(12, 3) += 1
                    Case "WR" : posCount(12, 4) += 1
                    Case "TE" : posCount(12, 5) += 1
                    Case "OT" : posCount(12, 6) += 1
                    Case "C" : posCount(12, 7) += 1
                    Case "OG" : posCount(12, 8) += 1
                    Case "DE" : posCount(12, 9) += 1
                    Case "DT" : posCount(12, 10) += 1
                    Case "OLB" : posCount(12, 11) += 1
                    Case "ILB" : posCount(12, 12) += 1
                    Case "CB" : posCount(12, 13) += 1
                    Case "FS" : posCount(12, 14) += 1
                    Case "SS" : posCount(12, 15) += 1
                    Case "K" : posCount(12, 16) += 1
                    Case "P" : posCount(12, 17) += 1
                End Select
            Case DraftPosEnd(12) + 1 To DraftPosEnd(13) 'LFA
                Result = "PracSquad"
                Count(13) += 1
                Select Case pos
                    Case "QB" : posCount(13, 1) += 1
                    Case "RB" : posCount(13, 2) += 1
                    Case "FB" : posCount(13, 3) += 1
                    Case "WR" : posCount(13, 4) += 1
                    Case "TE" : posCount(13, 5) += 1
                    Case "OT" : posCount(13, 6) += 1
                    Case "C" : posCount(13, 7) += 1
                    Case "OG" : posCount(13, 8) += 1
                    Case "DE" : posCount(13, 9) += 1
                    Case "DT" : posCount(13, 10) += 1
                    Case "OLB" : posCount(13, 11) += 1
                    Case "ILB" : posCount(13, 12) += 1
                    Case "CB" : posCount(13, 13) += 1
                    Case "FS" : posCount(13, 14) += 1
                    Case "SS" : posCount(13, 15) += 1
                    Case "K" : posCount(13, 16) += 1
                    Case "P" : posCount(13, 17) += 1
                End Select
            Case DraftPosEnd(13) + 1 To DraftPosEnd(14) 'Reject
                Result = "Reject"
                Count(14) += 1
                Select Case pos
                    Case "QB" : posCount(14, 1) += 1
                    Case "RB" : posCount(14, 2) += 1
                    Case "FB" : posCount(14, 3) += 1
                    Case "WR" : posCount(14, 4) += 1
                    Case "TE" : posCount(14, 5) += 1
                    Case "OT" : posCount(14, 6) += 1
                    Case "C" : posCount(14, 7) += 1
                    Case "OG" : posCount(14, 8) += 1
                    Case "DE" : posCount(14, 9) += 1
                    Case "DT" : posCount(14, 10) += 1
                    Case "OLB" : posCount(14, 11) += 1
                    Case "ILB" : posCount(14, 12) += 1
                    Case "CB" : posCount(14, 13) += 1
                    Case "FS" : posCount(14, 14) += 1
                    Case "SS" : posCount(14, 15) += 1
                    Case "K" : posCount(14, 16) += 1
                    Case "P" : posCount(14, 17) += 1
                End Select
        End Select

        Return Result

    End Function

End Class