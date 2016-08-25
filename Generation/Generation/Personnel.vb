''' <summary>
''' These are the "Football" People on the team--GM's, Coaches and Scouts.  They all have football related evaluations they use contained in the functions below
''' </summary>
Public Class Personnel
    Inherits Person

    Public Function GetSQLString(ByVal PersonnelType As String) As String
        Dim result As String = ""
        Select Case PersonnelType
            Case "Personnel"
                result = "PersonnelID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Height int NULL, Weight int NULL, Age int NULL, DOB varchar(12) NULL, PersonnelType int NULL, Experience int NULL,
ValuesDraftPicks int NULL, ValuesCombine int NULL, ValuesCharacter int NULL, ValuesProduction int NULL, ValuesIntangibles int NULL, FranchiseTag int NULL, TransitionTag int NULL, JudgingDraft int NULL, JudgingFA int NULL, JudgingOwn int NULL,
JudgingPot int NULL, JudgingQB int NULL, JudgingRB int NULL, JudgingWR int NULL, JudgingTE int NULL, JudgingOL int NULL, JudgingDL int NULL, JudgingLB int NULL, JudgingCB int NULL, JudgingSF int NULL, OffPhil varchar(20) NULL,
QBImp int NULL, RBImp int NULL, FBImp int NULL, WRImp int NULL, WR2Imp int NULL, WR3Imp int NULL, LTImp int NULL, LGImp int NULL, CImp int NULL, RGImp int NULL, RTImp int NULL, TEImp int NULL, DefPhil varchar(20) NULL, DEImp int NULL, DE2Imp int NULL, DTImp int NULL,
DT2Imp int NULL, NTImp int NULL, MLBImp int NULL, WLBImp int NULL, SLBImp int NULL, ROLBImp int NULL, LOLBImp int NULL, CB1Imp int NULL, CB2Imp int NULL, CB3Imp int NULL, FSImp int NULL, SSImp int NULL, DraftStrategy varchar(20) NULL,
TeamBuilding varchar(20) NULL, RespectedByPlayers int NULL, RespectedByCoaches int NULL, RespectedByScouts int NULL, OrganizationalPower int NULL, " + PersonSQLString

            Case "Coach"
                result = "CoachID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Height int NULL, Weight int NULL, Age int NULL, DOB varchar(12) NULL, CoachType int NULL, SideOfBall varchar(3) NULL, Experience int NULL,
OffPhil varchar(50) NULL, DefPhil Varchar(50) NULL, OffAbility int NULL, DefAbility int NULL, CoachQB int NULL, CoachRB int NULL, CoachWR int NULL, CoachTE int NULL, CoachOL int NULL, DevPlayers int NULL, CoachDL int NULL, CoachLB int NULL,
CoachDB int NULL, CoachST int NULL, JudgingAct int NULL, JudgingPot int NULL, JudgingQB int NULL, JudgingRB int NULL, JudgingWR int NULL, JudgingTE int NULL, JudgingOL int NULL, JudgingDL int NULL, JudgingLB int NULL, JudgingCB int NULL, JudgingSF int NULL,
ValuesST int NULL, ValuesCharacter int NULL, PlaycallingSkill int NULL, RespectedByPlayers int NULL, RespectedByCoaches int NULL, SeesBigPicture int NULL, DevQB int NULL, DevRB int NULL, DevWR int NULL, DevTE int NULL, DevOL int NULL, DevDL int NULL,
DevLB int NULL, DevCB int NULL, DevSF int NULL, LowerBodyTrain int NULL, UpperBodyTrain int NULL, CoreTrain int NULL, PreventInjuryTrain int NULL, StaminaTrain int NULL, " + PersonSQLString
        End Select
        Return result
    End Function
    Public Sub GenPersonnel(ByVal PersonnelNum As Integer, ByVal XPersonnel As Personnel, ByVal PersonnelDT As DataTable)
        XPersonnel = New Personnel
        PersonalityModel()

        Dim MyOPhil As String
        Dim MyDPhil As String
        Static PersonnelDict(33) As Dictionary(Of Integer, Integer) 'maintains this list throughout the process
        Dim PersonnelType As Integer

        If PersonnelNum = 1 Then 'only does this on the first time through
            For i As Integer = 1 To 32 'initializes the array of dictionaries that keeps track of how many of each personnel type are on each team
                PersonnelDict(i) = New Dictionary(Of Integer, Integer)
                For x As Integer = 0 To 13
                    PersonnelDict(i).Add(x, 0)
                Next x
            Next i
        End If

        Try
            PersonnelDT.Rows.Add(PersonnelNum)
            MyOPhil = GetOffPhil()
            MyDPhil = GetDefPhil()
            GenNames(PersonnelDT, PersonnelNum, "Personnel")
            GetPersonalityStats(PersonnelDT, PersonnelNum, XPersonnel)
            PersonnelDT.Rows(PersonnelNum).Item("PersonnelType") = GetPersonnelType(PersonnelDT.Rows(PersonnelNum).Item("Age"))
            PersonnelDT.Rows(PersonnelNum).Item("OrganizationalPower") = GetOrgPower(PersonnelDT.Rows(PersonnelNum).Item("PersonnelType"))
            PersonnelDT.Rows(PersonnelNum).Item("Experience") = (PersonnelDT.Rows(PersonnelNum).Item("Age") - 24)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingDraft") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingFA") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingOwn") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingPot") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingQB") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingRB") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingWR") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingTE") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingOL") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingDL") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingLB") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingCB") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("JudgingSF") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("OffPhil") = String.Format("'{0}'", MyOPhil)
            PersonnelDT.Rows(PersonnelNum).Item("DefPhil") = String.Format("'{0}'", MyDPhil)
            PersonnelDT.Rows(PersonnelNum).Item("DraftStrategy") = String.Format("'{0}'", DraftStrategy())
            PersonnelDT.Rows(PersonnelNum).Item("TeamBuilding") = String.Format("'{0}'", TeamBuilding())
            PositionalImp(PersonnelNum, MyOPhil, MyDPhil, PersonnelDT)
            PersonnelDT.Rows(PersonnelNum).Item("ValuesDraftPicks") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("FranchiseTag") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("ValuesCombine") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("ValuesCharacter") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("ValuesProduction") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("ValuesIntangibles") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("TransitionTag") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("RespectedByPlayers") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("RespectedByCoaches") = MT.GetGaussian(49.5, 16.5)
            'PersonnelDT.Rows(PersonnelNum).Item("DraftPreparation") = MT.GetGaussian(49.5, 16.5)
            PersonnelDT.Rows(PersonnelNum).Item("RespectedByScouts") = MT.GetGaussian(49.5, 16.5)
            PersonnelType = PersonnelDT.Rows(PersonnelNum).Item("PersonnelType")

            For x = 1 To 32
                Select Case PersonnelType
                    Case 1 To 8, 11, 12 'only 1 per team
                        If PersonnelDict(x).Item(PersonnelType) = 0 Then
                            PersonnelDict(x).Item(PersonnelType) = 1
                            PersonnelDT.Rows(PersonnelNum).Item("TeamID") = x
                            PersonnelType = 0
                        Else : PersonnelDT.Rows(PersonnelNum).Item("TeamID") = 0
                        End If

                    Case 9, 13 'National Scouts/Scouting Assistants 2 per team
                        If PersonnelDict(x).Item(PersonnelType) < 2 Then
                            PersonnelDict(x).Item(PersonnelType) += 1
                            PersonnelDT.Rows(PersonnelNum).Item("TeamID") = x
                            PersonnelType = 0
                        Else : PersonnelDT.Rows(PersonnelNum).Item("TeamID") = 0
                        End If

                    Case 10 'Area Scouts---6 Per Team
                        If PersonnelDict(x).Item(PersonnelType) < 6 Then
                            PersonnelDict(x).Item(PersonnelType) += 1
                            PersonnelDT.Rows(PersonnelNum).Item("TeamID") = x
                            PersonnelType = 0
                        Else : PersonnelDT.Rows(PersonnelNum).Item("TeamID") = 0
                        End If

                End Select
            Next x

        Catch ex As System.InvalidCastException
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.Data)
        End Try
    End Sub

    Private Function GetOrgPower(ByVal PersonnelType As Integer) As Integer
        Dim result As Integer
        Select Case PersonnelType
            Case 1 : result = MT.GetGaussian(75, 8.33)
            Case 2 : result = MT.GetGaussian(65, 8.33)
            Case 3 : result = MT.GetGaussian(60, 8.33)
            Case 4 : result = MT.GetGaussian(50, 8.33)
            Case 5 : result = MT.GetGaussian(55, 8.33)
            Case 6 : result = MT.GetGaussian(45, 8.33)
            Case 7 : result = MT.GetGaussian(50, 8.33)
            Case 8 : result = MT.GetGaussian(40, 8.33)
            Case 9 : result = MT.GetGaussian(35, 8.33)
            Case 10 : result = MT.GetGaussian(25, 8.33)
            Case 11 : result = MT.GetGaussian(30, 8.33)
            Case 12 : result = MT.GetGaussian(15, 5.0)
            Case 13 : result = MT.GetGaussian(10, 3.33)
        End Select
        Return result
    End Function
    ''' <summary>
    ''' Gets the type of personnel this person is based on age---younger people have  lower chance to be higher up in the organization than older people
    ''' </summary>
    ''' <param name="age"></param>
    ''' <returns></returns>
    Private Function GetPersonnelType(ByVal age As Integer) As Integer
        Dim result As Integer
        Select Case age
            Case < 26
                result = MT.GenerateInt32(1, 10)
                Select Case result
                    Case 1 To 4 : result = 13
                    Case 5 To 10 : result = 12 'NationalScoutingOrgScout
                End Select
            Case < 30
                result = 10 'Area Scout
            Case < 33
                result = MT.GenerateInt32(1, 6)
                Select Case result
                    Case 1 : result = 4
                    Case 2 : result = 6
                    Case 3 : result = 8
                    Case 4 : result = 9
                    Case 5 : result = 10
                    Case 6 : result = 11
                End Select
            Case < 39
                result = MT.GenerateInt32(1, 100)
                Select Case result
                    Case 1 '1% chance of being Assistant GM
                        result = 2
                    Case 2  '1% chance of being Director of Player Personnel
                        result = 3
                    Case 3 To 7 '5% chance of being AsstDir of Player Personnel
                        result = 4
                    Case 8 To 10 '3% chance of being Director of Pro Personnel
                        result = 5
                    Case 11 To 17 '7% chance of being Asst Director of Pro Personnel
                        result = 6
                    Case 18 To 26 '9% chance of being Director of College Scouting
                        result = 7
                    Case 27 To 41 '15% chance of being Asst Director of College Scouting
                        result = 8
                    Case 42 To 61 '20% chance of being National Scout
                        result = 9
                    Case 62 To 91 '30% chance of being Area Scout
                        result = 10
                    Case 92 To 100 '9% chance of being Advance Scout
                        result = 11
                End Select
            Case < 45
                result = MT.GenerateInt32(1, 100)
                Select Case result
                    Case 1 To 2 '2% chance of being selected as GM
                        result = 1
                    Case 3 To 6 '4% chance of being Assistant GM
                        result = 2
                    Case 7 To 10 '4% chance of being Dir Player Personnel
                        result = 3
                    Case 11 To 18 '8% chance of being Asst Director of Player Personnel
                        result = 4
                    Case 19 To 24 '6% Dir Pro Personnel
                        result = 5
                    Case 25 To 33 '9% chance Asst Dir Pro Personnel
                        result = 6
                    Case 34 To 44 '11% chance of Dir College Scouting
                        result = 7
                    Case 45 To 57 '13% chance of Asst Dir College Scouting
                        result = 8
                    Case 58 To 72 '15% chance National College Scout
                        result = 9
                    Case 73 To 92 '20% chance of Area Scout
                        result = 10
                    Case 92 To 100 '7% chance of Advance Scout
                        result = 11
                End Select
            Case Else
                result = MT.GenerateInt32(1, 100)
                Select Case result
                    Case 1 To 3 '3% GM
                        result = 1
                    Case 4 To 9 '6% Asst GM
                        result = 2
                    Case 10 To 19 '10% Player Personnel
                        result = 3
                    Case 20 To 27 '8% Asst Play Personnel
                        result = 4
                    Case 28 To 37 '10% Pro Personnel
                        result = 5
                    Case 38 To 45 '8% Asst Pro Personnel
                        result = 6
                    Case 46 To 55 '10% Dir College Scouting
                        result = 7
                    Case 56 To 63 '8% Asst Dir College Scouting
                        result = 8
                    Case 64 To 78 '15% National Scout
                        result = 9
                    Case 79 To 95 '17% Area Scout
                        result = 10
                    Case 96 To 100 '5% Advance Scout
                        result = 11
                End Select
        End Select
        Return result
    End Function

    Private Function DraftStrategy() As String
        Dim result As String = ""
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 15 : result = "DraftTradeDown"
            Case 16 To 30 : result = "DraftBestAvail"
            Case 31 To 45 : result = "DraftTheirGuy"
            Case 46 To 60 : result = "DraftBiggestNeed"
            Case 61 To 75 : result = "DraftTradeUp"
            Case 76 To 85 : result = "TradePicksForPlayers"
            Case 86 To 100 : result = "TradePlayersForPicks"
        End Select
        Return result
    End Function
    Private Function TeamBuilding() As String
        Dim result As String = ""
        Select Case MT.GenerateInt32(1, 90)
            Case 1 To 15 : result = "DraftKeepCore"
            Case 16 To 30 : result = "DraftLetVetsGo"
            Case 31 To 45 : result = "DraftGetVets"
            Case 46 To 60 : result = "FAKeepCore"
            Case 61 To 75 : result = "FALetVetsGo"
            Case 76 To 90 : result = "FAGetVets"
        End Select
        Return result
    End Function
    Private Sub PositionalImp(ByVal i As Integer, ByVal OffPhil As String, ByVal DefPhil As String, ByVal PersonnelDT As DataTable)
        Select Case OffPhil
            'Sets the Importance of Various Positions for the offenses--over 100 is above normal
            'Under 100 is below normal---100 is average importance
            '1170 Points Allotted
            Case "BalPass"
                PersonnelDT.Rows(i).Item("QBImp") = 110
                PersonnelDT.Rows(i).Item("RBImp") = 90
                PersonnelDT.Rows(i).Item("FBImp") = 74
                PersonnelDT.Rows(i).Item("WRImp") = 110
                PersonnelDT.Rows(i).Item("WR2Imp") = 95
                PersonnelDT.Rows(i).Item("WR3Imp") = 85
                PersonnelDT.Rows(i).Item("LTImp") = 110
                PersonnelDT.Rows(i).Item("LGImp") = 95
                PersonnelDT.Rows(i).Item("CImp") = 105
                PersonnelDT.Rows(i).Item("RGImp") = 95
                PersonnelDT.Rows(i).Item("RTImp") = 95
                PersonnelDT.Rows(i).Item("TEImp") = 106

            Case "BalRun"
                PersonnelDT.Rows(i).Item("QBImp") = 100
                PersonnelDT.Rows(i).Item("RBImp") = 110
                PersonnelDT.Rows(i).Item("FBImp") = 103
                PersonnelDT.Rows(i).Item("WRImp") = 90
                PersonnelDT.Rows(i).Item("WR2Imp") = 80
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 105
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 110
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 95
                PersonnelDT.Rows(i).Item("TEImp") = 108

            Case "VertPass"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100

            Case "Smashmouth"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100

            Case "WCPass"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "WCRun"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "WCBal"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "SpreadRun"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "SpreadPass"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "SpreadBal"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "Run-N-Shoot"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100

        End Select

        Select Case DefPhil
            Case "4-3Attack"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "4-3Cover"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "4-3Bal"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "4-3StuffRun"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "3-4Attack"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "3-4Cover"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "3-4Bal"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "3-4StuffRun"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "Cover2Attack"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "Cover2Cover"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "Cover2Bal"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "46"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
        End Select

    End Sub
    ''' <summary>
    ''' Determines what scouting organization the scout is a part of. 12 teams use BLESTO, 15 use NFS, and the others aren't part of an organziation.
    ''' </summary>
    ''' <param name="TeamID"></param>
    ''' <returns></returns>
    Private Function GetScoutOrg(ByVal TeamID As Integer) As Integer 'determines membership in BLESTO/NFS
        Dim result As Integer
        Select Case TeamID
            Case 1, 3, 4, 5, 6, 8, 9, 11 To 14, 16, 18 To 32
                result = 12 'BLESTO Scout/NFS Scout
            Case Else : result = 13 'Scouting Assistant
        End Select
        Return result
    End Function
    Public Sub GenScoutGrades(ByVal NumScouts As Integer, ByVal NumPlayers As Integer)

        'SQLiteTables.LoadTable(ScoutGradeDT, "ScoutGrades")
        'If ScoutGradeDT.Rows.Count = 0 Then
        'Dim SQLFields As String = "(PID INTEGER PRIMARY KEY,"

        'For ScoutID As Integer = 0 To NumScouts
        'If ScoutID <> NumScouts Then
        'SQLFields += "Scout" & ScoutID & " Decimal(9,2),"
        'Else
        'SQLFields += "Scout" & ScoutID & " Decimal(9,2));"
        'End If
        'Next ScoutID

        SQLiteTables.DeleteTable(MyDB, ScoutGradeDT, "ScoutsGrade")
        SQLiteTables.LoadTable(MyDB, ScoutGradeDT, "ScoutsGrade")

        ScoutGradeDT.Rows.Add(0)

        For PlayerId As Integer = 1 To NumPlayers
            ScoutGradeDT.Rows.Add(PlayerId)
        Next PlayerId

        Eval.ScoutPlayerEval()
        'End If
    End Sub
    Public Function GetOffPhil() As String
        Dim result As String = ""
        Select Case MT.GenerateInt32(1, 90)
            Case 1 To 10 : result = "BalPass"
            Case 11 To 20 : result = "BalRun"
            Case 21 To 30 : result = "VertPass"
            Case 31 To 40 : result = "Smashmouth"
            Case 41 To 50 : result = "WCPass"
            Case 51 To 60 : result = "WCRun"
            Case 61 To 70 : result = "WCBal"
            Case 71 To 76 : result = "SpreadRun"
            Case 77 To 82 : result = "SpreadPass"
            Case 83 To 88 : result = "SpreadBal"
            Case 89 To 90 : result = "Run-N-Shoot"
                'Case 91 To 100 : Return "PassHeavy"
        End Select
        Return result
    End Function
    Public Function GetDefPhil() As String
        Dim result As String = ""
        Select Case MT.GenerateInt32(1, 93)
            Case 1 To 12 : result = "4-3Attack"
            Case 13 To 23 : result = "4-3Cover"
            Case 23 To 33 : result = "4-3Bal"
            Case 33 To 43 : result = "4-3StuffRun"
            Case 44 To 49 : result = "3-4Attack"
            Case 50 To 55 : result = "3-4Cover"
            Case 56 To 61 : result = "3-4Bal"
            Case 62 To 67 : result = "3-4StuffRun"
            Case 68 To 75 : result = "Cover2Attack"
            Case 76 To 83 : result = "Cover2Cover"
            Case 84 To 91 : result = "Cover2Bal"
            Case 92 To 93 : result = "46"
                'Case 94 To 100 : Return "Hybrid"
        End Select
        Return result
    End Function
End Class