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
    internal class C_Loja : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_Loja;
        SqlDataAdapter da_Loja;

        private const string sqlTodos = "SELECT * FROM loja";
        private const string sqlFiltro = "SELECT * FROM loja WHERE nomeloja LIKE @pnomeloja";
        private const string sqlInsere = "INSERT INTO loja(nomeloja,codbairrofk,codruafk,codcepfk,codcidadefk,codestadofk,codpaisfk,numeroloja,cnpj) VALUES" +
            " (@pnomeloja,@pcodbairro,@pcodrua,@pcodcep,@pcodcidade,@pcodestado,@pcodpais,@pnumeroloja,@pcnpj)";
        private const string sqlAtualiza = "UPDATE loja SET nomeloja = @nomeloja, codbairrofk = @pcodbairro, codruafk = @pcodrua, codcepfk = @pcodcep, codcidadefk = @pcodcidade ," +
            " codestadofk = @pcodestado, codpaisfk = @pcodpais, numeroloja = @pnumeroloja, cnpj = @pcnpj  where codloja = @pcodloja";
        private const string sqlApaga = "DELETE FROM loja WHERE codloja = @pcodloja";
        private const string sqlTodosDataTable = "SELECT a.codloja, a.nomeloja, a.cnpj,  b.nomerua, a.numeroloja, c.nomebairro, d.nomecidade, e.numerocep, " +
            "f.nomeestado, g.nomepais FROM loja as a , rua as b, bairro as c, cidade as d, cep as e, estado as f, pais as g WHERE a.codruafk = b.codrua and  " +
            "a.codbairrofk = c.codbairro and a.codcidadefk = d.codcidade and a.codcepfk = e.codcep and a.codestadofk = f.codestado and a.codpaisfk = g.codpais;";
        private const string sqlTodosDataTableFiltro = "SELECT a.codloja, a.nomeloja, a.cnpj,  b.nomerua, a.numeroloja, c.nomebairro, d.nomecidade, e.numerocep, " +
            "f.nomeestado, g.nomepais from loja as a , rua as b, bairro as c, cidade as d, cep as e, estado as f, pais as g WHERE a.codruafk = b.codrua and  " +
            "a.codbairrofk = c.codbairro and a.codcidadefk = d.codcidade and a.codcepfk = e.codcep and a.codestadofk = f.codestado and a.codpaisfk = g.codpais and nomeloja LIKE @pnomeloja;";
        string sqlBuscar = "SELECT * FROM loja WHERE codloja = @pdcodloja";

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodloja", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Loja excluído com sucesso.");
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
            Loja loja = aux as Loja;
            if (loja == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Loja.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@nomeloja", loja.nomeloja);
            cmd.Parameters.AddWithValue("@pcodbairro", loja.bairro.codbairro);
            cmd.Parameters.AddWithValue("@pcodrua", loja.rua.codrua);
            cmd.Parameters.AddWithValue("@pcodcep", loja.cep.codcep);
            cmd.Parameters.AddWithValue("@pcodcidade", loja.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pcodestado", loja.estado.codestado);
            cmd.Parameters.AddWithValue("@pcodpais", loja.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumeroloja", loja.numeroloja);
            cmd.Parameters.AddWithValue("@pcnpj", loja.cnpj);
            cmd.Parameters.AddWithValue("@pcodloja", loja.codloja);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Loja atualizado com sucesso.");
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
            cmd.Parameters.AddWithValue("@pnomeloja", dados);

            conn.Open();

            da_Loja = new SqlDataAdapter(cmd);

            dt_Loja = new DataTable();
            da_Loja.Fill(dt_Loja);

            conn.Close();
            return dt_Loja;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pdcodloja", valor);

            conn.Open();

            SqlDataReader dr_loja;
            Loja loja = new Loja();
            try
            {
                dr_loja = cmd.ExecuteReader();
                if (dr_loja.Read())
                {
                    Rua rua = new Rua()
                    {
                        codrua = Int32.Parse(dr_loja["codruafk"].ToString())
                    };
                    Cidade cidade = new Cidade
                    {
                        codcidade = Int32.Parse(dr_loja["codcidadefk"].ToString())
                    };
                    Cep cep = new Cep
                    {
                        codcep = Int32.Parse(dr_loja["codcepfk"].ToString())
                    };
                    Bairro bairro = new Bairro
                    {
                        codbairro = Int32.Parse(dr_loja["codbairrofk"].ToString())
                    };
                    Pais pais = new Pais
                    {
                        codpais = Int32.Parse(dr_loja["codpaisfk"].ToString())
                    };
                    Estado estado = new Estado
                    {
                        codestado = Int32.Parse(dr_loja["codestadofk"].ToString())
                    };

                    loja = new Loja()
                    {
                        codloja = Int32.Parse(dr_loja["codloja"].ToString()),
                        nomeloja = dr_loja["nomeloja"].ToString(),
                        numeroloja = dr_loja["numeroloja"].ToString(),
                        cnpj = dr_loja["cnpj"].ToString(),
                        rua = rua,
                        cidade = cidade,
                        cep = cep,
                        bairro = bairro,
                        pais = pais,
                        estado = estado
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

            return loja;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodosDataTable, conn);

            conn.Open();

            da_Loja = new SqlDataAdapter(cmd);

            dt_Loja = new DataTable();
            da_Loja.Fill(dt_Loja);

            conn.Close();
            return dt_Loja;
        }

        public void Insere_Dados(object aux)
        {
            Loja loja = aux as Loja;
            if (loja == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo loja.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnomeloja", loja.nomeloja);
            cmd.Parameters.AddWithValue("@pcodbairro", loja.bairro.codbairro);
            cmd.Parameters.AddWithValue("@pcodrua", loja.rua.codrua);
            cmd.Parameters.AddWithValue("@pcodcep", loja.cep.codcep);
            cmd.Parameters.AddWithValue("@pcodcidade", loja.cidade.codcidade);
            cmd.Parameters.AddWithValue("@pcodestado", loja.estado.codestado);
            cmd.Parameters.AddWithValue("@pcodpais", loja.pais.codpais);
            cmd.Parameters.AddWithValue("@pnumeroloja", loja.numeroloja);
            cmd.Parameters.AddWithValue("@pcnpj", loja.cnpj);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("loja inserido com sucesso.");
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

        public List<Loja> DadosCidade()
        {
            List<Loja> lista_Loja = new List<Loja>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Loja;
            conn.Open();

            try
            {
                dr_Loja = cmd.ExecuteReader();
                while (dr_Loja.Read())
                {
                    Rua rua = new Rua
                    {
                        codrua = Int32.Parse(dr_Loja["codruafk"].ToString())
                    };
                    Cidade cidade = new Cidade()
                    {
                        codcidade = Int32.Parse(dr_Loja["codcidadefk"].ToString())
                    };
                    Cep cep = new Cep()
                    {
                        codcep = Int32.Parse(dr_Loja["codcepfk"].ToString())
                    };
                    Bairro bairro = new Bairro()
                    {
                        codbairro = Int32.Parse(dr_Loja["codbairrofk"].ToString())
                    };
                    Pais pais = new Pais()
                    {
                        codpais = Int32.Parse(dr_Loja["codpaisfk"].ToString())
                    };
                    Estado estado = new Estado()
                    {
                        codestado = Int32.Parse(dr_Loja["codestadofk"].ToString())
                    };

                    Loja aux = new Loja()
                    {
                        codloja = Int32.Parse(dr_Loja["codloja"].ToString()),
                        nomeloja = dr_Loja["nomeloja"].ToString(),
                        numeroloja = dr_Loja["numeroloja"].ToString(),
                        cnpj = dr_Loja["cnpj"].ToString(),
                        rua = rua,
                        cidade = cidade,
                        cep = cep,
                        bairro = bairro,
                        pais = pais,
                        estado = estado
                    };

                    lista_Loja.Add(aux);
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

            return lista_Loja;
        }

        public List<Loja> DadosCidadeFiltro(string filtro)
        {
            List<Loja> lista_Loja = new List<Loja>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pnomeloja", filtro + "%");

            SqlDataReader dr_Loja;
            conn.Open();

            try
            {
                dr_Loja = cmd.ExecuteReader();
                while (dr_Loja.Read())
                {
                    Rua rua = new Rua()
                    {
                        codrua = Int32.Parse(dr_Loja["codruafk"].ToString())
                    };
                    Cidade cidade = new Cidade()
                    {
                        codcidade = Int32.Parse(dr_Loja["codcidadefk"].ToString())
                    };
                    Cep cep = new Cep()
                    {
                        codcep = Int32.Parse(dr_Loja["codcepfk"].ToString())
                    };
                    Bairro bairro = new Bairro()
                    {
                        codbairro = Int32.Parse(dr_Loja["codbairrofk"].ToString())
                    };
                    Pais pais = new Pais()
                    {
                        codpais = Int32.Parse(dr_Loja["codpaisfk"].ToString())
                    };
                    Estado estado = new Estado()
                    {
                        codestado = Int32.Parse(dr_Loja["codestadofk"].ToString())
                    };

                    Loja aux = new Loja()
                    {
                        codloja = Int32.Parse(dr_Loja["codloja"].ToString()),
                        nomeloja = dr_Loja["nomeloja"].ToString(),
                        numeroloja = dr_Loja["numeroloja"].ToString(),
                        cnpj = dr_Loja["cnpj"].ToString(),
                        rua = rua,
                        cidade = cidade,
                        cep = cep,
                        bairro = bairro,
                        pais = pais,
                        estado = estado
                    };

                    lista_Loja.Add(aux);
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

            return lista_Loja;
        }
    }
}
