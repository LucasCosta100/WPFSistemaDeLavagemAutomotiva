using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public interface IEnderecoDAO//Interface para operações de Endereço
    {
        int Salvar(Endereco endereco);
        void Atualizar(Endereco endereco);
        Endereco BuscarPorCodigo(int idEndereco);
        List<Endereco> BuscarTodos();
    }
}
