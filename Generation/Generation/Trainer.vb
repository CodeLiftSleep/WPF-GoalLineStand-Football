Public Class Trainer
    Inherits Person
    Public TrainerSQLString As String = "TrainerID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Age int NULL,  DOB varchar(12) NULL, Height int NULL, Weight int NULL, Experience int NULL,
TrainerType varchar(20) NULL, TreatHeadInj int NULL, TreatNeckInj int NULL, TreatShoulderInj int NULL, TreatArmInj int NULL, TreatWristInj int NULL, TreatHandInj int NULL, TreatChestInj int NULL, TreatBackInj int NULL, TreatCoreInj int NULL,
TreatHipInj int NULL, TreatQuadInj int NULL, TreatHamstringInj int NULL, TreatCalfInj int NULL, TreatAnkleInj int NULL, TreatFootInj int NULL, TreatKneeInj int NULL, TreatSpinalInj int NULL, DiagnoseInj int NULL, SurgicallyRepair int NULL, " + PersonSQLString
    Sub New()

    End Sub

    Public Sub GenTrainers(ByVal TrainerNum As Integer, ByVal XTrainer As Trainer, ByVal TrainerDT As DataTable)
        XTrainer = New Trainer

        TrainerDT.Rows.Add(TrainerNum)
        GenNames(TrainerDT, TrainerNum, "Trainer")
        TrainerDT.Rows(TrainerNum).Item("Experience") = MT.GenerateInt32(0, (TrainerDT.Rows(TrainerNum).Item("Age") - 22))
        GetPersonalityStats(TrainerDT, TrainerNum, XTrainer)
        TrainerDT.Rows(TrainerNum).Item("TrainerType") = String.Format("'{0}'", GetTrainerType())
        GetTrainerSkills(TrainerDT, TrainerNum, MyTrainer)
        TrainerDT.Rows(TrainerNum).Item("TeamID") = 0

    End Sub

    Public Sub GetTrainerSkills(ByVal TrainerDT As DataTable, ByVal TrainerNum As Integer, ByVal TrainerType As Integer)
        Select Case TrainerType
            Case 1 'Head Trainer---helps during the game, help during the week with injury treatment, etc
                TrainerDT.Rows(TrainerNum).Item("TreatHeadInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatNeckInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatShoulderInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatArmInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHandInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatWristInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatBackInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatSpinalInj") = MT.GetGaussian(25, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatChestInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatCoreInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHipInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatQuadInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatKneeInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHamstringInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatCalfInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatAnkleInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatFootInj") = MT.GetGaussian(75, 8.33)
                TrainerDT.Rows(TrainerNum).Item("DiagnoseInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("SurgicallyRepair") = 0

            Case 2 'Assistant Trainer---helps determine the recovery time for players
                TrainerDT.Rows(TrainerNum).Item("TreatHeadInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatNeckInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatShoulderInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatArmInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHandInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatWristInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatBackInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatSpinalInj") = MT.GetGaussian(15, 5)
                TrainerDT.Rows(TrainerNum).Item("TreatChestInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatCoreInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHipInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatQuadInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatKneeInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHamstringInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatCalfInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatAnkleInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatFootInj") = MT.GetGaussian(50, 8.33)
                TrainerDT.Rows(TrainerNum).Item("DiagnoseInj") = MT.GetGaussian(40, 8.33)
                TrainerDT.Rows(TrainerNum).Item("SurgicallyRepair") = 0

            Case 3 'Team Physician---treats general injuries and provides diagnoses
                TrainerDT.Rows(TrainerNum).Item("TreatHeadInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatNeckInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatShoulderInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatArmInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHandInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatWristInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatBackInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatSpinalInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatChestInj") = MT.GetGaussian(80, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatCoreInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHipInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatQuadInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatKneeInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHamstringInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatCalfInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatAnkleInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("TreatFootInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("DiagnoseInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("SurgicallyRepair") = 0

            Case 4 'Team Orthopedist---handles serious injuries like Knee, Shoulder, surgeries, etc---helps determine how long players are out for initially
                TrainerDT.Rows(TrainerNum).Item("TreatHeadInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatNeckInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatShoulderInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatArmInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatHandInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatWristInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatBackInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatSpinalInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatChestInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatCoreInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatHipInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatQuadInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatKneeInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatHamstringInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatCalfInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatAnkleInj") = 0
                TrainerDT.Rows(TrainerNum).Item("TreatFootInj") = 0
                TrainerDT.Rows(TrainerNum).Item("DiagnoseInj") = MT.GetGaussian(80, 6.33)
                TrainerDT.Rows(TrainerNum).Item("SurgicallyRepair") = MT.GetGaussian(75, 8.33)

            Case 5 'Physiotherapist---helps players recover during the week
                TrainerDT.Rows(TrainerNum).Item("TreatHeadInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatNeckInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatShoulderInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatArmInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHandInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatWristInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatBackInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatSpinalInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatChestInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatCoreInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHipInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatQuadInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatKneeInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatHamstringInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatCalfInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatAnkleInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("TreatFootInj") = MT.GetGaussian(60, 8.33)
                TrainerDT.Rows(TrainerNum).Item("DiagnoseInj") = 0
                TrainerDT.Rows(TrainerNum).Item("SurgicallyRepair") = 0
        End Select
    End Sub
    Public Function GetTrainerType() As String
        Dim result As String = ""
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 10
                result = "HeadTrainer"
                MyTrainer = 1
            Case 11 To 20
                result = "TeamPhysician"
                MyTrainer = 3
            Case 21 To 30
                result = "TeamOrthopedist"
                MyTrainer = 4
            Case 31 To 50
                result = "TeamPhysiotherapist"
                MyTrainer = 5
            Case Else
                result = "AssistantTrainer"
                MyTrainer = 2
        End Select
        Return result
    End Function

    Public Sub PutTrainerOnTeam(trainerDT As DataTable)
        Dim num As Integer = MT.GenerateInt32(1, trainerDT.Rows.Count - 1)
        Dim HTList As New List(Of Integer)
        Dim ATList As New List(Of Integer)
        Dim PTlist As New List(Of Integer)
        Dim DocList As New List(Of Integer)
        Dim OrthList As New List(Of Integer)
        Dim result As Integer = 0
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
            result = MT.GenerateInt32(0, HTList.Count - 1)
            trainerDT.Rows(HTList.Item(result)).Item("TeamID") = i
            HTList.RemoveAt(result)
            'choose 3 asst trainers
            For x = 1 To 3
                result = MT.GenerateInt32(0, ATList.Count - 1)
                trainerDT.Rows(ATList.Item(result)).Item("TeamID") = i
                ATList.RemoveAt(result)
            Next x
            'choose a team physician
            result = MT.GenerateInt32(0, DocList.Count - 1)
            trainerDT.Rows(DocList.Item(result)).Item("TeamID") = i
            DocList.RemoveAt(result)
            'choose a team orthopedist
            result = MT.GenerateInt32(0, OrthList.Count - 1)
            trainerDT.Rows(OrthList.Item(result)).Item("TeamID") = i
            OrthList.RemoveAt(result)
            'choose 2 physiotherapists
            result = MT.GenerateInt32(0, PTlist.Count - 1)
            trainerDT.Rows(PTlist.Item(result)).Item("TeamID") = i
            PTlist.RemoveAt(result)
        Next i
    End Sub
End Class