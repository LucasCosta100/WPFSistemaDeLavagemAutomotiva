using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface IServicoDAO
    {
        void salvar(Servico servico);
        void atualizar(Servico servico);
        void deletar(Servico servico);
<<<<<<< HEAD
        Servico buscarPorCodigo(int idServico);
=======
        void buscarPorCodigo(int _idServico);
>>>>>>> c03e0240500c1fbbf36ef5907a2ce8898bfe2a90
        List<Servico> buscarTodos();
    }
}
