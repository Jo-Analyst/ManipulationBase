using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Manipulation
{
    public partial class FrmManipulation : Form
    {
        string master = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=master;Integrated Security=True", directory, connectionString, _sql, dataBaseInit, value, nameColumns, err;
        int selectedIndexCbPrimaryKey, positionColumn;
        bool isIdentity = false;
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;

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

            if(cbMethods.SelectedIndex == 4)
                btnDesign.Visible = true;
            else
                btnDesign.Visible = false;

            //gerarComandoPadrao()
            generateStandardCommand(cbMethods.SelectedIndex);
            rbCommand.Focus();
        }

        private void generateStandardCommand(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    rbCommand.Text = "EXEC SP_RENAME 'table', 'renameColumn','column' ";
                    break;
                case 1:
                    rbCommand.Text = "ALTER DATABASE nameDatabase MODIFY NAME = newNamedatabase";
                    break;
                case 2:
                    rbCommand.Text = "EXEC SP_RENAME 'table', 'newTable' || ALTER table nameTable add column int not null";
                    break;
                case 3:
                    rbCommand.Text = "CREATE DATABASE nameDatabase";
                    break;
                case 4:
                    rbCommand.Text = "CREATE TABLE nameTable(\r\n" +
                   "id int primary key identity(1,1)\r\n" +
                   ")";
                    break;
                case 5:
                    rbCommand.Text = "DELETE FROM nameTable where id = ...";
                    break;
                case 6:
                    rbCommand.Text = "DROP DATABASE nameDatabase";
                    break;
                case 7:
                    rbCommand.Text = "DROP TABLE nameTable";
                    break;
                case 8:
                    rbCommand.Text = "INSERT INTO nameTable VALUES (...)";
                    break;
                case 10:
                    rbCommand.Text = "SELECT * FROM nameTable";
                    break;
                case 11:
                    rbCommand.Text = "UPDATE nameTable SET name = '...'";
                    break;
                default:
                    rbCommand.Clear();
                    break;
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (cbMethods.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o comando de dados para prosseguir!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrWhiteSpace(rbCommand.Text))
            {
                MessageBox.Show("Escreva o comando sql!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
           
            ExecuteCommandSQL();
        }

        private void ExecuteCommandSQL()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                _sql = rbCommand.Text;
                command = new SqlCommand(_sql, connection);
                connection.Open();
                if (cbMethods.SelectedIndex != 10)
                {
                    DialogResult dr = MessageBox.Show("O comando SQL inserido é um(a) " + cbMethods.Text + "?", "Manipulation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Comando realizado com sucesso.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbDataSource.Items.Clear();
                        CheckFolder(10);
                        cbDataSource.Text = dataBaseInit;
                    }
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

        private void cbDataSource_TextChanged(object sender, EventArgs e)
        {
            if (cbDataSource.Text != dataBaseInit)
            {
                SettingDefault();
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentTable.Text))
            {
                error.Clear();
                MessageBox.Show("Informe o nome da atual tabela!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                error.SetError(txtCurrentTable, "Campo obrigatório");
                txtCurrentTable.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtNewTable.Text))
            {
                error.Clear();
                MessageBox.Show("Informe o nome da nova tabela!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                error.SetError(txtNewTable, "Campo obrigatório");
                txtNewTable.Focus();
                return;
            }
            else if (txtCurrentTable.Text.Trim() == txtNewTable.Text.Trim())
            {
                error.Clear();
                MessageBox.Show("O nome da tabela são iguais.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                error.SetError(txtCurrentTable, "Campo obrigatório");
                return;
            }
            else if (!CheckTableExists(txtCurrentTable.Text.Trim()))
            {
                MessageBox.Show("Objeto " + txtCurrentTable.Text.Trim() + " não existe.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!CheckTableExists(txtNewTable.Text.Trim()))
            {
                MessageBox.Show("Objeto " + txtNewTable.Text.Trim() + " não existe.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(TakeColumnTable(txtNewTable.Text.Trim()).Rows.Count < TakeColumnTable(txtCurrentTable.Text.Trim()).Rows.Count)
            {
                MessageBox.Show("O número de colunas da tabela " + txtNewTable.Text.ToUpper() + " é menor que a tabela " + txtCurrentTable.Text.ToUpper() + "!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (TakeColumnTable(txtNewTable.Text.Trim()).Rows.Count > TakeColumnTable(txtCurrentTable.Text.Trim()).Rows.Count)
            {
                MessageBox.Show("O número de colunas da tabela " + txtNewTable.Text.ToUpper() + " é maior que a tabela " + txtCurrentTable.Text.ToUpper() + "!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if(ReadDataTableCurrent(txtNewTable.Text.Trim()).Rows.Count > 0)
            {
                MessageBox.Show("Não é permitido tranferir dados para uma tabela já populada!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TransferDataNewTable(ReadDataTableCurrent(txtCurrentTable.Text.Trim()), txtNewTable.Text.Trim());

            if (string.IsNullOrEmpty(err))
                MessageBox.Show("Dados da Tabela " + txtCurrentTable.Text.ToUpper() + " transferido para a tabela " + txtNewTable.Text.ToUpper(), "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private DataTable ReadDataTableCurrent(string nameTable)
        {
            connection = new SqlConnection(connectionString);
            _sql = "Select * from " + nameTable;
            adapter = new SqlDataAdapter(_sql, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        private void TransferDataNewTable(DataTable dataTableCurrent, string newTable)
        {
            try
            {
                if (dataTableCurrent.Rows.Count > 0)
                {
                    //pegar coluna da tabela
                    TakeColumnTable(txtCurrentTable.Text.Trim());

                    SetColumnsTable();

                    connection = new SqlConnection(connectionString);
                    for (int i = 0; i < dataTableCurrent.Rows.Count; i++)
                    {
                        SetValuesColumns(i, dataTableCurrent, TakeColumnTable(txtCurrentTable.Text.Trim()));

                        _sql = "INSERT INTO " + newTable + " (" + nameColumns + ") VALUES (" + value + ")";
                        command = new SqlCommand(_sql, connection);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            err = ex.Message;
                        }
                        finally
                        {
                            connection.Close();
                        }

                        value = null;
                    }

                   nameColumns = null;
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // pega as colunas da tabela e insere em um DataTable
        
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
                
        private void SetColumnsTable()
        {
            for (int i = 0; i < TakeColumnTable(txtCurrentTable.Text.Trim()).Rows.Count; i++)
            {
                if (cbxIdentity.Checked)
                {
                    if (TakePositionColumn() == i)
                    {
                        isIdentity = true;
                    }
                }

                if (!isIdentity)
                {
                    nameColumns += TakeColumnTable(txtCurrentTable.Text.Trim()).Rows[i]["column_name"].ToString();

                    if ((i + 1) < TakeColumnTable(txtCurrentTable.Text.Trim()).Rows.Count)
                    {
                        nameColumns += ", ";
                    }
                }

                isIdentity = false;
            }           
        }        

        private int TakePositionColumn()
        {
            for (int i = 0; i < TakeColumnTable(txtCurrentTable.Text.Trim()).Rows.Count; i++)
            {
                if (cbPrimaryKey.Text == TakeColumnTable(txtCurrentTable.Text.Trim()).Rows[i]["column_name"].ToString())
                   positionColumn = i;
            }

            return positionColumn;
        }

        private void SetValuesColumns(int index, DataTable dataTableCurrent, DataTable ColumnTable)
        {
            for (int cont = 0; cont < ColumnTable.Rows.Count; cont++)
            {
                if (cbxIdentity.Checked)
                {
                    if (TakePositionColumn() == cont)
                    {
                        isIdentity = true;
                    }
                }

                if (!isIdentity)
                {
                    value += "'" + dataTableCurrent.Rows[index][cont].ToString();

                    if ((cont + 1) == ColumnTable.Rows.Count)
                    {
                        value += "'";
                        return;
                    }

                    value += "', ";
                }

                isIdentity = false;
            }
        }

        private bool CheckTableExists(string currentTable)
        {
            connection = new SqlConnection(connectionString);
            _sql = "SELECT * FROM SYSOBJECTS  WHERE  ID = OBJECT_ID('" + currentTable + "')";
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
                MessageBox.Show("O acesso está liberado.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rbCommand.Enabled = true;
                btnExecute.Enabled = true;
                cbMethods.Enabled = true;
                cbMethods.SelectedIndex = -1;
                txtCurrentTable.Enabled = true;
                txtNewTable.Enabled = true;
                btnTranfer.Enabled = true;
                cbPrimaryKey.Enabled = false;
                cbHasPrimaryKey.Enabled = false;
                cbMethods.Focus();
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
            txtCurrentTable.Enabled = false;
            txtNewTable.Enabled = false;
            btnTranfer.Enabled = false;
            cbPrimaryKey.Enabled = false;
            cbxIdentity.Enabled = false;
            cbxIdentity.Checked = false;
            cbPrimaryKey.Items.Clear();
            cbPrimaryKey.SelectedIndex = -1;
            cbHasPrimaryKey.Enabled = false;
            cbHasPrimaryKey.Checked = false;
            cbMethods.SelectedIndex = -1;
            rbCommand.Clear();
            txtCurrentTable.Clear();
            txtNewTable.Clear();
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
                    err = null;
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

        private void cbHasPrimaryKey_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHasPrimaryKey.Checked)
            {
                cbPrimaryKey.Enabled = true;
                cbxIdentity.Enabled = true;
                AddValuesColumnInCbPrimaryKey();
            }
            else
            {
                cbPrimaryKey.Enabled = false;
                cbxIdentity.Enabled = false;
                cbxIdentity.Checked = false;
                cbPrimaryKey.SelectedIndex = -1;
            }
        }

        private void AddValuesColumnInCbPrimaryKey()
        {
            cbPrimaryKey.Items.Clear();
            foreach (DataRow dr in TakeColumnTable(txtCurrentTable.Text).Rows)
            {
                cbPrimaryKey.Items.Add(dr[0].ToString());
            }
            cbPrimaryKey.SelectedIndex = selectedIndexCbPrimaryKey;
        }

        private void cbPrimaryKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndexCbPrimaryKey = cbPrimaryKey.SelectedIndex;
        }

        private void btnDesign_Click(object sender, EventArgs e)
        {
            FrmDesign Design = new FrmDesign(connectionString);
            Design.ShowDialog();
        }

        private void txtCurrentTable_TextChanged(object sender, EventArgs e)
        {
            ReleaseOrNotComponentsAcess();
        }

        private void txtNewTable_TextChanged(object sender, EventArgs e)
        {
            // libera ou não o aceesso aos componentes
            ReleaseOrNotComponentsAcess();
        }

        private void ReleaseOrNotComponentsAcess()
        {
            error.Clear();


            if (CheckTableExists(txtCurrentTable.Text.Trim()) && CheckTableExists(txtNewTable.Text.Trim()))
                if (txtCurrentTable.Text.Trim().ToLower() == txtNewTable.Text.Trim().ToLower())
                {
                    cbHasPrimaryKey.Enabled = false;
                    cbHasPrimaryKey.Checked = false;
                    cbPrimaryKey.Enabled = false;
                    cbPrimaryKey.SelectedIndex = -1;
                    cbxIdentity.Checked = false;
                    cbxIdentity.Enabled = false;
                }
                else
                    cbHasPrimaryKey.Enabled = true;
        }

        private void cbPrimaryKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTransfer_Click(sender, e);
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
    }
}