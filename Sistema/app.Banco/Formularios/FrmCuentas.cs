using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app.Banco.Formularios
{
    public partial class FrmCuentas : Form
    {
        public FrmCuentas()
        {
            InitializeComponent();
        }

        private void iconCuentas_Click(object sender, EventArgs e)
        {
            FrmMovimientoCuentas frm = new FrmMovimientoCuentas();
            frm.ShowDialog();
        }

        private void iconAgregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este es el modulo de cuentas.");
        }

        private void iconCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
