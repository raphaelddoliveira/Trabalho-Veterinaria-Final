using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmVendaServico : Form
    {
        List<Cliente> clientes = new List<Cliente>();
        int posicao_cliente;
        int codCliente;

        List<Funcionario> funcionarios = new List<Funcionario>();
        int posicao_funcionario;
        int codFuncionario;

        List<Animal> animais = new List<Animal>();
        int posicao_animal;
        int codAnimal;

        DataTable Tabela_VendaServico;
        Boolean nova = true;
        int posicao;
        List<VendaServico> lista_VendaServico = new List<VendaServico>();

        public FrmVendaServico()
        {
            InitializeComponent();
            preencheComboCliente();
            preencheComboFuncionario();
            preencheComboAnimal();

            CarregaTabela();

            lista_VendaServico = carregaListaVendaServico();

            if (lista_VendaServico.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }
        List<VendaServico> carregaListaVendaServico()
        {
            List<VendaServico> lista = new List<VendaServico>();

            C_VendaServico cp = new C_VendaServico();
            lista = cp.DadosVendaServico();

            return lista;
        }

        private void preencheComboCliente()
        {
            clientes = carregaListaClientes();

            foreach (Cliente cliente in clientes)
            {
                cmbCliente.Items.Add(cliente.nomecliente);
            }
        }

        private void preencheComboFuncionario()
        {
            funcionarios = carregaListaFuncionarios();

            foreach (Funcionario funcionario in funcionarios)
            {
                cmbFuncionario.Items.Add(funcionario.nomefuncionario);
            }
        }

        private void preencheComboAnimal()
        {
            animais = carregaListaAnimal();

            foreach (Animal animal in animais)
            {
                cmbAnimal.Items.Add(animal.nomeanimal);
            }
        }

        List<Cliente> carregaListaClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            C_Cliente cm = new C_Cliente();
            DataTable dt = cm.Buscar_Todos();

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

        List<Funcionario> carregaListaFuncionarios()
        {
            List<Funcionario> lista = new List<Funcionario>();

            C_Funcionario cm = new C_Funcionario();
            DataTable dt = cm.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Funcionario funcionario = new Funcionario
                {
                    codfuncionario = Convert.ToInt32(row["codfuncionario"]),
                    nomefuncionario = row["nomefuncionario"].ToString()
                };

                lista.Add(funcionario);
            }

            return lista;
        }

        List<Animal> carregaListaAnimal()
        {
            List<Animal> lista = new List<Animal>();

            C_Animal cm = new C_Animal();
            DataTable dt = cm.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Animal animal = new Animal
                {
                    codanimal = Convert.ToInt32(row["codanimal"]),
                    nomeanimal = row["nomeanimal"].ToString()
                };

                lista.Add(animal);
            }

            return lista;
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_VendaServico[posicao].codvendaservico.ToString();
            mtbData.Text = lista_VendaServico[posicao].datavs.ToString();

            for (int i = 0; i < clientes.Count; i++)
            {
                if (clientes[i].codcliente == lista_VendaServico[posicao].cliente.codcliente)
                {
                    cmbCliente.SelectedItem = clientes[i].nomecliente;
                    break;
                }
            }

            for (int i = 0; i < funcionarios.Count; i++)
            {
                if (funcionarios[i].codfuncionario == lista_VendaServico[posicao].funcionario.codfuncionario)
                {
                    cmbFuncionario.SelectedItem = funcionarios[i].nomefuncionario;
                    break;
                }
            }

            for (int i = 0; i < animais.Count; i++)
            {
                if (animais[i].codanimal == lista_VendaServico[posicao].animal.codanimal)
                {
                    cmbAnimal.SelectedItem = animais[i].nomeanimal;
                    break;
                }
            }
        }

        public void CarregaTabela()
        {
            C_VendaServico cp = new C_VendaServico();
            DataTable dt = new DataTable();
            dt = cp.Buscar_Todos();
            Tabela_VendaServico = dt;
            dataGridView1.DataSource = Tabela_VendaServico;
        }
        private void AtivaBotoes()
        {
            btnNovo.Enabled = false;
            btnApagar.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            ativarCampos();
            AtivaBotoes();
            nova = true;
        }

        private void ativarCampos()
        {
            mtbData.Enabled = true;
            cmbCliente.Enabled = true;
            cmbFuncionario.Enabled = true;
            cmbAnimal.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            mtbData.Text = "";
            cmbCliente.SelectedIndex = -1;
            cmbFuncionario.SelectedIndex = -1;
            cmbAnimal.SelectedIndex = -1;
        }

        private void desativaCampos()
        {
            mtbData.Enabled = false;
            cmbCliente.Enabled = false;
            cmbFuncionario.Enabled = false;
            cmbAnimal.Enabled = false;
        }

        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validações dos campos obrigatórios
            if (string.IsNullOrEmpty(mtbData.Text) || !DateTime.TryParse(mtbData.Text, out DateTime dataVenda))
            {
                MessageBox.Show("A data da venda é obrigatória e deve ser válida.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o cliente.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbFuncionario.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o funcionário.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbAnimal.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o animal.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Criação do objeto Venda
            VendaServico vendaservico = new VendaServico()
            {
                codvendaservico = nova ? 0 : Convert.ToInt32(txtCodigo.Text),
                datavs = dataVenda,
                cliente = clientes[cmbCliente.SelectedIndex],
                funcionario = funcionarios[cmbFuncionario.SelectedIndex],
                animal = animais[cmbAnimal.SelectedIndex]
            };

            C_VendaServico cv = new C_VendaServico  ();

            try
            {
                if (nova)
                {
                    // Insere nova venda
                    cv.Insere_Dados(vendaservico);
                }
                else
                {
                    // Atualiza venda existente
                    cv.Atualizar_Dados(vendaservico);
                }

                // Atualiza a tabela e limpa os campos
                CarregaTabela();
                desativaCampos();
                desativaBotoes();
                limparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a venda: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            limparCampos();
            ativarCampos();
            AtivaBotoes();
            nova = true;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btnApagar_Click_1(object sender, EventArgs e)
        {
            C_VendaServico cp = new C_VendaServico();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                cp.Apaga_Dados(valor);
                CarregaTabela();
            }
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            AtivaBotoes();
            ativarCampos();
            nova = false;
        }

        private void btnPrimeiro_Click_1(object sender, EventArgs e)
        {
            if (lista_VendaServico.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_VendaServico = carregaListaVendaServico();
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
                lista_VendaServico = carregaListaVendaServico();
            }
        }

        private void btnProximo_Click_1(object sender, EventArgs e)
        {
            if (posicao < lista_VendaServico.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_VendaServico = carregaListaVendaServico();
            }
        }

        private void btnUltimo_Click_1(object sender, EventArgs e)
        {
            if (lista_VendaServico.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_VendaServico.Count - 1;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_VendaServico = carregaListaVendaServico();
            }
        }
        List<VendaServico> carregaListaVendaServicoFiltro()
        {
            List<VendaServico> lista = new List<VendaServico>();

            C_VendaServico cc = new C_VendaServico();
            lista = cc.DadosVendaServicoFiltro(txtBuscar.Text);

            return lista;
        }
        public void CarregaTabelaFiltro(String filtro)
        {
            C_VendaServico cf = new C_VendaServico();
            DataTable dt = new DataTable();
            dt = cf.Buscar_Filtro(filtro);
            Tabela_VendaServico = dt;
            dataGridView1.DataSource = Tabela_VendaServico;
        }
        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            lista_VendaServico = carregaListaVendaServicoFiltro();
            CarregaTabelaFiltro(txtBuscar.Text);

            if (lista_VendaServico.Count > 0)
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
