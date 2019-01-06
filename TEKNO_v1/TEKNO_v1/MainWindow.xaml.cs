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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TEKNO_v1.Clientes;
using TEKNO_v1.Compras;
using TEKNO_v1.Escuelas;
using TEKNO_v1.Login;
using TEKNO_v1.Productos;
using TEKNO_v1.Proveedor;
using TEKNO_v1.Usuarios;
using TEKNO_v1.Ventas;

namespace TEKNO_v1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String puesto;
        int idus;
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(String pu, int idu)
        {
            InitializeComponent();
            puesto = pu;
            idus = idu;
            if (puesto == "Cajero")
            {
                
                menuClientes.Visibility = Visibility.Hidden;
                menuRegistrarClientes.Visibility = Visibility.Hidden;
                menuRegistrarClientes.IsEnabled = false;
                menuVisualizarClientes.Visibility = Visibility.Hidden;
                menuVisualizarClientes.IsEnabled = false;

                menuProductos.Visibility = Visibility.Hidden;
                menuRegistrarProducto.Visibility = Visibility.Hidden;
                menuRegistrarProducto.IsEnabled = false;
                menuVisualizarProductos.Visibility = Visibility.Hidden;
                menuVisualizarProductos.IsEnabled = false;

                menuProveedores.Visibility = Visibility.Hidden;
                menuRegistrarProveedores.Visibility = Visibility.Hidden;
                menuRegistrarProveedores.IsEnabled = false;
                menuVisualizarProveedores.Visibility = Visibility.Hidden;
                menuVisualizarProveedores.IsEnabled = false;

                menuEscuelas.Visibility = Visibility.Hidden;
                menuRegistrarEscuela.Visibility = Visibility.Hidden;
                menuRegistrarEscuela.IsEnabled = false;
                
                menuRegistrarUsuario.Visibility = Visibility.Hidden;
                menuRegistrarUsuario.IsEnabled = false;
                menuVisualizarUsuarios.Visibility = Visibility.Hidden;
                menuVisualizarUsuarios.IsEnabled = false;

                menuVisualizarVentas.Visibility = Visibility.Hidden;
                menuVisualizarVentas.IsEnabled = false;
                menuVisualizarCompras.Visibility = Visibility.Hidden;
                menuVisualizarCompras.IsEnabled = false;


            }
        }

        private void RadRadialMenuItem_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            Registrar_Escuela esc = new Registrar_Escuela(idus);
            esc.Show();
        }

        private void RadRadialMenuItem_Click_1(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            Registrar_Usuario ru = new Registrar_Usuario();
            ru.Show();
        }

        private void RadRadialMenuItem_Click_2(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            Consultar_Usuario usu = new Consultar_Usuario();
            usu.Show();
        }

        private void RadRadialMenuItem_Click_3(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ConsultarEscuelas ce = new ConsultarEscuelas();
            ce.Show();
        }

        private void RadRadialMenuItem_Click_4(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RegistrarProducto rp = new RegistrarProducto(idus);
            rp.Show();
        }

        private void RadRadialMenuItem_Click_5(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ConsultarProductos cp = new ConsultarProductos();
            cp.Show();
        }

        private void RadRadialMenuItem_Click_6(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            Registro_Clientes rc = new Registro_Clientes(idus);
            rc.Show();
        }

        private void RadRadialMenuItem_Click_7(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ConsultarClientesxaml cc = new ConsultarClientesxaml(idus);
            cc.Show();
        }

        private void RadRadialMenuItem_Click_8(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            Registrar_Venta rv = new Registrar_Venta(idus);
            rv.Show();
        }

        private void RadRadialMenuItem_Click_9(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ConsultarVentas cv = new ConsultarVentas();
            cv.Show();
        }

        private void RadRadialMenuItem_Click_10(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            NuevaCompra nc = new NuevaCompra(idus);
            nc.Show();
        }

        private void RadRadialMenuItem_Click_11(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ConsultarCompras cc = new ConsultarCompras();
            cc.Show();
        }

        private void RadRadialMenuItem_Click_12(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RegistrarProveedor rp = new RegistrarProveedor(idus);
            rp.Show();
        }

        private void RadRadialMenuItem_Click_13(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ConsultarProveedores cp = new ConsultarProveedores();
            cp.Show();
        }

        private void RadRadialMenuItem_Click_14(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            IniciarSesion ise = new IniciarSesion();
            ise.Show();
            this.Close();
        }

        private void menuRegistrarVentasPublico_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RegistroVentasPublico rp = new RegistroVentasPublico(idus);
            rp.Show();
        }
    }
}
