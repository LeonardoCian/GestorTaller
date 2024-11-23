using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Windows;
using System;

namespace GestorTaller.Clases
{
    public class Rodados
    {
        public int Id { get; set; }
        public string Id_Cliente { get; set; }
        public string Id_Marca { get; set; }
        public string Id_Tipo { get; set; }
        public string Id_NumRodado { get; set; }
        public string Color { get; set; }

        public void TraerRodados(DataGrid grid)
        {
            List<Rodados> listaRodados = new List<Rodados>();

            string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Rodados.Id,  clientes.Nombre, marcas.marca, Tipos.Tipo,NumRodados.NumRodado, Color from Rodados JOIN Clientes ON  Rodados.Id_Cliente = clientes.Id JOIN Marcas ON  Rodados.Id_Marca =Marcas.Id JOIN Tipos ON  Rodados.Id_Tipo = Tipos.Id JOIN NumRodados ON  Rodados.Id_NumRodado = NumRodados.Id;";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Rodados rodado = new Rodados
                                {
                                    Id = reader.GetInt32(0), // Obtener el Id
                                    Id_Cliente = reader.GetString(1), // Obtener id del cliente
                                    Id_Marca = reader.GetString(2), // Obtener id de la marca
                                    Id_Tipo = reader.GetString(3), // Obtener id de la clase
                                    Id_NumRodado = reader.GetString(4), // Obtener id del rodado(numero)
                                    Color = reader.GetString(5) //Obtener color
                                };
                                listaRodados.Add(rodado); // Agregar a la lista
                            }
                            grid.ItemsSource = listaRodados;
                        }
                    }
                }
        }
    }
}