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
    public partial class FrmCep : Form
    {
        DataTable Tabela_cep;
        Boolean novo = true;
        int posicao;
        List<Cep> lista_cep = new List<Cep>();
        public FrmCep()
        {
            InitializeComponent();

            //Carregar o Datagrid de Ceps.
            CarregaTabela();

            if (lista_cep.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_cep[posicao].codcep.ToString();
            txtCep.Text = lista_cep[posicao].numerocep.ToString();
        }

        List<Cep> carregaListaCep()
        {
            List<Cep> lista = new List<Cep>();

            C_Cep cr = new C_Cep();
            lista = cr.DadosCep();

            return lista;
        }

        List<Cep> carregaListaCepFiltro()
        {
            List<Cep> lista = new List<Cep>();

            C_Cep cr = new C_Cep();
            lista = cr.DadosCepFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Cep cr = new C_Cep();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_cep = dt;
            dataGridView1.DataSource = Tabela_cep;
            lista_cep = carregaListaCep();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtCep.Text = dr.Cells[1].Value.ToString();
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
            txtCep.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtCep.Text = "";
        }

        private void desativaCampos()
        {
            txtCep.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cep cep = new Cep();

            cep.numerocep = txtCep.Text;

            C_Cep c_Cep = new C_Cep();

            if (novo == true)
            {
                c_Cep.Insere_Dados(cep);
            }
            else
            {
                cep.codcep = Int32.Parse(txtCodigo.Text);
                c_Cep.Atualizar_Dados(cep);
            }

            CarregaTabela();
            lista_cep = carregaListaCep();

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
            C_Cep cep = new C_Cep();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                cep.Apaga_Dados(valor);
                CarregaTabela();
                lista_cep = carregaListaCep();
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
            int total = lista_cep.Count - 1;
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
            posicao = lista_cep.Count - 1;
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
            C_Cep cr = new C_Cep();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_cep = dt;

            dataGridView1.DataSource = Tabela_cep;
            lista_cep = carregaListaCepFiltro();

            if (lista_cep.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_cep = carregaListaCep();
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
