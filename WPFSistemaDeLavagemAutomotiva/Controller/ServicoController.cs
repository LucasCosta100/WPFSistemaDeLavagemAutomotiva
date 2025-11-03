using System;
using System.Collections.Generic;
using WPFSistemaDeLavagemAutomotiva.Models;
using WPFSistemaDeLavagemAutomotiva.Service;

namespace WPFSistemaDeLavagemAutomotiva.Controller
{
    public class ServicoController
    {
        private readonly ServicoService _servicoService;

        public ServicoController()
        {
            _servicoService = new ServicoService();
        }

        // Salvar um novo serviço
        public void SalvarServico(Servico servico)
        {
            try
            {
                _servicoService.SalvarServico(servico);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar serviço: {ex.Message}");
            }
        }

        // Atualizar um serviço existente
        public void AtualizarServico(Servico servico)
        {
            try
            {
                _servicoService.AtualizarServico(servico);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar serviço: {ex.Message}");
            }
        }

        // Desativar serviço
        public void DesativarServico(Servico servico)
        {
            try
            {
                _servicoService.DesativarServico(servico);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao desativar serviço: {ex.Message}");
            }
        }

        // Ativar serviço
        public void AtivarServico(Servico servico)
        {
            try
            {
                _servicoService.AtivarServico(servico);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao ativar serviço: {ex.Message}");
            }
        }

        // Buscar serviço por ID
        public Servico BuscarPorCodigo(int idServico)
        {
            try
            {
                return _servicoService.BuscarPorCodigo(idServico);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar serviço: {ex.Message}");
            }
        }

        // Listar todos os serviços ativos
        public List<Servico> ListarServicos()
        {
            try
            {
                return _servicoService.ListarServicos();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar serviços: {ex.Message}");
            }
        }
    }
}
