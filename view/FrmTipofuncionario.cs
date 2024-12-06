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
    public partial class FrmTipofuncionario : Form
    {
        DataTable Tabela_tipofuncionario;
        Boolean novo = true;
        int posicao;
        List<Tipofuncionario> lista_tipofuncionario = new List<Tipofuncionario>();
        public FrmTipofuncionario()
        {
            InitializeComponent();

            //Carregar o Datagrid de Tipos de Funcionário.
            CarregaTabela();

            if (lista_tipofuncionario.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_tipofuncionario[posicao].codtipofuncionario.ToString();
            txtTipofuncionario.Text = lista_tipofuncionario[posicao].nometipofuncionario.ToString();
        }

        List<Tipofuncionario> carregaListaTipoFuncionario()
        {
            List<Tipofuncionario> lista = new List<Tipofuncionario>();

            C_TipoFuncionario cr = new C_TipoFuncionario();
            lista = cr.DadosTipoFuncionario();

            return lista;
        }

        List<Tipofuncionario> carregaListaTipoFuncionarioFiltro()
        {
            List<Tipofuncionario> lista = new List<Tipofuncionario>();

            C_TipoFuncionario cr = new C_TipoFuncionario();
            lista = cr.DadosTipoFuncionarioFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_TipoFuncionario cr = new C_TipoFuncionario();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_tipofuncionario = dt;
            dataGridView1.DataSource = Tabela_tipofuncionario;
            lista_tipofuncionario = carregaListaTipoFuncionario();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtTipofuncionario.Text = dr.Cells[1].Value.ToString();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            
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
            txtTipofuncionario.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtTipofuncionario.Text = "";
        }

        private void desativaCampos()
        {
            txtTipofuncionario.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Tipofuncionario tipofuncionario = new Tipofuncionario();

            tipofuncionario.nometipofuncionario = txtTipofuncionario.Text;

            C_TipoFuncionario c_TipoFuncionario = new C_TipoFuncionario();

            if (novo == true)
            {
                c_TipoFuncionario.Insere_Dados(tipofuncionario);
            }
            else
            {
                tipofuncionario.codtipofuncionario = Int32.Parse(txtCodigo.Text);
                c_TipoFuncionario.Atualizar_Dados(tipofuncionario);
            }

            CarregaTabela();
            lista_tipofuncionario = carregaListaTipoFuncionario();

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
            C_TipoFuncionario tipofuncionario = new C_TipoFuncionario();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                tipofuncionario.Apaga_Dados(valor);
                CarregaTabela();
                lista_tipofuncionario = carregaListaTipoFuncionario();
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
            int total = lista_tipofuncionario.Count - 1;
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
            posicao = lista_tipofuncionario.Count - 1;
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
            C_TipoFuncionario cr = new C_TipoFuncionario();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_tipofuncionario = dt;

            dataGridView1.DataSource = Tabela_tipofuncionario;
            lista_tipofuncionario = carregaListaTipoFuncionarioFiltro();

            if (lista_tipofuncionario.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_tipofuncionario = carregaListaTipoFuncionario();
            }
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            limparCampos();

            ativarCampos();

            AtivaBotoes();

            novo = true;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       

        

        

     
    }
}
