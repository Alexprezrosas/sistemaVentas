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
    /// Lógica de interacción para ConsultarCompras.xaml
    /// </summary>
    public partial class ConsultarCompras : Window
    {
        public ConsultarCompras()
        {
            InitializeComponent();
            vistaGrid();
        }

        void vistaGrid()
        {
            radGridCompras.ItemsSource = (from co in BaseDatos.GetBaseDatos().compras
                                         select new
                                         {
                                             id_venta = co.id_compra,
                                             proveedor = co.proveedor.nombre + " " + co.proveedor.ap_paterno + " " + co.proveedor.ap_materno,
                                             total = co.total,
                                             fecha = co.fecha_hora,
                                             usuario = co.usuario.nombre + " " + co.usuario.ap_paterno + " " + co.usuario.ap_materno
                                         });
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridCompras.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una compra");
            }
            else
            {
                dynamic vent = radGridCompras.SelectedItem;
                int idc = vent.id_compra;
                var compra = BaseDatos.GetBaseDatos().compras.Find(idc);
                NuevaCompra rv = new NuevaCompra(compra);
                rv.Show();
                this.Close();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridCompras.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una compra");
            }
            else
            {
                dynamic comp = radGridCompras.SelectedItem;
                int idc = comp.id_compra;
                var compra = BaseDatos.GetBaseDatos().compras.Find(idc);
                BaseDatos.GetBaseDatos().compras.Remove(compra);
                BaseDatos.GetBaseDatos().SaveChanges();
                MessageBox.Show("Se elimino correctamente");
                vistaGrid();
            }
        }
    }
}

