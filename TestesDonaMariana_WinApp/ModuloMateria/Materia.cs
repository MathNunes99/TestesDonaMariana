using System.Collections.Generic;
using TestesDonaMariana_WinApp.Compartilhado;
using TestesDonaMariana_WinApp.ModuloQuestao;

namespace TestesDonaMariana_WinApp
{
    public class Materia : EntidadeBase<Materia>
    {
        List<Questao> questoes = new List<Questao>();
        public Materia()
        {
        }

        public Materia(string t, Disciplina d, int s)
        {
            Titulo = t;
            Disciplina = d;
            Serie = s;            
        }
        public string Titulo { get; set; }
        public Disciplina Disciplina { get; set; }
        public List<Questao> Questoes { get { return questoes; } }
        public int Serie { get; set; }
        public int contadorQuestao = 1;

        public void AdicionarNovaQuestao(Questao questao)
        {
            questao.numero = contadorQuestao;
            if (Questoes.Exists(x => x.Equals(questao)) == false)
            {
                Questoes.Add(questao);
                contadorQuestao++;
            }
                
        }

        public override void Atualizar(Materia registro)
        {
        }
    }
}