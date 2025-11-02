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
    public class AgendamentoDAO : IAgendamentoDAO//Implementação da interface IAgendamentoDAO
    {   
        public void Salvar(Agendamento agendamento)//Método para salvar agendamento no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())//Usa a conexão com o banco de dados
                {
                    string sql = "INSERT INTO agendamentos (id_cliente, id_servico, data_agendamento, hora_agendamento, status_servico, valor_total, ativo) VALUES (@cliente, @servico, @data, @hora, @status, @valor, @ativo)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);//Comando SQL para inserir os dados do agendamento
                    cmd.Parameters.AddWithValue("@cliente", agendamento.ClienteAgendado.IdCliente);
                    cmd.Parameters.AddWithValue("@servico", agendamento.ServicoAgendado.IdServico);
                    cmd.Parameters.AddWithValue("@data", agendamento.DataAgendada);
                    cmd.Parameters.AddWithValue("@hora", agendamento.HoraAgendamento);
                    cmd.Parameters.AddWithValue("@status", "Pendente");
                    cmd.Parameters.AddWithValue("@valor", agendamento.ServicoAgendado.Valor);
                    cmd.Parameters.AddWithValue("@ativo", true);
                    

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agendamento salvo com sucesso!");//Mensagem de sucesso
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar agendamento: " + ex.Message);//Tratamento de erro
            }
        }

        public void Atualizar (Agendamento agendamento)//Método para atualizar agendamento no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE agendamentos SET id_cliente = @cliente, id_servico = @servico, data_agendamento = @data, hora_agendamento = @hora, status_servico = @status, valor_total = @valor, ativo = @ativo WHERE id_agendamento = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@cliente", agendamento.ClienteAgendado.IdCliente);
                    cmd.Parameters.AddWithValue("@servico", agendamento.ServicoAgendado.IdServico);
                    cmd.Parameters.AddWithValue("@data", agendamento.DataAgendada);
                    cmd.Parameters.AddWithValue("@hora", agendamento.HoraAgendamento);
                    cmd.Parameters.AddWithValue("@status", agendamento.StatusServico);
                    cmd.Parameters.AddWithValue("@valor", agendamento.ValorTotal);
                    cmd.Parameters.AddWithValue("@id", agendamento.IdAgendamento);
                    cmd.Parameters.AddWithValue("@ativo", true);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar agendamento: " + ex.Message);
            }
        }

        public void Desativar(Agendamento agendamento)//Método para deletar agendamento no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "UPDATE agendamentos SET ativo = @ativo WHERE id_agendamento = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ativo", false);
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

        public Agendamento BuscarPorCodigo(int idAgendamento)//Método para buscar agendamento pelo ID no banco de dados
        {
            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    ClienteDAO clienteDAO = new ClienteDAO();
                    ServicoDAO servicoDAO = new ServicoDAO();
                    string sql = "SELECT * FROM agendamentos WHERE id_agendamento = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idAgendamento);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())//Lê os dados do banco de dados
                    {
                        if (reader.Read())//Se encontrar o agendamento retornar os dados
                        {
                            Agendamento agendamento = new Agendamento()
                            {
                                IdAgendamento = reader.GetInt32(reader.GetOrdinal("id_agendamento")),
                                ClienteAgendado = clienteDAO.BuscarPorCodigo(reader.GetInt32(reader.GetOrdinal("id_cliente"))),
                                ServicoAgendado = servicoDAO.BuscarPorCodigo(reader.GetInt32(reader.GetOrdinal("id_servico"))),
                                DataAgendada = reader.GetDateTime(reader.GetOrdinal("data_agendamento")),
                                HoraAgendamento = reader.GetTimeSpan(reader.GetOrdinal("hora_agendamento")),
                                StatusServico = reader.GetString(reader.GetOrdinal("status_servico")),
                                ValorTotal = reader.GetDouble(reader.GetOrdinal("valor_total")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo"))
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

        public List<Agendamento> BuscarTodos()//Método para buscar todos os agendamentos no banco de dados
        {
            List<Agendamento> agendamentos = new List<Agendamento>();
            ClienteDAO clienteDAO = new ClienteDAO();
            ServicoDAO servicoDAO = new ServicoDAO();

            try
            {
                using (MySqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = "SELECT * FROM agendamentos ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())//Enquanto houver agendamentos, adicionar na lista
                        {
                            Agendamento agendamento = new Agendamento()
                            {
                                IdAgendamento = reader.GetInt32(reader.GetOrdinal("id_agendamento")),
                                ClienteAgendado = clienteDAO.BuscarPorCodigo(reader.GetInt32(reader.GetOrdinal("id_cliente"))),
                                ServicoAgendado = servicoDAO.BuscarPorCodigo(reader.GetInt32(reader.GetOrdinal("id_servico"))),
                                DataAgendada = reader.GetDateTime(reader.GetOrdinal("data_agendamento")),
                                HoraAgendamento = reader.GetTimeSpan(reader.GetOrdinal("hora_agendamento")),
                                StatusServico = reader.GetString(reader.GetOrdinal("status_servico")),
                                ValorTotal = reader.GetDouble(reader.GetOrdinal("valor_total")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo"))
                            };
                            agendamentos.Add(agendamento);
                        }
                        return agendamentos;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar todos os agendamentos: " + ex.Message);
            }
        }
    }
}
