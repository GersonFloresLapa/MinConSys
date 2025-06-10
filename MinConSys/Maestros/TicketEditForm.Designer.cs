
namespace MinConSys.Maestros
{
    partial class TicketEditForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBuscarDeposito = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.cboDeposito = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cboRemitente = new System.Windows.Forms.ComboBox();
            this.cboChofer = new System.Windows.Forms.ComboBox();
            this.cboPlacaVehiculo = new System.Windows.Forms.ComboBox();
            this.cboPlacaTracto = new System.Windows.Forms.ComboBox();
            this.cboTransportista = new System.Windows.Forms.ComboBox();
            this.cboProcedencia = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cboEmpresa = new System.Windows.Forms.ComboBox();
            this.PesoAcreditado = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.PesoNeto = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.PesoTara = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.PesoBruto = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.dtpFechaPesoTara = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaPesoBruto = new System.Windows.Forms.DateTimePicker();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpFechaGuiaRem = new System.Windows.Forms.DateTimePicker();
            this.txtNroGuiaRemitente = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnBuscarProveedor = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNroGuiaTransporte = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtBrevete = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnBuscarChofer = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEstadoSunat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuscarTransportista = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscarProcedencia = new System.Windows.Forms.Button();
            this.cboBalanza = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.cboClase = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cboTipoOperacion = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNroTicket = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PesoAcreditado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PesoNeto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PesoTara)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PesoBruto)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGuardar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1354, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::MinConSys.Properties.Resources.Guardar;
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(108, 36);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnBuscarDeposito);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.cboDeposito);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.txtObservacion);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.cboRemitente);
            this.panel1.Controls.Add(this.cboChofer);
            this.panel1.Controls.Add(this.cboPlacaVehiculo);
            this.panel1.Controls.Add(this.cboPlacaTracto);
            this.panel1.Controls.Add(this.cboTransportista);
            this.panel1.Controls.Add(this.cboProcedencia);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.cboEmpresa);
            this.panel1.Controls.Add(this.PesoAcreditado);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.PesoNeto);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.PesoTara);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.PesoBruto);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.dtpFechaPesoTara);
            this.panel1.Controls.Add(this.dtpFechaPesoBruto);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.dtpFechaGuiaRem);
            this.panel1.Controls.Add(this.txtNroGuiaRemitente);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.btnBuscarProveedor);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.txtNroGuiaTransporte);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.txtBrevete);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.btnBuscarChofer);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtEstadoSunat);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnBuscarTransportista);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnBuscarProcedencia);
            this.panel1.Controls.Add(this.cboBalanza);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboProducto);
            this.panel1.Controls.Add(this.cboClase);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.cboTipoOperacion);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtNroTicket);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 39);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1354, 704);
            this.panel1.TabIndex = 2;
            // 
            // btnBuscarDeposito
            // 
            this.btnBuscarDeposito.Location = new System.Drawing.Point(813, 202);
            this.btnBuscarDeposito.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarDeposito.Name = "btnBuscarDeposito";
            this.btnBuscarDeposito.Size = new System.Drawing.Size(79, 28);
            this.btnBuscarDeposito.TabIndex = 99;
            this.btnBuscarDeposito.Text = "Buscar";
            this.btnBuscarDeposito.UseVisualStyleBackColor = true;
            this.btnBuscarDeposito.Click += new System.EventHandler(this.btnBuscarDeposito_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(524, 208);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(68, 17);
            this.label32.TabIndex = 98;
            this.label32.Text = "Depósito:";
            // 
            // cboDeposito
            // 
            this.cboDeposito.FormattingEnabled = true;
            this.cboDeposito.Location = new System.Drawing.Point(605, 202);
            this.cboDeposito.Margin = new System.Windows.Forms.Padding(4);
            this.cboDeposito.Name = "cboDeposito";
            this.cboDeposito.Size = new System.Drawing.Size(200, 24);
            this.cboDeposito.TabIndex = 97;
            this.cboDeposito.Tag = "requerido";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(43, 551);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(96, 17);
            this.label31.TabIndex = 96;
            this.label31.Text = "Observación :";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(147, 546);
            this.txtObservacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(465, 22);
            this.txtObservacion.TabIndex = 95;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(872, 17);
            this.label6.TabIndex = 94;
            this.label6.Text = "_________________________________________________________________________________" +
    "___________________________";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(872, 17);
            this.label5.TabIndex = 93;
            this.label5.Text = "_________________________________________________________________________________" +
    "___________________________";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 238);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(872, 17);
            this.label15.TabIndex = 92;
            this.label15.Text = "_________________________________________________________________________________" +
    "___________________________";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 425);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(872, 17);
            this.label17.TabIndex = 91;
            this.label17.Text = "_________________________________________________________________________________" +
    "___________________________";
            // 
            // cboRemitente
            // 
            this.cboRemitente.FormattingEnabled = true;
            this.cboRemitente.Location = new System.Drawing.Point(147, 459);
            this.cboRemitente.Margin = new System.Windows.Forms.Padding(4);
            this.cboRemitente.Name = "cboRemitente";
            this.cboRemitente.Size = new System.Drawing.Size(315, 24);
            this.cboRemitente.TabIndex = 90;
            this.cboRemitente.Tag = "requerido";
            // 
            // cboChofer
            // 
            this.cboChofer.FormattingEnabled = true;
            this.cboChofer.Location = new System.Drawing.Point(147, 356);
            this.cboChofer.Margin = new System.Windows.Forms.Padding(4);
            this.cboChofer.Name = "cboChofer";
            this.cboChofer.Size = new System.Drawing.Size(256, 24);
            this.cboChofer.TabIndex = 89;
            this.cboChofer.Tag = "";
            this.cboChofer.SelectedIndexChanged += new System.EventHandler(this.cboChofer_SelectedIndexChanged);
            // 
            // cboPlacaVehiculo
            // 
            this.cboPlacaVehiculo.FormattingEnabled = true;
            this.cboPlacaVehiculo.Location = new System.Drawing.Point(605, 314);
            this.cboPlacaVehiculo.Margin = new System.Windows.Forms.Padding(4);
            this.cboPlacaVehiculo.Name = "cboPlacaVehiculo";
            this.cboPlacaVehiculo.Size = new System.Drawing.Size(200, 24);
            this.cboPlacaVehiculo.TabIndex = 88;
            this.cboPlacaVehiculo.Tag = "";
            // 
            // cboPlacaTracto
            // 
            this.cboPlacaTracto.FormattingEnabled = true;
            this.cboPlacaTracto.Location = new System.Drawing.Point(147, 314);
            this.cboPlacaTracto.Margin = new System.Windows.Forms.Padding(4);
            this.cboPlacaTracto.Name = "cboPlacaTracto";
            this.cboPlacaTracto.Size = new System.Drawing.Size(256, 24);
            this.cboPlacaTracto.TabIndex = 87;
            this.cboPlacaTracto.Tag = "requerido";
            // 
            // cboTransportista
            // 
            this.cboTransportista.FormattingEnabled = true;
            this.cboTransportista.Location = new System.Drawing.Point(147, 267);
            this.cboTransportista.Margin = new System.Windows.Forms.Padding(4);
            this.cboTransportista.Name = "cboTransportista";
            this.cboTransportista.Size = new System.Drawing.Size(256, 24);
            this.cboTransportista.TabIndex = 86;
            this.cboTransportista.Tag = "requerido";
            this.cboTransportista.SelectedIndexChanged += new System.EventHandler(this.cboTransportista_SelectedIndexChanged);
            // 
            // cboProcedencia
            // 
            this.cboProcedencia.FormattingEnabled = true;
            this.cboProcedencia.Location = new System.Drawing.Point(147, 206);
            this.cboProcedencia.Margin = new System.Windows.Forms.Padding(4);
            this.cboProcedencia.Name = "cboProcedencia";
            this.cboProcedencia.Size = new System.Drawing.Size(256, 24);
            this.cboProcedencia.TabIndex = 85;
            this.cboProcedencia.Tag = "requerido";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(31, 68);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(68, 17);
            this.label30.TabIndex = 84;
            this.label30.Text = "Empresa:";
            // 
            // cboEmpresa
            // 
            this.cboEmpresa.FormattingEnabled = true;
            this.cboEmpresa.Location = new System.Drawing.Point(107, 65);
            this.cboEmpresa.Margin = new System.Windows.Forms.Padding(4);
            this.cboEmpresa.Name = "cboEmpresa";
            this.cboEmpresa.Size = new System.Drawing.Size(372, 24);
            this.cboEmpresa.TabIndex = 83;
            this.cboEmpresa.Tag = "requerido";
            // 
            // PesoAcreditado
            // 
            this.PesoAcreditado.DecimalPlaces = 3;
            this.PesoAcreditado.Location = new System.Drawing.Point(675, 658);
            this.PesoAcreditado.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PesoAcreditado.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PesoAcreditado.Name = "PesoAcreditado";
            this.PesoAcreditado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PesoAcreditado.Size = new System.Drawing.Size(120, 22);
            this.PesoAcreditado.TabIndex = 82;
            this.PesoAcreditado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(656, 587);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(191, 17);
            this.label29.TabIndex = 81;
            this.label29.Text = "PESO ACREDITADO MTC";
            // 
            // PesoNeto
            // 
            this.PesoNeto.DecimalPlaces = 3;
            this.PesoNeto.Location = new System.Drawing.Point(520, 658);
            this.PesoNeto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PesoNeto.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PesoNeto.Name = "PesoNeto";
            this.PesoNeto.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PesoNeto.Size = new System.Drawing.Size(120, 22);
            this.PesoNeto.TabIndex = 79;
            this.PesoNeto.Tag = "requerido";
            this.PesoNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(459, 663);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(28, 17);
            this.label28.TabIndex = 78;
            this.label28.Text = "TM";
            // 
            // PesoTara
            // 
            this.PesoTara.DecimalPlaces = 3;
            this.PesoTara.Location = new System.Drawing.Point(332, 658);
            this.PesoTara.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PesoTara.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PesoTara.Name = "PesoTara";
            this.PesoTara.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PesoTara.Size = new System.Drawing.Size(120, 22);
            this.PesoTara.TabIndex = 77;
            this.PesoTara.Tag = "requerido";
            this.PesoTara.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(244, 663);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(28, 17);
            this.label27.TabIndex = 76;
            this.label27.Text = "TM";
            // 
            // PesoBruto
            // 
            this.PesoBruto.DecimalPlaces = 3;
            this.PesoBruto.Location = new System.Drawing.Point(117, 658);
            this.PesoBruto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PesoBruto.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PesoBruto.Name = "PesoBruto";
            this.PesoBruto.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PesoBruto.Size = new System.Drawing.Size(120, 22);
            this.PesoBruto.TabIndex = 75;
            this.PesoBruto.Tag = "requerido";
            this.PesoBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(37, 663);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 17);
            this.label26.TabIndex = 73;
            this.label26.Text = "Peso :";
            // 
            // dtpFechaPesoTara
            // 
            this.dtpFechaPesoTara.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPesoTara.Location = new System.Drawing.Point(329, 613);
            this.dtpFechaPesoTara.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaPesoTara.Name = "dtpFechaPesoTara";
            this.dtpFechaPesoTara.Size = new System.Drawing.Size(132, 22);
            this.dtpFechaPesoTara.TabIndex = 72;
            this.dtpFechaPesoTara.Tag = "requerido";
            // 
            // dtpFechaPesoBruto
            // 
            this.dtpFechaPesoBruto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPesoBruto.Location = new System.Drawing.Point(116, 613);
            this.dtpFechaPesoBruto.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaPesoBruto.Name = "dtpFechaPesoBruto";
            this.dtpFechaPesoBruto.Size = new System.Drawing.Size(132, 22);
            this.dtpFechaPesoBruto.TabIndex = 71;
            this.dtpFechaPesoBruto.Tag = "requerido";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(537, 587);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 17);
            this.label25.TabIndex = 70;
            this.label25.Text = "PESO NETO";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(356, 587);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(96, 17);
            this.label24.TabIndex = 69;
            this.label24.Text = "PESO TARA";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(128, 587);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(109, 17);
            this.label23.TabIndex = 68;
            this.label23.Text = "PESO BRUTO";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(37, 613);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(55, 17);
            this.label22.TabIndex = 67;
            this.label22.Text = "Fecha :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 562);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(872, 17);
            this.label21.TabIndex = 66;
            this.label21.Text = "_________________________________________________________________________________" +
    "___________________________";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(275, 507);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(136, 17);
            this.label20.TabIndex = 65;
            this.label20.Text = "Fecha Guia Remis. :";
            // 
            // dtpFechaGuiaRem
            // 
            this.dtpFechaGuiaRem.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaGuiaRem.Location = new System.Drawing.Point(420, 505);
            this.dtpFechaGuiaRem.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaGuiaRem.Name = "dtpFechaGuiaRem";
            this.dtpFechaGuiaRem.Size = new System.Drawing.Size(132, 22);
            this.dtpFechaGuiaRem.TabIndex = 64;
            // 
            // txtNroGuiaRemitente
            // 
            this.txtNroGuiaRemitente.Location = new System.Drawing.Point(147, 505);
            this.txtNroGuiaRemitente.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNroGuiaRemitente.Name = "txtNroGuiaRemitente";
            this.txtNroGuiaRemitente.Size = new System.Drawing.Size(105, 22);
            this.txtNroGuiaRemitente.TabIndex = 63;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 505);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(124, 17);
            this.label19.TabIndex = 62;
            this.label19.Text = "Nro. Guia Remis. :";
            // 
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.Location = new System.Drawing.Point(473, 459);
            this.btnBuscarProveedor.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(79, 28);
            this.btnBuscarProveedor.TabIndex = 60;
            this.btnBuscarProveedor.Text = "Buscar";
            this.btnBuscarProveedor.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(63, 466);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(76, 17);
            this.label18.TabIndex = 59;
            this.label18.Text = "Remitente:";
            // 
            // txtNroGuiaTransporte
            // 
            this.txtNroGuiaTransporte.Location = new System.Drawing.Point(147, 400);
            this.txtNroGuiaTransporte.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNroGuiaTransporte.Name = "txtNroGuiaTransporte";
            this.txtNroGuiaTransporte.Size = new System.Drawing.Size(105, 22);
            this.txtNroGuiaTransporte.TabIndex = 54;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 405);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(126, 17);
            this.label16.TabIndex = 53;
            this.label16.Text = "Nro. Guia Transp.:";
            // 
            // txtBrevete
            // 
            this.txtBrevete.Location = new System.Drawing.Point(605, 358);
            this.txtBrevete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBrevete.Name = "txtBrevete";
            this.txtBrevete.ReadOnly = true;
            this.txtBrevete.Size = new System.Drawing.Size(149, 22);
            this.txtBrevete.TabIndex = 52;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(531, 358);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 17);
            this.label14.TabIndex = 51;
            this.label14.Text = "Brevete:";
            // 
            // btnBuscarChofer
            // 
            this.btnBuscarChofer.Location = new System.Drawing.Point(407, 353);
            this.btnBuscarChofer.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarChofer.Name = "btnBuscarChofer";
            this.btnBuscarChofer.Size = new System.Drawing.Size(79, 28);
            this.btnBuscarChofer.TabIndex = 49;
            this.btnBuscarChofer.Text = "Buscar";
            this.btnBuscarChofer.UseVisualStyleBackColor = true;
            this.btnBuscarChofer.Click += new System.EventHandler(this.btnBuscarChofer_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(85, 363);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 17);
            this.label13.TabIndex = 47;
            this.label13.Text = "Chofer:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(487, 321);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 17);
            this.label8.TabIndex = 44;
            this.label8.Text = "Placa Vehículo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 321);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 17);
            this.label7.TabIndex = 43;
            this.label7.Text = "Placa Tracto:";
            // 
            // txtEstadoSunat
            // 
            this.txtEstadoSunat.Location = new System.Drawing.Point(605, 270);
            this.txtEstadoSunat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEstadoSunat.Name = "txtEstadoSunat";
            this.txtEstadoSunat.ReadOnly = true;
            this.txtEstadoSunat.Size = new System.Drawing.Size(179, 22);
            this.txtEstadoSunat.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(495, 270);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 17);
            this.label4.TabIndex = 39;
            this.label4.Text = "Estado Sunat:";
            // 
            // btnBuscarTransportista
            // 
            this.btnBuscarTransportista.Location = new System.Drawing.Point(407, 266);
            this.btnBuscarTransportista.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarTransportista.Name = "btnBuscarTransportista";
            this.btnBuscarTransportista.Size = new System.Drawing.Size(79, 28);
            this.btnBuscarTransportista.TabIndex = 38;
            this.btnBuscarTransportista.Text = "Buscar";
            this.btnBuscarTransportista.UseVisualStyleBackColor = true;
            this.btnBuscarTransportista.Click += new System.EventHandler(this.btnBuscarTransportista_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 270);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 36;
            this.label3.Text = "Transportista:";
            // 
            // btnBuscarProcedencia
            // 
            this.btnBuscarProcedencia.Location = new System.Drawing.Point(407, 206);
            this.btnBuscarProcedencia.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarProcedencia.Name = "btnBuscarProcedencia";
            this.btnBuscarProcedencia.Size = new System.Drawing.Size(79, 28);
            this.btnBuscarProcedencia.TabIndex = 35;
            this.btnBuscarProcedencia.Text = "Buscar";
            this.btnBuscarProcedencia.UseVisualStyleBackColor = true;
            this.btnBuscarProcedencia.Click += new System.EventHandler(this.btnBuscarProcedencia_Click);
            // 
            // cboBalanza
            // 
            this.cboBalanza.FormattingEnabled = true;
            this.cboBalanza.Location = new System.Drawing.Point(605, 64);
            this.cboBalanza.Margin = new System.Windows.Forms.Padding(4);
            this.cboBalanza.Name = "cboBalanza";
            this.cboBalanza.Size = new System.Drawing.Size(228, 24);
            this.cboBalanza.TabIndex = 33;
            this.cboBalanza.Tag = "requerido";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 209);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 32;
            this.label2.Text = "Procedencia:";
            // 
            // cboProducto
            // 
            this.cboProducto.FormattingEnabled = true;
            this.cboProducto.Location = new System.Drawing.Point(147, 164);
            this.cboProducto.Margin = new System.Windows.Forms.Padding(4);
            this.cboProducto.Name = "cboProducto";
            this.cboProducto.Size = new System.Drawing.Size(256, 24);
            this.cboProducto.TabIndex = 28;
            this.cboProducto.Tag = "requerido";
            // 
            // cboClase
            // 
            this.cboClase.FormattingEnabled = true;
            this.cboClase.Location = new System.Drawing.Point(605, 158);
            this.cboClase.Margin = new System.Windows.Forms.Padding(4);
            this.cboClase.Name = "cboClase";
            this.cboClase.Size = new System.Drawing.Size(195, 24);
            this.cboClase.TabIndex = 27;
            this.cboClase.Tag = "requerido";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(69, 171);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 17);
            this.label12.TabIndex = 26;
            this.label12.Text = "Producto:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(545, 164);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 17);
            this.label11.TabIndex = 25;
            this.label11.Text = "Clase:";
            // 
            // cboTipoOperacion
            // 
            this.cboTipoOperacion.FormattingEnabled = true;
            this.cboTipoOperacion.Location = new System.Drawing.Point(147, 119);
            this.cboTipoOperacion.Margin = new System.Windows.Forms.Padding(4);
            this.cboTipoOperacion.Name = "cboTipoOperacion";
            this.cboTipoOperacion.Size = new System.Drawing.Size(132, 24);
            this.cboTipoOperacion.TabIndex = 24;
            this.cboTipoOperacion.Tag = "requerido";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 119);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 17);
            this.label10.TabIndex = 23;
            this.label10.Text = "Tipo Operación:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(529, 68);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "Balanza:";
            // 
            // txtNroTicket
            // 
            this.txtNroTicket.BackColor = System.Drawing.SystemColors.Info;
            this.txtNroTicket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNroTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroTicket.ForeColor = System.Drawing.Color.Red;
            this.txtNroTicket.Location = new System.Drawing.Point(391, 6);
            this.txtNroTicket.Margin = new System.Windows.Forms.Padding(4);
            this.txtNroTicket.Name = "txtNroTicket";
            this.txtNroTicket.Size = new System.Drawing.Size(147, 24);
            this.txtNroTicket.TabIndex = 8;
            this.txtNroTicket.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro. Ticket:";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(874, 442);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(474, 250);
            this.panel2.TabIndex = 100;
            // 
            // TicketEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 743);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TicketEditForm";
            this.Text = "TicketEditForm";
            this.Load += new System.EventHandler(this.TicketEditForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PesoAcreditado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PesoNeto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PesoTara)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PesoBruto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNroTicket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboProducto;
        private System.Windows.Forms.ComboBox cboClase;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboTipoOperacion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboBalanza;
        private System.Windows.Forms.Button btnBuscarProcedencia;
        private System.Windows.Forms.Button btnBuscarTransportista;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEstadoSunat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBuscarChofer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBrevete;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNroGuiaTransporte;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnBuscarProveedor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtNroGuiaRemitente;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dtpFechaGuiaRem;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown PesoTara;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown PesoBruto;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker dtpFechaPesoTara;
        private System.Windows.Forms.DateTimePicker dtpFechaPesoBruto;
        private System.Windows.Forms.NumericUpDown PesoNeto;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.NumericUpDown PesoAcreditado;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cboEmpresa;
        private System.Windows.Forms.ComboBox cboProcedencia;
        private System.Windows.Forms.ComboBox cboTransportista;
        private System.Windows.Forms.ComboBox cboPlacaVehiculo;
        private System.Windows.Forms.ComboBox cboPlacaTracto;
        private System.Windows.Forms.ComboBox cboChofer;
        private System.Windows.Forms.ComboBox cboRemitente;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox cboDeposito;
        private System.Windows.Forms.Button btnBuscarDeposito;
        private System.Windows.Forms.Panel panel2;
    }
}