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
    internal class C_Pais : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_pais;
        SqlDataAdapter da_pais;

        int teste;

        public List<Pais> DadosPais()
        {
            //Cria uma Lista do tipo Pais - Array
            List<Pais> lista_pais = new List<Pais>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_pais;
            conn.Open();

            try
            {
                dr_pais = cmd.ExecuteReader();
                while (dr_pais.Read())
                {
                    Pais aux = new Pais();
                    aux.codpais = Int32.Parse(dr_pais["codpais"].ToString());
                    aux.nomepais = dr_pais["nomepais"].ToString();
                    aux.bandeira = (byte[])dr_pais["bandeira"];

                    lista_pais.Add(aux);
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

            return lista_pais;
        }

        public List<Pais> DadosPaisFiltro(String parametro)
        {
            //Cria uma Lista do tipo Pais - Array
            List<Pais> lista_pais = new List<Pais>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnomepais", parametro + "%");

            SqlDataReader dr_pais;
            conn.Open();

            try
            {
                dr_pais = cmd.ExecuteReader();
                while (dr_pais.Read())
                {
                    Pais aux = new Pais();
                    aux.codpais = Int32.Parse(dr_pais["codpais"].ToString());
                    aux.nomepais = dr_pais["nomepais"].ToString();
                    aux.bandeira = (byte[])dr_pais["bandeira"];

                    lista_pais.Add(aux);
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

            return lista_pais;
        }

        String sqlApaga = "DELETE FROM pais WHERE codpais = @pcod";
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

        String sqlTodos = "SELECT * FROM pais";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_pais = new SqlDataAdapter(cmd);

            dt_pais = new DataTable();
            da_pais.Fill(dt_pais);

            return dt_pais;
        }

        String sqlFiltro = "SELECT * FROM pais WHERE nomepais LIKE @pnomepais";
        public DataTable Buscar_Filtro(String ppais)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomepais", ppais);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_pais = new SqlDataAdapter(cmd);

            dt_pais = new DataTable();
            da_pais.Fill(dt_pais);

            //Finaliza a Conexão
            conn.Close();
            return dt_pais;
        }

        String sqlInsere = "INSERT INTO pais(nomepais, bandeira) VALUES (@pnome, @pbandeira)";
        public void Insere_Dados(Object aux)
        {
            Pais pais = new Pais();
            pais = (Pais)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", pais.nomepais);
            cmd.Parameters.AddWithValue("@pbandeira", pais.bandeira);

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

        String sqlAtualiza = "UPDATE pais SET nomepais = @pnome, bandeira = @pbandeira WHERE codpais = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Pais dados = new Pais();
            dados = (Pais)aux;

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codpais);
            cmd.Parameters.AddWithValue("@pnome", dados.nomepais);
            cmd.Parameters.AddWithValue("@pbandeira", dados.bandeira);

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
