using Farmacia.BLL;
using Farmacia.DAL;
using Farmacia.Entity;
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
    public partial class MDIMenu : Form
    {
        public static bool confirmarSalir = false;
        private Mensajes mensaje = new Mensajes();
        private Form formularioActivo = null;
        public MDIMenu()
        {
            InitializeComponent();
        }


        DatosConexion datos = GestorConexion.CargarDatosConexion();

        #region Metodos
        private void centrarEtiquetas()
        {
            try
            {
                var anchoContenedor = panelContenedor.ClientSize.Width;
                var altoContenedor = panelContenedor.ClientSize.Height;

                int espacioEntreLasEtiquetas = 10;
                //int altoTotal = lblTitulo.Height + espacioEntreLasEtiquetas + lblEmpresa.Height;

                //int topInicial = (altoContenedor - altoTotal) / 2;

                ////lblTitulo.Top = topInicial;
                ////lblTitulo.Left = (anchoContenedor - lblTitulo.Width) / 2;

                //lblEmpresa.Top = lblTitulo.Bottom + espacioEntreLasEtiquetas;
                //lblEmpresa.Left = (anchoContenedor - lblEmpresa.Width) / 2;
            }
            catch (Exception ex)
            {

            }
        }

        private void AbrirFormulario(Form formularioHijo, bool esHijoDelPanelContenedor = true)
        {
            try
            {
               
                // si es formulario debe de ser hijo del panelContenedor
                if (esHijoDelPanelContenedor)
                {
                    // si hay formulario activo lo cerramos
                    if (formularioActivo != null)
                    {
                        formularioActivo.Close();
                    }

                    // configuramos el nuevo formulario como hijo
                    formularioActivo = formularioHijo;
                    formularioHijo.TopLevel = false;
                    formularioHijo.FormBorderStyle = FormBorderStyle.None;
                    formularioHijo.Dock = DockStyle.Fill;

                    // añadimos el formulario al panel contenedor
                    panelContenedor.Controls.Add(formularioHijo);
                    panelContenedor.Tag = formularioHijo;
                    formularioHijo.BringToFront();

                    formularioHijo.Show();
                }
                else
                {
                    // si no debe ser hijo lo configuramos como un formario independiennte
                    formularioHijo.TopLevel = true;
                    formularioHijo.FormBorderStyle = FormBorderStyle.Sizable; // permite redimencionar

                    formularioHijo.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                mensaje.mensajeError("Se ha generado un error inesperado al cargar el formulario");
            }
        }

        #endregion

        #region Eventos del formulario

        private void MDIMenu_Resize(object sender, EventArgs e)
        {
            centrarEtiquetas();
        }

        #endregion

        private void MDIMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!confirmarSalir)
            {
                if(mensaje.mensajeConfirmacion("¿Seguro que desea salir del sistema?") == DialogResult.OK)
                {
                    confirmarSalir = true;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmPlantilla(), true);
        }

        private void listadoDeLaboratoriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmLab(), true);
        }

        private void administradorDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmUsuario(), true);
        }

        private void listadoDePresentacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmPresentacion(), true);
        }

        private void iconSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
