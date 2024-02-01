using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_de_Loja
{
    public partial class F_login : Form
    {
        Form1 form;
        DataTable dt = new DataTable();
        public F_login(Form1 f)
        {
            InitializeComponent();
            form = f;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            String username = tb_username.Text;
            String senha = tb_senha.Text;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Usuário ou senha inválidos");
            }
            String sql = String.Format(@"
                             SELECT * FROM 
                                         tb_usuarios
                             WHERE     
                                         T_USERNAME = '{0}'
                             AND
                                         T_SENHA = '{1}'", username, senha);
            dt = Banco.dql(sql);

            if (dt.Rows.Count == 1) 
            {
                form.lb_username.Text = dt.Rows[0].Field<String>("T_USERNAME");
                form.lb_nivel.Text = dt.Rows[0].ItemArray[4].ToString();
                Globais.nivel = int.Parse(dt.Rows[0].Field<Int64>("N_NIVEL").ToString());
                form.pb_logado.Image = Properties.Resources.icons8_marca_de_seleção_emoji_48;
                Globais.logado = true;
                Close();
            }
            else
            {
                MessageBox.Show("Usuario não encontrado");
                tb_username.Focus();
            }

        }
    }
}
