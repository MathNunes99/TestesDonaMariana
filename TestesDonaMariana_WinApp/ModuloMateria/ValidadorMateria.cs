using FluentValidation;

namespace TestesDonaMariana_WinApp
{
    public class ValidadorMateria : AbstractValidator<Materia>
    {
        public ValidadorMateria()
        {
            RuleFor(x => x.Titulo)
                .NotNull().NotEmpty();
            RuleFor(x => x.Serie)
                .NotNull().NotEmpty();
            RuleFor(x => x.Disciplina)
                .NotNull().NotEmpty();

        }
    }
}