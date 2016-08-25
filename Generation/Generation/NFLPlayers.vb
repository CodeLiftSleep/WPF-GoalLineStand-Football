

''' <summary>
''' Class for Generating NFL Players at game startup, ie, players that are already on a team
''' </summary>
Public Class NFLPlayers
    Inherits Players
    Dim MyPos As String


    Public Sub PutPlayerOnTeam(ByVal Pos As Integer, ByVal PlayerDT As DataTable)
        'determines what team player is on via generic position limits

        Dim NumAllowed As Integer
        Dim MinAllowed As Integer
        'Dim SelectTeam As Integer
        Dim Count As Integer
        Dim i As Integer
        Dim PosString As String = ""
        Dim RowArray As New ArrayList

        i = 1
        Count = 0
        NumAllowed = 0

        Select Case Pos
            Case 1, 2, 5, 7 : NumAllowed = 4 : MinAllowed = 3 '1 QB, 2 RB, 5 TE
            Case 6, 12 : NumAllowed = 3 : MinAllowed = 2 '6 C, 12 ILB
            Case 8, 9, 10, 11 : NumAllowed = 5 : MinAllowed = 3 '7 OG, 8 OT, 9 DE, 10 DT, 11 OLB
            Case 14, 15 : NumAllowed = 4 : MinAllowed = 2 '14 FS, 15 SS
            Case 3 : NumAllowed = 2 : MinAllowed = 1 '16 K, 17 P, 3 FB
            Case 16, 17 : NumAllowed = 1 : MinAllowed = 1
            Case 4, 13 : NumAllowed = 6 : MinAllowed = 4 '4 WR, 13 CB
        End Select

        Select Case Pos
            Case 1 : PosString = "'QB'"
            Case 2 : PosString = "'RB'"
            Case 3 : PosString = "'FB'"
            Case 4 : PosString = "'WR'"
            Case 5 : PosString = "'TE'"
            Case 6 : PosString = "'C'"
            Case 7 : PosString = "'OG'"
            Case 8 : PosString = "'OT'"
            Case 9 : PosString = "'DE'"
            Case 10 : PosString = "'DE'"
            Case 11 : PosString = "'OLB'"
            Case 12 : PosString = "'ILB'"
            Case 13 : PosString = "'CB'"
            Case 14 : PosString = "'FS'"
            Case 15 : PosString = "'SS'"
            Case 16 : PosString = "'K'"
            Case 17 : PosString = "'P'"
        End Select

        For x As Integer = 1 To PlayerDT.Rows.Count - 1
            If PlayerDT.Rows(x).Item("POS") = PosString Then
                RowArray.Add(x) 'adds all rows to an arraylist
            End If
        Next x

        For i = 1 To 32 'numteams
            Dim getnumpos As Integer = MT.GenerateInt32(MinAllowed, NumAllowed)

            For n As Integer = 1 To getnumpos
                If RowArray.Count > 0 Then
                    Dim ChooseArray As Integer = MT.GenerateInt32(0, RowArray.Count - 1)
                    Dim GetRow As Integer = RowArray.Item(ChooseArray)
                    RowArray.RemoveAt(ChooseArray)
                    PlayerDT.Rows(GetRow).Item("TeamID") = i
                End If
            Next n
        Next i

    End Sub
    Public Sub GetRosterPlayers(ByVal PlayerNum As Integer, ByVal XNFLPlayer As NFLPlayers, ByVal PlayerDT As DataTable)

        XNFLPlayer = New NFLPlayers

        Try
            PlayerDT.Rows.Add(PlayerNum)

            MyPos = GetCollegePos(PlayerNum, PlayerDT) 'returns the "normal" version without the  ' '
            PlayerDT.Rows(PlayerNum).Item("POS") = String.Format("'{0}'", MyPos)
            GenNames(PlayerDT, PlayerNum, "NFLPlayer", MyPos)
            GetPersonalityStats(PlayerDT, PlayerNum, XNFLPlayer)
            PlayerDT.Rows(PlayerNum).Item("AgentID") = 0
            PlayerDT.Rows(PlayerNum).Item("TeamID") = 0
            PlayerDT.Rows(PlayerNum).Item("FortyYardTime") = Get40Time(MyPos, PlayerNum, PlayerDT)
            PlayerDT.Rows(PlayerNum).Item("RETKickReturn") = GetKickRetAbility(MyPos, PlayerNum)
            PlayerDT.Rows(PlayerNum).Item("RETPuntReturn") = GetPuntRetAbility(MyPos, PlayerNum, PlayerDT)
            GetSTAbility(MyPos, PlayerNum, PlayerDT)
            GetLSAbility(MyPos, PlayerNum, PlayerDT)
            PlayerDT.Rows(PlayerNum).Item("PosType") = String.Format("'{0}'", GetPosType(MyPos, PlayerNum, PlayerDT))

            PlayerDT.Rows(PlayerNum).Item("DLPrimaryTech") = "'NONE'"
            PlayerDT.Rows(PlayerNum).Item("DLSecondaryTech") = "'NONE'"
            PlayerDT.Rows(PlayerNum).Item("DLPassRushTech") = "'NONE'"

            For col As Integer = 0 To PlayerDT.Columns.Count - 1
                If PlayerDT.Rows(PlayerNum).Item(col) Is DBNull.Value Then
                    PlayerDT.Rows(PlayerNum).Item(col) = MT.GetGaussian(49.5, 16.5)
                End If
            Next col

            GetPosRatings(MyPos, PlayerDT.Rows(PlayerNum).Item("PosType"), PlayerNum, PlayerDT)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.Data)
        End Try
    End Sub
End Class

''' <summary>
''' This specifies contract data for the players
''' </summary>
Public Class Contracts
    Inherits NFLPlayers
    Dim TotalValue As Integer
    Dim SigningBonus As Integer 'prorated over the length of the contract---each year is the same amount
    Dim GuarenteedMoney As Integer
    Dim LengthOfYears As Integer
    Dim SalaryCapHit As Integer
    Dim YearlyBaseSalary(10) As Integer 'Base salary for contract in each possible year of the contract
    Dim YearlyRosterBonus(10) As Integer 'Roster bonus for any given year of the contract
    Dim YearlyWorkoutBonus(10) As Integer 'Workout Bonus for any given year of the contract--typically the same for each year of the contract--only paid if player chooses to participate in offseason workouts with team.
    Dim YearlyOptionBonus(10) As Integer 'Option Bonus for any given year of the contract---this bonus acts like a signing bonus and pro-rates over remaining years of the contract if the team decides to exercise it---must make decision by first date of new season
    Dim YearlyPerGameRosterBonus(10) As Integer 'Roster Bonus in any given year of the contract that is pro-rated for each game of the season the player remains on the 53 man roster.  Is not guarenteed and team can get out of remainder by cutting player after any given week
    Dim PlayerCanVoidContract(10) As Boolean 'boolean as to whether the player is able to void their contract in the given year---either negotiated as part of the contract or typically in relation to a player hitting certain incentives

    '###Need to research the various types of Incentive Bonuses and Base Salary Escalators used in contracts
End Class
