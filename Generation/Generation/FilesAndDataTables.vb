﻿Imports System.IO
''' <summary>
''' Holds any Public variables for use within the Generation project
''' </summary>
Public Module FilesAndDataTables
    Public MT As New Mersenne.MersenneTwister
    Public CoachDT As New DataTable
    Public DraftDT As New DataTable
    Public PersonnelDT As New DataTable
    Public OwnerDT As New DataTable
    Public ScoutDT As New DataTable
    Public ScoutGradeDT As New DataTable
    Public PlayerDT As New DataTable
    Public FirstNames As New DataTable
    Public LastNames As New DataTable
    Public Colleges As New DataTable
    Public Eval As New Evaluation
    Public DraftClass As New ArrayList
    Public DraftClassType As New ArrayList
    Public DraftClassDesc As New List(Of String)
    Public posCount(14, 17) As Integer
    'Public CoachTest As New Coaches
    Public SQLiteTables As New SQLFunctions.SQLiteDataFunctions
    Public MyDB As String = "Football"
    Public MyScoutAssignment As New ScoutAssignment
    Public MyPersonnel As New PersonnelType
    Public MyTrainer As New TrainerType
    Public filepath As String = "Project_Files/"
    Public ReadFName As StreamReader = New StreamReader(filepath + "FName.txt")
    Public ReadLName As StreamReader = New StreamReader(filepath + "LName.txt")
    Public ReadCollege As StreamReader = New StreamReader(filepath + "Colleges.txt")
    Public MyAgent As AgentType
    Public Enum PersonType
        Owner = 1
        Personnel = 2
        Coach = 3
        Player = 4
        Trainer = 5
        Agent = 6
    End Enum
    ''' <summary>
    ''' Sets the assignment of the scout/presonnel person.  1-6 only scout players in their region---these are area scouts.  National scouts the entire nation of college players, selecting key individuals.  Advance scouts the next opponent.  Pro scouts the NFL players.
    ''' All scouts all of them---this would be like a GM, who is responsible for all players
    ''' </summary>
    Public Enum ScoutAssignment
        East = 1
        Atlantic = 2
        South = 3
        Midwest = 4
        Central = 5
        West = 6
        NationalCollege = 7
        Advance = 8
        Pro = 9
        All = 10
    End Enum
    ''' <summary>
    ''' Lists the types of coaches that can be on a staff.  Not all staffs will have every type of coach, although 85% of the staffs will be the same, some have fewer QC coaches than others, etc...
    ''' </summary>
    Public Enum CoachType
        HeadCoach = 1
        AssistantHeadCoach = 2
        OffensiveCoordinator = 3
        DefensiveCoordinator = 4
        SpecialTeamsCoach = 5
        AssistantSpecialTeamsCoach = 6
        QBCoach = 7
        RBCoach = 8
        WRCoach = 9
        AssistantWRCoach = 10
        TECoach = 11
        OLCoach = 12
        AssistantOLCoach = 13
        DLCoach = 14
        AssistantDLCoach = 15
        InsideLBCoach = 16
        OutsideLBCoach = 17
        DBCoach = 18
        AssistantDBCoach = 19
        OQualityControl = 20
        DQualityControl = 21
        StrCondCoach = 22
        AsstStrCondCoach = 23 '2-3 coaches
        OffensiveAssistant = 24
        DefensiveAssistant = 25
    End Enum

    ''' <summary>
    ''' Lists the type of Personnel the person is.
    ''' </summary>
    Public Enum PersonnelType
        GM = 1
        AssistantGM = 2
        DirectorPlayerPersonnel = 3
        AssistantDirPlayerPersonnel = 4
        DirectorProPersonnel = 5
        AssistantDirProPersonnel = 6
        DirectorCollegeScouting = 7
        AssistantDirCollegeScouting = 8
        NationalCollegeScout = 9
        AreaScout = 10
        AdvanceScout = 11
        NatScoutingOrgScout = 12 'Will be BLESTO or NFS depending on what team they are on
        ScoutingAssistant = 13
    End Enum

    Public Enum TrainerType
        HeadTrainer = 1
        AssistantTrainer = 2
        TeamPhysician = 3
        TeamOrthopedist = 4
        Physiotherapist = 5
    End Enum

    Public Enum AgentType
        SuperAgent = 1 'represents 40+ clients
        BigAgent = 2 'respsents 20+ clients
        AboveAvgAgent = 3 'represents 10+ clients
        RespectedAgent = 4 'represents 5-9 clients
        AverageAgent = 5 'represents 1-4 clients
        PoorAgent = 6 'represents no clients
    End Enum

    Public Sub Initialize(ByVal dbName As String, ByVal dt As DataTable, ByVal tableName As String, sqlFieldNames As String, Optional ByVal myFilePath As String = "")
        Try
            SQLiteTables.CreateTable(dbName, dt, tableName, sqlFieldNames, myFilePath)
            SQLiteTables.DeleteTable(dbName, tableName, myFilePath)
            SQLiteTables.LoadTable(dbName, dt, tableName, myFilePath)

            dt.Rows.Add(0)
            LoadData()
        Catch ex As System.ArgumentException
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.Data)
        End Try
    End Sub

    Public Sub Update(ByVal dbName As String, ByVal dt As DataTable, ByVal tableName As String, Optional ByVal myFilePath As String = "")
        Try
            SQLiteTables.BulkInsert(dbName, dt, tableName, myFilePath)
        Catch ex As System.InvalidOperationException
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.Data)
        End Try
    End Sub
    Public Sub LoadData()
        Person.PutDataInDT(FirstNames, ReadFName)
        Person.PutDataInDT(LastNames, ReadLName)
        Person.PutDataInDT(Colleges, ReadCollege)
    End Sub

    ''' <summary>
    ''' Takes in the name of the file, ColumnName str array, and a DT.  Reads each line of the file, splits it by a ";" and then adds in the array to the DT
    ''' after calling the GetColumnNames Sub to take the parameter array and create columns. Optional Parameters for DateFormatting string and the index which which to apply it
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <param name="ColumnNames"></param>
    ''' <param name="DT"></param>
    ''' <param name="DateFormatPattern"></param>
    ''' <param name="DateFormatIndex"></param>
    Public Sub ReadFile(ByVal fileName As String, ByVal columnNames() As String, ByVal dt As DataTable, Optional dateFormatPattern As String = "",
                         Optional dateFormatIndex As Integer = -1, Optional orderBy As String = "")
        Dim Str As String = ""
        Dim MyArray() As String

        SQLFunctions.SQLiteDataFunctions.GetColumnNames(dt, columnNames)
        'Automatically dispose of the StreamReader once its completed.
        Using MyReader As New StreamReader(fileName)
            'First Line contains information about formatting and not actual content
            MyReader.ReadLine()
            While MyReader.EndOfStream = False
                Str = MyReader.ReadLine()
                MyArray = Str.Split(";")
                'Checks to see if there is a Date involved and if there is, formats it to its proper form before returning it back to the array
                If dateFormatPattern <> "" Then
                    Dim MyDate As New Date
                    MyDate = MyArray(dateFormatIndex)
                    MyArray(dateFormatIndex) = MyDate.ToString(dateFormatPattern)
                End If

                'Adds the array to the data row and sorts it according to the parameter supplied
                dt.Rows.Add().ItemArray = MyArray
                dt.Select("", orderBy, DataViewRowState.CurrentRows)
            End While
        End Using
    End Sub

End Module