using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PIM8
{
    class Endereco
    {
        protected int id;
        public string logradouro;
        public int numero;
        public int cep;
        public string bairro;
        public string cidade;
        public string estado;
    }
}
