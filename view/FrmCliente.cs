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
    public partial class FrmCliente : Form
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

        DataTable Tabela_Cliente;
        Boolean novo = true;
        int posicao;
        List<Cliente> lista_Cliente = new List<Cliente>();

        public FrmCliente()
        {
            InitializeComponent();
            preencheComboBairro();
            preencheComboRua();
            preencheComboCidade();
            preencheComboEstado();
            preencheComboPais();
            preencheComboCep();

            CarregaTabela();

            lista_Cliente = carregaListaCliente();

            if (lista_Cliente.Count > 0)
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
            txtCodigo.Text = lista_Cliente[posicao].codcliente.ToString();
            txtNumero.Text = lista_Cliente[posicao].numerocasa.ToString();
            txtCliente.Text = lista_Cliente[posicao].nomecliente.ToString();
            txtcpf.Text = lista_Cliente[posicao].cpf.ToString();

            if (lista_Cliente[posicao].fotocliente != null)
            {
                using (MemoryStream ms = new MemoryStream(lista_Cliente[posicao].fotocliente))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pictureBox1.Image = null;
            }

            for (int i = 0; i < bairros.Count; i++)
            {
                if (bairros[i].codbairro == lista_Cliente[posicao].bairro.codbairro)
                {
                    cmbBairro.SelectedItem = bairros[i].nomebairro;
                    break;
                }
            }

            foreach (Rua ru in ruas)
            {
                if (ru.codrua == lista_Cliente[posicao].rua.codrua)
                {
                    cmbRua.SelectedItem = ru.nomerua;
                    break;
                }
            }

            foreach (Cidade ci in cidades)
            {
                if (ci.codcidade == lista_Cliente[posicao].cidade.codcidade)
                {
                    cmbCidade.SelectedItem = ci.nomecidade;
                    break;
                }
            }

            foreach (Estado es in estados)
            {
                if (es.codestado == lista_Cliente[posicao].estado.codestado)
                {
                    cmbEstado.SelectedItem = es.nomeestado;
                    break;
                }
            }

            foreach (Pais pa in paises)
            {
                if (pa.codpais == lista_Cliente[posicao].pais.codpais)
                {
                    cmbPais.SelectedItem = pa.nomepais;
                    break;
                }
            }

            foreach (Cep ce in ceps)
            {
                if (ce.codcep == lista_Cliente[posicao].cep.codcep)
                {
                    cmbCEP.SelectedItem = ce.numerocep;
                    break;
                }
            }
        }

        List<Cliente> carregaListaCliente()
        {
            List<Cliente> lista = new List<Cliente>();

            C_Cliente cc = new C_Cliente();
            lista = cc.DadosCliente();

            return lista;
        }

        List<Cliente> carregaListaClienteFiltro()
        {
            List<Cliente> lista = new List<Cliente>();

            C_Cliente cc = new C_Cliente();
            lista = cc.DadosClienteFiltro(txtBuscar.Text);

            return lista;
        }

        public void CarregaTabela()
        {
            C_Cliente cc = new C_Cliente();
            DataTable dt = new DataTable();
            dt = cc.Buscar_Todos();
            Tabela_Cliente = dt;
            dataGridView1.DataSource = Tabela_Cliente;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                DataGridViewRow dr = dataGridView1.Rows[index];

                txtCodigo.Text = dr.Cells[0].Value.ToString();
                txtCliente.Text = dr.Cells[1].Value.ToString();
                txtcpf.Text = dr.Cells[2].Value.ToString();
                cmbRua.SelectedItem = dr.Cells[3].Value.ToString();
                txtNumero.Text = dr.Cells[4].Value.ToString();
                cmbBairro.SelectedItem = dr.Cells[5].Value.ToString();
                cmbCidade.SelectedItem = dr.Cells[6].Value.ToString();
                cmbCEP.SelectedItem = dr.Cells[7].Value.ToString();
                cmbEstado.SelectedItem = dr.Cells[8].Value.ToString();
                cmbPais.SelectedItem = dr.Cells[9].Value.ToString();

                // Supondo que a imagem esteja na coluna 10
                if (dr.Cells[10].Value != DBNull.Value)
                {
                    byte[] imgData = (byte[])dr.Cells[10].Value;
                    using (MemoryStream ms = new MemoryStream(imgData))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
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
            btnCarregarImagem.Enabled = true;
        }

        private void ativarCampos()
        {
            txtcpf.Enabled = true;
            txtCliente.Enabled = true;
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
            txtCliente.Text = "";
            txtcpf.Text = "";
            cmbBairro.SelectedIndex = -1;
            cmbRua.SelectedIndex = -1;
            cmbCidade.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;
            cmbPais.SelectedIndex = -1;
            cmbCEP.SelectedIndex = -1;
            pictureBox1.Image = null;
        }

        private void desativaCampos()
        {
            txtcpf.Enabled = false;
            txtCliente.Enabled = false;
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
        
            byte[] imageBytes = null;
            if (pictureBox1.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    imageBytes = ms.ToArray();
                }
            }

            if (novo)
            {
                // Cria um novo cliente e configura suas propriedades
                Cliente cliente = new Cliente()
                {
                    nomecliente = txtCliente.Text,
                    cpf = txtcpf.Text,
                    numerocasa = txtNumero.Text,
                    rua = ruas[posicao_rua],
                    bairro = bairros[posicao_bairro],
                    cidade = cidades[posicao_cidade],
                    estado = estados[posicao_estado],
                    pais = paises[posicao_pais],
                    cep = ceps[posicao_cep],
                    fotocliente = imageBytes // Adiciona a foto do cliente
                };

                // Salva o novo cliente no banco de dados
                C_Cliente cc = new C_Cliente();
                cc.InserirCliente(cliente);
            }
            else
            {
                // Atualiza um cliente existente
                Cliente cliente = new Cliente()
                {
                    codcliente = Convert.ToInt32(txtCodigo.Text),
                    nomecliente = txtCliente.Text,
                    cpf = txtcpf.Text,
                    numerocasa = txtNumero.Text,
                    rua = ruas[posicao_rua],
                    bairro = bairros[posicao_bairro],
                    cidade = cidades[posicao_cidade],
                    estado = estados[posicao_estado],
                    pais = paises[posicao_pais],
                    cep = ceps[posicao_cep],
                    fotocliente = imageBytes // Adiciona a foto do cliente
                };

                // Atualiza o cliente no banco de dados
                C_Cliente cc = new C_Cliente();
                cc.AtualizarCliente(cliente);
            }

            // Atualiza a tabela e os campos
            CarregaTabela();
            desativaCampos();
            desativaBotoes();
        }

        

        private void btnCarregarImagem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Selecione uma imagem";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        byte[] imageBytes = ms.ToArray();

                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpar os campos de texto
            txtCodigo.Clear();
            txtCliente.Clear();
            txtcpf.Clear();
            cmbRua.SelectedItem = null;
            txtNumero.Clear();
            cmbBairro.SelectedItem = null;
            cmbCidade.SelectedItem = null;
            cmbCEP.SelectedItem = null;
            cmbEstado.SelectedItem = null;
            cmbPais.SelectedItem = null;

            // Limpar a imagem (se houver)
            pictureBox1.Image = null;
        }


        private void btnApagar_Click(object sender, EventArgs e)
            {
                C_Cliente c_Cliente = new C_Cliente();

                if (txtCodigo.Text != "")
                {
                    int valor = Int32.Parse(txtCodigo.Text);
                    c_Cliente.Apaga_Dados(valor);
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
            if (lista_Cliente.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Cliente = carregaListaCliente();
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
                lista_Cliente = carregaListaCliente();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (posicao < lista_Cliente.Count - 1)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Cliente = carregaListaCliente();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (lista_Cliente.Count > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_Cliente.Count - 1;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
                lista_Cliente = carregaListaCliente();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lista_Cliente = carregaListaClienteFiltro();

            if (lista_Cliente.Count > 0)
            {
                dataGridView1.DataSource = lista_Cliente;
                posicao = 0;
                atualizaCampos();
            }
            else
            {
                dataGridView1.DataSource= null;
                limparCampos();
            }
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {

        }
    }
}
