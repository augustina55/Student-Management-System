Imports System.Data.SqlClient
Public Class home
    Dim connection As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagementDb;Integrated Security=True")
    Dim cmd As New SqlCommand
    Dim total_students = 0


    Private Sub home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection.Open()
        cmd = New SqlCommand("SELECT count(*) FROM TB_STUDENT", connection)
        total_students = Convert.ToString(cmd.ExecuteScalar())
        connection.Close()
        CircularProgressBar1.Text = total_students

    End Sub


End Class