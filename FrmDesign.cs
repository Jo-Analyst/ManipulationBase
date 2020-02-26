using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Manipulation
{
    public partial class FrmDesign : Form
    {
        string connectionString;

        public FrmDesign(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }

        string commandCreate;

        private void lblFechar_MouseLeave(object sender, EventArgs e)
        {
            this.lblFechar.BackColor = Color.Transparent;
        }

        private void lblFechar_MouseEnter(object sender, EventArgs e)
        {
            this.lblFechar.BackColor = Color.Red;           
        }

        private void lblFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNameTable.Text))
            {
                error.Clear();
                MessageBox.Show("Informe o nome da tabela!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                error.SetError(txtNameTable, "Campo obrigatório");
                txtNameTable.Focus();
                return;
            }
            else if (dgvDesignTabela.Rows.Count == 1)
            {
                MessageBox.Show("Informe as colunas da tabela!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (cbxDefineKey.Checked && cbPrimaryKey.SelectedIndex == -1)
            {
                MessageBox.Show("Informe a chave primária!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CreateTable();

            if (!string.IsNullOrEmpty(commandCreate))
            {
                ExecuteCommand();
            }
        }

        private void ExecuteCommand()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string _sql = commandCreate;
            SqlCommand command = new SqlCommand(_sql, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Tabela criado com sucesso.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                connection.Close();
            }
        }

        string Key;

        private void CreateTable()
        {
            int countRowDgw = dgvDesignTabela.Rows.Count - 1;
            commandCreate = "CREATE TABLE " + txtNameTable.Text.Trim() + " (\r\n";

            try
            {
                for (int i = 0; i < countRowDgw; i++)
                {
                    if (cbxDefineKey.Checked)
                    {
                        if (dgvDesignTabela.Rows[i].Cells[0].Value.ToString() == cbPrimaryKey.Text)
                        {
                            if (cbxIdentity.Checked)
                                Key = "PRIMARY KEY IDENTITY";

                            else
                                Key = "PRIMARY KEY";
                        }

                    }

                    commandCreate += dgvDesignTabela.Rows[i].Cells[0].Value.ToString() + " "
                       + dgvDesignTabela.Rows[i].Cells[1].Value.ToString() + " " + Key + " " + dgvDesignTabela.Rows[i].Cells[2].Value.ToString();

                    if (countRowDgw > 1 && (i + 1) < countRowDgw)
                    {
                        commandCreate += ", \r\n";
                    }

                    Key = null;
                }

                commandCreate += ")";
            }
            catch
            {
                MessageBox.Show("Preencha as colunas necessárias!", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                commandCreate = null;
            }
        }

        ErrorProvider error = new ErrorProvider();
        public DataTable columnTable = new DataTable();
        int selectedIndexCbPrimaryKey = -1;

        public string nameTable { get; set; }

        private void dgvDesignTabela_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cbPrimaryKey.Items.Clear();

                for(int i = 0; i < dgvDesignTabela.Rows.Count - 1; i++)
                {
                    cbPrimaryKey.Items.Add(dgvDesignTabela.Rows[i].Cells["ColName"].Value.ToString());
                }
            }
        }

        private void dgvDesignTabela_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                cbPrimaryKey.Items.Clear();

                for (int i = 0; i < dgvDesignTabela.Rows.Count - 1; i++)
                {
                    cbPrimaryKey.Items.Add(dgvDesignTabela.Rows[i].Cells["ColName"].Value.ToString());
                }
            }
        }

        private void cbxDefineKey_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDefineKey.Checked)
            {
                cbxIdentity.Enabled = true;
                cbPrimaryKey.Enabled = true;
                AddValuesColumnInCbPrimaryKey();
            }
            else
            {
                cbxIdentity.Enabled = false;
                cbPrimaryKey.Enabled = false;
                cbPrimaryKey.SelectedIndex = -1;
                cbxIdentity.Checked = false;
                Key = "";
            }
        }

        private void AddValuesColumnInCbPrimaryKey()
        {
            cbPrimaryKey.Items.Clear();

            for (int i = 0; i < dgvDesignTabela.Rows.Count - 1; i++)
            {
                try
                {
                    if (dgvDesignTabela.Rows[i].Cells[0].Value.ToString() == null)
                        return;

                    cbPrimaryKey.Items.Add(dgvDesignTabela.Rows[i].Cells[0].Value.ToString());
                }
                catch
                {
                    
                }
            }

            cbPrimaryKey.SelectedIndex = selectedIndexCbPrimaryKey;
        }

        private void txtNameTable_TextChanged(object sender, EventArgs e)
        {
            error.Clear();
        }

        private void cbPrimaryKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndexCbPrimaryKey = cbPrimaryKey.SelectedIndex;
        }

        private void cbPrimaryKey_Click(object sender, EventArgs e)
        {
            AddValuesColumnInCbPrimaryKey();
        }
    }
}
