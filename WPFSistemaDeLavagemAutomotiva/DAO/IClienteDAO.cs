using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface IClienteDAO
    {
        void salvar(Cliente cliente);
        void atualizar(Cliente cliente);
        void desativar(Cliente cliente);
        Cliente buscarPorCodigo(int idCliente);
        List<Cliente> buscarTodos();
    }
}
