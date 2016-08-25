Imports System.ComponentModel
Imports System.Windows.Media.Animation
Imports System.Windows.Threading

Public Class DraftTickerControl
    Implements INotifyPropertyChanged

    Private _time As TimeSpan
    Dim myrand As New Random
    Dim Min As Integer
    Dim Sec As Integer
    Dim WithEvents TimeThis As String
    Dim Timer As New Stopwatch
    Dim _updateTime As String

    Public Sub New()

        For i As Integer = 0 To 30
            myrand.Next()
        Next i
        Min = myrand.Next(1, 10)
        Sec = myrand.Next(1, 59)
        _time = New TimeSpan(0, Min, Sec)
        ' This call is required by the designer.
        InitializeComponent()
        DataContext = Me
        ' Add any initialization after the InitializeComponent() call.
        Dim storyDraftTicker As Storyboard = CType(DraftTicker.FindResource("DraftScroll"), Storyboard)
        storyDraftTicker.Begin()
        Timer.Start()

    End Sub
#Region "INotifyPropertyChanged"
    Public Shadows Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Shadows Sub OnPropertyChanged(name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub
#End Region

    Public Sub MyTimer(ByVal MyControl As DraftTickerControl)
        'Dim task As Task
        'txtClock.Text
        While _time.Subtract(Timer.Elapsed) > TimeSpan.Zero
            MyControl.Dispatcher.Invoke(Sub()
                                            UpdateTime = _time.Subtract(Timer.Elapsed).ToString("mm\:ss")
                                        End Sub)
            ' UpdateTime = _time.Subtract(Timer.Elapsed)
        End While

    End Sub

    Public Property UpdateTime As String
        Get
            Return _updateTime
        End Get
        Set(value As String)
            _updateTime = value
            OnPropertyChanged("UpdateTime")
        End Set
    End Property

End Class