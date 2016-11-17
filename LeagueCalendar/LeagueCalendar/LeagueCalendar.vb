''' <summary>
''' This class runs the league year dayd by day and raises events to call different states based on the date
''' Current States: Offseason, Combine, Free Agency, Pre-Draft, Draft, Post Draft, Mini Camps/OTA's, Training Camps, Pre-Season, Regular Season, Post Season
''' Dates are stored as variables as they will change year to year
''' </summary>
Public Class LeagueCalendar
    Private CurLeagueDate As New Date
    Public LgState As New LeagueState
    Public WithEvents LeagueCalendarEvents As New LeagueCalendarEventHandlers
    Public OffseasonStartDate As String = "2/9/2016"
    Public CombineStartDate As String = "2/23/2016"
    Public FreeAgencyStartDate As String = "3/7/2016"
    Public PreDraftStartDate As String = "3/20/2016"
    Public DraftStartDate As String = "4/28/2016"
    Public PostDraftStartDate As String = "4/30/2016"
    Public MiniCampOTAStartDate As String = "5/13/2016"
    Public TrainingCampStartDate As String = "7/30/2016"
    Public PreSeasonStartDate As String = "8/15/2016"
    Public RegSeasonStartDate As String = "9/1/2016"
    Public PostSeasonStartDate As String = "1/3/2017"
    ''' <summary>
    ''' returns the current date
    ''' </summary>
    ''' <returns></returns>
    Property CurDate As Date
        Get
            Return CurLeagueDate
        End Get
        Set(value As Date)
            CurLeagueDate = value
        End Set
    End Property
    ''' <summary>
    ''' Advances the day by the value set in the property when called
    ''' </summary>
    ''' <returns></returns>
    Property AdvanceDay As Date
        Get
            Return CurLeagueDate
        End Get
        Set(value As Date)
            CurLeagueDate = value
        End Set
    End Property

    ''' <summary>
    ''' Initializes A New Instance of this class with a standard start date, the day after the Super Bowl, which begins the offseason
    ''' </summary>
    Public Sub New()
        CurDate = New Date(2016, 2, 8).ToShortDateString()
        LgState = LeagueState.Offseason
    End Sub

    Public Function AdvDay(ByVal Currentdate As Date, ByVal NumDays As Integer) As Date
        For i As Integer = 1 To NumDays
            CurDate = AdvanceDay.AddDays(1)
        Next i
        CheckDate(CurDate)
        Return CurDate.ToShortDateString()
    End Function
    ''' <summary>
    ''' checks to see if we need to raise an event
    ''' </summary>
    Public Sub CheckDate(ByVal curDate As Date)
        Select Case curDate.ToShortDateString
            Case OffseasonStartDate
                LeagueCalendarEvents.RaiseOffseason(curDate)
            Case CombineStartDate
                LeagueCalendarEvents.RaiseCombine(curDate)
            Case FreeAgencyStartDate
                LeagueCalendarEvents.RaiseFreeAgency(curDate)
            Case PreDraftStartDate
                LeagueCalendarEvents.RaisePreDraft(curDate)
            Case DraftStartDate
                LeagueCalendarEvents.RaiseDraft(curDate)
            Case PostDraftStartDate
                LeagueCalendarEvents.RaisePostDraft(curDate)
            Case MiniCampOTAStartDate
                LeagueCalendarEvents.RaiseMiniCampOTA(curDate)
            Case TrainingCampStartDate
                LeagueCalendarEvents.RaiseTrainingCamp(curDate)
            Case PreSeasonStartDate
                LeagueCalendarEvents.RaisePreSeason(curDate)
            Case RegSeasonStartDate
                LeagueCalendarEvents.RaiseRegSeason(curDate)
            Case PostSeasonStartDate
                LeagueCalendarEvents.RaisePostSeason(curDate)
        End Select
    End Sub
    ''' <summary>
    ''' Offseason related event code goes here
    ''' </summary>
    Public Overridable Sub OnOffSeasonDate() Handles LeagueCalendarEvents.OffseasonDate
        Console.WriteLine("It's the OffSeason!")
        LgState = LeagueState.Offseason
    End Sub

    ''' <summary>
    ''' Combine Related Draft Code goes here
    ''' </summary>
    Public Overridable Sub OnCombinenDate() Handles LeagueCalendarEvents.CombineDate
        Console.WriteLine("It's the Combine!")
        LgState = LeagueState.Combine
    End Sub
    ''' <summary>
    ''' Free Agency Event Code Goes here
    ''' </summary>
    Public Overridable Sub OnFreeAgencyDate() Handles LeagueCalendarEvents.FreeAgencyDate
        Console.WriteLine("It's time for Free Agency!")
        LgState = LeagueState.FreeAgency
    End Sub
    ''' <summary>
    ''' Pre-Draft Event code goes here
    ''' </summary>
    Public Overridable Sub OnPreDraftDate() Handles LeagueCalendarEvents.PreDraftDate
        Console.WriteLine("It's Pre Draft!")
        LgState = LeagueState.PreDraft
    End Sub
    ''' <summary>
    ''' Draft Event code goes here
    ''' </summary>
    Public Overridable Sub OnDraftDate() Handles LeagueCalendarEvents.DraftDate
        Console.WriteLine("It's Draft! Who are you picking?")
        LgState = LeagueState.Draft
    End Sub
    ''' <summary>
    ''' Post-Draft Event code goes here
    ''' </summary>
    Public Overridable Sub OnPostDraftDate() Handles LeagueCalendarEvents.PostDraftDate
        Console.WriteLine("It's Post Draft!")
        LgState = LeagueState.PostDraft
    End Sub
    ''' <summary>
    ''' MiniCampOTA Event code goes here
    ''' </summary>
    Public Overridable Sub OnMiniCampOTADate() Handles LeagueCalendarEvents.MiniCampOTADate
        Console.WriteLine("It's time for Mini Camps and OTA's")
        LgState = LeagueState.MiniCampOTA
    End Sub
    ''' <summary>
    ''' Training Camp Event code goes here
    ''' </summary>
    Public Overridable Sub OnTrainingCampDate() Handles LeagueCalendarEvents.TrainingCampDate
        Console.WriteLine("It's time for 2 a days!")
        LgState = LeagueState.TrainingCamp
    End Sub
    ''' <summary>
    ''' Pre-Season Event code goes here
    ''' </summary>
    Public Overridable Sub OnPreSeasonDate() Handles LeagueCalendarEvents.PreSeasonDate
        Console.WriteLine("It's the Pre Season!")
        LgState = LeagueState.PreSeason
    End Sub
    ''' <summary>
    ''' Regular Season Event code goes here
    ''' </summary>
    Public Overridable Sub OnRegSeasonDate() Handles LeagueCalendarEvents.RegSeasonDate
        Console.WriteLine("It's the Regular Season!")
        LgState = LeagueState.RegSeason
    End Sub
    ''' <summary>
    ''' Post Season Event code goes here
    ''' </summary>
    Public Overridable Sub OnPostSeasonDate() Handles LeagueCalendarEvents.PostSeasonDate
        Console.WriteLine("It's the Post Season!!")
        LgState = LeagueState.PostSeason
    End Sub
End Class