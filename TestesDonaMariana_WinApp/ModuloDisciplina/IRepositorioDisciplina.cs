using System.Collections.Generic;

namespace TestesDonaMariana_WinApp.ModuloDisciplina
{
    public interface IRepositorioDisciplina : IRepositorio<Disciplina>
    {
        List<Disciplina> SelecionarTodos();
    }
}