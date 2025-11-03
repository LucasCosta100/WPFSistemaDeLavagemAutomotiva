using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.DAO;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.Service
{
    public class ServicoService
    {
        private readonly ServicoDAO _servicoDAO;

        public ServicoService()
        {
            _servicoDAO = new ServicoDAO();
        }

        public void SalvarServicos(Servico servico)
        {
            //Validações básicas usando o try e catch
            if (string.IsNullOrWhiteSpace(servico.NomeServico))
                throw new ArgumentException("O nome do serviço não pode estar vazio.");
            if (servico.Valor < 0)
                throw new ArgumentException("O preço do serviço não pode ser negativo.");
            _servicoDAO.Salvar(servico);
        }

        public void AtualizarServico(Servico servico)
        {
            if (string.IsNullOrWhiteSpace(servico.NomeServico))
                throw new ArgumentException("O nome do serviço não pode estar vazio.");
            if (servico.Valor < 0)
                throw new ArgumentException("O preço do serviço não pode ser negativo.");
            _servicoDAO.Atualizar(servico);
        }

        public void DesativarServico(Servico servico)
        {
            var servicoExistente = _servicoDAO.BuscarPorCodigo(servico.IdServico);
            if (!servico.Ativo)
                throw new Exception("Serviço já está inativo.");
            servico.Ativo = false;
            if (servicoExistente == null)
                throw new Exception("Serviço não encontrado.");
            _servicoDAO.Atualizar(servico);
        }

        public void AtivarServico(Servico servico)
        {
            var servicoExistente = _servicoDAO.BuscarPorCodigo(servico.IdServico);
            if (servico.Ativo)
                throw new Exception("Serviço já está ativo.");
            servico.Ativo = true;
            if (servicoExistente == null)
                throw new Exception("Serviço não encontrado.");
            _servicoDAO.Atualizar(servico);
        }

        public Servico BuscarPorCodigo(int idServico)
        {
            if (idServico <= 0)
                throw new ArgumentException("ID inválido.");
            if (_servicoDAO.BuscarPorCodigo(idServico) == null)
                throw new Exception("Serviço não encontrado.");
            return _servicoDAO.BuscarPorCodigo(idServico);
        }

        public List<Servico> ListarServicos()
        {
            if (_servicoDAO.BuscarTodos() == null || _servicoDAO.BuscarTodos().Count == 0)
                throw new Exception("Nenhum serviço encontrado.");

            if (_servicoDAO.BuscarTodos().All(c => !c.Ativo))
                throw new Exception("Nenhum serviço ativo encontrado.");

            return _servicoDAO.BuscarTodos().Where(c => c.Ativo).ToList(); //Retorna apenas clientes ativos
        }

    }
}
