using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestesDonaMariana_WinApp.Arquivos;
using TestesDonaMariana_WinApp.BancoDeDados;
using TestesDonaMariana_WinApp.Compartilhado;
using TestesDonaMariana_WinApp.ModuloDisciplina;
using TestesDonaMariana_WinApp.ModuloTeste;

namespace TestesDonaMariana_WinApp
{
    public partial class TelaMenu : Form
    {
        ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext contextoDados;

        public TelaMenu(DataContext contextoDados)
        {
            InitializeComponent();

            this.contextoDados = contextoDados;

            InicializarControladores();
            ConfigurarTelaPrincipal("Testes");
        }
        private void btnTestes_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal("Testes");
            btnInserirQuestoes.Enabled = false;
            btnVerQuestoes.Enabled = false;
            btnGerarPDF.Enabled = true;
            btnDuplicar.Enabled = true;
        }

        private void btnDisciplinas_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal("Disciplinas");
            btnInserirQuestoes.Enabled = false;
            btnVerQuestoes.Enabled = false;
            btnGerarPDF.Enabled = false;
            btnDuplicar.Enabled = false;
        }

        private void btnMaterias_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal("Materias");
            btnInserirQuestoes.Enabled = true;
            btnVerQuestoes.Enabled = true;
            btnGerarPDF.Enabled = false;
            btnDuplicar.Enabled = false;
        }

        private void ConfigurarTelaPrincipal(string tipo)
        {            
            controlador = controladores[tipo];
            
            ConfigurarListagem();
        }

        private void ConfigurarListagem()
        {
            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void InicializarControladores()
        {
            var repositorioDisciplina = new RepositorioDisciplinaBancoDeDados();
            var repositorioMateria = new RepositorioMateriaBancoDeDados();
            //var repositorioMateria = new RepositorioMateriaEmArquivo(contextoDados);
            var repositorioTeste = new RepositorioTesteEmArquivo(contextoDados);

            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Disciplinas", new ControladorDisciplina(repositorioDisciplina));
            controladores.Add("Materias", new ControladorMaterias(repositorioDisciplina,repositorioMateria));
            controladores.Add("Testes", new ControladorTeste(repositorioTeste, repositorioMateria,repositorioDisciplina));
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void btnInserirQuestoes_Click(object sender, EventArgs e)
        {
            controlador.AdicionarQuestao();
        }

        private void btnVerQuestoes_Click(object sender, EventArgs e)
        {
            controlador.VerQuestoes();
        }

        private void btnGerarPDF_Click(object sender, EventArgs e)
        {
            ((ControladorTeste)controlador).GerarPdf();
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            ((ControladorTeste)controlador).Duplicar();
        }
    }
}
