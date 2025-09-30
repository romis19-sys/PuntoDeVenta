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
    public partial class FrmMensajes : Form
    {
        public FrmMensajes(string mensaje, string tipo)
        {
            InitializeComponent();
            this.Opacity = 1;
            timer1.Interval = 150;


            lblMensaje.Text = mensaje;

            switch(tipo.ToLower())
            {
                case "error":
                    picIcono.Image = Properties.Resources.icon_error_50;
                    this.BackColor = Color.FromArgb(220, 53, 69); // rojo
                    break;

                case "ok":
                    picIcono.Image = Properties.Resources.icon_ok_50;
                    this.BackColor = Color.FromArgb(25, 135, 84); // verde
                    break;

                case "info":
                    picIcono.Image = Properties.Resources.icon_info_50;
                    this.BackColor = Color.FromArgb(13, 110, 153); // azul
                    break;

                case "warning":
                    picIcono.Image = Properties.Resources.icon_warning_50;
                    this.BackColor = Color.FromArgb(255, 152, 0); // naranja
                    break;

                case "confirmar":
                    picIcono.Image = Properties.Resources.icon_confirm_50;
                    this.BackColor = Color.FromArgb(108, 117, 125); // gris
                    break;
            }

        }
        #region Metodos
        private void centrarFormulario()
        {
            Rectangle screeanArea = Screen.PrimaryScreen.WorkingArea;

            int margenDerecho = 10;

            // posición x: pegado al borde derecho
            int posX = screeanArea.Right - this.Width - margenDerecho;

            // posicion y: centrado verticalmente
            int posY = screeanArea.Top + (screeanArea.Height - this.Height) / 2;

            this.Location = new Point(posX, posY);
        }
        #endregion

        #region Botonos de comando
        private void iconAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void iconCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        private void FrmMensajes_Load(object sender, EventArgs e)
        {
            centrarFormulario();
            timer1.Start();

            if(!iconCancelar.Visible)
            {
                iconAceptar.Location = new Point(199, 85);
            }
            else
            {
                iconAceptar.Location = new Point(60, 85);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.02;

            if(this.Opacity <= 0 )
            {
                timer1.Stop();
                this.Close();
            }
        }
    }
}
