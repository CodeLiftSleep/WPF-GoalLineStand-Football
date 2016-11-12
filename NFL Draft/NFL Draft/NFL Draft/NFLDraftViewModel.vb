Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data

Public Class NFLDraftViewModel
    Implements INotifyPropertyChanged

#Region "INotifyPropertyChanged"

    Public Shadows Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

#End Region

End Class