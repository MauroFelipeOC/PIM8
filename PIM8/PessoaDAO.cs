using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PIM8
{
    class PessoaDAO
    {        
        static public bool Exclua(Pessoa pessoa)
        {
            return true;
        }

        static public bool Insira(Pessoa pessoa)
        {
            SqlConnection conexao = new SqlConnection(@"Server= localhost\SQLEXPRESS; Database= TestePIM; Integrated Security=True;");
            string strSQL = "INSERT INTO PESSOA (NOME, CPF, ENDERECO) VALUES(@NOME, @CPF, @ENDERECO)";
            SqlCommand cmdPessoa = new SqlCommand(strSQL, conexao);
            SqlDataAdapter da;
            SqlDataReader dr;

            //comando.Parameters.AddWithValue("@ID", pessoa.ID);
            cmdPessoa.Parameters.AddWithValue("@NOME", pessoa.nome);
            cmdPessoa.Parameters.AddWithValue("@CPF", pessoa.cpf);
            cmdPessoa.Parameters.AddWithValue("@ENDERECO", 4);

            strSQL = "INSERT INTO ENDERECO (LOGRADOURO, NUMERO, CEP, BAIRRO, CIDADE, UF) VALUES(@LOGRADOURO, @NUMERO, @CEP, @BAIRRO, @CIDADE, @UF)";
            SqlCommand cmdEndereco = new SqlCommand(strSQL, conexao);
            cmdEndereco.Parameters.AddWithValue("@LOGRADOURO", pessoa.endereco.logradouro);
            cmdEndereco.Parameters.AddWithValue("@NUMERO", pessoa.endereco.numero);
            cmdEndereco.Parameters.AddWithValue("@CEP", pessoa.endereco.cep);
            cmdEndereco.Parameters.AddWithValue("@BAIRRO", pessoa.endereco.cidade);
            cmdEndereco.Parameters.AddWithValue("@UF", pessoa.endereco.estado);

            strSQL = "INSERT INTO PESSOA_TELEFONE (ID_PESSOA, ID_TELEFONE) VALUES(@ID_PESSOA, @ID_TELEFONE)";
            SqlCommand cmdPessoaTelefone = new SqlCommand(strSQL, conexao);
            cmdPessoaTelefone.Parameters.AddWithValue("@ID_PESSOA", pessoa.ID);
            cmdPessoaTelefone.Parameters.AddWithValue("@ID_TELEFONE", pessoa.telefones[0].ID);

            strSQL = "INSERT INTO PESSOA_TELEFONE (NUMERO, DDD, TIPO) VALUES(@NUMERO, @DDD, @TIPO)";
            SqlCommand cmdTelefone = new SqlCommand(strSQL, conexao);
            cmdTelefone.Parameters.AddWithValue("@NUMERO", pessoa.ID);
            cmdTelefone.Parameters.AddWithValue("@DDD", pessoa.telefones[0].ID);
            cmdTelefone.Parameters.AddWithValue("@TIPO", pessoa.telefones[0].ID);

            try
            {
                conexao.Open();
                cmdPessoa.ExecuteNonQuery();
                //cmdEndereco.ExecuteNonQuery();
                //cmdPessoaTelefone.ExecuteNonQuery();
                //cmdTelefone.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }
            conexao = null;



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
