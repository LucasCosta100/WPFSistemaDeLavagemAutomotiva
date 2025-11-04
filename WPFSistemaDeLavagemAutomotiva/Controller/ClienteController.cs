using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.Controller
{
    public class ClienteController
    {
        private readonly ClienteService _clienteService;

        public ClienteController()
        {
            _clienteService = new ClienteService();
        }

        // Salva um novo cliente.
        public (bool sucesso, string mensagem) SalvarCliente(Cliente cliente)
        {
            try
            {
                _clienteService.SalvarCliente(cliente);
                return (true, "Cliente salvo com sucesso!");
            }
            catch (ArgumentException ex)
            {
                return (false, $"Atenção: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao salvar cliente: {ex.Message}");
            }
        }

        // Atualiza um cliente existente.
        public (bool sucesso, string mensagem) AtualizarCliente(Cliente cliente)
        {
            try
            {
                _clienteService.AtualizarCliente(cliente);
                return (true, "Cliente atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar cliente: {ex.Message}");
            }
        }

        // Desativa um cliente (marcando como inativo).

        public (bool sucesso, string mensagem) DesativarCliente(Cliente cliente)
        {
            try
            {
                _clienteService.DesativarCliente(cliente);
                return (true, "Cliente desativado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao desativar cliente: {ex.Message}");
            }
        }


        // Ativa um cliente previamente desativado.

        public (bool sucesso, string mensagem) AtivarCliente(Cliente cliente)
        {
            try
            {
                _clienteService.AtivarCliente(cliente);
                return (true, "Cliente ativado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao ativar cliente: {ex.Message}");
            }
        }

        // Busca um cliente pelo ID.
        public (Cliente cliente, string mensagem) BuscarPorCodigo(int id)
        {
            try
            {
                var cliente = _clienteService.BuscarPorCodigo(id);
                return (cliente, "Cliente encontrado com sucesso!");
            }
            catch (Exception ex)
            {
                return (null, $"Erro: {ex.Message}");
            }
        }

        // Retorna a lista de clientes ativos.
        public (List<Cliente> listaClientes, string mensagem) ListarClientes()
        {
            try
            {
                var clientes = _clienteService.ListarClientes();
                return (clientes, "Lista carregada com sucesso!");
            }
            catch (Exception ex)
            {
                return (new List<Cliente>(), $"Erro ao listar clientes: {ex.Message}");
            }
        }
    }
}
    

