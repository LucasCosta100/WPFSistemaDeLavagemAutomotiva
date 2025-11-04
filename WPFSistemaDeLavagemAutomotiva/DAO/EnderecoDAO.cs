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
    public class EnderecoDAO : IEnderecoDAO //Implementação da interface IEnderecoDAO
    {
        public int Salvar(Endereco endereco) // Método para salvar endereço no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = @"
                INSERT INTO enderecos (rua, numero, complemento, bairro, cidade, estado, cep) 
                VALUES (@rua, @numero, @complemento, @bairro, @cidade, @estado, @cep);
                SELECT LAST_INSERT_ID();"; // retorna o id gerado

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@rua", endereco.Rua);
                    cmd.Parameters.AddWithValue("@numero", endereco.Numero);
                    cmd.Parameters.AddWithValue("@complemento", endereco.Complemento);
                    cmd.Parameters.AddWithValue("@bairro", endereco.Bairro);
                    cmd.Parameters.AddWithValue("@cidade", endereco.Cidade);
                    cmd.Parameters.AddWithValue("@estado", endereco.Estado);
                    cmd.Parameters.AddWithValue("@cep", endereco.Cep);

                    conn.Open();
                    int idGerado = Convert.ToInt32(cmd.ExecuteScalar()); // retorna o id do endereço
                    return idGerado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar o endereço");
            }
        }

        public void Atualizar(Endereco endereco)//Método para atualizar endereco no banco de dados
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

        public Endereco BuscarPorCodigo(int idEndereco)//Método para buscar endereco pelo ID no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM enderecos WHERE id_endereco = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idEndereco);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())//Lê os dados do banco de dados
                    {
                        if (reader.Read())//Se encontrar o IdEndereco retornar os dados
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
                                Cep = reader.GetString(reader.GetOrdinal("cep"))
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

        public List<Endereco> BuscarTodos()//Método para buscar todos os enderecos no banco de dados
        {
            List<Endereco> enderecos = new List<Endereco>();//Lista para armazenar os enderecos
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM enderecos WHERE ativo = true";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())//Lê os dados do banco de dados
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
                                Cep = reader.GetString(reader.GetOrdinal("cep"))
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
