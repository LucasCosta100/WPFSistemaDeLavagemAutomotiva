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
        Agendamento buscarPorcodigo(int idAgendamento);
        List<Agendamento> buscarTodos();
    }
}
