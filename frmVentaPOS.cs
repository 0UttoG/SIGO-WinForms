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
    public partial class frmVentaPOS : Form
    {
        // --- 1. Instanciamos los adaptadores que usaremos ---
        PacientesTableAdapter adaptadorPacientes = new PacientesTableAdapter();
        ProductosTableAdapter adaptadorProductos = new ProductosTableAdapter();
        VentasTableAdapter adaptadorVentas = new VentasTableAdapter();
        DetalleVentasTableAdapter adaptadorDetalle = new DetalleVentasTableAdapter();
        OrdenesTrabajoTableAdapter adaptadorOrdenes = new OrdenesTrabajoTableAdapter(); // <-- AÑADIDO

        // --- 2. Variables para guardar el estado de la venta ---
        DataTable carrito = new DataTable();
        int? idPacienteSeleccionado = null;
        int idUsuarioActual = 1; // <-- Placeholder. Debería venir de un Login

        public frmVentaPOS()
        {
            InitializeComponent();
        }

        // --- 3. Evento Load: Se ejecuta cuando se abre el formulario ---
        private void frmVentaPOS_Load(object sender, EventArgs e)
        {
            ConfigurarCarrito();
            CargarMetodosPago();
            CargarProductosEnComboBox();
        }

        // --- 4. Métodos de Configuración Inicial ---

        // Prepara el DataGridView del carrito
        private void ConfigurarCarrito()
        {
            carrito.Columns.Add("ProductoID", typeof(int));
            carrito.Columns.Add("NombreProducto", typeof(string));
            carrito.Columns.Add("Cantidad", typeof(int));
            carrito.Columns.Add("PrecioUnitario", typeof(decimal));
            DataColumn subTotalCol = new DataColumn("SubTotal", typeof(decimal));
            subTotalCol.Expression = "Cantidad * PrecioUnitario";
            carrito.Columns.Add(subTotalCol);
            dgvCarrito.DataSource = carrito;
        }

        // Llena el ComboBox de métodos de pago
        private void CargarMetodosPago()
        {
            cmbMetodoPago.Items.Add("Efectivo");
            cmbMetodoPago.Items.Add("Tarjeta");
            cmbMetodoPago.SelectedIndex = 0;
        }

        // Llena el ComboBox de Productos
        private void CargarProductosEnComboBox()
        {
            try
            {
                cmbProducto.DataSource = adaptadorProductos.GetData();
                cmbProducto.DisplayMember = "NombreProducto";
                cmbProducto.ValueMember = "ProductoID";
                cmbProducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message);
            }
        }

        // --- 5. Evento DropDown del ComboBox de Productos ---
        // (Para que se actualice si creaste un producto nuevo)
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void cmbProducto_DropDown(object sender, EventArgs e)
        {
            CargarProductosEnComboBox();
        }

        // --- 6. Botón "Buscar Paciente" ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscarPaciente.Text;
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                MessageBox.Show("Por favor ingrese un nombre o ID para buscar.");
                return;
            }

            try
            {
                var tablaPacientes = adaptadorPacientes.GetDataBy3(busqueda);

                if (tablaPacientes.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún paciente con ese nombre.");
                    lblNombrePaciente.Text = "Cliente: (Ninguno)";
                    idPacienteSeleccionado = null;
                }
                else if (tablaPacientes.Rows.Count > 1)
                {
                    MessageBox.Show("Se encontraron varios pacientes. Por favor, sea más específico.");
                    lblNombrePaciente.Text = "Cliente: (Varios resultados)";
                    idPacienteSeleccionado = null;
                }
                else
                {
                    DataRow fila = tablaPacientes.Rows[0];
                    idPacienteSeleccionado = (int)fila["PacienteID"];
                    lblNombrePaciente.Text = $"Cliente: {fila["NombreCompleto"].ToString()}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar paciente: " + ex.Message);
                idPacienteSeleccionado = null;
            }
        }

        // ---- INICIO DE CÓDIGO NUEVO ----
        // --- 7. Botón "Agregar al Carrito" ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un producto.", "Error");
                return;
            }

            try
            {
                // Obtenemos el producto de la BD (usando el método que creamos)
                int idProducto = (int)cmbProducto.SelectedValue;
                var productoTabla = adaptadorProductos.GetDataByProductoID(idProducto);

                if (productoTabla.Rows.Count == 0)
                {
                    MessageBox.Show("Error: No se encontró el producto.");
                    return;
                }

                var filaProducto = productoTabla[0];
                int cantidad = (int)numCantidad.Value;

                // Verificamos si hay stock
                int stockActual = (int)filaProducto["CantidadActual"];
                if (cantidad > stockActual)
                {
                    MessageBox.Show($"No hay stock suficiente. Solo quedan {stockActual} unidades.", "Stock Insuficiente");
                    return;
                }

                // Agregamos al carrito
                carrito.Rows.Add(
                    filaProducto["ProductoID"],
                    filaProducto["NombreProducto"],
                    cantidad,
                    filaProducto["PrecioVenta"]
                );

                ActualizarTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        // --- 8. Método para calcular y mostrar los totales ---
        private void ActualizarTotales()
        {
            // Calculamos el subtotal sumando la columna "SubTotal" del carrito
            decimal subtotal = 0;
            if (carrito.Rows.Count > 0)
            {
                subtotal = (decimal)carrito.Compute("SUM(SubTotal)", "");
            }

            // Obtenemos el descuento
            decimal descuento = numDescuento.Value;

            // Calculamos el total
            decimal total = subtotal - (subtotal * (descuento / 100));

            // Mostramos en formato de moneda
            txtSubtotal.Text = subtotal.ToString("C2");
            txtTotal.Text = total.ToString("C2");
        }

        // --- 9. Evento si el usuario cambia el descuento ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void numDescuento_ValueChanged(object sender, EventArgs e)
        {
            ActualizarTotales(); // Recalculamos
        }

        // --- 10. Botón "Finalizar Venta" ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (idPacienteSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un paciente para la venta.", "Error");
                return;
            }
            if (carrito.Rows.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.", "Error");
                return;
            }

            // Confirmación
            if (MessageBox.Show($"Total a pagar: {txtTotal.Text}\n¿Desea finalizar la venta?", "Confirmar Venta", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                // 1. Guardar la Venta principal
                decimal totalVenta = decimal.Parse(txtTotal.Text, System.Globalization.NumberStyles.Currency);
                string metodoPago = cmbMetodoPago.Text;

                // Usamos la consulta que creamos (InsertarVenta)
                // Nos devuelve el ID de la venta recién creada
                int nuevaVentaID = (int)adaptadorVentas.InsertarVenta(
                    (int)idPacienteSeleccionado,
                    idUsuarioActual, // El ID del vendedor (hardcodeado por ahora)
                    metodoPago,
                    totalVenta
                );

                // 2. Guardar el Detalle de la Venta (el carrito)
                bool necesitaOrdenTrabajo = false; // Bandera para el Módulo 4

                foreach (DataRow filaCarrito in carrito.Rows)
                {
                    int productoID = (int)filaCarrito["ProductoID"];
                    int cantidad = (int)filaCarrito["Cantidad"];
                    decimal precio = (decimal)filaCarrito["PrecioUnitario"];

                    // Usamos la consulta que creamos
                    adaptadorDetalle.InsertarDetalle(nuevaVentaID, productoID, cantidad, precio);

                    // 3. Descontar el Stock (Módulo 2)
                    // Usamos la consulta de UPDATE que creamos
                    adaptadorProductos.ActualizarStock(cantidad, productoID);

                    // 4. Verificamos si necesita Orden de Trabajo (Módulo 4)
                    // (Asumimos que la CategoriaID 2 = "Lentes de Contacto" o algo que requiera receta)
                    // Puedes cambiar este '2' por el ID de categoría correcto
                    var producto = (adaptadorProductos.GetDataByProductoID(productoID))[0];
                    if ((int)producto["CategoriaID"] == 2)
                    {
                        necesitaOrdenTrabajo = true;
                    }
                }

                // 5. Crear Orden de Trabajo (Módulo 4)
                if (necesitaOrdenTrabajo)
                {
                    // Usamos la consulta que creamos
                    adaptadorOrdenes.CrearOrdenDeTrabajo(nuevaVentaID);
                }

                // 6. Limpieza y Éxito
                MessageBox.Show($"¡Venta #{nuevaVentaID} finalizada con éxito!", "Venta Completada");
                LimpiarFormularioVenta();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico al finalizar la venta: " + ex.Message);
            }
        }

        // --- 11. Método para limpiar todo el formulario ---
        private void LimpiarFormularioVenta()
        {
            idPacienteSeleccionado = null;
            lblNombrePaciente.Text = "Cliente: (Ninguno)";
            txtBuscarPaciente.Text = "";

            carrito.Clear(); // Vacía el DataGridView
            ActualizarTotales(); // Pone los totales en $0.00

            numDescuento.Value = 0;
            numCantidad.Value = 1;
            cmbMetodoPago.SelectedIndex = 0;
            CargarProductosEnComboBox(); // Recarga los productos (con el stock actualizado)
        }

        // --- Tus eventos fantasma (los dejamos para que el diseñador no falle) ---
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        // ---- FIN DE CÓDIGO NUEVO ----
    }
}