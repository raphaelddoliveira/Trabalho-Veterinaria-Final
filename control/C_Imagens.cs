using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Imagens : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_Imagens;
        SqlDataAdapter da_Imagens;

        private const string sqlTodos = "SELECT * FROM imagens";
        private const string sqlFiltro = "SELECT * FROM imagens WHERE descricao LIKE @pdescricao";
        private const string sqlInsere = "INSERT INTO imagens(descricao, foto, codprodutofk) VALUES (@pdescricao, @pfoto, @pcodproduto)";
        private const string sqlAtualiza = "UPDATE imagens SET descricao = @pdescricao, foto = @pfoto, codprodutofk = @pcodproduto WHERE codimagens = @pcodimagens";
        private const string sqlApaga = "DELETE FROM imagens WHERE codimagens = @pcodimagens";
        private const string sqlBuscar = "SELECT * FROM imagens WHERE codimagens = @pcodimagens";
        private const string sqlTodosDataTable = "SELECT i.codimagens, i.descricao, i.foto, p.nomeproduto FROM imagens i JOIN produto p ON i.codprodutofk = p.codproduto";
        private const string sqlFiltroImagens = "SELECT i.codimagens, i.descricao, p.nomeproduto FROM imagens i JOIN produto p ON i.codprodutofk = p.codproduto WHERE i.codimagens = @pcodimagens";

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodimagens", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Imagem excluída com sucesso.");
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
            Imagens imagem = aux as Imagens;
            if (imagem == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Imagens.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pdescricao", imagem.descricao);
            cmd.Parameters.AddWithValue("@pfoto", imagem.foto);
            cmd.Parameters.AddWithValue("@pcodproduto", imagem.produto.codproduto);
            cmd.Parameters.AddWithValue("@pcodimagens", imagem.codimagens);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Imagem atualizada com sucesso.");
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
            cmd.Parameters.AddWithValue("@pdescricao", dados + "%");

            conn.Open();

            da_Imagens = new SqlDataAdapter(cmd);
            dt_Imagens = new DataTable();
            da_Imagens.Fill(dt_Imagens);

            conn.Close();
            return dt_Imagens;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodimagens", valor);

            conn.Open();

            SqlDataReader dr_imagem;
            Imagens imagem = new Imagens();
            try
            {
                dr_imagem = cmd.ExecuteReader();
                if (dr_imagem.Read())
                {
                    Produto produto = new Produto()
                    {
                        codproduto = Int32.Parse(dr_imagem["codprodutofk"].ToString())
                    };

                    imagem = new Imagens()
                    {
                        codimagens = Int32.Parse(dr_imagem["codimagens"].ToString()),
                        descricao = dr_imagem["descricao"].ToString(),
                        foto = (byte[])dr_imagem["foto"],
                        produto = produto
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

            return imagem;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodosDataTable, conn);

            conn.Open();

            da_Imagens = new SqlDataAdapter(cmd);
            dt_Imagens = new DataTable();
            da_Imagens.Fill(dt_Imagens);

            conn.Close();
            return dt_Imagens;
        }

        public void Insere_Dados(object aux)
        {
            Imagens imagem = aux as Imagens;
            if (imagem == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Imagens.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pdescricao", imagem.descricao);
            cmd.Parameters.AddWithValue("@pfoto", imagem.foto);
            cmd.Parameters.AddWithValue("@pcodproduto", imagem.produto.codproduto);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Imagem inserida com sucesso.");
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

        public List<Imagens> DadosImagens()
        {
            List<Imagens> lista_Imagens = new List<Imagens>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Imagem;
            conn.Open();

            try
            {
                dr_Imagem = cmd.ExecuteReader();
                while (dr_Imagem.Read())
                {
                    Produto produto = new Produto()
                    {
                        codproduto = Int32.Parse(dr_Imagem["codprodutofk"].ToString())
                    };

                    Imagens imagem = new Imagens()
                    {
                        codimagens = Int32.Parse(dr_Imagem["codimagens"].ToString()),
                        descricao = dr_Imagem["descricao"].ToString(),
                        foto = (byte[])dr_Imagem["foto"],
                        produto = produto
                    };

                    lista_Imagens.Add(imagem);
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

            return lista_Imagens;
        }

        public List<Imagens> DadosImagensFiltro(string filtro)
        {
            List<Imagens> lista_Imagens = new List<Imagens>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pdescricao", filtro + "%");

            SqlDataReader dr_Imagem;
            conn.Open();

            try
            {
                dr_Imagem = cmd.ExecuteReader();
                while (dr_Imagem.Read())
                {
                    Produto produto = new Produto()
                    {
                        codproduto = Int32.Parse(dr_Imagem["codprodutofk"].ToString())
                    };

                    Imagens imagem = new Imagens()
                    {
                        codimagens = Int32.Parse(dr_Imagem["codimagens"].ToString()),
                        descricao = dr_Imagem["descricao"].ToString(),
                        foto = (byte[])dr_Imagem["foto"],
                        produto = produto
                    };

                    lista_Imagens.Add(imagem);
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

            return lista_Imagens;
        }
    }
}
