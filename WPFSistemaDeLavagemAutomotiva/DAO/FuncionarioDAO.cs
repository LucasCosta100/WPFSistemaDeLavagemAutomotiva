using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using WPFSistemaDeLavagemAutomotiva.Database;
using WPFSistemaDeLavagemAutomotiva.Models;
using WPFSistemaDeLavagemAutomotiva.Utils;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public class FuncionarioDAO : IFuncionarioDAO//Implementação da interface IFuncionarioDAO
    {
        private EnderecoDAO _enderecoDAO = new EnderecoDAO();
        public void Salvar(Funcionario funcionario)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    // Primeiro salva o endereço e pega o ID
                    EnderecoDAO enderecoDAO = new EnderecoDAO();
                    int idEndereco = enderecoDAO.Salvar(funcionario.Endereco);

                    // Query para salvar funcionário com id_endereco e senha_hash
                    string sql = @"
                INSERT INTO funcionarios (nome, cargo, ativo, usuario, senha_hash, id_endereco)
                VALUES (@nome, @cargo, @ativo, @usuario, @senha_hash, @id_endereco)";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                    cmd.Parameters.AddWithValue("@ativo", true);
                    cmd.Parameters.AddWithValue("@usuario", funcionario.Usuario);
                    cmd.Parameters.AddWithValue("@senha_hash", funcionario.SenhaHash); // a senha já deve vir hash
                    cmd.Parameters.AddWithValue("@id_endereco", idEndereco);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar funcionário: " + ex.Message);
            }
        }
        public void Atualizar(Funcionario funcionario)//Método para atualizar funcionário no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE funcionarios SET nome = @nome, cargo = @cargo, ativo = @ativo WHERE id_funcionario = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                    cmd.Parameters.AddWithValue("@id", funcionario.IdFuncionario);
                    cmd.Parameters.AddWithValue("@ativo", funcionario.Ativo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar funcionário: " + ex.Message);
            }
        }

        public void Desativar(Funcionario funcionario)//Método para deletar funcionário no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE funcionarios SET ativo = @ativo WHERE id_funcionario = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ativo", false);
                    cmd.Parameters.AddWithValue("@id", funcionario.IdFuncionario);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar funcionário: " + ex.Message);
            }
        }

        public Funcionario BuscarPorCodigo(int idFuncionario)//Método para buscar funcionário pelo ID no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM funcionario WHERE id_funcionario = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idFuncionario);

                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())//Lê os dados do banco de dados
                    {
                        if (reader.Read())//Se encontrar o funcionário retornar os dados
                        {
                            Funcionario func = new Funcionario()
                            {
                                IdFuncionario = reader.GetInt32("id_funcionario"),
                                Nome = reader.GetString("nome"),
                                Cargo = reader.GetString("cargo"),
                                Ativo = reader.GetBoolean("ativo")
                            };
                            return func;
                        }
                        else
                        {
                            return null; // Funcionário não encontrado
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar funcionário por código: " + ex.Message);
            }
        }

        public List<Funcionario> BuscarTodos()//Método para buscar todos os funcionários no banco de dados
        {
            List<Funcionario> funcionarios = new List<Funcionario>();//Lista para armazenar os funcionários

            using (MySqlConnection conn = Conexao.ObterConexao())
            {
                EnderecoDAO enderecoDAO = new EnderecoDAO();
                String sql = "SELECT * FROM funcionarios";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                { 
                    while (reader.Read())//Enquanto houver funcionários para ler
                    {
                        int idxEndereco = reader.GetOrdinal("id_endereco");
                        Endereco endereco = reader.IsDBNull(idxEndereco)
                            ? null
                            : enderecoDAO.BuscarPorCodigo(reader.GetInt32(idxEndereco));
                        Funcionario func = new Funcionario()
                        {
                            IdFuncionario = reader.GetInt32("id_funcionario"),
                            Nome = reader.GetString("nome"),
                            Cargo = reader.GetString("cargo"),
                            Endereco = endereco,
                            Ativo = reader.GetBoolean("ativo")
                        };
                        funcionarios.Add(func);
                    }
                    return funcionarios;
                }
            }
        }

        public Funcionario ObterUsuarioeSenha(string usuario, string senha)
        {
            Funcionario funcionario = null;
            string senhaHash = GerarHashSenhaUtils.GerarHash(senha);

            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    EnderecoDAO enderecoDAO = new EnderecoDAO();
                    string sql = "SELECT * FROM funcionarios WHERE usuario = @usuario AND senha_hash = @senha_hash";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@senha_hash", senhaHash);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // precisa chamar Read primeiro
                        {
                            int idxEndereco = reader.GetOrdinal("id_endereco");
                            Endereco endereco = reader.IsDBNull(idxEndereco)
                                ? null
                                : enderecoDAO.BuscarPorCodigo(reader.GetInt32(idxEndereco));

                            funcionario = new Funcionario
                            {
                                IdFuncionario = reader.GetInt32("id_funcionario"),
                                Nome = reader.GetString("nome"),
                                Cargo = reader.GetString("cargo"),
                                Usuario = reader.GetString("usuario"),
                                SenhaHash = reader.GetString("senha_hash"),
                                Endereco = endereco
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return funcionario;
        }
    }
}
