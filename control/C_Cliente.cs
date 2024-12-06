using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Cliente : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_Cliente;
        SqlDataAdapter da_Cliente;

        private const string sqlTodos = "SELECT * FROM cliente";
        private const string sqlFiltro = "select * from cliente where nomecliente like @pnomecliente";
        private const string sqlInsere = "INSERT INTO cliente(nomecliente, codbairrofk, codruafk, codcepfk, codcidadefk, codestadofk, codpaisfk, numerocasa, cpf, fotocliente) VALUES (@pnomecliente, @pcodbairro, @pcodrua, @pcodcep, @pcodcidade, @pcodestado, @pcodpais, @pnumerocasa, @pcpf, @pfotocliente)";
        private const string sqlAtualiza = "UPDATE cliente SET nomecliente = @nomecliente, codbairrofk = @pcodbairro, codruafk = @pcodrua, codcepfk = @pcodcep, codcidadefk = @pcodcidade, codestadofk = @pcodestado, codpaisfk = @pcodpais, numerocasa = @pnumerocasa, cpf = @pcpf, fotocliente = @pfotocliente WHERE codcliente = @pcodcliente";

        private const string sqlApaga = "DELETE FROM cliente WHERE codcliente = @pcodcliente";

        private const string sqlTodosDataTable = "SELECT a.codcliente, a.nomecliente, a.cpf, b.nomerua, a.numerocasa, c.nomebairro, d.nomecidade, e.numerocep, f.nomeestado, g.nomepais FROM cliente AS a, rua AS b, bairro AS c, cidade AS d, cep AS e, estado AS f, pais AS g WHERE a.codruafk = b.codrua AND a.codbairrofk = c.codbairro AND a.codcidadefk = d.codcidade AND a.codcepfk = e.codcep AND a.codestadofk = f.codestado AND a.codpaisfk = g.codpais;";

        private const string sqlTodosDataTableFiltro = "SELECT a.codcliente, a.nomecliente, a.cpf, b.nomerua, a.numerocasa, c.nomebairro, d.nomecidade, e.numerocep, f.nomeestado, g.nomepais FROM cliente AS a, rua AS b, bairro AS c, cidade AS d, cep AS e, estado AS f, pais AS g WHERE a.codruafk = b.codrua AND a.codbairrofk = c.codbairro AND a.codcidadefk = d.codcidade AND a.codcepfk = e.codcep AND a.codestadofk = f.codestado AND a.codpaisfk = g.codpais AND nomecliente LIKE @pnomecliente;";

        string sqlBuscar = "SELECT * FROM cliente WHERE codcliente = @pdcodcliente";



        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodcliente", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Cliente excluído com sucesso.");
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
            Cliente cliente = aux as Cliente;
            if (cliente == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Cliente.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@nomecliente", cliente.nomecliente);
            cmd.Parameters.AddWithValue("@pcodbairro", cliente.bairro.codbairro);
            cmd.Parameters.AddWithValue("@pcodrua", cliente.rua.codrua);
            cmd.Parameters.AddWithValue("@pcodcep", cliente.cep.codcep);
            cmd.Parameters.AddWithValue("@pcodcidade", cliente.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pcodestado", cliente.estado.codestado);
            cmd.Parameters.AddWithValue("@pcodpais", cliente.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumerocasa", cliente.numerocasa);
            cmd.Parameters.AddWithValue("@pcpf", cliente.cpf);
            cmd.Parameters.AddWithValue("@pcodcliente", cliente.codcliente);
            cmd.Parameters.AddWithValue("@pfotocliente", cliente.fotocliente);


            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Cliente atualizado com sucesso.");
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
            cmd = new SqlCommand(sqlTodosDataTableFiltro, conn);
            cmd.Parameters.AddWithValue("@pnomecliente", dados);

            conn.Open();

            da_Cliente = new SqlDataAdapter(cmd);
            dt_Cliente = new DataTable();
            da_Cliente.Fill(dt_Cliente);

            conn.Close();
            return dt_Cliente;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pdcodcliente", valor);

            conn.Open();

            SqlDataReader dr_cliente;
            Cliente cliente = new Cliente();
            try
            {
                dr_cliente = cmd.ExecuteReader();
                if (dr_cliente.Read())
                {
                    Rua rua = new Rua()
                    {
                        codrua = Int32.Parse(dr_cliente["codruafk"].ToString())
                    };
                    Cidade cidade = new Cidade
                    {
                        codcidade = Int32.Parse(dr_cliente["codcidadefk"].ToString())
                    };
                    Cep cep = new Cep
                    {
                        codcep = Int32.Parse(dr_cliente["codcepfk"].ToString())
                    };
                    Bairro bairro = new Bairro
                    {
                        codbairro = Int32.Parse(dr_cliente["codbairrofk"].ToString())
                    };
                    Pais pais = new Pais
                    {
                        codpais = Int32.Parse(dr_cliente["codpaisfk"].ToString())
                    };
                    Estado estado = new Estado
                    {
                        codestado = Int32.Parse(dr_cliente["codestadofk"].ToString())
                    };

                    cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_cliente["codcliente"].ToString()),
                        nomecliente = dr_cliente["nomecliente"].ToString(),
                        numerocasa = dr_cliente["numerocasa"].ToString(),
                        cpf = dr_cliente["cpf"].ToString(),
                        rua = rua,
                        cidade = cidade,
                        cep = cep,
                        bairro = bairro,
                        pais = pais,
                        estado = estado,
                        fotocliente = dr_cliente["fotocliente"] as byte[]
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

            return cliente;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodosDataTable, conn);

            conn.Open();

            da_Cliente = new SqlDataAdapter(cmd);
            dt_Cliente = new DataTable();
            da_Cliente.Fill(dt_Cliente);

            conn.Close();
            return dt_Cliente;
        }

        public void Insere_Dados(object aux)
        {
            Cliente cliente = aux as Cliente;
            if (cliente == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Cliente.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnomecliente", cliente.nomecliente);
            cmd.Parameters.AddWithValue("@pcodbairro", cliente.bairro.codbairro);
            cmd.Parameters.AddWithValue("@pcodrua", cliente.rua.codrua);
            cmd.Parameters.AddWithValue("@pcodcep", cliente.cep.codcep);
            cmd.Parameters.AddWithValue("@pcodcidade", cliente.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pcodestado", cliente.estado.codestado);
            cmd.Parameters.AddWithValue("@pcodpais", cliente.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumerocasa", cliente.numerocasa);
            cmd.Parameters.AddWithValue("@pcpf", cliente.cpf);
            cmd.Parameters.AddWithValue("@pfotocliente", cliente.fotocliente);


            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Cliente inserido com sucesso.");
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

        public List<Cliente> DadosCliente()
        {
            List<Cliente> lista_Cliente = new List<Cliente>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Cliente;
            conn.Open();

            try
            {
                dr_Cliente = cmd.ExecuteReader();
                while (dr_Cliente.Read())
                {
                    Rua rua = new Rua
                    {
                        codrua = Int32.Parse(dr_Cliente["codruafk"].ToString())
                    };
                    Cidade cidade = new Cidade()
                    {
                        codcidade = Int32.Parse(dr_Cliente["codcidadefk"].ToString())
                    };
                    Cep cep = new Cep()
                    {
                        codcep = Int32.Parse(dr_Cliente["codcepfk"].ToString())
                    };
                    Bairro bairro = new Bairro()
                    {
                        codbairro = Int32.Parse(dr_Cliente["codbairrofk"].ToString())
                    };
                    Pais pais = new Pais()
                    {
                        codpais = Int32.Parse(dr_Cliente["codpaisfk"].ToString())
                    };
                    Estado estado = new Estado()
                    {
                        codestado = Int32.Parse(dr_Cliente["codestadofk"].ToString())
                    };

                    Cliente aux = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_Cliente["codcliente"].ToString()),
                        nomecliente = dr_Cliente["nomecliente"].ToString(),
                        numerocasa = dr_Cliente["numerocasa"].ToString(),
                        cpf = dr_Cliente["cpf"].ToString(),
                        rua = rua,
                        cidade = cidade,
                        cep = cep,
                        bairro = bairro,
                        pais = pais,
                        estado = estado,
                        fotocliente = dr_Cliente["fotocliente"] as byte[]
                    };

                    lista_Cliente.Add(aux);
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

            return lista_Cliente;
        }
        public void InserirCliente(object aux)
        {
            Cliente cliente = aux as Cliente;
            if (cliente == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Cliente.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnomecliente", cliente.nomecliente);
            cmd.Parameters.AddWithValue("@pcodbairro", cliente.bairro.codbairro);
            cmd.Parameters.AddWithValue("@pcodrua", cliente.rua.codrua);
            cmd.Parameters.AddWithValue("@pcodcep", cliente.cep.codcep);
            cmd.Parameters.AddWithValue("@pcodcidade", cliente.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pcodestado", cliente.estado.codestado);
            cmd.Parameters.AddWithValue("@pcodpais", cliente.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumerocasa", cliente.numerocasa);
            cmd.Parameters.AddWithValue("@pcpf", cliente.cpf);
            cmd.Parameters.AddWithValue("@pfotocliente", cliente.fotocliente); // Campo de foto

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Cliente inserido com sucesso.");
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
        public void AtualizarCliente(object aux)
        {
            Cliente cliente = aux as Cliente;
            if (cliente == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Cliente.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@nomecliente", cliente.nomecliente);
            cmd.Parameters.AddWithValue("@pcodbairro", cliente.bairro.codbairro);
            cmd.Parameters.AddWithValue("@pcodrua", cliente.rua.codrua);
            cmd.Parameters.AddWithValue("@pcodcep", cliente.cep.codcep);
            cmd.Parameters.AddWithValue("@pcodcidade", cliente.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pcodestado", cliente.estado.codestado);
            cmd.Parameters.AddWithValue("@pcodpais", cliente.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumerocasa", cliente.numerocasa);
            cmd.Parameters.AddWithValue("@pcpf", cliente.cpf);
            cmd.Parameters.AddWithValue("@pcodcliente", cliente.codcliente); // Identificador do cliente para atualização
            cmd.Parameters.AddWithValue("@pfotocliente", cliente.fotocliente); // Campo de foto

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Cliente atualizado com sucesso.");
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



        public List<Cliente> DadosClienteFiltro(string filtro)
        {
            List<Cliente> lista_Cliente = new List<Cliente>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pnomecliente", filtro + "%");

            SqlDataReader dr_Cliente;
            conn.Open();

            try
            {
                dr_Cliente = cmd.ExecuteReader();
                while (dr_Cliente.Read())
                {
                    Rua rua1 = new Rua()
                    {
                        codrua = Int32.Parse(dr_Cliente["codruafk"].ToString()),
                        //nomerua = dr_Cliente["nomerua"].ToString()
                    };
                    Cidade cidade1 = new Cidade()
                    {
                        codcidade = Int32.Parse(dr_Cliente["codcidadefk"].ToString()),
                       // nomecidade = dr_Cliente["nomecidade"].ToString()
                    };
                    Cep cep1 = new Cep()
                    {
                        codcep = Int32.Parse(dr_Cliente["codcepfk"].ToString()),
                       // numerocep = dr_Cliente["numerocep"].ToString()
                    };
                    Bairro bairro1 = new Bairro()
                    {
                        codbairro = Int32.Parse(dr_Cliente["codbairrofk"].ToString()),
                       // nomebairro = dr_Cliente["nomebairro"].ToString()
                    };
                    Pais pais1 = new Pais()
                    {
                        codpais = Int32.Parse(dr_Cliente["codpaisfk"].ToString()),
                      //  nomepais = dr_Cliente["nomepais"].ToString()
                    };
                    Estado estado1 = new Estado()
                    {
                        codestado = Int32.Parse(dr_Cliente["codestadofk"].ToString()),
                        //nomeestado = dr_Cliente["nomeestado"].ToString()
                    };

                    Cliente aux = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_Cliente["codcliente"].ToString()),
                        nomecliente = dr_Cliente["nomecliente"].ToString(),
                        numerocasa = dr_Cliente["numerocasa"].ToString(),
                        cpf = dr_Cliente["cpf"].ToString(),
                        

                        rua = rua1,
                        cidade = cidade1,
                        cep = cep1,
                        bairro = bairro1,
                        pais = pais1,
                        estado = estado1,
                        fotocliente = dr_Cliente["fotocliente"] as byte[]
                    };

                    lista_Cliente.Add(aux);
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

            return lista_Cliente;
        }
    }
}