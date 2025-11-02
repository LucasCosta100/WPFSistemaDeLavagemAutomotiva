using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFSistemaDeLavagemAutomotiva.DAO;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva
{
    public class ClienteService
    {
        private readonly ClienteDAO _clienteDAO;

        public ClienteService()
        {
            _clienteDAO = new ClienteDAO();
        }

        public void SalvarCliente(Cliente cliente)//Método para salvar cliente com validações
        {
            //Validações básicas usando o try e catch

            if (cliente.Telefone.Length < 10)
                throw new ArgumentException("O telefone deve conter pelo menos 10 dígitos.");

            if (string.IsNullOrWhiteSpace(cliente.Nome)) 
                throw new ArgumentException("O nome do cliente não pode estar vazio.");

            if (string.IsNullOrWhiteSpace(cliente.Email)) 
                throw new Exception("O e-mail é obrigatório.");

            if (!cliente.Email.Contains("@") || !cliente.Email.Contains(".")) 
                throw new Exception("E-mail inválido.");

            if (!string.IsNullOrWhiteSpace(cliente.Telefone) && cliente.Telefone.Length < 8) 
                throw new Exception("Telefone inválido.");

            if(string.IsNullOrEmpty(cliente.EnderecoCliente.Rua) || string.IsNullOrEmpty(cliente.EnderecoCliente.Cidade) || string.IsNullOrEmpty(cliente.EnderecoCliente.Estado))
                throw new Exception("Endereço incompleto. Rua, cidade e estado são obrigatórios.");

            _clienteDAO.Salvar(cliente);
        }

        public void AtualizarCliente(Cliente cliente)//Método para atualizar cliente com validações
        {
            if (cliente.Telefone.Length < 10)
                throw new ArgumentException("O telefone deve conter pelo menos 10 dígitos.");

            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new ArgumentException("O nome do cliente não pode estar vazio.");

            if (string.IsNullOrWhiteSpace(cliente.Email))
                throw new Exception("O e-mail é obrigatório.");

            if (!cliente.Email.Contains("@") || !cliente.Email.Contains("."))
                throw new Exception("E-mail inválido.");

            if (!string.IsNullOrWhiteSpace(cliente.Telefone) && cliente.Telefone.Length < 8)
                throw new Exception("Telefone inválido.");

            if (string.IsNullOrEmpty(cliente.EnderecoCliente.Rua) || string.IsNullOrEmpty(cliente.EnderecoCliente.Cidade) || string.IsNullOrEmpty(cliente.EnderecoCliente.Estado))
                throw new Exception("Endereço incompleto. Rua, cidade e estado são obrigatórios.");

            if (!cliente.Ativo)
                throw new Exception("Não é possível atualizar um cliente inativo.");

            _clienteDAO.Atualizar(cliente);
        }

        public void DesativarCliente(Cliente cliente)//Método para desativar cliente com validações
        {
            if (cliente == null)
                throw new Exception("Cliente não encontrado.");
            if (cliente.Ativo == false)
                throw new Exception("O cliente já está inativo.");
            cliente.Ativo = false;

            _clienteDAO.Desativar(cliente);
        }

        public void AtivarCliente(Cliente cliente)//Método para ativar cliente com validações
        {
            var clienteExistente = _clienteDAO.BuscarPorCodigo(cliente.IdCliente);
            if (clienteExistente == null)
                throw new Exception("Cliente não encontrado.");
            if (cliente.Ativo)
                throw new Exception("O cliente já está ativo.");
            cliente.Ativo = true;
            _clienteDAO.Atualizar(cliente);
        }

        public Cliente BuscarPorCodigo(int id)//Método para buscar cliente por ID com validações
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");
            if (_clienteDAO.BuscarPorCodigo(id) == null)
                throw new Exception("Cliente não encontrado.");

            return _clienteDAO.BuscarPorCodigo(id);
        }

        public List<Cliente> ListarClientes()//Método para listar todos os clientes com validações
        {
            if (_clienteDAO.BuscarTodos() == null || _clienteDAO.BuscarTodos().Count == 0)
                throw new Exception("Nenhum cliente encontrado.");

            if (_clienteDAO.BuscarTodos().All(c => !c.Ativo))
                throw new Exception("Nenhum cliente ativo encontrado.");
          
            return _clienteDAO.BuscarTodos().Where(c => c.Ativo).ToList(); //Retorna apenas clientes ativos
        }

    }
}
