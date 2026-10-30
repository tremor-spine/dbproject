Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class frmLogin
    Dim connStr As String = "server=localhost;user=root;password=;database=student_system"

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrWhiteSpace(txtUser.Text) Or String.IsNullOrWhiteSpace(txtPass.Text) Then
            MessageBox.Show("Please enter Username and Password!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(
                "SELECT COUNT(*) FROM users WHERE username=@u AND password=@p", conn)
            cmd.Parameters.AddWithValue("@u", txtUser.Text.Trim())
            cmd.Parameters.AddWithValue("@p", txtPass.Text.Trim())

            If CInt(cmd.ExecuteScalar()) > 0 Then
                MessageBox.Show("✅ Login Successful!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Hide()
                Dim main As New frmMain()
                main.ShowDialog()
                Me.Close()
            Else
                MessageBox.Show("❌ Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Application.Exit()
    End Sub
End Class