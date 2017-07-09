Public Class Trainer
    Inherits Person
    Public TrainerSQLString As String = "TrainerID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Age int NULL,  DOB varchar(12) NULL, Height int NULL, Weight int NULL, Experience int NULL,
TrainerType varchar(20) NULL, TreatHeadInj int NULL, TreatNeckInj int NULL, TreatShoulderInj int NULL, TreatArmInj int NULL, TreatWristInj int NULL, TreatHandInj int NULL, TreatChestInj int NULL, TreatBackInj int NULL, TreatCoreInj int NULL,
TreatHipInj int NULL, TreatQuadInj int NULL, TreatHamstringInj int NULL, TreatCalfInj int NULL, TreatAnkleInj int NULL, TreatFootInj int NULL, TreatKneeInj int NULL, TreatSpinalInj int NULL, DiagnoseInj int NULL, SurgicallyRepair int NULL, " + PersonSQLString
    Sub New()

    End Sub

    Public Sub GenTrainers(ByVal trainerNum As Integer, ByVal xTrainer As Trainer, ByVal trainerDT As DataTable)
        xTrainer = New Trainer
        PersonalityModel(trainerDT, trainerNum, xTrainer)
        trainerDT.Rows.Add(trainerNum)
        GenNames(trainerDT, trainerNum, "Trainer", 0)
        trainerDT.Rows(trainerNum).Item("Experience") = MT.GenerateInt32(0, (trainerDT.Rows(trainerNum).Item("Age") - 22))
        'GetPersonalityStats()
        trainerDT.Rows(trainerNum).Item("TrainerType") = String.Format("'{0}'", GetTrainerType())
        GetTrainerSkills(trainerDT, trainerNum, MyTrainer)
        trainerDT.Rows(trainerNum).Item("TeamID") = 0

    End Sub

    Public Sub GetTrainerSkills(ByVal trainerDT As DataTable, ByVal trainerNum As Integer, ByVal trainerType As Integer)
        Select Case trainerType
            Case 1 'Head Trainer---helps during the game, help during the week with injury treatment, etc
                trainerDT.Rows(trainerNum).Item("TreatHeadInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatNeckInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatShoulderInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatArmInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatHandInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatWristInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatBackInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatSpinalInj") = MT.GetGaussian(25, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatChestInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatCoreInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatHipInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatQuadInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatKneeInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatHamstringInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatCalfInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatAnkleInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatFootInj") = MT.GetGaussian(75, 8.33)
                trainerDT.Rows(trainerNum).Item("DiagnoseInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("SurgicallyRepair") = 0

            Case 2 'Assistant Trainer---helps determine the recovery time for players
                trainerDT.Rows(trainerNum).Item("TreatHeadInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatNeckInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatShoulderInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatArmInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatHandInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatWristInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatBackInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatSpinalInj") = MT.GetGaussian(15, 5)
                trainerDT.Rows(trainerNum).Item("TreatChestInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatCoreInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatHipInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatQuadInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatKneeInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatHamstringInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatCalfInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatAnkleInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatFootInj") = MT.GetGaussian(50, 8.33)
                trainerDT.Rows(trainerNum).Item("DiagnoseInj") = MT.GetGaussian(40, 8.33)
                trainerDT.Rows(trainerNum).Item("SurgicallyRepair") = 0

            Case 3 'Team Physician---treats general injuries and provides diagnoses
                trainerDT.Rows(trainerNum).Item("TreatHeadInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatNeckInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatShoulderInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatArmInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatHandInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatWristInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatBackInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatSpinalInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatChestInj") = MT.GetGaussian(80, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatCoreInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatHipInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatQuadInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatKneeInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatHamstringInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatCalfInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatAnkleInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("TreatFootInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("DiagnoseInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("SurgicallyRepair") = 0

            Case 4 'Team Orthopedist---handles serious injuries like Knee, Shoulder, surgeries, etc---helps determine how long players are out for initially
                trainerDT.Rows(trainerNum).Item("TreatHeadInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatNeckInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatShoulderInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatArmInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatHandInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatWristInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatBackInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatSpinalInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatChestInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatCoreInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatHipInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatQuadInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatKneeInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatHamstringInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatCalfInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatAnkleInj") = 0
                trainerDT.Rows(trainerNum).Item("TreatFootInj") = 0
                trainerDT.Rows(trainerNum).Item("DiagnoseInj") = MT.GetGaussian(80, 6.33)
                trainerDT.Rows(trainerNum).Item("SurgicallyRepair") = MT.GetGaussian(75, 8.33)

            Case 5 'Physiotherapist---helps players recover during the week
                trainerDT.Rows(trainerNum).Item("TreatHeadInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatNeckInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatShoulderInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatArmInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatHandInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatWristInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatBackInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatSpinalInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatChestInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatCoreInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatHipInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatQuadInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatKneeInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatHamstringInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatCalfInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatAnkleInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("TreatFootInj") = MT.GetGaussian(60, 8.33)
                trainerDT.Rows(trainerNum).Item("DiagnoseInj") = 0
                trainerDT.Rows(trainerNum).Item("SurgicallyRepair") = 0
        End Select
    End Sub
    Public Function GetTrainerType() As String
        Dim Result As String = ""
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 10
                Result = "HeadTrainer"
                MyTrainer = 1
            Case 11 To 20
                Result = "TeamPhysician"
                MyTrainer = 3
            Case 21 To 30
                Result = "TeamOrthopedist"
                MyTrainer = 4
            Case 31 To 50
                Result = "TeamPhysiotherapist"
                MyTrainer = 5
            Case Else
                Result = "AssistantTrainer"
                MyTrainer = 2
        End Select
        Return Result
    End Function

    Public Sub PutTrainerOnTeam(trainerDT As DataTable)
        Dim Num As Integer = MT.GenerateInt32(1, trainerDT.Rows.Count - 1)
        Dim HTList As New List(Of Integer)
        Dim ATList As New List(Of Integer)
        Dim PTlist As New List(Of Integer)
        Dim DocList As New List(Of Integer)
        Dim OrthList As New List(Of Integer)
        Dim Result As Integer = 0
        Dim TType As String = ""

        For i As Integer = 1 To trainerDT.Rows.Count - 1
            TType = trainerDT.Rows(i).Item("TrainerType")
            Select Case TType
                Case "'HeadTrainer'"
                    HTList.Add(i)
                Case "'AssistantTrainer'"
                    ATList.Add(i)
                Case "'TeamPhysician'"
                    DocList.Add(i)
                Case "'TeamOrthopedist'"
                    OrthList.Add(i)
                Case "'TeamPhysiotherapist'"
                    PTlist.Add(i)
            End Select
        Next i

        For i As Integer = 1 To 32
            'choose a head trainer
            Result = MT.GenerateInt32(0, HTList.Count - 1)
            trainerDT.Rows(HTList.Item(Result)).Item("TeamID") = i
            HTList.RemoveAt(Result)
            'choose 3 asst trainers
            For x = 1 To 3
                Result = MT.GenerateInt32(0, ATList.Count - 1)
                trainerDT.Rows(ATList.Item(Result)).Item("TeamID") = i
                ATList.RemoveAt(Result)
            Next x
            'choose a team physician
            Result = MT.GenerateInt32(0, DocList.Count - 1)
            trainerDT.Rows(DocList.Item(Result)).Item("TeamID") = i
            DocList.RemoveAt(Result)
            'choose a team orthopedist
            Result = MT.GenerateInt32(0, OrthList.Count - 1)
            trainerDT.Rows(OrthList.Item(Result)).Item("TeamID") = i
            OrthList.RemoveAt(Result)
            'choose 2 physiotherapists
            Result = MT.GenerateInt32(0, PTlist.Count - 1)
            trainerDT.Rows(PTlist.Item(Result)).Item("TeamID") = i
            PTlist.RemoveAt(Result)
        Next i
    End Sub
End Class