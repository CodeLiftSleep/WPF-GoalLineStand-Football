Imports Microsoft.SqlServer
Imports System.data
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class GenSQL
    Private i As Integer
    Private Quarter As Integer
    Private MinLeftMax As Integer
    Private MinLeftMin As Integer
    Private SecLeftMax As Integer
    Private SecLeftMin As Integer
    Private Down As Integer
    Private DistMax As Integer
    Private DistMin As Integer
    Private PDMax As Integer
    Private PDMin As Integer
    Private YLMax As Integer
    Private YLMin As Integer

    Public Sub GetSQL(ByVal DT As DataTable, ByVal TableName As String, ByVal StoredProc As String, ByVal Parameter As String, ByVal ColName As String)
        Dim GetValue As New ArrayList
        GetTables.LoadTable(DT, TableName)

        For i = 0 To 31
            Dim Read As SqlClient.SqlDataReader
            Dim Conn As SqlClient.SqlConnection = New SqlClient.SqlConnection
            Conn.ConnectionString = "Data Source=M-664C3012670A4\SQLEXPRESS;Initial Catalog=FootballData;Integrated Security=True"
            Conn.Open()

            Dim CMD As New SqlClient.SqlCommand(StoredProc, Conn)
            CMD.CommandType = CommandType.StoredProcedure
            CMD.Parameters.Add(Parameter, SqlDbType.VarChar, 10)

            DT.Rows.Add()
            CMD.Parameters(Parameter).Value = GetTeam(i)
            Read = CMD.ExecuteReader

            Do While Read.Read
                GetValue.Add(Read.GetValue(0))
            Loop
            DT.Rows(i).Item(ColName) = GetValue(i)
            Console.WriteLine(DT.Rows(i).Item(ColName))

        Next i

    End Sub
    Public Sub GetSQL(ByVal DT As DataTable, ByVal TableName As String, ByVal StoredProc As String, ByVal Quart As String, _
    ByVal DistMx As String, ByVal DistMn As String, ByVal PDMx As String, ByVal PDMn As String, ByVal YLMx As String, ByVal YLMn As String, _
    ByVal Dwn As String, ByVal MinLeftMx As String, ByVal MinLeftMn As String, ByVal SecLeftMx As String, ByVal SecLeftMn As String, ByVal ColName As String)

        Dim GetValue As New ArrayList
        GetTables.LoadTable(DT, TableName)
        GetColName()

        For i = 0 To 31
            Dim Read As SqlClient.SqlDataReader
            Dim Conn As SqlClient.SqlConnection = New SqlClient.SqlConnection
            Conn.ConnectionString = "Data Source=M-664C3012670A4\SQLEXPRESS;Initial Catalog=FootballData;Integrated Security=True;Min Pool Size=5;Max Pool Size=60;Connect Timeout=60"

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim CMD As New SqlClient.SqlCommand(StoredProc, Conn)
            CMD.CommandType = CommandType.StoredProcedure
            'CMD.Parameters.Add(Team, SqlDbType.VarChar, 10)
            CMD.Parameters.Add(Quart, SqlDbType.Int)
            CMD.Parameters.Add(DistMx, SqlDbType.Int)
            CMD.Parameters.Add(DistMn, SqlDbType.Int)
            CMD.Parameters.Add(PDMx, SqlDbType.Int)
            CMD.Parameters.Add(PDMn, SqlDbType.Int)
            CMD.Parameters.Add(YLMx, SqlDbType.Int)
            CMD.Parameters.Add(YLMn, SqlDbType.Int)
            CMD.Parameters.Add(Dwn, SqlDbType.Int)
            CMD.Parameters.Add(MinLeftMx, SqlDbType.Int)
            CMD.Parameters.Add(MinLeftMn, SqlDbType.Int)
            CMD.Parameters.Add(SecLeftMx, SqlDbType.Int)
            CMD.Parameters.Add(SecLeftMn, SqlDbType.Int)

            DT.Rows.Add()
            'CMD.Parameters(Team).Value = GetTeam(i)
            CMD.Parameters(Quart).Value = CInt(Quarter)
            CMD.Parameters(DistMx).Value = CInt(DistMax)
            CMD.Parameters(DistMn).Value = CInt(DistMin)
            CMD.Parameters(PDMx).Value = CInt(PDMax)
            CMD.Parameters(PDMn).Value = CInt(PDMin)
            CMD.Parameters(YLMx).Value = CInt(YLMax)
            CMD.Parameters(YLMn).Value = CInt(YLMin)
            CMD.Parameters(Dwn).Value = CInt(Down)
            CMD.Parameters(MinLeftMx).Value = CInt(MinLeftMax)
            CMD.Parameters(MinLeftMn).Value = CInt(MinLeftMin)
            CMD.Parameters(SecLeftMx).Value = CInt(SecLeftMax)
            CMD.Parameters(SecLeftMn).Value = CInt(SecLeftMin)

            Read = CMD.ExecuteReader

            Do While Read.Read
                GetValue.Add(Read.GetValue(0))
            Loop

            DT.Rows(i).Item(ColName) = GetValue(i)
            Console.WriteLine(DT.Rows(i).Item(ColName))
            Conn.Close()
        Next i


    End Sub

    Public Sub StoredProcs()
        GetSQL(PlayStats, "Playstats", "dbo.SP_GetPassData", "@GetTeam", "PassPlays")
        GetSQL(PlayStats, "Playstats", "dbo.SP_GetRunData", "@GetTeam", "RunPlays")
        GetSQL(PlayStats, "PlayStats", "dbo.SP_GetTotalPlays", "@GetTeam", "TotalPlays")
        GetPassPct(PlayStats, "Playstats")
        GetRunPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_GetTotTD", "@GetTeam", "TotalTD")
        GetTotTDPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_GetPassTD", "@GetTeam", "PassTD")
        GetPassTDPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_GetRunTD", "@GetTeam", "RunTD")
        GetRunTDPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_TotalINT", "@GetTeam", "TotalInt")
        GetSQL(PlayStats, "Playstats", "dbo.SP_INTTD", "@GetTeam", "IntTD")
        GetIntTDPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_GetTotalFum", "@GetTeam", "TotalFum")
        GetSQL(PlayStats, "Playstats", "dbo.SP_GetFumRec", "@GetTeam", "FumRec")
        GetSQL(PlayStats, "Playstats", "dbo.SP_FumRecTD", "@GetTeam", "TDFumRec")
        GetFumTDPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_GetTotKR", "@GetTeam", "TotKR")
        GetSQL(PlayStats, "Playstats", "dbo.SP_KRTD", "@GetTeam", "KRTD")
        GetKRTDPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_TotPR", "@GetTeam", "TotPR")
        GetSQL(PlayStats, "Playstats", "dbo.SP_PRTD", "@GetTeam", "TotPRTD")
        GetPRTDPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_PRFC", "@GetTeam", "TotPRFC")
        GetSQL(PlayStats, "Playstats", "dbo.SP_TotPunts", "@GetTeam", "TotPunts")
        GetPRFCPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.PuntDowned", "@GetTeam", "PuntDowned")
        GetPuntDownPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_PuntTB", "@GetTeam", "PuntTB")
        GetPuntTBPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_PuntMuffs", "@GetTeam", "PuntMuffs")
        GetPuntMuffPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_KO", "@GetTeam", "TotKO")
        GetSQL(PlayStats, "Playstats", "dbo.SP_KOTB", "@GetTeam", "KOTB")
        GetSQL(PlayStats, "Playstats", "dbo.SP_KOOOB", "@GetTeam", "KOOOB")
        GetKOOOBPct(PlayStats, "Playstats")
        GetSQL(PlayStats, "Playstats", "dbo.SP_FGAtt", "@GetTeam", "FGATT")
        GetSQL(PlayStats, "Playstats", "dbo.SP_FGMade", "@GetTeam", "FGMADE")
        GetFGPct(PlayStats, "Playstats")
        GetPlays()



    End Sub

    Private Function GetTeam(ByVal TeamNum As Integer) As String
        Select Case TeamNum
            Case 0 : Return "ARI"
            Case 1 : Return "ATL"
            Case 2 : Return "BAL"
            Case 3 : Return "BUF"
            Case 4 : Return "CAR"
            Case 5 : Return "CHI"
            Case 6 : Return "CIN"
            Case 7 : Return "CLE"
            Case 8 : Return "DAL"
            Case 9 : Return "DEN"
            Case 10 : Return "DET"
            Case 11 : Return "GB"
            Case 12 : Return "HOU"
            Case 13 : Return "IND"
            Case 14 : Return "JAX"
            Case 15 : Return "KC"
            Case 16 : Return "MIA"
            Case 17 : Return "MIN"
            Case 18 : Return "NE"
            Case 19 : Return "NO"
            Case 20 : Return "NYG"
            Case 21 : Return "NYJ"
            Case 22 : Return "OAK"
            Case 23 : Return "PHI"
            Case 24 : Return "PIT"
            Case 25 : Return "SD"
            Case 26 : Return "SEA"
            Case 27 : Return "SF"
            Case 28 : Return "STL"
            Case 29 : Return "TB"
            Case 30 : Return "TEN"
            Case 31 : Return "WAS"

        End Select
    End Function
    Private Sub GetPassPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            DT.Rows(i).Item("PassPct") = (DT.Rows(i).Item("PassPlays") / DT.Rows(i).Item("TotalPlays")) * 100
        Next i
    End Sub
    Private Sub GetRunPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            DT.Rows(i).Item("RunPct") = (DT.Rows(i).Item("RunPlays") / DT.Rows(i).Item("TotalPlays")) * 100
        Next i
    End Sub
    Private Sub GetTotTDPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            DT.Rows(i).Item("TotTDPct") = (DT.Rows(i).Item("TotalTD") / DT.Rows(i).Item("TotalPlays")) * 100
        Next i
    End Sub
    Private Sub GetPassTDPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            DT.Rows(i).Item("TDPassPct") = (DT.Rows(i).Item("PassPlays") / DT.Rows(i).Item("PassTd")) * 100
        Next i
    End Sub
    Private Sub GetRunTDPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            DT.Rows(i).Item("TDRunPct") = (DT.Rows(i).Item("RunPlays") / DT.Rows(i).Item("RunTD")) * 100
        Next i
    End Sub
    Private Sub GetIntTDPct(ByVal DT As DataTable, ByVal TableName As String)

        For i = 0 To 31
            If DT.Rows(i).Item("INTTD") <> 0 Then
                DT.Rows(i).Item("TDIntRetPct") = (DT.Rows(i).Item("TotalINT") / DT.Rows(i).Item("IntTD")) * 100
            Else
                DT.Rows(i).Item("TDIntRetPct") = 0
            End If
        Next i

    End Sub
    Private Sub GetFumTDPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            If DT.Rows(i).Item("TDFumRec") <> 0 Then
                DT.Rows(i).Item("TDFumRecPct") = (DT.Rows(i).Item("FumRec") / DT.Rows(i).Item("TDFumRec")) * 100
            Else
                DT.Rows(i).Item("TDFumRecPct") = 0
            End If
        Next i
    End Sub
    Private Sub GetKRTDPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            If DT.Rows(i).Item("KRTD") <> 0 Then
                DT.Rows(i).Item("TDKRPct") = (DT.Rows(i).Item("TotKR") / DT.Rows(i).Item("KRTD")) * 100
            Else
                DT.Rows(i).Item("KRTD") = 0
            End If

        Next i
    End Sub
    Private Sub GetPRTDPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            If DT.Rows(i).Item("TotPRTD") <> 0 Then
                DT.Rows(i).Item("TDPRPct") = (DT.Rows(i).Item("TotPR") / DT.Rows(i).Item("TotPRTD")) * 100
            Else
                DT.Rows(i).Item("TotPRTD") = 0
            End If

        Next i
    End Sub
    Private Sub GetPRFCPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31

            DT.Rows(i).Item("TotFCPct") = (DT.Rows(i).Item("TotPunts") / DT.Rows(i).Item("TotPRFC")) * 100

        Next i

    End Sub
    Private Sub GetPuntDownPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            If DT.Rows(i).Item("PuntDowned") <> 0 Then
                DT.Rows(i).Item("PuntDownedPct") = (DT.Rows(i).Item("TotPunts") / DT.Rows(i).Item("PuntDowned")) * 100
            Else
                DT.Rows(i).Item("PuntDownedPct") = 0
            End If

        Next i
    End Sub
    Private Sub GetPuntTBPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            If DT.Rows(i).Item("PuntTB") <> 0 Then
                DT.Rows(i).Item("PuntTBPct") = (DT.Rows(i).Item("PuntTB") / DT.Rows(i).Item("TotPunts")) * 100
            Else
                DT.Rows(i).Item("PuntTB") = 0
            End If
        Next i


    End Sub
    Private Sub GetPuntMuffPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            If DT.Rows(i).Item("PuntMuffs") <> 0 Then
                DT.Rows(i).Item("PuntMuffPct") = (DT.Rows(i).Item("PuntMuffs") / DT.Rows(i).Item("TotPunts")) * 100
            Else
                DT.Rows(i).Item("PuntMuffPct") = 0
            End If
        Next i
    End Sub
    Private Sub GetKOTBPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            If DT.Rows(i).Item("KOTB") <> 0 Then
                DT.Rows(i).Item("KOTBPct") = (DT.Rows(i).Item("KOTB") / DT.Rows(i).Item("TOtKO")) * 100
            Else
                DT.Rows(i).Item("KOTBPct") = 0
            End If

        Next i
    End Sub
    Private Sub GetKOOOBPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            If DT.Rows(i).Item("KOOOB") <> 0 Then
                DT.Rows(i).Item("KOOOBPct") = (DT.Rows(i).Item("KOOOB") / DT.Rows(i).Item("TotKO")) * 100
            Else
                DT.Rows(i).Item("KOOOBPct") = 0
            End If
        Next i
    End Sub
    Private Sub GetFGPct(ByVal DT As DataTable, ByVal TableName As String)
        For i = 0 To 31
            If DT.Rows(i).Item("FGMade") <> 0 Then
                DT.Rows(i).Item("FGPct") = (DT.Rows(i).Item("FGMade") / DT.Rows(i).Item("FGAtt")) * 100
            Else
                DT.Rows(i).Item("FGPct") = 0
            End If

        Next i
    End Sub
    Private Sub GetPlays()
        Dim Count As Integer
        For i = 1 To 22
            QuartTime(i) 'sets game situation to grab data on
            For d As Integer = 1 To 4
                Down = d
                For Dist As Integer = 1 To 9
                    Distance(Dist)
                    For PointDiff As Integer = 1 To 10
                        GetPD(PointDiff)
                        For Yardline As Integer = 1 To 12
                            GetYardline(Yardline)
                            GetSQL(PlayData, "Playdata", "dbo.PD_Passes", "@Quarter", "@DistMax", "DistMin", "PDMax", _
                            "@PDMin", "@YLMax", "@YLMin", "@Down", "@MinLeftMax", "@MinLeftMin", "@SecLeftMax", "@SecLeftMin", "Passes")
                            Count += 1
                            'Console.WriteLine(GetColName() & "   Situation#:" & Count)
                        Next Yardline
                    Next PointDiff
                Next Dist
            Next d
        Next i
    End Sub
    Private Function GQ(ByVal Quarter As Integer) As Integer
        Select Case GQ
            Case 1 : Return 1
            Case 2 : Return 2
            Case 3 : Return 3
            Case 4 : Return 4
            Case 5 : Return 5
        End Select

    End Function
    Private Sub QuartTime(ByVal Num As Integer)
        Select Case Num
            Case 1 'Whole Quarter
                Quarter = 1
                MinLeftMax = 15
                MinLeftMin = 0
            Case 2 'Q2 15-10
                Quarter = 2
                MinLeftMax = 15
                MinLeftMin = 6
            Case 3 'Q2 10-5
                Quarter = 2
                MinLeftMax = 5
                MinLeftMin = 3
            Case 4 'Q2 5-2
                Quarter = 2
                MinLeftMax = 2
                MinLeftMin = 1
            Case 5 'Q2 2-1
                Quarter = 2
                SecLeftMax = 59
                SecLeftMin = 31
                MinLeftMax = 0
                MinLeftMin = 0
            Case 6 'Q2 60-30sec
                Quarter = 2
                SecLeftMax = 30
                SecLeftMin = 10
            Case 7 'Q2 30-10sec
                Quarter = 2
                SecLeftMax = 9
                SecLeftMin = 1
            Case 8 'Q2 10-1sec
                Quarter = 3
                MinLeftMax = 15
                MinLeftMin = 6
            Case 9
                Quarter = 3
                MinLeftMax = 5
                MinLeftMin = 1

            Case 10
                Quarter = 4
                MinLeftMax = 15
                MinLeftMin = 11
            Case 11
                Quarter = 4
                MinLeftMax = 10
                MinLeftMin = 7
            Case 12
                Quarter = 4
                MinLeftMax = 6
                MinLeftMin = 5
            Case 13
                Quarter = 4
                MinLeftMax = 4
                MinLeftMin = 3
            Case 14
                Quarter = 4
                MinLeftMax = 2
                MinLeftMin = 1
            Case 15
                Quarter = 4
                SecLeftMax = 59
                SecLeftMin = 31
                MinLeftMax = 0
                MinLeftMin = 0
            Case 16
                Quarter = 4
                SecLeftMax = 30
                SecLeftMin = 10
                MinLeftMax = 0
                MinLeftMin = 0
            Case 17
                Quarter = 4
                SecLeftMax = 9
                SecLeftMin = 1
                MinLeftMax = 0
                MinLeftMin = 0
            Case 18
                Quarter = 5
                MinLeftMax = 15
                MinLeftMin = 3
            Case 19
                Quarter = 5
                MinLeftMax = 2
                MinLeftMin = 1
            Case 20
                Quarter = 5
                SecLeftMax = 59
                SecLeftMin = 31
                MinLeftMax = 0
                MinLeftMin = 0
            Case 21
                Quarter = 5
                SecLeftMax = 30
                SecLeftMin = 11
                MinLeftMax = 0
                MinLeftMin = 0
            Case 22
                Quarter = 5
                SecLeftMax = 10
                SecLeftMin = 1
                MinLeftMax = 0
                MinLeftMin = 0
        End Select


    End Sub
    Private Sub Distance(ByVal Num As Integer)
        Select Case Num
            Case 1
                DistMax = 99
                DistMin = 15
            Case 2
                DistMax = 14
                DistMin = 11
            Case 3
                DistMax = 10
                DistMin = 8
            Case 4
                DistMax = 7
                DistMin = 6
            Case 5
                DistMax = 5
                DistMin = 4
            Case 6
                DistMax = 3
                DistMin = 3
            Case 7
                DistMax = 2
                DistMin = 2
            Case 8
                DistMax = 1
                DistMin = 1
            Case 9
                DistMax = 0
                DistMin = 0
        End Select


    End Sub
    Private Sub GetPD(ByVal Num As Integer)
        Select Case Num
            Case 1
                PDMax = -100
                PDMin = -20
            Case 2
                PDMax = -19
                PDMin = -17
            Case 3
                PDMax = -16
                PDMin = -14
            Case 4
                PDMax = -13
                PDMin = -12
            Case 5
                PDMax = -11
                PDMin = -9
            Case 6
                PDMax = -8
                PDMin = -7
            Case 7
                PDMax = -6
                PDMin = -4
            Case 8
                PDMax = -3
                PDMin = -1
            Case 9
                PDMax = 0
                PDMin = 0
            Case 10
                PDMax = 3
                PDMin = 1
            Case 11
                PDMax = 6
                PDMin = 4
            Case 12
                PDMax = 8
                PDMin = 7
            Case 13
                PDMax = 11
                PDMin = 9
            Case 14
                PDMax = 13
                PDMin = 12
            Case 15
                PDMax = 16
                PDMin = 14
            Case 16
                PDMax = 19
                PDMin = 17
            Case 17
                PDMax = 100
                PDMin = 20
        End Select

    End Sub
    Private Sub GetYardline(ByVal Num As Integer)
        Select Case Num
            Case 1
                YLMax = 2
                YLMin = 0
            Case 2
                YLMax = 5
                YLMin = 3
            Case 3
                YLMax = 10
                YLMin = 6
            Case 4
                YLMax = 19
                YLMin = 11
            Case 5
                YLMax = 39
                YLMin = 20
            Case 6
                YLMax = 59
                YLMin = 40
            Case 7
                YLMax = 69
                YLMin = 60
            Case 8
                YLMax = 79
                YLMin = 70
            Case 9
                YLMax = 89
                YLMin = 80
            Case 10
                YLMax = 94
                YLMin = 90
            Case 11
                YLMax = 97
                YLMin = 95
            Case 12
                YLMax = 100
                YLMin = 98

        End Select

    End Sub
    Private Function GetColName() As String
        PlayData.Rows.Add()
        PlayData.Rows(i - 1).Item("Quarter") = CStr(Quarter)
        PlayData.Rows(i - 1).Item("DistMax") = CStr(DistMax)
        PlayData.Rows(i - 1).Item("DistMin") = CStr(DistMin)
        PlayData.Rows(i - 1).Item("PDMax") = CStr(PDMax)
        PlayData.Rows(i - 1).Item("PDMin") = CStr(PDMin)
        PlayData.Rows(i - 1).Item("YLMax") = CStr(YLMax)
        PlayData.Rows(i - 1).Item("YLMin") = CStr(YLMin)
        PlayData.Rows(i - 1).Item("Down") = CStr(Down)
        PlayData.Rows(i - 1).Item("MinLeftMax") = CStr(MinLeftMax)
        PlayData.Rows(i - 1).Item("MinLeftMin") = CStr(MinLeftMin)
        PlayData.Rows(i - 1).Item("SecLeftMax") = CStr(SecLeftMax)
        PlayData.Rows(i - 1).Item("SecLeftMin") = CStr(SecLeftMin)
    End Function

End Class
