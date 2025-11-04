using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFSistemaDeLavagemAutomotiva.Models;
using WPFSistemaDeLavagemAutomotiva.Service;

namespace WPFSistemaDeLavagemAutomotiva.Controller
{
    public class FuncionarioController
    {
        private readonly FuncionarioService _funcionarioService;

        public FuncionarioController()
        {
            _funcionarioService = new FuncionarioService();
        }

        public void SalvarFuncionario(Funcionario funcionario)
        {
            try
            {
                _funcionarioService.CadastrarFuncionario(funcionario);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Validar login
        public Funcionario ValidarLogin(string usuario, string senhaHash)
        {
            try
            {
                return _funcionarioService.ValidarLogin(usuario, senhaHash);
            }
            catch
            {
                // Retorna null se houver erro
                return null;
            }
        }

        public (bool sucesso, string mensagem) AtualizarFuncionario(Funcionario funcionario)
        {
            try
            {
                _funcionarioService.AtualizarFuncionario(funcionario);
                return (true, "Funcionário atualizado com sucesso!");
            }
            catch (ArgumentException ex)
            {
                return (false, $"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar funcionário: {ex.Message}");
            }
        }

        public (bool sucesso, string mensagem) DesativarFuncionario(Funcionario funcionario)
        {
            try
            {
                _funcionarioService.DesativarFuncionario(funcionario);
                return (true, "Funcionário desativado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao desativar funcionário: {ex.Message}");
            }
        }

        public (bool sucesso, string mensagem) AtivarFuncionario(Funcionario funcionario)
        {
            try
            {
                _funcionarioService.AtivarFuncionario(funcionario);
                return (true, "Funcionário ativado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao ativar funcionário: {ex.Message}");
            }
        }

        public (Funcionario funcionario, string mensagem) BuscarPorCodigo(int idFuncionario)
        {
            try
            {
                var funcionario = _funcionarioService.BuscarPorCodigo(idFuncionario);
                return (funcionario, "Funcionário encontrado com sucesso!");
            }
            catch (Exception ex)
            {
                return (null, $"Erro ao buscar funcionário: {ex.Message}");
            }
        }

        public (List<Funcionario> funcionarios, string mensagem) ListarFuncionarios()
        {
            try
            {
                var funcionarios = _funcionarioService.ListarFuncionarios();
                return (funcionarios, "Lista de funcionários carregada com sucesso!");
            }
            catch (Exception ex)
            {
                return (new List<Funcionario>(), $"Erro ao listar funcionários: {ex.Message}");
            }
        }
    }
}
