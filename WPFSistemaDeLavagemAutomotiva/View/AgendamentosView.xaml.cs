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
using WPFSistemaDeLavagemAutomotiva.View.Components;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para AgendamentosView.xam
    /// </summary>
    public partial class AgendamentosView : Page
    {
        public AgendamentosView()
        {
            InitializeComponent();
        }

        private void lbTabelas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selecionado = lbTabelas.SelectedItem as NavButton; // Pega o item selecionado e converte para NavButton, como NavButton herda de ListBoxItem é possível fazer essa conversão
        }
    }
}
