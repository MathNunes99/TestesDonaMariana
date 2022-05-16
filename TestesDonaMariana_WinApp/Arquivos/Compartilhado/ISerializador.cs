namespace TestesDonaMariana_WinApp.Arquivos.Compartilhado
{
    public interface ISerializador
    {
        DataContext CarregarDadosDoArquivo();

        void GravarDadosEmArquivo(DataContext dados);

    }
}