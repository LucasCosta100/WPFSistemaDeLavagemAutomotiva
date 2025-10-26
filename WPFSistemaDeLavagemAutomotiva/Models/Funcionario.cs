using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSistemaDeLavagemAutomotiva.Models
{
    public class Funcionario
    {
        private int _idFuncionario;
        public int IdFuncionario
        {
            get { return _idFuncionario; }
            set { _idFuncionario = value; }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _cargo;
        public string Cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }

        public Funcionario()
        {
        }

        public Funcionario(int idFuncionario, string nome, string cargo)
        {
            this._idFuncionario = idFuncionario;
            this._nome = nome;
            this._cargo = cargo;
        }
    }
}
