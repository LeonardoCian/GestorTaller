using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GestorTaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartTimer();
        }

        private void ClickBtnMenuClientes(object sender, RoutedEventArgs e)
        {
            ClientesWin ventanaClientes = new ClientesWin();
            ventanaClientes.ShowDialog();
        }

        private void ClickBtnMenuSalir(object sender, RoutedEventArgs e)
        {
            const string caption = "Salir";
            MessageBoxResult result = MessageBox.Show("¿Estás seguro que deseas salir del programa?", caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) { Application.Current.Shutdown(); }
        }
        // Metodo para iniciar el contador
        private void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(0);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
            lblHora.Content = DateTime.Now.ToString("HH:mm"); // Formato de hora
            lblFecha.Content = (DateTime.Now.ToString("dddd")).ToUpper() + " " + DateTime.Now.ToString("d");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
    
