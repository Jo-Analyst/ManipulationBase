using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Manipulation
{
    public partial class FrmDataView : Form
    {
        public FrmDataView(DataTable table)
        {
            InitializeComponent();

            dgvViewData.DataSource = table;
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
    }
}
