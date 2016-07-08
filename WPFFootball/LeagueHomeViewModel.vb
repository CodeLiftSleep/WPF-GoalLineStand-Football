Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data

Public Class LeagueHomeViewModel
    Inherits NewGameViewModel
    Implements INotifyPropertyChanged

#Region "INotifyPropertyChanged"

    Public Shadows Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    private Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

#End Region

#Region "Private Variables"
    Private _leaguedate as Date
    Private _myDT as New ObservableCollection(Of DataTable)
    Private _eventBtn as New Command(AddressOf LgEvent)
    Private _standingsBtn as New Command(AddressOf LgStandings)
    Private _transBtn As New Command(AddressOf LgTrans)

   

#End Region

#Region "Public Properties"
    Public Property Leaguedate as Date
        Get
            return _leaguedate
        End Get
        Set(value as Date)
            _leaguedate = value
            OnPropertyChanged("Leaguedate")
        End Set
    End Property

    Public Property MyDT as ObservableCollection(Of DataTable)
        Get
            Return _myDT
        End Get
        Set(value as ObservableCollection(Of DataTable))
            _myDT = value
            OnPropertyChanged("MyDT")
        End Set
    End Property

    Public ReadOnly Property EventBtn as ICommand
        Get
            return _eventBtn
        End Get
    End Property
    Public ReadOnly Property StandingsBtn As ICommand
        Get
            Return _standingsBtn
        End Get
    End Property
    Public ReadOnly Property TransBtn As ICommand
    Get
            Return _TransBtn
    End Get
    End Property

#End Region

