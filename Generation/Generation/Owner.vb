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

    Public Sub GenOwners(ByVal ownerNum As Integer, ByVal xOwner As Owner, ByVal ownerDT As DataTable)

        xOwner = New Owner

        ownerDT.Rows.Add(ownerNum)
        GenNames(ownerDT, ownerNum, "Owner") 'Gets first and last name, college, Age, DOB, Height and Weight
        GetPersonalityStats(ownerDT, ownerNum, xOwner)

        ownerDT.Rows(ownerNum).Item("TeamID") = 0
        ownerDT.Rows(ownerNum).Item("OwnerRep") = MT.GetGaussian(49.5, 16.5)
        ownerDT.Rows(ownerNum).Item("Experience") = MT.GenerateInt32(1, 50)
        ownerDT.Rows(ownerNum).Item("PersonalWealth") = MT.GetGaussian(49.5, 16.5) '<---affects how much money he has for signing bonuses

    End Sub
    Public Sub GetTeamOwner(ByVal ownerDT As DataTable)
        Dim Result As Integer = MT.GenerateInt32(1, ownerDT.Rows.Count - 1)
        For i As Integer = 1 To 32

            While ownerDT.Rows(Result).Item("TeamID") <> 0
                Result = MT.GenerateInt32(1, ownerDT.Rows.Count - 1)
            End While

            ownerDT.Rows(Result).Item("TeamID") = i
        Next i

    End Sub
End Class