Imports System.ComponentModel

Public Class UserScreenViewModel
    Inherits NewGameViewModel
    Implements INotifyPropertyChanged

    Sub New()

    End Sub

#Region "INotifyPropertyChanged"

    Public Shadows Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    private Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

#End Region

#Region "Private Variables"
    Private _image1 as ImageSource
    Private _image2 as ImageSource
    Private _image3 as ImageSource
    Private _image4 as ImageSource
    Private _image5 as ImageSource
#End Region

#Region "Public Properties"
    Public Property Image1 as ImageSource
        Get
            return _image1
        End Get
        Set(value As ImageSource)
            _image1 = value
            OnPropertyChanged("Image1")
        End Set
    End Property
    Public Property Image2 as ImageSource
        Get
            return _image2
        End Get
        Set(value as ImageSource)
            _image2 = value
            OnPropertyChanged("Image2")
        End Set
    End Property
    Public Property Image3 as ImageSource
        Get
            return _image3
        End Get
        Set(value as ImageSource)
            _image3 = value
            OnPropertyChanged("Image3")
        End Set
    End Property
    Public Property Image4 as ImageSource
        Get
            return _image4
        End Get
        Set(value as ImageSource)
            _image4 = value
            OnPropertyChanged("Image4")
        End Set
    End Property
    Public Property Image5 as ImageSource
        Get
            return _image5
        End Get
        Set(value as ImageSource)
            _image5 = value
            OnPropertyChanged("Image5")
        End Set
    End Property
#End Region

End Class