Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Linq

Public Class LeagueHomeViewModel
    Inherits NewGameViewModel
    Implements INotifyPropertyChanged

#Region "INotifyPropertyChanged"

    Public Shadows Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

#End Region

#Region "Private Variables"
    Private _leaguedate As Date
    Private _myDT As New ObservableCollection(Of DataTable)
    Private _eventBtn As New Command(AddressOf LgEvent)
    Private _standingsBtn As New Command(AddressOf LgStandings)
    Private _transBtn As New Command(AddressOf LgTrans)
    Private _injuriesBtn As New Command(AddressOf LgInjuries)
    Private _milestonesBtn As New Command(AddressOf LgMilestones)

#End Region
#Region "Public Properties"
    Public Property Leaguedate As Date
        Get
            Return _leaguedate
        End Get
        Set(value As Date)
            _leaguedate = value
            OnPropertyChanged("Leaguedate")
        End Set
    End Property
    Public Property MyDT As ObservableCollection(Of DataTable)
        Get
            Return _myDT
        End Get
        Set(value As ObservableCollection(Of DataTable))
            _myDT = value
            OnPropertyChanged("MyDT")
        End Set
    End Property
    Public ReadOnly Property EventBtn As ICommand
        Get
            Return _eventBtn
        End Get
    End Property
    Public ReadOnly Property StandingsBtn As ICommand
        Get
            Return _standingsBtn
        End Get
    End Property
    Public ReadOnly Property TransBtn As ICommand
        Get
            Return _transBtn
        End Get
    End Property
    Public ReadOnly Property InjuriesBtn As Command
        Get
            Return _injuriesBtn
        End Get
    End Property
    Public ReadOnly Property MilestonesBtn As Command
        Get
            Return _milestonesBtn
        End Get
    End Property
#End Region

