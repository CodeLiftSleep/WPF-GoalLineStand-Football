Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data

Public Class LeagueNewsViewModel
    Implements INotifyPropertyChanged

#Region "INotifyPropertyChanged"
    Public Shadows Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub
#End Region
#Region "Private Variables"
    Private _myDT As New ObservableCollection(Of DataTable)
#End Region
#Region "Public Properties"
    Public Property MyDT As ObservableCollection(Of DataTable)
        Get
            Return _myDT
        End Get
        Set(value As ObservableCollection(Of DataTable))
            _myDT = value
            OnPropertyChanged("MyDT")
        End Set
    End Property
#End Region

    ''' <summary>
    ''' Loads the league news into the League News DataGrid upon Entering
    ''' </summary>
    Sub New()

    End Sub

End Class

Public Class Command
    Implements ICommand

#Region "Private Variables"
    Private ReadOnly _action As Action
#End Region

    Sub New(myAction As Action)
        _action = myAction
    End Sub

#Region "ICommand Methods"
    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _action()
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return True
    End Function
#End Region
End Class