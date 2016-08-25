Imports System.Data

Public Class LeagueNews
    Dim MyVM As New LeagueNewsViewModel
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        LoadLgNews()
        ' Add any initialization after the InitializeComponent() call.
        DataContext = MyVM

    End Sub

    Private Sub LoadLgNews()
        MyVM.MyDT.Clear()
        Dim TempDT As New DataTable

        Dim ColNames() As String = {"Date", "Team", "News"}
        Generation.ReadFile("LeagueNews.Txt", ColNames, TempDT, "ddd MMMM dd, yyyy", 0)

        MyVM.MyDT.Add(TempDT)
    End Sub

End Class