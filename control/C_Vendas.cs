using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Vendas : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_Vendas;
        SqlDataAdapter da_Vendas;

        private const string sqlTodos = "SELECT * FROM vendas";
        private const string sqlFiltro = "SELECT * FROM vendas ";
        private const string sqlInsere = "INSERT INTO vendas(datavenda, codclientefk, codfuncionariofk, codlojafk) VALUES (@pdatavenda, @pcodcliente, @pcodfuncionario, @pcodloja)";
        private const string sqlAtualiza = "UPDATE vendas SET datavenda = @pdatavenda, codclientefk = @pcodcliente, codfuncionariofk = @pcodfuncionario, codlojafk = @pcodloja WHERE codvenda = @pcodvenda";
        private const string sqlApaga = "DELETE FROM vendas WHERE codvenda = @pcodvenda";
        private const string sqlBuscar = "SELECT * FROM vendas WHERE codvenda = @pcodvenda";
        private const string sqlTodosDataTable = "SELECT v.codvenda, v.datavenda, c.nomecliente, f.nomefuncionario, l.nomeloja FROM vendas v, cliente c, funcionario f, loja l WHERE v.codclientefk = c.codcliente AND v.codfuncionariofk = f.codfuncionario AND v.codlojafk = l.codloja";

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodvenda", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Venda excluída com sucesso.");
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
            Vendas venda = aux as Vendas;
            if (venda == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Vendas.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pdatavenda", venda.datavenda);
            cmd.Parameters.AddWithValue("@pcodcliente", venda.cliente.codcliente);
            cmd.Parameters.AddWithValue("@pcodfuncionario", venda.funcionario.codfuncionario);
            cmd.Parameters.AddWithValue("@pcodloja", venda.loja.codloja);
            cmd.Parameters.AddWithValue("@pcodvenda", venda.codvenda);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Venda atualizada com sucesso.");
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
            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodvenda", dados);

            conn.Open();

            da_Vendas = new SqlDataAdapter(cmd);
            dt_Vendas = new DataTable();
            da_Vendas.Fill(dt_Vendas);

            conn.Close();
            return dt_Vendas;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodvenda", valor);

            conn.Open();

            SqlDataReader dr_venda;
            Vendas venda = new Vendas();
            try
            {
                dr_venda = cmd.ExecuteReader();
                if (dr_venda.Read())
                {
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_venda["codclientefk"].ToString())
                    };
                    Funcionario funcionario = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_venda["codfuncionariofk"].ToString())
                    };
                    Loja loja = new Loja()
                    {
                        codloja = Int32.Parse(dr_venda["codlojafk"].ToString())
                    };

                    venda = new Vendas()
                    {
                        codvenda = Int32.Parse(dr_venda["codvenda"].ToString()),
                        datavenda = DateTime.Parse(dr_venda["datavenda"].ToString()),
                        cliente = cliente,
                        funcionario = funcionario,
                        loja = loja
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

            return venda;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodosDataTable, conn);

            conn.Open();

            da_Vendas = new SqlDataAdapter(cmd);
            dt_Vendas = new DataTable();
            da_Vendas.Fill(dt_Vendas);

            conn.Close();
            return dt_Vendas;
        }

        public void Insere_Dados(object aux)
        {
            Vendas venda = aux as Vendas;
            if (venda == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Vendas.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pdatavenda", venda.datavenda);
            cmd.Parameters.AddWithValue("@pcodcliente", venda.cliente.codcliente);
            cmd.Parameters.AddWithValue("@pcodfuncionario", venda.funcionario.codfuncionario);
            cmd.Parameters.AddWithValue("@pcodloja", venda.loja.codloja);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Venda inserida com sucesso.");
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

        public List<Vendas> DadosVendas()
        {
            List<Vendas> lista_Vendas = new List<Vendas>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Vendas;
            conn.Open();

            try
            {
                dr_Vendas = cmd.ExecuteReader();
                while (dr_Vendas.Read())
                {
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_Vendas["codclientefk"].ToString())
                    };
                    Funcionario funcionario = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_Vendas["codfuncionariofk"].ToString())
                    };
                    Loja loja = new Loja()
                    {
                        codloja = Int32.Parse(dr_Vendas["codlojafk"].ToString())
                    };

                    Vendas aux = new Vendas()
                    {
                        codvenda = Int32.Parse(dr_Vendas["codvenda"].ToString()),
                        datavenda = DateTime.Parse(dr_Vendas["datavenda"].ToString()),
                        cliente = cliente,
                        funcionario = funcionario,
                        loja = loja
                    };

                    lista_Vendas.Add(aux);
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

            return lista_Vendas;
        }

        public List<Vendas> DadosVendasFiltro(string filtro)
        {
            List<Vendas> lista_Vendas = new List<Vendas>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@nomecliente", filtro + "%");

            SqlDataReader dr_Vendas;
            conn.Open();

            try
            {
                dr_Vendas = cmd.ExecuteReader();
                while (dr_Vendas.Read())
                {
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_Vendas["codclientefk"].ToString())
                    };
                    Funcionario funcionario = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_Vendas["codfuncionariofk"].ToString())
                    };
                    Loja loja = new Loja()
                    {
                        codloja = Int32.Parse(dr_Vendas["codlojafk"].ToString())
                    };

                    Vendas aux = new Vendas()
                    {
                        codvenda = Int32.Parse(dr_Vendas["codvenda"].ToString()),
                        datavenda = DateTime.Parse(dr_Vendas["datavenda"].ToString()),
                        cliente = cliente,
                        funcionario = funcionario,
                        loja = loja
                    };

                    lista_Vendas.Add(aux);
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

            return lista_Vendas;
        }
    }
}
