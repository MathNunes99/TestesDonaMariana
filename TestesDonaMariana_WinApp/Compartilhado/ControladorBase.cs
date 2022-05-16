using System.Windows.Forms;

namespace TestesDonaMariana_WinApp.Compartilhado
{
    public abstract class ControladorBase
    {
        public abstract void Inserir();
        public abstract void Editar();
        public abstract void Excluir();
        public abstract UserControl ObtemListagem();
        public virtual void AdicionarQuestao() { }
        public virtual void VerQuestoes() { }
    }
}
