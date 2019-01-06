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

namespace TEKNO_v1.Productos
{
    /// <summary>
    /// Lógica de interacción para RegistrarProducto.xaml
    /// </summary>
    public partial class RegistrarProducto : Window
    {
        int idp;
        int idus;
        public RegistrarProducto()
        {
            InitializeComponent();
        }

        public RegistrarProducto(int idusuario)
        {
            InitializeComponent();
            idus = idusuario;
        }

        public RegistrarProducto(producto pr)
        {
            InitializeComponent();
            idp = pr.id_producto;
            txtProducto.Text = pr.nombre;
            txtDescripcion.Text = pr.descripcion;
            txtMarca.Text = pr.marca;
            cbbAlmacen.Text = pr.almacen;
            txtpreciocompra.Text = Convert.ToString(pr.preciocompra);
            txtprecioprofesor.Text = pr.precioprofesor.ToString();
            txtpreciopublico.Text = Convert.ToString(pr.preciopublico);
            btnGuardar.Visibility = Visibility.Hidden;
            btnActualizar.Visibility = Visibility.Visible;
        }

        void Guardar()
        {
            if (txtProducto.Text == "")
            {
                MessageBox.Show("Ingresa el nombre del producto");
            }else
            {
                if (txtDescripcion.Text == "")
                {
                    MessageBox.Show("Ingresa la descripcion");
                }else
                {
                    if (txtMarca.Text == "")
                    {
                        MessageBox.Show("Ingresa la marca");
                    }else
                    {
                        if (txtpreciocompra.Text == "")
                        {
                            MessageBox.Show("Ingressa el precio de compra");
                        }else
                        {
                            if (txtprecioprofesor.Text == "")
                            {
                                MessageBox.Show("Ingresa el precio para profesores");
                            }
                            else
                            {
                                if(txtpreciopublico.Text == "")
                                {
                                    MessageBox.Show("Ingresa el precio para el publico en general");
                                }
                                else
                                {
                                    if(cbbAlmacen.Text == "")
                                    {
                                        MessageBox.Show("Selecciona el almacen");
                                    }
                                    else
                                    {
                                        producto pr = new producto
                                        {
                                            nombre = txtProducto.Text,
                                            descripcion = txtDescripcion.Text,
                                            marca = txtMarca.Text,
                                            almacen = cbbAlmacen.Text,
                                            usuario_id = idus,
                                            estatus = "Activo",
                                            preciocompra =Convert.ToDecimal(txtpreciocompra.Text),
                                            precioprofesor =Convert.ToDecimal(txtprecioprofesor.Text),
                                            preciopublico =Convert.ToDecimal(txtpreciopublico.Text),
                                            existencias = 0,
                                        };
                                        BaseDatos.GetBaseDatos().productoes.Add(pr);
                                        BaseDatos.GetBaseDatos().SaveChanges();
                                        Limpiar();
                                        MessageBox.Show("Registro exitoso");
                                    }
                                }
                            }
                           
                        }
                    }
                }
            }
        }

        void Actualizar()
        {
            if (txtProducto.Text == "")
            {
                MessageBox.Show("Ingresa el nombre del producto");
            }
            else
            {
                if (txtDescripcion.Text == "")
                {
                    MessageBox.Show("Ingresa la descripcion");
                }
                else
                {
                    if (txtMarca.Text == "")
                    {
                        MessageBox.Show("Ingresa la marca");
                    }
                    else
                    {
                        if (txtpreciocompra.Text == "")
                        {
                            MessageBox.Show("Inserta el precio de compra");
                        }
                        else
                        {
                            if(txtprecioprofesor.Text == "")
                            {
                                MessageBox.Show("Inserta el precio para Profesores");
                            }else
                            {
                                if(txtpreciopublico.Text == "")
                                {
                                    MessageBox.Show("Inserta el precio para publico en general");
                                }else
                                {
                                    if(cbbAlmacen.Text == "")
                                    {
                                        MessageBox.Show("Selecciona almacen");
                                    }else
                                    {
                                        var pr = BaseDatos.GetBaseDatos().productoes.Find(idp);
                                        pr.nombre = txtProducto.Text;
                                        pr.descripcion = txtDescripcion.Text;
                                        pr.marca = txtMarca.Text;
                                        pr.almacen = cbbAlmacen.Text;
                                        pr.preciocompra = Convert.ToDecimal(txtpreciocompra.Text);
                                        pr.precioprofesor = Convert.ToDecimal(txtprecioprofesor.Text);
                                        pr.preciopublico = Convert.ToDecimal(txtpreciopublico.Text);
                                        BaseDatos.GetBaseDatos().SaveChanges();
                                        ConsultarProductos cp = new ConsultarProductos();
                                        cp.Show();
                                        this.Close();
                                    }
                                }
                            }
                         
                        }
                    }
                }
            }
        }

        void Limpiar()
        {
            txtProducto.Text = String.Empty;
            txtDescripcion.Text = String.Empty;
            txtMarca.Text = String.Empty;
            cbbAlmacen.SelectedItem = -1;
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
