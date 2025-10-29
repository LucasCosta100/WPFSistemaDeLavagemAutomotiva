using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface IEnderecoDAO
    {
        void salvar(Endereco endereco);
        void atualizar(Endereco endereco);
        void desativar(Endereco endereco);
        Endereco buscarPorCodigo(int idEndereco);
        List<Endereco> buscarTodos();
    }
}
