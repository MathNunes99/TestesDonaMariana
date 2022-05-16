using FluentValidation;

namespace TestesDonaMariana_WinApp.ModuloQuestao
{
    public class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            RuleFor(x => x.titulo)
                .NotEmpty().NotNull();
            RuleFor(x => x.pergunta1)
                .NotEmpty().NotNull();
            RuleFor(x => x.pergunta2)
                .NotEmpty().NotNull();
            RuleFor(x => x.pergunta3)
                .NotEmpty().NotNull();
            RuleFor(x => x.pergunta4)
                .NotEmpty().NotNull();
            RuleFor(x => x.gabarito)
                .NotEmpty().NotNull();
        }

    }
}
