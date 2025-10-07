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
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }

        private void iconAgregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este es el modulo de clientes.");
        }

        private void iconCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
