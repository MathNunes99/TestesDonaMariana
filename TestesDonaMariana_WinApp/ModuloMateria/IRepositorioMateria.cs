using TestesDonaMariana_WinApp.ModuloDisciplina;
using TestesDonaMariana_WinApp.ModuloQuestao;

namespace TestesDonaMariana_WinApp
{
    public interface IRepositorioMateria : IRepositorio<Materia>
    {
        void AdicionarQuestao(Materia materiaSelecionada, Questao questao);
    }
}