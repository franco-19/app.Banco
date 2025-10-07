using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using app.Banco.Formularios;
using app.Banco.Utilidades;

namespace app.Banco
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var conexion = AdminstrarConexion.Cargar();
            if(string.IsNullOrWhiteSpace(conexion.servidor) || string.IsNullOrWhiteSpace
              (conexion.baseDatos))
            {
                using(var frm = new FrmConexion())
                {
                    if(frm.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("No se configuro la conexion. La aplicacion se cerrara.",
                            "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }   return;
                }
            }
            Application.Run(new MDImenu());
        }
    }
}
