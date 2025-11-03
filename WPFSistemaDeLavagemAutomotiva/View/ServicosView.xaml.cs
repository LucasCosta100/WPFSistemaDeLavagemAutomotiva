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
    /// </summary>metdo S
    public partial class ServicosView : Page
    {
        ServicoService servicoService = new ServicoService();

        public ServicosView()
        {
            InitializeComponent();
            TodosServicos();
        }

        private void TodosServicos()
        {
            dgServicos.Items.Clear();
            var ListarServicos = servicoService.ListarServicos();
            dgServicos.ItemsSource = ListarServicos;
            tbTotalServicos.Text = $"Total de Serviços: {ListarServicos.Count().ToString()}";
            tbTotalServicos.Text = $"Total de Funcionários: {ListarServicos.Count().ToString()}";
        }
    }
}
