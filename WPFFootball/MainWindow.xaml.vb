Imports NFL_Draft

Class MainWindow
    Public Shared NewGameScreen As New NewGame
    Sub New()
        Try
            InitializeComponent()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub NewGameBtn_Click(sender As Object, e As RoutedEventArgs) Handles NewGameBtn.Click
        NewGameScreen.Show()
        Me.Close()
    End Sub

    Private Sub QuitGameBtn_Click(sender As Object, e As RoutedEventArgs) Handles QuitGameBtn.Click

        Dim res As MsgBoxResult = MsgBox("Are You Sure You Want To Exit The Game? Any Unsaved Data will be lost!",
                                         MsgBoxStyle.OkCancel, "Exit Game")
        If res = MsgBoxResult.Ok Then
            End
        End If
    End Sub

End Class