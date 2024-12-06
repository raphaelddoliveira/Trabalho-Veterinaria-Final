using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_TipoServico : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_tipoServico;
        SqlDataAdapter da_tipoServico;

        public List<Tiposervico> DadosTipoServico()
        {
            //Cria uma Lista do tipo TipoServico - Array
            List<Tiposervico> lista_tipoServico = new List<Tiposervico>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tipoServico;
            conn.Open();

            try
            {
                dr_tipoServico = cmd.ExecuteReader();
                while (dr_tipoServico.Read())
                {
                    Tiposervico aux = new Tiposervico();
                    aux.codtiposervico = Int32.Parse(dr_tipoServico["codtiposervico"].ToString());
                    aux.nometiposervico = dr_tipoServico["nometiposervico"].ToString();
                    aux.valortiposervico = Decimal.Parse(dr_tipoServico["valortiposervico"].ToString());

                    lista_tipoServico.Add(aux);
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

            return lista_tipoServico;
        }

        public List<Tiposervico> DadosTipoServicoFiltro(String parametro)
        {
            //Cria uma Lista do tipo TipoServico - Array
            List<Tiposervico> lista_tipoServico = new List<Tiposervico>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnometiposervico", parametro + "%");

            SqlDataReader dr_tipoServico;
            conn.Open();

            try
            {
                dr_tipoServico = cmd.ExecuteReader();
                while (dr_tipoServico.Read())
                {
                    Tiposervico aux = new Tiposervico();
                    aux.codtiposervico = Int32.Parse(dr_tipoServico["codtiposervico"].ToString());
                    aux.nometiposervico = dr_tipoServico["nometiposervico"].ToString();
                    aux.valortiposervico = Decimal.Parse(dr_tipoServico["valortiposervico"].ToString());

                    lista_tipoServico.Add(aux);
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

            return lista_tipoServico;
        }

        String sqlApaga = "DELETE FROM tiposervico WHERE codtiposervico = @pcod";
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

        String sqlTodos = "SELECT * FROM tiposervico";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoServico = new SqlDataAdapter(cmd);

            dt_tipoServico = new DataTable();
            da_tipoServico.Fill(dt_tipoServico);

            return dt_tipoServico;
        }

        String sqlFiltro = "SELECT * FROM tiposervico WHERE nometiposervico LIKE @pnometiposervico";
        public DataTable Buscar_Filtro(String ptipoServico)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnometiposervico", ptipoServico);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoServico = new SqlDataAdapter(cmd);

            dt_tipoServico = new DataTable();
            da_tipoServico.Fill(dt_tipoServico);

            //Finaliza a Conexão
            conn.Close();
            return dt_tipoServico;
        }

        // Atualizando a query SQL para incluir valortiposervico
        String sqlInsere = "INSERT INTO tiposervico(nometiposervico, valortiposervico) VALUES (@pnometiposervico, @pvalortiposervico)";
        public void Insere_Dados(Object aux)
        {
            Tiposervico tipoServico = new Tiposervico();
            tipoServico = (Tiposervico)aux; // casting

            // Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnometiposervico", tipoServico.nometiposervico);
            cmd.Parameters.AddWithValue("@pvalortiposervico", tipoServico.valortiposervico); // Adicionando o valor do serviço

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


        String sqlAtualiza = "UPDATE tiposervico SET nometiposervico = @pnometiposervico WHERE codtiposervico = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Tiposervico dados = new Tiposervico();
            dados = (Tiposervico)aux;

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtiposervico);
            cmd.Parameters.AddWithValue("@pnometiposervico", dados.nometiposervico);
            cmd.Parameters.AddWithValue("@pvalortiposervico", dados.valortiposervico);

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
