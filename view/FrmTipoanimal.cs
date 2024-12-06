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
    public partial class FrmTipoanimal : Form
    {
        DataTable Tabela_tipoanimal;
        Boolean novo = true;
        int posicao;
        List<Tipoanimal> lista_tipoanimal = new List<Tipoanimal>();
        public FrmTipoanimal()
        {
            InitializeComponent();

            //Carregar o Datagrid de Raças.
            CarregaTabela();


            if (lista_tipoanimal.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }


        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_tipoanimal[posicao].codtipoanimal.ToString();
            txtTipoanimal.Text = lista_tipoanimal[posicao].nometipoanimal.ToString();
        }

        List<Tipoanimal> carregaListaTipoanimal()
        {
            List<Tipoanimal> lista = new List<Tipoanimal>();

            C_TipoAnimal cr = new C_TipoAnimal();
            lista = cr.DadosTipoAnimal();

            return lista;
        }

        List<Tipoanimal> carregaListaTipoanimalFiltro()
        {
            List<Tipoanimal> lista = new List<Tipoanimal>();

            C_TipoAnimal cr = new C_TipoAnimal();
            lista = cr.DadosTipoAnimalFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_TipoAnimal cr = new C_TipoAnimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_tipoanimal = dt;
            dataGridView1.DataSource = Tabela_tipoanimal;
            lista_tipoanimal = carregaListaTipoanimal();

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtTipoanimal.Text = dr.Cells[1].Value.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
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
            txtTipoanimal.Enabled = true;

        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtTipoanimal.Text = "";
        }

        private void desativaCampos()
        {
            txtTipoanimal.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Tipoanimal tipoanimal = new Tipoanimal();

            tipoanimal.nometipoanimal = txtTipoanimal.Text;

            C_TipoAnimal c_TipoAnimal = new C_TipoAnimal();

            if (novo == true)
            {
                c_TipoAnimal.Insere_Dados(tipoanimal);
            }
            else
            {
                tipoanimal.codtipoanimal = Int32.Parse(txtCodigo.Text);
                c_TipoAnimal.Atualizar_Dados(tipoanimal);
            }


            CarregaTabela();
            lista_tipoanimal = carregaListaTipoanimal();

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
            C_TipoAnimal tipoAnimal = new C_TipoAnimal();


            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                tipoAnimal.Apaga_Dados(valor);
                CarregaTabela();
                lista_tipoanimal = carregaListaTipoanimal();
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
            int total = lista_tipoanimal.Count - 1;
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
            posicao = lista_tipoanimal.Count - 1;
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
            //Foi definido um atributo chamado cr do tipo C_Raca
            C_TipoAnimal cr = new C_TipoAnimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_tipoanimal = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_tipoanimal;

            //Carrega a Lista_raca com o valor da consulta com parâmetro
            lista_tipoanimal = carregaListaTipoanimalFiltro();

            if (lista_tipoanimal.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_tipoanimal = carregaListaTipoanimal();
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
            C_TipoAnimal cr = new C_TipoAnimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_tipoanimal = dt;

            dataGridView1.DataSource = Tabela_tipoanimal;
            lista_tipoanimal = carregaListaTipoanimalFiltro();

            if (lista_tipoanimal.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_tipoanimal = carregaListaTipoanimal();
            }
        }
    }
}
