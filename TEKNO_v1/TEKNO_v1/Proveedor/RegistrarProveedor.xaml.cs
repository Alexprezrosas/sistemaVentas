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

namespace TEKNO_v1.Proveedor
{
    /// <summary>
    /// Lógica de interacción para RegistrarProveedor.xaml
    /// </summary>
    public partial class RegistrarProveedor : Window
    {
        int idp;
        int idus;
        public RegistrarProveedor()
        {
            InitializeComponent();
        }

        public RegistrarProveedor(int idusuario)
        {
            InitializeComponent();
            idus = idusuario;
        }

        public RegistrarProveedor(proveedor pro)
        {
            InitializeComponent();
            idp = pro.id_proveedor;
            txtNombre.Text = pro.nombre;
            txtPaterno.Text = pro.ap_paterno;
            txtMaterno.Text = pro.ap_materno;
            txtRFC.Text = pro.RFC;
            txtDireccion.Text = pro.direccion;

            btnGuardar.Visibility = Visibility.Hidden;
            btnActualizar.Visibility = Visibility.Visible;
        }

        void Limpiar()
        {
            txtNombre.Text = String.Empty;
            txtPaterno.Text = String.Empty;
            txtMaterno.Text = String.Empty;
            txtRFC.Text = String.Empty;
            txtDireccion.Text = String.Empty;
        }

        void Guardar()
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingresa el nombre del proveedor");
            }else
            {
                if (txtPaterno.Text == "")
                {
                    MessageBox.Show("Ingresa el apellido paterno");
                }else
                {
                    if (txtMaterno.Text == "")
                    {
                        MessageBox.Show("Ingresa el apellido materno");
                    }else
                    {
                        if (txtRFC.Text == "")
                        {
                            MessageBox.Show("Ingresa el RFC");
                        }else
                        {
                            if (txtDireccion.Text == "")
                            {
                                MessageBox.Show("Ingresa la direccion");
                            }else
                            {
                                proveedor p = new proveedor
                                {
                                    nombre = txtNombre.Text,
                                    ap_paterno = txtPaterno.Text,
                                    ap_materno = txtMaterno.Text,
                                    RFC = txtRFC.Text,
                                    direccion = txtDireccion.Text,
                                    usuario_id = idus,
                                    status = "Activo"
                                };
                                BaseDatos.GetBaseDatos().proveedors.Add(p);
                                BaseDatos.GetBaseDatos().SaveChanges();
                                Limpiar();
                            }
                        }
                    }
                }
            }
        }

        void Actualizar()
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingresa el nombre del proveedor");
            }
            else
            {
                if (txtPaterno.Text == "")
                {
                    MessageBox.Show("Ingresa el apellido paterno");
                }
                else
                {
                    if (txtMaterno.Text == "")
                    {
                        MessageBox.Show("Ingresa el apellido materno");
                    }
                    else
                    {
                        if (txtRFC.Text == "")
                        {
                            MessageBox.Show("Ingresa el RFC");
                        }
                        else
                        {
                            if (txtDireccion.Text == "")
                            {
                                MessageBox.Show("Ingresa la direccion");
                            }
                            else
                            {
                                var proveedor = BaseDatos.GetBaseDatos().proveedors.Find(idp);
                                proveedor.nombre = txtNombre.Text;
                                proveedor.ap_paterno = txtPaterno.Text;
                                proveedor.ap_materno = txtMaterno.Text;
                                proveedor.RFC = txtRFC.Text;
                                proveedor.direccion = txtDireccion.Text;
                                BaseDatos.GetBaseDatos().SaveChanges();
                                this.Close();
                                ConsultarProveedores cp = new ConsultarProveedores();
                                cp.Show();
                            }
                        }
                    }
                }
            }
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Actualizar();
        }
    }
}
