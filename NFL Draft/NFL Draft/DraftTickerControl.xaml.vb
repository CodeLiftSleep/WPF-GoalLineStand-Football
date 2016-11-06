Imports System.ComponentModel
Imports System.Windows.Media.Animation
Imports System.Windows.Threading
Imports System.Collections.ObjectModel
Imports System.Data

Public Class DraftTickerControl
    Implements INotifyPropertyChanged

    Private _time As TimeSpan
    Dim myrand As New Random
    Dim WithEvents TimeThis As String
    Dim Timer As New Stopwatch
    Dim _updateTime As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        'DataContext = Me

        For i As Integer = 0 To 30
            myrand.Next()
        Next i

        _time = New TimeSpan(0, myrand.Next(1, 10), myrand.Next(1, 59))
        Dim storyDraftTicker As Storyboard = CType(DraftTicker.FindResource("DraftScroll"), Storyboard)
        storyDraftTicker.Begin()
        Timer.Start()
        AddHandler DraftPick.PickMade, AddressOf UpdateTicker
    End Sub
#Region "INotifyPropertyChanged"
    Public Shadows Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Shadows Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub
#End Region

    Public Sub MyTimer(ByVal MyControl As DraftTickerControl)
        While _time.Subtract(Timer.Elapsed) > TimeSpan.Zero
            MyControl.Dispatcher.Invoke(Sub()
                                            UpdateTime = _time.Subtract(Timer.Elapsed).ToString("mm\:ss")
                                        End Sub)
        End While
    End Sub
#Region "Private Variables"
    Private _MyDT As New ObservableCollection(Of DataTable)
    Private _LstGrid As New ObservableCollection(Of DataTable)
#End Region

#Region "Public Properties"
    Public Property MyDT As ObservableCollection(Of DataTable)
        Get
            Return _MyDT
        End Get
        Set(value As ObservableCollection(Of DataTable))
            _MyDT = value
            OnPropertyChanged("MyDT")
        End Set
    End Property
    Public Property LstGrid As ObservableCollection(Of DataTable)
        Get
            Return _LstGrid
        End Get
        Set(value As ObservableCollection(Of DataTable))
            _LstGrid = value
            OnPropertyChanged("LstGrid")
        End Set
    End Property
    Public Property UpdateTime As String
        Get
            Return _updateTime
        End Get
        Set(value As String)
            _updateTime = value
            OnPropertyChanged("UpdateTime")
        End Set
    End Property
#End Region
    ''' <summary>
    ''' Updates the ticker once a new selection is made
    ''' </summary>
    Private Sub UpdateTicker()

    End Sub

End Class