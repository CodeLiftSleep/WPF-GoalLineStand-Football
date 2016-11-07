Imports System.Data
Imports System.Globalization

Public Class LeagueHome
    ReadOnly MyVM As New LeagueHomeViewModel
    Private TempDT As New DataTable
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        DataContext = MyVM
        MyVM.leagueDate = "2/16/16"

        Dim ColNames() As String = {"Date", "Team", "News"}
        MyVM.ReadFile("LeagueNews.Txt", ColNames, TempDT, "ddd MMMM dd, yyyy", 0)
        MyVM.leagueNews.Clear()
        MyVM.leagueNews.Add(TempDT)
        MyVM.LgEvent() 'By default league events will be displayed in the data grid on load.
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class