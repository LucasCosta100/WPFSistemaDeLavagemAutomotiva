using MySqlConnector;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFSistemaDeLavagemAutomotiva.Database;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public class ClienteDAO : IClienteDAO
    {
        public void Salvar(Cliente cliente)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "INSERT INTO clientes (id_cliente, nome, email, telefone, ativo, id_endereco) VALUES (@id, @nome, @email, @telefone, @ativo, @endereco)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    cmd.Parameters.AddWithValue("@ativo", true);
                    cmd.Parameters.AddWithValue("@endereco", cliente.EnderecoCliente.IdEndereco);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar cliente: " + ex.Message);
            }
        }

        public void Atualizar(Cliente cliente)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE clientes SET nome = @nome, email = @email, telefone = @telefone, ativo = @ativo, id_endereco = @id_endereco WHERE id_cliente = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    cmd.Parameters.AddWithValue("@ativo", cliente.Ativo);
                    cmd.Parameters.AddWithValue("@id_endereco", cliente.EnderecoCliente.IdEndereco);
                    cmd.Parameters.AddWithValue("@id", cliente.IdCliente);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar cliente: " + ex.Message);
            }
        }

        public void Desativar(Cliente cliente)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE clientes SET ativo = @ativo WHERE id_cliente = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ativo", false);
                    cmd.Parameters.AddWithValue("@id", cliente.IdCliente);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar cliente: " + ex.Message);
            }
        }

        public Cliente BuscarPorCodigo(int idCliente)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    EnderecoDAO enderecoDAO = new EnderecoDAO();
                    string sql = "SELECT * FROM clientes WHERE id_cliente = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idCliente);

                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Pega o Index e verifica se é nulo antes de buscar o endereço
                            int idxEndereco = reader.GetOrdinal("id_endereco");
                            Endereco endereco = reader.IsDBNull(idxEndereco)
                                ? null
                                : enderecoDAO.BuscarPorCodigo(reader.GetInt32(idxEndereco));
                            Cliente client = new Cliente()
                            {
                                IdCliente = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Telefone = reader.GetString(reader.GetOrdinal("telefone")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo")),
                                EnderecoCliente = endereco 
                            };
                            return client;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar cliente por código: " + ex.Message);
            }
            return null;
        }

        public List<Cliente> BuscarTodos()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    EnderecoDAO enderecoDAO = new EnderecoDAO();
                    string sql = "SELECT * FROM clientes";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idxEndereco = reader.GetOrdinal("id_endereco");
                            Endereco endereco = reader.IsDBNull(idxEndereco) ? null
                                : enderecoDAO.BuscarPorCodigo(reader.GetInt32(idxEndereco));

                            Cliente client = new Cliente()
                            {
                                IdCliente = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Telefone = reader.GetString(reader.GetOrdinal("telefone")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo")),
                                EnderecoCliente = endereco
                            };
                            clientes.Add(client);
                        }
                        return clientes;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar todos os clientes: " + ex.Message);
            }
        }
    }
}
