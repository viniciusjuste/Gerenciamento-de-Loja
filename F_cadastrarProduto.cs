using System;
using System.IO;
using System.Windows.Forms;

namespace Gerenciamento_de_Loja
{
    public partial class F_cadastrarProduto : Form
    {
        String origemCompleto = "";
        String foto = "";
        String pastaDestino = Globais.caminhoFoto;
        String destinoCompleto = "";
        public F_cadastrarProduto()
        {
            InitializeComponent();
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            tb_nomeProduto.Clear();
            tb_dscProduto.Clear();
            tb_preco.Clear();
            tb_quantidade.Clear();
            pb_fotoProduto.Image = Properties.Resources.add_produto;
            tb_nomeProduto.Focus();
        }

        private void btn_addFoto_Click(object sender, EventArgs e)
        {
            origemCompleto = "";
            foto = "";
            pastaDestino = Globais.caminhoFoto;
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
                    pb_fotoProduto.ImageLocation = destinoCompleto;
                }
                else
                {
                    if (MessageBox.Show("Erro ao localizar foto, deseja continuar ?", "Erro", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(tb_nomeProduto.Text) || string.IsNullOrWhiteSpace(tb_dscProduto.Text) || string.IsNullOrWhiteSpace(tb_preco.Text) || String.IsNullOrWhiteSpace(tb_quantidade.Text))
            {
                MessageBox.Show("Não é possível cadastrar o produto");
                tb_nomeProduto.Focus();
                return;
            }
            String queryCadastroProduto = String.Format(@"
                                     INSERT INTO 
                                               tb_produtos
                                               (T_NOMEPRODUTO, T_DSCPRODUTO,N_QTDPRODUTO, N_PRECO, T_FOTO)
                                     VALUES
                                                ('{0}', '{1}', '{2}', '{3}', '{4}')", tb_nomeProduto.Text, tb_dscProduto.Text, tb_quantidade.Text, tb_preco.Text, destinoCompleto);
            Banco.dml(queryCadastroProduto);
            MessageBox.Show("Produto adicionado");
        }
    }
}
