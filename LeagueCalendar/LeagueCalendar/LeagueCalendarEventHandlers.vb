''' <summary>
''' Handles the League Calendar Events
''' </summary>
Public Class LeagueCalendarEventHandlers
    Public Event OffseasonDate(ByVal curDate As Date)
    Public Event CombineDate(ByVal curDate As Date)
    Public Event FreeAgencyDate(ByVal curDate As Date)
    Public Event PreDraftDate(ByVal curDate As Date)
    Public Event DraftDate(ByVal curDate As Date)
    Public Event PostDraftDate(ByVal curDate As Date)
    Public Event MiniCampOTADate(ByVal curDate As Date)
    Public Event TrainingCampDate(ByVal curDate As Date)
    Public Event PreSeasonDate(ByVal curDate As Date)
    Public Event RegSeasonDate(ByVal curDate As Date)
    Public Event PostSeasonDate(ByVal curDate As Date)
    Public Sub RaiseOffseason(ByVal curDate As Date)
        RaiseEvent OffseasonDate(curDate)
    End Sub
    Public Sub RaiseCombine(ByVal curDate As Date)
        RaiseEvent CombineDate(curDate)
    End Sub
    Public Sub RaisePreDraft(ByVal curDate As Date)
        RaiseEvent PreDraftDate(curDate)
    End Sub
    Public Sub RaiseFreeAgency(ByVal curDate As Date)
        RaiseEvent FreeAgencyDate(curDate)
    End Sub
    Public Sub RaiseDraft(ByVal curDate As Date)
        RaiseEvent DraftDate(curDate)
    End Sub
    Public Sub RaisePostDraft(ByVal curDate As Date)
        RaiseEvent PostDraftDate(curDate)
    End Sub

    Public Sub RaiseMiniCampOTA(ByVal curDate As Date)
        RaiseEvent MiniCampOTADate(curDate)
    End Sub

    Public Sub RaiseTrainingCamp(ByVal curDate As Date)
        RaiseEvent TrainingCampDate(curDate)
    End Sub
    Public Sub RaisePreSeason(ByVal curDate As Date)
        RaiseEvent PreSeasonDate(curDate)
    End Sub

    Public Sub RaiseRegSeason(ByVal curDate As Date)
        RaiseEvent RegSeasonDate(curDate)
    End Sub
    Public Sub RaisePostSeason(ByVal curDate As Date)
        RaiseEvent PostSeasonDate(curDate)
    End Sub

End Class