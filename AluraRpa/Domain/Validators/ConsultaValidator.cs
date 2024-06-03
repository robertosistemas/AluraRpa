using AluraRpa.Domain.Models;
using FluentValidation;

namespace AluraRpa.Domain.Validators
{
    public class ConsultaValidator : AbstractValidator<Consulta>
    {
        public ConsultaValidator()
        {
            RuleFor(consulta => consulta.Titulo).NotEmpty();
            RuleFor(consulta => consulta.Professores).NotEmpty();
            RuleFor(consulta => consulta.CargaHoraria).NotEmpty();
            RuleFor(consulta => consulta.Descricao).NotEmpty();
        }
    }
}
