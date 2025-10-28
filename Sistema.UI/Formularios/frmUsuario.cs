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
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Sistema.UI.Formularios
{
    public partial class frmUsuario : Form
    {
        private Mensajes mensaje = new Mensajes();

        public frmUsuario()
        {
            InitializeComponent();
        }

        #region "Metodos"
        private void ajustarColumnas()
        {
            try
            {
                if (dgvListado.Columns.Count == 0)
                    return;

                // no se que dice pero miente
                dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                // le damos el ancho a las columnas que pusimos en el data
                int anchoRol = 200;
                int anchoNombre = 130;
                int anchoApellido = 150;
                int anchoTelefono = 150;
                int anchoEmail = 180;

                int anchoEditar = 50;
                int anchoEliminar = 50;

                // sacamos el ancho disponible dentro del data
                int anchoDisponible = dgvListado.ClientSize.Width;
                int ancho = anchoDisponible - (anchoRol + anchoNombre + anchoApellido + anchoTelefono + anchoEmail + anchoEditar + anchoEliminar + 20);

                // asiganamos los anchos a las columnas del data
                dgvListado.Columns["ROL"].Width = anchoRol;
                dgvListado.Columns["USUARIO"].Width = ancho;
                dgvListado.Columns["NOMBRE"].Width = anchoNombre;
                dgvListado.Columns["APELLIDO"].Width = anchoApellido;
                dgvListado.Columns["TELEFONO"].Width = anchoTelefono;
                dgvListado.Columns["EMAIL"].Width = anchoEmail;
                dgvListado.Columns["Eliminar"].Width = anchoEliminar;
                dgvListado.Columns["Editar"].Width = anchoEditar;

                // refrescamos para que, no se, pero sirve :v
                dgvListado.Refresh();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error en el sistema.");
            }
            //try
            //{
            //    if (dgvListado.Columns.Count == 0)
            //        return;
            //    dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //    // ancho disponible sin bordes ni scrollbars
            //    int anchoDisponible = dgvListado.ClientSize.Width;

            //    // anchos fijo para las columnas
            //    int anchoRol = 200;
            //    int anchoUsuario = 210;
            //    int anchoNombre = 130;
            //    int anchoApellido = 150;
            //    int anchoTelefono = 150;
            //    int anchoEmail= 180;

            //    // ancho para usuarios
            //    // int anchoUsuario = anchoDisponible - (anchoRol + anchoNombre + anchoApellido + anchoTelefono + anchoEmail + 20);

            //    // anchos fijos para los botones de eliminar y actualizar
            //    int anchoEliminar = 50;
            //    int anchoEditar = 50;

            //    // ancho para las columnas
            //    int ancho = anchoDisponible - (anchoEliminar - anchoEditar + 20);

            //    // asignar los anchos de las columnas
            //    dgvListado.Columns["ROL"].Width = anchoRol;
            //    dgvListado.Columns["USUARIO"].Width = anchoUsuario;
            //    dgvListado.Columns["NOMBRE"].Width = anchoNombre;
            //    dgvListado.Columns["APELLIDO"].Width = anchoApellido;
            //    dgvListado.Columns["TELEFONO"].Width = anchoTelefono;
            //    dgvListado.Columns["EMAIL"].Width = anchoEmail;

            //    // ancho para ver si los botones cambian de tamaño
            //    dgvListado.Columns["Editar"].Width = anchoEditar;
            //    dgvListado.Columns["Eliminar"].Width = anchoEliminar;
            //}
            //catch (Exception)
            //{
            //    mensaje.mensajeError("Ocurrió un error en el sistema.");
            //}
        }
        private void listarUsuarios()
        {
           try
            {
                dgvListado.AutoGenerateColumns = false;
                dgvListado.DataSource = bUsuario.listarUsuarios();
                ajustarColumnas();

                txtBuscar.Focus();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al listar los Usuarios.");
            }
        }
        private void seleccionarRegistros(int filaSeleccionada)
        {
            try
            {
                int id = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["ID"].Value);
                string idRol = dgvListado.Rows[filaSeleccionada].Cells["ROL"].Value?.ToString();
                string Usuario = dgvListado.Rows[filaSeleccionada].Cells["USUARIO"].Value?.ToString();
                string Nombre = dgvListado.Rows[filaSeleccionada].Cells["NOMBRE"].Value?.ToString();
                string Apellido = dgvListado.Rows[filaSeleccionada].Cells["APELLIDO"].Value?.ToString();
                string Telefono = dgvListado.Rows[filaSeleccionada].Cells["TELEFONO"].Value?.ToString();
                string Email = dgvListado.Rows[filaSeleccionada].Cells["EMAIL"].Value?.ToString();
                string Clave = dgvListado.Rows[filaSeleccionada].Cells["CLAVE"].Value?.ToString();

                frmAgregarUsuario frm = new frmAgregarUsuario(id,idRol,Usuario, Nombre, Apellido, Telefono, Email, Clave);
                frm.registroAgregado += listarUsuarios;
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
                    string resultado = bUsuario.eliminarUsuario(id);

                    if (resultado.Contains("éxito"))
                    {
                        mensaje.mensajeOk(resultado);
                    }
                    else
                    {
                        mensaje.mensajeInformacion(resultado);
                    }

                    listarUsuarios();
                }
            }

            catch (Exception)
            {
                mensaje.mensajeError("Error al eliminar el registro.");
            }
        }
        #endregion

        #region Eventos del Formulario
        private void frmUsuario_Load(object sender, EventArgs e)
        {
            listarUsuarios();
        }
        #endregion

        #region cajas de Texto
        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                dgvListado.DataSource = bUsuario.buscarUsuarios(txtBuscar.Text.Trim());
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

        #region Botones
        private void iconAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarUsuario frm = new frmAgregarUsuario();
            frm.registroAgregado += listarUsuarios;
            mostrarModal.MostrarConCapaTransparente(this, frm);
        }

        private void iconSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Eventos del Data
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
                        string usuario = dgvListado.Rows[indice].Cells["USUARIO"].Value?.ToString();

                        // el id de la presentacion debe de existir
                        if (id > 0)
                            if (MessageBox.Show("¿Está seguro que desea Editar al Usuario: " + usuario, "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void dgvListado_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == 8)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.icons8_actualizar_32.Width;
                    var h = Properties.Resources.icons8_actualizar_32.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.icons8_actualizar_32, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }

                if (e.ColumnIndex == 9)
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
        #endregion
    }
}
