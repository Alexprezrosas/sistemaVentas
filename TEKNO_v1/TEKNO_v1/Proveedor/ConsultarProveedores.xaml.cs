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

namespace TEKNO_v1.Proveedor
{
    /// <summary>
    /// Lógica de interacción para ConsultarProveedores.xaml
    /// </summary>
    public partial class ConsultarProveedores : Window
    {
        public ConsultarProveedores()
        {
            InitializeComponent();
            vistaGrid();
        }

        void vistaGrid()
        {
            radGridProveedores.ItemsSource = (from pr in BaseDatos.GetBaseDatos().proveedors
                                            where pr.status == "Activo"
                                            select new
                                            {
                                                id_proveedor = pr.id_proveedor,
                                                nombre = pr.nombre + " " + pr.ap_paterno + " " + pr.ap_materno,
                                                rfc = pr.RFC,
                                                direccion = pr.direccion,
                                                usuario = pr.usuario.nombre + " " + pr.usuario.ap_paterno + " " + pr.usuario.ap_materno
                                            });
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridProveedores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un proveedor");
            }
            else
            {
                dynamic prov = radGridProveedores.SelectedItem;
                int idp = prov.id_proveedor;
                var proveedor = BaseDatos.GetBaseDatos().proveedors.Find(idp);
                RegistrarProveedor rp = new RegistrarProveedor(proveedor);
                rp.Show();
                this.Close();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridProveedores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un proveedor");
            }
            else
            {
                dynamic prov = radGridProveedores.SelectedItem;
                int idp = prov.id_proveedor;
                var proveedor = BaseDatos.GetBaseDatos().proveedors.Find(idp);
                proveedor.status = "Inactivo";
                BaseDatos.GetBaseDatos().SaveChanges();
                MessageBox.Show("Se elimino correctamente");
                vistaGrid();
            }
        }
    }
}