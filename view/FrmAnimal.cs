using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmAnimal : Form
    {
        DataTable Tabela_Animal;
        bool novo = true;
        int posicao;
        List<Animal> lista_Animal = new List<Animal>();

        List<Sexo> sexos = new List<Sexo>();
        List<Raca> racas = new List<Raca>();
        List<Tipoanimal> tiposAnimal = new List<Tipoanimal>();
        List<Cliente> clientes = new List<Cliente>();

        public FrmAnimal()
        {
            InitializeComponent();

            preencheComboSexo();
            preencheComboRaca();
            preencheComboTipoAnimal();
            preencheComboCliente();

            CarregaTabela();

            lista_Animal = carregaListaAnimal();

            if (lista_Animal.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void preencheComboSexo()
        {
            sexos = carregaListaSexo();
            foreach (Sexo sexo in sexos)
            {
                cmbSexo.Items.Add(sexo.nomesexo);
            }
        }

        private void preencheComboRaca()
        {
            racas = carregaListaRaca();
            foreach (Raca raca in racas)
            {
                cmbRaca.Items.Add(raca.nomeraca);
            }
        }

        private void preencheComboTipoAnimal()
        {
            tiposAnimal = carregaListaTipoAnimal();
            foreach (Tipoanimal tipo in tiposAnimal)
            {
                cmbTipoanimal.Items.Add(tipo.nometipoanimal);
            }
        }

        private void preencheComboCliente()
        {
            clientes = carregaListaCliente();
            foreach (Cliente cliente in clientes)
            {
                cmbCliente.Items.Add(cliente.nomecliente);
            }
        }

        private List<Sexo> carregaListaSexo()
        {

            List<Sexo> lista = new List<Sexo>();

            C_Sexo ct = new C_Sexo();
            DataTable dt = ct.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Sexo sexo = new Sexo
                {
                    codsexo = Convert.ToInt32(row["codsexo"]),
                    nomesexo = row["nomesexo"].ToString()
                };

                lista.Add(sexo);
            }

            return lista;
        }

        private List<Raca> carregaListaRaca()
        {
            List<Raca> lista = new List<Raca>();

            C_Raca ct = new C_Raca();
            DataTable dt = ct.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Raca raca = new Raca
                {
                    codraca = Convert.ToInt32(row["codraca"]),
                    nomeraca = row["nomeraca"].ToString()
                };

                lista.Add(raca);
            }
            return lista;
        }

        private List<Tipoanimal> carregaListaTipoAnimal()
        {
            List<Tipoanimal> lista = new List<Tipoanimal>();

            C_TipoAnimal ct = new C_TipoAnimal();
            DataTable dt = ct.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Tipoanimal tipo = new Tipoanimal
                {
                    codtipoanimal = Convert.ToInt32(row["codtipoanimal"]),
                    nometipoanimal = row["nometipoanimal"].ToString()
                };

                lista.Add(tipo);
            }
            return lista;
        }

        private List<Cliente> carregaListaCliente()
        {
            List<Cliente> lista = new List<Cliente>();

            C_Cliente ct = new C_Cliente();
            DataTable dt = ct.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Cliente cliente = new Cliente
                {
                    codcliente = Convert.ToInt32(row["codcliente"]),
                    nomecliente = row["nomecliente"].ToString()
                };

                lista.Add(cliente);
            }
            return lista;
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_Animal[posicao].codanimal.ToString();
            txtNomeanimal.Text = lista_Animal[posicao].nomeanimal;

            cmbSexo.SelectedIndex = sexos.FindIndex(s => s.codsexo == lista_Animal[posicao].sexo.codsexo);
            cmbRaca.SelectedIndex = racas.FindIndex(r => r.codraca == lista_Animal[posicao].raca.codraca);
            cmbTipoanimal.SelectedIndex = tiposAnimal.FindIndex(t => t.codtipoanimal == lista_Animal[posicao].tipoanimal.codtipoanimal);
            cmbCliente.SelectedIndex = clientes.FindIndex(c => c.codcliente == lista_Animal[posicao].cliente.codcliente);
        }

        private List<Animal> carregaListaAnimal()
        {
            List<Animal> lista = new List<Animal>();

            C_Animal cp = new C_Animal();
            lista = cp.DadosAnimal();

            return lista;
        }
        private List<Animal> carregaListaAnimalFiltro()
        {
            List<Animal> lista = new List<Animal>();

            C_Animal cc = new C_Animal();
            lista = cc.DadosAnimalFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Animal ca = new C_Animal();
            Tabela_Animal = ca.Buscar_Todos();
            dataGridView1.DataSource = Tabela_Animal;
        }
        public void CarregaTabelaFiltro(String filtro)
        {
            C_Animal cf = new C_Animal();
            DataTable dt = new DataTable();
            dt = cf.Buscar_Filtro(filtro);
            Tabela_Animal = dt;
            dataGridView1.DataSource = Tabela_Animal;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtNomeanimal.Text = "";
            cmbSexo.SelectedIndex = -1;
            cmbRaca.SelectedIndex = -1;
            cmbTipoanimal.SelectedIndex = -1;
            cmbCliente.SelectedIndex = -1;
        }

        private void ativarCampos()
        {
            txtNomeanimal.Enabled = true;
            cmbSexo.Enabled = true;
            cmbRaca.Enabled = true;
            cmbTipoanimal.Enabled = true;
            cmbCliente.Enabled = true;
        }

        private void desativaCampos()
        {
            txtNomeanimal.Enabled = false;
            cmbSexo.Enabled = false;
            cmbRaca.Enabled = false;
            cmbTipoanimal.Enabled = false;
            cmbCliente.Enabled = false;
        }

        private void AtivaBotoes()
        {
            btnNovo.Enabled = false;
            btnApagar.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            ativarCampos();
            AtivaBotoes();
            novo = true;
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeanimal.Text))
            {
                MessageBox.Show("O nome do animal é obrigatório.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Animal animal = new Animal
            {
                codanimal = novo ? 0 : Convert.ToInt32(txtCodigo.Text),
                nomeanimal = txtNomeanimal.Text,
                sexo = sexos[cmbSexo.SelectedIndex],
                raca = racas[cmbRaca.SelectedIndex],
                tipoanimal = tiposAnimal[cmbTipoanimal.SelectedIndex],
                cliente = clientes[cmbCliente.SelectedIndex]
            };

            C_Animal ca = new C_Animal();

            try
            {
                if (novo)
                {
                    ca.Insere_Dados(animal);
                }
                else
                {
                    ca.Atualizar_Dados(animal);
                }

                CarregaTabela();
                limparCampos();
                desativaCampos();
                desativaBotoes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o animal: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            limparCampos();
            desativaCampos();
            desativaBotoes();
        }

        private void btnApagar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigo.Text)) return;

            C_Animal ca = new C_Animal();
            int codigo = Convert.ToInt32(txtCodigo.Text);

            try
            {
                ca.Apaga_Dados(codigo);
                CarregaTabela();
                limparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar o animal: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            AtivaBotoes();
            ativarCampos();
            novo = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lista_Animal = carregaListaAnimalFiltro();
            CarregaTabelaFiltro(txtBuscar.Text);

            if (lista_Animal.Count > 0)
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

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (lista_Animal.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Animal = carregaListaAnimal();
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
                lista_Animal = carregaListaAnimal();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_Animal.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Animal = carregaListaAnimal();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_Animal.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_Animal.Count - 1;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Animal = carregaListaAnimal();
            }
        }
    }
}
