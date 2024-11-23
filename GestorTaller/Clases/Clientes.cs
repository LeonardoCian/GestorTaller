using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Controls;
using System;
using System.Windows;

namespace GestorTaller.Clases
{
    public class Clientes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }

        public void TraerClientes(DataGrid grid)
        {

            try
            {
                List<Clientes> clientesLista = new List<Clientes>();

                string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

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
                            grid.ItemsSource = clientesLista;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
    }
}
