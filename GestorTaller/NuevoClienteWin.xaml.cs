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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using GestorTaller.Clases;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace GestorTaller
{
    /// <summary>
    /// Lógica de interacción para NuevoClienteWin.xaml
    /// </summary>
    public partial class NuevoClienteWin : Window
    {
        public string connectionString = "Data Source=C:\\Users\\Leo\\source\\repos\\GestorTaller\\GestorTaller\\Taller.db";

        Clientes a = new Clientes();

        public NuevoClienteWin()
        {
            InitializeComponent();
        }

        private void txtGuardarNuevoCliente_Click(object sender, RoutedEventArgs e)
        {

            
            if(txtNombreNuevoCliente.Text.Length > 40)
            {
                MessageBox.Show("El campo 'Nombre y apellido' no puede ser de mas de 40 caracteres");
                txtNombreNuevoCliente.Text = string.Empty;
            }
            if (txtDocumentoNuevoCliente.Text.Length > 8)
            {
                MessageBox.Show("El campo 'Documento' no puede ser de mas de 8 Digitos");
                txtDocumentoNuevoCliente.Text = string.Empty;
            }
            if(txtTelefonoNuevoCliente.Text.Length > 10)
            {
                MessageBox.Show("El campo 'Telefono' no puede ser de mas de 10 Digitos");
                txtTelefonoNuevoCliente.Text = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(txtNombreNuevoCliente.Text) ||
                string.IsNullOrWhiteSpace(txtDocumentoNuevoCliente.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoNuevoCliente.Text))
            {
                MessageBox.Show("Los campos deben ser rellenados!!!");
            }
            else
            {
                try
                {
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        // Iniciar una transacción
                        using (var transaction = connection.BeginTransaction())
                        {
                            string sql = "INSERT INTO CLIENTES(Nombre, Documento, Telefono) VALUES(@nombre, @documento, @telefono);";
                            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@nombre", txtNombreNuevoCliente.Text);
                                command.Parameters.AddWithValue("@documento", txtDocumentoNuevoCliente.Text);
                                command.Parameters.AddWithValue("@telefono", txtTelefonoNuevoCliente.Text);

                                command.ExecuteNonQuery();
                            }

                            // Confirmar la transacción
                            transaction.Commit();
                        }

                        MessageBox.Show("Cliente añadido con éxito!");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void clickCancelarNuevoCliente(object sender, RoutedEventArgs e)
        {
            txtNombreNuevoCliente.Text = null;
            txtDocumentoNuevoCliente.Text = null;
            txtTelefonoNuevoCliente.Text = null;
            this.Close();
        }

        private bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _); // Intenta convertir el texto a un número entero
        }

        private void txtDocumentoNuevoCliente_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextNumeric(e.Text))
            {
                MessageBox.Show("Solo se permiten caracteres numericos");
                e.Handled = true;
            }
        }

        private void txtTelefonoNuevoCliente_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextNumeric(e.Text))
            {
                MessageBox.Show("Solo se permiten caracteres numericos");
                e.Handled = true;
            }
        }
    }
}
