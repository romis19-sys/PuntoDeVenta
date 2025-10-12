using Farmacia.BLL;
using Sistema.UI.FormularioBase;
using Sistema.UI.Modulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Sistema.UI.Formularios
{
    public partial class frmPresentacion : Form 
    {
        private Mensajes mensaje = new Mensajes();
        public frmPresentacion()
        {
            InitializeComponent();
        }

        #region Método

        private void ajustarColumnas()
        {
            try
            {
                if (dgvListado.Columns.Count == 0)
                    return;
                // no se que dice pero miente
                dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                // le damos el ancho a las columnas que pusimos en el data
                int anchoEditar = 50;
                int anchoEliminar = 50;

                // sacamos el ancho disponible dentro del data
                int anchoDisponible = dgvListado.ClientSize.Width;
                int ancho = anchoDisponible - (anchoEditar + anchoEliminar + 20);

                // asiganamos los anchos a las columnas del data
                dgvListado.Columns["Nombre"].Width = ancho;
                dgvListado.Columns["Editar"].Width = anchoEditar;
                dgvListado.Columns["Eliminar"].Width = anchoEliminar;

                // refrescamos para que, no se, pero sirve :v
                dgvListado.Refresh();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error en el sistema.");
            }
        }
        private void listarPresentacion()
        {
            try
            {
                dgvListado.AutoGenerateColumns = false;
                dgvListado.DataSource = bPresentacion.listarPresentacion();
                ajustarColumnas();

                txtBuscar.Focus();
            }

            catch (Exception)
            {
                mensaje.mensajeError("Error al cargar registros.");
            }
        }

        private void seleccionarRegistros(int filaSeleccionada)
        {
            try
            {
                int id = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["ID"].Value);
                string Presentacion = dgvListado.Rows[filaSeleccionada].Cells["Nombre"].Value?.ToString();

                frmAgregarPresentacion frm = new frmAgregarPresentacion(id, Presentacion);
                frm.registroAgregado += listarPresentacion;
                mostrarModal.MostrarConCapaTransparente(this, frm);
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al seleccionar el registro.");
            }
        }

        private void EliminarRegistro(int filaSeleccionada)
        {
            try
            {
                if (mensaje.mensajeConfirmacion("¿Seguro que desea eliminar el registro?") == DialogResult.OK)
                {
                    int id = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["ID"].Value);
                    string resultado = bPresentacion.eliminarPresentacion(id);

                    if (resultado.Contains("éxito"))
                    {
                        mensaje.mensajeOk(resultado);
                    }
                    else
                    {
                        mensaje.mensajeInformacion(resultado);
                    }
                }
            }

            catch (Exception)
            {
                mensaje.mensajeError("Error al eliminar el registro.");
            }
        }
        #endregion

        #region Botonones de comando
        private void iconSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iconAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarPresentacion frm = new frmAgregarPresentacion();
            frm.registroAgregado += listarPresentacion;
            mostrarModal.MostrarConCapaTransparente(this, frm);
        }

        #endregion

        #region Eventos del formulario
        private void frmPresentacion_Load(object sender, EventArgs e)
        {
            listarPresentacion();
            ajustarColumnas(); 
        }

        #endregion

        #region Eventos de las cajas de texto
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvListado.DataSource = bPresentacion.buscarPresentacion(txtBuscar.Text.Trim());
                if (dgvListado.Rows.Count > 0)
                {
                    //btnEditar.Enabled = true;
                    //btnEliminar.Enabled = true;
                }
                else
                {
                    //btnEditar.Enabled = false;
                    //btnEliminar.Enabled = false;
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al buscar registros.");
            }
        }
        #endregion

        #region Eventos del data
        private void dgvListado_Resize(object sender, EventArgs e)
        {
            ajustarColumnas();
        }

        private void dgvListado_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == 2)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.icons8_actualizar_32.Width;
                    var h = Properties.Resources.icons8_actualizar_32.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.icons8_actualizar_32, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }

                if (e.ColumnIndex == 3)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.icons8_eliminar_32__1_.Width;
                    var h = Properties.Resources.icons8_eliminar_32__1_.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.icons8_eliminar_32__1_, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Vista preeliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvListado.Columns[e.ColumnIndex].Name == "Editar")
                {
                    int indice = e.RowIndex;

                    if (indice >= 0)
                    {
                        //  le cambie el estado por el id porq es presentacion y no venta
                        int id = Convert.ToInt32(dgvListado.Rows[indice].Cells["ID"].Value);
                        string presentacion = dgvListado.Rows[indice].Cells["Nombre"].Value?.ToString();

                        // el id de la presentacion debe de existir
                        if (id > 0)
                        if (MessageBox.Show("¿Está seguro que desea editar el nombre de la Presentación: " + presentacion, "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // se llamada al metodo
                                seleccionarRegistros(indice);
                            }
                    }
                }

                if (dgvListado.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    int indice = e.RowIndex;

                    if (indice >= 0)
                    {
                        EliminarRegistro(dgvListado.CurrentRow.Index);
                    }
                }

                listarPresentacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Vista preeliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
