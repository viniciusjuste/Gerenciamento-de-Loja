using System;
using System.Windows.Forms;

namespace Gerenciamento_de_Loja
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            F_login f_Login = new F_login(this);
            f_Login.ShowDialog();
        }

        private void abrirForm(int nivel, Form f)
        {
            if (Globais.logado)
            {
                if (Globais.nivel >= nivel)
                {
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Acesso não permitido");
                }
            }
            else
            {
                MessageBox.Show("É necessário estar logado");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lOGINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_login f_Login = new F_login(this);
            f_Login.ShowDialog();
        }

        private void novoUsuárioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            F_cadastroUsuarios f_CadastroUsuarios = new F_cadastroUsuarios();
            abrirForm(2, f_CadastroUsuarios);
        }

        private void lOGOFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lb_username.Text = "------";
            lb_nivel.Text = "0";
            pb_logado.Image = Properties.Resources.icons8_fechar_janela_48;
            Globais.nivel = 0;
            Globais.logado = false;

        }

        private void cadastroDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_cadastrarProduto f_CadastrarProduto = new F_cadastrarProduto();
            abrirForm(1, f_CadastrarProduto);
        }

        private void gestãoDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_gestaoProdutos f_GestaoProdutos = new F_gestaoProdutos();
            abrirForm(2, f_GestaoProdutos);
        }

        private void cadastrarFuncionáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_cadastroFuncionarios f_CadastroFuncionarios = new F_cadastroFuncionarios();
            abrirForm(2, f_CadastroFuncionarios);
        }

        private void gestãoDeFuncionáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_gestaoFuncionarios f_GestaoFuncionarios = new F_gestaoFuncionarios();
            abrirForm(2, f_GestaoFuncionarios);
        }
    }
}
