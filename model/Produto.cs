using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.control;

namespace Veterinaria.model
{
    internal class Produto
    {
        public int codproduto { get; set; }               
        public string nomeproduto { get; set; }           
        public Marca marca { get; set; }                  
        public decimal quantidade { get; set; }            
        public decimal valor { get; set; }                 
        public Tipoproduto tipoproduto { get; set; }      


        public Produto() { }

        // Construtor com parâmetros
        public Produto(int codproduto, string nomeproduto, Marca marca, decimal quantidade, decimal valor, Tipoproduto tipoProduto)
        {
            this.codproduto = codproduto;
            this.nomeproduto = nomeproduto;
            this.marca = marca;
            this.quantidade = quantidade;
            this.valor = valor;
            this.tipoproduto = tipoProduto;
        }
    }
}
