using SIGO_WinForm.SIGO_DBDataSetTableAdapters;
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
    public partial class frmPacientes : Form
    {
        string idpaciente;

        // Instanciamos los adaptadores
        PacientesTableAdapter pacientes = new PacientesTableAdapter();
        ExamenesTableAdapter adaptadorExamenes = new ExamenesTableAdapter();
        VentasTableAdapter adaptadorVentas = new VentasTableAdapter();
        // OrdenesTrabajoTableAdapter adaptadorOrdenes ya no es necesario aquí


        public frmPacientes()
        {
            InitializeComponent();
        }

        private void frmPacientes_Load(object sender, EventArgs e)
        {
            cargarpacientes();
        }

        private void dgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Este evento es un fantasma, lo ignoramos.
        }

        public void cargarpacientes()
        {
            try
            {
                dgvPacientes.DataSource = pacientes.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los pacientes: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // (Tu código de 'Nuevo' está perfecto)
            string nombre = txtNombreCompleto.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("El formato del email no es válido. Debe contener '@' y un dominio.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dtPacientes = pacientes.GetData();

            bool emailExists = dtPacientes.AsEnumerable()
                .Any(row => row.Field<string>("Email").Equals(email, StringComparison.OrdinalIgnoreCase));

            if (emailExists)
            {
                MessageBox.Show("El email ingresado ya está registrado. Por favor, use uno diferente.", "Error de Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool nameExists = dtPacientes.AsEnumerable()
                .Any(row => row.Field<string>("NombreCompleto").Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (nameExists)
            {
                MessageBox.Show("El nombre completo ingresado ya está registrado.", "Error de Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                pacientes.InsertPacientes(nombre, telefono, direccion, email);
                cargarpacientes();
                MessageBox.Show("Paciente guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el paciente: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombreCompleto.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            idpaciente = null;
            txtNombreCompleto.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // (Tu código de 'Eliminar' está perfecto)
            if (dgvPacientes.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un paciente de la lista para eliminar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataRowView drv = (DataRowView)dgvPacientes.CurrentRow.DataBoundItem;
                int idParaBorrar = (int)drv["PacienteID"];
                string nombreParaBorrar = (string)drv["NombreCompleto"];

                var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar a: {nombreParaBorrar}?",
                                                  "Confirmar Eliminación",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        pacientes.DeletePaciente(idParaBorrar);
                        cargarpacientes();
                        LimpiarCampos();
                        MessageBox.Show("Paciente eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el paciente: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los datos de la fila: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPacientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // --- ¡AQUÍ ES DONDE CARGAMOS EL HISTORIAL! ---
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPacientes.Rows[e.RowIndex];
                try
                {
                    // 1. Rellenamos los campos 
                    idpaciente = row.Cells["PacienteID"].Value.ToString();
                    txtNombreCompleto.Text = row.Cells["NombreCompleto"].Value.ToString();
                    txtTelefono.Text = row.Cells["Telefono"].Value.ToString();
                    txtDireccion.Text = row.Cells["Direccion"].Value.ToString();
                    txtEmail.Text = row.Cells["Email"].Value.ToString();

                    int pacienteID = Convert.ToInt32(idpaciente);

                    // 2. Cargamos el historial de exámenes (Historial Clínico)
                    dgvExamenes.DataSource = adaptadorExamenes.GetDataByPacienteID(pacienteID);

                    // 3. Cargamos el historial de Ventas (Historial de Compras)
                    dgvVentasHistorial.DataSource = adaptadorVentas.GetDataByPacienteID(pacienteID);

                    // El grid de órdenes de laboratorio fue eliminado para estabilidad.

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al seleccionar la fila: {ex.Message}\nAsegúrese de que los nombres de las columnas sean correctos.", "Error de Selección", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // (Tu código de 'Actualizar' está perfecto)
            if (string.IsNullOrEmpty(idpaciente))
            {
                MessageBox.Show("Por favor, seleccione un paciente de la lista para actualizar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombreCompleto.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("El formato del email no es válido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idParaActualizar;
            if (!int.TryParse(idpaciente, out idParaActualizar))
            {
                MessageBox.Show("Error al obtener el ID del paciente.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                pacientes.UpdatePaciente(nombre, telefono, direccion, email, idParaActualizar);

                cargarpacientes();
                MessageBox.Show("Paciente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el paciente: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            // --- CÓDIGO CORREGIDO para la búsqueda en 3 campos ---
            string terminoBusqueda = txtBuscarPaciente.Text.Trim();

            if (string.IsNullOrEmpty(terminoBusqueda))
            {
                MessageBox.Show("Por favor, ingrese un nombre, teléfono o email para buscar.", "Campo Vacío");
                return;
            }

            try
            {
                string parametroLike = "%" + terminoBusqueda + "%";
                // Usa la consulta que modificamos para buscar en 3 campos
                dgvPacientes.DataSource = pacientes.GetDataBy3(parametroLike);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar pacientes: {ex.Message}", "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarExamen_Click(object sender, EventArgs e)
        {
            // (Tu código de 'Agregar Examen' está perfecto)
            if (string.IsNullOrEmpty(idpaciente))
            {
                MessageBox.Show("Por favor, seleccione un paciente de la lista primero.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(idpaciente);
            string nombre = txtNombreCompleto.Text;

            frmExamen nuevoExamen = new frmExamen(id, nombre);
            nuevoExamen.MdiParent = this.MdiParent;
            nuevoExamen.Show();
        }

        // --- Tus eventos fantasma (los dejamos) ---
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}