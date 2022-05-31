using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using TestesDonaMariana_WinApp.Compartilhado;

namespace TestesDonaMariana_WinApp.ModuloDisciplina
{
    public class ControladorDisciplina : ControladorBase
    {
        private readonly IRepositorioDisciplina repositorioDisciplina;

        private TelaDisciplinaControl tabelaDisciplinas;

        public ControladorDisciplina(IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioDisciplina = repositorioDisciplina;
        }

        public override void Inserir()
        {
            TelaCadastroDisciplina tela = new TelaCadastroDisciplina();
            
            tela.Disciplina = new Disciplina();

            tela.GravarRegistro = repositorioDisciplina.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                

                CarregarDisciplina();

                
            }
        }

        public override void Editar()
        {
            Disciplina disciplinaSelecionada = ObtemDisciplinaSelecionada();

            if (disciplinaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Disciplina primeiro",
                "Edição de Disciplina", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroDisciplina tela = new TelaCadastroDisciplina();

            tela.Disciplina = disciplinaSelecionada;

            tela.GravarRegistro = repositorioDisciplina.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarDisciplina();
            }
        }

        public override void Excluir()
        {
            Disciplina disciplinaSelecionada = ObtemDisciplinaSelecionada();

            if (disciplinaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Disciplina primeiro",
                "Exclusão de Disciplinas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Disciplina?",
                "Exclusão de Disciplinas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioDisciplina.Excluir(disciplinaSelecionada);
                CarregarDisciplina();
            }
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaDisciplinas == null)
                tabelaDisciplinas = new TelaDisciplinaControl();

            CarregarDisciplina();

            return tabelaDisciplinas;
        }

        private Disciplina ObtemDisciplinaSelecionada()
        {
            var numero = tabelaDisciplinas.ObtemNumeroDisciplinaSelecionado();

            return repositorioDisciplina.SelecionarPorNumero(numero);
        }


        private void CarregarDisciplina()
        {
            List<Disciplina> disciplinas = repositorioDisciplina.SelecionarTodos();

            tabelaDisciplinas.AtualizarRegistros(disciplinas);
        }

    }
}
