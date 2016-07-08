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

    Private _mystartyear as string
    Private _myleaguerules as string
    Private _myleaguetype as string
    Private _myrostersize as string
    private _myinactives as string
    Private _mypracsquadsize as string
    Private _myotformat As string
    Private _myfieldtype as string
    Private _mypenalties As string
    Private _mynumteams as string
    Private _mynumconf As string
    Private _mynumdiv as string
    Private _myfantasydraft as boolean
    private _myuserfired as Boolean
    Private _myallowexpansion as Boolean
    Private _myallowrelocation as Boolean
    Private _myallowFA As Boolean
    Private _myallowdraft as Boolean
    Private _mynumdraftrounds as string
    private _allowsuppdraft as Boolean
    Private _comppicksforfaloss as Boolean
    Private _mysalcap as Boolean
    Private _mysalcaptype as String
    Private _myluxurytax as Boolean
    Private _myadjustcap as Boolean
    Private _myrookiepool as Boolean
    Private _mycapcarryover as Boolean
    Private _myhometeamgate as String
    private _myleaguesalcap as Integer
    Private _myshareluxboxrev as Boolean
    Private _mysharemerchrev as Boolean
    Private _myposconmin as integer
    private _myallowlowervetmin as Boolean
    private _myvetminnumyears as String
    private _myvetmincontract as integer
    Private _myposition as integer

#End Region

#Region "Public Array Variables"

    Public _myminconvalue(14) as integer
    Public _myfranchise(15) as integer
    Public _myverygood(15) as integer
    Public _mygood(15) as integer
    Public _myaverage(15) as integer
    Public _mybelowavg(15) as integer
    Public _mydepth(15) as integer

#End Region

