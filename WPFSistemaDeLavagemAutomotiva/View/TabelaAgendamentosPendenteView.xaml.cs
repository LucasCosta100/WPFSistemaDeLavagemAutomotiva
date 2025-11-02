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
    /// Interação lógica para TabelaAgendamentosPendenteView.xam
    /// </summary>
    public partial class TabelaAgendamentosPendenteView : Page
    {
        AgendamentoService agendamentoService = new AgendamentoService();
        public TabelaAgendamentosPendenteView()
        {
            InitializeComponent();
            CarregarTabelas();
        }

        public void CarregarTabelas()
        {
            dgAgendamentosPendente.Items.Clear();
            var listarAgendamentos = agendamentoService.ListarAgendamentosPorStatus("Pendente");
            dgAgendamentosPendente.ItemsSource = listarAgendamentos;
        }
    }
}
