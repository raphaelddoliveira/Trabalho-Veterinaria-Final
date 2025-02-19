﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.model
{
    /*
     cliente = {codcliente, nomecliente, cpf, codbairrofk, codruafk, 
     codcepfk, codcidadefk, codestadofk, codpaisfk, numerocasa, fotocliente}
    */
    internal class Cliente
    {
        public int codcliente { get; set; }
        public String nomecliente { get; set; }
        public String cpf { get; set; }
        public Bairro bairro { get; set; }
        public string nomebairro;
        public Rua rua { get; set; }
        public string nomerua;
        public Cep cep { get; set; }
        public string numerocep;
        public Cidade cidade { get; set; }
        public string nomecidade;
        public Estado estado { get; set; }
        public string nomeestado;
        public Pais pais { get; set; }
        public string nomepais;
        public String numerocasa { get; set; }
        public Byte[] fotocliente { get; set; }


        public Cliente() { }

        public Cliente(int codcliente, string nomecliente, string cpf, Bairro bairro, Rua rua, Cep cep, Cidade cidade, Estado estado, Pais pais, string numeroca, byte[] fotocliente)
        {
            this.codcliente = codcliente;
            this.nomecliente = nomecliente;
            this.cpf = cpf;
            this.bairro = bairro;
            this.rua = rua;
            this.cep = cep;
            this.cidade = cidade;
            this.estado = estado;
            this.pais = pais;
            this.numerocasa = numeroca;
            this.fotocliente = fotocliente;
        }
    }
}