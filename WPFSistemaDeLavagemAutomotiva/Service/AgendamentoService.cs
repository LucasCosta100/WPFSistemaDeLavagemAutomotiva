using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.DAO;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.Service
{
    public class AgendamentoService
    {
        private readonly AgendamentoDAO _agendamentoDAO;

        public AgendamentoService()
        {
            _agendamentoDAO = new AgendamentoDAO();
        }

        public void SalvarAgendamento(Agendamento agendamento)
        {
            //Validações básicas usando o try e catch
            if (agendamento.DataAgendada < DateTime.Now)
                throw new ArgumentException("A data e hora do agendamento não podem ser no passado.");
            if (agendamento.HoraAgendamento < TimeSpan.Zero || agendamento.HoraAgendamento > new TimeSpan(23, 59, 59))
                throw new ArgumentException("A hora do agendamento é inválida.");
            if (agendamento.ClienteAgendado == null)
                throw new ArgumentException("O agendamento deve estar associado a um cliente.");
            if (agendamento.ServicoAgendado == null)
                throw new ArgumentException("O agendamento deve estar associado a um serviço.");
            if (agendamento.ValorTotal < 0)
                throw new ArgumentException("O valor total do agendamento não pode ser negativo.");
            if (string.IsNullOrWhiteSpace(agendamento.StatusServico))
                throw new ArgumentException("O status do serviço não pode estar vazio.");
            if (!agendamento.Ativo)
                throw new Exception("Não é possível salvar um agendamento inativo.");
            if (agendamento.ClienteAgendado != null && !agendamento.ClienteAgendado.Ativo)
                throw new Exception("Não é possível agendar para um cliente inativo.");
            _agendamentoDAO.Salvar(agendamento);
        }

        public void AtualizarAgendamento(Agendamento agendamento)
        {
            if (agendamento.DataAgendada < DateTime.Now)
                throw new ArgumentException("A data e hora do agendamento não podem ser no passado.");
            if (agendamento.HoraAgendamento < TimeSpan.Zero || agendamento.HoraAgendamento > new TimeSpan(23, 59, 59))
                throw new ArgumentException("A hora do agendamento é inválida.");
            if (agendamento.ClienteAgendado == null)
                throw new ArgumentException("O agendamento deve estar associado a um cliente.");
            if (agendamento.ServicoAgendado == null)
                throw new ArgumentException("O agendamento deve estar associado a um serviço.");
            if (agendamento.ValorTotal < 0)
                throw new ArgumentException("O valor total do agendamento não pode ser negativo.");
            if (string.IsNullOrWhiteSpace(agendamento.StatusServico))
                throw new ArgumentException("O status do serviço não pode estar vazio.");
            if (!agendamento.Ativo)
                throw new Exception("Não é possível atualizar um agendamento inativo.");
            if (agendamento.ClienteAgendado != null && !agendamento.ClienteAgendado.Ativo)
                throw new Exception("Não é possível agendar para um cliente inativo.");

            if(agendamento.StatusServico != "Pendente" && agendamento.StatusServico != "Concluído" && agendamento.StatusServico != "Cancelado" && agendamento.StatusServico != "Em Andamento")
                throw new Exception("Status do serviço inválido. Use 'Pendente', 'Em Andamento', 'Concluído' ou 'Cancelado'.");
            _agendamentoDAO.Atualizar(agendamento);
        }
        
        public void DesativarAgendamento(Agendamento agendamento)
        {
            var agendamentoExistente = _agendamentoDAO.BuscarPorCodigo(agendamento.IdAgendamento);
            if (!agendamento.Ativo)
                throw new Exception("Agendamento já está inativo.");
            agendamento.Ativo = false;
            if (agendamentoExistente == null)
                throw new Exception("Agendamento não encontrado.");
            _agendamentoDAO.Atualizar(agendamento);
        }

        public void AtivarAgendamento(Agendamento agendamento)
        {
            var agendamentoExistente = _agendamentoDAO.BuscarPorCodigo(agendamento.IdAgendamento);
            if (agendamento.Ativo)
                throw new Exception("Agendamento já está ativo.");
            agendamento.Ativo = true;
            if (agendamentoExistente == null)
                throw new Exception("Agendamento não encontrado.");
            _agendamentoDAO.Atualizar(agendamento);
        }

        public Agendamento BuscarPorCodigo(int idAgendamento)
        {
            if (idAgendamento <= 0)
                throw new ArgumentException("ID inválido.");
            if (_agendamentoDAO.BuscarPorCodigo(idAgendamento) == null)
                throw new Exception("Agendamento não encontrado.");
            if (!_agendamentoDAO.BuscarPorCodigo(idAgendamento).Ativo)
                throw new Exception("O agendamento está inativo.");
            return _agendamentoDAO.BuscarPorCodigo(idAgendamento);
        }

        public List<Agendamento> ListarAgendamentos()
        {
            if (_agendamentoDAO.BuscarTodos() == null || _agendamentoDAO.BuscarTodos().Count == 0)
                throw new Exception("Nenhum agendamento encontrado.");
            return _agendamentoDAO.BuscarTodos().Where(a => a.Ativo).ToList();
        }

        public List<Agendamento> ListarAgendamentosPendente()
        {
            if (_agendamentoDAO.BuscarClientesPendente() == null || _agendamentoDAO.BuscarClientesPendente().Count == 0)
                throw new Exception("Nenhum agendamento pendente encontrado.");
            return _agendamentoDAO.BuscarClientesPendente().Where(a => a.Ativo).ToList();
        }

        public List<Agendamento> ListarAgendamentoEmAndamento()
        {
            if (_agendamentoDAO.BuscarClientesEmAndamento() == null || _agendamentoDAO.BuscarClientesEmAndamento().Count == 0)
                throw new Exception("Nenhum agendamento em andamento encontrado.");
            return _agendamentoDAO.BuscarClientesEmAndamento().Where(a => a.Ativo).ToList();
        }

        public List<Agendamento> ListarAgendamentosConcluidos()
        {
            if (_agendamentoDAO.BuscarClientesConcluido() == null || _agendamentoDAO.BuscarClientesConcluido().Count == 0)
                throw new Exception("Nenhum agendamento concluído encontrado.");
            return _agendamentoDAO.BuscarClientesConcluido().Where(a => a.Ativo).ToList();
        }

        public List<Agendamento> ListarAgendamentosCancelados()
        {
            if (_agendamentoDAO.BuscarClientesCancelado() == null || _agendamentoDAO.BuscarClientesCancelado().Count == 0)
                throw new Exception("Nenhum agendamento cancelado encontrado.");
            return _agendamentoDAO.BuscarClientesCancelado().Where(a => a.Ativo).ToList();
        }   
    }
}
