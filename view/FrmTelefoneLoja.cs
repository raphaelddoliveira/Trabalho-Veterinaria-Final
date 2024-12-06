using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmTelefoneLoja : Form
    {
        List<Loja> lojas = new List<Loja>();
        List<Telefone> telefones = new List<Telefone>();
        DataTable tabela_TelefoneLoja;
        bool novo = true;
        int posicao;
        List<Telefoneloja> lista_TelefoneLoja = new List<Telefoneloja>();

        public FrmTelefoneLoja()
        {
            InitializeComponent();
            PreencheComboLoja();
            PreencheComboTelefone();
            CarregaTabela();

            lista_TelefoneLoja = CarregaListaTelefoneLoja();

            if (lista_TelefoneLoja.Count > 0)
            {
                posicao = 0;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void PreencheComboLoja()
        {
            lojas = CarregaListaLojas();
            cmbLoja.DisplayMember = "nomeloja";
            cmbLoja.ValueMember = "codloja";
            cmbLoja.DataSource = lojas;
        }

        private void PreencheComboTelefone()
        {
            telefones = CarregaListaTelefones();
            cmbTelefone.DisplayMember = "numerotelefone";
            cmbTelefone.ValueMember = "codtelefone";
            cmbTelefone.DataSource = telefones;
        }

        private List<Loja> CarregaListaLojas()
        {
            List<Loja> lista = new List<Loja>();
            C_Loja cl = new C_Loja();
            DataTable dt = cl.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Loja loja = new Loja
                {
                    codloja = Convert.ToInt32(row["codloja"]),
                    nomeloja = row["nomeloja"].ToString()
                };

                lista.Add(loja);
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
            if (lista_TelefoneLoja.Count > 0 && posicao >= 0)
            {
                var telefoneLoja = lista_TelefoneLoja[posicao];
                cmbLoja.SelectedValue = telefoneLoja.loja.codloja;
                cmbTelefone.SelectedValue = telefoneLoja.telefone.codtelefone;
            }
        }

        private List<Telefoneloja> CarregaListaTelefoneLoja()
        {
            List<Telefoneloja> lista = new List<Telefoneloja>();
            C_TelefoneLoja ctl = new C_TelefoneLoja();
            DataTable dt = ctl.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Telefoneloja telefoneLoja = new Telefoneloja
                {
                    loja = new Loja { codloja = Convert.ToInt32(row["codlojafk"]) },
                    telefone = new Telefone { codtelefone = Convert.ToInt32(row["codtelefonefk"]) }
                };

                lista.Add(telefoneLoja);
            }

            return lista;
        }

        private void CarregaTabela()
        {
            C_TelefoneLoja ctl = new C_TelefoneLoja();
            tabela_TelefoneLoja = ctl.Buscar_Todos();
            dataGridView1.DataSource = tabela_TelefoneLoja;
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
            if (cmbLoja.SelectedValue == null || cmbTelefone.SelectedValue == null)
            {
                MessageBox.Show("Selecione uma loja e um telefone.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Telefoneloja telefoneLoja = new Telefoneloja
            {
                loja = new Loja { codloja = Convert.ToInt32(cmbLoja.SelectedValue) },
                telefone = new Telefone { codtelefone = Convert.ToInt32(cmbTelefone.SelectedValue) }
            };

            C_TelefoneLoja controle = new C_TelefoneLoja();

            try
            {
                if (novo)
                {
                    controle.Insere_Dados(telefoneLoja);
                }
                else
                {
                    controle.Atualizar_Dados(telefoneLoja);
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
            if (cmbLoja.SelectedValue == null)
            {
                MessageBox.Show("Selecione uma loja válida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int codLoja = Convert.ToInt32(cmbLoja.SelectedValue);
            C_TelefoneLoja controle = new C_TelefoneLoja();
            controle.Apaga_Dados(codLoja);
            CarregaTabela();
        }

        private void LimparCampos()
        {
            cmbLoja.SelectedIndex = -1;
            cmbTelefone.SelectedIndex = -1;
        }

        private void AtivaCampos()
        {
            cmbLoja.Enabled = true;
            cmbTelefone.Enabled = true;
        }

        private void DesativarCampos()
        {
            cmbLoja.Enabled = false;
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

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (lista_TelefoneLoja.Count == 0 || posicao < 0)
            {
                MessageBox.Show("Nenhum registro selecionado para editar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AtivaCampos();
            AtivaBotoes();
            novo = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            DesativarBotoes();
            DesativarCampos();
        }


        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_TelefoneLoja.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_TelefoneLoja = CarregaListaTelefoneLoja();
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
                lista_TelefoneLoja = CarregaListaTelefoneLoja();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_TelefoneLoja.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_TelefoneLoja = CarregaListaTelefoneLoja();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_TelefoneLoja.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_TelefoneLoja.Count - 1;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_TelefoneLoja = CarregaListaTelefoneLoja();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_TelefoneLoja cc = new C_TelefoneLoja();
            DataTable dt = new DataTable();
            dt = cc.Buscar_Filtro(txtBuscar.Text + "%");
            tabela_TelefoneLoja = dt;
            dataGridView1.DataSource = tabela_TelefoneLoja;

            lista_TelefoneLoja = CarregaListaTelefoneLoja();

            if (lista_TelefoneLoja.Count > 0)
            {
                posicao = 0;
                AtualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_TelefoneLoja = CarregaListaTelefoneLoja();
            }
        }
    }
}
