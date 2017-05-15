Imports GlobalResources
''' <summary>
''' This will be the parent class for both NFLPlayers and CollegePlayers.
''' </summary>
Public MustInherit Class Players
    Inherits Person

    ''' <summary>
    ''' FieldNames for players will use the same attributes, but the College Players will have additional Combine related fields as well.
    ''' </summary>
    ''' <param name="playerType"></param>
    ''' <returns></returns>
    Public Function GetSQLString(ByVal playerType As String) As String
        Dim SQLFieldNames As String = ""
        Select Case playerType
            Case "College"

                SQLFieldNames = "DraftID int PRIMARY KEY NOT NULL, AgentID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, ScoutRegion varchar(10) NULL, Height int NULL, Weight int NULL, Age int NULL, DOB varchar(12) NULL," +
"CollegePOS varchar(5) NULL, ActualGrade decimal(5,2) NULL, ProjNFLPos varchar(5) NULL, PosType varchar(20) NULL,  ArmLength decimal(4,2) NULL, HandLength decimal(4,2) NULL, FortyYardTime decimal(3,2) NULL, TwentyYardTime decimal(3,2) NULL," +
"TenYardTime decimal(3,2) NULL, ShortShuttle decimal(3,2) NULL, BroadJump int NULL, VertJump decimal(3,1) NULL, ThreeConeDrill decimal(3,2) NULL, BenchPress int NULL, InterviewSkills int NULL, WonderlicTest int NULL, SkillsTranslateToNFL int NULL," +
"Reaction int NULL, QAB int NULL, COD int NULL, Hands int NULL, BodyCatch int NULL, StiffArm int NULL, ReleaseOffLine int NULL, CatchWhenHit int NULL, BreaksTackles int NULL, ContactBalance int NULL, RunAfterCatch int NULL, LowerBodyStrength int NULL," +
"UpperBodyStrength int NULL, Footwork int NULL, HandUse int NULL, JumpingAbility int NULL, PassBlockVsPower int NULL, PassBlockVsSpeed int NULL, RunBlocking int NULL, PlaySpeed int NULL, RouteRunning int NULL, KickAccuracy int NULL, AdjustToBall int NULL," +
"Tackling int NULL, Blitz int NULL, AvoidBlockers int NULL, ShedBlock int NULL, DefeatBlock int NULL, ManToManCoverage int NULL, ZoneCoverage int NULL, RETKickReturn int NULL, RETPuntReturn int NULL, PlayStrength int NULL, QBMechanics int NULL," +
"QBRelQuickness int NULL, QBAccuracy int NULL, QBDecMaking int NULL, QBBallHandling int NULL, QBLocateRec int NULL, QBPocketPresence int NULL, QBEscape int NULL, QBScrambling int NULL, QBRolloutRight int NULL, QBRolloutLeft int NULL, QBArmStrength int NULL," +
"QBTouch int NULL, QBPlayAction int NULL, RBRunVision int NULL, RBSetsUpBlocks int NULL, RBPatience int NULL, WRRunDBOff int NULL, WRDisguiseRoute int NULL, OLPulling int NULL, OLSlide int NULL, OLMoveInSpace int NULL, OLSnapAbility int NULL," +
"OLLongSnapAbility int NULL, OLAnchorAbility int NULL, OLRecover int NULL, DLPrimaryTech varchar(15) NULL, DLSecondaryTech varchar(15) NULL, DLPassRushTech varchar(15) NULL, DLRunAtHim int NULL, DLAgainstPullAbility int NULL, DLSlideABility int NULL, DLRunPursuit int NULL," +
"DLCanTakeDoubleTeam int NULL, DLFinish int NULL, DLsetUpPassRush int NULL, LBDropDepth int NULL, LBFillGaps int NULL, DBPressBailCoverage int NULL, DBRunContain int NULL, DBBump int NULL, DBBaitQB int NULL, DBCatchUpSpeed int NULL, DBTechnique int NULL," +
"KFakeAbility int NULL, KKickRise int NULL, PFakeAbility int NULL, PDistance int NULL, PHangTime int NULL, STCoverage int NULL, STWillingness int NULL, STAssignment int NULL, STDiscipline int NULL, Flexibility int NULL, Consistency int NULL," +
"Instincts int NULL, Coachability int NULL, Leadership int NULL, Confidence int NULL, Clutch int NULL, WorkEthic int NULL, FilmStudy int NULL, Durability int NULL, Explosion int NULL, DeliversBlow int NULL, Toughness int NULL, ReadKeys int NULL," +
"FieldAwareness int NULL, PlaybookKnowledge int NULL, BallSecurity int NULL, LovesFootball int NULL, Concentration int NULL, HandlesElements int NULL, Potential int NULL, Raw int NULL, " + PersonSQLString

            Case "NFL"

                SQLFieldNames = "PlayerID int PRIMARY KEY Not NULL, AgentID int NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, ScoutRegion varchar(10) NULL, Age int NULL, DOB varchar(12) NULL, Height int NULL,
