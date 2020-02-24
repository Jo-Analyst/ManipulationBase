using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManipulationBase
{
    public partial class FrmDesign : Form
    {
        public FrmDesign()
        {
            InitializeComponent();
        }

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
                if (string.IsNullOrWhiteSpace(txtNameTable.Text))
                {
                    error.Clear();
                    MessageBox.Show("Informe o nome da tabela", "Manipulation Base", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    error.SetError(txtNameTable, "Campo obrigatório");
                    txtNameTable.Focus();
                    return;
                }
                if (dgvDesignTabela.Rows.Count == 1)
                {
                    MessageBox.Show("Informe as colunas da tabela", "Manipulation Base", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //for (int i = 0; i < dgvDesignTabela.Rows.Count - 1; i++)
                //{
                //    if (string.IsNullOrEmpty(dgvDesignTabela.Rows[i].Cells[0].Value.ToString()) || string.IsNullOrEmpty(dgvDesignTabela.Rows[i].Cells[1].Value.ToString()) || string.IsNullOrEmpty(dgvDesignTabela.Rows[i].Cells[2].Value.ToString()))
                //    {
                //        camposPreenchidos = true;
                //    }
                //}

                //if (camposPreenchidos)
                //{
                //    MessageBox.Show("Preencha em todas as células das colunas que irão ser criadas", "Manipulation Base", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                ColumnTable();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Erro. Informe os dados corretamente para prosseguir.", "Manipulation Base", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ErrorProvider error = new ErrorProvider();
        public DataTable columnTable = new DataTable();
        public string nameTable { get; set; }

        private void ColumnTable()
        {
            columnTable.Columns.Add("ColName", typeof(string));
            columnTable.Columns.Add("ColDataType", typeof(string));
            columnTable.Columns.Add("ColAllowNull", typeof(string));

            for (int i = 0; i < dgvDesignTabela.Rows.Count - 1; i++)
            {
                if (dgvDesignTabela.Rows.Count > 1)
                {
                  columnTable.Rows.Add(dgvDesignTabela.Rows[i].Cells["ColName"].Value.ToString(), dgvDesignTabela.Rows[i].Cells["ColDataType"].Value.ToString(), dgvDesignTabela.Rows[i].Cells["ColAllowNull"].Value.ToString());
                }
            }
        }

        private void dgvDesignTabela_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                comboBox1.Items.Clear();

                for(int i = 0; i < dgvDesignTabela.Rows.Count - 1; i++)
                {
                    comboBox1.Items.Add(dgvDesignTabela.Rows[i].Cells["ColName"].Value.ToString());
                }
            }
        }

        private void dgvDesignTabela_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                comboBox1.Items.Clear();

                for (int i = 0; i < dgvDesignTabela.Rows.Count - 1; i++)
                {
                    comboBox1.Items.Add(dgvDesignTabela.Rows[i].Cells["ColName"].Value.ToString());
                }
            }
        }
    }
}
