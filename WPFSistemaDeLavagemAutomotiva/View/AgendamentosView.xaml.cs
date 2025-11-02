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
using WPFSistemaDeLavagemAutomotiva.View.Components;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para AgendamentosView.xam
    /// </summary>
    public partial class AgendamentosView : Page
    {
        private AgendamentoService _agendamentoService = new AgendamentoService();
        public AgendamentosView()
        {
            InitializeComponent();
            var proximoCliente = _agendamentoService.BuscarProximoAgendamento();
            txtNome.Text = proximoCliente.ClienteAgendado.Nome;
            txtData.Text = (proximoCliente.DataAgendada + proximoCliente.HoraAgendamento).ToString("dd/MM/yyyy HH:mm");
            txtServico.Text = $"Serviço: {proximoCliente.ServicoAgendado.NomeServico}";
            txtValor.Text = $"R$ {proximoCliente.ValorTotal.ToString("F2")}";
        }


        private void lbTabelas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selecionado = lbTabelas.SelectedItem as NavButton; // Pega o item selecionado e converte para NavButton, como NavButton herda de ListBoxItem é possível fazer essa conversão
            frameTabelas.Navigate(selecionado.NavLink);
        }
    }
}
