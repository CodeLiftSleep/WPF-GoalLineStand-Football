Imports NFL_Draft

Class MainWindow
    Public Shared NewGameScreen As New NewGame

    Private Sub NewGameBtn_Click(sender As Object, e As RoutedEventArgs) Handles NewGameBtn.Click

        NewGameScreen.Show()
        GetWindow(NewGameScreen)
    End Sub

    Private Sub QuitGameBtn_Click(sender As Object, e As RoutedEventArgs) Handles QuitGameBtn.Click

        Dim res As MsgBoxResult = MsgBox("Are You Sure You Want To Exit The Game? Any Unsaved Data will be lost!",
                                         MsgBoxStyle.OkCancel, "Exit Game")
        If res = MsgBoxResult.Ok Then
            End
        End If
    End Sub

End Class