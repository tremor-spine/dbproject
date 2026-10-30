Imports System.Data
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class frmMain
    Dim connStr As String = "server=localhost;user=root;password=;database=student_system"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAllRecords()
    End Sub

    Private Sub LoadAllRecords()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim da As New MySqlDataAdapter("SELECT * FROM students", conn)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvList.DataSource = dt
        End Using
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MessageBox.Show("Please enter Full Name!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(
                "INSERT INTO students (full_name, course, is_active) VALUES (@n, @c, @a)", conn)
            cmd.Parameters.AddWithValue("@n", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@c", cboCourse.Text)
            cmd.Parameters.AddWithValue("@a", If(chkActive.Checked, 1, 0))
            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("✅ Record Added!")
        LoadAllRecords()
        ClearInputs()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If dgvList.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a record first!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id As Integer = CInt(dgvList.SelectedRows(0).Cells("id").Value)

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(
                "UPDATE students SET full_name=@n, course=@c, is_active=@a WHERE id=@id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.Parameters.AddWithValue("@n", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@c", cboCourse.Text)
            cmd.Parameters.AddWithValue("@a", If(chkActive.Checked, 1, 0))
            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("✅ Record Updated!")
        LoadAllRecords()
        ClearInputs()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvList.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a record first!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id As Integer = CInt(dgvList.SelectedRows(0).Cells("id").Value)

        If MessageBox.Show("Delete this record?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim cmd As New MySqlCommand("DELETE FROM students WHERE id=@id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
            MessageBox.Show("✅ Record Deleted!")
            LoadAllRecords()
            ClearInputs()
        End If
    End Sub

    Private Sub btnExtra_Click(sender As Object, e As EventArgs) Handles btnExtra.Click
        If dgvList.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a student first!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim id As Integer = CInt(dgvList.SelectedRows(0).Cells("id").Value)
        Dim extra As New frmExtra()
        extra.studentID = id
        extra.ShowDialog()
        LoadAllRecords()
    End Sub

    Private Sub dgvList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellClick
        If e.RowIndex >= 0 Then
            Dim row = dgvList.Rows(e.RowIndex)
            txtName.Text = row.Cells("full_name").Value.ToString()
            cboCourse.Text = row.Cells("course").Value.ToString()
            chkActive.Checked = CBool(row.Cells("is_active").Value)
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearInputs()
    End Sub

    Private Sub ClearInputs()
        txtName.Clear()
        If cboCourse.Items.Count > 0 Then cboCourse.SelectedIndex = 0
        chkActive.Checked = False
    End Sub
End Class