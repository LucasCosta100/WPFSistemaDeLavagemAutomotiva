using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFSistemaDeLavagemAutomotiva.Controller;
using WPFSistemaDeLavagemAutomotiva.Models;


namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Lógica interna para EditarServicosView.xaml
    /// </summary>
    public partial class EditarServicosView : Window
    {
        private Servico _servico = new Servico();
        private ServicoController _servicoController = new ServicoController();
        public EditarServicosView(Servico servico)
        {
            InitializeComponent();
            _servico = servico;

            txtNome.Text = servico.NomeServico;
            txtValor.Text = servico.Valor.ToString("N2", new CultureInfo("pt-BR"));
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            
            string nomeServico = txtNome.Text.Trim();
            if (string.IsNullOrEmpty(nomeServico))
            {
                System.Windows.MessageBox.Show("O nome do serviço é obrigatório", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            decimal valorTotal;
            bool sucesso = decimal.TryParse(txtValor.Text, NumberStyles.Number, new CultureInfo("pt-BR"), out valorTotal); // Verificação do valor digitado

            if (!sucesso)
            {
                System.Windows.MessageBox.Show("Digite um valor numérico válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtValor.Text = string.Empty;
                txtValor.Focus();
                return;
            }

            _servico.NomeServico = nomeServico;
            _servico.Valor = Math.Round(valorTotal, 2);

            try
            {
                var (validado, mensagem) = _servicoController.AtualizarServico(_servico);

                if (validado)
                {
                    System.Windows.MessageBox.Show(mensagem, "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show(mensagem, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erro inesperado ao atualizar Serviço: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
