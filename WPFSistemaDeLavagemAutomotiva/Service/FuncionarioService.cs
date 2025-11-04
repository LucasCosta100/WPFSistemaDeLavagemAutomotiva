using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public void CadastrarFuncionario(Funcionario func)
        {
            if (string.IsNullOrWhiteSpace(func.Nome))
                throw new ArgumentException("O nome do funcionário é obrigatório.");

            if (string.IsNullOrWhiteSpace(func.Usuario))
                throw new ArgumentException("O usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(func.SenhaHash))
                throw new ArgumentException("A senha é obrigatória.");

            try
            {
                _funcionarioDAO.Salvar(func);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AtualizarFuncionario(Funcionario funcionario)//Método para atualizar funcionário com validações
        {
            if (string.IsNullOrWhiteSpace(funcionario.Nome))
                throw new ArgumentException("O nome do funcionário não pode estar vazio.");
            if (string.IsNullOrWhiteSpace(funcionario.Cargo))
                throw new ArgumentException("O cargo do funcionário não pode estar vazio.");
            if(string.IsNullOrEmpty(funcionario.Endereco.Rua) || string.IsNullOrEmpty(funcionario.Endereco.Cidade) || string.IsNullOrEmpty(funcionario.Endereco.Estado))
                throw new Exception("Endereço incompleto. Rua, cidade e estado são obrigatórios.");
            _funcionarioDAO.Atualizar(funcionario);
        }

        public void DesativarFuncionario(Funcionario funcionario)//Método para desativar funcionário com validações
        {
            if (funcionario == null)
                throw new Exception("Cliente não encontrado.");
            if (funcionario.Ativo == false)
                throw new Exception("O cliente já está inativo.");
            funcionario.Ativo = false;

            _funcionarioDAO.Desativar(funcionario);
        }

        public void AtivarFuncionario(Funcionario funcionario)//Método para ativar funcionário com validações
        {
            var funcionarioExistente = _funcionarioDAO.BuscarPorCodigo(funcionario.IdFuncionario);
            if (funcionario.Ativo)
                throw new Exception("Funcionário já está ativo.");
            funcionario.Ativo = true;
            if (funcionarioExistente == null)
                throw new Exception("Funcionário não encontrado.");
            _funcionarioDAO.Atualizar(funcionario);
        }

        public Funcionario BuscarPorCodigo(int idFuncionario)//Método para buscar funcionário por código com validações
        {
            if (idFuncionario <= 0)
                throw new ArgumentException("ID inválido.");
            if (_funcionarioDAO.BuscarPorCodigo(idFuncionario) == null)
                throw new Exception("Funcionário não encontrado.");
            return _funcionarioDAO.BuscarPorCodigo(idFuncionario);
        }

        public List<Funcionario> ListarFuncionarios()//Método para listar todos os funcionários com validações
        {
            if (_funcionarioDAO.BuscarTodos() == null || _funcionarioDAO.BuscarTodos().Count == 0)
                throw new Exception("Nenhum funcionário encontrado.");

            if (_funcionarioDAO.BuscarTodos().All(c => !c.Ativo))
                throw new Exception("Nenhum funcionário ativo encontrado.");

            return _funcionarioDAO.BuscarTodos().Where(c => c.Ativo).ToList(); //Retorna apenas clientes ativos
        }

        public Funcionario ValidarLogin(string usuario, string senhaHash)
        {
             // Validação de login
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senhaHash))
                throw new ArgumentException("Usuário e senha são obrigatórios.");

            return _funcionarioDAO.ObterUsuarioeSenha(usuario, senhaHash);
        }
    }
}
