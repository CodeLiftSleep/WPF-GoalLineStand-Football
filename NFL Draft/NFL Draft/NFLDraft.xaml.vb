Class NFLDraft
    Dim MyDraft As New DraftTickerControl
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        MyDraft.Visibility = Visibility.Visible

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Async Sub Button_Click(sender As Object, e As RoutedEventArgs)
        stkTest.Children.Add(MyDraft)
        Await Task.Run(Sub() MyDraft.MyTimer(MyDraft))
    End Sub
End Class