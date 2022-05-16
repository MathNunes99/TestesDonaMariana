using FluentValidation.Results;
using System;
using System.Collections.Generic;
using TestesDonaMariana_WinApp.Compartilhado;
using TestesDonaMariana_WinApp.ModuloQuestao;

namespace TestesDonaMariana_WinApp
{
    public class Teste : EntidadeBase<Teste>
    {
        public Teste()
        {
        }
        public string Titulo { get; set; }
        public int NQuestoes { get; set; }
        public int Serie { get; set; }
        public string Gabarito { get; set; }
        public List<Questao> Questoes { get; set; }

        public Func<Materia, ValidationResult> GravarRegistro { get; set; }

        public override void Atualizar(Teste registro)
        {            
        }
    }
}