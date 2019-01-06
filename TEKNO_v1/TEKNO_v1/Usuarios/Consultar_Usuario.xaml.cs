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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.GridView.SearchPanel;

namespace TEKNO_v1.Usuarios
{
    /// <summary>
    /// Lógica de interacción para Consultar_Usuario.xaml
    /// </summary>
    public partial class Consultar_Usuario : Window
    {
        public Consultar_Usuario()
        {
            InitializeComponent();
            //radUsuario.ItemsSource = BaseDatos.GetBaseDatos().usuarios.ToList();
            ConsultaUsu();
            radUsuario.SearchPanelVisibilityChanged += RadGridView_SearchPanelVisibilityChanged;
        }

        private void RadGridView_SearchPanelVisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            if (e.NewVisibility == Visibility.Collapsed)
            {
                var clearSearchValue = GridViewSearchPanelCommands.ClearSearchValue as RoutedUICommand;
                clearSearchValue.Execute(null, radUsuario.ChildrenOfType<GridViewSearchPanel>().FirstOrDefault());
            }
        }

        void ConsultaUsu()
        {
            radUsuario.ItemsSource = (from us in BaseDatos.GetBaseDatos().usuarios
                                      where us.status == "Activo"
                                      select new
                                      {
                                          id_usuario = us.id_usuario,
                                          nombre = us.nombre,
                                          ap_paterno = us.ap_paterno,
                                          ap_materno = us.ap_materno,
                                          password = us.password,
                                          puesto = us.puesto
                                      });
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (radUsuario.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un registro");
            }else
            {
                dynamic us = radUsuario.SelectedItem;
                int idu = us.id_usuario;
                var uss = BaseDatos.GetBaseDatos().usuarios.Find(idu);
                Registrar_Usuario ru = new Registrar_Usuario(uss);
                ru.Show();
                radUsuario.SelectedItem = null;
                this.Close();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (radUsuario.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un registro");
            }
            else
            {
                dynamic us = radUsuario.SelectedItem;
                int idu = us.id_usuario;
                var usua = BaseDatos.GetBaseDatos().usuarios.Find(idu);
                usua.status = "Inactivo";
                BaseDatos.GetBaseDatos().SaveChanges();
                radUsuario.SelectedItem = null;
                MessageBox.Show("El registro se elimino correctamente");
                ConsultaUsu();
            }
        }
    }
}
