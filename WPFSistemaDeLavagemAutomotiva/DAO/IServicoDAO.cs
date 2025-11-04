using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface IServicoDAO//Interface para operações de Serviço
    {
        void Salvar(Servico servico);
        void Atualizar(Servico servico);
        void Desativar(Servico servico);
        Servico BuscarPorCodigo(int idServico);
        Servico BuscarPorNome(string nomeServico);
        List<Servico> BuscarTodos();
    }
}
