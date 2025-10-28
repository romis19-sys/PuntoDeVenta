using Sistema.BLL;
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
    public partial class frmHistorialCierreCaja : Form
    {
        private Mensajes mensaje = new Mensajes();
        public frmHistorialCierreCaja()
        {
            InitializeComponent();
        }

        #region Métodos

        private void ListarCajas()
        {
            try
            {
                dgvListado.DataSource = bCaja.ListarCaja();
                if (dgvListado.Rows.Count > 0)
                {
                    dtpFechaInicio.Focus();
                }

                dgvListado.Columns[0].Visible = false;

                // Ajustar el tamaño de las columnas al contenido
                dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvListado.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

                dgvListado.Columns["INICIA"].DefaultCellStyle.Format = "N2";
                dgvListado.Columns["EFECTIVO"].DefaultCellStyle.Format = "N2";
                dgvListado.Columns["TARJETA"].DefaultCellStyle.Format = "N2";
                dgvListado.Columns["CAMBIO"].DefaultCellStyle.Format = "N2";
                dgvListado.Columns["ENTREGADO"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al cargar registros.");
            }
        }

        #endregion

        #region Eventos de formulario

        private void frmHistorialCierrreCaja_Load(object sender, EventArgs e)
        {
            ListarCajas();
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFinal.Value = DateTime.Now;
        }




        #endregion

        #region Botones de comando

        private void iconBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFinal = dtpFechaFinal.Value.Date;

                if (fechaInicio > fechaFinal)
                {
                    mensaje.mensajeValidacion("La fecha inicial no puede ser mayor que la fecha final.");
                    dtpFechaInicio.Focus();
                    return;
                }

                dgvListado.DataSource = bCaja.buscarCajaPorFecha(fechaInicio, fechaFinal);
                if (dgvListado.Rows.Count == 0)
                {
                    mensaje.mensajeInformacion("No existe ningún registro para la consulta solicitada.");
                    dtpFechaInicio.Focus();
                    return;
                }
            }

            catch (Exception)
            {
                mensaje.mensajeError("Error al buscar los registros.");
            }
        }

        private void iconActualizar_Click_1(object sender, EventArgs e)
        {
            ListarCajas();
        }

        private void iconCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


    }
}
