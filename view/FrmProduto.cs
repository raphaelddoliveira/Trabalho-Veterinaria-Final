using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmProduto : Form
    {
        List<Tipoproduto> tiposProduto = new List<Tipoproduto>();
        int posicao_tipoProduto;
        int codTipoProduto;

        List<Marca> marcas = new List<Marca>();
        int posicao_marca;
        int codMarca;

        DataTable Tabela_Produto;
        Boolean novo = true;
        int posicao;
        List<Produto> lista_Produto = new List<Produto>();

        public FrmProduto()
        {
            InitializeComponent();
            preencheComboTipoProduto();
            preencheComboMarca();

            CarregaTabela();

            lista_Produto = carregaListaProduto();

            if (lista_Produto.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }
        List<Produto> carregaListaProdutoFiltro()
        {
            List<Produto> lista = new List<Produto>();

            C_Produto cc = new C_Produto();
            lista = cc.DadosProdutoFiltro(txtBuscar.Text);

            return lista;
        }

        private void preencheComboTipoProduto()
        {
            tiposProduto = carregaListaTipoProduto();

            foreach (Tipoproduto tipo in tiposProduto)
            {
                cmbTipoProduto.Items.Add(tipo.nometipoproduto);
            }
        }

        private void preencheComboMarca()
        {
            marcas = carregaListaMarca();

            foreach (Marca marca in marcas)
            {
                cmbMarca.Items.Add(marca.nomemarca);
            }
        }

        List<Tipoproduto> carregaListaTipoProduto()
        {
            List<Tipoproduto> lista = new List<Tipoproduto>();

            C_TipoProduto ct = new C_TipoProduto();
            DataTable dt = ct.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Tipoproduto tipo = new Tipoproduto
                {
                    codtipoproduto = Convert.ToInt32(row["codTipoProduto"]),
                    nometipoproduto = row["nomeTipoProduto"].ToString()
                };

                lista.Add(tipo);
            }

            return lista;
        }

        List<Marca> carregaListaMarca()
        {
            List<Marca> lista = new List<Marca>();

            C_Marca cm = new C_Marca();
            DataTable dt = cm.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Marca marca = new Marca
                {
                    codmarca = Convert.ToInt32(row["codMarca"]),
                    nomemarca = row["nomeMarca"].ToString()
                };

                lista.Add(marca);
            }

            return lista;
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_Produto[posicao].codproduto.ToString();
            txtNomeproduto.Text = lista_Produto[posicao].nomeproduto;
            txtQuant.Text = lista_Produto[posicao].quantidade.ToString();
            txtValor.Text = lista_Produto[posicao].valor.ToString();

            for (int i = 0; i < tiposProduto.Count; i++)
            {
                if (tiposProduto[i].codtipoproduto == lista_Produto[posicao].tipoproduto.codtipoproduto)
                {
                    cmbTipoProduto.SelectedItem = tiposProduto[i].nometipoproduto;
                    break;
                }
            }

            for (int i = 0; i < marcas.Count; i++)
            {
                if (marcas[i].codmarca == lista_Produto[posicao].marca.codmarca)
                {
                    cmbMarca.SelectedItem = marcas[i].nomemarca;
                    break;
                }
            }
        }

        List<Produto> carregaListaProduto()
        {
            List<Produto> lista = new List<Produto>();

            C_Produto cp = new C_Produto();
            lista = cp.DadosProduto();

            return lista;
        }

        public void CarregaTabela()
        {
            C_Produto cp = new C_Produto();
            DataTable dt = new DataTable();
            dt = cp.Buscar_Todos();
            Tabela_Produto = dt;
            dataGridView1.DataSource = Tabela_Produto;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                DataGridViewRow dr = dataGridView1.Rows[index];

                txtCodigo.Text = dr.Cells[0].Value.ToString();
                txtNomeproduto.Text = dr.Cells[1].Value.ToString();
                txtQuant.Text = dr.Cells[2].Value.ToString();
                txtValor.Text = dr.Cells[3].Value.ToString();
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
            txtNomeproduto.Enabled = true;
            txtQuant.Enabled = true;
            txtValor.Enabled = true;
            cmbTipoProduto.Enabled = true;
            cmbMarca.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtNomeproduto.Text = "";
            txtQuant.Text = "";
            txtValor.Text = "";
            cmbTipoProduto.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
        }

        private void desativaCampos()
        {
            txtNomeproduto.Enabled = false;
            txtQuant.Enabled = false;
            txtValor.Enabled = false;
            cmbTipoProduto.Enabled = false;
            cmbMarca.Enabled = false;
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
            C_Produto cp = new C_Produto();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
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
            if (string.IsNullOrEmpty(txtNomeproduto.Text))
            {
                MessageBox.Show("O nome do produto é obrigatório.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTipoProduto.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o tipo do produto.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbMarca.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione a marca do produto.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Produto produto = new Produto()
            {
                codproduto = novo ? 0 : Convert.ToInt32(txtCodigo.Text),
                nomeproduto = txtNomeproduto.Text,
                quantidade = Convert.ToDecimal(txtQuant.Text),
                valor = Convert.ToDecimal(txtValor.Text),
                tipoproduto = tiposProduto[cmbTipoProduto.SelectedIndex],
                marca = marcas[cmbMarca.SelectedIndex]
            };

            C_Produto cp = new C_Produto();

            try
            {
                if (novo)
                {
                    cp.Insere_Dados(produto);
                    
                }
                else
                {
                    cp.Atualizar_Dados(produto);
                    
                }

                CarregaTabela();
                desativaCampos();
                desativaBotoes();
                limparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_Produto.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Produto = carregaListaProduto();
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
                lista_Produto = carregaListaProduto();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_Produto.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Produto = carregaListaProduto();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_Produto.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_Produto.Count - 1;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Produto = carregaListaProduto();
            }
        }
        public void CarregaTabelaFiltro(String filtro)
        {
            C_Produto cf = new C_Produto();
            DataTable dt = new DataTable();
            dt = cf.Buscar_Filtro(filtro);
            Tabela_Produto = dt;
            dataGridView1.DataSource = Tabela_Produto;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lista_Produto = carregaListaProdutoFiltro();
            CarregaTabelaFiltro(txtBuscar.Text);

            if (lista_Produto.Count > 0)
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
