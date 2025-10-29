﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface IAgendamentoDAO
    {
        void Salvar(Agendamento agendamento);
        void Atualizar(Agendamento agendamento);
        void Desativar(Agendamento agendamento);
        Agendamento BuscarPorCodigo(int idAgendamento);
        List<Agendamento> BuscarTodos();
    }
}
