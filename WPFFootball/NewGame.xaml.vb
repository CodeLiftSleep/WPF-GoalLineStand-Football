Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports SQLFunctions
Imports Troschuetz.Random
Public Class NewGame
    Private ReadOnly SQLTable As New SQLiteDataFunctions
    Public Shared ReadOnly TeamDT As New DataTable
    Public ReadOnly OwnerDT As New DataTable
    Public ReadOnly PersonnelDT As New DataTable
    Public ReadOnly CoachDT As New DataTable
    Public ReadOnly PlayerDT As New DataTable
    Public ReadOnly Football As New DataSet
    Public TempDT As New DataTable

    ReadOnly MyVM As New NewGameViewModel

    ReadOnly MyDB As String = "Football"

    ReadOnly myQueue As New Queue
    ReadOnly myRand As New TRandom

    ReadOnly filepath As String = "Project Files/"
    ReadOnly SR As New StreamReader(filepath + My.Resources.Schedule4GamesMaxTxt)

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
        SQLTable.LoadTable(MyDB, OwnerDT, "Owners")
        SQLTable.LoadTable(MyDB, PersonnelDT, "Personnel")
        SQLTable.LoadTable(MyDB, CoachDT, "Coaches")
        SQLTable.LoadTable(MyDB, PlayerDT, "RosterPlayers")

        'Add DataTables to the Football DataSet for operations without having to continuously load tables
        Football.Tables.Add(CoachDT)
        Football.Tables.Add(OwnerDT)
        Football.Tables.Add(PersonnelDT)
        Football.Tables.Add(PlayerDT)
        Football.Tables.Add(TeamDT)
    End Sub

    Private Shared Function EnumToList(Of T)() As IEnumerable(Of T)
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

    Private Shared Function GetEnumDescription(value As [Enum]) As String
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
        MyVM.MyPrimColor = NewGameViewModel.ConvertColor(myQueue.Dequeue())
        MyVM.MySecColor = NewGameViewModel.ConvertColor(myQueue.Dequeue())
        MyVM.MyTrimColor = NewGameViewModel.ConvertColor(myQueue.Dequeue())
        GetPeople(TeamCombo.SelectedIndex, myQueue)
        GetTeamValues(TeamCombo.SelectedIndex)
        LoadTeamSchedule(TeamCombo.SelectedIndex)
    End Sub

    ''' <summary>
    '''     This updates the team information when a user selects a team via the IndexChanged propert of the combobox
    ''' </summary>
    ''' <param name="TeamNum"></param>
    Private Sub GetTeamValues(TeamNum As Integer)
        Dim MyDiv As New NewGameViewModel.DivisionNames
        Dim MyAvg As Integer = TeamDT.Rows(TeamNum).Item("AvgAttendance")
        Dim MyCap As Integer = TeamDT.Rows(TeamNum).Item("StadiumCapacity")
        Dim PerOfCap As Single = Math.Round(((MyAvg / MyCap) * 100), 1)
        Dim Off As Integer = myRand.NextUInt(75, 99)
        Dim Def As Integer = myRand.NextUInt(75, 99)
        Dim ST As Integer = myRand.NextUInt(75, 99)
        Dim SalCapTotal As Integer = myRand.NextUInt(151000000, 159000000)
        Dim Top51Contracts As Integer = SalCapTotal * (myRand.NextDouble(0.8, 0.93))
        Dim TotContracts As Integer = Top51Contracts * (myRand.NextDouble(1.02, 1.06))
        Dim DeadCap As Integer = myRand.NextUInt(3000000, 12000000)
        Dim AvailCap As Integer = SalCapTotal - TotContracts - DeadCap

        MyDiv = TeamDT.Rows(TeamNum).Item("DivID")

        MyVM.MyStadiumName = String.Format("Stadium Name: {0}", TeamDT.Rows(TeamNum).Item("StadiumName"))
        MyVM.MyStadiumCapacity = String.Format("Stadium Capacity: {0}", MyCap)
        MyVM.MyStadiumPic = New BitmapImage(New Uri(NewGameViewModel.GetStadiumPic(TeamCombo.SelectedIndex),
                                                    UriKind.RelativeOrAbsolute))
        MyVM.MyCityState = String.Format("{0}, {1}", TeamDT.Rows(TeamNum).Item("City"),
                                         TeamDT.Rows(TeamNum).Item("State"))
        MyVM.MyAvgAttendance = String.Format("Last Year Avg. Attendance: {0}{1}({2}% of Capacity)",
                                             Environment.NewLine, MyAvg, PerOfCap)
        MyVM.MyTeamRecord =
            String.Format("Last Year Record: {0} Wins  {1} Losses  {2} Ties  {3}{4}{5} Place in the {6}",
                          TeamDT.Rows(TeamNum).Item("Wins"), TeamDT.Rows(TeamNum).Item("Losses"),
                          TeamDT.Rows(TeamNum).Item("Ties"), Environment.NewLine,
                          TeamDT.Rows(TeamNum).Item("DivStanding"),
                          GetDivPlace(TeamDT.Rows(TeamNum).Item("DivStanding")), GetEnumDescription(MyDiv))
        TeamStaff.Inlines.Clear()
        TeamStaff.Inlines.Add(New Run() With {.FontSize = 40, .Text = "Team Information "})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "Owner "})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 (String.Format("{0} {1} Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                                myQueue.Dequeue()))})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "General Manager "})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 String.Format("{0} {1}  Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                               myQueue.Dequeue())})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "Head Coach "})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 String.Format("{0} {1}  Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                               myQueue.Dequeue())})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "Off. Coordinator "})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 String.Format("{0} {1}  Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                               myQueue.Dequeue())})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(New Run() With {.Foreground = MyVM.MyTrimColor, .Text = "Def. Coordinator "})
        TeamStaff.Inlines.Add(New LineBreak)
        TeamStaff.Inlines.Add(
            New Run() _
                                 With {.FontSize = 24,
                                 .Text =
                                 String.Format("{0} {1}  Age {2} ", myQueue.Dequeue(), myQueue.Dequeue(),
                                               myQueue.Dequeue())})
        TeamStaff.Inlines.Add(New LineBreak)

        TeamRatings.Text = String.Format("Team Ratings: OFF: {0}  DEF: {1}  ST: {2}  Overall: {3} ", Off, Def, ST,
                                         CInt((Off * 0.4) + (Def * 0.4) + (ST * 0.2)))

        TempDT = SQLTable.FilterTable(PlayerDT, TempDT, String.Format("TeamID = {0}", TeamCombo.SelectedIndex + 1),
                                      "Pos")

        MyVM.MyDTProperty.Clear()
        MyVM.MyDTProperty.Add(TempDT)

        TeamRosterDT.Visibility = 0
        TeamRosterText.Text = "Team Roster "
        SalCapTabControl.Visibility = 0
        Tab1.Visibility = 0
        Tab2.Visibility = 0
        Tab3.Visibility = 0

        SalaryCap.Inlines.Clear()
        SalaryCap.Inlines.Add(New Run() _
                                 With {.FontSize = 40, .Foreground = MyVM.MyTrimColor, .Text = "Team Salary Cap Info:"})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.Add(
            New Run() _
                                 With {.FontSize = 36, .Foreground = MyVM.MyTrimColor,
                                 .Text = String.Format("Team Salary Cap: {0} ", SalCapTotal.ToString("N0"))})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.Add(
            New Run() _
                                 With {.FontSize = 30, .Foreground = MyVM.MySecColor,
                                 .Text = String.Format("Top 51 Contracts: {0} ", Top51Contracts.ToString("N0"))})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.Add(
            New Run() _
                                 With {.FontSize = 30, .Foreground = MyVM.MySecColor,
                                 .Text = String.Format("Dead Cap Space: {0} ", DeadCap.ToString("N0"))})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.Add(
            New Run() _
                                 With {.FontSize = 30, .Foreground = MyVM.MySecColor,
                                 .Text = String.Format("Total Cap Spent: {0} ", TotContracts.ToString("N0"))})
        SalaryCap.Inlines.Add(New LineBreak)
        SalaryCap.Inlines.Add(
            New Run() _
                                 With {.FontSize = 30, .Foreground = MyVM.MySecColor,
                                 .Text = String.Format("Available Cap Space: {0} ", AvailCap.ToString("N0"))})
    End Sub

    Private Function GetDivPlace(DivStanding As Integer) As String
        Dim myString = ""
        Select Case DivStanding
            Case 1 : myString = "st"
            Case 2 : myString = "nd"
            Case 3 : myString = "rd"
            Case 4 : myString = "th"
        End Select
        Return myString
    End Function

    ''' <summary>
    '''     Returns a Queue of the following people: Owner, GM, Head Coach, DC and OC in format "First Name", "Last Name",
    '''     "Age"
    ''' </summary>
    ''' <param name="TeamNum"></param>
    ''' <param name="myQueue"></param>
    ''' <returns></returns>
    Private Function GetPeople(TeamNum As Integer, myQueue As Queue) As Queue
        Dim TeamID As Integer = TeamNum + 1
        For i = 0 To OwnerDT.Rows.Count - 1
            If OwnerDT.Rows(i).Item("TeamID") = TeamID Then
                myQueue.Enqueue(OwnerDT.Rows(i).Item("FName"))
                myQueue.Enqueue(OwnerDT.Rows(i).Item("LName"))
                myQueue.Enqueue(OwnerDT.Rows(i).Item("Age"))
            End If
        Next i
        For i = 0 To PersonnelDT.Rows.Count - 1
            If PersonnelDT.Rows(i).Item("TeamID") = TeamID And PersonnelDT.Rows(i).Item("PersonnelType") = 1 Then
                myQueue.Enqueue(PersonnelDT.Rows(i).Item("FName"))
                myQueue.Enqueue(PersonnelDT.Rows(i).Item("LName"))
                myQueue.Enqueue(PersonnelDT.Rows(i).Item("Age"))
            End If
        Next i

        For i = 0 To CoachDT.Rows.Count - 1
            If CoachDT.Rows(i).Item("TeamID") = TeamID And CoachDT.Rows(i).Item("CoachType") = 1 Then
                myQueue.Enqueue(CoachDT.Rows(i).Item("FName"))
                myQueue.Enqueue(CoachDT.Rows(i).Item("LName"))
                myQueue.Enqueue(CoachDT.Rows(i).Item("Age"))
            End If
        Next i

        For i = 0 To CoachDT.Rows.Count - 1
            If CoachDT.Rows(i).Item("TeamID") = TeamID And CoachDT.Rows(i).Item("CoachType") = 3 Then
                myQueue.Enqueue(CoachDT.Rows(i).Item("FName"))
                myQueue.Enqueue(CoachDT.Rows(i).Item("LName"))
                myQueue.Enqueue(CoachDT.Rows(i).Item("Age"))
            End If
        Next i

        For i = 0 To CoachDT.Rows.Count - 1
            If CoachDT.Rows(i).Item("TeamID") = TeamID And CoachDT.Rows(i).Item("CoachType") = 4 Then
                myQueue.Enqueue(CoachDT.Rows(i).Item("FName"))
                myQueue.Enqueue(CoachDT.Rows(i).Item("LName"))
                myQueue.Enqueue(CoachDT.Rows(i).Item("Age"))
            End If
        Next i
        Return myQueue
    End Function

    ''' <summary>
    '''     User has selected this to be their Team
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Helmet_OnClick(sender As Object, e As RoutedEventArgs)
        Dim res As MsgBoxResult = MsgBox("Are You Sure You Want To Select This Team?", MsgBoxStyle.YesNo, "Team Select")

        If res = MsgBoxResult.Yes Then

            Dim myTeam As New UserScreen(TeamCombo.SelectedIndex)
            Close()
            myTeam.Show()
            GetWindow(myTeam)
            SR.Dispose()
        End If
    End Sub

    ''' <summary>
    '''     Loads Team Schedule for team and displays on schedule pane
    ''' </summary>
    ''' <param name="TeamID"></param>
    Private Sub LoadTeamSchedule(TeamID As Integer)
        TeamID += 1 'increments the 0 based index to match
        Dim Week As Integer
        Dim lineString As String
        Dim pos As Integer
        Dim opponent As Integer
        Dim substring As String

        TeamSchedule.Inlines.Clear()
        TeamSchedule.Inlines.Add(New Run() _
                                    With {.FontSize = 32, .Foreground = MyVM.MyTrimColor, .Text = "Team Schedule"})
        TeamSchedule.Inlines.Add(New LineBreak)
        SR.DiscardBufferedData()
        SR.BaseStream.Seek(0, SeekOrigin.Begin) 'resets streamreader to beginning of stream

        While SR.EndOfStream <> True 'reads through the file
            lineString = SR.ReadLine
            pos = 0
            opponent = 0
            substring = ""

            If lineString.Contains(String.Format("({0}) vs.", TeamID)) Then 'This is a home game
                Week += 1

                pos = lineString.IndexOfAny("vs. ")
                If pos > 0 Then

                    substring = New String(lineString.Substring(pos, lineString.Length - pos))
                    opponent = Integer.Parse(Regex.Replace(substring, "[^\d]", ""))
                End If

                MyVM.TeamEnumList = opponent
                TeamSchedule.Inlines.Add(
                    New Run() _
                                            With {.FontSize = 26, .Foreground = MyVM.MyTrimColor,
                                            .Text =
                                            String.Format("Week {0}: Vs. {1}", Week,
                                                          GetEnumDescription(MyVM.TeamEnumList))})
                TeamSchedule.Inlines.Add(New LineBreak)

            ElseIf lineString.Contains("Teams On Bye:") Then
                If lineString.Contains(String.Format("({0})", TeamID)) Then 'Team is on a Bye
                    Week += 1
                    TeamSchedule.Inlines.Add(
                        New Run() _
                                                With {.FontSize = 26, .Foreground = MyVM.MyTrimColor,
                                                .Text = String.Format("Week {0}: ***BYE WEEK***", Week)})
                    TeamSchedule.Inlines.Add(New LineBreak)
                End If

            ElseIf lineString.Contains(String.Format("({0})", TeamID)) Then 'Away game
                Week += 1

                pos = lineString.IndexOfAny("(")
                If pos > 0 Then

                    substring = New String(lineString.Substring(pos, 3))
                    opponent = Integer.Parse(Regex.Replace(substring, "[^\d]", ""))
                End If

                MyVM.TeamEnumList = opponent
                TeamSchedule.Inlines.Add(
                    New Run() _
                                            With {.FontSize = 26, .Foreground = MyVM.MySecColor,
                                            .Text =
                                            String.Format("Week {0}: At {1}", Week,
                                                          GetEnumDescription(MyVM.TeamEnumList))})
                TeamSchedule.Inlines.Add(New LineBreak)

            End If
        End While
    End Sub
End Class