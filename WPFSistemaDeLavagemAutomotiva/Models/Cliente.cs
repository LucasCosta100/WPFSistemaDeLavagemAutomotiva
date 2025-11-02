using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace WPFSistemaDeLavagemAutomotiva.Models
{
    public class Cliente
    {   //Atributos e Propriedades
        private int _idCliente;
        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _telefone;
        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

        private bool _ativo;
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        private Endereco _endereco;
        public Endereco EnderecoCliente
        {
            get { return _endereco; }
            set { _endereco = value; }
        }


        public Cliente() //Construtor Padrão
        {

        }
        
        public Cliente(int idCliente, string nome, string email, string telefone, bool ativo, Endereco endereco)
        {   //Construtor com Parâmetros
            this._idCliente = idCliente;
            this._nome = nome;
            this._email = email;
            this._telefone = telefone;
            this._ativo = ativo;
            this._endereco = endereco;
        }
    }
}
