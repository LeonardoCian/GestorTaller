using GestorTaller.Clases;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace GestorTaller
{
    /// <summary>
    /// Lógica de interacción para RodadosWin.xaml
    /// </summary>
    public partial class RodadosWin : Window
    {
        Rodados rodados = new Rodados();

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            rodados.TraerRodados(dgRodados);
        }

        public RodadosWin()
        {
            InitializeComponent();
        }

        private void btnNuevoClientes_Click(object sender, RoutedEventArgs e)
        {
            NuevoRodadoWin ventana = new NuevoRodadoWin();
            ventana.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            rodados.TraerRodados(dgRodados);
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

        private void txtBuscarClientes_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Clientes> clientesLista = new List<Clientes>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                dgRodados.ItemsSource = null;


                // Abrir conexion con la base de datos
                connection.Open();

                // Crear el comando SQL
                string sql = "SELECT Id,Nombre,Documento,Telefono FROM Clientes WHERE Nombre LIKE @parametro OR Documento LIKE @parametro;";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@parametro", "%" + txtBuscarClientes.Text + "%");

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

                        dgRodados.ItemsSource = clientesLista;
                    }
                }
            }
        }
    }
}
