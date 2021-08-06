Imports System.Data.SqlClient
Public Class student
    Dim connection As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")
    Dim datareader As SqlDataReader
    Dim cmd As New SqlCommand
    Dim img_path = "NULL"
    Dim gender = "NULL"

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

    Sub ClearFields()
        Guna2Button2.Visible = False
        Guna2Button3.Visible = False
        gender = "NULL"
        img_path = "NULL"
        Label8.Text = "Upload student pic"
        Label8.ForeColor = Color.Black
        Guna2TextBox1.Clear()
        Guna2TextBox2.Clear()
        Guna2TextBox3.Clear()
        Guna2TextBox4.Clear()
        Guna2TextBox5.Clear()
        Guna2TextBox6.Clear()
        Guna2Button1.Enabled = True
        Guna2ComboBox1.SelectedIndex = -1
        Guna2ComboBox2.SelectedIndex = -1
        Guna2ComboBox3.SelectedIndex = -1
        Guna2DateTimePicker1.Value = Today.Date
        Guna2RadioButton1.Checked = False
        Guna2RadioButton2.Checked = False
        Guna2DateTimePicker2.Value = Today.Date
        PictureBox1.BackgroundImage = Image.FromFile("D:\StudentManagement\StudentManagement\Images\icons8-name-50.png")
        AutoId()
    End Sub

    Sub borderblue()
        Label8.Text = "Upload student pic"
        Label8.ForeColor = Color.Black
        Guna2TextBox1.BorderColor = Color.DodgerBlue
        Guna2TextBox2.BorderColor = Color.DodgerBlue
        Guna2TextBox3.BorderColor = Color.DodgerBlue
        Guna2TextBox4.BorderColor = Color.DodgerBlue
        Guna2TextBox5.BorderColor = Color.DodgerBlue
        Guna2TextBox6.BorderColor = Color.DodgerBlue
        Guna2ComboBox1.BorderColor = Color.DodgerBlue
        Guna2ComboBox2.BorderColor = Color.DodgerBlue
        Guna2ComboBox3.BorderColor = Color.DodgerBlue
        Guna2DateTimePicker1.BorderColor = Color.DodgerBlue
        Guna2DateTimePicker2.BorderColor = Color.DodgerBlue
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        ClearFields()
    End Sub
    Sub AutoId()
        Dim autiId As Single
        connection.Open()
        Dim command As New SqlCommand("SELECT count(*) as COUNT FROM TB_STUDENT", connection)
        datareader = command.ExecuteReader
        While datareader.Read
            autiId = Val(datareader.Item("COUNT").ToString) + 1
        End While

        Select Case Len(Trim(autiId))
            Case 1 : Label15.Text = "ST000" + Trim(Str(autiId))
            Case 2 : Label15.Text = "ST00" + Trim(Str(autiId))
            Case 3 : Label15.Text = "ST0" + Trim(Str(autiId))
        End Select

        connection.Close()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "JPEGs|*.jpg|PNG|*.png|Bitmaps|*.bmp"

        If ofd.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Try
            img_path = ofd.FileName
            Dim bmp As New Bitmap(ofd.FileName)
            PictureBox1.BackgroundImage = bmp
        Catch
            MsgBox("Not a valid image file.")
        End Try
    End Sub
    Sub loadDataGrid()
        connection.Open()
        Dim command As New SqlCommand("SELECT * FROM TB_STUDENT", connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        Guna2DataGridView1.DataSource = table
        connection.Close()
    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        borderblue()
        Dim i = 0
        If (Guna2TextBox1.Text = "") Then
            i = 1
            Guna2TextBox1.BorderColor = Color.Red
        End If
        If (Guna2TextBox2.Text = "") Then
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
        If (Guna2TextBox5.Text = "" Or Guna2TextBox5.Text.Length < 10 Or Guna2TextBox5.Text.Length > 10) Then
            i = 1
            Guna2TextBox5.BorderColor = Color.Red
        End If
        If (Guna2TextBox6.Text = "") Then
            i = 1
            Guna2TextBox6.BorderColor = Color.Red
        End If
        If (Guna2ComboBox1.Text = "") Then
            i = 1
            Guna2ComboBox1.BorderColor = Color.Red
        End If
        If (Guna2ComboBox2.Text = "") Then
            i = 1
            Guna2ComboBox2.BorderColor = Color.Red
        End If
        If (Guna2ComboBox3.Text = "") Then
            i = 1
            Guna2ComboBox3.BorderColor = Color.Red
        End If
        If (img_path = "NULL") Then
            i = 1
            MessageBox.Show("Please upload Student pic")

        End If

        If (gender = "NULL") Then
            i = 1
            MessageBox.Show("Select gender")
        End If


        If (i = 0) Then
            SaveDb()
        End If

        loadDataGrid()
    End Sub

    Sub SaveDb()
        connection.Open()
        cmd = New SqlCommand("insert into TB_STUDENT values ('" + Label15.Text + "','" + Guna2TextBox1.Text + "','" + Guna2TextBox2.Text + "','" + Guna2TextBox3.Text + "','" + Guna2DateTimePicker1.Text + "','" + gender + "','" + Guna2ComboBox1.Text + "','" + Guna2TextBox6.Text + "','" + Guna2DateTimePicker2.Text + "','" + Guna2ComboBox2.Text + "','" + Guna2ComboBox3.Text + "','" + Guna2TextBox4.Text + "','" + Guna2TextBox5.Text + "','" + img_path + "')", connection)
        cmd.ExecuteNonQuery()

        connection.Close()
        MessageBox.Show("Record Saved!")
        clearFields()
        AutoId()
        loadDataGrid()

    End Sub
    Private Sub student_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AutoId()
        loadCourse()
        loadDataGrid()
        Guna2Button3.Visible = False
        Guna2Button2.Visible = False
    End Sub

    Private Sub Guna2RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2RadioButton1.CheckedChanged
        gender = Guna2RadioButton1.Text.Trim
        Guna2RadioButton2.Checked = False
    End Sub

    Private Sub Guna2RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2RadioButton2.CheckedChanged
        gender = Guna2RadioButton2.Text.Trim
        Guna2RadioButton1.Checked = False
    End Sub

    Private Sub Guna2TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox5.KeyPress
        Dim allowedChars As String = "1234567890"
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

    Private Sub Guna2TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox2.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = True
        Else
            If Len(Guna2TextBox2.Text) < 25 Or Char.IsControl(e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Guna2TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox3.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = True
        Else
            If Len(Guna2TextBox3.Text) < 25 Or Char.IsControl(e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        Guna2RadioButton1.Checked = False
        Guna2RadioButton2.Checked = False
        Dim i As Integer
        i = Guna2DataGridView1.CurrentRow.Index
        If IsDBNull(Guna2DataGridView1.Item(0, i).Value) Then
            MessageBox.Show("No Records found!")
            Guna2Button2.Visible = False
            Guna2Button3.Visible = False
            Guna2Button1.Enabled = True

        Else
            Guna2Button1.Enabled = False

            Guna2Button2.Visible = True
            Guna2Button3.Visible = True

            Me.Label15.Text = Guna2DataGridView1.Item(0, i).Value
            Me.Guna2TextBox1.Text = Guna2DataGridView1.Item(1, i).Value
            Me.Guna2TextBox2.Text = Guna2DataGridView1.Item(2, i).Value
            Me.Guna2TextBox3.Text = Guna2DataGridView1.Item(3, i).Value

            Me.Guna2DateTimePicker1.Value = Guna2DataGridView1.Item(4, i).Value



            If Guna2DataGridView1.Item(5, i).Value.ToString.StartsWith("Male") Then
                Me.Guna2RadioButton1.Checked = True
                gender = "Male"
            Else
                Me.Guna2RadioButton2.Checked = True
                gender = "Female"
            End If

            Guna2ComboBox1.SelectedItem = Guna2DataGridView1.Item(6, i).Value
            Me.Guna2TextBox6.Text = Guna2DataGridView1.Item(7, i).Value
            Me.Guna2DateTimePicker2.Value = Guna2DataGridView1.Item(8, i).Value
            Me.Guna2ComboBox2.SelectedItem = Guna2DataGridView1.Item(9, i).Value
            Me.Guna2ComboBox3.SelectedItem = Guna2DataGridView1.Item(10, i).Value

            Me.Guna2TextBox4.Text = Guna2DataGridView1.Item(11, i).Value
            Me.Guna2TextBox5.Text = Guna2DataGridView1.Item(12, i).Value
            img_path = Guna2DataGridView1.Item(13, i).Value
            Me.PictureBox1.BackgroundImage = Image.FromFile(Guna2DataGridView1.Item(13, i).Value)
        End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        If (Label15.Text = "-") Then
            MessageBox.Show("No records found!")
        Else
            Dim result = MessageBox.Show("Do you really want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                connection.Open()
                cmd.Connection = connection
                cmd.CommandText = "Delete From TB_STUDENT where S_id= '" + Label15.Text + "'"
                cmd.ExecuteNonQuery()
                connection.Close()

                MessageBox.Show("Record Deleted!")
                loadDataGrid()
                ClearFields()
            End If
        End If

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        borderblue()
        Dim j = 0
        If (Guna2TextBox1.Text = "") Then
            j = 1
            Guna2TextBox1.BorderColor = Color.Red
        End If
        If (Guna2TextBox2.Text = "") Then
            j = 1
            Guna2TextBox2.BorderColor = Color.Red
        End If
        If (Guna2TextBox3.Text = "") Then
            j = 1
            Guna2TextBox3.BorderColor = Color.Red
        End If
        If (Guna2TextBox4.Text = "") Then
            j = 1
            Guna2TextBox4.BorderColor = Color.Red
        End If
        If (Guna2TextBox5.Text = "" Or Guna2TextBox5.Text.Length < 10 Or Guna2TextBox5.Text.Length > 10) Then
            j = 1
            Guna2TextBox5.BorderColor = Color.Red
        End If
        If (Guna2TextBox6.Text = "") Then
            j = 1
            Guna2TextBox6.BorderColor = Color.Red
        End If
        If (Guna2ComboBox1.Text = "") Then
            j = 1
            Guna2ComboBox1.BorderColor = Color.Red
        End If
        If (Guna2ComboBox2.Text = "") Then
            j = 1
            Guna2ComboBox2.BorderColor = Color.Red
        End If
        If (Guna2ComboBox3.Text = "") Then
            j = 1
            Guna2ComboBox3.BorderColor = Color.Red
        End If
        If (img_path = "NULL") Then
            j = 1
            Label8.Text = "Select Pic"
            Label8.ForeColor = Color.Red
        End If

        If (gender = "NULL") Then
            j = 1
            MessageBox.Show("Select gender")
        End If


        If (j = 0) Then
            Dim result = MessageBox.Show("All the changes made will be updated \n Do you want to update record?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                connection.Open()
                Dim cmdd As New SqlCommand("UPDATE TB_STUDENT SET  S_id='" + Label15.Text + "',Reg_No='" + Guna2TextBox1.Text + "',Name='" + Guna2TextBox2.Text + "',Father_Name='" + Guna2TextBox3.Text + "',DOB='" + Guna2DateTimePicker2.Text + "',Gender='" + gender + "',Religion='" + Guna2ComboBox1.Text + "',Caste='" + Guna2TextBox6.Text + "',Date_of_join='" + Guna2DateTimePicker2.Value + "',Course='" + Guna2ComboBox2.Text + "',Sem='" + Guna2ComboBox3.Text + "',Address='" + Guna2TextBox4.Text + "',Phone='" + Guna2TextBox5.Text + "',Pic='" + img_path + "'  WHERE S_id ='" + Label15.Text + "'", connection)
                cmdd.ExecuteNonQuery()
                MessageBox.Show("Record Updated!")
                connection.Close()
                loadDataGrid()
                ClearFields()
                AutoId()
            End If
        End If


    End Sub


End Class