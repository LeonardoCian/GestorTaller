using SharpDX.Direct3D10;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace GestorTaller.Clases
{
    class Marcas
    {
        public int Id { get; set; }
        public string Marca { get; set; }


        public void TraerMarcas(RadComboBox box)
        {
            List<Marcas> listaMarcas = new List<Marcas>();

            string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Marcas ORDER BY Marca ASC;";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Marcas marca = new Marcas
                            {
                                Id = reader.GetInt32(0),
                                Marca = reader.GetString(1)
                            };
                            listaMarcas.Add(marca); // Agregar a la lista
                        }
                        box.ItemsSource = listaMarcas;
                    }
                }
            }
        }
    }
}
