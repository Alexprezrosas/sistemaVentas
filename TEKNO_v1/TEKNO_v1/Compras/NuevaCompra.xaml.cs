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

namespace TEKNO_v1.Compras
{
    /// <summary>
    /// Lógica de interacción para NuevaCompra.xaml
    /// </summary>
    public partial class NuevaCompra : Window
    {
        int idc;
        Decimal tot;
        int idu;
        public NuevaCompra()
        {
            InitializeComponent();
            llenarautocompletes();
        }

        public NuevaCompra(int idusuario)
        {
            InitializeComponent();
            llenarautocompletes();
            idu = idusuario;
        }

        public NuevaCompra(compra c)
        {
            InitializeComponent();
            llenarautocompletes();

            idc = c.id_compra;
            vistaGrid();

            autoProveedor.SearchText = c.proveedor.nombre + " " + c.proveedor.ap_paterno + " " + c.proveedor.ap_materno;
            autoProveedor.IsEnabled = false;
            autoProducto.IsEnabled = true;
            txtCantidad.IsEnabled = true;
            btnNuevaCompra.IsEnabled = false;
            btnAgregar.IsEnabled = true;
            btnFinalizar.IsEnabled = true;
            tot = c.total;
            txtTotal.Text = c.total.ToString();
        }

        void llenarautocompletes()
        {
            autoProveedor.ItemsSource = (from pr in BaseDatos.GetBaseDatos().proveedors
                                         where pr.status == "Activo"
                                         select new
                                         {
                                             id_proveedor = pr.id_proveedor,
                                             nombre = pr.nombre + " " + pr.ap_paterno + " " + pr.ap_materno + " " + pr.RFC
                                         });

            autoProducto.ItemsSource = (from pr in BaseDatos.GetBaseDatos().productoes
                                        where pr.estatus == "Activo"
                                        select new
                                        {
                                            id_producto = pr.id_producto,
                                            nombre = pr.nombre + " " + pr.descripcion + " " + pr.marca,
                                            existencias = pr.existencias,
                                            costo = pr.preciocompra
                                        });
        }

        void vistaGrid()
        {
            radGridCompra.ItemsSource = (from c in BaseDatos.GetBaseDatos().compras
                                         join dc in BaseDatos.GetBaseDatos().detalle_compra
                                         on c.id_compra equals dc.compra_id
                                         join pr in BaseDatos.GetBaseDatos().productoes
                                         on dc.producto_id equals pr.id_producto
                                         where c.id_compra == idc
                                         select new
                                         {
                                             id_compra = c.id_compra,
                                             id_detalle_compra = dc.id_detallecpra,
                                             id_producto = pr.id_producto,
                                             producto = pr.nombre + " " + pr.descripcion + " " + pr.marca,
                                             cantidad = dc.cantidad,
                                             precio = pr.preciocompra,
                                             subtotal = dc.subtotal
                                         });
        }

        void NuevaCom()
        {
            if (autoProveedor.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un proveedor");
            }else
            {
                dynamic prov = autoProveedor.SelectedItem;
                int idp = prov.id_proveedor;
                compra co = new compra
                {
                    fecha_hora = DateTime.Now,
                    proveedor_id = idp,
                    usuario_id = idu,
                    total = 0
                };
                BaseDatos.GetBaseDatos().compras.Add(co);
                BaseDatos.GetBaseDatos().SaveChanges();
                idc = co.id_compra;

                autoProveedor.IsEnabled = false;
                autoProducto.IsEnabled = true;
                txtCantidad.IsEnabled = true;
                btnNuevaCompra.IsEnabled = false;
                btnAgregar.IsEnabled = true;
            }
        }

        void GuardarDetalle()
        {
            if (autoProducto == null)
            {
                MessageBox.Show("Selecciona un producto");
            }else
            {
                if (txtCantidad.Text == "")
                {
                    MessageBox.Show("Ingresa la cantidad");
                }else
                {
                    if (txtPCompra.Text == "")
                    {
                        MessageBox.Show("Ingresa el costo unitario");
                    }else
                    {
                        dynamic prod = autoProducto.SelectedItem;
                        int idp = prod.id_producto;
                        Decimal cos = prod.costo;
                        detalle_compra dc = new detalle_compra
                        {
                            compra_id = idc,
                            producto_id = idp,
                            cantidad = Int32.Parse(txtCantidad.Text),
                            subtotal = Int32.Parse(txtCantidad.Text) * cos
                        };
                        BaseDatos.GetBaseDatos().detalle_compra.Add(dc);
                        BaseDatos.GetBaseDatos().SaveChanges();
                        tot = Int32.Parse(txtCantidad.Text) * cos;
                        txtTotal.Text = tot.ToString();

                        var produ = BaseDatos.GetBaseDatos().productoes.Find(idp);
                        produ.existencias = produ.existencias + Int32.Parse(txtCantidad.Text);
                        produ.preciocompra = Decimal.Parse(txtPCompra.Text);
                        BaseDatos.GetBaseDatos().SaveChanges();

                        autoProducto.SelectedItem = null;
                        autoProducto.SearchText = String.Empty;
                        txtCantidad.Text = String.Empty;
                        btnFinalizar.IsEnabled = true;
                        vistaGrid();
                    }
                }
            }
        }

        void FinalizarCompra()
        {
            var comp = BaseDatos.GetBaseDatos().compras.Find(idc);
            comp.total = tot;
            BaseDatos.GetBaseDatos().SaveChanges();

            autoProveedor.SelectedItem = null;
            autoProveedor.SearchText = String.Empty;
            autoProveedor.IsEnabled = true;
            autoProducto.SelectedItem = null;
            autoProducto.SearchText = String.Empty;
            autoProducto.IsEnabled = false;
            txtCantidad.Text = String.Empty;
            txtCantidad.IsEnabled = false;
            btnNuevaCompra.IsEnabled = true;
            btnAgregar.IsEnabled = false;
            btnFinalizar.IsEnabled = false;
            idc = 0;
            vistaGrid();
            txtTotal.Text = String.Empty;
            tot = 0;
            txtPCompra.Text = String.Empty;
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

        private void btnNuevaCompra_Click(object sender, RoutedEventArgs e)
        {
            NuevaCom();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            GuardarDetalle();
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            FinalizarCompra();
        }

        private void autoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoProducto.SelectedItem != null)
            {
                dynamic prod = autoProducto.SelectedItem;
                int idp = prod.id_producto;
                var producto = BaseDatos.GetBaseDatos().productoes.Find(idp);
                txtPCompra.Text = producto.preciocompra.ToString();
            }
        }
    }
}
