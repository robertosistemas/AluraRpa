using AluraRpa.Domain.Exceptions;
using AluraRpa.Domain.Models;

namespace AluraRpa.Domain.Services
{
    public interface IConsultaService
    {
        Result<Consulta, DomainException> GetConsulta(string url);
    }
}
