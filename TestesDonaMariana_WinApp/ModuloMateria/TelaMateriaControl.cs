using System.Collections.Generic;
using System.Windows.Forms;
using TestesDonaMariana_WinApp.Compartilhado;

namespace TestesDonaMariana_WinApp.ModuloMateria
{
    public partial class TelaMateriaControl : UserControl
    {
        public TelaMateriaControl()
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

                new DataGridViewTextBoxColumn { DataPropertyName = "Materia", HeaderText = "Matéria"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Disciplina", HeaderText = "Disciplina"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Serie", HeaderText = "Série"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Numero De Questoes", HeaderText = "Número de Questões"}
            };

            return colunas;
        }

        public void AtualizarRegistros(List<Materia> materias)
        {
            grid.Rows.Clear();

            foreach (var materia in materias)
            {
                grid.Rows.Add(materia.Numero, materia.Titulo, materia.Disciplina.Titulo, materia.Serie, materia.Questoes.Count);
            }
        }

        public int ObtemNumeroMateriaSelecionada()
        {
            return grid.SelecionarNumero<int>();
        }
    }
}
