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
        private string _nomeServico;
        public string NomeServico
        {
            get { return _nomeServico; }
            set { _nomeServico = value; }
        }
        private decimal _precoServico;
        public decimal PrecoServico
        {
            get { return _precoServico; }
            set { _precoServico = value; }
        }

        public Servico(){ }

        public Servico(int idServico, string nomeServico, decimal precoServico)
        {
            this._idServico = idServico;
            this._nomeServico = nomeServico;
            this._precoServico = precoServico;
        }
    }
}
