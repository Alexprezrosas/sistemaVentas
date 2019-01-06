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

namespace TEKNO_v1.Escuelas
{
    /// <summary>
    /// Lógica de interacción para ConsultarEscuelas.xaml
    /// </summary>
    public partial class ConsultarEscuelas : Window
    {
        public ConsultarEscuelas()
        {
            InitializeComponent();
            vistaGrid();
        }

        void vistaGrid()
        {
            radGridEscuelas.ItemsSource = (from esc in BaseDatos.GetBaseDatos().escuelas
                                           where esc.estatus == "Activo"
                                           select new
                                           {
                                               id_escuela = esc.id_escuela,
                                               nombre = esc.nombre,
                                               direccion = esc.direccion
                                           });
        }

        void Editar()
        {
            if (radGridEscuelas.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una escuela");
            }else
            {
                dynamic esc = radGridEscuelas.SelectedItem;
                int ide = esc.id_escuela;
                var escu = BaseDatos.GetBaseDatos().escuelas.Find(ide);
                Registrar_Escuela re = new Registrar_Escuela(escu);
                re.Show();
                this.Close();
            }
        }

        void Eliminar()
        {
            if (radGridEscuelas.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una escuela");
            }else
            {
                dynamic esc = radGridEscuelas.SelectedItem;
                int ide = esc.id_escuela;
                var escu = BaseDatos.GetBaseDatos().escuelas.Find(ide);
                escu.estatus = "Inactivo";
                BaseDatos.GetBaseDatos().SaveChanges();
                MessageBox.Show("Se elimino el registro");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }
    }
}
