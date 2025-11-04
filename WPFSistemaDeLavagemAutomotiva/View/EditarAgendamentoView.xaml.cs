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
using System.Windows.Shapes;
using WPFSistemaDeLavagemAutomotiva.Controller;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Lógica interna para EditarAgendamentoView.xaml
    /// </summary>
    public partial class EditarAgendamentoView : Window
    {
        Agendamento _agendamento = new Agendamento();
        AgendamentosController _agendamentosController = new AgendamentosController();
        ServicoController _servicoController = new ServicoController();

        public EditarAgendamentoView(Agendamento agendamento)
        {
            InitializeComponent();
            _agendamento = agendamento;
            DataContext = agendamento; //DataContext atualiza automaticamente os valores por meio do Binding utilizado nas tabelas
            CarregarServicos();
            dpData.DisplayDateStart = DateTime.Today; // Impede seleção de datas passadas no calendário
        }

        public void CarregarServicos()
        {
            var listarServicos = _servicoController.ListarServicos();
            cbServicos.Items.Clear();
            cbServicos.ItemsSource = listarServicos;
            cbServicos.DisplayMemberPath = "NomeServico";         // Mostra o nome
            cbServicos.SelectedValuePath = "IdServico";           // Valor interno (ID)
            cbServicos.SelectedValue = _agendamento.ServicoAgendado;
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            Servico servicoSelecionado = cbServicos.SelectedItem as Servico; //Seleciona o serviço escolhido no ComboBox como "Servico"
            Cliente cliente = _agendamento.ClienteAgendado; //Pega o cliente associado ao agendamento atual
            DateTime dataAgendada = dpData.SelectedDate.Value;
            string horaTexto = txtHora.Text; // Ex: "14:30"
            TimeSpan horaAgendada = TimeSpan.Parse(horaTexto);
            ComboBoxItem selecionado = cbStatus.SelectedItem as ComboBoxItem; //Pega o status selecionado no ComboBox de status e transformo em string
            string statusServico = selecionado?.Content.ToString();
            double valorTotal;
            bool sucesso = double.TryParse(txtValor.Text, out valorTotal); // Verificação do valor digitado

            if (!sucesso)
            {
                MessageBox.Show("Digite um valor numérico válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtValor.Text = string.Empty;
                txtValor.Focus();
                return;
            }

            _agendamento.ServicoAgendado = servicoSelecionado;
            _agendamento.ClienteAgendado = cliente;
            _agendamento.DataAgendada = dataAgendada;
            _agendamento.HoraAgendamento = horaAgendada;
            _agendamento.StatusServico = statusServico;
            _agendamento.ValorTotal = valorTotal;
            try
            {
                _agendamentosController.AtualizarAgendamento(_agendamento);
                var atualizarAgendametos = new AgendamentosView();
                MessageBox.Show("Agendamento atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Fecha o formulário após salvar
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar agendamento: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
