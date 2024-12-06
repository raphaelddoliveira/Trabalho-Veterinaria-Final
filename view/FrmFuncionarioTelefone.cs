using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmFuncionarioTelefone : Form
    {
        List<Funcionario> funcionarios = new List<Funcionario>();
        List<Telefone> telefones = new List<Telefone>();
        DataTable tabela_FuncionarioTelefone;
        bool novo = true;
        int posicao;
        List<Funcionariotelefone> lista_FuncionarioTelefone = new List<Funcionariotelefone>();

        public FrmFuncionarioTelefone()
        {
            InitializeComponent();
            PreencheComboFuncionario();
            PreencheComboTelefone();
            CarregaTabela();

            lista_FuncionarioTelefone = CarregaListaFuncionarioTelefone();

            if (lista_FuncionarioTelefone.Count > 0)
            {
                posicao = 0;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void PreencheComboFuncionario()
        {
            funcionarios = CarregaListaFuncionarios();
            cmbFuncionario.DisplayMember = "nomefuncionario";
            cmbFuncionario.ValueMember = "codfuncionario";
            cmbFuncionario.DataSource = funcionarios;
        }

        private void PreencheComboTelefone()
        {
            telefones = CarregaListaTelefones();
            cmbTelefone.DisplayMember = "numerotelefone";
            cmbTelefone.ValueMember = "codtelefone";
            cmbTelefone.DataSource = telefones;
        }

        private List<Funcionario> CarregaListaFuncionarios()
        {
            List<Funcionario> lista = new List<Funcionario>();
            C_Funcionario cf = new C_Funcionario();
            DataTable dt = cf.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Funcionario funcionario = new Funcionario
                {
                    codfuncionario = Convert.ToInt32(row["codfuncionario"]),
                    nomefuncionario = row["nomefuncionario"].ToString()
                };

                lista.Add(funcionario);
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
            if (lista_FuncionarioTelefone.Count > 0 && posicao >= 0)
            {
                var funcionarioTelefone = lista_FuncionarioTelefone[posicao];
                cmbFuncionario.SelectedValue = funcionarioTelefone.funcionario.codfuncionario;
                cmbTelefone.SelectedValue = funcionarioTelefone.telefone.codtelefone;
            }
        }

        private List<Funcionariotelefone> CarregaListaFuncionarioTelefone()
        {
            List<Funcionariotelefone> lista = new List<Funcionariotelefone>();
            C_FuncionarioTelefone cft = new C_FuncionarioTelefone();
            DataTable dt = cft.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Funcionariotelefone funcionarioTelefone = new Funcionariotelefone
                {
                    funcionario = new Funcionario { codfuncionario = Convert.ToInt32(row["codfuncionariofk"]) },
                    telefone = new Telefone { codtelefone = Convert.ToInt32(row["codtelefonefk"]) }
                };

                lista.Add(funcionarioTelefone);
            }

            return lista;
        }

        private void CarregaTabela()
        {
            C_FuncionarioTelefone cft = new C_FuncionarioTelefone();
            tabela_FuncionarioTelefone = cft.Buscar_Todos();
            dataGridView1.DataSource = tabela_FuncionarioTelefone;
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
            if (cmbFuncionario.SelectedValue == null || cmbTelefone.SelectedValue == null)
            {
                MessageBox.Show("Selecione um funcionário e um telefone.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Funcionariotelefone funcionarioTelefone = new Funcionariotelefone
            {
                funcionario = new Funcionario { codfuncionario = Convert.ToInt32(cmbFuncionario.SelectedValue) },
                telefone = new Telefone { codtelefone = Convert.ToInt32(cmbTelefone.SelectedValue) }
            };

            C_FuncionarioTelefone controle = new C_FuncionarioTelefone();

            try
            {
                if (novo)
                {
                    controle.Insere_Dados(funcionarioTelefone);
                }
                else
                {
                    controle.Atualizar_Dados(funcionarioTelefone);
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
            if (cmbFuncionario.SelectedValue == null)
            {
                MessageBox.Show("Selecione um funcionário válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int codFuncionario = Convert.ToInt32(cmbFuncionario.SelectedValue);
            C_FuncionarioTelefone controle = new C_FuncionarioTelefone();
            controle.Apaga_Dados(codFuncionario);
            CarregaTabela();
        }

        private void LimparCampos()
        {
            cmbFuncionario.SelectedIndex = -1;
            cmbTelefone.SelectedIndex = -1;
        }

        private void AtivaCampos()
        {
            cmbFuncionario.Enabled = true;
            cmbTelefone.Enabled = true;
        }

        private void DesativarCampos()
        {
            cmbFuncionario.Enabled = false;
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

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_FuncionarioTelefone.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_FuncionarioTelefone = CarregaListaFuncionarioTelefone();
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
                lista_FuncionarioTelefone = CarregaListaFuncionarioTelefone();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_FuncionarioTelefone.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_FuncionarioTelefone = CarregaListaFuncionarioTelefone();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_FuncionarioTelefone.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_FuncionarioTelefone.Count - 1;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_FuncionarioTelefone = CarregaListaFuncionarioTelefone();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lista_FuncionarioTelefone.Count == 0 || posicao < 0)
            {
                MessageBox.Show("Nenhum registro selecionado para editar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AtivaCampos();
            AtivaBotoes();
            novo = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_FuncionarioTelefone cc = new C_FuncionarioTelefone();
            DataTable dt = new DataTable();
            dt = cc.Buscar_Filtro(txtBuscar.Text + "%");
            tabela_FuncionarioTelefone = dt;
            dataGridView1.DataSource = tabela_FuncionarioTelefone;

            lista_FuncionarioTelefone = CarregaListaFuncionarioTelefone();

            if (lista_FuncionarioTelefone.Count > 0)
            {
                posicao = 0;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_FuncionarioTelefone = CarregaListaFuncionarioTelefone();
            }
        }
    }
}
