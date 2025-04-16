namespace ScannerIngreso
{
    partial class panelPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(panelPrincipal));
            this.LblHoraActual = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.TxtScanner = new System.Windows.Forms.TextBox();
            this.TiempoScaneo = new System.Windows.Forms.Timer(this.components);
            this.Timeout = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanelBtn = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnSalidaTurno = new System.Windows.Forms.Button();
            this.BtnSalidaAlmuerzo = new System.Windows.Forms.Button();
            this.BtnEntradaAlmuerzo = new System.Windows.Forms.Button();
            this.BtnEntradaTurno = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnCerrarSesion = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.LabelID = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tableLayoutPanelBtn.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // LblHoraActual
            // 
            this.LblHoraActual.AutoSize = true;
            this.LblHoraActual.BackColor = System.Drawing.Color.Transparent;
            this.LblHoraActual.Location = new System.Drawing.Point(107, 55);
            this.LblHoraActual.Name = "LblHoraActual";
            this.LblHoraActual.Size = new System.Drawing.Size(0, 16);
            this.LblHoraActual.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TxtScanner
            // 
            this.TxtScanner.Location = new System.Drawing.Point(506, 126);
            this.TxtScanner.Name = "TxtScanner";
            this.TxtScanner.Size = new System.Drawing.Size(382, 22);
            this.TxtScanner.TabIndex = 12;
            this.TxtScanner.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtScanner_KeyPress);
            // 
            // TiempoScaneo
            // 
            this.TiempoScaneo.Tick += new System.EventHandler(this.TiempoScaneo_Tick);
            // 
            // Timeout
            // 
            this.Timeout.Tick += new System.EventHandler(this.Timeout_Tick);
            // 
            // tableLayoutPanelBtn
            // 
            this.tableLayoutPanelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelBtn.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelBtn.ColumnCount = 3;
            this.tableLayoutPanelBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.19086F));
            this.tableLayoutPanelBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.6176F));
            this.tableLayoutPanelBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.19154F));
            this.tableLayoutPanelBtn.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tableLayoutPanelBtn.Location = new System.Drawing.Point(-7, 253);
            this.tableLayoutPanelBtn.Name = "tableLayoutPanelBtn";
            this.tableLayoutPanelBtn.RowCount = 3;
            this.tableLayoutPanelBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.488372F));
            this.tableLayoutPanelBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96.51163F));
            this.tableLayoutPanelBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanelBtn.Size = new System.Drawing.Size(1361, 447);
            this.tableLayoutPanelBtn.TabIndex = 77;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.BtnSalidaTurno, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.BtnSalidaAlmuerzo, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.BtnEntradaAlmuerzo, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.BtnEntradaTurno, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(277, 18);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(805, 409);
            this.tableLayoutPanel3.TabIndex = 72;
            // 
            // BtnSalidaTurno
            // 
            this.BtnSalidaTurno.BackgroundImage = global::ScannerIngreso.Properties.Resources.salida_de_turno__1_;
            this.BtnSalidaTurno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSalidaTurno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnSalidaTurno.Location = new System.Drawing.Point(405, 207);
            this.BtnSalidaTurno.Name = "BtnSalidaTurno";
            this.BtnSalidaTurno.Size = new System.Drawing.Size(397, 199);
            this.BtnSalidaTurno.TabIndex = 54;
            this.BtnSalidaTurno.UseVisualStyleBackColor = true;
            this.BtnSalidaTurno.Click += new System.EventHandler(this.BtnSalidaTurno_Click);
            // 
            // BtnSalidaAlmuerzo
            // 
            this.BtnSalidaAlmuerzo.BackgroundImage = global::ScannerIngreso.Properties.Resources.salida_almorzar__1_;
            this.BtnSalidaAlmuerzo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSalidaAlmuerzo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnSalidaAlmuerzo.Location = new System.Drawing.Point(3, 207);
            this.BtnSalidaAlmuerzo.Name = "BtnSalidaAlmuerzo";
            this.BtnSalidaAlmuerzo.Size = new System.Drawing.Size(396, 199);
            this.BtnSalidaAlmuerzo.TabIndex = 55;
            this.BtnSalidaAlmuerzo.UseVisualStyleBackColor = true;
            this.BtnSalidaAlmuerzo.Click += new System.EventHandler(this.BtnSalidaAlmuerzo_Click);
            // 
            // BtnEntradaAlmuerzo
            // 
            this.BtnEntradaAlmuerzo.BackgroundImage = global::ScannerIngreso.Properties.Resources.entrada_almuerzo__1_;
            this.BtnEntradaAlmuerzo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEntradaAlmuerzo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnEntradaAlmuerzo.Location = new System.Drawing.Point(405, 3);
            this.BtnEntradaAlmuerzo.Name = "BtnEntradaAlmuerzo";
            this.BtnEntradaAlmuerzo.Size = new System.Drawing.Size(397, 198);
            this.BtnEntradaAlmuerzo.TabIndex = 53;
            this.BtnEntradaAlmuerzo.UseVisualStyleBackColor = true;
            this.BtnEntradaAlmuerzo.Click += new System.EventHandler(this.BtnEntradaAlmuerzo_Click);
            // 
            // BtnEntradaTurno
            // 
            this.BtnEntradaTurno.BackgroundImage = global::ScannerIngreso.Properties.Resources.Entrada_de_turno__4_;
            this.BtnEntradaTurno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEntradaTurno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnEntradaTurno.Location = new System.Drawing.Point(3, 3);
            this.BtnEntradaTurno.Name = "BtnEntradaTurno";
            this.BtnEntradaTurno.Size = new System.Drawing.Size(396, 198);
            this.BtnEntradaTurno.TabIndex = 52;
            this.BtnEntradaTurno.UseVisualStyleBackColor = true;
            this.BtnEntradaTurno.Click += new System.EventHandler(this.BtnEntradaTurno_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Azure;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.368616F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9852F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.80903F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.88083F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.884384F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 4, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-7, -5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.71233F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.28767F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1364, 170);
            this.tableLayoutPanel1.TabIndex = 78;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::ScannerIngreso.Properties.Resources.logoClinica;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(35, 44);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(266, 106);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Californian FB", 16F);
            this.label1.Location = new System.Drawing.Point(307, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(783, 75);
            this.label1.TabIndex = 7;
            this.label1.Text = "MARCAR AUTOMATICAMENTE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.72414F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.82759F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.58621F));
            this.tableLayoutPanel2.Controls.Add(this.BtnCerrarSesion, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1244, 44);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.76238F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.48515F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.75248F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(117, 106);
            this.tableLayoutPanel2.TabIndex = 19;
            // 
            // BtnCerrarSesion
            // 
            this.BtnCerrarSesion.AutoSize = true;
            this.BtnCerrarSesion.BackColor = System.Drawing.Color.White;
            this.BtnCerrarSesion.BackgroundImage = global::ScannerIngreso.Properties.Resources.log_out_circle_regular_24;
            this.BtnCerrarSesion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnCerrarSesion.Location = new System.Drawing.Point(34, 28);
            this.BtnCerrarSesion.Name = "BtnCerrarSesion";
            this.BtnCerrarSesion.Size = new System.Drawing.Size(46, 48);
            this.BtnCerrarSesion.TabIndex = 14;
            this.BtnCerrarSesion.UseVisualStyleBackColor = false;
            this.BtnCerrarSesion.Click += new System.EventHandler(this.BtnCerrarSesion_Click_1);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel6.ColumnCount = 5;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.Controls.Add(this.LabelID, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblNombre, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblTipo, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblHora, 3, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblFecha, 4, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(-7, 163);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.17647F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.82353F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1355, 80);
            this.tableLayoutPanel6.TabIndex = 79;
            // 
            // LabelID
            // 
            this.LabelID.AutoSize = true;
            this.LabelID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelID.Font = new System.Drawing.Font("Californian FB", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.LabelID.Location = new System.Drawing.Point(4, 24);
            this.LabelID.Name = "LabelID";
            this.LabelID.Size = new System.Drawing.Size(263, 32);
            this.LabelID.TabIndex = 0;
            this.LabelID.Text = "labelId";
            this.LabelID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNombre
            // 
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombre.Font = new System.Drawing.Font("Californian FB", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblNombre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNombre.Location = new System.Drawing.Point(274, 24);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(263, 32);
            this.lblNombre.TabIndex = 16;
            this.lblNombre.Text = "labelNombre";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTipo
            // 
            this.lblTipo.BackColor = System.Drawing.Color.Transparent;
            this.lblTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTipo.Font = new System.Drawing.Font("Californian FB", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblTipo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTipo.Location = new System.Drawing.Point(544, 24);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(263, 32);
            this.lblTipo.TabIndex = 17;
            this.lblTipo.Text = "labelTipo";
            this.lblTipo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHora.Font = new System.Drawing.Font("Californian FB", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblHora.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblHora.Location = new System.Drawing.Point(814, 24);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(263, 32);
            this.lblHora.TabIndex = 12;
            this.lblHora.Text = "labelHora";
            this.lblHora.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFecha.Font = new System.Drawing.Font("Californian FB", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblFecha.Location = new System.Drawing.Point(1084, 24);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(267, 32);
            this.lblFecha.TabIndex = 18;
            this.lblFecha.Text = "labelFecha";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 312F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 863F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel12, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel15, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(-6, 697);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.68628F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.31373F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1358, 264);
            this.tableLayoutPanel5.TabIndex = 80;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 1;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.73991F));
            this.tableLayoutPanel12.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(178, 3);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.03773F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.96227F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(306, 85);
            this.tableLayoutPanel12.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Californian FB", 11F);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(300, 72);
            this.label3.TabIndex = 12;
            this.label3.Text = "Desarrollado por Juan David Montoya, CTA SENA.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 3;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.73991F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.26009F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel15.Controls.Add(this.pictureBox3, 2, 1);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(11, 3);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 2;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.03773F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.96227F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(161, 85);
            this.tableLayoutPanel15.TabIndex = 14;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Image = global::ScannerIngreso.Properties.Resources.logo_sena_verde_complementario_png_2022;
            this.pictureBox3.Location = new System.Drawing.Point(57, 16);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(101, 66);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // panelPrincipal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BackgroundImage = global::ScannerIngreso.Properties.Resources.fondo_principal__1_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1345, 953);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanelBtn);
            this.Controls.Add(this.TxtScanner);
            this.Controls.Add(this.LblHoraActual);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "panelPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel Principal";
            this.Load += new System.EventHandler(this.panelPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tableLayoutPanelBtn.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LblHoraActual;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TextBox TxtScanner;
        private System.Windows.Forms.Timer TiempoScaneo;
        private System.Windows.Forms.Timer Timeout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button BtnSalidaTurno;
        private System.Windows.Forms.Button BtnSalidaAlmuerzo;
        private System.Windows.Forms.Button BtnEntradaAlmuerzo;
        private System.Windows.Forms.Button BtnEntradaTurno;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button BtnCerrarSesion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label LabelID;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}