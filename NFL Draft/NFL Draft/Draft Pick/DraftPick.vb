''' <summary>
''' Class for Loading Draft News for the Draft ListViews---This class also contains the PickMade event, which is subscribed to by a MultiCast Delegate which notifies
''' the ResetTimer Function, NextTeam Function, DraftNewsUpdate Function, TickerUpdate Function and RemovePlayerFromDraftBoard Function
''' </summary>
Public MustInherit Class DraftPick
    Public Shared Event PickMade()
    Public Shared Property DraftID As Integer

    ''' <summary>
    ''' Returns the player selected.  Also raises the PickMade Event which has multiple subscribers to it.
    ''' </summary>
    Public Shared Sub SelectionMade()
        RaiseEvent PickMade()
    End Sub

End Class