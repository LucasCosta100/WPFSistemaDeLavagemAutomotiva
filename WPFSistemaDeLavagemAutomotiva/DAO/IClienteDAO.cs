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
        void Salvar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Desativar(Cliente cliente);
        Cliente BuscarPorCodigo(int idCliente);
        List<Cliente> BuscarTodos();
    }
}
