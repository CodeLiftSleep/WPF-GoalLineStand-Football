Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Strings
Imports System.String

''' <summary>
''' AbstractReasoning/Intelligent/MentalCapacity/Nurturing/Friendly/Participator/Caring/EmotionallyStable/Adaptive/Mature/CalmUnderPressure/Stubborn/Aggressive/Competitive/Bossy/Enthusiastic/Impulsive/FunLoving/Expressive/Dutiful/TeamPlayer/FollowsRules/Moralistic
''' SociallyBold/ThickSkinned/Adventurous/Uninhibited/Refined/Intuitive/Sentimental/Sensitive/Suspicious/Oppositional/Distrustful/Vigilant/Impractical/Imaginative/AbsentMinded/AbstractThinker/Private/Astute/Diplomatic/Polished/Fearful/SelfDoubting/GuiltProne
''' Insecure/Adaptable/Expermiental/Analytical/Critical/SelfSufficient/Resourceful/Individualistic/Loner/Perfectionist/StrongWilled/Organized/Controlling/HighEnergy/Impatient/Driven/Tense/Honesty/Fairness/GreedAvoidance/Modesty
''' </summary>
Public MustInherit Class Person

    'Dim DTOutputTo As DataTable
    Dim ex As Exception

    Public PersonSQLString As String = "AbsentMinded int NULL, AbstractReasoning int NULL, AbstractThinker int NULL, Adaptable int NULL, Adventurous int NULL, Aggressive int NULL, Analytical int NULL, Astute int NULL, Bossy int NULL, CalmUnderPressure int NULL," +
"Caring int NULL, Competitive int NULL, Conforming int NULL, Controlling int NULL, Critical int NULL, Diplomatic int NULL, Driven int NULL, Dutiful int NULL, EmotionallyStable int NULL, Enthusiastic int NULL, Expermiental int NULL, Expressive int NULL," +
"Fairness int NULL, Fearful int NULL, FollowsRules int NULL, Friendly int NULL, FunLoving int NULL, GuiltProne int NULL, GreedAvoidance int NULL, HighEnergy int NULL, Honesty int NULL, Imaginative int NULL, Impatient int NULL, Impractical int NULL," +
"Impulsive int NULL, Individualistic int NULL, Insecure int NULL, Intelligent int NULL, Loner int NULL, Mature int NULL, MentalCapacity int NULL, Modesty int NULL, Moralistic int NULL, Nurturing int NULL,  Organized int NULL, Participator int NULL," +
"Perfectionist int NULL, Polished int NULL, Private int NULL, Resourceful int NULL, SelfDoubting int NULL, SelfSufficient int NULL, StrongWilled int NULL, Stubborn int NULL, TeamPlayer int NULL, Tense int NULL, Vigilant int NULL, Dominant varchar(25) NULL, Weakest varchar(25) NULL"

    Property Caring As Integer
    Property Nurturing As Integer
    Property AbstractReasoning As Integer
    Property MentalCapacity As Integer
    Property Intelligent As Integer
    Property Friendly As Integer
    Property Participator As Integer
    Property EmotionallyStable As Integer
    Property Conforming As Integer
    Property Mature As Integer
    Property CalmUnderPressure As Integer
    Property Stubborn As Integer
    Property Aggressive As Integer
    Property Competitive As Integer
    Property Bossy As Integer
    Property Enthusiastic As Integer
    Property Impulsive As Integer
    Property FunLoving As Integer
    Property Expressive As Integer
    Property Dutiful As Integer
    Property TeamPlayer As Integer
    Property FollowsRules As Integer
    Property Moralistic As Integer
    Property SociallyBold As Integer
    Property ThickSkinned As Integer
    Property Adventurous As Integer
    Property Uninhibited As Integer
    Property Refined As Integer
    Property Intuitive As Integer
    Property Sentimental As Integer
    Property Sensitive As Integer
    Property Suspicious As Integer
    Property Oppositional As Integer
    Property Distrustful As Integer
    Property Vigilant As Integer
    Property Impractical As Integer
    Property Imaginative As Integer
    Property AbsentMinded As Integer
    Property AbstractThinker As Integer
    Property Privateness As Integer
    Property Astute As Integer
    Property Diplomatic As Integer
    Property Polished As Integer
    Property Fearful As Integer
    Property SelfDoubting As Integer
    Property GuiltProne As Integer
    Property Insecure As Integer
    Property Adaptable As Integer
    Property Experimental As Integer
    Property Analytical As Integer
    Property Critical As Integer
    Property SelfSufficient As Integer
    Property Resourceful As Integer
    Property Individualistic As Integer
    Property Loner As Integer
    Property Perfectionist As Integer
    Property StrongWilled As Integer
    Property Organized As Integer
    Property Controlling As Integer
    Property HighEnergy As Integer
    Property Impatient As Integer
    Property Driven As Integer
    Property Tense As Integer
    Property Honesty As Integer
    Property Fairness As Integer
    Property GreedAvoidance As Integer
    Property Modesty As Integer
    Property FirstName As String
    Property LastName As String
    Property College As String
    Property Age As String
    Property DOB As Date
    Property Height As Integer
    Property Weight As Integer
    Property HandSize As Double
    Property ArmLength As Double
    Property PersonType As String
    Property Dominant As String
    Property Weakest As String



    Private Sub GetPersonalityStats(ByVal dt As DataTable, ByVal personNum As Integer)
        dt.Rows(personNum).Item("AbsentMinded") = AbsentMinded
        dt.Rows(personNum).Item("AbstractReasoning") = AbstractReasoning
        dt.Rows(personNum).Item("AbstractThinker") = AbstractThinker
        dt.Rows(personNum).Item("Adaptable") = Adaptable
        dt.Rows(personNum).Item("Adventurous") = Adventurous
        dt.Rows(personNum).Item("Aggressive") = Aggressive
        dt.Rows(personNum).Item("Analytical") = Analytical
        dt.Rows(personNum).Item("Astute") = Astute
        dt.Rows(personNum).Item("Bossy") = Bossy
        dt.Rows(personNum).Item("CalmUnderPressure") = CalmUnderPressure
        dt.Rows(personNum).Item("Caring") = Caring
        dt.Rows(personNum).Item("Competitive") = Competitive
        dt.Rows(personNum).Item("Conforming") = Conforming
        dt.Rows(personNum).Item("Controlling") = Controlling
        dt.Rows(personNum).Item("Critical") = Critical
        dt.Rows(personNum).Item("Diplomatic") = Diplomatic
        dt.Rows(personNum).Item("Dutiful") = Dutiful
        dt.Rows(personNum).Item("Driven") = Driven
        dt.Rows(personNum).Item("EmotionallyStable") = EmotionallyStable
        dt.Rows(personNum).Item("Enthusiastic") = Enthusiastic
        dt.Rows(personNum).Item("Expermiental") = Experimental
        dt.Rows(personNum).Item("Expressive") = Expressive
        dt.Rows(personNum).Item("Fairness") = Fairness
        dt.Rows(personNum).Item("Fearful") = Fearful
        dt.Rows(personNum).Item("FollowsRules") = FollowsRules
        dt.Rows(personNum).Item("Friendly") = Friendly
        dt.Rows(personNum).Item("FunLoving") = FunLoving
        dt.Rows(personNum).Item("GuiltProne") = GuiltProne
        dt.Rows(personNum).Item("GreedAvoidance") = GreedAvoidance
        dt.Rows(personNum).Item("HighEnergy") = HighEnergy
        dt.Rows(personNum).Item("Honesty") = Honesty
        dt.Rows(personNum).Item("Impulsive") = Impulsive
        dt.Rows(personNum).Item("Imaginative") = Imaginative
        dt.Rows(personNum).Item("Impatient") = Impatient
        dt.Rows(personNum).Item("Impractical") = Impractical
        dt.Rows(personNum).Item("Individualistic") = Individualistic
        dt.Rows(personNum).Item("Insecure") = Insecure
        dt.Rows(personNum).Item("Intelligent") = Intelligent
        dt.Rows(personNum).Item("Loner") = Loner
        dt.Rows(personNum).Item("Mature") = Mature
        dt.Rows(personNum).Item("MentalCapacity") = MentalCapacity
        dt.Rows(personNum).Item("Modesty") = Modesty
        dt.Rows(personNum).Item("Moralistic") = Moralistic
        dt.Rows(personNum).Item("Nurturing") = Nurturing
        dt.Rows(personNum).Item("Organized") = Organized
        dt.Rows(personNum).Item("Participator") = Participator
        dt.Rows(personNum).Item("Perfectionist") = Perfectionist
        dt.Rows(personNum).Item("Polished") = Polished
        dt.Rows(personNum).Item("Private") = Privateness
        dt.Rows(personNum).Item("Resourceful") = Resourceful
        dt.Rows(personNum).Item("SelfDoubting") = SelfDoubting
        dt.Rows(personNum).Item("SelfSufficient") = SelfSufficient
        dt.Rows(personNum).Item("StrongWilled") = StrongWilled
        dt.Rows(personNum).Item("Stubborn") = Stubborn
        dt.Rows(personNum).Item("TeamPlayer") = TeamPlayer
        dt.Rows(personNum).Item("Tense") = Tense
        dt.Rows(personNum).Item("Vigilant") = Vigilant
        dt.Rows(personNum).Item("Dominant") = Dominant
        dt.Rows(personNum).Item("Weakest") = Weakest
    End Sub

    ''' <summary>
    ''' Takes a Data Table parameter and the file name parameter and then creates 3 seperate data tables for them.
    ''' Loads Files to DataTables and then uses the datatables while generating to prevent constant opening and closing of files
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="file"></param>
    Public Shared Sub PutDataInDT(ByVal DT As DataTable, ByVal file As StreamReader)

        Dim StringArray As String() = file.ReadLine().Split(","c)
        Dim Row As DataRow
        Dim Line As String

        For Each s As String In StringArray
            DT.Columns.Add(New DataColumn())
        Next

        Row = DT.NewRow() 'adds in the initial line, or this was getting skipped previously
        Row.ItemArray = StringArray
        DT.Rows.Add(Row)

        Using file
            While Not file.EndOfStream
                Line = file.ReadLine
                Row = DT.NewRow()
                Try
                    Row.ItemArray = Line.Split(","c)
                    DT.Rows.Add(Row)
                Catch ex As System.ArgumentException
                    Console.WriteLine(ex.Message)
                End Try

            End While
        End Using
    End Sub

    Private Shared Function GetItem(ByVal myItem As DataTable, ByVal dtOutPutTo As DataTable) As String
        Dim Count As Integer = myItem.Rows.Count - 1
        Dim Result As String = ""
        Dim MinName As Double = 0
        Dim MaxName As Double = myItem.Rows(Count).Item(2) 'gets the last frequency value
        Dim ResName As Double = Math.Round(MT.GenerateDouble(MinName, MaxName), 3)
        Dim RowMin As Double
        Dim RowMax As Double

        'Console.WriteLine(ResName & "   " & ResCol)
        For row As Integer = 0 To myItem.Rows.Count - 1 'cycles through the rows until it finds the appropriate row
            RowMin = myItem.Rows(row).Item(1)
            RowMax = myItem.Rows(row).Item(2)
            If ResName >= RowMin And ResName <= RowMax Then 'return name in Proper Case instead of All Caps
                Result = StrConv(myItem.Rows(row).Item(0).ToString, VbStrConv.ProperCase)
                Exit For
            End If
        Next row
        Return Result
    End Function

    Private Shared Function GetCollege(ByVal myItem As DataTable, ByVal outPutTo As DataTable) As KeyValuePair(Of String, String) 'returns the College Name as Key and Scout Region as Value

        Dim Count As Integer = myItem.Rows.Count - 1
        Dim RowMin As Double
        Dim RowMax As Double
        Dim MinCol As Integer = 0
        Dim MaxCol As Integer = myItem.Rows(Count).Item(2)
        Dim ResCol As Integer = MT.GenerateInt32(MinCol, MaxCol)

        For row As Integer = 0 To myItem.Rows.Count - 1 'cycles through the rows until it finds the appropriate row
            RowMin = myItem.Rows(row).Item(1)
            RowMax = myItem.Rows(row).Item(2)
            If ResCol >= RowMin And ResCol <= RowMax Then
                Return New KeyValuePair(Of String, String)(myItem.Rows(row).Item(0).ToString, myItem.Rows(row).Item(3)) 'return college name as key, scouting region as value
                Exit For
            End If
        Next row
    End Function

    ''' <summary>
    ''' Generates all the Data a "Person" would have
    ''' For some reason names and colleges will return nothing at times, trying to track down the source of this...have it regenerating a value when this
    ''' happens currently.
    ''' </summary>
    ''' <param name="DTOutputTo"></param>
    ''' <param name="Row"></param>
    ''' <param name="PersonType"></param>
    ''' <param name="Position"></param>
    Public Shared Sub GenNames(ByRef dtOutputTo As DataTable, ByVal row As Integer, ByVal personType As String, Optional ByVal position As String = "", Optional ByVal DraftRound As String = "")

        Dim MyCollege As New KeyValuePair(Of String, String)
        MyCollege = GetCollege(Colleges, dtOutputTo)

        Try
            dtOutputTo.Rows(row).Item("FName") = String.Format("'{0}'", GetItem(FirstNames, dtOutputTo)) 'adds the necessary ' ' modifier to strings for SQLite
            dtOutputTo.Rows(row).Item("LName") = String.Format("'{0}'", GetItem(LastNames, dtOutputTo))
            dtOutputTo.Rows(row).Item("College") = String.Format("'{0}'", MyCollege.Key)
            dtOutputTo.Rows(row).Item("Age") = GenAge(personType, position)
            dtOutputTo.Rows(row).Item("DOB") = String.Format("'{0}", GetDOB(dtOutputTo.Rows(row).Item("Age")))
            dtOutputTo.Rows(row).Item("Height") = GetHeight(position, DraftRound)
            dtOutputTo.Rows(row).Item("Weight") = GetWeight(dtOutputTo.Rows(row).Item("Height"), position, DraftRound)
        Catch ex As System.InvalidCastException
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.Data)
        End Try

        If position <> "" Then 'only generates this data if they are a player as its not relevant to the other people
            dtOutputTo.Rows(row).Item("HandLength") = GetHandLength(dtOutputTo.Rows(row).Item("Height"), dtOutputTo.Rows(row).Item("Age"))
            dtOutputTo.Rows(row).Item("ArmLength") = GetArmLength(dtOutputTo.Rows(row).Item("Height"))
            dtOutputTo.Rows(row).Item("ScoutRegion") = String.Format("'{0}'", MyCollege.Value)
        End If

    End Sub

    ''' <summary>
    ''' Returns the person's Date Of Birth
    ''' </summary>
    ''' <param name="age"></param>
    ''' <returns></returns>
    Private Shared Function GetDOB(ByVal age As Integer) As String
        Dim Day As Integer
        Dim Month As Integer
        Dim Year As Integer

        Month = MT.GenerateInt32(1, 12)

        Select Case Month
            Case 1, 3, 5, 7, 8, 10, 12
                Day = MT.GenerateInt32(1, 31)
            Case 2
                Day = MT.GenerateInt32(1, 28)
            Case Else
                Day = MT.GenerateInt32(1, 30)
        End Select

        Year = Date.Today.Year - age

        Return String.Format("{0}/{1}/{2}'", Month, Day, Year) 'creates the proper format for SQLite ' ' around string

    End Function

    ''' <summary>
    ''' Generates the player age based on Person Type and in the case of NFL Player, by Position
    ''' </summary>
    ''' <param name="personType"></param>
    ''' <param name="position"></param>
    ''' <returns></returns>
    Private Shared Function GenAge(ByVal personType As String, Optional ByVal position As String = "") As Integer
        Dim Result As Integer
        Select Case personType
            Case "Owner"
                Result = MT.GenerateInt32(45, 89)
            Case "Personnel", "Coach", "Trainer", "Agent"
                Result = MT.GenerateInt32(24, 70)
            Case "NFLPlayer"
                Result = GetPlayerAge(position)
            Case "CollegePlayer"
                Result = GetDraftAge(position)
        End Select
        Return Result
    End Function

    Private Shared Function GetPlayerAge(ByVal pos As String) As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB"
                Select Case MT.GenerateInt32(1, 97)
                    Case 1 To 2 : Result = 21
                    Case 3 To 5 : Result = 22
                    Case 6 To 11 : Result = 23
                    Case 12 To 25 : Result = 24
                    Case 26 To 35 : Result = 25
                    Case 36 To 47 : Result = 26
                    Case 48 To 54 : Result = 27
                    Case 55 To 62 : Result = 28
                    Case 63 To 71 : Result = 29
                    Case 72 To 76 : Result = 30
                    Case 77 To 78 : Result = 31
                    Case 79 To 84 : Result = 32
                    Case 85 To 87 : Result = 33
                    Case 88 To 90 : Result = 34
                    Case 91 : Result = 35
                    Case 92 : Result = 36
                    Case 93 To 94 : Result = 37
                    Case 95 : Result = 38
                    Case 96 To 97 : Result = 39
                End Select
            Case "RB"
                Select Case MT.GenerateInt32(1, 128)
                    Case 1 To 2 : Result = 21
                    Case 3 To 18 : Result = 22
                    Case 19 To 31 : Result = 23
                    Case 32 To 56 : Result = 24
                    Case 57 To 71 : Result = 25
                    Case 72 To 89 : Result = 26
                    Case 90 To 99 : Result = 27
                    Case 100 To 106 : Result = 28
                    Case 107 To 112 : Result = 29
                    Case 113 To 121 : Result = 30
                    Case 122 To 124 : Result = 31
                    Case 125 To 126 : Result = 32
                    Case 127 To 128 : Result = 33
                End Select
            Case "FB"
                Select Case MT.GenerateInt32(1, 39)
                    Case 1 To 5 : Result = 23
                    Case 6 To 9 : Result = 24
                    Case 10 To 11 : Result = 25
                    Case 12 To 15 : Result = 26
                    Case 16 To 19 : Result = 27
                    Case 20 To 23 : Result = 28
                    Case 24 To 31 : Result = 29
                    Case 32 To 33 : Result = 30
                    Case 34 To 35 : Result = 31
                    Case 36 To 37 : Result = 32
                    Case 38 To 39
                        Select Case MT.GenerateInt32(1, 5)
                            Case 1 : Result = 33
                            Case 2 : Result = 34
                            Case 3 : Result = 35
                            Case 4 : Result = 36
                            Case 5 : Result = 37
                        End Select
                End Select
            Case "WR"
                Select Case MT.GenerateInt32(1, 195)
                    Case 1 To 4 : Result = 21
                    Case 5 To 23 : Result = 22
                    Case 24 To 48 : Result = 23
                    Case 49 To 75 : Result = 24
                    Case 76 To 98 : Result = 25
                    Case 99 To 121 : Result = 26
                    Case 122 To 135 : Result = 27
                    Case 136 To 157 : Result = 28
                    Case 158 To 162 : Result = 29
                    Case 163 To 174 : Result = 30
                    Case 175 To 179 : Result = 31
                    Case 180 To 182 : Result = 32
                    Case 183 To 188 : Result = 33
                    Case 189 : Result = 34
                    Case 190 To 191 : Result = 35
                    Case 192 To 194 : Result = 36
                    Case 195 : Result = 37
                End Select
            Case "TE"
                Select Case MT.GenerateInt32(1, 115)
                    Case 1 To 9 : Result = 22
                    Case 10 To 17 : Result = 23
                    Case 18 To 33 : Result = 24
                    Case 34 To 50 : Result = 25
                    Case 51 To 63 : Result = 26
                    Case 64 To 72 : Result = 27
                    Case 73 To 79 : Result = 28
                    Case 80 To 92 : Result = 29
                    Case 93 To 100 : Result = 30
                    Case 101 To 103 : Result = 31
                    Case 104 To 110 : Result = 32
                    Case 111 To 115 : Result = 33
                End Select
            Case "OC", "C"
                Select Case MT.GenerateInt32(1, 90)
                    Case 1 : Result = 22
                    Case 2 To 9 : Result = 23
                    Case 10 To 17 : Result = 24
                    Case 18 To 23 : Result = 25
                    Case 24 To 34 : Result = 26
                    Case 35 To 45 : Result = 27
                    Case 46 To 53 : Result = 28
                    Case 54 To 60 : Result = 29
                    Case 61 To 67 : Result = 30
                    Case 68 To 69 : Result = 31
                    Case 70 To 81 : Result = 32
                    Case 82 : Result = 33
                    Case 83 To 85 : Result = 34
                    Case 86 : Result = 35
                    Case 87 : Result = 36
                    Case 88 : Result = 37
                    Case 89 To 90 : Result = 38
                End Select
            Case "OG"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 4 : Result = 22
                    Case 5 To 15 : Result = 23
                    Case 16 To 26 : Result = 24
                    Case 27 To 36 : Result = 25
                    Case 37 To 50 : Result = 26
                    Case 51 To 66 : Result = 27
                    Case 67 To 72 : Result = 28
                    Case 73 To 81 : Result = 29
                    Case 82 To 88 : Result = 30
                    Case 89 To 93 : Result = 31
                    Case 94 To 97 : Result = 32
                    Case 98 To 100 : Result = 33
                End Select
            Case "OT"
                Select Case MT.GenerateInt32(1, 143)
                    Case 1 : Result = 21
                    Case 2 To 5 : Result = 22
                    Case 6 To 25 : Result = 23
                    Case 26 To 45 : Result = 24
                    Case 46 To 67 : Result = 25
                    Case 68 To 88 : Result = 26
                    Case 89 To 95 : Result = 27
                    Case 96 To 104 : Result = 28
                    Case 105 To 115 : Result = 29
                    Case 116 To 126 : Result = 30
                    Case 127 To 129 : Result = 31
                    Case 130 To 132 : Result = 32
                    Case 133 To 137 : Result = 33
                    Case 138 To 141 : Result = 34
                    Case 142 : Result = 35
                End Select
            Case "DE"
                Select Case MT.GenerateInt32(1, 155)
                    Case 1 To 4 : Result = 21
                    Case 5 To 13 : Result = 22
                    Case 14 To 35 : Result = 23
                    Case 36 To 53 : Result = 24
                    Case 54 To 70 : Result = 25
                    Case 71 To 87 : Result = 26
                    Case 88 To 98 : Result = 27
                    Case 99 To 109 : Result = 28
                    Case 110 To 118 : Result = 29
                    Case 119 To 128 : Result = 30
                    Case 129 To 134 : Result = 31
                    Case 135 To 142 : Result = 32
                    Case 143 To 148 : Result = 33
                    Case 149 To 151 : Result = 34
                    Case 152 To 154 : Result = 35
                    Case 155 : Result = 36
                End Select
            Case "DT"
                Select Case MT.GenerateInt32(1, 150)
                    Case 1 To 9 : Result = 22
                    Case 10 To 26 : Result = 23
                    Case 27 To 47 : Result = 24
                    Case 48 To 67 : Result = 25
                    Case 68 To 87 : Result = 26
                    Case 88 To 99 : Result = 27
                    Case 100 To 109 : Result = 28
                    Case 110 To 120 : Result = 29
                    Case 121 To 131 : Result = 30
                    Case 132 To 137 : Result = 31
                    Case 138 To 142 : Result = 32
                    Case 143 To 145 : Result = 33
                    Case 146 To 147 : Result = 34
                    Case 148 To 150
                        Select Case MT.GenerateInt32(1, 5)
                            Case 1 : Result = 35
                            Case 2 : Result = 36
                            Case 3 : Result = 37
                            Case 4 : Result = 38
                            Case 5 : Result = 39
                        End Select
                End Select
            Case "OLB", "ILB"
                Select Case MT.GenerateInt32(1, 238)
                    Case 1 To 15 : Result = 22
                    Case 16 To 45 : Result = 23
                    Case 46 To 80 : Result = 24
                    Case 81 To 114 : Result = 25
                    Case 115 To 143 : Result = 26
                    Case 144 To 166 : Result = 27
                    Case 167 To 182 : Result = 28
                    Case 183 To 197 : Result = 29
                    Case 198 To 210 : Result = 30
                    Case 211 To 217 : Result = 31
                    Case 218 To 228 : Result = 32
                    Case 229 To 232 : Result = 33
                    Case 233 To 236 : Result = 34
                    Case 237 To 238 : Result = 35
                End Select
            Case "CB"
                Select Case MT.GenerateInt32(1, 195)
                    Case 1 To 5 : Result = 21
                    Case 6 To 23 : Result = 22
                    Case 24 To 54 : Result = 23
                    Case 55 To 87 : Result = 24
                    Case 88 To 109 : Result = 25
                    Case 110 To 133 : Result = 26
                    Case 134 To 147 : Result = 27
                    Case 148 To 163 : Result = 28
                    Case 164 To 167 : Result = 29
                    Case 168 To 176 : Result = 30
                    Case 177 To 181 : Result = 31
                    Case 182 To 186 : Result = 32
                    Case 187 To 189 : Result = 33
                    Case 190 To 193 : Result = 34
                    Case 194 To 195 : Result = 35
                End Select
            Case "FS", "SS"
                Select Case MT.GenerateInt32(1, 138)
                    Case 1 To 10 : Result = 22
                    Case 11 To 18 : Result = 23
                    Case 19 To 42 : Result = 24
                    Case 43 To 57 : Result = 25
                    Case 58 To 86 : Result = 26
                    Case 87 To 101 : Result = 27
                    Case 102 To 110 : Result = 28
                    Case 111 To 119 : Result = 29
                    Case 120 To 125 : Result = 30
                    Case 126 To 130 : Result = 31
                    Case 132 To 133 : Result = 32
                    Case 134 To 135 : Result = 33
                    Case 136 : Result = 34
                    Case 137 To 138 : Result = 35
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 37)
                    Case 1 To 2 : Result = 22
                    Case 3 To 5 : Result = 23
                    Case 6 To 7 : Result = 24
                    Case 8 To 10 : Result = 25
                    Case 11 To 12 : Result = 26
                    Case 13 To 14 : Result = 27
                    Case 15 To 16 : Result = 28
                    Case 17 : Result = 29
                    Case 18 : Result = 30
                    Case 19 To 22 : Result = 31
                    Case 23 To 24 : Result = 32
                    Case 25 To 27 : Result = 33
                    Case 28 To 29 : Result = 34
                    Case 30 : Result = 35
                    Case 31 To 33 : Result = 36
                    Case 37 : Result = 1
                    Case 38 : Result = 1
                    Case 39 : Result = 1
                    Case 40 : Result = 1
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 34)
                    Case 1 : Result = 22
                    Case 2 To 3 : Result = 23
                    Case 4 : Result = 24
                    Case 5 To 7 : Result = 25
                    Case 8 To 9 : Result = 26
                    Case 10 To 13 : Result = 27
                    Case 14 To 16 : Result = 28
                    Case 17 To 19 : Result = 29
                    Case 20 To 21 : Result = 30
                    Case 22 To 23 : Result = 31
                    Case 24 To 25 : Result = 32
                    Case 26 To 27 : Result = 33
                    Case 28 : Result = 34
                    Case 29 : Result = 35
                    Case 30 : Result = 36
                    Case 31 : Result = 37
                    Case 32 : Result = 38
                    Case 33 : Result = 39
                    Case 34 : Result = 40
                End Select
        End Select
        Return Result
    End Function

    ''' <summary>
    ''' Generates the draft age for a college player
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function GetDraftAge(ByVal pos As String) As Integer
        Dim Result As Integer
        Dim i As Double = MT.GenerateDouble(0, 100)
        Select Case pos
            Case "CB"
                Select Case i
                    Case 0 To 0.29 : Result = 20
                    Case 0.3 To 6.12 : Result = 21
                    Case 6.13 To 56.52 : Result = 22
                    Case 56.53 To 92.71 : Result = 23
                    Case 92.71 To 99.29 : Result = 24
                    Case Else : Result = 25
                End Select
            Case "DE"
                Select Case i
                    Case 0 To 0.18 : Result = 20
                    Case 0.19 To 6.63 : Result = 21
                    Case 6.64 To 53.54 : Result = 22
                    Case 53.55 To 89.96 : Result = 23
                    Case 89.97 To 98.21 : Result = 24
                    Case 98.22 To 99.64 : Result = 25
                    Case Else : Result = 26
                End Select
            Case "DT"
                Select Case i
                    Case 0 To 0.77 : Result = 20
                    Case 0.78 To 7.68 : Result = 21
                    Case 7.69 To 53.07 : Result = 22
                    Case 53.08 To 88.1 : Result = 23
                    Case 88.11 To 97.31 : Result = 24
                    Case 97.32 To 99.04 : Result = 25
                    Case 99.05 To 99.81 : Result = 26
                    Case Else : Result = 27
                End Select
            Case "FB"
                Select Case i
                    Case 0 To 35.29 : Result = 22
                    Case 35.3 To 82.35 : Result = 23
                    Case 82.36 To 95.29 : Result = 24
                    Case 95.3 To 100.0 : Result = 25
                End Select
            Case "FS"
                Select Case i
                    Case 0 To 5.88 : Result = 21
                    Case 5.89 To 59.34 : Result = 22
                    Case 59.35 To 94.12 : Result = 23
                    Case 94.13 To 99.65 : Result = 24
                    Case Else : Result = 25
                End Select

            Case "ILB"
                Select Case i
                    Case 0 To 5.35 : Result = 21
                    Case 5.36 To 55.77 : Result = 22
                    Case 55.78 To 93.98 : Result = 23
                    Case 93.99 To 99.0 : Result = 24
                    Case 99.01 To 99.67 : Result = 25
                    Case Else : Result = 26
                End Select
            Case "K"
                Select Case i
                    Case 0 To 2.13 : Result = 21
                    Case 2.14 To 56.38 : Result = 22
                    Case 56.39 To 82.98 : Result = 23
                    Case Else : Result = 24
                End Select
            Case "LS"
                Select Case i
                    Case 0 To 25 : Result = 22
                    Case 26 To 75 : Result = 23
                    Case Else : Result = 24
                End Select
            Case "OC", "C"
                Select Case i
                    Case 0 To 0.5 : Result = 20
                    Case 0.51 To 2.5 : Result = 21
                    Case 2.51 To 49.75 : Result = 22
                    Case 49.76 To 91.5 : Result = 23
                    Case 91.51 To 99.0 : Result = 24
                    Case 99.01 To 99.5 : Result = 25
                    Case Else : Result = 26
                End Select
            Case "OG"
                Select Case i
                    Case 0 To 0.24 : Result = 20
                    Case 0.25 To 1.95 : Result = 21
                    Case 1.96 To 48.78 : Result = 22
                    Case 48.79 To 92.46 : Result = 23
                    Case 92.47 To 99.27 : Result = 24
                    Case 99.28 To 99.51 : Result = 25
                    Case Else : Result = 26
                End Select
            Case "OLB"
                Select Case i
                    Case 0 To 0.2 : Result = 20
                    Case 0.21 To 4.02 : Result = 21
                    Case 4.03 To 53.46 : Result = 22
                    Case 53.47 To 91.77 : Result = 23
                    Case 91.78 To 98.8 : Result = 24
                    Case 98.81 To 99.6 : Result = 25
                    Case Else : Result = 26
                End Select
            Case "OT"
                Select Case i
                    Case 0 To 0.39 : Result = 20
                    Case 0.4 To 2.73 : Result = 21
                    Case 2.74 To 48.05 : Result = 22
                    Case 48.06 To 90.23 : Result = 23
                    Case 90.24 To 98.63 : Result = 24
                    Case 98.64 To 99.61 : Result = 25
                    Case Else : Result = 26
                End Select
            Case "P"
                Select Case i
                    Case 0 To 1.72 : Result = 21
                    Case 1.73 To 32.22 : Result = 22
                    Case 32.23 To 84.0 : Result = 23
                    Case Else : Result = 24
                End Select
            Case "QB"
                Select Case i
                    Case 0 To 3.45 : Result = 21
                    Case 3.46 To 14.59 : Result = 22
                    Case 14.6 To 76.46 : Result = 23
                    Case 76.47 To 98.14 : Result = 24
                    Case 98.15 To 98.94 : Result = 25
                    Case 98.95 To 99.2 : Result = 26
                    Case 99.21 To 99.47 : Result = 27
                    Case 99.48 To 99.73 : Result = 28
                    Case Else : Result = 29
                End Select
            Case "RB"
                Select Case i
                    Case 0 To 10 : Result = 21
                    Case 10.01 To 63.82 : Result = 22
                    Case 63.83 To 94.39 : Result = 23
                    Case 94.4 To 98.95 : Result = 24
                    Case 98.96 To 99.82 : Result = 25
                    Case Else : Result = 27
                End Select
            Case "SS"
                Select Case i
                    Case 0 To 5.14 : Result = 21
                    Case 5.15 To 54.55 : Result = 22
                    Case 54.56 To 93.28 : Result = 23
                    Case 93.29 To 99.21 : Result = 24
                    Case 99.22 To 99.6 : Result = 25
                    Case Else : Result = 27
                End Select
            Case "TE"
                Select Case i
                    Case 0 To 0.25 : Result = 20
                    Case 0.26 To 4.58 : Result = 21
                    Case 4.59 To 51.72 : Result = 22
                    Case 51.73 To 89.57 : Result = 23
                    Case 89.58 To 98.98 : Result = 24
                    Case Else : Result = 25
                End Select
            Case "WR"
                Select Case i
                    Case 0 To 0.12 : Result = 20
                    Case 0.13 To 7.72 : Result = 21
                    Case 7.73 To 62.22 : Result = 22
                    Case 62.23 To 94.39 : Result = 23
                    Case 94.4 To 98.95 : Result = 24
                    Case 98.96 To 99.77 : Result = 25
                    Case Else : Result = 27
                End Select
        End Select

        If Result < 22 Then 'Check to see if this is an underclassman
            If Result = 20 Then
                Underclassman = True
            Else 'we need to determine the percentage chance that a 21 year old is an underclassman by position
                i = MT.GenerateDouble(0, 100)
                Select Case pos
                    Case "QB" : If i <= 48.4967 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "RB" : If i <= 58.83 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "WR" : If i <= 55.1166 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "TE" : If i <= 56.9869 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "OT" : If i <= 68.7546 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "OC" : If i <= 16.84 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "OG" : If i <= 52.0513 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "K" : If i <= 52.23532 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "P" : If i <= 55.4651 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "DE" : If i <= 43.6652 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "DT" : If i <= 32.1615 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "OLB" : If i <= 51.2935 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "ILB" : If i <= 41.4766 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "CB" : If i <= 48.4967 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "FS" : If i <= 52.0578 * (NumUnderclassmen / 91) Then Underclassman = True
                    Case "SS" : If i <= 41.0895 * (NumUnderclassmen / 91) Then Underclassman = True
                End Select
            End If
            If Underclassman Then TotalUCNum += 1
        End If
        Return Result
    End Function

    'Generates the Weight by position
    Private Shared Function GetWeight(ByVal height As Integer, Optional ByVal pos As String = "", Optional ByVal DraftRound As String = "") As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB" : Result = Math.Round(MT.GetGaussian(222.2606383, 11.47152503), 0)
            Case "RB" : Result = Math.Round(MT.GetGaussian(213.6614035, 14.70970716), 0)
            Case "FB" : Result = Math.Round(MT.GetGaussian(245.7697842, 15.19072779), 0)
            Case "WR" : Result = Math.Round(MT.GetGaussian(200.954386, 15.20905149), 0)
            Case "TE" : Result = Math.Round(MT.GetGaussian(254.346056, 10.5141955), 0)
            Case "OT" : Result = Math.Round(MT.GetGaussian(315.5371094, 14.3690044), 0)
            Case "OG" : Result = Math.Round(MT.GetGaussian(314.3625304, 13.33187844), 0)
            Case "C" : Result = Math.Round(MT.GetGaussian(301.67, 12.41619545), 0)
            Case "DE" : Result = Math.Round(MT.GetGaussian(268.3985637, 14.0785003), 0)
            Case "DT" : Result = Math.Round(MT.GetGaussian(305.9577735, 15.10292069), 0)
            Case "NT" : Result = Math.Round(MT.GetGaussian(320.35366, 8.533562), 0)
            Case "LB" : Result = Math.Round(MT.GetGaussian(235, 8.3536366), 0)
            Case "OLB" : Result = Math.Round(MT.GetGaussian(239.8855422, 10.51744511), 0)
            Case "ILB" : Result = Math.Round(MT.GetGaussian(241.4280936, 7.585575329), 0)
            Case "CB" : Result = Math.Round(MT.GetGaussian(192.3109843, 9.28641138), 0)
            Case "DB" : Result = Math.Round(MT.GetGaussian(198.232365, 8.55632656), 0)
            Case "SS" : Result = Math.Round(MT.GetGaussian(210.1620553, 8.886934348), 0)
            Case "FS" : Result = Math.Round(MT.GetGaussian(204.6089965, 9.27152462), 0)
            Case "K" : Result = Math.Round(MT.GetGaussian(199.2978723, 19.28945972), 0)
            Case "P" : Result = Math.Round(MT.GetGaussian(217.1034483, 12.06661286), 0)
            Case Else : Result = Math.Round(MT.GetGaussian(242, 9.291573243), 0)
        End Select
        Return Result
    End Function

    'Generates the Height By Position
    Private Shared Function GetHeight(Optional ByVal pos As String = "", Optional ByVal DraftRound As String = "") As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(75.84, 1.312757911), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(76, 1.414213562), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(75.78409091, 1.813224777), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(74.63636364, 2.203303305), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(74.57142857, 1.468721504), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(76, 1.936491673), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(75, 1.326649916), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(75.0625, 1.318295576), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(75.06976744, 1.579562158), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(74.47368421, 1.606352964), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(74.46147979, 1.528305375), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(74.44927536, 1.450257786), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(73.70478261, 1.450257786), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(72.96028986, 1.450257786), 2)
                End Select

            Case "RB"

                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(71.5, 1.381698559), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(71.8, 1.643167673), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(71.77777778, 1.092906421), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(70.95, 1.503504678), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(71.14893617, 1.999768719), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(71.10416667, 1.89332066), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(70.73333333, 1.973267673), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(70.00297619, 1.73743049), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(70.89814815, 1.686180315), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(70.8559322, 2.038531519), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(70.72235863, 1.987644525), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(70.58878505, 1.936757532), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(69.8828972, 1.936757532), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(69.17700935, 1.936757532), 2)
                End Select

            Case "FB"

                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    'Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(74, 1.414213562), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(72.8, 1.095445115), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(72.6, 1.273205652), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(72.33333333, 1.559914528), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(72.55, 1.35627198), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(72.42307692, 2.212203913), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(72.68931624, 1.812851523), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(72.95555556, 1.413499133), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(72.226, 1.413499133), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(71.49644444, 1.413499133), 2)
                End Select

            Case "WR"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(74.17045455, 1.65668412), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(74.11111111, 2.446819998), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(72.54411765, 2.046787296), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(73.33035714, 1.835924396), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(73.0484375, 2.265112202), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(72.76966292, 1.881513202), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(72.55965909, 2.374431063), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(72.74025974, 2.238435764), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(72.61797753, 2.362133175), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(72.62831858, 2.299824101), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(72.82737067, 2.312280433), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(73.02642276, 2.324736765), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(72.29615854, 2.324736765), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(71.56589431, 2.324736765), 2)
                End Select

            Case "TE"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(75.66666667, 0.577350269), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(77.75, 0.5), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(76.8, 1.473576795), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(76.56666667, 1.194335289), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(76.65853659, 1.389419953), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(76.36111111, 1.570309847), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(76.51111111, 1.324973794), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(76.11627907, 1.276328483), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(76.10169492, 1.373328643), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(76.16009116, 1.366767952), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(76.21848739, 1.360207262), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(75.45630252, 1.360207262), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(74.69411765, 1.360207262), 2)
                End Select

            Case "OT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(78, 0.755928946), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(77.54545455, 1.035725481), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(77.84, 1.106044002), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(77.84210526, 1.067872126), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(77.93877551, 1.214635804), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(77.3125, 1.187814908), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(77.4375, 1.089724736), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(77.625, 1.18417597), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(77.51923077, 1.364686053), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(77.91176471, 1.206144643), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(77.81767097, 1.272366092), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(77.72357724, 1.338587542), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(76.94634146, 1.338587542), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(76.16910569, 1.338587542), 2)
                End Select

            Case "OG"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(77, 0.52323), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(74, 0.532323), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(77.33333333, 0.516397779), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(75.55555556, 0.726483157), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(76.33333333, 1.109400392), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(76.025, 1.165475582), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(75.7826087, 1.227699628), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(75.88461538, 1.231193366), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(76.35135135, 0.919426639), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(76.18367347, 1.30181715), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(76.1582703, 1.306968523), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(76.13286713, 1.312119897), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(75.37153846, 1.312119897), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(74.61020979, 1.312119897), 2)
                End Select

            Case "C"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(76.25, 0.957427108), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(75.66666667, 0.516397779), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(75.72222222, 1.074055292), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(76.17647059, 1.131110854), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(75.18181818, 1.052722712), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(75.36363636, 1.206045378), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(75.14285714, 1.380131119), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(74.65384615, 1.547703013), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(74.91978022, 1.320864854), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(75.18571429, 1.094026695), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(74.43385714, 1.094026695), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(73.682, 1.094026695), 2)
                End Select

            Case "DE"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(76.73076923, 1.213901846), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(76.45454545, 1.213559752), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(75.79310345, 1.264326765), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(76.05434783, 1.296754081), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(76.13181818, 1.491375317), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(75.97115385, 1.429429687), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(75.74776786, 1.564610588), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(75.58333333, 1.484643616), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(75.71428571, 1.317218636), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(75.9234375, 1.394424611), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(75.84721493, 1.381740527), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(75.77099237, 1.369056444), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(75.01328244, 1.369056444), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(74.25557252, 1.369056444), 2)
                End Select

            Case "DT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(75.16666667, 1.169045194), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(75.375, 2.133909892), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(75.54166667, 1.744037461), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(74.91304348, 1.164358718), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(75.59615385, 1.332248805), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(74.82258065, 1.324723213), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(75.05769231, 1.460774191), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(74.79487179, 1.321472209), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(75.01666667, 1.408108079), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(75.06944444, 1.377141241), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(74.98710317, 1.435373329), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(74.9047619, 1.493605417), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(74.15571429, 1.493605417), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(73.40666667, 1.493605417), 2)
                End Select

            Case "NT"
                Select Case MT.GenerateInt32(1, 26)
                    Case 1 To 3 : Result = 72
                    Case 4 To 8 : Result = 73
                    Case 9 To 16 : Result = 74
                    Case 17 To 20 : Result = 75
                    Case 21 To 24 : Result = 76
                    Case 25 : Result = 77
                    Case 26 : Result = 78
                End Select
            Case "LB" : Result = Math.Round(MT.GetGaussian(73.67901235, 1.506877239), 2)

            Case "OLB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(74.5, 0.836660027), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(75.14285714, 2.267786838), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(74.13636364, 1.424127286), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(74.58333333, 1.621353718), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(74.07142857, 1.188691303), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(74.13461538, 1.428407123), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(73.86206897, 1.161491417), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(74.03225806, 1.459482751), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(73.84210526, 1.532848349), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(73.640625, 1.395766701), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(73.7426426, 1.407610174), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(73.84466019, 1.419453648), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(73.10621359, 1.419453648), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(72.36776699, 1.419453648), 2)
                End Select

            Case "ILB"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(74.5, 1.290994449), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(73, 1), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(73.75, 1.035098339), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(73.35483871, 1.2529535), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(73.33333333, 1.330950251), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(73.97058824, 1.193042815), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(73.41176471, 1.208997758), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(73.29166667, 1.39810949), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(73.57575758, 1.146470209), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(73.4846873, 1.213371536), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(73.39361702, 1.280272863), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(72.65968085, 1.280272863), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(71.92574468, 1.280272863), 2)
                End Select

            Case "CB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(72.25, 0.957427108), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(71.69230769, 1.250640861), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(71.62019231, 1.265148593), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(71.69444444, 1.653039528), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(71.48, 1.765740026), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(71.38709677, 1.553623101), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(71.37974684, 1.587642967), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(71.19512195, 1.510776306), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(71.04, 1.501710736), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(70.82352941, 1.473386693), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(70.9628596, 1.63866152), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(71.10218978, 1.803936348), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(70.39116788, 1.803936348), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(69.68014599, 1.803936348), 2)
                End Select

            Case "DB" : Result = Math.Round(MT.GetGaussian(71.79176683, 1.683366198), 2)
            Case "SS"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(72.2,   #REF!	), 2)
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(72.2, 1.303840481), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(71.66666667, 1.366260102), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(72.28571429, 1.603567451), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(72.53846154, 1.794007118), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(72.85, 1.755442664), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(72.19230769, 1.575289961), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(72.31034483, 1.62795771), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(72.36, 1.439907404), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(72.75, 1.662614121), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(72.49143836, 1.521116555), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(72.23287671, 1.379618989), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(71.51054795, 1.379618989), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(70.78821918, 1.379618989), 2)
                End Select

            Case "FS"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(73.5, 0.707106781), 2)
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(73, 0.70323265), 2)
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(72.6, 0.894427191), 2)
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(72.16666667, 1.329160136), 2)
                    Case "R2" : Result = Math.Round(MT.GetGaussian(72.83333333, 1.288766674), 2)
                    Case "R3" : Result = Math.Round(MT.GetGaussian(72.88461538, 1.243444348), 2)
                    Case "R4" : Result = Math.Round(MT.GetGaussian(72.5, 1.390119575), 2)
                    Case "R5" : Result = Math.Round(MT.GetGaussian(73.21875, 1.4532361), 2)
                    Case "R6" : Result = Math.Round(MT.GetGaussian(72.37209302, 1.327784689), 2)
                    Case "R7" : Result = Math.Round(MT.GetGaussian(73.07407407, 1.465656218), 2)
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(72.61203704, 1.536695344), 2)
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(72.15, 1.60773447), 2)
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(71.4285, 1.60773447), 2)
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(70.707, 1.60773447), 2)
                End Select

            Case "K" : Result = Math.Round(MT.GetGaussian(72.04255319, 2.136203935), 2)

            Case "P" : Result = Math.Round(MT.GetGaussian(74.42372881, 1.714073489), 2)

            Case "LS" : Result = Math.Round(MT.GetGaussian(74.42857143, 0.975900073), 2)

            Case Else
                Result = MT.GetGaussian(70, 3)
        End Select
        Return Result
    End Function

    ''' <summary>
    ''' We are going to use this to lookup values in the number Array and return values in the lookupArray.  For instance if we have the following arrays:
    ''' numArray = New {10, 20, 30, 40, 100} and lookupArray = New {"QB", "RB", "WR", "TE", "CB"}
    ''' A number is generated and checked against the index values of the number array---these are the BOUNDARY numbers, Ie, the first index means 0-9.99999999.  10 is the start of the next index.  So we then check
    ''' if the value is less thn each index.  As soon as it is, we have the boundary value.  We take this index and then pull the corresponding value from the lookupArray index.  For Example:
    ''' Random # is generated of 28.75, which is between 20 and 30.  It looks through the supplied indexes, and locates the first one where the value is less than the index value.  In this case it would be index 2,
    ''' with a value of 30---then it looks up the corresponding value in the supplied lookup index to return the proper position---in this case "WR".  
    ''' </summary>
    ''' <param name="numArray"></param>
    ''' <param name="lookupArray"></param>
    ''' <param name="minVal"></param>
    ''' <param name="maxval"></param>
    ''' <returns></returns>
    Public Shared Function CaseLookup(numArray As Array, lookupArray As Array, minVal As Double, maxval As Double) As String
        Dim result As String = ""
        Dim GetNum As Double = MT.GenerateDouble(minVal, maxval)

        For i = 0 To numArray.Length - 1
            If GetNum < numArray(i) Then result = lookupArray(i)
        Next i
        Return result
    End Function

    ''' <summary>
    ''' An equation exists to get predicted HandLength in CM when you know a male's height:
    ''' avg HL(cm)=(Height(cm)-80.4+(.195 x age in years) - 6.383)/5.122
    ''' STDev was 4.4cm of their height and since average height is roughly around 9.08 times your hand length, I took roughly 4.4/9, and reduced
    ''' it slightly because NFL players seemingly have larger hands for their bodies
    ''' </summary>
    ''' <param name="height"></param>
    ''' <param name="age"></param>
    ''' <returns></returns>
    Private Shared Function GetHandLength(height As Integer, age As Integer) As Double
        Dim result As Double

        Select Case height
            Case 65 : result = Math.Round(MT.GetGaussian(8.375, 0.176776695), 2)
            Case 66 : result = Math.Round(MT.GetGaussian(9.2825, 0.59200929), 2)
            Case 67 : result = Math.Round(MT.GetGaussian(8.970666667, 0.409672634), 2)
            Case 68 : result = Math.Round(MT.GetGaussian(9.12297619, 0.46257901), 2)
            Case 69 : result = Math.Round(MT.GetGaussian(9.165509259, 0.516239973), 2)
            Case 70 : result = Math.Round(MT.GetGaussian(9.15125, 0.493473421), 2)
            Case 71 : result = Math.Round(MT.GetGaussian(9.263981818, 0.52647244), 2)
            Case 72 : result = Math.Round(MT.GetGaussian(9.275428016, 0.498954974), 2)
            Case 73 : result = Math.Round(MT.GetGaussian(9.470688623, 0.487644619), 2)
            Case 74 : result = Math.Round(MT.GetGaussian(9.615797267, 0.512734345), 2)
            Case 75 : result = Math.Round(MT.GetGaussian(9.722888889, 0.526993773), 2)
            Case 76 : result = Math.Round(MT.GetGaussian(9.782690632, 0.483142692), 2)
            Case 77 : result = Math.Round(MT.GetGaussian(9.939315789, 0.50753913), 2)
            Case 78 : result = Math.Round(MT.GetGaussian(9.939803922, 0.507186736), 2)
            Case 79 : result = Math.Round(MT.GetGaussian(10.11408602, 0.577721323), 2)
            Case 80 : result = Math.Round(MT.GetGaussian(10.1604, 0.521540027), 2)
            Case 81 : result = Math.Round(MT.GetGaussian(9.25, 0.493235212), 2)
            Case 82 : result = Math.Round(MT.GetGaussian(10.5, 0.4821232323), 2)
        End Select

        Return result
    End Function

    ''' <summary>
    ''' Arm length based on combine data by height
    ''' </summary>
    ''' <param name="height"></param>
    ''' <returns></returns>
    Private Shared Function GetArmLength(height As Integer) As Double

        Dim result As Double

        Select Case height
            Case 65 : result = MT.GetGaussian(28.88, 0.710642315)
            Case 66 : result = MT.GetGaussian(29.22, 1.091515575)
            Case 67 : result = MT.GetGaussian(29.62, 0.775336025)
            Case 68 : result = MT.GetGaussian(29.82, 0.944693874)
            Case 69 : result = MT.GetGaussian(30.51, 0.888752285)
            Case 70 : result = MT.GetGaussian(30.93, 0.912302455)
            Case 71 : result = MT.GetGaussian(31.3, 0.987515643)
            Case 72 : result = MT.GetGaussian(31.52, 0.938939694)
            Case 73 : result = MT.GetGaussian(31.97, 0.984615512)
            Case 74 : result = MT.GetGaussian(32.47, 0.963542906)
            Case 75 : result = MT.GetGaussian(32.83, 1.018174135)
            Case 76 : result = MT.GetGaussian(33.11, 1.04347593)
            Case 77 : result = MT.GetGaussian(33.65, 1.074612657)
            Case 78 : result = MT.GetGaussian(33.85, 1.051983346)
            Case 79 : result = MT.GetGaussian(34.17, 1.091649255)
            Case 80 : result = MT.GetGaussian(34.51, 1.524794713)
            Case 81 : result = MT.GetGaussian(33.5, 0.886566566)
            Case 82 : result = MT.GetGaussian(33.38, 0.902365653)
        End Select

        Return Math.Round(result, 2)
    End Function

    ''' <summary>
    '''Introversion/Extraversion	        Low Anxiety/High Anxiety	         Receptivity/Tough-Mindedness	         Accommodation/Independence	                Lack of Restraint/Self-Control
    '''A: Reserved/Warm	                    C: Emotionally Stable/Reactive	     A: Warm/Reserved	                     E: Deferential/Dominant	                F: Serious/Lively	                         B: Problem-Solving
    '''F: Serious/Lively	                L: Trusting/Vigilant	             I: Sensitive/Unsentimental	             H: Shy/Bold	                            G: Expedient/Rule-Conscious
    '''H: Shy/Bold	                        O: Self-Assured/Apprehensive	     M: Abstracted/Practical	             L: Trusting/Vigilant	                    M: Abstracted/Practical
    '''N:  Private/Forthright	            Q4: Relaxed/Tense	                 Q1: Open-to-Change/Traditional	         Q1: Traditional/Open-to-Change	            Q3: Tolerates Disorder/Perfectionistic
    '''Q2: Self-Reliant/Group-Oriented
    ''' </summary>

    Public Sub PersonalityModel(ByVal DT As DataTable, ByVal num As Integer, ByVal classInstance As Object)
        Dim Result As Integer
        Dim Warmth As New List(Of String)
        Dim Reasoning As New List(Of String)
        Dim EmotionalStability As New List(Of String)
        Dim Dominance As New List(Of String)
        Dim Liveliness As New List(Of String)
        Dim RuleConscious As New List(Of String)
        Dim SocialBoldness As New List(Of String)
        Dim Sensitivity As New List(Of String)
        Dim Vigilance As New List(Of String)
        Dim Abstractedness As New List(Of String)
        Dim Privateness As New List(Of String)
        Dim Apprehension As New List(Of String)
        Dim Honesty As New List(Of String)
        Dim Attractive As New List(Of String)
        Dim OpennessToChange As New List(Of String)
        Dim SelfReliance As New List(Of String)
        Dim Perfectionism As New List(Of String)
        Dim Tension As New List(Of String)

        Dim PositiveTraits As New List(Of String)
        Dim NegativeTraits As New List(Of String)
        Dim BalancedTraits As New List(Of String)

        '####Factor B: Problem Solving---this is unrelated to any one category
        Reasoning.Add("AbstractReasoning")
        Reasoning.Add("Intelligent")
        Reasoning.Add("MentalCapacity")

        '####Factor A---how friendly a person is
        Warmth.Add("Nurturing")
        Warmth.Add("Friendly")
        Warmth.Add("Participator")
        Warmth.Add("Caring")
        '####Factor C---how stable a person is
        EmotionalStability.Add("EmotionallyStable")
        EmotionalStability.Add("Conforming")
        EmotionalStability.Add("Mature")
        EmotionalStability.Add("CalmUnderPressure")
        '####Factor E---dominance level(Alpha Dog)
        Dominance.Add("Stubborn")
        Dominance.Add("Aggressive")
        Dominance.Add("Competitive")
        Dominance.Add("Bossy")
        '####Factor F---energy level of the person
        Liveliness.Add("Enthusiastic")
        Liveliness.Add("Impulsive")
        Liveliness.Add("FunLoving")
        Liveliness.Add("Expressive")
        '####Factor G---how rule-following they are
        RuleConscious.Add("Dutiful")
        RuleConscious.Add("TeamPlayer")
        RuleConscious.Add("FollowsRules")
        RuleConscious.Add("Moralistic")
        '####Factor H---how likely they are to be bold in a social setting
        SocialBoldness.Add("SociallyBold")
        SocialBoldness.Add("ThickSkinned")
        SocialBoldness.Add("Adventurous")
        SocialBoldness.Add("Uninhibited")
        '####Factor I---how sensitive the person is
        Sensitivity.Add("Refined")
        Sensitivity.Add("Intuitive")
        Sensitivity.Add("Sentimental")
        Sensitivity.Add("Sensitive")
        '####Factor L---how trustful a person is
        Vigilance.Add("Suspicious")
        Vigilance.Add("Oppositional")
        Vigilance.Add("Distrustful")
        Vigilance.Add("Vigilant")
        '####Factor M---how a person thinks
        Abstractedness.Add("Impractical")
        Abstractedness.Add("Imaginative")
        Abstractedness.Add("AbsentMinded")
        Abstractedness.Add("AbstractThinker")
        '####Factor N---how private a person is
        Privateness.Add("Private")
        Privateness.Add("Astute")
        Privateness.Add("Diplomatic")
        Privateness.Add("Polished")
        '####Factor O---how apprehensive a person is
        Apprehension.Add("Fearful")
        Apprehension.Add("SelfDoubting")
        Apprehension.Add("GuiltProne")
        Apprehension.Add("Insecure")
        '####Factor Q1---How open they are to change
        OpennessToChange.Add("Adaptable")
        OpennessToChange.Add("Expermiental")
        OpennessToChange.Add("Analytical")
        OpennessToChange.Add("Critical")
        '####Factor Q2---how self-reliant they are
        SelfReliance.Add("SelfSufficient")
        SelfReliance.Add("Resourceful")
        SelfReliance.Add("Individualistic")
        SelfReliance.Add("Loner")
        '####Factor Q3---how things must be
        Perfectionism.Add("Perfectionist")
        Perfectionism.Add("StrongWilled")
        Perfectionism.Add("Organized")
        Perfectionism.Add("Controlling")
        '####Factor Q4---How high strung a person is
        Tension.Add("HighEnergy")
        Tension.Add("Impatient")
        Tension.Add("Driven")
        Tension.Add("Tense")
        'Factor 6---How Honest a person is
        Honesty.Add("Honesty")
        Honesty.Add("Fairness")
        Honesty.Add("GreedAvoidance")
        Honesty.Add("Modesty")

        BalancedTraits.Add("Warmth")
        BalancedTraits.Add("EmotionalStability")
        BalancedTraits.Add("Dominance")
        BalancedTraits.Add("Liveliness")
        BalancedTraits.Add("RuleConcious")
        BalancedTraits.Add("SocialBoldness")
        BalancedTraits.Add("Sensitivity")
        BalancedTraits.Add("Vigilance")
        BalancedTraits.Add("Abstractedness")
        BalancedTraits.Add("Privateness")
        BalancedTraits.Add("Apprehension")
        BalancedTraits.Add("OpennessToChange")
        BalancedTraits.Add("SelfReliance")
        BalancedTraits.Add("Perfectionism")
        BalancedTraits.Add("Tension")
        BalancedTraits.Add("Honesty")
        Try
            For i As Integer = 1 To 4 'randomly select 4 positive traits
                Result = MT.GenerateInt32(0, BalancedTraits.Count - 1)
                PositiveTraits.Add(BalancedTraits.Item(Result))
                BalancedTraits.Remove(BalancedTraits.Item(Result))
            Next i

            For i As Integer = 1 To 4 'randomly select 4 negative traits---the remainder stay balanced
                Result = MT.GenerateInt32(0, BalancedTraits.Count - 1)
                NegativeTraits.Add(BalancedTraits.Item(Result))
                BalancedTraits.Remove(BalancedTraits.Item(Result))
            Next i

            For i As Integer = 0 To PositiveTraits.Count - 1
                GetTrait(PositiveTraits.Item(i), "Positive")
            Next i

            For i As Integer = 0 To NegativeTraits.Count - 1
                GetTrait(NegativeTraits.Item(i), "Negative")
            Next i

            For i As Integer = 0 To BalancedTraits.Count - 1
                GetTrait(BalancedTraits.Item(i), "Balanced")
            Next i

            GetTrait("Reasoning", "Gaussian")
        Catch ex As TypeInitializationException
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.Data)
        End Try
        GetPersonalityStats(DT, num)
    End Sub

    ''' <summary>
    ''' This sets their traits---4 of the 16 groups will be positive traits, 4 of the 16 groups will be negative traits and the other 8 groups will be balanced, reasoning is its own group and has no positive or negative association
    ''' </summary>
    ''' <param name="trait"></param>
    ''' <param name="traitStrength"></param>
    Private Sub GetTrait(ByVal trait As String, ByVal traitStrength As String)
        Dim DTotal(2) As Single
        Dim WTotal(2) As Single
        Select Case trait
            Case "Warmth" 'Caring, Nurturing, Friendly, Participator
                Select Case traitStrength
                    Case "Positive"
                        Caring = MT.GetGaussian(82.5, 11.6667)
                        Nurturing = MT.GetGaussian(82.5, 11.6667)
                        Friendly = MT.GetGaussian(82.5, 11.6667)
                        Participator = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Caring + Nurturing + Friendly + Participator) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Friendly'"
                        End If

                    Case "Negative"
                        Caring = MT.GetGaussian(17.5, 11.6667)
                        Nurturing = MT.GetGaussian(17.5, 11.6667)
                        Friendly = MT.GetGaussian(17.5, 11.6667)
                        Participator = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Caring + Nurturing + Friendly + Participator) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Friendly'"
                        End If
                    Case "Balanced"
                        Caring = MT.GetGaussian(50, 4.6667)
                        Nurturing = MT.GetGaussian(50, 4.6667)
                        Friendly = MT.GetGaussian(50, 4.6667)
                        Participator = MT.GetGaussian(50, 4.6667)
                End Select
            Case "EmotionalStability"
                Select Case traitStrength
                    Case "Positive"
                        EmotionallyStable = MT.GetGaussian(82.5, 11.6667)
                        Conforming = MT.GetGaussian(82.5, 11.6667)
                        Mature = MT.GetGaussian(82.5, 11.6667)
                        CalmUnderPressure = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (EmotionallyStable + Conforming + Mature + CalmUnderPressure) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Emotionally Stable'"
                        End If

                    Case "Negative"
                        EmotionallyStable = MT.GetGaussian(17.5, 11.6667)
                        Conforming = MT.GetGaussian(17.5, 11.6667)
                        Mature = MT.GetGaussian(17.5, 11.6667)
                        CalmUnderPressure = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (EmotionallyStable + Conforming + Mature + CalmUnderPressure) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Emotionally Stable'"
                        End If
                    Case "Balanced"
                        EmotionallyStable = MT.GetGaussian(50, 4.6667)
                        Conforming = MT.GetGaussian(50, 4.6667)
                        Mature = MT.GetGaussian(50, 4.6667)
                        CalmUnderPressure = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Dominance"
                Select Case traitStrength
                    Case "Positive"
                        Stubborn = MT.GetGaussian(82.5, 11.6667)
                        Aggressive = MT.GetGaussian(82.5, 11.6667)
                        Competitive = MT.GetGaussian(82.5, 11.6667)
                        Bossy = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Stubborn + Aggressive + Competitive + Bossy) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Dominant'"
                        End If
                    Case "Negative"
                        Stubborn = MT.GetGaussian(17.5, 11.6667)
                        Aggressive = MT.GetGaussian(17.5, 11.6667)
                        Competitive = MT.GetGaussian(17.5, 11.6667)
                        Bossy = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Stubborn + Aggressive + Competitive + Bossy) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Dominant'"
                        End If
                    Case "Balanced"
                        Stubborn = MT.GetGaussian(50, 4.6667)
                        Aggressive = MT.GetGaussian(50, 4.6667)
                        Competitive = MT.GetGaussian(50, 4.6667)
                        Bossy = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Liveliness"
                Select Case traitStrength
                    Case "Positive"
                        Enthusiastic = MT.GetGaussian(82.5, 11.6667)
                        Impulsive = MT.GetGaussian(82.5, 11.6667)
                        FunLoving = MT.GetGaussian(82.5, 11.6667)
                        Expressive = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Enthusiastic + Impulsive + FunLoving + Expressive) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Fun Loving'"
                        End If
                    Case "Negative"
                        Enthusiastic = MT.GetGaussian(17.5, 11.6667)
                        Impulsive = MT.GetGaussian(17.5, 11.6667)
                        FunLoving = MT.GetGaussian(17.5, 11.6667)
                        Expressive = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Enthusiastic + Impulsive + FunLoving + Expressive) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Fun Loving'"
                        End If
                    Case "Balanced"
                        Enthusiastic = MT.GetGaussian(50, 4.6667)
                        Impulsive = MT.GetGaussian(50, 4.6667)
                        FunLoving = MT.GetGaussian(50, 4.6667)
                        Expressive = MT.GetGaussian(50, 4.6667)
                End Select
            Case "RuleConcious"
                Select Case traitStrength
                    Case "Positive"
                        Dutiful = MT.GetGaussian(82.5, 11.6667)
                        TeamPlayer = MT.GetGaussian(82.5, 11.6667)
                        FollowsRules = MT.GetGaussian(82.5, 11.6667)
                        Moralistic = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Dutiful + TeamPlayer + FollowsRules + Moralistic) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Rule Follower'"
                        End If
                    Case "Negative"
                        Dutiful = MT.GetGaussian(17.5, 11.6667)
                        TeamPlayer = MT.GetGaussian(17.5, 11.6667)
                        FollowsRules = MT.GetGaussian(17.5, 11.6667)
                        Moralistic = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Dutiful + TeamPlayer + FollowsRules + Moralistic) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Rule Follower'"
                        End If
                    Case "Balanced"
                        Dutiful = MT.GetGaussian(50, 4.6667)
                        TeamPlayer = MT.GetGaussian(50, 4.6667)
                        FollowsRules = MT.GetGaussian(50, 4.6667)
                        Moralistic = MT.GetGaussian(50, 4.6667)
                End Select
            Case "SocialBoldness"
                Select Case traitStrength
                    Case "Positive"
                        SociallyBold = MT.GetGaussian(82.5, 11.6667)
                        ThickSkinned = MT.GetGaussian(82.5, 11.6667)
                        Adventurous = MT.GetGaussian(82.5, 11.6667)
                        Uninhibited = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (SociallyBold + ThickSkinned + Adventurous + Uninhibited) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Socially Bold'"
                        End If
                    Case "Negative"
                        SociallyBold = MT.GetGaussian(17.5, 11.6667)
                        ThickSkinned = MT.GetGaussian(17.5, 11.6667)
                        Adventurous = MT.GetGaussian(17.5, 11.6667)
                        Uninhibited = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (SociallyBold + ThickSkinned + Adventurous + Uninhibited) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Socially Bold'"
                        End If
                    Case "Balanced"
                        SociallyBold = MT.GetGaussian(50, 4.6667)
                        ThickSkinned = MT.GetGaussian(50, 4.6667)
                        Adventurous = MT.GetGaussian(50, 4.6667)
                        Uninhibited = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Sensitivity"
                Select Case traitStrength
                    Case "Positive"
                        Refined = MT.GetGaussian(82.5, 11.6667)
                        Intuitive = MT.GetGaussian(82.5, 11.6667)
                        Sentimental = MT.GetGaussian(82.5, 11.6667)
                        Sensitive = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Refined + Intuitive + Sentimental + Sensitive) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Sensitive'"
                        End If
                    Case "Negative"
                        Refined = MT.GetGaussian(17.5, 11.6667)
                        Intuitive = MT.GetGaussian(17.5, 11.6667)
                        Sentimental = MT.GetGaussian(17.5, 11.6667)
                        Sensitive = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Refined + Intuitive + Sentimental + Sensitive) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Sensitive'"
                        End If
                    Case "Balanced"
                        Refined = MT.GetGaussian(50, 4.6667)
                        Intuitive = MT.GetGaussian(50, 4.6667)
                        Sentimental = MT.GetGaussian(50, 4.6667)
                        Sensitive = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Vigilance"
                Select Case traitStrength
                    Case "Positive"
                        Suspicious = MT.GetGaussian(82.5, 11.6667)
                        Oppositional = MT.GetGaussian(82.5, 11.6667)
                        Distrustful = MT.GetGaussian(82.5, 11.6667)
                        Vigilant = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Suspicious + Oppositional + Distrustful + Vigilant) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Distrustful'"
                        End If
                    Case "Negative"
                        Suspicious = MT.GetGaussian(17.5, 11.6667)
                        Oppositional = MT.GetGaussian(17.5, 11.6667)
                        Distrustful = MT.GetGaussian(17.5, 11.6667)
                        Vigilant = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Suspicious + Oppositional + Distrustful + Vigilant) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Distrustful'"
                        End If
                    Case "Balanced"
                        Suspicious = MT.GetGaussian(50, 4.6667)
                        Oppositional = MT.GetGaussian(50, 4.6667)
                        Distrustful = MT.GetGaussian(50, 4.6667)
                        Vigilant = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Abstractedness"
                Select Case traitStrength
                    Case "Positive"
                        Impractical = MT.GetGaussian(82.5, 11.6667)
                        Imaginative = MT.GetGaussian(82.5, 11.6667)
                        AbsentMinded = MT.GetGaussian(82.5, 11.6667)
                        AbstractThinker = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Impractical + Imaginative + AbsentMinded + AbstractThinker) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Abstract'"
                        End If
                    Case "Negative"
                        Impractical = MT.GetGaussian(17.5, 11.6667)
                        Imaginative = MT.GetGaussian(17.5, 11.6667)
                        AbsentMinded = MT.GetGaussian(17.5, 11.6667)
                        AbstractThinker = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Impractical + Imaginative + AbsentMinded + AbstractThinker) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Abstract'"
                        End If
                    Case "Balanced"
                        Impractical = MT.GetGaussian(50, 4.6667)
                        Imaginative = MT.GetGaussian(50, 4.6667)
                        AbsentMinded = MT.GetGaussian(50, 4.6667)
                        AbstractThinker = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Privateness"
                Select Case traitStrength
                    Case "Positive"
                        Privateness = MT.GetGaussian(82.5, 11.6667)
                        Astute = MT.GetGaussian(82.5, 11.6667)
                        Diplomatic = MT.GetGaussian(82.5, 11.6667)
                        Polished = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Privateness + Astute + Diplomatic + Polished) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Private'"
                        End If
                    Case "Negative"
                        Privateness = MT.GetGaussian(17.5, 11.6667)
                        Astute = MT.GetGaussian(17.5, 11.6667)
                        Diplomatic = MT.GetGaussian(17.5, 11.6667)
                        Polished = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Privateness + Astute + Diplomatic + Polished) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Private'"
                        End If
                    Case "Balanced"
                        Privateness = MT.GetGaussian(50, 4.6667)
                        Astute = MT.GetGaussian(50, 4.6667)
                        Diplomatic = MT.GetGaussian(50, 4.6667)
                        Polished = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Apprehension"
                Select Case traitStrength
                    Case "Positive"
                        Fearful = MT.GetGaussian(82.5, 11.6667)
                        SelfDoubting = MT.GetGaussian(82.5, 11.6667)
                        GuiltProne = MT.GetGaussian(82.5, 11.6667)
                        Insecure = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Fearful + SelfDoubting + GuiltProne + Insecure) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Apprehensive'"
                        End If
                    Case "Negative"
                        Fearful = MT.GetGaussian(17.5, 11.6667)
                        SelfDoubting = MT.GetGaussian(17.5, 11.6667)
                        GuiltProne = MT.GetGaussian(17.5, 11.6667)
                        Insecure = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Fearful + SelfDoubting + GuiltProne + Insecure) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Apprehensive'"
                        End If
                    Case "Balanced"
                        Fearful = MT.GetGaussian(50, 4.6667)
                        SelfDoubting = MT.GetGaussian(50, 4.6667)
                        GuiltProne = MT.GetGaussian(50, 4.6667)
                        Insecure = MT.GetGaussian(50, 4.6667)
                End Select
            Case "OpennessToChange"
                Select Case traitStrength
                    Case "Positive"
                        Adaptable = MT.GetGaussian(82.5, 11.6667)
                        Experimental = MT.GetGaussian(82.5, 11.6667)
                        Analytical = MT.GetGaussian(82.5, 11.6667)
                        Critical = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Adaptable + Experimental + Analytical + Critical) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Open To Change'"
                        End If
                    Case "Negative"
                        Adaptable = MT.GetGaussian(17.5, 11.6667)
                        Experimental = MT.GetGaussian(17.5, 11.6667)
                        Analytical = MT.GetGaussian(17.5, 11.6667)
                        Critical = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Adaptable + Experimental + Analytical + Critical) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Open To Change'"
                        End If
                    Case "Balanced"
                        Adaptable = MT.GetGaussian(50, 4.6667)
                        Experimental = MT.GetGaussian(50, 4.6667)
                        Analytical = MT.GetGaussian(50, 4.6667)
                        Critical = MT.GetGaussian(50, 4.6667)
                End Select
            Case "SelfReliance"
                Select Case traitStrength
                    Case "Positive"
                        SelfSufficient = MT.GetGaussian(82.5, 11.6667)
                        Resourceful = MT.GetGaussian(82.5, 11.6667)
                        Individualistic = MT.GetGaussian(82.5, 11.6667)
                        Loner = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (SelfSufficient + Resourceful + Individualistic + Loner) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Self Reliant'"
                        End If
                    Case "Negative"
                        SelfSufficient = MT.GetGaussian(17.5, 11.6667)
                        Resourceful = MT.GetGaussian(17.5, 11.6667)
                        Individualistic = MT.GetGaussian(17.5, 11.6667)
                        Loner = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (SelfSufficient + Resourceful + Individualistic + Loner) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Self Reliant'"
                        End If
                    Case "Balanced"
                        SelfSufficient = MT.GetGaussian(50, 4.6667)
                        Resourceful = MT.GetGaussian(50, 4.6667)
                        Individualistic = MT.GetGaussian(50, 4.6667)
                        Loner = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Perfectionism"
                Select Case traitStrength
                    Case "Positive"
                        Perfectionist = MT.GetGaussian(82.5, 11.6667)
                        StrongWilled = MT.GetGaussian(82.5, 11.6667)
                        Organized = MT.GetGaussian(82.5, 11.6667)
                        Controlling = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Perfectionist + StrongWilled + Organized + Controlling) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Perfectionist'"
                        End If
                    Case "Negative"
                        Perfectionist = MT.GetGaussian(17.5, 11.6667)
                        StrongWilled = MT.GetGaussian(17.5, 11.6667)
                        Organized = MT.GetGaussian(17.5, 11.6667)
                        Controlling = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Perfectionist + StrongWilled + Organized + Controlling) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Perfectionist'"
                        End If
                    Case "Balanced"
                        Perfectionist = MT.GetGaussian(50, 4.6667)
                        StrongWilled = MT.GetGaussian(50, 4.6667)
                        Organized = MT.GetGaussian(50, 4.6667)
                        Controlling = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Tension"
                Select Case traitStrength
                    Case "Positive"
                        HighEnergy = MT.GetGaussian(82.5, 11.6667)
                        Impatient = MT.GetGaussian(82.5, 11.6667)
                        Driven = MT.GetGaussian(82.5, 11.6667)
                        Tense = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (HighEnergy + Impatient + Driven + Tense) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Tense'"
                        End If
                    Case "Negative"
                        HighEnergy = MT.GetGaussian(17.5, 11.6667)
                        Impatient = MT.GetGaussian(17.5, 11.6667)
                        Driven = MT.GetGaussian(17.5, 11.6667)
                        Tense = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (HighEnergy + Impatient + Driven + Tense) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Tense'"
                        End If
                    Case "Balanced"
                        HighEnergy = MT.GetGaussian(50, 4.6667)
                        Impatient = MT.GetGaussian(50, 4.6667)
                        Driven = MT.GetGaussian(50, 4.6667)
                        Tense = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Honesty"
                Select Case traitStrength
                    Case "Positive"
                        Honesty = MT.GetGaussian(82.5, 11.6667)
                        Fairness = MT.GetGaussian(82.5, 11.6667)
                        GreedAvoidance = MT.GetGaussian(82.5, 11.6667)
                        Modesty = MT.GetGaussian(82.5, 11.6667)
                        DTotal(2) = (Honesty + Fairness + GreedAvoidance + Modesty) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Honest'"
                        End If
                    Case "Negative"
                        Honesty = MT.GetGaussian(17.5, 11.6667)
                        Fairness = MT.GetGaussian(17.5, 11.6667)
                        GreedAvoidance = MT.GetGaussian(17.5, 11.6667)
                        Modesty = MT.GetGaussian(17.5, 11.6667)
                        WTotal(2) = (Honesty + Fairness + GreedAvoidance + Modesty) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Tense'"
                        End If
                    Case "Balanced"
                        Honesty = MT.GetGaussian(50, 4.6667)
                        Fairness = MT.GetGaussian(50, 4.6667)
                        GreedAvoidance = MT.GetGaussian(50, 4.6667)
                        Modesty = MT.GetGaussian(50, 4.6667)
                End Select
            Case "Reasoning"
                Intelligent = MT.GetGaussian(49.5, 16.5)
                MentalCapacity = MT.GetGaussian(49.5, 16.5)
                AbstractReasoning = MT.GetGaussian(49.5, 16.5)
        End Select

    End Sub

End Class