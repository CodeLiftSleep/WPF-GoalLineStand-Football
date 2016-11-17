Public Class Owner
    Inherits Person
    Public OwnerSQLString As String = "OwnerID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Height int NULL, Weight int NULL, Age int NULL, DOB varchar(12) NULL,
 Experience int NULL, OwnerRep int NULL, PersonalWealth int NULL, " + PersonSQLString
    ''' <summary>
    ''' Generates the number of owners/potential owners specified for the league
    ''' only 32 are placed with a team, the other owners are "potential owners" that can
    ''' buy the team from the current owner
    ''' </summary>
    ''' <param name="OwnerNum"></param>
    ''' <param name="XOwner"></param>
    ''' <param name="OwnerDT"></param>

    Public Sub GenOwners(ByVal OwnerNum As Integer, ByVal XOwner As Owner, ByVal OwnerDT As DataTable)

        XOwner = New Owner

        OwnerDT.Rows.Add(OwnerNum)
        GenNames(OwnerDT, OwnerNum, "Owner") 'Gets first and last name, college, Age, DOB, Height and Weight
        GetPersonalityStats(OwnerDT, OwnerNum, XOwner)

        OwnerDT.Rows(OwnerNum).Item("TeamID") = 0
        OwnerDT.Rows(OwnerNum).Item("OwnerRep") = MT.GetGaussian(49.5, 16.5)
        OwnerDT.Rows(OwnerNum).Item("Experience") = MT.GenerateInt32(1, 50)
        OwnerDT.Rows(OwnerNum).Item("PersonalWealth") = MT.GetGaussian(49.5, 16.5) '<---affects how much money he has for signing bonuses

    End Sub
    Public Sub GetTeamOwner(ByVal OwnerDT As DataTable)
        Dim result As Integer = MT.GenerateInt32(1, OwnerDT.Rows.Count - 1)
        For i As Integer = 1 To 32

            While OwnerDT.Rows(result).Item("TeamID") <> 0
                result = MT.GenerateInt32(1, OwnerDT.Rows.Count - 1)
            End While

            OwnerDT.Rows(result).Item("TeamID") = i
        Next i

    End Sub
End Class