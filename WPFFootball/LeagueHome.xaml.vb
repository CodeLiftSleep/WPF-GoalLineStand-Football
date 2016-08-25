Imports System.Data
Imports System.Globalization

Public Class LeagueHome
    ReadOnly MyVM As New LeagueHomeViewModel
    'Private TempDT As New DataTable
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        DataContext = MyVM
        MyVM.Leaguedate = "2/16/16"
        LoadLgNews()

    End Sub
    Public Sub LoadLgNews()
        MyVM.MyDT2.Clear()
        Dim ColNames() As String = {"Date", "Team", "News"}
        Dim TempDT As New DataTable

        Generation.FilesAndDataTables.ReadFile("LeagueNews.Txt", ColNames, TempDT, "ddd MMMM dd, yyyy", 0)
        MyVM.MyDT2.Add(TempDT)

    End Sub

End Class