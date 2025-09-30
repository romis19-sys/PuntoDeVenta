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
    public partial class frmUsuario : FrmPlantilla
    {
        private Mensajes mensaje = new Mensajes();

        public frmUsuario()
        {
            InitializeComponent();
        }

        #region "Metodos"



        private void listarUsuarios()
        {
            dgvListado.DataSource = bUsuario.listarUsuarios();
            if (dgvListado.Rows.Count >0)
            {

                btnCerrar.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                txtBuscar.Enabled = true;

            }
            else
            {

                btnCerrar.Enabled = false;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                txtBuscar.Enabled = false;
            }
            dgvListado.Columns[0].Visible = false;
            dgvListado.Columns[1].Visible = false;
            dgvListado.Columns[7].Visible = false;

            txtBuscar.Focus();
        }


        private void seleccionarRegistros(int filaSeleccionada)
        {
            try
            {
                int id = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["ID"].Value);
                int idRol = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["ROL"].Value);
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





        private void frmUsuario_Load(object sender, EventArgs e)
        {
            listarUsuarios();
        }

        #region botones
        private void btnNurvo_Click(object sender, EventArgs e)
        {
            frmAgregarUsuario frm = new frmAgregarUsuario();
            frm.registroAgregado += listarUsuarios;
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
        #endregion
        

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvListado.DataSource = bUsuario.buscarUsuarios(txtBuscar.Text.Trim());
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
