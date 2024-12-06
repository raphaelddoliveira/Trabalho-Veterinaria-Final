using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class VendasProdutos
    {
        public Vendas vendas { get; set; }          
        public Produto produto { get; set; }        
        public decimal quantv { get; set; }         
        public decimal valorv { get; set; }         

   
        public VendasProdutos() { }

        public VendasProdutos(Vendas vendas, Produto produto, decimal quantv, decimal valorv)
        {
            this.vendas = vendas;
            this.produto = produto;
            this.quantv = quantv;
            this.valorv = valorv;
        }
    }
}
