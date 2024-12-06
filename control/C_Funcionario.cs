using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Funcionario : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_Funcionario;
        SqlDataAdapter da_Funcionario;

        private const string sqlTodos = "SELECT * FROM funcionario";
        private const string sqlFiltro = "SELECT * FROM funcionario WHERE nomefuncionario LIKE @pnomefuncionario";
        private const string sqlInsere = "INSERT INTO funcionario(nomefuncionario, codtipofuncionariofk, codlojafk) VALUES (@pnomefuncionario, @pnometipofuncionario, @pnomeloja)";
        private const string sqlAtualiza = "UPDATE funcionario SET nomefuncionario = @pnomefuncionario, codtipofuncionarioFK = @pnometipofuncionario, codlojafk = @pnomeloja WHERE codfuncionario = @pcodfuncionario";
        private const string sqlApaga = "DELETE FROM funcionario WHERE codfuncionario = @pcodfuncionario";
        private const string sqlBuscar = "SELECT * FROM funcionario WHERE codfuncionario = @pdcodfuncionario";
        private const string sqlTodosDataTable = "SELECT f.codfuncionario, f.nomefuncionario, t.nometipofuncionario, l.nomeloja FROM funcionario  f, tipoFuncionario t, loja l where f.codtipofuncionariofk = t.codtipofuncionario and f.codlojafk = l.codloja";
        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodfuncionario", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Funcionário excluído com sucesso.");
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
            Funcionario funcionario = aux as Funcionario;
            if (funcionario == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Funcionario.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pnomefuncionario", funcionario.nomefuncionario);
            cmd.Parameters.AddWithValue("@pnometipofuncionario", funcionario.tipofuncionario.codtipofuncionario);
            cmd.Parameters.AddWithValue("@pnomeloja", funcionario.loja.codloja);
            cmd.Parameters.AddWithValue("@pcodfuncionario", funcionario.codfuncionario);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Funcionário atualizado com sucesso.");
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
            cmd.Parameters.AddWithValue("@pnomefuncionario", dados +"%");

            conn.Open();

            da_Funcionario = new SqlDataAdapter(cmd);
            dt_Funcionario = new DataTable();
            da_Funcionario.Fill(dt_Funcionario);

            conn.Close();
            return dt_Funcionario;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pdcodfuncionario", valor);

            conn.Open();

            SqlDataReader dr_funcionario;
            Funcionario funcionario = new Funcionario();
            try
            {
                dr_funcionario = cmd.ExecuteReader();
                if (dr_funcionario.Read())
                {
                    Tipofuncionario tipoFuncionario = new Tipofuncionario()
                    {
                        codtipofuncionario = Int32.Parse(dr_funcionario["nometipofuncionario"].ToString())
                    };
                    Loja loja = new Loja()
                    {
                        codloja = Int32.Parse(dr_funcionario["nomeloja"].ToString())
                    };

                    funcionario = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_funcionario["codfuncionario"].ToString()),
                        nomefuncionario = dr_funcionario["nomefuncionario"].ToString(),
                        tipofuncionario = tipoFuncionario,
                        loja = loja
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

            return funcionario;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodosDataTable, conn);

            conn.Open();

            da_Funcionario = new SqlDataAdapter(cmd);
            dt_Funcionario = new DataTable();
            da_Funcionario.Fill(dt_Funcionario);

            conn.Close();
            return dt_Funcionario;
        }

        public void Insere_Dados(object aux)
        {
            Funcionario funcionario = aux as Funcionario;
            if (funcionario == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Funcionario.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnomefuncionario", funcionario.nomefuncionario);
            cmd.Parameters.AddWithValue("@pnometipofuncionario", funcionario.tipofuncionario.codtipofuncionario);
            cmd.Parameters.AddWithValue("@pnomeloja", funcionario.loja.codloja);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Funcionário inserido com sucesso.");
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

        public List<Funcionario> DadosFuncionario()
        {
            List<Funcionario> lista_Funcionario = new List<Funcionario>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Funcionario;
            conn.Open();

            try
            {
                dr_Funcionario = cmd.ExecuteReader();
                while (dr_Funcionario.Read())
                {
                    Tipofuncionario  tipoFuncionario = new Tipofuncionario()
                    {
                        codtipofuncionario = Int32.Parse(dr_Funcionario["codtipofuncionariofk"].ToString())
                    };
                    Loja loja = new Loja()
                    {
                        codloja = Int32.Parse(dr_Funcionario["codlojafk"].ToString())
                    };

                    Funcionario aux = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_Funcionario["codfuncionario"].ToString()),
                        nomefuncionario = dr_Funcionario["nomefuncionario"].ToString(),
                        tipofuncionario = tipoFuncionario,
                        loja = loja
                    };

                    lista_Funcionario.Add(aux);
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

            return lista_Funcionario;
        }

        public List<Funcionario> DadosFuncionarioFiltro(string filtro)
        {
            List<Funcionario> lista_Funcionario = new List<Funcionario>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pnomefuncionario", filtro + "%");

            SqlDataReader dr_Funcionario;
            conn.Open();

            try
            {
                dr_Funcionario = cmd.ExecuteReader();
                while (dr_Funcionario.Read())
                {
                    Tipofuncionario tipoFuncionario = new Tipofuncionario()
                    {
                        codtipofuncionario = Int32.Parse(dr_Funcionario["codtipofuncionariofk"].ToString())
                    };
                    Loja loja = new Loja()
                    {
                        codloja = Int32.Parse(dr_Funcionario["codlojafk"].ToString())
                    };

                    Funcionario aux = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_Funcionario["codfuncionario"].ToString()),
                        nomefuncionario = dr_Funcionario["nomefuncionario"].ToString(),
                        tipofuncionario = tipoFuncionario,
                        loja = loja
                    };

                    lista_Funcionario.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_Funcionario;
        }
    }
}
