using Farmacia.BLL;
using Farmacia.DAL;
using Sistema.UI.FormularioBase;
using Sistema.UI.Modulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Sistema.UI.Formularios
{
    public partial class FrmLab : Form
    {
        private Mensajes mensajes = new Mensajes();
        public FrmLab()
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
                // no se que dice pero miente
                dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                // le damos el ancho a las columnas que pusimos en el data
                int anchoTelefono = 200;
                int anchoContacto = 250;
                int anchoEditar = 50;
                int anchoEliminar = 50;

                // sacamos el ancho disponible dentro del data
                int anchoDisponible = dgvListado.ClientSize.Width;
                int ancho = anchoDisponible - (anchoTelefono + anchoContacto + anchoEditar + anchoEliminar + 20);

                // asiganamos los anchos a las columnas del data
                dgvListado.Columns["Telefono"].Width = anchoTelefono;
                dgvListado.Columns["Contacto"].Width = anchoContacto;
                dgvListado.Columns["Editar"].Width = anchoEditar;
                dgvListado.Columns["Eliminar"].Width = anchoEliminar;
                dgvListado.Columns["Laboratorio"].Width = ancho;

                // refrescamos para que, no se, pero sirve :v
                dgvListado.Refresh();
            }
            catch (Exception)
            {
                mensajes.mensajeError("Ocurrió un error en el sistema.");
            }
        }
        private void listarLaboratorios()
        {
            try
            {
                dgvListado.AutoGenerateColumns = false;
                dgvListado.DataSource = bLaboratorio.listarLaboratorios();
                ajustarColumnas();

                txtBuscar.Focus();
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al cargar los registros");
            }
        }

        private void seleccionarRegistro (int filaSeleccionada)
        {
            try
            {
                int id = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["ID"].Value);
                string laboratorio = dgvListado.Rows[filaSeleccionada].Cells["Laboratorio"].Value?.ToString();
                string telefono = dgvListado.Rows[filaSeleccionada].Cells["Telefono"].Value?.ToString();
                string contacto = dgvListado.Rows[filaSeleccionada].Cells["Contacto"].Value?.ToString();

                FrmAgregarLab frm = new FrmAgregarLab(id, laboratorio, telefono, contacto);
                frm.registroAgregado += listarLaboratorios;
                mostrarModal.MostrarConCapaTransparente(this, frm);
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al seleccionar el registro.");
            }
        }

        private void eliminarRegistro(int filaSeleccionada)
        {
            try
            {
                if(mensajes.mensajeConfirmacion("¿Seguro que desea eliminar este Laboratorio?") == DialogResult.OK)
                {
                    int id = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["ID"].Value);
                    string resultado = bLaboratorio.eliminarLaboratorio(id);

                    if (resultado.Contains("éxito"))
                    {
                        mensajes.mensajeOk(resultado);
                    }
                    else
                    {
                        mensajes.mensajeInformacion(resultado);
                    }

                    listarLaboratorios();
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al eliminar el registro.");
            }
        }
        #endregion

        #region Eventos del Formulario
        private void FrmLab_Load(object sender, EventArgs e)
        {
            listarLaboratorios();
        }
        #endregion

        #region Botones de Comando
        private void iconSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iconAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarLab frm = new FrmAgregarLab();
            frm.registroAgregado += listarLaboratorios;
            mostrarModal.MostrarConCapaTransparente(this, frm);
        }
        #endregion

        #region Cajas de Texto
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvListado.DataSource = bLaboratorio.buscarLaboratorios(txtBuscar.Text.Trim());
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
                mensajes.mensajeError("Error al buscar el registro.");
            }
        }
        #endregion

        #region Eventos del Data
        private void dgvListado_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
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
                        string laboratorio = dgvListado.Rows[indice].Cells["Laboratorio"].Value?.ToString();
                        // el id de la presentacion debe de existir
                        if (id > 0)
                        if (MessageBox.Show("¿Está seguro que desea Editar el Laboratorio de Nombre: " + laboratorio, "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // se llamada al metodo
                                seleccionarRegistro(indice);
                            }
                    }
                }

                if (dgvListado.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    int indice = e.RowIndex;

                    if (indice >= 0)
                    {
                        eliminarRegistro(dgvListado.CurrentRow.Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Vista preeliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListado_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == 4)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.icons8_actualizar_32.Width;
                    var h = Properties.Resources.icons8_actualizar_32.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.icons8_actualizar_32, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }

                if (e.ColumnIndex == 5)
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

        private void dgvListado_Resize(object sender, EventArgs e)
        {
            ajustarColumnas();
        }
        #endregion
    }
}
