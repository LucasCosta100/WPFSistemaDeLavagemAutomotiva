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
using WPFSistemaDeLavagemAutomotiva.Service;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para ServicosView.xam
    /// </summary>
    public partial class ServicosView : Page
    {
        private ServicoService _servicoService = new ServicoService();
        public ServicosView()
        {
            InitializeComponent();
            CarregarServicos();
        }

        public void CarregarServicos()
        {
            dgServicos.Items.Clear();
            var listarServicos = _servicoService.ListarServicos();
            dgServicos.ItemsSource = listarServicos;
        }
    }
}
