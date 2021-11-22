using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIM8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Pessoa p = new Pessoa();
                        
            //p.ID = 4;
            p.nome = "Tomate";
            p.cpf = 2453;
            //p.endereco
            p.endereco = new Endereco();
            //p.endereco.ID = 4;
            p.endereco.logradouro = "Rua";
            p.endereco.numero = 1;
            p.endereco.cep = 1234;
            p.endereco.bairro = "Ipiranga";
            p.endereco.cidade = "Paris";
            p.endereco.estado = "AP";
            //tipoTelefone
            Telefone telefone = new Telefone();
            telefone.numero = 32129999;
            telefone.ddd = 86;
            TipoTelefone tipoTelefone = new TipoTelefone();
            tipoTelefone.tipo = "Celular";
            telefone.tipoTelefone = tipoTelefone;
            p.telefones.Add(telefone);

            //PessoaDAO.Insira(p);

            PessoaDAO.Altere(7, p);
            //PessoaDAO.Exclua(8);
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Pessoa p = new Pessoa();
            p = PessoaDAO.Consulte(long.Parse(txtCPF.Text));
            label1.Text = "ID:" + p.ID + " NOME:" + p.nome + " LOGRADOURO:" + p.endereco.logradouro + " Telefone:" + p.telefones[0].numero.ToString();
            //MessageBox.Show(p.cpf.ToString());
        }
    }
}
