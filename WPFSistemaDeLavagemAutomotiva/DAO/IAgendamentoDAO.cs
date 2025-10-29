using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface IAgendamentoDAO
    {
        void salvar(Agendamento agendamento);
        void atualizar(Agendamento agendamento);
        void deletar(Agendamento agendamento);
<<<<<<< HEAD
        Agendamento buscarPorcodigo(int idAgendamento);
=======
        void buscarPorCodigo(int _idAgendamento);
>>>>>>> c03e0240500c1fbbf36ef5907a2ce8898bfe2a90
        List<Agendamento> buscarTodos();
    }
}
