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
        PacientesTableAdapter pacientes = new PacientesTableAdapter();
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
            // Puedes usar este evento si quieres cargar datos al hacer clic en una celda
        }

        public void cargarpacientes()
        {
            // Usar un try-catch es buena práctica por si falla la conexión a la DB
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
            // 1. Obtenemos y limpiamos los datos de los campos
            string nombre = txtNombreCompleto.Text.Trim(); // .Trim() quita espacios al inicio y final
            string telefono = txtTelefono.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string email = txtEmail.Text.Trim();

            // 2. Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detiene la ejecución si hay campos vacíos
            }

            // 3. Validación de formato de email (básica)
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("El formato del email no es válido. Debe contener '@' y un dominio.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Validación de duplicados
            var dtPacientes = pacientes.GetData(); // Obtenemos la tabla de pacientes actual

            // Comprobamos si el email ya existe (ignorando mayúsculas/minúsculas)
            // Asegúrate de que tu columna se llame "Email" en la base de datos
            bool emailExists = dtPacientes.AsEnumerable()
                .Any(row => row.Field<string>("Email").Equals(email, StringComparison.OrdinalIgnoreCase));

            if (emailExists)
            {
                MessageBox.Show("El email ingresado ya está registrado. Por favor, use uno diferente.", "Error de Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Comprobamos si el nombre completo ya existe (si también debe ser único)
            // Asegúrate de que tu columna se llame "NombreCompleto"
            bool nameExists = dtPacientes.AsEnumerable()
                .Any(row => row.Field<string>("NombreCompleto").Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (nameExists)
            {
                MessageBox.Show("El nombre completo ingresado ya está registrado.", "Error de Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5. Si todas las validaciones pasan, insertamos
            try
            {
                pacientes.InsertPacientes(nombre, telefono, direccion, email);
                cargarpacientes(); // Recargamos el DataGridView con el nuevo dato
                MessageBox.Show("Paciente guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos(); // Limpiamos los campos después de guardar
            }
            catch (Exception ex)
            {
                // Capturamos cualquier error que ocurra durante la inserción
                MessageBox.Show($"Error al guardar el paciente: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método auxiliar para limpiar los TextBoxes
        private void LimpiarCampos()
        {
            txtNombreCompleto.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";

            // Añade esta línea para "des-seleccionar" el paciente
            idpaciente = null;

            txtNombreCompleto.Focus(); // Ponemos el foco de nuevo en el primer campo
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // 1. Verificar si hay una fila seleccionada
            // Es mejor usar CurrentRow en lugar de SelectedRows por si solo han hecho clic en una celda
            if (dgvPacientes.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un paciente de la lista para eliminar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Obtener los datos de la fila seleccionada
                DataRowView drv = (DataRowView)dgvPacientes.CurrentRow.DataBoundItem;

                // !! IMPORTANTE: Asume que tu columna de ID se llama "Id"
                // Si se llama "PacienteID" o diferente, cámbialo aquí.
                // Y lo convertimos al tipo de dato correcto (usualmente 'int')
                // Corregido
                int idParaBorrar = (int)drv["PacienteID"]; 
                string nombreParaBorrar = (string)drv["NombreCompleto"]; // Para el mensaje

                // 3. Pedir confirmación (¡Importante!)
                var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar a: {nombreParaBorrar}?",
                                                 "Confirmar Eliminación",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    // 4. Si el usuario dice "Sí", proceder con la eliminación
                    try
                    {
                        // !! IMPORTANTE:
                        // Tu código usaba "DeletePaciente". El método por defecto del TableAdapter
                        // para borrar por ID suele llamarse "Delete". 
                        // Asegúrate de usar el nombre correcto.
                        pacientes.DeletePaciente(idParaBorrar);

                        // 5. Recargar los datos y limpiar
                        cargarpacientes();
                        LimpiarCampos();
                        MessageBox.Show("Paciente eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el paciente: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Si el usuario dice "No", no hacemos nada.
            }
            catch (Exception ex)
            {
                // Este catch captura errores al obtener el ID (ej. la columna "Id" no se llama así)
                MessageBox.Show($"Error al obtener los datos de la fila: {ex.Message}\n\nAsegúrese de que su columna de ID se llame 'Id'.", "Error de Selección", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPacientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que el clic no sea en el encabezado (e.RowIndex < 0)
            if (e.RowIndex >= 0)
            {
                // Obtenemos la fila que se seleccionó
                DataGridViewRow row = dgvPacientes.Rows[e.RowIndex];

                // !! IMPORTANTE: Asegúrate de que los nombres de las columnas
                // ("Id", "NombreCompleto", "Telefono", etc.)
                // coincidan exactamente con los de tu base de datos.

                try
                {
                    // Rellenamos los campos de texto con los valores de la fila
                    // Guardamos el ID en la variable que ya tenías
                    idpaciente = row.Cells["PacienteID"].Value.ToString();

                    // Rellenamos los textboxes
                    txtNombreCompleto.Text = row.Cells["NombreCompleto"].Value.ToString();
                    txtTelefono.Text = row.Cells["Telefono"].Value.ToString();
                    txtDireccion.Text = row.Cells["Direccion"].Value.ToString();
                    txtEmail.Text = row.Cells["Email"].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al seleccionar la fila: {ex.Message}\nAsegúrese de que los nombres de las columnas sean correctos.", "Error de Selección", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // 1. Verificar si se ha seleccionado un paciente (el ID no está vacío)
            if (string.IsNullOrEmpty(idpaciente))
            {
                MessageBox.Show("Por favor, seleccione un paciente de la lista para actualizar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Obtenemos y limpiamos los datos de los campos
            string nombre = txtNombreCompleto.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string email = txtEmail.Text.Trim();

            // 3. Validación de campos vacíos (igual que en "Nuevo")
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Validación de formato de email (básica)
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("El formato del email no es válido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 5. Convertir el ID a 'int' (¡importante!)
            int idParaActualizar;
            if (!int.TryParse(idpaciente, out idParaActualizar))
            {
                MessageBox.Show("Error al obtener el ID del paciente.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 6. Si todas las validaciones pasan, actualizamos
            try
            {
                // ¡Usamos el nuevo método que creamos!
                pacientes.UpdatePaciente(nombre, telefono, direccion, email, idParaActualizar);

                cargarpacientes(); // Recargamos el DataGridView
                MessageBox.Show("Paciente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos(); // Limpiamos los campos después de actualizar
            }
            catch (Exception ex)
            {
                // Capturamos cualquier error que ocurra durante la actualización
                MessageBox.Show($"Error al actualizar el paciente: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            // 1. Obtenemos el término de búsqueda del TextBox
            string terminoBusqueda = txtBuscarPaciente.Text.Trim();

            if (string.IsNullOrEmpty(terminoBusqueda))
            {
                MessageBox.Show("Por favor, ingrese un nombre para buscar.", "Campo Vacío");
                return;
            }

            try
            {
                // 2. Preparamos el parámetro para la consulta LIKE
                // Añadimos los '%' para que busque cualquier coincidencia
                string parametroLike =terminoBusqueda;

                // 3. Llamamos al nuevo método del TableAdapter
                // y asignamos el resultado al DataGridView
                // Corregido
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
    }
}
