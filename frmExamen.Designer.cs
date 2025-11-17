namespace SIGO_WinForm
{
    partial class frmExamen
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
            this.gbDatosGenerales = new System.Windows.Forms.GroupBox();
            this.dtpFechaExamen = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbOptometra = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombrePaciente = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numODAdicion = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numODEje = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numODEsfera = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numOSAdicion = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numOSEje = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numOSCilindro = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numOSEsfera = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGuardarExamen = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.numODCilindro = new System.Windows.Forms.NumericUpDown();
            this.gbDatosGenerales.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numODAdicion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numODEje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numODEsfera)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOSAdicion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOSEje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOSCilindro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOSEsfera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numODCilindro)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDatosGenerales
            // 
            this.gbDatosGenerales.Controls.Add(this.dtpFechaExamen);
            this.gbDatosGenerales.Controls.Add(this.label2);
            this.gbDatosGenerales.Controls.Add(this.cmbOptometra);
            this.gbDatosGenerales.Controls.Add(this.label1);
            this.gbDatosGenerales.Controls.Add(this.lblNombrePaciente);
            this.gbDatosGenerales.Location = new System.Drawing.Point(13, 13);
            this.gbDatosGenerales.Name = "gbDatosGenerales";
            this.gbDatosGenerales.Size = new System.Drawing.Size(663, 231);
            this.gbDatosGenerales.TabIndex = 0;
            this.gbDatosGenerales.TabStop = false;
            this.gbDatosGenerales.Text = "Datos Generales";
            this.gbDatosGenerales.Enter += new System.EventHandler(this.dtpFechaExamen_Enter);
            // 
            // dtpFechaExamen
            // 
            this.dtpFechaExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaExamen.Location = new System.Drawing.Point(263, 147);
            this.dtpFechaExamen.Name = "dtpFechaExamen";
            this.dtpFechaExamen.Size = new System.Drawing.Size(349, 30);
            this.dtpFechaExamen.TabIndex = 4;
            this.dtpFechaExamen.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha del Examen:";
            // 
            // cmbOptometra
            // 
            this.cmbOptometra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptometra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOptometra.FormattingEnabled = true;
            this.cmbOptometra.Location = new System.Drawing.Point(263, 96);
            this.cmbOptometra.Name = "cmbOptometra";
            this.cmbOptometra.Size = new System.Drawing.Size(121, 33);
            this.cmbOptometra.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Optómetra a cargo:";
            // 
            // lblNombrePaciente
            // 
            this.lblNombrePaciente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNombrePaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombrePaciente.Location = new System.Drawing.Point(170, 39);
            this.lblNombrePaciente.Name = "lblNombrePaciente";
            this.lblNombrePaciente.Size = new System.Drawing.Size(288, 23);
            this.lblNombrePaciente.TabIndex = 0;
            this.lblNombrePaciente.Text = "Paciente:";
            this.lblNombrePaciente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numODCilindro);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.numODAdicion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numODEje);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numODEsfera);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(699, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 231);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ojo Derecho (OD)";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // numODAdicion
            // 
            this.numODAdicion.DecimalPlaces = 2;
            this.numODAdicion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numODAdicion.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numODAdicion.Location = new System.Drawing.Point(227, 189);
            this.numODAdicion.Name = "numODAdicion";
            this.numODAdicion.Size = new System.Drawing.Size(174, 30);
            this.numODAdicion.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(43, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Adición (ADD):";
            // 
            // numODEje
            // 
            this.numODEje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numODEje.Location = new System.Drawing.Point(227, 145);
            this.numODEje.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numODEje.Name = "numODEje";
            this.numODEje.Size = new System.Drawing.Size(151, 30);
            this.numODEje.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(43, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "Eje:";
            // 
            // numODEsfera
            // 
            this.numODEsfera.DecimalPlaces = 2;
            this.numODEsfera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numODEsfera.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numODEsfera.Location = new System.Drawing.Point(227, 32);
            this.numODEsfera.Name = "numODEsfera";
            this.numODEsfera.Size = new System.Drawing.Size(151, 30);
            this.numODEsfera.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Esfera:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtObservaciones);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.numOSAdicion);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numOSEje);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numOSCilindro);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numOSEsfera);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(13, 258);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(874, 251);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ojo Izquierdo (OS)";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // numOSAdicion
            // 
            this.numOSAdicion.DecimalPlaces = 2;
            this.numOSAdicion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numOSAdicion.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numOSAdicion.Location = new System.Drawing.Point(179, 203);
            this.numOSAdicion.Name = "numOSAdicion";
            this.numOSAdicion.Size = new System.Drawing.Size(120, 30);
            this.numOSAdicion.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 203);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 25);
            this.label9.TabIndex = 6;
            this.label9.Text = "Adición (ADD):";
            // 
            // numOSEje
            // 
            this.numOSEje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numOSEje.Location = new System.Drawing.Point(132, 140);
            this.numOSEje.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numOSEje.Name = "numOSEje";
            this.numOSEje.Size = new System.Drawing.Size(120, 30);
            this.numOSEje.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 25);
            this.label8.TabIndex = 4;
            this.label8.Text = "Eje:";
            // 
            // numOSCilindro
            // 
            this.numOSCilindro.DecimalPlaces = 2;
            this.numOSCilindro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numOSCilindro.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numOSCilindro.Location = new System.Drawing.Point(132, 88);
            this.numOSCilindro.Name = "numOSCilindro";
            this.numOSCilindro.Size = new System.Drawing.Size(120, 30);
            this.numOSCilindro.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 25);
            this.label7.TabIndex = 2;
            this.label7.Text = "Cilindro:";
            // 
            // numOSEsfera
            // 
            this.numOSEsfera.DecimalPlaces = 2;
            this.numOSEsfera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numOSEsfera.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numOSEsfera.Location = new System.Drawing.Point(132, 36);
            this.numOSEsfera.Name = "numOSEsfera";
            this.numOSEsfera.Size = new System.Drawing.Size(120, 30);
            this.numOSEsfera.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Esfera:";
            // 
            // btnGuardarExamen
            // 
            this.btnGuardarExamen.Location = new System.Drawing.Point(981, 328);
            this.btnGuardarExamen.Name = "btnGuardarExamen";
            this.btnGuardarExamen.Size = new System.Drawing.Size(177, 128);
            this.btnGuardarExamen.TabIndex = 3;
            this.btnGuardarExamen.Text = "Guardar Examen";
            this.btnGuardarExamen.UseVisualStyleBackColor = true;
            this.btnGuardarExamen.Click += new System.EventHandler(this.btnGuardarExamen_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(547, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 25);
            this.label10.TabIndex = 8;
            this.label10.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservaciones.Location = new System.Drawing.Point(464, 70);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(336, 63);
            this.txtObservaciones.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(43, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 25);
            this.label11.TabIndex = 6;
            this.label11.Text = "Cilindro:";
            // 
            // numODCilindro
            // 
            this.numODCilindro.DecimalPlaces = 2;
            this.numODCilindro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numODCilindro.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numODCilindro.Location = new System.Drawing.Point(227, 92);
            this.numODCilindro.Name = "numODCilindro";
            this.numODCilindro.Size = new System.Drawing.Size(120, 30);
            this.numODCilindro.TabIndex = 7;
            // 
            // frmExamen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 526);
            this.Controls.Add(this.btnGuardarExamen);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbDatosGenerales);
            this.Name = "frmExamen";
            this.Text = "Guardar Examen";
            this.Load += new System.EventHandler(this.frmExamen_Load);
            this.gbDatosGenerales.ResumeLayout(false);
            this.gbDatosGenerales.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numODAdicion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numODEje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numODEsfera)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOSAdicion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOSEje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOSCilindro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOSEsfera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numODCilindro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDatosGenerales;
        private System.Windows.Forms.Label lblNombrePaciente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbOptometra;
        private System.Windows.Forms.DateTimePicker dtpFechaExamen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numODAdicion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numODEje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numODEsfera;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numOSEje;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numOSCilindro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numOSEsfera;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numOSAdicion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnGuardarExamen;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numODCilindro;
        private System.Windows.Forms.Label label11;
    }
}