using System.Collections.Generic;
using System.Windows.Forms;
using TestesDonaMariana_WinApp.Compartilhado;
using TestesDonaMariana_WinApp.ModuloDisciplina;
using TestesDonaMariana_WinApp.ModuloMateria;
using TestesDonaMariana_WinApp.ModuloQuestao;

namespace TestesDonaMariana_WinApp
{
    public class ControladorMaterias : ControladorBase
    {
        private readonly IRepositorioDisciplina repositorioDisciplina;
        private readonly IRepositorioMateria repositorioMateria;

        private TelaMateriaControl tabelaMateria;

        public ControladorMaterias(IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria repositorioMateria)
        {
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioMateria = repositorioMateria;
        }

        public override void Inserir()
        {
            List<Disciplina> disciplinas = repositorioDisciplina.SelecionarTodos();

            TelaCadastroMateria tela = new TelaCadastroMateria(disciplinas);

            tela.Materia = new Materia();
            tela.Materia.Disciplina = new Disciplina();

            tela.GravarRegistro = repositorioMateria.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMateria();
            }
        }

        public override void Editar()
        {
            Materia materiaSelecionada = ObtemMateriaSelecionada();

            if (materiaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Materia primeiro",
                "Edição de Materia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<Disciplina> disciplinas = repositorioDisciplina.SelecionarTodos();

            TelaCadastroMateria tela = new TelaCadastroMateria(disciplinas);

            tela.Materia = materiaSelecionada;

            tela.GravarRegistro = repositorioMateria.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMateria();
            }
        }

        public override void Excluir()
        {
            Materia materiaSelecionada = ObtemMateriaSelecionada();

            if (materiaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Materia primeiro",
                "Exclusão de Materias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Materia?",
                "Exclusão de Materias", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioMateria.Excluir(materiaSelecionada);
                CarregarMateria();
            }
        }
        
        public override UserControl ObtemListagem()
        {
            if (tabelaMateria == null)
                tabelaMateria = new TelaMateriaControl();

            CarregarMateria();

            return tabelaMateria;
        }

        private Materia ObtemMateriaSelecionada()
        {
            var numero = tabelaMateria.ObtemNumeroMateriaSelecionada();

            return repositorioMateria.SelecionarPorNumero(numero);
        }


        private void CarregarMateria()
        {
            List<Materia> Materias = repositorioMateria.SelecionarTodos();

            tabelaMateria.AtualizarRegistros(Materias);
        }

        public override void AdicionarQuestao()
        {
            Materia materiaSelecionada = ObtemMateriaSelecionada();

            if (materiaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Materia primeiro",
                "Edição de Materias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCriarQuestao tela = new TelaCriarQuestao();

            tela.Questao = new Questao();            

            if (tela.ShowDialog() == DialogResult.OK)
            {
                Questao questao = tela.Questao;             

                repositorioMateria.AdicionarQuestao(materiaSelecionada, questao);

                CarregarMateria();
            }
        }

        public override void VerQuestoes()
        {
            Materia materiaSelecionada = ObtemMateriaSelecionada();

            TelaVerQuestao tela = new TelaVerQuestao(materiaSelecionada);                        

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMateria();
            }
        }
        
    }    
}