using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_de_Loja
{
    public partial class F_cadastroFuncionarios : Form
    {
        String origemCompleto = "";
        String foto = "";
        String pastaDestino = "";
        String destinoCompleto = "";
        public F_cadastroFuncionarios()
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
            tb_funcao.Clear();
            tb_salario.Clear();
            tb_telefone.Clear();
            pb_fotoFuncionario.Image = Properties.Resources.nome;
        }

        private void btn_addFoto_Click(object sender, EventArgs e)
        {
            origemCompleto = "";
            foto = "";
            pastaDestino = Globais.caminhoFotoFunc;
            destinoCompleto = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                origemCompleto = openFileDialog1.FileName;
                foto = openFileDialog1.SafeFileName;
                destinoCompleto = pastaDestino + foto;

            }

            if (File.Exists(destinoCompleto))
            {
                if (MessageBox.Show("Arquivo já existe, deseja sobrescrever ?", "Sobrescrever", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (destinoCompleto == "")
            {
                MessageBox.Show("Sem foto selecionada");
                return;
            }
            if (destinoCompleto != "")
            {
                System.IO.File.Copy(origemCompleto, destinoCompleto, true);

                if (File.Exists(destinoCompleto))
                {
                    pb_fotoFuncionario.ImageLocation = destinoCompleto;
                }
                else
                {
                    if (MessageBox.Show("Erro ao localizar foto, deseja continuar ?", "Erro", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(tb_nome.Text) || string.IsNullOrWhiteSpace(tb_salario.Text) || string.IsNullOrWhiteSpace(tb_funcao.Text) || string.IsNullOrWhiteSpace(tb_telefone.Text))
            {
                MessageBox.Show("Não é possível cadastrar o funcionário");
                tb_nome.Focus();
                return;
            }
            String queryCadastrarFuncionario = String.Format(@"
                        INSERT INTO
                                tb_funcionarios
                                (T_NOMEFUNCIONARIO, T_TELEFONE, N_SALARIO, T_FUNCAO, T_FOTO)
                        VALUES
                                ('{0}', '{1}', '{2}', '{3}', '{4}')", tb_nome.Text, tb_telefone.Text, tb_salario.Text, tb_funcao.Text, destinoCompleto);
            Banco.dml(queryCadastrarFuncionario);
            MessageBox.Show("Funcionário adicionado");
        }
    }
}
