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
    /// Lógica de interacción para ConsultarVentas.xaml
    /// </summary>
    public partial class ConsultarVentas : Window
    {
        public ConsultarVentas()
        {
            InitializeComponent();
            vistaGrid();
        }

        void vistaGrid()
        {
            radGridVentas.ItemsSource = (from vn in BaseDatos.GetBaseDatos().ventas
                                            select new
                                            {
                                                id_venta = vn.id_venta,
                                                cliente = vn.cliente.nombre + " " + vn.cliente.ap_paterno + " " + vn.cliente.ap_materno,
                                                total = vn.total,
                                                fecha = vn.fecha_hora,
                                                usuario = vn.usuario.nombre + " " + vn.usuario.ap_paterno + " " + vn.usuario.ap_materno
                                            });
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridVentas.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una venta");
            }
            else
            {
                dynamic vent = radGridVentas.SelectedItem;
                int idv = vent.id_venta;
                var venta = BaseDatos.GetBaseDatos().ventas.Find(idv);
                Registrar_Venta rv = new Registrar_Venta(venta);
                rv.Show();
                this.Close();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridVentas.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una venta");
            }
            else
            {
                dynamic vent = radGridVentas.SelectedItem;
                int idv = vent.id_venta;
                var venta = BaseDatos.GetBaseDatos().ventas.Find(idv);
                BaseDatos.GetBaseDatos().ventas.Remove(venta);
                BaseDatos.GetBaseDatos().SaveChanges();
                MessageBox.Show("Se elimino correctamente");
                vistaGrid();
            }
        }
    }
}

