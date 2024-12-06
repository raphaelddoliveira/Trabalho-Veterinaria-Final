using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_TipoFuncionario : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_tipoFuncionario;
        SqlDataAdapter da_tipoFuncionario;

        public List<Tipofuncionario> DadosTipoFuncionario()
        {
            //Cria uma Lista do tipo TipoFuncionario - Array
            List<Tipofuncionario> lista_tipoFuncionario = new List<Tipofuncionario>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tipoFuncionario;
            conn.Open();

            try
            {
                dr_tipoFuncionario = cmd.ExecuteReader();
                while (dr_tipoFuncionario.Read())
                {
                    Tipofuncionario aux = new Tipofuncionario();
                    aux.codtipofuncionario = Int32.Parse(dr_tipoFuncionario["codtipofuncionario"].ToString());
                    aux.nometipofuncionario = dr_tipoFuncionario["nometipofuncionario"].ToString();

                    lista_tipoFuncionario.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_tipoFuncionario;
        }

        public List<Tipofuncionario> DadosTipoFuncionarioFiltro(String parametro)
        {
            //Cria uma Lista do tipo TipoFuncionario - Array
            List<Tipofuncionario> lista_tipoFuncionario = new List<Tipofuncionario>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnometipofuncionario", parametro + "%");

            SqlDataReader dr_tipoFuncionario;
            conn.Open();

            try
            {
                dr_tipoFuncionario = cmd.ExecuteReader();
                while (dr_tipoFuncionario.Read())
                {
                    Tipofuncionario aux = new Tipofuncionario();
                    aux.codtipofuncionario = Int32.Parse(dr_tipoFuncionario["codtipofuncionario"].ToString());
                    aux.nometipofuncionario = dr_tipoFuncionario["nometipofuncionario"].ToString();

                    lista_tipoFuncionario.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_tipoFuncionario;
        }

        String sqlApaga = "DELETE FROM tipofuncionario WHERE codtipofuncionario = @pcod";
        public void Apaga_Dados(int aux)
        {
            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcod", aux);

            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Apaguei");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public object Buscar_Id(int valor)
        {
            throw new NotImplementedException();
        }

        String sqlTodos = "SELECT * FROM tipofuncionario";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoFuncionario = new SqlDataAdapter(cmd);

            dt_tipoFuncionario = new DataTable();
            da_tipoFuncionario.Fill(dt_tipoFuncionario);

            return dt_tipoFuncionario;
        }

        String sqlFiltro = "SELECT * FROM tipofuncionario WHERE nometipofuncionario LIKE @pnometipofuncionario";
        public DataTable Buscar_Filtro(String ptipoFuncionario)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnometipofuncionario", ptipoFuncionario);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoFuncionario = new SqlDataAdapter(cmd);

            dt_tipoFuncionario = new DataTable();
            da_tipoFuncionario.Fill(dt_tipoFuncionario);

            //Finaliza a Conexão
            conn.Close();
            return dt_tipoFuncionario;
        }

        String sqlInsere = "INSERT INTO tipofuncionario(nometipofuncionario) VALUES (@pnometipofuncionario)";
        public void Insere_Dados(Object aux)
        {
            Tipofuncionario tipoFuncionario = new Tipofuncionario();
            tipoFuncionario = (Tipofuncionario)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnometipofuncionario", tipoFuncionario.nometipofuncionario);

            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Inseriu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        String sqlAtualiza = "UPDATE tipofuncionario SET nometipofuncionario = @pnometipofuncionario WHERE codtipofuncionario = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Tipofuncionario dados = new Tipofuncionario();
            dados = (Tipofuncionario)aux;

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtipofuncionario);
            cmd.Parameters.AddWithValue("@pnometipofuncionario", dados.nometipofuncionario);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Atualizei");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
