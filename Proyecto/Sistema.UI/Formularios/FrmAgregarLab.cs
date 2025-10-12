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
    public partial class FrmAgregarLab : Form
    {
        private Mensajes mensaje = new Mensajes();
        public event Action registroAgregado;
        Boolean actualizarRegistro = false;

        public FrmAgregarLab()
        {
            InitializeComponent();
            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;
        }

        public FrmAgregarLab(int id, string laboratorio, string telefono, string contacto)
        {
            InitializeComponent();
            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;

            txtId.Text = id.ToString();
            txtNombreLaboratorio.Text = laboratorio;
            txtTelefono.Text = telefono;
            txtContacto.Text = contacto;
            actualizarRegistro = true;
        }

        #region metodos
        private void errorControl(string campo)
        {
            switch (campo)
            {
                case "Laboratorio":
                    errorIcon.SetError(txtNombreLaboratorio, "Este campo es obligatorio.");
                    txtNombreLaboratorio.Focus();
                    break;
                case "Telefono":
                    errorIcon.SetError(txtTelefono, "Este campo es obligatorio.");
                    txtTelefono.Focus();
                    break;
                case "Contacto":
                    errorIcon.SetError(txtContacto, "Este campo es obligatorio.");
                    txtContacto.Focus();
                    break;
            }
        }

        private void limpiarControles()
        {
            txtId.Text = "0";
            txtNombreLaboratorio.Clear();
            txtTelefono.Clear();
            txtContacto.Clear();
            txtNombreLaboratorio.Focus();
        }
        #endregion

        #region botones de comando
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                errorIcon.Clear();
                oLaboratorio laboratorio = new oLaboratorio()
                {
                    laboratorio = txtNombreLaboratorio.Text.Trim(),
                    telefono = txtTelefono.Text.Trim(),
                    contacto = txtContacto.Text.Trim()
                };
                resultadoOperacion resultado;

                if(int.TryParse(txtId.Text.Trim(), out int Id) && Id == 0)
                {
                    resultado = bLaboratorio.registrarLaboratorio(laboratorio);
                }
                else
                {
                    laboratorio.idLaboratorio = Id;
                    resultado = bLaboratorio.editarLaboratorio(laboratorio);
                }


                if (!resultado.esValido)
                {
                    mensaje.mensajeValidacion(resultado.mensaje);

                    if (!string.IsNullOrWhiteSpace(resultado.campoInvalido))
                    {
                        errorControl(resultado.campoInvalido);
                    }
                    return;
                }
                mensaje.mensajeOk(resultado.mensaje);
                registroAgregado.Invoke();
                limpiarControles();
                if (actualizarRegistro) Close();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error inesperado.");
            }
            #endregion
        }

        #region "Eventos de las cajas de texto"

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Permitir dígitos
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }

            // Permitir signo "+" SOLO si:
            // 1. Está en la primera posición
            // 2. No existe ya un "+" al inicio
            if (e.KeyChar == '+')
            {
                if (txtTelefono.SelectionStart == 0 && !txtTelefono.Text.StartsWith("+"))
                {
                    return;
                }
            }

            // Permitir espacio SOLO si:
            // 1. No está en la primera posición
            // 2. Hay al menos un carácter antes
            // 3. El carácter anterior no es un espacio
            if (e.KeyChar == ' ')
            {
                int currentPosition = txtTelefono.SelectionStart;

                // No permitir espacio al inicio
                if (currentPosition == 0)
                {
                    e.Handled = true;
                    return;
                }

                // No permitir espacios consecutivos
                if (currentPosition > 0 && txtTelefono.Text.Length > 0)
                {
                    char previousChar = txtTelefono.Text[currentPosition - 1];
                    if (previousChar != ' ')
                    {
                        return;
                    }
                }
            }

            // Si no cumple ninguna condición, bloquear
            e.Handled = true;
        }

        private void txtNombreLaboratorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir tecla de retroceso
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Permitir letras
            if (char.IsLetter(e.KeyChar))
            {
                return;
            }

            // Permitir caracteres especiales específicos: " , . : ;
            if (e.KeyChar == '"' || e.KeyChar == ',' || e.KeyChar == '.' || e.KeyChar == ':' || e.KeyChar == ';')
            {
                return;
            }

            // Permitir espacio SOLO si:
            // 1. No está en la primera posición
            // 2. Hay al menos un carácter antes
            // 3. El carácter anterior no es un espacio
            if (e.KeyChar == ' ')
            {
                int currentPosition = txtNombreLaboratorio.SelectionStart;

                // No permitir espacio al inicio
                if (currentPosition == 0)
                {
                    e.Handled = true;
                    return;
                }

                // No permitir espacios consecutivos
                if (currentPosition > 0 && txtNombreLaboratorio.Text.Length > 0)
                {
                    char previousChar = txtNombreLaboratorio.Text[currentPosition - 1];
                    if (previousChar != ' ')
                    {
                        return;
                    }
                }
            }

            // Si no cumple ninguna condición, bloquear
            e.Handled = true;
        }

        private void txtContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir tecla de retroceso
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Permitir letras
            if (char.IsLetter(e.KeyChar))
            {
                return;
            }

            // Permitir caracteres especiales específicos: " , . : ;
            if (e.KeyChar == '"' || e.KeyChar == ',' || e.KeyChar == '.' || e.KeyChar == ':' || e.KeyChar == ';')
            {
                return;
            }

            // Permitir espacio SOLO si:
            // 1. No está en la primera posición
            // 2. Hay al menos un carácter antes
            // 3. El carácter anterior no es un espacio
            if (e.KeyChar == ' ')
            {
                int currentPosition = txtContacto.SelectionStart;

                // No permitir espacio al inicio
                if (currentPosition == 0)
                {
                    e.Handled = true;
                    return;
                }

                // No permitir espacios consecutivos
                if (currentPosition > 0 && txtContacto.Text.Length > 0)
                {
                    char previousChar = txtContacto.Text[currentPosition - 1];
                    if (previousChar != ' ')
                    {
                        return;
                    }
                }
            }

            // Si no cumple ninguna condición, bloquear
            e.Handled = true;
        }

        #endregion
    }
}
