using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.control;

namespace Veterinaria.model
{
    internal class ItensVendaServico
    {
        public Tiposervico tipoServico { get; set; }     
        public VendaServico vendaServico { get; set; }    
        public decimal quant { get; set; }                
        public decimal valor { get; set; }                
        public CidAnimal cidAnimal { get; set; }          


        public ItensVendaServico() { }


        public ItensVendaServico(Tiposervico tipoServico, VendaServico vendaServico, decimal quant, decimal valor, CidAnimal cidAnimal)
        {
            this.tipoServico = tipoServico;
            this.vendaServico = vendaServico;
            this.quant = quant;
            this.valor = valor;
            this.cidAnimal = cidAnimal;
        }
    }
}
