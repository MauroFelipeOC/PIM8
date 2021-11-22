using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPIM8
{
    class Telefone
    {
        protected int _id = -1;
        private int _numero;
        private int _ddd;

        public int ID
        {
            get { return _id; }
            set
            {
                if (_id == -1) _id = value;
            }
        }
        public int Numero { get { return this._numero; } set { this._numero = value; } }
        public int Ddd { get { return this._ddd; } set { this._ddd = value; } }
        public TipoTelefone tipoTelefone;
    }
}
