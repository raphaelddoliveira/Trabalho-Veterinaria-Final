using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmImagens : Form
    {
        DataTable tabelaImagens;
        bool novo = true;
        int posicao;
        List<Imagens> lista_Imagens = new List<Imagens>();
        List<Produto> produtos = new List<Produto>();

        public FrmImagens()
        {
            InitializeComponent();

            preencheComboProduto();
            CarregaTabela();

            lista_Imagens = carregaListaImagens();

            if (lista_Imagens.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }
        
        private void preencheComboProduto()
        {
            produtos = carregaListaProdutos();
            foreach (Produto produto in produtos)
            {
                cmbProduto.Items.Add(produto.nomeproduto);
            }
        }

        private List<Produto> carregaListaProdutos()
        {
            List<Produto> lista = new List<Produto>();

            // Substitua pelo método de carregamento correto do seu projeto
            C_Produto cp = new C_Produto();
            DataTable dt = cp.Buscar_Todos();

            foreach (DataRow row in dt.Rows)
            {
                Produto produto = new Produto
                {
                    codproduto = Convert.ToInt32(row["codProduto"]),
                    nomeproduto = row["nomeProduto"].ToString()
                };

                lista.Add(produto);
            }

            return lista;
        }
        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }
        private void desativaCampos()
        {
            txtDescricao.Enabled = false;
            cmbProduto.Enabled = false;
            picFoto.Image = null;
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_Imagens[posicao].codimagens.ToString();
            txtDescricao.Text = lista_Imagens[posicao].descricao;
            cmbProduto.SelectedIndex = produtos.FindIndex(p => p.codproduto == lista_Imagens[posicao].produto.codproduto);

            if (lista_Imagens[posicao].foto != null)
            {
                picFoto.Image = ByteArrayToImage(lista_Imagens[posicao].foto);
            }
            else
            {
                picFoto.Image = null;
            }
        }

        private void btncarregarimagem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivos de imagem (*.jpg; *.png)|*.jpg;*.png|Todos os arquivos (*.*)|*.*",
                Title = "Selecionar Imagem"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picFoto.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new System.IO.MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private List<Imagens> carregaListaImagens()
        {
            List<Imagens> lista = new List<Imagens>();

            C_Imagens ci = new C_Imagens();
            lista = ci.DadosImagens();

            return lista;
        }

        public void CarregaTabela()
        {
            C_Imagens ci = new C_Imagens();
            tabelaImagens = ci.Buscar_Todos();
            dataGridView1.DataSource = tabelaImagens;
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                MessageBox.Show("A descrição é obrigatória.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Imagens imagem = new Imagens
            {
                codimagens = novo ? 0 : Convert.ToInt32(txtCodigo.Text),
                descricao = txtDescricao.Text,
                produto = produtos[cmbProduto.SelectedIndex],
                foto = picFoto.Image != null ? ImageToByteArray(picFoto.Image) : null
            };

            C_Imagens ci = new C_Imagens();

            try
            {
                if (novo)
                {
                    ci.Insere_Dados(imagem);
                }
                else
                {
                    ci.Atualizar_Dados(imagem);
                }

                CarregaTabela();
                desativaCampos();
                desativaBotoes();
                limparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a imagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtDescricao.Text = "";
            cmbProduto.SelectedIndex = -1;
            picFoto.Image = null;
        }
        private void ativarCampos()
        {
            txtDescricao.Enabled = true;
            cmbProduto.Enabled = true;
            picFoto.Image = null;
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
            novo = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            C_Imagens cp = new C_Imagens();

            if (txtCodigo.Text != "")
            {
                int valor = Int32.Parse(txtCodigo.Text);
                cp.Apaga_Dados(valor);
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
            if (lista_Imagens.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Imagens = carregaListaImagens();
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
                lista_Imagens = carregaListaImagens();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_Imagens.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Imagens = carregaListaImagens();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_Imagens.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_Imagens.Count - 1;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Imagens = carregaListaImagens();
            }
        }
    }
}
