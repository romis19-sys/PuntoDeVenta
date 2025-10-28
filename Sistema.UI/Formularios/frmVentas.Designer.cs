namespace Sistema.UI.Formularios
{
    partial class frmVentas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Elipse4 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblImpuesto = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gunaCarrito = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dgvListadoDetalles = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSalir = new Guna.UI2.WinForms.Guna2Button();
            this.btnImprimer = new Guna.UI2.WinForms.Guna2Button();
            this.btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            this.btnNuevo = new Guna.UI2.WinForms.Guna2Button();
            this.txtBuscarProductos = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lblGrabaImpuesto = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblTipoVentas = new System.Windows.Forms.Label();
            this.lblprecio = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.s = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gunaProductos = new System.Windows.Forms.Panel();
            this.lblDescripcionValor = new System.Windows.Forms.Label();
            this.lblTipoVentaValor = new System.Windows.Forms.Label();
            this.lblcodigo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblLaboratorio = new System.Windows.Forms.Label();
            this.lblFarmacos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gunaCarrito.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListadoDetalles)).BeginInit();
            this.s.SuspendLayout();
            this.gunaProductos.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse4
            // 
            this.guna2Elipse4.BorderRadius = 20;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BackColor = System.Drawing.Color.Gray;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Black;
            this.lblTotal.Location = new System.Drawing.Point(219, 536);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(464, 64);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblImpuesto
            // 
            this.lblImpuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImpuesto.BackColor = System.Drawing.Color.Gray;
            this.lblImpuesto.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpuesto.ForeColor = System.Drawing.Color.Black;
            this.lblImpuesto.Location = new System.Drawing.Point(235, 467);
            this.lblImpuesto.Name = "lblImpuesto";
            this.lblImpuesto.Size = new System.Drawing.Size(448, 33);
            this.lblImpuesto.TabIndex = 13;
            this.lblImpuesto.Text = "0.00";
            this.lblImpuesto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtotal.BackColor = System.Drawing.Color.Gray;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotal.ForeColor = System.Drawing.Color.Black;
            this.lblSubtotal.Location = new System.Drawing.Point(224, 434);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(459, 33);
            this.lblSubtotal.TabIndex = 12;
            this.lblSubtotal.Text = "0.00";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Gray;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(30, 486);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 28);
            this.label11.TabIndex = 11;
            this.label11.Text = "Impuesto:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Gray;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(30, 436);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 28);
            this.label9.TabIndex = 10;
            this.label9.Text = "SubTotal:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Gray;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(18, 543);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 50);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // gunaCarrito
            // 
            this.gunaCarrito.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaCarrito.BorderRadius = 15;
            this.gunaCarrito.Controls.Add(this.lblTotal);
            this.gunaCarrito.Controls.Add(this.lblImpuesto);
            this.gunaCarrito.Controls.Add(this.lblSubtotal);
            this.gunaCarrito.Controls.Add(this.label11);
            this.gunaCarrito.Controls.Add(this.label9);
            this.gunaCarrito.Controls.Add(this.label4);
            this.gunaCarrito.Controls.Add(this.dgvListadoDetalles);
            this.gunaCarrito.CustomBorderColor = System.Drawing.Color.Transparent;
            this.gunaCarrito.FillColor = System.Drawing.Color.Gray;
            this.gunaCarrito.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaCarrito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gunaCarrito.Location = new System.Drawing.Point(507, 88);
            this.gunaCarrito.Name = "gunaCarrito";
            this.gunaCarrito.Size = new System.Drawing.Size(908, 621);
            this.gunaCarrito.TabIndex = 5;
            // 
            // dgvListadoDetalles
            // 
            this.dgvListadoDetalles.AllowUserToAddRows = false;
            this.dgvListadoDetalles.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvListadoDetalles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListadoDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListadoDetalles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvListadoDetalles.ColumnHeadersHeight = 35;
            this.dgvListadoDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListadoDetalles.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvListadoDetalles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvListadoDetalles.Location = new System.Drawing.Point(3, 9);
            this.dgvListadoDetalles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvListadoDetalles.Name = "dgvListadoDetalles";
            this.dgvListadoDetalles.RowHeadersVisible = false;
            this.dgvListadoDetalles.RowHeadersWidth = 51;
            this.dgvListadoDetalles.RowTemplate.Height = 35;
            this.dgvListadoDetalles.Size = new System.Drawing.Size(902, 412);
            this.dgvListadoDetalles.TabIndex = 3;
            this.dgvListadoDetalles.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvListadoDetalles.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvListadoDetalles.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvListadoDetalles.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvListadoDetalles.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvListadoDetalles.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvListadoDetalles.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvListadoDetalles.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvListadoDetalles.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvListadoDetalles.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListadoDetalles.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvListadoDetalles.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvListadoDetalles.ThemeStyle.HeaderStyle.Height = 35;
            this.dgvListadoDetalles.ThemeStyle.ReadOnly = false;
            this.dgvListadoDetalles.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvListadoDetalles.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvListadoDetalles.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListadoDetalles.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvListadoDetalles.ThemeStyle.RowsStyle.Height = 35;
            this.dgvListadoDetalles.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvListadoDetalles.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvListadoDetalles.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListadoDetalles_CellEndEdit);
            this.dgvListadoDetalles.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvListadoDetalles_CellValidating);
            this.dgvListadoDetalles.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvListadoDetalles_EditingControlShowing);
            this.dgvListadoDetalles.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvListadoDetalles_UserDeletedRow);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSalir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(161)))));
            this.btnSalir.BorderRadius = 8;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSalir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSalir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSalir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSalir.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(161)))));
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Image = global::Sistema.UI.Properties.Resources.icon_salida_48;
            this.btnSalir.ImageSize = new System.Drawing.Size(50, 50);
            this.btnSalir.Location = new System.Drawing.Point(1253, 11);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(70, 62);
            this.btnSalir.TabIndex = 69;
            this.toolTip1.SetToolTip(this.btnSalir, "Salir");
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnImprimer
            // 
            this.btnImprimer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnImprimer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(161)))));
            this.btnImprimer.BorderRadius = 8;
            this.btnImprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnImprimer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnImprimer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnImprimer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnImprimer.Enabled = false;
            this.btnImprimer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(161)))));
            this.btnImprimer.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimer.ForeColor = System.Drawing.Color.White;
            this.btnImprimer.Image = global::Sistema.UI.Properties.Resources.icon_print_48;
            this.btnImprimer.ImageSize = new System.Drawing.Size(50, 50);
            this.btnImprimer.Location = new System.Drawing.Point(1166, 11);
            this.btnImprimer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImprimer.Name = "btnImprimer";
            this.btnImprimer.Size = new System.Drawing.Size(70, 62);
            this.btnImprimer.TabIndex = 68;
            this.toolTip1.SetToolTip(this.btnImprimer, "Imprimir Factura");
            this.btnImprimer.Click += new System.EventHandler(this.btnImprimer_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGuardar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(161)))));
            this.btnGuardar.BorderRadius = 8;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGuardar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGuardar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGuardar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGuardar.Enabled = false;
            this.btnGuardar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(161)))));
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Image = global::Sistema.UI.Properties.Resources.icon_save_48;
            this.btnGuardar.ImageSize = new System.Drawing.Size(50, 50);
            this.btnGuardar.Location = new System.Drawing.Point(1078, 11);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(70, 62);
            this.btnGuardar.TabIndex = 67;
            this.toolTip1.SetToolTip(this.btnGuardar, "Guardar Venta");
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNuevo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(161)))));
            this.btnNuevo.BorderRadius = 8;
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNuevo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNuevo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNuevo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNuevo.Enabled = false;
            this.btnNuevo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(161)))));
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Image = global::Sistema.UI.Properties.Resources.icon_add_50;
            this.btnNuevo.ImageSize = new System.Drawing.Size(50, 50);
            this.btnNuevo.Location = new System.Drawing.Point(989, 11);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(70, 62);
            this.btnNuevo.TabIndex = 66;
            this.toolTip1.SetToolTip(this.btnNuevo, "Nueva pagina");
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // txtBuscarProductos
            // 
            this.txtBuscarProductos.BackColor = System.Drawing.Color.DarkGray;
            this.txtBuscarProductos.BorderColor = System.Drawing.Color.Transparent;
            this.txtBuscarProductos.BorderRadius = 20;
            this.txtBuscarProductos.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBuscarProductos.DefaultText = "";
            this.txtBuscarProductos.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBuscarProductos.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBuscarProductos.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBuscarProductos.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBuscarProductos.FillColor = System.Drawing.Color.Silver;
            this.txtBuscarProductos.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBuscarProductos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarProductos.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBuscarProductos.IconLeft = global::Sistema.UI.Properties.Resources.icons8_search_241;
            this.txtBuscarProductos.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.txtBuscarProductos.IconLeftSize = new System.Drawing.Size(25, 25);
            this.txtBuscarProductos.Location = new System.Drawing.Point(21, 48);
            this.txtBuscarProductos.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBuscarProductos.Name = "txtBuscarProductos";
            this.txtBuscarProductos.PlaceholderForeColor = System.Drawing.Color.White;
            this.txtBuscarProductos.PlaceholderText = "";
            this.txtBuscarProductos.SelectedText = "";
            this.txtBuscarProductos.Size = new System.Drawing.Size(450, 54);
            this.txtBuscarProductos.TabIndex = 1;
            this.txtBuscarProductos.Tag = "";
            this.toolTip1.SetToolTip(this.txtBuscarProductos, "Buscar por Nombre");
            this.txtBuscarProductos.IconLeftClick += new System.EventHandler(this.txtBuscarProductos_IconLeftClick);
            this.txtBuscarProductos.TextChanged += new System.EventHandler(this.txtBuscarProductos_TextChanged);
            this.txtBuscarProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarProductos_KeyDown);
            // 
            // guna2Elipse3
            // 
            this.guna2Elipse3.BorderRadius = 20;
            this.guna2Elipse3.TargetControl = this.dgvListadoDetalles;
            // 
            // lblGrabaImpuesto
            // 
            this.lblGrabaImpuesto.AutoSize = true;
            this.lblGrabaImpuesto.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrabaImpuesto.Location = new System.Drawing.Point(30, 599);
            this.lblGrabaImpuesto.Name = "lblGrabaImpuesto";
            this.lblGrabaImpuesto.Size = new System.Drawing.Size(22, 28);
            this.lblGrabaImpuesto.TabIndex = 11;
            this.lblGrabaImpuesto.Text = "E";
            this.lblGrabaImpuesto.Visible = false;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.Location = new System.Drawing.Point(30, 568);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(23, 28);
            this.lblId.TabIndex = 10;
            this.lblId.Text = "0";
            this.lblId.Visible = false;
            // 
            // lblTipoVentas
            // 
            this.lblTipoVentas.AutoSize = true;
            this.lblTipoVentas.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoVentas.Location = new System.Drawing.Point(31, 268);
            this.lblTipoVentas.Name = "lblTipoVentas";
            this.lblTipoVentas.Size = new System.Drawing.Size(144, 28);
            this.lblTipoVentas.TabIndex = 9;
            this.lblTipoVentas.Text = "Tipo de Venta:";
            this.lblTipoVentas.Visible = false;
            // 
            // lblprecio
            // 
            this.lblprecio.AutoSize = true;
            this.lblprecio.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprecio.ForeColor = System.Drawing.Color.Lime;
            this.lblprecio.Location = new System.Drawing.Point(173, 503);
            this.lblprecio.Name = "lblprecio";
            this.lblprecio.Size = new System.Drawing.Size(23, 28);
            this.lblprecio.TabIndex = 8;
            this.lblprecio.Text = "0";
            this.lblprecio.Visible = false;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStock.ForeColor = System.Drawing.Color.Lime;
            this.lblStock.Location = new System.Drawing.Point(178, 457);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(23, 28);
            this.lblStock.TabIndex = 7;
            this.lblStock.Text = "0";
            this.lblStock.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(30, 503);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 28);
            this.label6.TabIndex = 6;
            this.label6.Text = "Precio:";
            this.label6.Visible = false;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // s
            // 
            this.s.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(161)))));
            this.s.Controls.Add(this.btnSalir);
            this.s.Controls.Add(this.btnImprimer);
            this.s.Controls.Add(this.btnGuardar);
            this.s.Controls.Add(this.btnNuevo);
            this.s.Controls.Add(this.label1);
            this.s.Controls.Add(this.label8);
            this.s.Controls.Add(this.label7);
            this.s.Dock = System.Windows.Forms.DockStyle.Top;
            this.s.Location = new System.Drawing.Point(0, 0);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(1427, 82);
            this.s.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 64);
            this.label1.TabIndex = 65;
            this.label1.Text = "Punto de ventas:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.label8.Location = new System.Drawing.Point(464, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 64);
            this.label8.TabIndex = 64;
            this.label8.Text = "Farma";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(351, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 64);
            this.label7.TabIndex = 63;
            this.label7.Text = "Johar";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gunaProductos
            // 
            this.gunaProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gunaProductos.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gunaProductos.Controls.Add(this.lblDescripcionValor);
            this.gunaProductos.Controls.Add(this.lblTipoVentaValor);
            this.gunaProductos.Controls.Add(this.lblcodigo);
            this.gunaProductos.Controls.Add(this.label3);
            this.gunaProductos.Controls.Add(this.lblGrabaImpuesto);
            this.gunaProductos.Controls.Add(this.lblId);
            this.gunaProductos.Controls.Add(this.lblTipoVentas);
            this.gunaProductos.Controls.Add(this.lblprecio);
            this.gunaProductos.Controls.Add(this.lblStock);
            this.gunaProductos.Controls.Add(this.label6);
            this.gunaProductos.Controls.Add(this.label5);
            this.gunaProductos.Controls.Add(this.lblDescripcion);
            this.gunaProductos.Controls.Add(this.lblLaboratorio);
            this.gunaProductos.Controls.Add(this.lblFarmacos);
            this.gunaProductos.Controls.Add(this.txtBuscarProductos);
            this.gunaProductos.Controls.Add(this.label2);
            this.gunaProductos.Location = new System.Drawing.Point(0, 82);
            this.gunaProductos.Name = "gunaProductos";
            this.gunaProductos.Size = new System.Drawing.Size(501, 636);
            this.gunaProductos.TabIndex = 4;
            // 
            // lblDescripcionValor
            // 
            this.lblDescripcionValor.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionValor.Location = new System.Drawing.Point(178, 307);
            this.lblDescripcionValor.Name = "lblDescripcionValor";
            this.lblDescripcionValor.Size = new System.Drawing.Size(293, 130);
            this.lblDescripcionValor.TabIndex = 15;
            this.lblDescripcionValor.Text = "d";
            this.lblDescripcionValor.Visible = false;
            // 
            // lblTipoVentaValor
            // 
            this.lblTipoVentaValor.AutoSize = true;
            this.lblTipoVentaValor.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoVentaValor.Location = new System.Drawing.Point(173, 268);
            this.lblTipoVentaValor.Name = "lblTipoVentaValor";
            this.lblTipoVentaValor.Size = new System.Drawing.Size(22, 28);
            this.lblTipoVentaValor.TabIndex = 14;
            this.lblTipoVentaValor.Text = "v";
            this.lblTipoVentaValor.Visible = false;
            // 
            // lblcodigo
            // 
            this.lblcodigo.AutoSize = true;
            this.lblcodigo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcodigo.Location = new System.Drawing.Point(120, 185);
            this.lblcodigo.Name = "lblcodigo";
            this.lblcodigo.Size = new System.Drawing.Size(21, 28);
            this.lblcodigo.TabIndex = 13;
            this.lblcodigo.Text = "c";
            this.lblcodigo.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 28);
            this.label3.TabIndex = 12;
            this.label3.Text = "Codigo:";
            this.label3.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 457);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 28);
            this.label5.TabIndex = 5;
            this.label5.Text = "Stock:";
            this.label5.Visible = false;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Location = new System.Drawing.Point(31, 307);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(122, 28);
            this.lblDescripcion.TabIndex = 4;
            this.lblDescripcion.Text = "Descripcion:";
            this.lblDescripcion.Visible = false;
            // 
            // lblLaboratorio
            // 
            this.lblLaboratorio.AutoSize = true;
            this.lblLaboratorio.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLaboratorio.Location = new System.Drawing.Point(30, 226);
            this.lblLaboratorio.Name = "lblLaboratorio";
            this.lblLaboratorio.Size = new System.Drawing.Size(121, 28);
            this.lblLaboratorio.TabIndex = 3;
            this.lblLaboratorio.Text = "Laboratorio:";
            this.lblLaboratorio.Visible = false;
            // 
            // lblFarmacos
            // 
            this.lblFarmacos.AutoSize = true;
            this.lblFarmacos.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFarmacos.Location = new System.Drawing.Point(26, 135);
            this.lblFarmacos.Name = "lblFarmacos";
            this.lblFarmacos.Size = new System.Drawing.Size(170, 50);
            this.lblFarmacos.TabIndex = 2;
            this.lblFarmacos.Text = "Farmaco";
            this.lblFarmacos.Visible = false;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(501, 34);
            this.label2.TabIndex = 0;
            this.label2.Text = "Buscar Productos.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1427, 719);
            this.Controls.Add(this.gunaCarrito);
            this.Controls.Add(this.s);
            this.Controls.Add(this.gunaProductos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmVentas";
            this.Text = "frmVentas";
            this.Load += new System.EventHandler(this.frmVentas_Load_1);
            this.Shown += new System.EventHandler(this.frmVentas_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVentas_KeyDown);
            this.Resize += new System.EventHandler(this.frmVentas_Resize);
            this.gunaCarrito.ResumeLayout(false);
            this.gunaCarrito.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListadoDetalles)).EndInit();
            this.s.ResumeLayout(false);
            this.gunaProductos.ResumeLayout(false);
            this.gunaProductos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse4;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblImpuesto;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2GroupBox gunaCarrito;
        public Guna.UI2.WinForms.Guna2DataGridView dgvListadoDetalles;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private System.Windows.Forms.ToolTip toolTip1;
        public Guna.UI2.WinForms.Guna2Button btnSalir;
        public Guna.UI2.WinForms.Guna2Button btnImprimer;
        public Guna.UI2.WinForms.Guna2Button btnGuardar;
        public Guna.UI2.WinForms.Guna2Button btnNuevo;
        private Guna.UI2.WinForms.Guna2TextBox txtBuscarProductos;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
        private System.Windows.Forms.Label lblGrabaImpuesto;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblTipoVentas;
        private System.Windows.Forms.Label lblprecio;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Panel s;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel gunaProductos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblLaboratorio;
        private System.Windows.Forms.Label lblFarmacos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblcodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTipoVentaValor;
        private System.Windows.Forms.Label lblDescripcionValor;
    }
}