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
    public class FuncionarioDAO : IFuncionarioDAO
    {
        public void Salvar(Funcionario funcionario)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "INSERT INTO funcionarios (id_funcionario, nome, cargo, ativo, id_endereco) VALUES (@id, @nome, @cargo, @ativo, @endereco)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                    cmd.Parameters.AddWithValue("@ativo", true);
                    cmd.Parameters.AddWithValue("@endereco", funcionario.Endereco.IdEndereco);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar funcionário: " + ex.Message);
            }
        }

        public void Atualizar(Funcionario funcionario)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE funcionarios SET nome = @nome, cargo = @cargo, ativo = @ativo, id_endereco = @endereco WHERE id_funcionario = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                    cmd.Parameters.AddWithValue("@id", funcionario.IdFuncionario);
                    cmd.Parameters.AddWithValue("@ativo", funcionario.Ativo);
                    cmd.Parameters.AddWithValue("@endereco", funcionario.Endereco.IdEndereco);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar funcionário: " + ex.Message);
            }
        }

        public void Desativar(Funcionario funcionario)
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

        public Funcionario BuscarPorCodigo(int idFuncionario)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    EnderecoDAO enderecoDAO = new EnderecoDAO();
                    string sql = "SELECT * FROM funcionario WHERE id_funcionario = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idFuncionario);

                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idxEndereco = reader.GetOrdinal("id_endereco");
                            Endereco endereco = reader.IsDBNull(idxEndereco) // Se for nulo, retorna null, se não, busca o endereço
                                ? null
                                : enderecoDAO.BuscarPorCodigo(reader.GetInt32(idxEndereco));
                            Funcionario func = new Funcionario()
                            {
                                IdFuncionario = reader.GetInt32("id_funcionario"),
                                Nome = reader.GetString("nome"),
                                Cargo = reader.GetString("cargo"),
                                Ativo = reader.GetBoolean("ativo"),
                                Endereco = endereco

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

        public List<Funcionario> BuscarTodos()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

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
                            Ativo = reader.GetBoolean("ativo"),
                            Endereco = endereco
                        };
                        funcionarios.Add(func);
                    }
                    return funcionarios;
                }
            }
        }
    }
}
