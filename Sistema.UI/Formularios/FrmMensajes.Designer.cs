namespace Sistema.UI.Formularios
{
    partial class FrmMensajes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.picIcono = new System.Windows.Forms.PictureBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.iconAceptar = new System.Windows.Forms.Button();
            this.iconCancelar = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picIcono)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 15;
            this.guna2Elipse1.TargetControl = this;
            // 
            // picIcono
            // 
            this.picIcono.Location = new System.Drawing.Point(36, 25);
            this.picIcono.Name = "picIcono";
            this.picIcono.Size = new System.Drawing.Size(50, 50);
            this.picIcono.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picIcono.TabIndex = 0;
            this.picIcono.TabStop = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.Color.White;
            this.lblMensaje.Location = new System.Drawing.Point(105, 30);
            this.lblMensaje.MaximumSize = new System.Drawing.Size(270, 0);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(265, 56);
            this.lblMensaje.TabIndex = 1;
            this.lblMensaje.Text = "¿Seguro que desea eliminar el registro?";
            // 
            // iconAceptar
            // 
            this.iconAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconAceptar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconAceptar.ForeColor = System.Drawing.Color.White;
            this.iconAceptar.Location = new System.Drawing.Point(109, 105);
            this.iconAceptar.Name = "iconAceptar";
            this.iconAceptar.Size = new System.Drawing.Size(97, 44);
            this.iconAceptar.TabIndex = 2;
            this.iconAceptar.Text = "Aceptar";
            this.iconAceptar.UseVisualStyleBackColor = true;
            this.iconAceptar.Click += new System.EventHandler(this.iconAceptar_Click);
            // 
            // iconCancelar
            // 
            this.iconCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconCancelar.ForeColor = System.Drawing.Color.White;
            this.iconCancelar.Location = new System.Drawing.Point(265, 105);
            this.iconCancelar.Name = "iconCancelar";
            this.iconCancelar.Size = new System.Drawing.Size(97, 44);
            this.iconCancelar.TabIndex = 3;
            this.iconCancelar.Text = "Cancelar";
            this.iconCancelar.UseVisualStyleBackColor = true;
            this.iconCancelar.Click += new System.EventHandler(this.iconCancelar_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMensajes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.ClientSize = new System.Drawing.Size(393, 179);
            this.Controls.Add(this.iconCancelar);
            this.Controls.Add(this.iconAceptar);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.picIcono);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMensajes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMensajes";
            this.Load += new System.EventHandler(this.FrmMensajes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picIcono)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.PictureBox picIcono;
        private System.Windows.Forms.Button iconAceptar;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button iconCancelar;
        private System.Windows.Forms.Timer timer1;
    }
}