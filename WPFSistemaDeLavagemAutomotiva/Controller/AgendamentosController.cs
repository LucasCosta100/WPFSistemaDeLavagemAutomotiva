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
    public class AgendamentosController
    {
        private readonly AgendamentoService _agendamentoService;

        public AgendamentosController()
        {
            _agendamentoService = new AgendamentoService();
        }

        // ✅ Salvar
        public string SalvarAgendamento(Agendamento agendamento)
        {
            try
            {
                _agendamentoService.SalvarAgendamento(agendamento);
                return "Agendamento salvo com sucesso!";
            }
            catch (Exception ex)
            {
                return "Erro ao salvar agendamento: " + ex.Message;
            }
        }

        // ✅ Atualizar
        public string AtualizarAgendamento(Agendamento agendamento)
        {
            try
            {
                _agendamentoService.AtualizarAgendamento(agendamento);
                return "Agendamento atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                return "Erro ao atualizar agendamento: " + ex.Message;
            }
        }

        // ✅ Desativar
        public string DesativarAgendamento(Agendamento agendamento)
        {
            try
            {
                _agendamentoService.DesativarAgendamento(agendamento);
                return "Agendamento desativado com sucesso!";
            }
            catch (Exception ex)
            {
                return "Erro ao desativar agendamento: " + ex.Message;
            }
        }


        // ✅ Ativar
        public string AtivarAgendamento(Agendamento agendamento)
        {
            try
            {
                _agendamentoService.AtivarAgendamento(agendamento);
                return "Agendamento ativado com sucesso!";
            }
            catch (Exception ex)
            {
                return "Erro ao ativar agendamento: " + ex.Message;
            }

        }

        // ✅ Buscar por código
        public (Agendamento agendamento, string mensagem) BuscarPorCodigo(int idAgendamento)
        {
            try
            {
                var agendamento = _agendamentoService.BuscarPorCodigo(idAgendamento);
                return (agendamento, "Agendamento encontrado com sucesso!");
            }
            catch (Exception ex)
            {
                return (null, "Erro ao buscar agendamento: " + ex.Message);
            }
        }

        // ✅ Listar todos
        public (List<Agendamento> lista, string mensagem) ListarAgendamentos()
        {
            try
            {
                var lista = _agendamentoService.ListarAgendamentos();
                return (lista, $"Foram encontrados {lista.Count} agendamentos ativos.");
            }
            catch (Exception ex)
            {
                return (new List<Agendamento>(), "Erro ao listar agendamentos: " + ex.Message);
            }
        }

        public (List<Agendamento> lista, string mensagem) ListarAgendamentosHoje()
        {
       
            var hoje = DateTime.Today;
            var lista = _agendamentoService.ListarAgendamentos().Where(a => a.DataAgendada.Date == hoje).Where(a => a.StatusServico != "Concluído").ToList();
            return (lista, $"Foram encontrados {lista.Count} agendamentos para hoje.");
        }

        // ✅ Listar por status
        public (List<Agendamento> lista, string mensagem) ListarAgendamentosPorStatus(string status)
        {
            try
            {
                var lista = _agendamentoService.ListarAgendamentosPorStatus(status);
                return (lista, $"Foram encontrados {lista.Count} agendamentos com status '{status}'.");
            }
            catch (Exception ex)
            {
                return (new List<Agendamento>(), "Erro ao listar por status: " + ex.Message);
            }
        }

        // ✅ Buscar próximo
        public Agendamento BuscarProximoAgendamento()
        {
            try
            {
                var agendamento = _agendamentoService.BuscarProximoAgendamento();
                if (agendamento == null)
                    throw new Exception("Nenhum agendamento pendente encontrado.");
                return agendamento;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar próximo agendamento: " + ex.Message);
            }
        }
    }
}

