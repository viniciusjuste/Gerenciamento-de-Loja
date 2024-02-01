using System;
using System.Data;
using System.Windows.Forms;

namespace Gerenciamento_de_Loja
{
    public partial class F_gestaoProdutos : Form
    {
        String idSelecionado;
        DataTable dtProdutos;
        public F_gestaoProdutos()
        {
            InitializeComponent();
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void F_gestaoProdutos_Load(object sender, EventArgs e)
        {
            string queryDGV = String.Format(@"
                                SELECT
                                     N_IDPRODUTO as 'ID',
                                     T_NOMEPRODUTO as 'Nome',
                                     T_DSCPRODUTO as 'Descrição',
                                     N_QTDPRODUTO as 'Estoque',
                                     ' ' || N_PRECO as 'Preço'
                                FROM tb_produtos");

            dgv_gestaoProdutos.DataSource = Banco.dql(queryDGV);
            dgv_gestaoProdutos.Columns[0].Width = 50;
            dgv_gestaoProdutos.Columns[4].Width = 84;

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            tb_nomeProduto.Clear();
            tb_descricao.Clear();
            tb_precoProduto.Clear();
            tb_quantidade.Clear();
        }

        private void dgv_gestaoProdutos_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.SelectedRows.Count > 0)
            {
                idSelecionado = dgv.Rows[dgv.SelectedRows[0].Index].Cells[0].Value.ToString();

                String queryPreencherCampos = String.Format(@" 
                                SELECT
                                     T_NOMEPRODUTO,
                                     T_DSCPRODUTO,
                                     N_QTDPRODUTO,
                                     ' ' || N_PRECO,
                                     T_FOTO
                                FROM 
                                     tb_produtos
                                WHERE
                                     N_IDPRODUTO = {0}", idSelecionado);

                dtProdutos = Banco.dql(queryPreencherCampos);
                tb_nomeProduto.Text = dtProdutos.Rows[0].Field<String>("T_NOMEPRODUTO");
                tb_descricao.Text = dtProdutos.Rows[0].Field<String>("T_DSCPRODUTO");
                tb_quantidade.Text = dtProdutos.Rows[0].Field<Int64>("N_QTDPRODUTO").ToString();
                tb_precoProduto.Text = dtProdutos.Rows[0].Field<String>("' ' || N_PRECO").ToString();
                pb_fotoProduto.ImageLocation = dtProdutos.Rows[0].Field<String>("T_FOTO");
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir ?", "Excluir", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                String excluirProduto = String.Format(@"
                                   DELETE
                                        FROM tb_produtos
                                   WHERE N_IDPRODUTO = {0}", idSelecionado);
                Banco.dml(excluirProduto);
                dgv_gestaoProdutos.Rows.Remove(dgv_gestaoProdutos.CurrentRow);
                MessageBox.Show("Produto removido");
            }

        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tb_nomeProduto.Text) || String.IsNullOrWhiteSpace(tb_descricao.Text) || String.IsNullOrWhiteSpace(tb_precoProduto.Text) || String.IsNullOrWhiteSpace(tb_quantidade.Text))
            {
                MessageBox.Show("Certifique-se de que nenhum espaço esteja em branco antes de tentar salvar");
                tb_nomeProduto.Focus();
                return;
            }
            String querySalvarAlteracao = String.Format(@"
                                    UPDATE tb_produtos 
                                    SET 
                                            T_NOMEPRODUTO = '{0}',
                                            T_DSCPRODUTO = '{1}',
                                            N_QTDPRODUTO = '{2}',
                                            N_PRECO = ' {3}'
                                    WHERE
                                            N_IDPRODUTO = {4}", tb_nomeProduto.Text, tb_descricao.Text, tb_quantidade.Text, tb_precoProduto.Text, idSelecionado);
            Banco.dml(querySalvarAlteracao);
            MessageBox.Show("Feche o form e abra de novo para vizualizar as alterações");
        }
    }
}
