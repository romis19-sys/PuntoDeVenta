using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UI.Modulos
{
    public class Utilidades
    {
        private static Mensajes mensaje = new Mensajes();

        public static void PasarFocus(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al pasar el focus.");
            }
        }


       

        public static void ControlEsc(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Escape)
                {
                    if(sender is Form formulario)
                    {
                        formulario.Close();
                    }
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error en el método controlarEsc.");
            }
        }

        // Validar que la entrada sea un número decimal
        public static void validarDecimal(object sender, KeyPressEventArgs e)
        {
            if (sender == null || !(sender is Control control))
            {
                e.Handled = true;
                return;
            }

            string currentText = control.Text;
            int selectionStart = control is TextBox textBox ? textBox.SelectionStart : 0;

            // 1. Permitir teclas de control (Backspace, Delete)
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
                return;
            }

            // 2. Permitir dígitos (0-9)
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
                return;
            }

            // 3. Permitir punto (.) o coma (,) como separador decimal
            bool isDecimalSeparator = e.KeyChar == '.';
            if (isDecimalSeparator)
            {
                bool alreadyHasDecimal = currentText.Contains('.');
                e.Handled = alreadyHasDecimal;
                return;
            }

            // 4. Bloquear cualquier otro carácter (letras, símbolos)
            e.Handled = true;
        }

        // Formato decimal
        public static void formatoDecimal(Control txtControl)
        {
            try
            {
                var txt = txtControl as Guna.UI2.WinForms.Guna2TextBox;
                if (txt == null) return;

                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = 0m.ToString("N2");
                    return;
                }

                if (decimal.TryParse(txt.Text, out decimal valor))
                {
                    txt.Text = valor.ToString("N2");
                }
                else
                {
                    mensaje.mensajeValidacion("Número inválido.");
                    txt.Focus();
                }
            }
            catch (Exception ex)
            {
                mensaje.mensajeError($"Error en formatoDecimal: {ex.Message}");
            }
        }

        // Digitar solo números enteros
        public static void numerosEnteros(KeyPressEventArgs e, string textoActual)
        {
            try
            {
                // Permitir teclas de control (Backspace)
                if (char.IsControl(e.KeyChar))
                    return;

                // Permitir solo dígitos
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }

                // Evitar que el primer dígito sea 0
                if (e.KeyChar == '0' && string.IsNullOrEmpty(textoActual))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                mensaje.mensajeError("Error en formato entero: " + ex.Message);
            }
        }
    }
}
