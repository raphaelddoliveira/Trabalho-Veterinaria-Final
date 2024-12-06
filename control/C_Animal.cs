using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Animal : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_Animal;
        SqlDataAdapter da_Animal;

        private const string sqlTodos = "SELECT * FROM animal";
        private const string sqlFiltro = "SELECT * FROM animal WHERE nomeanimal LIKE @pnomeanimal";
        private const string sqlInsere = "INSERT INTO animal(nomeanimal, codsexofk, codracafk, codtipoanimalfk, codclientefk) VALUES (@pnomeanimal, @pcodsexo, @pcodraca, @pcodtipoanimal, @pcodcliente)";
        private const string sqlAtualiza = "UPDATE animal SET nomeanimal = @pnomeanimal, codsexofk = @pcodsexo, codracafk = @pcodraca, codtipoanimalfk = @pcodtipoanimal, codclientefk = @pcodcliente WHERE codanimal = @pcodanimal";
        private const string sqlApaga = "DELETE FROM animal WHERE codanimal = @pcodanimal";
        private const string sqlBuscar = "SELECT * FROM animal WHERE codanimal = @pcodanimal";
        private const string sqlTodosDataTable = "SELECT a.codanimal, a.nomeanimal, s.nomesexo, r.nomeraca, t.nometipoanimal, c.nomecliente FROM animal a, sexo s, raca r, tipoanimal t, cliente c WHERE a.codsexofk = s.codsexo AND a.codracafk = r.codraca AND a.codtipoanimalfk = t.codtipoanimal AND a.codclientefk = c.codcliente";

        public void Apaga_Dados(int aux)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodanimal", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Animal excluído com sucesso.");
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
            Animal animal = aux as Animal;
            if (animal == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Animal.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pnomeanimal", animal.nomeanimal);
            cmd.Parameters.AddWithValue("@pcodsexo", animal.sexo.codsexo);
            cmd.Parameters.AddWithValue("@pcodraca", animal.raca.codraca);
            cmd.Parameters.AddWithValue("@pcodtipoanimal", animal.tipoanimal.codtipoanimal);
            cmd.Parameters.AddWithValue("@pcodcliente", animal.cliente.codcliente);
            cmd.Parameters.AddWithValue("@pcodanimal", animal.codanimal);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Animal atualizado com sucesso.");
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
            cmd.Parameters.AddWithValue("@pnomeanimal", dados + "%");

            conn.Open();

            da_Animal = new SqlDataAdapter(cmd);
            dt_Animal = new DataTable();
            da_Animal.Fill(dt_Animal);

            conn.Close();
            return dt_Animal;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodanimal", valor);

            conn.Open();

            SqlDataReader dr_animal;
            Animal animal = new Animal();
            try
            {
                dr_animal = cmd.ExecuteReader();
                if (dr_animal.Read())
                {
                    Sexo sexo = new Sexo()
                    {
                        codsexo = Int32.Parse(dr_animal["codsexofk"].ToString())
                    };
                    Raca raca = new Raca()
                    {
                        codraca = Int32.Parse(dr_animal["codracafk"].ToString())
                    };
                    Tipoanimal tipoanimal = new Tipoanimal()
                    {
                        codtipoanimal = Int32.Parse(dr_animal["codtipoanimalfk"].ToString())
                    };
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_animal["codclientefk"].ToString())
                    };

                    animal = new Animal()
                    {
                        codanimal = Int32.Parse(dr_animal["codanimal"].ToString()),
                        nomeanimal = dr_animal["nomeanimal"].ToString(),
                        sexo = sexo,
                        raca = raca,
                        tipoanimal = tipoanimal,
                        cliente = cliente
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

            return animal;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodosDataTable, conn);

            conn.Open();

            da_Animal = new SqlDataAdapter(cmd);
            dt_Animal = new DataTable();
            da_Animal.Fill(dt_Animal);

            conn.Close();
            return dt_Animal;
        }

        public void Insere_Dados(object aux)
        {
            Animal animal = aux as Animal;
            if (animal == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo Animal.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnomeanimal", animal.nomeanimal);
            cmd.Parameters.AddWithValue("@pcodsexo", animal.sexo.codsexo);
            cmd.Parameters.AddWithValue("@pcodraca", animal.raca.codraca);
            cmd.Parameters.AddWithValue("@pcodtipoanimal", animal.tipoanimal.codtipoanimal);
            cmd.Parameters.AddWithValue("@pcodcliente", animal.cliente.codcliente);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Animal inserido com sucesso.");
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

        public List<Animal> DadosAnimal()
        {
            List<Animal> lista_Animal = new List<Animal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Animal;
            conn.Open();

            try
            {
                dr_Animal = cmd.ExecuteReader();
                while (dr_Animal.Read())
                {
                    Sexo sexo = new Sexo()
                    {
                        codsexo = Int32.Parse(dr_Animal["codsexofk"].ToString())
                    };
                    Raca raca = new Raca()
                    {
                        codraca = Int32.Parse(dr_Animal["codracafk"].ToString())
                    };
                    Tipoanimal tipoanimal = new Tipoanimal()
                    {
                        codtipoanimal = Int32.Parse(dr_Animal["codtipoanimalfk"].ToString())
                    };
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_Animal["codclientefk"].ToString())
                    };

                    Animal aux = new Animal()
                    {
                        codanimal = Int32.Parse(dr_Animal["codanimal"].ToString()),
                        nomeanimal = dr_Animal["nomeanimal"].ToString(),
                        sexo = sexo,
                        raca = raca,
                        tipoanimal = tipoanimal,
                        cliente = cliente
                    };

                    lista_Animal.Add(aux);
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

            return lista_Animal;
        }

        public List<Animal> DadosAnimalFiltro(string filtro)
        {
            List<Animal> lista_Animal = new List<Animal>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pnomeanimal", filtro + "%");

            SqlDataReader dr_Animal;
            conn.Open();

            try
            {
                dr_Animal = cmd.ExecuteReader();
                while (dr_Animal.Read())
                {
                    Sexo sexo = new Sexo()
                    {
                        codsexo = Int32.Parse(dr_Animal["codsexofk"].ToString())
                    };
                    Raca raca = new Raca()
                    {
                        codraca = Int32.Parse(dr_Animal["codracafk"].ToString())
                    };
                    Tipoanimal tipoanimal = new Tipoanimal()
                    {
                        codtipoanimal = Int32.Parse(dr_Animal["codtipoanimalfk"].ToString())
                    };
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_Animal["codclientefk"].ToString())
                    };

                    Animal aux = new Animal()
                    {
                        codanimal = Int32.Parse(dr_Animal["codanimal"].ToString()),
                        nomeanimal = dr_Animal["nomeanimal"].ToString(),
                        sexo = sexo,
                        raca = raca,
                        tipoanimal = tipoanimal,
                        cliente = cliente
                    };

                    lista_Animal.Add(aux);
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

            return lista_Animal;
        }
    }
}
