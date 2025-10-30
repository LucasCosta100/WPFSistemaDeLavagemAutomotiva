using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.DAO;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.Service
{
    public class FuncionarioService
    {
        private readonly FuncionarioDAO _funcionarioDAO;

        public FuncionarioService()
        {
            _funcionarioDAO = new FuncionarioDAO();
        }

        public void SalvarFuncionario(Funcionario funcionario)
        {
            //Validações básicas usando o try e catch
            if (string.IsNullOrWhiteSpace(funcionario.Nome))
                throw new ArgumentException("O nome do funcionário não pode estar vazio.");
            if (string.IsNullOrWhiteSpace(funcionario.Cargo))
                throw new ArgumentException("O cargo do funcionário não pode estar vazio.");
            if(string.IsNullOrEmpty(funcionario.Endereco.Rua) || string.IsNullOrEmpty(funcionario.Endereco.Cidade) || string.IsNullOrEmpty(funcionario.Endereco.Estado))
                throw new Exception("Endereço incompleto. Rua, cidade e estado são obrigatórios.");
            _funcionarioDAO.Salvar(funcionario);
        }

        public void AtualizarFuncionario(Funcionario funcionario)
        {
            if (string.IsNullOrWhiteSpace(funcionario.Nome))
                throw new ArgumentException("O nome do funcionário não pode estar vazio.");
            if (string.IsNullOrWhiteSpace(funcionario.Cargo))
                throw new ArgumentException("O cargo do funcionário não pode estar vazio.");
            if(string.IsNullOrEmpty(funcionario.Endereco.Rua) || string.IsNullOrEmpty(funcionario.Endereco.Cidade) || string.IsNullOrEmpty(funcionario.Endereco.Estado))
                throw new Exception("Endereço incompleto. Rua, cidade e estado são obrigatórios.");
            _funcionarioDAO.Atualizar(funcionario);
        }

        public void DesativarFuncionario(Funcionario funcionario)
        {
            var funcionarioExistente = _funcionarioDAO.BuscarPorCodigo(funcionario.IdFuncionario);

            if (!funcionario.Ativo)
                throw new Exception("Funcionário já está inativo.");
            funcionario.Ativo = false;
            if (funcionarioExistente == null)
                throw new Exception("Funcionário não encontrado.");

            _funcionarioDAO.Atualizar(funcionario);
        }

        public void AtivarFuncionario(Funcionario funcionario)
        {
            var funcionarioExistente = _funcionarioDAO.BuscarPorCodigo(funcionario.IdFuncionario);
            if (funcionario.Ativo)
                throw new Exception("Funcionário já está ativo.");
            funcionario.Ativo = true;
            if (funcionarioExistente == null)
                throw new Exception("Funcionário não encontrado.");
            _funcionarioDAO.Atualizar(funcionario);
        }

        public Funcionario BuscarPorCodigo(int idFuncionario)
        {
            if (idFuncionario <= 0)
                throw new ArgumentException("ID inválido.");
            if (_funcionarioDAO.BuscarPorCodigo(idFuncionario) == null)
                throw new Exception("Funcionário não encontrado.");
            return _funcionarioDAO.BuscarPorCodigo(idFuncionario);
        }

        public List<Funcionario> ListarFuncionarios()
        {
            if (_funcionarioDAO.BuscarTodos() == null || _funcionarioDAO.BuscarTodos().Count == 0)
                throw new Exception("Nenhum funcionário encontrado.");

            if (_funcionarioDAO.BuscarTodos().All(c => !c.Ativo))
                throw new Exception("Nenhum funcionário ativo encontrado.");

            return _funcionarioDAO.BuscarTodos().Where(c => c.Ativo).ToList(); //Retorna apenas clientes ativos
        }
    }
}
