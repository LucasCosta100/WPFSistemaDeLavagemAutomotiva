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
    /// Lógica interna para EditarClienteView.xaml
    /// </summary>
    public partial class EditarClienteView : Window
    {
        private Cliente _cliente = new Cliente();
        private ClienteController _clienteController;

        public EditarClienteView(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;
            DataContext = cliente;
            _clienteController = new ClienteController();
            if (_cliente.EnderecoCliente == null)
                _cliente.EnderecoCliente = new Endereco();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();
            string telefone = txtTelefone.Text.Trim();
            string cep = txtCEP.Text.Trim();
            string rua = txtRua.Text.Trim();
            string numero = txtNumero.Text.Trim();
            string complemento = txtComplemento.Text.Trim();
            string bairro = txtBairro.Text.Trim();
            string cidade = txtCidade.Text.Trim();
            string estado = txtEstado.Text.Trim();

            if (_cliente.EnderecoCliente == null)
            {
                _cliente.EnderecoCliente = new Endereco();
            }

            // 🔍 Validações básicas
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNome.Focus();
                txtNome.Text = string.Empty;
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("O campo E-mail é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                txtEmail.Text = string.Empty;
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Informe um e-mail válido.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                txtEmail.Text = string.Empty;
                return;
            }

            if (string.IsNullOrWhiteSpace(telefone))
            {
                MessageBox.Show("O campo Telefone é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTelefone.Focus();
                txtTelefone.Text = string.Empty;
                return;
            }

            if (telefone.Length < 10)
            {
                MessageBox.Show("O telefone deve conter pelo menos 10 dígitos (DDD + número).", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTelefone.Focus();
                txtTelefone.Text = "";
                return;
            }

            if (string.IsNullOrWhiteSpace(rua) || string.IsNullOrWhiteSpace(cidade) || string.IsNullOrWhiteSpace(estado))
            {
                MessageBox.Show("Os campos Rua, Cidade e Estado são obrigatórios.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(cep) && !System.Text.RegularExpressions.Regex.IsMatch(cep, @"^\d{8}$"))
            {
                MessageBox.Show("O CEP deve conter exatamente 8 números, sem traço ou ponto.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCEP.Focus();
                txtCEP.Text = "";
                return;
            }

            

            // 🔁 Se passou nas validações, preenche o objeto cliente
            _cliente.Nome = nome;
            _cliente.Email = email;
            _cliente.Telefone = telefone;
            _cliente.EnderecoCliente.Cep = cep;
            _cliente.EnderecoCliente.Rua = rua;
            _cliente.EnderecoCliente.Numero = numero;
            _cliente.EnderecoCliente.Complemento = complemento;
            _cliente.EnderecoCliente.Bairro = bairro;
            _cliente.EnderecoCliente.Cidade = cidade;
            _cliente.EnderecoCliente.Estado = estado;

            // 💾 Confirmação antes de salvar
            var confirmacao = MessageBox.Show(
                "Deseja realmente salvar as alterações do cliente?",
                "Confirmação",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmacao != MessageBoxResult.Yes)
                return;

            // 🚀 Tenta atualizar via controller
            try
            {
                var (sucesso, mensagem) = _clienteController.AtualizarCliente(_cliente);

                if (sucesso)
                {
                    MessageBox.Show(mensagem, "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); // Fecha a janela após salvar
                }
                else
                {
                    MessageBox.Show(mensagem, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao atualizar cliente: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
