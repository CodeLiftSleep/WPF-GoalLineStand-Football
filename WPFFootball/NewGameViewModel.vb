Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports WPFFootball.My.Resources

''' <summary>
'''     Subs and Functions that are used by more than one window.
''' </summary>
Public Class NewGameViewModel
    Implements INotifyPropertyChanged

    Public TeamEnumList As New Teams

    Public Sub New()

        MyBackgroundImg = New BitmapImage(New Uri(GetBackgroundFilePath,
                                                  UriKind.RelativeOrAbsolute))
    End Sub

#Region "INotifyPropertyChanged"

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    private Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

#End Region

#Region "Private Variables"
    Private _myhelmet as Imagesource
    private _primcolor as Brush
    Private _seccolor as Brush
    private _trimcolor as Brush
    private _mystadiumname as string
    private _mystadiumcapacity As String
    private _mycitystate As String
    private _mystadiumpic as ImageSource
    private _myavgattendance as string
    private _myteamrecord as string
    private _mybackgroundimg as imagesource
    private _MyDT as New ObservableCollection(Of DataTable)

#End Region

#Region "Public Properties"

    public Property MyStadiumName as String
        Get
            return _mystadiumname
        End Get
        Set
            _mystadiumname = value
            OnPropertyChanged("MyStadiumName")
        End Set
    End Property

    public Property MyPrimColor as Brush
        Get
            Return _primcolor
        End Get
        Set
            _primcolor = value
            OnPropertyChanged("MyPrimColor")
        End Set
    End Property

    public Property MyTrimColor as Brush
        Get
            Return _trimcolor
        End Get
        Set
            _trimcolor = value
            OnPropertyChanged("MyTrimColor")
        End Set
    End Property

    Public Property MySecColor as Brush
        Get
            Return _seccolor
        End Get
        Set
            _seccolor = value
            OnPropertyChanged("MySecColor")
        End Set
    End Property

    Public Property MyDTProperty As ObservableCollection(Of DataTable)
        Get
            Return _MyDT
        End Get
        Set
            _MyDT = value
            OnPropertyChanged("MyDTProperty")
        End Set
    End Property

    public Property MyStadiumCapacity as String
        Get

            return _MyStadiumCapacity
        End Get
        Set
            _MyStadiumCapacity = value
            OnPropertyChanged("MyStadiumCapacity")
        End Set
    End Property

    public Property MyCityState as String
        get
            return _mycitystate
        End Get
        Set
            _mycitystate = value
            OnPropertyChanged("MyCityState")
        End Set
    End Property

    public Property MyStadiumPic as ImageSource
        Get
            Return _mystadiumpic
        End Get
        Set
            _mystadiumpic = value
            OnPropertyChanged("MyStadiumPic")
        End Set
    End Property

    Public Property MyHelmet As ImageSource
        Get
            Return _myhelmet
        End Get
        Set(value As ImageSource)
            _myhelmet = value
            OnPropertyChanged("MyHelmet")
        End Set
    End Property

    public Property MyAvgAttendance as String
        Get
            return _myavgattendance
        End Get
        Set
            _myavgattendance = value
            OnPropertyChanged("MyAvgAttendance")
        End Set
    End Property

    public Property MyTeamRecord as string
        Get
            return _myteamrecord
        End Get
        Set
            _myteamrecord = value
            OnPropertyChanged("MyTeamRecord")
        End Set
    End Property

    public property MyBackgroundImg as imagesource
        get
            return _mybackgroundimg
        End Get
        Set
            _mybackgroundimg = value
            OnPropertychanged("MyBackgroundImg")
        End Set
    End Property

#End Region

#Region "Enums"

    public Enum DivisionNames
        <Description("AFC East")> AFCE = 1
        <Description("AFC North")> AFCN = 2
        <Description("AFC South")> AFCS = 3
        <Description("AFC West")> AFCW = 4
        <Description("NFC East")> NFCE = 5
        <Description("NFC North")> NFCN = 6
        <Description("NFC South")> NFCS = 7
        <Description("NFC West")> NFCW = 8
    End Enum

    Public Enum Teams

        <Description("Buffalo Bills")> BUF = 1
        <Description("New England Patriots")> NWE = 2
        <Description("New York Jets")> NYJ = 3
        <Description("Miami Dolphins")> MIA = 4
        <Description("Cincinnati Bengals")> CIN = 5
        <Description("Pittsburgh Steelers")> PIT = 6
        <Description("Baltimore Ravens")> BAL = 7
        <Description("Cleveland Browns")> CLE = 8
        <Description("Houston Texans")> HOU = 9
        <Description("Indianapolis Colts")> IND = 10
        <Description("Jacksonville Jaguars")> JAX = 11
        <Description("Tennessee Titans")> TEN = 12
        <Description("Denver Broncos")> DEN = 13
        <Description("Kansas City Chiefs")> KC = 14
        <Description("Oakland Raiders")> OAK = 15
        <Description("San Diego Chargers")> SDO = 16
        <Description("Washington Redskins")> WAS = 17
        <Description("Philadelphia Eagles")> PHI = 18
        <Description("New York Giants")> NYG = 19
        <Description("Dallas Cowboys")> DAL = 20
        <Description("Minnesota Vikings")> MIN = 21
        <Description("Green Bay Packers")> GNB = 22
        <Description("Detroit Lions")> DET = 23
        <Description("Chicago Bears")> CHI = 24
        <Description("Carolina Panthers")> CAR = 25
        <Description("Atlanta Falcons")> ATL = 26
        <Description("New Orleans Saints")> NWO = 27
        <Description("Tampa Bay Buccaneers")> TAM = 28
        <Description("Arizona Cardinals")> ARI = 29
        <Description("Seattle Seahawks")> SEA = 30
        <Description("Los Angeles Rams")> LAR = 31
        <Description("San Francisco 49ers")> SFO = 32
    End Enum

#End Region

    ''' <summary>
    '''     Sets the background picture of the screen
    ''' </summary>
    ''' <param name="TeamNum"></param>
    ''' <returns></returns>
    Public Shared Function GetBackgroundFilePath(optional byval TeamNum As Integer = 32) As String
        Dim filepath = "pack://application:,,,/Project Files/"

        Select Case TeamNum
            Case 0 : filepath += Bills02Jpg
            Case 1 : filepath += Patriots2Jpg
            Case 2 : filepath += JetsJpg
            Case 3 : filepath += Dolphins_2013Jpg
            Case 4 : filepath += Bengals3Jpg
            Case 5 : filepath += Steelers2Jpg
            Case 6 : filepath += Ravens3Jpg
            Case 7 : filepath += Browns2Jpg1
            Case 8 : filepath += Texans2Jpg
            Case 9 : filepath += Colts2Jpg
            Case 10 : filepath += Jaguars2Jpg
            Case 11 : filepath += Titans2Jpg
            Case 12 : filepath += Broncos2Jpg
            Case 13 : filepath += Chiefs3Jpg
            Case 14 : filepath += RaidersJpg
            Case 15 : filepath += Chargers5Jpg
            case 16 : filepath += Redskins2Jpg
            case 17 : filepath += Eagles2Jpg
            case 18 : filepath += Giants5Jpg
            case 19 : filepath += Cowboys3Jpg
            case 20 : filepath += Vikings_2013_06Jpg
            case 21 : filepath += Packers5Jpg
            case 22 : filepath += Lions2Jpg
            case 23 : filepath += Bears4Jpg
            Case 24 : filepath += Panthers2Jpg
            case 25 : filepath += FalconsJpg
            case 26 : filepath += Saints2Jpg
            case 27 : filepath += Buccaneers2Jpg
            case 28 : filepath += Cardinals3Jpg
            case 29 : filepath += Seahawks2_2012Jpg
            case 30 : filepath += RamsJpg
            case 31 : filepath += _49ers04Jpg
            case 32 : filepath += GlobalClass_GetBackgroundFilePath_FootballGoalLine_jpg
        End Select

        Return filepath
    End Function

    ''' <summary>
    '''     Sets the helmet image of the team on the button
    ''' </summary>
    ''' <param name="TeamNum"></param>
    ''' <returns></returns>

    Public Shared Function GetImage(TeamNum As Integer) As Image
        Dim MyImage As New Image
        Dim filepath = "Project Files/"

        Select Case TeamNum
            Case 0 : filepath += Bills_PHelmet_2011Jpg
            Case 1 : filepath += Patriots_PHelmetJpg
            Case 2 : filepath += Jets_PHelmetJpg
            Case 3 : filepath += Dolphins_PHelmetJpg
            Case 4 : filepath += Bengals_PHelmetJpg
            Case 5 : filepath += Steelers_PHelmetJpg
            Case 6 : filepath += Ravens_PHelmetJpg
            Case 7 : filepath += Browns_PHelmetJpg
            Case 8 : filepath += Texans_PHelmetJpg
            Case 9 : filepath += Colts_PHelmetJpg
            Case 10 : filepath += Jaguars_PHelmetJpg
            Case 11 : filepath += Titans_PHelmetJpg
            Case 12 : filepath += Broncos_PHelmetJpg
            Case 13 : filepath += Chiefs_PHelmetJpg
            Case 14 : filepath += Raiders_HelmetJpg
            Case 15 : filepath += Chargers_PHelmet2Jpg
            Case 16 : filepath += Redskins_PHelmetJpg
            Case 17 : filepath += Eagles_PHelmetJpg
            Case 18 : filepath += Giants_PHelmetJpg
            Case 19 : filepath += Cowboys_PhelmetJpg
            Case 20 : filepath += Vikings_PHelmet_2013Jpg
            Case 21 : filepath += Packers_PHelmetJpg
            Case 22 : filepath += Lions_PHelmetJpg
            Case 23 : filepath += Bears_PHelmet2Jpg
            Case 24 : filepath += Panthers_PHelmetJpg
            Case 25 : filepath += Falcons_PHelmetJpg
            Case 26 : filepath += Saints_PHelmetJpg
            Case 27 : filepath += Buccaneers_PHelmetJpg
            Case 28 : filepath += Cardinals_HelmetJpg
            Case 29 : filepath += Seahawks_PHelmet_2012Jpg
            Case 30 : filepath += Rams1Png
            Case 31 : filepath += _49ers_PHelmet_NewJpg
        End Select
        MyImage.Source = New BitmapImage(New Uri(filepath, UriKind.Relative))
        Return MyImage
    End Function

    ''' <summary>
    '''     Loads picture of the proper stadium by team
    ''' </summary>
    ''' <param name="TeamNum"></param>
    ''' <returns></returns>
    Public Shared Function GetStadiumPic(TeamNum As integer) As String
        dim FilePath = "pack://application:,,,/Project Files/"
        select case TeamNum
            Case 0 : FilePath += RalphWilsonStadiumJpg
            Case 1 : FilePath += GilletteStadiumJpg
            case 2, 18 : FilePath += MetLife_StadiumJpg
            case 3 : FilePath += SunLifeStadiumJpg
            case 4 : FilePath += PaulBrownStadiumJpg
            case 5 : FilePath += HeinzFieldJpg
            case 6 : FilePath += MTBankStadiumJpg
            case 7 : FilePath += FirstEnergyStadiumJpg
            case 8 : FilePath += NRGStadiumJpg
            case 9 : FilePath += LucasOilStadiumJpg
            case 10 : FilePath += EverBankFieldJpg
            case 11 : FilePath += NissanStadiumJpg
            case 12 : Filepath += SportsAuthorityFieldJpg
            case 13 : FilePath += ArrowheadStadiumJpg
            case 14 : FilePath += OaklandColiseumJpg
            case 15 : FilePath += QualcommStadiumJpg
            case 16 : FilePath += FedexFieldJpg
            case 17 : FilePath += LincolnFinancialFieldJpg
            case 19 : FilePath += ATTStadiumJpg
            case 20 : FilePath += USBankStadiumJpg
            case 21 : FilePath += LambeaufieldJpg
            case 22 : FilePath += FordfieldJpg
            case 23 : FilePath += SoldierFieldJpg
            case 24 : FilePath += BankOfAmericaStadiumJpg
            case 25 : FilePath += GeorgiaDomePng
            case 26 : FilePath += Superdome_saintsJpg
            case 27 : FilePath += RaymondJamesStadiumJpg
            case 28 : FilePath += University_phoenix_stadiumJpg
            case 29 : FilePath += CenturyLinkFieldJpg
            case 30 : FilePath += LosAngelesColiseumJpg
            case 31 : FilePath += LevisStadiumJpg
        End Select
        return filepath
    End Function

    Public shared function GetBrush(TeamNum As integer, MyQueue As Queue, TeamDT As datatable) As Queue
        TeamNum += 1
        for i = 0 To TeamDT.Rows.Count - 1
            if TeamDT.Rows(i).Item("TeamID") = TeamNum Then
                myQueue.enqueue(TeamDT.Rows(i).Item("MainColor"))
                myQueue.Enqueue(TeamDT.Rows(i).Item("SecondaryColor"))
                myQueue.Enqueue(TeamDT.Rows(i).Item("TrimColor"))
                Exit For
            End If
        Next i
        Return myQueue
    End function

    Public Shared Function ConvertColor(HexString As String) As Brush
        Dim converter = New BrushConverter()
        dim myBrush As Brush
        myBrush = DirectCast(converter.ConvertFromString(HexString), Brush)
        return MyBrush
    End Function


End Class