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
            string strSQL;

            strSQL = "INSERT INTO ENDERECO (LOGRADOURO, NUMERO, CEP, BAIRRO, CIDADE, UF) VALUES(@LOGRADOURO, @NUMERO, @CEP, @BAIRRO, @CIDADE, @UF)";
            SqlCommand cmdEndereco = new SqlCommand(strSQL, conexao);
            cmdEndereco.Parameters.AddWithValue("@LOGRADOURO", pessoa.endereco.logradouro);
            cmdEndereco.Parameters.AddWithValue("@NUMERO", pessoa.endereco.numero);
            cmdEndereco.Parameters.AddWithValue("@CEP", pessoa.endereco.cep);
            cmdEndereco.Parameters.AddWithValue("@BAIRRO", pessoa.endereco.bairro);
            cmdEndereco.Parameters.AddWithValue("@CIDADE", pessoa.endereco.cidade);
            cmdEndereco.Parameters.AddWithValue("@UF", pessoa.endereco.estado);

            strSQL = "INSERT INTO PESSOA (NOME, CPF, ENDERECO) VALUES(@NOME, @CPF, @ENDERECO)";
            SqlCommand cmdPessoa = new SqlCommand(strSQL, conexao);
            //comando.Parameters.AddWithValue("@ID", pessoa.ID);
            cmdPessoa.Parameters.AddWithValue("@NOME", pessoa.nome);
            cmdPessoa.Parameters.AddWithValue("@CPF", pessoa.cpf);
            cmdPessoa.Parameters.AddWithValue("@ENDERECO", 4);

            /*strSQL = "INSERT INTO TELEFONE_TIPO (TIPO) VALUES(@TIPO)";
            SqlCommand cmdTelefoneTipo = new SqlCommand(strSQL, conexao);
            cmdTelefoneTipo.Parameters.AddWithValue("@TIPO", 1);*/
            

            strSQL = "INSERT INTO TELEFONE (NUMERO, DDD, TIPO) VALUES(@NUMERO, @DDD, @TIPO)";
            SqlCommand cmdTelefone = new SqlCommand(strSQL, conexao);
            cmdTelefone.Parameters.AddWithValue("@NUMERO", 32121234);
            cmdTelefone.Parameters.AddWithValue("@DDD", pessoa.telefones[0].ddd);
            cmdTelefone.Parameters.AddWithValue("@TIPO", 1 /*pessoa.telefones[0].tipoTelefone*/);

            strSQL = "INSERT INTO PESSOA_TELEFONE (ID_PESSOA, ID_TELEFONE) VALUES(@ID_PESSOA, @ID_TELEFONE)";
            SqlCommand cmdPessoaTelefone = new SqlCommand(strSQL, conexao);
            cmdPessoaTelefone.Parameters.AddWithValue("@ID_PESSOA", 4 /*pessoa.ID*/);
            cmdPessoaTelefone.Parameters.AddWithValue("@ID_TELEFONE", 4 /*pessoa.telefones[0].ID*/);            

            try
            {
                conexao.Open();
                cmdEndereco.ExecuteNonQuery();
                cmdPessoa.ExecuteNonQuery();
                cmdTelefone.ExecuteNonQuery();
                cmdPessoaTelefone.ExecuteNonQuery();
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
