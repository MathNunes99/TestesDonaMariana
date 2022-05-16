using TestesDonaMariana_WinApp.Compartilhado;

namespace TestesDonaMariana_WinApp
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public Disciplina()
        {
        }

        public Disciplina(string t)
        {
            Titulo = t;
        }
        public string Titulo { get; set; }

        public override void Atualizar(Disciplina registro)
        {
        }
    }
}