﻿namespace Veterinaria.view
{
    partial class FrmFuncionario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNovo = new System.Windows.Forms.ToolStripButton();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnApagar = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.btnAnterior = new System.Windows.Forms.ToolStripButton();
            this.btnProximo = new System.Windows.Forms.ToolStripButton();
            this.btnUltimo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblBuscar = new System.Windows.Forms.ToolStripLabel();
            this.txtBuscar = new System.Windows.Forms.ToolStripTextBox();
            this.btnBuscar = new System.Windows.Forms.ToolStripButton();
            this.txtNomefuncionario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipoFuncionario = new System.Windows.Forms.ComboBox();
            this.cmbLoja = new System.Windows.Forms.ComboBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovo,
            this.btnSalvar,
            this.btnCancelar,
            this.btnApagar,
            this.btnEditar,
            this.toolStripSeparator1,
            this.btnPrimeiro,
            this.btnAnterior,
            this.btnProximo,
            this.btnUltimo,
            this.toolStripSeparator2,
            this.lblBuscar,
            this.txtBuscar,
            this.btnBuscar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(587, 39);
            this.toolStrip1.TabIndex = 35;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNovo
            // 
            this.btnNovo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNovo.Image = global::Veterinaria.Properties.Resources.botao_adicionar;
            this.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(36, 36);
            this.btnNovo.Text = "Novo";
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.Image = global::Veterinaria.Properties.Resources.salvar;
            this.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(36, 36);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click_1);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = global::Veterinaria.Properties.Resources.cancelar;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(36, 36);
            this.btnCancelar.Text = "Cancelar";
            // 
            // btnApagar
            // 
            this.btnApagar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnApagar.Image = global::Veterinaria.Properties.Resources.apagar;
            this.btnApagar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApagar.Name = "btnApagar";
            this.btnApagar.Size = new System.Drawing.Size(36, 36);
            this.btnApagar.Text = "Apagar";
            this.btnApagar.Click += new System.EventHandler(this.btnApagar_Click_1);
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.Image = global::Veterinaria.Properties.Resources.editar;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(36, 36);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btnPrimeiro
            // 
            this.btnPrimeiro.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrimeiro.Image = global::Veterinaria.Properties.Resources.primeiro;
            this.btnPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrimeiro.Name = "btnPrimeiro";
            this.btnPrimeiro.Size = new System.Drawing.Size(36, 36);
            this.btnPrimeiro.Text = "Primeiro";
            this.btnPrimeiro.Click += new System.EventHandler(this.btnPrimeiro_Click_1);
            // 
            // btnAnterior
            // 
            this.btnAnterior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAnterior.Image = global::Veterinaria.Properties.Resources.seta_esquerda;
            this.btnAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(36, 36);
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click_1);
            // 
            // btnProximo
            // 
            this.btnProximo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProximo.Image = global::Veterinaria.Properties.Resources.seta_direita;
            this.btnProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(36, 36);
            this.btnProximo.Text = "Próximo";
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click_1);
            // 
            // btnUltimo
            // 
            this.btnUltimo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUltimo.Image = global::Veterinaria.Properties.Resources.ultimo;
            this.btnUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUltimo.Name = "btnUltimo";
            this.btnUltimo.Size = new System.Drawing.Size(36, 36);
            this.btnUltimo.Text = "Último";
            this.btnUltimo.Click += new System.EventHandler(this.btnUltimo_Click_1);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // lblBuscar
            // 
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(42, 36);
            this.lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(140, 39);
            // 
            // btnBuscar
            // 
            this.btnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBuscar.Image = global::Veterinaria.Properties.Resources.ampliacao;
            this.btnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(36, 36);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click_1);
            // 
            // txtNomefuncionario
            // 
            this.txtNomefuncionario.Enabled = false;
            this.txtNomefuncionario.Location = new System.Drawing.Point(106, 67);
            this.txtNomefuncionario.Name = "txtNomefuncionario";
            this.txtNomefuncionario.Size = new System.Drawing.Size(456, 20);
            this.txtNomefuncionario.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Nome Funcionário:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Location = new System.Drawing.Point(45, 41);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(86, 20);
            this.txtCodigo.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Código:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 103);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(562, 179);
            this.dataGridView1.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Cod Tipo Funcionário:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(367, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Cod Loja:";
            // 
            // cmbTipoFuncionario
            // 
            this.cmbTipoFuncionario.Enabled = false;
            this.cmbTipoFuncionario.FormattingEnabled = true;
            this.cmbTipoFuncionario.Location = new System.Drawing.Point(272, 41);
            this.cmbTipoFuncionario.Name = "cmbTipoFuncionario";
            this.cmbTipoFuncionario.Size = new System.Drawing.Size(77, 21);
            this.cmbTipoFuncionario.TabIndex = 40;
            // 
            // cmbLoja
            // 
            this.cmbLoja.Enabled = false;
            this.cmbLoja.FormattingEnabled = true;
            this.cmbLoja.Location = new System.Drawing.Point(425, 40);
            this.cmbLoja.Name = "cmbLoja";
            this.cmbLoja.Size = new System.Drawing.Size(107, 21);
            this.cmbLoja.TabIndex = 41;
            // 
            // FrmFuncionario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 340);
            this.Controls.Add(this.cmbLoja);
            this.Controls.Add(this.cmbTipoFuncionario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtNomefuncionario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmFuncionario";
            this.Text = "FrmFuncionario";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNovo;
        private System.Windows.Forms.ToolStripButton btnSalvar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnApagar;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnPrimeiro;
        private System.Windows.Forms.ToolStripButton btnAnterior;
        private System.Windows.Forms.ToolStripButton btnProximo;
        private System.Windows.Forms.ToolStripButton btnUltimo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblBuscar;
        private System.Windows.Forms.ToolStripTextBox txtBuscar;
        private System.Windows.Forms.ToolStripButton btnBuscar;
        private System.Windows.Forms.TextBox txtNomefuncionario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTipoFuncionario;
        private System.Windows.Forms.ComboBox cmbLoja;
    }
}