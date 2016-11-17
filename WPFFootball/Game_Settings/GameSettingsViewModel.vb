Imports System.ComponentModel
Imports System.Globalization

Public Class GameSettingsViewModel
    Inherits NewGameViewModel
    Implements INotifyPropertyChanged

    Public Sub New()

    End Sub

#Region "INotifyProperyChanged"

    Public Shadows Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    private Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

#End Region

#Region "Private Variables"

    Private _MyStartYear As String
    Private _MyLeagueRules As String
    Private _MyLeagueType As String
    Private _MyRosterSize As String
    Private _MyInactives As String
    Private _MyPracSquadSize As String
    Private _MyOTFormat As String
    Private _MyFieldType As String
    Private _MyPenalties As String
    Private _MyNumTeams As String
    Private _MyNumConf As String
    Private _MyNumDiv As String
    Private _MyFantasyDraft As Boolean
    Private _MyUserFired As Boolean
    Private _MyAllowExpansion As Boolean
    Private _MyAllowRelocation As Boolean
    Private _MyAllowFA As Boolean
    Private _MyAllowDraft As Boolean
    Private _MyNumDraftRounds As String
    Private _AllowSuppDraft As Boolean
    Private _CompPicksForFALoss As Boolean
    Private _MySalCap As Boolean
    Private _MySalCapType As String
    Private _MyLuxuryTax As Boolean
    Private _MyAdjustCap As Boolean
    Private _MyRookiePool As Boolean
    Private _MyCapCarryOver As Boolean
    Private _MyHomeTeamGate As String
    Private _MyLeagueSalCap As Integer
    Private _MyShareLuxBoxRev As Boolean
    Private _MyShareMerchRev As Boolean
    Private _MyPosConMin As Integer
    Private _MyAllowLowerVetMin As Boolean
    Private _MyVetMinNumYears As String
    Private _MyVetMinContract As Integer
    Private _MyPosition As Integer

#End Region

#Region "Public Array Variables"

    Public _MyMinConValue(14) As Integer
    Public _MyFranchise(15) As Integer
    Public _MyVeryGood(15) As Integer
    Public _MyGood(15) As Integer
    Public _MyAverage(15) As Integer
    Public _MyBelowAvg(15) As Integer
    Public _MyDepth(15) As Integer

#End Region

