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
    /// Lógica interna para EditarFuncionariosView.xaml
    /// </summary>
    public partial class EditarFuncionariosView : Window
    {
        private Funcionario _funcionario = new Funcionario();
        private FuncionarioController _funcionarioController = new FuncionarioController();
        public EditarFuncionariosView(Funcionario funcionario)
        {
            InitializeComponent();
            _funcionario = funcionario;
            DataContext = funcionario;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string cargo = txtCargo.Text.Trim();
            string cep = txtCEP.Text.Trim();
            string rua = txtRua.Text.Trim();
            string numero = txtNumero.Text.Trim();
            string complemento = txtComplemento.Text.Trim();
            string bairro = txtBairro.Text.Trim();
            string cidade = txtCidade.Text.Trim();
            string estado = txtEstado.Text.Trim();

            // 🔍 --- Validações básicas ---
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cargo))
            {
                MessageBox.Show("O campo Cargo é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCargo.Focus();
                return;
            }

            // 🔍 --- Validação do CEP (caso tenha sido digitado) ---
            if (!string.IsNullOrEmpty(cep))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(cep, @"^\d{8}$"))
                {
                    MessageBox.Show("O CEP deve conter exatamente 8 números, sem traço ou ponto.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCEP.Focus();
                    return;
                }
            }

            // 🔍 --- Validação dos campos obrigatórios do endereço ---
            if (string.IsNullOrWhiteSpace(rua) || string.IsNullOrWhiteSpace(cidade) || string.IsNullOrWhiteSpace(estado))
            {
                MessageBox.Show("Os campos Rua, Cidade e Estado são obrigatórios.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // --- Cria ou garante que o Endereco não seja nulo ---
            if (_funcionario.Endereco == null)
                _funcionario.Endereco = new Endereco();

            // 🔁 --- Atualiza os dados no objeto funcionário ---
            _funcionario.Nome = nome;
            _funcionario.Cargo = cargo;
            _funcionario.Endereco.Cep = cep;
            _funcionario.Endereco.Rua = rua;
            _funcionario.Endereco.Numero = numero;
            _funcionario.Endereco.Complemento = complemento;
            _funcionario.Endereco.Bairro = bairro;
            _funcionario.Endereco.Cidade = cidade;
            _funcionario.Endereco.Estado = estado;

            // 💬 Confirmação antes de salvar
            var confirmacao = MessageBox.Show(
                "Deseja realmente salvar as alterações do funcionário?",
                "Confirmação",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmacao != MessageBoxResult.Yes)
                return;

            // 🚀 Tenta atualizar via controller
            try
            {
                var (sucesso, mensagem) = _funcionarioController.AtualizarFuncionario(_funcionario);

                if (sucesso)
                {
                    MessageBox.Show(mensagem, "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(mensagem, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao atualizar funcionário: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
