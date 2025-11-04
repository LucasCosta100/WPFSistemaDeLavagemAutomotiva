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
using WPFSistemaDeLavagemAutomotiva.DAO;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para ClientesView.xam
    /// </summary>
    public partial class ClientesView : Page
    {
        private ClienteController _clienteController = new ClienteController();
        public ClientesView()
        {
            InitializeComponent();
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            dgClientes.Items.Clear();
            var (clientes, mensagem) = _clienteController.ListarClientes();
            dgClientes.ItemsSource = clientes;
            tbTotalClientes.Text = $"Total de Clientes: {clientes.Count.ToString()}";
        }

        private void btnEditarCliente_Click(object sender, RoutedEventArgs e)
        {
            var botao = sender as Button;
            var cliente = botao.DataContext as Cliente;

            if(cliente != null)
            {
                var janelaEditar = new EditarClienteView(cliente);
                janelaEditar.ShowDialog();
            }
        }

        private void btnDesativarCliente_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("Deseja realmente desativar este cliente?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                var botao = sender as Button; //Pega o botão que foi clicado
                var clienteSelecionado = botao.DataContext as Models.Cliente; //Pega o agendamento associado ao botão clicado por meio do DataContext
                if (clienteSelecionado != null)
                {
                    _clienteController.DesativarCliente(clienteSelecionado);
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
