Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data

Public Class NewGame2VM
    Implements INotifyPropertyChanged

#Region "INotifyPropertyChanged"

    Public Shadows Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

#End Region
#Region "Private Variables"

#End Region

#Region "Public Properties"

    Public Property leagueName As String
    Public Property conferenceOne As String
    Public Property conferenceTwo As String

#End Region

End Class