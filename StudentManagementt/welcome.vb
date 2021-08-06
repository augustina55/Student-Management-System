Public Class welcome
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If (Timer1.Interval = 4000) Then
            Timer1.Stop()
            Dim lg = New login
            lg.Show()
            Me.Hide()
        End If


    End Sub
End Class
