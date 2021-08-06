Imports System.Data.SqlClient
Public Class Fees
    Dim connection As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")
    Dim datareader As SqlDataReader
    Dim cmd As New SqlCommand

    Sub loadDataGrid()
        connection.Open()
        Dim command As New SqlCommand("SELECT * FROM TB_FEES", connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        Guna2DataGridView1.DataSource = table
        connection.Close()
    End Sub

    Sub AutoFeesId()
        Dim autiId As Single
        connection.Open()
        Dim command As New SqlCommand("SELECT count(*) as COUNT FROM TB_FEES", connection)
        datareader = command.ExecuteReader
        While datareader.Read
            autiId = Val(datareader.Item("COUNT").ToString) + 1
        End While

        Select Case Len(Trim(autiId))
            Case 1 : Label5.Text = "F0" + Trim(Str(autiId))
            Case 2 : Label5.Text = "F00" + Trim(Str(autiId))
            Case 3 : Label5.Text = "F000" + Trim(Str(autiId))
        End Select

        connection.Close()

    End Sub

    Sub loadCourse()
        Using conn As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")
            Using da As SqlDataAdapter = New SqlDataAdapter("SELECT Course from TB_COURSE", conn)
                Dim dt As DataTable = New DataTable
                da.Fill(dt)
                Guna2ComboBox2.ValueMember = "Course"
                Guna2ComboBox2.DisplayMember = "Course"
                Guna2ComboBox2.DataSource = dt
            End Using
        End Using
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        If Not (Guna2TextBox1.Text = "") Then
            connection.Open()
            Dim command As New SqlCommand("SELECT S_id,Name FROM TB_STUDENT WHERE CONCAT(S_id,Reg_No,Name) like '%" & Guna2TextBox1.Text & "%'", connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            If table.Rows.Count > 0 Then
                For i As Integer = 0 To table.Rows.Count - 1
                    Label9.Text = table.Rows(i)(0)
                    Guna2TextBox2.Text = table.Rows(i)(1)
                Next
            Else
                MessageBox.Show("No Records found!")
            End If
            connection.Close()
        End If
    End Sub

    Private Sub Fees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AutoFeesId()
        loadCourse()
        Guna2Button3.Visible = False
        Guna2Button2.Visible = False
        Bordercolor()
        loadDataGrid()
    End Sub

    Private Sub Guna2TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox4.KeyPress
        Dim allowedChars As String = "1234567890."
        If (e.KeyChar.ToString = ".") And (Guna2TextBox4.Text.Contains(e.KeyChar.ToString)) Then
            e.Handled = True
            Exit Sub
        End If

        If allowedChars.IndexOf(e.KeyChar) = -1 AndAlso
            Not e.KeyChar = ChrW(8) Then
            ' Invalid Character
            e.Handled = True
        End If
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Checkrecords("ADD")
    End Sub

    Sub Bordercolor()
        Guna2TextBox3.BorderColor = Color.DodgerBlue
        Guna2TextBox4.BorderColor = Color.DodgerBlue
        Guna2DateTimePicker1.BorderColor = Color.DodgerBlue
        Guna2ComboBox2.BorderColor = Color.DodgerBlue
    End Sub
    Sub Checkrecords(str As String)
        Bordercolor()
        Dim i = 0
        If (Label9.Text = "-") Then
            i = 1
            MessageBox.Show("Student record missing!")
        End If
        If (Guna2TextBox3.Text = "") Then
            i = 1
            Guna2TextBox3.BorderColor = Color.Red
        End If
        If (Guna2TextBox4.Text = "") Then
            i = 1
            Guna2TextBox4.BorderColor = Color.Red
        End If


        If (i = 0) Then
            If (str = "ADD") Then
                savedb()
            ElseIf (str = "UPDATE") Then
                updateDb()
            End If
        End If

    End Sub

    Sub savedb()
        connection.Open()
        cmd = New SqlCommand("insert into TB_FEES values ('" + Label5.Text + "','" + Label9.Text + "','" + Guna2TextBox2.Text + "','" + Guna2ComboBox2.Text + "','" + Guna2DateTimePicker1.Text + "','" + Guna2TextBox4.Text + "','" + Guna2TextBox3.Text + "')", connection)
        cmd.ExecuteNonQuery()
        connection.Close()
        MessageBox.Show("Record Saved!")
        clearFields()
        AutoFeesId()
        loadDataGrid()
    End Sub

    Sub updateDb()
        Dim result = MessageBox.Show("All the changes made will be updated \n Do you want to update record?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            connection.Open()
            Dim cmdd As New SqlCommand("UPDATE TB_FEES SET  F_Id='" + Label5.Text + "',S_id='" + Label9.Text + "',Name='" + Guna2TextBox2.Text + "',Course='" + Guna2ComboBox2.Text + "',Date='" + Guna2DateTimePicker1.Text + "',Fees='" + Guna2TextBox4.Text + "',Description='" + Guna2TextBox3.Text + "' WHERE F_id ='" + Label5.Text + "'", connection)
            cmdd.ExecuteNonQuery()
            MessageBox.Show("Record Updated!")
            connection.Close()
            clearFields()
            AutoFeesId()
            loadDataGrid()
        End If

    End Sub

    Sub clearFields()
        AutoFeesId()
        loadCourse()
        Label9.Text = "-"
        Guna2Button5.Enabled = True
        Guna2DateTimePicker1.Value = Today.Date
        Guna2TextBox2.Clear()
        Guna2TextBox3.Clear()
        Guna2TextBox4.Clear()
        Guna2Button3.Visible = False
        Guna2Button2.Visible = False
    End Sub
    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        clearFields()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Checkrecords("UPDATE")
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Dim result = MessageBox.Show("Do you really want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            connection.Open()
            cmd.Connection = connection
            cmd.CommandText = "Delete From TB_FEES where F_Id= '" + Label5.Text + "'"
            cmd.ExecuteNonQuery()
            connection.Close()
            MessageBox.Show("Record Deleted!")
            loadDataGrid()
            clearFields()
            AutoFeesId()
        End If

    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        Dim i As Integer
        i = Guna2DataGridView1.CurrentRow.Index
        If IsDBNull(Guna2DataGridView1.Item(0, i).Value) Then
            Guna2Button3.Visible = False
            Guna2Button2.Visible = False
            MessageBox.Show("No Records found!")
            Guna2Button5.Enabled = True
        Else
            Guna2Button5.Enabled = False
            Guna2Button3.Visible = True
            Guna2Button2.Visible = True
            Me.Label5.Text = Guna2DataGridView1.Item(0, i).Value
            Me.Label9.Text = Guna2DataGridView1.Item(1, i).Value
            Me.Guna2TextBox2.Text = Guna2DataGridView1.Item(2, i).Value

            Me.Guna2ComboBox2.SelectedItem = Guna2DataGridView1.Item(3, i).Value
            Me.Guna2DateTimePicker1.Value = Guna2DataGridView1.Item(4, i).Value
            Me.Guna2TextBox4.Text = Guna2DataGridView1.Item(5, i).Value
            Me.Guna2TextBox3.Text = Guna2DataGridView1.Item(6, i).Value
        End If
    End Sub
End Class