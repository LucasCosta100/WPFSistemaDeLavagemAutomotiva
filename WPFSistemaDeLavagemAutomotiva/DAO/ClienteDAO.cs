using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface ClienteDAO
    {
        void salvar(Cliente cliente);
        void atualizar(Cliente cliente);
        void deletar(Cliente cliente);

        Agendamento BuscarPorCodigo(int _idCliente);
        List<Agendamento> ListarTodos();
    }
}
