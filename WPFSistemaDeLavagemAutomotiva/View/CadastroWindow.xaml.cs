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
using WPFSistemaDeLavagemAutomotiva.Service;
using WPFSistemaDeLavagemAutomotiva.Utils;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Lógica interna para CadastroWindow.xaml
    /// </summary>
    public partial class CadastroWindow : Window
    {
        private FuncionarioController _funcController = new FuncionarioController();
        public CadastroWindow()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var senha = txtSenha.Password;
                var senhaHash = GerarHashSenhaUtils.GerarHash(senha.Trim());
                // Criar objeto Endereco
                Endereco endereco = new Endereco
                {
                    Cep = txtCEP.Text.Trim(),
                    Rua = txtRua.Text.Trim(),
                    Numero = txtNumero.Text.Trim(),
                    Complemento = txtComplemento.Text.Trim(),
                    Bairro = txtBairro.Text.Trim(),
                    Cidade = txtCidade.Text.Trim(),
                    Estado = txtEstado.Text.Trim()
                };

                // Criar objeto Funcionario
                Funcionario func = new Funcionario
                {
                    Nome = txtNome.Text.Trim(),
                    Cargo = txtCargo.Text.Trim(),
                    Usuario = txtUsuario.Text.Trim(),
                    SenhaHash = senhaHash,
                    Endereco = endereco
                };


                try
                {
                    _funcController.SalvarFuncionario(func);
                    MessageBox.Show("Funcionário Cadastrado", "Confirmar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message );
                }

                // Limpar campos ou fechar janela
                LoginWindow login = new LoginWindow();
                login.Show();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEntrar_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
