using System;
using System.Windows.Forms;

namespace Gerenciamento_de_Loja
{
    public partial class F_cadastroUsuarios : Form
    {
        public F_cadastroUsuarios()
        {
            InitializeComponent();
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            tb_nome.Clear();
            tb_username.Clear();
            tb_senha.Clear();
            tb_nivel.Clear();
            tb_nome.Focus();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            Usuarios usuario = new Usuarios();
            usuario.nome = tb_nome.Text;
            usuario.username = tb_username.Text;
            usuario.senha = tb_senha.Text;
            usuario.nivel = Convert.ToInt32(tb_nivel.Text.ToString());
            Banco.NovoUsuario(usuario);

            MessageBox.Show("Usuário criado com sucesso");
        }
    }
}
