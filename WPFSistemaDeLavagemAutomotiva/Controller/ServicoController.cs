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

        // ✅ Salvar um novo serviço
        public (bool sucesso, string mensagem) SalvarServico(Servico servico)
        {
            try
            {
                _servicoService.SalvarServico(servico);
                return (true, "Serviço salvo com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao salvar serviço: {ex.Message}");
            }
        }

        // ✅ Atualizar serviço existente
        public (bool sucesso, string mensagem) AtualizarServico(Servico servico)
        {
            try
            {
                _servicoService.AtualizarServico(servico);
                return (true, "Serviço atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar serviço: {ex.Message}");
            }
        }

        // ✅ Desativar serviço
        public (bool sucesso, string mensagem) DesativarServico(Servico servico)
        {
            try
            {
                _servicoService.DesativarServico(servico);
                return (true, "Serviço desativado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao desativar serviço: {ex.Message}");
            }
        }

        // ✅ Ativar serviço
        public (bool sucesso, string mensagem) AtivarServico(Servico servico)
        {
            try
            {
                _servicoService.AtivarServico(servico);
                return (true, "Serviço ativado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao ativar serviço: {ex.Message}");
            }
        }

        // ✅ Buscar serviço por código
        public (bool sucesso, Servico servico, string mensagem) BuscarPorCodigo(int idServico)
        {
            try
            {
                var servico = _servicoService.BuscarPorCodigo(idServico);
                return (true, servico, "Serviço encontrado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, null, $"Erro ao buscar serviço: {ex.Message}");
            }
        }

        // ✅ Listar todos os serviços ativos
        public (bool sucesso, List<Servico> servicos, string mensagem) ListarServicos()
        {
            try
            {
                var lista = _servicoService.ListarServicos();
                return (true, lista, "Serviços carregados com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, new List<Servico>(), $"Erro ao listar serviços: {ex.Message}");
            }
        }
    }
}
