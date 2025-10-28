using Farmacia.BLL;
using Farmacia.Entity;
using Sistema.UI.Modulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Sistema.UI.Formularios
{
    public partial class frmVentas : Form
    {
        private DataTable detalleVenta = new DataTable();
        private Mensajes mensajes = new Mensajes();

        public frmVentas()
        {
            InitializeComponent();
        }

        #region Métodos

        private void CrearTabla()
        {
            try
            {
                detalleVenta.Columns.Add("idFarmaco", System.Type.GetType("System.Int32"));
                detalleVenta.Columns.Add("Farmaco", System.Type.GetType("System.String"));
                detalleVenta.Columns.Add("Cantidad", System.Type.GetType("System.Int32"));
                detalleVenta.Columns.Add("Precio", System.Type.GetType("System.Decimal"));
                detalleVenta.Columns.Add("Stock", System.Type.GetType("System.Decimal"));

                dgvListadoDetalles.DataSource = detalleVenta;
                FormatoTabla();
            }
            catch (Exception)
            {
                mensajes.mensajeError("Errro al crear el DataTable.");
            }
        }

        private void FormatoTabla()
        {
            dgvListadoDetalles.Columns[0].Visible = false;
            dgvListadoDetalles.Columns[1].HeaderText = "FARMACO";
            dgvListadoDetalles.Columns[2].HeaderText = "CANTIDAD";
            dgvListadoDetalles.Columns[3].HeaderText = "PRECIO";
            dgvListadoDetalles.Columns[4].Visible = false;

            dgvListadoDetalles.Columns["Precio"].DefaultCellStyle.Format = "N2";

            dgvListadoDetalles.Columns["Farmaco"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgvListadoDetalles.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListadoDetalles.Columns["PRECIO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvListadoDetalles.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListadoDetalles.Columns["PRECIO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvListadoDetalles.Columns[1].ReadOnly = true;
            dgvListadoDetalles.Columns[3].ReadOnly = true;
        }

        private void AjustarColumnas()
        {
            try
            {
                if (detalleVenta.Columns.Count == 0)
                    return;

                int anchodisponible = dgvListadoDetalles.ClientSize.Width;

                int anchoCant = 120;
                int anchoPrecio = 200;

                int anchoProducto = anchodisponible - (anchoCant + anchoPrecio + 20);

                dgvListadoDetalles.Columns["CANTIDAD"].Width = anchoCant;
                dgvListadoDetalles.Columns["FARMACO"].Width = anchoProducto;
                dgvListadoDetalles.Columns["PRECIO"].Width = anchoPrecio;

            }
            catch (Exception)
            { 
                mensajes.mensajeError("Ocurrió un error al ajustar el diseño del DataGridView.");
            }
        }

        private void BuscarProducto()
        {
            //try
            //{
                
                var producto = bFarmaco.buscarFarmacoVenta(1, txtBuscarProductos.Text.Trim());
                if (producto.Rows.Count > 0)
                {
                    lblId.Text = producto.Rows[0]["ID"].ToString();
                    lblcodigo.Text = producto.Rows[0]["Codigo"].ToString();
                    lblFarmacos.Text = producto.Rows[0]["Farmaco"].ToString();
                    lblLaboratorio.Text = producto.Rows[0]["Laboratorio"].ToString();
                    lblprecio.Text = Convert.ToDecimal(producto.Rows[0]["P_VENTA"]).ToString("N2");
                    lblTipoVentaValor.Text = producto.Rows[0]["TIPO_VENTA"].ToString();
                    lblDescripcionValor.Text = producto.Rows[0]["Descripcion"].ToString();
                    lblStock.Text = producto.Rows[0]["Stock"].ToString();
                    //lblGrabaImpuesto.Text = proucto.Rows[0]["IMPUESTO"].ToString();

                    lblcodigo.Visible = true;
                    lblFarmacos.Visible = true;
                    lblLaboratorio.Visible = true;
                    lblDescripcionValor.Visible = true;
                    label3.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    lblDescripcion.Visible = true;
                    lblTipoVentaValor.Visible = true;
                    lblprecio.Visible = true;
                    lblStock.Visible = true;
                    
                    txtBuscarProductos.Focus();
                }
                else
                {
                    LimpiarControles();
                }
            //}
            //catch (Exception ex)
            //{
            //    mensajes.mensajeError("Error al buscar el producto. Detalles" + ex.Message);
            //}
        }

        private void LimpiarControles()
        {
            lblId.Text = "0";
            lblcodigo.Text = "";
            lblFarmacos.Text = "";
            lblLaboratorio.Text = "";
            lblprecio.Text = "0";
            lblStock.Text = "0";

            lblcodigo.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            lblDescripcion.Visible = false;
            lblFarmacos.Visible = false;
            lblLaboratorio.Visible = false;
            lblDescripcionValor.Visible = false;
            lblTipoVentaValor.Visible = false;
            lblprecio.Visible = false;
            lblStock.Visible = false;
            txtBuscarProductos.Focus();
        }

        public void AgregarProductos(int idFarmaco, string Farmaco, decimal precio, int stock)
        {
            try
            {
                bool registroDuplicado = false;

                foreach (DataGridViewRow fila in dgvListadoDetalles.Rows)
                {
                    if (fila.Cells["idFarmaco"].Value != null && Convert.ToInt32(fila.Cells["idFarmaco"].Value) == idFarmaco)
                    {
                        mensajes.mensajeInformacion("El producto ya se encuentra en la lista.");
                        registroDuplicado = true;
                        break;
                    }
                }

                if (!registroDuplicado)
                {
                    DataRow nuevaFila = detalleVenta.NewRow();
                    nuevaFila["idFarmaco"] = idFarmaco;
                    nuevaFila["Farmaco"] = Farmaco;
                    nuevaFila["Cantidad"] = 1;
                    nuevaFila["Precio"] = precio;
                    nuevaFila["Stock"] = stock;
                    detalleVenta.Rows.Add(nuevaFila);
                }

                LimpiarControles();
                btnGuardar.Enabled = true;
                btnNuevo.Enabled = true;
                CalcularTotales();

                txtBuscarProductos.Focus();


            }
            catch (Exception)
            {
                mensajes.mensajeError("Ocurrió un error al agregar registro al DataTable.");
            }
        }

        private void CalcularTotales()
        {
            try
            {
                decimal subTotal = 0;
                decimal impuestoTotal = 0;
                //const decimal tasaImpuesto = 0.15m;

                foreach (DataRow fila in detalleVenta.Rows)
                {
                    int cantidad = Convert.ToInt32(fila["Cantidad"]);
                    decimal precio = Convert.ToDecimal(fila["Precio"]);

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
                mensajes.mensajeError("Error al calcular totales.");
            }
        }

        private void Txt_KeyPressCantidad(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox txt)
                Utilidades.numerosEnteros(e, txt.Text);
        }

        private void BloquearControles()
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
            detalleVenta.Clear();
            lblSubtotal.Text = "0.00";
            lblImpuesto.Text = "0.00";
            lblTotal.Text = "0.00";

            Variables.terminarVentas = 0;
            Variables.numeroPedido = "";

            // LIMPIAR AQUÍ cuando se crea una NUEVA venta
            Variables.montoEfectivoVenta = 0;
            Variables.montoTarjetaVenta = 0;
            Variables.cambioVenta = 0;

            txtBuscarProductos.Focus();
        }

        private void imprimirVoucher(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;

                // === Fuentes ===
                Font fontTitulo = new Font("Consolas", 11, FontStyle.Bold);
                Font fontNormal = new Font("Consolas", 9);
                Font fontNegrita = new Font("Consolas", 9, FontStyle.Bold);

                // === Formatos ===
                StringFormat formatoDerecha = new StringFormat { Alignment = StringAlignment.Far };
                StringFormat formatoCentro = new StringFormat { Alignment = StringAlignment.Center };

                // === Coordenadas base ===
                float y = 20;
                float lineHeight = fontNormal.GetHeight();
                float etiquetaX = 80f;
                float precioX = 150f;
                float columnaEtiquetaAncho = 80f;
                float anchoVoucher = 280f;

                // === ENCABEZADO ===
                RectangleF rect = new RectangleF(0, y, anchoVoucher, lineHeight);
                g.DrawString("FARMACIA LA JOHARFARMA", fontTitulo, Brushes.Black, rect, formatoCentro); y += lineHeight + 10;

                rect = new RectangleF(0, y, anchoVoucher, lineHeight);
                g.DrawString("Tel: 2266-0000", fontNormal, Brushes.Black, rect, formatoCentro); y += lineHeight;

                rect = new RectangleF(0, y, anchoVoucher, lineHeight);
                g.DrawString("RUC: 0011223344", fontNormal, Brushes.Black, rect, formatoCentro); y += lineHeight + 5;

                g.DrawString("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fontNormal, Brushes.Black, 10, y); y += lineHeight;
                g.DrawString("N° Pedido: " + Variables.numeroPedido, fontNormal, Brushes.Black, 10, y); y += lineHeight + 5;

                // === Separadores ===
                g.DrawString(new string('-', 40), fontNormal, Brushes.Black, 0, y); y += lineHeight;
                g.DrawString("Detalle de la venta", fontNormal, Brushes.Black, 10, y); y += lineHeight;
                g.DrawString(new string('-', 40), fontNormal, Brushes.Black, 0, y); y += lineHeight;

                // === DETALLE DE PRODUCTOS ===
                foreach (DataGridViewRow fila in dgvListadoDetalles.Rows)
                {
                    if (fila.IsNewRow) continue;

                    int cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                    string producto = fila.Cells["Farmaco"].Value?.ToString() ?? "";
                    decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);
                    //string impuesto = fila.Cells["impuesto"].Value?.ToString() ?? "E";

                    if (producto.Length > 15)
                        producto = producto.Substring(0, 15);

                    string linea = $"{cantidad,3}  {producto,-15} {precio,15}";
                    g.DrawString(linea, fontNormal, Brushes.Black, 0, y);
                    y += lineHeight;
                }

                // === TOTALES ===
                y += 10;
                g.DrawString(new string('-', 40), fontNormal, Brushes.Black, 0, y); y += lineHeight;

                g.DrawString("SUBTOTAL:", fontNormal, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(lblSubtotal.Text.PadLeft(15), fontNormal, Brushes.Black, precioX, y); y += lineHeight;

                g.DrawString("IMPUESTO:", fontNormal, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(lblImpuesto.Text.PadLeft(15), fontNormal, Brushes.Black, precioX, y); y += lineHeight;

                g.DrawString("TOTAL:", fontNegrita, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(lblTotal.Text.PadLeft(15), fontNegrita, Brushes.Black, precioX, y); y += lineHeight;

                //Asi estaba antes
                string Efectivo = Convert.ToDecimal(Variables.montoEfectivoVenta).ToString("N2").PadLeft(15);
                string Tarjeta = Convert.ToDecimal(Variables.montoTarjetaVenta).ToString("N2").PadLeft(15);
                string Cambio = Convert.ToDecimal(Variables.cambioVenta).ToString("N2").PadLeft(15);

                g.DrawString("EFECTIVO:", fontNormal, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(Efectivo, fontNormal, Brushes.Black, precioX, y); y += lineHeight;

                g.DrawString("TARJETA:", fontNormal, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(Tarjeta, fontNormal, Brushes.Black, precioX, y); y += lineHeight;

                g.DrawString("CAMBIO:", fontNegrita, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(Cambio, fontNegrita, Brushes.Black, precioX, y); y += lineHeight + 20;

                // === MENSAJE FINAL CENTRADO ===
                rect = new RectangleF(0, y, anchoVoucher, lineHeight);
                g.DrawString("Gracias por su compra", fontNormal, Brushes.Black, rect, formatoCentro); y += lineHeight;

                rect = new RectangleF(0, y, anchoVoucher, lineHeight);
                g.DrawString("¡Vuelva pronto!", fontNormal, Brushes.Black, rect, formatoCentro);
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al imprimir el voucher.");
            }
        }


        #endregion

        #region eventos del formulario
        private void frmVentas_Load_1(object sender, EventArgs e)
        {
            CrearTabla();
        }

        private void frmVentas_Resize(object sender, EventArgs e)
        {
            AjustarColumnas();
        }

        private void frmVentas_Shown(object sender, EventArgs e)
        {
            AjustarColumnas();
        }

        private void frmVentas_KeyDown(object sender, KeyEventArgs e)
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

        #region Cajas de textos
        private void txtBuscarProductos_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarProductos.Text == string.Empty)
            {
                LimpiarControles();
            }
            else
            {
                BuscarProducto();

            }
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
                        string Farmaco = lblFarmacos.Text.Trim();
                        decimal.TryParse(lblprecio.Text.Trim(), out decimal precio);
                        int.TryParse(lblStock.Text.Trim(), out int stock);

                        


                        AgregarProductos(idFarmaco, Farmaco, precio,stock);
                        txtBuscarProductos.Clear();
                    }
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error inesperado al agregar registros al DataTable.");
            }
        }
        #endregion

        #region Botones de comandos

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            DesbloquearControles();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Variables.idCaja == 0)
                //{
                //    mensaje.mensajeInformacion("Es necesario aperturar la caja para continuar.");
                //    return;
                //}

                if (dgvListadoDetalles.Rows.Count == 0)
                {
                    mensajes.mensajeValidacion("Debe agregar al menos un producto al carrito de compra.");
                    return;
                }

                if (!decimal.TryParse(lblSubtotal.Text.Trim(), out decimal subTotal))
                {
                    mensajes.mensajeError("No se pudo obtener el subtotal. Verifique que es un valor válido.");
                    return;
                }

                //if (!decimal.TryParse(lblImpuesto.Text.Trim(), out decimal impuesto))
                //{
                //    mensaje.mensajeError("No se pudo obtener el impuesto. Verifique que es un valor válido.");
                //    return;
                //}

                if (!decimal.TryParse(lblTotal.Text.Trim(), out decimal total) && total <= 0)
                {
                    mensajes.mensajeError("El total de la venta debe ser mayor a cero.");
                    return;
                }

                frmTerminarVenta frm = new frmTerminarVenta(total);
                mostrarModal.MostrarConCapaTransparente(this, frm);

                /*
                 public int idVenta { get; set; }
        public int idUsuario { get; set; }
        public string cliente { get; set; }
        public string NVenta { get; set; }
        public decimal subTotal { get; set; }
        public decimal impuesto { get; set; }
        public decimal totalGeneral { get; set; }
        public decimal montoEfectivo { get; set; }
        public decimal montoTargeta { get; set; }
        public DataTable detalles { get; set; }
                 */

                if (Variables.terminarVentas == 1)
                {
                    oVentas Ventas = new oVentas()
                    {
                        //idUsuario = Variables.idUsuario,
                        cliente = Variables.clientes,
                        NVenta = Variables.NVentas,
                        subTotal = subTotal,
                        totalGeneral = total,
                        montoEfectivo = Variables.montoEfectivoVenta,
                        montoTargeta = Variables.montoTarjetaVenta,
                        cambio = Variables.cambioVenta,
                        detalles = detalleVenta
                    };

                    var resultado = bVentas.registrarVentas(Ventas);

                    if (!resultado.esValido)
                    {
                        mensajes.mensajeValidacion(resultado.mensaje);
                        return;
                    }

                    Variables.numeroPedido = resultado.numeroVenta;

                    btnImprimer_Click(this, EventArgs.Empty);
                    BloquearControles();

                    //Aqui estaba antes pero como al guardar la venta se pasa los valores al voucher pero al tocar el boton no porque
                    //lo inicializa en 0 entonces lo comente

                    //Variables.montoEfectivoVenta = 0;
                    //Variables.montoTarjetaVenta = 0;
                    //Variables.cambioVenta = 0;
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al guardar el registro.");
            }

            
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {

            try
            {
                PrintDocument printDoc = new PrintDocument();

                // Crear el documento
                printDoc = new PrintDocument();

                // Altura dinámica
                int filas = dgvListadoDetalles.Rows.Count;
                int altoBase = 300;
                int altoPorFila = 40;
                int alturaFinal = altoBase + (filas * altoPorFila);

                // Configurar tamaño de papel personalizado
                PaperSize papel = new PaperSize("Voucher", 280, alturaFinal);
                printDoc.DefaultPageSettings.PaperSize = papel;

                // Asignar el evento
                printDoc.PrintPage += imprimirVoucher;

                

                // Mostrar vista previa centrada
                PrintPreviewDialog preview = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 400,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterScreen
                };

                // Zoom 100%
                foreach (Control c in preview.Controls)
                {
                    if (c is PrintPreviewControl ctrl)
                    {
                        ctrl.AutoZoom = true;
                        break;
                    }
                }

                preview.ShowDialog();
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al mostrar vista previa.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtBuscarProductos_IconLeftClick(object sender, EventArgs e)
        {
            frmBuscarFarmacosVenta frm = new frmBuscarFarmacosVenta(frmBuscarFarmacosVenta.tipoFormulario.venta);
            frm.Owner = this;

            //vamos a obtener la pocicion del gunaproducto
            Point ubicacion = gunaProductos.PointToScreen(Point.Empty);

            //mover formuario buscar productos
            int x = ubicacion.X + gunaProductos.Width - frm.Width;
            int y = ubicacion.Y + gunaProductos.Height - frm.Height;

            frm.Location = new Point(x, y);
            mostrarModal.MostrarConCapaTransparente(this, frm);
        }





        #endregion

        #region Eventos del datagrid

        private void dgvListadoDetalles_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dgvListadoDetalles.CurrentCell.ColumnIndex == 2 && e.Control is TextBox txt)
                {
                    txt.KeyPress -= Txt_KeyPressCantidad;
                    txt.KeyPress += Txt_KeyPressCantidad;
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al validar datos.");
            }
        }

        private void dgvListadoDetalles_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvListadoDetalles.Columns[e.ColumnIndex].Name == "Cantidad")
                {
                    if (string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                    {
                        mensajes.mensajeError("La cantidad debe ser un número entero mayor a cero.");
                        e.Cancel = true;
                    }
                    else
                    {
                        int cantidad = int.Parse(e.FormattedValue.ToString());
                        int stockDisponible = Convert.ToInt32(dgvListadoDetalles.CurrentRow.Cells["Stock"].Value);
                        if (cantidad > stockDisponible)
                        {
                            mensajes.mensajeInformacion($"La cantidad no puede ser mayor al stock: {stockDisponible}.");
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al validar los datos.");
            }
        }

        private void dgvListadoDetalles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvListadoDetalles.Rows[e.RowIndex] != null)
                {
                    DataGridViewRow fila = dgvListadoDetalles.Rows[e.RowIndex];

                    if (fila.Cells["Cantidad"].Value != null && fila.Cells["Precio"].Value != null)
                    {
                        CalcularTotales();
                    }
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al editar registro.");
            }
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
                mensajes.mensajeError("Error al eliminar registro.");
            }
        }
        #endregion


    }
}
