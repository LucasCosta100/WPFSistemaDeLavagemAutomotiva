using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.DAO;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.Service
{
    public class EnderecoService
    {
        private readonly EnderecoDAO _enderecoDAO;

        public EnderecoService()
        {
            _enderecoDAO = new EnderecoDAO();
        }

        public void SalvarEndereco(Endereco endereco)
        {
            if(string.IsNullOrEmpty(endereco.Rua) || string.IsNullOrEmpty(endereco.Cidade) || string.IsNullOrEmpty(endereco.Estado))
                throw new Exception("Endereço incompleto. Rua, cidade e estado são obrigatórios.");
            _enderecoDAO.Salvar(endereco);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            if(string.IsNullOrEmpty(endereco.Rua) || string.IsNullOrEmpty(endereco.Cidade) || string.IsNullOrEmpty(endereco.Estado))
                throw new Exception("Endereço incompleto. Rua, cidade e estado são obrigatórios.");
            _enderecoDAO.Atualizar(endereco);
        }

        public Endereco BuscarPorCodigo(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");
            if (_enderecoDAO.BuscarPorCodigo(id) == null)
                throw new Exception("Endereço não encontrado.");
            return _enderecoDAO.BuscarPorCodigo(id);
        }

        public List<Endereco> ListarEnderecos()
        {
            if (_enderecoDAO.BuscarTodos() == null || _enderecoDAO.BuscarTodos().Count == 0)
                throw new Exception("Nenhum endereço encontrado.");

            var enderecos = _enderecoDAO.BuscarTodos();

            return enderecos;
        }


    }
}
