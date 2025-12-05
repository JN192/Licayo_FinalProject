Imports MySql.Data.MySqlClient

Public Class Form1

    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; userid=root; password=root; database=employee_record_system;"

        Try

            conn.Open()
            MessageBox.Show("Connected!")

        Catch ex As Exception

            MessageBox.Show(ex.Message)
            conn.Close()

        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Dim query As String = "INSERT INTO `employee_record_system`.`employee_record_system` (`id`, `name`, `position`,`salary`, `department` ) VALUES (@id, @name, @position, @salary, @department);"
        Try
            Using conn As New MySqlConnection("server = localhost; userid=root; password=root; database=crud_demo.db;")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", CInt(txtId.Text))
                    cmd.Parameters.AddWithValue("@name", (txtName.Text))
                    cmd.Parameters.AddWithValue("@position", (txtPosition.Text))
                    cmd.Parameters.AddWithValue("@salary", CInt(txtSalary.Text))
                    cmd.Parameters.AddWithValue("@department", (txtDepartment.Text))
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record insert successful")

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
    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click
        Dim query As String = "SELECT * FROM `employee_record_system`employee_record_system"
        Try
            Using conn As New MySqlConnection("server = localhost; userid=root; password=root; database=employee_record_system;")
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)

                DataGridView1.DataSource = table
                DataGridView1.Columns("id").Visible = False

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Dim query As String = "INSERT INTO `employee_record_system`.`employee_record_system` (`id`, `name`, `position`,`salary`, `department` ) VALUES (@id, @name, @position, @salary, @department);"


        Try

            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=employee_record_system;")

                conn.Open()

                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", CInt(txtId.Text))
                    cmd.Parameters.AddWithValue("@name", (txtName.Text))
                    cmd.Parameters.AddWithValue("@position", (txtPosition.Text))
                    cmd.Parameters.AddWithValue("@salary", CInt(txtSalary.Text))
                    cmd.Parameters.AddWithValue("@department", (txtDepartment.Text))
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record updated successfully!")

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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim selectRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            txtId.Text = selectRow.Cells("id").Value.ToString()
            txtName.Text = selectRow.Cells("name").Value.ToString()
            txtPosition.Text = selectRow.Cells("position").Value.ToString()
            txtSalary.Text = selectRow.Cells("salary").Value.ToString()
            txtDepartment.Text = selectRow.Cells("department").Value.ToString()
        End If

    End Sub

End Class
