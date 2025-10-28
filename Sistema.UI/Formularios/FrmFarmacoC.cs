using Farmacia.BLL;
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

namespace Sistema.UI.Formularios
{
    public partial class FrmFarmacoC : Form
    {
        private Mensajes mensaje = new Mensajes();
        //esto lo agregue para lo de las paginas
        private int paginaActual = 1;
        private int registrosPorPagina = 5;
        private int totalPaginas = 1;
        private DataTable datosCompletos;
        public FrmFarmacoC()
        {
            InitializeComponent();
        }

        #region Metodos
        private void ajustarColumnas()
        {
            try
            {
                if (dgvListado.Columns.Count == 0)
                    return;

                dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                int anchoCodigo = 250;
                int anchoNombre = 300;
                int anchoLaboratorio = 300;
                int anchoStock = 100;
                int anchoCaducidad = 250; // <-- asignamos un ancho fijo
                int anchoEditar = 50;
                int anchoEliminar = 50;

                int anchoDisponible = dgvListado.ClientSize.Width;
                int anchoDescripcion = anchoDisponible -
                        (anchoCodigo + anchoNombre + anchoLaboratorio + anchoStock + anchoCaducidad + anchoEditar + anchoEliminar + 20);

                dgvListado.Columns["CODIGO"].Width = anchoCodigo;
                dgvListado.Columns["FARMACO"].Width = anchoNombre;
                dgvListado.Columns["LABORATORIO"].Width = anchoLaboratorio;
                dgvListado.Columns["STOCK"].Width = anchoStock;
                dgvListado.Columns["DESCRIPCION"].Width = anchoDescripcion;
                dgvListado.Columns["Editar"].Width = anchoEditar;
                dgvListado.Columns["Eliminar"].Width = anchoEliminar;

                dgvListado.Refresh();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al ajustar las columnas.");
            }
        }
        private void MostrarPagina(int numeroPagina)
        {
            if (datosCompletos == null || datosCompletos.Rows.Count == 0)
                return;

            int inicio = (numeroPagina - 1) * registrosPorPagina;
            int fin = Math.Min(inicio + registrosPorPagina, datosCompletos.Rows.Count);

            DataTable pagina = datosCompletos.Clone(); // Estructura sin datos

            for (int i = inicio; i < fin; i++)
            {
                pagina.ImportRow(datosCompletos.Rows[i]);
            }

            dgvListado.DataSource = pagina;

            lblPaginacion.Text = $"Página {paginaActual} de {totalPaginas}";
        }
        private void listarFarmacos()
        {


            try
            {
                dgvListado.AutoGenerateColumns = false;
                datosCompletos = bFarmaco.listarTodosFarmacos(); // Todos los registros

                if (datosCompletos != null && datosCompletos.Rows.Count > 0)
                {
                    totalPaginas = (int)Math.Ceiling(datosCompletos.Rows.Count / (double)registrosPorPagina);
                    MostrarPagina(paginaActual);
                }
                else
                {
                    dgvListado.DataSource = null;
                }

                ajustarColumnas();
                txtBuscar.Focus();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al cargar registros.");
            }
            //try
            //{
            //    dgvListado.AutoGenerateColumns = false;
            //    dgvListado.DataSource = bFarmaco.listarTodosFarmacos();
            //    ajustarColumnas();

            //    txtBuscar.Focus();
            //}
            //catch (Exception)
            //{
            //    mensaje.mensajeError("Error al cargar los registros de fármacos.");
            //}
        }

        private void seleccionarRegistro(int fila)
        {
            try
            {
                int id = Convert.ToInt32(dgvListado.Rows[fila].Cells["ID"].Value);
                string codigo = dgvListado.Rows[fila].Cells["CODIGO"].Value?.ToString();
                string nombre = dgvListado.Rows[fila].Cells["FARMACO"].Value?.ToString();
                string laboratorio = dgvListado.Rows[fila].Cells["LABORATORIO"].Value?.ToString();
                string descripcion = dgvListado.Rows[fila].Cells["DESCRIPCION"].Value?.ToString();
                string presentacion = dgvListado.Rows[fila].Cells["PRESENTACION"].Value?.ToString();
                string tipoVenta = dgvListado.Rows[fila].Cells["TIPO_VENTA"].Value?.ToString();
                int stock = Convert.ToInt32(dgvListado.Rows[fila].Cells["STOCK"].Value);
                decimal pCompra = Convert.ToDecimal(dgvListado.Rows[fila].Cells["P_COMPRA"].Value);
                decimal pVenta = Convert.ToDecimal(dgvListado.Rows[fila].Cells["P_VENTA"].Value);

                DateTime? caducidad = null;
                var fecha = dgvListado.Rows[fila].Cells["CADUCIDAD"].Value;
                if (fecha != null && DateTime.TryParse(fecha.ToString(), out DateTime temp))
                {
                    caducidad = temp;
                }

                FrmAgregarFarmaco frm = new FrmAgregarFarmaco(
                    id, codigo, nombre, laboratorio, descripcion,
                    presentacion, tipoVenta, stock, pCompra, pVenta, caducidad
                );

                frm.registroAgregado += listarFarmacos;
                mostrarModal.MostrarConCapaTransparente(this, frm);
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al seleccionar el registro.");
            }
        }

