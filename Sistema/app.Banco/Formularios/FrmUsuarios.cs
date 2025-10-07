using app.Banco.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace app.Banco.Formularios
{
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        #region Metodos

        private void ListarRegistros()
        {
            try
            {
                string connetionSting = ConexionDB.ObtenerConexion();

                using(SqlConnection conexion =new SqlConnection(connetionSting))
                {
                    string consulta = @"
                          SELECT
                                idUsuario AS id,
                                nombreUsuario AS Usuarios,
                                telefonoUsuario AS Telefono,
                                emailUsuario AS Email
                          FROM usuario";

                    SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexion);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvListado.DataSource = dt;
                    dgvListado.Columns[0].Visible = false;
                }     
                    
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        #endregion


        #region Eventos del Formulario

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            ListarRegistros();
        }
        #endregion

        #region Botones de Comando
        private void iconAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarUsuario frm = new FrmAgregarUsuario();
            frm.registroAgregado += ListarRegistros;
            MostrarModal.MostraConCap(this, frm);
        }

        private void iconEliminar_Click(object sender, EventArgs e)
        {
            if(dgvListado.CurrentRow != null)
            {
                try
                {
                    if (MessageBox.Show("¿Seguro que desea eliminar el registro?", "Confirmacion",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int.TryParse(dgvListado.CurrentRow.Cells["Id"].Value.ToString(), out int idUsuario);
                        string connetionSting = ConexionDB.ObtenerConexion();

                        using (SqlConnection conexion = new SqlConnection(connetionSting))
                        {
                            string consulta = "DELETE FROM usuario WHERE idUsuario = @IdUsuario";

                            SqlCommand command = new SqlCommand(consulta, conexion);
                            command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                            conexion.Open();

                            int resultado = command.ExecuteNonQuery();
                            if (resultado > 0)
                            {
                                MessageBox.Show("Registro eliminado correctamente", "Informacion",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                               
                            }
                            else
                            {

                                MessageBox.Show("No se puedo eliminar el registro.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }   ListarRegistros();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        #endregion

        #region Eventos del DataGridView
        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int id = Convert.ToInt32(dgvListado.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                    string usuario = dgvListado.Rows[e.RowIndex].Cells["Usuarios"].Value.ToString();
                    string telefono = dgvListado.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                    string email = dgvListado.Rows[e.RowIndex].Cells["Email"].Value.ToString();

                    FrmAgregarUsuario frm = new FrmAgregarUsuario(id, usuario, telefono, email);
                    frm.registroAgregado += ListarRegistros;
                    MostrarModal.MostraConCap(this, frm);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        #endregion

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();

            try
            {
                string connetionSting = ConexionDB.ObtenerConexion();

                using (SqlConnection conexion = new SqlConnection(connetionSting))
                {
                    string consulta = $@"
                          SELECT
                                idUsuario AS id,
                                nombreUsuario AS Usuarios,
                                telefonoUsuario AS Telefono,
                                emailUsuario AS Email
                          FROM usuario
                          WHERE nombreUsuario LIKE '%{nombre}%'
                          OR telefonoUsuario LIKE '%{nombre}%'";

                    SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexion);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvListado.DataSource = dt;
                    dgvListado.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void iconCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
