using System;
using System.Data;
using System.Windows.Forms;

namespace Gerenciamento_de_Loja
{
    public partial class F_gestaoFuncionarios : Form
    {
        String idSelecionado;
        DataTable dtFuncionarios;
        public F_gestaoFuncionarios()
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

        private void F_gestaoFuncionarios_Load(object sender, EventArgs e)
        {
            String QueryDGV = String.Format(@"   
                            SELECT
                                    N_IDFUNCIONARIO as 'ID',
                                    T_NOMEFUNCIONARIO as 'Nome',
                                    T_TELEFONE as 'Telefone',
                                    ' ' || N_SALARIO as 'Salário',
                                    T_FUNCAO as 'Função'
                            FROM tb_funcionarios");

            dgv_gestaoFuncionarios.DataSource = Banco.dql(QueryDGV);
            dgv_gestaoFuncionarios.Columns[0].Width = 50;
            dgv_gestaoFuncionarios.Columns[4].Width = 84;
        }

        private void dgv_gestaoFuncionarios_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.SelectedRows.Count > 0)
            {
                idSelecionado = dgv.Rows[dgv.SelectedRows[0].Index].Cells[0].Value.ToString();

                String queryPreencherCampos = String.Format(@" 
                              SELECT
                                     N_IDFUNCIONARIO,
                                     T_NOMEFUNCIONARIO,
                                     T_TELEFONE,
                                     ' ' || N_SALARIO,
                                     T_FUNCAO,
                                     T_FOTO
                                FROM tb_funcionarios
                                WHERE
                                     N_IDFUNCIONARIO = {0}", idSelecionado);

                dtFuncionarios = Banco.dql(queryPreencherCampos);
                tb_nome.Text = dtFuncionarios.Rows[0].Field<String>("T_NOMEFUNCIONARIO");
                tb_telefone.Text = dtFuncionarios.Rows[0].Field<String>("T_TELEFONE");
                tb_salario.Text = dtFuncionarios.Rows[0].Field<String>("' ' || N_SALARIO").ToString();
                tb_funcao.Text = dtFuncionarios.Rows[0].Field<String>("T_FUNCAO");
                pb_fotoFuncionario.ImageLocation = dtFuncionarios.Rows[0].Field<String>("T_FOTO");
            }
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tb_nome.Text) || String.IsNullOrWhiteSpace(tb_telefone.Text) || String.IsNullOrWhiteSpace(tb_funcao.Text) || String.IsNullOrWhiteSpace(tb_salario.Text))
            {
                MessageBox.Show("Certifique-se de que nenhum espaço esteja em branco antes de tentar salvar");
                tb_nome.Focus();
                return;
            }
            String querySalvarAlteracao = String.Format(@"
                                    UPDATE tb_funcionarios 
                                    SET 
                                            T_NOMEFUNCIONARIO = '{0}',
                                            T_TELEFONE = '{1}',
                                            N_SALARIO = ' {2}',
                                            T_FUNCAO = '{3}'
                                    WHERE
                                            N_IDFUNCIONARIO = {4}", tb_nome.Text, tb_telefone.Text, tb_salario.Text, tb_funcao.Text, idSelecionado);
            Banco.dml(querySalvarAlteracao);
            MessageBox.Show("Feche o form e abra de novo para vizualizar as alterações");
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir ?", "Excluir", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                String excluirProduto = String.Format(@"
                                   DELETE
                                        FROM tb_funcionarios
                                   WHERE N_IDFUNCIONARIO = {0}", idSelecionado);
                Banco.dml(excluirProduto);
                dgv_gestaoFuncionarios.Rows.Remove(dgv_gestaoFuncionarios.CurrentRow);
                MessageBox.Show("Funcionário removido");
            }
        }
    }
}
