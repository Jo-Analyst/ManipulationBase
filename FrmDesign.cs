using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manipulation
{
    public partial class FrmDesign : Form
    {
        public FrmDesign()
        {
            InitializeComponent();
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

        bool camposPreenchidos = false;

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CreateTable();
                
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message +",   Erro. Informe os dados corretamente para prosseguir.", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateTable()
        {
            if (string.IsNullOrWhiteSpace(txtNameTable.Text))
            {
                error.Clear();
                MessageBox.Show("Informe o nome da tabela", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                error.SetError(txtNameTable, "Campo obrigatório");
                txtNameTable.Focus();
                return;
            }
            if (dgvDesignTabela.Rows.Count == 1)
            {
                MessageBox.Show("Informe as colunas da tabela", "Manipulation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int countRowDgw = dgvDesignTabela.Rows.Count - 1;
            commandCreate = "CREATE TABLE " + txtNameTable.Text.Trim() + " (\r\n";
            for (int i = 0; i < countRowDgw; i++)
            {
                  commandCreate += dgvDesignTabela.Rows[i].Cells[0].Value.ToString() + " " +
                   dgvDesignTabela.Rows[i].Cells[1].Value.ToString() + " " + dgvDesignTabela.Rows[i].Cells[2].Value.ToString();

                if(countRowDgw > 1 && (i + 1) < countRowDgw)
                {
                    commandCreate += ", \r\n";
                }

            }
            commandCreate += ")";
                MessageBox.Show(commandCreate);
        }

        ErrorProvider error = new ErrorProvider();
        public DataTable columnTable = new DataTable();
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

        private void cbDefineKey_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDefineKey.Checked)
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
                //cbPrimaryKey.Items.Clear();
            }
        }

        private void AddValuesColumnInCbPrimaryKey()
        {
            //cbPrimaryKey.Items.Clear();
            //foreach (DataRow dr in TakeColumnTable(txtNewTable.Text).Rows)
            //{
            //    cbPrimaryKey.Items.Add(dr[0].ToString());
            //}
            //cbPrimaryKey.SelectedIndex = selectedIndexCbPrimaryKey;
        }

        private void txtNameTable_TextChanged(object sender, EventArgs e)
        {
            error.Clear();
        }
    }
}
