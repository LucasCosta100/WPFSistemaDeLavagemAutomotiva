using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSistemaDeLavagemAutomotiva.Models
{
    public class Agendamento
    {
        private int _idAgendamento;
        public int IdAgendamento { 
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

        private DateTime _horaAgendamento;
        public DateTime HoraAgendamento
        {
            get { return _horaAgendamento; }
            set { _horaAgendamento = value; }
        }

        public Agendamento(int idAgendamento, Cliente cliente, Servico servico, DateTime dataAgendada, DateTime horaAgendamento)
        {
            this._idAgendamento = idAgendamento;
            this._cliente = cliente;
            this._servico = servico;
            this._dataAgendada = dataAgendada;
            this._horaAgendamento = horaAgendamento;
        }

        public Agendamento()
        {

        }
    }
}
