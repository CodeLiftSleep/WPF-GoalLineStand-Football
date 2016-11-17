Public Class Agent
    Inherits Person
    'RelTeamX= his relationship with that team
    Public AgentSQLString As String = "AgentID int PRIMARY KEY NOT NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Height int NULL, Weight int NULL, Age int NULL, DOB varchar(12) NULL, Experience int NULL, AgentType varchar(20) NULL,
ClientList varchar NULL, RelTeam1 int NULL, RelTeam2 int NULL, RelTeam3 int NULL, RelTeam4 int NULL, RelTeam5 int NULL, RelTeam6 int NULL, RelTeam7 int NULL, RelTeam8 int NULL, RelTeam9 int NULL, RelTeam10 int NULL, RealTeam11 int NULL, RelTeam12 int NULL,
RelTeam13 int NULL, RelTeam14 int NULL, RelTeam15 int NULL, RelTeam16 int NULL, RelTeam17 int NULL, RelTeam18 int NULL, RelTeam19 int NULL, RelTeam20 int NULL, RelTeam21 int NULL, RelTeam22 int NULL, RelTeam23 int NULL, RelTeam24 int NULL,
RelTeam25 int NULL, RelTeam26 int NULL, RelTeam27 int NULL, RelTEam28 int NULL, RelTeam29 int NULL, RelTeam30 int NULL, RelTeam31 int NULL, RelTeam32 int NULL, " + PersonSQLString
    Sub New()

    End Sub

    Public Sub GenAgents(ByVal AgentNum As Integer, ByVal XAgent As Agent, ByVal AgentDT As DataTable, ByVal PlayerDT As DataTable)

        XAgent = New Agent 'intializes a new instance of this class

        AgentDT.Rows.Add(AgentNum)

        GenNames(AgentDT, AgentNum, "Agent")
        GetPersonalityStats(AgentDT, AgentNum, XAgent)

        AgentDT.Rows(AgentNum).Item("Experience") = MT.GenerateInt32(0, (AgentDT.Rows(AgentNum).Item("Age") - 24))
        AgentDT.Rows(AgentNum).Item("AgentType") = String.Format("'{0}'", GetAgentType(AgentNum, AgentDT, PlayerDT))
        AgentDT.Rows(AgentNum).Item("ClientList") = String.Format("'{0}'", AssignClientList(AgentNum, AgentDT, PlayerDT, AgentDT.Rows(AgentNum).Item("AgentType")))
        For i As Integer = 0 To AgentDT.Columns.Count - 1
            If AgentDT.Rows(AgentNum).Item(i) Is DBNull.Value Then
                AgentDT.Rows(AgentNum).Item(i) = MT.GetGaussian(49.5, 16.5) 'Fills out the team relationships as this is the only field not filled in
            End If
        Next i

    End Sub
    Public Function GetAgentType(ByVal AgentNum As Integer, ByVal AgentDT As DataTable, ByVal PlayerDT As DataTable) As String
        Dim result As AgentType = 1
        Select Case MT.GenerateInt32(1, 1000)
            Case 1 To 420 'No clients
                result = 6
            Case 421 To 850 '1 to 4 clients 614 clients
                result = 5
            Case 851 To 950 '5 to 9 clients Avg 515 clients
                result = 4
            Case 951 To 983 '10 to 20 clients Avg 353 clients
                result = 3
            Case 984 To 995 '20-40 clients Avg 257 clients
                result = 2
            Case Else '40+ clients Avg 160 clients
                result = 1
        End Select

        Return result.ToString
    End Function

    ''' <summary>
    ''' Loads the player datatable and the agent datatable and assigns agents to players
    ''' </summary>
    ''' <param name="AgentDT"></param>
    ''' <param name="PlayerDT"></param>
    ''' <returns></returns>
    Public Function AssignClientList(ByVal AgentNum As Integer, ByVal AgentDT As DataTable, ByVal PlayerDT As DataTable, ByVal AType As String) As String
        Dim result As String = ""
        Dim NumClients As Integer
        Static PlayersLeft As Integer = PlayerDT.Rows.Count 'Keeps a Static count of how many players are left
        Dim RowNum As Integer

        If AType <> "'PoorAgent'" Then

            Select Case AType

                Case "'AverageAgent'" '1 to 4 clients
                    NumClients = MT.GenerateInt32(1, 4)
                Case "'RespectedAgent'" '5 to 9 clients
                    NumClients = MT.GenerateInt32(5, 9)
                Case "'AboveAvgAgent'" '10 to 20 clients
                    NumClients = MT.GenerateInt32(10, 20)
                Case "'BigAgent'" '21 to 40 clients
                    NumClients = MT.GenerateInt32(21, 40)
                Case "'SuperAgent'" '40+ clients
                    NumClients = MT.GenerateInt32(41, 50)
            End Select

            If NumClients > PlayersLeft Then
                NumClients = PlayersLeft
            End If

            For i As Integer = 1 To NumClients
                RowNum = MT.GenerateInt32(1, PlayerDT.Rows.Count - 1)
                Do While PlayerDT.Rows(RowNum).Item("AgentID") <> 0
                    RowNum = MT.GenerateInt32(1, PlayerDT.Rows.Count - 1)
                Loop
                PlayersLeft -= 1
                result += RowNum.ToString + "," 'comma between PlayerID's
                PlayerDT.Rows(RowNum).Item("AgentID") = AgentNum
            Next i
        Else
            result = ""
        End If

        If result <> "" Then
            result = result.Remove(result.Length - 1, 1) 'removes the last comma
        End If
        Return result

    End Function
End Class