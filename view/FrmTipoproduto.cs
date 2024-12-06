using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmTipoproduto : Form
    {
        DataTable Tabela_tipoproduto;
        Boolean novo = true;
        int posicao;
        List<Tipoproduto> lista_tipoproduto = new List<Tipoproduto>();
        public FrmTipoproduto()
        {
            InitializeComponent();

            //Carregar o Datagrid de Tipoprodutos.
            CarregaTabela();

            if (lista_tipoproduto.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_tipoproduto[posicao].codtipoproduto.ToString();
            txtTipoproduto.Text = lista_tipoproduto[posicao].nometipoproduto.ToString();
        }

        List<Tipoproduto> carregaListaTipoproduto()
        {
            List<Tipoproduto> lista = new List<Tipoproduto>();

            C_TipoProduto cr = new C_TipoProduto();
            lista = cr.DadosTipoProduto();

            return lista;
        }

        List<Tipoproduto> carregaListaTipoprodutoFiltro()
        {
            List<Tipoproduto> lista = new List<Tipoproduto>();

            C_TipoProduto cr = new C_TipoProduto();
            lista = cr.DadosTipoProdutoFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_TipoProduto cr = new C_TipoProduto();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_tipoproduto = dt;
            dataGridView1.DataSource = Tabela_tipoproduto;
            lista_tipoproduto = carregaListaTipoproduto();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtTipoproduto.Text = dr.Cells[1].Value.ToString();
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
            txtTipoproduto.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtTipoproduto.Text = "";
        }

        private void desativaCampos()
        {
            txtTipoproduto.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Tipoproduto tipoproduto = new Tipoproduto();

            tipoproduto.nometipoproduto = txtTipoproduto.Text;

            C_TipoProduto c_Tipoproduto = new C_TipoProduto();

            if (novo == true)
            {
                c_Tipoproduto.Insere_Dados(tipoproduto);
            }
            else
            {
                tipoproduto.codtipoproduto = Int32.Parse(txtCodigo.Text);
                c_Tipoproduto.Atualizar_Dados(tipoproduto);
            }

            CarregaTabela();
            lista_tipoproduto = carregaListaTipoproduto();

            desativaCampos();
            desativaBotoes();
        }

        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;

            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desativaBotoes();
            desativaCampos();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            C_TipoProduto tipoproduto = new C_TipoProduto();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                tipoproduto.Apaga_Dados(valor);
                CarregaTabela();
                lista_tipoproduto = carregaListaTipoproduto();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ativarCampos();
            AtivaBotoes();
            novo = false;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = 0;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            int total = lista_tipoproduto.Count - 1;
            if (total > posicao)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = lista_tipoproduto.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao--;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_TipoProduto cr = new C_TipoProduto();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_tipoproduto = dt;

            dataGridView1.DataSource = Tabela_tipoproduto;
            lista_tipoproduto = carregaListaTipoprodutoFiltro();

            if (lista_tipoproduto.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_tipoproduto = carregaListaTipoproduto();
            }
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
