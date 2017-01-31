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

    Private Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

#End Region

#Region "Private Variables"
    Private _myhelmet As ImageSource
    Private _primcolor As Brush
    Private _seccolor As Brush
    Private _trimcolor As Brush
    Private _mystadiumname As String
    Private _mystadiumcapacity As String
    Private _mycitystate As String
    Private _mystadiumpic As ImageSource
    Private _myavgattendance As String
    Private _myteamrecord As String
    Private _mybackgroundimg As ImageSource
    Private _MyDT As New ObservableCollection(Of DataTable)

#End Region

#Region "Public Properties"

    Public Property MyStadiumName As String
        Get
            Return _mystadiumname
        End Get
        Set
            _mystadiumname = Value
            OnPropertyChanged("MyStadiumName")
        End Set
    End Property

    Public Property MyPrimColor As Brush
        Get
            Return _primcolor
        End Get
        Set
            _primcolor = Value
            OnPropertyChanged("MyPrimColor")
        End Set
    End Property

    Public Property MyTrimColor As Brush
        Get
            Return _trimcolor
        End Get
        Set
            _trimcolor = Value
            OnPropertyChanged("MyTrimColor")
        End Set
    End Property

    Public Property MySecColor As Brush
        Get
            Return _seccolor
        End Get
        Set
            _seccolor = Value
            OnPropertyChanged("MySecColor")
        End Set
    End Property

    Public Property MyDTProperty As ObservableCollection(Of DataTable)
        Get
            Return _MyDT
        End Get
        Set
            _MyDT = Value
            OnPropertyChanged("MyDTProperty")
        End Set
    End Property

    Public Property MyStadiumCapacity As String
        Get

            Return _mystadiumcapacity
        End Get
        Set
            _mystadiumcapacity = Value
            OnPropertyChanged("MyStadiumCapacity")
        End Set
    End Property

    Public Property MyCityState As String
        Get
            Return _mycitystate
        End Get
        Set
            _mycitystate = Value
            OnPropertyChanged("MyCityState")
        End Set
    End Property

    Public Property MyStadiumPic As ImageSource
        Get
            Return _mystadiumpic
        End Get
        Set
            _mystadiumpic = Value
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

    Public Property MyAvgAttendance As String
        Get
            Return _myavgattendance
        End Get
        Set
            _myavgattendance = Value
            OnPropertyChanged("MyAvgAttendance")
        End Set
    End Property

    Public Property MyTeamRecord As String
        Get
            Return _myteamrecord
        End Get
        Set
            _myteamrecord = Value
            OnPropertyChanged("MyTeamRecord")
        End Set
    End Property

    Public Property MyBackgroundImg As ImageSource
        Get
            Return _mybackgroundimg
        End Get
        Set
            _mybackgroundimg = Value
            OnPropertyChanged("MyBackgroundImg")
        End Set
    End Property

#End Region

