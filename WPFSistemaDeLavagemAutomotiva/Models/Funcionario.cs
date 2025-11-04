using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSistemaDeLavagemAutomotiva.Models
{
    public class Funcionario
    {   //Atributos e propriedades
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

        private bool _ativo;
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        private Endereco _endereco;
        public Endereco Endereco
        {
            get { return _endereco; }
            set { _endereco = value; }
        }

        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        private string _senhaHash;
        public string SenhaHash
        {
            get { return _senhaHash; }
            set { _senhaHash = value; }
        }

        public Funcionario() //Construtor Padrão
        {
        }

        public Funcionario(int idFuncionario, string nome, string cargo, bool ativo, Endereco endereco, string usuario, string senhaHash)
        {   //Construtor com parâmetros
            this._idFuncionario = idFuncionario;
            this._nome = nome;
            this._cargo = cargo;
            this._ativo = ativo;
            this._endereco = endereco;
            this._usuario = usuario;
            this._senhaHash = senhaHash;
        }
    }
}
