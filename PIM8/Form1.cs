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
            Pessoa p = new Pessoa();
            p.ID = 4;
            p.nome = "Fulano";
            p.cpf = 046;            
            InitializeComponent();
            PessoaDAO.Insira(p);
            
        }
    }
}
