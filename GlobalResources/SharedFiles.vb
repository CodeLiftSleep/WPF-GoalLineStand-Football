Public Module SharedFiles
    Public TeamDT As New DataTable
    Public OwnerDT As New DataTable
    Public PersonnelDT As New DataTable
    Public CoachDT As New DataTable
    Public PlayerDT As New DataTable
    Public DraftDT As New DataTable
    Public DraftGradesDT As New DataTable 'DT for all college draft grades
    Public ProGradesDT As New DataTable 'DT for all player draft grades
    Public Football As New DataSet

    ''' <summary>
    ''' Since an Enum can only take an integer and we need it to take a double, we are multiplying the needed round grade by 100--to use simply divide the
    ''' Enum value by 100 and compare it to the grade
    ''' </summary>
    Public Enum PlayerGrades
        FirstEarlyRound = 900
        FirstMidRound = 850
        FirstLateRound = 800
        SecondRound = 750
        ThirdRound = 700
        FourthRound = 650
        FifthRound = 600
        SixthRound = 550
        SeventhRound = 525
        PUFA = 500 'Priority UFA
        LUFA = 475 'Lower UFA
        PSQuad = 450
        Reject = 0 'An NFL reject is anything under 450
    End Enum
    Public Enum OffSystem
        WCP = 1
        WCR
        WCB
        SMA
        SPP
        SPR
        SPB
        RNS
        MOP
        MOR
        MOB
    End Enum
    Public Enum DefSystem
        A43 = 1
        C43
        B43
        SR43
        A34
        C34
        B34
        SR34
        C2A
        C2C
        C2B
        HYB
    End Enum

    Sub Main()

    End Sub

End Module