Weight int NULL, ArmLength decimal(4,2) NULL, HandLength decimal(4,2) NULL, Pos varchar(4) NULL, PosType varchar(20) NULL, FortyYardTime decimal(3,2) NULL, Reaction int NULL, QAB int NULL, COD int NULL, Hands int NULL, BodyCatch int NULL, StiffArm int NULL,
ReleaseOffLine int NULL, CatchWhenHit int NULL, BreaksTackles int NULL, ContactBalance int NULL, RunAfterCatch int NULL, LowerBodyStrength int NULL, UpperBodyStrength int NULL, Footwork int NULL, HandUse int NULL, JumpingAbility int NULL,
PassBlockVsPower int NULL, PassBlockVsSpeed int NULL, RunBlocking int NULL, PlaySpeed int NULL, RouteRunning int NULL, KickAccuracy int NULL, AdjustToBall int NULL, Tackling int NULL, Blitz int NULL, AvoidBlockers int NULL, ShedBlock int NULL,
DefeatBlock int NULL, ManToManCoverage int NULL, ZoneCoverage int NULL, RETKickReturn int NULL, RETPuntReturn int NULL, PlayStrength int NULL, QBMechanics int NULL, QBRelQuickness int NULL, QBAccuracy int NULL, QBDecMaking int NULL, QBBallHandling int NULL,
QBLocateRec int NULL, QBPocketPresence int NULL, QBEscape int NULL, QBScrambling int NULL, QBRolloutRight int NULL, QBRolloutLeft int NULL, QBArmStrength int NULL, QBTouch int NULL, QBPlayAction int NULL, RBRunVision int NULL, RBSetsUpBlocks int NULL,
RBPatience int NULL, WRRunDBOff int NULL, WRDisguiseRoute int NULL, OLPulling int NULL, OLSlide int NULL, OLMoveInSpace int NULL, OLSnapAbility int NULL, OLLongSnapAbility int NULL, OLAnchorAbility int NULL, OLRecover int NULL, DLPrimaryTech varchar(15) NULL,
DLSecondaryTech varchar(15) NULL, DLPassRushTech varchar(15) NULL, DLRunAtHim int NULL, DLAgainstPullAbility int NULL, DLSlideABility int NULL, DLRunPursuit int NULL,  DLCanTakeDoubleTeam int NULL, DLFinish int NULL, DLsetUpPassRush int NULL,
LBDropDepth int NULL, LBFillGaps int NULL, DBPressBailCoverage int NULL, DBRunContain int NULL, DBBump int NULL, DBBaitQB int NULL, DBCatchUpSpeed int NULL, DBTechnique int NULL, KFakeAbility int NULL, KKickRise int NULL, PFakeAbility int NULL,
PDistance int NULL, PHangTime int NULL, STCoverage int NULL, STWillingness int NULL, STAssignment int NULL, STDiscipline int NULL, Flexibility int NULL, Consistency int NULL, Instincts int NULL, Coachability int NULL, Leadership int NULL, Confidence int NULL,
Clutch int NULL, WorkEthic int NULL, FilmStudy int NULL, Durability int NULL, Explosion int NULL, DeliversBlow int NULL, Toughness int NULL, ReadKeys int NULL, FieldAwareness int NULL, PlaybookKnowledge int NULL, BallSecurity int NULL, LovesFootball int NULL,
Concentration int NULL, HandlesElements int NULL, Potential int NULL, Raw int NULL, " + PersonSQLString

        End Select
        Return SQLFieldNames
    End Function
    Public Shared Function Get40Time(ByVal pos As String, ByVal idNum As Integer, ByVal dt As DataTable) As Double
        Dim Result As Double
        Select Case pos
            Case "QB" : Result = Math.Round(MT.GetGaussian(4.8339, 0.1851), 2)
            Case "RB"
                Result = Math.Round(MT.GetGaussian(4.5661, 0.1212), 2)
                'Add to the Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.24) * (100 / 79))) * 1.4

            Case "FB" : Result = Math.Round(MT.GetGaussian(4.7638, 0.1361), 2)

            Case "WR"
                Result = Math.Round(MT.GetGaussian(4.5123, 0.1042), 2)
                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.24) * (100 / 61))) * 1.4
            Case "TE"
                Result = Math.Round(MT.GetGaussian(4.7911, 0.1442), 2)
                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.37) * (100 / 74))) * 1.4
            Case "OT" : Result = Math.Round(MT.GetGaussian(5.2726, 0.1878), 2)
            Case "OG" : Result = Math.Round(MT.GetGaussian(5.3232, 0.1871), 2)
            Case "C" : Result = Math.Round(MT.GetGaussian(5.2231, 0.1351), 2)
            Case "DE" : Result = Math.Round(MT.GetGaussian(4.8347, 0.1411), 2)
            Case "DT" : Result = Math.Round(MT.GetGaussian(5.1049, 0.1605), 2)

            Case "OLB"
                Result = Math.Round(MT.GetGaussian(4.6973, 0.1257), 2)
                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.4) * (100 / 74))) * 1.4

            Case "ILB" : Result = Math.Round(MT.GetGaussian(4.7626, 0.1308), 2)

            Case "CB"
                Result = Math.Round(MT.GetGaussian(4.4987, 0.0984), 2)
                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.25) * (100 / 70))) * 1.4
            Case "SS", "FS"
                Result = Math.Round(MT.GetGaussian(4.5696, 0.1021), 2)
                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.31) * (100 / 65))) * 1.4
            Case "K" : Result = Math.Round(MT.GetGaussian(4.94, 0.08), 2)
            Case "P" : Result = Math.Round(MT.GetGaussian(4.765, 0.08), 2)
        End Select

        If Result < 4.21 Then Result = 4.21
        If Result > 5.5 Then Result = 5.5
        dt.Rows(idNum).Item("PlaySpeed") = 100 - ((Result - 4.3) * 83.333)
        Return Result

    End Function
    Public Shared Function GetCollegePos(ByVal playernum As Integer, ByVal dt As DataTable) As String
        Dim Result As String = ""
        Dim GetNevUsed As New List(Of String)
        Dim GetRareUsed As New List(Of String)

        Select Case MT.GenerateDouble(0, 100)
            Case 0 To 5.186
                Result = "QB"
            Case 5.187 To 12.102
                Result = "RB"
            Case 12.103 To 14.209
                Result = "FB"
            Case 14.21 To 24.743
                Result = "WR"
            Case 24.744 To 30.956
                Result = "TE"
            Case 30.957 To 35.764
                Result = "C"
            Case 35.765 To 41.167
                Result = "OG"
            Case 41.168 To 48.892
                Result = "OT"
            Case 48.893 To 57.266
                Result = "DE"
            Case 57.267 To 65.37
                Result = "DT"
            Case 65.371 To 74.029
                Result = "OLB"
            Case 74.03 To 78.23
                Result = "ILB"
            Case 78.231 To 83.581
                Result = "FS"
            Case 83.582 To 88.763
                Result = "SS"
            Case 88.764 To 96.218
                Result = "CB"
            Case 96.219 To 98.217
                Result = "K"
            Case Else
                Result = "P"
        End Select

        GetNevUsed = GetNeverUsed(Result)
        For i As Integer = 0 To GetNevUsed.Count - 1
            dt.Rows(playernum).Item(GetNevUsed(i)) = 0
        Next i

        GetRareUsed = GetRarelyUsed(Result)
        For i As Integer = 0 To GetRareUsed.Count - 1
            dt.Rows(playernum).Item(GetRareUsed(i)) = MT.GenerateInt32(0, 15)
        Next i

        Return Result
    End Function
    Public Shared Function GetNeverUsed(ByVal pos As String) As List(Of String)
        Dim Result As New List(Of String)
        Select Case pos
            Case "QB"
                Result.Add("OLPulling")
                Result.Add("OLSlide")
                Result.Add("OLMoveInSpace")
                Result.Add("OLSnapAbility")
                Result.Add("OLLongSnapAbility")
                Result.Add("OLAnchorAbility")
                Result.Add("OLRecover")
                Result.Add("DLRunAtHim")
                Result.Add("DLAgainstPullAbility")
                Result.Add("DLSlideAbility")
                Result.Add("DLRunPursuit")
                Result.Add("DLCanTakeDoubleTeam")
                Result.Add("DLFinish")
                Result.Add("DLSetUpPassRush")
                Result.Add("LBDropDepth")
                Result.Add("LBFillGaps")
                Result.Add("DBPressBailCoverage")
                Result.Add("DBRunContain")
                Result.Add("DBBump")
                Result.Add("DBBaitQB")
                Result.Add("DBCatchUpSpeed")
                Result.Add("DBTechnique")
                Result.Add("KickAccuracy")
                Result.Add("KFakeAbility")
                Result.Add("KKickRise")
                Result.Add("PFakeAbility")
                Result.Add("PDistance")
                Result.Add("PHangTime")
                Result.Add("Blitz")
                Result.Add("WRRunDBOff")
                Result.Add("WRDisguiseRoute")
                Result.Add("PassBlockVsSpeed")
                Result.Add("PassBlockVsPower")
                Result.Add("ManToManCoverage")
                Result.Add("ZoneCoverage")
                Result.Add("HandUse")

            Case "RB", "FB"
                Result.Add("OLPulling")
                Result.Add("OLSlide")
                Result.Add("OLMoveInSpace")
                Result.Add("OLSnapAbility")
                Result.Add("OLLongSnapAbility")
                Result.Add("OLAnchorAbility")
                Result.Add("OLRecover")
                Result.Add("DLRunAtHim")
                Result.Add("DLAgainstPullAbility")
                Result.Add("DLSlideAbility")
                Result.Add("DLRunPursuit")
                Result.Add("DLCanTakeDoubleTeam")
                Result.Add("DLFinish")
                Result.Add("DLSetUpPassRush")
                Result.Add("LBDropDepth")
                Result.Add("LBFillGaps")
                Result.Add("DBPressBailCoverage")
                Result.Add("DBRunContain")
                Result.Add("DBBump")
                Result.Add("DBBaitQB")
                Result.Add("DBCatchUpSpeed")
                Result.Add("DBTechnique")
                Result.Add("KickAccuracy")
                Result.Add("KFakeAbility")
                Result.Add("KKickRise")
                Result.Add("PFakeAbility")
                Result.Add("PDistance")
                Result.Add("PHangTime")
                Result.Add("Blitz")
                Result.Add("ManToManCoverage")
                Result.Add("ZoneCoverage")
                Result.Add("WRRunDBOff")
                Result.Add("WRDisguiseRoute")
                Result.Add("QBPlayAction")

            Case "WR", "TE"
                Result.Add("OLPulling")
                Result.Add("OLSlide")
                Result.Add("OLMoveInSpace")
                Result.Add("OLSnapAbility")
                Result.Add("OLLongSnapAbility")
                Result.Add("OLAnchorAbility")
                Result.Add("OLRecover")
                Result.Add("DLRunAtHim")
                Result.Add("DLAgainstPullAbility")
                Result.Add("DLSlideAbility")
                Result.Add("DLRunPursuit")
                Result.Add("DLCanTakeDoubleTeam")
                Result.Add("DLFinish")
                Result.Add("DLSetUpPassRush")
                Result.Add("LBDropDepth")
                Result.Add("LBFillGaps")
                Result.Add("DBPressBailCoverage")
                Result.Add("DBRunContain")
                Result.Add("DBBump")
                Result.Add("DBBaitQB")
                Result.Add("DBCatchUpSpeed")
                Result.Add("DBTechnique")
                Result.Add("KickAccuracy")
                Result.Add("KFakeAbility")
                Result.Add("KKickRise")
                Result.Add("PFakeAbility")
                Result.Add("PDistance")
                Result.Add("PHangTime")
                Result.Add("Blitz")
                Result.Add("ManToManCoverage")
                Result.Add("ZoneCoverage")
                Result.Add("QBPlayAction")

            Case "OT", "C", "OG"
                Result.Add("QBMechanics")
                Result.Add("QBRelQuickness")
                Result.Add("QBAccuracy")
                Result.Add("QBDecMaking")
                Result.Add("QBBallHandling")
                Result.Add("QBLocateRec")
                Result.Add("QBPocketPresence")
                Result.Add("QBEscape")
                Result.Add("QBScrambling")
                Result.Add("QBRolloutRight")
                Result.Add("QBRolloutLeft")
                Result.Add("QBArmStrength")
                Result.Add("QBTouch")
                Result.Add("RBRunvision")
                Result.Add("RBSetsUpBlocks")
                Result.Add("RBPatience")
                Result.Add("DLRunAtHim")
                Result.Add("DLAgainstPullAbility")
                Result.Add("DLSlideAbility")
                Result.Add("DLRunPursuit")
                Result.Add("DLCanTakeDoubleTeam")
                Result.Add("DLFinish")
                Result.Add("DLSetUpPassRush")
                Result.Add("LBDropDepth")
                Result.Add("LBFillGaps")
                Result.Add("DBPressBailCoverage")
                Result.Add("DBRunContain")
                Result.Add("DBBump")
                Result.Add("DBBaitQB")
                Result.Add("DBCatchUpSpeed")
                Result.Add("DBTechnique")
                Result.Add("KickAccuracy")
                Result.Add("KFakeAbility")
                Result.Add("KKickRise")
                Result.Add("PFakeAbility")
                Result.Add("PDistance")
                Result.Add("PHangTime")
                Result.Add("Blitz")
                Result.Add("ManToManCoverage")
                Result.Add("ZoneCoverage")
                Result.Add("QBPlayAction")
                Result.Add("WRRunDBOff")
                Result.Add("WRDisguiseRoute")
                Result.Add("StiffArm")

            Case "DE", "DT"
                Result.Add("OLPulling")
                Result.Add("OLSlide")
                Result.Add("OLMoveInSpace")
                Result.Add("OLSnapAbility")
                Result.Add("OLLongSnapAbility")
                Result.Add("OLAnchorAbility")
                Result.Add("OLRecover")
                Result.Add("QBMechanics")
                Result.Add("QBRelQuickness")
                Result.Add("QBAccuracy")
                Result.Add("QBDecMaking")
                Result.Add("QBBallHandling")
                Result.Add("QBLocateRec")
                Result.Add("QBPocketPresence")
                Result.Add("QBEscape")
                Result.Add("QBScrambling")
                Result.Add("QBRolloutRight")
                Result.Add("QBRolloutLeft")
                Result.Add("QBArmStrength")
                Result.Add("QBTouch")
                Result.Add("KickAccuracy")
                Result.Add("KFakeAbility")
                Result.Add("KKickRise")
                Result.Add("PFakeAbility")
                Result.Add("PDistance")
                Result.Add("PHangTime")
                Result.Add("WRRunDBOff")
                Result.Add("WRDisguiseRoute")
                Result.Add("RBRunvision")
                Result.Add("RBSetsUpBlocks")
                Result.Add("RBPatience")
                Result.Add("ReleaseOffLine")
                Result.Add("RouteRunning")
                Result.Add("LBDropDepth")
                Result.Add("LBFillGaps")
                Result.Add("DBPressBailCoverage")
                Result.Add("DBRunContain")
                Result.Add("DBBump")
                Result.Add("DBBaitQB")
                Result.Add("DBCatchUpSpeed")
                Result.Add("DBTechnique")

            Case "OLB", "ILB"
                Result.Add("OLPulling")
                Result.Add("OLSlide")
                Result.Add("OLMoveInSpace")
                Result.Add("OLSnapAbility")
                Result.Add("OLLongSnapAbility")
                Result.Add("OLAnchorAbility")
                Result.Add("OLRecover")
                Result.Add("DLRunAtHim")
                Result.Add("DLAgainstPullAbility")
                Result.Add("DLSlideAbility")
                Result.Add("DLRunPursuit")
                Result.Add("DLCanTakeDoubleTeam")
                Result.Add("DLFinish")
                Result.Add("DLSetUpPassRush")
                Result.Add("QBMechanics")
                Result.Add("QBRelQuickness")
                Result.Add("QBAccuracy")
                Result.Add("QBDecMaking")
                Result.Add("QBBallHandling")
                Result.Add("QBLocateRec")
                Result.Add("QBPocketPresence")
                Result.Add("QBEscape")
                Result.Add("QBScrambling")
                Result.Add("QBRolloutRight")
                Result.Add("QBRolloutLeft")
                Result.Add("QBArmStrength")
                Result.Add("QBTouch")
                Result.Add("KickAccuracy")
                Result.Add("KFakeAbility")
                Result.Add("KKickRise")
                Result.Add("PFakeAbility")
                Result.Add("PDistance")
                Result.Add("PHangTime")
                Result.Add("WRRunDBOff")
                Result.Add("WRDisguiseRoute")
                Result.Add("RBRunvision")
                Result.Add("RBSetsUpBlocks")
                Result.Add("RBPatience")
                Result.Add("ReleaseOffLine")
                Result.Add("RouteRunning")
                Result.Add("DBPressBailCoverage")
                Result.Add("DBRunContain")
                Result.Add("DBBump")
                Result.Add("DBCatchUpSpeed")
                Result.Add("DBTechnique")

            Case "CB", "FS", "SS"
                Result.Add("OLPulling")
                Result.Add("OLSlide")
                Result.Add("OLMoveInSpace")
                Result.Add("OLSnapAbility")
                Result.Add("OLLongSnapAbility")
                Result.Add("OLAnchorAbility")
                Result.Add("OLRecover")
                Result.Add("DLRunAtHim")
                Result.Add("DLAgainstPullAbility")
                Result.Add("DLSlideAbility")
                Result.Add("DLRunPursuit")
                Result.Add("DLCanTakeDoubleTeam")
                Result.Add("DLFinish")
                Result.Add("DLSetUpPassRush")
                Result.Add("QBMechanics")
                Result.Add("QBRelQuickness")
                Result.Add("QBAccuracy")
                Result.Add("QBDecMaking")
                Result.Add("QBBallHandling")
                Result.Add("QBLocateRec")
                Result.Add("QBPocketPresence")
                Result.Add("QBEscape")
                Result.Add("QBScrambling")
                Result.Add("QBRolloutRight")
                Result.Add("QBRolloutLeft")
                Result.Add("QBArmStrength")
                Result.Add("QBTouch")
                Result.Add("KickAccuracy")
                Result.Add("KFakeAbility")
                Result.Add("KKickRise")
                Result.Add("PFakeAbility")
                Result.Add("PDistance")
                Result.Add("PHangTime")
                Result.Add("WRRunDBOff")
                Result.Add("WRDisguiseRoute")
                Result.Add("RBRunvision")
                Result.Add("RBSetsUpBlocks")
                Result.Add("RBPatience")
                Result.Add("ReleaseOffLine")
                Result.Add("RouteRunning")
                Result.Add("LBDropDepth")
                Result.Add("LBFillGaps")

            Case "K", "P"
                Result.Add("OLPulling")
                Result.Add("OLSlide")
                Result.Add("OLMoveInSpace")
                Result.Add("OLSnapAbility")
                Result.Add("OLLongSnapAbility")
                Result.Add("OLAnchorAbility")
                Result.Add("OLRecover")
                Result.Add("DLRunAtHim")
                Result.Add("DLAgainstPullAbility")
                Result.Add("DLSlideAbility")
                Result.Add("DLRunPursuit")
                Result.Add("DLCanTakeDoubleTeam")
                Result.Add("DLFinish")
                Result.Add("DLSetUpPassRush")
                Result.Add("QBMechanics")
                Result.Add("QBRelQuickness")
                Result.Add("QBAccuracy")
                Result.Add("QBDecMaking")
                Result.Add("QBBallHandling")
                Result.Add("QBLocateRec")
                Result.Add("QBPocketPresence")
                Result.Add("QBEscape")
                Result.Add("QBScrambling")
                Result.Add("QBRolloutRight")
                Result.Add("QBRolloutLeft")
                Result.Add("QBArmStrength")
                Result.Add("QBTouch")
                Result.Add("RunBlocking")
                Result.Add("WRRunDBOff")
                Result.Add("WRDisguiseRoute")
                Result.Add("RBRunvision")
                Result.Add("RBSetsUpBlocks")
                Result.Add("RBPatience")
                Result.Add("ReleaseOffLine")
                Result.Add("RouteRunning")
                Result.Add("LBDropDepth")
                Result.Add("LBFillGaps")
                Result.Add("BodyCatch")
                Result.Add("CatchWhenHit")
                Result.Add("BreaksTackles")
                Result.Add("RunAfterCatch")
                Result.Add("HandUse")
                Result.Add("DBPressBailCoverage")
                Result.Add("DBRunContain")
                Result.Add("DBBump")
                Result.Add("DBCatchUpSpeed")
                Result.Add("DBTechnique")
        End Select

        Return Result
    End Function
    Public Shared Function GetRarelyUsed(ByVal pos As String) As List(Of String)
        Dim Result As New List(Of String)
        Select Case pos
            Case "QB"
                Result.Add("ReleaseOffLine")
                Result.Add("RunBlocking")
                Result.Add("RBRunVision")
                Result.Add("RBSetsUpBlocks")
                Result.Add("RBPatience")
                Result.Add("BodyCatch")
                Result.Add("CatchWhenHit")
                Result.Add("RunAfterCatch")
                Result.Add("RouteRunning")
                Result.Add("AdjustToBall")
                Result.Add("Tackling")
                Result.Add("AvoidBlockers")
                Result.Add("ShedBlock")
                Result.Add("DefeatBlock")
                Result.Add("StiffArm")

            Case "RB", "FB"
                Result.Add("Tackling")
                Result.Add("AvoidBlockers")
                Result.Add("ShedBlock")
                Result.Add("DefeatBlock")
                Result.Add("QBMechanics")
                Result.Add("QBRelQuickness")
                Result.Add("QBAccuracy")
                Result.Add("QBDecMaking")
                Result.Add("QBBallHandling")
                Result.Add("QBLocateRec")
                Result.Add("QBPocketPresence")
                Result.Add("QBEscape")
                Result.Add("QBScrambling")
                Result.Add("QBRolloutRight")
                Result.Add("QBRolloutLeft")
                Result.Add("QBArmStrength")
                Result.Add("QBTouch")

            Case "WR", "TE"
                Result.Add("Tackling")
                Result.Add("AvoidBlockers")
                Result.Add("ShedBlock")
                Result.Add("DefeatBlock")
                Result.Add("QBMechanics")
                Result.Add("QBRelQuickness")
                Result.Add("QBAccuracy")
                Result.Add("QBDecMaking")
                Result.Add("QBBallHandling")
                Result.Add("QBLocateRec")
                Result.Add("QBPocketPresence")
                Result.Add("QBEscape")
                Result.Add("QBScrambling")
                Result.Add("QBRolloutRight")
                Result.Add("QBRolloutLeft")
                Result.Add("QBArmStrength")
                Result.Add("QBTouch")
                Result.Add("RBRunvision")
                Result.Add("RBSetsUpBlocks")
                Result.Add("RBPatience")

            Case "OT", "C", "OG"
                Result.Add("Tackling")
                Result.Add("AvoidBlockers")
                Result.Add("ShedBlock")
                Result.Add("DefeatBlock")

            Case "DE", "DT"
                Result.Add("ManToManCoverage")
                Result.Add("BodyCatch")
                Result.Add("BreaksTackles")
                Result.Add("StiffArm")

            Case "OLB", "ILB"
                Result.Add("RunBlocking")
                Result.Add("BreaksTackles")
                Result.Add("StiffArm")

            Case "CB", "FS", "SS"
                Result.Add("RunBlocking")
                Result.Add("BreaksTackles")
                Result.Add("StiffArm")

            Case "K", "P"
                Result.Add("StiffArm")

        End Select
        'Reaction Int NULL, QAB int NULL, COD int NULL, Hands int NULL, BodyCatch int NULL, ReleaseOffLine int NULL, CatchWhenHit int NULL, BreaksTackles int NULL,
        'ContactBalance Int NULL, RunAfterCatch int NULL, LowerBodyStrength int NULL, UpperBodyStrength int NULL, Footwork int NULL, HandUse int NULL, JumpingAbility int NULL, PassBlockVsPower int NULL, PassBlockVsSpeed int NULL, RunBlocking int NULL,
        'PlaySpeed Int NULL, RouteRunning int NULL, KickAccuracy int NULL, AdjustToBall int NULL, Tackling int NULL, Blitz int NULL, AvoidBlockers int NULL, ShedBlock int NULL, DefeatBlock int NULL, ManToManCoverage int NULL, ZoneCoverage int NULL,
        'RETKickReturn Int NULL, RETPuntReturn int NULL, PlayStrength int NULL, QBMechanics int NULL, QBRelQuickness int NULL, QBAccuracy int NULL, QBDecMaking int NULL, QBBallHandling int NULL, QBLocateRec int NULL, QBPocketPresence int NULL, QBEscape int NULL,
        'QBScrambling Int NULL, QBRolloutRight int NULL, QBRolloutLeft int NULL, QBArmStrength int NULL, QBTouch int NULL, QBPlayAction int NULL, RBRunVision int NULL, RBSetsUpBlocks int NULL, RBPatience int NULL, WRRunDBOff int NULL,
        'WRDisguiseRoute Int NULL, OLPulling int NULL, OLSlide int NULL, OLMoveInSpace int NULL, OLSnapAbility int NULL, OLLongSnapAbility int NULL, OLAnchorAbility int NULL, OLRecover int NULL, DLPrimaryTech varchar(15) NULL, DLSecondaryTech varchar(15) NULL,
        'DLRunAtHim Int NULL, DLAgainstPullAbility int NULL, DLSlideABility int NULL, DLRunPursuit int NULL, DLPassRushTech varchar(15) NULL, DLCanTakeDoubleTeam int NULL, DLFinish int NULL, DLsetUpPassRush int NULL, LBDropDepth int NULL, LBFillGaps int NULL,
        'DBPressBailCoverage Int NULL, DBRunContain int NULL, DBBump int NULL, DBBaitQB int NULL, DBCatchUpSpeed int NULL, DBTechnique int NULL, KFakeAbility int NULL, KKickRise int NULL, PFakeAbility int NULL, PDistance int NULL, PHangTime int NULL,
        'STCoverage Int NULL, STWillingness int NULL, STAssignment int NULL, STDiscipline int NULL, Flexibility int NULL, Consistency int NULL, Instincts int NULL, FilmStudy int NULL, Durability int NULL, Explosion int NULL, DeliversBlow int NULL,
        'Toughness Int NULL, ReadKeys int NULL, FieldAwareness int NULL, PlaybookKnowledge int NULL, BallSecurity int NULL, LovesFootball int NULL, Concentration int NULL, HandlesElements int NULL, Potential int NULL, Raw int NULL, "
        Return Result
    End Function
    ''' <summary>
    ''' Gets the key ratings by running the exponential decay formula = RatingsStartPoint*((1-ratingsdecay)^TimeValue)+MT.generateint32(-10,10)
    ''' account for Draft Round and position of the player. Starting Point of the Ratings is the highest value---in this case 90
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="idNum"></param>
    ''' <param name="pos"></param>
    ''' <param name="draftRound"></param>
    ''' <param name="ratingsStartPoint"></param>
    ''' <param name="ratingsAtTimeT"></param>
    ''' <param name="timeT"></param>
    Public Shared Sub GetKeyRatings(ByVal dt As DataTable, ByVal idNum As Integer, ByVal pos As String, draftRound As String, Optional ByVal ratingsStartPoint As Integer = 90, Optional ByVal ratingsAtTimeT As Integer = 50, Optional ByVal timeT As Integer = 7)
        Dim TimeValue As Integer
        Dim RatingsDecay As Double
        Dim Rating As Integer

        Dim KeyRatingsList As New List(Of String)
        KeyRatingsList = KeyRatings(pos)

        Select Case draftRound
            Case "R1Top5"
                TimeValue = 0
            Case "R1Top10"
                TimeValue = 1
            Case "R1MidFirst"
                TimeValue = 2
            Case "R1LateFirst"
                TimeValue = 3
            Case "R2"
                TimeValue = 4
            Case "R3"
                TimeValue = 5
            Case "R4"
                TimeValue = 6
            Case "R5"
                TimeValue = 7
            Case "R6"
                TimeValue = 8
            Case "R7"
                TimeValue = 9
            Case "PUFA"
                TimeValue = 10
            Case "LUFA"
                TimeValue = 11
            Case "PracSquad"
                TimeValue = 12
            Case "Reject"
                TimeValue = 13
        End Select
        Try
            RatingsDecay = GetRatingsDecay(ratingsStartPoint, ratingsAtTimeT, timeT)

            For i As Integer = 0 To KeyRatingsList.Count - 1 'runs through the attribute key attribute list and aplpies the proper ratings based on round the player is generated for
                Rating = CInt(ratingsStartPoint * ((1 + RatingsDecay) ^ TimeValue) + MT.GenerateInt32(-10, 10))
                dt.Rows(idNum).Item(String.Format("{0}", KeyRatingsList(i))) = Rating
                'Add the Key Rating to the KeyRatings grade total
                ActualGrade.KeyRatings += Rating
                'Console.WriteLine("{0}:  {1}", KeyRatingsList(i), DT.Rows(IDNum).Item(String.Format("{0}", KeyRatingsList(i))))
            Next i
        Catch ex As System.ArgumentOutOfRangeException
            Console.WriteLine(ex.Data)
            Console.WriteLine(ex.Message)
        End Try
        KeyRatingsList.Clear()
    End Sub

    ''' <summary>
    ''' creates the decay between first round players and reject players---y(t)=ae^kt where y(t) is the value at the current time, a=initial time, k=rate of decay and t=time
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function GetRatingsDecay(ByVal initialValue As Integer, ByVal valueAtTimeT As Integer, ByVal timeT As Integer) As Single
        Dim k As Single
        k = (Math.Log(valueAtTimeT / initialValue)) / timeT
        Return k
    End Function
    ''' <summary>
    ''' These are the KeyRatings for the position---for instance, the ratings that determine what round a player is drafted in---Average of 50 is for a player drafted in round 5, results will be higher or lower based on that
    ''' List of ratings is returned
    ''' </summary>
    ''' <param name="pos"></param>
    ''' <returns></returns>
    Private Shared Function KeyRatings(ByVal pos As String) As List(Of String)

        Dim Result As New List(Of String)

        Select Case pos
            Case "QB"
                Result.Add("QBArmStrength")
                Result.Add("QBAccuracy")
                Result.Add("QBTouch")
                Result.Add("Footwork")
                Result.Add("QBRelQuickness")
                Result.Add("QBLocateRec")
                Result.Add("Leadership")
                Result.Add("QBDecMaking")
                Result.Add("QBMechanics")
                Result.Add("QBPocketPresence")

            Case "RB"
                Result.Add("RBRunVision")
                Result.Add("QAB")
                Result.Add("Explosion")
                Result.Add("LowerBodyStrength")
                Result.Add("ContactBalance")
                Result.Add("COD")
                Result.Add("RBPatience")
                Result.Add("RBSetsUpBlocks")
                Result.Add("Flexibility")
                Result.Add("Instincts")

            Case "FB"
                Result.Add("RunBlocking")
                Result.Add("LowerBodyStrength")
                Result.Add("Toughness")
                Result.Add("Hands")
                Result.Add("ContactBalance")
                Result.Add("Reaction")
                Result.Add("Explosion")
                Result.Add("DeliversBlow")
                Result.Add("QAB")
                Result.Add("COD")

            Case "WR"
                Result.Add("Explosion")
                Result.Add("QAB")
                Result.Add("COD")
                Result.Add("ReleaseOffLine")
                Result.Add("WRDisguiseRoute")
                Result.Add("AdjustToBall")
                Result.Add("RunAfterCatch")
                Result.Add("Toughness")
                Result.Add("Flexibility")
                Result.Add("Hands")

            Case "TE"
                Result.Add("PassBlockVsSpeed")
                Result.Add("QAB")
                Result.Add("COD")
                Result.Add("ReleaseOffLine")
                Result.Add("RunBlocking")
                Result.Add("AdjustToBall")
                Result.Add("RunAfterCatch")
                Result.Add("Toughness")
                Result.Add("Flexibility")
                Result.Add("Hands")

            Case "OT"
                Result.Add("HandUse")
                Result.Add("PassBlockVsPower")
                Result.Add("PassBlockVsSpeed")
                Result.Add("RunBlocking")
                Result.Add("Footwork")
                Result.Add("QAB")
                Result.Add("Flexibility")
                Result.Add("Explosion")
                Result.Add("OLRecover")
                Result.Add("Reaction")

            Case "C"
                Result.Add("HandUse")
                Result.Add("OLAnchorAbility")
                Result.Add("PassBlockVsPower")
                Result.Add("RunBlocking")
                Result.Add("Footwork")
                Result.Add("QAB")
                Result.Add("OLPulling")
                Result.Add("Explosion")
                Result.Add("Leadership")
                Result.Add("Toughness")

            Case "OG"
                Result.Add("HandUse")
                Result.Add("OLAnchorAbility")
                Result.Add("PassBlockVsPower")
                Result.Add("RunBlocking")
                Result.Add("Footwork")
                Result.Add("QAB")
                Result.Add("OLPulling")
                Result.Add("Explosion")
                Result.Add("Toughness")
                Result.Add("Flexibility")

            Case "DE"
                Result.Add("DefeatBlock")
                Result.Add("ContactBalance")
                Result.Add("Reaction")
                Result.Add("HandUse")
                Result.Add("QAB")
                Result.Add("Explosion")
                Result.Add("Flexibility")
                Result.Add("ReadKeys")
                Result.Add("Instincts")
                Result.Add("COD")

            Case "DT"
                Result.Add("Toughness")
                Result.Add("ShedBlock")
                Result.Add("Reaction")
                Result.Add("HandUse")
                Result.Add("QAB")
                Result.Add("Explosion")
                Result.Add("Flexibility")
                Result.Add("ReadKeys")
                Result.Add("DLRunAtHim")
                Result.Add("COD")

            Case "OLB"
                Result.Add("QAB")
                Result.Add("COD")
                Result.Add("Explosion")
                Result.Add("Flexibility")
                Result.Add("ReadKeys")
                Result.Add("Reaction")
                Result.Add("ShedBlock")
                Result.Add("Tackling")
                Result.Add("ZoneCoverage")
                Result.Add("AvoidBlockers")

            Case "ILB"
                Result.Add("QAB")
                Result.Add("COD")
                Result.Add("LBFillGaps")
                Result.Add("Leadership")
                Result.Add("ReadKeys")
                Result.Add("Reaction")
                Result.Add("ShedBlock")
                Result.Add("Tackling")
                Result.Add("AvoidBlockers")
                Result.Add("Toughness")

            Case "CB"
                Result.Add("Explosion")
                Result.Add("QAB")
                Result.Add("COD")
                Result.Add("Flexibility")
                Result.Add("Footwork")
                Result.Add("DBBaitQB")
                Result.Add("DBTechnique")
                Result.Add("DBCatchUpSpeed")
                Result.Add("ManToManCoverage")
                Result.Add("AdjustToBall")

            Case "FS", "SS"
                Result.Add("Explosion")
                Result.Add("QAB")
                Result.Add("COD")
                Result.Add("Flexibility")
                Result.Add("Footwork")
                Result.Add("ReadKeys")
                Result.Add("Tackling")
                Result.Add("DBTechnique")
                Result.Add("DeliversBlow")
                Result.Add("DBBaitQB")

            Case "K"
                Result.Add("KickAccuracy")
                Result.Add("Consistency")
                Result.Add("Confidence")
                Result.Add("Clutch")
                Result.Add("Footwork")
                Result.Add("LowerBodyStrength")
                Result.Add("Explosion")
                Result.Add("Reaction")
                Result.Add("HandlesElements")
                Result.Add("Flexibility")

            Case "P"
                Result.Add("Flexibility")
                Result.Add("PHangTime")
                Result.Add("Footwork")
                Result.Add("LowerBodyStrength")
                Result.Add("Reaction") 'for catching bad snaps
                Result.Add("Hands")
                Result.Add("Consistency")
                Result.Add("HandlesElements")
                Result.Add("JumpingAbility")
                Result.Add("Explosion")

        End Select

        Return Result
    End Function
    ''' <summary>
    ''' Each Positional Type has its stregnths and weaknesses---this creates a list of those strengths and weaknesses and then gives a major(30-50% boost) or minor(15-25% boost) increase for strengths and a major or minor decrease for weaknesses
    ''' Primary weaknesses will be harder to improve than normal attributes as it is an INHERENT weakness of the style this player plays, and by the same token primary strengths will be slower to degrade
    ''' Each Position type has 2 Primary Strengths/Weaknesses and 2 Secondary Strengths/Weaknesses, other than Balanced which has a range of +/-10% on all attributes
    ''' </summary>
    ''' <param name="pos"></param>
    ''' <param name="posType"></param>
    ''' <param name="idNum"></param>
    ''' <param name="dt"></param>
    Public Shared Sub GetPosRatings(pos As String, ByVal posType As String, ByVal idNum As Integer, ByVal dt As DataTable)
        Dim PrimStrength As New List(Of String)
        Dim SecStrength As New List(Of String)
        Dim PrimWeakness As New List(Of String)
        Dim SecWeakness As New List(Of String)
        Dim Balanced As New List(Of String)

        Select Case pos
            Case "QB"
                Select Case posType
                    Case "StrongArm" 'strong arm and confidence but often tries to force balls into coverage, makes bad decisions and is slow
                        PrimStrength.Add("QBArmStrength")
                        SecStrength.Add("QBMechanics")
                        SecStrength.Add("Toughness")
                        PrimStrength.Add("Confidence")
                        SecWeakness.Add("QBEscape")
                        PrimWeakness.Add("PlaySpeed")
                        SecWeakness.Add("QBDecMaking")
                        PrimWeakness.Add("QBTouch")

                    Case "WestCoast" 'good accuracy, touch and escapability but lacks arm strength and tends to be less durable, also tends to get routes jumped more often due to throwing short so much
                        PrimStrength.Add("QBAccuracy")
                        PrimStrength.Add("QBTouch")
                        SecStrength.Add("QBEscape")
                        SecStrength.Add("Footwork")
                        PrimWeakness.Add("QBArmStrength")
                        PrimWeakness.Add("FieldAwareness")
                        SecWeakness.Add("Durability")
                        SecWeakness.Add("Toughness")

                    Case "Balanced" 'No Big Boosts but no real weaknesses either
                        Balanced.Add("QBArmStrength")
                        Balanced.Add("QBAccuracy")
                        Balanced.Add("QBDecMaking")
                        Balanced.Add("QBTouch")
                        Balanced.Add("QBEscape")
                        Balanced.Add("QBPocketPresence")
                        Balanced.Add("Footwork")
                        Balanced.Add("QBRelQuickness")

                    Case "FieldGeneral" 'Leadership, LocateReceivers, FieldAwareness, Low Escape--stands in the pocket too long and takes sacks sometimes, Low Ballhandling gets stripped a lot from getting hit, Coachability is low because he thinks he knows it all
                        SecStrength.Add("QBLocateRec")
                        PrimStrength.Add("Leadership")
                        SecStrength.Add("FieldAwareness")
                        PrimStrength.Add("Clutch")
                        PrimWeakness.Add("BallSecurity")
                        PrimWeakness.Add("Coachability")
                        SecWeakness.Add("QBRelQuickness")
                        SecWeakness.Add("QBEscape")

                    Case "PocketPasser" 'Sits in the pocket and will carve a defense up, but tends to lose confidence easily if he starts getting hit. Not tough, moves around the pocket well but has issues escaping from the pocket
                        PrimStrength.Add("QBAccuracy")
                        PrimStrength.Add("QBTouch")
                        SecStrength.Add("QBArmstrength")
                        SecStrength.Add("QBPocketPresence")
                        PrimWeakness.Add("QBEscape")
                        PrimWeakness.Add("Confidence")
                        SecWeakness.Add("Toughness")
                        SecWeakness.Add("BallSecurity")

                    Case "Mobile" 'Mobile QB---boosts to playspeed, escape, rollout, QAB and COD--typically suffer from poor footwork, lack of pocket presence
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("QBScrambling")
                        SecStrength.Add("QBEscape")
                        SecStrength.Add("Instincts")
                        PrimWeakness.Add("Footwork")
                        PrimWeakness.Add("QBPocketPresence")
                        SecWeakness.Add("Durability")
                        SecWeakness.Add("QBMechanics")
                End Select

            Case "RB"
                Select Case posType
                    Case "Balanced"
                        Balanced.Add("ContactBalance")
                        Balanced.Add("BreaksTackles")
                        Balanced.Add("Hands")
                        Balanced.Add("QAB")
                        Balanced.Add("COD")
                        Balanced.Add("RBPatience")
                        Balanced.Add("RBRunVision")
                        Balanced.Add("RBSetsUpBlocks")

                    Case "PowerBack" 'Toughness, Explosion, DeliversBlow, BreaksTackle but weak speed, QAB, COD and Patience
                        PrimStrength.Add("ContactBalance")
                        PrimStrength.Add("BreaksTackles")
                        SecStrength.Add("StiffArm")
                        SecStrength.Add("DeliversBlow")
                        PrimWeakness.Add("PlaySpeed")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("COD")
                        SecWeakness.Add("RBPatience")

                    Case "SpeedBack"
                        PrimStrength.Add("Flexibility")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("COD")
                        SecStrength.Add("QAB")
                        PrimWeakness.Add("BreaksTackles")
                        PrimWeakness.Add("DeliversBlow")
                        SecWeakness.Add("Toughness")
                        SecWeakness.Add("ContactBalance")

                    Case "ReceivingBack"
                        PrimStrength.Add("Hands")
                        PrimStrength.Add("RouteRunning")
                        SecStrength.Add("RunAfterCatch")
                        SecStrength.Add("FieldAwareness")
                        PrimWeakness.Add("RBRunVision")
                        PrimWeakness.Add("RBSetsUpBlocks")
                        SecWeakness.Add("BreaksTackles")
                        SecWeakness.Add("Toughness")

                    Case "NorthSouthBack"
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("RBRunVision")
                        SecStrength.Add("ContactBalance")
                        SecStrength.Add("BreaksTackles")
                        PrimWeakness.Add("COD")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("Hands")
                        SecWeakness.Add("Durability")
                End Select
            Case "FB"

                Select Case posType
                    Case "BatteringRam" 'typical blocking FB
                        PrimStrength.Add("RunBlocking")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("ContactBalance")
                        SecStrength.Add("LowerBodyStrength")
                        PrimWeakness.Add("COD")
                        PrimWeakness.Add("RBRunVision")
                        SecWeakness.Add("RBSetsUpBlocks")
                        SecWeakness.Add("QAB")

                    Case "Balanced"
                        Balanced.Add("Runblocking")
                        Balanced.Add("Hands")
                        Balanced.Add("Explosion")
                        Balanced.Add("LowerBodyStrength")
                        Balanced.Add("RouteRunning")
                        Balanced.Add("QAB")
                        Balanced.Add("COD")
                        Balanced.Add("AdjustToBall")

                    Case "Receiving"
                        PrimStrength.Add("Hands")
                        PrimStrength.Add("RouteRunning")
                        SecStrength.Add("AdjustToBall")
                        SecStrength.Add("BodyCatch")
                        PrimWeakness.Add("RunBlocking")
                        PrimWeakness.Add("Explosion")
                        SecWeakness.Add("ContactBalance")
                        SecWeakness.Add("LowerBodyStrength")

                End Select
            Case "WR"
                Select Case posType
                    Case "Balanced"
                        Balanced.Add("PlaySpeed")
                        Balanced.Add("Explosion")
                        Balanced.Add("Hands")
                        Balanced.Add("ReleaseOffLine")
                        Balanced.Add("QAB")
                        Balanced.Add("AdjustToBall")
                        Balanced.Add("RouteRunning")
                        Balanced.Add("RunBlocking")

                    Case "Speed"
                        PrimStrength.Add("Reaction")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("WRRunDBOff")
                        SecStrength.Add("QAB")
                        PrimWeakness.Add("ReleaseOffLine")
                        PrimWeakness.Add("Hands")
                        SecWeakness.Add("Durability")
                        SecWeakness.Add("BodyCatch")

                    Case "Possession"
                        PrimStrength.Add("Hands")
                        PrimStrength.Add("RouteRunning")
                        SecStrength.Add("ReleaseOffLine")
                        SecStrength.Add("RunBlocking")
                        PrimWeakness.Add("Explosion")
                        PrimWeakness.Add("RunAfterCatch")
                        SecWeakness.Add("PlaySpeed")
                        SecWeakness.Add("COD")

                    Case "Polished"
                        PrimStrength.Add("RouteRunning")
                        PrimStrength.Add("Runblocking")
                        SecStrength.Add("ReleaseOFfLine")
                        SecStrength.Add("FieldAwareness")
                        PrimWeakness.Add("ContactBalance")
                        PrimWeakness.Add("COD")
                        SecWeakness.Add("Concentration")
                        SecWeakness.Add("JumpingAbility")

                    Case "RZThreat"
                        PrimStrength.Add("JumpingAbility")
                        PrimStrength.Add("Hands")
                        SecStrength.Add("FieldAwareness")
                        SecStrength.Add("Aggressive")
                        PrimWeakness.Add("PlaySpeed")
                        PrimWeakness.Add("PlayBookKnowledge")
                        SecWeakness.Add("Flexibility")
                        SecWeakness.Add("QAB")

                End Select

            Case "TE"
                Select Case posType
                    Case "Balanced"
                        Balanced.Add("Playspeed")
                        Balanced.Add("QAB")
                        Balanced.Add("Toughness")
                        Balanced.Add("Hands")
                        Balanced.Add("Runblocking")
                        Balanced.Add("ReleaseOffLine")
                        Balanced.Add("Explosion")
                        Balanced.Add("RouteRunning")

                    Case "BlockingTE"
                        PrimStrength.Add("RunBlocking")
                        PrimStrength.Add("LowerBodyStrength")
                        SecStrength.Add("PassBlockVsPower")
                        SecStrength.Add("Toughness")
                        PrimWeakness.Add("PlaySpeed")
                        PrimWeakness.Add("Hands")
                        SecWeakness.Add("QAB")
                        SecWeakness.Add("COD")

                    Case "VerticalThreat"
                        PrimStrength.Add("QAB")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("ReleaseOffLine")
                        SecStrength.Add("Hands")
                        PrimWeakness.Add("RunBlocking")
                        PrimWeakness.Add("PassBlockVsPower")
                        SecWeakness.Add("LowerBodyStrength")
                        SecWeakness.Add("Toughness")

                    Case "Hybrid"
                        PrimStrength.Add("QAB")
                        PrimStrength.Add("COD")
                        SecStrength.Add("RunBlocking")
                        SecStrength.Add("Hands")
                        PrimWeakness.Add("Explosion")
                        PrimWeakness.Add("AdjustToBall")
                        SecWeakness.Add("Flexibility")
                        SecWeakness.Add("RouteRunning")

                    Case "Receiving"
                        PrimStrength.Add("RouteRunning")
                        PrimStrength.Add("Hands")
                        SecStrength.Add("QAB")
                        SecStrength.Add("AdjustToBall")
                        PrimWeakness.Add("PassBlockVsPower")
                        PrimWeakness.Add("Toughness")
                        SecWeakness.Add("RunBlocking")
                        SecWeakness.Add("ContactBalance")
                End Select

            Case "OT"
                Select Case posType
                    Case "LTProtoType" 'Strong passblocking Taclke versus speed, not always as good versus power, but not weak at it either
                        PrimStrength.Add("PassBlockVsSpeed")
                        PrimStrength.Add("Footwork")
                        SecStrength.Add("Flexibility")
                        SecStrength.Add("QAB")
                        PrimWeakness.Add("ContactBalance")
                        PrimWeakness.Add("Toughness")
                        SecWeakness.Add("RunBlocking")
                        SecWeakness.Add("OLAnchorAbility")

                    Case "RTProtoType" 'Run blocking tackle
                        PrimStrength.Add("RunBlocking")
                        PrimStrength.Add("OLAnchorAbility")
                        SecStrength.Add("Explosion")
                        SecStrength.Add("LowerBodyStrength")
                        PrimWeakness.Add("PassBlockVsSpeed")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("OLMoveInSpace")
                        SecWeakness.Add("OLPulling")

                    Case "Balanced" '
                        Balanced.Add("PassBlockVsSpeed")
                        Balanced.Add("PassBlockVsPower")
                        Balanced.Add("Runblocking")
                        Balanced.Add("Flexibility")
                        Balanced.Add("ContactBalance")
                        Balanced.Add("HandUse")
                        Balanced.Add("QAB")
                        Balanced.Add("Footwork")

                    Case "AthleticLacksTechnique"
                        PrimStrength.Add("QAB")
                        PrimStrength.Add("Flexibility")
                        SecStrength.Add("OLRecover")
                        SecStrength.Add("PassBlockVsSpeed")
                        PrimWeakness.Add("Footwork")
                        PrimWeakness.Add("HandUse")
                        SecWeakness.Add("PassBlockVsPower")
                        SecWeakness.Add("ContactBalance")

                    Case "TechniqueLacksAthleticism"
                        PrimStrength.Add("HandUse")
                        PrimStrength.Add("Footwork")
                        SecStrength.Add("RunBlocking")
                        SecStrength.Add("OLRecover")
                        PrimWeakness.Add("Flexibility")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("PassBlockVsPower")
                        SecWeakness.Add("PassBlockVsSpeed")
                End Select

            Case "C", "OG"
                If pos = "C" Then
                    dt.Rows(idNum).Item("OLSnapAbility") = MT.GetGaussian(75, 8.33) 'this is not really a weakness or stength just a special job they have to do
                End If
                Select Case posType

                    Case "Balanced"
                        Balanced.Add("OLPulling")
                        Balanced.Add("QAB")
                        Balanced.Add("PassBlockVsPower")
                        Balanced.Add("Flexibility")
                        Balanced.Add("HandUse")
                        Balanced.Add("Footwork")
                        Balanced.Add("RunBlocking")
                        Balanced.Add("PassBlockVsPower")

                    Case "RoadGrader" 'big strong, nasty run blocker but suffers in pass protection and is not quick enough to pull---mostly for man to man blocking schemes, not good for zone blocking schemes
                        PrimStrength.Add("RunBlocking")
                        PrimStrength.Add("LowerBodyStrength")
                        SecStrength.Add("ContactBalance")
                        SecStrength.Add("HandUse")
                        PrimWeakness.Add("PassBlockVsSpeed")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("PassBlockVsPower")
                        SecWeakness.Add("Flexibility")

                    Case "RunBlocking" 'better run blocker than pass blocker
                        PrimStrength.Add("RunBlocking")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("OLPulling")
                        SecStrength.Add("HandUse")
                        PrimWeakness.Add("OLRecover")
                        PrimWeakness.Add("Reaction")
                        SecWeakness.Add("PassBlockVsPower")
                        SecWeakness.Add("PassBlockVsSpeed")

                    Case "ZoneBlocker" 'lighter, quicker OL used for pulling and getting to the second level in zone schemes, but struggle in strength
                        PrimStrength.Add("QAB")
                        PrimStrength.Add("OLPulling")
                        SecStrength.Add("OLMoveInSpace")
                        SecStrength.Add("Flexibility")
                        PrimWeakness.Add("PassBlockVsPower")
                        PrimWeakness.Add("LowerBodyStrength")
                        SecWeakness.Add("OLAnchorAbility")
                        SecWeakness.Add("ContactBalance")

                    Case "PassBlocker"
                        PrimStrength.Add("PassBlockVsPower")
                        PrimStrength.Add("PassBlockVsSpeed")
                        SecStrength.Add("Footwork")
                        SecStrength.Add("Flexibility")
                        PrimWeakness.Add("RunBlocking")
                        PrimWeakness.Add("OLAnchorAbility")
                        SecWeakness.Add("LowerBodyStrength")
                        SecWeakness.Add("HandUse")

                End Select

            Case "DE"
                Select Case posType
                    Case "Balanced4-3"
                        Balanced.Add("ReadKeys")
                        Balanced.Add("ShedBlock")
                        Balanced.Add("Flexibility")
                        Balanced.Add("Reaction")
                        Balanced.Add("Explosion")
                        Balanced.Add("DLSLideAbility")
                        Balanced.Add("DLRunAtHim")
                        Balanced.Add("COD")

                    Case "PrototypeLDE4-3" 'pass rushing DE---weaker against run
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("HandUse")
                        SecStrength.Add("Flexibility")
                        SecStrength.Add("QAB")
                        PrimWeakness.Add("LowerBodyStrength")
                        PrimWeakness.Add("DLRunAtHim")
                        SecWeakness.Add("ReadKeys") 'Too busy trying to get up field
                        SecWeakness.Add("FieldAwareness")

                    Case "ProtoTypeRDE4-3" 'run defending DE---typically not as good of a pass rusher
                        PrimStrength.Add("DLRunAtHim")
                        PrimStrength.Add("DLAgainstPullAbility")
                        SecStrength.Add("ShedBlock")
                        SecStrength.Add("LowerBodyStrength")
                        PrimWeakness.Add("Explosion")
                        PrimWeakness.Add("Flexibility")
                        SecWeakness.Add("QAB")
                        SecWeakness.Add("COD")

                    Case "Versatile3-4"
                        PrimStrength.Add("ReadKeys")
                        PrimStrength.Add("DLSlideAbility")
                        SecStrength.Add("COD")
                        SecStrength.Add("ShedBlock")
                        PrimWeakness.Add("Explosion")
                        PrimWeakness.Add("ContactBalance")
                        SecWeakness.Add("HandUse")
                        SecWeakness.Add("LowerBodyStrength")

                    Case "RunStopper3-4"
                        PrimStrength.Add("DLRunAtHim")
                        PrimStrength.Add("LowerBodyStrength")
                        SecStrength.Add("DLAgainstPullAbility")
                        SecStrength.Add("DLRunPursuit")
                        PrimWeakness.Add("Explosion")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("COD")
                        SecWeakness.Add("DLSetUpPassRush")

                    Case "SituationalPassRusher" 'Guys that come in to rush on 3rd downs typically
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("QAB")
                        SecStrength.Add("DLSetUpPassRush")
                        SecStrength.Add("Reaction")
                        PrimWeakness.Add("PlayBookKnowledge")
                        PrimWeakness.Add("DLRunAtHim")
                        SecWeakness.Add("ReadKeys")
                        SecWeakness.Add("ContactBalance") 'only in there for obvious passing downs

                    Case "Hybrid" 'Tweener Type players
                        PrimStrength.Add("ZoneCoverage")
                        PrimStrength.Add("QAB")
                        SecStrength.Add("COD")
                        SecStrength.Add("Flexibility")
                        PrimWeakness.Add("DLRunAtHim")
                        PrimWeakness.Add("DLAgainstPullAbility")
                        SecWeakness.Add("HandUse")
                        SecWeakness.Add("LowerBodyStrength")
                End Select

            Case "DT"
                Select Case posType
                    Case "Penetrator" 'Athletic, quick
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("HandUse")
                        SecStrength.Add("QAB")
                        SecStrength.Add("Reaction")
                        PrimWeakness.Add("ReadKeys")
                        PrimWeakness.Add("DLCanTakeDoubleTeam")
                        SecWeakness.Add("DLSlideAbility")
                        SecWeakness.Add("DLAgainstPullAbility")

                    Case "NoseTackle" 'Big, heavy, strong player used for ability to eat up blocks rather than make plays or penetrate
                        PrimStrength.Add("DLCanTakeDoubleTeam")
                        PrimStrength.Add("DLRunAtHim")
                        SecStrength.Add("LowerBodyStrength")
                        SecStrength.Add("ShedBlock")
                        PrimWeakness.Add("Flexibility")
                        PrimWeakness.Add("Explosion")
                        SecWeakness.Add("QAB")
                        SecWeakness.Add("COD")

                    Case "RunStopper"
                        PrimStrength.Add("DLRunAtHim")
                        PrimStrength.Add("DLSlideAbility")
                        SecStrength.Add("ShedBlock")
                        SecStrength.Add("Tackling")
                        PrimWeakness.Add("Flexibility")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("Explosion")
                        SecWeakness.Add("COD")

                    Case "Balanced"
                        Balanced.Add("DLRunAtHim")
                        Balanced.Add("QAB")
                        Balanced.Add("ShedBlock")
                        Balanced.Add("DLSlideAbility")
                        Balanced.Add("Flexibility")
                        Balanced.Add("COD")
                        Balanced.Add("Explosion")
                        Balanced.Add("Reaction")

                    Case "Versatile"
                        PrimStrength.Add("QAB")
                        PrimStrength.Add("DLSlideAbility")
                        SecStrength.Add("ReadKeys")
                        SecStrength.Add("Flexibility")
                        PrimWeakness.Add("DLCanTakeDoubleTeam")
                        PrimWeakness.Add("DLRunAtHim")
                        SecWeakness.Add("HandUse")
                        SecWeakness.Add("LowerBodyStrength")
                End Select
            Case "OLB"
                Select Case posType
                    Case "WillProtoType4-3" 'must be able to play the run and rush the quarterback, but more than anything they are used in coverage.
                        PrimStrength.Add("QAB")
                        PrimStrength.Add("COD")
                        SecStrength.Add("ZoneCoverage")
                        SecStrength.Add("Flexibility")
                        PrimWeakness.Add("LBFillGaps")
                        PrimWeakness.Add("AvoidBlockers")
                        SecWeakness.Add("ShedBlock")
                        SecWeakness.Add("ContactBalance")

                    Case "PassRush3-4" 'quick, explosive passrusher
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("Flexibility")
                        SecStrength.Add("QAB")
                        SecStrength.Add("HandUse")
                        PrimWeakness.Add("AvoidBlockers")
                        PrimWeakness.Add("ReadKeys")
                        SecWeakness.Add("LBFillGaps")
                        SecWeakness.Add("LBDropDepth")

                    Case "Tweener4-3" 'Athletic but lacking in size, have tendency to get "engulfed" by bigger O-Line players
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("QAB")
                        SecStrength.Add("COD")
                        SecStrength.Add("Flexibility")
                        PrimWeakness.Add("LowerBodyStrength")
                        PrimWeakness.Add("AvoidBlockers")
                        SecWeakness.Add("Tackling")
                        SecWeakness.Add("ContactBalance")

                    Case "SamPrototype4-3" 'must have the power to take on blockers and attack the run, the flexibility to rush the quarterback off the edge & the feet to play in coverage against tight ends. Must be a complete player in a 4-3 defense.
                        PrimStrength.Add("LBFillGaps")
                        PrimStrength.Add("ShedBlock")
                        SecStrength.Add("AvoidBlockers")
                        SecStrength.Add("Explosion")
                        PrimWeakness.Add("COD")
                        PrimWeakness.Add("MantoManCoverage")
                        SecWeakness.Add("LBFillGaps")
                        SecWeakness.Add("AdjustToBall")

                    Case "Balanced"
                        Balanced.Add("LBFillGaps")
                        Balanced.Add("ShedBlock")
                        Balanced.Add("PlaySpeed")
                        Balanced.Add("Explosion")
                        Balanced.Add("Reaction")
                        Balanced.Add("Tackling")
                        Balanced.Add("QAB")
                        Balanced.Add("ReadKeys")
                End Select

            Case "ILB"
                Select Case posType
                    Case "MikeProtoType" 'Mike LB used in either system--Fiedl General/Do Everything backer
                        PrimStrength.Add("Leadership")
                        PrimStrength.Add("ReadKeys")
                        SecStrength.Add("Instincts")
                        SecStrength.Add("FieldAwareness")
                        PrimWeakness.Add("LBDropDepth")
                        PrimWeakness.Add("DefeatBlock")
                        SecWeakness.Add("COD")
                        SecWeakness.Add("AdjustToBall")

                    Case "TedProtoType3-4" 'Run Stopper LB
                        PrimStrength.Add("AvoidBlockers")
                        PrimStrength.Add("LBFillGaps")
                        SecStrength.Add("DeliversBlow")
                        SecStrength.Add("Tackling")
                        PrimWeakness.Add("ZoneCoverage")
                        PrimWeakness.Add("ManToManCoverage")
                        SecWeakness.Add("Flexibility")
                        SecWeakness.Add("PlaySpeed")

                    Case "Cover2ProtoType" 'Coverage, Deep Drops, speed, quickness
                        PrimStrength.Add("ZoneCoverage")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("LBDropDepth")
                        SecStrength.Add("QAB")
                        PrimWeakness.Add("ManToManCoverage")
                        PrimWeakness.Add("Tackling")
                        SecWeakness.Add("AvoidBlockers")
                        SecWeakness.Add("ShedBlock")

                    Case "TacklingMachine"  'Great Tackler
                        PrimStrength.Add("Tackling")
                        PrimStrength.Add("AvoidBlockers")
                        SecStrength.Add("ShedBlock")
                        SecStrength.Add("ReadKeys")
                        PrimWeakness.Add("ManToManCoverage")
                        PrimWeakness.Add("Flexibility")
                        SecWeakness.Add("LBDropDepth")
                        SecWeakness.Add("COD")

                    Case "Balanced"
                        Balanced.Add("Reaction")
                        Balanced.Add("ReadKeys")
                        Balanced.Add("Tackling")
                        Balanced.Add("ManToManCoverage")
                        Balanced.Add("QAB")
                        Balanced.Add("ShedBlock")
                        Balanced.Add("COD")
                        Balanced.Add("AvoidBlockers")
                End Select
            Case "CB"
                Select Case posType
                    Case "CoverCorner" 'great cover skills, not great in run support
                        PrimStrength.Add("ManToManCoverage")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("Flexibility")
                        SecStrength.Add("QAB")
                        PrimWeakness.Add("ZoneCoverage")
                        PrimWeakness.Add("DBRunContain")
                        SecWeakness.Add("AvoidBlockers")
                        SecWeakness.Add("ShedBlock")

                    Case "ZoneCorner" 'good zone cover skills but not man to man
                        PrimStrength.Add("ZoneCoverage")
                        PrimStrength.Add("QAB")
                        SecStrength.Add("COD")
                        SecStrength.Add("ReadKeys")
                        PrimWeakness.Add("ManToManCoverage")
                        PrimWeakness.Add("DBCatchUpSpeed")
                        SecWeakness.Add("Explosion")
                        SecWeakness.Add("Tackling")

                    Case "Balanced"
                        Balanced.Add("ManToManCoverage")
                        Balanced.Add("ZoneCoverage")
                        Balanced.Add("DBCatchUpSpeed")
                        Balanced.Add("PlaySpeed")
                        Balanced.Add("DBRunContain")
                        Balanced.Add("QAB")
                        Balanced.Add("COD")
                        Balanced.Add("Flexibility")

                    Case "RunSupport" 'Great CB for run support, lacks top end speed and agility to play great coverage tho
                        PrimStrength.Add("DBRunContain")
                        PrimStrength.Add("AvoidBlockers")
                        SecStrength.Add("Tackling")
                        SecStrength.Add("Reaction")
                        PrimWeakness.Add("COD")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("DBCatchUpSpeed")
                        SecWeakness.Add("Explosion")

                    Case "SlotCorner" 'very quick and agile, good blitzer off the slot and tackler but lacks top speed and size
                        PrimStrength.Add("QAB")
                        PrimStrength.Add("COD")
                        SecStrength.Add("Blitz")
                        SecStrength.Add("Tackling")
                        PrimWeakness.Add("DBRunContain")
                        PrimWeakness.Add("AvoidBlockers")
                        SecWeakness.Add("DBCatchUpSpeed")
                        SecWeakness.Add("ShedBlock")

                    Case "Physical" 'tough physical corner but has issues with faster receivers due to lacking top end speed, but helps out against the run as well
                        PrimStrength.Add("DBBump")
                        PrimStrength.Add("UpperBodyStrength")
                        SecStrength.Add("ManToManCoverage")
                        SecStrength.Add("DBRunContain")
                        PrimWeakness.Add("COD")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("Explosion")
                        SecWeakness.Add("DBCatchUpSpeed")
                End Select

            Case "FS", "SS"
                Select Case posType
                    Case "Zone" 'good at zone coverage, but not so great at man
                        PrimStrength.Add("ZoneCoverage")
                        PrimStrength.Add("ReadKeys")
                        SecStrength.Add("COD")
                        SecStrength.Add("Reaction")
                        PrimWeakness.Add("ManToManCoverage")
                        PrimWeakness.Add("AvoidBlockers")
                        SecWeakness.Add("DBCatchUpSpeed")
                        SecWeakness.Add("Explosion")

                    Case "Playmaker" 'All over the field making plays but a coverage liability and not the best tackler
                        PrimStrength.Add("Reaction")
                        PrimStrength.Add("DBBaitQB")
                        SecStrength.Add("ReadKeys")
                        SecStrength.Add("FieldAwareness")
                        PrimWeakness.Add("ManToManCoverage")
                        PrimWeakness.Add("Tackling")
                        SecWeakness.Add("ShedBlock")
                        SecWeakness.Add("ZoneCoverage")

                    Case "Balanced"
                        Balanced.Add("ManToManCoverage")
                        Balanced.Add("ZoneCoverage")
                        Balanced.Add("Tackling")
                        Balanced.Add("Blitz")
                        Balanced.Add("ReadKeys")
                        Balanced.Add("AvoidBlockers")
                        Balanced.Add("QAB")
                        Balanced.Add("COD")

                    Case "RunSupport" 'big hitter and run support safety...coverage liability and slower
                        PrimStrength.Add("DBRunContain")
                        PrimStrength.Add("DeliversBlow")
                        SecStrength.Add("Tackling")
                        SecStrength.Add("AvoidBlockers")
                        PrimWeakness.Add("ManToManCoverage")
                        PrimWeakness.Add("ZoneCoverage")
                        SecWeakness.Add("QAB")
                        SecWeakness.Add("Explosion")

                    Case "Hybrid" 'a tweener between the two types
                        PrimStrength.Add("Flexibility")
                        PrimStrength.Add("QAB")
                        SecStrength.Add("MantoManCoverage")
                        SecStrength.Add("ZoneCoverage")
                        PrimWeakness.Add("AvoidBlockers")
                        PrimWeakness.Add("DBCatchUpSpeed")
                        SecWeakness.Add("DBRunContain")
                        SecWeakness.Add("Tackling")
                End Select

            Case "K"
                Select Case posType
                    Case "Clutch" 'makes the big kicks when it counts
                        PrimStrength.Add("Clutch")
                        SecStrength.Add("Confidence")
                        PrimWeakness.Add("Footwork") 'slower kick time
                        SecWeakness.Add("KickAccuracy")

                    Case "Accurate" 'usually accurate but if he misses it can affect confidence for the remainder of the game or a few weeks
                        PrimStrength.Add("KickAccuracy")
                        SecStrength.Add("Consistency")
                        PrimWeakness.Add("Footwork")
                        SecWeakness.Add("Confidence")

                    Case "Balanced"
                        Balanced.Add("Footwork")
                        Balanced.Add("KickAccuracy")
                        Balanced.Add("HandlesElements")
                        Balanced.Add("Consistency")

                    Case "BigLeg"
                        PrimStrength.Add("LowerBodyStrength")
                        SecStrength.Add("Explosion")
                        PrimWeakness.Add("Footwork") 'slower kick times
                        SecWeakness.Add("KKickRise") 'lower trajectory

                    Case "KickOffSpecialist"
                        PrimStrength.Add("LowerBodyStrength")
                        SecStrength.Add("Explosion")
                        PrimWeakness.Add("KickAccuracy")
                        SecWeakness.Add("Consistency")
                End Select

            Case "P"
                Select Case posType
                    Case "BigLeg"
                        PrimStrength.Add("LowerBodyStrength")
                        SecStrength.Add("Explosion")
                        PrimWeakness.Add("Footwork") 'slower kick
                        SecWeakness.Add("AvoidBlockers") 'gets blocked more

                    Case "Accurate"
                        PrimStrength.Add("KickAccuracy")
                        SecStrength.Add("Consistency")
                        PrimWeakness.Add("LowerBodyStrength")
                        SecWeakness.Add("PHangTime")

                    Case "Balanced"
                        Balanced.Add("PHangTime")
                        Balanced.Add("LowerBodyStrength")
                        Balanced.Add("KickAccuracy")
                        Balanced.Add("Footwork")

                    Case "GreatHangTime"
                        PrimStrength.Add("PHangTime")
                        SecStrength.Add("Footwork")
                        PrimWeakness.Add("Consistency")
                        SecWeakness.Add("KickAccuracy")

                    Case "DirectionalPunter"
                        PrimStrength.Add("KickAccuracy")
                        SecStrength.Add("Footwork")
                        PrimWeakness.Add("Explosion")
                        SecWeakness.Add("LowerBodyStrength")

                    Case "AussieRules"
                        PrimStrength.Add("Reaction") 'catch bad punts
                        SecStrength.Add("Consistency")
                        PrimWeakness.Add("Footwork")
                        SecWeakness.Add("LowerBodyStrength")
                End Select
        End Select

        Dim RatingToChange As Integer
        Dim NewRating As Integer
        If PrimStrength.Count > 0 Then
            For i As Integer = 0 To PrimStrength.Count - 1
                'We need to get the difference between the "updated ratings" and the original ratings in "ActualGrade.OtherRatings"
                RatingToChange = dt.Rows(idNum).Item(PrimStrength(i))
                NewRating = RatingToChange * MT.GenerateDouble(1.3, 1.5)
                dt.Rows(idNum).Item(PrimStrength(i)) = NewRating
                ActualGrade.OtherRatings += NewRating - RatingToChange

                RatingToChange = dt.Rows(idNum).Item(SecStrength(i))
                NewRating = RatingToChange * MT.GenerateDouble(1.15, 1.25)
                dt.Rows(idNum).Item(SecStrength(i)) = NewRating
                ActualGrade.OtherRatings += NewRating - RatingToChange

                RatingToChange = dt.Rows(idNum).Item(PrimWeakness(i))
                NewRating = RatingToChange * MT.GenerateDouble(0.5, 0.7)
                dt.Rows(idNum).Item(PrimWeakness(i)) = NewRating
                ActualGrade.OtherRatings += NewRating - RatingToChange

                RatingToChange = dt.Rows(idNum).Item(SecWeakness(i))
                NewRating = RatingToChange * MT.GenerateDouble(0.75, 0.85)
                dt.Rows(idNum).Item(SecWeakness(i)) = NewRating
                ActualGrade.OtherRatings += NewRating - RatingToChange

            Next i
        End If

        If dt.Rows(idNum).Item("PlaySpeed") > 100 Then
            dt.Rows(idNum).Item("FortyYardTime") = 4.3
        Else
            dt.Rows(idNum).Item("FortyYardTime") = Math.Round(((100 - dt.Rows(idNum).Item("PlaySpeed")) / 83.3333) + 4.3, 2)
        End If

        If Balanced.Count <> 0 Then
            For i As Integer = 0 To Balanced.Count - 1
                'Update the ratings again
                RatingToChange = dt.Rows(idNum).Item(Balanced(i))
                NewRating = RatingToChange * MT.GenerateDouble(0.9, 1.1)
                dt.Rows(idNum).Item(Balanced(i)) = NewRating
                ActualGrade.OtherRatings += NewRating - RatingToChange
            Next i
        End If
    End Sub

    Public Shared Function GetPosType(ByVal pos As String, idNum As Integer, ByVal dt As DataTable) As String
        Dim Result As String = ""
        Dim GetNum As Integer = MT.GenerateInt32(1, 100)
        Select Case pos
            Case "QB" 'Gets the type of QB this player will be---stats will be generated within the framework set up for each subtype
                If dt.Rows(idNum).Item("FortyYardTime") < 4.65 Then
                    Result = "Mobile"
                Else
                    Select Case GetNum
                        Case 1 To 10
                            Result = "StrongArm"
                        Case 11 To 30
                            Result = "WestCoast"
                        Case 31 To 40
                            Result = "FieldGeneral"
                        Case 41 To 84
                            Result = "Balanced"
                        Case Else
                            Result = "PocketPasser"
                    End Select
                End If

            Case "RB"
                If dt.Rows(idNum).Item("FortyYardTime") < 4.47 Then
                    Result = "SpeedBack"
                Else
                    Select Case GetNum
                        Case 1 To 40
                            Result = "Balanced"
                        Case 41 To 58
                            Result = "PowerBack"
                        Case 59 To 79
                            Result = "Receiving"
                        Case Else
                            Result = "OneCut"
                    End Select
                End If
            Case "FB"
                Select Case GetNum
                    Case 1 To 40
                        Result = "BatteringRam"
                    Case 41 To 85
                        Result = "Balanced"
                    Case Else
                        Result = "Receiving"
                End Select
            Case "WR"
                If dt.Rows(idNum).Item("FortyYardTime") < 4.47 Then
                    Result = "Speed"
                Else
                    Select Case GetNum
                        Case 1 To 44
                            Result = "Balanced"
                        Case 46 To 65
                            Result = "Possession"
                        Case 66 To 85
                            Result = "Polished"
                        Case Else
                            Result = "RZThreat"
                    End Select
                End If

            Case "TE"
                If dt.Rows(idNum).Item("FortyYardTime") < 4.61 Then
                    Result = "VerticalThreat"
                Else
                    Select Case GetNum
                        Case 1 To 40
                            Result = "Balanced"
                        Case 41 To 60
                            Result = "Blocking"
                        Case 61 To 80
                            Result = "Hybrid" 'HBack Type TE
                        Case Else
                            Result = "Receiving"
                    End Select
                End If

            Case "OT"
                Select Case GetNum
                    Case 1 To 10
                        Result = "LTProtoType"
                    Case 11 To 30
                        Result = "RTProtoType"
                    Case 31 To 70
                        Result = "Balanced"
                    Case 71 To 85
                        Result = "AthleticLacksTechnique"
                    Case 86 To 100
                        Result = "TechniqueLacksAthleticism"
                End Select

            Case "C", "OG"
                Select Case GetNum
                    Case 1 To 20
                        Result = "RunBlocker"
                    Case 21 To 37
                        Result = "RoadGrader"
                    Case 38 To 57
                        Result = "ZoneBlocker"
                    Case 58 To 80
                        Result = "Balanced"
                    Case Else
                        Result = "PassBlocker"
                End Select

            Case "DE"
                Select Case GetNum
                    Case 1 To 25
                        Result = "Balanced4-3"
                    Case 26 To 35
                        Result = "ProtoTypeLDE4-3"
                    Case 36 To 50
                        Result = "ProtoTypeRDE4-3"
                    Case 51 To 65
                        Result = "Versatile3-4"
                    Case 66 To 80
                        Result = "RunStopper3-4"
                    Case 81 To 90
                        Result = "SituationalPassRusher"
                    Case Else
                        Result = "Hybrid"
                End Select

            Case "DT"
                Select Case GetNum
                    Case 1 To 15
                        Result = "Penetrator"
                    Case 16 To 30
                        Result = "NoseTackle"
                    Case 31 To 50
                        Result = "RunStopper"
                    Case 51 To 85
                        Result = "Balanced"
                    Case Else
                        Result = "Versatile"
                End Select

            Case "OLB"
                Select Case GetNum
                    Case 1 To 19
                        Result = "WillProtoType4-3" 'used more in coverage in a 3-4
                    Case 20 To 40
                        Result = "Balanced"
                    Case 41 To 58
                        Result = "PassRush3-4"
                    Case 59 To 70
                        Result = "Tweener4-3" 'Athletic
                    Case Else
                        Result = "SamPrototype4-3" 'must be a good run defender but also covers tight end---good all around player

                End Select
            Case "ILB"
                Select Case GetNum
                    Case 1 To 20
                        Result = "MikeProtoType" 'Mike LB used in either system--Fiedl General/Do Everything backer
                    Case 21 To 40
                        Result = "TedProtoType3-4" 'Run Stopper LB
                    Case 41 To 60
                        Result = "Cover2ProtoType" 'Coverage, Deep Drops, speed, quickness
                    Case 61 To 80
                        Result = "TacklingMachine" 'Great Tackler
                    Case Else
                        Result = "Balanced" 'comes in during Nickel situations for coverage skills

                End Select

            Case "CB"
                If dt.Rows(idNum).Item("FortyYardTime") < 4.45 Then
                    Result = "CoverCorner"
                Else
                    Select Case GetNum
                        Case 1 To 20
                            Result = "ZoneCorner"
                        Case 21 To 64
                            Result = "Balanced"
                        Case 65 To 75
                            Result = "RunSupport"
                        Case 76 To 92
                            Result = "SlotCorner"
                        Case Else
                            Result = "Physical"
                    End Select
                End If

            Case "SS", "FS"
                Select Case GetNum
                    Case 1 To 20
                        Result = "Zone"
                    Case 21 To 35
                        Result = "PlayMaker"
                    Case 36 To 65
                        Result = "Balanced"
                    Case 66 To 85
                        Result = "RunSupport"
                    Case Else
                        Result = "Hybrid"
                End Select

            Case "K"
                Select Case GetNum
                    Case 1 To 10
                        Result = "Clutch"
                    Case 11 To 30
                        Result = "Accurate"
                    Case 31 To 75
                        Result = "Balanced"
                    Case 76 To 82
                        Result = "KickoffSpecialist"
                    Case Else
                        Result = "BigLeg"
                End Select

            Case "P"
                Select Case GetNum
                    Case 1 To 15
                        Result = "BigLeg"
                    Case 16 To 30
                        Result = "DirectionalPunter"
                    Case 31 To 45
                        Result = "Accurate"
                    Case 46 To 55
                        Result = "AussieRules"
                    Case 56 To 70
                        Result = "GreatHangTime"
                    Case Else
                        Result = "Balanced"
                End Select
        End Select
        Return Result
    End Function
    Public Shared Function GetKickRetAbility(ByVal pos As String, ByVal i As Integer) As Integer
        'gets the ability to return kicks
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 75
                Return 0
            Case Else
                Select Case pos
                    Case "RB", "WR", "CB", "FS"
                        Return MT.GetGaussian(49.5, 16.5)
                    Case Else
                        Return 0
                End Select
        End Select
    End Function
    Public Shared Function GetPuntRetAbility(ByVal pos As String, ByVal i As Integer, ByVal dt As DataTable) As Integer
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 75
                Return 0
            Case Else
                Select Case pos
                    Case "RB", "WR", "CB", "FS"
                        Return dt.Rows(i).Item("RETKickReturn") = MT.GetGaussian(49.5, 16.5)
                    Case Else
                        Return 0
                End Select
        End Select
    End Function
    Public Shared Sub GetSTAbility(ByVal pos As String, ByVal i As Integer, ByVal dt As DataTable)

        Select Case pos
            Case <> "QB", "K", "P", "DT"
                dt.Rows(i).Item("STCoverage") = MT.GetGaussian(49.5, 16.5)
                dt.Rows(i).Item("STWillingness") = MT.GetGaussian(49.5, 16.5)
                dt.Rows(i).Item("STAssignment") = MT.GetGaussian(49.5, 16.5)
                dt.Rows(i).Item("STDiscipline") = MT.GetGaussian(49.5, 16.5)
        End Select
    End Sub
    Public Shared Sub GetLSAbility(ByVal pos As String, ByVal i As Integer, ByVal dt As DataTable)
        Select Case pos
            Case "C", "TE", "DE", "OG"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 90
                        dt.Rows(i).Item("OLLongSnapAbility") = 0
                    Case Else
                        dt.Rows(i).Item("OLLongSnapAbility") = MT.GetGaussian(49.5, 16.5)
                End Select
            Case Else
                dt.Rows(i).Item("OLLongSnapAbility") = 0
        End Select
    End Sub

End Class