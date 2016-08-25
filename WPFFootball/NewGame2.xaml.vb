Imports System.ComponentModel
Imports System.Data

Public Class NewGame2
    Dim MyVM As New NewGame2VM
    Dim dv As DataView
    Dim Query(7)
    ''' <summary>
    ''' Need to Load the Appropraite Columns from the DataTable upon Load for the 8 Divisions
    ''' Divisions Will be added in the Following Order: AFC East, AFC North, AFC South, AFC West, NFC East, NFC North, NFC South, NFC West
    ''' </summary>
    Public Sub New()
        Dim ColNames As String() = {"ConfID", "DivID", "TeamID", "TeamName", "TeamNickName", "TeamAbrev", "City", "State", "MetroPopulation", "StadiumName", "StadiumCapacity"}

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        For i As Integer = 0 To Query.Length - 1
            Dim x = i + 1
            Query(i) = From teams In NewGame.TeamDT.AsEnumerable()
                       Where teams.Item("DivID") = x
                       Order By teams.Item("TeamID")
                       Select New With {
                .ConfID = ($"    {teams.Item("ConfId")}    "),
                .DivID = ($"    {teams.Item("DivID")}    "),
                .TeamName = ($"  {teams.Item("TeamName")}  "),
                .NickName = ($"  {teams.Item("TeamNickname")}  "),
                .Abb = ($"   {teams.Item("TeamAbrev")}   "),
                .City = ($"   {teams.Item("City")}   "),
                .State = ($"   {teams.Item("State")}   "),
                .Population = ($" {String.Format("{0:n0}", teams.Item("MetroPopulation"))} "),
                .Stadium = ($" {teams.Item("StadiumName")} "),
                .Capacity = ($"  {String.Format("{0:n0}", teams.Item("StadiumCapacity"))}  ")
                }
        Next i

        Div1.DataContext = Query(0)
        Div2.DataContext = Query(1)
        Div3.DataContext = Query(2)
        Div4.DataContext = Query(3)
        Div5.DataContext = Query(4)
        Div6.DataContext = Query(5)
        Div7.DataContext = Query(6)
        Div8.DataContext = Query(7)

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim Draft As New NFL_Draft.NFLDraft
        Draft.Show()
        GetWindow(Draft)
        Close()
    End Sub
End Class