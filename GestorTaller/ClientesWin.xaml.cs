using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
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
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static GestorTaller.ClientesWin;
using GestorTaller.Clases;
using System.Data.SqlClient;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace GestorTaller
{
    /// <summary>
    /// Lógica de interacción para ClientesWin.xaml
    /// </summary>
    public partial class ClientesWin : Window
    {
         public string id,nombre,documento,telefono;

        public void actualizarClientes()
        {
            List<Clientes> clientesLista = new List<Clientes>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre, Documento, Telefono FROM Clientes";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clientes cliente = new Clientes
                            {
                                Id = reader.GetInt32(0), // Obtener el Id
                                Nombre = reader.GetString(1), // Obtener el Nombre
                                Documento = reader.GetString(2), // Obtener el Documento
                                Telefono = reader.GetString(3) // Obtener el Telefono
                            };

                            clientesLista.Add(cliente); // Agregar a la lista
                        }

                        datagridClientes.ItemsSource = clientesLista;
                    }
                }
            }
        }




        Clientes clientes = new Clientes();

        // Cadena de conexión a tu base de datos SQLite
        public string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

        public ClientesWin()
        {
            InitializeComponent();
        }

        private void ClickBtnClientesSalir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClickBtnClientesNuevo(object sender, RoutedEventArgs e)
        {
            NuevoClienteWin nuevoClienteWin = new NuevoClienteWin();
            nuevoClienteWin.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            actualizarClientes();
        }

        // Busqueda a medida que se va tipeando en el TextBox

        private void txtBuscarClientes_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                datagridClientes.ItemsSource = null;


                // Abrir conexion con la base de datos
                connection.Open();

                // Crear el comando SQL
                string sql = "SELECT * FROM Clientes WHERE Nombre LIKE @parametro OR Documento LIKE @parametro;";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@parametro", "%" + txtBuscarClientes.Text + "%");


                    // Crear un adaptador para llenar el DataTable
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Asignar el DataTable como fuente de datos del DataGrid
                    datagridClientes.ItemsSource = dataTable.DefaultView;
                }
            }
        }
        //Habilitar botones cuando se selecciona una fila del DataGrid
        private void datagridClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEliminarClientes.IsEnabled = true;
            btnEditarClientes.IsEnabled = true;
            btnCancelarClientes.IsEnabled = true;
            btnClienteRodados.IsEnabled = true;

            try
            {
                if (datagridClientes.SelectedItem is Clientes clienteSeleccionado)
                {
                    // Acceder a los valores de la fila seleccionada
                    int id = clienteSeleccionado.Id;
                    string nombre = clienteSeleccionado.Nombre;
                    string documento = clienteSeleccionado.Documento;
                    string telefono = clienteSeleccionado.Telefono;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Fila o celda no válida");
            }
            

        }

        // Vaciar campos cuando se presiona el boton "Cancelar"
        private void btnCancelarClientes_Click(object sender, RoutedEventArgs e)
        {
            datagridClientes.SelectedItem = null;
            btnCancelarClientes.IsEnabled = false;
            btnEditarClientes.IsEnabled = false;
            btnEliminarClientes.IsEnabled = false;
            btnClienteRodados.IsEnabled = false;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            actualizarClientes();
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            actualizarClientes();

            datagridClientes.SelectedItem = null;
            btnCancelarClientes.IsEnabled = false;
            btnEditarClientes.IsEnabled = false;
            btnEliminarClientes.IsEnabled = false;
            btnClienteRodados.IsEnabled = false;
        }

        private void btnEliminarClientes_Click_1(object sender, RoutedEventArgs e)
        {
            int id = 0;
            if (datagridClientes.SelectedItem is Clientes clienteSeleccionado)
            {
                // Acceder a los valores de la fila seleccionada
                id = clienteSeleccionado.Id;
            }
            try
            {
                    // Iniciar una transacción
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                            string sql = "DELETE FROM Clientes WHERE Id = @id;";
                            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@id",id);

                                command.ExecuteNonQuery();
                            }
                    }

                    MessageBox.Show("Cliente eliminado del registro con éxito!");

                    datagridClientes.SelectedItem = null;
                    btnCancelarClientes.IsEnabled = false;
                    btnEditarClientes.IsEnabled = false;
                    btnEliminarClientes.IsEnabled = false;
                    btnClienteRodados.IsEnabled = false;


                actualizarClientes();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void btnEditarClientes_Click(object sender, RoutedEventArgs e) 
        {
            if (datagridClientes.SelectedItem is Clientes clienteSeleccionado)
            {
                // Acceder a los valores de la fila seleccionada
                int id = clienteSeleccionado.Id;
                string nombre = clienteSeleccionado.Nombre;
                string documento = clienteSeleccionado.Documento;
                string telefono = clienteSeleccionado.Telefono;

                // Crear y mostrar la nueva ventana, pasando los valores
                ModClienteWin ventanaDetalles = new ModClienteWin(id, nombre, documento, telefono);
                ventanaDetalles.Show();
            }
        }
    }
    
}
