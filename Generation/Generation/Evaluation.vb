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

    Private Sub GetPosIdeals(ByVal Scoutnum As Integer, ByVal Playernum As Integer, ByVal Pos As String)
        PlayerGrade = 0
        Select Case Pos
            Case "QB"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 70 : PlayerGrade += -3 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 71 : PlayerGrade += -1.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 72 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 73 To 75 : PlayerGrade += 1.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 2.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 77 : PlayerGrade += 1.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 190 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 190 To 200 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 201 To 210 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 211 To 224 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 225 To 240 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 240 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("WonderlicTest")
                'Case Is < 20 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 20 To 24 : PlayerGrade += -0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 25 To 28 : PlayerGrade += 0.1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 29 To 32 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 33 To 36 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 36 : PlayerGrade += 0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("10YardTime")
                'Case Is < 1.6 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.6 To 1.65 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.66 To 1.71 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.72 To 1.77 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 1.77 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "RB"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 68 : PlayerGrade += -1.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 68 : PlayerGrade += -0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 69 To 73 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 74 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 185 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 185 To 195 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 196 To 205 : PlayerGrade += -0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 206 To 215 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 216 To 230 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 231 To 240 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 240 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("40YardTime")
                'Case Is < 4.41 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.41 To 4.5 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.51 To 4.6 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.61 To 4.7 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.7 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.15 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.15 To 4.22 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.23 To 4.31 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.32 To 4.41 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.41 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BroadJump")
                'Case Is > 127 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 122 To 127 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 117 To 121 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 112 To 116 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 112 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                'Case Is > 26 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 23 To 25 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 20 To 22 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 17 To 19 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 17 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "FB"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 71 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 71 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 72 To 75 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 210 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 210 To 219 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 220 To 235 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 236 To 250 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 251 To 260 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 260 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.2 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.2 To 4.29 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.3 To 4.4 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.41 To 4.49 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.5 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BroadJump")
                'Case Is > 120 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 116 To 120 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 111 To 115 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 106 To 110 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 106 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("10YardTime")
                'Case Is < 1.6 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.6 To 1.64 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.65 To 1.78 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.69 To 1.72 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 1.72 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                'Case Is > 30 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 27 To 29 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 24 To 26 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 21 To 23 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 21 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "WR"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 70 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 70 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 71 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 72 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 73 : PlayerGrade += 0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 73 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 180 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 180 To 190 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 191 To 200 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 201 To 220 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 221 To 235 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 235 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("40YardTime")
                'Case Is < 4.41 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.41 To 4.5 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.51 To 4.6 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.61 To 4.7 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.7 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.08 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.08 To 4.15 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.16 To 4.25 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.26 To 4.35 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.35 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("VertJump")
                'Case Is > 38 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 36 To 38 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 34 To 35 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 30 To 33 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 30 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "TE"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 74 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 75 To 78 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 78 : PlayerGrade += 0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 240 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 240 To 249 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 250 To 259 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 260 To 270 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 271 To 280 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 280 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("40YardTime")
                'Case Is < 4.51 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.51 To 4.64 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.65 To 4.74 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.75 To 4.84 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.84 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.2 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.2 To 4.29 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.3 To 4.39 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.4 To 4.49 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.49 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("VertJump")
                'Case Is > 37 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 35 To 37 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 32 To 34 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 29 To 31 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 29 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                'Case Is > 30 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 27 To 29 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 24 To 26 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 21 To 23 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 21 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "OT"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 75 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += -1.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 78 : PlayerGrade += 0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 79 : PlayerGrade += 1.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 79 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 305 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 305 To 315 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 316 To 329 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 330 To 345 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 346 To 355 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 355 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("10YardTime")
                'Case Is < 1.75 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.75 To 1.78 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.79 To 1.83 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.84 To 1.86 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 1.86 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.58 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.58 To 4.67 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.68 To 4.77 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.78 To 4.87 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.87 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BroadJump")
                'Case Is > 110 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 107 To 110 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 102 To 106 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 96 To 101 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 96 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                'Case Is > 32 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 28 To 31 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 24 To 27 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 20 To 23 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 20 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "OG"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 74 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += -0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 78 : PlayerGrade += 1.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 79 : PlayerGrade += 1.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 79 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 290 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 290 To 299 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 300 To 309 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 310 To 324 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 325 To 335 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 335 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("10YardTime")
                'Case Is < 1.73 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.73 To 1.76 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.77 To 1.83 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.84 To 1.86 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 1.86 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.62 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.62 To 4.71 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.72 To 4.81 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.82 To 4.91 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.91 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BroadJump")
                'Case Is > 108 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 104 To 107 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 100 To 103 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 96 To 99 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 96 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                'Case Is > 32 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 28 To 31 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 24 To 27 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 20 To 23 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 20 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "C"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 73 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 73 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 1.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 77 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 285 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 286 To 295 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 296 To 305 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 306 To 320 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 321 To 330 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 330 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("10YardTime")
                'Case Is < 1.73 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.73 To 1.76 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.77 To 1.83 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.84 To 1.86 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 1.86 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.42 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.43 To 4.52 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.53 To 4.62 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.63 To 4.72 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.72 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BroadJump")
                'Case Is > 109 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 106 To 109 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 102 To 105 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 97 To 101 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 97 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                ' Case Is > 32 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 28 To 31 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 24 To 27 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 20 To 23 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case Is < 20 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "DE"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 72 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 73 : PlayerGrade += -1.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 76 : PlayerGrade += 0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 1.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 78 : PlayerGrade += 1.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 78 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 250 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 250 To 259 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 260 To 269 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 270 To 285 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 286 To 295 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 295 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("40YardTime")
                'Case Is < 4.61 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.61 To 4.7 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.71 To 4.8 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.81 To 4.9 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.9 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("10YardTime")
                'Case Is < 1.58 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.58 To 1.62 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.63 To 1.68 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.69 To 1.73 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 1.73 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.22 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.23 To 4.32 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.33 To 4.45 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.46 To 4.58 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.58 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BroadJump")
                'Case Is > 122 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 119 To 122 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 114 To 118 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 110 To 113 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 110 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                ' Case Is > 32 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 28 To 31 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 24 To 27 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 20 To 23 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case Is < 20 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "DT"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 74 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 75 To 78 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 78 : PlayerGrade += 0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 290 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 290 To 299 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 300 To 310 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 311 To 325 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 326 To 340 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 340 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("10YardTime")
                'Case Is < 1.67 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.67 To 1.7 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.71 To 1.74 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 1.75 To 1.78 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 1.78 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.43 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.43 To 4.52 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.53 To 4.63 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.64 To 4.75 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.75 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BroadJump")
                'Case Is > 112 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 109 To 112 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 106 To 108 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 102 To 105 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 102 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                'Case Is > 34 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 30 To 33 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 26 To 29 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 22 To 25 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 22 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "OLB"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 72 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 72 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 73 To 76 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 77 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 225 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 225 To 234 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 235 To 244 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 245 To 260 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 261 To 270 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 270 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("40YardTime")
                'Case Is < 4.51 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.51 To 4.6 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.61 To 4.7 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.71 To 4.75 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.75 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.15 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.16 To 4.22 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.23 To 4.3 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.31 To 4.42 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.42 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BroadJump")
                ' Case Is > 124 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 120 To 124 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 116 To 119 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 111 To 115 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 111 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                ' Case Is > 30 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 26 To 29 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 22 To 25 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 18 To 21 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 18 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "ILB"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 72 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 72 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 73 To 76 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 77 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 77 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 225 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 225 To 234 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 235 To 244 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 245 To 260 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 261 To 270 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 270 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                ' Select Case DraftDT.Rows(Playernum).Item("40YardTime")
                'Case Is < 4.61 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.61 To 4.7 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.71 To 4.8 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.81 To 4.85 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.85 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                ' Case Is < 4.16 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.17 To 4.24 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.25 To 4.32 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.33 To 4.42 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.42 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BroadJump")
                'Case Is > 119 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 117 To 119 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 113 To 116 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 109 To 112 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 109 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("BenchPress")
                'Case Is > 30 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 26 To 29 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 22 To 25 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 18 To 21 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 18 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "CB"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 69 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 69 : PlayerGrade += -0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 70 : PlayerGrade += -0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 71 To 73 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 74 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 74 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 175 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 175 To 184 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 185 To 194 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 195 To 210 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 211 To 220 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 220 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                '       Select Case DraftDT.Rows(Playernum).Item("Height")
                'Case Is < 72 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 72 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 73 To 76 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 77 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 77 : PlayerGrade += -0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("Weight")
                'Case Is < 225 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 225 To 234 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 235 To 244 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 245 To 260 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 261 To 270 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 270 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("40YardTime")
                ' Case Is < 4.4 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 4.4 To 4.47 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 4.48 To 4.54 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 4.55 To 4.61 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case Is > 4.61 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.08 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.09 To 4.16 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.17 To 4.23 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.24 To 4.33 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.34 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("VertJump")
                ' Case Is > 40 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 38 To 40 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 35 To 37 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 31 To 34 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case Is < 31 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
            Case "FS", "SS"
                Select Case DraftDT.Rows(Playernum).Item("Height")
                    Case Is < 70 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 70 : PlayerGrade += -0.75 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 71 : PlayerGrade += -0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 72 To 74 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 75 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 75 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                Select Case DraftDT.Rows(Playernum).Item("Weight")
                    Case Is < 185 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 185 To 194 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 195 To 204 : PlayerGrade += 0.5 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 205 To 220 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case 221 To 230 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                    Case Is > 230 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                End Select
                'Select Case DraftDT.Rows(Playernum).Item("40YardTime")
                ' Case Is < 4.45 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 4.45 To 4.54 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 4.55 To 4.59 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case 4.6 To 4.65 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                ' Case Is > 4.65 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("ShortShuttle")
                'Case Is < 4.08 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.09 To 4.16 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.17 To 4.23 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 4.24 To 4.33 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is > 4.34 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
                'Select Case DraftDT.Rows(Playernum).Item("VertJump")
                'Case Is > 40 : PlayerGrade += 2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 38 To 40 : PlayerGrade += 1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 35 To 37 : PlayerGrade += 0.25 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case 31 To 34 : PlayerGrade += -1 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'Case Is < 31 : PlayerGrade += -2 * (ScoutDT.Rows(Scoutnum).Item("ValuesCombine") / 50)
                'End Select
        End Select
    End Sub
    Private Sub GetEvalOverall(ByVal Scout As Integer, ByVal Player As Integer, ByVal Pos As String)
        Dim AthleticGrade As Single
        Dim CompetetivenessGrade As Single
        Dim MentalAlertnessGrade As Single
        Dim StrengthExplosionGrade As Single
        Dim PositionalGrade As Single
        Dim ScoutModification As Single
        Dim RealGrade As Single

        Select Case Pos
            Case "QB"
                AthleticGrade = (DraftDT.Rows(Player).Item("QBDropQuickness") + DraftDT.Rows(Player).Item("QBSetUpQuickness") _
                + DraftDT.Rows(Player).Item("Flexibility") + DraftDT.Rows(Player).Item("Athleticism") _
                + DraftDT.Rows(Player).Item("QAB") + DraftDT.Rows(Player).Item("COD")) / 57.22

                CompetetivenessGrade = (DraftDT.Rows(Player).Item("QBPoise") + DraftDT.Rows(Player).Item("Leadership") _
                + DraftDT.Rows(Player).Item("Consistency") + DraftDT.Rows(Player).Item("Production") _
                + DraftDT.Rows(Player).Item("TeamPlayer") + DraftDT.Rows(Player).Item("Clutch")) / 57.22

                MentalAlertnessGrade = ((DraftDT.Rows(Player).Item("WonderlicTest") * 2) + DraftDT.Rows(Player).Item("Instincts") _
                + DraftDT.Rows(Player).Item("Focus")) / 28.61

                StrengthExplosionGrade = (DraftDT.Rows(Player).Item("Durability") + DraftDT.Rows(Player).Item("Explosion") _
                + DraftDT.Rows(Player).Item("DeliversBlow") + DraftDT.Rows(Player).Item("PlayStrength")) / 38.14

                PositionalGrade = ((DraftDT.Rows(Player).Item("QBReleaseQuickness") * 1.5) + DraftDT.Rows(Player).Item("QBShortAcc") _
                                 + DraftDT.Rows(Player).Item("QBMedAcc") + (DraftDT.Rows(Player).Item("QBLongAcc") * 2) _
                                 + (DraftDT.Rows(Player).Item("QBDecMaking") * 2) + (DraftDT.Rows(Player).Item("QBFieldVision") * 2) _
                                 + DraftDT.Rows(Player).Item("QBBallHandling") + DraftDT.Rows(Player).Item("QBTiming") _
                                 + DraftDT.Rows(Player).Item("QBDelivery") + DraftDT.Rows(Player).Item("QBFollowThrough") _
                                 + (DraftDT.Rows(Player).Item("QBAvoidRush") * 1.5) + DraftDT.Rows(Player).Item("QBEscape") _
                                 + (DraftDT.Rows(Player).Item("QBScrambling") * 0.5) + (DraftDT.Rows(Player).Item("QBRolloutRight") * 0.5) _
                                 + (DraftDT.Rows(Player).Item("QBRolloutLeft") * 0.5) + (DraftDT.Rows(Player).Item("QBArmStrength") * 3) _
                                 + DraftDT.Rows(Player).Item("QBZip") + DraftDT.Rows(Player).Item("QBTouchScreenPass") _
                                 + DraftDT.Rows(Player).Item("QBTouchSwingPass") + DraftDT.Rows(Player).Item("QBEffectiveShortOut") _
                                 + (DraftDT.Rows(Player).Item("QBEffectiveDeepOut") * 2) + (DraftDT.Rows(Player).Item("QBEffectiveGoRoute") * 2) _
                                 + DraftDT.Rows(Player).Item("QBEffectivePostRoute") + DraftDT.Rows(Player).Item("QBEffectiveCornerRoute")) / 228.9
            Case "RB"

                AthleticGrade = (DraftDT.Rows(Player).Item("QAB") + DraftDT.Rows(Player).Item("COD") _
                                + DraftDT.Rows(Player).Item("Flexibility") + DraftDT.Rows(Player).Item("Athleticism") _
                                + DraftDT.Rows(Player).Item("RBStart")) / 47.69

                CompetetivenessGrade = (DraftDT.Rows(Player).Item("Clutch") + DraftDT.Rows(Player).Item("Leadership") _
                                      + DraftDT.Rows(Player).Item("Consistency") + DraftDT.Rows(Player).Item("Production") _
                                      + DraftDT.Rows(Player).Item("TeamPlayer") + DraftDT.Rows(Player).Item("RBEffortBlocking")) / 57.22

                MentalAlertnessGrade = ((DraftDT.Rows(Player).Item("WonderlicTest") * 2) + DraftDT.Rows(Player).Item("Instincts") _
                                      + DraftDT.Rows(Player).Item("Focus")) / 28.61

                StrengthExplosionGrade = (DraftDT.Rows(Player).Item("Durability") + DraftDT.Rows(Player).Item("Explosion") _
                                        + DraftDT.Rows(Player).Item("PlayStrength") + DraftDT.Rows(Player).Item("DeliversBlow")) / 38.14

                PositionalGrade = (DraftDT.Rows(Player).Item("RBRunVision") + DraftDT.Rows(Player).Item("RBInsideAbility") _
                                 + DraftDT.Rows(Player).Item("RBOutsideAbility") + DraftDT.Rows(Player).Item("RBElusiveAbility") _
                                 + DraftDT.Rows(Player).Item("RBPowerAbility") + DraftDT.Rows(Player).Item("RBRunBlocking") _
                                 + DraftDT.Rows(Player).Item("RBDurability") + DraftDT.Rows(Player).Item("RBBallSecurity") _
                                 + DraftDT.Rows(Player).Item("RBPassBlocking") + DraftDT.Rows(Player).Item("RBHands") _
                                 + DraftDT.Rows(Player).Item("RBRouteRunning") + DraftDT.Rows(Player).Item("RBRouteRunning")) / 114.45
                ' Select Case ScoutDT.Rows(Scout).Item("JudgingQB")
                'Determines how much the scout misses by...
                'Misses Big-40-60% of player value
                'Case 1 To 20
                'End Select
        End Select
        Dim Num As Integer = MT.GenerateInt32(1, 100)
        If PositionalGrade > 0 And Pos = "QB" Then

            RealGrade = Math.Round((((AthleticGrade / 2) + (MentalAlertnessGrade / 2) _
            + (CompetetivenessGrade * (Num / 100)) + (StrengthExplosionGrade * ((100 - Num) / 100)) _
            + (PositionalGrade * 3)) / 5), 2)

            'ScoutModification = Math.Round((((AthleticGrade * (ScoutDT.Rows(Scout).Item("AthleticismVsMental") / 100)) _
            '+ (MentalAlertnessGrade * ((100 - ScoutDT.Rows(Scout).Item("athleticismVsMental")) / 100)) _
            '+ (CompetetivenessGrade * (Num / 100)) + (StrengthExplosionGrade * ((100 - Num) / 100)) _
            '+ (PositionalGrade * 3)) / 5) + GetScoutModification("QB", Scout), 2)

            Console.WriteLine("RealGrade:" & RealGrade & "  PlayerNum:" & Player & "  Pos:" & Pos)
            'Console.WriteLine("ScoutNum:" & Scout & "  ScoutGrade:" & ScoutModification & "  PlayerNum:" & Player & "  Pos:" & Pos)

        End If

    End Sub
    Private Function GetNewEval(ByVal Scout As Integer, ByVal Player As Integer, ByVal Pos As String) As Single
        Dim ActualGrade As Single = DraftDT.Rows(Player).Item("ActualGrade")
        Dim result As Single
        Select Case Pos
            Case "QB"
                result = Math.Round(MT.GetGaussian(DraftDT.Rows(Player).Item("ActualGrade"), ((100 - ScoutDT.Rows(Scout).Item("JudgingQB")) / 100) * 0.85), 2)
            Case "RB", "FB"
                result = Math.Round(MT.GetGaussian(DraftDT.Rows(Player).Item("ActualGrade"), ((100 - ScoutDT.Rows(Scout).Item("JudgingRB")) / 100) * 0.85), 2)
            Case "WR", "TE"
                result = Math.Round(MT.GetGaussian(DraftDT.Rows(Player).Item("ActualGrade"), ((100 - ScoutDT.Rows(Scout).Item("JudgingRec")) / 100) * 0.85), 2)
            Case "OT", "OG", "C"
                result = Math.Round(MT.GetGaussian(DraftDT.Rows(Player).Item("ActualGrade"), ((100 - ScoutDT.Rows(Scout).Item("JudgingOL")) / 100) * 0.85), 2)
            Case "DE", "DT"
                result = Math.Round(MT.GetGaussian(DraftDT.Rows(Player).Item("ActualGrade"), ((100 - ScoutDT.Rows(Scout).Item("JudgingDL")) / 100) * 0.85), 2)
            Case "OLB", "ILB"
                result = Math.Round(MT.GetGaussian(DraftDT.Rows(Player).Item("ActualGrade"), ((100 - ScoutDT.Rows(Scout).Item("JudgingLB")) / 100) * 0.85), 2)
            Case "CB"
                result = Math.Round(MT.GetGaussian(DraftDT.Rows(Player).Item("ActualGrade"), ((100 - ScoutDT.Rows(Scout).Item("JudgingCB")) / 100) * 0.85), 2)
            Case "SS", "FS"
                result = Math.Round(MT.GetGaussian(DraftDT.Rows(Player).Item("ActualGrade"), ((100 - ScoutDT.Rows(Scout).Item("JudgingSF")) / 100) * 0.85), 2)
        End Select
        Return result
    End Function
    Private Function GetScoutModification(ByVal Pos As String, ByVal Scout As Integer) As Integer
        Dim result As Integer
        Select Case Pos
            Case "QB"
                Select Case MT.GenerateDouble(1, 100)
                    Case Is <= ScoutDT.Rows(Scout).Item("JudgingQB") * 0.7 'Scout Fails
                        Select Case MT.GenerateInt32(1, 100)
                            Case Is < ScoutDT.Rows(Scout).Item("JudgingQB") 'Scout off by more
                                Select Case MT.GenerateDouble(1, 100)
                                    Case 1 To 50 : result = MT.GetGaussian(1.75, 0.0833)
                                    Case 51 To 100 : result = MT.GetGaussian(-1.75, 0.0833)
                                End Select
                            Case Else
                                Select Case MT.GenerateDouble(1, 100)
                                    Case 1 To 50 : result = MT.GetGaussian(1.25, 0.0833)
                                    Case 51 To 100 : result = MT.GetGaussian(-1.25, 0.0833)
                                End Select
                        End Select
                    Case Else 'Scout Succeeds
                        Select Case MT.GenerateInt32(1, 100)
                            Case Is < ScoutDT.Rows(Scout).Item("JudgingQB") 'Scout off by more
                                Select Case MT.GenerateDouble(1, 100)
                                    Case 1 To 50 : result = MT.GetGaussian(0.375, 0.0833)
                                    Case 51 To 100 : result = MT.GetGaussian(-0.375, 0.0833)
                                End Select
                            Case Else
                                Select Case MT.GenerateDouble(1, 100)
                                    Case 1 To 50 : result = MT.GetGaussian(0.125, 0.0833)
                                    Case 51 To 100 : result = MT.GetGaussian(-0.125, 0.0833)
                                End Select
                        End Select
                End Select
        End Select
        Return result
    End Function

End Class