using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Cidanimal : I_Metodos_Comuns
    {

        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_cidanimal;
        SqlDataAdapter da_cidanimal;

        public List<CidAnimal> DadosCidanimal()
        {

            List<CidAnimal> lista_cidanimal = new List<CidAnimal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_cidanimal;
            conn.Open();

            try
            {
                dr_cidanimal = cmd.ExecuteReader();
                while (dr_cidanimal.Read())
                {
                    CidAnimal aux = new CidAnimal();
                    aux.codcidanimal = Int32.Parse(dr_cidanimal["codcidanimal"].ToString());
                    aux.nomecidanimal = dr_cidanimal["nomecidanimal"].ToString();
                    aux.descricao = dr_cidanimal["descricao"].ToString();

                    lista_cidanimal.Add(aux);
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

            return lista_cidanimal;
        }

        public List<CidAnimal> DadosCidanimalFiltro(String parametro)
        {

            List<CidAnimal> lista_cidanimal = new List<CidAnimal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);


            cmd.Parameters.AddWithValue("pnomecidanimal", parametro + "%");

            SqlDataReader dr_cidanimal;
            conn.Open();

            try
            {
                dr_cidanimal = cmd.ExecuteReader();
                while (dr_cidanimal.Read())
                {
                    CidAnimal aux = new CidAnimal();
                    aux.codcidanimal = Int32.Parse(dr_cidanimal["codcidanimal"].ToString());
                    aux.nomecidanimal = dr_cidanimal["nomecidanimal"].ToString();
                    aux.descricao = dr_cidanimal["descricao"].ToString();

                    lista_cidanimal.Add(aux);
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

            return lista_cidanimal;
        }

        String sqlApaga = "DELETE FROM cidanimal WHERE codcidanimal = @pcod";
        public void Apaga_Dados(int aux)
        {

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

        String sqlTodos = "SELECT * FROM cidanimal";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();

            da_cidanimal = new SqlDataAdapter(cmd);

            dt_cidanimal = new DataTable();
            da_cidanimal.Fill(dt_cidanimal);

            return dt_cidanimal;
        }

        String sqlFiltro = "SELECT * FROM cidanimal WHERE nomecidanimal LIKE @pnomecidanimal";
        public DataTable Buscar_Filtro(String pnomeCidanimal)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomecidanimal", pnomeCidanimal);

            conn.Open();

            da_cidanimal = new SqlDataAdapter(cmd);

            dt_cidanimal = new DataTable();
            da_cidanimal.Fill(dt_cidanimal);

            conn.Close();
            return dt_cidanimal;
        }

        String sqlInsere = "INSERT INTO cidanimal(nomecidanimal, descricao) VALUES (@pnomecidanimal, @pdescricao)";
        public void Insere_Dados(Object aux)
        {
            CidAnimal cidanimal = new CidAnimal();
            cidanimal = (CidAnimal)aux; 
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnomecidanimal", cidanimal.nomecidanimal);
            cmd.Parameters.AddWithValue("@pdescricao", cidanimal.descricao);

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

        String sqlAtualiza = "UPDATE cidanimal SET nomecidanimal = @pnomecidanimal, descricao = @pdescricao WHERE codcidanimal = @pcod";
        public void Atualizar_Dados(object aux)
        {
            CidAnimal dados = new CidAnimal();
            dados = (CidAnimal)aux;

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codcidanimal);
            cmd.Parameters.AddWithValue("@pnomecidanimal", dados.nomecidanimal);
            cmd.Parameters.AddWithValue("@pdescricao", dados.descricao);

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
