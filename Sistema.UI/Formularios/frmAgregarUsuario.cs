using Farmacia.BLL;
using Farmacia.DAL;
using Farmacia.Entity;
using Sistema.UI.Modulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Sistema.UI.Formularios
{
    public partial class frmAgregarUsuario : Form
    {

        private Mensajes mensajes = new Mensajes();
        private Utilidades utilidades = new Utilidades();
        public event Action registroAgregado;
        Boolean actualizarRegistro = false;

        public frmAgregarUsuario()
        {
            InitializeComponent();

            CargarRoles();
            cboRol.SelectedIndex = 0;

            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;
        }

        public frmAgregarUsuario(int id, int rol, string usuario, string nombre, string apellido, string telefono, string email, string clave)
        {
            try
            {
                InitializeComponent();

                this.KeyPress += Utilidades.PasarFocus;
                this.KeyDown += Utilidades.ControlEsc;

                CargarRoles();

                txtId.Text = id.ToString();
                txtUsuario.Text = usuario;
                txtNombre.Text = nombre;
                txtApellido.Text = apellido;
                txtTelefono.Text = telefono;
                txtemail.Text = email;
                txtCave.Text = clave;
                cboRol.Text = Convert.ToString(rol);

                actualizarRegistro = true;

            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al inicializar el formulario.");
            }
        }
        #region metodos

        private void CargarRoles()
        {
            try
            {
                var usuario = bUsuario.listarRol();
                if (usuario != null && usuario.Rows.Count > 0)
                {
                    cboRol.DataSource = usuario;
                    cboRol.ValueMember = "ID";
                    cboRol.DisplayMember = "ROL";
                    cboRol.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al cargar las categorías.");
            }
        }

        

        private void errorControl(string control)
        {
            switch(control)
            {
                case "idRol":
                    errorIcon.SetError(cboRol, "Este campo es obligatorio.");
                    cboRol.Focus();
                    break;
                case "Usuario":
                    errorIcon.SetError(txtUsuario, "Este campo es obligatorio.");
                    txtUsuario.Focus();
                    break;
                case "Nombre":
                    errorIcon.SetError(txtNombre, "Este campo es obligatorio.");
                    txtUsuario.Focus();
                    break;
                case "Apellido":
                    errorIcon.SetError(txtApellido, "Este campo es obligatorio.");
                    txtApellido.Focus();
                    break;
                case "Telefono":
                    errorIcon.SetError(txtTelefono, "Este campo es obligatorio.");
                    txtTelefono.Focus();
                    break;
                case "Email":
                    errorIcon.SetError(txtemail, "Este campo es obligatorio.");
                    txtemail.Focus();
                    break;
                case "clave":
                    errorIcon.SetError(txtCave, "Este campo es obligatorio.");
                    txtCave.Focus();
                    break;
            }
        }

        private void LimpiarControles()
        {
            txtId.Text = "0";
            txtUsuario.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            cboRol.SelectedIndex = 0;
            txtemail.Clear();
            txtCave.Clear();

            txtUsuario.Focus();
        }
        #endregion

        private void iconMostrar_Click(object sender, EventArgs e)
        {
            if(txtCave.PasswordChar == '*')
            {
                txtCave.PasswordChar = '\0';
            }
            else
            {
                txtCave.PasswordChar = '*';
            }
        }
        #region Botones

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                errorIcon.Clear();

                if (!int.TryParse(cboRol.SelectedValue?.ToString(), out int idRol))
                {
                    mensajes.mensajeValidacion("Debe seleccionar un rol válido.");
                    errorIcon.SetError(cboRol, "Este campo es obligatorio.");
                    cboRol.Focus();
                    return;
                }

                OUsuario laboratorio = new OUsuario
                {
                    Nombreusuario = txtUsuario.Text,
                    Nombre = txtNombre.Text,
                    apellido = txtApellido.Text,
                    telefono = txtTelefono.Text,
                    email = txtemail.Text,
                    idRol = idRol,
                    clave = txtCave.Text,
                };

                resultadoOperacion resultado;

                if(int.TryParse(txtId.Text.Trim(), out int id) && id == 0)
                {
                    resultado = bUsuario.registrarUsuario(laboratorio);
                }
                else
                {
                    laboratorio.idUsuario = id;
                    resultado = bUsuario.editarUsuario(laboratorio);
                }

                if (!resultado.esValido)
                {
                    mensajes.mensajeValidacion(resultado.mensaje);

                    if (!string.IsNullOrEmpty(resultado.campoInvalido))
                    {
                        errorControl(resultado.campoInvalido);
                    }
                    return;
                }
                mensajes.mensajeOk(resultado.mensaje);
                registroAgregado?.Invoke();
                LimpiarControles();
                if (actualizarRegistro) Close();
                txtUsuario.Focus();
            }
            catch (Exception)
            {
                mensajes.mensajeError("Ocurrio un errror inesperado");
            }
        }


        #endregion

        private void cboRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmAgregarUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}
