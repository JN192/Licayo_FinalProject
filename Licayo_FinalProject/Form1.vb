Imports MySql.Data.MySqlClient

Public Class Form1

    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Private selectedId As Integer = -1

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click

        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; userid=root; password=admin; database=employee_db;"

        Try

            conn.Open()
            MessageBox.Show("Connected!")

        Catch ex As Exception

            MessageBox.Show(ex.Message)
            conn.Close()

        End Try

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Dim query As String = "INSERT INTO `employee_db`.`employees` (`id`, `name`, `position`, `salary`, `department`) VALUES (@id, @name, @position, @salary, @department);"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=admin; database=employee_db;")

                conn.Open()
                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", CInt(txtId.Text))
                    cmd.Parameters.AddWithValue("@name", txtName.Text)
                    cmd.Parameters.AddWithValue("@position", txtPosition.Text)
                    cmd.Parameters.AddWithValue("@salary", Decimal.Parse(txtSalary.Text))
                    cmd.Parameters.AddWithValue("@department", txtDepartment.Text)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Record insert successful!")

                    txtId.Clear()
                    txtName.Clear()
                    txtPosition.Clear()
                    txtSalary.Clear()
                    txtDepartment.Clear()

                End Using

            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnLoad.Click

        Dim query As String = "SELECT id, name, position, salary, department FROM employee_db.employees WHERE is_deleted = 0"

        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=admin; database=employee_db;")

                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim table As New DataTable()

                adapter.Fill(table)
                dgvEmployees.DataSource = table

                dgvEmployees.Columns("id").Visible = False

            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEmployees.CellContentDoubleClick

        If e.RowIndex >= 0 Then

            Dim selectedRow As DataGridViewRow = dgvEmployees.Rows(e.RowIndex)

            selectedId = Convert.ToInt32(selectedRow.Cells("id").Value)
            txtId.Text = selectedRow.Cells("id").Value.ToString()
            txtName.Text = selectedRow.Cells("name").Value.ToString()
            txtPosition.Text = selectedRow.Cells("position").Value.ToString()
            txtSalary.Text = selectedRow.Cells("salary").Value.ToString()
            txtDepartment.Text = selectedRow.Cells("department").Value.ToString()

        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If selectedId = -1 Then
            MessageBox.Show("Select a record first.")
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete this record?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then
            Return
        End If

        Dim query As String = "UPDATE `employee_db`.`employees` SET is_deleted = 1 WHERE (`id` = @id);"

        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=admin; database=employee_db;")

                conn.Open()
                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", selectedId)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Record deleted successfully!")

                    txtId.Clear()
                    txtName.Clear()
                    txtPosition.Clear()
                    txtSalary.Clear()
                    txtDepartment.Clear()
                    selectedId = -1

                End Using

            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If selectedId = -1 Then
            MessageBox.Show("Select a record first.")
            Return
        End If

        Dim query As String = "UPDATE `employee_db`.`employees` 
                                SET `id` = @id,
                                    `name` = @name,
                                    `position` = @position,
                                    `salary` = @salary,
                                    `department` = @department 
                                     WHERE (`id` = @id);"

        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=admin; database=employee_db;")

                conn.Open()
                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", CInt(txtId.Text))
                    cmd.Parameters.AddWithValue("@name", txtName.Text)
                    cmd.Parameters.AddWithValue("@position", txtPosition.Text)
                    cmd.Parameters.AddWithValue("@salary", Decimal.Parse(txtSalary.Text))
                    cmd.Parameters.AddWithValue("@department", txtDepartment.Text)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Record updated successfully!")

                    txtId.Clear()
                    txtName.Clear()
                    txtPosition.Clear()
                    txtSalary.Clear()
                    txtDepartment.Clear()
                    selectedId = -1

                End Using

            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

End Class