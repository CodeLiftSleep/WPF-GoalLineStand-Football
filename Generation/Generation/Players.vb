Imports GlobalResources

''' <summary>
''' This will be the parent class for both NFLPlayers and CollegePlayers.
''' </summary>
Public MustInherit Class Players
    Inherits Person

    Public Property AthleticFreak As Boolean

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
"FieldAwareness int NULL, PlaybookKnowledge int NULL, BallSecurity int NULL, LovesFootball int NULL, Concentration int NULL, HandlesElements int NULL, Potential int NULL, Raw int NULL, Underclassman int NULL" + PersonSQLString

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

    Public Shared Function Get40Time(ByVal pos As String, ByVal idNum As Integer, ByVal dt As DataTable, DraftRound As String) As Double
        Dim Result As Double
        Select Case pos
            Case "QB"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(4.7464, 0.195168303), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.761666667, 0.216279141), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.715454545, 0.112637794), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.829090909, 0.176207522), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.757619048, 0.119996032), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.8904, 0.200613227), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.813461538, 0.179486875), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.8253125, 0.153138865), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.838809524, 0.199697361), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.845789474, 0.149478943), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.854026124, 0.162585411), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.862262774, 0.175691879), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.886574088, 0.175691879), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.910885401, 0.175691879), 2)
                End Select

            Case "RB"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(4.468333333, 0.088094302), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.4175, 0.110264833), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.4925, 0.07459414), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.456, 0.089053503), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.527021277, 0.085080905), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.504375, 0.097955836), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.539, 0.093549588), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.535714286, 0.087961823), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.559056604, 0.107135144), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.554237288, 0.124571035), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.581113971, 0.12123202), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.607990654, 0.117893004), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.631030607, 0.117893004), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.654070561, 0.117893004), 2)
                End Select

                'Add to the Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.24) * (100 / 79))) * 1.4

            Case "FB"
                Select Case DraftRound
                    'Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    'Case "R1MidFirst",3 : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1LateFirst",4 : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10", "R1MidFirst", "R1LateFirst", "R2", 5 : Result = Math.Round(MT.GetGaussian(4.705, 0.120208153), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.618, 0.047644517), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.7435, 0.154418263), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.7345, 0.096544236), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.7615, 0.145177749), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.748461538, 0.108726899), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.773675214, 0.125723788), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.798888889, 0.142720678), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.822883333, 0.142720678), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.846877778, 0.142720678), 2)
                End Select

            Case "WR"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(4.448181818, 0.059969689), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.435555556, 0.110412181), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.465882353, 0.095855283), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.445, 0.086688032), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.4745, 0.092966579), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.477752809, 0.096091254), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.492386364, 0.094662065), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.496447368, 0.092443914), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.504827586, 0.083134272), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.501727273, 0.102848058), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.538201035, 0.098004265), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.574674797, 0.093160471), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.597548171, 0.093160471), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.620421545, 0.093160471), 2)
                End Select

                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.24) * (100 / 61))) * 1.4
            Case "TE"
                Select Case DraftRound
                    Case "R1Top5", 1, "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.51, 0.115325626), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.635, 0.122338329), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.632666667, 0.131663351), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.732666667, 0.105795291), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.73025, 0.116233353), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.750833333, 0.137329115), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.746222222, 0.121361385), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.759069767, 0.140607762), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.806785714, 0.158333447), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.825619748, 0.145686434), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.844453782, 0.13303942), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.86867605, 0.13303942), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.892898319, 0.13303942), 2)
                End Select

                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.37) * (100 / 74))) * 1.4
            Case "OT"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(5.078571429, 0.205533345), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(5.153636364, 0.097187728), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(5.218, 0.164974746), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(5.222105263, 0.164099528), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(5.216734694, 0.155018652), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(5.182083333, 0.154561264), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(5.243958333, 0.174523541), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(5.252, 0.144816615), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(5.235769231, 0.172084637), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(5.294264706, 0.192767095), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(5.332465686, 0.18736266), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(5.370666667, 0.181958225), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(5.39752, 0.181958225), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(5.424373333, 0.181958225), 2)
                End Select

            Case "OG"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(5.05, 0.2132365323), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(5.28, 0.296984848), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(5.156666667, 0.128166558), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(5.267777778, 0.130936796), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(5.204444444, 0.164908677), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(5.264, 0.179440727), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(5.284565217, 0.161006646), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(5.277254902, 0.170212555), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(5.257567568, 0.160059111), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(5.289574468, 0.167188483), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(5.336395626, 0.189117087), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(5.383216783, 0.211045691), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(5.410132867, 0.211045691), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(5.437048951, 0.211045691), 2)
                End Select

            Case "C"
                Select Case DraftRound
                    'Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(5.05, 0.0525), 2)
                    'Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(5.28, 0.296984848), 2)
                    Case "R1Top5", "R1Top10", "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(5.1425, 0.089953692), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(5.23, 0.180554701), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(5.125555556, 0.132630501), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(5.197058824, 0.147511216), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(5.241904762, 0.156895476), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(5.211, 0.140590027), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(5.211481481, 0.172039881), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(5.2336, 0.18463658), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(5.251585714, 0.184719572), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(5.269571429, 0.184802564), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(5.295919286, 0.184802564), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(5.322267143, 0.184802564), 2)
                End Select

            Case "DE"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(4.683076923, 0.119260542), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.801818182, 0.135264052), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.747857143, 0.111533325), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.73826087, 0.110768069), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.795818182, 0.105702078), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.790576923, 0.106502572), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.802321429, 0.143311372), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.838823529, 0.151018503), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.861428571, 0.146305143), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.849487179, 0.147189227), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.879781758, 0.137861175), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.910076336, 0.128533122), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.934626718, 0.128533122), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.959177099, 0.128533122), 2)
                End Select

            Case "DT"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(5.045, 0.080187281), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(5.03625, 0.142822317), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(5.029583333, 0.203885713), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(5.040434783, 0.135058178), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(5.06, 0.175566311), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(5.063870968, 0.148460458), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(5.109215686, 0.148254418), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(5.134615385, 0.14730645), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(5.094310345, 0.140101618), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(5.105797101, 0.151525632), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(5.129058551, 0.159600062), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(5.15232, 0.167674493), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(5.1780816, 0.167674493), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(5.2038432, 0.167674493), 2)
                End Select

            Case "OLB"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(4.551666667, 0.080353386), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.608571429, 0.114953407), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.632727273, 0.13209599), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.591666667, 0.090033664), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.638928571, 0.10482329), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.645576923, 0.097425959), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.66637931, 0.080081061), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.698196721, 0.117381262), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.68122807, 0.132153458), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.690655738, 0.141667085), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.719788653, 0.134616539), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.748921569, 0.127565994), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.772666176, 0.127565994), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.796410784, 0.127565994), 2)
                End Select

                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.4) * (100 / 74))) * 1.4

            Case "ILB" : Result = Math.Round(MT.GetGaussian(4.7626, 0.1308), 2)

                Select Case DraftRound
                    Case "R1Top5", 1, "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.6075, 0.060759087), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.583333333, 0.064291005), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.65375, 0.082624365), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.709354839, 0.112069487), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.711111111, 0.094710922), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.738823529, 0.101317524), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.695588235, 0.111795028), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.75625, 0.11514877), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.792121212, 0.124216484), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.811113798, 0.123039609), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.830106383, 0.121862735), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.854256915, 0.121862735), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.878407447, 0.121862735), 2)
                End Select

            Case "CB"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(4.395, 0.075498344), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.413076923, 0.078780773), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.424615385, 0.075854111), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.432777778, 0.059694992), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.4584, 0.076229384), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.471612903, 0.079060933), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.479240506, 0.082799916), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.499876543, 0.085095444), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.5068, 0.084775698), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.488271605, 0.084495416), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.526819626, 0.090914786), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.565367647, 0.097334156), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.588194485, 0.097334156), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.611021324, 0.097334156), 2)
                End Select

                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.25) * (100 / 70))) * 1.4
            Case "SS"
                Select Case DraftRound
                    Case "R1Top5", 1, "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.474, 0.097877474), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.521666667, 0.097450842), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.491428571, 0.045981363), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.507307692, 0.079627015), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.563684211, 0.068085773), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.538076923, 0.06864513), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.577857143, 0.084737378), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.556086957, 0.064012721), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.574117647, 0.091919518), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.600483481, 0.096208285), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.626849315, 0.100497052), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.649983562, 0.100497052), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.673117808, 0.100497052), 2)

                End Select
                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.31) * (100 / 65))) * 1.4
            Case "FS"
                Select Case DraftRound
                    Case "R1Top5", 1 : Result = Math.Round(MT.GetGaussian(4.455, 0.077781746), 2)
                    Case "R1Top10", 2 : Result = Math.Round(MT.GetGaussian(4.4, 0.05), 2)
                    Case "R1MidFirst", 3 : Result = Math.Round(MT.GetGaussian(4.514, 0.085615419), 2)
                    Case "R1LateFirst", 4 : Result = Math.Round(MT.GetGaussian(4.506666667, 0.053541261), 2)
                    Case "R2", 5 : Result = Math.Round(MT.GetGaussian(4.534333333, 0.091111275), 2)
                    Case "R3", 6 : Result = Math.Round(MT.GetGaussian(4.515769231, 0.089807486), 2)
                    Case "R4", 7 : Result = Math.Round(MT.GetGaussian(4.535526316, 0.071949583), 2)
                    Case "R5", 8 : Result = Math.Round(MT.GetGaussian(4.5459375, 0.084733334), 2)
                    Case "R6", 9 : Result = Math.Round(MT.GetGaussian(4.560714286, 0.084662684), 2)
                    Case "R7", 10 : Result = Math.Round(MT.GetGaussian(4.581153846, 0.084300744), 2)
                    Case "PUFA", 11 : Result = Math.Round(MT.GetGaussian(4.602222493, 0.096680542), 2)
                    Case "LUFA", 12 : Result = Math.Round(MT.GetGaussian(4.623291139, 0.10906034), 2)
                    Case "PracSquad", 13 : Result = Math.Round(MT.GetGaussian(4.646407595, 0.10906034), 2)
                    Case "Reject", 14 : Result = Math.Round(MT.GetGaussian(4.669524051, 0.10906034), 2)
                End Select

                'Add to Actual Grade
                ActualGrade.Combine += (100 - (100 * (Result - 4.31) * (100 / 65))) * 1.4
            Case "K" : Result = Math.Round(MT.GetGaussian(4.94, 0.08), 2)
            Case "P" : Result = Math.Round(MT.GetGaussian(4.765, 0.08), 2)
            Case "LS" : Result = Math.Round(MT.GetGaussian(5.02, 0.159373775), 2)
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
            Case 0 To 5.64287 : Result = "QB"
            Case 5.64387 To 14.17452 : Result = "RB"
            Case 14.17552 To 16.25505 : Result = "FB"
            Case 16.25605 To 29.05254 : Result = "WR"
            Case 29.05354 To 34.93489 : Result = "TE"
            Case 34.93589 To 37.92845 : Result = "OC"
            Case 37.92945 To 44.08023 : Result = "OG"
            Case 44.08123 To 51.74375 : Result = "OT"
            Case 51.74475 To 52.44724 : Result = "K"
            Case 52.44824 To 53.31537 : Result = "P"
            Case 53.31637 To 61.65245 : Result = "DE"
            Case 61.65345 To 69.48062 : Result = "DT"
            Case 69.48162 To 73.95599 : Result = "ILB"
            Case 73.95699 To 81.395 : Result = "OLB"
            Case 81.396 To 91.88744 : Result = "CB"
            Case 91.88844 To 96.21314 : Result = "FS"
            Case 96.21414 To 99.9 : Result = "SS"
            Case Else : Result = "LS"
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
            Case "R1Top10", 2
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
                Rating = CInt(ratingsStartPoint * ((1 + RatingsDecay) ^ TimeValue) + MT.GenerateInt32(-5, 5))
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
                If dt.Rows(idNum).Item("FortyYardTime") < 4.65 Then 'There is a 50% chance this will be a "Mobile QB"
                    Select Case GetNum
                        Case < 50 : Result = "Mobile"
                        Case 51 To 60 : Result = "StrongArm"
                        Case 61 To 70 : Result = "WestCoast"
                        Case 71 To 80 : Result = "Balanced"
                        Case 81 To 90 : Result = "Field General"
                        Case Else : Result = "PocketPasser"
                    End Select
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
                    Select Case GetNum
                        Case < 50 : Result = "SpeedBack"
                        Case 51 To 60 : Result = "Balanced"
                        Case 61 To 70 : Result = "PowerBack"
                        Case 71 To 80 : Result = "Receiving"
                        Case 81 To 90 : Result = "One Cut"
                        Case 91 To 100 : Result = "Scat Back"
                    End Select

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
                If dt.Rows(idNum).Item("FortyYardTime") < 4.47 Then 'Cannot be a possession receiver
                    If dt.Rows(idNum).Item("Height") < 71 And dt.Rows(idNum).Item("Weight") < 186 Then 'Slot receiver type
                        Select Case GetNum
                            Case < 50 : Result = "Slot"
                            Case 51 To 80 : Result = "Speed"
                            Case 81 To 90 : Result = "Balanced"
                            Case 91 To 100 : Result = "Polished"
                        End Select
                    Else
                        Select Case GetNum 'Still can be a slot receiver but just a lower chance
                            Case < 50 : Result = "Speed"
                            Case 51 To 70 : Result = "Balanced"
                            Case 71 To 85 : Result = "Polished"
                            Case Else : Result = "Slot"
                        End Select
                    End If
                ElseIf dt.Rows(idNum).Item("Height") > 74 And dt.Rows(idNum).Item("Weight") > 199 Then 'Higher chance to be an RZ Threat as a big bodied receiver
                    Select Case GetNum
                        Case 1 To 50 : Result = "RZThreat"
                        Case 51 To 65 : Result = "Balanced"
                        Case 66 To 80 : Result = "Possession"
                        Case 81 To 94 : Result = "Polished"
                        Case Else : Result = "Slot" 'lower chance to be a slot receiver
                    End Select
                Else 'Can't be a RZThreat
                    Select Case GetNum
                        Case 1 To 30 : Result = "Balanced"
                        Case 31 To 60 : Result = "Possession"
                        Case 61 To 90 : Result = "Polished"
                        Case Else : Result = "Slot" 'lower chance to be a slot receiver
                    End Select
                End If

            Case "TE"
                If dt.Rows(idNum).Item("FortyYardTime") < 4.61 Then
                    Select Case GetNum
                        Case < 50 : Result = "VerticalThreat"
                        Case 51 To 65 : Result = "Balanced"
                        Case 66 To 80 : Result = "Hybrid"
                        Case Else : Result = "Receiving"
                    End Select
                ElseIf dt.Rows(idNum).Item("FortyYardTime") > 4.74 And dt.Rows(idNum).Item("Weight") > 259 Then
                    Select Case GetNum
                        Case < 75 : Result = "Blocking"
                        Case Else : Result = "Hybrid" 'HBack Type TE
                    End Select
                Else
                    Select Case GetNum
                        Case 1 To 40 : Result = "Balanced"
                        Case 41 To 65 : Result = "Blocking"
                        Case 66 To 74 : Result = "Hybrid"
                        Case Else : Result = "Receiving"
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