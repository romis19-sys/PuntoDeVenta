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

namespace Sistema.UI.Formularios
{
    public partial class frmPresentacion : Form 
    {
        private Mensajes mensaje = new Mensajes();
        public frmPresentacion()
        {
            InitializeComponent();
        }

        #region Métodos

        private void listarPresentacion()
        {
            try
            {
                dgvListado.DataSource = bPresentacion.listarPresentacion();
                if (dgvListado.Rows.Count > 0)
                {
                    btnEditar.Enabled = true;
                    btnEliminar.Enabled = true;
                    //btnBuscar.Enabled = true;
                }
                else
                {
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                    //btnBuscar.Enabled = false;
                }

                dgvListado.Columns[0].Visible = false;

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
                string Presentacion = dgvListado.Rows[filaSeleccionada].Cells["Presentacion"].Value?.ToString();
                //string descripcion = dgvListado.Rows[filaSeleccionada].Cells["DESCRIPCION"].Value?.ToString();

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

                    listarPresentacion();
                }
            }

            catch (Exception)
            {
                mensaje.mensajeError("Error al eliminar el registro.");
            }
        }
        #endregion

        #region Botonones de comando
        private void btnguardar_Click(object sender, EventArgs e)
        {
            frmAgregarPresentacion frm = new frmAgregarPresentacion();
            frm.registroAgregado += listarPresentacion;
            mostrarModal.MostrarConCapaTransparente(this, frm);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvListado.CurrentRow != null)
            {
                seleccionarRegistros(dgvListado.CurrentRow.Index);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvListado.CurrentRow != null)
            {
                EliminarRegistro(dgvListado.CurrentRow.Index);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }



        #endregion

       
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            frmAgregarPresentacion frm = new frmAgregarPresentacion();
            frm.registroAgregado += listarPresentacion;
            mostrarModal.MostrarConCapaTransparente(this, frm);
        }

        #region Eventos del formulario
        private void frmPresentacion_Load(object sender, EventArgs e)
        {
            listarPresentacion();
        }


        #endregion

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvListado.DataSource = bPresentacion.buscarPresentacion(txtBuscar.Text.Trim());
                if (dgvListado.Rows.Count > 0)
                {
                    btnEditar.Enabled = true;
                    btnEliminar.Enabled = true;
                }
                else
                {
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al buscar registros.");
            }
        }
    }
}
