using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSistemaDeLavagemAutomotiva.Models
{
   public class Agendamento
    {   //Atributos e Propriedades
        private int _idAgendamento;
        public int IdAgendamento
        {
            get { return _idAgendamento; }
            set { _idAgendamento = value; }
        }
        private Cliente _cliente;
        public Cliente ClienteAgendado
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        private Servico _servico;
        public Servico ServicoAgendado
        {
            get { return _servico; }
            set { _servico = value; }
        }
        private DateTime _dataAgendada;
        public DateTime DataAgendada
        {
            get { return _dataAgendada; }
            set { _dataAgendada = value; }
        }

        private TimeSpan _horaAgendamento;
        public TimeSpan HoraAgendamento
        {
            get { return _horaAgendamento; }
            set { _horaAgendamento = value; }
        }

        private string _statusServico;
        public string StatusServico
        {
            get { return _statusServico; }
            set { _statusServico = value; }
        }

        private double _valorTotal;
        public double ValorTotal
        {
            get { return _valorTotal; }
            set { _valorTotal = value; }
        }

        private bool _ativo;
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public Agendamento() { } // Contrutor padrão

        public Agendamento(int idAgendamento, Cliente cliente, Servico servico, DateTime dataAgendada, TimeSpan horaAgendamento, string statusServico, double valorTotal, bool ativo)
        {   // Construtor com parâmetros
            this._idAgendamento = idAgendamento;
            this._cliente = cliente;
            this._servico = servico;
            this._dataAgendada = dataAgendada;
            this._horaAgendamento = horaAgendamento;
            this._statusServico = statusServico;
            this._valorTotal = valorTotal;
            this._ativo = ativo;
        }
    }
}
