using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public class ServicoDAO
    {
        public void salvar(Servico servico)
        {
            try
            {
                using (MySqlConnector.MySqlConnection conn = Database.Conexao.ObterConexao())
                {
                    string sql = "INSERT INTO servicos (id_servico, servico, valor) VALUES (@id, @servico, @valor)";
                    MySqlConnector.MySqlCommand cmd = new MySqlConnector.MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", servico.NomeServico);
                    cmd.Parameters.AddWithValue("@descricao", servico.Valor);

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
                using (MySqlConnector.MySqlConnection conn = Database.Conexao.ObterConexao())
                {
                    string sql = "UPDATE servicos SET servico = @servico, valor = @valor WHERE id_servico = @id";
                    MySqlConnector.MySqlCommand cmd = new MySqlConnector.MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@servico", servico.NomeServico);
                    cmd.Parameters.AddWithValue("@valor", servico.Valor);
                    cmd.Parameters.AddWithValue("@id", servico.IdServico);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar serviço: " + ex.Message);
            }
        }

        public void deletar(Servico servico)
        {
            try
            {
                using (MySqlConnector.MySqlConnection conn = Database.Conexao.ObterConexao())
                {
                    string sql = "DELETE FROM servicos WHERE id_servico = @id";
                    MySqlConnector.MySqlCommand cmd = new MySqlConnector.MySqlCommand(sql, conn);
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
    }
}
