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
    public partial class FrmCidanimal : Form
    {
        DataTable Tabela_cidanimal;
        Boolean novo = true;
        int posicao;
        List<CidAnimal> lista_cidanimal = new List<CidAnimal>();
        public FrmCidanimal()
        {
            InitializeComponent();

            //Carregar o Datagrid de Cidanimal.
            CarregaTabela();

            if (lista_cidanimal.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_cidanimal[posicao].codcidanimal.ToString();
            txtCidanimal.Text = lista_cidanimal[posicao].nomecidanimal.ToString();
            txtDescricao.Text = lista_cidanimal[posicao].descricao.ToString(); // Novo campo para descrição
        }

        List<CidAnimal> carregaListaCidanimal()
        {
            List<CidAnimal> lista = new List<CidAnimal>();

            C_Cidanimal cr = new C_Cidanimal();
            lista = cr.DadosCidanimal();

            return lista;
        }

        List<CidAnimal> carregaListaCidanimalFiltro()
        {
            List<CidAnimal> lista = new List<CidAnimal>();

            C_Cidanimal cr = new C_Cidanimal();
            lista = cr.DadosCidanimalFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Cidanimal cr = new C_Cidanimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_cidanimal = dt;
            dataGridView1.DataSource = Tabela_cidanimal;
            lista_cidanimal = carregaListaCidanimal();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtCidanimal.Text = dr.Cells[1].Value.ToString();
            txtDescricao.Text = dr.Cells[2].Value.ToString(); // Novo campo para descrição
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
            txtCidanimal.Enabled = true;
            txtDescricao.Enabled = true; // Ativando campo de descrição
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtCidanimal.Text = "";
            txtDescricao.Text = ""; // Limpa o campo de descrição
        }

        private void desativaCampos()
        {
            txtCidanimal.Enabled = false;
            txtDescricao.Enabled = false; // Desativando campo de descrição
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            CidAnimal cidanimal = new CidAnimal();

            cidanimal.nomecidanimal = txtCidanimal.Text;
            cidanimal.descricao = txtDescricao.Text; // Salvando o valor da descrição

            C_Cidanimal c_Cidanimal = new C_Cidanimal();

            if (novo == true)
            {
                c_Cidanimal.Insere_Dados(cidanimal);
            }
            else
            {
                cidanimal.codcidanimal = Int32.Parse(txtCodigo.Text);
                c_Cidanimal.Atualizar_Dados(cidanimal);
            }

            CarregaTabela();
            lista_cidanimal = carregaListaCidanimal();

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
            C_Cidanimal cidanimal = new C_Cidanimal();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                cidanimal.Apaga_Dados(valor);
                CarregaTabela();
                lista_cidanimal = carregaListaCidanimal();
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
            int total = lista_cidanimal.Count - 1;
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
            posicao = lista_cidanimal.Count - 1;
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
            C_Cidanimal cr = new C_Cidanimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_cidanimal = dt;

            dataGridView1.DataSource = Tabela_cidanimal;
            lista_cidanimal = carregaListaCidanimalFiltro();

            if (lista_cidanimal.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_cidanimal = carregaListaCidanimal();
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
