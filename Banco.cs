using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_de_Loja
{
    internal class Banco
    {
        private static SQLiteConnection conexao;
        private static SQLiteConnection conexaoBanco()
        {
            conexao = new SQLiteConnection("Data source = " + Globais.caminhoBanco + Globais.nomeBanco);
            conexao.Open();
            return conexao;
        }

        public static DataTable dql(String sql)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                var vcon = conexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = sql;

                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                da.Fill(dt);
                vcon.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void dml(String Sql, string msgOK = null, String msgErro = null)
        {
            SQLiteDataAdapter da = null;

            try
            {
                var vcon = conexaoBanco();
                var cmd = vcon.CreateCommand();

                cmd.CommandText = Sql;

                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();

                if (msgOK != null)
                {
                    MessageBox.Show(msgOK);
                }
            }
            catch (Exception ex)
            {
                if (msgErro != null)
                {
                    MessageBox.Show(msgErro + "\n" + ex.Message);
                }

            }
        }

        public static void NovoUsuario(Usuarios u)
        {
            if (ExisteUserName(u))
            {
                MessageBox.Show("Username já existe");
                return;
            }
            try
            {
                var vcon = conexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = "INSERT INTO tb_usuarios (T_NOME, T_USERNAME, T_SENHA, N_NIVEL) VALUES (@nome,@username,@senha,@nivel)";
                cmd.Parameters.AddWithValue("@nome", u.nome);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("senha", u.senha);
                cmd.Parameters.AddWithValue("@nivel", u.nivel);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuário adicionado com sucesso");
                vcon.Close();

            }
            catch
            {
                MessageBox.Show("Erro ao adicionar usuário");
            }
        }

        public static bool ExisteUserName(Usuarios u)
        {
            bool res;
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            var vcon = conexaoBanco();
            var cmd = vcon.CreateCommand();
            cmd.CommandText = "SELECT T_USERNAME FROM tb_usuarios WHERE T_USERNAME = '" + u.username + "'";
            da = new SQLiteDataAdapter(cmd.CommandText, vcon);
            da.Fill(dt);

            if (dt.Rows.Count > 1)
            {
                res = true;
            }
            else
            {
                res = false;
            }

            vcon.Close();
            return res;
        }

    }
}
