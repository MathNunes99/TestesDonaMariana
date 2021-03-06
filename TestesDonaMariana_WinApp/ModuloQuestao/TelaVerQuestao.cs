using System;
using System.Windows.Forms;

namespace TestesDonaMariana_WinApp.ModuloQuestao
{
    public partial class TelaVerQuestao : Form
    {
        Materia materia;
        public TelaVerQuestao(Materia materiaSelecionada)
        {
            InitializeComponent();
            materia = materiaSelecionada;
            CarregarQuestoes();
            
        }        

        private void CarregarQuestoes()
        {
            listQuestoes.Items.Clear();
            
            foreach (Questao questao in materia.Questoes)
            {                
                listQuestoes.Items.Add(questao);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Questao questaoSelecionada = (Questao)listQuestoes.SelectedItem;

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma Questão primeiro",
                "Edição de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCriarQuestao tela = new TelaCriarQuestao();

            tela.Questao = questaoSelecionada;

            tela.ShowDialog();

            CarregarQuestoes();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Questao questaoSelecionada = (Questao)listQuestoes.SelectedItem;

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma Questão primeiro",
                "Edição de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (Questao questao in materia.Questoes)
            {
                if (questao.titulo == questaoSelecionada.titulo)
                {
                    materia.Questoes.Remove(questao);
                    CarregarQuestoes();
                    return;
                }
            }
            
        }
    }
}
