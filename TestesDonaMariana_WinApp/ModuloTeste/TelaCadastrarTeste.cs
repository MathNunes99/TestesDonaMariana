using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestesDonaMariana_WinApp.ModuloQuestao;

namespace TestesDonaMariana_WinApp.ModuloTeste
{
    public partial class TelaCadastrarTeste : Form
    {
        private List<Disciplina> disciplinas;
        private List<Materia> materias;
        public TelaCadastrarTeste(List<Disciplina> disciplinas, List<Materia> materias)
        {
            InitializeComponent();
            this.disciplinas = disciplinas;
            this.materias = materias;
            CarregarComboBox();
            CarregarListaQuestoes();
        }        

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }

        private Teste teste;

        public Teste Teste
        {
            get { return teste; }
            set
            {
                teste = value;

                txtTitulo.Text = Teste.Titulo;
                if (teste.Questoes != null)
                AtualizarLista();

                AtualizarCheckBoxSerie();
            }
        }

        private void AtualizarLista()
        {

            foreach (Questao questao in teste.Questoes)
            {
                listQuestoesTeste.Items.Add(questao);
            }

        }

        private void CarregarComboBox()
        {
            foreach (Disciplina disciplina in disciplinas)
            {
                comboDisciplina.Items.Add(disciplina);
            }
            foreach (Materia materia in materias)
            {
                comboMaterias.Items.Add(materia);
            }
        }
        private void CarregarListaQuestoes()
        {
            foreach (Materia materia in materias)
            {
                foreach (Questao questao in materia.Questoes)
                {
                    listQuestoesDisponiveis.Items.Add(questao);
                }
            }
        }

        private void AtualizarCheckBoxSerie()
        {
            if (teste.Serie == 1)
            {
                checkBox1.Checked = true;
            }
            else if (teste.Serie == 2)
            {
                checkBox2.Checked = true;
            }
        }
        private int VerificarCheckBox()
        {
            int serie = 0;
            if (checkBox1.Checked == true)
            {
                serie = 1;
            }
            else if (checkBox2.Checked == true)
            {
                serie = 2;
            }
            return serie;
        }

        private void btnGerarTeste_Click(object sender, EventArgs e)
        {
            Teste.Titulo = txtTitulo.Text;
            Teste.Serie = VerificarCheckBox();
            Teste.Questoes = listQuestoesTeste.Items.Cast<Questao>().ToList();
            Teste.NQuestoes = Teste.Questoes.Count();


            var resultadoValidacao = GravarRegistro(Teste);

            if (resultadoValidacao.IsValid == false)
            {
                DialogResult = DialogResult.None;
            }
        }        

        private void checkBox1_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void comboDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMaterias.Items.Clear();
            listQuestoesDisponiveis.Items.Clear();
            foreach (Materia materia in materias)
            {
                if (materia.Disciplina == comboDisciplina.SelectedItem)
                {
                    comboMaterias.Items.Add(materia);
                    foreach (Questao questao in materia.Questoes)
                    {
                        listQuestoesDisponiveis.Items.Add(questao);
                    }
                }
            }
        }

        private void comboMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {
            listQuestoesDisponiveis.Items.Clear();
            foreach (Materia materia in materias)
            {
                if (materia == comboMaterias.SelectedItem)
                {
                    foreach (Questao questao in materia.Questoes)
                    {
                        listQuestoesDisponiveis.Items.Add(questao);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listQuestoesTeste.Items.Add(listQuestoesDisponiveis.SelectedItem);
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            listQuestoesTeste.Items.Remove(listQuestoesTeste.SelectedItem);
        }
    }
}