#Region "Enums"

    Public Enum DivisionNames
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
    ''' <param name="teamNum"></param>
    ''' <returns></returns>
    Public Shared Function GetBackgroundFilePath(Optional ByVal teamNum As Integer = 32) As String
        Dim FilePath = "pack://application:,,,/Project_Files/"

        Select Case teamNum
            Case 0 : FilePath += Bills02Jpg
            Case 1 : FilePath += Patriots2Jpg
            Case 2 : FilePath += JetsJpg
            Case 3 : FilePath += Dolphins_2013Jpg
            Case 4 : FilePath += Bengals3Jpg
            Case 5 : FilePath += Steelers2Jpg
            Case 6 : FilePath += Ravens3Jpg
            Case 7 : FilePath += Browns2Jpg1
            Case 8 : FilePath += Texans2Jpg
            Case 9 : FilePath += Colts2Jpg
            Case 10 : FilePath += Jaguars2Jpg
            Case 11 : FilePath += Titans2Jpg
            Case 12 : FilePath += Broncos2Jpg
            Case 13 : FilePath += Chiefs3Jpg
            Case 14 : FilePath += RaidersJpg
            Case 15 : FilePath += Chargers5Jpg
            Case 16 : FilePath += Redskins2Jpg
            Case 17 : FilePath += Eagles2Jpg
            Case 18 : FilePath += Giants5Jpg
            Case 19 : FilePath += Cowboys3Jpg
            Case 20 : FilePath += Vikings_2013_06Jpg
            Case 21 : FilePath += Packers5Jpg
            Case 22 : FilePath += Lions2Jpg
            Case 23 : FilePath += Bears4Jpg
            Case 24 : FilePath += Panthers2Jpg
            Case 25 : FilePath += FalconsJpg
            Case 26 : FilePath += Saints2Jpg
            Case 27 : FilePath += Buccaneers2Jpg
            Case 28 : FilePath += Cardinals3Jpg
            Case 29 : FilePath += Seahawks2_2012Jpg
            Case 30 : FilePath += RamsJpg
            Case 31 : FilePath += _49ers04Jpg
            Case 32 : FilePath += GlobalClass_GetBackgroundFilePath_FootballGoalLine_jpg
        End Select

        Return FilePath
    End Function

    ''' <summary>
    '''     Sets the helmet image of the team on the button
    ''' </summary>
    ''' <param name="teamNum"></param>
    ''' <returns></returns>

    Public Shared Function GetImage(teamNum As Integer) As Image
        Dim MyImage As New Image
        Dim FilePath = "pack://application:,,,/Project_Files/"

        Select Case teamNum
            Case 0 : FilePath += Bills_PHelmet_2011Jpg
            Case 1 : FilePath += Patriots_PHelmetJpg
            Case 2 : FilePath += Jets_PHelmetJpg
            Case 3 : FilePath += Dolphins_PHelmetJpg
            Case 4 : FilePath += Bengals_PHelmetJpg
            Case 5 : FilePath += Steelers_PHelmetJpg
            Case 6 : FilePath += Ravens_PHelmetJpg
            Case 7 : FilePath += Browns_PHelmetJpg
            Case 8 : FilePath += Texans_PHelmetJpg
            Case 9 : FilePath += Colts_PHelmetJpg
            Case 10 : FilePath += Jaguars_PHelmetJpg
            Case 11 : FilePath += Titans_PHelmetJpg
            Case 12 : FilePath += Broncos_PHelmetJpg
            Case 13 : FilePath += Chiefs_PHelmetJpg
            Case 14 : FilePath += Raiders_HelmetJpg
            Case 15 : FilePath += Chargers_PHelmet2Jpg
            Case 16 : FilePath += Redskins_PHelmetJpg
            Case 17 : FilePath += Eagles_PHelmetJpg
            Case 18 : FilePath += Giants_PHelmetJpg
            Case 19 : FilePath += Cowboys_PhelmetJpg
            Case 20 : FilePath += Vikings_PHelmet_2013Jpg
            Case 21 : FilePath += Packers_PHelmetJpg
            Case 22 : FilePath += Lions_PHelmetJpg
            Case 23 : FilePath += Bears_PHelmet2Jpg
            Case 24 : FilePath += Panthers_PHelmetJpg
            Case 25 : FilePath += Falcons_PHelmetJpg
            Case 26 : FilePath += Saints_PHelmetJpg
            Case 27 : FilePath += Buccaneers_PHelmetJpg
            Case 28 : FilePath += Cardinals_HelmetJpg
            Case 29 : FilePath += Seahawks_PHelmet_2012Jpg
            Case 30 : FilePath += Rams1Png
            Case 31 : FilePath += _49ers_PHelmet_NewJpg
        End Select
        MyImage.Source = New BitmapImage(New Uri(FilePath, UriKind.RelativeOrAbsolute))
        Return MyImage
    End Function

    ''' <summary>
    '''     Loads picture of the proper stadium by team
    ''' </summary>
    ''' <param name="teamNum"></param>
    ''' <returns></returns>
    Public Shared Function GetStadiumPic(teamNum As Integer) As String
        Dim FilePath = "pack://application:,,,/Project_Files/"
        Select Case teamNum
            Case 0 : FilePath += NewEraFieldJpg
            Case 1 : FilePath += GilletteStadiumJpg
            Case 2, 18 : FilePath += MetLife_StadiumJpg
            Case 3 : FilePath += SunLifeStadiumJpg
            Case 4 : FilePath += PaulBrownStadiumJpg
            Case 5 : FilePath += HeinzFieldJpg
            Case 6 : FilePath += MTBankStadiumJpg
            Case 7 : FilePath += FirstEnergyStadiumJpg
            Case 8 : FilePath += NRGStadiumJpg
            Case 9 : FilePath += LucasOilStadiumJpg
            Case 10 : FilePath += EverBankFieldJpg
            Case 11 : FilePath += NissanStadiumJpg
            Case 12 : FilePath += SportsAuthorityFieldJpg
            Case 13 : FilePath += ArrowheadStadiumJpg
            Case 14 : FilePath += OaklandColiseumJpg
            Case 15 : FilePath += QualcommStadiumJpg
            Case 16 : FilePath += FedexFieldJpg
            Case 17 : FilePath += LincolnFinancialFieldJpg
            Case 19 : FilePath += ATTStadiumJpg
            Case 20 : FilePath += USBankStadiumJpg
            Case 21 : FilePath += LambeaufieldJpg
            Case 22 : FilePath += FordfieldJpg
            Case 23 : FilePath += SoldierFieldJpg
            Case 24 : FilePath += BankOfAmericaStadiumJpg
            Case 25 : FilePath += GeorgiaDomePng
            Case 26 : FilePath += Superdome_saintsJpg
            Case 27 : FilePath += RaymondJamesStadiumJpg
            Case 28 : FilePath += University_phoenix_stadiumJpg
            Case 29 : FilePath += CenturyLinkFieldJpg
            Case 30 : FilePath += LosAngelesColiseumJpg
            Case 31 : FilePath += LevisStadiumJpg
        End Select
        Return FilePath
    End Function

    Public Shared Function GetBrush(teamNum As Integer, myQueue As Queue, teamDT As DataTable) As Queue
        teamNum += 1
        For i = 0 To teamDT.Rows.Count - 1
            If teamDT.Rows(i).Item("TeamID") = teamNum Then
                myQueue.Enqueue(teamDT.Rows(i).Item("MainColor"))
                myQueue.Enqueue(teamDT.Rows(i).Item("SecondaryColor"))
                myQueue.Enqueue(teamDT.Rows(i).Item("TrimColor"))
                Exit For
            End If
        Next i
        Return myQueue
    End Function

    Public Shared Function ConvertColor(HexString As String) As Brush
        Dim Converter = New BrushConverter()
        Dim MyBrush As Brush
        MyBrush = DirectCast(Converter.ConvertFromString(HexString), Brush)
        Return MyBrush
    End Function

End Class