using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using daoPessoa;
using ModelPIM8;

namespace PIM8
{
    class YtlDAO
    {
        static public bool Exclua(Pessoa pessoa)
        {
            return true;
        }

        static public bool Insira(Pessoa pessoa)
        {
            SqlConnection conexao = new SqlConnection(@"Server= localhost\SQLEXPRESS; Database= dbPIM8; Integrated Security=True;");
            string strSQL = "INSERT INTO TESTE (NOME) VALUES(@NOME)";
            SqlCommand comando = new SqlCommand(strSQL, conexao);            

            comando.Parameters.AddWithValue("@NOME", pessoa.Nome);
            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }
            return true;
        }

        static public bool Altere(Pessoa pessoa)
        {
            return true;
        }

        static public Pessoa Consulte(long cpf)
        {
            Pessoa p = new Pessoa();
            return p;
        }
    }
}
