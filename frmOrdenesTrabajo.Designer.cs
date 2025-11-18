namespace SIGO_WinForm
{
    partial class frmOrdenesTrabajo
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
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.btnActualizarEstado = new System.Windows.Forms.Button();
            this.chkListasParaEntrega = new System.Windows.Forms.CheckBox();
            this.btnMostrarTodas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes.Location = new System.Drawing.Point(12, 149);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.RowHeadersWidth = 51;
            this.dgvOrdenes.RowTemplate.Height = 24;
            this.dgvOrdenes.Size = new System.Drawing.Size(1233, 358);
            this.dgvOrdenes.TabIndex = 0;
            this.dgvOrdenes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenes_CellClick);
            // 
            // cmbEstado
            // 
            this.cmbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(37, 84);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(491, 33);
            this.cmbEstado.TabIndex = 1;
            // 
            // btnActualizarEstado
            // 
            this.btnActualizarEstado.Location = new System.Drawing.Point(898, 21);
            this.btnActualizarEstado.Name = "btnActualizarEstado";
            this.btnActualizarEstado.Size = new System.Drawing.Size(188, 105);
            this.btnActualizarEstado.TabIndex = 2;
            this.btnActualizarEstado.Text = "Actualizar Estado";
            this.btnActualizarEstado.UseVisualStyleBackColor = true;
            this.btnActualizarEstado.Click += new System.EventHandler(this.btnActualizarEstado_Click);
            // 
            // chkListasParaEntrega
            // 
            this.chkListasParaEntrega.AutoSize = true;
            this.chkListasParaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListasParaEntrega.Location = new System.Drawing.Point(37, 12);
            this.chkListasParaEntrega.Name = "chkListasParaEntrega";
            this.chkListasParaEntrega.Size = new System.Drawing.Size(318, 29);
            this.chkListasParaEntrega.TabIndex = 3;
            this.chkListasParaEntrega.Text = "Mostrar solo \'Listas para Entrega";
            this.chkListasParaEntrega.UseVisualStyleBackColor = true;
            this.chkListasParaEntrega.CheckedChanged += new System.EventHandler(this.chkListasParaEntrega_CheckedChanged);
            // 
            // btnMostrarTodas
            // 
            this.btnMostrarTodas.Location = new System.Drawing.Point(601, 21);
            this.btnMostrarTodas.Name = "btnMostrarTodas";
            this.btnMostrarTodas.Size = new System.Drawing.Size(200, 105);
            this.btnMostrarTodas.TabIndex = 4;
            this.btnMostrarTodas.Text = "Mostrar Todas";
            this.btnMostrarTodas.UseVisualStyleBackColor = true;
            this.btnMostrarTodas.Click += new System.EventHandler(this.btnMostrarTodas_Click);
            // 
            // frmOrdenesTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 650);
            this.Controls.Add(this.btnMostrarTodas);
            this.Controls.Add(this.chkListasParaEntrega);
            this.Controls.Add(this.btnActualizarEstado);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.dgvOrdenes);
            this.Name = "frmOrdenesTrabajo";
            this.Text = "frmOrdenesTrabajo";
            this.Load += new System.EventHandler(this.frmOrdenesTrabajo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Button btnActualizarEstado;
        private System.Windows.Forms.CheckBox chkListasParaEntrega;
        private System.Windows.Forms.Button btnMostrarTodas;
    }
}