using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestesDonaMariana_WinApp.Compartilhado;

namespace TestesDonaMariana_WinApp.ModuloTeste
{
    public partial class TelaTesteControl : UserControl
    {
        public TelaTesteControl()
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

                new DataGridViewTextBoxColumn { DataPropertyName = "Titulo", HeaderText = "Título"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Serie", HeaderText = "Série"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Nº de Questoes", HeaderText = "Nº de Questões"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Gabarito", HeaderText = "Gabarito"},
            };

            return colunas;
        }
        public void AtualizarRegistros(List<Teste> testes)
        {
            grid.Rows.Clear();

            foreach (var teste in testes)
            {                
                grid.Rows.Add(teste.Numero, teste.Titulo, teste.Serie, teste.NQuestoes, teste.Gabarito);
            }
        }        

        public int ObtemNumeroTesteSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }
    }
}
