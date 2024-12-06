using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.control;

namespace Veterinaria.model
{
    internal class Imagens
    {
        public int codimagens { get; set; }         // Identificador único da imagem
        public string descricao { get; set; }        // Descrição da imagem
        public byte[] foto { get; set; }             // Imagem em formato binário
        public Produto produto { get; set; }         // Referência ao produto relacionado

        public Imagens() { }

        public Imagens(int codimagens, string descricao, byte[] foto, Produto produto)
        {
            this.codimagens = codimagens;
            this.descricao = descricao;
            this.foto = foto;
            this.produto = produto;
        }
    }
}
