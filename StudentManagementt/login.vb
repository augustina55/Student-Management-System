Imports System.Data.SqlClient
Public Class login


    Private Sub Guna2CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2CheckBox1.CheckedChanged
        If (Guna2CheckBox1.Checked = True) Then
            Guna2CheckBox1.Text = "Hide Password"
            Guna2TextBox2.PasswordChar = ""
        Else
            Guna2CheckBox1.Text = "Show Password"
            Guna2TextBox2.PasswordChar = "*"
        End If
    End Sub

    Private Sub Guna2PictureBox2_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox2.Click
        Dim result = MessageBox.Show(" Are you sure you want to quit", "Are you sure?",
                                    MessageBoxButtons.YesNoCancel)

        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim i = 0
        Guna2TextBox1.BorderColor = Color.DodgerBlue
        Guna2TextBox2.BorderColor = Color.DodgerBlue
        If (Guna2TextBox1.Text = "") Then
            i = 1
            Guna2TextBox1.BorderColor = Color.Red
        End If
        If (Guna2TextBox2.Text = "") Then
            i = 1
            Guna2TextBox2.BorderColor = Color.Red
        End If
        If (i = 0) Then
            Dim connection As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")
            Dim command As New SqlCommand("select * from TB_ADMIN where Name = @username and Password = @password", connection)

            command.Parameters.Add("@username", SqlDbType.VarChar).Value = Guna2TextBox1.Text
            command.Parameters.Add("@password", SqlDbType.VarChar).Value = Guna2TextBox2.Text
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)

            If table.Rows.Count() <= 0 Then
                MessageBox.Show("Username Or Password are invalid", "Ops", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Login Successfully")
                Dim lg = New Homepage
                lg.Show()
                Me.Hide()
            End If
        End If

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        MessageBox.Show("Please Contact Admin for new Account", "Restricted", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class