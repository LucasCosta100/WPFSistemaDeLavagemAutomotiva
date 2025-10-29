using MySqlConnector;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Database;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public class ClienteDAO
    {
        public void salvar(Cliente cliente)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "INSERT INTO clientes (id_cliente, nome, email, telefone) VALUES (@id, @nome, @email, @telefone)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@endereco", cliente.Telefone);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar cliente: " + ex.Message);
            }
        }

        public void atualizar(Cliente cliente)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE clientes SET nome = @nome, email = @email, telefone = @telefone WHERE id_cliente = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
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

        public void deletar(Cliente cliente)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "DELETE FROM clientes WHERE id_cliente = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
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

        public Cliente buscarPorCodigo(int idCliente)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM clientes WHERE id_cliente = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idCliente);

                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Cliente client = new Cliente
                            (
                                reader.GetInt32(reader.GetOrdinal("id_cliente")),
                                reader.GetString(reader.GetOrdinal("nome")),
                                reader.GetString(reader.GetOrdinal("email")),
                                reader.GetString(reader.GetOrdinal("telefone"))
                            );
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

        public List<Cliente> buscarTodos()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM clientes";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente client = new Cliente
                            (
                                reader.GetInt32("id_cliente"),
                                reader.GetString("nome"),
                                reader.GetString("email"),
                                reader.GetString("telefone")
                            );
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
