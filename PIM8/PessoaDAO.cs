using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

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

            strSQL = "SELECT IDENT_CURRENT('ENDERECO');";
            SqlCommand cmdIdentCurrentEndereco = new SqlCommand(strSQL, conexao);

            strSQL = "SELECT IDENT_CURRENT('PESSOA');";
            SqlCommand cmdIdentCurrentPessoa = new SqlCommand(strSQL, conexao);

            strSQL = "SELECT IDENT_CURRENT('TELEFONE');";
            SqlCommand cmdIdentCurrentTelefone = new SqlCommand(strSQL, conexao);

            int idEndereco;
            int idPessoa;
            int idTelefone;

            try
            {
                conexao.Open();
                idEndereco = 1 + Convert.ToInt32(cmdIdentCurrentEndereco.ExecuteScalar());
                idPessoa = 1 + Convert.ToInt32(cmdIdentCurrentPessoa.ExecuteScalar());
                idTelefone = 1 + Convert.ToInt32(cmdIdentCurrentTelefone.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmdIdentCurrentEndereco.Dispose();
                cmdIdentCurrentTelefone.Dispose();
                conexao.Close();
            }

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
            cmdPessoa.Parameters.AddWithValue("@NOME", pessoa.nome);
            cmdPessoa.Parameters.AddWithValue("@CPF", pessoa.cpf);
            cmdPessoa.Parameters.AddWithValue("@ENDERECO", idEndereco);

            /*strSQL = "INSERT INTO TELEFONE (NUMERO, DDD, TIPO) VALUES(@NUMERO, @DDD, @TIPO)";
            SqlCommand cmdTelefone = new SqlCommand(strSQL, conexao);
            cmdTelefone.Parameters.AddWithValue("@NUMERO", 32121234);
            cmdTelefone.Parameters.AddWithValue("@DDD", pessoa.telefones[0].ddd);
            cmdTelefone.Parameters.AddWithValue("@TIPO", pessoa.telefones[0].tipoTelefone);

            strSQL = "INSERT INTO PESSOA_TELEFONE (ID_PESSOA, ID_TELEFONE) VALUES(@ID_PESSOA, @ID_TELEFONE)";
            SqlCommand cmdPessoaTelefone = new SqlCommand(strSQL, conexao);
            cmdPessoaTelefone.Parameters.AddWithValue("@ID_PESSOA", idPessoa *//*pessoa.ID*//*);
            cmdPessoaTelefone.Parameters.AddWithValue("@ID_TELEFONE", idTelefone *//*pessoa.telefones[0].ID*//*);*/            

            foreach(Telefone telefone in pessoa.telefones)
            {
                strSQL = "INSERT INTO TELEFONE (NUMERO, DDD, TIPO) VALUES(@NUMERO, @DDD, @TIPO)";
                SqlCommand cmdTelefone = new SqlCommand(strSQL, conexao);
                cmdTelefone.Parameters.AddWithValue("@NUMERO", telefone.numero);
                cmdTelefone.Parameters.AddWithValue("@DDD", telefone.ddd);
                cmdTelefone.Parameters.AddWithValue("@TIPO", 1 /*telefone.tipoTelefone*/);

                strSQL = "INSERT INTO PESSOA_TELEFONE (ID_PESSOA, ID_TELEFONE) VALUES(@ID_PESSOA, @ID_TELEFONE)";
                SqlCommand cmdPessoaTelefone = new SqlCommand(strSQL, conexao);
                cmdPessoaTelefone.Parameters.AddWithValue("@ID_PESSOA", idPessoa /*pessoa.ID*/);
                cmdPessoaTelefone.Parameters.AddWithValue("@ID_TELEFONE", idTelefone /*pessoa.telefones[0].ID*/);

                try
                {
                    conexao.Open();
                    cmdTelefone.ExecuteNonQuery();
                    cmdPessoaTelefone.ExecuteNonQuery();
                    ++idTelefone;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conexao.Close();
                    cmdTelefone.Dispose();
                    cmdPessoaTelefone.Dispose();
                }
            }

            try
            {
                conexao.Open();
                cmdEndereco.ExecuteNonQuery();
                cmdPessoa.ExecuteNonQuery();
                //cmdTelefone.ExecuteNonQuery();
                //cmdPessoaTelefone.ExecuteNonQuery();
                cmdEndereco.Dispose();
                cmdPessoa.Dispose();
                //cmdTelefone.Dispose();
                //cmdPessoaTelefone.Dispose();
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

        static public int Altere(/*Pessoa pessoa*/)
        {
            SqlConnection conexao = new SqlConnection(@"Server= localhost\SQLEXPRESS; Database= TestePIM; Integrated Security=True;");
            string strSQL;

            strSQL = "SELECT IDENT_CURRENT('PESSOA');";
            SqlCommand cmdEndereco = new SqlCommand(strSQL, conexao);

            int retor;
            try
            {
                conexao.Open();
                retor = Convert.ToInt32(cmdEndereco.ExecuteScalar());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cmdEndereco.Dispose();
                conexao.Close();
            }

            return retor;
        }

        static public Pessoa Consulte(long cpf)
        {
            
            
            SqlConnection conexao = new SqlConnection(@"Server= localhost\SQLEXPRESS; Database= TestePIM; Integrated Security=True;");

            string strSQL = "SELECT * FROM PESSOA WHERE CPF = @CPF";
            SqlCommand cmdConsulte = new SqlCommand(strSQL, conexao);

            cmdConsulte.Parameters.AddWithValue("@CPF", cpf);

            Pessoa p = new Pessoa();

            try
            {
                conexao.Open();

                SqlDataAdapter dataAdapter;
                SqlDataReader dataReader;

                dataReader = cmdConsulte.ExecuteReader();

                p.cpf = cpf;

                while (dataReader.Read())
                {
                    p.nome = (string)dataReader["NOME"];
                    p.ID = (int)dataReader["ID"];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexao.Close();
                cmdConsulte.Dispose();
            }

            return p;
        }
    }
}
