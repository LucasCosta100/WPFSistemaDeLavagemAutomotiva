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
        private readonly AgendamentoDAO _agendamentoDAO;//Instância do AgendamentoDAO para operações de banco de dados

        public AgendamentoService()//Construtor da classe
        {
            _agendamentoDAO = new AgendamentoDAO();//Inicializa o AgendamentoDAO
        }

        public void SalvarAgendamento(Agendamento agendamento)//Método para salvar agendamento com validações
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

        public void AtualizarAgendamento(Agendamento agendamento)//Método para atualizar agendamento com validações
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
        
        public void DesativarAgendamento(Agendamento agendamento)//Método para desativar agendamento com validações
        {
            var agendamentoExistente = _agendamentoDAO.BuscarPorCodigo(agendamento.IdAgendamento);
            if (!agendamento.Ativo)
                throw new Exception("Agendamento já está inativo.");
            agendamento.Ativo = false;
            if (agendamentoExistente == null)
                throw new Exception("Agendamento não encontrado.");
            _agendamentoDAO.Atualizar(agendamento);
        }

        public void AtivarAgendamento(Agendamento agendamento)//Método para ativar agendamento com validações
        {
            var agendamentoExistente = _agendamentoDAO.BuscarPorCodigo(agendamento.IdAgendamento);
            if (agendamento.Ativo)
                throw new Exception("Agendamento já está ativo.");
            agendamento.Ativo = true;
            if (agendamentoExistente == null)
                throw new Exception("Agendamento não encontrado.");
            _agendamentoDAO.Atualizar(agendamento);
        }

        public Agendamento BuscarPorCodigo(int idAgendamento)//Método para buscar agendamento por ID com validações
        {
            if (idAgendamento <= 0)
                throw new ArgumentException("ID inválido.");
            if (_agendamentoDAO.BuscarPorCodigo(idAgendamento) == null)
                throw new Exception("Agendamento não encontrado.");
            if (!_agendamentoDAO.BuscarPorCodigo(idAgendamento).Ativo)
                throw new Exception("O agendamento está inativo.");
            return _agendamentoDAO.BuscarPorCodigo(idAgendamento);
        }

        public List<Agendamento> ListarAgendamentos()//Método para listar todos os agendamentos com validações
        {
            if (_agendamentoDAO.BuscarTodos() == null || _agendamentoDAO.BuscarTodos().Count == 0)
                throw new Exception("Nenhum agendamento encontrado.");
            return _agendamentoDAO.BuscarTodos().Where(a => a.Ativo).ToList();
        }

        public List<Agendamento> ListarAgendamentosPorStatus(string status)
        {
            if (status != "Pendente" && status != "Concluído" && status != "Cancelado" && status != "Em Andamento")
                throw new Exception("Status do serviço inválido. Use 'Pendente', 'Em Andamento', 'Concluído' ou 'Cancelado'.");
            return _agendamentoDAO.BuscarPorStatus(status);
        } 

        public Agendamento BuscarProximoAgendamento()
        {
            if (_agendamentoDAO.BuscarProximoAgendamento() == null)
                throw new Exception("Nenhum agendamento futuro encontrado.");
            return _agendamentoDAO.BuscarProximoAgendamento();
        }
    }
}
