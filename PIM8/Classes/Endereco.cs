using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ModelPIM8
{
    class Endereco
    {
        protected int _id;
        private string _logradouro;
        private int _numero;
        private int _cep;
        private string _bairro;
        private string _cidade;
        private string _estado;

        public int ID { get { return this._id; } set { this._id = value; } }
        public string Logradouro { get { return this._logradouro; } set { this._logradouro = value; } }
        public int Numero { get { return this._numero; } set { this._numero = value; } }
        public int Cep { get { return this._cep; } set { this._cep = value; } }
        public string Bairro { get { return this._bairro; } set { this._bairro = value; } }
        public string Cidade { get { return this._cidade; } set { this._cidade = value; } }
        public string Estado { get { return this._estado; } set { this._estado = value; } }

    }
}
