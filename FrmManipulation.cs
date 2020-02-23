using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Manipulation
{
    public partial class FrmManipulation : Form
    {
        string master = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=master;Integrated Security=True", directory, connectionString, _sql, dataBaseInit;
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;

        public FrmManipulation()
        {
            InitializeComponent();
            CheckFolder(10);
        }

        private void CheckFolder(int num)
        {
            directory = @"C:\Program Files\Microsoft SQL Server\MSSQL" + num + @".SQLEXPRESS\MSSQL\DATA";
            
            if (Directory.Exists(directory))
            {

                string[] files = Directory.GetFiles(directory, "*.mdf", SearchOption.AllDirectories);
                foreach(string file in files)
                {
                    string filename = Path.GetFileName(file);
                    int lenghtFile = filename.Length - 4;
                    cbDataSource.Items.Add(filename.Substring(0, lenghtFile));
                }
            }
            else
            {
                num++;
                if (num <= 15)
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
            if(selectedIndex == 0)
                rbCommand.Text = "ALTER table nameTable add column int not null";
            else if (selectedIndex == 1)
                rbCommand.Text = "ALTER DATABASE nameDatabase MODIFY NAME = newNamedatabase";
            else if (selectedIndex == 2)
                rbCommand.Text = "EXEC SP_RENAME 'table', 'newTable'";
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
            try
            {
                TransferDataNewTable(ReadDateTable1(txtTable1.Text.Trim()) ,txtTable2.Text.Trim());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TransferDataNewTable(DataTable table, string newTable)
        {
            if(table.Rows.Count > 0)
            {
                connection = new SqlConnection(connectionString);
            }
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
            cbMethods.SelectedIndex = -1;
            rbCommand.Clear();
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
