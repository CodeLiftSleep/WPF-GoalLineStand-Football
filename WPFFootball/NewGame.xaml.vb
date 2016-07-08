Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports SQLFunctions
Imports Troschuetz.Random
Public Class NewGame
    private Readonly SQLTable as New SQLiteDataFunctions
    public Shared TeamDT as new DataTable
    private ReadOnly OwnerDT as new DataTable
    private ReadOnly PersonnelDT as new Datatable
    private ReadOnly CoachDT as new Datatable
    Private ReadOnly PlayerDT as New Datatable
    Private ReadOnly Football as New DataSet
    private TempDT as new Datatable

    Dim ReadOnly MyVM as new NewGameViewModel

    Dim ReadOnly MyDB as String = "Football"

    Dim ReadOnly myQueue as new Queue
    Dim ReadOnly myRand as new TRandom

    Dim ReadOnly filepath As String = "Project Files/"
    Dim ReadOnly SR as New StreamReader(filepath + My.Resources.Schedule4GamesMaxTxt)

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        For Each team As NewGameViewModel.Teams In EnumToList(Of NewGameViewModel.Teams)()
            TeamCombo.Items.Add(GetEnumDescription(team))
            myRand.NextUInt() 'Initiate the random number generator to get good randomness
        Next team

        'Sets the DataContext of the Model to the ViewModel
        DataContext = MyVM

        SQLTable.LoadTable(MyDB, TeamDT, "Teams")
        SQLTable.LoadTable(myDB, OwnerDT, "Owners")
        SQLTable.LoadTable(mydb, PersonnelDT, "Personnel")
        SQlTable.LoadTable(MyDB, CoachDT, "Coaches")
        SQLTable.LoadTable(MyDB, PlayerDT, "RosterPlayers")

        'Add DataTables to the Football DataSet for operations without having to continuously load tables
        Football.Tables.Add(CoachDT)
        Football.Tables.Add(OwnerDT)
        Football.Tables.Add(PersonnelDT)
        Football.Tables.Add(PlayerDT)
        Football.Tables.Add(TeamDT)
    End Sub

    Public Shared Function EnumToList(Of T)() As IEnumerable(Of T)
        Dim enumType As Type = GetType(T)

        ' Can't use generic type constraints on value types,
        ' so have to do check like this
        If enumType.BaseType <> GetType([Enum]) Then
            Throw New ArgumentException("T must be of type System.Enum")
        End If

        Dim enumValArray As Array = [Enum].GetValues(enumType)
        Dim enumValList As New List(Of T)(enumValArray.Length)

        For Each val As Integer In enumValArray
            enumValList.Add(DirectCast([Enum].Parse(enumType, val.ToString()), T))
        Next

        Return enumValList
    End Function

    Public Shared Function GetEnumDescription(value As [Enum]) As String
        Dim fi As FieldInfo = value.[GetType]().GetField(value.ToString())

        Dim attributes = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())

        If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
            Return attributes(0).Description
        Else
            Return value.ToString()
        End If
    End Function

    ''' <summary>
    '''     This event fires when the user selects a team from the drop down menu---it updates all the information for that
    '''     team
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TeamCombo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) _
        Handles TeamCombo.SelectionChanged

        MyVM.MyHelmet = NewGameViewModel.GetImage(TeamCombo.SelectedIndex).Source
        Helmet.Visibility = 0
        MyVM.MyBackgroundImg = New BitmapImage(New Uri(NewGameViewModel.GetBackgroundFilePath(TeamCombo.SelectedIndex),
                                                       UriKind.RelativeOrAbsolute))
        NewGameViewModel.GetBrush(TeamCombo.SelectedIndex, myQueue, TeamDT)
        MyVM.MyPrimColor = NewGameViewModel.ConvertColor(MyQueue.Dequeue())
        MyVM.MySecColor = NewGameViewModel.ConvertColor(MyQueue.Dequeue())
        MyVM.MyTrimColor = NewGameViewModel.ConvertColor(MyQueue.Dequeue())
        GetPeople(TeamCombo.Selectedindex, myQueue)
        GetTeamValues(TeamCombo.selectedindex)
        LoadTeamSchedule(TeamCombo.SelectedIndex)
    End Sub

    ''' <summary>
    '''     This updates the team information when a user selects a team via the IndexChanged propert of the combobox
    ''' </summary>
    ''' <param name="TeamNum"></param>
    Private sub GetTeamValues(TeamNum As integer)
        Dim MyDiv as New NewGameViewModel.DivisionNames
        dim MyAvg as integer = TeamDT.Rows(teamnum).Item("AvgAttendance")
        dim MyCap as Integer = TeamDT.Rows(teamnum).Item("StadiumCapacity")
        Dim PerOfCap as Single = Math.Round(((myavg / mycap) * 100), 1)
        Dim Off As Integer = myRand.NextUInt(75, 99)
        Dim Def As Integer = myRand.NextUInt(75, 99)
        Dim ST As Integer = myrand.NextUInt(75, 99)
        Dim SalCapTotal as integer = myRand.NextUInt(151000000, 159000000)
        dim Top51Contracts As integer = SalCapTotal * (myrand.NextDouble(.80, .93))
        dim TotContracts as integer = Top51Contracts * (myRand.NextDouble(1.02, 1.06))
        dim DeadCap as integer = myRand.NextUInt(3000000, 12000000)
        dim AvailCap as Integer = SalCapTotal - TotContracts - DeadCap

        MyDiv = TeamDT.Rows(teamnum).Item("DivID")

        MyVM.MyStadiumName = string.format("Stadium Name: {0}", Teamdt.Rows(teamnum).item("StadiumName"))
        MyVM.MyStadiumCapacity = string.format("Stadium Capacity: {0}", MyCap)
        MyVM.MYStadiumPic = New BitmapImage(New Uri(NewGameViewModel.GetStadiumPic(TeamCombo.SelectedIndex),
                                                    UriKind.RelativeOrAbsolute))
        MyVM.MyCityState = string.format("{0}, {1}", Teamdt.Rows(teamnum).Item("City"),
                                         TeamDT.Rows(teamnum).Item("State"))
        MyVM.MyAvgAttendance = string.format("Last Year Avg. Attendance: {0}{1}({2}% of Capacity)",
                                             Environment.newline, MyAvg, PerOfCap)
        MyVM.MyTeamRecord =
            string.format("Last Year Record: {0} Wins  {1} Losses  {2} Ties  {3}{4}{5} Place in the {6}",
                          TeamDT.Rows(teamnum).Item("Wins"), TeamDT.Rows(TeamNum).Item("Losses"),
                          TeamDT.Rows(teamnum).Item("Ties"), Environment.NewLine,
                          teamdt.Rows(teamnum).Item("DivStanding"),
                          GetDivPlace(teamdt.Rows(teamnum).Item("DivStanding")), GetEnumDescription(MyDiv))
        TeamStaff.Inlines.Clear()
        TeamStaff.Inlines.Add(New Run() With {.FontSize = 40, .Text = "Team Information "})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "Owner "})
        TeamStaff.Inlines.Add(New LineBreak)
        Teamstaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 (String.Format("{0} {1} Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                                myQueue.Dequeue()))})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "General Manager "})
        teamstaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 String.Format("{0} {1}  Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                               myQueue.Dequeue())})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "Head Coach "})
        Teamstaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 String.Format("{0} {1}  Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                               myQueue.Dequeue())})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "Off. Coordinator "})
        Teamstaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 String.Format("{0} {1}  Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                               myQueue.Dequeue())})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "Def. Coordinator "})
        Teamstaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 String.Format("{0} {1}  Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                               myQueue.Dequeue())})
        TeamStaff.Inlines.Add(New LineBreak)

        TeamRatings.Text = String.Format("Team Ratings: OFF: {0}  DEF: {1}  ST: {2}  Overall: {3} ", Off, Def, ST,
                                         CInt((Off * .4) + (def * .4) + (ST * .2)))

        TempDT = SQLTable.FilterTable(PlayerDT, TempDT, String.Format("TeamID = {0}", TeamCombo.SelectedIndex + 1),
                                      "Pos")

        MyVM.MyDTProperty.clear
        MyVM.MyDTProperty.Add(TempDT)

        TeamRosterDT.Visibility = 0
        TeamRosterText.Text = "Team Roster "
        SalCapTabControl.Visibility = 0
        Tab1.Visibility = 0
        Tab2.Visibility = 0
        Tab3.Visibility = 0

        SalaryCap.Inlines.clear
        SalaryCap.Inlines.Add(New Run() _
                                 With {.FontSize = 40, .Foreground = MyVM.MyTrimColor, .Text = "Team Salary Cap Info:"})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.Add(
            New Run() _
                                 With {.FontSize = 36, .Foreground = MyVM.MyTrimColor,
                                 .Text = String.Format("Team Salary Cap: {0} ", SalCapTotal.ToString("N0"))})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.add(
            New Run() _
                                 With {.FontSize = 30, .Foreground = MyVM.MySecColor,
                                 .Text = string.Format("Top 51 Contracts: {0} ", Top51Contracts.ToString("N0"))})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.add(
            New Run() _
                                 With {.FontSize = 30, .Foreground = MyVM.MySecColor,
                                 .Text = string.Format("Dead Cap Space: {0} ", DeadCap.ToString("N0"))})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.add(
            New Run() _
                                 With {.FontSize = 30, .Foreground = MyVM.MySecColor,
                                 .Text = string.Format("Total Cap Spent: {0} ", TotContracts.ToString("N0"))})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.add(
            New Run() _
                                 With {.FontSize = 30, .Foreground = MyVM.MySecColor,
                                 .Text = string.Format("Available Cap Space: {0} ", AvailCap.ToString("N0"))})
    End sub

    private function GetDivPlace(DivStanding as integer) As String
        Dim myString = ""
        select case DivStanding
            Case 1 : myString = "st"
            Case 2 : myString = "nd"
            Case 3 : myString = "rd"
            Case 4 : myString = "th"
        End Select
        return myString
    End function

    ''' <summary>
    '''     Returns a Queue of the following people: Owner, GM, Head Coach, DC and OC in format "First Name", "Last Name",
    '''     "Age"
    ''' </summary>
    ''' <param name="TeamNum"></param>
    ''' <param name="myQueue"></param>
    ''' <returns></returns>
    private Function GetPeople(TeamNum As integer, myQueue as Queue) As Queue
        dim TeamID as Integer = TeamNum + 1
        for i = 0 To ownerdt.Rows.Count - 1
            if OwnerDT.Rows(i).Item("TeamID") = TeamID Then
                myQueue.Enqueue(OwnerDT.Rows(i).Item("FName"))
                myQueue.Enqueue(OwnerDT.Rows(i).Item("LName"))
                myQueue.Enqueue(OwnerDT.Rows(i).Item("Age"))
            End If
        Next i
        for i = 0 to PersonnelDT.Rows.Count - 1
            if PersonnelDT.rows(i).item("TeamID") = TeamID and PersonnelDT.Rows(i).Item("PersonnelType") = 1 Then
                myQueue.Enqueue(PersonnelDT.Rows(i).Item("FName"))
                myQueue.Enqueue(personnelDT.rows(i).Item("LName"))
                myQueue.Enqueue(PersonnelDT.Rows(i).Item("Age"))
            End If
        Next i

        for i = 0 To CoachDT.Rows.Count - 1
            if coachdt.rows(i).item("TeamID") = TeamID And CoachDT.rows(i).item("CoachType") = 1 then
                myqueue.enqueue(CoachDT.rows(i).item("FName"))
                myqueue.enqueue(CoachDT.rows(i).item("LName"))
                myqueue.enqueue(CoachDT.rows(i).item("Age"))
            End If
        Next i

        for i = 0 To CoachDT.Rows.Count - 1
            if coachdt.rows(i).item("TeamID") = TeamID And CoachDT.rows(i).item("CoachType") = 3 then
                myqueue.enqueue(CoachDT.rows(i).item("FName"))
                myqueue.enqueue(CoachDT.rows(i).item("LName"))
                myqueue.enqueue(CoachDT.rows(i).item("Age"))
            End If
        Next i

        for i = 0 To CoachDT.Rows.Count - 1
            if coachdt.rows(i).item("TeamID") = TeamID And CoachDT.rows(i).item("CoachType") = 4 then
                myqueue.enqueue(CoachDT.rows(i).item("FName"))
                myqueue.enqueue(CoachDT.rows(i).item("LName"))
                myqueue.enqueue(CoachDT.rows(i).item("Age"))
            End If
        Next i
        return myQueue
    End Function

    ''' <summary>
    '''     User has selected this to be their Team
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Helmet_OnClick(sender As Object, e As RoutedEventArgs)
        Dim res As MsgBoxResult = MsgBox("Are You Sure You Want To Select This Team?", MsgBoxStyle.YesNo, "Team Select")

        if res = MsgBoxResult.Yes then

            dim myTeam as New UserScreen(TeamCombo.SelectedIndex)
            Close()
            myteam.Show()
            GetWindow(myTeam)
            SR.Dispose()
        end if
    End Sub

    ''' <summary>
    '''     Loads Team Schedule for team and displays on schedule pane
    ''' </summary>
    ''' <param name="TeamID"></param>
    Private Sub LoadTeamSchedule(TeamID As integer)
        TeamID += 1 'increments the 0 based index to match
        dim Week as integer
        dim lineString as string
        dim pos as Integer
        dim opponent as Integer
        Dim substring as string

        TeamSchedule.Inlines.clear
        TeamSchedule.Inlines.Add(New Run() _
                                    With {.FontSize = 32, .Foreground = MyVM.MyTrimColor, .Text = "Team Schedule"})
        TeamSchedule.Inlines.Add(New LineBreak)
        SR.DiscardBufferedData()
        SR.BaseStream.Seek(0, SeekOrigin.Begin) 'resets streamreader to beginning of stream

        while SR.EndOfStream <> True 'reads through the file
            lineString = SR.Readline
            pos = 0
            opponent = 0
            substring = ""

            if linestring.Contains(String.Format("({0}) vs.", TeamID)) then 'This is a home game
                Week += 1

                pos = lineString.IndexOfAny("vs. ")
                if pos > 0 Then

                    substring = New String(linestring.Substring(pos, linestring.Length - pos))
                    opponent = Integer.Parse(Regex.Replace(substring, "[^\d]", ""))
                End If

                MyVM.TeamEnumList = opponent
                TeamSchedule.Inlines.Add(
                    New Run() _
                                            With {.FontSize = 26, .Foreground = MyVM.MyTrimColor,
                                            .Text =
                                            string.Format("Week {0}: Vs. {1}", Week,
                                                          GetEnumDescription(MyVM.TeamEnumList))})
                TeamSchedule.Inlines.Add(New LineBreak)

            ElseIf linestring.Contains("Teams On Bye:") then
                if linestring.contains(String.Format("({0})", TeamID)) Then 'Team is on a Bye
                    Week += 1
                    TeamSchedule.Inlines.Add(
                        New Run() _
                                                With {.FontSize = 26, .Foreground = MyVM.Mytrimcolor,
                                                .Text = string.Format("Week {0}: ***BYE WEEK***", Week)})
                    TeamSchedule.Inlines.Add(New LineBreak)
                end if

            ElseIf lineString.Contains(string.Format("({0})", TeamID)) Then 'Away game
                Week += 1

                pos = lineString.IndexOfAny("(")
                if pos > 0 Then

                    substring = New String(linestring.Substring(pos, 3))
                    opponent = Integer.Parse(Regex.Replace(substring, "[^\d]", ""))
                End If

                MyVM.TeamEnumList = opponent
                TeamSchedule.Inlines.Add(
                    New Run() _
                                            With {.FontSize = 26, .Foreground = MyVM.MySecColor,
                                            .Text =
                                            string.Format("Week {0}: At {1}", Week,
                                                          GetEnumDescription(MyVM.TeamEnumList))})
                TeamSchedule.Inlines.Add(New LineBreak)

            End If
        end while
    End Sub
End Class