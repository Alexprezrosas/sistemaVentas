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

namespace TEKNO_v1.Clientes
{
    /// <summary>
    /// Lógica de interacción para Registro_Clientes.xaml
    /// </summary>
    public partial class Registro_Clientes : Window
    {
        int idc, ide;
        int idus;
        public Registro_Clientes()
        {
            InitializeComponent();
            Autocomplete();
        }

        public Registro_Clientes(int idusuario)
        {
            InitializeComponent();
            Autocomplete();
            idus = idusuario;
        }

        public Registro_Clientes(cliente cl, escuela esc, int idusuario)
        {
            InitializeComponent();
            Autocomplete();
            idc = cl.id_cliente;
            ide = esc.id_escuela;
            txtCliente.Text = cl.nombre;
            txtPaterno.Text = cl.ap_paterno;
            txtMaterno.Text = cl.ap_materno;
            txtemail.Text = cl.correoelectronico;
            txtTelefono.Text = cl.telefono;
            autoEscuela.SearchText = esc.nombre;
            btnGuardar.Visibility = Visibility.Hidden;
            btnActualizar.Visibility = Visibility.Visible;
            idus = idusuario;
        }

        void Autocomplete()
        {
            autoEscuela.ItemsSource = (from esc in BaseDatos.GetBaseDatos().escuelas
                                       where esc.estatus == "Activo"
                                       select new
                                       {
                                           id_escuela = esc.id_escuela,
                                           nombre = esc.nombre,
                                           direccion = esc.direccion
                                       });
        }

        void Guardar()
        {
            if (txtCliente.Text == "")
            {
                MessageBox.Show("Ingresa el nombre del cliente");
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
                        if (txtemail.Text == "")
                        {
                            MessageBox.Show("Ingresa el e-mail");
                        }else
                        {
                            if (txtTelefono.Text == "")
                            {
                                MessageBox.Show("Ingresa un telefono");
                            }else
                            {
                                if (autoEscuela.SelectedItem == null)
                                {
                                    MessageBox.Show("Selecciona una escuela");
                                }else
                                {
                                    dynamic escu = autoEscuela.SelectedItem;
                                    int ides = escu.id_escuela;

                                    cliente cl = new cliente
                                    {
                                        nombre = txtCliente.Text,
                                        ap_paterno = txtPaterno.Text,
                                        ap_materno = txtMaterno.Text,
                                        correoelectronico = txtemail.Text,
                                        telefono = txtTelefono.Text,
                                        escuela_id = ides,
                                        usuario_id = idus,
                                        estatus = "Activo"
                                    };
                                    BaseDatos.GetBaseDatos().clientes.Add(cl);
                                    BaseDatos.GetBaseDatos().SaveChanges();
                                    Limpiar();
                                    MessageBox.Show("Se guardo el registro correctamente");
                                }
                            }
                        }
                    }
                }
            }
        }

        void Actualizar()
        {
            if (txtCliente.Text == "")
            {
                MessageBox.Show("Ingresa el nombre del cliente");
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
                        if (txtemail.Text == "")
                        {
                            MessageBox.Show("Ingresa el e-mail");
                        }
                        else
                        {
                            if (txtTelefono.Text == "")
                            {
                                MessageBox.Show("Ingresa un telefono");
                            }
                            else
                            {
                                if (autoEscuela.SelectedItem == null)
                                {
                                    MessageBox.Show("Selecciona una escuela");
                                }
                                else
                                {
                                    dynamic escu = autoEscuela.SelectedItem;
                                    int ides = escu.id_escuela;
                                    var cli = BaseDatos.GetBaseDatos().clientes.Find(idc);
                                    cli.nombre = txtCliente.Text;
                                    cli.ap_paterno = txtPaterno.Text;
                                    cli.ap_materno = txtMaterno.Text;
                                    cli.correoelectronico = txtemail.Text;
                                    cli.usuario_id = idus;
                                    cli.escuela_id = ides;
                                    BaseDatos.GetBaseDatos().SaveChanges();
                                    ConsultarClientesxaml cc = new ConsultarClientesxaml(idus);
                                    cc.Show();
                                    this.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        void Limpiar()
        {
            txtCliente.Text = String.Empty;
            txtPaterno.Text = String.Empty;
            txtMaterno.Text = String.Empty;
            txtemail.Text = String.Empty;
            txtTelefono.Text = String.Empty;
            autoEscuela.SearchText = String.Empty;
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

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Actualizar();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
