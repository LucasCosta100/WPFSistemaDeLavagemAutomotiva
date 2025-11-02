using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFSistemaDeLavagemAutomotiva.DAO;
using WPFSistemaDeLavagemAutomotiva.Database;
using WPFSistemaDeLavagemAutomotiva.Models;
using WPFSistemaDeLavagemAutomotiva.Service;
using WPFSistemaDeLavagemAutomotiva.View.Components;

namespace WPFSistemaDeLavagemAutomotiva
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;  // maximiza

        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selecionado = sidebar.SelectedItem as NavButton; // Pega o item selecionado e converte para NavButton, como NavButton herda de ListBoxItem é possível fazer essa conversão


            paginasnav.Navigate(selecionado.NavLink); // Navega para a página definida na propriedade NavLink do NavButton selecionado
        }
    }
}
