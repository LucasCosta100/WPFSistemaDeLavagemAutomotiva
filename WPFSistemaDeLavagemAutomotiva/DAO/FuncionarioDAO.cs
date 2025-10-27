using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface FuncionarioDAO
    {
        void salvar(Funcionario funcionario);
        void atualizar(Funcionario funcionario);
        void deletar(Funcionario funcionario);

        Agendamento BuscarPorCodigo(int _idFuncionario);
        List<Agendamento> ListarTodos();
    }
}
