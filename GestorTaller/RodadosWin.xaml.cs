﻿using System;
using System.Collections.Generic;
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
        public RodadosWin()
        {
            InitializeComponent();
        }

        private void btnNuevoClientes_Click(object sender, RoutedEventArgs e)
        {
            NuevoRodadoWin ventana = new NuevoRodadoWin();
            ventana.ShowDialog();
        }
    }
}
