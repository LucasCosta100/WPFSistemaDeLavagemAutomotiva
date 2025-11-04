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
using WPFSistemaDeLavagemAutomotiva.Models;
using WPFSistemaDeLavagemAutomotiva.Service;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para TabelaAgendamentosCanceladoView.xam
    /// </summary>
    public partial class TabelaAgendamentosCanceladoView : Page
    {
        AgendamentosController agendamentosController = new AgendamentosController();
        public TabelaAgendamentosCanceladoView()
        {
            InitializeComponent();
            CarregarTabela();
        }

        public void CarregarTabela()
        {
            dgAgendamentosCancelado.Items.Clear();
            var (listaAgendamentosCancelados, mensagem) = agendamentosController.ListarAgendamentosPorStatus("Cancelado");
            dgAgendamentosCancelado.ItemsSource = listaAgendamentosCancelados;

        }

        //Após clicar no botão editar, abre a janela de edição do agendamento selecionado e por meio do Binding passa os dados do agendamento para a janela de edição.
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botao = sender as Button;
            var agendamentoSelecionado = botao.DataContext as Models.Agendamento;

            if (agendamentoSelecionado != null)
            {
                var janelaEditar = new EditarAgendamentoView(agendamentoSelecionado);
                janelaEditar.ShowDialog();
            }
        }
    }
}
