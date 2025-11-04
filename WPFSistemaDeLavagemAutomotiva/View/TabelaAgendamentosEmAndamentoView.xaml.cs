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
using WPFSistemaDeLavagemAutomotiva.Controller;
using WPFSistemaDeLavagemAutomotiva.Service;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para TabelaAgendamentosEmAndamentoView.xam
    /// </summary>
    public partial class TabelaAgendamentosEmAndamentoView : Page
    {
        AgendamentosController agendamentosController = new AgendamentosController();
        public TabelaAgendamentosEmAndamentoView()
        {
            InitializeComponent();
            CarregarTabelas();
        }

        public void CarregarTabelas()
        {
            dgAgendamentosEmAndamento.Items.Clear();
            var (listarAgendamentos, mensagem) = agendamentosController.ListarAgendamentosPorStatus("Em Andamento");
            dgAgendamentosEmAndamento.ItemsSource = listarAgendamentos;
        }

        private void btnEditarEmAndamento_Click(object sender, RoutedEventArgs e)
        {
            var botao = sender as Button;
            var agendamentoSelecionado = botao.DataContext as Models.Agendamento;

            if (agendamentoSelecionado != null)
            {
                var janelaEditar = new EditarAgendamentoView(agendamentoSelecionado);
                janelaEditar.ShowDialog();
            }
        }

        private void btnDesativarEmAndamento_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("Deseja realmente desativar o agendamento em andamento?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                var botao = sender as Button; //Pega o botão que foi clicado
                var agendamentoSelecionado = botao.DataContext as Models.Agendamento; //Pega o agendamento associado ao botão clicado por meio do DataContext
                if (agendamentoSelecionado != null)
                {
                    agendamentosController.DesativarAgendamento(agendamentoSelecionado);
                    MessageBox.Show("Agendamento desativado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
