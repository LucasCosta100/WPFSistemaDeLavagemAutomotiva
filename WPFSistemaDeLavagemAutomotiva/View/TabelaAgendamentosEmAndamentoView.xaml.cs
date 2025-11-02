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
    /// Interação lógica para TabelaAgendamentosEmAndamentoView.xam
    /// </summary>
    public partial class TabelaAgendamentosEmAndamentoView : Page
    {
        AgendamentoService agendamentoService = new AgendamentoService();
        public TabelaAgendamentosEmAndamentoView()
        {
            InitializeComponent();
            CarregarTabelas();
        }

        public void CarregarTabelas()
        {
            dgAgendamentosEmAndamento.Items.Clear();
            var listarAgendamentos = agendamentoService.ListarAgendamentosPorStatus("Em Andamento");
            dgAgendamentosEmAndamento.ItemsSource = listarAgendamentos;
        }
    }
}
