using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSistemaDeLavagemAutomotiva.Models
{
    public class Endereco
    {
        private int _idEndereco;
        public int IdEndereco
        {
            get { return _idEndereco; }
            set { _idEndereco = value; }
        }

        private string _rua;
        public string Rua
        {
            get { return _rua; }
            set { _rua = value; }
        }

        private string _numero;
        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        private string _complemento;
        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }

        private string _bairro;
        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }

        private string _cidade;
        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }

        private string _estado; //2 letras
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private string _cep;
        public string Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }

        public Endereco()
        {

        }

        public Endereco(int idEndereco, string rua, string numero, string complemento, string bairro, string cidade, string estado, string cep)
        {
            this._idEndereco = idEndereco;
            this._rua = rua;
            this._numero = numero;
            this._complemento = complemento;
            this._bairro = bairro;
            this._cidade = cidade;
            this._estado = estado;
            this._cep = cep;
        }
    }
}
