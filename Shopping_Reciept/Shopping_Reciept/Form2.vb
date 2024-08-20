Imports System.Data.SqlClient
Public Class Form2
    Dim cn As New SqlConnection
    Dim cm As New SqlCommand
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            cn.Open()
            cm = New SqlCommand()
            With cm
                .Connection = cn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_insertproduct"
                .Parameters.AddWithValue("@id", TextBox1.Text)
                .Parameters.AddWithValue("@name", TextBox2.Text)
                .Parameters.AddWithValue("@detail", TextBox3.Text)
                .Parameters.AddWithValue("@price", CDbl(TextBox4.Text))
                cm.ExecuteNonQuery()
            End With
            cn.Close()
            MsgBox("Product Added successfully", vbInformation)
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Connection()
        With cn
            .ConnectionString = "Data Source=DESKTOP-IJ1NB7I\SQLEXPRESS;Initial Catalog=Pr_Inventory;Integrated Security=True"
        End With
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connection()
        LoadData()
    End Sub



    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            cn.Open()
            cm = New SqlCommand()
            With cm
                .Connection = cn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_updateproduct"
                .Parameters.AddWithValue("@id", TextBox1.Text)
                .Parameters.AddWithValue("@name", TextBox2.Text)
                .Parameters.AddWithValue("@detail", TextBox3.Text)
                .Parameters.AddWithValue("@price", CDbl(TextBox4.Text))
                cm.ExecuteNonQuery()
            End With
            cn.Close()
            MsgBox("Product updated successfully", vbInformation)
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            cn.Open()
            cm = New SqlCommand()
            With cm
                .Connection = cn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_deleteproduct"
                .Parameters.AddWithValue("@id", TextBox1.Text)

                cm.ExecuteNonQuery()
            End With
            cn.Close()
            MsgBox("Product successfully deleted", vbInformation)
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub LoadData()
        Try
            cn.Open()
            Dim da As New SqlDataAdapter("SELECT * FROM Products", cn)
            Dim dt As New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadData()
    End Sub


End Class