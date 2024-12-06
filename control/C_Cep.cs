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
    internal class C_Cep : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_cep;
        SqlDataAdapter da_cep;

        public List<Cep> DadosCep()
        {
            //Cria uma Lista do tipo Cep - Array
            List<Cep> lista_cep = new List<Cep>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_cep;
            conn.Open();

            try
            {
                dr_cep = cmd.ExecuteReader();
                while (dr_cep.Read())
                {
                    Cep aux = new Cep();
                    aux.codcep = Int32.Parse(dr_cep["codcep"].ToString());
                    aux.numerocep = dr_cep["numerocep"].ToString();

                    lista_cep.Add(aux);
                }
            }
            catch (Exception ex)
            {
                // Tratar o erro aqui, se necessário
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_cep;
        }

        public List<Cep> DadosCepFiltro(String parametro)
        {
            //Cria uma Lista do tipo Cep - Array
            List<Cep> lista_cep = new List<Cep>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnumerocep", parametro + "%");

            SqlDataReader dr_cep;
            conn.Open();

            try
            {
                dr_cep = cmd.ExecuteReader();
                while (dr_cep.Read())
                {
                    Cep aux = new Cep();
                    aux.codcep = Int32.Parse(dr_cep["codcep"].ToString());
                    aux.numerocep = dr_cep["numerocep"].ToString();

                    lista_cep.Add(aux);
                }
            }
            catch (Exception ex)
            {
                // Tratar o erro aqui, se necessário
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista_cep;
        }

        String sqlApaga = "DELETE FROM cep WHERE codcep = @pcod";
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

        String sqlTodos = "SELECT * FROM cep";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_cep = new SqlDataAdapter(cmd);

            dt_cep = new DataTable();
            da_cep.Fill(dt_cep);

            return dt_cep;
        }

        String sqlFiltro = "SELECT * FROM cep WHERE numerocep LIKE @pnumerocep";
        public DataTable Buscar_Filtro(String pcep)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnumerocep", pcep);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_cep = new SqlDataAdapter(cmd);

            dt_cep = new DataTable();
            da_cep.Fill(dt_cep);

            //Finaliza a Conexão
            conn.Close();
            return dt_cep;
        }

        String sqlInsere = "INSERT INTO cep(numerocep) VALUES (@pnumerocep)";
        public void Insere_Dados(Object aux)
        {
            Cep cep = new Cep();
            cep = (Cep)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnumerocep", cep.numerocep);

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

        String sqlAtualiza = "UPDATE cep SET numerocep = @pnumerocep WHERE codcep = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Cep dados = new Cep();
            dados = (Cep)aux;

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codcep);
            cmd.Parameters.AddWithValue("@pnumerocep", dados.numerocep);

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