#Region "Public Properties"

    Public Property MyStartYear As String
        Get
            Return _MyStartYear
        End Get
        Set
            _MyStartYear = Value
            OnPropertyChanged("MyStartYear")
        End Set
    End Property

    Public Property MyLeagueRules As String
        Get
            Return _MyLeagueRules
        End Get
        Set
            _MyLeagueRules = Value
            OnPropertyChanged("MyLeagueRules")
        End Set
    End Property

    Public Property MyLeagueType As String
        Get
            Return _MyLeagueType
        End Get
        Set
            _MyLeagueType = Value
            OnPropertyChanged("MyLeagueType")
        End Set
    End Property

    Public Property MyRosterSize as String
        Get
            Return _MyRosterSize
        End Get
        Set
            _MyRosterSize = Value
            OnPropertyChanged("MyRosterSize")
        End Set
    End Property

    Public Property MyInactives As String
        Get
            Return _MyInactives
        End Get
        Set
            _MyInactives = Value
            OnPropertyChanged("MyInactives")
        End Set
    End Property

    Public Property MyPracSquadSize as String
        Get
            Return _MyPracSquadSize
        End Get
        Set
            _MyPracSquadSize = Value
            OnPropertyChanged("MyPracSquadSize")
        End Set
    End Property

    Public Property MyOTFormat As String
        Get
            Return _MyOTFormat
        End Get
        Set
            _MyOTFormat = Value
            OnPropertyChanged("MyOTFormat")
        End Set
    End Property

    Public Property MyFieldType as String
        Get
            Return _MyFieldType
        End Get
        Set
            _MyFieldType = Value
            OnPropertyChanged("MyFieldType")
        End Set
    End Property

    Public Property MyPenalties As String
        Get
            Return _MyPenalties
        End Get
        Set
            _MyPenalties = Value
            OnPropertyChanged("MyPenalties")
        End Set
    End Property

    Public Property MyNumTeams As String
        Get
            Return _MyNumTeams
        End Get
        Set
            _MyNumTeams = Value
            OnPropertyChanged("MyNumTeams")
        End Set
    End Property

    Public Property MyNumConf As String
        Get
            Return _MyNumConf
        End Get
        Set
            _MyNumConf = Value
            OnPropertyChanged("MyNumConf")
        End Set
    End Property

    Public Property MyNumDiv As String
        Get
            Return _MyNumDiv
        End Get
        Set
            _MyNumDiv = Value
            OnPropertyChanged("MyNumDiv")
        End Set
    End Property

    Public Property MyFantasyDraft as Boolean
        Get
            Return _MyFantasyDraft
        End Get
        Set
            _MyFantasyDraft = Value
            OnPropertyChanged("MyFantasyDraft")
        End Set
    End Property

    Public Property MyUserFired as Boolean
        Get
            Return _MyUserFired
        End Get
        Set
            _MyUserFired = Value
            OnPropertyChanged("MyUserFired")
        End Set
    End Property

    Public Property MyAllowExpansion as Boolean
        Get
            Return _MyAllowExpansion
        End Get
        Set
            _MyAllowExpansion = Value
            OnPropertyChanged("MyAllowExpansion")
        End Set
    End Property

    Public Property MyAllowRelocation As Boolean
        Get
            Return _MyAllowRelocation
        End Get
        Set
            _MyAllowRelocation = Value
            OnPropertyChanged("MyAllowRelocation")
        End Set
    End Property

    Public Property MyAllowFA as Boolean
        Get
            Return _MyAllowFA
        End Get
        Set
            _MyAllowFA = Value
            OnPropertyChanged("MyAllowFA")
        End Set
    End Property

    Public Property MyAllowDraft as Boolean
        Get
            Return _MyAllowDraft
        End Get
        Set
            _MyAllowDraft = Value
            OnPropertyChanged("MyAllowDraft")
        End Set
    End Property

    Public Property MyNumDraftRounds as String
        Get
            Return _MyNumDraftRounds
        End Get
        Set
            _MyNumDraftRounds = Value
            OnPropertyChanged("MyNumDraftRounds")
        End Set
    End Property

    Public Property AllowSuppDraft as Boolean
        Get
            Return _AllowSuppDraft
        End Get
        Set
            _AllowSuppDraft = Value
            OnPropertyChanged("AllowSuppDraft")
        End Set
    End Property

    Public Property CompPicksForFALoss As Boolean
        Get
            Return _CompPicksForFALoss
        End Get
        Set
            _CompPicksForFALoss = Value
            OnPropertyChanged("CompPicksForFALoss")
        End Set
    End Property

    Public Property MyDepth as Integer
        Get
            Return _MyDepth(MyPosition).ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _MyDepth(MyPosition) = Value
            OnPropertyChanged("MyDepth")
        End Set
    End Property

    Public Property MyBelowAvg As Integer
        Get
            Return _MyBelowAvg(MyPosition).ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _MyBelowAvg(MyPosition) = Value
            OnPropertyChanged("MyBelowAvg")
        End Set
    End Property

    Public Property MyAverage as Integer
        Get
            Return _MyAverage(MyPosition).ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _MyAverage(MyPosition) = Value
            OnPropertyChanged("MyAverage")
        End Set
    End Property

    Public Property MyGood As Integer
        Get
            Return _MyGood(MyPosition).ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _MyGood(MyPosition) = Value
            OnPropertyChanged("MyGood")
        End Set
    End Property

    Public Property MyVeryGood as Integer
        Get
            Return _MyVeryGood(MyPosition).ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _MyVeryGood(MyPosition) = Value
            OnPropertyChanged("MyVeryGood")
        End Set
    End Property

    Public Property MyFranchise As Integer
        Get
            Return _MyFranchise(MyPosition).ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _MyFranchise(MyPosition) = Value
            OnPropertyChanged("MyFranchise")
        End Set
    End Property

    Public Property MyPosition as Integer
        Get
            Return _MyPosition
        End Get
        Set
            _MyPosition = Value
            OnPropertyChanged("MyPosition")
        End Set
    End Property

    Public Property MyVetMinContract as Integer
        Get
            Return _MyVetMinContract.ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _MyVetMinContract = Value
            OnPropertyChanged("MyVetMinContract")
        End Set
    End Property

    Public Property MyVetMinNumYears as String
        Get
            Return _MyVetMinNumYears
        End Get
        Set
            _MyVetMinNumYears = Value
            OnPropertyChanged("MyVetMinNumYears")
        End Set
    End Property

    Public Property MyAllowLowerVetMin as Boolean
        Get
            Return _MyAllowLowerVetMin
        End Get
        Set
            _MyAllowLowerVetMin = Value
            OnPropertyChanged("MyAllowLowerVetMin")
        End Set
    End Property

    Public Property MyMinConValue as Integer
        Get
            Return _MyMinConValue(MyPosConMin).ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _MyMinConValue(MyPosConMin) = Value
            OnPropertyChanged("MyMinConValue")
        End Set
    End Property

    Public Property MyPosConMin As Integer
        Get
            Return _MyPosConMin
        End Get
        Set
            _MyPosConMin = Value
            OnPropertyChanged("MyPosConMin")
        End Set
    End Property

    Public Property MyShareMerchRev As Boolean
        Get
            Return _MyShareMerchRev
        End Get
        Set
            _MyShareMerchRev = Value
            OnPropertyChanged("MyShareMerchRev")
        End Set
    End Property

    Public Property MyShareLuxBoxRev as Boolean
        Get
            Return _MyShareLuxBoxRev
        End Get
        Set
            _MyShareLuxBoxRev = Value
            OnPropertyChanged("MyShareLuxBoxRev")
        End Set
    End Property

    Public Property MyLeagueSalCap As Integer
        Get
            Return _MyLeagueSalCap.ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _MyLeagueSalCap = Value
            OnPropertyChanged("MyLeagueSalCap")
        End Set
    End Property

    Public Property MyHomeTeamGate as String
        Get
            Return _MyHomeTeamGate
        End Get
        Set
            _MyHomeTeamGate = Value
            OnPropertyChanged("MyHomeTeamGate")
        End Set
    End Property

    Public Property MyCapCarryOver As Boolean
        Get
            Return _MyCapCarryOver
        End Get
        Set
            _MyCapCarryOver = Value
            OnPropertyChanged("MyCapCarryOver")
        End Set
    End Property

    Public Property MyRookiePool As Boolean
        Get
            Return _MyRookiePool
        End Get
        Set
            _MyRookiePool = Value
            OnPropertyChanged("MyRookiePool")
        End Set
    End Property

    Public Property MyAdjustCap as Boolean
        Get
            Return _MyAdjustCap
        End Get
        Set
            _MyAdjustCap = Value
            OnPropertyChanged("MyAdjustCap")
        End Set
    End Property

    Public Property MyLuxuryTax as Boolean
        Get
            Return _MyLuxuryTax
        End Get
        Set
            _MyLuxuryTax = Value
            OnPropertyChanged("MyLuxuryTax")
        End Set
    End Property

    Public Property MySalCapType as String
        Get
            Return _MySalCapType
        End Get
        Set
            _MySalCapType = Value
            OnPropertyChanged("MySalCapType")
        End Set
    End Property

    Public Property MySalCap As Boolean
        Get
            Return _MySalCap
        End Get
        Set
            _MySalCap = Value
            OnPropertyChanged("MySalCap")
        End Set
    End Property

#End Region
End Class