
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
            this.txtNroTicket = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboTipoOperacion = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cboClase = new System.Windows.Forms.ComboBox();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBalanza = new System.Windows.Forms.ComboBox();
            this.txtProcedencia = new System.Windows.Forms.TextBox();
            this.btnBuscarProcedencia = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTransportista = new System.Windows.Forms.TextBox();
            this.btnBuscarTransportista = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEstadoSunat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPlacaTracto = new System.Windows.Forms.TextBox();
            this.txtPlacaVehiculo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtChofer = new System.Windows.Forms.TextBox();
            this.btnBuscarChofer = new System.Windows.Forms.Button();
            this.txtNombreChofer = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBrevete = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNroGuiaTransporte = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnBuscarProveedor = new System.Windows.Forms.Button();
            this.txtNombreProveedor = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtNroGuiaRemitente = new System.Windows.Forms.TextBox();
            this.dtpFechaGuiaRem = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.dtpFechaPesoBruto = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaPesoTara = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.nudPesoBruto = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.nudPesoTara = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.nudPesoNeto = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.nupPesoAcreditado = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPesoBruto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPesoTara)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPesoNeto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupPesoAcreditado)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGuardar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(980, 39);
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
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nupPesoAcreditado);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.nudPesoNeto);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.nudPesoTara);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.nudPesoBruto);
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
            this.panel1.Controls.Add(this.txtNombreProveedor);
            this.panel1.Controls.Add(this.btnBuscarProveedor);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.txtProveedor);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtNroGuiaTransporte);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.txtBrevete);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtNombreChofer);
            this.panel1.Controls.Add(this.btnBuscarChofer);
            this.panel1.Controls.Add(this.txtChofer);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtPlacaVehiculo);
            this.panel1.Controls.Add(this.txtPlacaTracto);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtEstadoSunat);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnBuscarTransportista);
            this.panel1.Controls.Add(this.txtTransportista);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnBuscarProcedencia);
            this.panel1.Controls.Add(this.txtProcedencia);
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
            this.panel1.Size = new System.Drawing.Size(980, 691);
            this.panel1.TabIndex = 2;
            // 
            // txtNroTicket
            // 
            this.txtNroTicket.BackColor = System.Drawing.SystemColors.Info;
            this.txtNroTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroTicket.ForeColor = System.Drawing.Color.Red;
            this.txtNroTicket.Location = new System.Drawing.Point(391, 6);
            this.txtNroTicket.Margin = new System.Windows.Forms.Padding(4);
            this.txtNroTicket.Name = "txtNroTicket";
            this.txtNroTicket.ReadOnly = true;
            this.txtNroTicket.Size = new System.Drawing.Size(147, 24);
            this.txtNroTicket.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(302, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro. Ticket:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(77, 71);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "Balanza:";
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(491, 171);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 17);
            this.label11.TabIndex = 25;
            this.label11.Text = "Clase:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(70, 171);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 17);
            this.label12.TabIndex = 26;
            this.label12.Text = "Producto:";
            // 
            // cboClase
            // 
            this.cboClase.FormattingEnabled = true;
            this.cboClase.Location = new System.Drawing.Point(559, 164);
            this.cboClase.Margin = new System.Windows.Forms.Padding(4);
            this.cboClase.Name = "cboClase";
            this.cboClase.Size = new System.Drawing.Size(195, 24);
            this.cboClase.TabIndex = 27;
            this.cboClase.Tag = "requerido";
            // 
            // cboProducto
            // 
            this.cboProducto.FormattingEnabled = true;
            this.cboProducto.Location = new System.Drawing.Point(147, 164);
            this.cboProducto.Margin = new System.Windows.Forms.Padding(4);
            this.cboProducto.Name = "cboProducto";
            this.cboProducto.Size = new System.Drawing.Size(200, 24);
            this.cboProducto.TabIndex = 28;
            this.cboProducto.Tag = "requerido";
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
            // cboBalanza
            // 
            this.cboBalanza.FormattingEnabled = true;
            this.cboBalanza.Location = new System.Drawing.Point(146, 65);
            this.cboBalanza.Margin = new System.Windows.Forms.Padding(4);
            this.cboBalanza.Name = "cboBalanza";
            this.cboBalanza.Size = new System.Drawing.Size(228, 24);
            this.cboBalanza.TabIndex = 33;
            this.cboBalanza.Tag = "requerido";
            // 
            // txtProcedencia
            // 
            this.txtProcedencia.Location = new System.Drawing.Point(147, 209);
            this.txtProcedencia.Name = "txtProcedencia";
            this.txtProcedencia.Size = new System.Drawing.Size(200, 22);
            this.txtProcedencia.TabIndex = 34;
            // 
            // btnBuscarProcedencia
            // 
            this.btnBuscarProcedencia.Location = new System.Drawing.Point(354, 206);
            this.btnBuscarProcedencia.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarProcedencia.Name = "btnBuscarProcedencia";
            this.btnBuscarProcedencia.Size = new System.Drawing.Size(79, 28);
            this.btnBuscarProcedencia.TabIndex = 35;
            this.btnBuscarProcedencia.Text = "Buscar";
            this.btnBuscarProcedencia.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 269);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 36;
            this.label3.Text = "Transportista:";
            // 
            // txtTransportista
            // 
            this.txtTransportista.Location = new System.Drawing.Point(147, 269);
            this.txtTransportista.Name = "txtTransportista";
            this.txtTransportista.Size = new System.Drawing.Size(200, 22);
            this.txtTransportista.TabIndex = 37;
            // 
            // btnBuscarTransportista
            // 
            this.btnBuscarTransportista.Location = new System.Drawing.Point(354, 266);
            this.btnBuscarTransportista.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarTransportista.Name = "btnBuscarTransportista";
            this.btnBuscarTransportista.Size = new System.Drawing.Size(79, 28);
            this.btnBuscarTransportista.TabIndex = 38;
            this.btnBuscarTransportista.Text = "Buscar";
            this.btnBuscarTransportista.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(495, 269);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 17);
            this.label4.TabIndex = 39;
            this.label4.Text = "Estado Sunat:";
            // 
            // txtEstadoSunat
            // 
            this.txtEstadoSunat.Location = new System.Drawing.Point(605, 269);
            this.txtEstadoSunat.Name = "txtEstadoSunat";
            this.txtEstadoSunat.Size = new System.Drawing.Size(179, 22);
            this.txtEstadoSunat.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(792, 17);
            this.label6.TabIndex = 42;
            this.label6.Text = "_________________________________________________________________________________" +
    "_________________";
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
            // txtPlacaTracto
            // 
            this.txtPlacaTracto.Location = new System.Drawing.Point(147, 318);
            this.txtPlacaTracto.Name = "txtPlacaTracto";
            this.txtPlacaTracto.Size = new System.Drawing.Size(179, 22);
            this.txtPlacaTracto.TabIndex = 45;
            // 
            // txtPlacaVehiculo
            // 
            this.txtPlacaVehiculo.Location = new System.Drawing.Point(605, 318);
            this.txtPlacaVehiculo.Name = "txtPlacaVehiculo";
            this.txtPlacaVehiculo.Size = new System.Drawing.Size(179, 22);
            this.txtPlacaVehiculo.TabIndex = 46;
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
            // txtChofer
            // 
            this.txtChofer.Location = new System.Drawing.Point(147, 360);
            this.txtChofer.Name = "txtChofer";
            this.txtChofer.Size = new System.Drawing.Size(78, 22);
            this.txtChofer.TabIndex = 48;
            // 
            // btnBuscarChofer
            // 
            this.btnBuscarChofer.Location = new System.Drawing.Point(450, 354);
            this.btnBuscarChofer.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarChofer.Name = "btnBuscarChofer";
            this.btnBuscarChofer.Size = new System.Drawing.Size(79, 28);
            this.btnBuscarChofer.TabIndex = 49;
            this.btnBuscarChofer.Text = "Buscar";
            this.btnBuscarChofer.UseVisualStyleBackColor = true;
            // 
            // txtNombreChofer
            // 
            this.txtNombreChofer.Location = new System.Drawing.Point(231, 360);
            this.txtNombreChofer.Name = "txtNombreChofer";
            this.txtNombreChofer.Size = new System.Drawing.Size(202, 22);
            this.txtNombreChofer.TabIndex = 50;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(537, 360);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 17);
            this.label14.TabIndex = 51;
            this.label14.Text = "Brevete:";
            // 
            // txtBrevete
            // 
            this.txtBrevete.Location = new System.Drawing.Point(605, 358);
            this.txtBrevete.Name = "txtBrevete";
            this.txtBrevete.Size = new System.Drawing.Size(149, 22);
            this.txtBrevete.TabIndex = 52;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 405);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(126, 17);
            this.label16.TabIndex = 53;
            this.label16.Text = "Nro. Guia Trasnp.:";
            // 
            // txtNroGuiaTransporte
            // 
            this.txtNroGuiaTransporte.Location = new System.Drawing.Point(146, 400);
            this.txtNroGuiaTransporte.Name = "txtNroGuiaTransporte";
            this.txtNroGuiaTransporte.Size = new System.Drawing.Size(105, 22);
            this.txtNroGuiaTransporte.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(792, 17);
            this.label5.TabIndex = 55;
            this.label5.Text = "_________________________________________________________________________________" +
    "_________________";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(2, 236);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(792, 17);
            this.label15.TabIndex = 56;
            this.label15.Text = "_________________________________________________________________________________" +
    "_________________";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(2, 425);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(792, 17);
            this.label17.TabIndex = 57;
            this.label17.Text = "_________________________________________________________________________________" +
    "_________________";
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(147, 461);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(86, 22);
            this.txtProveedor.TabIndex = 58;
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
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.Location = new System.Drawing.Point(450, 461);
            this.btnBuscarProveedor.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(79, 28);
            this.btnBuscarProveedor.TabIndex = 60;
            this.btnBuscarProveedor.Text = "Buscar";
            this.btnBuscarProveedor.UseVisualStyleBackColor = true;
            // 
            // txtNombreProveedor
            // 
            this.txtNombreProveedor.Location = new System.Drawing.Point(241, 463);
            this.txtNombreProveedor.Name = "txtNombreProveedor";
            this.txtNombreProveedor.Size = new System.Drawing.Size(202, 22);
            this.txtNombreProveedor.TabIndex = 61;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 504);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(124, 17);
            this.label19.TabIndex = 62;
            this.label19.Text = "Nro. Guia Remis. :";
            // 
            // txtNroGuiaRemitente
            // 
            this.txtNroGuiaRemitente.Location = new System.Drawing.Point(146, 504);
            this.txtNroGuiaRemitente.Name = "txtNroGuiaRemitente";
            this.txtNroGuiaRemitente.Size = new System.Drawing.Size(105, 22);
            this.txtNroGuiaRemitente.TabIndex = 63;
            // 
            // dtpFechaGuiaRem
            // 
            this.dtpFechaGuiaRem.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaGuiaRem.Location = new System.Drawing.Point(420, 504);
            this.dtpFechaGuiaRem.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaGuiaRem.Name = "dtpFechaGuiaRem";
            this.dtpFechaGuiaRem.Size = new System.Drawing.Size(132, 22);
            this.dtpFechaGuiaRem.TabIndex = 64;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(274, 507);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(136, 17);
            this.label20.TabIndex = 65;
            this.label20.Text = "Fecha Guia Remis. :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(2, 527);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(792, 17);
            this.label21.TabIndex = 66;
            this.label21.Text = "_________________________________________________________________________________" +
    "_________________";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(37, 595);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(55, 17);
            this.label22.TabIndex = 67;
            this.label22.Text = "Fecha :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(128, 569);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(109, 17);
            this.label23.TabIndex = 68;
            this.label23.Text = "PESO BRUTO";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(356, 569);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(96, 17);
            this.label24.TabIndex = 69;
            this.label24.Text = "PESO TARA";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(537, 569);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 17);
            this.label25.TabIndex = 70;
            this.label25.Text = "PESO NETO";
            // 
            // dtpFechaPesoBruto
            // 
            this.dtpFechaPesoBruto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPesoBruto.Location = new System.Drawing.Point(116, 595);
            this.dtpFechaPesoBruto.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaPesoBruto.Name = "dtpFechaPesoBruto";
            this.dtpFechaPesoBruto.Size = new System.Drawing.Size(132, 22);
            this.dtpFechaPesoBruto.TabIndex = 71;
            // 
            // dtpFechaPesoTara
            // 
            this.dtpFechaPesoTara.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPesoTara.Location = new System.Drawing.Point(329, 595);
            this.dtpFechaPesoTara.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaPesoTara.Name = "dtpFechaPesoTara";
            this.dtpFechaPesoTara.Size = new System.Drawing.Size(132, 22);
            this.dtpFechaPesoTara.TabIndex = 72;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(37, 645);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 17);
            this.label26.TabIndex = 73;
            this.label26.Text = "Peso :";
            // 
            // nudPesoBruto
            // 
            this.nudPesoBruto.DecimalPlaces = 3;
            this.nudPesoBruto.Location = new System.Drawing.Point(117, 640);
            this.nudPesoBruto.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPesoBruto.Name = "nudPesoBruto";
            this.nudPesoBruto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nudPesoBruto.Size = new System.Drawing.Size(120, 22);
            this.nudPesoBruto.TabIndex = 75;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(244, 645);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(28, 17);
            this.label27.TabIndex = 76;
            this.label27.Text = "TM";
            // 
            // nudPesoTara
            // 
            this.nudPesoTara.DecimalPlaces = 3;
            this.nudPesoTara.Location = new System.Drawing.Point(332, 640);
            this.nudPesoTara.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPesoTara.Name = "nudPesoTara";
            this.nudPesoTara.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nudPesoTara.Size = new System.Drawing.Size(120, 22);
            this.nudPesoTara.TabIndex = 77;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(459, 645);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(28, 17);
            this.label28.TabIndex = 78;
            this.label28.Text = "TM";
            // 
            // nudPesoNeto
            // 
            this.nudPesoNeto.DecimalPlaces = 3;
            this.nudPesoNeto.Location = new System.Drawing.Point(520, 640);
            this.nudPesoNeto.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPesoNeto.Name = "nudPesoNeto";
            this.nudPesoNeto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nudPesoNeto.Size = new System.Drawing.Size(120, 22);
            this.nudPesoNeto.TabIndex = 79;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(656, 569);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(191, 17);
            this.label29.TabIndex = 81;
            this.label29.Text = "PESO ACREDITADO MTC";
            // 
            // nupPesoAcreditado
            // 
            this.nupPesoAcreditado.DecimalPlaces = 3;
            this.nupPesoAcreditado.Location = new System.Drawing.Point(674, 640);
            this.nupPesoAcreditado.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupPesoAcreditado.Name = "nupPesoAcreditado";
            this.nupPesoAcreditado.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nupPesoAcreditado.Size = new System.Drawing.Size(120, 22);
            this.nupPesoAcreditado.TabIndex = 82;
            // 
            // TicketEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 730);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TicketEditForm";
            this.Text = "TicketEditForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPesoBruto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPesoTara)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPesoNeto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupPesoAcreditado)).EndInit();
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
        private System.Windows.Forms.TextBox txtProcedencia;
        private System.Windows.Forms.Button btnBuscarProcedencia;
        private System.Windows.Forms.Button btnBuscarTransportista;
        private System.Windows.Forms.TextBox txtTransportista;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEstadoSunat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPlacaVehiculo;
        private System.Windows.Forms.TextBox txtPlacaTracto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBuscarChofer;
        private System.Windows.Forms.TextBox txtChofer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNombreChofer;
        private System.Windows.Forms.TextBox txtBrevete;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNroGuiaTransporte;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBuscarProveedor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtNombreProveedor;
        private System.Windows.Forms.TextBox txtNroGuiaRemitente;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dtpFechaGuiaRem;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown nudPesoTara;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown nudPesoBruto;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker dtpFechaPesoTara;
        private System.Windows.Forms.DateTimePicker dtpFechaPesoBruto;
        private System.Windows.Forms.NumericUpDown nudPesoNeto;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.NumericUpDown nupPesoAcreditado;
        private System.Windows.Forms.Label label29;
    }
}