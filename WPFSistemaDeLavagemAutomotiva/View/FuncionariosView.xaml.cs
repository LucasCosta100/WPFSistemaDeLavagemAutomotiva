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
    /// Interação lógica para FuncionariosView.xam
    /// </summary>
    public partial class FuncionariosView : Page
    {
        private FuncionarioController _funcionarioController = new FuncionarioController();
        public FuncionariosView()
        {
            InitializeComponent();
            CadastrarFuncionario();
        }

        private void CadastrarFuncionario()
        {
            dgFuncionario.Items.Clear();
            var (ListarFuncionarios, mensagem) = _funcionarioController.ListarFuncionarios();
            dgFuncionario.ItemsSource = ListarFuncionarios;
            tbTotalFuncionarios.Text = $"Total de Funcionários: {ListarFuncionarios.Count().ToString()}";
        }

        private void btnDesativarFuncionario_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("Deseja realmente desativar este Funcionário?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                var botao = sender as Button; //Pega o botão que foi clicado
                var funcionarioSelecionado = botao.DataContext as Models.Funcionario; //Pega o agendamento associado ao botão clicado por meio do DataContext
                if (funcionarioSelecionado != null)
                {
                    _funcionarioController.DesativarFuncionario(funcionarioSelecionado);
                    MessageBox.Show("Funcionário desativado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                return;
            }

        }

        private void btnEditarFuncionario_Click(object sender, RoutedEventArgs e)
        {
            var botao = sender as Button;
            var funcionario = botao.DataContext as Funcionario;

            if (funcionario != null)
            {
                var janelaEditar = new EditarFuncionariosView(funcionario);
                janelaEditar.ShowDialog();
            }
        }
    }
}
