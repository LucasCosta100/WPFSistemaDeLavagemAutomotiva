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
    /// Interação lógica para ServicosView.xam
    /// </summary>
    public partial class ServicosView : Page
    {
        private ServicoController _servicoController = new ServicoController();
        public ServicosView()
        {
            InitializeComponent();
            CarregarServicos();
        }

        public void CarregarServicos()
        {
            dgServicos.Items.Clear();
            var (sucesso, listarServicos, mensagem) = _servicoController.ListarServicos();
            dgServicos.ItemsSource = listarServicos;
            tbTotalServicos.Text = $"Total de Serviços: {listarServicos.Count().ToString()}";
        }

        private void btnEditarServico_Click(object sender, RoutedEventArgs e)
        {
            var botao = sender as Button;
            var servicoEscolhido = botao.DataContext as Models.Servico;
            if (servicoEscolhido != null)
            {
                var janelaEditar = new EditarServicosView(servicoEscolhido);
                janelaEditar.ShowDialog();
            }
        }

        private void btnDesativarServico_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("Deseja realmente desativar este Servico?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                var botao = sender as Button; //Pega o botão que foi clicado
                var servicoSelecionado = botao.DataContext as Models.Servico; //Pega o agendamento associado ao botão clicado por meio do DataContext
                if (servicoSelecionado != null)
                {
                    _servicoController.DesativarServico(servicoSelecionado);
                    MessageBox.Show("Cliente desativado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                return;
            }
        }
    }
}
