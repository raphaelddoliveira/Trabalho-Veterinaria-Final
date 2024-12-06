using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_VendaServico : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_VendaServico;
        SqlDataAdapter da_VendaServico;

        private const string sqlTodos = "SELECT * FROM vendaservico";
        private const string sqlFiltro = "SELECT * FROM vendaservico WHERE codvendaservico LIKE @pcodvendaservico";
        private const string sqlInsere = "INSERT INTO vendaservico(codfuncionariofk, datavs, codclientefk, codanimalfk) VALUES (@pcodfuncionariofk, @pdatavs, @pcodclientefk, @pcodanimalfk)";
        private const string sqlAtualiza = "UPDATE vendaservico SET codfuncionariofk = @pcodfuncionariofk, datavs = @pdatavs, codclientefk = @pcodclientefk, codanimalfk = @pcodanimalfk WHERE codvendaservico = @pcodvendaservico";
        private const string sqlApaga = "DELETE FROM vendaservico WHERE codvendaservico = @pcodvendaservico";
        private const string sqlBuscar = "SELECT * FROM vendaservico WHERE codvendaservico = @pcodvendaservico";
        private const string sqlTodosDataTable = "SELECT v.codvendaservico, v.datavs, f.nomefuncionario, c.nomecliente, a.nomeanimal FROM vendaservico v, funcionario f, cliente c, animal a WHERE v.codfuncionariofk = f.codfuncionario AND v.codclientefk = c.codcliente AND v.codanimalfk = a.codanimal";

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
                    MessageBox.Show("Venda de serviço excluída com sucesso.");
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
            VendaServico vendaServico = aux as VendaServico;
            if (vendaServico == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo VendaServico.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcodfuncionariofk", vendaServico.funcionario.codfuncionario);
            cmd.Parameters.AddWithValue("@pdatavs", vendaServico.datavs);
            cmd.Parameters.AddWithValue("@pcodclientefk", vendaServico.cliente.codcliente);
            cmd.Parameters.AddWithValue("@pcodanimalfk", vendaServico.animal.codanimal);
            cmd.Parameters.AddWithValue("@pcodvendaservico", vendaServico.codvendaservico);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Venda de serviço atualizada com sucesso.");
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
            cmd.Parameters.AddWithValue("@pcodvendaservico", dados + "%");

            conn.Open();

            da_VendaServico = new SqlDataAdapter(cmd);
            dt_VendaServico = new DataTable();
            da_VendaServico.Fill(dt_VendaServico);

            conn.Close();
            return dt_VendaServico;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodvendaservico", valor);

            conn.Open();

            SqlDataReader dr_vendaServico;
            VendaServico vendaServico = new VendaServico();
            try
            {
                dr_vendaServico = cmd.ExecuteReader();
                if (dr_vendaServico.Read())
                {
                    Funcionario funcionario = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_vendaServico["codfuncionariofk"].ToString())
                    };
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_vendaServico["codclientefk"].ToString())
                    };
                    Animal animal = new Animal()
                    {
                        codanimal = Int32.Parse(dr_vendaServico["codanimalfk"].ToString())
                    };

                    vendaServico = new VendaServico()
                    {
                        codvendaservico = Int32.Parse(dr_vendaServico["codvendaservico"].ToString()),
                        datavs = DateTime.Parse(dr_vendaServico["datavs"].ToString()),
                        funcionario = funcionario,
                        cliente = cliente,
                        animal = animal
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

            return vendaServico;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodosDataTable, conn);

            conn.Open();

            da_VendaServico = new SqlDataAdapter(cmd);
            dt_VendaServico = new DataTable();
            da_VendaServico.Fill(dt_VendaServico);

            conn.Close();
            return dt_VendaServico;
        }

        public void Insere_Dados(object aux)
        {
            VendaServico vendaServico = aux as VendaServico;
            if (vendaServico == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo VendaServico.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pcodfuncionariofk", vendaServico.funcionario.codfuncionario);
            cmd.Parameters.AddWithValue("@pdatavs", vendaServico.datavs);
            cmd.Parameters.AddWithValue("@pcodclientefk", vendaServico.cliente.codcliente);
            cmd.Parameters.AddWithValue("@pcodanimalfk", vendaServico.animal.codanimal);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Venda de serviço inserida com sucesso.");
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
        public List<VendaServico> DadosVendaServico()
        {
            List<VendaServico> lista_VendaServico = new List<VendaServico>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_VendaServico;
            conn.Open();

            try
            {
                dr_VendaServico = cmd.ExecuteReader();
                while (dr_VendaServico.Read())
                {
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_VendaServico["codclientefk"].ToString())
                    };
                    Funcionario funcionario = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_VendaServico["codfuncionariofk"].ToString())
                    };
                    Animal animal = new Animal()
                    {
                        codanimal = Int32.Parse(dr_VendaServico["codanimalfk"].ToString())
                    };

                    VendaServico aux = new VendaServico()
                    {
                        codvendaservico = Int32.Parse(dr_VendaServico["codvendaservico"].ToString()),
                        datavs = DateTime.Parse(dr_VendaServico["datavs"].ToString()),
                        funcionario = funcionario,
                        cliente = cliente,
                        animal = animal
                    };

                    lista_VendaServico.Add(aux);
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

            return lista_VendaServico;
        }

        public List<VendaServico> DadosVendaServicoFiltro(string filtro)
        {
            List<VendaServico> lista_VendaServico = new List<VendaServico>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_VendaServico;
            conn.Open();

            try
            {
                dr_VendaServico = cmd.ExecuteReader();
                while (dr_VendaServico.Read())
                {
                    Funcionario funcionario = new Funcionario()
                    {
                        codfuncionario = Int32.Parse(dr_VendaServico["codfuncionariofk"].ToString())
                    };
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_VendaServico["codclientefk"].ToString())
                    };
                    Animal animal = new Animal()
                    {
                        codanimal = Int32.Parse(dr_VendaServico["codanimalfk"].ToString())
                    };

                    VendaServico aux = new VendaServico()
                    {
                        codvendaservico = Int32.Parse(dr_VendaServico["codvendaservico"].ToString()),
                        datavs = DateTime.Parse(dr_VendaServico["datavs"].ToString()),
                        funcionario = funcionario,
                        cliente = cliente,
                        animal = animal
                    };

                    lista_VendaServico.Add(aux);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "List");
            }
            finally
            {
                conn.Close();
            }

            return lista_VendaServico;
        }
    }
}
