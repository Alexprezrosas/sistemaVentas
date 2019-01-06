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

namespace TEKNO_v1.Clientes
{
    /// <summary>
    /// Lógica de interacción para ConsultarClientesxaml.xaml
    /// </summary>
    public partial class ConsultarClientesxaml : Window
    {
        int idus;
        public ConsultarClientesxaml()
        {
            InitializeComponent();
            vistaGrid();
        }

        public ConsultarClientesxaml(int idusuario)
        {
            InitializeComponent();
            vistaGrid();
            idus = idusuario;
        }

        void vistaGrid()
        {
            radGridClientes.ItemsSource = (from cl in BaseDatos.GetBaseDatos().clientes
                                           join esc in BaseDatos.GetBaseDatos().escuelas
                                           on cl.escuela_id equals esc.id_escuela
                                           where cl.estatus == "Activo"
                                           select new
                                           {
                                               id_cliente = cl.id_cliente,
                                               id_escuela = esc.id_escuela,
                                               nombre = cl.nombre,
                                               ap_paterno = cl.ap_paterno,
                                               ap_materno = cl.ap_materno,
                                               correoelectronico = cl.correoelectronico,
                                               telefono = cl.telefono,
                                               escuela = esc.nombre
                                           });
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridClientes.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un rregistro");
            }else
            {
                dynamic client = radGridClientes.SelectedItem;
                int idcl = client.id_cliente;
                int idesc = client.id_escuela;
                var clie = BaseDatos.GetBaseDatos().clientes.Find(idcl);
                var escu = BaseDatos.GetBaseDatos().escuelas.Find(idesc);
                Registro_Clientes rc = new Registro_Clientes(clie, escu, idus);
                rc.Show();
                this.Close();
                
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (radGridClientes.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un rregistro");
            }
            else
            {
                dynamic client = radGridClientes.SelectedItem;
                int idcl = client.id_cliente;
                var clie = BaseDatos.GetBaseDatos().clientes.Find(idcl);
                clie.estatus = "Inactivo";
                BaseDatos.GetBaseDatos().SaveChanges();
                MessageBox.Show("Se elimino el registro");
                vistaGrid();
            }
        }
    }
}
