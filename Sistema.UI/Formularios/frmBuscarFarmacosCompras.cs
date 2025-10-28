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

namespace Sistema.UI.Formularios
{
    public partial class frmBuscarFarmacosCompras : Form
    {
        Mensajes mensajes = new Mensajes();
        public frmBuscarFarmacosCompras()
        {
            InitializeComponent();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
