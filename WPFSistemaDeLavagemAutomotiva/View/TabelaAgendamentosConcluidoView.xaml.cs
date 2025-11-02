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
    /// Interação lógica para TabelaAgendamentosConcluidoView.xam
    /// </summary>
    public partial class TabelaAgendamentosConcluidoView : Page
    {
        AgendamentoService agendamentoService = new AgendamentoService();
        public TabelaAgendamentosConcluidoView()
        {
            InitializeComponent();
            CarregarTabelas();
        }

        public void CarregarTabelas()
        {
            dgAgendamentosConcluido.Items.Clear();
            var listarAgendamentos = agendamentoService.ListarAgendamentosPorStatus("Concluído");
            dgAgendamentosConcluido.ItemsSource = listarAgendamentos;
        }
    }
}
