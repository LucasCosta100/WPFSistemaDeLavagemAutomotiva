using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface IFuncionarioDAO
    {
        void salvar(Funcionario funcionario);
        void atualizar(Funcionario funcionario);
        void desativar(Funcionario funcionario);
        Funcionario buscarPorCodigo(int _idFuncionario);
        List<Funcionario> buscarTodos();
    }
}
