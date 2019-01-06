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

namespace TEKNO_v1.Login
{
    /// <summary>
    /// Lógica de interacción para IniciarSesion.xaml
    /// </summary>
    public partial class IniciarSesion : Window
    {
        public IniciarSesion()
        {
            InitializeComponent();
        }

        void IniciarS()
        {
            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Ingresa el usuario");
            }
            else
            {
                if (passwordBox.Password == "")
                {
                    MessageBox.Show("Ingresa la contraseña");
                }
                else
                {

                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (txtUsuario.Text != string.Empty && passwordBox.Password != string.Empty)
            {
                var usu = BaseDatos.GetBaseDatos().usuarios.FirstOrDefault(a => a.nombre.Equals(txtUsuario.Text) && a.password.Equals(passwordBox.Password) && a.status == "Activo");
                if (usu != null)
                {
                    if (usu.password.Equals(passwordBox.Password))
                    {
                        String puesto = usu.puesto;
                        int idu = usu.id_usuario;
                        MainWindow main = new MainWindow(puesto, idu);
                        main.Show();
                        this.Close();
                    }
                    else
                        MessageBox.Show("Datos incorrectos");
                }
                else
                    MessageBox.Show("Datos incorrectos");
            }
            else
                MessageBox.Show("Favor de llenar los campos");


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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