        private void eliminarRegistro(int fila)
        {
            try
            {
                if (mensaje.mensajeConfirmacion("¿Seguro que desea eliminar este fármaco?") == DialogResult.OK)
                {
                    int id = Convert.ToInt32(dgvListado.Rows[fila].Cells["ID"].Value);
                    string resultado = bFarmaco.eliminarFarmaco(id);

                    if (resultado.Contains("éxito"))
                        mensaje.mensajeOk(resultado);
                    else
                        mensaje.mensajeInformacion(resultado);

                    listarFarmacos();
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al eliminar el registro.");
            }
        }

        #endregion

        #region Eventos del Formulario
        private void FrmFarmacoC_Load(object sender, EventArgs e)
        {
            listarFarmacos();
        }
        #endregion

        #region Botones de Comando
        private void iconAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarFarmaco frm = new FrmAgregarFarmaco();
            frm.registroAgregado += listarFarmacos;
            mostrarModal.MostrarConCapaTransparente(this, frm);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Cajas de Texto
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

            try
            {
                // Llenamos datosCompletos con el resultado de la búsqueda
                datosCompletos = bFarmaco.buscarTodosFarmaco(1, txtBuscar.Text.Trim());

                if (datosCompletos != null && datosCompletos.Rows.Count > 0)
                {
                    paginaActual = 1; // Reiniciamos a la primera página
                    totalPaginas = (int)Math.Ceiling(datosCompletos.Rows.Count / (double)registrosPorPagina);
                    MostrarPagina(paginaActual);
                }
                else
                {
                    dgvListado.DataSource = null;
                    lblPaginacion.Text = "Página 0 de 0";
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al buscar el registro.");
            }

            //try
            //{
            //    dgvListado.DataSource = bFarmaco.buscarFarmaco(1, txtBuscar.Text.Trim());
            //    if (dgvListado.Rows.Count > 0)
            //    {
            //        //btnEditar.Enabled = true;
            //        //btnEliminar.Enabled = true;
            //    }
            //    else
            //    {
            //        //btnEditar.Enabled = false;
            //        //btnEliminar.Enabled = false;
            //    }
            //}
            //catch (Exception)
            //{
            //    mensaje.mensajeError("Error al buscar el registro.");
            //}
        }

        #endregion

        #region Eventos del Data
        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (dgvListado.Columns[e.ColumnIndex].Name == "Editar")
                {
                    int fila = e.RowIndex;
                    string nombre = dgvListado.Rows[fila].Cells["FARMACO"].Value?.ToString();

                    if (MessageBox.Show($"¿Desea editar el fármaco {nombre}?", "Confirmar acción",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        seleccionarRegistro(fila);
                    }
                }

                if (dgvListado.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    eliminarRegistro(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en acción de tabla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListado_Resize(object sender, EventArgs e)
        {
            ajustarColumnas();
        }

        private void dgvListado_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == dgvListado.Columns["Editar"].Index)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var icon = Properties.Resources.icons8_actualizar_32;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - icon.Width) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - icon.Height) / 2;
                    e.Graphics.DrawImage(icon, new Rectangle(x, y, icon.Width, icon.Height));
                    e.Handled = true;
                }

                if (e.ColumnIndex == dgvListado.Columns["Eliminar"].Index)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var icon = Properties.Resources.icons8_eliminar_32__1_;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - icon.Width) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - icon.Height) / 2;
                    e.Graphics.DrawImage(icon, new Rectangle(x, y, icon.Width, icon.Height));
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al pintar iconos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                MostrarPagina(paginaActual);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {

            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                MostrarPagina(paginaActual);
            }
        }
    }
}
