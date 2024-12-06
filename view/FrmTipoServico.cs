using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmTipoServico : Form
    {
        DataTable Tabela_tipoServico;
        Boolean novo = true;
        int posicao;
        List<Tiposervico> lista_tipoServico = new List<Tiposervico>();

        public FrmTipoServico()
        {
            InitializeComponent();

            // Carregar o Datagrid de TipoServico.
            CarregaTabela();

            if (lista_tipoServico.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_tipoServico[posicao].codtiposervico.ToString();
            txtNomeTipoServico.Text = lista_tipoServico[posicao].nometiposervico.ToString();
            txtValorTipoServico.Text = lista_tipoServico[posicao].valortiposervico.ToString();
        }

        List<Tiposervico> carregaListaTipoServico()
        {
            List<Tiposervico> lista = new List<Tiposervico>();

            C_TipoServico cr = new C_TipoServico();
            lista = cr.DadosTipoServico();

            return lista;
        }

        List<Tiposervico> carregaListaTipoServicoFiltro()
        {
            List<Tiposervico> lista = new List<Tiposervico>();

            C_TipoServico cr = new C_TipoServico();
            lista = cr.DadosTipoServicoFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_TipoServico cr = new C_TipoServico();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_tipoServico = dt;
            dataGridView1.DataSource = Tabela_tipoServico;
            lista_tipoServico = carregaListaTipoServico();
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
            txtNomeTipoServico.Enabled = true;
            txtValorTipoServico.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtNomeTipoServico.Text = "";
            txtValorTipoServico.Text = "";
        }

        private void desativaCampos()
        {
            txtNomeTipoServico.Enabled = false;
            txtValorTipoServico.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Tiposervico tipoServico = new Tiposervico();

            tipoServico.nometiposervico = txtNomeTipoServico.Text;
            tipoServico.valortiposervico = Decimal.Parse(txtValorTipoServico.Text);

            C_TipoServico c_TipoServico = new C_TipoServico();

            if (novo == true)
            {
                c_TipoServico.Insere_Dados(tipoServico);
            }
            else
            {
                tipoServico.codtiposervico = Int32.Parse(txtCodigo.Text);
                c_TipoServico.Atualizar_Dados(tipoServico);
            }

            CarregaTabela();
            lista_tipoServico = carregaListaTipoServico();

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
            C_TipoServico tipoServico = new C_TipoServico();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                tipoServico.Apaga_Dados(valor);
                CarregaTabela();
                lista_tipoServico = carregaListaTipoServico();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ativarCampos();
            AtivaBotoes();
            novo = false;
        }

        // Funções de navegação
        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = 0;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            int total = lista_tipoServico.Count - 1;
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
            posicao = lista_tipoServico.Count - 1;
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

        // Função de busca
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_TipoServico cr = new C_TipoServico();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_tipoServico = dt;

            dataGridView1.DataSource = Tabela_tipoServico;
            lista_tipoServico = carregaListaTipoServicoFiltro();

            if (lista_tipoServico.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_tipoServico = carregaListaTipoServico();
            }
        }
    }
}
