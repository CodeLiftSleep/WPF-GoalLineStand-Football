Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Public Class CollegePlayers
    Inherits Players

    'Public DraftDT As DataTable
    'Helper function to create draft class in JS
    Public Function CreatePlayers(ByVal numPlayers As Integer)

        Dim myColPlayer As New CollegePlayers
        'Initialize the DB
        Initialize("Football", DraftDT, "DraftPlayers", GetSQLString("College"))
        'Generate a Draft Class
        GenDraftClass()
        'Cycle through and create the specified number of players
        For x As Integer = 1 To numPlayers
            GenDraftPlayers(x, myColPlayer, DraftDT)
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

        draftDT.Rows.Add()
        xCollegePlayer = New CollegePlayers
        PersonalityModel(draftDT, playerNum, xCollegePlayer)
        Try

            MyPos = GetCollegePos(playerNum, draftDT)
            draftDT.Rows(playerNum).Item("CollegePOS") = String.Format("'{0}'", MyPos)
            draftDT.Rows(playerNum).Item("FortyYardTime") = Get40Time(MyPos, playerNum, draftDT)
            PosType = GetPosType(MyPos, playerNum, draftDT)
            draftDT.Rows(playerNum).Item("PosType") = String.Format("'{0}'", PosType)
            GenNames(draftDT, playerNum, "CollegePlayer", MyPos)
            'GetPersonalityStats(draftDT, playerNum, xCollegePlayer)
            DraftRound = GetDraftRound(MyPos)
            draftDT.Rows(playerNum).Item("TwentyYardTime") = Get20Time(MyPos)
            draftDT.Rows(playerNum).Item("TenYardTime") = Get10Time(MyPos)
            draftDT.Rows(playerNum).Item("ShortShuttle") = GetShortShuttle(MyPos)
            draftDT.Rows(playerNum).Item("BroadJump") = GetBroadJump(MyPos)
            draftDT.Rows(playerNum).Item("VertJump") = GetVertJump(MyPos)
            draftDT.Rows(playerNum).Item("ThreeConeDrill") = Get3Cone(MyPos)
            draftDT.Rows(playerNum).Item("BenchPress") = GetBenchPress(MyPos)
            draftDT.Rows(playerNum).Item("InterviewSkills") = CInt(MT.GetGaussian(49.5, 16.5))
            draftDT.Rows(playerNum).Item("WonderlicTest") = GetWonderlic(MyPos)
            draftDT.Rows(playerNum).Item("SkillsTranslateToNFL") = GetSkillsTranslate(MyPos)
            draftDT.Rows(playerNum).Item("ProjNFLPos") = GetNFLPos(String.Format("'{0}'", MyPos), playerNum)
            draftDT.Rows(playerNum).Item("DLPrimaryTech") = "'NONE'"
            draftDT.Rows(playerNum).Item("DLSecondaryTech") = "'NONE'"
            draftDT.Rows(playerNum).Item("DLPassRushTech") = "'NONE'"
            draftDT.Rows(playerNum).Item("RETKickReturn") = GetKickRetAbility(MyPos, playerNum)
            draftDT.Rows(playerNum).Item("RETPuntReturn") = GetPuntRetAbility(MyPos, playerNum, draftDT)
            GetSTAbility(MyPos, playerNum, draftDT)
            GetLSAbility(MyPos, playerNum, draftDT)
            GetKeyRatings(draftDT, playerNum, MyPos, DraftRound) 'Assigns base ratings based on Round drafted using an Exponential Decay Function

            For x As Integer = 0 To draftDT.Columns.Count - 1 'cycles through the columns and assigns a rating to any of them that are still NULL values
                If draftDT.Rows(playerNum).Item(x) Is DBNull.Value Then
                    draftDT.Rows(playerNum).Item(x) = MT.GetGaussian(49.5, 16.5)
                End If
            Next x
            GetPosRatings(MyPos, PosType, playerNum, draftDT) 'Will get positional skills for players based on their position type
            draftDT.Rows(playerNum).Item("DraftID") = playerNum
        Catch ex As System.InvalidCastException
            Console.WriteLine(ex.Data)
            Console.WriteLine(ex.Message)
        End Try
    End Sub

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
    Public Shared Function GetWonderlic(ByVal pos As String) As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB" : Result = CInt(MT.GetGaussian(26.678, 6.521))
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
    Public Shared Function GetBenchPress(ByVal pos As String) As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB" : Result = CInt(MT.GetGaussian(19.8, 2.85657))
            Case "RB" : Result = CInt(MT.GetGaussian(20.3148, 4.51673))
            Case "FB" : Result = CInt(MT.GetGaussian(23.4848, 4.28614))
            Case "WR" : Result = CInt(MT.GetGaussian(16.7586, 3.38001))
            Case "TE" : Result = CInt(MT.GetGaussian(21.3333, 4.45034))
            Case "OT" : Result = CInt(MT.GetGaussian(24.9524, 4.60651))
            Case "C" : Result = CInt(MT.GetGaussian(26.5313, 4.24253))
            Case "OG" : Result = CInt(MT.GetGaussian(26.4419, 4.79487))
            Case "DE" : Result = CInt(MT.GetGaussian(24.6825, 5.9089))
            Case "DT" : Result = CInt(MT.GetGaussian(28.3519, 5.78362))
            Case "OLB" : Result = CInt(MT.GetGaussian(22.5873, 4.95899))
            Case "ILB" : Result = CInt(MT.GetGaussian(22.8947, 3.85793))
            Case "CB" : Result = CInt(MT.GetGaussian(15.4, 3.32265))
            Case "FS" : Result = CInt(MT.GetGaussian(16.5, 3.59048))
            Case "SS" : Result = CInt(MT.GetGaussian(17.2333, 4.63093))
        End Select
        Return Result
    End Function
    Public Shared Function Get3Cone(ByVal pos As String) As Double
        Dim Result As Double
        Select Case pos
            Case "QB" : Result = Math.Round(MT.GetGaussian(7.16143, 0.27591), 2)
            Case "RB" : Result = Math.Round(MT.GetGaussian(7.00848, 0.16455), 2)
            Case "FB" : Result = Math.Round(MT.GetGaussian(7.30794, 0.26069), 2)
            Case "WR" : Result = Math.Round(MT.GetGaussian(6.95564, 0.16844), 2)
            Case "TE" : Result = Math.Round(MT.GetGaussian(7.15089, 0.20702), 2)
            Case "OT" : Result = Math.Round(MT.GetGaussian(7.80527, 0.30271), 2)
            Case "C" : Result = Math.Round(MT.GetGaussian(7.71718, 0.25131), 2)
            Case "OG" : Result = Math.Round(MT.GetGaussian(7.87723, 0.32694), 2)
            Case "DE" : Result = Math.Round(MT.GetGaussian(7.33298, 0.27823), 2)
            Case "DT" : Result = Math.Round(MT.GetGaussian(7.70881, 0.22825), 2)
            Case "OLB" : Result = Math.Round(MT.GetGaussian(7.12055, 0.23154), 2)
            Case "ILB" : Result = Math.Round(MT.GetGaussian(7.222, 0.21437), 2)
            Case "CB" : Result = Math.Round(MT.GetGaussian(6.984, 0.22688), 2)
            Case "FS" : Result = Math.Round(MT.GetGaussian(7.02656, 0.19514), 2)
            Case "SS" : Result = Math.Round(MT.GetGaussian(7.06844, 0.24738), 2)
        End Select
        Return Result
    End Function
    Public Shared Function GetVertJump(ByVal pos As String) As Double
        Dim Result As Double
        Dim NumString As String = ""
        Dim Num As Integer
        Dim NumStr As Double
        Select Case pos
            Case "QB" : NumString = CStr(Math.Round(MT.GetGaussian(30.551, 3.69258), 1))
            Case "RB" : NumString = CStr(Math.Round(MT.GetGaussian(33.3657, 2.86198), 1))
            Case "FB" : NumString = CStr(Math.Round(MT.GetGaussian(32.0172, 2.91395), 1))
            Case "WR" : NumString = CStr(Math.Round(MT.GetGaussian(34.3807, 3.20614), 1))
            Case "TE" : NumString = CStr(Math.Round(MT.GetGaussian(32.1915, 3.69956), 1))
            Case "OT" : NumString = CStr(Math.Round(MT.GetGaussian(26.8952, 3.42092), 1))
            Case "C" : NumString = CStr(Math.Round(MT.GetGaussian(28.2368, 3.332601), 1))
            Case "OG" : NumString = CStr(Math.Round(MT.GetGaussian(26.1935, 2.41976), 1))
            Case "DE" : NumString = CStr(Math.Round(MT.GetGaussian(32.4123, 3.98806), 1))
            Case "DT" : NumString = CStr(Math.Round(MT.GetGaussian(29, 3.07743), 1))
            Case "OLB" : NumString = CStr(Math.Round(MT.GetGaussian(33.5635, 4.15808), 1))
            Case "ILB" : NumString = CStr(Math.Round(MT.GetGaussian(32.9865, 3.51538), 1))
            Case "CB" : NumString = CStr(Math.Round(MT.GetGaussian(35.5467, 3.33027), 1))
            Case "FS" : NumString = CStr(Math.Round(MT.GetGaussian(35.0238, 3.37348), 1))
            Case "SS" : NumString = CStr(Math.Round(MT.GetGaussian(35.2439, 3.07433), 1))
            Case "K", "P" : NumString = CStr(0)
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
        Return Result
    End Function
    Public Shared Function GetBroadJump(ByVal pos As String) As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB" : Result = CInt(MT.GetGaussian(109.792, 5.32666))
            Case "RB" : Result = CInt(MT.GetGaussian(117.897, 6.53304))
            Case "FB" : Result = CInt(MT.GetGaussian(112.414, 6.87583))
            Case "WR" : Result = CInt(MT.GetGaussian(120.213, 4.79013))
            Case "TE" : Result = CInt(MT.GetGaussian(113.587, 5.61665))
            Case "OT" : Result = CInt(MT.GetGaussian(102.7, 5.56567))
            Case "C" : Result = CInt(MT.GetGaussian(102.432, 4.95133))
            Case "OG" : Result = CInt(MT.GetGaussian(100.839, 5.82041))
            Case "DE" : Result = CInt(MT.GetGaussian(113.898, 5.58356))
            Case "DT" : Result = CInt(MT.GetGaussian(106.217, 4.38829))
            Case "OLB" : Result = CInt(MT.GetGaussian(116.323, 5.55841))
            Case "ILB" : Result = CInt(MT.GetGaussian(113.263, 5.00886))
            Case "CB" : Result = CInt(MT.GetGaussian(121.865, 5.82475))
            Case "FS" : Result = CInt(MT.GetGaussian(119.432, 6.13162))
            Case "SS" : Result = CInt(MT.GetGaussian(119.075, 6.38117))
        End Select
        Return Result
    End Function
    Public Shared Function GetShortShuttle(ByVal pos As String) As Double
        Dim Result As Double
        Select Case pos
            Case "QB" : Result = Math.Round(MT.GetGaussian(4.37, 0.18174), 2)
            Case "RB" : Result = Math.Round(MT.GetGaussian(4.31, 0.13471), 2)
            Case "FB" : Result = Math.Round(MT.GetGaussian(4.4, 0.16438), 2)
            Case "WR" : Result = Math.Round(MT.GetGaussian(4.25, 0.12744), 2)
            Case "TE" : Result = Math.Round(MT.GetGaussian(4.39, 0.16592), 2)
            Case "OT" : Result = Math.Round(MT.GetGaussian(4.76, 0.19038), 2)
            Case "C" : Result = Math.Round(MT.GetGaussian(4.62, 0.17958), 2)
            Case "OG" : Result = Math.Round(MT.GetGaussian(4.81, 0.18228), 2)
            Case "DE" : Result = Math.Round(MT.GetGaussian(4.45, 0.18935), 2)
            Case "DT" : Result = Math.Round(MT.GetGaussian(4.64, 0.18484), 2)
            Case "OLB" : Result = Math.Round(MT.GetGaussian(4.31, 0.15413), 2)
            Case "ILB" : Result = Math.Round(MT.GetGaussian(4.32, 0.12603), 2)
            Case "CB" : Result = Math.Round(MT.GetGaussian(4.24, 0.13143), 2)
            Case "FS" : Result = Math.Round(MT.GetGaussian(4.28, 0.16186), 2)
            Case "SS" : Result = Math.Round(MT.GetGaussian(4.23, 0.15594), 2)
        End Select
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
    Private Shared Function Get10Time(ByVal pos As String) As Double
        Dim Result As Double
        Select Case pos
            Case "QB" : Result = Math.Round(MT.GetGaussian(1.84, 0.064), 2)
            Case "RB" : Result = Math.Round(MT.GetGaussian(1.6, 0.0519), 2)
            Case "FB" : Result = Math.Round(MT.GetGaussian(1.67, 0.056), 2)
            Case "WR" : Result = Math.Round(MT.GetGaussian(1.59, 0.0535), 2)
            Case "TE" : Result = Math.Round(MT.GetGaussian(1.69, 0.0573), 2)
            Case "OT" : Result = Math.Round(MT.GetGaussian(1.84, 0.064), 2)
            Case "OG" : Result = Math.Round(MT.GetGaussian(1.84, 0.0695), 2)
            Case "C" : Result = Math.Round(MT.GetGaussian(1.8, 0.0632), 2)
            Case "DE" : Result = Math.Round(MT.GetGaussian(1.68, 0.0605), 2)
            Case "DT" : Result = Math.Round(MT.GetGaussian(1.77, 0.0636), 2)
            Case "OLB" : Result = Math.Round(MT.GetGaussian(1.63, 0.0548), 2)
            Case "ILB" : Result = Math.Round(MT.GetGaussian(1.66, 0.0638), 2)
            Case "CB" : Result = Math.Round(MT.GetGaussian(1.49, 0.0465), 2)
            Case "SS" : Result = Math.Round(MT.GetGaussian(1.51, 0.0403), 2)
            Case "FS" : Result = Math.Round(MT.GetGaussian(1.51, 0.0386), 2)
        End Select
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
        'Shallow-10%/3-13% less 1st/2nd round talent|25-35% less mid round talent
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
                Case 3 To 8 'shallow
                    TopEnd = MT.GenerateDouble(0.87, 0.97)
                    MidRound = MT.GenerateDouble(0.65, 0.75)
                    DraftClassDesc.Add("Shallow")
                Case 9 To 15 'LackingButDeep
                    TopEnd = MT.GenerateDouble(0.75, 0.85)
                    MidRound = MT.GenerateDouble(1.15, 1.25)
                    DraftClassDesc.Add("LackingButDeep")
                Case 16 To 86 'Normal
                    TopEnd = MT.GenerateDouble(0.95, 1.05)
                    MidRound = MT.GenerateDouble(0.95, 1.05)
                    DraftClassDesc.Add("Normal")
                Case 87 To 92 'TopHeavy
                    TopEnd = MT.GenerateDouble(1.15, 1.25)
                    MidRound = MT.GenerateDouble(0.75, 0.85)
                    DraftClassDesc.Add("TopHeavy")
                Case 93 To 98 'Deep
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