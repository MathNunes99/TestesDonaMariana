using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using TestesDonaMariana_WinApp.Arquivos;

namespace TestesDonaMariana_WinApp.ModuloTeste
{
    internal class RepositorioTesteEmArquivo : RepositorioArquivoBase<Teste>, IRepositorioTeste
    {
        public RepositorioTesteEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Materias.Count > 0)
                contador = dataContext.Materias.Max(x => x.Numero);
        }

        public override List<Teste> ObterRegistros()
        {
            return dataContext.Testes;
        }

        public override AbstractValidator<Teste> ObterValidador()
        {
            return new ValidadorTeste();
        }
    }
}
