using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM8
{
    class Telefone
    {
        protected int id;
        public int ID
        {
            get { return id; }
            set
            {
                if (id == -1) id = value;
            }
        }
        public int numero;
        public int ddd;
        public TipoTelefone tipoTelefone;
    }
}
