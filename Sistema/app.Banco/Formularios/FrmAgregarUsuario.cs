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

namespace app.Banco.Formularios
{
    public partial class FrmAgregarUsuario : Form
    {
        public event Action registroAgregado;
        public FrmAgregarUsuario()
        {
            InitializeComponent();

            this.KeyPress += ValidacionEntrada.PasarFocus;
            this.KeyDown += ValidacionEntrada.ControlEsc;

            txtUsuario.Focus();

        }
        public FrmAgregarUsuario(int idUsuario, string nombre, string telefono, string email)
        {
            InitializeComponent();

            this.KeyPress += ValidacionEntrada.PasarFocus;
            this.KeyDown += ValidacionEntrada.ControlEsc;

            txtId.Text = idUsuario.ToString();
            txtUsuario.Text = nombre;
            txtTelefono.Text = telefono;
            txtEmail.Text = email;

            txtUsuario.Focus();
        }

        #region Metodos
        public void Guardar(string nombre, string telefono, string email, string clave)
        {
            try
            {
                string connetionSting = ConexionDB.ObtenerConexion();

                using (SqlConnection conexion = new SqlConnection(connetionSting))
                {
                    string consulta = @"
                          INSERT INTO usuario(nombreUsuario,telefonoUsuario,emailUsuario,claveUsuario)
                                VALUES(@Nombre,
                                       @Telefono,
                                       @Email,
                                       @Clave)";

                    SqlCommand command = new SqlCommand(consulta, conexion);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Clave", clave);
                    conexion.Open();

                    int resultado = command.ExecuteNonQuery();
                    if (resultado > 0)
                    {
                        MessageBox.Show("Registro almacenado correctamente", "Informacion",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {

                        MessageBox.Show("No se puedo guardar el registro.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void Actualizar(int id,string nombre, string telefono, string email, string clave)
        {
            try
            {
                string connetionSting = ConexionDB.ObtenerConexion();

                using (SqlConnection conexion = new SqlConnection(connetionSting))
                {
                    string consulta = @"
                          UPDATE  usuario
                          SET nombreUsuario = @Nombre,
                              telefonoUsuario = @Telefono,
                              emailUsuario = @Email,
                              claveUsuario = @Clave
                          WHERE idUsuario = @IdUsuario";

                    SqlCommand command = new SqlCommand(consulta, conexion);
                    command.Parameters.AddWithValue("@IdUsuario", id);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Clave", clave);
                    conexion.Open();

                    int resultado = command.ExecuteNonQuery();
                    if (resultado > 0)
                    {
                        MessageBox.Show("Registro actualizado correctamente", "Informacion",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {

                        MessageBox.Show("No se puedo actualizar el registro.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        #endregion


        #region Botones de Comando
        private void iconCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iconGuardar_Click(object sender, EventArgs e)
        {
            errorIcono.Clear();
            bool datosValidos = true;

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2TextBox gunaTextBox)
                {
                    if (string.IsNullOrWhiteSpace(gunaTextBox.Text))
                    {
                        errorIcono.SetError(gunaTextBox, "Esre campo es obligatorio. ");
                        datosValidos = false;
                    }
                }
            }

            if (!datosValidos)
            {
                MessageBox.Show("Informacion incompleta, seran remarcados los datos que faltan. ",
                    "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string usuario = txtUsuario.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string email = txtEmail.Text.Trim();
            string clave = txtClave.Text.Trim();

            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text.Trim()))
                {
                    Guardar(usuario, telefono, email, clave);
                }
                else
                {
                    if(!int.TryParse(txtId.Text.Trim(), out int idUsuario))
                    {
                        MessageBox.Show("El ID no es valido ", "Validacion",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    Actualizar(idUsuario, usuario, telefono, email, clave);
                }
                registroAgregado?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error; " + ex.Message);
            }
        }
        #endregion
    }
}
