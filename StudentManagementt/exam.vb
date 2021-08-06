Imports System.Data.SqlClient
Public Class exam
    Dim connection As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")
    Dim datareader As SqlDataReader
    Dim cmd As New SqlCommand
    Dim percentage = 0.0
    Sub CalPercentage()
        If (Guna2TextBox5.Text.Length > 0 And Guna2TextBox3.Text.Length > 0) Then
            If (Double.Parse(Guna2TextBox5.Text) > 0 And Double.Parse(Guna2TextBox3.Text) > 0) Then
                percentage = Math.Round(((Double.Parse(Guna2TextBox5.Text) * 100) / Double.Parse(Guna2TextBox3.Text)), 2)
                Label9.Text = percentage.ToString + "%"
            End If
        End If
    End Sub
    Sub loadDataGrid()
        connection.Open()
        Dim command As New SqlCommand("SELECT * FROM TB_EXAM", connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        Guna2DataGridView1.DataSource = table
        connection.Close()
    End Sub

    Private Sub exam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadDataGrid()
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        If Not (Guna2TextBox4.Text = "") Then
            connection.Open()
            Dim command As New SqlCommand("SELECT S_id,Name FROM TB_STUDENT WHERE CONCAT(S_id,Reg_No,Name) like '%" & Guna2TextBox4.Text & "%'", connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            If table.Rows.Count > 0 Then
                For i As Integer = 0 To table.Rows.Count - 1
                    Label4.Text = table.Rows(i)(0)
                    Guna2TextBox1.Text = table.Rows(i)(1)
                Next
            Else
                MessageBox.Show("No Records found!")
            End If
            connection.Close()
        End If
    End Sub

    Sub ClearFields()
        Label4.Text = "-"
        Guna2TextBox1.Clear()
        Guna2TextBox2.Clear()
        Guna2TextBox3.Clear()
        Guna2TextBox4.Clear()
        Guna2TextBox5.Clear()
        Label9.Text = "%"
        bordercolor()
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        ClearFields()
    End Sub
    Sub bordercolor()
        Guna2TextBox3.BorderColor = Color.DodgerBlue
        Guna2TextBox5.BorderColor = Color.DodgerBlue
        Guna2TextBox2.BorderColor = Color.DodgerBlue
    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        bordercolor()
        Dim i = 0
        If (Label4.Text = "-") Then
            i = 1
            MessageBox.Show("Student record missing!")
        End If
        If (Guna2TextBox2.Text = "") Then
            i = 1
            Guna2TextBox2.BorderColor = Color.Red
        End If
        If (Guna2TextBox3.Text = "") Then
            i = 1
            Guna2TextBox3.BorderColor = Color.Red
        End If
        If (Guna2TextBox5.Text = "") Then
            i = 1
            Guna2TextBox5.BorderColor = Color.Red
        End If

        If (i = 0) Then
            connection.Open()
            cmd = New SqlCommand("insert into TB_EXAM values ('" + Label4.Text + "','" + Guna2TextBox1.Text + "','" + Guna2TextBox2.Text + "','" + Guna2TextBox5.Text + "','" + Guna2TextBox3.Text + "','" + percentage.ToString + "')", connection)
            cmd.ExecuteNonQuery()
            connection.Close()
            MessageBox.Show("Record Saved!")
            ClearFields()
            loadDataGrid()
        End If
    End Sub

    Private Sub Guna2TextBox5_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox5.TextChanged
        CalPercentage()
    End Sub

    Private Sub Guna2TextBox3_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox3.TextChanged
        CalPercentage()
    End Sub

    Private Sub Guna2TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox5.KeyPress
        Dim allowedChars As String = "1234567890."
        If (e.KeyChar.ToString = ".") And (Guna2TextBox5.Text.Contains(e.KeyChar.ToString)) Then
            e.Handled = True
            Exit Sub
        End If

        If allowedChars.IndexOf(e.KeyChar) = -1 AndAlso
            Not e.KeyChar = ChrW(8) Then
            ' Invalid Character
            e.Handled = True
        End If
    End Sub

    Private Sub Guna2TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox3.KeyPress
        Dim allowedChars As String = "1234567890."
        If (e.KeyChar.ToString = ".") And (Guna2TextBox3.Text.Contains(e.KeyChar.ToString)) Then
            e.Handled = True
            Exit Sub
        End If

        If allowedChars.IndexOf(e.KeyChar) = -1 AndAlso
            Not e.KeyChar = ChrW(8) Then
            ' Invalid Character
            e.Handled = True
        End If
    End Sub
End Class