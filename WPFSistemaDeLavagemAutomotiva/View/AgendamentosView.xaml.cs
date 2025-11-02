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
using WPFSistemaDeLavagemAutomotiva.View.Components;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para AgendamentosView.xam
    /// </summary>
    public partial class AgendamentosView : Page
    {
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
                txtNome.Text = proximoCliente.ClienteAgendado.Nome;
                txtData.Text = (proximoCliente.DataAgendada + proximoCliente.HoraAgendamento).ToString("dd/MM/yyyy HH:mm");
                txtServico.Text = $"Serviço: {proximoCliente.ServicoAgendado.NomeServico}";
                txtValor.Text = $"R$ {proximoCliente.ValorTotal.ToString("F2")}";
                txtTextoValor.Text = "Valor Total: ";
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
                txtServico = null;
                txtValor = null;
                spBotoes.Visibility = Visibility.Collapsed;
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

        private void lbTabelas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selecionado = lbTabelas.SelectedItem as NavButton; // Pega o item selecionado e converte para NavButton, como NavButton herda de ListBoxItem é possível fazer essa conversão
            frameTabelas.Navigate(selecionado.NavLink);
        }
    }
}
