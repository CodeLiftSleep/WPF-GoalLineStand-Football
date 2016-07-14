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
        MyVM.Leaguedate = "2/16/16"

    End Sub

End Class