using GestorTaller.Clases;
using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;

namespace GestorTaller
{


    /// <summary>
    /// Lógica de interacción para ModClienteWin.xaml
    /// </summary>
    public partial class ModClienteWin : Window
    {
        public string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

        public string nombre,documento,telefono;

        public ModClienteWin(int id, string nombre, string documento, string telefono)
        {
            InitializeComponent();
            txtModClienteDocumento.Text = documento;
            txtModClienteNombre.Text = nombre;
            txtModClienteTelefono.Text = telefono;
            lblIdCliente.Content = id;
        }

        
        

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs exit)
        {
            exit.Cancel = true;
            txtModClienteDocumento.Text = string.Empty;
            txtModClienteNombre.Text = string.Empty;
            txtModClienteTelefono.Text = string.Empty;
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClientesWin clientes = new ClientesWin();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ClientesWin clientes = new ClientesWin();
        }

        private bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _); // Intenta convertir el texto a un número entero
        }

        private void txtModClienteDocumento_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextNumeric(e.Text))
            {
                MessageBox.Show("Solo se permiten caracteres numericos");
                e.Handled = true;
            }
        }

        private void txtModClienteTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextNumeric(e.Text))
            {
                MessageBox.Show("Solo se permiten caracteres numericos");
                e.Handled = true;
            }
        }

        private void btnGuardarModCliente_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtModClienteNombre.Text) ||
                string.IsNullOrWhiteSpace(txtModClienteDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtModClienteTelefono.Text))
            {
                MessageBox.Show("Los campos deben ser rellenados!!!");
            }
            else
            {
                try
                {
                    // Iniciar una transacción
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "UPDATE Clientes SET Nombre = @nombre ,Documento = @documento, Telefono= @telefono WHERE Id = @id;";
                        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", lblIdCliente.Content);
                            command.Parameters.AddWithValue("@nombre", txtModClienteNombre.Text);
                            command.Parameters.AddWithValue("@documento", txtModClienteDocumento.Text);
                            command.Parameters.AddWithValue("@telefono", txtModClienteTelefono.Text);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Datos modificados con éxito!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            this.Close();
        }

        private void btnCancelarModCliente_Click(object sender, RoutedEventArgs e)
        {
            txtModClienteDocumento.Text = string.Empty;
            txtModClienteNombre.Text = string.Empty;
            txtModClienteTelefono.Text = string.Empty;

            this.Close();
        }
    }
}
