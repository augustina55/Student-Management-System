Imports System.Data.SqlClient
Public Class search
    Dim connection As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")

    Dim S_id = "NULL"

    Sub loadDataGrid()
        connection.Open()
        'Student
        Dim command1 As New SqlCommand("SELECT * FROM TB_STUDENT", connection)
        Dim table1 As New DataTable()
        Dim adapter As New SqlDataAdapter(command1)
        adapter.Fill(table1)
        Guna2DataGridView1.DataSource = table1

        'Marks
        Dim command2 As New SqlCommand("SELECT * FROM TB_EXAM", connection)
        Dim table2 As New DataTable()
        Dim adapter2 As New SqlDataAdapter(command2)
        adapter2.Fill(table2)
        Guna2DataGridView2.DataSource = table2


        'Fees
        Dim command3 As New SqlCommand("SELECT * FROM TB_FEES", connection)
        Dim table3 As New DataTable()
        Dim adapter3 As New SqlDataAdapter(command3)
        adapter3.Fill(table3)
        Guna2DataGridView4.DataSource = table3


        'Attendance
        Dim command4 As New SqlCommand("SELECT * FROM TB_ATTENDANCE", connection)
        Dim table4 As New DataTable()
        Dim adapter4 As New SqlDataAdapter(command4)
        adapter4.Fill(table4)
        Guna2DataGridView3.DataSource = table4
        connection.Close()
    End Sub
    Private Sub search_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadDataGrid()
        Guna2TextBox2.BorderColor = Color.DodgerBlue
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If Not (Guna2TextBox2.Text = "") Then
            connection.Open()
            Dim command As New SqlCommand("SELECT S_id FROM TB_STUDENT WHERE CONCAT(S_id,Reg_No,Name) like '%" & Guna2TextBox2.Text & "%'", connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            If table.Rows.Count > 0 Then
                For i As Integer = 0 To table.Rows.Count - 1
                    S_id = table.Rows(i)(0)
                    searchRecord(S_id)
                Next
            Else
                MessageBox.Show("No Records found!")
            End If

        End If
    End Sub

    Sub searchRecord(str As String)

        'Student
        Dim command1 As New SqlCommand("SELECT * FROM TB_STUDENT Where S_id='" + str + "'", connection)
        Dim table1 As New DataTable()
        Dim adapter As New SqlDataAdapter(command1)
        adapter.Fill(table1)
        Guna2DataGridView1.DataSource = table1

        'Marks
        Dim command2 As New SqlCommand("SELECT * FROM TB_EXAM  Where S_id='" + str + "' ", connection)
        Dim table2 As New DataTable()
        Dim adapter2 As New SqlDataAdapter(command2)
        adapter2.Fill(table2)
        Guna2DataGridView2.DataSource = table2


        'Fees
        Dim command3 As New SqlCommand("SELECT * FROM TB_FEES  Where S_id='" + str + "' ", connection)
        Dim table3 As New DataTable()
        Dim adapter3 As New SqlDataAdapter(command3)
        adapter3.Fill(table3)
        Guna2DataGridView4.DataSource = table3


        'Attendance
        Dim command4 As New SqlCommand("SELECT * FROM TB_ATTENDANCE  Where S_id='" + str + "' ", connection)
        Dim table4 As New DataTable()
        Dim adapter4 As New SqlDataAdapter(command4)
        adapter4.Fill(table4)
        Guna2DataGridView3.DataSource = table4
        connection.Close()
    End Sub
End Class