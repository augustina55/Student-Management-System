Public Class Homepage
    Dim btn = 1
    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click
        If (btn = 1) Then
            Guna2Panel3.Width = 200
            btn = 2
        Else
            btn = 1
            Guna2Panel3.Width = 40
        End If
    End Sub

    Private Sub Homepage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2Panel3.Width = 40
        Guna2Panel4.Controls.Clear()

        Dim frm = New home

        frm.TopLevel = False
        Guna2Panel4.Controls.Add(frm)
        frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frm.Show()

    End Sub

    Private Sub Guna2PictureBox2_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox2.Click
        Dim result = MessageBox.Show(" Are you sure you want to quit", "Are you sure?",
                                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            Application.Exit()
        End If

    End Sub


    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Guna2Panel3.Width = 40
        Guna2Panel4.Controls.Clear()
        Dim frm = New home
        frm.TopLevel = False
        Guna2Panel4.Controls.Add(frm)
        frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frm.Show()

    End Sub

    Private Sub Guna2GradientButton7_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton7.Click
        Guna2Panel3.Width = 40
        Dim result = MessageBox.Show(" Are you sure you want to logout", "Are you sure?",
                                  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

        If result = DialogResult.Yes Then
            Me.Close()
            Dim lg = New login
            lg.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        Guna2Panel3.Width = 40
        Guna2Panel4.Controls.Clear()

        Dim frm = New student
        frm.TopLevel = False
        Guna2Panel4.Controls.Add(frm)
        frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frm.Show()
    End Sub

    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        Guna2Panel3.Width = 40
        Guna2Panel4.Controls.Clear()

        Dim frm = New department
        frm.TopLevel = False
        Guna2Panel4.Controls.Add(frm)
        frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frm.Show()
    End Sub

    Private Sub Guna2GradientButton4_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton4.Click
        Guna2Panel3.Width = 40
        Guna2Panel4.Controls.Clear()

        Dim frm = New Fees
        frm.TopLevel = False
        Guna2Panel4.Controls.Add(frm)
        frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frm.Show()
    End Sub

    Private Sub Guna2GradientButton8_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton8.Click
        Guna2Panel3.Width = 40
        Guna2Panel4.Controls.Clear()

        Dim frm = New Attendance
        frm.TopLevel = False
        Guna2Panel4.Controls.Add(frm)
        frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frm.Show()
    End Sub

    Private Sub Guna2GradientButton5_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton5.Click
        Guna2Panel3.Width = 40
        Guna2Panel4.Controls.Clear()

        Dim frm = New exam
        frm.TopLevel = False
        Guna2Panel4.Controls.Add(frm)
        frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frm.Show()
    End Sub

    Private Sub Guna2GradientButton6_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton6.Click
        Guna2Panel3.Width = 40
        Guna2Panel4.Controls.Clear()

        Dim frm = New admin
        frm.TopLevel = False
        Guna2Panel4.Controls.Add(frm)
        frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frm.Show()
    End Sub

    Private Sub Guna2PictureBox3_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Guna2GradientButton9_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton9.Click
        Guna2Panel3.Width = 40
        Guna2Panel4.Controls.Clear()

        Dim frm = New search
        frm.TopLevel = False
        Guna2Panel4.Controls.Add(frm)
        frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frm.Show()
    End Sub

    Private Sub Guna2Panel3_MouseHover(sender As Object, e As EventArgs)
        ShowMenu()
    End Sub

    Private Sub Guna2Panel3_MouseLeave(sender As Object, e As EventArgs)
        HideMenu()
    End Sub
    Sub HideMenu()
        If (Guna2Panel3.Width = 200) Then
            Guna2Panel3.Width = 40
        End If
    End Sub
    Sub ShowMenu()
        If (Guna2Panel3.Width = 40) Then
            Guna2Panel3.Width = 200
        End If
    End Sub

    Private Sub Guna2GradientButton1_MouseHover(sender As Object, e As EventArgs) Handles Guna2Panel3.MouseHover, Guna2GradientButton9.MouseHover, Guna2GradientButton8.MouseHover, Guna2GradientButton7.MouseHover, Guna2GradientButton6.MouseHover, Guna2GradientButton5.MouseHover, Guna2GradientButton4.MouseHover, Guna2GradientButton3.MouseHover, Guna2GradientButton2.MouseHover, Guna2GradientButton1.MouseHover
        ShowMenu()
    End Sub

    Private Sub Guna2GradientButton1_MouseLeave(sender As Object, e As EventArgs) Handles Guna2Panel3.MouseLeave, Guna2GradientButton9.MouseLeave, Guna2GradientButton8.MouseLeave, Guna2GradientButton7.MouseLeave, Guna2GradientButton6.MouseLeave, Guna2GradientButton5.MouseLeave, Guna2GradientButton4.MouseLeave, Guna2GradientButton3.MouseLeave, Guna2GradientButton2.MouseLeave, Guna2GradientButton1.MouseLeave
        'HideMenu()
    End Sub
End Class