Class MainWindow
    Public shared NewGameScreen As New NewGame

    Private Sub NewGameBtn_Click(sender As Object, e As RoutedEventArgs) Handles NewGameBtn.Click

        NewGameScreen.Show()
        GetWindow(NewGameScreen)
    End Sub

    Private Sub QuitGameBtn_Click(sender As Object, e As RoutedEventArgs) Handles QuitGameBtn.Click

        Dim res As MsgBoxResult = MsgBox("Are You Sure You Want To Exit The Game? Any Unsaved Data will be lost!",
                                         MsgBoxStyle.OkCancel, "Exit Game")
        if res = MsgBoxResult.Ok Then
            End
        end if
    End Sub
End Class