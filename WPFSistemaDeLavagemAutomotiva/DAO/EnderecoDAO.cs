using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;
using MySqlConnector;
using WPFSistemaDeLavagemAutomotiva.Database;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public class EnderecoDAO
    {
        public void salvar(Endereco endereco)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "INSERT INTO enderecos (rua, numero, complemento, bairro, cidade, estado, cep) VALUES (@rua, @numero, @complemento, @bairro, @cidade, @estado, @cep)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@rua", endereco.Rua);
                    cmd.Parameters.AddWithValue("@numero", endereco.Numero);
                    cmd.Parameters.AddWithValue("@complemento", endereco.Complemento);
                    cmd.Parameters.AddWithValue("@bairro", endereco.Bairro);
                    cmd.Parameters.AddWithValue("@cidade", endereco.Cidade);
                    cmd.Parameters.AddWithValue("@estado", endereco.Estado);
                    cmd.Parameters.AddWithValue("@cep", endereco.Cep);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar endereco: " + ex.Message);
            }
        }

        public void atualizar(Endereco endereco)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE enderecos SET rua = @rua, numero = @numero, complemento = @complemento, bairro = @bairro, cidade = @cidade, estado = @estado, cep = @cep WHERE id_endereco = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@rua", endereco.Rua);
                    cmd.Parameters.AddWithValue("@numero", endereco.Numero);
                    cmd.Parameters.AddWithValue("@complemento", endereco.Complemento);
                    cmd.Parameters.AddWithValue("@bairro", endereco.Bairro);
                    cmd.Parameters.AddWithValue("@cidade", endereco.Cidade);
                    cmd.Parameters.AddWithValue("@estado", endereco.Estado);
                    cmd.Parameters.AddWithValue("@cep", endereco.Cep);
                    cmd.Parameters.AddWithValue("@id", endereco.IdEndereco);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar endereco: " + ex.Message);
            }
        }

        public void desativar(Endereco endereco)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE enderecos SET ativo = @ativo WHERE id_endereco = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ativo", false);
                    cmd.Parameters.AddWithValue("@id", endereco.IdEndereco);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao desativar endereco: " + ex.Message);
            }
        }

        public Endereco buscarPorCodigo(int idEndereco)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM enderecos WHERE id_endereco = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idEndereco);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Endereco endereco = new Endereco
                            {
                                IdEndereco = reader.GetInt32(reader.GetOrdinal("id_endereco")),
                                Rua = reader.GetString(reader.GetOrdinal("rua")),
                                Numero = reader.GetString(reader.GetOrdinal("numero")),
                                Complemento = reader.GetString(reader.GetOrdinal("complemento")),
                                Bairro = reader.GetString(reader.GetOrdinal("bairro")),
                                Cidade = reader.GetString(reader.GetOrdinal("cidade")),
                                Estado = reader.GetString(reader.GetOrdinal("estado")),
                                Cep = reader.GetInt32(reader.GetOrdinal("cep"))
                            };
                            return endereco;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar endereco por codigo: " + ex.Message);
            }
        }

        public List<Endereco> listarTodos()
        {
            List<Endereco> enderecos = new List<Endereco>();
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM enderecos WHERE ativo = true";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Endereco endereco = new Endereco
                            {
                                IdEndereco = reader.GetInt32(reader.GetOrdinal("id_endereco")),
                                Rua = reader.GetString(reader.GetOrdinal("rua")),
                                Numero = reader.GetString(reader.GetOrdinal("numero")),
                                Complemento = reader.GetString(reader.GetOrdinal("complemento")),
                                Bairro = reader.GetString(reader.GetOrdinal("bairro")),
                                Cidade = reader.GetString(reader.GetOrdinal("cidade")),
                                Estado = reader.GetString(reader.GetOrdinal("estado")),
                                Cep = reader.GetInt32(reader.GetOrdinal("cep"))
                            };
                            enderecos.Add(endereco);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar enderecos: " + ex.Message);
            }
            return enderecos;
        }
    }
}
