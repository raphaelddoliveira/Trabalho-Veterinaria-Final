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
    internal class C_Telefone : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_telefone;
        SqlDataAdapter da_telefone;

        public List<Telefone> DadosTelefone()
        {
            //Cria uma Lista do tipo Telefone - Array
            List<Telefone> lista_telefone = new List<Telefone>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_telefone;
            conn.Open();

            try
            {
                dr_telefone = cmd.ExecuteReader();
                while (dr_telefone.Read())
                {
                    Telefone aux = new Telefone();
                    aux.codtelefone = Int32.Parse(dr_telefone["codtelefone"].ToString());
                    aux.numerotelefone = dr_telefone["numerotelefone"].ToString();

                    lista_telefone.Add(aux);
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

            return lista_telefone;
        }

        public List<Telefone> DadosTelefoneFiltro(String parametro)
        {
            //Cria uma Lista do tipo Telefone - Array
            List<Telefone> lista_telefone = new List<Telefone>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnumerotelefone", parametro + "%");

            SqlDataReader dr_telefone;
            conn.Open();

            try
            {
                dr_telefone = cmd.ExecuteReader();
                while (dr_telefone.Read())
                {
                    Telefone aux = new Telefone();
                    aux.codtelefone = Int32.Parse(dr_telefone["codtelefone"].ToString());
                    aux.numerotelefone = dr_telefone["numerotelefone"].ToString();

                    lista_telefone.Add(aux);
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

            return lista_telefone;
        }

        String sqlApaga = "DELETE FROM telefone WHERE codtelefone = @pcod";
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

        String sqlTodos = "SELECT * FROM telefone";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_telefone = new SqlDataAdapter(cmd);

            dt_telefone = new DataTable();
            da_telefone.Fill(dt_telefone);

            return dt_telefone;
        }

        String sqlFiltro = "SELECT * FROM telefone WHERE numerotelefone LIKE @pnumerotelefone";
        public DataTable Buscar_Filtro(String ptelefone)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnumerotelefone", ptelefone);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_telefone = new SqlDataAdapter(cmd);

            dt_telefone = new DataTable();
            da_telefone.Fill(dt_telefone);

            //Finaliza a Conexão
            conn.Close();
            return dt_telefone;
        }

        String sqlInsere = "INSERT INTO telefone(numerotelefone) VALUES (@pnumerotelefone)";
        public void Insere_Dados(Object aux)
        {
            Telefone telefone = new Telefone();
            telefone = (Telefone)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnumerotelefone", telefone.numerotelefone);

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

        String sqlAtualiza = "UPDATE telefone SET numerotelefone = @pnumerotelefone WHERE codtelefone = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Telefone dados = new Telefone();
            dados = (Telefone)aux;

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtelefone);
            cmd.Parameters.AddWithValue("@pnumerotelefone", dados.numerotelefone);

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
