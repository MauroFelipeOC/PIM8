using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM8
{
    class Pessoa
    {
        protected int id = -1;
        public int ID
        {
            get { return id; }
            set
            {
                if (id == -1) id = value;
            }
        }
        public string nome;
        public Endereco endereco;
        public long cpf;
        public List<Telefone> telefones = new List<Telefone>();
    }
}
