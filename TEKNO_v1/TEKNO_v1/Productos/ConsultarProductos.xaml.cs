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

namespace TEKNO_v1.Productos
{
    /// <summary>
    /// Lógica de interacción para ConsultarProductos.xaml
    /// </summary>
    public partial class ConsultarProductos : Window
    {
        public ConsultarProductos()
        {
            InitializeComponent();
            vistaGrid();
        }

        void vistaGrid()
        {
            radGridProductos.ItemsSource = (from pr in BaseDatos.GetBaseDatos().productoes
                                            where pr.estatus == "Activo"
                                            select new
                                            {
                                                id_producto = pr.id_producto,
                                                nombre = pr.nombre,
                                                descripcion = pr.descripcion,
                                                marca = pr.marca,
                                                almacen = pr.almacen,
                                                pcompra = pr.preciocompra,
                                                pprofesores = pr.precioprofesor,
                                                pgeneral = pr.preciopublico,
                                                existencias = pr.existencias
                                            });
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridProductos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un producto");
            }else
            {
                dynamic prod = radGridProductos.SelectedItem;
                int idp = prod.id_producto;
                var producto = BaseDatos.GetBaseDatos().productoes.Find(idp);
                RegistrarProducto rp = new RegistrarProducto(producto);
                rp.Show();
                this.Close();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridProductos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un producto");
            }
            else
            {
                dynamic prod = radGridProductos.SelectedItem;
                int idp = prod.id_producto;
                var producto = BaseDatos.GetBaseDatos().productoes.Find(idp);
                producto.estatus = "Inactivo";
                BaseDatos.GetBaseDatos().SaveChanges();
                MessageBox.Show("Se elimino correctamente");
                vistaGrid();
            }
        }
    }
}
