    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Veterinaria.control;
    using Veterinaria.model;

    namespace Veterinaria.view
    {
        public partial class FrmFuncionario : Form
        {
            List<Tipofuncionario> tiposFuncionario = new List<Tipofuncionario>();
            int posicao_tipoFuncionario;
            int codTipoFuncionario;

            List<Loja> lojas = new List<Loja>();
            int posicao_loja;
            int codLoja;

            DataTable Tabela_Funcionario;
            Boolean novo = true;
            int posicao;
            List<Funcionario> lista_Funcionario = new List<Funcionario>();

            public FrmFuncionario()
            {
                InitializeComponent();
                preencheComboLoja();
                preencheComboTipoFuncionario();

                CarregaTabela();

                lista_Funcionario = carregaListaFuncionario();

                if (lista_Funcionario.Count > 0)
                {
                    posicao = 0;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
                }
            }

            private void preencheComboLoja()
            {
                lojas = carregaListaLoja();

                foreach (Loja loja in lojas)
                {
                    cmbLoja.Items.Add(loja.nomeloja);
                }
            }

            private void preencheComboTipoFuncionario()
            {
                tiposFuncionario = carregaListaTipoFuncionario();

                foreach (Tipofuncionario tipo in tiposFuncionario)
                {
                    cmbTipoFuncionario.Items.Add(tipo.nometipofuncionario);
                }
            }

            List<Loja> carregaListaLoja()
            {
                List<Loja> lista = new List<Loja>();

                C_Loja cl = new C_Loja();
                DataTable dt = cl.Buscar_Todos();

                foreach (DataRow row in dt.Rows)
                {
                    Loja loja = new Loja
                    {
                        codloja = Convert.ToInt32(row["codloja"]),
                        nomeloja = row["nomeloja"].ToString()
                    };

                    lista.Add(loja);
                }

                return lista;
            }

            List<Tipofuncionario> carregaListaTipoFuncionario()
            {
                List<Tipofuncionario> lista = new List<Tipofuncionario>();

                C_TipoFuncionario ct = new C_TipoFuncionario();
                DataTable dt = ct.Buscar_Todos();

                foreach (DataRow row in dt.Rows)
                {
                    Tipofuncionario tipo = new Tipofuncionario
                    {
                        codtipofuncionario = Convert.ToInt32(row["codtipofuncionario"]),
                        nometipofuncionario = row["nometipofuncionario"].ToString()
                    };

                    lista.Add(tipo);
                }

                return lista;
            }

            private void atualizaCampos()
            {
                txtCodigo.Text = lista_Funcionario[posicao].codfuncionario.ToString();
                txtNomefuncionario.Text = lista_Funcionario[posicao].nomefuncionario;

                for (int i = 0; i < lojas.Count; i++)
                {
                    if (lojas[i].codloja == lista_Funcionario[posicao].loja.codloja)
                    {
                        cmbLoja.SelectedItem = lojas[i].nomeloja;
                        break;
                    }
                }

                foreach (Tipofuncionario tipo in tiposFuncionario)
                {
                    if (tipo.codtipofuncionario == lista_Funcionario[posicao].tipofuncionario.codtipofuncionario)
                    {
                        cmbTipoFuncionario.SelectedItem = tipo.nometipofuncionario;
                        break;
                    }
                }
            }

            List<Funcionario> carregaListaFuncionario()
            {
                List<Funcionario> lista = new List<Funcionario>();

                C_Funcionario cf = new C_Funcionario();
                lista = cf.DadosFuncionario();

                return lista;
            }

            public void CarregaTabela()
            {
                C_Funcionario cf = new C_Funcionario();
                DataTable dt = new DataTable();
                dt = cf.Buscar_Todos();
                Tabela_Funcionario = dt;
                dataGridView1.DataSource = Tabela_Funcionario;
            }
            public void CarregaTabelaFiltro(String filtro)
            {
                C_Funcionario cf = new C_Funcionario();
                DataTable dt = new DataTable();
                dt = cf.Buscar_Filtro(filtro);
                Tabela_Funcionario = dt;
                dataGridView1.DataSource = Tabela_Funcionario;
            }

            private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    int index = e.RowIndex;
                    DataGridViewRow dr = dataGridView1.Rows[index];

                    txtCodigo.Text = dr.Cells[0].Value.ToString();
                    txtNomefuncionario.Text = dr.Cells[1].Value.ToString();
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
                txtNomefuncionario.Enabled = true;
                cmbLoja.Enabled = true;
                cmbTipoFuncionario.Enabled = true;
            }

            private void limparCampos()
            {
                txtCodigo.Text = "";
                txtNomefuncionario.Text = "";
                cmbLoja.SelectedIndex = -1;
                cmbTipoFuncionario.SelectedIndex = -1;
            }

            private void desativaCampos()
            {
                txtNomefuncionario.Enabled = false;
                cmbLoja.Enabled = false;
                cmbTipoFuncionario.Enabled = false;
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
            }

            private void btnApagar_Click(object sender, EventArgs e)
            {
                C_Funcionario cf = new C_Funcionario();

                if (txtCodigo.Text != "")
                {
                    int valor = Int32.Parse(txtCodigo.Text);
                    cf.Apaga_Dados(valor);
                    CarregaTabela();
                }
            }

            private void btnEditar_Click(object sender, EventArgs e)
            {
                AtivaBotoes();
                ativarCampos();
                novo = false;
            }

            private void btnPrimeiro_Click(object sender, EventArgs e)
            {
                if (lista_Funcionario.Count > 0)
                {
                    dataGridView1.Rows[posicao].Selected = false;
                    posicao = 0;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
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
                }
            }

            private void btnProximo_Click(object sender, EventArgs e)
            {
                if (posicao < lista_Funcionario.Count - 1)
                {
                    dataGridView1.Rows[posicao].Selected = false;
                    posicao++;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
                }
            }

            private void btnUltimo_Click(object sender, EventArgs e)
            {
                if (lista_Funcionario.Count > 0)
                {
                    dataGridView1.Rows[posicao].Selected = false;
                    posicao = lista_Funcionario.Count - 1;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
                }
            }

            private void btnBuscar_Click(object sender, EventArgs e)
            {
                lista_Funcionario = carregaListaFuncionario();

                if (lista_Funcionario.Count > 0)
                {
                    dataGridView1.DataSource = lista_Funcionario;
                    posicao = 0;
                    atualizaCampos();
                }
                else
                {
                    dataGridView1.DataSource = null;
                    limparCampos();
                }
            }

       

            private void btnSalvar_Click1(object sender, EventArgs e)
            {
                // Verifica se os campos obrigatórios estão preenchidos
                if (string.IsNullOrEmpty(txtNomefuncionario.Text))
                {
                    MessageBox.Show("O nome do funcionário é obrigatório.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verifica se uma loja foi selecionada
                if (cmbLoja.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecione uma loja.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verifica se o tipo de funcionário foi selecionado
                if (cmbTipoFuncionario.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecione o tipo de funcionário.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cria o objeto Funcionario com os dados do formulário
                Funcionario funcionario = new Funcionario()
                {
                    codfuncionario = novo ? 0 : Convert.ToInt32(txtCodigo.Text),  // Se for novo, o código do funcionário é 0
                    nomefuncionario = txtNomefuncionario.Text,
                    loja = lojas[cmbLoja.SelectedIndex],  // Loja selecionada pelo índice
                    tipofuncionario = tiposFuncionario[cmbTipoFuncionario.SelectedIndex]  // Tipo de funcionário selecionado pelo índice
                };

                // Cria o controlador C_Funcionario
                C_Funcionario cf = new C_Funcionario();

                try
                {
                    if (novo)  // Se for um novo funcionário
                    {
                        cf.Insere_Dados(funcionario);
                        MessageBox.Show("Funcionário salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else  // Se for para atualizar um funcionário existente
                    {
                        cf.Atualizar_Dados(funcionario);
                        MessageBox.Show("Funcionário atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Atualizar a tabela após salvar
                    CarregaTabela();  // Método para atualizar a tabela com os funcionários
                    desativaCampos();  // Desativa os campos após salvar
                    desativaBotoes();  // Desativa os botões após salvar

                    // Limpa os campos após o processo de salvar
                    limparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar o funcionário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void btnSalvar_Click_1(object sender, EventArgs e)
            {
                // Verifica se os campos obrigatórios estão preenchidos
                if (string.IsNullOrEmpty(txtNomefuncionario.Text))
                {
                    MessageBox.Show("O nome do funcionário é obrigatório.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verifica se uma loja foi selecionada
                if (cmbLoja.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecione uma loja.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verifica se o tipo de funcionário foi selecionado
                if (cmbTipoFuncionario.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecione o tipo de funcionário.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cria o objeto Funcionario com os dados do formulário
                Funcionario funcionario = new Funcionario()
                {
                    codfuncionario = novo ? 0 : Convert.ToInt32(txtCodigo.Text),  // Se for novo, o código do funcionário é 0
                    nomefuncionario = txtNomefuncionario.Text,
                    loja = lojas[cmbLoja.SelectedIndex],  // Loja selecionada pelo índice
                    tipofuncionario = tiposFuncionario[cmbTipoFuncionario.SelectedIndex]  // Tipo de funcionário selecionado pelo índice
                };

                // Cria o controlador C_Funcionario
                C_Funcionario cf = new C_Funcionario();

                try
                {
                    if (novo)  // Se for um novo funcionário
                    {
                        cf.Insere_Dados(funcionario);
                   
                    }
                    else  // Se for para atualizar um funcionário existente
                    {
                        cf.Atualizar_Dados(funcionario);
                    
                    }

                    // Atualizar a tabela após salvar
                    CarregaTabela();  // Método para atualizar a tabela com os funcionários
                    desativaCampos();  // Desativa os campos após salvar
                    desativaBotoes();  // Desativa os botões após salvar
                
                
                    limparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar o funcionário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            List<Funcionario> carregaListaFuncionarioFiltro()
            {
                List<Funcionario> lista = new List<Funcionario>();

                C_Funcionario cc = new C_Funcionario();
                lista = cc.DadosFuncionarioFiltro(txtBuscar.Text);

                return lista;
            }
            private void btnPrimeiro_Click_1(object sender, EventArgs e)
            {
                if (lista_Funcionario.Count > 0)
                {
                    dataGridView1.Rows[posicao].Selected = false;
                    posicao = 0;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
                    lista_Funcionario = carregaListaFuncionario();
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
                    lista_Funcionario = carregaListaFuncionario();
                }
            }

            private void btnProximo_Click_1(object sender, EventArgs e)
            {
                if (posicao < lista_Funcionario.Count - 1)
                {
                    dataGridView1.Rows[posicao].Selected = false;
                    posicao++;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
                    lista_Funcionario = carregaListaFuncionario();
                }
            }

            private void btnUltimo_Click_1(object sender, EventArgs e)
            {
                if (lista_Funcionario.Count > 0)
                {
                    dataGridView1.Rows[posicao].Selected = false;
                    posicao = lista_Funcionario.Count - 1;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
                    lista_Funcionario = carregaListaFuncionario();
                }
            }

            private void btnBuscar_Click_1(object sender, EventArgs e)
            {
                lista_Funcionario = carregaListaFuncionarioFiltro();
                CarregaTabelaFiltro(txtBuscar.Text);

                if (lista_Funcionario.Count > 0)
                {
                    //dataGridView1.DataSource = lista_Funcionario;
                    posicao = 0;
                    atualizaCampos();
                }
                else
                {
                    dataGridView1.DataSource = null;
                    limparCampos();
                }
            }

            private void btnEditar_Click_1(object sender, EventArgs e)
            {
                AtivaBotoes();
                ativarCampos();
                novo = false;
            }

            private void btnApagar_Click_1(object sender, EventArgs e)
            {
                C_Funcionario c_Funcionario = new C_Funcionario();

                if (txtCodigo.Text != "")
                {
                    int valor = Int32.Parse(txtCodigo.Text);
                    c_Funcionario.Apaga_Dados(valor);
                    CarregaTabela();
                }
            }
        }
    }
