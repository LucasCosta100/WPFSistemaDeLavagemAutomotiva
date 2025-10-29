using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Database;
using WPFSistemaDeLavagemAutomotiva.Models;
using MySqlConnector;
using WPFSistemaDeLavagemAutomotiva.DAO;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public class ServicoDAO : IServicoDAO
    {
        public void salvar(Servico servico)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "INSERT INTO servicos (id_servico, servico, valor, ativo) VALUES (@id, @servico, @valor, @ativo)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", servico.NomeServico);
                    cmd.Parameters.AddWithValue("@descricao", servico.Valor);
                    cmd.Parameters.AddWithValue("@ativo", true);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar serviço: " + ex.Message);
            }
        }

        public void atualizar(Servico servico)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE servicos SET servico = @servico, valor = @valor, ativo = @ativo WHERE id_servico = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@servico", servico.NomeServico);
                    cmd.Parameters.AddWithValue("@valor", servico.Valor);
                    cmd.Parameters.AddWithValue("@id", servico.IdServico);
                    cmd.Parameters.AddWithValue("@ativo", true);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar serviço: " + ex.Message);
            }
        }

        public void desativar(Servico servico)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE servicos SET ativo = @ativo WHERE id_servico = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ativo", false);
                    cmd.Parameters.AddWithValue("@id", servico.IdServico);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar serviço: " + ex.Message);
            }
        }
        public Servico buscarPorCodigo(int idServico)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM servicos WHERE id_servico = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idServico);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Servico servico = new Servico()
                            {
                                IdServico = reader.GetInt32(reader.GetOrdinal("id_servico")),
                                NomeServico = reader.GetString(reader.GetOrdinal("servico")),
                                Valor = reader.GetDecimal(reader.GetOrdinal("valor")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo"))
                            };
                            return servico;
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
                throw new Exception("Erro ao buscar serviço por código: " + ex.Message);
            }
        }

        public List<Servico> buscarTodos()
        {
            List<Servico> servicos = new List<Servico>();
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM servicos";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Servico servico = new Servico()
                            {
                                IdServico = reader.GetInt32(reader.GetOrdinal("id_servico")),
                                NomeServico = reader.GetString(reader.GetOrdinal("servico")),
                                Valor = reader.GetDecimal(reader.GetOrdinal("valor")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo"))
                            };
                            servicos.Add(servico);
                        }
                        return servicos;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao selecionar tabela " + ex.Message);
            }
        }
    }
}
