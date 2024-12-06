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
    internal class C_Estado : I_Metodos_Comuns
    {
        //Variáveis Globais da Classe
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_estado;
        SqlDataAdapter da_estado;

        public List<Estado> DadosEstado()
        {
            //Cria uma Lista do tipo Estado - Array
            List<Estado> lista_estado = new List<Estado>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_estado;
            conn.Open();

            try
            {
                dr_estado = cmd.ExecuteReader();
                while (dr_estado.Read())
                {
                    Estado aux = new Estado();
                    aux.codestado = Int32.Parse(dr_estado["codestado"].ToString());
                    aux.nomeestado = dr_estado["nomeestado"].ToString();
                    aux.sigla = dr_estado["sigla"].ToString();

                    lista_estado.Add(aux);
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

            return lista_estado;
        }

        public List<Estado> DadosEstadoFiltro(String parametro)
        {
            //Cria uma Lista do tipo Estado - Array
            List<Estado> lista_estado = new List<Estado>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnomeestado", parametro + "%");

            SqlDataReader dr_estado;
            conn.Open();

            try
            {
                dr_estado = cmd.ExecuteReader();
                while (dr_estado.Read())
                {
                    Estado aux = new Estado();
                    aux.codestado = Int32.Parse(dr_estado["codestado"].ToString());
                    aux.nomeestado = dr_estado["nomeestado"].ToString();
                    aux.sigla = dr_estado["sigla"].ToString();

                    lista_estado.Add(aux);
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

            return lista_estado;
        }

        String sqlApaga = "DELETE FROM estado WHERE codestado = @pcod";
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

        String sqlTodos = "SELECT * FROM estado";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_estado = new SqlDataAdapter(cmd);

            dt_estado = new DataTable();
            da_estado.Fill(dt_estado);

            return dt_estado;
        }

        String sqlFiltro = "SELECT * FROM estado WHERE nomeestado LIKE @pnomeestado";
        public DataTable Buscar_Filtro(String pestado)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomeestado", pestado);

            //Abrir Conexão
            conn.Open();

            //Criar o DataAdapter
            da_estado = new SqlDataAdapter(cmd);

            dt_estado = new DataTable();
            da_estado.Fill(dt_estado);

            //Finaliza a Conexão
            conn.Close();
            return dt_estado;
        }

        String sqlInsere = "INSERT INTO estado(nomeestado, sigla) VALUES (@pnome, @psigla)";
        public void Insere_Dados(Object aux)
        {
            Estado estado = new Estado();
            estado = (Estado)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", estado.nomeestado);
            cmd.Parameters.AddWithValue("@psigla", estado.sigla);

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

        String sqlAtualiza = "UPDATE estado SET nomeestado = @pnome, sigla = @psigla WHERE codestado = @pcod";
        public void Atualizar_Dados(object aux)
        {
            Estado dados = new Estado();
            dados = (Estado)aux;

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codestado);
            cmd.Parameters.AddWithValue("@pnome", dados.nomeestado);
            cmd.Parameters.AddWithValue("@psigla", dados.sigla);

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
