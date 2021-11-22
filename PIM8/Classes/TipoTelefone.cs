using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPIM8
{
    class TipoTelefone
    {
        protected int _id = -1;
        private string _tipo;
        
        public int ID { get { return this._id; } set { this._id = value; } }
        public string Tipo { get { return this._tipo; } set { this._tipo = value; } }
    }
}
