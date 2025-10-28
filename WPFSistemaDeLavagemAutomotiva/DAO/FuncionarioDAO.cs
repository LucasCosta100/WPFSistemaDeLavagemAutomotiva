using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSistemaDeLavagemAutomotiva.Models;

namespace WPFSistemaDeLavagemAutomotiva.DAO
{
    public class FuncionarioDAO
    {
        public void salvar(Funcionario funcionario)
        {
            try
            {
                using (MySqlConnector.MySqlConnection conn = Database.Conexao.ObterConexao())
                {
                    string sql = "INSERT INTO funcionarios (id_funcionario, nome, cargo) VALUES (@id, @nome, @cargo)";
                    MySqlConnector.MySqlCommand cmd = new MySqlConnector.MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar funcionário: " + ex.Message);
            }
        }

        public void atualizar(Funcionario funcionario)
        {
            try
            {
                using (MySqlConnector.MySqlConnection conn = Database.Conexao.ObterConexao())
                {
                    string sql = "UPDATE funcionarios SET nome = @nome, cargo = @cargo WHERE id_funcionario = @id";
                    MySqlConnector.MySqlCommand cmd = new MySqlConnector.MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                    cmd.Parameters.AddWithValue("@id", funcionario.IdFuncionario);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar funcionário: " + ex.Message);
            }
        }

        public void deletar(Funcionario funcionario)
        {
            try
            {
                using (MySqlConnector.MySqlConnection conn = Database.Conexao.ObterConexao())
                {
                    string sql = "DELETE FROM funcionarios WHERE id_funcionario = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", funcionario.IdFuncionario);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar funcionário: " + ex.Message);
            }
        }
    }
}
