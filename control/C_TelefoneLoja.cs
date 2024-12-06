using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;
using Veterinaria.view;

namespace Veterinaria.control
{
    internal class C_TelefoneLoja : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_TelefoneLoja;
        SqlDataAdapter da_TelefoneLoja;

        private const string sqlTodos = "SELECT * FROM telefoneloja";
        private const string sqlFiltro = "SELECT * FROM telefoneloja WHERE codlojafk = @pcodloja";
        private const string sqlInsere = "INSERT INTO telefoneloja(codtelefonefk, codlojafk) VALUES (@pcodtelefonefk, @pcodlojafk)";
        private const string sqlAtualiza = "UPDATE telefoneloja SET codtelefonefk = @pcodtelefonefk, codlojafk = @pcodlojafk WHERE codtelefonefk = @pcodtelefonefk AND codlojafk = @pcodlojafk";
        private const string sqlApaga = "DELETE FROM telefoneloja WHERE codtelefonefk = @pcodtelefonefk AND codlojafk = @pcodlojafk";
        private const string sqlBuscar = "SELECT * FROM telefoneloja WHERE codtelefonefk = @pcodtelefonefk AND codlojafk = @pcodlojafk";

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", aux); 
            cmd.Parameters.AddWithValue("@pcodlojafk", aux);    

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Telefone da loja excluído com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Atualizar_Dados(object aux)
        {
            Telefoneloja telefoneLoja = aux as Telefoneloja;
            if (telefoneLoja == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo TelefoneLoja.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", telefoneLoja.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@pcodlojafk", telefoneLoja.loja.codloja);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Telefone da loja atualizado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable Buscar_Filtro(string dados)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pcodlojafk", dados);

            conn.Open();

            da_TelefoneLoja = new SqlDataAdapter(cmd);
            dt_TelefoneLoja = new DataTable();
            da_TelefoneLoja.Fill(dt_TelefoneLoja);

            conn.Close();
            return dt_TelefoneLoja;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", valor);
            cmd.Parameters.AddWithValue("@pcodlojafk", valor);

            conn.Open();

            SqlDataReader dr_TelefoneLoja;
            Telefoneloja telefoneLoja = new Telefoneloja();
            try
            {
                dr_TelefoneLoja = cmd.ExecuteReader();
                if (dr_TelefoneLoja.Read())
                {
                    telefoneLoja = new Telefoneloja()
                    {
                        telefone = new Telefone()
                        {
                            codtelefone = Int32.Parse(dr_TelefoneLoja["codtelefonefk"].ToString())
                        },
                        loja = new Loja()
                        {
                            codloja = Int32.Parse(dr_TelefoneLoja["codlojafk"].ToString())
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return telefoneLoja;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();

            da_TelefoneLoja = new SqlDataAdapter(cmd);
            dt_TelefoneLoja = new DataTable();
            da_TelefoneLoja.Fill(dt_TelefoneLoja);

            conn.Close();
            return dt_TelefoneLoja;
        }

        public void Insere_Dados(object aux)
        {
            Telefoneloja telefoneLoja = aux as Telefoneloja;
            if (telefoneLoja == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo TelefoneLoja.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", telefoneLoja.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@pcodlojafk", telefoneLoja.loja.codloja);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Telefone da loja inserido com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Telefoneloja> DadosTelefoneloja()
        {
            List<Telefoneloja> lista_Telefoneloja = new List<Telefoneloja>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Telefoneloja;
            conn.Open();

            try
            {
                dr_Telefoneloja = cmd.ExecuteReader();
                while (dr_Telefoneloja.Read())
                {
                    Loja loja = new Loja()
                    {
                        codloja = Int32.Parse(dr_Telefoneloja["codlojafk"].ToString())
                    };
                    Telefone telefone = new Telefone()
                    {
                        codtelefone = Int32.Parse(dr_Telefoneloja["codtelefonefk"].ToString())
                    };

                    Telefoneloja aux = new Telefoneloja()
                    {
                        loja = loja,
                        telefone = telefone
                    };

                    lista_Telefoneloja.Add(aux);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "List");
            }
            finally
            {
                conn.Close();
            }

            return lista_Telefoneloja;
        }
        public List<Telefoneloja> DadosTelefonelojaFiltro(string filtro)
        {
            List<Telefoneloja> lista_Telefoneloja = new List<Telefoneloja>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pcodlojafk", filtro + "%");

            SqlDataReader dr_Telefoneloja;
            conn.Open();
            try
            {
                dr_Telefoneloja = cmd.ExecuteReader();
                while (dr_Telefoneloja.Read())
                {
                    Loja loja = new Loja()
                    {
                        codloja = Int32.Parse(dr_Telefoneloja["codlojafk"].ToString())
                    };
                    Telefone telefone = new Telefone()
                    {
                        codtelefone = Int32.Parse(dr_Telefoneloja["codtelefonefk"].ToString())
                    };

                    Telefoneloja aux = new Telefoneloja()
                    {
                        loja = loja,
                        telefone = telefone
                    };

                    lista_Telefoneloja.Add(aux);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "List");
            }
            finally
            {
                conn.Close();
            }

            return lista_Telefoneloja;
        }
    }
}
