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
        void Salvar(Funcionario funcionario);
        void Atualizar(Funcionario funcionario);
        void Desativar(Funcionario funcionario);
        Funcionario BuscarPorCodigo(int _idFuncionario);
        List<Funcionario> BuscarTodos();
    }
}
