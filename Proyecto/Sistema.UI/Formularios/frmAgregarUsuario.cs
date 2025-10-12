using Farmacia.BLL;
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

namespace Sistema.UI.Formularios
{
    public partial class frmAgregarUsuario : Form
    {
        private Mensajes mensajes = new Mensajes();
        public event Action registroAgregado;
        Boolean actualizarRegistro = false;
        private string realText = ""; // Guardará el texto real
        
        public frmAgregarUsuario()
        {
            InitializeComponent();

            CargarRoles();
            cboRol.SelectedIndex = 0;

            // Configurar el TextBox
            txtClave.PasswordChar = '*';
            txtClave.Multiline = true; // para permitir Enter
            txtClave.KeyDown += txtClave_KeyDown;
            txtClave.TextChanged += txtClave_TextChanged;

            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;

        }

        public frmAgregarUsuario(int id, string rol, string usuario, string nombre, string apellido, string telefono, string email, string clave)
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
                txtEmail.Text = email;
                txtClave.Text = clave;
                cboRol.Text = Convert.ToString(rol);

                actualizarRegistro = true;

            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al inicializar el formulario.");
            }
        }

        #region metodos

        // generar la clave
        private static string GenerarClaveSegura()
        {
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string minusculas = "abcdefghijklmnopqrstuvwxyz";
            const string numeros = "0123456789";
            const string especiales = "!@#$%^&*(),.?\":{}|<>";

            string todos = mayusculas + minusculas + numeros + especiales;

            var random = new Random();
            var sb = new StringBuilder();

            // Garantizar al menos un carácter de cada tipo
            sb.Append(mayusculas[random.Next(mayusculas.Length)]);
            sb.Append(minusculas[random.Next(minusculas.Length)]);
            sb.Append(numeros[random.Next(numeros.Length)]);
            sb.Append(especiales[random.Next(especiales.Length)]);

            // Rellenar hasta tener mínimo 10 caracteres
            for (int i = sb.Length; i < 10; i++)
            {
                sb.Append(todos[random.Next(todos.Length)]);
            }

            return sb.ToString();
        }
    
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
            switch (control)
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
                    errorIcon.SetError(txtEmail, "Este campo es obligatorio.");
                    txtEmail.Focus();
                    break;
                case "clave":
                    errorIcon.SetError(txtClave, "Este campo es obligatorio.");
                    txtClave.Focus();
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
            txtEmail.Clear();
            txtClave.Clear();

            txtUsuario.Focus();
        }
        #endregion

        #region Botones de comando
        private void btnAceptar_Click(object sender, EventArgs e)
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
                    email = txtEmail.Text,
                    idRol = idRol,
                    clave = txtClave.Text,
                };

                resultadoOperacion resultado;

                if (int.TryParse(txtId.Text.Trim(), out int id) && id == 0)
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();    
        }

        private void iconMostrar_Click(object sender, EventArgs e)
        {
            if (txtClave.PasswordChar == '*')
            {
                txtClave.PasswordChar = '\0';
            }
            else
            {
                txtClave.PasswordChar = '*';
            }
        }

        #endregion

        #region Eventos de caja de texto
        private void txtClave_TextChanged(object sender, EventArgs e)
        {
            // Guardamos el texto real
            realText = txtClave.Text;
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            // Si se presiona Shift + Enter
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // evita salto de línea
                txtClave.PasswordChar = '\0'; // mostrar texto real
                txtClave.Text = realText;
                txtClave.SelectionStart = txtClave.Text.Length; // cursor al final
            }
            // Si se presiona solo Enter (sin Shift), ocultamos de nuevo
            else if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                txtClave.PasswordChar = '*'; // volver a ocultar
                txtClave.Text = realText;
                txtClave.SelectionStart = txtClave.Text.Length;
            }
        }

        //Validacion para telefono que no permita letras
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
         //   / Permitir tecla de retroceso
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Permitir dígitos
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }

            // permitir solo si el signo es escrito de primera letra, no pueden estar dos signos distintos
            if (e.KeyChar == '+')
            {
                if (txtTelefono.SelectionStart == 0 && !txtTelefono.Text.StartsWith("+"))
                {
                    return;
                }
            }

            // permitir espacio solo si no esta en la primera posición
            if (e.KeyChar == ' ')
            {
                int currentPosition = txtTelefono.SelectionStart;

                // no permitir espacio al inicio
                if (currentPosition == 0)
                {
                    e.Handled = true;
                    return;
                }

                // no permitir espacios consecutivos
                if (currentPosition > 0 && txtTelefono.Text.Length > 0)
                {
                    char previousChar = txtTelefono.Text[currentPosition - 1];
                    if (previousChar != ' ')
                    {
                        return;
                    }
                }
            }
            // si no cumple ninguna condición, bloquear
            e.Handled = true;
        }

        private void chkClaveSegura_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSugerirClave.Checked)
            {
                // Generar y sugerir una contraseña segura
                string clave = GenerarClaveSegura();
                txtClave.Text = clave;
                txtClave.Focus();
            }
            else
            {
                // Limpiar el TextBox si se quita el check
                txtClave.Text = "";
            }
        }
        #endregion
    }
}
