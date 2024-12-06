using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    internal class VendaServico
    {
        public int codvendaservico { get; set; }          
        public Funcionario funcionario { get; set; }      
        public DateTime datavs { get; set; }              
        public Cliente cliente { get; set; }              
        public Animal animal { get; set; }               

        public VendaServico()
        {
        }

     
        public VendaServico(int codvendaservico, Funcionario funcionario, DateTime datavs, Cliente cliente, Animal animal)
        {
            this.codvendaservico = codvendaservico;
            this.funcionario = funcionario;
            this.datavs = datavs;
            this.cliente = cliente;
            this.animal = animal;
        }
    }
}
