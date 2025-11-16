using SIGO_WinForm.SIGO_DBDataSetTableAdapters; // Importamos el adaptador
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
    public partial class frmOrdenesTrabajo : Form
    {
        // 1. Instanciamos el adaptador
        OrdenesTrabajoTableAdapter adaptadorOrdenes = new OrdenesTrabajoTableAdapter();

        // 2. Variable para guardar el ID de la orden seleccionada
        int? idOrdenSeleccionada = null;

        public frmOrdenesTrabajo()
        {
            InitializeComponent();
        }

        // --- 3. Evento Load: Se ejecuta cuando se abre el formulario ---
        private void frmOrdenesTrabajo_Load(object sender, EventArgs e)
        {
            CargarOrdenes();
            CargarEstadosComboBox();
        }

        // --- 4. Método para Cargar el ComboBox de Estados ---
        private void CargarEstadosComboBox()
        {
            // Llenamos el ComboBox manualmente con los estados permitidos
            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("En Laboratorio");
            cmbEstado.Items.Add("Listo para Entrega");
            cmbEstado.Items.Add("Entregado");
        }

        // --- 5. Método para Cargar (o recargar) el DataGridView ---
        private void CargarOrdenes()
        {
            try
            {
                // Revisa si el CheckBox está marcado
                if (chkListasParaEntrega.Checked)
                {
                    // Si SÍ está marcado, usa la nueva consulta 'GetDataByEstado'
                    // Esto cumple con el "Panel de Entregas" 
                    dgvOrdenes.DataSource = adaptadorOrdenes.GetDataByEstado("Listo para Entrega");
                }
                else
                {
                    // Si NO está marcado, muestra todas las órdenes
                    dgvOrdenes.DataSource = adaptadorOrdenes.GetData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las órdenes: " + ex.Message);
            }
            // Limpiamos la selección
            LimpiarSeleccion();
        }

        // --- 6. Evento CellClick: Cargar datos al seleccionar en el grid ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void dgvOrdenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow fila = dgvOrdenes.Rows[e.RowIndex];

                    // Guardamos el ID de la orden seleccionada
                    idOrdenSeleccionada = (int)fila.Cells["OrdenID"].Value;

                    // Ponemos el estado actual en el ComboBox
                    string estadoActual = fila.Cells["Estado"].Value.ToString();
                    cmbEstado.SelectedItem = estadoActual;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al seleccionar la orden: " + ex.Message);
                }
            }
        }

        // --- 7. Botón "Actualizar Estado" ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void btnActualizarEstado_Click(object sender, EventArgs e)
        {
            // Validamos que se haya seleccionado una orden
            if (idOrdenSeleccionada == null)
            {
                MessageBox.Show("Por favor, seleccione una orden de la lista.");
                return;
            }
            // Validamos que se haya elegido un nuevo estado
            if (cmbEstado.SelectedItem == null)
            {
                MessageBox.Show("Por favor, elija un nuevo estado.");
                return;
            }

            try
            {
                string nuevoEstado = cmbEstado.SelectedItem.ToString();

                // Usamos la consulta UPDATE que creamos [cite: 88-89]
                adaptadorOrdenes.ActualizarEstado(nuevoEstado, (int)idOrdenSeleccionada);

                MessageBox.Show("¡Estado actualizado con éxito!");

                // Recargamos el grid para ver el cambio
                CargarOrdenes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el estado: " + ex.Message);
            }
        }

        // --- 8. Evento del CheckBox: Se activa al marcar/desmarcar ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void chkListasParaEntrega_CheckedChanged(object sender, EventArgs e)
        {
            // Cada vez que el usuario lo marca o desmarca, recargamos el grid
            CargarOrdenes();
        }

        // --- 9. Botón "Mostrar Todas" ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void btnMostrarTodas_Click(object sender, EventArgs e)
        {
            // Simplemente desmarca el CheckBox y recarga el grid
            chkListasParaEntrega.Checked = false;
            CargarOrdenes();
        }

        // --- 10. Método para limpiar la selección ---
        private void LimpiarSeleccion()
        {
            idOrdenSeleccionada = null;
            cmbEstado.SelectedIndex = -1;
            dgvOrdenes.ClearSelection();
        }
    }
}