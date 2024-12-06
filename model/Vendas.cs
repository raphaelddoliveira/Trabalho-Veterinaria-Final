using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class Vendas
    {
        public int codvenda { get; set; }                
        public DateTime datavenda { get; set; }           
        public Cliente cliente { get; set; }              
        public Funcionario funcionario { get; set; }      
        public Loja loja { get; set; }                    

        
        public Vendas() { }

        
        public Vendas(int codvenda, DateTime datavenda, Cliente cliente, Funcionario funcionario, Loja loja)
        {
            this.codvenda = codvenda;
            this.datavenda = datavenda;
            this.cliente = cliente;
            this.funcionario = funcionario;
            this.loja = loja;
        }
    }
}
