using FluentValidation.Results;
using System;
using System.Windows.Forms;

namespace TestesDonaMariana_WinApp.ModuloDisciplina
{
    public partial class TelaCadastroDisciplina : Form
    {
        public TelaCadastroDisciplina()
        {
            InitializeComponent();
        }

        private Disciplina disciplina;

        public Func<Disciplina, ValidationResult> GravarRegistro { get; set; }

        public Disciplina Disciplina
        {
            get { return disciplina; }
            set
            {
                disciplina = value;

                txtTitulo.Text = disciplina.Titulo;                
            }
        }

        private void btnCadastrar_Click(object sender, System.EventArgs e)
        {
            disciplina.Titulo = txtTitulo.Text;

            var resultadoValidacao = GravarRegistro(Disciplina);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;                

                DialogResult = DialogResult.None;
            }
        }

    }
}
