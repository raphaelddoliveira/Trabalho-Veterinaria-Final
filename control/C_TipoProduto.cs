using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_TipoProduto : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_tipoProduto;
        SqlDataAdapter da_tipoProduto;

        public List<Tipoproduto> DadosTipoProduto()
        {
            //Cria uma Lista do tipo TipoProduto - Array
            List<Tipoproduto> lista_tipoProduto = new List<Tipoproduto>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tipoProduto;
            conn.Open();

            try
            {
                dr_tipoProduto = cmd.ExecuteReader();
                while (dr_tipoProduto.Read())
                {
                    Tipoproduto aux = new Tipoproduto();
                    aux.codtipoproduto = Int32.Parse(dr_tipoProduto["codtipoproduto"].ToString());
                    aux.nometipoproduto = dr_tipoProduto["nometipoproduto"].ToString();

                    lista_tipoProduto.Add(aux);
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

            return lista_tipoProduto;
        }

        public List<Tipoproduto> DadosTipoProdutoFiltro(String parametro)
        {
            //Cria uma Lista do tipo TipoProduto - Array
            List<Tipoproduto> lista_tipoProduto = new List<Tipoproduto>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnometipoproduto", parametro + "%");

            SqlDataReader dr_tipoProduto;
            conn.Open();

            try
            {
                dr_tipoProduto = cmd.ExecuteReader();
                while (dr_tipoProduto.Read())
                {
                    Tipoproduto aux = new Tipoproduto();
                    aux.codtipoproduto = Int32.Parse(dr_tipoProduto["codtipoproduto"].ToString());
                    aux.nometipoproduto = dr_tipoProduto["nometipoproduto"].ToString();

                    lista_tipoProduto.Add(aux);
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

            return lista_tipoProduto;
        }

        String sqlApaga = "DELETE FROM tipoproduto WHERE codtipoproduto = @pcod";
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

        String sqlTodos = "SELECT * FROM tipoproduto";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoProduto = new SqlDataAdapter(cmd);

            dt_tipoProduto = new DataTable();
            da_tipoProduto.Fill(dt_tipoProduto);

            return dt_tipoProduto;
        }

        String sqlFiltro = "SELECT * FROM tipoproduto WHERE nometipoproduto LIKE @pnometipoproduto";
        public DataTable Buscar_Filtro(String ptipoProduto)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnometipoproduto", ptipoProduto);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoProduto = new SqlDataAdapter(cmd);

            dt_tipoProduto = new DataTable();
            da_tipoProduto.Fill(dt_tipoProduto);

            //Finaliza a Conexão
            conn.Close();
            return dt_tipoProduto;
        }

        String sqlInsere = "INSERT INTO tipoproduto(nometipoproduto) VALUES (@pnometipoproduto)";
        public void Insere_Dados(Object aux)
        {
            Tipoproduto tipoProduto = new Tipoproduto();
            tipoProduto = (Tipoproduto)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnometipoproduto", tipoProduto.nometipoproduto);

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

        String sqlAtualiza = "UPDATE tipoproduto SET nometipoproduto = @pnometipoproduto WHERE codtipoproduto = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Tipoproduto dados = new Tipoproduto();
            dados = (Tipoproduto)aux;

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtipoproduto);
            cmd.Parameters.AddWithValue("@pnometipoproduto", dados.nometipoproduto);

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
