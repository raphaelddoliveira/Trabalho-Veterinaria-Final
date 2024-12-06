using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_ItensVendaServico : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_ItensVendaServico;
        SqlDataAdapter da_ItensVendaServico;

        private const string sqlTodos = "SELECT * FROM itensvendaservico";
        private const string sqlFiltro = "SELECT * FROM itensvendaservico WHERE codvendaservicofk LIKE @pcodvendaservicofk";
        private const string sqlInsere = "INSERT INTO itensvendaservico(codtiposervicofk, codvendaservicofk, quant, valor, codcidanimalfk) VALUES (@pcodtiposervico, @pcodvendaservico, @pquant, @pvalor, @pcodcidanimal)";
        private const string sqlAtualiza = "UPDATE itensvendaservico SET quant = @pquant, valor = @pvalor, codtiposervicofk = @pcodtiposervico, codvendaservicofk = @pcodvendaservico, codcidanimalfk = @pcodcidanimal WHERE codtiposervicofk = @pcodtiposervico AND codvendaservicofk = @pcodvendaservico AND codcidanimalfk = codcidanimal";
        private const string sqlApaga = "DELETE FROM itensvendaservico WHERE codvendaservicofk = @pcodvendaservico";
        private const string sqlTodosDataTable = "SELECT v.codvendaservico, i.quant, i.valor, c.codcidanimal, t.nometiposervico FROM itensvendaservico i, cidanimal c, vendaservico v, tiposervico t WHERE i.codtiposervicofk = t.codtiposervico AND i.codvendaservicofk = v.codvendaservico AND i.codcidanimalfk = c.codcidanimal";
        private const string sqlBuscar = "SELECT * FROM itensvendaservico WHERE codvendaservicofk = @pcodvendaservicofk";

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodvendaservico", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Item de venda/serviço excluído com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir item: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
       
        public void Atualizar_Dados(object aux)
        {
            ItensVendaServico item = aux as ItensVendaServico;
            if (item == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo ItensVendaServico.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcodtiposervico", item.tipoServico.codtiposervico);
            cmd.Parameters.AddWithValue("@pcodvendaservico", item.vendaServico.codvendaservico);
            cmd.Parameters.AddWithValue("@pcodcidanimal", item.cidAnimal.codcidanimal);
            cmd.Parameters.AddWithValue("@pquant", item.quant);
            cmd.Parameters.AddWithValue("@pvalor", item.valor);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Item de venda/serviço atualizado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar item: " + ex.Message);
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
            cmd.Parameters.AddWithValue("@pcodvendaservicofk", dados + "%");

            conn.Open();

            da_ItensVendaServico = new SqlDataAdapter(cmd);
            dt_ItensVendaServico = new DataTable();
            da_ItensVendaServico.Fill(dt_ItensVendaServico);

            conn.Close();
            return dt_ItensVendaServico;
        }

        public object Buscar_Id(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodvendaservicofk", aux);
            

            conn.Open();

            SqlDataReader dr_ItensVendaServico;
            ItensVendaServico item = new ItensVendaServico();
            try
            {
                dr_ItensVendaServico = cmd.ExecuteReader();
                if (dr_ItensVendaServico.Read())
                {
                    Tiposervico tiposervico = new Tiposervico()
                    {
                        codtiposervico = Int32.Parse(dr_ItensVendaServico["codtiposervicofk"].ToString())
                    };
                    VendaServico vendaServico = new VendaServico()
                    {
                        codvendaservico = Int32.Parse(dr_ItensVendaServico["codvendaservicofk"].ToString())
                    };
                    CidAnimal cidAnimal = new CidAnimal()
                    {
                        codcidanimal = Int32.Parse(dr_ItensVendaServico["codcidanimalfk"].ToString())
                    };

                    item = new ItensVendaServico()
                    {
                        tipoServico = tiposervico,
                        vendaServico = vendaServico,
                        quant = decimal.Parse(dr_ItensVendaServico["quant"].ToString()),
                        valor = decimal.Parse(dr_ItensVendaServico["valor"].ToString()),
                        cidAnimal = cidAnimal
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar item: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return item;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodosDataTable, conn);

            conn.Open();

            da_ItensVendaServico = new SqlDataAdapter(cmd);
            dt_ItensVendaServico = new DataTable();
            da_ItensVendaServico.Fill(dt_ItensVendaServico);

            conn.Close();
            return dt_ItensVendaServico;
        }

        public void Insere_Dados(object aux)
        {
            ItensVendaServico item = aux as ItensVendaServico;
            if (item == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo ItensVendaServico.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pcodtiposervico", item.tipoServico.codtiposervico);
            cmd.Parameters.AddWithValue("@pcodvendaservico", item.vendaServico.codvendaservico);
            cmd.Parameters.AddWithValue("@pquant", item.quant);
            cmd.Parameters.AddWithValue("@pvalor", item.valor);
            cmd.Parameters.AddWithValue("@pcodcidanimal", item.cidAnimal.codcidanimal);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Item de venda/serviço inserido com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir item: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public List<ItensVendaServico> DadosItensVendaServico()
        {
            List<ItensVendaServico> lista_ItensVendaServico = new List<ItensVendaServico>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_ItensVendaServico;
            conn.Open();
            try
            {
                dr_ItensVendaServico = cmd.ExecuteReader();
                while (dr_ItensVendaServico.Read())
                {
                    Tiposervico tiposervico = new Tiposervico()
                    {
                        codtiposervico = Int32.Parse(dr_ItensVendaServico["codtiposervicofk"].ToString())
                    };
                    VendaServico vendaservico = new VendaServico()
                    {
                        codvendaservico = Int32.Parse(dr_ItensVendaServico["codvendaservicofk"].ToString())
                    };
                    CidAnimal cidanimal = new CidAnimal()
                    {
                        codcidanimal = Int32.Parse(dr_ItensVendaServico["codcidanimalfk"].ToString())
                    };


                    ItensVendaServico aux = new ItensVendaServico()
                    {
                        tipoServico = tiposervico,
                        vendaServico = vendaservico,
                        quant = decimal.Parse(dr_ItensVendaServico["quant"].ToString()),
                        valor = decimal.Parse(dr_ItensVendaServico["valor"].ToString()),
                        cidAnimal = cidanimal
                    };

                    lista_ItensVendaServico.Add(aux);
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

            return lista_ItensVendaServico;
        }
    

            public List<ItensVendaServico> DadosItensVendaServicoFiltro(string filtro)
        {
            List<ItensVendaServico> lista_ItensVendaServico = new List<ItensVendaServico>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pcodvendaservicofk", filtro + "%");

            SqlDataReader dr_ItensVendaServico;
            conn.Open();

            try
            {
                dr_ItensVendaServico = cmd.ExecuteReader();
                while (dr_ItensVendaServico.Read())
                {
                    Tiposervico tiposervico = new Tiposervico()
                    {
                        codtiposervico = Int32.Parse(dr_ItensVendaServico["codtiposervicofk"].ToString())
                    };
                    VendaServico vendaservico = new VendaServico()
                    {
                        codvendaservico = Int32.Parse(dr_ItensVendaServico["codvendaservicofk"].ToString())
                    };
                    CidAnimal cidanimal = new CidAnimal() 
                    { 
                        codcidanimal = Int32.Parse(dr_ItensVendaServico["codcidanimalfk"].ToString())
                    };


                    ItensVendaServico aux = new ItensVendaServico()
                    {
                        tipoServico = tiposervico,
                        vendaServico = vendaservico,
                        quant = decimal.Parse(dr_ItensVendaServico["quant"].ToString()),
                        valor = decimal.Parse(dr_ItensVendaServico["valor"].ToString()),
                        cidAnimal = cidanimal
                    };

                    lista_ItensVendaServico.Add(aux);
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

            return lista_ItensVendaServico;
        }

    }
}

