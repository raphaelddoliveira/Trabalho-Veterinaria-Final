using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmClienteTelefone : Form
    {
        List<Cliente> clientes = new List<Cliente>();
        List<Telefone> telefones = new List<Telefone>();
        DataTable tabela_ClienteTelefone;
        bool novo = true;
        int posicao;
        List<Clientetelefone> lista_ClienteTelefone = new List<Clientetelefone>();

        public FrmClienteTelefone()
        {
            InitializeComponent();
            PreencheComboCliente();
            PreencheComboTelefone();
            CarregaTabela();

            lista_ClienteTelefone = CarregaListaClienteTelefone();

            if (lista_ClienteTelefone.Count > 0)
            {
                posicao = 0;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }
        public void CarregaTabelaFiltro(String filtro)
        {
            C_ClienteTelefone cf = new C_ClienteTelefone();
            DataTable dt = new DataTable();
            dt = cf.Buscar_Filtro(filtro);
            tabela_ClienteTelefone = dt;
            dataGridView1.DataSource = tabela_ClienteTelefone;
        }

        private void PreencheComboCliente()
        {
            clientes = CarregaListaClientes();
            cmbCliente.DisplayMember = "nomecliente";
            cmbCliente.ValueMember = "codcliente";
            cmbCliente.DataSource = clientes;
        }

        private void PreencheComboTelefone()
        {
            telefones = CarregaListaTelefones();
            cmbTelefone.DisplayMember = "numerotelefone";
            cmbTelefone.ValueMember = "codtelefone";
            cmbTelefone.DataSource = telefones;
        }


        private List<Cliente> CarregaListaClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            C_Cliente cc = new C_Cliente();
            DataTable dt = cc.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Cliente cliente = new Cliente
                {
                    codcliente = Convert.ToInt32(row["codcliente"]),
                    nomecliente = row["nomecliente"].ToString()
                };

                lista.Add(cliente);
            }

            return lista;
        }

        private List<Telefone> CarregaListaTelefones()
        {
            List<Telefone> lista = new List<Telefone>();
            C_Telefone ct = new C_Telefone();
            DataTable dt = ct.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Telefone telefone = new Telefone
                {
                    codtelefone = Convert.ToInt32(row["codtelefone"]),
                    numerotelefone = row["numerotelefone"].ToString()
                };

                lista.Add(telefone);
            }

            return lista;
        }

        private void AtualizaCampos()
        {
            if (lista_ClienteTelefone.Count > 0 && posicao >= 0)
            {
                var clienteTelefone = lista_ClienteTelefone[posicao];
                cmbCliente.SelectedValue = clienteTelefone.cliente.codcliente;
                cmbTelefone.SelectedValue = clienteTelefone.telefone.codtelefone;
            }
        }



        private List<Clientetelefone> CarregaListaClienteTelefone()
        {
            List<Clientetelefone> lista = new List<Clientetelefone>();
            C_ClienteTelefone cc = new C_ClienteTelefone();
            DataTable dt = cc.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Clientetelefone clienteTelefone = new Clientetelefone
                {
                    cliente = new Cliente { codcliente = Convert.ToInt32(row["codclientefk"]) },
                    telefone = new Telefone { codtelefone = Convert.ToInt32(row["codtelefonefk"]) }
                };

                lista.Add(clienteTelefone);
            }

            return lista;
        }

        public void CarregaTabela()
        {
            C_ClienteTelefone cc = new C_ClienteTelefone();
            tabela_ClienteTelefone = cc.Buscar_Todos();
            dataGridView1.DataSource = tabela_ClienteTelefone;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
            AtivaCampos();
            AtivaBotoes();
            novo = true;
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedValue == null || cmbTelefone.SelectedValue == null)
            {
                MessageBox.Show("Selecione um cliente e um telefone.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Clientetelefone clienteTelefone = new Clientetelefone
            {
                cliente = new Cliente { codcliente = Convert.ToInt32(cmbCliente.SelectedValue) },
                telefone = new Telefone { codtelefone = Convert.ToInt32(cmbTelefone.SelectedValue) }
            };

            C_ClienteTelefone controle = new C_ClienteTelefone();

            try
            {
                if (novo)
                {
                    controle.Insere_Dados(clienteTelefone);
                }
                else
                {
                    controle.Atualizar_Dados(clienteTelefone);
                    MessageBox.Show("Registro editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CarregaTabela();
                LimparCampos();
                DesativarCampos();
                DesativarBotoes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar/editar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnApagar_Click_1(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Selecione um cliente válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int codCliente = Convert.ToInt32(cmbCliente.SelectedValue);
            C_ClienteTelefone controle = new C_ClienteTelefone();
            controle.Apaga_Dados(codCliente);
            CarregaTabela();
        }






        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            LimparCampos();
            AtivaBotoes();
        }

        private void LimparCampos()
        {
            cmbCliente.SelectedIndex = -1;
            cmbTelefone.SelectedIndex = -1;
        }

        private void AtivaCampos()
        {
            cmbCliente.Enabled = true;
            cmbTelefone.Enabled = true;
        }

        private void DesativarCampos()
        {
            cmbCliente.Enabled = false;
            cmbTelefone.Enabled = false;
        }

        private void AtivaBotoes()
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnApagar.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void DesativarBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lista_ClienteTelefone.Count == 0 || posicao < 0)
            {
                MessageBox.Show("Nenhum registro selecionado para editar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AtivaCampos();
            AtivaBotoes();
            novo = false; 
        }

        List<Clientetelefone> carregaListaClientetelefone()
        {
            List<Clientetelefone> lista = new List<Clientetelefone>();

            C_ClienteTelefone cp = new C_ClienteTelefone();
            lista = cp.DadosClientetelefone();

            return lista;
        }
        List<Clientetelefone> carregaListaClienteTelefoneFiltro()
        {
            List<Clientetelefone> lista = new List<Clientetelefone>();

            C_ClienteTelefone cc = new C_ClienteTelefone();
            lista = cc.DadosClienteTelefoneFiltro(txtBuscar.Text);

            return lista;
        }
        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_ClienteTelefone.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_ClienteTelefone = carregaListaClientetelefone();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao--;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_ClienteTelefone = carregaListaClientetelefone();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_ClienteTelefone.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_ClienteTelefone = carregaListaClientetelefone();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_ClienteTelefone.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_ClienteTelefone.Count - 1;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_ClienteTelefone = carregaListaClientetelefone();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lista_ClienteTelefone = carregaListaClienteTelefoneFiltro();
            CarregaTabelaFiltro(txtBuscar.Text);

            if (lista_ClienteTelefone.Count > 0)
            {

                posicao = 0;
                AtualizaCampos();
            }
            else
            {
                dataGridView1.DataSource = null;
                LimparCampos();
            }
        }
    }
}
