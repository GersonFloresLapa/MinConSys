
namespace MinConSys.Maestros
{
    partial class EmpresaEditForm
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
            this.txtZonaPartidaElectronica = new System.Windows.Forms.TextBox();
            this.txtPartidaElectronica = new System.Windows.Forms.TextBox();
            this.cboCondicionContribuyente = new System.Windows.Forms.ComboBox();
            this.cboEstadoContribuyente = new System.Windows.Forms.ComboBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtDireccionComercial = new System.Windows.Forms.TextBox();
            this.txtDireccionFiscal = new System.Windows.Forms.TextBox();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.txtRUC = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGuardar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(549, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = global::MinConSys.Properties.Resources.guardar;
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "toolStripButton1";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtZonaPartidaElectronica);
            this.panel1.Controls.Add(this.txtPartidaElectronica);
            this.panel1.Controls.Add(this.cboCondicionContribuyente);
            this.panel1.Controls.Add(this.cboEstadoContribuyente);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtTelefono);
            this.panel1.Controls.Add(this.txtDireccionComercial);
            this.panel1.Controls.Add(this.txtDireccionFiscal);
            this.panel1.Controls.Add(this.txtRazonSocial);
            this.panel1.Controls.Add(this.txtRUC);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 283);
            this.panel1.TabIndex = 1;
            // 
            // txtZonaPartidaElectronica
            // 
            this.txtZonaPartidaElectronica.Location = new System.Drawing.Point(343, 235);
            this.txtZonaPartidaElectronica.Name = "txtZonaPartidaElectronica";
            this.txtZonaPartidaElectronica.Size = new System.Drawing.Size(156, 20);
            this.txtZonaPartidaElectronica.TabIndex = 19;
            // 
            // txtPartidaElectronica
            // 
            this.txtPartidaElectronica.Location = new System.Drawing.Point(122, 235);
            this.txtPartidaElectronica.Name = "txtPartidaElectronica";
            this.txtPartidaElectronica.Size = new System.Drawing.Size(123, 20);
            this.txtPartidaElectronica.TabIndex = 18;
            // 
            // cboCondicionContribuyente
            // 
            this.cboCondicionContribuyente.FormattingEnabled = true;
            this.cboCondicionContribuyente.Location = new System.Drawing.Point(343, 199);
            this.cboCondicionContribuyente.Name = "cboCondicionContribuyente";
            this.cboCondicionContribuyente.Size = new System.Drawing.Size(156, 21);
            this.cboCondicionContribuyente.TabIndex = 17;
            this.cboCondicionContribuyente.Tag = "requerido";
            // 
            // cboEstadoContribuyente
            // 
            this.cboEstadoContribuyente.FormattingEnabled = true;
            this.cboEstadoContribuyente.Location = new System.Drawing.Point(122, 199);
            this.cboEstadoContribuyente.Name = "cboEstadoContribuyente";
            this.cboEstadoContribuyente.Size = new System.Drawing.Size(123, 21);
            this.cboEstadoContribuyente.TabIndex = 16;
            this.cboEstadoContribuyente.Tag = "requerido";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(343, 163);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(156, 20);
            this.txtEmail.TabIndex = 15;
            this.txtEmail.Tag = "email";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(122, 163);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(123, 20);
            this.txtTelefono.TabIndex = 14;
            this.txtTelefono.Tag = "entero";
            // 
            // txtDireccionComercial
            // 
            this.txtDireccionComercial.Location = new System.Drawing.Point(122, 127);
            this.txtDireccionComercial.Name = "txtDireccionComercial";
            this.txtDireccionComercial.Size = new System.Drawing.Size(377, 20);
            this.txtDireccionComercial.TabIndex = 13;
            // 
            // txtDireccionFiscal
            // 
            this.txtDireccionFiscal.Location = new System.Drawing.Point(122, 91);
            this.txtDireccionFiscal.Name = "txtDireccionFiscal";
            this.txtDireccionFiscal.Size = new System.Drawing.Size(377, 20);
            this.txtDireccionFiscal.TabIndex = 12;
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Location = new System.Drawing.Point(122, 55);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(377, 20);
            this.txtRazonSocial.TabIndex = 11;
            this.txtRazonSocial.Tag = "requerido";
            // 
            // txtRUC
            // 
            this.txtRUC.Location = new System.Drawing.Point(122, 22);
            this.txtRUC.Name = "txtRUC";
            this.txtRUC.Size = new System.Drawing.Size(100, 20);
            this.txtRUC.TabIndex = 10;
            this.txtRUC.Tag = "longitud:11|requerido|requerido";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(265, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Zona Partida:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 238);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Partida Electronica:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(265, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Condicion:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Estado:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(265, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Email:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Telefono:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Direccion Comercial:\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Direccion Fiscal:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Razon Social:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RUC:";
            // 
            // EmpresaEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 308);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EmpresaEditForm";
            this.Text = "Empresa";
            this.Load += new System.EventHandler(this.EmpresaEditForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.TextBox txtZonaPartidaElectronica;
        private System.Windows.Forms.TextBox txtPartidaElectronica;
        private System.Windows.Forms.ComboBox cboCondicionContribuyente;
        private System.Windows.Forms.ComboBox cboEstadoContribuyente;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtDireccionComercial;
        private System.Windows.Forms.TextBox txtDireccionFiscal;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.TextBox txtRUC;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}