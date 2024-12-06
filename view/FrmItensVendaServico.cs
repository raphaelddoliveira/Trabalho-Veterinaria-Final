using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmItensVendaServico : Form
    {
        List<Tiposervico> tiposServico = new List<Tiposervico>();
        int posicao_tipoServico;
        int codTipoServico;

        List<VendaServico> vendasServicos = new List<VendaServico>();
        int posicao_vendaServico;
        int codVendaServico;

        List<CidAnimal> animais = new List<CidAnimal>();
        int posicao_animal;
        int codAnimal;

        DataTable Tabela_ItensVendaServico;
        Boolean novo = true;
        int posicao;
        List<ItensVendaServico> lista_ItensVendaServico = new List<ItensVendaServico>();

        public FrmItensVendaServico()
        {
            InitializeComponent();
            preencheComboTipoServico();
            preencheComboVendaServico();
            preencheComboAnimal();

            CarregaTabela();

            lista_ItensVendaServico = carregaListaItensVendaServico();

            if (lista_ItensVendaServico.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        List<ItensVendaServico> carregaListaItensVendaServicoFiltro()
        {
            List<ItensVendaServico> lista = new List<ItensVendaServico>();

            C_ItensVendaServico cc = new C_ItensVendaServico();
            lista = cc.DadosItensVendaServicoFiltro(txtBuscar.Text);

            return lista;
        }

        private void preencheComboTipoServico()
        {
            tiposServico = carregaListaTipoServico();

            foreach (Tiposervico tipo in tiposServico)
            {
                cmbTiposervico.Items.Add(tipo.nometiposervico);
            }
        }

        private void preencheComboVendaServico()
        {
            vendasServicos = carregaListaVendaServico();

            foreach (VendaServico venda in vendasServicos)
            {
                cmbVendaservico.Items.Add(venda.codvendaservico);
            }
        }

        private void preencheComboAnimal()
        {
            animais = carregaListaAnimal();

            foreach (CidAnimal animal in animais)
            {
                cmbCidanimal.Items.Add(animal.nomecidanimal);
            }
        }

        List<Tiposervico> carregaListaTipoServico()
        {
            List<Tiposervico> lista = new List<Tiposervico>();

            C_TipoServico ct = new C_TipoServico();
            DataTable dt = ct.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Tiposervico tipo = new Tiposervico
                {
                    codtiposervico = Convert.ToInt32(row["codTipoServico"]),
                    nometiposervico = row["nomeTipoServico"].ToString()
                };

                lista.Add(tipo);
            }

            return lista;
        }

        List<VendaServico> carregaListaVendaServico()
        {
            List<VendaServico> lista = new List<VendaServico>();

            C_VendaServico cv = new C_VendaServico();
            DataTable dt = cv.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                VendaServico venda = new VendaServico
                {
                    codvendaservico = Convert.ToInt32(row["codVendaServico"]),
                };

                lista.Add(venda);
            }

            return lista;
        }

        List<CidAnimal> carregaListaAnimal()
        {
            List<CidAnimal> lista = new List<CidAnimal>();

            C_Cidanimal ca = new C_Cidanimal();
            DataTable dt = ca.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                CidAnimal animal = new CidAnimal
                {
                    codcidanimal = Convert.ToInt32(row["codCidAnimal"]),
                    nomecidanimal = row["nomeCidAnimal"].ToString()
                };

                lista.Add(animal);
            }

            return lista;
        }

        private void atualizaCampos()
        {
            txtQuant.Text = lista_ItensVendaServico[posicao].quant.ToString();
            txtValor.Text = lista_ItensVendaServico[posicao].valor.ToString();

            for (int i = 0; i < tiposServico.Count; i++)
            {
                if (tiposServico[i].codtiposervico == lista_ItensVendaServico[posicao].tipoServico.codtiposervico)
                {
                    cmbTiposervico.SelectedItem = tiposServico[i].nometiposervico;
                    break;
                }
            }

            for (int i = 0; i < vendasServicos.Count; i++)
            {
                if (vendasServicos[i].codvendaservico == lista_ItensVendaServico[posicao].vendaServico.codvendaservico)
                {
                    cmbVendaservico.SelectedItem = vendasServicos[i].codvendaservico;
                    break;
                }
            }

            for (int i = 0; i < animais.Count; i++)
            {
                if (animais[i].codcidanimal == lista_ItensVendaServico[posicao].cidAnimal.codcidanimal)
                {
                    cmbCidanimal.SelectedItem = animais[i].nomecidanimal;
                    break;
                }
            }
        }

        List<ItensVendaServico> carregaListaItensVendaServico()
        {
            List<ItensVendaServico> lista = new List<ItensVendaServico>();

            C_ItensVendaServico cvs = new C_ItensVendaServico();
            lista = cvs.DadosItensVendaServico();

            return lista;
        }

        public void CarregaTabela()
        {
            C_ItensVendaServico cv = new C_ItensVendaServico();
            DataTable dt = new DataTable();
            dt = cv.Buscar_Todos();
            Tabela_ItensVendaServico = dt;
            dataGridView1.DataSource = Tabela_ItensVendaServico;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                DataGridViewRow dr = dataGridView1.Rows[index];

                txtQuant.Text = dr.Cells[1].Value.ToString();
                txtValor.Text = dr.Cells[2].Value.ToString();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            ativarCampos();
            AtivaBotoes();
            novo = true;
        }

        private void AtivaBotoes()
        {
            btnNovo.Enabled = false;
            btnApagar.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void ativarCampos()
        {
            txtQuant.Enabled = true;

            txtValor.Enabled = true;
            cmbTiposervico.Enabled = true;
            cmbVendaservico.Enabled = true;
            cmbCidanimal.Enabled = true;
        }

        private void limparCampos()
        {
            txtQuant.Text = "";
            txtValor.Text = "";
            cmbTiposervico.SelectedIndex = -1;
            cmbVendaservico.SelectedIndex = -1;
            cmbCidanimal.SelectedIndex = -1;
        }

        private void desativaCampos()
        {
            txtQuant.Enabled = false;
            txtValor.Enabled = false;
            cmbTiposervico.Enabled = false;
            cmbVendaservico.Enabled = false;
            cmbCidanimal.Enabled = false;
        }

        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btnApagar_Click_1(object sender, EventArgs e)
        {
            C_ItensVendaServico cp = new C_ItensVendaServico();

            if (cmbVendaservico.Text != "")
            {
                int valor = Int32.Parse(cmbVendaservico.Text);
                cp.Apaga_Dados(valor);
                CarregaTabela();
            }
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            AtivaBotoes();
            ativarCampos();
            novo = false;
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuant.Text) || string.IsNullOrEmpty(txtValor.Text))
            {
                MessageBox.Show("Quantidade e valor são obrigatórios.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ItensVendaServico item = new ItensVendaServico()
            {
                quant = Convert.ToDecimal(txtQuant.Text),
                valor = Convert.ToDecimal(txtValor.Text),
                tipoServico = tiposServico[cmbTiposervico.SelectedIndex],
                vendaServico = vendasServicos[cmbVendaservico.SelectedIndex],
                cidAnimal = animais[cmbCidanimal.SelectedIndex]
            };

            C_ItensVendaServico ccv = new C_ItensVendaServico();

            if (novo)
            {
                ccv.Insere_Dados(item);
            }
            else
            {
                ccv.Atualizar_Dados(item);
            }

            CarregaTabela();

            desativaCampos();

            desativaBotoes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lista_ItensVendaServico = carregaListaItensVendaServicoFiltro();
            dataGridView1.DataSource = lista_ItensVendaServico;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_ItensVendaServico.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_ItensVendaServico = carregaListaItensVendaServico();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao--;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_ItensVendaServico = carregaListaItensVendaServico();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_ItensVendaServico.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_ItensVendaServico = carregaListaItensVendaServico();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_ItensVendaServico.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_ItensVendaServico.Count - 1;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_ItensVendaServico = carregaListaItensVendaServico();
            }
        }
        public void CarregaTabelaFiltro(String filtro)
        {
            C_ItensVendaServico cf = new C_ItensVendaServico();
            DataTable dt = new DataTable();
            dt = cf.Buscar_Filtro(filtro);
            Tabela_ItensVendaServico = dt;
            dataGridView1.DataSource = Tabela_ItensVendaServico;
        }
        
        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            lista_ItensVendaServico = carregaListaItensVendaServicoFiltro();
            CarregaTabelaFiltro(txtBuscar.Text);

            if (lista_ItensVendaServico.Count > 0)
            {

                posicao = 0;
                atualizaCampos();
            }
            else
            {
                dataGridView1.DataSource = null;
                limparCampos();
            }
        }
    }
}
