Imports System.Data.SqlClient
Public Class admin
    Dim connection As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")
    Dim mycmd As New SqlCommand
    Dim dataAdapter As New SqlDataAdapter
    Dim dtable As New DataTable
    Dim datareader As SqlDataReader
    Dim cmd As New SqlCommand
    Sub clearFields()
        Guna2TextBox1.Clear()
        Guna2TextBox2.Clear()
        Guna2TextBox3.Clear()
        Guna2TextBox4.Clear()
        Guna2TextBox5.Clear()
        AutoId()
    End Sub

    Sub AutoId()
        Dim autiId As Single
        connection.Open()
        Dim command As New SqlCommand("SELECT count(*) as COUNT FROM TB_ADMIN", connection)
        datareader = command.ExecuteReader
        While datareader.Read
            autiId = Val(datareader.Item("COUNT").ToString) + 1
        End While

        Select Case Len(Trim(autiId))
            Case 1 : Label4.Text = "U000" + Trim(Str(autiId))
            Case 2 : Label4.Text = "U00" + Trim(Str(autiId))
            Case 3 : Label4.Text = "U0" + Trim(Str(autiId))
        End Select

        connection.Close()

    End Sub
    Private Sub Guna2TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox1.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = True
        Else
            If Len(Guna2TextBox1.Text) < 25 Or Char.IsControl(e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Guna2TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox2.KeyPress
        Dim allowedChars As String = "1234567890"
        If (e.KeyChar.ToString = ".") And (Guna2TextBox2.Text.Contains(e.KeyChar.ToString)) Then
            e.Handled = True
            Exit Sub
        End If

        If allowedChars.IndexOf(e.KeyChar) = -1 AndAlso
            Not e.KeyChar = ChrW(8) Then
            ' Invalid Character
            e.Handled = True
        End If
    End Sub

    Sub borderblue()
        Guna2TextBox1.BorderColor = Color.DodgerBlue
        Guna2TextBox2.BorderColor = Color.DodgerBlue
        Guna2TextBox3.BorderColor = Color.DodgerBlue
        Guna2TextBox4.BorderColor = Color.DodgerBlue
        Guna2TextBox5.BorderColor = Color.DodgerBlue
    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        borderblue()
        Dim i = 0
        If (Guna2TextBox1.Text = "") Then
            i = 1
            Guna2TextBox1.BorderColor = Color.Red
        End If
        If (Guna2TextBox2.Text = "" Or Guna2TextBox2.Text.Length < 10) Then
            i = 1
            Guna2TextBox2.BorderColor = Color.Red
        End If
        If (Guna2TextBox3.Text = "") Then
            i = 1
            Guna2TextBox3.BorderColor = Color.Red
        End If
        If (Guna2TextBox4.Text = "") Then
            i = 1
            Guna2TextBox4.BorderColor = Color.Red
        End If
        If (Guna2TextBox5.Text = "") Then
            i = 1
            Guna2TextBox5.BorderColor = Color.Red
        End If
        If (i = 0) Then
            SaveDb()
        End If
    End Sub
    Sub loadDataGrid()
        connection.Open()
        Dim command As New SqlCommand("SELECT * FROM TB_ADMIN", connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        Guna2DataGridView1.DataSource = table
        connection.Close()
    End Sub
    Sub SaveDb()
        connection.Open()
        cmd = New SqlCommand("insert into TB_ADMIN values ('" + Label4.Text + "','" + Guna2TextBox1.Text + "','" + Guna2TextBox2.Text + "','" + Guna2TextBox3.Text + "','" + Guna2TextBox4.Text + "','" + Guna2TextBox5.Text + "')", connection)
        cmd.ExecuteNonQuery()
        connection.Close()
        MessageBox.Show("Record Saved!")
        clearFields()
        AutoId()
        loadDataGrid()
    End Sub

    Private Sub admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2Button3.Visible = False
        AutoId()
        loadDataGrid()
    End Sub



    Private Sub Guna2Button4_Click_1(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        clearFields()
    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        Dim i As Integer = -1
        i = Guna2DataGridView1.CurrentRow.Index
        If IsDBNull(Guna2DataGridView1.Item(0, i).Value) Then
            MessageBox.Show("No Records found!")
            Guna2Button3.Visible = False
        Else
            Guna2Button3.Visible = True
            Me.Label4.Text = Guna2DataGridView1.Item(0, i).Value
            Me.Guna2TextBox1.Text = Guna2DataGridView1.Item(1, i).Value
            Me.Guna2TextBox2.Text = Guna2DataGridView1.Item(2, i).Value
            Me.Guna2TextBox3.Text = Guna2DataGridView1.Item(3, i).Value
            Me.Guna2TextBox4.Text = Guna2DataGridView1.Item(4, i).Value
            Me.Guna2TextBox5.Text = Guna2DataGridView1.Item(5, i).Value
        End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        If (Label4.Text = "-") Then
            MessageBox.Show("No records found!")
        Else
            Dim result = MessageBox.Show("Do you really want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                connection.Open()
                cmd.Connection = connection
                cmd.CommandText = "Delete From TB_ADMIN where user_Id= '" + Label4.Text + "'"
                cmd.ExecuteNonQuery()
                connection.Close()

                MessageBox.Show("Record Deleted!")
            End If

        End If
        loadDataGrid()
        clearFields()
    End Sub


End Class