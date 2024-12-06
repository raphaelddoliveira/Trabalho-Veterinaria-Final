using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Produto : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_Produto;
        SqlDataAdapter da_Produto;

        private const string sqlTodos = "SELECT * FROM produto";
        private const string sqlFiltro = "SELECT * FROM produto WHERE nomeproduto LIKE @pnomeproduto";
        private const string sqlInsere = "INSERT INTO produto(nomeproduto, codmarcafk, quantidade, valor, codtipoprodutofk) VALUES (@pnomeproduto, @pcodmarca, @pquantidade, @pvalor, @pcodtipoproduto)";
        private const string sqlAtualiza = "UPDATE produto SET nomeproduto = @pnomeproduto, codmarcafk = @pcodmarca, quantidade = @pquantidade, valor = @pvalor, codtipoprodutofk = @pcodtipoproduto WHERE codproduto = @pcodproduto";
        private const string sqlApaga = "DELETE FROM produto WHERE codproduto = @pcodproduto";
        private const string sqlBuscar = "SELECT * FROM produto WHERE codproduto = @pcodproduto";
        private const string sqlTodosDataTable = "SELECT p.codproduto, p.nomeproduto, p.valor, p. quantidade, m.nomemarca, t.nometipoproduto FROM produto p, marca m, tipoproduto t WHERE p.codmarcafk = m.codmarca AND p.codtipoprodutofk = t.codtipoproduto";

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodproduto", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Produto excluído com sucesso.");
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
            Produto produto = aux as Produto;
            if (produto == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Produto.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pnomeproduto", produto.nomeproduto);
            cmd.Parameters.AddWithValue("@pcodmarca", produto.marca.codmarca);
            cmd.Parameters.AddWithValue("@pquantidade", produto.quantidade);
            cmd.Parameters.AddWithValue("@pvalor", produto.valor);
            cmd.Parameters.AddWithValue("@pcodtipoproduto", produto.tipoproduto.codtipoproduto);
            cmd.Parameters.AddWithValue("@pcodproduto", produto.codproduto);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Produto atualizado com sucesso.");
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
            cmd.Parameters.AddWithValue("@pnomeproduto", dados + "%");

            conn.Open();

            da_Produto = new SqlDataAdapter(cmd);
            dt_Produto = new DataTable();
            da_Produto.Fill(dt_Produto);

            conn.Close();
            return dt_Produto;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodproduto", valor);

            conn.Open();

            SqlDataReader dr_Produto;
            Produto produto = new Produto();
            try
            {
                dr_Produto = cmd.ExecuteReader();
                if (dr_Produto.Read())
                {
                    Marca marca = new Marca()
                    {
                        codmarca = Int32.Parse(dr_Produto["codmarcafk"].ToString())
                    };
                    Tipoproduto tipoProduto = new Tipoproduto()
                    {
                        codtipoproduto = Int32.Parse(dr_Produto["codtipoprodutofk"].ToString())
                    };

                    produto = new Produto()
                    {
                        codproduto = Int32.Parse(dr_Produto["codproduto"].ToString()),
                        nomeproduto = dr_Produto["nomeproduto"].ToString(),
                        marca = marca,
                        quantidade = decimal.Parse(dr_Produto["quantidade"].ToString()),
                        valor = decimal.Parse(dr_Produto["valor"].ToString()),
                        tipoproduto = tipoProduto
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

            return produto;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodosDataTable, conn);

            conn.Open();

            da_Produto = new SqlDataAdapter(cmd);
            dt_Produto = new DataTable();
            da_Produto.Fill(dt_Produto);

            conn.Close();
            return dt_Produto;
        }

        public void Insere_Dados(object aux)
        {
            Produto produto = aux as Produto;
            if (produto == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Produto.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnomeproduto", produto.nomeproduto);
            cmd.Parameters.AddWithValue("@pcodmarca", produto.marca.codmarca);
            cmd.Parameters.AddWithValue("@pquantidade", produto.quantidade);
            cmd.Parameters.AddWithValue("@pvalor", produto.valor);
            cmd.Parameters.AddWithValue("@pcodtipoproduto", produto.tipoproduto.codtipoproduto);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Produto inserido com sucesso.");
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

        public List<Produto> DadosProduto()
        {
            List<Produto> lista_Produto = new List<Produto>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Produto;
            conn.Open();

            try
            {
                dr_Produto = cmd.ExecuteReader();
                while (dr_Produto.Read())
                {
                    Marca marca = new Marca()
                    {
                        codmarca = Int32.Parse(dr_Produto["codmarcafk"].ToString())
                    };
                    Tipoproduto tipoProduto = new Tipoproduto()
                    {
                        codtipoproduto = Int32.Parse(dr_Produto["codtipoprodutofk"].ToString())
                    };

                    Produto aux = new Produto()
                    {
                        codproduto = Int32.Parse(dr_Produto["codproduto"].ToString()),
                        nomeproduto = dr_Produto["nomeproduto"].ToString(),
                        marca = marca,
                        quantidade = decimal.Parse(dr_Produto["quantidade"].ToString()),
                        valor = decimal.Parse(dr_Produto["valor"].ToString()),
                        tipoproduto = tipoProduto
                    };

                    lista_Produto.Add(aux);
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

            return lista_Produto;
        }

        public List<Produto> DadosProdutoFiltro(string filtro)
        {
            List<Produto> lista_Produto = new List<Produto>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pnomeproduto", filtro + "%");

            SqlDataReader dr_Produto;
            conn.Open();

            try
            {
                dr_Produto = cmd.ExecuteReader();
                while (dr_Produto.Read())
                {
                    Marca marca = new Marca()
                    {
                        codmarca = Int32.Parse(dr_Produto["codmarcafk"].ToString())
                    };
                    Tipoproduto tipoProduto = new Tipoproduto()
                    {
                        codtipoproduto = Int32.Parse(dr_Produto["codtipoprodutofk"].ToString())
                    };

                    Produto aux = new Produto()
                    {
                        codproduto = Int32.Parse(dr_Produto["codproduto"].ToString()),
                        nomeproduto = dr_Produto["nomeproduto"].ToString(),
                        marca = marca,
                        quantidade = decimal.Parse(dr_Produto["quantidade"].ToString()),
                        valor = decimal.Parse(dr_Produto["valor"].ToString()),
                        tipoproduto = tipoProduto
                    };

                    lista_Produto.Add(aux);
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

            return lista_Produto;
        }
    }
}
