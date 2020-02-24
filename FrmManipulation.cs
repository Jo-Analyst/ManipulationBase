using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Manipulation
{
    public partial class FrmManipulation : Form
    {
        string master = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=master;Integrated Security=True", directory, connectionString, _sql, dataBaseInit, value, columns;
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        int countColumn;

        public FrmManipulation()
        {
            InitializeComponent();
            CheckFolder(1);
        }

        private void CheckFolder(int num)
        {
            directory = @"C:\Program Files\Microsoft SQL Server\MSSQL" + num + @".SQLEXPRESS\MSSQL\DATA";

            if (Directory.Exists(directory))
            {

                string[] files = Directory.GetFiles(directory, "*.mdf", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    string filename = Path.GetFileName(file);
                    int lenghtFile = filename.Length - 4;
                    cbDataSource.Items.Add(filename.Substring(0, lenghtFile));
                }
            }
            else
            {
                num++;
                if (num <= 100)
                    CheckFolder(num);
            }
        }

        private void cbMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbCommand.Clear();
            //gerarComandoPadrao()
            generateStandardCommand(cbMethods.SelectedIndex);
        }

        private void generateStandardCommand(int selectedIndex)
        {
            if (selectedIndex == 0)
                rbCommand.Text = "EXEC SP_RENAME 'table', 'renameColumn','column' ";
            else if (selectedIndex == 1)
                rbCommand.Text = "ALTER DATABASE nameDatabase MODIFY NAME = newNamedatabase";
            else if (selectedIndex == 2)
                rbCommand.Text = "EXEC SP_RENAME 'table', 'newTable' || ALTER table nameTable add column int not null";
            else if (selectedIndex == 3)
                rbCommand.Text = "CREATE DATABASE nameDatabase";
            else if (selectedIndex == 4)
                rbCommand.Text = "CREATE TABLE nameTable(\r\n" +
                    "  id int primary key identity(1,1)\r\n" +
                    ")";
            else if (selectedIndex == 5)
                rbCommand.Text = "DELETE FROM nameTable where id = ...";
            else if (selectedIndex == 6)
                rbCommand.Text = "DROP DATABASE nameDatabase";
            else if (selectedIndex == 7)
                rbCommand.Text = "DROP TABLE nameTable";
            else if (selectedIndex == 8)
                rbCommand.Text = "INSERT INTO nameTable VALUES (...)";
            else if (selectedIndex == 10)
                rbCommand.Text = "SELECT * FROM nameTable";
            else if (selectedIndex == 11)
                rbCommand.Text = "UPDATE nameTable SET name = '...'";
            else
                rbCommand.Clear();
        }

        string err;

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rbCommand.Text))
            {
                try
                {
                    connection = new SqlConnection(connectionString);
                    _sql = rbCommand.Text;
                    command = new SqlCommand(_sql, connection);
                    connection.Open();
                    if (cbMethods.SelectedIndex != 10)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Comando realizado com sucesso.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbDataSource.Items.Clear();
                        CheckFolder(10);
                        cbDataSource.Text = dataBaseInit;
                    }
                    else
                    {
                        adapter = new SqlDataAdapter(_sql, connection);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        FrmDataView dataView = new FrmDataView(table);
                        dataView.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
                MessageBox.Show("Escreva o comando sql!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void cbDataSource_TextChanged(object sender, EventArgs e)
        {
            if (cbDataSource.Text != dataBaseInit)
            {
                SettingDefault();
            }
        }

        private void cbDataSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnFreeAcess_Click(sender, e);
        }

        private void FrmManipulation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                btnExecute_Click(sender, e);
        }

        private void txtTable1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTransfer_Click(sender, e);
        }

        private void txtTable2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTransfer_Click(sender, e);
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTable1.Text))
            {
                MessageBox.Show("Informe o nome da atual tabela.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.SetError(txtTable1, "Campo obrigatório");
                txtTable1.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtTable2.Text))
            {
                MessageBox.Show("Informe o nome da nova tabela.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.SetError(txtTable2, "Campo obrigatório");
                txtTable2.Focus();
                return;
            }
            else if(txtTable1.Text.Trim() == txtTable2.Text.Trim())
            {
                MessageBox.Show("O nome da tabela são iguais", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!CheckTableExists(txtTable1.Text.Trim()))
            {
                MessageBox.Show("Objeto " + txtTable1.Text.Trim() + " não existe.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!CheckTableExists(txtTable2.Text.Trim()))
            {
                MessageBox.Show("Objeto " + txtTable2.Text.Trim() + " não existe.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                TransferDataNewTable(ReadDateTable1(txtTable1.Text.Trim()), txtTable2.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TransferDataNewTable(DataTable table, string newTable)
        {
            try
            {
                if (table.Rows.Count > 0)
                {
                    //pegar coluna da tabela
                    TakeColumnTable(txtTable2.Text.Trim());

                    SetColumnsTable();

                    connection = new SqlConnection(connectionString);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        setValuesColumns(i, table, TakeColumnTable(txtTable2.Text.Trim()));

                        _sql = "INSERT INTO " + newTable + " (" + columns + ") VALUES (" + value + ")";
                        command = new SqlCommand(_sql, connection);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connection.Close();
                        }

                        value = null;
                    }
                    columns = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckTableExists(string table1)
        {
            connection = new SqlConnection(connectionString);
            _sql = "SELECT * FROM SYSOBJECTS  WHERE  ID = OBJECT_ID('" + table1 + "')";
            command = new SqlCommand(_sql, connection);
            try
            {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        private void setValuesColumns(int index, DataTable table, DataTable dataTable)
        {
            for (int cont = 0; cont < dataTable.Rows.Count; cont++)
            {
                value += "'" + table.Rows[index][cont].ToString();

                if ((cont + 1) == dataTable.Rows.Count)
                {
                    value += "'";
                    return;
                }

                value += "', ";
            }
        }

        private void SetColumnsTable()
        {
            for (int i = 0; i < TakeColumnTable(txtTable2.Text.Trim()).Rows.Count; i++)
            {
                columns += TakeColumnTable(txtTable2.Text.Trim()).Rows[i]["column_name"].ToString();
                
                if ((i + 1) < TakeColumnTable(txtTable2.Text.Trim()).Rows.Count)
                {
                    columns += ", ";                    
                }                
            }
        }

        private void txtTable1_TextChanged(object sender, EventArgs e)
        {
            error.Clear();
        }

        private void txtTable2_TextChanged(object sender, EventArgs e)
        {
            error.Clear();
        }

        private DataTable ReadDateTable1(string nameTable)
        {
            connection = new SqlConnection(connectionString);
            _sql = "Select * from " + nameTable;
            adapter = new SqlDataAdapter(_sql, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        private DataTable TakeColumnTable(string nameTable)
        {
            connection = new SqlConnection(connectionString);
            _sql = "select column_name from information_Schema.columns where table_name = @name_Table";
            adapter = new SqlDataAdapter(_sql, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@name_Table", nameTable);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }



        private void cbDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            error.Clear();
            dataBaseInit = cbDataSource.Text;
        }

        private void btnFreeAcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbDataSource.Text))
            {
                MessageBox.Show("Informe o nome do banco de dados!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                error.SetError(cbDataSource, "Campo obrigatório");
                cbDataSource.Focus();
                return;
            }

            if (CheckDataBaseExists() == true)
            {
                rbCommand.Enabled = true;
                btnExecute.Enabled = true;
                cbMethods.Enabled = true;
                cbMethods.SelectedIndex = 3;
                txtTable1.Enabled = true;
                txtTable2.Enabled = true;
                btnTranfer.Enabled = true;
                cbPrimaryKey.Enabled = true;
                cbxIdentity.Enabled = true;
            }
            else if (!string.IsNullOrWhiteSpace(err))
            {
                MessageBox.Show(err, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Banco de dados não existe", "Manipulation Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SettingDefault();
            }
        }

        private void SettingDefault()
        {
            rbCommand.Enabled = false;
            btnExecute.Enabled = false;
            cbMethods.Enabled = false;
            txtTable1.Enabled = false;
            txtTable2.Enabled = false;
            btnTranfer.Enabled = false;
            cbPrimaryKey.Enabled = false;
            cbxIdentity.Enabled = false;
            cbPrimaryKey.Items.Clear();
            cbMethods.SelectedIndex = -1;
            rbCommand.Clear();
            txtTable1.Clear();
            txtTable2.Clear();
        }

        private bool CheckDataBaseExists()
        {
            connection = new SqlConnection(master);
            _sql = "SELECT * from SYS.DATABASES where NAME = @database";
            command = new SqlCommand(_sql, connection);
            command.Parameters.AddWithValue("@database", cbDataSource.Text.Trim());
            try
            {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=" + cbDataSource.Text.Trim() + ";Integrated Security=True";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}