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
                cmdEndereco.Dispose();
                cmdPessoa.Dispose();
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

        static public Pessoa Consulte(long cpf)
        {         
            SqlConnection conexao = new SqlConnection(@"Server= localhost\SQLEXPRESS; Database= TestePIM; Integrated Security=True;");

            string strSQL = "SELECT * FROM PESSOA WHERE CPF = @CPF";
            SqlCommand cmdConsultePessoa = new SqlCommand(strSQL, conexao);

            cmdConsultePessoa.Parameters.AddWithValue("@CPF", cpf);

            Pessoa p = new Pessoa();
            int idEndereco=0;

            try
            {
                conexao.Open();
                                
                SqlDataReader dataReader;

                dataReader = cmdConsultePessoa.ExecuteReader();

                p.cpf = cpf;

                while (dataReader.Read())
                {
                    p.nome = (string)dataReader["NOME"];
                    p.ID = (int)dataReader["ID"];
                    idEndereco = (int)dataReader["ENDERECO"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Close();
                cmdConsultePessoa.Dispose();
            }
            
            strSQL = "SELECT * FROM ENDERECO WHERE ID = @ID";
            SqlCommand cmdConsulteEndereco = new SqlCommand(strSQL, conexao);

            cmdConsulteEndereco.Parameters.AddWithValue("@ID", idEndereco);

            try
            {
                conexao.Open();
                
                SqlDataReader dataReader;
                dataReader = cmdConsulteEndereco.ExecuteReader();

                p.endereco = new Endereco();                

                while (dataReader.Read())
                {
                    p.endereco.logradouro = (string)dataReader["LOGRADOURO"];
                    p.endereco.numero = (int)dataReader["NUMERO"];
                    p.endereco.cep = (int)dataReader["CEP"];
                    p.endereco.bairro = (string)dataReader["BAIRRO"];
                    p.endereco.cidade = (string)dataReader["CIDADE"];
                    p.endereco.estado = (string)dataReader["UF"];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexao.Close();
                cmdConsulteEndereco.Dispose();
            }

            strSQL = "SELECT * FROM PESSOA_TELEFONE WHERE ID_PESSOA = @ID_PESSOA";
            SqlCommand cmdConsultePessoaTelefone = new SqlCommand(strSQL, conexao);

            cmdConsultePessoaTelefone.Parameters.AddWithValue("@ID_PESSOA", p.ID);

            List<int> idsTelefones = new List<int>();

            try
            {
                conexao.Open();

                SqlDataReader dataReader;
                dataReader = cmdConsultePessoaTelefone.ExecuteReader();
                
                while (dataReader.Read())
                {
                    idsTelefones.Add((int)dataReader["ID_TELEFONE"]);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexao.Close();
                cmdConsulteEndereco.Dispose();
            }

            int tipoTelefone;

            foreach(int id in idsTelefones)
            {
                strSQL = "SELECT * FROM TELEFONE WHERE ID = @ID";
                SqlCommand cmdConsulteTelefone = new SqlCommand(strSQL, conexao);

                cmdConsulteTelefone.Parameters.AddWithValue("@ID", 1);

                Telefone telefone = new Telefone();
                telefone.ID = id;

                try
                {
                    conexao.Open();
                    
                    SqlDataReader dataReader;
                    dataReader = cmdConsulteTelefone.ExecuteReader();

                    while (dataReader.Read())
                    {
                        telefone.numero = Convert.ToInt32(dataReader["NUMERO"]);
                        telefone.ddd = (int)dataReader["DDD"];
                        tipoTelefone = (int)dataReader["TIPO"];
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conexao.Close();
                    cmdConsulteTelefone.Dispose();
                }
                p.telefones.Add(telefone);
            }
            return p;
        }

        public static void Exclua(int id)
        {
            string dataSource = @"Server= localhost\SQLEXPRESS; Database= TestePIM; Integrated Security=True;";
            SqlConnection conexao = new SqlConnection(dataSource);
            try
            {
                conexao.Open();
                string query = "DELETE FROM dbo.Pessoa WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Close();
            }
        }

        public static void Altere(int id, Pessoa pessoa)
        {
            string dataSource = @"Server= localhost\SQLEXPRESS; Database= TestePIM; Integrated Security=True; MultipleActiveResultSets=true";
            SqlConnection conexao = new SqlConnection(dataSource);
            try
            {
                conexao.Open();
                //Buscar Endereço da Pessoa a ser alterada
                string query = "SELECT * FROM dbo.Pessoa WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                int enderecoIdPessoaAlterada = 0;
                if (reader.Read())
                {
                enderecoIdPessoaAlterada = Convert.ToInt32(reader["ENDERECO"].ToString());
                }                

                //Atualizar endereço
                Endereco endereco = pessoa.endereco;
                query = "UPDATE dbo.Endereco SET logradouro=@logradouro, " +
                    "numero=@numero, cep=@cep , bairro=@bairro , cidade=@cidade , uf=@uf WHERE id=@id ";                                  
                cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@logradouro", endereco.logradouro);
                cmd.Parameters.AddWithValue("@numero", endereco.numero);
                cmd.Parameters.AddWithValue("@cep", endereco.cep);
                cmd.Parameters.AddWithValue("@bairro", endereco.bairro);
                cmd.Parameters.AddWithValue("@cidade", endereco.cidade);
                cmd.Parameters.AddWithValue("@uf", endereco.estado);
                cmd.Parameters.AddWithValue("@id", enderecoIdPessoaAlterada);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

                //Atualizar Pessoa
                query = "UPDATE PESSOA SET nome=@nome, cpf=@cpf WHERE id=@id";
                cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@nome", pessoa.nome);
                cmd.Parameters.AddWithValue("@cpf", pessoa.cpf);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
