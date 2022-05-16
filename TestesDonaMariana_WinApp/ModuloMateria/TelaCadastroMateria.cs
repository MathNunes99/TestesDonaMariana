using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestesDonaMariana_WinApp.ModuloMateria
{
    public partial class TelaCadastroMateria : Form
    {
        public TelaCadastroMateria(List<Disciplina> disciplinas)
        {
            InitializeComponent();

            CarregarDisciplinas(disciplinas);
        }

        private void CarregarDisciplinas(List<Disciplina> disciplinas)
        {
            comboDisciplina.Items.Clear();

            foreach (var item in disciplinas)
            {
                comboDisciplina.Items.Add(item);
            }
        }

        private Materia materia;

        public Func<Materia, ValidationResult> GravarRegistro { get; set; }

        public Materia Materia
        {
            get { return materia; }
            set
            {
                materia = value;

                txtTitulo.Text = Materia.Titulo;
                comboDisciplina.SelectedItem = materia.Disciplina;

                AtualizarCheckBoxSerie();
            }
        }

        private void AtualizarCheckBoxSerie()
        {
            if (materia.Serie == 1)
            {
                cBox1Serie.Checked = true;
            }
            else if (materia.Serie == 2)
            {
                cBox2Serie.Checked = true;
            }
        }        

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            Materia.Titulo = txtTitulo.Text;
            Materia.Serie = VerificarCheckBox();
            Materia.Disciplina = (Disciplina)comboDisciplina.SelectedItem;

            var resultadoValidacao = GravarRegistro(Materia);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                DialogResult = DialogResult.None;
            }
        }

        private int VerificarCheckBox()
        {
            int serie = 0;
            if (cBox1Serie.Checked == true)
            {
                serie = 1;
            }
            else if (cBox2Serie.Checked == true)
            {
                serie = 2;
            }
            return serie;
        }

        private void cBox1Serie_Click(object sender, EventArgs e)
        {
            cBox2Serie.Checked = false;
        }

        private void cBox2Serie_Click(object sender, EventArgs e)
        {
            cBox1Serie.Checked = false;
        }
    }
}
