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

    Public Shared Sub GetPersonalityStats(ByVal dt As DataTable, ByVal personNum As Integer, ByVal classInstance As Object)
        dt.Rows(personNum).Item("AbsentMinded") = classInstance.AbsentMinded
        dt.Rows(personNum).Item("AbstractReasoning") = classInstance.AbstractReasoning
        dt.Rows(personNum).Item("AbstractThinker") = classInstance.AbstractThinker
        dt.Rows(personNum).Item("Adaptable") = classInstance.Adaptable
        dt.Rows(personNum).Item("Adventurous") = classInstance.Adventurous
        dt.Rows(personNum).Item("Aggressive") = classInstance.Aggressive
        dt.Rows(personNum).Item("Analytical") = classInstance.Analytical
        dt.Rows(personNum).Item("Astute") = classInstance.Astute
        dt.Rows(personNum).Item("Bossy") = classInstance.Bossy
        dt.Rows(personNum).Item("CalmUnderPressure") = classInstance.CalmUnderPressure
        dt.Rows(personNum).Item("Caring") = classInstance.Caring
        dt.Rows(personNum).Item("Competitive") = classInstance.Competitive
        dt.Rows(personNum).Item("Conforming") = classInstance.Conforming
        dt.Rows(personNum).Item("Controlling") = classInstance.Controlling
        dt.Rows(personNum).Item("Critical") = classInstance.Critical
        dt.Rows(personNum).Item("Diplomatic") = classInstance.Diplomatic
        dt.Rows(personNum).Item("Dutiful") = classInstance.Dutiful
        dt.Rows(personNum).Item("Driven") = classInstance.Driven
        dt.Rows(personNum).Item("EmotionallyStable") = classInstance.EmotionallyStable
        dt.Rows(personNum).Item("Enthusiastic") = classInstance.Enthusiastic
        dt.Rows(personNum).Item("Expermiental") = classInstance.Experimental
        dt.Rows(personNum).Item("Expressive") = classInstance.Expressive
        dt.Rows(personNum).Item("Fairness") = classInstance.Fairness
        dt.Rows(personNum).Item("Fearful") = classInstance.Fearful
        dt.Rows(personNum).Item("FollowsRules") = classInstance.FollowsRules
        dt.Rows(personNum).Item("Friendly") = classInstance.Friendly
        dt.Rows(personNum).Item("FunLoving") = classInstance.FunLoving
        dt.Rows(personNum).Item("GuiltProne") = classInstance.GuiltProne
        dt.Rows(personNum).Item("GreedAvoidance") = classInstance.GreedAvoidance
        dt.Rows(personNum).Item("HighEnergy") = classInstance.HighEnergy
        dt.Rows(personNum).Item("Honesty") = classInstance.Honesty
        dt.Rows(personNum).Item("Impulsive") = classInstance.Impulsive
        dt.Rows(personNum).Item("Imaginative") = classInstance.Imaginative
        dt.Rows(personNum).Item("Impatient") = classInstance.Impatient
        dt.Rows(personNum).Item("Impractical") = classInstance.Impractical
        dt.Rows(personNum).Item("Individualistic") = classInstance.Individualistic
        dt.Rows(personNum).Item("Insecure") = classInstance.Insecure
        dt.Rows(personNum).Item("Intelligent") = classInstance.Intelligent
        dt.Rows(personNum).Item("Loner") = classInstance.Loner
        dt.Rows(personNum).Item("Mature") = classInstance.Mature
        dt.Rows(personNum).Item("MentalCapacity") = classInstance.MentalCapacity
        dt.Rows(personNum).Item("Modesty") = classInstance.Modesty
        dt.Rows(personNum).Item("Moralistic") = classInstance.Moralistic
        dt.Rows(personNum).Item("Nurturing") = classInstance.Nurturing
        dt.Rows(personNum).Item("Organized") = classInstance.Organized
        dt.Rows(personNum).Item("Participator") = classInstance.Participator
        dt.Rows(personNum).Item("Perfectionist") = classInstance.Perfectionist
        dt.Rows(personNum).Item("Polished") = classInstance.Polished
        dt.Rows(personNum).Item("Private") = classInstance.Privateness
        dt.Rows(personNum).Item("Resourceful") = classInstance.Resourceful
        dt.Rows(personNum).Item("SelfDoubting") = classInstance.SelfDoubting
        dt.Rows(personNum).Item("SelfSufficient") = classInstance.SelfSufficient
        dt.Rows(personNum).Item("StrongWilled") = classInstance.StrongWilled
        dt.Rows(personNum).Item("Stubborn") = classInstance.Stubborn
        dt.Rows(personNum).Item("TeamPlayer") = classInstance.TeamPlayer
        dt.Rows(personNum).Item("Tense") = classInstance.Tense
        dt.Rows(personNum).Item("Vigilant") = classInstance.Vigilant
        dt.Rows(personNum).Item("Dominant") = classInstance.Dominant
        dt.Rows(personNum).Item("Weakest") = classInstance.Weakest
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
                Result = GetDraftAge()
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
    Private Shared Function GetDraftAge() As Integer
        Dim Result As Integer
        Dim i As Integer = MT.GenerateInt32(1, 100)
        Select Case i
            Case 1 To 82
                Result = 22
            Case 83 To 92
                Result = 21
            Case 93 To 96
                Result = 23
            Case 97
                Result = 20
            Case 98 To 99
                Result = 24
            Case 100
                Result = 25
        End Select
        Return Result
    End Function
    'Generates the Weight by position
    Private Shared Function GetWeight(ByVal height As Integer, Optional ByVal pos As String = "") As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB" : Result = CInt(MT.GetGaussian(3.0, 0.16) * height)
            Case "RB" : Result = CInt(MT.GetGaussian(3.07, 0.16) * height)
            Case "FB" : Result = CInt(MT.GetGaussian(3.39, 0.13) * height)
            Case "WR" : Result = CInt(MT.GetGaussian(2.74, 0.15) * height)
            Case "TE" : Result = CInt(MT.GetGaussian(3.37, 0.14) * height)
            Case "OT" : Result = CInt(MT.GetGaussian(4.07, 0.17) * height)
            Case "OG" : Result = CInt(MT.GetGaussian(4.13, 0.18) * height)
            Case "C" : Result = CInt(MT.GetGaussian(4.01, 0.16) * height)
            Case "DE" : Result = CInt(MT.GetGaussian(3.61, 0.22) * height)
            Case "DT" : Result = CInt(MT.GetGaussian(4.09, 0.22) * height)
            Case "NT" : Result = CInt(MT.GetGaussian(4.29, 0.27) * height)
            Case "LB" : Result = CInt(MT.GetGaussian(3.26, 0.14) * height)
            Case "OLB" : Result = CInt(MT.GetGaussian(3.29, 0.15) * height)
            Case "ILB" : Result = CInt(MT.GetGaussian(3.3, 0.11) * height)
            Case "CB" : Result = CInt(MT.GetGaussian(2.7, 0.11) * height)
            Case "DB" : Result = CInt(MT.GetGaussian(2.77, 0.15) * height)
            Case "SS" : Result = CInt(MT.GetGaussian(2.92, 0.06) * height)
            Case "FS" : Result = CInt(MT.GetGaussian(2.83, 0.11) * height)
            Case "K" : Result = CInt(MT.GetGaussian(2.81, 0.21) * height)
            Case "P" : Result = CInt(MT.GetGaussian(2.9, 0.21) * height)
            Case Else
                Result = (CInt(MT.GetGaussian(3.5, 0.5)) * height)
        End Select
        Return Result
    End Function
    'Generates the Height By Position
    Private Shared Function GetHeight(Optional ByVal pos As String = "") As Integer
        Dim Result As Integer
        Select Case pos
            Case "QB"
                Select Case MT.GenerateInt32(1, 141)
                    Case 1 : Result = 68
                    Case 2 : Result = 69
                    Case 3 : Result = 70
                    Case 4 : Result = 71
                    Case 5 To 12 : Result = 72
                    Case 13 To 29 : Result = 73
                    Case 30 To 68 : Result = 74
                    Case 69 To 93 : Result = 75
                    Case 94 To 119 : Result = 76
                    Case 120 To 138 : Result = 77
                    Case 139 To 141 : Result = 78
                End Select
            Case "RB"
                Select Case MT.GenerateInt32(1, 183)
                    Case 1 To 2 : Result = 66
                    Case 3 To 6 : Result = 67
                    Case 7 To 14 : Result = 68
                    Case 15 To 41 : Result = 69
                    Case 42 To 88 : Result = 70
                    Case 89 To 125 : Result = 71
                    Case 126 To 155 : Result = 72
                    Case 156 To 176 : Result = 73
                    Case 177 To 181 : Result = 74
                    Case 182 : Result = 75
                    Case 183 : Result = 76
                End Select
            Case "FB"
                Select Case MT.GenerateInt32(1, 92)
                    Case 1 : Result = 69
                    Case 2 To 6 : Result = 70
                    Case 7 To 25 : Result = 71
                    Case 26 To 54 : Result = 72
                    Case 55 To 70 : Result = 73
                    Case 71 To 84 : Result = 74
                    Case 85 To 88 : Result = 75
                    Case 89 To 92 : Result = 76
                End Select
            Case "WR"
                Select Case MT.GenerateInt32(1, 375)
                    Case 1 : Result = 67
                    Case 2 To 11 : Result = 68
                    Case 12 To 27 : Result = 69
                    Case 28 To 65 : Result = 70
                    Case 66 To 112 : Result = 71
                    Case 113 To 174 : Result = 72
                    Case 175 To 233 : Result = 73
                    Case 234 To 286 : Result = 74
                    Case 287 To 329 : Result = 75
                    Case 330 To 358 : Result = 76
                    Case 359 To 370 : Result = 77
                    Case 371 To 375 : Result = 78
                End Select
            Case "TE"
                Select Case MT.GenerateInt32(1, 182)
                    Case 1 : Result = 72
                    Case 2 To 5 : Result = 73
                    Case 6 To 16 : Result = 74
                    Case 17 To 52 : Result = 75
                    Case 53 To 104 : Result = 76
                    Case 105 To 147 : Result = 77
                    Case 148 To 170 : Result = 78
                    Case 171 To 177 : Result = 79
                    Case 178 To 182 : Result = 80
                End Select
            Case "OT"
                Select Case MT.GenerateInt32(1, 213)
                    Case 1 : Result = 74
                    Case 2 To 14 : Result = 75
                    Case 15 To 54 : Result = 76
                    Case 55 To 106 : Result = 77
                    Case 107 To 158 : Result = 78
                    Case 159 To 195 : Result = 79
                    Case 196 To 210 : Result = 80
                    Case 211 To 213 : Result = 81
                End Select
            Case "OG"
                Select Case MT.GenerateInt32(1, 183)
                    Case 1 : Result = 73
                    Case 2 To 21 : Result = 74
                    Case 22 To 69 : Result = 75
                    Case 70 To 125 : Result = 76
                    Case 126 To 162 : Result = 77
                    Case 163 To 175 : Result = 78
                    Case 176 To 182 : Result = 79
                    Case 183 : Result = 80
                End Select
            Case "C"
                Select Case MT.GenerateInt32(1, 82)
                    Case 1 : Result = 72
                    Case 2 To 5 : Result = 73
                    Case 6 To 21 : Result = 74
                    Case 22 To 48 : Result = 75
                    Case 49 To 70 : Result = 76
                    Case 71 To 81 : Result = 77
                    Case 82 : Result = 78
                End Select
            Case "DE"
                Select Case MT.GenerateInt32(1, 231)
                    Case 1 : Result = 71
                    Case 2 : Result = 72
                    Case 3 To 10 : Result = 73
                    Case 11 To 42 : Result = 74
                    Case 43 To 98 : Result = 75
                    Case 99 To 159 : Result = 76
                    Case 160 To 202 : Result = 77
                    Case 203 To 225 : Result = 78
                    Case 226 To 231 : Result = 79
                End Select
            Case "DT"
                Select Case MT.GenerateInt32(1, 194)
                    Case 1 To 2 : Result = 71
                    Case 3 To 13 : Result = 72
                    Case 14 To 34 : Result = 73
                    Case 35 To 77 : Result = 74
                    Case 78 To 132 : Result = 75
                    Case 133 To 167 : Result = 76
                    Case 168 To 183 : Result = 77
                    Case 184 To 191 : Result = 78
                    Case 192 To 194 : Result = 79
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
            Case "LB"
                Select Case MT.GenerateInt32(1, 227)
                    Case 1 : Result = 69
                    Case 2 To 6 : Result = 70
                    Case 7 To 14 : Result = 71
                    Case 9 To 51 : Result = 72
                    Case 52 To 111 : Result = 73
                    Case 112 To 161 : Result = 74
                    Case 162 To 209 : Result = 75
                    Case 210 To 218 : Result = 76
                    Case 219 To 226 : Result = 77
                    Case 227 : Result = 78
                End Select
            Case "OLB"
                Select Case MT.GenerateInt32(1, 83)
                    Case 1 To 3 : Result = 71
                    Case 4 To 19 : Result = 72
                    Case 20 To 30 : Result = 73
                    Case 31 To 46 : Result = 74
                    Case 47 To 66 : Result = 75
                    Case 67 To 77 : Result = 76
                    Case 78 To 82 : Result = 77
                    Case 83 : Result = 78
                End Select
            Case "ILB"
                Select Case MT.GenerateInt32(1, 48)
                    Case 1 : Result = 70
                    Case 2 To 3 : Result = 71
                    Case 4 To 8 : Result = 72
                    Case 9 To 25 : Result = 73
                    Case 26 To 42 : Result = 74
                    Case 43 To 46 : Result = 75
                    Case 47 To 48 : Result = 76
                End Select
            Case "CB"
                Select Case MT.GenerateInt32(1, 173)
                    Case 1 To 5 : Result = 68
                    Case 6 To 21 : Result = 69
                    Case 22 To 66 : Result = 70
                    Case 67 To 109 : Result = 71
                    Case 110 To 140 : Result = 72
                    Case 141 To 166 : Result = 73
                    Case 167 To 171 : Result = 74
                    Case 172 : Result = 75
                    Case 173 : Result = 76
                End Select
            Case "DB"
                Select Case MT.GenerateInt32(1, 222)
                    Case 1 To 4 : Result = 68
                    Case 5 To 19 : Result = 69
                    Case 20 To 58 : Result = 70
                    Case 59 To 104 : Result = 71
                    Case 105 To 139 : Result = 72
                    Case 140 To 188 : Result = 73
                    Case 189 To 210 : Result = 74
                    Case 211 To 222 : Result = 75
                End Select
            Case "SS"
                Select Case MT.GenerateInt32(1, 47)
                    Case 1 : Result = 68
                    Case 2 : Result = 69
                    Case 3 To 8 : Result = 70
                    Case 9 To 13 : Result = 71
                    Case 14 To 29 : Result = 72
                    Case 30 To 40 : Result = 73
                    Case 41 To 45 : Result = 74
                    Case 46 To 47 : Result = 75
                End Select
            Case "FS"
                Select Case MT.GenerateInt32(1, 49)
                    Case 1 : Result = 68
                    Case 2 : Result = 69
                    Case 3 To 7 : Result = 70
                    Case 8 To 21 : Result = 71
                    Case 22 To 30 : Result = 72
                    Case 31 To 36 : Result = 73
                    Case 37 To 46 : Result = 74
                    Case 47 : Result = 75
                    Case 48 : Result = 76
                    Case 49 : Result = 77
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 53)
                    Case 1 : Result = 68
                    Case 2 To 4 : Result = 69
                    Case 5 To 10 : Result = 70
                    Case 11 To 19 : Result = 71
                    Case 20 To 33 : Result = 72
                    Case 34 To 42 : Result = 73
                    Case 43 To 49 : Result = 74
                    Case 50 To 51 : Result = 75
                    Case 52 : Result = 76
                    Case 53 : Result = 77
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 63)
                    Case 1 : Result = 68
                    Case 2 : Result = 69
                    Case 3 To 5 : Result = 70
                    Case 6 To 8 : Result = 71
                    Case 9 To 16 : Result = 72
                    Case 17 To 27 : Result = 73
                    Case 28 To 40 : Result = 74
                    Case 41 To 50 : Result = 75
                    Case 51 To 56 : Result = 76
                    Case 57 To 61 : Result = 77
                    Case 62 : Result = 78
                    Case 63 : Result = 79
                End Select
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

        Dim HeightinCM As Double = (height * 2.54) 'converts to cm
        Dim HLinCM As Double = (HeightinCM - 80.4 + (0.195 * age) - 6.383) / 5.122 'gets avg HL in cm for that height
        HLinCM /= 2.54
        Return (Math.Round(MT.GetGaussian(HLinCM, 0.454), 2) + 0.75)

    End Function
    ''' <summary>
    ''' Arm Length is on average A person's height * 0.45
    ''' </summary>
    ''' <param name="height"></param>
    ''' <returns></returns>
    Private Shared Function GetArmLength(height As Integer) As Double
        Return Math.Round(MT.GetGaussian((height * 0.435), 0.55), 2)
    End Function
    ''' <summary>
    '''Introversion/Extraversion	        Low Anxiety/High Anxiety	         Receptivity/Tough-Mindedness	         Accommodation/Independence	                Lack of Restraint/Self-Control
    '''A: Reserved/Warm	                    C: Emotionally Stable/Reactive	     A: Warm/Reserved	                     E: Deferential/Dominant	                F: Serious/Lively	                         B: Problem-Solving
    '''F: Serious/Lively	                L: Trusting/Vigilant	             I: Sensitive/Unsentimental	             H: Shy/Bold	                            G: Expedient/Rule-Conscious
    '''H: Shy/Bold	                        O: Self-Assured/Apprehensive	     M: Abstracted/Practical	             L: Trusting/Vigilant	                    M: Abstracted/Practical
    '''N:  Private/Forthright	            Q4: Relaxed/Tense	                 Q1: Open-to-Change/Traditional	         Q1: Traditional/Open-to-Change	            Q3: Tolerates Disorder/Perfectionistic
    '''Q2: Self-Reliant/Group-Oriented
    ''' </summary>

    Public Sub PersonalityModel()
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
                PositiveTraits.Add(BalancedTraits.Item(result))
                BalancedTraits.Remove(BalancedTraits.Item(result))
            Next i

            For i As Integer = 1 To 4 'randomly select 4 negative traits---the remainder stay balanced
                Result = MT.GenerateInt32(0, BalancedTraits.Count - 1)
                NegativeTraits.Add(BalancedTraits.Item(result))
                BalancedTraits.Remove(BalancedTraits.Item(result))
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