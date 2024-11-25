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
    class NumRodados
    {
        public int Id { get; set; }
        public string NumRodado { get; set; }


        public void TraerNumRodado(RadComboBox box)
        {
            List<NumRodados> ListaNumRodado = new List<NumRodados>();

            string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM NumRodados ORDER BY NumRodado ASC;";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NumRodados numRodados = new NumRodados
                            {
                                Id = reader.GetInt32(0),
                                NumRodado = reader.GetString(1)
                            };
                            ListaNumRodado.Add(numRodados); // Agregar a la lista
                        }
                        box.ItemsSource = ListaNumRodado;
                    }
                }
            }
        }
    }
}
