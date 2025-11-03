using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WPFSistemaDeLavagemAutomotiva.Database;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public class FuncionarioDAO : IFuncionarioDAO//Implementação da interface IFuncionarioDAO
    {
        public void Salvar(Funcionario funcionario)//Método para salvar funcionário no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())//Usa a conexão com o banco de dados
                {
                    string sql = "INSERT INTO funcionarios (id_funcionario, nome, cargo, ativo) VALUES (@id, @nome, @cargo, @ativo)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);//Comando SQL para inserir os dados do funcionário
                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                    cmd.Parameters.AddWithValue("@ativo", true);

                    conn.Open();
                    cmd.ExecuteNonQuery();//Executa o comando SQL
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar funcionário: " + ex.Message);//Tratamento de erro
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
                    
                    while (reader.Read())
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
    }
}
