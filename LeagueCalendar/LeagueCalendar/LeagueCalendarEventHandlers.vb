''' <summary>
''' Handles the League Calendar Events
''' </summary>
Public Class LeagueCalendarEventHandlers
    Public Event OffseasonDate(ByVal CurDate As Date)
    Public Event CombineDate(ByVal CurDate As Date)
    Public Event FreeAgencyDate(ByVal CurDate As Date)
    Public Event PreDraftDate(ByVal CurDate As Date)
    Public Event DraftDate(ByVal CurDate As Date)
    Public Event PostDraftDate(ByVal CurDate As Date)
    Public Event MiniCampOTADate(ByVal CurDate As Date)
    Public Event TrainingCampDate(ByVal CurDate As Date)
    Public Event PreSeasonDate(ByVal CurDate As Date)
    Public Event RegSeasonDate(ByVal CurDate As Date)
    Public Event PostSeasonDate(ByVal CurDate As Date)
    Public Sub RaiseOffseason(ByVal CurDate As Date)
        RaiseEvent OffseasonDate(CurDate)
    End Sub
    Public Sub RaiseCombine(ByVal CurDate As Date)
        RaiseEvent CombineDate(CurDate)
    End Sub
    Public Sub RaisePreDraft(ByVal CurDate As Date)
        RaiseEvent PreDraftDate(CurDate)
    End Sub
    Public Sub RaiseFreeAgency(ByVal Curdate As Date)
        RaiseEvent FreeAgencyDate(Curdate)
    End Sub
    Public Sub RaiseDraft(ByVal CurDate As Date)
        RaiseEvent DraftDate(CurDate)
    End Sub
    Public Sub RaisePostDraft(ByVal CurDate As Date)
        RaiseEvent PostDraftDate(CurDate)
    End Sub

    Public Sub RaiseMiniCampOTA(ByVal CurDate As Date)
        RaiseEvent MiniCampOTADate(CurDate)
    End Sub

    Public Sub RaiseTrainingCamp(ByVal CurDate As Date)
        RaiseEvent TrainingCampDate(CurDate)
    End Sub
    Public Sub RaisePreSeason(ByVal CurDate As Date)
        RaiseEvent PreSeasonDate(CurDate)
    End Sub

    Public Sub RaiseRegSeason(ByVal CurDate As Date)
        RaiseEvent RegSeasonDate(CurDate)
    End Sub
    Public Sub RaisePostSeason(ByVal CurDate As Date)
        RaiseEvent PostSeasonDate(CurDate)
    End Sub

End Class