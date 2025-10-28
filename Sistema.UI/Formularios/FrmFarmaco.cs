using Farmacia.BLL;
using Farmacia.Entity;
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
using System.Windows.Media.Animation;

namespace Sistema.UI.Formularios
{
    public partial class FrmFarmaco : Form
    {
        private Mensajes mensaje = new Mensajes();
        public FrmFarmaco()
        {
            InitializeComponent();
        }      

        #region metodos
        private void listarTodosFarmacos()
        {
            try
            {
                dgvListado.DataSource = bFarmaco.listarTodosFarmacos();
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
                dgvListado.Columns[3].Visible = false;
                dgvListado.Columns[4].Visible = false;
                //dgvListado.Columns[9].Visible = false; //este descripcion
                //dgvListado.Columns[10].Visible = false; //esto es caducidad
               





                txtBuscar.Focus();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al cargar registros.");
            }
        }

        private void seleccionarRegistros(int filaSeleccionada)
        {
            //try
            //{
                int id = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["ID"].Value);
                string codigo = dgvListado.Rows[filaSeleccionada].Cells["CODIGO"].Value?.ToString();
                string nombre = dgvListado.Rows[filaSeleccionada].Cells["FARMACO"].Value?.ToString();
                string laboratorio = dgvListado.Rows[filaSeleccionada].Cells["LABORATORIO"].Value?.ToString();
                string descripcion = dgvListado.Rows[filaSeleccionada].Cells["DESCRIPCION"].Value?.ToString();
                string presentacion = dgvListado.Rows[filaSeleccionada].Cells["PRESENTACION"].Value?.ToString();
                string tipoVenta = dgvListado.Rows[filaSeleccionada].Cells["TIPO_VENTA"].Value?.ToString();
                int stock = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["STOCK"].Value);
                decimal precioCompra = Convert.ToDecimal(dgvListado.Rows[filaSeleccionada].Cells["P_COMPRA"].Value); // <-- agregado
                decimal precioVenta = Convert.ToDecimal(dgvListado.Rows[filaSeleccionada].Cells["P_VENTA"].Value);

                DateTime? fechaCaducacion = null;
                var fecha = dgvListado.Rows[filaSeleccionada].Cells["CADUCIDAD"].Value;
                if (fecha != null && DateTime.TryParse(fecha.ToString(), out DateTime tempFecha))
                {
                    fechaCaducacion = tempFecha;
                }

                FrmAgregarFarmaco frm = new FrmAgregarFarmaco(
                    id,
                    codigo,
                    nombre,
                    laboratorio,
                    descripcion,
                    presentacion,
                    tipoVenta,
                    stock,
                    precioCompra, // <-- agregado al constructor
                    precioVenta,
                    fechaCaducacion
                );

                frm.registroAgregado += listarTodosFarmacos;
                mostrarModal.MostrarConCapaTransparente(this, frm);
            //}
            //catch (Exception)
            //{
            //    mensaje.mensajeError("Error al seleccionar el registro.");
            //}
        }

        private void EliminarRegistro(int filaSeleccionada)
        {
            try
            {
                if (mensaje.mensajeConfirmacion("Seguro que desea eliminar el registro?") == DialogResult.OK)
                {
                    int id = Convert.ToInt32(dgvListado.Rows[filaSeleccionada].Cells["ID"].Value);
                    string resultado = bFarmaco.eliminarFarmaco(id);

                    if (resultado.Contains("éxito"))
                    {
                        mensaje.mensajeOk(resultado);
                    }
                    else
                    {
                        mensaje.mensajeInformacion(resultado);
                    }
                    listarTodosFarmacos();
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al eliminar el registro");
            }
        }



        #endregion

        #region botones de comando
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            FrmAgregarFarmaco frm = new FrmAgregarFarmaco();
            frm.registroAgregado += listarTodosFarmacos;
            mostrarModal.MostrarConCapaTransparente(this, frm);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //if (dgvListado.CurrentRow != null)
            //{
                seleccionarRegistros(dgvListado.CurrentRow.Index);
            //}
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvListado.CurrentRow != null)
            {
                EliminarRegistro(dgvListado.CurrentRow.Index);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Eventos del formulario
        private void FrmFarmaco_Load(object sender, EventArgs e)
        {
            listarTodosFarmacos();
        }



        #endregion

        private void txtBuscarR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvListado.DataSource = bFarmaco.buscarFarmacoVenta(1, txtBuscar.Text.Trim());
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
