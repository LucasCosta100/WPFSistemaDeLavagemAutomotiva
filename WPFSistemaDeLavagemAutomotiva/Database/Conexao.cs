using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPFSistemaDeLavagemAutomotiva.Database
{
    public class Conexao //Classe para gerenciar a conexão com o banco de dados MySQL
    {
        private static string servidor = "localhost";
        private static string banco = "db_lavagem";
        private static string usuario = "root";
        private static string senha = "agi9]d8y6G&`";
        private static string stringDeConexao = $"server={servidor};user id = {usuario}; password={senha};database={banco};";

        public static MySqlConnection ObterConexao()
        {
            return new MySqlConnection(stringDeConexao);
        }
    }
}
