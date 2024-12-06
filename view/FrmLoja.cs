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
    public partial class FrmLoja : Form
    {

        List<Rua> ruas = new List<Rua>();
        int posicao_rua;
        int codrua;

        List<Bairro> bairros = new List<Bairro>();
        int posicao_bairro;
        int codbairro;

        List<Cidade> cidades = new List<Cidade>();
        int posicao_cidade;
        int codcidade;

        List<Estado> estados = new List<Estado>();
        int posicao_estado;
        int codestado;

        List<Pais> paises = new List<Pais>();
        int posicao_pais;
        int codpais;

        List<Cep> ceps = new List<Cep>();
        int posicao_cep;
        int codcep;

        DataTable Tabela_Loja;
        Boolean novo = true;
        int posicao;
        List<Loja> lista_Loja = new List<Loja>();

        public FrmLoja()
        {
            InitializeComponent();
            preencheComboBairro();
            preencheComboRua();
            preencheComboCidade();
            preencheComboEstado();
            preencheComboPais();
            preencheComboCep();

            CarregaTabela();

            lista_Loja = carregaListaLoja();

            if (lista_Loja.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void preencheComboBairro()
        {

            bairros = carregaListaBairro();

            foreach (Bairro ba in bairros)
            {
                cmbBairro.Items.Add(ba.nomebairro);
            }

        }

        List<Bairro> carregaListaBairro()
        {
            List<Bairro> lista = new List<Bairro>();

            C_Bairro cb = new C_Bairro();
            DataTable dt = cb.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Bairro bairro = new Bairro
                {
                    codbairro = Convert.ToInt32(row["codbairro"]),
                    nomebairro = row["nomebairro"].ToString()
                };

                lista.Add(bairro);
            }

            return lista;
        }

        private void preencheComboRua()
        {
            C_Rua c_Rua = new C_Rua();

            ruas = c_Rua.DadosRua();
            foreach (Rua ru in ruas)
            {
                cmbRua.Items.Add(ru.nomerua);
            }
        }

        private void preencheComboCidade()
        {
            C_Cidade c_Cidade = new C_Cidade();

            cidades = c_Cidade.DadosCidade();
            foreach (Cidade ci in cidades)
            {
                cmbCidade.Items.Add(ci.nomecidade);
            }
        }

        private void preencheComboEstado()
        {
            C_Estado c_Estado = new C_Estado();

            estados = c_Estado.DadosEstado();
            foreach (Estado es in estados)
            {
                cmbEstado.Items.Add(es.nomeestado);
            }
        }

        private void preencheComboPais()
        {
            C_Pais c_Pais = new C_Pais();

            paises = c_Pais.DadosPais();
            foreach (Pais pa in paises)
            {
                cmbPais.Items.Add(pa.nomepais);
            }
        }

        private void preencheComboCep()
        {
            C_Cep c_Cep = new C_Cep();

            ceps = c_Cep.DadosCep();
            foreach (Cep ce in ceps)
            {
                cmbCEP.Items.Add(ce.numerocep);
            }
        }

        private void atualizaCampos()
        {

            txtCodigo.Text = lista_Loja[posicao].codloja.ToString();
            txtNumero.Text = lista_Loja[posicao].numeroloja.ToString();
            txtLoja.Text = lista_Loja[posicao].nomeloja.ToString();
            txtCnpj.Text = lista_Loja[posicao].cnpj.ToString();
            for (int i = 0; i < bairros.Count; i++)
            {
                if (bairros[i].codbairro == lista_Loja[posicao].bairro.codbairro)
                {
                    cmbBairro.SelectedItem = bairros[i].nomebairro;
                    break;
                }

            }

            foreach (Rua ru in ruas)
            {
                if (ru.codrua == lista_Loja[posicao].rua.codrua)
                {
                    cmbRua.SelectedItem = ru.nomerua;
                    break;
                }

            }
            foreach (Cidade ci in cidades)
            {
                if (ci.codcidade == lista_Loja[posicao].cidade.codcidade)
                {
                    cmbCidade.SelectedItem = ci.nomecidade;
                    break;
                }
            }
            foreach (Estado es in estados)
            {
                if (es.codestado == lista_Loja[posicao].estado.codestado)
                {
                    cmbEstado.SelectedItem = es.nomeestado;
                    break;
                }
            }
            foreach (Pais pa in paises)
            {
                if (pa.codpais == lista_Loja[posicao].pais.codpais)
                {
                    cmbPais.SelectedItem = pa.nomepais;
                    break;
                }
            }
            foreach (Cep ce in ceps)
            {
                if (ce.codcep == lista_Loja[posicao].cep.codcep)
                {
                    cmbCEP.SelectedItem = ce.numerocep;
                    break;
                }
            }
        }

        List<Loja> carregaListaLoja()
        {
            List<Loja> lista = new List<Loja>();

            C_Loja cc = new C_Loja();
            lista = cc.DadosCidade();

            return lista;
        }

        List<Loja> carregaListaLojaFiltro()
        {
            List<Loja> lista = new List<Loja>();

            C_Loja cc = new C_Loja();
            lista = cc.DadosCidadeFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Loja cc = new C_Loja();
            DataTable dt = new DataTable();
            dt = cc.Buscar_Todos();
            Tabela_Loja = dt;
            dataGridView1.DataSource = Tabela_Loja;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                DataGridViewRow dr = dataGridView1.Rows[index];

                txtCodigo.Text = dr.Cells[0].Value.ToString();
                txtLoja.Text = dr.Cells[1].Value.ToString();
                txtCnpj.Text = dr.Cells[2].Value.ToString();
                cmbRua.SelectedItem = dr.Cells[3].Value.ToString();
                txtNumero.Text = dr.Cells[4].Value.ToString();
                cmbBairro.SelectedItem = dr.Cells[5].Value.ToString();
                cmbCidade.SelectedItem = dr.Cells[6].Value.ToString();
                cmbCEP.SelectedItem = dr.Cells[7].Value.ToString();
                cmbEstado.SelectedItem = dr.Cells[8].Value.ToString();
                cmbPais.SelectedItem = dr.Cells[9].Value.ToString();


            }
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
            txtCnpj.Enabled = true;
            txtLoja.Enabled = true;
            txtNumero.Enabled = true;
            cmbBairro.Enabled = true;
            cmbRua.Enabled = true;
            cmbCidade.Enabled = true;
            cmbEstado.Enabled = true;
            cmbPais.Enabled = true;
            cmbCEP.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtNumero.Text = "";
            txtLoja.Text = "";
            txtCnpj.Text = "";
            cmbBairro.SelectedIndex = -1;
            cmbRua.SelectedIndex = -1;
            cmbCidade.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;
            cmbPais.SelectedIndex = -1;
            cmbCEP.SelectedIndex = -1;

        }

        private void desativaCampos()
        {
            txtCnpj.Enabled = false;
            txtLoja.Enabled = false;
            txtNumero.Enabled = false;
            cmbBairro.Enabled = false;
            cmbRua.Enabled = false;
            cmbCidade.Enabled = false;
            cmbEstado.Enabled = false;
            cmbPais.Enabled = false;
            cmbCEP.Enabled = false;
        }

        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }







        private void btnEditar_Click(object sender, EventArgs e)
        {
            ativarCampos();
            AtivaBotoes();
            novo = false;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_Loja.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Loja = carregaListaLoja();
            }
        }

        private void btnProximo_Click_1(object sender, EventArgs e)
        {
            if (posicao < lista_Loja.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Loja = carregaListaLoja();
            }
        }

        private void btnUltimo_Click_1(object sender, EventArgs e)
        {
            if (lista_Loja.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_Loja.Count - 1;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Loja = carregaListaLoja();
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
                lista_Loja = carregaListaLoja();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_Loja cc = new C_Loja();
            DataTable dt = new DataTable();
            dt = cc.Buscar_Filtro(txtBuscar.Text + "%");
            Tabela_Loja = dt;
            dataGridView1.DataSource = Tabela_Loja;

            lista_Loja = carregaListaLojaFiltro();

            if (lista_Loja.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Loja = carregaListaLoja();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            ativarCampos();
            AtivaBotoes();
            novo = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Loja loja = new Loja();
            loja.numeroloja = txtNumero.Text;
            loja.nomeloja = txtLoja.Text;
            loja.cnpj = txtCnpj.Text;

            Rua rua = new Rua();
            posicao_rua = cmbRua.SelectedIndex;
            rua.codrua = ruas[posicao_rua].codrua;
            loja.rua = rua;

            Bairro bairro = new Bairro();
            posicao_bairro = cmbBairro.SelectedIndex;
            bairro.codbairro = bairros[posicao_bairro].codbairro;
            loja.bairro = bairro;

            Cidade cidade = new Cidade();
            posicao_cidade = cmbCidade.SelectedIndex;
            cidade.codcidade = cidades[posicao_cidade].codcidade;
            loja.cidade = cidade;

            Estado estado = new Estado();
            posicao_estado = cmbEstado.SelectedIndex;
            estado.codestado = estados[posicao_estado].codestado;
            loja.estado = estado;

            Pais pais = new Pais();
            posicao_pais = cmbPais.SelectedIndex;
            pais.codpais = paises[posicao_pais].codpais;
            loja.pais = pais;

            Cep cep = new Cep();
            posicao_cep = cmbCEP.SelectedIndex;
            cep.codcep = ceps[posicao_cep].codcep;
            loja.cep = cep;


            C_Loja c_Loja = new C_Loja();

            if (novo == true)
            {
                c_Loja.Insere_Dados(loja);
            }
            else
            {
                loja.codloja = Int32.Parse(txtCodigo.Text);
                c_Loja.Atualizar_Dados(loja);
            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();
            lista_Loja = carregaListaLoja();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desativaBotoes();
            desativaCampos();
        }

        private void btnApagar_Click_1(object sender, EventArgs e)
        {
            C_Loja c_loja = new C_Loja();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                c_loja.Apaga_Dados(valor);
                CarregaTabela();
            }
        }
    }
}
