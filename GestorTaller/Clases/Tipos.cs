using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace GestorTaller.Clases
{
    class Tipos
    {
        public int Id { get; set; }
        public string Tipo { get; set; }

        
            public void TraerTipos(RadComboBox box)
            {
            List<Tipos> listaClases = new List<Tipos>();

            string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Tipos ORDER BY Tipo ASC;";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tipos tipo = new Tipos
                            {
                                Id = reader.GetInt32(0),
                                Tipo = reader.GetString(1)
                            };
                            listaClases.Add(tipo); // Agregar a la lista
                        }
                        box.ItemsSource = listaClases;
                    }
                }
            }
        }
    }
    
}
