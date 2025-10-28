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
    public class AgendamentoDAO : IAgendamentoDAO
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
    }
}
