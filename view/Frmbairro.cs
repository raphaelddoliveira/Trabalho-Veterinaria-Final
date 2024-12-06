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
    public partial class Frmbairro : Form
    {
        DataTable Tabela_bairro;
        Boolean novo = true;
        int posicao;
        List<Bairro> lista_bairro = new List<Bairro>();
        public Frmbairro()
        {
            InitializeComponent();

            //Carregar o Datagrid de Bairros.
            CarregaTabela();

            

            if (lista_bairro.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_bairro[posicao].codbairro.ToString();
            txtBairro.Text = lista_bairro[posicao].nomebairro.ToString();
        }

        List<Bairro> carregaListaBairro()
        {
            List<Bairro> lista = new List<Bairro>();

            C_Bairro cr = new C_Bairro();
            lista = cr.DadosBairro();

            return lista;
        }

        List<Bairro> carregaListaBairroFiltro()
        {
            List<Bairro> lista = new List<Bairro>();

            C_Bairro cr = new C_Bairro();
            lista = cr.DadosBairroFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Bairro cr = new C_Bairro();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_bairro = dt;
            dataGridView1.DataSource = Tabela_bairro;
            lista_bairro = carregaListaBairro();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtBairro.Text = dr.Cells[1].Value.ToString();
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
            txtBairro.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtBairro.Text = "";
        }

        private void desativaCampos()
        {
            txtBairro.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Bairro bairro = new Bairro();

            bairro.nomebairro = txtBairro.Text;

            C_Bairro c_Bairro = new C_Bairro();

            if (novo == true)
            {
                c_Bairro.Insere_Dados(bairro);
            }
            else
            {
                bairro.codbairro = Int32.Parse(txtCodigo.Text);
                c_Bairro.Atualizar_Dados(bairro);
            }

            CarregaTabela();
            lista_bairro = carregaListaBairro();

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
            C_Bairro bairro = new C_Bairro();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                bairro.Apaga_Dados(valor);
                CarregaTabela();
                lista_bairro = carregaListaBairro();
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
            int total = lista_bairro.Count - 1;
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
            posicao = lista_bairro.Count - 1;
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
            C_Bairro cr = new C_Bairro();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_bairro = dt;

            dataGridView1.DataSource = Tabela_bairro;
            lista_bairro = carregaListaBairroFiltro();

            if (lista_bairro.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_bairro = carregaListaBairro();
            }
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            C_Bairro cr = new C_Bairro();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_bairro = dt;

            dataGridView1.DataSource = Tabela_bairro;
            lista_bairro = carregaListaBairroFiltro();

            if (lista_bairro.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_bairro = carregaListaBairro();
            }
        }
    }
}
