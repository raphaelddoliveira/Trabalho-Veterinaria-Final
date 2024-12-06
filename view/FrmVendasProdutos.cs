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
    public partial class FrmVendasProdutos : Form
    {
        List<Vendas> vendas = new List<Vendas>();
        int posicao_vendas;
        int codvenda;

        List<Produto> produto = new List<Produto>();
        int posicao_produto;
        int codproduto;

        DataTable Tabela_VendasProdutos;
        Boolean novo = true;
        int posicao;
        List<VendasProdutos> lista_VendasProdutos = new List<VendasProdutos>();

        public FrmVendasProdutos()
        {
            InitializeComponent();
            preencheComboVendas();
            preencheComboProduto();

            CarregaTabela();

            lista_VendasProdutos = carregaListaVendasProduto();

            if (lista_VendasProdutos.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void preencheComboVendas()
        {
            vendas = carregaListaVendas();

            foreach (Vendas tipo in vendas)
            {
                cmbVendas.Items.Add(tipo.codvenda);
            }
        }

        private void preencheComboProduto()
        {
            produto = carregaListaProduto();

            foreach (Produto produto in produto)
            {
                cmbProduto.Items.Add(produto.nomeproduto);
            }
        }


        List<Vendas> carregaListaVendas()
        {
            List<Vendas> lista = new List<Vendas>();

            C_Vendas ct = new C_Vendas();
            DataTable dt = ct.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Vendas tipo = new Vendas
                {
                    codvenda = Convert.ToInt32(row["codvenda"])
                };


                lista.Add(tipo);
            }

            return lista;
        }

        List<Produto> carregaListaProduto()
        {
            List<Produto> lista = new List<Produto>();

            C_Produto cv = new C_Produto();
            DataTable dt = cv.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Produto produto = new Produto
                {
                    codproduto = Convert.ToInt32(row["codproduto"]),
                    nomeproduto = row["nomeproduto"].ToString()
                };

                lista.Add(produto);
            }

            return lista;
        }


        private void atualizaCampos()
        {
            txtQuant.Text = lista_VendasProdutos[posicao].quantv.ToString();
            txtValor.Text = lista_VendasProdutos[posicao].valorv.ToString();

            for (int i = 0; i < vendas.Count; i++)
            {
                if (vendas[i].codvenda == lista_VendasProdutos[posicao].vendas.codvenda)
                {
                    cmbVendas.SelectedItem = vendas[i].codvenda;
                    break;
                }
            }

            for (int i = 0; i < produto.Count; i++)
            {
                if (produto[i].codproduto == lista_VendasProdutos[posicao].produto.codproduto)
                {
                    cmbProduto.SelectedItem = produto[i].nomeproduto;
                    break;
                }
            }
        }

        List<VendasProdutos> carregaListaVendasProduto()
        {
            List<VendasProdutos> lista = new List<VendasProdutos>();

            C_VendasProdutos cvs = new C_VendasProdutos();
            lista = cvs.DadosVendasProdutos();

            return lista;
        }
        List<VendasProdutos> carregaListaVendasProdutosFiltro()
        {
            List<VendasProdutos> lista = new List<VendasProdutos>();

            C_VendasProdutos cc = new C_VendasProdutos();
            lista = cc.DadosVendasProdutosFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_VendasProdutos cv = new C_VendasProdutos();
            DataTable dt = new DataTable();
            dt = cv.Buscar_Todos();
            Tabela_VendasProdutos = dt;
            dataGridView1.DataSource = Tabela_VendasProdutos;
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
            cmbVendas.Enabled = true;
            cmbProduto.Enabled = true;

        }

        private void limparCampos()
        {
            txtQuant.Text = "";
            txtValor.Text = "";
            cmbVendas.SelectedIndex = -1;
            cmbProduto.SelectedIndex = -1;
        }

        private void desativaCampos()
        {
            txtQuant.Enabled = false;
            txtValor.Enabled = false;
            cmbVendas.Enabled = false;
            cmbProduto.Enabled = false;
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
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            C_VendasProdutos cp = new C_VendasProdutos();

            if (cmbVendas.Text != "")
            {
                int valor = Int32.Parse(cmbVendas.Text);
                cp.Apaga_Dados(valor);
                CarregaTabela();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            AtivaBotoes();
            ativarCampos();
            novo = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuant.Text) || string.IsNullOrEmpty(txtValor.Text))
            {
                MessageBox.Show("Quantidade e valor são obrigatórios.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VendasProdutos item = new VendasProdutos()
            {
                quantv = Convert.ToDecimal(txtQuant.Text),
                valorv = Convert.ToDecimal(txtValor.Text),
                vendas = vendas[cmbVendas.SelectedIndex],
                produto = produto[cmbProduto.SelectedIndex]
            };

            C_VendasProdutos ccv = new C_VendasProdutos();

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
            lista_VendasProdutos = carregaListaVendasProdutosFiltro();
            dataGridView1.DataSource = lista_VendasProdutos;
        }

        private void btnPrimeiro_Click_1(object sender, EventArgs e)
        {
            if (lista_VendasProdutos.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_VendasProdutos = carregaListaVendasProduto();
            }
        }

        private void btnAnterior_Click_1(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao--;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_VendasProdutos = carregaListaVendasProduto();
            }
        }

        private void btnProximo_Click_1(object sender, EventArgs e)
        {
            if (posicao < lista_VendasProdutos.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_VendasProdutos = carregaListaVendasProduto();
            }
        }

        private void btnUltimo_Click_1(object sender, EventArgs e)
        {
            if (lista_VendasProdutos.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_VendasProdutos.Count - 1;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_VendasProdutos = carregaListaVendasProduto();
            }
        }
        public void CarregaTabelaFiltro(String filtro)
        {
            C_ItensVendaServico cf = new C_ItensVendaServico();
            DataTable dt = new DataTable();
            dt = cf.Buscar_Filtro(filtro);
            Tabela_VendasProdutos = dt;
            dataGridView1.DataSource = Tabela_VendasProdutos;
        }

        private void btnBuscar_Click_2(object sender, EventArgs e)
        {
            lista_VendasProdutos = carregaListaVendasProdutosFiltro();
            CarregaTabelaFiltro(txtBuscar.Text);

            if (lista_VendasProdutos.Count > 0)
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
