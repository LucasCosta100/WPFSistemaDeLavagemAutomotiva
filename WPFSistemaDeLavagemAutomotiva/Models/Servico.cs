using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSistemaDeLavagemAutomotiva.Models
{
    public class Servico
    {
        private int _idServico;
        public int IdServico
        {
            get { return _idServico; }
            set { _idServico = value; }
        }
        private string _servico;
        public string NomeServico
        {
            get { return _servico; }
            set { _servico = value; }
        }
        private decimal _valor;
        public decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        private bool _ativo;
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public Servico(){ }

        public Servico(int idServico, string servico, decimal valor, bool ativo)
        {
            this._idServico = idServico;
            this._servico = servico;
            this._valor = valor;
            this._ativo = ativo;
        }
    }
}
