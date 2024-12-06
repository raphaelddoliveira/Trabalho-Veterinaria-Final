using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;
using Veterinaria.view;

namespace Veterinaria.control
{
    internal class C_FuncionarioTelefone : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_FuncionarioTelefone;
        SqlDataAdapter da_FuncionarioTelefone;

        private const string sqlTodos = "SELECT * FROM funcionariotelefone";
        private const string sqlFiltro = "SELECT * FROM funcionariotelefone WHERE codfuncionariofk = @pcodfuncionario";
        private const string sqlInsere = "INSERT INTO funcionariotelefone(codtelefonefk, codfuncionariofk) VALUES (@pcodtelefonefk, @pcodfuncionariofk)";
        private const string sqlAtualiza = "UPDATE funcionariotelefone SET codtelefonefk = @pcodtelefonefk, codfuncionariofk = @pcodfuncionario WHERE codfuncionariofk = @pcodfuncionariofk";
        private const string sqlApaga = "DELETE FROM funcionariotelefone WHERE codfuncionariofk = @pcodfuncionariofk";
        private const string sqlBuscar = "SELECT * FROM funcionariotelefone WHERE codfuncionariofk = @pcodfuncionario";

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodfuncionariofk", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Telefone do funcionário excluído com sucesso.");
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
            Funcionariotelefone funcionarioTelefone = aux as Funcionariotelefone;
            if (funcionarioTelefone == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo FuncionarioTelefone.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", funcionarioTelefone.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@pcodfuncionariofk", funcionarioTelefone.funcionario.codfuncionario);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Telefone do funcionário atualizado com sucesso.");
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
            cmd.Parameters.AddWithValue("@pcodfuncionario", dados);

            conn.Open();

            da_FuncionarioTelefone = new SqlDataAdapter(cmd);
            dt_FuncionarioTelefone = new DataTable();
            da_FuncionarioTelefone.Fill(dt_FuncionarioTelefone);

            conn.Close();
            return dt_FuncionarioTelefone;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", valor);
            cmd.Parameters.AddWithValue("@pcodfuncionariofk", valor);

            conn.Open();

            SqlDataReader dr_FuncionarioTelefone;
            Funcionariotelefone funcionarioTelefone = new Funcionariotelefone();
            try
            {
                dr_FuncionarioTelefone = cmd.ExecuteReader();
                if (dr_FuncionarioTelefone.Read())
                {
                    funcionarioTelefone = new Funcionariotelefone()
                    {
                        telefone = new Telefone()
                        {
                            codtelefone = Int32.Parse(dr_FuncionarioTelefone["codtelefonefk"].ToString())
                        },
                        funcionario = new Funcionario()
                        {
                            codfuncionario = Int32.Parse(dr_FuncionarioTelefone["codfuncionariofk"].ToString())
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

            return funcionarioTelefone;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();

            da_FuncionarioTelefone = new SqlDataAdapter(cmd);
            dt_FuncionarioTelefone = new DataTable();
            da_FuncionarioTelefone.Fill(dt_FuncionarioTelefone);

            conn.Close();
            return dt_FuncionarioTelefone;
        }

        public void Insere_Dados(object aux)
        {
            Funcionariotelefone funcionarioTelefone = aux as Funcionariotelefone;
            if (funcionarioTelefone == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo FuncionarioTelefone.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", funcionarioTelefone.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@pcodfuncionariofk", funcionarioTelefone.funcionario.codfuncionario);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Telefone do funcionário inserido com sucesso.");
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
        public List<Funcionariotelefone> DadosFuncionariotelefone()
        {
            List<Funcionariotelefone> lista_Funcionariotelefone = new List<Funcionariotelefone>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Funcionariotelefone;
            conn.Open();

            try
            {
                dr_Funcionariotelefone = cmd.ExecuteReader();
                while (dr_Funcionariotelefone.Read())
                {
                    Funcionario funcionario = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_Funcionariotelefone["codfuncionariofk"].ToString())
                    };
                    Telefone telefone = new Telefone()
                    {
                        codtelefone = Int32.Parse(dr_Funcionariotelefone["codtelefonefk"].ToString())
                    };

                    Funcionariotelefone aux = new Funcionariotelefone()
                    {
                        funcionario = funcionario,
                        telefone = telefone
                    };

                    lista_Funcionariotelefone.Add(aux);
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

            return lista_Funcionariotelefone;
        }
        public List<Funcionariotelefone> DadosFuncionariotelefoneFiltro(string filtro)
        {
            List<Funcionariotelefone> lista_Funcionariotelefone = new List<Funcionariotelefone>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pcodclientefk", filtro + "%");

            SqlDataReader dr_Funcionariotelefone;
            conn.Open();
            try
            {
                dr_Funcionariotelefone = cmd.ExecuteReader();
                while (dr_Funcionariotelefone.Read())
                {
                    Funcionario funcionario = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_Funcionariotelefone["codfuncionariofk"].ToString())
                    };
                    Telefone telefone = new Telefone()
                    {
                        codtelefone = Int32.Parse(dr_Funcionariotelefone["codtelefonefk"].ToString())
                    };

                    Funcionariotelefone aux = new Funcionariotelefone()
                    {
                        funcionario = funcionario,
                        telefone = telefone
                    };

                    lista_Funcionariotelefone.Add(aux);
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

            return lista_Funcionariotelefone;
        }
    }
}
