using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacia.BLL;
using Sistema.UI.FormularioBase;
using Sistema.UI.Modulos;

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
        private void listarLaboratorios()
        {
            try
            {
                dgvListado.DataSource = bLaboratorio.listarLaboratorios();
                if (dgvListado.Rows.Count > 0)
                {
                    btnEditar.Enabled = true;
                    btnEliminar.Enabled = true;
                    txtBuscar.Enabled = true;
                }
                else
                {
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                    txtBuscar.Enabled = false;
                }

                dgvListado.Columns[0].Visible = false;
                dgvListado.Columns[1].Width = 450;
                dgvListado.Columns[1].Width = 350;
                dgvListado.Columns[1].Width = 200;
                dgvListado.Columns[1].Width = 350;
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
                if(mensajes.mensajeConfirmacion("¿Seguro que desea eliminar este registro?") == DialogResult.OK)
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

        #region eventos del formulario
        private void FrmLab_Load(object sender, EventArgs e)
        {
            listarLaboratorios();
        }
        #endregion

        #region botones de comando
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FrmAgregarLab frm = new FrmAgregarLab();
            frm.registroAgregado += listarLaboratorios;
            mostrarModal.MostrarConCapaTransparente(this, frm);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(dgvListado.CurrentRow != null)
            {
                seleccionarRegistro(dgvListado.CurrentRow.Index);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvListado.CurrentRow != null)
            {
                eliminarRegistro(dgvListado.CurrentRow.Index);
            }
        }
        #endregion

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region cajas de texto
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvListado.DataSource = bLaboratorio.buscarLaboratorios(txtBuscar.Text.Trim());
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
                mensajes.mensajeError("Error al buscar el registro.");
            }
        }
        #endregion
    }
}
