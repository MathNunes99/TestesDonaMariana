using FluentValidation;
using FluentValidation.Results;
using System;
using System.Windows.Forms;

namespace TestesDonaMariana_WinApp.ModuloQuestao
{
    public partial class TelaCriarQuestao : Form
    {
        public TelaCriarQuestao()
        {
            InitializeComponent();
        }

        Questao questao;

        public Questao Questao
        {
            get { return questao; }
            set
            {
                questao = value;

                txtTitulo.Text = Questao.titulo;
                txtPergunta1.Text = Questao.pergunta1;
                txtPergunta2.Text = Questao.pergunta2;
                txtPergunta3.Text = Questao.pergunta3;
                txtPergunta4.Text = Questao.pergunta4;

                VerGabarito();
            }
        }        

        private void VerGabarito()
        {
            if (Questao.gabarito == 'A')
            {
                checkBoxA.Checked = true;
            }
            else if (Questao.gabarito == 'B')
            {
                checkBoxB.Checked = true;
            }
            else if (Questao.gabarito == 'C')
            {
                checkBoxC.Checked = true;
            }
            else if (Questao.gabarito == 'D')
            {
                checkBoxD.Checked = true;
            }
        }

        private AbstractValidator<Questao> ObterValidador()
        {
            return new ValidadorQuestao();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Questao.titulo = txtTitulo.Text;
            Questao.pergunta1 = txtPergunta1.Text;
            Questao.pergunta2 = txtPergunta2.Text;
            Questao.pergunta3 = txtPergunta3.Text;
            Questao.pergunta4 = txtPergunta4.Text;

            setarGabarito();

            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(Questao);

            if (resultadoValidacao.IsValid == false)
            {
                DialogResult = DialogResult.None;
            }
        }

        private void setarGabarito()
        {
            if (checkBoxA.Checked == true)
            {
                Questao.gabarito = 'A';
            }
            else if (checkBoxB.Checked == true)
            {
                Questao.gabarito = 'B';
            }
            else if (checkBoxC.Checked == true)
            {
                Questao.gabarito = 'C';
            }
            else if (checkBoxD.Checked == true)
            {
                Questao.gabarito = 'D';
            }
        }

        private void checkBoxA_Click(object sender, EventArgs e)
        {
            checkBoxB.Checked = false;
            checkBoxC.Checked = false;
            checkBoxD.Checked = false;
        }

        private void checkBoxB_Click(object sender, EventArgs e)
        {
            checkBoxA.Checked = false;
            checkBoxC.Checked = false;
            checkBoxD.Checked = false;
        }

        private void checkBoxC_Click(object sender, EventArgs e)
        {
            checkBoxA.Checked = false;
            checkBoxB.Checked = false;
            checkBoxD.Checked = false;
        }

        private void checkBoxD_Click(object sender, EventArgs e)
        {
            checkBoxA.Checked = false;
            checkBoxB.Checked = false;
            checkBoxC.Checked = false;
        }
    }
}
