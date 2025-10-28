using Farmacia.BLL;
using Farmacia.Entity;
using Sistema.BLL;
using Sistema.UI.Modulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Sistema.UI.Formularios
{
    public partial class FrmCompra : Form
    {
        private DataTable detalleCompra = new DataTable();
        Mensajes mensaje = new Mensajes();
        public FrmCompra()
        {
            InitializeComponent();
        }

        #region "Metodos"

        private void crearTabla()
        {
            try
            {
                detalleCompra.Columns.Add("IdFarmaco", System.Type.GetType("System.Int32"));
                detalleCompra.Columns.Add("Farmaco", System.Type.GetType("System.String"));
                detalleCompra.Columns.Add("Cantidad", System.Type.GetType("System.Int32"));
                detalleCompra.Columns.Add("precioCompra", System.Type.GetType("System.Decimal"));
                //detalleCompra.Columns.Add("precioVenta", System.Type.GetType("System.Decimal"));
                //detalleCompra.Columns.Add("impuesto", System.Type.GetType("System.String"));

                dgvListadoDetalles.DataSource = detalleCompra;
                formatoTabla();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al crear el DataTable.");
            }
        }

        private void bloquearControles()
        {
            gunaCarrito.Enabled = false;
            gunaProductos.Enabled = false;
            btnGuardar.Enabled = false;
            btnImprimer.Enabled = true;
            btnNuevo.Enabled = true;
        }

        private void DesbloquearControles()
        {
            gunaCarrito.Enabled = true;
            gunaProductos.Enabled = true;
            btnGuardar.Enabled = true;
            btnImprimer.Enabled = false;
            btnNuevo.Enabled = false;
            detalleCompra.Clear();
            lblSubtotal.Text = "0.00";
            lblImpuesto.Text = "0.00";
            lblTotal.Text = "0.00";

            Variables.terminarCompra = 0;
            Variables.numeroCompra = "";

            txtBuscarProductos.Focus();
        }

        private void formatoTabla()
        {
            dgvListadoDetalles.Columns[0].Visible = false;
            dgvListadoDetalles.Columns[1].HeaderText = "Producto";
            dgvListadoDetalles.Columns[2].HeaderText = "Cantidad";
            dgvListadoDetalles.Columns[3].HeaderText = "Precio";
            //dgvListadoDetalles.Columns[4].Visible = false;
            //dgvListadoDetalles.Columns[4].Visible = false;

            dgvListadoDetalles.Columns["precioCompra"].DefaultCellStyle.Format = "N2";
            dgvListadoDetalles.Columns["Farmaco"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvListadoDetalles.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListadoDetalles.Columns["precioCompra"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvListadoDetalles.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListadoDetalles.Columns["precioCompra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvListadoDetalles.Columns[1].ReadOnly = true;
            dgvListadoDetalles.Columns[3].ReadOnly = true;
        }

        private void ajustarColumnas()
        {
            try
            {
                if (dgvListadoDetalles.Columns.Count == 0)
                    return;

                int anchoDisponible = dgvListadoDetalles.ClientSize.Width;

                int anchoCant = 120;
                int anchoPrecio = 120;

                int anchoFarmaco = anchoDisponible - (anchoCant + anchoPrecio + 20);

                dgvListadoDetalles.Columns["Cantidad"].Width = anchoCant;
                dgvListadoDetalles.Columns["precioCompra"].Width = anchoPrecio;
                dgvListadoDetalles.Columns["Farmaco"].Width = anchoFarmaco;
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al ajustar el diseño de los datos.");
            }
        }

        private void limpiarControles()
        {
            lblId.Text = "0";
            lblcodigo.Text = "";
            lblFarmacos.Text = "";
            lblNombreLab.Text = "";
            lblprecio.Text = "0";
            lblStock.Text = "0";

            lblcodigo.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            lblFarmacos.Visible = false;
            laaaaaaab.Visible = false;
            lblNombreLab.Visible = false;
            lblTipoVentas.Visible = false;
            lblTipoVentaValor.Visible = false;
            lblprecio.Visible = false;
            lblStock.Visible = false;
            txtBuscarProductos.Focus();
        }

        private void buscarCaja()
        {
            try
            {
                var cajaAbierta = bCaja.buscarCaja(Variables.idUsuario);
                if (cajaAbierta != null && cajaAbierta.Rows.Count > 0)
                {
                    var id = cajaAbierta.Rows[0]["ID"];
                    if(id != DBNull.Value)
                    {
                        Variables.idCaja = Convert.ToInt32(id);
                    }
                }
                else
                {
                    mensaje.mensajeInformacion("No existe una caja abierta para este usuario.");
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al obtener la caja disponible.");
            }
        }

        public void agregarProductos(int idFarmaco, string farmaco, decimal precio, int stock, string impuesto)
        {
            try
            {
                bool registroDuplicado = false;

                foreach(DataGridViewRow fila in dgvListadoDetalles.Rows)
                {
                    if (fila.Cells["IdFarmaco"].Value != null && Convert.ToInt32(fila.Cells["IdFarmaco"].Value) == idFarmaco)
                    {
                        mensaje.mensajeInformacion("El fármaco ya se encuentra en la lista.");
                        registroDuplicado = true;
                        break;
                    }
                }

                if(!registroDuplicado)
                {
                    DataRow nuevaFila = detalleCompra.NewRow();
                    nuevaFila["IdFarmaco"] = idFarmaco;
                    nuevaFila["Farmaco"] = farmaco;
                    nuevaFila["Cantidad"] = 1;
                    nuevaFila["precioCompra"] = precio;
                    //nuevaFila["precioVenta"] = idFarmaco;
                    //nuevaFila["impuesto"] = impuesto;

                    detalleCompra.Rows.Add(nuevaFila); 
                }
                limpiarControles();
                btnGuardar.Enabled = true;
                CalcularTotales();

                txtBuscarProductos.Focus();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al agregar el producto al DataTable.");
            }
        }

        private void buscarProducto()
        {
            try
            {
                var farmacos = bFarmaco.buscarFarmaco(1, txtBuscarProductos.Text.Trim());
                if (farmacos.Rows.Count > 0)
                {

                    lblId.Text = farmacos.Rows[0]["ID"].ToString();
                    lblFarmacos.Text = farmacos.Rows[0]["FARMACO"].ToString();
                    lblcodigo.Text = farmacos.Rows[0]["CODIGO"].ToString();
                    lblStock.Text = Convert.ToInt32(farmacos.Rows[0]["STOCK"]).ToString();
                    lblprecio.Text = Convert.ToDecimal(farmacos.Rows[0]["P_COMPRA"]).ToString("N2");
                    lblNombreLab.Text = farmacos.Rows[0]["LABORATORIO"].ToString();

                    lblTipoVentas.Visible = true;
                    lblcodigo.Visible = true;
                    lblFarmacos.Visible = true;
                    lblNombreLab.Visible = true;
                    laaaaaaab.Visible = true;
                    label3.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    lblTipoVentaValor.Visible = true;
                    lblprecio.Visible = true;
                    lblStock.Visible = true;

                    txtBuscarProductos.Focus();
                }
                else
                {
                    limpiarControles();
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al buscar el producto.");
            }
        }

        private void CalcularTotales()
        {

            try
            {
                decimal subTotal = 0;
                decimal impuestoTotal = 0;
                //const decimal tasaImpuesto = 0.15m;

                foreach (DataRow fila in detalleCompra.Rows)
                {
                    int cantidad = Convert.ToInt32(fila["Cantidad"]);
                    decimal precio = Convert.ToDecimal(fila["precioCompra"]);

                    decimal totalFila = cantidad * precio;

                    subTotal += totalFila;
                }

                decimal totalGeneral = subTotal + impuestoTotal;

                lblSubtotal.Text = subTotal.ToString("N2");
                lblImpuesto.Text = impuestoTotal.ToString("N2");
                lblTotal.Text = totalGeneral.ToString("N2");
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al calcular totales.");
            }
        }

        private void Txt_KeyPreesCantidad(object sender, KeyPressEventArgs e)
        {
            if(sender is TextBox txt)
            {
                Utilidades.numerosEnteros(e, txt.Text);
            }
        }

        #endregion

        #region "Botones"
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            DesbloquearControles();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Variables.idCaja == 0)
                {
                    mensaje.mensajeInformacion("Es necesario aperturar la caja para continuar.");
                }

                if(dgvListadoDetalles.Rows.Count == 0)
                {
                    mensaje.mensajeValidacion("Debe agregar al menos un fármaco al carrito de compras.");
                    return;
                }

                if (!decimal.TryParse(lblSubtotal.Text.Trim(), out decimal subtotal))
                {
                    mensaje.mensajeError("Ocurrió un error al obtener el subtotal. Verifique que es un valor válido");
                    return;
                }

                if (!decimal.TryParse(lblImpuesto.Text.Trim(), out decimal impuesto))
                {
                    mensaje.mensajeError("Ocurrió un error al obtener el impuesto. Verifique que es un valor válido");
                    return;
                }

                if (!decimal.TryParse(lblTotal.Text.Trim(), out decimal total) && total <= 0)
                {
                    mensaje.mensajeError("El total de la compra debe ser mayor a 0.");
                    return;
                }

                FrmTerminarCompra frm = new FrmTerminarCompra(total);
                mostrarModal.MostrarConCapaTransparente(this, frm);

                if(Variables.terminarCompra == 1)
                {
                    oCompra compra = new oCompra()
                    {
                        idUsuario = Variables.idUsuario,
                        idCaja = Variables.idCaja,
                        idLaboratorio = Variables.idLaboratorio,
                        subtotal = subtotal,
                        impuesto = impuesto,
                        totalGeneral = total,
                        NCompra = Variables.NCompra,
                        montoEfectivo = Variables.montoEfectivo,
                        montoTarjeta = Variables.montoTarjeta,
                        cambio = Variables.cambio,
                        detalles = detalleCompra
                    };
                    var resultado = bCompras.registrarCompra(compra);
                    {
                        if(!resultado.esValido)
                        {
                            mensaje.mensajeValidacion(resultado.mensaje);
                            return;
                        }

                        Variables.numeroCompra = resultado.numeroCompra;

                        btnImprimer_Click(this, EventArgs.Empty);
                        bloquearControles();

                        Variables.montoEfectivo = 0;
                        Variables.montoTarjeta = 0;
                        Variables.cambio = 0;
                    }

                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al guardar el fármaco.");
            }
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region "Eventos de las cajas de texto"
        private void txtBuscarProductos_IconLeftClick(object sender, EventArgs e)
        {
            frmBuscarFarmacosCompras frm = new frmBuscarFarmacosCompras();

            frm.Owner = this;

            // vamos a obtener la posicion del GunaProducto
            Point ubicacion = gunaProductos.PointToScreen(Point.Empty);

            // mover formulario buscar producto
            int x = ubicacion.X + gunaProductos.Width - frm.Width;
            int y = ubicacion.Y + gunaProductos.Height - frm.Height;

            frm.Location = new Point(x, y);

            mostrarModal.MostrarConCapaTransparente(this, frm);
        }

        private void txtBuscarProductos_TextChanged(object sender, EventArgs e)
        {
            buscarProducto();
        }

        private void txtBuscarProductos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (lblId.Text.Trim() != "0")
                    {
                        int.TryParse(lblId.Text.Trim(), out int idFarmaco);
                        string farmaco = lblFarmacos.Text.Trim();
                        int.TryParse(lblStock.Text.Trim(), out int stock);
                        decimal.TryParse(lblprecio.Text.Trim(), out decimal precio);
                        string grabaImpuesto = lblImpuesto.Text.Trim();

                        agregarProductos(idFarmaco, farmaco, precio, stock, grabaImpuesto);
                        txtBuscarProductos.Clear();
                    }
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error inesperado al agregar el fármaco al DataTable.");
            }
        }
        #endregion

        #region "Eventos del Formulario"
        private void FrmCompra_Load(object sender, EventArgs e)
        {
            crearTabla();
            buscarCaja();
        }
        private void FrmCompra_Shown(object sender, EventArgs e)
        {
            ajustarColumnas();
        }

        private void FrmCompra_Resize(object sender, EventArgs e)
        {
            ajustarColumnas();
        }
        private void FrmCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtBuscarProductos_IconLeftClick(sender, e);
            }

            if (e.KeyCode == Keys.F12)
            {
                btnGuardar_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.N)
            {
                btnNuevo_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.P)
            {
                btnImprimer_Click(sender, e);
            }
        }
        #endregion

        #region "Eventos del Data"
        private void dgvListadoDetalles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvListadoDetalles.Rows[e.RowIndex] != null)
                {
                    DataGridViewRow fila = dgvListadoDetalles.Rows[e.RowIndex];
                    {
                        if (fila.Cells["Cantidad"].Value != null && fila.Cells["precioCompra"] != null)
                        {
                            CalcularTotales();
                        }
                    }
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al editar el fármaco.");
            }
        }

        private void dgvListadoDetalles_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if(dgvListadoDetalles.CurrentCell.ColumnIndex == 2 && e.Control is TextBox txt)
                {
                    txt.KeyPress -= Txt_KeyPreesCantidad;
                    txt.KeyPress += Txt_KeyPreesCantidad;
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al validar los datos.");
            }
        }

        private void dgvListadoDetalles_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // yo no tengo stock, yo voy a añadir stock al producto, no se si tengo que hacer esto, pero por si las dudas lo come

            //try
            //{
            //    if (dgvListadoDetalles.Columns[e.ColumnIndex].Name == "Cantidad")
            //    {
            //        if (string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
            //        {
            //            mensaje.mensajeError("La cantidad debe de ser un número entero mayor a cero.");
            //            e.Cancel = true;
            //        }
            //        else
            //        {
            //            int cantidad = int.Parse(e.FormattedValue.ToString());
            //            int stockDisponible = Convert.ToInt32(dgvListadoDetalles.CurrentRow.Cells["stock"].Value);
            //            if(cantidad > stockDisponible)
            //            {
            //                mensaje.mensajeInformacion($"La cantidad no puede ser mayor al stock: {stockDisponible}.");
            //                e.Cancel = true;
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    mensaje.mensajeError("Ocurrió un error al validar los datos.");
            //}
        }

        private void dgvListadoDetalles_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                CalcularTotales();
                txtBuscarProductos.Focus();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al eliminar el fármaco del carrito.");
            }
        }
        #endregion
    }
}