#Region "Commands"
    ''' <summary>
    ''' Controls what happens when Event Button is clicked
    ''' </summary>
    Private Sub LgEvent()
        Dim TempDT As New DataTable
        Dim ColNames() As String = {"Date", "Event Scheduled", "Location"}

        MyDT.Clear()
        ReadFile("LeagueEvents.Txt", ColNames, TempDT, "ddd MMMM dd, yyyy", 0)
        MyDT.Add(TempDT)

    End Sub

    ''' <summary>
    ''' Controls what happens when LgStandings Button is Clicked
    ''' </summary>
    Private Sub LgStandings()
        MyDT.Clear()
        Dim TempDT As New DataTable
        Dim ColNames() As String = {"Team Name", "Wins||", "Losses||", "Ties||", "Points For||", "Pts Against||", "Net Pts||", "Home||", "Road||", "Div||", "Conf||", "Non-Conf||", "Streak||"}
        Dim DivId As Integer = 1
        GetColumnNames(TempDT, ColNames)

        While DivId < 9
            'LINQ Query to get DB info
            Dim GetDiv =
                From myrow In NewGame.TeamDT
                Where myrow.Item("DivID") = DivId
                Order By (myrow.Item("DivStanding"))
                Select New With {
                    .TeamName = myrow.Item("TeamName") + " " + myrow.Item("TeamNickName"),
                    .Wins = myrow.Item("Wins"), .Losses = myrow.Item("Losses"), .Ties = myrow.Item("Ties"),
                    .PF = 0, .PA = 0, .NP = 0, .Home = String.Format($"{myrow.Item("Home Wins")}-{myrow.Item("Home Losses")}-{myrow.Item("HomeTies")}"),
                    .Road = String.Format($"{myrow.Item("Away Wins")}-{myrow.Item("Away Losses")}-{myrow.Item("AwayTies")}"),
                    .Div = String.Format($"{myrow.Item("DivWins")}-{myrow.Item("DivLosses")}-{myrow.Item("DivTies")}"),
                    .Conf = String.Format($"{myrow.Item("ConfWins")}-{myrow.Item("ConfLosses")}-{myrow.Item("ConfTies")}"),
                    .NonConf = String.Format($"{(myrow.Item("Wins") - myrow.Item("ConfWins"))}-{(myrow.Item("Losses") - myrow.Item("ConfLosses"))}-{(myrow.Item("Ties") - myrow.Item("ConfTies"))}"),
                    .Streak = 0}

            Select Case TempDT.Rows.Count 'Add the column header for each Division
                Case 0 : TempDT.Rows.Add("AFC East", "W", "L", "T", "PF", "PA", "Net", "Home", "Road", "Div", "Conf", "Non-Conf", "Strk")
                Case 5 : TempDT.Rows.Add("AFC North", "W", "L", "T", "PF", "PA", "Net", "Home", "Road", "Div", "Conf", "Non-Conf", "Strk")
                Case 10 : TempDT.Rows.Add("AFC Central", "W", "L", "T", "PF", "PA", "Net", "Home", "Road", "Div", "Conf", "Non-Conf", "Strk")
                Case 15 : TempDT.Rows.Add("AFC South", "W", "L", "T", "PF", "PA", "Net", "Home", "Road", "Div", "Conf", "Non-Conf", "Strk")
                Case 20 : TempDT.Rows.Add("NFC East", "W", "L", "T", "PF", "PA", "Net", "Home", "Road", "Div", "Conf", "Non-Conf", "Strk")
                Case 25 : TempDT.Rows.Add("NFC North", "W", "L", "T", "PF", "PA", "Net", "Home", "Road", "Div", "Conf", "Non-Conf", "Strk")
                Case 30 : TempDT.Rows.Add("NFC Central", "W", "L", "T", "PF", "PA", "Net", "Home", "Road", "Div", "Conf", "Non-Conf", "Strk")
                Case 35 : TempDT.Rows.Add("NFC South", "W", "L", "T", "PF", "PA", "Net", "Home", "Road", "Div", "Conf", "Non-Conf", "Strk")
            End Select

            For Each item In GetDiv
                TempDT.Rows.Add().ItemArray = {item.TeamName, item.Wins, item.Losses, item.Ties, item.PF, item.PA, item.NP, item.Home, item.Road, item.Div, item.Conf,
                    item.NonConf, item.Streak}
            Next item
            'Adds a header under each Division

            DivId += 1
        End While

        MyDT.Add(TempDT)

    End Sub
    ''' <summary>
    ''' Controls what happens when LgTrans Button is Clicked---reads the Lg
    ''' </summary>
    Private Sub LgTrans()
        MyDT.Clear()
        Dim TempDT As New DataTable
        Dim ColumnNames() As String = {"Team", "Trans Date", "Player Name", "Position", "Transaction Type"}
        ReadFile("LeagueTrans.Txt", ColumnNames, TempDT, "M/dd", 1, "Trans Date DESC")
        MyDT.Add(TempDT)
    End Sub
    ''' <summary>
    ''' Controls what happens when LgInjuries Button is Clicked
    ''' </summary>
    Private Sub LgInjuries()
        MyDT.Clear()
        Dim TempDT As New DataTable
        Dim ColumnNames() As String = {"Team", "Inj Date", "Player Name", "Position", "Body Part", "Prognosis"}
        ReadFile("LeagueInj.Txt", ColumnNames, TempDT, "M/dd", 1, "Inj Date DESC")
        MyDT.Add(TempDT)
    End Sub

    Private Sub LgMilestones()
        MyDT.Clear()
        Dim TempDT As New DataTable
        Dim ColumnNames() As String = {"Team", "Milestone Date", "Player Name", "Position", "Milestone Achieved"}
        ReadFile("LeagueMile.Txt", ColumnNames, TempDT, "M/dd", 1, "Milestone Date DESC")
        MyDT.Add(TempDT)
    End Sub
    ''' <summary>
    ''' Takes in the name of the file, ColumnName str array, and a DT.  Reads each line of the file, splits it by a ";" and then adds in the array to the DT
    ''' after calling the GetColumnNames Sub to take the parameter array and create columns. Optional Parameters for DateFormatting string and the index which which to apply it
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <param name="ColumnNames"></param>
    ''' <param name="DT"></param>
    ''' <param name="DateFormatPattern"></param>
    ''' <param name="DateFormatIndex"></param>
    Private Sub ReadFile(ByVal FileName As String, ByVal ColumnNames() As String, ByVal DT As DataTable, Optional DateFormatPattern As String = "",
                         Optional DateFormatIndex As Integer = -1, Optional OrderBy As String = "")
        Dim str As String = ""
        Dim myarray() As String

        GetColumnNames(DT, ColumnNames)
        'Automatically dispose of the StreamReader once its completed.
        Using myreader As New StreamReader(FileName)
            'First Line contains information about formatting and not actual content
            myreader.ReadLine()
            While myreader.EndOfStream = False
                str = myreader.ReadLine()
                myarray = str.Split(";")
                'Checks to see if there is a Date involved and if there is, formats it to its proper form before returning it back to the array
                If DateFormatPattern <> "" Then
                    Dim MyDate As New Date
                    MyDate = myarray(DateFormatIndex)
                    myarray(DateFormatIndex) = MyDate.ToString(DateFormatPattern)
                End If

                'Adds the array to the data row and sorts it according to the parameter supplied
                DT.Rows.Add().ItemArray = myarray
                DT.Select("", OrderBy, DataViewRowState.CurrentRows)
            End While
        End Using
    End Sub

    Private Sub GetColumnNames(ByVal DT As DataTable, ByVal ParamArray args() As String)
        For i = 0 To args.Count - 1
            DT.Columns.Add(args(i))
        Next i
    End Sub
#End Region

    Public Class Command
        Implements ICommand

#Region "Private Variables"
        Private ReadOnly _action As Action
#End Region
        Sub New(myAction As Action)
            _action = myAction
        End Sub

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _action()
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function
    End Class

End Class