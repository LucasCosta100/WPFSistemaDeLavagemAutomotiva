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

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para FuncionariosView.xam
    /// </summary>
    public partial class FuncionariosView : Page
    {
        FuncionarioService funcionarioService = new FuncionarioService();
        public FuncionariosView()
        {
            InitializeComponent();
            ListarFuncionario();
        }

        private void ListarFuncionario()
        {
            dgFuncionario.Items.Clear();
            var ListarFuncionarios = funcionarioService.ListarFuncionarios();
            dgFuncionario.ItemsSource = ListarFuncionarios;
            tbTotalFuncionarios.Text = $"Total de Funcionários: {ListarFuncionarios.Count().ToString()}";
        }
    }
}
