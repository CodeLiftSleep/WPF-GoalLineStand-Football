Imports System.Data
Imports System.Globalization

Public  Class LeagueHome
    Dim ReadOnly MyVM as new LeagueHomeViewModel
    Private TempDT as New DataTable
    Sub New

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        DataContext = MyVM
        MyVM.Leaguedate = "2/16/16"
        
        
    End Sub

   


End Class