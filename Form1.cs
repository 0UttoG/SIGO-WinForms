using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIGO_WinForm
{
    public partial class Index : Form
    {
        public Index()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void modulo1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPacientes pacientes = new frmPacientes();
            pacientes.MdiParent = this;
            pacientes.Show();
        }

        private void Index_Load(object sender, EventArgs e)
        {

        }

        private void inventaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventario Inventario = new frmInventario();
            Inventario.MdiParent = this;
            Inventario.Show();
        }
    }
}
