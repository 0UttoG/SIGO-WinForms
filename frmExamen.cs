using SIGO_WinForm.SIGO_DBDataSetTableAdapters; // Importamos los adaptadores
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIGO_WinForm
{
    public partial class frmExamen : Form
    {
        // --- Adaptadores ---
        ExamenesTableAdapter adaptadorExamenes = new ExamenesTableAdapter();
        UsuariosTableAdapter adaptadorUsuarios = new UsuariosTableAdapter();

        // --- Variables para guardar los datos del paciente ---
        private int _pacienteID;
        private string _nombrePaciente;

        // Constructor por defecto
        public frmExamen()
        {
            InitializeComponent();
        }

        // --- Constructor NUEVO para pasar datos ---
        public frmExamen(int pacienteID, string nombrePaciente)
        {
            InitializeComponent();
            this._pacienteID = pacienteID;
            this._nombrePaciente = nombrePaciente;
        }

        // --- Evento Load: Se ejecuta cuando se abre el formulario ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void frmExamen_Load(object sender, EventArgs e)
        {
            lblNombrePaciente.Text = $"Paciente: {_nombrePaciente}";
            CargarOptometras();
        }

        // --- Método para llenar el ComboBox de Optómetras ---
        private void CargarOptometras()
        {
            try
            {
                cmbOptometra.DataSource = adaptadorUsuarios.GetData();
                cmbOptometra.DisplayMember = "NombreCompleto";
                cmbOptometra.ValueMember = "UsuarioID";
                cmbOptometra.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de optómetras: " + ex.Message);
            }
        }

        // --- Botón "Guardar Examen" ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void btnGuardarExamen_Click(object sender, EventArgs e)
        {
            if (cmbOptometra.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un optómetra.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. Recolectamos todos los datos del formulario
                int pacienteID = this._pacienteID;
                int optometraID = (int)cmbOptometra.SelectedValue;

                // --- ¡AQUÍ ESTÁ LA CORRECCIÓN! ---
                // Leemos el valor del control 'dtpFechaExamen', no del GroupBox
                DateTime fecha = dtpFechaExamen.Value;

                decimal odEsfera = numODEsfera.Value;
                decimal odCilindro = numODCilindro.Value;
                int odEje = (int)numODEje.Value;
                decimal odAdicion = numODAdicion.Value;

                decimal osEsfera = numOSEsfera.Value;
                decimal osCilindro = numOSCilindro.Value;
                int osEje = (int)numOSEje.Value;
                decimal osAdicion = numOSAdicion.Value;

                string observaciones = txtObservaciones.Text;

                // 2. Llamamos al método INSERT
                adaptadorExamenes.InsertarExamen(
                    pacienteID, optometraID, fecha,
                    odEsfera, odCilindro, odEje, odAdicion,
                    osEsfera, osCilindro, osEje, osAdicion,
                    observaciones
                );

                MessageBox.Show("¡Examen guardado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Cerramos el formulario de examen
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el examen: " + ex.Message);
            }
        }

        // --- Tus eventos fantasma (los dejamos para que el diseñador no falle) ---
        private void dtpFechaExamen_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}