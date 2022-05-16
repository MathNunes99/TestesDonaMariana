using FluentValidation;

namespace TestesDonaMariana_WinApp.ModuloDisciplina
{
    public class ValidadorDisciplina : AbstractValidator<Disciplina>
    {
        public ValidadorDisciplina()
        {
            RuleFor(x => x.Titulo)
                .NotNull().NotEmpty()
                .MinimumLength(3);
        }
    }
}
