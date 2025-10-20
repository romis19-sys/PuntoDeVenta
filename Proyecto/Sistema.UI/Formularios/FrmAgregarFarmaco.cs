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
    public partial class FrmAgregarFarmaco : Form
    {
        private Mensajes mensaje = new Mensajes();
        public event Action registroAgregado;
        private bool actualizarRegistro = false;
      

        public FrmAgregarFarmaco()
        {
            InitializeComponent();
            InicializarCombos();

            // eliminar los iconos que no quiero que aparezcan, lo hice por codigo
            txtCodigo.IconLeft = null;
            txtFarmaco.IconLeft = null;
            txtDescripcion.IconLeft = null;
            txtStock.IconLeft = null;
            txtPrecioCompra.IconLeft = null;
            txtPrecioVenta.IconLeft = null;


            cboPresentacion.SelectedIndex = 0;
            cboVentaReceta.SelectedIndex = 0;

            // Inicialmente bloqueado
            dtpFechaVencimiento.Enabled = false;
            chkTieneVencimiento.Checked = false;


            // Eventos de teclado y foco
            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;

            //cboVentaReceta.SelectedIndex = 0;
            dtpFechaVencimiento.Value = DateTime.Now;

            chkTieneVencimiento.CheckedChanged += chkTieneVencimiento_CheckedChanged;
        }

        public FrmAgregarFarmaco(int id, string codigo, string nombreFarmaco, string laboratorio,string descripcion, string presentacion, string tipoVenta, int stock, decimal precioCompra, decimal precioVenta, DateTime? fechaCaducacion)
        {
            InitializeComponent();
            InicializarCombos();
            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;

            // Asignar datos a controles
            // Asignar valores a los controles
            txtId.Text = id.ToString();
            txtCodigo.Text = codigo;
            txtFarmaco.Text = nombreFarmaco;
            cboLaboratorio.Text = laboratorio;
            txtDescripcion.Text = descripcion;
            cboPresentacion.Text = presentacion;
            cboVentaReceta.Text = tipoVenta;
            txtStock.Text = stock.ToString();
            txtPrecioCompra.Text = precioCompra.ToString("0.00");
            txtPrecioVenta.Text = precioVenta.ToString("0.00");

            if (fechaCaducacion.HasValue)
            {
                chkTieneVencimiento.Checked = true;
                dtpFechaVencimiento.Enabled = true;

                // Permite cualquier fecha, sin importar si ya pasó
                dtpFechaVencimiento.MinDate = DateTimePicker.MinimumDateTime;
                dtpFechaVencimiento.Value = fechaCaducacion.Value;
            }
            else
            {
                chkTieneVencimiento.Checked = false;
                dtpFechaVencimiento.Enabled = false; // <--- bloqueado si no hay fecha
            }

            actualizarRegistro = true;
        }

      

        #region Métodos
        private void CargarCategorias()
        {
            try
            {
                var categoria = bPresentacion.listarPresentacion();
                if (categoria != null && categoria.Rows.Count > 0)
                {
                    cboPresentacion.DataSource = categoria;
                    cboPresentacion.ValueMember = "ID";
                    cboPresentacion.DisplayMember = "PRESENTACION";
                    cboPresentacion.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al cargar las categorías.");
            }
        }

        private void CargarLaboratorios()
        {
            try
            {
                var laboratorio = bLaboratorio.listarLaboratorios();
                if (laboratorio != null && laboratorio.Rows.Count > 0)
                {
                    cboLaboratorio.DataSource = laboratorio;
                    cboLaboratorio.ValueMember = "ID";
                    cboLaboratorio.DisplayMember = "LABORATORIO";
                    cboLaboratorio.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al cargar los laboratorios.");
            }
        }
        private void InicializarCombos()
        {
            try
            {
                CargarCategorias();
                CargarLaboratorios();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al cargar combos.");
            }
        }

        private void Limpiar()
        {
            txtId.Text = "0";
            txtCodigo.Clear();
            txtFarmaco.Clear();
            txtStock.Text = "0";
            txtPrecioCompra.Text = "0.00";
            txtPrecioVenta.Text = "0.00";
            cboPresentacion.SelectedIndex = 0;
            cboLaboratorio.SelectedIndex = 0;
            cboVentaReceta.SelectedIndex = 0;
            chkTieneVencimiento.Checked = false;
            dtpFechaVencimiento.Value = DateTime.Now;
        }

        private void MostrarError(string campo)
        {
            switch (campo)
            {
                case "codigo":
                    errorIcono.SetError(txtCodigo, "Ingrese un código válido");
                    txtCodigo.Focus();
                    break;
                case "nombreFarmaco":
                    errorIcono.SetError(txtFarmaco, "Ingrese el nombre del fármaco");
                    txtFarmaco.Focus();
                    break;
                case "idLaboratorio":
                    errorIcono.SetError(cboLaboratorio, "Seleccione un laboratorio");
                    cboLaboratorio.Focus();
                    break;
                case "idPresentacion":
                    errorIcono.SetError(cboPresentacion, "Seleccione una presentación");
                    cboPresentacion.Focus();
                    break;
                case "precioCompra":
                    errorIcono.SetError(txtPrecioCompra, "Ingrese un precio de compra válido");
                    txtPrecioCompra.Focus();
                    break;
                case "precioVenta":
                    errorIcono.SetError(txtPrecioVenta, "Ingrese un precio de venta válido");
                    txtPrecioVenta.Focus();
                    break;
            }
        }

        #endregion

        #region Botones
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            try
            {
                errorIcono.Clear();
                if (!int.TryParse(cboPresentacion.SelectedValue?.ToString(), out int idPresentacion))
                {
                    mensaje.mensajeValidacion("Debe especificar una categoría válida.");
                    errorIcono.SetError(cboPresentacion, "Este campo es obligatorio.");
                    cboPresentacion.Focus();
                    return;
                }

                if (!int.TryParse(cboLaboratorio.SelectedValue?.ToString(), out int idLaboratorio))
                {
                    mensaje.mensajeValidacion("Debe especificar un laboratorio válido.");
                    errorIcono.SetError(cboLaboratorio, "Este campo es obligatorio.");
                    cboLaboratorio.Focus();
                    return;
                }
                if (!decimal.TryParse(txtPrecioCompra.Text, out decimal precioCompra))
                {
                    mensaje.mensajeValidacion("El precio de compra no tiene un formato válido.");
                    errorIcono.SetError(txtPrecioCompra, "El precio de compra debe ser un número mayor a cero.");
                    txtPrecioCompra.Focus();
                    return;
                }

                if (!decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta))
                {
                    mensaje.mensajeValidacion("El precio de venta no tiene un formato válido.");
                    errorIcono.SetError(txtPrecioVenta, "El precio de venta debe ser un número mayor a cero.");
                    txtPrecioVenta.Focus();
                    return;
                }
                if (chkTieneVencimiento.Checked && dtpFechaVencimiento.Value.Date < DateTime.Today)
                {
                    mensaje.mensajeError("La fecha de caducación no puede ser menor al día de hoy.");
                    dtpFechaVencimiento.Focus();
                    return;
                }

                

                oFarmaco farmaco = new oFarmaco()
                {
                    idFarmaco = Convert.ToInt32(txtId.Text),
                    codigo = txtCodigo.Text.Trim(),
                    nombreFarmaco = txtFarmaco.Text.Trim(),
                    descripcion = txtDescripcion.Text.Trim(),
                    idPresentacion = Convert.ToInt32(cboPresentacion.SelectedValue),
                    idLaboratorio = Convert.ToInt32(cboLaboratorio.SelectedValue),
                    tipoVenta = cboVentaReceta.Text,
                    stock = int.Parse(txtStock.Text),
                    precioCompra = decimal.Parse(txtPrecioCompra.Text),
                    precioVenta = decimal.Parse(txtPrecioVenta.Text),
                    fechaCaducacion = chkTieneVencimiento.Checked ? dtpFechaVencimiento.Value : (DateTime?)null,
                    estado = true
                };

                resultadoOperacion resultado;
                if (int.TryParse(txtId.Text.Trim(), out int idProducto) && idProducto == 0)
                {
                    resultado = bFarmaco.registrarFarmaco(farmaco);
                }
                else
                {
                    farmaco.idFarmaco = idProducto;
                    resultado = bFarmaco.editarFarmaco(farmaco);
                }

                if (!resultado.esValido)
                {
                    mensaje.mensajeValidacion(resultado.mensaje);
                    if (!string.IsNullOrWhiteSpace(resultado.campoInvalido))
                        MostrarError(resultado.campoInvalido);
                    return;
                }

                mensaje.mensajeOk(resultado.mensaje);
                registroAgregado?.Invoke();
                Limpiar();
                this.Close();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al registrar el fármaco.");
            }
        }



        #endregion

        #region Validaciones
        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidades.numerosEnteros(e, txtStock.Text.Trim());
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidades.validarDecimal(sender, e);
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidades.validarDecimal(sender, e);
        }

        private void txtPrecioCompra_Leave(object sender, EventArgs e)
        {
            Utilidades.formatoDecimal((Control)sender);
        }

        private void txtPrecioVenta_Leave(object sender, EventArgs e)
        {
            Utilidades.formatoDecimal((Control)sender);
        }

        private void chkTieneVencimiento_CheckedChanged(object sender, EventArgs e)
        {


            dtpFechaVencimiento.Enabled = chkTieneVencimiento.Checked;
            if (chkTieneVencimiento.Checked && !actualizarRegistro)
            {
                dtpFechaVencimiento.MinDate = DateTime.Today;
            }
        }



        #endregion

        //esto es para el cosito de las fechas no pueda seleccionar una fecha anterior a la actual
        private void FrmAgregarFarmaco_Load(object sender, EventArgs e)
        {
            // Solo restringir la fecha si NO estamos editando
            if (!actualizarRegistro)
            {
                dtpFechaVencimiento.MinDate = DateTime.Today; // No permite fechas pasadas
            }
            else
            {
                // Al editar, permite mostrar cualquier fecha sin error
                dtpFechaVencimiento.MinDate = DateTimePicker.MinimumDateTime;
            }
        }
    }
}
