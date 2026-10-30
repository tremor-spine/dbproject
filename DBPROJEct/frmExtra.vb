Imports MySql.Data.MySqlClient

Public Class frmExtra
    Public studentID As Integer = 0
    Dim connStr As String = "server=localhost;user=root;password=;database=student_system"

    Private Sub frmExtra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtEmail.Clear()
        txtPhone.Clear()
        txtAddress.Clear()

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT email, phone, address FROM students WHERE id=@id", conn)
            cmd.Parameters.AddWithValue("@id", studentID)
            Using dr = cmd.ExecuteReader()
                If dr.Read() Then
                    txtEmail.Text = dr("email").ToString()
                    txtPhone.Text = dr("phone").ToString()
                    txtAddress.Text = dr("address").ToString()
                End If
            End Using
        End Using
    End Sub

    Private Sub btnSaveExtra_Click(sender As Object, e As EventArgs) Handles btnSaveExtra.Click
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(
                "UPDATE students SET email=@e, phone=@p, address=@a WHERE id=@id", conn)
            cmd.Parameters.AddWithValue("@e", txtEmail.Text.Trim())
            cmd.Parameters.AddWithValue("@p", txtPhone.Text.Trim())
            cmd.Parameters.AddWithValue("@a", txtAddress.Text.Trim())
            cmd.Parameters.AddWithValue("@id", studentID)
            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("✅ Details Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
    End Sub

    Private Sub btnCloseExtra_Click(sender As Object, e As EventArgs) Handles btnCloseExtra.Click
        Me.Close()
    End Sub
End Class