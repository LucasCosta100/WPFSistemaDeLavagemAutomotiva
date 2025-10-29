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
        void buscarPorCodigo(int _idServico);
        List<Servico> buscarTodos();
    }
}
