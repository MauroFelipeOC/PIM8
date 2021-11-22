using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPIM8
{
    class Pessoa
    {
        protected int _id = -1;
        
        private string _nome;
        private long _cpf;

        public int ID
        {
            get { return _id; }
            set
            {
                if (_id == -1) _id = value;
            }
        }
        public string Nome { get { return this._nome; } set { this._nome = value; } }
        public Endereco endereco;
        public long Cpf { get { return this._cpf; } set { this._cpf = value; } }
        public List<Telefone> Telefones = new List<Telefone>();
    }
}
