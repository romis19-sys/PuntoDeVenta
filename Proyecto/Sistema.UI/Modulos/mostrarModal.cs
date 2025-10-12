using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UI.Modulos
{
    public static class mostrarModal
    {
        public static void MostrarConCapaTransparente(Form mdiForm, Form modalForm)
        {
            Form overlay = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.Black,
                Opacity = 0.3,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                Location = mdiForm.PointToScreen(Point.Empty),
                Size = mdiForm.ClientSize,
                TopMost = false,
                Owner = mdiForm
            };

            overlay.Show();
            modalForm.ShowInTaskbar = false;
            modalForm.ShowDialog();
            overlay.Close();
        }
    }
}
