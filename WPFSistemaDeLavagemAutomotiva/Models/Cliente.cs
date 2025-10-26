using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace WPFSistemaDeLavagemAutomotiva.Models
{
    public class Cliente
    {
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

        public Cliente()
        {

        }
        
        public Cliente(int idCliente, string nome, string email, string telefone)
        {
            this._idCliente = idCliente;
            this._nome = nome;
            this._email = email;
            this._telefone = telefone;
        }
    }
}
