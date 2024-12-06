    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using Veterinaria.control;
    using Veterinaria.model;

    namespace Veterinaria.view
    {
        public partial class FrmPais : Form
        {
            DataTable Tabela_pais;
            Boolean novo = true;
            int posicao;
            List<Pais> lista_pais = new List<Pais>();

            public FrmPais()
            {
                InitializeComponent();

                // Carregar o Datagrid de Pais.
                CarregaTabela();

                if (lista_pais.Count - 1 > 0)
                {
                    posicao = 0;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
                }
            }

            private void atualizaCampos()
            {
                txtCodigo.Text = lista_pais[posicao].codpais.ToString();
                txtPais.Text = lista_pais[posicao].nomepais.ToString();

                // Carrega a imagem da bandeira a partir do banco de dados
                if (lista_pais[posicao].bandeira != null)
                {
                    using (MemoryStream ms = new MemoryStream(lista_pais[posicao].bandeira))
                    {
                        pictureBoxbandeira.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBoxbandeira.Image = null;
                }
            }

            List<Pais> carregaListaPais()
            {
                List<Pais> lista = new List<Pais>();

                C_Pais cr = new C_Pais();
                lista = cr.DadosPais();

                return lista;
            }

            List<Pais> carregaListaPaisFiltro()
            {
                List<Pais> lista = new List<Pais>();

                C_Pais cr = new C_Pais();
                lista = cr.DadosPaisFiltro(txtBuscar.Text);

                return lista;
            }

            public void CarregaTabela()
            {
                C_Pais cr = new C_Pais();
                DataTable dt = new DataTable();
                dt = cr.Buscar_Todos();
                Tabela_pais = dt;
                dataGridView1.DataSource = Tabela_pais;
                lista_pais = carregaListaPais();
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
                txtPais.Enabled = true;
                btncarregarimagem.Enabled = true; // Ativa o botão de carregar imagem
            }

            private void limparCampos()
            {
                txtCodigo.Text = "";
                txtPais.Text = "";
                pictureBoxbandeira.Image = null; // Limpa a imagem
            }

            private void desativaCampos()
            {
                txtPais.Enabled = false;
                btncarregarimagem.Enabled = false; // Desativa o botão de carregar imagem
            }

            private void btnSalvar_Click(object sender, EventArgs e)
            {
                Pais pais = new Pais();

                pais.nomepais = txtPais.Text;

                // Converte a imagem da bandeira para um array de bytes
                if (pictureBoxbandeira.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBoxbandeira.Image.Save(ms, pictureBoxbandeira.Image.RawFormat);
                        pais.bandeira = ms.ToArray();
                    }
                }

                C_Pais c_Pais = new C_Pais();

                if (novo == true)
                {
                    c_Pais.Insere_Dados(pais);
                }
                else
                {
                    pais.codpais = Int32.Parse(txtCodigo.Text);
                    c_Pais.Atualizar_Dados(pais);
                }

                CarregaTabela();
                lista_pais = carregaListaPais();

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
                C_Pais pais = new C_Pais();

                if (txtCodigo.Text != "")
                {
                    int valor = Int32.Parse(txtCodigo.Text);
                    pais.Apaga_Dados(valor);
                    CarregaTabela();
                    lista_pais = carregaListaPais();
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
                int total = lista_pais.Count - 1;
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
                posicao = lista_pais.Count - 1;
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
                C_Pais cr = new C_Pais();
                DataTable dt = new DataTable();
                dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
                Tabela_pais = dt;

                dataGridView1.DataSource = Tabela_pais;
                lista_pais = carregaListaPaisFiltro();

                if (lista_pais.Count > 0)
                {
                    posicao = 0;
                    atualizaCampos();
                    dataGridView1.Rows[posicao].Selected = true;
                    lista_pais = carregaListaPais();
                }
            }

            private void btncarregarimagem_Click_1(object sender, EventArgs e)
            {

                // Abre o diálogo de arquivo para carregar a imagem
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxbandeira.Image = Image.FromFile(openFileDialog.FileName);
                }
            }

      
        }
    }
