using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesDonaMariana_WinApp.ModuloTeste
{
    public class ValidadorTeste : AbstractValidator<Teste>
    {
        public ValidadorTeste()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().NotNull();
            RuleFor(x => x.NQuestoes)
                .NotNull().NotEmpty();
            RuleFor(x => x.Serie)
                .NotEmpty().NotNull();            
        }
    }
}
