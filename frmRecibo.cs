using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing; // Necesario para la impresión
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIGO_WinForm
{
    public partial class frmRecibo : Form
    {
        // --- 1. Variables para guardar los datos de la venta ---
        private DataTable _carrito;
        private string _total;
        private string _subtotal;
        private string _descuento;
        private string _metodoPago;
        private string _nombrePaciente;
        private int _ventaID;

        // --- 2. Constructor MODIFICADO ---
        // Lo usaremos para recibir los datos desde frmVentaPOS
        public frmRecibo(DataTable carrito, string total, string subtotal, decimal descuento, string metodoPago, string nombrePaciente, int ventaID)
        {
            InitializeComponent();

            // Guardamos los datos recibidos
            this._carrito = carrito;
            this._total = total;
            this._subtotal = subtotal;
            this._descuento = descuento.ToString("F2"); // Formateamos el descuento
            this._metodoPago = metodoPago;
            this._nombrePaciente = nombrePaciente;
            this._ventaID = ventaID;
        }

        // --- 3. Evento Load: Se ejecuta cuando se abre el formulario ---
        private void frmRecibo_Load(object sender, EventArgs e)
        {
            GenerarTextoRecibo();
        }

        // --- 4. Método para crear el texto del recibo ---
        private void GenerarTextoRecibo()
        {
            // Usamos un StringBuilder para construir el texto
            StringBuilder sb = new StringBuilder();

            // Encabezado
            sb.AppendLine("         ÓPTICA SIGO - RECIBO DE VENTA");
            sb.AppendLine("-------------------------------------------------");
            sb.AppendLine($"Venta ID:     {_ventaID}");
            sb.AppendLine($"Fecha:        {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            sb.AppendLine($"Cliente:      {_nombrePaciente}");
            sb.AppendLine("-------------------------------------------------");
            sb.AppendLine("");

            // Encabezados de productos
            sb.AppendLine("Cant.  Descripción                Precio   Subtotal");
            sb.AppendLine("-------------------------------------------------");

            // 5. Recorremos el carrito y agregamos cada producto
            foreach (DataRow fila in _carrito.Rows)
            {
                string nombre = fila["NombreProducto"].ToString().PadRight(25, ' ').Substring(0, 25);
                int cantidad = (int)fila["Cantidad"];
                decimal precio = (decimal)fila["PrecioUnitario"];
                decimal subtotal = (decimal)fila["SubTotal"];

                // Formateamos cada línea
                string linea = String.Format("{0,-6} {1} {2,8} {3,10}",
                    cantidad.ToString(),
                    nombre,
                    precio.ToString("C2"),
                    subtotal.ToString("C2")
                );
                sb.AppendLine(linea);
            }

            // Totales
            sb.AppendLine("-------------------------------------------------");
            sb.AppendLine($"Subtotal:                {_subtotal,20}");
            sb.AppendLine($"Descuento ({_descuento}%):       {_descuento,20}"); // (Aquí puedes calcular el monto si quieres)
            sb.AppendLine($"TOTAL:                   {_total,20}");
            sb.AppendLine($"Método de Pago:          {_metodoPago,20}");
            sb.AppendLine("");
            sb.AppendLine("         ¡Gracias por su compra!");

            // Asignamos el texto al RichTextBox
            rtbRecibo.Text = sb.ToString();
        }

        // --- 6. Botones ---
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra este formulario de recibo
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // (La impresión real es compleja, por ahora solo mostramos un aviso)
            MessageBox.Show("¡Enviando a la impresora...!\n(Esta función es una demostración)", "Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}