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
    Public Shared Sub GenNames(ByRef dtOutputTo As DataTable, ByVal row As Integer, ByVal personType As String, Optional ByVal position As String = "")

        Dim MyCollege As New KeyValuePair(Of String, String)
        MyCollege = GetCollege(Colleges, dtOutputTo)

        Try
            dtOutputTo.Rows(row).Item("FName") = String.Format("'{0}'", GetItem(FirstNames, dtOutputTo)) 'adds the necessary ' ' modifier to strings for SQLite
            dtOutputTo.Rows(row).Item("LName") = String.Format("'{0}'", GetItem(LastNames, dtOutputTo))
            dtOutputTo.Rows(row).Item("College") = String.Format("'{0}'", MyCollege.Key)
            dtOutputTo.Rows(row).Item("Age") = GenAge(personType, position)
            dtOutputTo.Rows(row).Item("DOB") = String.Format("'{0}", GetDOB(dtOutputTo.Rows(row).Item("Age")))
            dtOutputTo.Rows(row).Item("Height") = GetHeight(position)
            dtOutputTo.Rows(row).Item("Weight") = GetWeight(dtOutputTo.Rows(row).Item("Height"), position)
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
            Case "C"
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
        Dim i As Integer = MT.GenerateDouble(0, 100)
        Select Case pos
            Case "CB"
                Select Case i
                    Case 0 To 0.39 : Result = 20
                    Case 0.4 To 8.32 : Result = 21
                    Case 8.33 To 49.71 : Result = 22
                    Case 49.72 To 90.14 : Result = 23
                    Case 90.15 To 99.03 : Result = 24
                    Case 99.04 To 100.0 : Result = 25
                End Select
            Case "DE"
                Select Case i
                    Case 0 To 0.27 : Result = 20
                    Case 0.28 To 9.81 : Result = 21
                    Case 9.82 To 43.24 : Result = 22
                    Case 43.25 To 85.15 : Result = 23
                    Case 85.16 To 97.35 : Result = 24
                    Case 97.36 To 99.47 : Result = 25
                    Case 99.48 To 100.0 : Result = 26
                End Select
            Case "DT"
                Select Case i
                    Case 0 To 1.11 : Result = 20
                    Case 1.12 To 11.11 : Result = 21
                    Case 11.12 To 43.33 : Result = 22
                    Case 43.34 To 82.78 : Result = 23
                    Case 82.79 To 96.11 : Result = 24
                    Case 96.12 To 98.61 : Result = 25
                    Case 98.62 To 99.72 : Result = 26
                    Case 99.73 To 100.0 : Result = 27
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
                    Case 0 To 9.09 : Result = 21
                    Case 9.1 To 50.8 : Result = 22
                    Case 50.81 To 90.91 : Result = 23
                    Case 90.92 To 99.47 : Result = 24
                    Case 99.48 To 100.0 : Result = 25
                End Select

            Case "ILB"
                Select Case i
                    Case 0 To 8.25 : Result = 21
                    Case 8.26 To 45.36 : Result = 22
                    Case 45.37 To 90.72 : Result = 23
                    Case 90.73 To 98.45 : Result = 24
                    Case 98.46 To 99.48 : Result = 25
                    Case 99.49 To 100.0 : Result = 26
                End Select
            Case "K"
                Select Case i
                    Case 0 To 3.03 : Result = 21
                    Case 3.04 To 48.48 : Result = 22
                    Case 48.49 To 75.76 : Result = 23
                    Case 75.77 To 100.0 : Result = 24
                End Select
            Case "LS"
                Select Case i
                    Case 0 To 25 : Result = 22
                    Case 26 To 75 : Result = 23
                    Case Else : Result = 24
                End Select
            Case "OC"
                Select Case i
                    Case 0 To 0.87 : Result = 20
                    Case 0.88 To 4.35 : Result = 21
                    Case 4.36 To 31.3 : Result = 22
                    Case 31.31 To 85.22 : Result = 23
                    Case 85.23 To 98.26 : Result = 24
                    Case 98.27 To 99.13 : Result = 25
                    Case 99.14 To 100.0 : Result = 26
                End Select
            Case "OG"
                Select Case i
                    Case 0 To 0.43 : Result = 20
                    Case 0.44 To 3.42 : Result = 21
                    Case 3.43 To 28.63 : Result = 22
                    Case 28.64 To 86.75 : Result = 23
                    Case 86.76 To 98.72 : Result = 24
                    Case 98.73 To 99.15 : Result = 25
                    Case 99.16 To 100.0 : Result = 26
                End Select
            Case "OLB"
                Select Case i
                    Case 0 To 0.28 : Result = 20
                    Case 0.29 To 5.57 : Result = 21
                    Case 5.58 To 45.13 : Result = 22
                    Case 45.14 To 88.58 : Result = 23
                    Case 88.59 To 98.33 : Result = 24
                    Case 98.34 To 99.44 : Result = 25
                    Case 99.45 To 100.0 : Result = 26
                End Select
            Case "OT"
                Select Case i
                    Case 0 To 0.59 : Result = 20
                    Case 0.6 To 4.12 : Result = 21
                    Case 4.13 To 34.41 : Result = 22
                    Case 34.42 To 85.29 : Result = 23
                    Case 85.3 To 97.94 : Result = 24
                    Case 97.95 To 99.41 : Result = 25
                    Case 99.42 To 100.0 : Result = 26
                End Select
            Case "P"
                Select Case i
                    Case 0 To 2.78 : Result = 21
                    Case 2.79 To 22.22 : Result = 22
                    Case 22.23 To 75.0 : Result = 23
                    Case 75.01 To 100.0 : Result = 24
                End Select
            Case "QB"
                Select Case i
                    Case 0 To 6.16 : Result = 21
                    Case 6.17 To 26.54 : Result = 22
                    Case 26.55 To 77.73 : Result = 23
                    Case 77.74 To 96.68 : Result = 24
                    Case 96.69 To 98.1 : Result = 25
                    Case 98.11 To 98.58 : Result = 26
                    Case 98.59 To 99.05 : Result = 27
                    Case 99.06 To 99.53 : Result = 28
                    Case 99.54 To 100.0 : Result = 29
                End Select
            Case "RB"
                Select Case i
                    Case 0 To 17.92 : Result = 21
                    Case 17.93 To 55.03 : Result = 22
                    Case 55.04 To 89.94 : Result = 23
                    Case 89.95 To 98.11 : Result = 24
                    Case 98.12 To 99.69 : Result = 25
                    Case 99.7 To 100.0 : Result = 27
                End Select
            Case "SS"
                Select Case i
                    Case 0 To 8.07 : Result = 21
                    Case 8.08 To 42.86 : Result = 22
                    Case 42.87 To 89.44 : Result = 23
                    Case 89.45 To 98.76 : Result = 24
                    Case 98.77 To 99.38 : Result = 25
                    Case 99.39 To 100.0 : Result = 27
                End Select
            Case "TE"
                Select Case i
                    Case 0 To 0.4 : Result = 20
                    Case 0.41 To 7.2 : Result = 21
                    Case 7.21 To 38.4 : Result = 22
                    Case 38.41 To 83.6 : Result = 23
                    Case 83.61 To 98.4 : Result = 24
                    Case 98.41 To 100.0 : Result = 25
                End Select
            Case "WR"
                Select Case i
                    Case 0 To 0.19 : Result = 20
                    Case 0.2 To 12.36 : Result = 21
                    Case 12.37 To 54.49 : Result = 22
                    Case 54.5 To 91.01 : Result = 23
                    Case 91.02 To 98.31 : Result = 24
                    Case 98.32 To 99.63 : Result = 25
                    Case 99.64 To 100.0 : Result = 27
                End Select
        End Select
        Return Result
    End Function

    'Generates the Weight by position
    Private Shared Function GetWeight(ByVal height As Integer, Optional ByVal pos As String = "", Optional ByVal DraftRound As String = "") As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(3.003699108, 0.123396638), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(3.015538355, 0.089620613), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(3.080938917, 0.119446753), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(3.004249567, 0.089201945), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(2.98407395, 0.09160889), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(2.977188217, 0.116711851), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(2.982141441, 0.162016258), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(2.949304533, 0.125600723), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(2.934897009, 0.110407616), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(2.970727168, 0.118579623), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(2.962257506, 0.131202397), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(2.953787843, 0.14382517), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(2.924249965, 0.14382517), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(2.894712087, 0.14382517), 0) * height
                End Select

            Case "RB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(3.145330281, 0.184785975), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(2.921546711, 0.120030258), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(3.178221876, 0.261885191), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(3.045921432, 0.151010826), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(3.076710136, 0.14689853), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(3.002405199, 0.159204157), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(3.023456358, 0.188065907), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(2.968671046, 0.167514653), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(3.02607181, 0.157511895), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(3.019723593, 0.213190993), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(3.010130564, 0.190826412), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(3.000537534, 0.16846183), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(2.970532159, 0.16846183), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(2.940526784, 0.16846183), 0) * height
                End Select

            Case "FB"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 0) * height
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 0) * height
                    'Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 0) * height
                    'Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(3.508949772, 0.229502968), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(3.268792987, 0.074676991), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(3.409490894, 0.127814127), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(3.320027249, 0.146637608), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(3.408492376, 0.351573592), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(3.416055271, 0.181941228), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(3.396590564, 0.188342053), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(3.377125858, 0.194742878), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(3.343354599, 0.194742878), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(3.30958334, 0.194742878), 0) * height
                End Select

            Case "WR"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(2.878050679, 0.13790027), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(2.820531314, 0.152772052), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(2.769601207, 0.095873513), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(2.814119401, 0.158427992), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(2.769485519, 0.173780544), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(2.751983511, 0.148253559), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(2.745593197, 0.164274201), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(2.715396021, 0.165086519), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(2.761429846, 0.172527942), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(2.727412614, 0.161498127), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(2.744007854, 0.15936696), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(2.760603094, 0.157235794), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(2.732997063, 0.157235794), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(2.705391032, 0.157235794), 0) * height
                End Select

            Case "TE"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 0) * height
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(3.32625731, 0.052728079), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(3.340742591, 0.102434325), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(3.294840116, 0.098095697), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(3.364839303, 0.124863567), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(3.323679321, 0.096336292), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(3.331437105, 0.163662341), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(3.332416292, 0.110690371), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(3.338154674, 0.133395057), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(3.309857899, 0.136991643), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(3.324335664, 0.141966243), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(3.33881343, 0.146940843), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(3.305425296, 0.146940843), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(3.272037162, 0.146940843), 0) * height
                End Select

            Case "OT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.128013337, 0.291322837), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.054830955, 0.160396642), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.09042381, 0.234550303), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.087884549, 0.240544699), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(4.045470775, 0.131974217), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(4.070312343, 0.159883187), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(4.094661827, 0.139046894), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(4.052433025, 0.208789456), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(4.018033707, 0.165895843), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(4.044203948, 0.177364955), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.057357097, 0.184156626), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.070510245, 0.190948297), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.029805143, 0.190948297), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(3.98910004, 0.190948297), 0) * height
                End Select

            Case "OG"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.142857143, 0.00532352), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.243243243, 0.057332982), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.108280608, 0.176609158), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.140023544, 0.071275999), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(4.154585455, 0.189253974), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(4.161878211, 0.165069285), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(4.157018897, 0.201381863), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(4.131887553, 0.161587063), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(4.08687883, 0.160347948), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(4.094256593, 0.168345707), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.114162068, 0.171849084), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.134067543, 0.175352461), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(4.092726868, 0.175352461), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(4.051386193, 0.175352461), 0) * height
                End Select

            Case "C"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 0) * height
                    'Case "R1Top10" : Result = Math.Round(MT.GetGaussian(                #REF!	,   #REF!	), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.119606402, 0.180327068), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.063947368, 0.06329437), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(4.009953455, 0.071413548), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(3.99760414, 0.102724539), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(4.048362086, 0.087614717), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(3.970194651, 0.258080014), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(3.995399578, 0.185328619), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(3.987296851, 0.226782373), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(3.994460588, 0.183097476), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.001624326, 0.139412578), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(3.961608082, 0.139412578), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(3.921591839, 0.139412578), 0) * height
                End Select

            Case "DE"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(3.564770162, 0.185051416), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(3.610796329, 0.266431885), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(3.577860622, 0.179025065), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(3.547725622, 0.170417349), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(3.541719995, 0.152499565), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(3.53623495, 0.149314484), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(3.537329365, 0.16708782), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(3.555613345, 0.194385079), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(3.566493425, 0.209385695), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(3.527475026, 0.202300321), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(3.516394764, 0.186120854), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(3.505314503, 0.169941388), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(3.470261358, 0.169941388), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(3.435208213, 0.169941388), 0) * height
                End Select

            Case "DT"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(4.124310262, 0.154570859), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(4.106714377, 0.22652317), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(4.07040748, 0.222294997), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(4.125450642, 0.163317016), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(4.062790068, 0.189487988), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(4.102091297, 0.170898834), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(4.083486766, 0.224333075), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(4.123058949, 0.185607464), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(4.073809782, 0.220106305), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(4.042051978, 0.290126666), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(4.038348346, 0.252191128), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(4.034644715, 0.21425559), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(3.994298268, 0.21425559), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(3.953951821, 0.21425559), 0) * height
                End Select

            Case "NT" : Result = CInt(MT.GetGaussian(4.29, 0.27) * height)
            Case "LB" : Result = CInt(MT.GetGaussian(3.25886366, 0.22761413) * height)
            Case "OLB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(3.378282118, 0.072551029), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(3.302605928, 0.115748052), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(3.314396271, 0.14541675), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(3.27795127, 0.134993054), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(3.26097097, 0.154513591), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(3.240139862, 0.119749522), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(3.243760087, 0.11701307), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(3.225455634, 0.134653294), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(3.232632861, 0.120299887), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(3.238817437, 0.141158293), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(3.229814405, 0.130752526), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(3.220811373, 0.120346759), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(3.188603259, 0.120346759), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(3.156395145, 0.120346759), 0) * height
                End Select

            Case "ILB"
                Select Case DraftRound
                    'Case"r1Top5",  "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 0) * height
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(3.292080305, 0.022101405), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(3.237780589, 0.076466835), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(3.328066483, 0.130632713), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(3.2825155, 0.103225907), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(3.273239734, 0.117187605), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(3.270329113, 0.07954734), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(3.274353496, 0.112657201), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(3.299572376, 0.11937643), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(3.312460323, 0.106755577), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(3.298910886, 0.099600815), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(3.28536145, 0.092446053), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(3.252507835, 0.092446053), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(3.219654221, 0.092446053), 0) * height
                End Select

            Case "CB"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(2.840293186, 0.158072916), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(2.715970142, 0.096337728), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(2.717826145, 0.104052774), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(2.716045297, 0.095867458), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(2.719427596, 0.110597779), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(2.702004905, 0.098496213), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(2.70886641, 0.105107122), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(2.695361835, 0.100950288), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(2.690204038, 0.124060574), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(2.685250219, 0.135575872), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(2.684192373, 0.127600655), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(2.683134526, 0.119625439), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(2.656303181, 0.119625439), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(2.629471836, 0.119625439), 0) * height
                End Select

            Case "DB" : Result = CInt(MT.GetGaussian(2.751497012, 0.111561502) * height)
            Case "SS"
                Select Case DraftRound
                    'Case "R1Top5" : Result = Math.Round(MT.GetGaussian(             #REF!	,   #REF!	), 0) * height
                    Case "R1Top5", "R1Top10" : Result = Math.Round(MT.GetGaussian(2.919767341, 0.078913623), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(2.905290099, 0.072640226), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(2.897312292, 0.092444192), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(2.948656933, 0.091568715), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(2.903821791, 0.083652184), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(2.916727579, 0.118404029), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(2.895904935, 0.115391547), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(2.880106784, 0.0899513), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(2.866493549, 0.146522805), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(2.888574011, 0.128158489), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(2.910654472, 0.109794173), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(2.881547928, 0.109794173), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(2.852441383, 0.109794173), 0) * height
                End Select

            Case "FS"
                Select Case DraftRound
                    Case "R1Top5" : Result = Math.Round(MT.GetGaussian(2.999259534, 0.15393513), 0) * height
                    Case "R1Top10" : Result = Math.Round(MT.GetGaussian(2.917808219, 0.1356656215), 0) * height
                    Case "R1MidFirst" : Result = Math.Round(MT.GetGaussian(2.933783523, 0.127630602), 0) * height
                    Case "R1LateFirst" : Result = Math.Round(MT.GetGaussian(2.794089478, 0.062443496), 0) * height
                    Case "R2" : Result = Math.Round(MT.GetGaussian(2.832525255, 0.1125825), 0) * height
                    Case "R3" : Result = Math.Round(MT.GetGaussian(2.822130288, 0.114004496), 0) * height
                    Case "R4" : Result = Math.Round(MT.GetGaussian(2.800061899, 0.101852847), 0) * height
                    Case "R5" : Result = Math.Round(MT.GetGaussian(2.844227237, 0.140222589), 0) * height
                    Case "R6" : Result = Math.Round(MT.GetGaussian(2.812912572, 0.108694958), 0) * height
                    Case "R7" : Result = Math.Round(MT.GetGaussian(2.839624309, 0.12325654), 0) * height
                    Case "PUFA" : Result = Math.Round(MT.GetGaussian(2.819249801, 0.119042942), 0) * height
                    Case "LUFA" : Result = Math.Round(MT.GetGaussian(2.798875292, 0.114829344), 0) * height
                    Case "PracSquad" : Result = Math.Round(MT.GetGaussian(2.770886539, 0.114829344), 0) * height
                    Case "Reject" : Result = Math.Round(MT.GetGaussian(2.742897786, 0.114829344), 0) * height
                End Select

            Case "K" : Result = CInt(MT.GetGaussian(2.76402701, 0.225939859) * height)
            Case "P" : Result = CInt(MT.GetGaussian(2.918257853, 0.150105271) * height)
            Case Else
                Result = (CInt(MT.GetGaussian(3.5, 0.5)) * height)
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
                        Caring = MT.GetGaussian(75, 8.33)
                        Nurturing = MT.GetGaussian(75, 8.33)
                        Friendly = MT.GetGaussian(75, 8.33)
                        Participator = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Caring + Nurturing + Friendly + Participator) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Friendly'"
                        End If

                    Case "Negative"
                        Caring = MT.GetGaussian(25, 8.33)
                        Nurturing = MT.GetGaussian(25, 8.33)
                        Friendly = MT.GetGaussian(25, 8.33)
                        Participator = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Caring + Nurturing + Friendly + Participator) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Friendly'"
                        End If
                    Case "Balanced"
                        Caring = MT.GetGaussian(50, 8.33)
                        Nurturing = MT.GetGaussian(50, 8.33)
                        Friendly = MT.GetGaussian(50, 8.33)
                        Participator = MT.GetGaussian(50, 8.33)
                End Select
            Case "EmotionalStability"
                Select Case traitStrength
                    Case "Positive"
                        EmotionallyStable = MT.GetGaussian(75, 8.33)
                        Conforming = MT.GetGaussian(75, 8.33)
                        Mature = MT.GetGaussian(75, 8.33)
                        CalmUnderPressure = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (EmotionallyStable + Conforming + Mature + CalmUnderPressure) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Emotionally Stable'"
                        End If

                    Case "Negative"
                        EmotionallyStable = MT.GetGaussian(25, 8.33)
                        Conforming = MT.GetGaussian(25, 8.33)
                        Mature = MT.GetGaussian(25, 8.33)
                        CalmUnderPressure = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (EmotionallyStable + Conforming + Mature + CalmUnderPressure) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Emotionally Stable'"
                        End If
                    Case "Balanced"
                        EmotionallyStable = MT.GetGaussian(50, 8.33)
                        Conforming = MT.GetGaussian(50, 8.33)
                        Mature = MT.GetGaussian(50, 8.33)
                        CalmUnderPressure = MT.GetGaussian(50, 8.33)
                End Select
            Case "Dominance"
                Select Case traitStrength
                    Case "Positive"
                        Stubborn = MT.GetGaussian(75, 8.33)
                        Aggressive = MT.GetGaussian(75, 8.33)
                        Competitive = MT.GetGaussian(75, 8.33)
                        Bossy = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Stubborn + Aggressive + Competitive + Bossy) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Dominant'"
                        End If
                    Case "Negative"
                        Stubborn = MT.GetGaussian(25, 8.33)
                        Aggressive = MT.GetGaussian(25, 8.33)
                        Competitive = MT.GetGaussian(25, 8.33)
                        Bossy = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Stubborn + Aggressive + Competitive + Bossy) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Dominant'"
                        End If
                    Case "Balanced"
                        Stubborn = MT.GetGaussian(50, 8.33)
                        Aggressive = MT.GetGaussian(50, 8.33)
                        Competitive = MT.GetGaussian(50, 8.33)
                        Bossy = MT.GetGaussian(50, 8.33)
                End Select
            Case "Liveliness"
                Select Case traitStrength
                    Case "Positive"
                        Enthusiastic = MT.GetGaussian(75, 8.33)
                        Impulsive = MT.GetGaussian(75, 8.33)
                        FunLoving = MT.GetGaussian(75, 8.33)
                        Expressive = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Enthusiastic + Impulsive + FunLoving + Expressive) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Fun Loving'"
                        End If
                    Case "Negative"
                        Enthusiastic = MT.GetGaussian(25, 8.33)
                        Impulsive = MT.GetGaussian(25, 8.33)
                        FunLoving = MT.GetGaussian(25, 8.33)
                        Expressive = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Enthusiastic + Impulsive + FunLoving + Expressive) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Fun Loving'"
                        End If
                    Case "Balanced"
                        Enthusiastic = MT.GetGaussian(50, 8.33)
                        Impulsive = MT.GetGaussian(50, 8.33)
                        FunLoving = MT.GetGaussian(50, 8.33)
                        Expressive = MT.GetGaussian(50, 8.33)
                End Select
            Case "RuleConcious"
                Select Case traitStrength
                    Case "Positive"
                        Dutiful = MT.GetGaussian(75, 8.33)
                        TeamPlayer = MT.GetGaussian(75, 8.33)
                        FollowsRules = MT.GetGaussian(75, 8.33)
                        Moralistic = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Dutiful + TeamPlayer + FollowsRules + Moralistic) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Rule Follower'"
                        End If
                    Case "Negative"
                        Dutiful = MT.GetGaussian(25, 8.33)
                        TeamPlayer = MT.GetGaussian(25, 8.33)
                        FollowsRules = MT.GetGaussian(25, 8.33)
                        Moralistic = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Dutiful + TeamPlayer + FollowsRules + Moralistic) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Rule Follower'"
                        End If
                    Case "Balanced"
                        Dutiful = MT.GetGaussian(50, 8.33)
                        TeamPlayer = MT.GetGaussian(50, 8.33)
                        FollowsRules = MT.GetGaussian(50, 8.33)
                        Moralistic = MT.GetGaussian(50, 8.33)
                End Select
            Case "SocialBoldness"
                Select Case traitStrength
                    Case "Positive"
                        SociallyBold = MT.GetGaussian(75, 8.33)
                        ThickSkinned = MT.GetGaussian(75, 8.33)
                        Adventurous = MT.GetGaussian(75, 8.33)
                        Uninhibited = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (SociallyBold + ThickSkinned + Adventurous + Uninhibited) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Socially Bold'"
                        End If
                    Case "Negative"
                        SociallyBold = MT.GetGaussian(25, 8.33)
                        ThickSkinned = MT.GetGaussian(25, 8.33)
                        Adventurous = MT.GetGaussian(25, 8.33)
                        Uninhibited = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (SociallyBold + ThickSkinned + Adventurous + Uninhibited) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Socially Bold'"
                        End If
                    Case "Balanced"
                        SociallyBold = MT.GetGaussian(50, 8.33)
                        ThickSkinned = MT.GetGaussian(50, 8.33)
                        Adventurous = MT.GetGaussian(50, 8.33)
                        Uninhibited = MT.GetGaussian(50, 8.33)
                End Select
            Case "Sensitivity"
                Select Case traitStrength
                    Case "Positive"
                        Refined = MT.GetGaussian(75, 8.33)
                        Intuitive = MT.GetGaussian(75, 8.33)
                        Sentimental = MT.GetGaussian(75, 8.33)
                        Sensitive = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Refined + Intuitive + Sentimental + Sensitive) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Sensitive'"
                        End If
                    Case "Negative"
                        Refined = MT.GetGaussian(25, 8.33)
                        Intuitive = MT.GetGaussian(25, 8.33)
                        Sentimental = MT.GetGaussian(25, 8.33)
                        Sensitive = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Refined + Intuitive + Sentimental + Sensitive) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Sensitive'"
                        End If
                    Case "Balanced"
                        Refined = MT.GetGaussian(50, 8.33)
                        Intuitive = MT.GetGaussian(50, 8.33)
                        Sentimental = MT.GetGaussian(50, 8.33)
                        Sensitive = MT.GetGaussian(50, 8.33)
                End Select
            Case "Vigilance"
                Select Case traitStrength
                    Case "Positive"
                        Suspicious = MT.GetGaussian(75, 8.33)
                        Oppositional = MT.GetGaussian(75, 8.33)
                        Distrustful = MT.GetGaussian(75, 8.33)
                        Vigilant = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Suspicious + Oppositional + Distrustful + Vigilant) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Distrustful'"
                        End If
                    Case "Negative"
                        Suspicious = MT.GetGaussian(25, 8.33)
                        Oppositional = MT.GetGaussian(25, 8.33)
                        Distrustful = MT.GetGaussian(25, 8.33)
                        Vigilant = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Suspicious + Oppositional + Distrustful + Vigilant) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Distrustful'"
                        End If
                    Case "Balanced"
                        Suspicious = MT.GetGaussian(50, 8.33)
                        Oppositional = MT.GetGaussian(50, 8.33)
                        Distrustful = MT.GetGaussian(50, 8.33)
                        Vigilant = MT.GetGaussian(50, 8.33)
                End Select
            Case "Abstractedness"
                Select Case traitStrength
                    Case "Positive"
                        Impractical = MT.GetGaussian(75, 8.33)
                        Imaginative = MT.GetGaussian(75, 8.33)
                        AbsentMinded = MT.GetGaussian(75, 8.33)
                        AbstractThinker = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Impractical + Imaginative + AbsentMinded + AbstractThinker) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Abstract'"
                        End If
                    Case "Negative"
                        Impractical = MT.GetGaussian(25, 8.33)
                        Imaginative = MT.GetGaussian(25, 8.33)
                        AbsentMinded = MT.GetGaussian(25, 8.33)
                        AbstractThinker = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Impractical + Imaginative + AbsentMinded + AbstractThinker) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Abstract'"
                        End If
                    Case "Balanced"
                        Impractical = MT.GetGaussian(50, 8.33)
                        Imaginative = MT.GetGaussian(50, 8.33)
                        AbsentMinded = MT.GetGaussian(50, 8.33)
                        AbstractThinker = MT.GetGaussian(50, 8.33)
                End Select
            Case "Privateness"
                Select Case traitStrength
                    Case "Positive"
                        Privateness = MT.GetGaussian(75, 8.33)
                        Astute = MT.GetGaussian(75, 8.33)
                        Diplomatic = MT.GetGaussian(75, 8.33)
                        Polished = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Privateness + Astute + Diplomatic + Polished) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Private'"
                        End If
                    Case "Negative"
                        Privateness = MT.GetGaussian(25, 8.33)
                        Astute = MT.GetGaussian(25, 8.33)
                        Diplomatic = MT.GetGaussian(25, 8.33)
                        Polished = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Privateness + Astute + Diplomatic + Polished) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Private'"
                        End If
                    Case "Balanced"
                        Privateness = MT.GetGaussian(50, 8.33)
                        Astute = MT.GetGaussian(50, 8.33)
                        Diplomatic = MT.GetGaussian(50, 8.33)
                        Polished = MT.GetGaussian(50, 8.33)
                End Select
            Case "Apprehension"
                Select Case traitStrength
                    Case "Positive"
                        Fearful = MT.GetGaussian(75, 8.33)
                        SelfDoubting = MT.GetGaussian(75, 8.33)
                        GuiltProne = MT.GetGaussian(75, 8.33)
                        Insecure = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Fearful + SelfDoubting + GuiltProne + Insecure) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Apprehensive'"
                        End If
                    Case "Negative"
                        Fearful = MT.GetGaussian(25, 8.33)
                        SelfDoubting = MT.GetGaussian(25, 8.33)
                        GuiltProne = MT.GetGaussian(25, 8.33)
                        Insecure = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Fearful + SelfDoubting + GuiltProne + Insecure) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Apprehensive'"
                        End If
                    Case "Balanced"
                        Fearful = MT.GetGaussian(50, 8.33)
                        SelfDoubting = MT.GetGaussian(50, 8.33)
                        GuiltProne = MT.GetGaussian(50, 8.33)
                        Insecure = MT.GetGaussian(50, 8.33)
                End Select
            Case "OpennessToChange"
                Select Case traitStrength
                    Case "Positive"
                        Adaptable = MT.GetGaussian(75, 8.33)
                        Experimental = MT.GetGaussian(75, 8.33)
                        Analytical = MT.GetGaussian(75, 8.33)
                        Critical = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Adaptable + Experimental + Analytical + Critical) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Open To Change'"
                        End If
                    Case "Negative"
                        Adaptable = MT.GetGaussian(25, 8.33)
                        Experimental = MT.GetGaussian(25, 8.33)
                        Analytical = MT.GetGaussian(25, 8.33)
                        Critical = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Adaptable + Experimental + Analytical + Critical) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Open To Change'"
                        End If
                    Case "Balanced"
                        Adaptable = MT.GetGaussian(50, 8.33)
                        Experimental = MT.GetGaussian(50, 8.33)
                        Analytical = MT.GetGaussian(50, 8.33)
                        Critical = MT.GetGaussian(50, 8.33)
                End Select
            Case "SelfReliance"
                Select Case traitStrength
                    Case "Positive"
                        SelfSufficient = MT.GetGaussian(75, 8.33)
                        Resourceful = MT.GetGaussian(75, 8.33)
                        Individualistic = MT.GetGaussian(75, 8.33)
                        Loner = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (SelfSufficient + Resourceful + Individualistic + Loner) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Self Reliant'"
                        End If
                    Case "Negative"
                        SelfSufficient = MT.GetGaussian(25, 8.33)
                        Resourceful = MT.GetGaussian(25, 8.33)
                        Individualistic = MT.GetGaussian(25, 8.33)
                        Loner = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (SelfSufficient + Resourceful + Individualistic + Loner) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Self Reliant'"
                        End If
                    Case "Balanced"
                        SelfSufficient = MT.GetGaussian(50, 8.33)
                        Resourceful = MT.GetGaussian(50, 8.33)
                        Individualistic = MT.GetGaussian(50, 8.33)
                        Loner = MT.GetGaussian(50, 8.33)
                End Select
            Case "Perfectionism"
                Select Case traitStrength
                    Case "Positive"
                        Perfectionist = MT.GetGaussian(75, 8.33)
                        StrongWilled = MT.GetGaussian(75, 8.33)
                        Organized = MT.GetGaussian(75, 8.33)
                        Controlling = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Perfectionist + StrongWilled + Organized + Controlling) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Perfectionist'"
                        End If
                    Case "Negative"
                        Perfectionist = MT.GetGaussian(25, 8.33)
                        StrongWilled = MT.GetGaussian(25, 8.33)
                        Organized = MT.GetGaussian(25, 8.33)
                        Controlling = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Perfectionist + StrongWilled + Organized + Controlling) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Perfectionist'"
                        End If
                    Case "Balanced"
                        Perfectionist = MT.GetGaussian(50, 8.33)
                        StrongWilled = MT.GetGaussian(50, 8.33)
                        Organized = MT.GetGaussian(50, 8.33)
                        Controlling = MT.GetGaussian(50, 8.33)
                End Select
            Case "Tension"
                Select Case traitStrength
                    Case "Positive"
                        HighEnergy = MT.GetGaussian(75, 8.33)
                        Impatient = MT.GetGaussian(75, 8.33)
                        Driven = MT.GetGaussian(75, 8.33)
                        Tense = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (HighEnergy + Impatient + Driven + Tense) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Tense'"
                        End If
                    Case "Negative"
                        HighEnergy = MT.GetGaussian(25, 8.33)
                        Impatient = MT.GetGaussian(25, 8.33)
                        Driven = MT.GetGaussian(25, 8.33)
                        Tense = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (HighEnergy + Impatient + Driven + Tense) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Tense'"
                        End If
                    Case "Balanced"
                        HighEnergy = MT.GetGaussian(50, 8.33)
                        Impatient = MT.GetGaussian(50, 8.33)
                        Driven = MT.GetGaussian(50, 8.33)
                        Tense = MT.GetGaussian(50, 8.33)
                End Select
            Case "Honesty"
                Select Case traitStrength
                    Case "Positive"
                        Honesty = MT.GetGaussian(75, 8.33)
                        Fairness = MT.GetGaussian(75, 8.33)
                        GreedAvoidance = MT.GetGaussian(75, 8.33)
                        Modesty = MT.GetGaussian(75, 8.33)
                        DTotal(2) = (Honesty + Fairness + GreedAvoidance + Modesty) / 4
                        If DTotal(2) > DTotal(1) Then
                            DTotal(1) = DTotal(2)
                            Dominant = "'Honest'"
                        End If
                    Case "Negative"
                        Honesty = MT.GetGaussian(25, 8.33)
                        Fairness = MT.GetGaussian(25, 8.33)
                        GreedAvoidance = MT.GetGaussian(25, 8.33)
                        Modesty = MT.GetGaussian(25, 8.33)
                        WTotal(2) = (Honesty + Fairness + GreedAvoidance + Modesty) / 4
                        If WTotal(2) > WTotal(1) Then
                            WTotal(1) = WTotal(2)
                            Weakest = "'Tense'"
                        End If
                    Case "Balanced"
                        Honesty = MT.GetGaussian(50, 8.33)
                        Fairness = MT.GetGaussian(50, 8.33)
                        GreedAvoidance = MT.GetGaussian(50, 8.33)
                        Modesty = MT.GetGaussian(50, 8.33)
                End Select
            Case "Reasoning"
                Intelligent = MT.GetGaussian(49.5, 16.5)
                MentalCapacity = MT.GetGaussian(49.5, 16.5)
                AbstractReasoning = MT.GetGaussian(49.5, 16.5)
        End Select

    End Sub

End Class