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

namespace TEKNO_v1.Devoluciones
{
    /// <summary>
    /// Lógica de interacción para NuevaDevolucion.xaml
    /// </summary>
    public partial class NuevaDevolucion : Window
    {
        int idD;
        Decimal total;
        int canti;
        int iddetalle;
        public NuevaDevolucion()
        {
            InitializeComponent();
        }

        void VistaGrid()
        {
            radGridDevolucion.ItemsSource = (from v in BaseDatos.GetBaseDatos().ventas
                                        join dv in BaseDatos.GetBaseDatos().detalle_venta
                                        on v.id_venta equals dv.venta_id
                                        join pr in BaseDatos.GetBaseDatos().productoes
                                        on dv.producto_id equals pr.id_producto
                                        where v.id_venta == idD
                                        select new
                                        {
                                            id_venta = v.id_venta,
                                            id_detalleventa = dv.id_detallevta,
                                            id_producto = pr.id_producto,
                                            nombre = pr.nombre + " " + pr.descripcion + " " + pr.marca,
                                            cantidad = dv.cantidad,
                                            precio = pr.preciopublico,
                                            subtotal = dv.subtotal
                                        });
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtTicket.Text == "")
            {
                MessageBox.Show("Ingresa el # de Ticket");
            }else
            {
                idD = Int32.Parse(txtTicket.Text);
                var venta = BaseDatos.GetBaseDatos().ventas.Find(idD);
                total = venta.total;
                VistaGrid();
                txtTicket.IsEnabled = false;
                btnBuscar.IsEnabled = false;
                btnDevolucion.IsEnabled = true;
                txtCantidad.IsEnabled = true;
            }
        }

        private void btnDevolucion_Click(object sender, RoutedEventArgs e)
        {
            if (radGridDevolucion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un producto");
            }else
            {
                dynamic producto = radGridDevolucion.SelectedItem;
                iddetalle = producto.id_detalleventa;
                int idp = producto.id_producto;
                canti = producto.cantidad;

                if (txtCantidad.Text == "")
                {
                    MessageBox.Show("Ingresa la cantidad a devolver");
                }else
                {
                    if (Int32.Parse(txtCantidad.Text) > canti)
                    {
                        MessageBox.Show("No se puede devolver mas del producto solicitado");
                    }else
                    {
                        if (Int32.Parse(txtCantidad.Text) < 0)
                        {
                            MessageBox.Show("No se aceptan numeros negativos");
                        }else
                        {
                            if (Int32.Parse(txtCantidad.Text) == canti)
                            {
                                var detalle = BaseDatos.GetBaseDatos().detalle_venta.Find(iddetalle);
                                var venta = BaseDatos.GetBaseDatos().ventas.Find(idD);
                                venta.total = venta.total - (canti * Decimal.Parse(detalle.precio.ToString()));
                                BaseDatos.GetBaseDatos().SaveChanges();

                                var produc = BaseDatos.GetBaseDatos().productoes.Find(idp);
                                produc.existencias = produc.existencias + canti;

                                BaseDatos.GetBaseDatos().detalle_venta.Remove(detalle);
                                BaseDatos.GetBaseDatos().SaveChanges();
                                VistaGrid();

                                radGridDevolucion.SelectedItem = null;
                                txtCantidad.Text = String.Empty;
                            }else
                            {
                                if (Int32.Parse(txtCantidad.Text) == 0)
                                {
                                    MessageBox.Show("La cantidad minima a devolver es 1");
                                }else
                                {
                                    //Actualizamos existencias
                                    var prod = BaseDatos.GetBaseDatos().productoes.Find(idp);
                                    prod.existencias = prod.existencias + Int32.Parse(txtCantidad.Text);
                                    BaseDatos.GetBaseDatos().SaveChanges();

                                    //Actualizamos el detalle
                                    var deta = BaseDatos.GetBaseDatos().detalle_venta.Find(iddetalle);
                                    deta.cantidad = deta.cantidad - Int32.Parse(txtCantidad.Text);
                                    BaseDatos.GetBaseDatos().SaveChanges();
                                    VistaGrid();
                                    radGridDevolucion.SelectedItem = null;
                                    txtCantidad.Text = String.Empty;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
