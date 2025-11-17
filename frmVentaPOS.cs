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
        OrdenesTrabajoTableAdapter adaptadorOrdenes = new OrdenesTrabajoTableAdapter();

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

        private void CargarMetodosPago()
        {
            cmbMetodoPago.Items.Add("Efectivo");
            cmbMetodoPago.Items.Add("Tarjeta");
            cmbMetodoPago.SelectedIndex = 0;
        }

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
        private void cmbProducto_DropDown(object sender, EventArgs e)
        {
            CargarProductosEnComboBox();
        }

        // --- 6. Botón "Buscar Paciente" ---
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

        // --- 7. Botón "Agregar al Carrito" ---
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un producto.", "Error");
                return;
            }

            try
            {
                int idProducto = (int)cmbProducto.SelectedValue;
                var productoTabla = adaptadorProductos.GetDataByProductoID(idProducto);

                if (productoTabla.Rows.Count == 0)
                {
                    MessageBox.Show("Error: No se encontró el producto.");
                    return;
                }

                var filaProducto = productoTabla[0];
                int cantidad = (int)numCantidad.Value;

                int stockActual = (int)filaProducto["CantidadActual"];
                if (cantidad > stockActual)
                {
                    MessageBox.Show($"No hay stock suficiente. Solo quedan {stockActual} unidades.", "Stock Insuficiente");
                    return;
                }

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
            decimal subtotal = 0;
            if (carrito.Rows.Count > 0)
            {
                subtotal = (decimal)carrito.Compute("SUM(SubTotal)", "");
            }

            decimal descuento = numDescuento.Value;
            decimal total = subtotal - (subtotal * (descuento / 100));

            txtSubtotal.Text = subtotal.ToString("C2");
            txtTotal.Text = total.ToString("C2");
        }

        // --- 9. Evento si el usuario cambia el descuento ---
        private void numDescuento_ValueChanged(object sender, EventArgs e)
        {
            ActualizarTotales(); // Recalculamos
        }

        // --- 10. Botón "Finalizar Venta" ---
        private void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
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

            if (MessageBox.Show($"Total a pagar: {txtTotal.Text}\n¿Desea finalizar la venta?", "Confirmar Venta", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            // --- Guardamos los datos ANTES de que se borren ---
            string totalRecibo = txtTotal.Text;
            string subtotalRecibo = txtSubtotal.Text;
            decimal descuentoRecibo = numDescuento.Value;
            string metodoPagoRecibo = cmbMetodoPago.Text;
            string pacienteRecibo = lblNombrePaciente.Text.Replace("Cliente: ", "");
            DataTable carritoRecibo = carrito.Copy(); // ¡Importante! Copiamos el carrito

            try
            {
                decimal totalVenta = decimal.Parse(totalRecibo, System.Globalization.NumberStyles.Currency);
                string metodoPago = metodoPagoRecibo;
                DateTime fechaVenta = DateTime.Now;

                // 1. Guardar la Venta principal
                int nuevaVentaID = (int)adaptadorVentas.InsertarVenta(
                    (int)idPacienteSeleccionado,
                    idUsuarioActual,
                    fechaVenta,
                    metodoPago,
                    totalVenta
                );

                // 2. Guardar el Detalle de la Venta (el carrito)
                bool necesitaOrdenTrabajo = false;

                foreach (DataRow filaCarrito in carrito.Rows)
                {
                    int productoID = (int)filaCarrito["ProductoID"];
                    int cantidad = (int)filaCarrito["Cantidad"];
                    decimal precio = (decimal)filaCarrito["PrecioUnitario"];

                    adaptadorDetalle.InsertarDetalle(nuevaVentaID, productoID, cantidad, precio);
                    adaptadorProductos.ActualizarStock(cantidad, productoID);

                    var producto = (adaptadorProductos.GetDataByProductoID(productoID))[0];
                    int catID = (int)producto["CategoriaID"];

                    if (catID == 2 || catID == 3) // Lentes de Contacto (2) o Solución (3)
                    {
                        necesitaOrdenTrabajo = true;
                    }
                }

                // 5. Crear Orden de Trabajo (Módulo 4)
                if (necesitaOrdenTrabajo)
                {
                    adaptadorOrdenes.CrearOrdenDeTrabajo(nuevaVentaID, fechaVenta);
                }

                // --- ¡AQUÍ ESTÁ EL CAMBIO! ---
                // 6. Limpieza y Éxito

                // En lugar de un MessageBox, mostramos el formulario de Recibo
                frmRecibo recibo = new frmRecibo(
                    carritoRecibo,
                    totalRecibo,
                    subtotalRecibo,
                    descuentoRecibo,
                    metodoPagoRecibo,
                    pacienteRecibo,
                    nuevaVentaID
                );

                recibo.MdiParent = this.MdiParent; // Para que se abra dentro del MDI
                recibo.Show();

                LimpiarFormularioVenta(); // Limpiamos el POS para la siguiente venta
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

            carrito.Clear();
            ActualizarTotales();

            numDescuento.Value = 0;
            numCantidad.Value = 1;
            cmbMetodoPago.SelectedIndex = 0;
            CargarProductosEnComboBox();
        }

        // --- Tus eventos fantasma (los dejamos para que el diseñador no falle) ---
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}