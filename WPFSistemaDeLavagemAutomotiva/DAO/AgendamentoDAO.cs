using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFSistemaDeLavagemAutomotiva.Database;
using WPFSistemaDeLavagemAutomotiva.Models;
using MySqlConnector;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public class AgendamentoDAO 
    {
        public void salvar(Agendamento agendamento)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "INSERT INTO agendamentos (id_cliente, id_servico, data_agendamento, hora_agendamento, status_servico, valor_total) VALUES (@cliente, @servico, @data, @hora, @status, @valor)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@cliente", agendamento.ClienteAgendado.IdCliente);
                    cmd.Parameters.AddWithValue("@servico", agendamento.ServicoAgendado.IdServico);
                    cmd.Parameters.AddWithValue("@data", agendamento.DataAgendada);
                    cmd.Parameters.AddWithValue("@hora", agendamento.HoraAgendamento);
                    cmd.Parameters.AddWithValue("@status", "Pendente");
                    cmd.Parameters.AddWithValue("@valor", agendamento.ServicoAgendado.Valor);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar agendamento: " + ex.Message);
            }
        }

        public void atualizar (Agendamento agendamento)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE agendamentos SET id_cliente = @cliente, id_servico = @servico, data_agendamento = @data, hora_agendamento = @hora, status_servico = @status, valor_total = @valor WHERE id_agendamento = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@cliente", agendamento.ClienteAgendado.IdCliente);
                    cmd.Parameters.AddWithValue("@servico", agendamento.ServicoAgendado.IdServico);
                    cmd.Parameters.AddWithValue("@data", agendamento.DataAgendada);
                    cmd.Parameters.AddWithValue("@hora", agendamento.HoraAgendamento);
                    cmd.Parameters.AddWithValue("@status", agendamento.StatusServico);
                    cmd.Parameters.AddWithValue("@valor", agendamento.ValorTotal);
                    cmd.Parameters.AddWithValue("@id", agendamento.IdAgendamento);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar agendamento: " + ex.Message);
            }
        }

        public void Deletar(Agendamento agendamento)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "DELETE FROM agendamentos WHERE id_agendamento = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", agendamento.IdAgendamento);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar agendamento: " + ex.Message);
            }
        }

        public Agendamento buscarPorCodigo(int idAgendamento)
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM agendamentos WHERE id_agendamento = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idAgendamento);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Agendamento agendamento = new Agendamento()
                            {
                                IdAgendamento = reader.GetInt32(reader.GetOrdinal("id_agendamento")),
                                DataAgendada = reader.GetDateTime(reader.GetOrdinal("data_agendamento")),
                                HoraAgendamento = reader.GetTimeSpan(reader.GetOrdinal("hora_agendamento")),
                                StatusServico = reader.GetString(reader.GetOrdinal("status_servico")),
                                ValorTotal = reader.GetDouble(reader.GetOrdinal("valor_total"))
                            };
                            return agendamento;
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
                throw new Exception("Erro ao selecionar tabela " + ex.Message);
            }
        }

        public List<Agendamento> buscarTodos()
        {
            List<Agendamento> agendamentos = new List<Agendamento>();
            try
            {
                using(MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM agendamentos ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Agendamento agendamento = new Agendamento()
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar todos os agendamentos: " + ex.Message);
            }
    }
}