#Region "Commands"
    ''' <summary>
    ''' Controls what happens when Event Button is clicked
    ''' </summary>
    Private Sub LgEvent
        Dim TempDT as new DataTable
        Dim MyDate as new Date
        Dim formatpattern as String = "ddd MMMM dd, yyyy"

        TempDT.Columns.Add("Date", GetType(string))
        TempDT.Columns.Add("Event Scheduled", GetType(String))
        TempDT.Columns.Add("Location", GetType(String))

        '##TODO: Add in code that actually pulls the events---possibly to a function that uses a ParamArray to take Column Names and returns the filled DT
        'so it can be reused with each Command Event.  Likely will put the Events in a formatted text file and use a StreamReader to pull them out

        MyDate = "2/16/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "First Day to Designate Franchise/Transition Tag Players", "---")
        MyDate = "2/23/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Combine", "Indianapolis, IN")
        MyDate = "3/1/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Franchise/Transition Tag Deadline(4PM ET)", "---")
        MyDate = "3/9/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Free Agency Begins", "---")
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Deadline to Submit RFA Offers(4PM ET)", "---")
        MyDate = "3/20/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Annual Meetings Begin", "Boca Raton, FL")
        MyDate = "3/23/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Annual Meetings End", "Boca Raton, FL")
        MyDate = "4/4/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Teams with new Head Coaches can begin offseason programs", "---")
        MyDate = "4/18/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Teams with returning Head Coaches can begin offseason programs", "Boca Raton, FL")
        MyDate = "4/28/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Draft Round 1", "Chicago, IL")
        MyDate = "4/29/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Draft Round 2 and 3", "Chicago, IL")
        MyDate = "4/30/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Draft Round 4 thru 7", "Chicago, IL")
        MyDate = "5/6/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "First Rookie Minicamp", "---")
        MyDate = "5/13/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Second Rookie Minicamp", "---")
        MyDate = "5/23/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Spring Meetings Begin", "Charlotte, NC")
        MyDate = "5/25/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Spring Meetings End", "Charlotte, NC")
        MyDate = "6/7/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "First week for Mandatory MiniCamps", "---")
        MyDate = "6/19/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Rookie Symposium Begins", "Aurora, OH")
        MyDate = "6/25/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Rookie Symposium Ends", "Aurora, OH")
        MyDate = "7/15/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Deadline for Franchise Tagged Players to sign long term deals(4PM ET)", "---")
        MyDate = "7/22/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Earliest Date for Rookies to Report to Training Camp", "---")
        MyDate = "7/25/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Earliest Date for Veterans to Report to Training Camp", "---")
        MyDate = "8/6/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Hall Of Fame Enshrinement Ceremonies", "Canton, OH")
        MyDate = "8/7/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Hall of Fame Game", "Canton, OH")
        MyDate = "8/11/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Preseason Begins", "---")
        MyDate = "8/31/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Deadline for NFL Rosters to be reduced to 75 players(4PM ET)", "---")
        MyDate = "9/2/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Preseason Ends", "---")
        MyDate = "9/4/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Deadline for NFL Rosters to be reduced to 53 players(4PM ET)", "---")
        MyDate = "9/5/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "10 Player NFL Practice Squads Can Be Established", "---")
        MyDate = "9/8/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Regular Season Begin", "---")
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "'Top 51' Rule Expires for All NFL Teams", "---")
        MyDate = "10/7/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Fall Meeting Begins", "New York, NY")
        MyDate = "10/8/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Fall Meeting Ends", "New York, NY")
        MyDate = "11/1/16"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Trade Deadline(4PM ET)", "---")
        MyDate = "1/1/17"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Regular Season Ends", "---")
        MyDate = "1/7/17"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL WildCard Weekend Kicks Off", "TBD")
        MyDate = "1/14/17"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Divisional Playoff Round Kicks Off", "TBD")
        MyDate = "1/22/17"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Conference Championship Games", "TBD")
        MyDate = "1/29/17"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "NFL Pro Bowl Game", "Orlando, FL")
        MyDate = "2/5/17"
        TempDT.Rows.Add(MyDate.ToString(formatpattern), "Super Bowl", "Houston, TX")
        
        MyDT.Add(TempDT)
    End Sub

    ''' <summary>
    ''' Controls what happens when LgStandings Button is Clicked
    ''' </summary>
    Private Sub LgStandings
        MyDT.Clear()
        Dim TempDT As New DataTable
        Dim Teams As New Teams       
        
        TempDT.Columns.Add("Team Name", GetType(String))
        TempDT.Columns.Add("Wins||", GetType(string))
        TempDT.Columns.Add("Losses||", GetType(string))
        TempDT.Columns.Add("Ties||",GetType(string))
        TempDT.Columns.Add("Points For||",GetType(string))
        TempDT.Columns.Add("Pts Against||",GetType(string))
        TempDT.Columns.Add("Net Pts||",GetType(string))
        TempDT.Columns.Add("Home||",GetType(string))
        TempDT.Columns.Add("Road||",GetType(string))
        TempDT.Columns.Add("Div||",GetType(string))
        TempDT.Columns.Add("Conf||",GetType(string))
        TempDT.Columns.Add("Non-Conf||",GetType(string))
        TempDT.Columns.Add("Streak||",GetType(string))

        '##TODO: Add in code that actually pulls the League Standings---possibly to a function that uses a ParamArray to take Column Names and returns the filled DT
        'so it can be reused with each Command Event.

        For Each team As Teams In NewGame.EnumToList(Of Teams)() 'cycle through teams in the enum list and add their description

            Select Case TempDT.Rows.Count 'Add the column header for each Division
                Case 0:  TempDT.Rows.Add("AFC East","W", "L","T","PF","PA","Net", "Home","Road","Div","Conf","Non-Conf","Strk")
                Case 5:  TempDT.Rows.Add("AFC North","W", "L","T","PF","PA","Net", "Home","Road","Div","Conf","Non-Conf","Strk")
                Case 10:  TempDT.Rows.Add("AFC Central","W", "L","T","PF","PA","Net", "Home","Road","Div","Conf","Non-Conf","Strk")
                Case 15: TempDT.Rows.Add("AFC South","W", "L","T","PF","PA","Net", "Home","Road","Div","Conf","Non-Conf","Strk")
                Case 20: TempDT.Rows.Add("NFC East","W", "L","T","PF","PA","Net", "Home","Road","Div","Conf","Non-Conf","Strk")
                Case 25: TempDT.Rows.Add("NFC North","W", "L","T","PF","PA","Net", "Home","Road","Div","Conf","Non-Conf","Strk")
                Case 30: TempDT.Rows.Add("NFC Central","W", "L","T","PF","PA","Net", "Home","Road","Div","Conf","Non-Conf","Strk")
                Case 35: TempDT.Rows.Add("NFC South","W", "L","T","PF","PA","Net", "Home","Road","Div","Conf","Non-Conf","Strk")
            End Select
            TempDT.Rows.Add({$"{NewGame.GetEnumDescription(team)}",0,0,0,0,0,0,"0-0","0-0","0-0","0-0","0-0","W0"})
        Next team
       
        
        MyDT.Add(TempDT)

    End Sub
    ''' <summary>
    ''' Controls what happens when LgTrans Button is Clicked
    ''' </summary>
     Private Sub LgTrans()
        MyDT.Clear()
        Dim TempDT As New DataTable 
        Dim MyDate as new Date
        Dim Team As New Teams
        Dim formatpattern as String = "M/dd"
        
        '##TODO: Add in code that actually pulls the transcations---possibly to a function that uses a ParamArray to take Column Names and returns the filled DT
        'so it can be reused with each Command Event.
    
        TempDT.Columns.Add("Team",GetType(string))      
        TempDT.Columns.Add("Trans Date",GetType(string))
        TempDT.Columns.Add("Player Name",GetType(String))
        TempDT.Columns.Add("Position",GetType(String))
        TempDT.Columns.Add("Transaction Type",GetType(String)) 
        
        MyDate="6/17/16"
       
        TempDT.Rows.Add({$"{NewGame.GetEnumDescription(Teams.BUF)}",MyDate.ToString(formatpattern),"Justin Renfrow", "OT","Free Agent Signing"})
        TempDT.Rows.Add({$"{NewGame.GetEnumDescription(Teams.BUF)}",MyDate.ToString(formatpattern),"Phillip Thomas", "DB","Waived, Injured, Prior to Cut to 75"})
        TempDT.Rows.Add({$"{NewGame.GetEnumDescription(Teams.GNB)}",MyDate.ToString(formatpattern),"Kenny Clark", "NT", "Selection List Signing"})
        TempDT.Rows.Add({$"{NewGame.GetEnumDescription(Teams.NYJ)}",MyDate.ToString(formatpattern),"Kyle Williams", "WR", "Free Agent Signing"})
        TempDT.Rows.Add({$"{NewGame.GetEnumDescription(Teams.NWO)}",MyDate.ToString(formatpattern),"Jamarca Sanford", "SS", "Reserve/Injured; Prior to Cut to 75"})
        TempDT.Rows.Add({$"{NewGame.GetEnumDescription(Teams.NWO)}",MyDate.ToString(formatpattern),"Lawrence Virgil", "DT", "Free Agent Signing"})


        




        MyDT.Add(TempDT)
    End Sub
#End Region

    Public Class Command
        Implements ICommand

#Region "Private Variables"
        Private ReadOnly _action as Action
#End Region
        Sub New(myAction As Action)
            _action = myAction
        End Sub

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _action
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function
    End Class

End Class