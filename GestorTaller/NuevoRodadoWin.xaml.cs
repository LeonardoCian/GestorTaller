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
    /// Lógica de interacción para NuevoRodadoWin.xaml
    /// </summary>
    public partial class NuevoRodadoWin : Window
    {
        Clientes clientes = new Clientes();
        Marcas marcas = new Marcas();
        Tipos tipos = new Tipos();
        NumRodados numRodados = new NumRodados();
        
        public NuevoRodadoWin()
        {
            InitializeComponent();
        }

        private void btnNuevoRodadoVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            clientes.TraerClientes(dgNuevoRodadoClientes);
            marcas.TraerMarcas(cbMarcas);
            tipos.TraerTipos(cbTipo);
            numRodados.TraerNumRodado(cbRodado);
        }

        private void txtBuscarClientes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
