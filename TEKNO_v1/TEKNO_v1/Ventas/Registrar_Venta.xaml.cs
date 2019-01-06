using AccesoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TEKNO_v1.Ventas
{
    /// <summary>
    /// Lógica de interacción para Registrar_Venta.xaml
    /// </summary>
    public partial class Registrar_Venta : Window
    {
        int idv;
        Decimal totalven = 0;
        int idus;
        public Registrar_Venta()
        {
            InitializeComponent();
            autocompletes();
        }

        public Registrar_Venta(int idusuario)
        {
            InitializeComponent();
            autocompletes();
            idus = idusuario;
        }

        public Registrar_Venta(venta ve)
        {
            InitializeComponent();
            autocompletes();
            idv = ve.id_venta;
            VistaGrid();
            autoCliente.SearchText = ve.cliente.nombre + " " + ve.cliente.ap_paterno + " " + ve.cliente.ap_materno;
            autoCliente.IsEnabled = false;
            autoProducto.IsEnabled = true;
            txtCantidad.IsEnabled = true;
            btnNuevaVenta.IsEnabled = false;
            btnAgregar.IsEnabled = true;
            btnFinalizar.IsEnabled = true;
            totalven = ve.total;
            txtTotal.Text = totalven.ToString();
        }

        void VistaGrid()
        {
            radGridVenta.ItemsSource = (from v in BaseDatos.GetBaseDatos().ventas
                                        join dv in BaseDatos.GetBaseDatos().detalle_venta
                                        on v.id_venta equals dv.venta_id
                                        join pr in BaseDatos.GetBaseDatos().productoes
                                        on dv.producto_id equals pr.id_producto
                                        where v.id_venta == idv
                                        select new
                                        {
                                            id_venta = v.id_venta,
                                            id_detalleventa = dv.id_detallevta,
                                            id_producto = pr.id_producto,
                                            nombre = pr.nombre + " " + pr.descripcion + " " + pr.marca,
                                            cantidad = dv.cantidad,
                                            precio = pr.precioprofesor,
                                            subtotal = dv.subtotal
                                        });
        }

        void autocompletes()
        {
            autoCliente.ItemsSource = (from cl in BaseDatos.GetBaseDatos().clientes
                                       where cl.estatus == "Activo"
                                       select new
                                       {
                                           id_cliente = cl.id_cliente,
                                           nombre = cl.nombre + " " + cl.ap_paterno + " " + cl.ap_materno
                                       });
            autoProducto.ItemsSource = (from pr in BaseDatos.GetBaseDatos().productoes
                                        where pr.estatus == "Activo"
                                        select new
                                        {
                                            id_producto = pr.id_producto,
                                            nombre = pr.nombre + " " + pr.descripcion + " " + pr.marca,
                                            precio = pr.precioprofesor,
                                            existencias = pr.existencias
                                        });
        }

        void NuevaVenta()
        {
            if (autoCliente.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un cliente");
            }else
            {
                dynamic cl = autoCliente.SelectedItem;
                int idc = cl.id_cliente;
                venta v = new venta
                {
                    cliente_id = idc,
                    fecha_hora = DateTime.Now,
                    usuario_id = idus,
                    total = 0
                };
                BaseDatos.GetBaseDatos().ventas.Add(v);
                BaseDatos.GetBaseDatos().SaveChanges();
                idv = v.id_venta;
                autoProducto.IsEnabled = true;
                txtCantidad.IsEnabled = true;
                btnAgregar.IsEnabled = true;
                btnFinalizar.IsEnabled = true;
                autoCliente.IsEnabled = false;
                btnNuevaVenta.IsEnabled = false;
            }

            
        }

        void GuardarDetalle()
        {
            if (autoProducto.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un prodcuto");
            }else
            {
                if (txtCantidad.Text == "")
                {
                    MessageBox.Show("Ingresa la cantidad del producto");
                }else
                {

                    dynamic producto = autoProducto.SelectedItem;
                    if (producto.existencias > 0)
                    {
                        if (producto.existencias > Int32.Parse(txtCantidad.Text))
                        {
                            int idp = producto.id_producto;
                            Decimal precio = producto.precio;
                            detalle_venta dv = new detalle_venta
                            {
                                venta_id = idv,
                                producto_id = idp,
                                cantidad = Int32.Parse(txtCantidad.Text),
                                precio = precio,
                                subtotal = Int32.Parse(txtCantidad.Text) * precio
                            };
                            BaseDatos.GetBaseDatos().detalle_venta.Add(dv);
                            BaseDatos.GetBaseDatos().SaveChanges();

                            var prd = BaseDatos.GetBaseDatos().productoes.Find(idp);
                            prd.existencias = prd.existencias - Int32.Parse(txtCantidad.Text);
                            BaseDatos.GetBaseDatos().SaveChanges();

                            totalven = totalven + (Int32.Parse(txtCantidad.Text) * precio);
                            txtTotal.Text = totalven.ToString();
                            autoProducto.SelectedItem = null;
                            VistaGrid();
                            Limpiardetalle();
                        }else
                        {
                            MessageBox.Show("Solo cuentas con: " + producto.existencias + "existencias");
                            txtCantidad.Text = String.Empty;
                        }
                    }else
                    {
                        MessageBox.Show("No cuentas con existencias de: " + producto.nombre);
                        txtCantidad.Text = String.Empty;
                        autoProducto.SelectedItem = null;
                        autoProducto.SearchText = String.Empty;
                        txtPrecio.Text = String.Empty;
                    }
                }
            }
        }

        void Limpiardetalle()
        {
            autoProducto.SearchText = String.Empty;
            txtPrecio.Text = String.Empty;
            txtCantidad.Text = String.Empty;
        }

        void LimpiarVentaGeneral()
        {
            autoCliente.SelectedItem = null;
            autoCliente.SearchText = String.Empty;
            txtTotal.Text = String.Empty;
            Limpiardetalle();
            idv = 0;
            VistaGrid();
            totalven = 0;
            autoCliente.IsEnabled = true;
            autoProducto.IsEnabled = false;
            txtCantidad.IsEnabled = false;
            btnNuevaVenta.IsEnabled = true;
            btnAgregar.IsEnabled = false;
            btnFinalizar.IsEnabled = false;
        }

        void Finalizar()
        {
            var venta = BaseDatos.GetBaseDatos().ventas.Find(idv);
            venta.total = totalven;
            BaseDatos.GetBaseDatos().SaveChanges();
            LimpiarVentaGeneral();
        }

        private void validarLetras(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)

                e.Handled = false;

            else e.Handled = true;
        }

        private void validarNumeros(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 4 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }

        private void validarNumLetras(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 90 || ascci >= 97 && ascci <= 122 || ascci == 46)

                e.Handled = false;

            else e.Handled = true;
        }

        private void validarDecimal(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else if (e.Key == Key.Decimal)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void autoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoProducto.SelectedItem != null)
            {
                dynamic produc = autoProducto.SelectedItem;
                int idp = produc.id_producto;
                var prod = BaseDatos.GetBaseDatos().productoes.Find(idp);
                txtPrecio.Text = prod.precioprofesor.ToString();
            }
        }

        private void btnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            NuevaVenta();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            GuardarDetalle();
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            Finalizar();
        }
    }
}