#Region "Public Properties"

    public Property MyStartYear As String
        Get
            return _mystartyear
        End Get
        Set
            _mystartyear = value
            OnPropertyChanged("MyStartYear")
        End Set
    End Property

    Public Property MyLeagueRules As String
        Get
            return _myleaguerules
        End Get
        Set
            _myleaguerules = value
            OnPropertyChanged("MyLeagueRules")
        End Set
    End Property

    Public Property MyLeagueType As String
        Get
            return _myleaguetype
        End Get
        Set
            _myleaguetype = value
            OnPropertyChanged("MyLeagueType")
        End Set
    End Property

    Public Property MyRosterSize as String
        get
            return _myrostersize
        End Get
        Set
            _myrostersize = value
            OnPropertyChanged("MyRosterSize")
        End Set
    End Property

    Public Property MyInactives As String
        get
            Return _myinactives
        End Get
        Set
            _myinactives = value
            OnPropertyChanged("MyInactives")
        End Set
    End Property

    Public Property MyPracSquadSize as String
        Get
            Return _mypracsquadsize
        End Get
        Set
            _mypracsquadsize = value
            OnPropertyChanged("MyPracSquadSize")
        End Set
    End Property

    Public Property MyOTFormat As String
        get
            return _myotformat
        End Get
        Set
            _myotformat = value
            OnPropertyChanged("MyOTFormat")
        End Set
    End Property

    Public Property MyFieldType as String
        get
            return _myfieldtype
        End Get
        Set
            _myfieldtype = value
            OnPropertyChanged("MyFieldType")
        End Set
    End Property

    Public Property MyPenalties As String
        Get
            Return _mypenalties
        End Get
        Set
            _mypenalties = value
            OnPropertyChanged("MyPenalties")
        End Set
    End Property

    Public Property MyNumTeams As String
        Get
            Return _mynumteams
        End Get
        Set
            _mynumteams = value
            OnPropertyChanged("MyNumTeams")
        End Set
    End Property

    Public Property MyNumConf As String
        Get
            Return _mynumconf
        End Get
        Set
            _mynumconf = value
            OnPropertyChanged("MyNumConf")
        End Set
    End Property

    Public Property MyNumDiv As String
        Get
            Return _mynumdiv
        End Get
        Set
            _mynumdiv = value
            OnPropertyChanged("MyNumDiv")
        End Set
    End Property

    Public Property MyFantasyDraft as Boolean
        Get
            return _myfantasydraft
        End Get
        Set
            _myfantasydraft = value
            OnPropertyChanged("MyFantasyDraft")
        End Set
    End Property

    Public Property MyUserFired as Boolean
        get
            return _myuserfired
        End Get
        Set
            _myuserfired = value
            OnPropertyChanged("MyUserFired")
        End Set
    End Property

    Public Property MyAllowExpansion as Boolean
        get
            return _myallowexpansion
        End Get
        Set
            _myallowexpansion = value
            OnPropertyChanged("MyAllowExpansion")
        End Set
    End Property

    Public Property MyAllowRelocation As Boolean
        Get
            return _myallowrelocation
        End Get
        Set
            _myallowrelocation = value
            OnPropertyChanged("MyAllowRelocation")
        End Set
    End Property

    Public Property MyAllowFA as Boolean
        Get
            return _myallowFA
        End Get
        Set
            _myallowFA = value
            OnPropertyChanged("MyAllowFA")
        End Set
    End Property

    Public Property MyAllowDraft as Boolean
        get
            return _myallowdraft
        End Get
        Set
            _myallowdraft = value
            OnPropertyChanged("MyAllowDraft")
        End Set
    End Property

    Public Property MyNumDraftRounds as String
        get
            return _mynumdraftrounds
        End Get
        Set
            _mynumdraftrounds = value
            OnPropertyChanged("MyNumDraftRounds")
        End Set
    End Property

    Public Property AllowSuppDraft as Boolean
        Get
            return _allowsuppdraft
        End Get
        Set
            _allowsuppdraft = value
            OnPropertyChanged("AllowSuppDraft")
        End Set
    End Property

    Public Property CompPicksForFALoss As Boolean
        Get
            return _comppicksforfaloss
        End Get
        Set
            _comppicksforfaloss = value
            OnPropertyChanged("CompPicksForFALoss")
        End Set
    End Property

    Public Property MyDepth as Integer
        Get
            return _mydepth(MyPosition).tostring("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _mydepth(MyPosition) = value
            OnPropertyChanged("MyDepth")
        End Set
    End Property

    Public Property MyBelowAvg As Integer
        Get
            return _mybelowavg(MyPosition).tostring("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _mybelowavg(MyPosition) = value
            OnPropertyChanged("MyBelowAvg")
        End Set
    End Property

    Public Property MyAverage as Integer
        Get
            Return _myaverage(MyPosition).tostring("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _myaverage(MyPosition) = value
            OnPropertyChanged("MyAverage")
        End Set
    End Property

    Public Property MyGood As Integer
        Get
            return _mygood(MyPosition).tostring("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _mygood(MyPosition) = value
            OnPropertyChanged("MyGood")
        End Set
    End Property

    Public Property MyVeryGood as Integer
        Get
            return _myverygood(MyPosition).tostring("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _myverygood(MyPosition) = value
            OnPropertyChanged("MyVeryGood")
        End Set
    End Property

    Public Property MyFranchise As Integer
        get
            Return _myfranchise(MyPosition).tostring("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _myfranchise(MyPosition) = value
            OnPropertyChanged("MyFranchise")
        End Set
    End Property

    Public Property MyPosition as Integer
        Get
            return _myposition
        End Get
        Set
            _myposition = value
            OnPropertyChanged("MyPosition")
        End Set
    End Property

    Public Property MyVetMinContract as Integer
        Get
            return _myvetmincontract.tostring("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _myvetmincontract = value
            OnPropertyChanged("MyVetMinContract")
        End Set
    End Property

    Public Property MyVetMinNumYears as String
        Get
            Return _myvetminnumyears
        End Get
        Set
            _myvetminnumyears = value
            OnPropertyChanged("MyVetMinNumYears")
        End Set
    End Property

    Public Property MyAllowLowerVetMin as Boolean
        Get
            return _myallowlowervetmin
        End Get
        Set
            _myallowlowervetmin = value
            OnPropertyChanged("MyAllowLowerVetMin")
        End Set
    End Property

    Public Property MyMinConValue as Integer
        Get
            return _myminconvalue(MyPosConMin).ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _myminconvalue(MyPosConMin) = value
            OnPropertyChanged("MyMinConValue")
        End Set
    End Property

    Public Property MyPosConMin As Integer
        Get
            Return _myposconmin
        End Get
        set
            _myposconmin = value
            OnPropertyChanged("MyPosConMin")
        End Set
    End Property

    Public Property MyShareMerchRev As Boolean
        Get
            return _mysharemerchrev
        End Get
        Set
            _mysharemerchrev = value
            OnPropertyChanged("MyShareMerchRev")
        End Set
    End Property

    Public Property MyShareLuxBoxRev as Boolean
        Get
            return _myshareluxboxrev
        End Get
        Set
            _myshareluxboxrev = value
            OnPropertyChanged("MyShareLuxBoxRev")
        End Set
    End Property

    Public Property MyLeagueSalCap As Integer
        Get
            return _myleaguesalcap.ToString("N0", CultureInfo.InvariantCulture)
        End Get
        Set
            _myleaguesalcap = value
            OnPropertyChanged("MyLeagueSalCap")
        End Set
    End Property

    Public Property MyHomeTeamGate as String
        Get
            return _myhometeamgate
        End Get
        Set
            _myhometeamgate = value
            OnPropertyChanged("MyHomeTeamGate")
        End Set
    End Property

    Public Property MyCapCarryOver as Boolean
        Get
            return _mycapcarryover
        End Get
        Set
            _mycapcarryover = value
            OnPropertyChanged("MyCapCarryOver")
        End Set
    End Property

    Public Property MyRookiePool As Boolean
        Get
            return _myrookiepool
        End Get
        Set
            _myrookiepool = value
            OnPropertyChanged("MyRookiePool")
        End Set
    End Property

    Public Property MyAdjustCap as Boolean
        Get
            Return _myadjustcap
        End Get
        Set
            _myadjustcap = value
            OnPropertyChanged("MyAdjustCap")
        End Set
    End Property

    Public Property MyLuxuryTax as Boolean
        Get
            return _myluxurytax
        End Get
        Set
            _myluxurytax = value
            OnPropertyChanged("MyLuxuryTax")
        End Set
    End Property

    Public Property MySalCapType as String
        Get
            return _mysalcaptype
        End Get
        Set
            _mysalcaptype = value
            OnPropertyChanged("MySalCapType")
        End Set
    End Property

    Public Property MySalCap As Boolean
        Get
            return _mysalcap
        End Get
        Set
            _mysalcap = value
            OnPropertyChanged("MySalCap")
        End Set
    End Property

#End Region
End Class