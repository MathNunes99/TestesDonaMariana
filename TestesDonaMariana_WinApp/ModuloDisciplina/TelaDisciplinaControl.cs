using System.Collections.Generic;
using System.Windows.Forms;
using TestesDonaMariana_WinApp.Compartilhado;

namespace TestesDonaMariana_WinApp.ModuloDisciplina
{
    public partial class TelaDisciplinaControl : UserControl
    {
        public TelaDisciplinaControl()
        {
            InitializeComponent();
            grid.ConfigurarGridZebrado();
            grid.ConfigurarGridSomenteLeitura();
            grid.Columns.AddRange(ObterColunas());
        }
        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Disciplina", HeaderText = "Disciplina"},                
            };

            return colunas;
        }

        public void AtualizarRegistros(List<Disciplina> disciplinas)
        {
            grid.Rows.Clear();

            foreach (var disciplina in disciplinas)
            {
                grid.Rows.Add(disciplina.Numero,disciplina.Titulo);
            }
        }

        public int ObtemNumeroDisciplinaSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }
    }
}
