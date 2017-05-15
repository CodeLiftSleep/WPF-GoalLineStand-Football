Public Class Evaluation
    Inherits Scouts
    Private PlayerGrade As Double

    Public Sub ScoutPlayerEval()
        'This Sub controls how players are evaluated by scouts
        Dim Scout As Integer
        Dim Player As Integer

        If DraftDT.Rows.Count = 0 Then
            SQLiteTables.LoadTable(MyDB, DraftDT, "DraftPlayers")
        End If

        If ScoutDT.Rows.Count = 0 Then
            SQLiteTables.LoadTable(MyDB, ScoutDT, "Scouts")
        End If

        Dim ScoutRows As DataRowCollection = ScoutDT.Rows
        Dim PlayerRows As DataRowCollection = DraftDT.Rows

        For Player = 1 To PlayerRows.Count - 1
            For Scout = 1 To 653
                ScoutGradeDT.Rows(Player).Item("Scout0") = DraftDT.Rows(Player).Item("ActualGrade")
                ScoutGradeDT.Rows(Player).Item("Scout" & Scout & "") = GetNewEval(Scout, Player, DraftDT.Rows(Player).Item("CollegePOS"))
                'Console.WriteLine(ScoutGradeDT.Rows(Player).Item("Scout" & Scout & ""))
            Next Scout
        Next Player

        SQLiteTables.BulkInsert(MyDB, ScoutGradeDT, "ScoutsGrade")
    End Sub

    'Private Sub GradePlayer(ByVal ScoutNum As Integer, ByVal PlayerNum As Integer)
    'GetPosIdeals(ScoutNum, PlayerNum, DraftDT.Rows(PlayerNum).Item("CollegePOS"))
    'GetNewEval(ScoutNum, PlayerNum, DraftDT.Rows(PlayerNum).Item("CollegePOS"))
    'End Sub
    ''' <summary>
    ''' Players who don't meet certain physical "ideals" for their position typically see their grades lowered by a round or more, regardless of talent, the further away, the more its lowered
    ''' </summary>
    ''' <param name="scoutNum"></param>
    ''' <param name="playerNum"></param>
    ''' <param name="Pos"></param>
    Private Sub GetPosIdeals(ByVal scoutNum As Integer, ByVal playerNum As Integer, ByVal Pos As String)
        PlayerGrade = 0
        Select Case Pos
            Case "QB"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 70 : PlayerGrade += -3 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 71 : PlayerGrade += -1.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 72 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 73 To 75 : PlayerGrade += 1.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 2.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 77 : PlayerGrade += 1.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 190 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 190 To 200 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 201 To 210 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 211 To 224 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 225 To 240 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 240 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "RB"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 68 : PlayerGrade += -1.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 68 : PlayerGrade += -0.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 69 To 73 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 74 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 185 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 185 To 195 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 196 To 205 : PlayerGrade += -0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 206 To 215 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 216 To 230 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 231 To 240 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 240 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "FB"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 71 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 71 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 72 To 75 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 210 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 210 To 219 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 220 To 235 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 236 To 250 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 251 To 260 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 260 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "WR"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 70 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 70 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 71 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 72 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 73 : PlayerGrade += 0.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 73 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 180 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 180 To 190 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 191 To 200 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 201 To 220 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 221 To 235 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 235 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "TE"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 74 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 75 To 78 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 78 : PlayerGrade += 0.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 240 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 240 To 249 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 250 To 259 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 260 To 270 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 271 To 280 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 280 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "OT"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 75 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += -1.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 78 : PlayerGrade += 0.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 79 : PlayerGrade += 1.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 79 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 305 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 305 To 315 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 316 To 329 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 330 To 345 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 346 To 355 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 355 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "OG"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 74 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += -0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 0.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 78 : PlayerGrade += 1.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 79 : PlayerGrade += 1.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 79 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 290 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 290 To 299 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 300 To 309 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 310 To 324 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 325 To 335 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 335 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "C"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 73 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 73 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 1.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 77 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 285 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 286 To 295 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 296 To 305 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 306 To 320 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 321 To 330 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 330 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "DE"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 72 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 73 : PlayerGrade += -1.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 0.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 1.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 78 : PlayerGrade += 1.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 78 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 250 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 250 To 259 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 260 To 269 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 270 To 285 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 286 To 295 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 295 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "DT"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 74 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 75 To 78 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 78 : PlayerGrade += 0.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 290 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 290 To 299 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 300 To 310 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 311 To 325 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 326 To 340 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 340 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select

            Case "OLB"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 72 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 72 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 73 To 76 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 77 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 225 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 225 To 234 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 235 To 244 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 245 To 260 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 261 To 270 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 270 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "ILB"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 72 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 72 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 73 To 76 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 77 : PlayerGrade += -0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 225 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 225 To 234 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 235 To 244 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 245 To 260 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 261 To 270 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 270 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "CB"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 69 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 69 : PlayerGrade += -0.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 70 : PlayerGrade += -0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 71 To 73 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 74 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 175 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 175 To 184 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 185 To 194 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 195 To 210 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 211 To 220 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 220 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
            Case "FS", "SS"
                Select Case DraftDT.Rows(playerNum).Item("Height")
                    Case Is < 70 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 70 : PlayerGrade += -0.75 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 71 : PlayerGrade += -0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 72 To 74 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 75 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(playerNum).Item("Weight")
                    Case Is < 185 : PlayerGrade += -2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 185 To 194 : PlayerGrade += -1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 195 To 204 : PlayerGrade += 0.5 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 205 To 220 : PlayerGrade += 2 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case 221 To 230 : PlayerGrade += 1 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                    Case Is > 230 : PlayerGrade += 0.25 * (ScoutDT.Rows(scoutNum).Item("ValuesCombine") / 50)
                End Select
        End Select
    End Sub
    Private Sub GetEvalOverall(ByVal scout As Integer, ByVal player As Integer, ByVal pos As String)
        Dim AthleticGrade As Single
        Dim CompetetivenessGrade As Single
        Dim MentalAlertnessGrade As Single
        Dim StrengthExplosionGrade As Single
        Dim PositionalGrade As Single
        Dim ScoutModification As Single
        Dim RealGrade As Single

        Select Case pos
            Case "QB"
                AthleticGrade = (DraftDT.Rows(player).Item("QBDropQuickness") + DraftDT.Rows(player).Item("QBSetUpQuickness") _
                + DraftDT.Rows(player).Item("Flexibility") + DraftDT.Rows(player).Item("Athleticism") _
                + DraftDT.Rows(player).Item("QAB") + DraftDT.Rows(player).Item("COD")) / 57.22

                CompetetivenessGrade = (DraftDT.Rows(player).Item("QBPoise") + DraftDT.Rows(player).Item("Leadership") _
                + DraftDT.Rows(player).Item("Consistency") + DraftDT.Rows(player).Item("Production") _
                + DraftDT.Rows(player).Item("TeamPlayer") + DraftDT.Rows(player).Item("Clutch")) / 57.22

                MentalAlertnessGrade = ((DraftDT.Rows(player).Item("WonderlicTest") * 2) + DraftDT.Rows(player).Item("Instincts") _
                + DraftDT.Rows(player).Item("Focus")) / 28.61

                StrengthExplosionGrade = (DraftDT.Rows(player).Item("Durability") + DraftDT.Rows(player).Item("Explosion") _
                + DraftDT.Rows(player).Item("DeliversBlow") + DraftDT.Rows(player).Item("PlayStrength")) / 38.14

                PositionalGrade = ((DraftDT.Rows(player).Item("QBReleaseQuickness") * 1.5) + DraftDT.Rows(player).Item("QBShortAcc") _
                                 + DraftDT.Rows(player).Item("QBMedAcc") + (DraftDT.Rows(player).Item("QBLongAcc") * 2) _
                                 + (DraftDT.Rows(player).Item("QBDecMaking") * 2) + (DraftDT.Rows(player).Item("QBFieldVision") * 2) _
                                 + DraftDT.Rows(player).Item("QBBallHandling") + DraftDT.Rows(player).Item("QBTiming") _
                                 + DraftDT.Rows(player).Item("QBDelivery") + DraftDT.Rows(player).Item("QBFollowThrough") _
                                 + (DraftDT.Rows(player).Item("QBAvoidRush") * 1.5) + DraftDT.Rows(player).Item("QBEscape") _
                                 + (DraftDT.Rows(player).Item("QBScrambling") * 0.5) + (DraftDT.Rows(player).Item("QBRolloutRight") * 0.5) _
                                 + (DraftDT.Rows(player).Item("QBRolloutLeft") * 0.5) + (DraftDT.Rows(player).Item("QBArmStrength") * 3) _
                                 + DraftDT.Rows(player).Item("QBZip") + DraftDT.Rows(player).Item("QBTouchScreenPass") _
                                 + DraftDT.Rows(player).Item("QBTouchSwingPass") + DraftDT.Rows(player).Item("QBEffectiveShortOut") _
                                 + (DraftDT.Rows(player).Item("QBEffectiveDeepOut") * 2) + (DraftDT.Rows(player).Item("QBEffectiveGoRoute") * 2) _
                                 + DraftDT.Rows(player).Item("QBEffectivePostRoute") + DraftDT.Rows(player).Item("QBEffectiveCornerRoute")) / 228.9
            Case "RB"

                AthleticGrade = (DraftDT.Rows(player).Item("QAB") + DraftDT.Rows(player).Item("COD") _
                                + DraftDT.Rows(player).Item("Flexibility") + DraftDT.Rows(player).Item("Athleticism") _
                                + DraftDT.Rows(player).Item("RBStart")) / 47.69

                CompetetivenessGrade = (DraftDT.Rows(player).Item("Clutch") + DraftDT.Rows(player).Item("Leadership") _
                                      + DraftDT.Rows(player).Item("Consistency") + DraftDT.Rows(player).Item("Production") _
                                      + DraftDT.Rows(player).Item("TeamPlayer") + DraftDT.Rows(player).Item("RBEffortBlocking")) / 57.22

                MentalAlertnessGrade = ((DraftDT.Rows(player).Item("WonderlicTest") * 2) + DraftDT.Rows(player).Item("Instincts") _
                                      + DraftDT.Rows(player).Item("Focus")) / 28.61

                StrengthExplosionGrade = (DraftDT.Rows(player).Item("Durability") + DraftDT.Rows(player).Item("Explosion") _
                                        + DraftDT.Rows(player).Item("PlayStrength") + DraftDT.Rows(player).Item("DeliversBlow")) / 38.14

                PositionalGrade = (DraftDT.Rows(player).Item("RBRunVision") + DraftDT.Rows(player).Item("RBInsideAbility") _
                                 + DraftDT.Rows(player).Item("RBOutsideAbility") + DraftDT.Rows(player).Item("RBElusiveAbility") _
                                 + DraftDT.Rows(player).Item("RBPowerAbility") + DraftDT.Rows(player).Item("RBRunBlocking") _
                                 + DraftDT.Rows(player).Item("RBDurability") + DraftDT.Rows(player).Item("RBBallSecurity") _
                                 + DraftDT.Rows(player).Item("RBPassBlocking") + DraftDT.Rows(player).Item("RBHands") _
                                 + DraftDT.Rows(player).Item("RBRouteRunning") + DraftDT.Rows(player).Item("RBRouteRunning")) / 114.45
                ' Select Case ScoutDT.Rows(Scout).Item("JudgingQB")
                'Determines how much the scout misses by...
                'Misses Big-40-60% of player value
                'Case 1 To 20
                'End Select
        End Select
        Dim Num As Integer = MT.GenerateInt32(1, 100)
        If PositionalGrade > 0 And pos = "QB" Then

            RealGrade = Math.Round((((AthleticGrade / 2) + (MentalAlertnessGrade / 2) _
            + (CompetetivenessGrade * (Num / 100)) + (StrengthExplosionGrade * ((100 - Num) / 100)) _
            + (PositionalGrade * 3)) / 5), 2)

            'ScoutModification = Math.Round((((AthleticGrade * (ScoutDT.Rows(Scout).Item("AthleticismVsMental") / 100)) _
            '+ (MentalAlertnessGrade * ((100 - ScoutDT.Rows(Scout).Item("athleticismVsMental")) / 100)) _
            '+ (CompetetivenessGrade * (Num / 100)) + (StrengthExplosionGrade * ((100 - Num) / 100)) _
            '+ (PositionalGrade * 3)) / 5) + GetScoutModification("QB", Scout), 2)

            Console.WriteLine("RealGrade:" & RealGrade & "  PlayerNum:" & player & "  Pos:" & pos)
            'Console.WriteLine("ScoutNum:" & Scout & "  ScoutGrade:" & ScoutModification & "  PlayerNum:" & Player & "  Pos:" & Pos)

        End If

    End Sub
    Private Function GetNewEval(ByVal scout As Integer, ByVal player As Integer, ByVal pos As String) As Single
        Dim ActualGrade As Single = DraftDT.Rows(player).Item("ActualGrade")
        Dim Result As Single
        Select Case pos
            Case "QB"
                Result = Math.Round(MT.GetGaussian(DraftDT.Rows(player).Item("ActualGrade"), ((100 - ScoutDT.Rows(scout).Item("JudgingQB")) / 100) * 0.85), 2)
            Case "RB", "FB"
                Result = Math.Round(MT.GetGaussian(DraftDT.Rows(player).Item("ActualGrade"), ((100 - ScoutDT.Rows(scout).Item("JudgingRB")) / 100) * 0.85), 2)
            Case "WR", "TE"
                Result = Math.Round(MT.GetGaussian(DraftDT.Rows(player).Item("ActualGrade"), ((100 - ScoutDT.Rows(scout).Item("JudgingRec")) / 100) * 0.85), 2)
            Case "OT", "OG", "C"
                Result = Math.Round(MT.GetGaussian(DraftDT.Rows(player).Item("ActualGrade"), ((100 - ScoutDT.Rows(scout).Item("JudgingOL")) / 100) * 0.85), 2)
            Case "DE", "DT"
                Result = Math.Round(MT.GetGaussian(DraftDT.Rows(player).Item("ActualGrade"), ((100 - ScoutDT.Rows(scout).Item("JudgingDL")) / 100) * 0.85), 2)
            Case "OLB", "ILB"
                Result = Math.Round(MT.GetGaussian(DraftDT.Rows(player).Item("ActualGrade"), ((100 - ScoutDT.Rows(scout).Item("JudgingLB")) / 100) * 0.85), 2)
            Case "CB"
                Result = Math.Round(MT.GetGaussian(DraftDT.Rows(player).Item("ActualGrade"), ((100 - ScoutDT.Rows(scout).Item("JudgingCB")) / 100) * 0.85), 2)
            Case "SS", "FS"
                Result = Math.Round(MT.GetGaussian(DraftDT.Rows(player).Item("ActualGrade"), ((100 - ScoutDT.Rows(scout).Item("JudgingSF")) / 100) * 0.85), 2)
        End Select
        Return Result
    End Function
    Private Function GetScoutModification(ByVal pos As String, ByVal scout As Integer) As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB"
                Select Case MT.GenerateDouble(1, 100)
                    Case Is <= ScoutDT.Rows(scout).Item("JudgingQB") * 0.7 'Scout Fails
                        Select Case MT.GenerateInt32(1, 100)
                            Case Is < ScoutDT.Rows(scout).Item("JudgingQB") 'Scout off by more
                                Select Case MT.GenerateDouble(1, 100)
                                    Case 1 To 50 : Result = MT.GetGaussian(1.75, 0.0833)
                                    Case 51 To 100 : Result = MT.GetGaussian(-1.75, 0.0833)
                                End Select
                            Case Else
                                Select Case MT.GenerateDouble(1, 100)
                                    Case 1 To 50 : Result = MT.GetGaussian(1.25, 0.0833)
                                    Case 51 To 100 : Result = MT.GetGaussian(-1.25, 0.0833)
                                End Select
                        End Select
                    Case Else 'Scout Succeeds
                        Select Case MT.GenerateInt32(1, 100)
                            Case Is < ScoutDT.Rows(scout).Item("JudgingQB") 'Scout off by more
                                Select Case MT.GenerateDouble(1, 100)
                                    Case 1 To 50 : Result = MT.GetGaussian(0.375, 0.0833)
                                    Case 51 To 100 : Result = MT.GetGaussian(-0.375, 0.0833)
                                End Select
                            Case Else
                                Select Case MT.GenerateDouble(1, 100)
                                    Case 1 To 50 : Result = MT.GetGaussian(0.125, 0.0833)
                                    Case 51 To 100 : Result = MT.GetGaussian(-0.125, 0.0833)
                                End Select
                        End Select
                End Select
        End Select
        Return Result
    End Function

End Class