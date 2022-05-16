using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TestesDonaMariana_WinApp.Compartilhado;
using TestesDonaMariana_WinApp.ModuloDisciplina;

namespace TestesDonaMariana_WinApp.ModuloTeste
{
    public class ControladorTeste : ControladorBase
    {
        private IRepositorioTeste repositorioTeste;
        private IRepositorioMateria repositorioMateria;
        private IRepositorioDisciplina repositorioDisciplina;

        private TelaTesteControl tabelaTeste;

        public ControladorTeste(IRepositorioTeste repositorioTeste, IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioTeste = repositorioTeste;
            this.repositorioMateria = repositorioMateria;
            this.repositorioDisciplina = repositorioDisciplina;
        }

        public override void Inserir()
        {
            List<Disciplina> disciplinas = repositorioDisciplina.SelecionarTodos();
            List<Materia> materias = repositorioMateria.SelecionarTodos();

            TelaCadastrarTeste tela = new TelaCadastrarTeste(disciplinas,materias);

            tela.Teste = new Teste();

            tela.GravarRegistro = repositorioTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTeste();
            }
        }

        public override void Editar()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            List<Disciplina> disciplinas = repositorioDisciplina.SelecionarTodos();
            List<Materia> materias = repositorioMateria.SelecionarTodos();

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione um Teste primeiro",
                "Edição de Teste", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<Teste> testes = repositorioTeste.SelecionarTodos();

            TelaCadastrarTeste tela = new TelaCadastrarTeste(disciplinas,materias);

            tela.Teste = testeSelecionado;

            tela.GravarRegistro = repositorioTeste.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTeste();
            }
        }

        public override void Excluir()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione uma Teste primeiro",
                "Exclusão de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir o Teste?",
                "Exclusão de Testes", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioTeste.Excluir(testeSelecionado);
                CarregarTeste();
            }
        }        

        public override UserControl ObtemListagem()
        {
            if (tabelaTeste == null)
                tabelaTeste = new TelaTesteControl();

            CarregarTeste();

            return tabelaTeste;
        }
        private void CarregarTeste()
        {
            List<Teste> testes = repositorioTeste.SelecionarTodos();

            tabelaTeste.AtualizarRegistros(testes);
        }

        private Teste ObtemTesteSelecionado()
        {
            var numero = tabelaTeste.ObtemNumeroTesteSelecionado();

            return repositorioTeste.SelecionarPorNumero(numero);
        }

        public void GerarPdf()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione uma Teste primeiro",
                "Gerar Pdf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string nomeArquivo = @"C:\temp\dados.pdf";

            FileStream arquivoPdf = new FileStream(nomeArquivo, FileMode.Create);
            Document doc = new Document(PageSize.A4);
            PdfWriter escritorPdf = PdfWriter.GetInstance(doc, arquivoPdf);

            doc.Open();
            string dados = "";

            Paragraph paragrafoTitulo = new Paragraph(dados);

            paragrafoTitulo.Alignment = Element.ALIGN_CENTER;
            paragrafoTitulo.Add(testeSelecionado.Titulo + "\n");
            

            Paragraph paragrafoQuestoes = new Paragraph(dados);
            paragrafoQuestoes.Alignment = Element.ALIGN_LEFT;


            int contador = 1;
            foreach (var questao in testeSelecionado.Questoes)
            {
                CriarCelula(questao.titulo);

                paragrafoQuestoes.Add(contador + " - " + questao.titulo + "\n\n");
                paragrafoQuestoes.Add("A.(  )" + questao.pergunta1 + "\n");
                paragrafoQuestoes.Add("B.(  )" + questao.pergunta2 + "\n");
                paragrafoQuestoes.Add("C.(  )" + questao.pergunta3 + "\n");
                paragrafoQuestoes.Add("D.(  )" + questao.pergunta4 + "\n\n");
                contador++;
            }

            doc.Open();
            doc.Add(paragrafoTitulo);
            doc.Add(paragrafoQuestoes);
            doc.Close();

            MessageBox.Show("PDF gerado com sucesso",
            "Gerãção de PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private static PdfPCell CriarCelula(string texto)
        {
            var celula = new PdfPCell(new Phrase("Código"));

            return celula;
        }

        public void Duplicar()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione um teste primeiro",
                "Duplicação de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Teste testeDuplicado = new Teste();
            CopiaTeste(testeSelecionado, testeDuplicado);

            var disciplinas = repositorioDisciplina.SelecionarTodos();
            var materias = repositorioMateria.SelecionarTodos();            

            TelaCadastrarTeste tela = new TelaCadastrarTeste(disciplinas, materias);
            tela.Teste = testeDuplicado;

            tela.GravarRegistro = repositorioTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTeste();
            }

        }

        private static void CopiaTeste(Teste testeSelecionado, Teste testeDuplicado)
        {
            testeDuplicado.Titulo = testeSelecionado.Titulo;            
            testeDuplicado.NQuestoes = testeSelecionado.NQuestoes;
            testeDuplicado.Serie = testeSelecionado.Serie;
            testeDuplicado.Questoes = testeSelecionado.Questoes;
        }
    }
}
