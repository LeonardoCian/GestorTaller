using GestorTaller.Clases;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Telerik.Windows.Core;

namespace GestorTaller
{
    /// <summary>
    /// Lógica de interacción para NuevoRodadoWin.xaml
    /// </summary>
    public partial class NuevoRodadoWin : Window
    {
        Clientes clientes = new Clientes();
        Marcas marcas = new Marcas();
        Tipos tipos = new Tipos();
        NumRodados numRodados = new NumRodados();

        public string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

        public void limpiarLabesls()
        {
            lblNombre.Content = null;
            lblDocCliente.Content = null;
            lblIdCliente.Content = null;
            lblTelCliente.Content = null;
        }
        
        public NuevoRodadoWin()
        {
            InitializeComponent();
            limpiarLabesls();
            marcas.TraerMarcas(cbMarcas);
            tipos.TraerTipos(cbTipo);
            numRodados.TraerNumRodado(cbRodados);
        }

        private void btnNuevoRodadoVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            clientes.TraerClientes(dgNuevoRodadoClientes);
        }

        private void txtBuscarClientes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgNuevoRodadoClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnNuevoCliente.IsEnabled = false;
            btnCancelarCliente.IsEnabled = true;
            try
            {
                if (dgNuevoRodadoClientes.SelectedItem is Clientes clienteSeleccionado)
                {
                    // Acceder a los valores de la fila seleccionada
                    lblIdCliente.Content = clienteSeleccionado.Id;
                    lblNombre.Content = clienteSeleccionado.Nombre;
                    lblDocCliente.Content = clienteSeleccionado.Documento;
                    lblTelCliente.Content = clienteSeleccionado.Telefono;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Fila o celda no válida");
            }
        }

        private void btnCancelarCliente_click(object sender, RoutedEventArgs e)
        {
            dgNuevoRodadoClientes.SelectedItem = null;
            btnCancelarCliente.IsEnabled = false;
            btnNuevoCliente.IsEnabled= true;
            limpiarLabesls();
        }


        private void btnCancelarRodado_Click(object sender, RoutedEventArgs e)
        {
            limpiarLabesls();
            dgNuevoRodadoClientes.SelectedItem = null;
            btnCancelarCliente.IsEnabled = false;
            btnNuevoCliente.IsEnabled = true;
            cbMarcas.SelectedItem = null;
            cbRodados.SelectedItem = null;
            cbTipo.SelectedItem = null;
        }

        public string idMarca, idTipo, idNumrRodado;

        private void cbRodados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRodados.SelectedItem != null)
            {
                NumRodados selectedItem = (NumRodados)cbRodados.SelectedItem;
                idNumrRodado = (selectedItem.Id).ToString();
            }
        }

        private void cbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTipo.SelectedItem != null)
            {
                Tipos selectedItem = (Tipos)cbTipo.SelectedItem;
                idTipo = (selectedItem.Id).ToString();
            }
        }

        private void cbMarcas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMarcas.SelectedItem != null)
            {
                Marcas selectedItem = (Marcas)cbMarcas.SelectedItem;
                idMarca = (selectedItem.Id).ToString();
            }
        }

        private void btnAndirRodado_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(cbMarcas.Text))
            {
                MessageBox.Show("Se debe seleccionar una marca");
            }else if (string.IsNullOrEmpty(cbRodados.Text))
            {
                MessageBox.Show("Se debe seleccionar un rodado");
            }else if (string.IsNullOrEmpty(cbTipo.Text))
            {
                MessageBox.Show("Se debe seleccionar una clase");
            }else if (string.IsNullOrEmpty(txtColor.Text))
            {
                MessageBox.Show("Se debe rellenar el campo 'Color'");
            }else if (lblNombre.Content== null ||
                      lblDocCliente.Content== null ||
                      lblIdCliente.Content== null ||
                      lblTelCliente.Content==null)
            {
                MessageBox.Show("Se debe seleccionar un cliente de la lista");
            }else
            {
                try
                {
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        // Iniciar una transacción
                        using (var transaction = connection.BeginTransaction())
                        {
                            string sql = "INSERT INTO Rodados(Id_Cliente, Id_Marca, Id_tipo,Id_NumRodado,Color) VALUES (@idCliente, @idMarca, @idTipo, @idNumRodado, @color);";
                            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@idCliente", lblIdCliente.Content.ToString());
                                command.Parameters.AddWithValue("@idMarca", idMarca.ToString());
                                command.Parameters.AddWithValue("@idTipo", idTipo.ToString());
                                command.Parameters.AddWithValue("@idNumRodado", idNumrRodado.ToString());
                                command.Parameters.AddWithValue("@color", txtColor.Text.ToString());

                                command.ExecuteNonQuery();
                            }

                            // Confirmar la transacción
                            transaction.Commit();
                        }

                        MessageBox.Show("Rodado añadido con éxito!");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

    }
}
