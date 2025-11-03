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
using WPFSistemaDeLavagemAutomotiva.View.Components;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para AgendamentosView.xam
    /// </summary>
    public partial class AgendamentosView : Page
    {
        private Agendamento _agendamentoAtual;
        private AgendamentosController _agendamentoController = new AgendamentosController();
        public AgendamentosView()
        {
            InitializeComponent();
            BuscarProximoAgendamento();
            ListarAgendamentosHoje();
            BuscarTodosAgendamentos();
        }


        public void BuscarProximoAgendamento()
        {
            try
            {
                var proximoCliente = _agendamentoController.BuscarProximoAgendamento();
                _agendamentoAtual = proximoCliente;
                txtNome.Text = proximoCliente.ClienteAgendado.Nome;
                txtData.Text = (proximoCliente.DataAgendada + proximoCliente.HoraAgendamento).ToString("dd/MM/yyyy - HH:mm");
                txtServico.Text = $"Serviço: {proximoCliente.ServicoAgendado.NomeServico}";
                txtValor.Text = $"R$ {proximoCliente.ValorTotal.ToString("F2")}";
                txtTextoValor.Text = "Valor Total: ";
                spBotoes.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            { //Caso não encontrar o texto, exibe uma mensagem personalizada
                txtNome.Text = "Nenhum agendamento encontrado";
                txtNome.FontSize = 27;
                txtNome.TextWrapping = TextWrapping.Wrap;
                txtNome.HorizontalAlignment = HorizontalAlignment.Center;
                txtNome.TextAlignment = TextAlignment.Center;
                txtNome.Margin = new Thickness(0, 50, 0, 0);
                txtNome.Foreground = new SolidColorBrush(Color.FromRgb(255, 99, 71));
                txtData.Text = "";
                txtServico.Text = "";
                txtValor.Text = "";
                txtTextoValor.Text = "";
                spBotoes.Visibility = Visibility.Collapsed;
                _agendamentoAtual = null;
            }
        }

        public void ListarAgendamentosHoje()
        {
            try
            {
                var (lista, mensagem) = _agendamentoController.ListarAgendamentosHoje();
                txtAgendamentosHoje.Text = lista.Count.ToString();
            }
            catch (Exception ex)
            {
                txtAgendamentosHoje.Text = "0";
            }
        }

        public void BuscarTodosAgendamentos()
        {
            try
            {
                var (lista, mensagem) = _agendamentoController.ListarAgendamentos();
                txtAgendamentos.Text = lista.Count.ToString();
            }
            catch (Exception ex)
            {
                txtAgendamentos.Text = "0";
            }
        }

        //Usado para navegar entre as tabelas, utiilizando o NavButton personalizado
        private void lbTabelas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selecionado = lbTabelas.SelectedItem as NavButton; // Pega o item selecionado e converte para NavButton, como NavButton herda de ListBoxItem é possível fazer essa conversão
            frameTabelas.Navigate(selecionado.NavLink);
        }


        //Caso atenda, ele trocar o status para Em Andamento e puxa o próximo agendamento
        private void btnAtender_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("Deseja realmente atender este cliente?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                _agendamentoAtual.StatusServico = "Em Andamento";
                _agendamentoController.AtualizarAgendamento(_agendamentoAtual);
                MessageBox.Show("Cliente atendido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                BuscarProximoAgendamento();
                ListarAgendamentosHoje();
                BuscarTodosAgendamentos();
            }
            else
            {
                return;
            }
        }

        //Mesma lógica de atender
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("Deseja realmente cancelar este agendamento?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultado == MessageBoxResult.Yes)
            {
                txtTextoValor.Text = "";
                _agendamentoAtual.StatusServico = "Cancelado";
                _agendamentoController.AtualizarAgendamento(_agendamentoAtual);
                MessageBox.Show("Agendamento cancelado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                BuscarProximoAgendamento();
                ListarAgendamentosHoje();
                BuscarTodosAgendamentos();
            }
        }
    }
}
