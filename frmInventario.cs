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
    public partial class frmInventario : Form
    {
        // 1. Instanciamos los adaptadores que usaremos
        ProductosTableAdapter adaptadorProductos = new ProductosTableAdapter();
        CategoriasTableAdapter adaptadorCategorias = new CategoriasTableAdapter();

        // Variable para guardar el ID del producto que seleccionemos
        int? idProductoSeleccionado = null;

        public frmInventario()
        {
            InitializeComponent();
        }

        // --- 2. Evento Load: Se ejecuta cuando se abre el formulario ---
        private void frmInventario_Load(object sender, EventArgs e)
        {
            // Cargamos el listado de productos y las categorías
            CargarInventario();
            CargarCategorias();
        }

        // --- 3. Métodos para cargar datos ---
        private void CargarInventario()
        {
            try
            {
                // Llena el DataGridView con los productos
                dgvInventario.DataSource = adaptadorProductos.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message);
            }
        }

        private void CargarCategorias()
        {
            try
            {
                // Llena el ComboBox con las categorías
                cmbCategoria.DataSource = adaptadorCategorias.GetData();
                cmbCategoria.DisplayMember = "NombreCategoria"; // Lo que ve el usuario
                cmbCategoria.ValueMember = "CategoriaID"; // El valor interno (ID)
                cmbCategoria.SelectedIndex = -1; // Para que no aparezca nada seleccionado
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message);
            }
        }

        // --- 4. Método para limpiar los campos ---
        private void LimpiarCampos()
        {
            idProductoSeleccionado = null; // Limpiamos el ID
            cmbCategoria.SelectedIndex = -1;
            txtNombreProducto.Text = ""; // <--- AÑADIDO
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtColor.Text = "";
            txtMaterial.Text = "";
            txtTamaño.Text = "";
            numPrecioVenta.Value = 0;
            numCantidadActual.Value = 0;
            numStockMinimo.Value = 0;
        }

        // --- 5. Evento CellClick: Cargar datos al seleccionar en el grid ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void dgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvInventario.Rows[e.RowIndex];

                try
                {
                    // Guardamos el ID
                    idProductoSeleccionado = Convert.ToInt32(fila.Cells["ProductoID"].Value);

                    // Llenamos los campos
                    cmbCategoria.SelectedValue = fila.Cells["CategoriaID"].Value;

                    // --- CORREGIDO ---
                    txtNombreProducto.Text = fila.Cells["NombreProducto"].Value.ToString();

                    txtMarca.Text = fila.Cells["Marca"].Value.ToString();
                    txtModelo.Text = fila.Cells["Modelo"].Value.ToString();
                    txtColor.Text = fila.Cells["Color"].Value.ToString();
                    txtMaterial.Text = fila.Cells["Material"].Value.ToString();
                    txtTamaño.Text = fila.Cells["Tamano"].Value.ToString();
                    numPrecioVenta.Value = Convert.ToDecimal(fila.Cells["PrecioVenta"].Value);
                    numCantidadActual.Value = Convert.ToDecimal(fila.Cells["CantidadActual"].Value);
                    numStockMinimo.Value = Convert.ToDecimal(fila.Cells["StockMinimo"].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al seleccionar el producto: " + ex.Message);
                    LimpiarCampos();
                }
            }
        }

        // --- 6. Botones de Acción ---
        // (Recuerda conectar este evento en el diseñador ⚡)
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // El botón "Nuevo" es para INSERTAR
            try
            {
                // --- CORREGIDO ---
                string nombre = txtNombreProducto.Text;
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                string color = txtColor.Text;
                string material = txtMaterial.Text;
                string tamaño = txtTamaño.Text;
                int categoriaID = (int)cmbCategoria.SelectedValue;
                decimal precio = numPrecioVenta.Value;
                int cantidad = (int)numCantidadActual.Value;
                int stockMin = (int)numStockMinimo.Value;

                // --- CORREGIDO (añadimos 'nombre') ---
                adaptadorProductos.InsertarProducto(categoriaID, nombre, marca, modelo, color, material, tamaño, precio, cantidad, stockMin);

                MessageBox.Show("Producto guardado exitosamente.");
                CargarInventario(); // Recargamos el grid
                LimpiarCampos(); // Limpiamos los campos
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el producto: " + ex.Message);
            }
        }

        // (Recuerda conectar este evento en el diseñador ⚡)
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // El botón "Actualizar" es para MODIFICAR
            if (idProductoSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un producto de la lista para actualizar.");
                return;
            }

            try
            {
                // --- CORREGIDO ---
                string nombre = txtNombreProducto.Text;
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                string color = txtColor.Text;
                string material = txtMaterial.Text;
                string tamaño = txtTamaño.Text;
                int categoriaID = (int)cmbCategoria.SelectedValue;
                decimal precio = numPrecioVenta.Value;
                int cantidad = (int)numCantidadActual.Value;
                int stockMin = (int)numStockMinimo.Value;
                int idOriginal = (int)idProductoSeleccionado;

                // --- CORREGIDO (añadimos 'nombre') ---
                adaptadorProductos.ActualizarProducto(categoriaID, nombre, marca, modelo, color, material, tamaño, precio, cantidad, stockMin, idOriginal);

                MessageBox.Show("Producto actualizado exitosamente.");
                CargarInventario();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el producto: " + ex.Message);
            }
        }

       
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un producto de la lista para eliminar.");
                return;
            }

            if (MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    adaptadorProductos.EliminarProducto((int)idProductoSeleccionado);
                    MessageBox.Show("Producto eliminado.");
                    CargarInventario();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el producto: " + ex.Message);
                }
            }
        }

        // (Recuerda conectar este evento en el diseñador ⚡)
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }


        // --- Tus eventos fantasma (los dejamos para que el diseñador no falle) ---
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}