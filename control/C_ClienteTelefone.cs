using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_ClienteTelefone : I_Metodos_Comuns
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_ClienteTelefone;
        SqlDataAdapter da_ClienteTelefone;

        private const string sqlTodos = "SELECT * FROM clientetelefone";
        private const string sqlFiltro = "SELECT * FROM clientetelefone WHERE codtelefonefk = @pcodtelefonefk";
        private const string sqlInsere = "INSERT INTO clientetelefone(codclientefk, codtelefonefk) VALUES (@pcodclientefk, @pcodtelefonefk)";
        private const string sqlAtualiza = "UPDATE clientetelefone SET codtelefonefk = @pcodtelefonefk, codclientefk = @pcodclientefk WHERE codclientefk = @pcodclientefk AND codtelefonefk = @pcodtelefonefk";
        private const string sqlApaga = "DELETE FROM clientetelefone WHERE codclientefk = @pcodclientefk";
        private const string sqlBuscar = "SELECT * FROM clientetelefone WHERE codtelefonefk = @pcodtelefonefk";
       

        public void Apaga_Dados(int aux)
        {

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcodclientefk", aux);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Telefone do cliente excluído com sucesso.");
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
            Clientetelefone clienteTelefone = aux as Clientetelefone;
            if (clienteTelefone == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo ClienteTelefone.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", clienteTelefone.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@pcodclientefk", clienteTelefone.cliente.codcliente);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Telefone do cliente atualizado com sucesso.");
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
            cmd.Parameters.AddWithValue("@pcodtelefonefk", dados + "%");

            conn.Open();

            da_ClienteTelefone = new SqlDataAdapter(cmd);
            dt_ClienteTelefone = new DataTable();
            da_ClienteTelefone.Fill(dt_ClienteTelefone);

            conn.Close();
            return dt_ClienteTelefone;
        }

        public object Buscar_Id(int valor)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlBuscar, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", valor);

            conn.Open();

            SqlDataReader dr_ClienteTelefone;
            Clientetelefone clienteTelefone = new Clientetelefone();
            try
            {
                dr_ClienteTelefone = cmd.ExecuteReader();
                if (dr_ClienteTelefone.Read())
                {
                    clienteTelefone = new Clientetelefone()
                    {
                        telefone = new Telefone()
                        {
                            codtelefone = Int32.Parse(dr_ClienteTelefone["codtelefonefk"].ToString())
                        },
                        cliente = new Cliente()
                        {
                            codcliente = Int32.Parse(dr_ClienteTelefone["codclientefk"].ToString())
                        }
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

            return clienteTelefone;
        }

        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();

            da_ClienteTelefone = new SqlDataAdapter(cmd);
            dt_ClienteTelefone = new DataTable();
            da_ClienteTelefone.Fill(dt_ClienteTelefone);

            conn.Close();
            return dt_ClienteTelefone;
        }

        public void Insere_Dados(object aux)
        {
            Clientetelefone clienteTelefone = aux as Clientetelefone;
            if (clienteTelefone == null)
            {
                throw new ArgumentException("O objeto fornecido não é do tipo ClienteTelefone.");
            }

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pcodtelefonefk", clienteTelefone.telefone.codtelefone);
            cmd.Parameters.AddWithValue("@pcodclientefk", clienteTelefone.cliente.codcliente);

            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Telefone do cliente inserido com sucesso.");
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
        public List<Clientetelefone> DadosClientetelefone()
        {
            List<Clientetelefone> lista_Clientetelefone = new List<Clientetelefone>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_Clientetelefone;
            conn.Open();

            try
            {
                dr_Clientetelefone = cmd.ExecuteReader();
                while (dr_Clientetelefone.Read())
                {
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_Clientetelefone["codclientefk"].ToString())
                    };
                    Telefone telefone = new Telefone()
                    {
                        codtelefone = Int32.Parse(dr_Clientetelefone["codtelefonefk"].ToString())
                    };

                    Clientetelefone aux = new Clientetelefone()
                    {
                        cliente = cliente,
                        telefone = telefone
                    };

                    lista_Clientetelefone.Add(aux);
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

            return lista_Clientetelefone;
        }
        public List<Clientetelefone> DadosClienteTelefoneFiltro(string filtro)
        {
            List<Clientetelefone> lista_Clientetelefone = new List<Clientetelefone>();

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("@pcodclientefk", filtro + "%");

            SqlDataReader dr_Clientetelefone;
            conn.Open();
            try
            {
                dr_Clientetelefone = cmd.ExecuteReader();
                while (dr_Clientetelefone.Read())
                {
                    Cliente cliente = new Cliente()
                    {
                        codcliente = Int32.Parse(dr_Clientetelefone["codclientefk"].ToString())
                    };
                    Telefone telefone = new Telefone()
                    {
                        codtelefone = Int32.Parse(dr_Clientetelefone["codtelefonefk"].ToString())
                    };

                    Clientetelefone aux = new Clientetelefone()
                    {
                        cliente = cliente,
                        telefone = telefone
                    };

                    lista_Clientetelefone.Add(aux);
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

            return lista_Clientetelefone;
        }
    }
}
