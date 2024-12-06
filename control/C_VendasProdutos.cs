using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;
using Veterinaria.view;

namespace Veterinaria.control
{
    internal class C_VendasProdutos : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_VendasProdutos;
        SqlDataAdapter da_VendasProdutos;

        private const string sqlTodos = "SELECT * FROM vendasprodutos";
        private const string sqlFiltro = "SELECT * FROM vendasprodutos WHERE codvendafk = @pcodvendafk";
        private const string sqlInsere = "INSERT INTO vendasprodutos (codvendafk, codprodutofk, quantv, valorv) VALUES (@pcodvenda, @pcodproduto, @pquantv, @pvalorv)";
        private const string sqlAtualiza = "UPDATE vendasprodutos SET quantv = @pquantv, valorv = @pvalorv, codvendafk = @pcodvenda WHERE codvendafk = @pcodvenda AND codprodutofk = @pcodproduto";
        private const string sqlApaga = "DELETE FROM vendasprodutos WHERE codvendafk = @pcodvendafk";
        private const string sqlBuscar = "SELECT * FROM vendasprodutos WHERE codvendafk = @pcodvendafk";

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodvendafk", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Produto da venda excluído com sucesso.");
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
            VendasProdutos vendaProduto = aux as VendasProdutos;
            if (vendaProduto == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo VendasProdutos.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcodvenda", vendaProduto.vendas.codvenda);
            cmd.Parameters.AddWithValue("@pcodproduto", vendaProduto.produto.codproduto);
            cmd.Parameters.AddWithValue("@pquantv", vendaProduto.quantv);
            cmd.Parameters.AddWithValue("@pvalorv", vendaProduto.valorv);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Produto da venda atualizado com sucesso.");
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
            cmd.Parameters.AddWithValue("@pcodvendafk", dados);

            conn.Open();

            da_VendasProdutos = new SqlDataAdapter(cmd);
            dt_VendasProdutos = new DataTable();
            da_VendasProdutos.Fill(dt_VendasProdutos);

            conn.Close();
            return dt_VendasProdutos;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodvendafk", valor);

            conn.Open();

            SqlDataReader dr_VendasProdutos;
            VendasProdutos vendaProduto = new VendasProdutos();

            try
            {
                dr_VendasProdutos = cmd.ExecuteReader();
                if (dr_VendasProdutos.Read())
                {
                    Produto produto = new Produto()
                    {
                        codproduto = Int32.Parse(dr_VendasProdutos["codprodutofk"].ToString())
                    };

                    vendaProduto = new VendasProdutos()
                    {
                        vendas = new Vendas { codvenda = valor },
                        produto = produto,
                        quantv = Decimal.Parse(dr_VendasProdutos["quantv"].ToString()),
                        valorv = Decimal.Parse(dr_VendasProdutos["valorv"].ToString())
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

            return vendaProduto;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();

            da_VendasProdutos = new SqlDataAdapter(cmd);
            dt_VendasProdutos = new DataTable();
            da_VendasProdutos.Fill(dt_VendasProdutos);

            conn.Close();
            return dt_VendasProdutos;
        }

        public void Insere_Dados(object aux)
        {
            VendasProdutos vendaProduto = aux as VendasProdutos;
            if (vendaProduto == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo VendasProdutos.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pcodvenda", vendaProduto.vendas.codvenda);
            cmd.Parameters.AddWithValue("@pcodproduto", vendaProduto.produto.codproduto);
            cmd.Parameters.AddWithValue("@pquantv", vendaProduto.quantv);
            cmd.Parameters.AddWithValue("@pvalorv", vendaProduto.valorv);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Produto inserido na venda com sucesso.");
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
        public List<VendasProdutos> DadosVendasProdutos()
        {
            List<VendasProdutos> lista_VendasProdutos = new List<VendasProdutos>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_VendasProdutos;
            conn.Open();
            try
            {
                dr_VendasProdutos = cmd.ExecuteReader();
                while (dr_VendasProdutos.Read())
                {
                    Produto produto = new Produto()
                    {
                        codproduto = Int32.Parse(dr_VendasProdutos["codprodutofk"].ToString())
                    };
                    Vendas vendas = new Vendas()
                    {
                        codvenda = Int32.Parse(dr_VendasProdutos["codvendafk"].ToString())
                    };
                    VendasProdutos aux = new VendasProdutos()
                    {
                        produto = produto,
                        vendas = vendas,
                        quantv = decimal.Parse(dr_VendasProdutos["quantv"].ToString()),
                        valorv = decimal.Parse(dr_VendasProdutos["valorv"].ToString())
                    };

                    lista_VendasProdutos.Add(aux);
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

            return lista_VendasProdutos;
        }
        public List<VendasProdutos> DadosVendasProdutosFiltro(string filtro)
        {
            List<VendasProdutos> lista_VendasProdutos = new List<VendasProdutos>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pcodvendaservicofk", filtro + "%");

            SqlDataReader dr_VendasProdutos;
            conn.Open();
            try
            {
                dr_VendasProdutos = cmd.ExecuteReader();
                while (dr_VendasProdutos.Read())
                {
                    Produto produto = new Produto()
                    {
                        codproduto = Int32.Parse(dr_VendasProdutos["codprodutofk"].ToString())
                    };
                    Vendas vendas = new Vendas()
                    {
                        codvenda = Int32.Parse(dr_VendasProdutos["codvendafk"].ToString())
                    };
                    VendasProdutos aux = new VendasProdutos()
                    {
                        produto = produto,
                        vendas = vendas,
                        quantv = decimal.Parse(dr_VendasProdutos["quantv"].ToString()),
                        valorv = decimal.Parse(dr_VendasProdutos["valorv"].ToString())
                    };

                    lista_VendasProdutos.Add(aux);
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

            return lista_VendasProdutos;
        }
    }
    
}
