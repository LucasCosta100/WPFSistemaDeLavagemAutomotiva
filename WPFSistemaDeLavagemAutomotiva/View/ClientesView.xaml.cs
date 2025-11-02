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
using WPFSistemaDeLavagemAutomotiva.DAO;

namespace WPFSistemaDeLavagemAutomotiva.View
{
    /// <summary>
    /// Interação lógica para ClientesView.xam
    /// </summary>
    public partial class ClientesView : Page
    {
        private ClienteService _clienteService = new ClienteService();
        public ClientesView()
        {
            InitializeComponent();
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            dgClientes.Items.Clear();
            var clientes = _clienteService.ListarClientes();
            dgClientes.ItemsSource = clientes;
            tbTotalClientes.Text = $"Total de Clientes: {clientes.Count.ToString()}";
        }
    }
}
