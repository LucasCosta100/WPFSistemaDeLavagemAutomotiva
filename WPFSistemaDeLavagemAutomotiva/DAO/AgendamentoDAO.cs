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
                    MessageBox.Show("Agendamento salvo com sucesso!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar agendamento: " + ex.Message);
            }
        }
    }
}
