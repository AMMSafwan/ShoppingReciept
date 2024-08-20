Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Text

Public Class Form3
    Dim cn As New SqlConnection
    Dim cm As New SqlCommand

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub


    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connection()
    End Sub

    Sub Connection()
        With cn
            .ConnectionString = "Data Source=DESKTOP-IJ1NB7I\SQLEXPRESS;Initial Catalog=Pr_Inventory;Integrated Security=True"
        End With
    End Sub

    Private Sub GetDataById(id As String)
        Try
            cn.Open()
            cm = New SqlCommand("SELECT * FROM Products WHERE id = @id", cn)
            cm.Parameters.AddWithValue("@id", id)

            Dim dr As SqlDataReader = cm.ExecuteReader()

            If dr.Read() Then
                TextBox2.Text = dr("name").ToString()
                TextBox3.Text = dr("price").ToString()
            Else
                ' Clear the text boxes if no record is found
                TextBox2.Text = ""
                TextBox3.Text = ""
                MsgBox("No product found with this ID.", vbInformation)
            End If

            dr.Close()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text <> "" Then
            GetDataById(TextBox1.Text)
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If TextBox1.Text <> "" Then
            GetDataById(TextBox1.Text)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button1_Enter(sender As Object, e As EventArgs) Handles Button1.Enter

        ' Check if all required fields are filled
        If TextBox1.Text = "" OrElse TextBox2.Text = "" OrElse TextBox3.Text = "" OrElse TextBox4.Text = "" OrElse TextBox5.Text = "" Then
            MsgBox("Please fill all the fields.", vbExclamation)
            Exit Sub
        End If

        ' Add a new row to the DataGridView
        Dim row As String() = New String() {TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text}
        DataGridView1.Rows.Add(row)

        ' Clear the text boxes after adding the row
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub

    Private Sub PrintReceipt()
        Dim subtotal As Double = 0
        Dim totalDiscount As Double = 0
        Dim totalAmount As Double = 0
        Dim paidAmount As Double = CDbl(TextBox6.Text) ' Paid amount from user input
        Dim balance As Double = 0

        Dim receipt As New StringBuilder()

        ' Header of the receipt
        receipt.AppendLine("********* Shopping Receipt *********")
        receipt.AppendLine("Item Name     Qty     Unit Price    Total Price")
        receipt.AppendLine("--------------------------------------------")

        ' Loop through each row in the DataGridView to calculate totals
        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                Dim productName As String = row.Cells(1).Value.ToString()
                Dim unitPrice As Double = CDbl(row.Cells(2).Value)
                Dim quantity As Integer = CInt(row.Cells(3).Value)
                Dim discount As Double = CDbl(row.Cells(4).Value)

                ' Calculate the total price for this item before discount
                Dim totalPrice As Double = unitPrice * quantity

                ' Apply the discount
                Dim discountAmount As Double = totalPrice * (discount / 100)
                Dim finalPrice As Double = totalPrice - discountAmount

                ' Add to the subtotal and total discount
                subtotal += finalPrice
                totalDiscount += discountAmount

                ' Add to the receipt string
                receipt.AppendLine($"{productName.PadRight(12)}  {quantity.ToString().PadRight(6)}  {unitPrice.ToString("F2").PadRight(10)}  {finalPrice.ToString("F2")}")
            End If
        Next

        ' Calculate the total and balance
        totalAmount = subtotal
        balance = paidAmount - totalAmount

        ' Add the summary to the receipt
        receipt.AppendLine("--------------------------------------------")
        receipt.AppendLine($"Subtotal:          {subtotal.ToString("F2")}")
        receipt.AppendLine($"Total Discount:    {totalDiscount.ToString("F2")}")
        receipt.AppendLine($"Total Amount:      {totalAmount.ToString("F2")}")
        receipt.AppendLine($"Paid Amount:       {paidAmount.ToString("F2")}")
        receipt.AppendLine($"Balance:           {balance.ToString("F2")}")
        receipt.AppendLine("********************************************")

        ' Show the receipt in a message box or print it
        MessageBox.Show(receipt.ToString(), "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Optional: Print the receipt (depends on the printing setup in your environment)
        ' PrintReceipt(receipt.ToString())
    End Sub

    ' Button click event to trigger receipt generation and printing
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PrintReceipt()
    End Sub



End Class