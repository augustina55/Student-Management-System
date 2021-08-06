Imports System.Data.SqlClient
Public Class department
    Dim connection As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")
    Dim mycmd As New SqlCommand
    Dim dataAdapter As New SqlDataAdapter
    Dim dtable As New DataTable
    Dim datareader As SqlDataReader
    Dim cmd As New SqlCommand
    Dim Dpt_No = "NULL"

    Sub loadDepartmentDataGrid()
        connection.Open()
        Dim command As New SqlCommand("SELECT * FROM TB_DEPARTMENT", connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        Guna2DataGridView1.DataSource = table
        connection.Close()
    End Sub
    Sub loadCourseDataGrid()
        connection.Open()
        Dim command As New SqlCommand("SELECT * FROM TB_COURSE", connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        Guna2DataGridView2.DataSource = table
        connection.Close()
    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If (Guna2TextBox1.Text = "") Then
            MessageBox.Show("Enter Department name")
        Else

            connection.Open()
            cmd = New SqlCommand("SELECT count(*) FROM TB_DEPARTMENT where Department='" + Guna2TextBox1.Text.Trim + "' ", connection)
            Dim count = Convert.ToString(cmd.ExecuteScalar())

            If (count = 1) Then
                MessageBox.Show(Guna2TextBox1.Text + " Department already saved in the table")
            Else
                cmd = New SqlCommand("insert into TB_DEPARTMENT values ('" + Label4.Text + "','" + Guna2TextBox1.Text + "')", connection)
                cmd.ExecuteNonQuery()

                MessageBox.Show("Record Saved!")
                Guna2TextBox1.Clear()
                AutoDepartmentId()
                loadDepartmentDataGrid()
                loadDepartment()
            End If
            connection.Close()
        End If
    End Sub
    Sub AutoDepartmentId()
        Dim autiId As Single
        connection.Open()
        Dim command As New SqlCommand("SELECT count(*) as COUNT FROM TB_DEPARTMENT", connection)
        datareader = command.ExecuteReader
        While datareader.Read
            autiId = Val(datareader.Item("COUNT").ToString) + 1
        End While

        Select Case Len(Trim(autiId))
            Case 1 : Label4.Text = "D0" + Trim(Str(autiId))
            Case 2 : Label4.Text = "D00" + Trim(Str(autiId))
            Case 3 : Label4.Text = "D000" + Trim(Str(autiId))
        End Select

        connection.Close()

    End Sub
    Sub AutoCourseId()
        Dim autiId As Single
        connection.Open()
        Dim command As New SqlCommand("SELECT count(*) as COUNT FROM TB_COURSE", connection)
        datareader = command.ExecuteReader
        While datareader.Read
            autiId = Val(datareader.Item("COUNT").ToString) + 1
        End While

        Select Case Len(Trim(autiId))
            Case 1 : Label5.Text = "C0" + Trim(Str(autiId))
            Case 2 : Label5.Text = "C00" + Trim(Str(autiId))
            Case 3 : Label5.Text = "C000" + Trim(Str(autiId))
        End Select

        connection.Close()

    End Sub

    Sub loadDepartment()

        Using conn As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")
            Using da As SqlDataAdapter = New SqlDataAdapter("SELECT Department from TB_DEPARTMENT", conn)
                Dim dt As DataTable = New DataTable
                da.Fill(dt)
                Guna2ComboBox1.ValueMember = "Department"
                Guna2ComboBox1.DisplayMember = "Department"
                Guna2ComboBox1.DataSource = dt
            End Using
        End Using
    End Sub
    Private Sub department_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AutoDepartmentId()
        AutoCourseId()
        loadDepartmentDataGrid()
        loadCourseDataGrid()
        Guna2Button4.Visible = False
        Guna2Button5.Visible = False
        Guna2Button3.Visible = False
        Guna2Button2.Visible = False
        loadDepartment()
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


    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        If (Guna2TextBox1.Text = "") Then
            MessageBox.Show("Enter Department name")
        Else
            connection.Open()
            cmd = New SqlCommand("SELECT count(*) FROM TB_DEPARTMENT where Department='" + Guna2TextBox1.Text.Trim + "' ", connection)
            Dim count = Convert.ToString(cmd.ExecuteScalar())

            If (count = 1) Then
                MessageBox.Show(Guna2TextBox1.Text + " Department already saved in the table")
            Else
                Dim result = MessageBox.Show("All the changes made will be updated \n Do you want to update record?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                If result = DialogResult.Yes Then
                    connection.Open()
                    Dim cmdd As New SqlCommand("UPDATE TB_DEPARTMENT SET  D_Id='" + Label4.Text + "',Department='" + Guna2TextBox1.Text + "'  WHERE D_Id ='" + Label4.Text + "'", connection)
                    cmdd.ExecuteNonQuery()
                    MessageBox.Show("Record Updated!")

                    AutoDepartmentId()
                    Guna2TextBox1.Clear()
                    loadDepartmentDataGrid()
                    loadDepartment()
                End If
            End If
            connection.Close()
        End If
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        If (String.Compare(Label4.Text, "-")) Then
            MessageBox.Show("No records found!")
        Else
            Dim result = MessageBox.Show("Do you really want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                connection.Open()
                cmd.Connection = connection
                cmd.CommandText = "Delete From TB_DEPARTMENT where D_Id= '" + Label4.Text + "'"
                cmd.ExecuteNonQuery()
                connection.Close()

                MessageBox.Show("Record Deleted!")
                Guna2TextBox1.Clear()
                loadDepartmentDataGrid()
                AutoDepartmentId()
                loadDepartment()
            End If

        End If

    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged
        connection.Open()
        cmd = New SqlCommand("SELECT D_Id FROM TB_DEPARTMENT WHERE Department='" + Guna2ComboBox1.Text + "' ", connection)
        Dpt_No = Convert.ToString(cmd.ExecuteScalar())
        connection.Close()
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        If (Guna2TextBox2.Text = "") Then
            MessageBox.Show("Enter Course name")
        Else
            connection.Open()
            cmd = New SqlCommand("SELECT count(*) FROM TB_COURSE where Course='" + Guna2TextBox2.Text.Trim + "' ", connection)
            Dim count = Convert.ToString(cmd.ExecuteScalar())

            If (count = 1) Then
                MessageBox.Show(Guna2TextBox2.Text + " Course already saved in the table")
            Else
                cmd = New SqlCommand("insert into TB_COURSE values ('" + Label5.Text + "','" + Dpt_No + "','" + Guna2TextBox2.Text + "')", connection)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Record Saved!")
                Guna2TextBox2.Clear()
                AutoCourseId()
                loadCourseDataGrid()
            End If
            connection.Close()
            End If
    End Sub



    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        If (Guna2TextBox2.Text = "") Then
            MessageBox.Show("Enter Course name")
        Else

            connection.Open()
            cmd = New SqlCommand("SELECT count(*) FROM TB_COURSE where Course='" + Guna2TextBox2.Text.Trim + "' ", connection)
            Dim count = Convert.ToString(cmd.ExecuteScalar())

            If (count = 1) Then
                MessageBox.Show(Guna2TextBox2.Text + " Course already saved in the table")
            Else
                Dim result = MessageBox.Show("All the changes made will be updated \n Do you want to update record?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                If result = DialogResult.Yes Then
                    connection.Open()
                    Dim cmdd As New SqlCommand("UPDATE TB_COURSE SET  C_Id='" + Label5.Text + "',D_Id='" + Dpt_No + "',Course='" + Guna2TextBox2.Text + "'  WHERE C_Id ='" + Label5.Text + "'", connection)
                    cmdd.ExecuteNonQuery()
                    MessageBox.Show("Record Updated!")

                    AutoCourseId()
                    Guna2TextBox2.Clear()
                    loadCourseDataGrid()
                End If
            End If
            connection.Close()
        End If
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        If (String.Compare(Label5.Text, "-")) Then
            MessageBox.Show("No records found!")
        Else
            Dim result = MessageBox.Show("Do you really want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                connection.Open()
                cmd.Connection = connection
                cmd.CommandText = "Delete From TB_COURSE where C_Id= '" + Label5.Text + "'"
                cmd.ExecuteNonQuery()
                connection.Close()

                MessageBox.Show("Record Deleted!")
                Guna2TextBox2.Clear()
                loadCourseDataGrid()
                AutoCourseId()
            End If

        End If
    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        Dim i As Integer
        i = Guna2DataGridView1.CurrentRow.Index
        If IsDBNull(Guna2DataGridView1.Item(0, i).Value) Then
            MessageBox.Show("No Records found!")
            Guna2Button4.Visible = False
            Guna2Button5.Visible = False
            Guna2Button1.Enabled = True
        Else
            Guna2Button1.Enabled = False
            Guna2Button4.Visible = True
            Guna2Button5.Visible = True
            Me.Label4.Text = Guna2DataGridView1.Item(0, i).Value
            Me.Guna2TextBox1.Text = Guna2DataGridView1.Item(1, i).Value
        End If
    End Sub

    Private Sub Guna2DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView2.CellContentClick
        Dim i As Integer
        i = Guna2DataGridView2.CurrentRow.Index
        If IsDBNull(Guna2DataGridView2.Item(0, i).Value) Then
            MessageBox.Show("No Records found!")
            Guna2Button3.Visible = False
            Guna2Button2.Visible = False
            Guna2Button6.Enabled = True
        Else
            Guna2Button6.Enabled = False

            Guna2Button3.Visible = True
            Guna2Button2.Visible = True
            Me.Label5.Text = Guna2DataGridView2.Item(0, i).Value
            connection.Open()
            cmd = New SqlCommand("SELECT Department FROM TB_DEPARTMENT WHERE D_Id='" + Guna2DataGridView2.Item(1, i).Value + "' ", connection)
            Me.Guna2ComboBox1.SelectedItem = Convert.ToString(cmd.ExecuteScalar())
            connection.Close()
            Me.Guna2TextBox2.Text = Guna2DataGridView2.Item(2, i).Value
        End If
    End Sub
End Class