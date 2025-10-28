using Farmacia.Entity;
using Sistema.BLL;
using Sistema.Entity;
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

namespace Sistema.UI.Formularios
{
    public partial class frmCerrarCaja : Form
    {
        private Mensajes mensaje = new Mensajes();

        public frmCerrarCaja()
        {
            InitializeComponent();

            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;
        }

        #region Métodos

        private void errorControl(string campo)
        {
            switch (campo)
            {
                case "idUsuario":
                    mensaje.mensajeError("el ID del usuario no es válido.");
                    break;

                case "totalEntregado":
                    errorIcono.SetError(txtEntregar, "El monto de cierre debe ser un npumeto mayor a cero.");
                    txtEntregar.Focus();
                    break;

                case "codigo":
                    errorIcono.SetError(txtCodigo, "Este campo es obligatorio.");
                    txtCodigo.Focus();
                    break;
            }
        }

        private void buscarCaja()
        {
            try
            {
                var cajaAbierta = bCaja.buscarCaja(Variables.idUsuario);
                if (cajaAbierta.Rows.Count > 0)
                {
                    Variables.idCaja = Convert.ToInt32(cajaAbierta.Rows[0]["ID"]);
                    txtCaja.Text = cajaAbierta.Rows[0]["CAJA"].ToString();
                    var valor = cajaAbierta.Rows[0]["INICIA"];

                    if (valor != DBNull.Value)
                    {
                        txtApertura.Text = Convert.ToDecimal(valor).ToString("N2");
                    }
                    else
                    {
                        txtApertura.Text = "0.00";
                    }

                    SumarTotales();
                }
                else
                {
                    mensaje.mensajeError("Error: No hay caja abierta para este usuario.");
                    Close();
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al verificar el estado de la caja.");
            }
        }

        private void SumarTotales()
        {
            //try
            //{
                var cajaAbierta = bCaja.SumarTotales(Variables.idUsuario);
                if (cajaAbierta.Rows.Count > 0)
                {
                    var row = cajaAbierta.Rows[0];

                    decimal totalEfectivo = row["TOTAL_EFECTIVO"] != DBNull.Value ? Convert.ToDecimal(row["TOTAL_EFECTIVO"]) : 0;
                    decimal totalTarjeta = row["TOTAL_TARJETA"] != DBNull.Value ? Convert.ToDecimal(row["TOTAL_TARJETA"]) : 0;
                    decimal totalCambio = row["TOTAL_CAMBIO"] != DBNull.Value ? Convert.ToDecimal(row["TOTAL_CAMBIO"]) : 0;

                    txtEfectivo.Text = totalEfectivo.ToString("N2");
                    txtTarjeta.Text = totalTarjeta.ToString("N2");
                    txtCambio.Text = totalCambio.ToString("N2");

                    if (decimal.TryParse(txtApertura.Text.Trim(), out decimal montoApertura))
                    {
                        decimal saldo = totalEfectivo - montoApertura - totalCambio;
                        txtEntregar.Text = saldo.ToString("N2");
                    }
                    else
                    {
                        txtEntregar.Text = "0.00";
                        mensaje.mensajeInformacion("Monto de apertura inválido.");
                    }
                }
            //}
            //catch (Exception)
            //{
            //    mensaje.mensajeError("Error al sumar los totales.");
            //}
        }

        private void BloquearControles()
        {
            txtEfectivo.ReadOnly = true;
            txtTarjeta.ReadOnly = true;
            txtEntregar.ReadOnly = true;
            txtObservacion.ReadOnly = true;
            txtCodigo.ReadOnly = true;
            btnAceptar.Enabled = false;
        }

        private void imprimirVoucherCierreCaja(object sender, PrintPageEventArgs e)
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
                float etiquetaX = 60f;
                float precioX = 140f;
                float columnaEtiquetaAncho = 80f;
                float anchoVoucher = 280f;

                // === ENCABEZADO ===
                RectangleF rect = new RectangleF(0, y, anchoVoucher, lineHeight);
                g.DrawString("FARMACIA LA PREFERIDA", fontTitulo, Brushes.Black, rect, formatoCentro); y += lineHeight;

                rect = new RectangleF(0, y, anchoVoucher, lineHeight);
                g.DrawString("CIERRE DE CAJA " + txtCaja.Text.Trim(), fontNegrita, Brushes.Black, rect, formatoCentro); y += lineHeight + 10;

                g.DrawString("Fecha Cierre: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fontNormal, Brushes.Black, 10, y); y += lineHeight + 5;

                g.DrawString(new string('-', 40), fontNormal, Brushes.Black, 0, y); y += lineHeight;

                // === DETALLE DE CAJA ===
                g.DrawString("Monto Apertura:", fontNormal, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(txtApertura.Text.PadLeft(17), fontNormal, Brushes.Black, precioX, y); y += lineHeight + 2;

                g.DrawString("Pagos en Efectivo:", fontNormal, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(txtEfectivo.Text.PadLeft(17), fontNormal, Brushes.Black, precioX, y); y += lineHeight + 2;

                g.DrawString("Pagos con Tarjeta:", fontNormal, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(txtTarjeta.Text.PadLeft(17), fontNormal, Brushes.Black, precioX, y); y += lineHeight + 2;

                g.DrawString("Total Cambio:", fontNormal, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(txtCambio.Text.PadLeft(17), fontNormal, Brushes.Black, precioX, y); y += lineHeight + 2;

                g.DrawString("Total Entregado:", fontNegrita, Brushes.Black, etiquetaX + columnaEtiquetaAncho, y, formatoDerecha);
                g.DrawString(txtEntregar.Text.PadLeft(17), fontNegrita, Brushes.Black, precioX, y); y += lineHeight + 10;

                // === OBSERVACIÓN ===
                g.DrawString("Observación:", fontNormal, Brushes.Black, 10, y); y += lineHeight;

                string textoObs = string.IsNullOrWhiteSpace(txtObservacion.Text) ? "Ninguna" : txtObservacion.Text;
                float margen = 10f;
                RectangleF rectObs = new RectangleF(margen, y, anchoVoucher - 2 * margen, 0);
                g.DrawString(textoObs, fontNormal, Brushes.Black, rectObs);
                SizeF tamObs = g.MeasureString(textoObs, fontNormal, (int)(anchoVoucher - 2 * margen));
                y += tamObs.Height + 5;

                g.DrawString(new string('-', 40), fontNormal, Brushes.Black, 0, y); y += lineHeight + 50;

                // === FIRMA DEL USUARIO ===
                g.DrawLine(Pens.Black, 40, y, anchoVoucher - 40, y); y += lineHeight;
                rect = new RectangleF(0, y, anchoVoucher, lineHeight);

                g.DrawString(Variables.usuario, fontNormal, Brushes.Black, rect, formatoCentro); y += lineHeight;
                rect = new RectangleF(0, y, anchoVoucher, lineHeight);
                g.DrawString("Firma del Cajero", fontNormal, Brushes.Black, rect, formatoCentro); y += lineHeight + 15;

                // === SUPERVISOR AUTORIZA ===
                g.DrawString("Autorizado por: ", fontNormal, Brushes.Black, 10, y); y += lineHeight;
                g.DrawString(txtSupervisor.Text, fontNormal, Brushes.Black, 10, y); y += lineHeight + 30;

                // === MENSAJE FINAL ===
                rect = new RectangleF(0, y, anchoVoucher, lineHeight);
                g.DrawString("¡Gracias por su esfuerzo!", fontNormal, Brushes.Black, rect, formatoCentro);
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al imprimir el voucher de cierre de caja.");
            }
        }


        #endregion

        #region Botones de comando

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //try
            //{
                errorIcono.Clear();

                oCaja caja = new oCaja()
                {
                    idCaja = Variables.idCaja,
                    totalEfectivo = Convert.ToDecimal(txtEfectivo.Text),
                    totalTarjeta = Convert.ToDecimal(txtTarjeta.Text),
                    totalCambio = Convert.ToDecimal(txtCambio.Text),
                    totalEntregado = Convert.ToDecimal(txtEntregar.Text),
                    codigo = txtCodigo.Text.Trim(),
                    observacion = txtObservacion.Text.Trim()
                };

                resultadoOperacion resultado;

                resultado = bCaja.CerrarCaja(caja);

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
                txtSupervisor.Text = resultado.supervisor;
                Variables.idCaja = resultado.idCaja;

                iconImprimir_Click(this, EventArgs.Empty);
                BloquearControles();
            //}

            //catch (Exception)
            //{
            //    mensaje.mensajeError("Error al cerrar la caja.");
            //}
        }

        private void iconImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument printDoc = new PrintDocument();

                // === Cálculo de altura dinámica (observación) ===
                int altoBase = 300; // Base estimada del voucher sin observación

                using (Graphics g = Graphics.FromImage(new Bitmap(1, 1)))
                {
                    Font fontNormal = new Font("Consolas", 9);
                    float anchoTexto = 260; // Ancho real del contenido impreso
                    string observacion = string.IsNullOrWhiteSpace(txtObservacion.Text) ? "Ninguna" : txtObservacion.Text;

                    SizeF sizeObs = g.MeasureString(observacion, fontNormal, (int)anchoTexto);

                    // Añadir el alto de la observación y un margen
                    altoBase += (int)Math.Ceiling(sizeObs.Height) + 170;
                }

                // Configurar tamaño de papel personalizado
                PaperSize papel = new PaperSize("Voucher", 280, altoBase);
                printDoc.DefaultPageSettings.PaperSize = papel;

                // Asignar el evento que genera el contenido del voucher
                printDoc.PrintPage += imprimirVoucherCierreCaja;

                // Mostrar vista previa centrada
                PrintPreviewDialog preview = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 400,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterScreen
                };

                // Zoom automático
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
                mensaje.mensajeError("Error al mostrar la vista previa del cierre de caja.");
            }
        }

        #endregion

        #region Eventos de Cajas de texto
        private void frmCerrarCaja_Load(object sender, EventArgs e)
        {
            buscarCaja();
        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidades.validarDecimal(sender, e);
        }

        private void txtEfectivo_Leave(object sender, EventArgs e)
        {
            Utilidades.formatoDecimal((Control)sender);
        }

        private void txtTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidades.validarDecimal(sender, e);
        }

        private void txtTarjeta_Leave(object sender, EventArgs e)
        {
            Utilidades.formatoDecimal((Control)sender);
        }

        #endregion

      
    }
}
